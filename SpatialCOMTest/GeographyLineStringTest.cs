using System;
using SpatialCOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyLineStringTest
    {
        [TestMethod]
        public void TestInitialize()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(1d, 3d);
            GeographyLineString l = new GeographyLineString();
            l.Initialize(p1, p2);
            Assert.AreEqual(111302, l.Length,1);
        }
    }
}
