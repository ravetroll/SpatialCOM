using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("02D57424-2D6B-4B44-ACB5-5B3550E2CE0F")]
    public interface IGeographyCollection: IEnumerable, IGeography
    {
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(2502)]
        new bool STIsEmpty();

        [DispId(2503)]
        bool Add(IGeography geog);

        [DispId(2504)]
        void Clear();

        [DispId(2505)]
        new int STSrid { get; }

        [DispId(2506)]
        new double STLength();

        [DispId(2507)]
        new string STAsText();

        [DispId(2508)]
        new double STArea();

        [DispId(2509)]
        new double STDistance(IGeography geography);

        [DispId(2510)]
        new bool STIsValid();

        [DispId(2511)]
        new string STGeometryType();

        [DispId(2512)]
        new string Name { get; set; }

        [DispId(2513)]
        new string Description { get; set; }

        [DispId(2514)]
        new IGeography STBuffer(double distance);

        [DispId(2515)]
        new bool STContains(IGeography geog);

        [DispId(2516)]
        new IGeography STConvexHull();

        [DispId(2517)]
        new IGeography STDifference(IGeography geog);

        [DispId(2518)]
        new int STDimension();

        [DispId(2519)]
        new bool STDisjoint(IGeography geog);

        [DispId(2520)]
        new IGeographyPoint STEndPoint();

        [DispId(2521)]
        new bool STEquals(IGeography geog);

        [DispId(2522)]
        new IGeography STGeometryN(int number);

        [DispId(2523)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2524)]
        new bool STIntersects(IGeography geog);

        [DispId(2525)]
        new bool STIsClosed();

        [DispId(2526)]
        new int STNumGeometries();

        [DispId(2527)]
        new int STNumPoints();

        [DispId(2528)]
        new bool STOverlaps(IGeography geog);

        [DispId(2529)]
        new IGeographyPoint STPointN(int number);

        [DispId(2530)]
        new IGeographyPoint STStartPoint();

        [DispId(2531)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2532)]
        new IGeography STUnion(IGeography geog);

        [DispId(2533)]
        new bool STWithin(IGeography geog);


    }
}
