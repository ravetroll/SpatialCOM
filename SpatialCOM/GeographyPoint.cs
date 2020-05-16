using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    public class GeographyPoint : IGeographyPoint
    {
        private Microsoft.SqlServer.Types.SqlGeography p;
        public GeographyPoint()
        {

        }

        public double Latitude => p.Lat.Value;

        public double Longitude => p.Long.Value;

        public bool IsEmpty => p is null;

        public void Initialize(double lat, double lon)
        {
            var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);
            b.BeginFigure(lat, lon);
            b.EndFigure();
            b.EndGeography();
            p = b.ConstructedGeography;
        }
    }
}
