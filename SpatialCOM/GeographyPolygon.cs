using Microsoft.SqlServer.Types;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("843EDAF3-45BE-42D7-9672-84AD7E68B319")]
    public class GeographyPolygon : IGeographyPolygon
    {
        private Microsoft.SqlServer.Types.SqlGeography p;        
        private GeographyLineString outerPoints;
        private List<GeographyLineString> innerPolys;
        private string name = "";
        private string description = "";


        public GeographyPolygon()
        {
            SqlGeographyBuilder b = new SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(OpenGisGeographyType.Polygon);
            b.EndGeography();
            p = b.ConstructedGeography;
            outerPoints = new GeographyLineString();
            innerPolys = new List<GeographyLineString>();
        }

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                return p;
            }
            set
            {
                if (value.STGeometryType() == "Polygon")
                {
                    outerPoints = new GeographyLineString();
                    innerPolys = new List<GeographyLineString>();
                    var equivGeom = SqlGeometry.STGeomFromText(value.AsTextZM(), value.STSrid.Value);
                    var outerRingGeom = equivGeom.STExteriorRing();
                    var outerRingGeog = SqlGeography.STLineFromText(outerRingGeom.AsTextZM(), value.STSrid.Value);
                    InitializeFromLine( (GeographyLineString)Internal.SQLToGeography.LoadSqlGeography(outerRingGeog));
                    for (int count = 0; count < equivGeom.STNumInteriorRing(); count++)
                    {
                        var innerRingGeom = equivGeom.STInteriorRingN(count);
                        var innerRingGeog = SqlGeography.STLineFromText(innerRingGeom.AsTextZM(), value.STSrid.Value);
                        AddInnerFromLine((GeographyLineString)Internal.SQLToGeography.LoadSqlGeography(innerRingGeog));
                    }
                }
            }
        }

        public int STSrid => p.STSrid.Value;

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }

        public bool STIsEmpty() { return p.STIsEmpty().Value; }

        public string STAsText() => new String(p.STAsText().Value);
       

        

        public double STArea() => p.STArea().Value;
       

        public double STDistance(IGeography geography)
        {
           
             return p.STDistance(geography.Geography).Value;
        }

        public bool InitializeFromLine(GeographyLineString lines)
        {
            
            return InitOuter(lines);
        }

        public bool InitializeFromPoints(GeographyMultiPoint points)
        {
            GeographyLineString lines = new GeographyLineString();
            lines.InitializeFromPoints(points);
            return InitOuter(lines);
           
        }

        private bool InitOuter(GeographyLineString lp)
        {
            if (STIsEmpty() && lp.Points.Count() > 2)
            {
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                var firstPoint = lp.FirstPoint;
                b.SetSrid(firstPoint.STSrid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Polygon);
                
                b.BeginFigure(firstPoint.Latitude, firstPoint.Longitude, firstPoint.Z, firstPoint.M);
                foreach (var bg in lp.Points.Skip(1))
                {
                    b.AddLine(bg.Latitude, bg.Longitude, bg.Z, bg.M);
                }

                b.EndFigure();
                b.EndGeography();
                if (b.ConstructedGeography.STIsValid())
                {
                    p = b.ConstructedGeography;
                    outerPoints = lp;
                    return true;
                }
            }
            return false;
        }

        public bool AddInnerFromPoints(GeographyMultiPoint points)
        {
            GeographyLineString line = new GeographyLineString();
            line.InitializeFromPoints(points);
            return InitInner(line);
        }

        public bool AddInnerFromLine(GeographyLineString line)
        {
            
            return InitInner(line);
        }

        private bool InitInner(GeographyLineString lp)
        {
            if (!STIsEmpty() && lp.Points.Count() > 2 && STIsValid() && lp.FirstPoint.STSrid == outerPoints.FirstPoint.STSrid)
            {
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(STSrid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Polygon);
                var firstPoint = outerPoints.FirstPoint;
                b.BeginFigure(firstPoint.Latitude, firstPoint.Longitude, firstPoint.Z == double.MinValue ? (double?)null : firstPoint.Z, firstPoint.M == double.MinValue ? (double?)null : firstPoint.M);
                foreach (var bg in outerPoints.Points.Skip(1))
                {
                    b.AddLine(bg.Latitude, bg.Longitude, bg.Z == double.MinValue ? (double?)null : bg.Z, bg.M == double.MinValue ? (double?)null : bg.M);
                }

                b.EndFigure();
                if (innerPolys.Count() > 0)
                {
                    foreach (GeographyLineString gp in innerPolys)
                    {
                        b.BeginFigure(gp.FirstPoint.Latitude, gp.FirstPoint.Longitude, gp.FirstPoint.Z == double.MinValue ? (double?)null : gp.FirstPoint.Z, gp.FirstPoint.M == double.MinValue ? (double?)null : gp.FirstPoint.M);
                        foreach (var bg in gp.Points.Skip(1))
                        {
                            b.AddLine(bg.Latitude, bg.Longitude, bg.Z == double.MinValue ? (double?)null : bg.Z, bg.M == double.MinValue ? (double?)null : bg.M);
                        }
                        b.EndFigure();
                    }
                }
                b.BeginFigure(lp.FirstPoint.Latitude, lp.FirstPoint.Longitude, lp.FirstPoint.Z == double.MinValue ? (double?)null : lp.FirstPoint.Z, lp.FirstPoint.M == double.MinValue ? (double?)null : lp.FirstPoint.M);
                foreach (var bg in lp.Points.Skip(1))
                {
                    b.AddLine(bg.Latitude, bg.Longitude, bg.Z == double.MinValue ? (double?)null : bg.Z, bg.M == double.MinValue ? (double?)null : bg.M);
                }
                b.EndFigure();
                b.EndGeography();
                if (b.ConstructedGeography.STIsValid())
                {
                    p = b.ConstructedGeography;                    
                    innerPolys.Add(lp);
                    return true;
                }
            }
            return false;
        }

        public bool STIsValid() => p.STIsValid().Value;

        public string STGeometryType() => p.STGeometryType().Value;


        public IGeography STBuffer(double distance)
        {
            var buffered = p.STBuffer(distance);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public bool STContains(IGeography geog)
        {
            return p.STContains(geog.Geography).Value;
        }

        public IGeography STConvexHull()
        {
            
            return Internal.SQLToGeography.LoadSqlGeography(p.STConvexHull());
        }

        public IGeography STDifference(IGeography geog)
        {
            var difference = p.STDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public int STDimension()
        {
            return p.STDimension().Value;
        }

        public bool STDisjoint(IGeography geog)
        {            
            return p.STDisjoint(geog.Geography).Value;
        }

        public IGeographyPoint STEndPoint()
        {
            
            var ep = p.STEndPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public bool STEquals(IGeography geog)
        {
            
            return p.STEquals(geog.Geography).Value;
        }

        public IGeography STGeometryN(int number)
        {
            
            var buffered = p.STGeometryN(number);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public IGeography STIntersection(IGeography geog)
        {
            
            var difference = p.STIntersection(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STIntersects(IGeography geog)
        {
            
            return p.STIntersects(geog.Geography).Value;
        }

        public bool STIsClosed()
        {            
            return p.STIsClosed().Value;
        }

        public double STLength()
        {
            return p.STLength().Value;
        }

        public int STNumGeometries()
        {
            
            return p.STNumGeometries().Value;
        }

        public int STNumPoints()
        {
            
            return p.STNumPoints().Value;
        }

        public bool STOverlaps(IGeography geog)
        {
            return p.STOverlaps(geog.Geography).Value;
        }

        public IGeographyPoint STPointN(int number)
        {
            
            var point = p.STPointN(number);
            return (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(point);
        }

        public IGeographyPoint STStartPoint()
        {
            
            var ep = p.STStartPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public IGeography STSymDifference(IGeography geog)
        {
            
            var difference = p.STSymDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public IGeography STUnion(IGeography geog)
        {
            
            var difference = p.STUnion(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STWithin(IGeography geog)
        {
            
            return p.STWithin(geog.Geography).Value;
        }
    }
}
