using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("3D581F7D-3957-4E7E-A911-25FED93FE37F")]
    public interface IGeography
    {
        [ComVisible(false)]
        Microsoft.SqlServer.Types.SqlGeography Geography { get; }

        [DispId(1100)]
        bool IsEmpty { get; }

        [DispId(1101)]
        bool IsValid();

        [DispId(1102)]
        double Area();

        [DispId(1104)]
        double DistanceTo(IGeography geography);

    }
}
