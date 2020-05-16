using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("FC0D9535-5615-431F-B1B3-01D2DB829BBB")]
    public interface IGeographyPoint
    {
        double Latitude { get; }

        double Longitude { get; }

        int Srid { get; }

        bool IsEmpty { get; }

        void Initialize(double lat, double lon, int srid = 4326);
    }
}
