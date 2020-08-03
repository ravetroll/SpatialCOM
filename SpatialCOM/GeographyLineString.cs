using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("A9BD1E0A-E8CF-43ED-8380-0774D32409D5")]
    public class GeographyLineString : IGeographyLineString
    {

        private string name = "";
        private string description = "";
        public GeographyLineString() 
        {
            var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
            b.EndGeography();
            l = b.ConstructedGeography;
            _linePoints = new List<IGeographyPoint>();
        }
        
        private Microsoft.SqlServer.Types.SqlGeography l;

        private List<IGeographyPoint> _linePoints;
        

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                return l;
            }
            set
            {
                if (value.STGeometryType() == "LineString")
                {
                    GeographyMultiPoint points = new GeographyMultiPoint();
                    for (int count = 1; count < value.STNumPoints().Value + 1;count++)
                    {
                        var p = new GeographyPoint();
                        p.Geography = value.STPointN(count);
                        points.Add(p);
                    }
                    InitializeFromPoints(points);
                }
            }
        }

        public List<IGeographyPoint> Points
        {
            get
            {
                return _linePoints;
            }
        }

        double IGeographyLineString.STLength => l.STLength().Value;

        public int STSrid => l.STSrid.Value;

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }

        public IGeographyPoint FirstPoint => _linePoints.FirstOrDefault();

        public IGeographyPoint LastPoint => _linePoints.LastOrDefault();

        public bool STIsEmpty() => l.STIsEmpty().Value;

        public void Initialize(IGeographyPoint firstPoint, IGeographyPoint lastPoint)
        {
            if (STIsEmpty())
            {
                _linePoints.Clear();
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(firstPoint.STSrid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
                b.BeginFigure(firstPoint.Latitude, firstPoint.Longitude, firstPoint.Z == double.MinValue ? (double?)null : firstPoint.Z, firstPoint.M == double.MinValue ? (double?)null : firstPoint.M);
                b.AddLine(lastPoint.Latitude, lastPoint.Longitude, lastPoint.Z == double.MinValue ? (double?)null : lastPoint.Z, lastPoint.M == double.MinValue ? (double?)null : lastPoint.M);
                b.EndFigure();
                b.EndGeography();
                l = b.ConstructedGeography;
                _linePoints.Add( firstPoint);
                _linePoints.Add  (lastPoint);
            }
            
        }

        public bool InitializeFromPoints(GeographyMultiPoint points)
        {
            if (STIsEmpty() && points.Points.Count() > 1)
            {
                _linePoints.Clear();
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(points.STSrid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
                var firstPoint = points.Points.FirstOrDefault();
                _linePoints.Add(firstPoint);
                b.BeginFigure(firstPoint.Latitude, firstPoint.Longitude, firstPoint.Z, firstPoint.M);
                foreach (var bg in points.Points.Skip(1))
                {
                    b.AddLine(bg.Latitude, bg.Longitude, bg.Z, bg.M);
                    _linePoints.Add(bg);
                }

                b.EndFigure();
                b.EndGeography();
                if (b.ConstructedGeography.STIsValid())
                {
                    l = b.ConstructedGeography;
                    return true;
                }
            }
            return false;
        }


        public double STLength() => l.STLength().Value;

        public bool STIsValid() => l.STIsValid().Value;

        public double STArea() => l.STArea().Value;
       
        public double STDistance(IGeography geography) => l.STDistance(geography.Geography).Value;

        public string STAsText() => new string(l.STAsText().Value);

        public string STGeometryType() => l.STGeometryType().Value;

        public bool AddPoint(IGeographyPoint point)
        {
            if (!STIsEmpty())
            {
                _linePoints.Add(point);
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(_linePoints.First().STSrid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
                b.BeginFigure(_linePoints.First().Latitude, _linePoints.First().Longitude, _linePoints.First().Z == double.MinValue ? (double?)null : _linePoints.First().Z, _linePoints.First().M == double.MinValue ? (double?)null : _linePoints.First().M);
                _linePoints.Skip(1).ToList().ForEach(lastPoint => b.AddLine(lastPoint.Latitude, lastPoint.Longitude, lastPoint.Z == double.MinValue ? (double?)null : lastPoint.Z, lastPoint.M == double.MinValue ? (double?)null : lastPoint.M));                
                b.EndFigure();
                b.EndGeography();
                l = b.ConstructedGeography;
                return true;
            }
            return false;
        }

        public IGeography STBuffer(double distance)
        {
            
            var buffered = l.STBuffer(distance);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public bool STContains(IGeography geog)
        {
            
            return l.STContains(geog.Geography).Value;
        }

        public IGeography STConvexHull()
        {
            
            return Internal.SQLToGeography.LoadSqlGeography(l.STConvexHull());
        }

        public IGeography STDifference(IGeography geog)
        {
            
            var difference = l.STDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public int STDimension()
        {
           
            return l.STDimension().Value;
        }

        public bool STDisjoint(IGeography geog)
        {            
            return l.STDisjoint(geog.Geography).Value;
        }

        public IGeographyPoint STEndPoint()
        {
            
            var ep = l.STEndPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public bool STEquals(IGeography geog)
        {
            
            return l.STEquals(geog.Geography).Value;
        }

        public IGeography STGeometryN(int number)
        {
            
            var buffered = l.STGeometryN(number);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public IGeography STIntersection(IGeography geog)
        {
           
            var difference = l.STIntersection(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STIntersects(IGeography geog)
        {
            
            return l.STIntersects(geog.Geography).Value;
        }

        public bool STIsClosed()
        {
            
            return l.STIsClosed().Value;
        }

        public int STNumGeometries()
        {
            
            return l.STNumGeometries().Value;
        }

        public int STNumPoints()
        {
            
            return l.STNumPoints().Value;
        }

        public bool STOverlaps(IGeography geog)
        {
            return l.STOverlaps(geog.Geography).Value;
        }

        public IGeographyPoint STPointN(int number)
        {
            var point = l.STPointN(number);
            return (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(point);
        }

        public IGeographyPoint STStartPoint()
        {
            
            var ep = l.STStartPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public IGeography STSymDifference(IGeography geog)
        {
            
            var difference = l.STSymDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public IGeography STUnion(IGeography geog)
        {
            
            var difference = l.STUnion(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STWithin(IGeography geog)
        {
            
            return l.STWithin(geog.Geography).Value;
        }
    }
}
