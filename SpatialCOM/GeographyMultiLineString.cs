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
    public class GeographyMultiLineString : IGeographyMultiLineString
    {

        private List<GeographyLineString> lines;
        private Microsoft.SqlServer.Types.SqlGeography l;
        private bool recalcNeeded;

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                Recalc();
                return l;
            }
        }

        public bool IsEmpty => lines.Count() == 0;

        public int Srid
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                else
                    return lines.First().Srid;
            }

        }

        public GeographyMultiLineString()
        {
            lines = new List<GeographyLineString>();
            recalcNeeded = true;
        }
        public IEnumerator GetEnumerator()
        {
            

            return lines.GetEnumerator();
        }

        public void Add(GeographyLineString line)
        {
            if (line.Srid == this.Srid || this.Srid == 0)
            {
                lines.Add(line);
                recalcNeeded = true;
            }
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
                if (!IsEmpty)
                {
                    l = null;
                    var buildString = String.Join(",", lines.Select(s => s.WKT));
                    buildString = $"GEOMETRYCOLLECTION ({buildString})";                    
                    l = Microsoft.SqlServer.Types.SqlGeography.STGeomCollFromText(new SqlChars(new SqlString(buildString)), this.Srid);
                    
                }
            }
            recalcNeeded = false;

        }

        public double Length
        {
            get
            {
                Recalc();
                return l == null ? 0d : l.STLength().Value;
            }
        }

        public string WKT
        {
            get
            {
                Recalc();
                return l == null ? "" : new string(l.STAsText().Value);
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
                return l.STDistance(geography.Geography).Value;
        }

    }
}
