using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]   
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("0C319EA8-BDC5-408C-A542-D483567DFD4D")]
    public class GeographyPoint : IGeographyPoint
    {
        private Microsoft.SqlServer.Types.SqlGeography p;
        public GeographyPoint()
        {

        }

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                return p;
            }
        }

        public double Latitude
        {
            get
            {
                if (p == null)
                {
                    return double.MinValue;
                }
                else
                    return p.Lat.Value;
            }
        }

        public double Longitude
        {
            get
            {
                if (p == null)
                {
                    return double.MinValue;
                }
                else
                    return p.Long.Value;
            }
        }

        public double Z
        {
            get
            {
                if (p == null)
                {
                    return double.MinValue;
                }
                else
                    return p.Z.Value;
            }
        }

        public double M
        {
            get
            {
                if (p == null)
                {
                    return double.MinValue;
                }
                else
                    return p.M.Value;
            }
        }

        public bool IsEmpty => p is null;

        public int Srid => p is null ? 0 : p.STSrid.Value ;

        public void Initialize(double lat, double lon, double z = double.MinValue, double m = double.MinValue, int srid = 4326)
        {
            double? _z = z == double.MinValue ? (double?)null : z;
            double? _m = m == double.MinValue ? (double?)null : m;
            if (IsEmpty)
            {
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(srid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);                 
                b.BeginFigure(lat, lon, _z, _m);
                b.EndFigure();
                b.EndGeography();
                p = b.ConstructedGeography;
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

        public string WKT
        {
            get
            {

                if (IsEmpty)
                {
                    return "";
                }
                else
                    return new String(p.STAsText().Value);
            }
        }

        public bool IsValid()
        {

            if (IsEmpty)
            {
                return false;
            }
            else
            {
                
                return p.STIsValid().Value;
            }
        }




    }
}
