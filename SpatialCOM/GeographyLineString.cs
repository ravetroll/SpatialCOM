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
        public GeographyLineString() { }
        
        private Microsoft.SqlServer.Types.SqlGeography l;

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                return l;
            }
        }

        public bool IsEmpty => l is null;

        public void Initialize(GeographyPoint point1, GeographyPoint point2)
        {
            if (IsEmpty)
            {
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(point1.Srid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.LineString);
                b.BeginFigure(point1.Latitude, point1.Longitude);
                b.AddLine(point2.Latitude, point2.Longitude);
                b.EndFigure();
                b.EndGeography();
                l = b.ConstructedGeography;
            }
            
        }

        public double Length { get 
            { 
                if (IsEmpty)
                {
                    return 0d;
                }
                else
                    return l.STLength().Value; 
            } 
        }

        public string WKT
        {
            get
            {

                if (IsEmpty)
                {
                    return "";
                }
                else
                    return new String(l.STAsText().Value);
            }
        }

        public int Srid
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                else
                    return l.STSrid.Value;
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
