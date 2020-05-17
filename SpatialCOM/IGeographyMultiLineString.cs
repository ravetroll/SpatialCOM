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
    public interface IGeographyMultiLineString: IEnumerable
    {
        new IEnumerator GetEnumerator();

        void Add(GeographyLineString line);
    }
}
