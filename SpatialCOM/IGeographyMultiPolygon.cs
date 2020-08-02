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
    [Guid("86F20160-486C-4C12-B577-893199E4DDF3")]
    public interface IGeographyMultiPolygon: IEnumerable, IGeography
    {
       

        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(2602)]
        new bool STIsEmpty();

        [DispId(2603)]
        bool Add(IGeographyPolygon poly);

        [DispId(2604)]
        void Clear();

        [DispId(2605)]
        new int STSrid { get; }

        [DispId(2606)]
        new double STLength();

        [DispId(2607)]
        new string STAsText();

        [DispId(2608)]
        new double STArea();

        [DispId(2609)]
        new double STDistance(IGeography geography);

        [DispId(2610)]
        new bool STIsValid();

        [DispId(2611)]
        new string STGeometryType();

        [DispId(2612)]
        new string Name { get; set; }

        [DispId(2613)]
        new string Description { get; set; }

        [DispId(2614)]
        new IGeography STBuffer(double distance);

        [DispId(2615)]
        new bool STContains(IGeography geog);

        [DispId(2616)]
        new IGeography STConvexHull();

        [DispId(2617)]
        new IGeography STDifference(IGeography geog);

        [DispId(2618)]
        new int STDimension();

        [DispId(2619)]
        new bool STDisjoint(IGeography geog);

        [DispId(2620)]
        new IGeographyPoint STEndPoint();

        [DispId(2621)]
        new bool STEquals(IGeography geog);

        [DispId(2622)]
        new IGeography STGeometryN(int number);

        [DispId(2623)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2624)]
        new bool STIntersects(IGeography geog);

        [DispId(2625)]
        new bool STIsClosed();

        [DispId(2626)]
        new int STNumGeometries();

        [DispId(2627)]
        new int STNumPoints();

        [DispId(2628)]
        new bool STOverlaps(IGeography geog);

        [DispId(2629)]
        new IGeographyPoint STPointN(int number);

        [DispId(2630)]
        new IGeographyPoint STStartPoint();

        [DispId(2631)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2632)]
        new IGeography STUnion(IGeography geog);

        [DispId(2633)]
        new bool STWithin(IGeography geog);
    }
}
