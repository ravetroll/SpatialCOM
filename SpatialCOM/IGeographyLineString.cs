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
        new bool STIsEmpty();

        [DispId(2102)]
        void Initialize(IGeographyPoint firstPoint, IGeographyPoint lastPoint);

        [DispId(2103)]
        new double STLength { get; }

        [DispId(2104)]
        new string STAsText();

        [DispId(2105)]
        new int STSrid { get; }

        [DispId(2106)]
        new double STArea();

        [DispId(2107)]
        new double STDistance(IGeography geography);

        [DispId(2108)]
        new bool STIsValid();

        [DispId(2109)]
        IGeographyPoint FirstPoint { get;  }

        [DispId(2110)]
        IGeographyPoint LastPoint { get;  }

        [DispId(2111)]
        new string STGeometryType();

        [DispId(2112)]
        bool AddPoint(IGeographyPoint point);

        [DispId(2113)]
        bool InitializeFromPoints(GeographyMultiPoint points);

        [DispId(2114)]
        new string Name { get; set; }

        [DispId(2115)]
        new string Description { get; set; }

        [DispId(2116)]
        new IGeography STBuffer(double distance);

        [DispId(2117)]
        new bool STContains(IGeography geog);

        [DispId(2118)]
        new IGeography STConvexHull();

        [DispId(2119)]
        new IGeography STDifference(IGeography geog);

        [DispId(2120)]
        new int STDimension();

        [DispId(2121)]
        new bool STDisjoint(IGeography geog);

        [DispId(2122)]
        new IGeographyPoint STEndPoint();

        [DispId(2123)]
        new bool STEquals(IGeography geog);

        [DispId(2124)]
        new IGeography STGeometryN(int number);

        [DispId(2125)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2126)]
        new bool STIntersects(IGeography geog);

        [DispId(2127)]
        new bool STIsClosed();

        [DispId(2128)]
        new int STNumGeometries();

        [DispId(2129)]
        new int STNumPoints();

        [DispId(2130)]
        new bool STOverlaps(IGeography geog);

        [DispId(2131)]
        new IGeographyPoint STPointN(int number);

        [DispId(2132)]
        new IGeographyPoint STStartPoint();

        [DispId(2133)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2134)]
        new IGeography STUnion(IGeography geog);

        [DispId(2135)]
        new bool STWithin(IGeography geog);


    }
}
