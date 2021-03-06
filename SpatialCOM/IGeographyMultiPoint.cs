﻿using System;
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
    [Guid("0E86990D-11B7-47AB-8C29-3CE86383A622")]
    public interface IGeographyMultiPoint: IEnumerable, IGeography
    {
       
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(2202)]
        new bool STIsEmpty();

        [DispId(2203)]
        bool Add(IGeographyPoint point);

        [DispId(2204)]
        void Clear();

        [DispId(2205)]
        new int STSrid { get; }

        [DispId(2206)]
        new string STAsText();

        [DispId(2207)]
        new double STArea();

        [DispId(2208)]
        new double STDistance(IGeography geography);

        [DispId(2209)]
        new bool STIsValid();

        [DispId(2210)]
        new string STGeometryType();

        [DispId(2211)]
        new string Name { get; set; }

        [DispId(2212)]
        new string Description { get; set; }

        [DispId(2213)]
        new IGeography STBuffer(double distance);

        [DispId(2214)]
        new bool STContains(IGeography geog);

        [DispId(2215)]
        new IGeography STConvexHull();

        [DispId(2216)]
        new IGeography STDifference(IGeography geog);

        [DispId(2217)]
        new int STDimension();

        [DispId(2218)]
        new bool STDisjoint(IGeography geog);

        [DispId(2219)]
        new IGeographyPoint STEndPoint();

        [DispId(2220)]
        new bool STEquals(IGeography geog);

        [DispId(2221)]
        new IGeography STGeometryN(int number);

        [DispId(2222)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2223)]
        new bool STIntersects(IGeography geog);

        [DispId(2224)]
        new bool STIsClosed();

        [DispId(2225)]
        new double STLength();

        [DispId(2226)]
        new int STNumGeometries();

        [DispId(2227)]
        new int STNumPoints();

        [DispId(2228)]
        new bool STOverlaps(IGeography geog);

        [DispId(2229)]
        new IGeographyPoint STPointN(int number);

        [DispId(2230)]
        new IGeographyPoint STStartPoint();

        [DispId(2231)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2232)]
        new IGeography STUnion(IGeography geog);

        [DispId(2233)]
        new bool STWithin(IGeography geog);
    }
}
