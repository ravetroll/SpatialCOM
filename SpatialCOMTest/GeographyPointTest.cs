using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyPointTest
    {
        [TestMethod]
        public void TestInitialize()
        {
            SpatialCOM.GeographyPoint p = new SpatialCOM.GeographyPoint();
            p.Initialize(1d, 2d);
            Assert.AreEqual(1d, p.Latitude);
            Assert.AreEqual(2d, p.Longitude);

        }
    }
}
