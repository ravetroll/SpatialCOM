using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("C6A70B04-ACB8-42ED-A15C-0D2DC6EEAE59")]
    public interface IGeographyPolygon: IGeography
    {

        [DispId(2401)]
        new int STSrid { get; }

        [DispId(2402)]
        new bool STIsEmpty();

        [DispId(2403)]
        bool InitializeFromPoints(GeographyMultiPoint points);

        [DispId(2404)]
        bool InitializeFromLine(GeographyLineString lines);

        [DispId(2405)]
        new string STAsText();

        [DispId(2406)]
        new double STArea();

        [DispId(2407)]
        new double STDistance(IGeography geography);        

        [DispId(2408)]
        new bool STIsValid();

        [DispId(2409)]
        new string STGeometryType();

        [DispId(2410)]
        bool AddInnerFromPoints(GeographyMultiPoint points);

        [DispId(2411)]
        bool AddInnerFromLine(GeographyLineString line);

        [DispId(2412)]
        new string Name { get; set; }

        [DispId(2413)]
        new string Description { get; set; }

        [DispId(2414)]
        new IGeography STBuffer(double distance);

        [DispId(2415)]
        new bool STContains(IGeography geog);

        [DispId(2416)]
        new IGeography STConvexHull();

        [DispId(2417)]
        new IGeography STDifference(IGeography geog);

        [DispId(2418)]
        new int STDimension();

        [DispId(2419)]
        new bool STDisjoint(IGeography geog);

        [DispId(2420)]
        new IGeographyPoint STEndPoint();

        [DispId(2421)]
        new bool STEquals(IGeography geog);

        [DispId(2422)]
        new IGeography STGeometryN(int number);

        [DispId(2423)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2424)]
        new bool STIntersects(IGeography geog);

        [DispId(2425)]
        new bool STIsClosed();

        [DispId(2426)]
        new double STLength();

        [DispId(2427)]
        new int STNumGeometries();

        [DispId(2428)]
        new int STNumPoints();

        [DispId(2429)]
        new bool STOverlaps(IGeography geog);

        [DispId(2430)]
        new IGeographyPoint STPointN(int number);

        [DispId(2431)]
        new IGeographyPoint STStartPoint();

        [DispId(2432)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2433)]
        new IGeography STUnion(IGeography geog);

        [DispId(2434)]
        new bool STWithin(IGeography geog);
    }
}
