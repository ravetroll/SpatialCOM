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

        public double Latitude => p.Lat.Value;

        public double Longitude => p.Long.Value;

        public bool IsEmpty => p is null;

        public int Srid => p is null ? 0 : p.STSrid.Value ;

        public void Initialize(double lat, double lon, int srid = 4326)
        {
            var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(srid);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);
            b.BeginFigure(lat, lon);
            b.EndFigure();
            b.EndGeography();
            p = b.ConstructedGeography;
        }
    }
}
