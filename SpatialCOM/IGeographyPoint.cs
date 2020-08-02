using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [Guid("FC0D9535-5615-431F-B1B3-01D2DB829BBB")]
    public interface IGeographyPoint: IGeography
    {

       

        [DispId(2001)]
        double Latitude { get; }

        [DispId(2002)]
        double Longitude { get; }

        [DispId(2003)]
        new int STSrid { get; }

        [DispId(2004)]
        new bool STIsEmpty();

        [DispId(2005)]
        void Initialize(double lat, double lon, double z = double.MinValue, double m = double.MinValue, int srid = 4326);

        [DispId(2006)]
        new string STAsText();

        [DispId(2007)]
        new double STArea();

        [DispId(2008)]
        new double STDistance(IGeography geography);

        [DispId(2009)]
        double Z { get; }

        [DispId(2010)]
        double M { get; }

        [DispId(2011)]
        new bool STIsValid();

        [DispId(2012)]
        new string STGeometryType();

        [DispId(2013)]
        new string Name { get; set; }

        [DispId(2014)]
        new string Description { get; set; }

        [DispId(2015)]
        new IGeography STBuffer(double distance);

        [DispId(2016)]
        new bool STContains(IGeography geog);

        [DispId(2017)]
        new IGeography STConvexHull();

        [DispId(2018)]
        new IGeography STDifference(IGeography geog);

        [DispId(2019)]
        new int STDimension();

        [DispId(2020)]
        new bool STDisjoint(IGeography geog);

        [DispId(2021)]
        new IGeographyPoint STEndPoint();

        [DispId(2022)]
        new bool STEquals(IGeography geog);

        [DispId(2023)]
        new IGeography STGeometryN(int number);

        [DispId(2024)]
        new IGeography STIntersection(IGeography geog);

        [DispId(2025)]
        new bool STIntersects(IGeography geog);

        [DispId(2026)]
        new bool STIsClosed();

        [DispId(2027)]
        new double STLength();

        [DispId(2028)]
        new int STNumGeometries();

        [DispId(2029)]
        new int STNumPoints();

        [DispId(2030)]
        new bool STOverlaps(IGeography geog);

        [DispId(2031)]
        new IGeographyPoint STPointN(int number);

        [DispId(2032)]
        new IGeographyPoint STStartPoint();

        [DispId(2033)]
        new IGeography STSymDifference(IGeography geog);

        [DispId(2034)]
        new IGeography STUnion(IGeography geog);

        [DispId(2035)]
        new bool STWithin(IGeography geog);

    }
}
