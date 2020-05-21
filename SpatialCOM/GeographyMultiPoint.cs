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

        private List<GeographyPoint> points;
        private Microsoft.SqlServer.Types.SqlGeography p;
        private bool recalcNeeded;

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                Recalc();
                return p;
            }
        }

        public bool IsEmpty => points.Count() == 0;

        public int Srid
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                else
                    return points.First().Srid;
            }

        }

        public GeographyMultiPoint()
        {
            points = new List<GeographyPoint>();
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {
            

            return points.GetEnumerator();
        }

        public void Add(GeographyPoint point)
        {
            if (point.Srid == this.Srid || this.Srid == 0)
            {
                points.Add(point);
                recalcNeeded = true;
            }
        }

        public void Clear()
        {
            points.Clear();
            recalcNeeded = true;
        }

        private void Recalc()
        {
            if (recalcNeeded)
            {
                p = null;
                if (!IsEmpty)
                {
                    p = null;
                    var buildString = String.Join(",", points.Select(s => s.WKT));
                    buildString = $"GEOMETRYCOLLECTION ({buildString})";                    
                    p = Microsoft.SqlServer.Types.SqlGeography.STGeomCollFromText(new SqlChars(new SqlString(buildString)), this.Srid);
                    
                }
            }
            recalcNeeded = false;

        }

        public string WKT
        {
            get
            {

                Recalc();
                if (IsEmpty)
                {
                    return "";
                }
                else
                    return new String(p.STAsText().Value);
            }
        }

        public double Area()
        {
            return 0d;
        }

        public double DistanceTo(IGeography geography)
        {
            if (IsEmpty || geography.IsEmpty)
            {
                return -1d;
            }
            else
                return p.STDistance(geography.Geography).Value;
        }


    }
}
