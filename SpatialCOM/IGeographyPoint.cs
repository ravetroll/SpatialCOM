using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    public interface IGeographyPoint
    {
        double Latitude { get; }

        double Longitude { get; }

        bool IsEmpty { get; }

        void Initialize(double lat, double lon);
    }
}
