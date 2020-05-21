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
    public interface IGeographyLineString: IGeography
    {
        [DispId(2101)]
        new bool IsEmpty { get; }

        [DispId(2102)]
        void Initialize(GeographyPoint point1, GeographyPoint point2);

        [DispId(2103)]
        double Length { get; }

        [DispId(2104)]
        string WKT { get; }

        [DispId(2105)]
        int Srid { get; }

        [DispId(2106)]
        new double Area();

        [DispId(2107)]
        new double DistanceTo(IGeography geography);
    }
}
