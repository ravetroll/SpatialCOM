using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("2CB8CEB8-7326-45B4-AE4B-A287D8AC35B8")]
    public interface IGeographyMultiLineString: IEnumerable, IGeography
    {
       

        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(2302)]
        new bool STIsEmpty();

        [DispId(2303)]
        bool Add(IGeographyLineString line);

        [DispId(2304)]
        void Clear();

        [DispId(2305)]
        new int STSrid { get; }

        [DispId(2306)]
        new double STLength();

        [DispId(2307)]
        new string STAsText();

        [DispId(2308)]
        new double STArea();

        [DispId(2309)]
        new double STDistance(IGeography geography);

        [DispId(2310)]
        new bool STIsValid();

        [DispId(2311)]
        new string STGeometryType();

        [DispId(2312)]
        new string Name { get; set; }

        [DispId(2313)]
        new string Description { get; set; }

        [DispId(2314)]
        new IGeography STBuffer(double distance);

        [DispId(2315)]
        new bool STContains(IGeography geog);

        [DispId(2316)]
        new IGeography STConvexHull();

        [DispId(2317)]
        new IGeography STDifference(IGeography geog);

        [DispId(2318)]
        new int STDimension();

        [DispId(2319)]
        new bool STDisjoint(IGeography geog);

        [DispId(2320)]
        new IGeographyPoint STEndPoint();

        [DispId(2321)]
        new bool STEquals(IGeography geog);

        [DispId(2322)]
        new IGeography STGeometryN(int number);

        [DispId(2323)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2324)]
        new bool STIntersects(IGeography geog);

        [DispId(2325)]
        new bool STIsClosed();

        [DispId(2326)]
        new int STNumGeometries();

        [DispId(2327)]
        new int STNumPoints();

        [DispId(2328)]
        new bool STOverlaps(IGeography geog);

        [DispId(2329)]
        new IGeographyPoint STPointN(int number);

        [DispId(2330)]
        new IGeographyPoint STStartPoint();

        [DispId(2331)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2332)]
        new IGeography STUnion(IGeography geog);

        [DispId(2333)]
        new bool STWithin(IGeography geog);
    }
}
