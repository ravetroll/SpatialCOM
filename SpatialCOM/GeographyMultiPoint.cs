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
    [Guid("C2D00AFA-16F4-4AEB-A112-25BB068C80FE")]
    public class GeographyMultiPoint : IGeographyMultiPoint
    {
        private Microsoft.SqlServer.Types.SqlGeography p;
        private bool recalcNeeded;
        private string name = "";
        private string description = "";

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                Recalc();
                return p;
            }
            set
            {
                if (value.STGeometryType() == "MultiPoint")
                {
                    Points = new List<IGeographyPoint>();
                    for (int count = 0; count < value.STNumPoints().Value; count++)
                    {
                        IGeographyPoint point = (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(value.STPointN(count));
                        Points.Add(point);
                    }
                    recalcNeeded = true;
                }
            }
        }

        public List<IGeographyPoint> Points { get; private set; }

        public bool STIsEmpty() => Points.Count() == 0;

        public int STSrid
        {
            get
            {
                if (STIsEmpty())
                {
                    return 0;
                }
                else
                    return Points.First().STSrid;
            }

        }

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }

        public GeographyMultiPoint()
        {
            Points = new List<IGeographyPoint>();
            Microsoft.SqlServer.Types.SqlGeographyBuilder b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.MultiPoint);
            b.EndGeography();
            p = b.ConstructedGeography;
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {
            

            return Points.GetEnumerator();
        }

        public bool Add(IGeographyPoint point)
        {
            
            Points.Add(point);
            recalcNeeded = true;
            return true;
        }

        public void Clear()
        {
            Points.Clear();
            recalcNeeded = true;
        }

        private void Recalc()
        {
            if (recalcNeeded)
            {
                p = null;
                if (!STIsEmpty())
                {
                    Microsoft.SqlServer.Types.SqlGeographyBuilder b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                    b.SetSrid(Points.First().STSrid);
                    b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.MultiPoint);                    
                    foreach (GeographyPoint point in Points)
                    {
                        b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);
                        b.BeginFigure(point.Latitude, point.Longitude, point.Z == double.MinValue ? (double?)null : point.Z, point.M == double.MinValue ? (double?)null : point.M);
                        b.EndFigure();
                        b.EndGeography();
                    }
                    b.EndGeography();
                    p = b.ConstructedGeography;
                }
            }
            recalcNeeded = false;

        }

        public string STAsText()
        {
            
            Recalc();                
            return new String(p.STAsText().Value);
           
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
                return p.STDistance(geography.Geography).Value;
        }

        public bool STIsValid()
        {

            
            Recalc();
            return p.STIsValid().Value;
            
        }

        public string STGeometryType() => p.STGeometryType().Value;

        public IGeography STBuffer(double distance)
        {
            Recalc();
            var buffered = p.STBuffer(distance);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public bool STContains(IGeography geog)
        {
            Recalc();
            return p.STContains(geog.Geography).Value;
        }

        public IGeography STConvexHull()
        {
            Recalc();
            return Internal.SQLToGeography.LoadSqlGeography(p.STConvexHull());
        }

        public IGeography STDifference(IGeography geog)
        {
            Recalc();
            var difference = p.STDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public int STDimension()
        {
            Recalc();
            return p.STDimension().Value;
        }

        public bool STDisjoint(IGeography geog)
        {
            Recalc();
            return p.STDisjoint(geog.Geography).Value;
        }

        public IGeographyPoint STEndPoint()
        {
            Recalc();
            var ep = p.STEndPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public bool STEquals(IGeography geog)
        {
            Recalc();
            return p.STEquals(geog.Geography).Value;
        }

        public IGeography STGeometryN(int number)
        {
            Recalc();
            var buffered = p.STGeometryN(number);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public IGeography STIntersection(IGeography geog)
        {
            Recalc();
            var difference = p.STIntersection(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STIntersects(IGeography geog)
        {
            Recalc();
            return p.STIntersects(geog.Geography).Value;
        }

        public bool STIsClosed()
        {
            Recalc();
            return p.STIsClosed().Value;
        }

        public double STLength()
        {
            Recalc();
            return p.STLength().Value;
        }

        public int STNumGeometries()
        {
            Recalc();
            return p.STNumGeometries().Value;
        }

        public int STNumPoints()
        {
            Recalc();
            return p.STNumPoints().Value;
        }

        public bool STOverlaps(IGeography geog)
        {
            Recalc();
            return p.STOverlaps(geog.Geography).Value;
        }

        public IGeographyPoint STPointN(int number)
        {
            Recalc();
            var point = p.STPointN(number);
            return (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(point);
        }

        public IGeographyPoint STStartPoint()
        {
            Recalc();
            var ep = p.STStartPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public IGeography STSymDifference(IGeography geog)
        {
            Recalc();
            var difference = p.STSymDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public IGeography STUnion(IGeography geog)
        {
            Recalc();
            var difference = p.STUnion(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STWithin(IGeography geog)
        {
            Recalc();
            return p.STWithin(geog.Geography).Value;
        }
    }
}
