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
        int Srid { get; }

        [DispId(2004)]
        new bool IsEmpty { get; }

        [DispId(2005)]
        void Initialize(double lat, double lon, double z = double.MinValue, double m = double.MinValue, int srid = 4326);

        [DispId(2006)]
        string WKT { get; }

        [DispId(2007)]
        new double Area();

        [DispId(2008)]
        new double DistanceTo(IGeography geography);

        [DispId(2009)]
        double Z { get; }

        [DispId(2010)]
        double M { get; }

        [DispId(2011)]
        new bool IsValid();

    }
}
