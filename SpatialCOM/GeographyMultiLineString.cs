using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("7C63039E-3A5D-431A-8DCF-D0CB13A7F248")]
    public class GeographyMultiLineString : IGeographyMultiLineString
    {

        private List<IGeographyLineString> lines;
        private Microsoft.SqlServer.Types.SqlGeography l;
        private bool recalcNeeded;
        private string name = "";
        private string description = "";

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                Recalc();
                return l;
            }
            set
            {
                if (value.STGeometryType() == "MultiLineString")
                {
                    lines = new List<IGeographyLineString>();
                    for (int count = 0; count < value.STNumGeometries();count++)
                    {
                        var line = (IGeographyLineString)Internal.SQLToGeography.LoadSqlGeography(value.STGeometryN(count));
                        lines.Add(line);

                    }
                    recalcNeeded = true;
                }
            }
        }

        public bool STIsEmpty() => lines.Count() == 0;

        public int STSrid
        {
            get
            {
                Recalc();
                return l.STSrid.Value;
            }

        }

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }

        public GeographyMultiLineString()
        {
            lines = new List<IGeographyLineString>();
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {
            

            return lines.GetEnumerator();
        }

        public bool Add(IGeographyLineString line)
        {
           
            lines.Add(line);
            recalcNeeded = true;
            return true;
        }

        public void Clear()
        {
            lines.Clear();
            recalcNeeded = true;
        }

        private void Recalc()
        {
            if (recalcNeeded)
            {
                l = null;
                if (!STIsEmpty())
                {
                    l = null;
                    Microsoft.SqlServer.Types.SqlGeographyBuilder b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                    b.SetSrid(lines.First().STSrid);
                    b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.MultiLineString);                    
                    foreach (GeographyLineString line in lines)
                    {
                        b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
                        b.BeginFigure(line.FirstPoint.Latitude,line.FirstPoint.Longitude, line.FirstPoint.Z == double.MinValue ? (double?)null : line.FirstPoint.Z, line.FirstPoint.M == double.MinValue ? (double?)null : line.FirstPoint.M);
                        foreach (GeographyPoint p in line.Points.Skip(1))
                        {
                            b.AddLine(p.Latitude, p.Longitude, p.Z == double.MinValue ? (double?)null : p.Z, p.M == double.MinValue ? (double?)null : p.M);
                        }
                        b.EndFigure();
                        b.EndGeography();
                    }
                    b.EndGeography();
                    l = b.ConstructedGeography;

                }
            }
            recalcNeeded = false;

        }

        public double STLength()
        {
           
                Recalc();
                return l.STLength().Value;
           
        }

        public string STAsText()
        {
            
                Recalc();
                return new string(l.STAsText().Value);
           
        }

        public double STArea()
        {
            return 0d;
        }

        public double STDistance(IGeography geography)
        {
            Recalc();
            if (STIsEmpty() || geography.STIsEmpty())
            {
                return 0d;
            }
            else
                return l.STDistance(geography.Geography).Value;
        }

        public bool STIsValid()
        {
            
           
            Recalc();
            return l.STIsValid().Value;
           
        }

        public string STGeometryType()
        {
            Recalc();
            return l.STGeometryType().Value;
        }

        public IGeography STBuffer(double distance)
        {
            Recalc();
            var buffered = l.STBuffer(distance);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public bool STContains(IGeography geog)
        {
            Recalc();
            return l.STContains(geog.Geography).Value;
        }

        public IGeography STConvexHull()
        {
            Recalc();
            return Internal.SQLToGeography.LoadSqlGeography(l.STConvexHull());
        }

        public IGeography STDifference(IGeography geog)
        {
            Recalc();
            var difference = l.STDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public int STDimension()
        {
            Recalc();
            return l.STDimension().Value;
        }

        public bool STDisjoint(IGeography geog)
        {
            Recalc();
            return l.STDisjoint(geog.Geography).Value;
        }

        public IGeographyPoint STEndPoint()
        {
            Recalc();
            var ep = l.STEndPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public bool STEquals(IGeography geog)
        {
            Recalc();
            return l.STEquals(geog.Geography).Value;
        }

        public IGeography STGeometryN(int number)
        {
            Recalc();
            var buffered = l.STGeometryN(number);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public IGeography STIntersection(IGeography geog)
        {
            Recalc();
            var difference = l.STIntersection(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STIntersects(IGeography geog)
        {
            Recalc();
            return l.STIntersects(geog.Geography).Value;
        }

        public bool STIsClosed()
        {
            Recalc();
            return l.STIsClosed().Value;
        }

        public int STNumGeometries()
        {
            Recalc();
            return l.STNumGeometries().Value;
        }

        public int STNumPoints()
        {
            Recalc();
            return l.STNumPoints().Value;
        }

        public bool STOverlaps(IGeography geog)
        {
            Recalc();
            return l.STOverlaps(geog.Geography).Value;
        }

        public IGeographyPoint STPointN(int number)
        {
            Recalc();
            var point = l.STPointN(number);
            return (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(point);
        }

        public IGeographyPoint STStartPoint()
        {
            Recalc();
            var ep = l.STStartPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public IGeography STSymDifference(IGeography geog)
        {
            Recalc();
            var difference = l.STSymDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public IGeography STUnion(IGeography geog)
        {
            Recalc();
            var difference = l.STUnion(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STWithin(IGeography geog)
        {
            Recalc();
            return l.STWithin(geog.Geography).Value;
        }
    }
}
