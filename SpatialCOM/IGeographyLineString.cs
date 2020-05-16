using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("7D17B508-AB69-4526-A7F0-610200E4F46A")]
    public interface IGeographyLineString
    {
        bool IsEmpty { get; }

        void Initialize(GeographyPoint point1, GeographyPoint point2);

        double Length();
    }
}
