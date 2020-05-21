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
    [Guid("0E86990D-11B7-47AB-8C29-3CE86383A622")]
    public interface IGeographyMultiPoint: IEnumerable, IGeography
    {
       
        [DispId(-4)]
        new IEnumerator GetEnumerator();

        [DispId(2202)]
        new bool IsEmpty { get; }

        [DispId(2203)]
        void Add(GeographyPoint point);

        [DispId(2204)]
        void Clear();

        [DispId(2205)]
        int Srid { get; }

        [DispId(2206)]
        string WKT { get; }

        [DispId(2207)]
        new double Area();

        [DispId(2208)]
        new double DistanceTo(IGeography geography);

        [DispId(2209)]
        new bool IsValid();


    }
}
