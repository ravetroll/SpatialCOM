using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("0D79C206-B4E0-4E89-A5F1-F609AE75F0D1")]
    public class GeographyMultiLineString : IGeographyMultiLineString
    {

        private List<GeographyLineString> lines;

        public GeographyMultiLineString()
        {
            lines = new List<GeographyLineString>();
        }
        public IEnumerator GetEnumerator()
        {
            

            return lines.GetEnumerator();
        }

        public void Add(GeographyLineString line)
        {
            lines.Add(line);
        }

        


    }
}
