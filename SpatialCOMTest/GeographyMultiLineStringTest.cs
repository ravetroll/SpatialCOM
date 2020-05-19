using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpatialCOM;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyMultiLineStringTest
    {
        [TestMethod]
        public void TestLength()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(1d, 3d);
            GeographyLineString l1 = new GeographyLineString();
            l1.Initialize(p1, p2);
            GeographyPoint p3 = new GeographyPoint();
            p3.Initialize(3d, 2d);
            GeographyPoint p4 = new GeographyPoint();
            p4.Initialize(4d, 3d);
            GeographyLineString l2 = new GeographyLineString();
            l2.Initialize(p3, p4);
            GeographyMultiLineString ml = new GeographyMultiLineString();
            ml.Add(l1);
            Assert.AreEqual(l1.Length, ml.Length);
            ml.Add(l2);
            Assert.AreEqual(l1.Length + l2.Length, ml.Length);


        }
    }
}
