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

        [TestMethod]
        public void TestDistance()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(2d, 3d);
            GeographyLineString l = new GeographyLineString();
            l.Initialize(p1, p2);
            GeographyPoint p3 = new GeographyPoint();
            p3.Initialize(2.5d, 5d);
            GeographyPoint p4 = new GeographyPoint();
            p4.Initialize(3d, 5d);
            GeographyLineString l2 = new GeographyLineString();
            l2.Initialize(p3, p4);
            Assert.IsTrue(l2.IsValid());
            Assert.AreEqual(229234d, l.DistanceTo(l2),1d);

        }
    }
}
