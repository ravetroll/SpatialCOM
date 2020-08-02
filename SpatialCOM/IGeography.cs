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
        Microsoft.SqlServer.Types.SqlGeography Geography { get; set; }

        [DispId(1100)]
        bool STIsEmpty();

        [DispId(1101)]
        bool STIsValid();

        [DispId(1102)]
        double STArea();

        [DispId(1104)]
        double STDistance(IGeography geography);
        
        [DispId(1105)]
        int STSrid { get; }

        [DispId(1106)]
        string STAsText();

        [DispId(1107)]
        string STGeometryType();

        [DispId(1108)]
        string Name { get; set; }

        [DispId(1109)]
        string Description { get; set; }

        [DispId(1110)]
        IGeography STBuffer(double distance);

        [DispId(1111)]
        bool STContains(IGeography geog);

        [DispId(1112)]
        IGeography STConvexHull();

        [DispId(1113)]
        IGeography STDifference(IGeography geog);

        [DispId(1114)]
        int STDimension();

        [DispId(1115)]
        bool STDisjoint(IGeography geog);

        [DispId(1116)]
        IGeographyPoint STEndPoint();

        [DispId(1117)]
        bool STEquals(IGeography geog);

        [DispId(1118)]
        IGeography STGeometryN(int number);

        [DispId(1119)]
        IGeography STIntersection(IGeography geog);

        [DispId(1120)]
        bool STIntersects(IGeography geog);

        [DispId(1121)]
        bool STIsClosed();

        [DispId(1122)]
        double STLength();

        [DispId(1123)]
        int STNumGeometries();

        [DispId(1124)]
        int STNumPoints();

        [DispId(1125)]
        bool STOverlaps(IGeography geog);

        [DispId(1126)]
        IGeographyPoint STPointN(int number);

        [DispId(1127)]
        IGeographyPoint STStartPoint();

        [DispId(1128)]
        IGeography STSymDifference(IGeography geog);

        [DispId(1129)]
        IGeography STUnion(IGeography geog);

        [DispId(1130)]
        bool STWithin(IGeography geog);
    }
}
