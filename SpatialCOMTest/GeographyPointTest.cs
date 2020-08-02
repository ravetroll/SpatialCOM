using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyPointTest
    {
        [TestMethod]
        public void TestInitializeLatLon()
        {
            SpatialCOM.GeographyPoint p = new SpatialCOM.GeographyPoint();
            p.Initialize(1d, 2d);
            Assert.AreEqual(1d, p.Latitude);
            Assert.AreEqual(2d, p.Longitude);
            Assert.AreEqual("Point", p.STGeometryType());
        }

        [TestMethod]
        public void TestInitializeAllValues()
        {
            SpatialCOM.GeographyPoint p = new SpatialCOM.GeographyPoint();
            p.Initialize(1d, 2d,100d,200d,4326);
            Assert.AreEqual(1d, p.Latitude);
            Assert.AreEqual(2d, p.Longitude);
            Assert.AreEqual(100d, p.Z);
            Assert.AreEqual(200d, p.M);
            
        }

        [TestMethod]
        public void PointIsValid()
        {
            
            SpatialCOM.GeographyPoint p = new SpatialCOM.GeographyPoint();
            p.Initialize(1d, 2d, 100d, 200d, 4326);
            Assert.IsTrue(p.STIsValid());

        }

        [TestMethod]
        public void DistanceToPointTest()
        {

            SpatialCOM.GeographyPoint p1 = new SpatialCOM.GeographyPoint();
            p1.Initialize(1d, 2d, 100d, 200d, 4326);
            SpatialCOM.GeographyPoint p2 = new SpatialCOM.GeographyPoint();
            p2.Initialize(2d, 2d, 100d, 200d, 4326);
            Assert.AreEqual(p1.STDistance(p2),110575d,1d);

        }


    }
}
