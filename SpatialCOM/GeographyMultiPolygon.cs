using Microsoft.SqlServer.Types;
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
    [Guid("0D79C206-B4E0-4E89-A5F1-F609AE75F0D1")]
    public class GeographyMultiPolygon : IGeographyMultiPolygon
    {

        private List<IGeographyPolygon> polys;
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
                if (value.STGeometryType() == "MultiPolygon")
                {
                    polys = new List<IGeographyPolygon>();
                    for (int count = 0; count < value.STNumGeometries();count++)
                    {
                        var poly = (IGeographyPolygon)Internal.SQLToGeography.LoadSqlGeography(value.STGeometryN(count));
                        polys.Add(poly);

                    }
                    recalcNeeded = true;
                }
            }
        }

        public bool STIsEmpty() => polys.Count() == 0;

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

        public GeographyMultiPolygon()
        {
            polys = new List<IGeographyPolygon>();            
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {
            

            return polys.GetEnumerator();
        }

        public bool Add(IGeographyPolygon poly)
        {
           
            polys.Add(poly);
            recalcNeeded = true;
            return true;
        }

        public void Clear()
        {
            polys.Clear();
            recalcNeeded = true;
        }

        private void Recalc()
        {
            if (recalcNeeded)
            {
                
                if (!STIsEmpty())
                {
                    List<string> polyArray = new List<string>();
                    foreach (IGeographyPolygon poly in polys)
                    {
                        var polyString = new string(poly.Geography.AsTextZM().Value);
                        var polyStripped = polyString.Replace("POLYGON", "");
                        polyArray.Add(polyStripped);
                    }
                    if (polyArray.Count() == 0)
                    {
                        l = SqlGeography.STGeomFromText(new SqlChars("MULTIPOLYGON EMPTY"), 4326);
                    }
                    else
                    {
                        l = SqlGeography.STGeomFromText(new SqlChars("MULTIPOLYGON (" + String.Join(",",polyArray) +")"), polys.First().STSrid);
                    }
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
