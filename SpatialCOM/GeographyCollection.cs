using Microsoft.SqlServer.Types;
using System;
using System.Collections;
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
    [Guid("1BA4AD10-0789-4E56-9AD2-3E8B04B6BAF2")]
    public class GeographyCollection : IGeographyCollection
    {
        private List<IGeography> geogs;
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
                if (value.STGeometryType() == "GeometryCollection")
                {
                    List<IGeography> geogList = new List<IGeography>();
                    for (int count = 0; count < value.STNumGeometries(); count++)
                    {
                        var sqlgeog = value.STGeometryN(count);
                        var igeog = Internal.SQLToGeography.LoadSqlGeography(sqlgeog);
                        if (igeog != null)
                        {
                            geogList.Add(igeog);
                        }
                    }
                    geogs = geogList;
                    recalcNeeded = true;
                }
            }
        }

        public List<IGeography> Geographies
        {
            get
            {
                return geogs;
            }
        }

        public bool STIsEmpty() => geogs.Count() == 0;

        public int STSrid
        {
            get
            {
                if (STIsEmpty())
                {
                    return 0;
                }
                else
                    return geogs.First().STSrid;
            }

        }

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }

        public GeographyCollection()
        {
            geogs = new List<IGeography>();
            Microsoft.SqlServer.Types.SqlGeographyBuilder b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.GeometryCollection);
            b.EndGeography();
            p = b.ConstructedGeography;
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {


            return geogs.GetEnumerator();
        }

        public bool Add(IGeography geog)
        {

            if (geog.STGeometryType() != "GeometryCollection")
            {
                geogs.Add(geog);
                recalcNeeded = true;
                return true;
            }
            return false;

        }

        public void Clear()
        {
            geogs.Clear();
            recalcNeeded = true;
        }

        private void Recalc()
        {
            
            if (recalcNeeded)
            {
                p = null;
                if (!STIsEmpty())
                {
                    List<string> WKTList = new List<string>();                    
                    foreach (IGeography geog in geogs)
                    {
                        WKTList.Add(geog.STAsText());
                    }
                    p = SqlGeography.STGeomCollFromText(new SqlChars("GEOMETRYCOLLECTION (" + string.Join(", ", WKTList) + ")"), geogs.First().STSrid) ;
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
            Recalc();
            return p.STArea().Value;
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

        public double STLength()
        {
            Recalc();
            return p.STLength().Value;
        }

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
