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
    [Guid("2CB8CEB8-7326-45B4-AE4B-A287D8AC35B8")]
    public interface IGeographyMultiLineString: IEnumerable
    {
        [DispId(2301)]
        bool IsEmpty { get; }

        [DispId(2302)]
        new IEnumerator GetEnumerator();

        [DispId(2303)]
        void Add(GeographyLineString line);

        [DispId(2304)]
        void Clear();

        [DispId(2305)]
        int Srid { get; }

        [DispId(2306)]
        double Length { get; }

        [DispId(2307)]
        string WKT { get; }
    }
}