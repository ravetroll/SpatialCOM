using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpatialCOM;

namespace SpatialCOMTest
{
    [TestClass]
    public class KMLReaderTest
    {
        [TestMethod]
        public void TestPlacemarkReader()
        {
            KMLReader k = new KMLReader();
            var g = k.LoadAllPlacemarksFromKML("LineString.kml");
            Assert.AreEqual(true, g is GeographyCollection);
        }

        [TestMethod]
        public void TestPolygonReader()
        {
            KMLReader k = new KMLReader();
            var g = k.LoadNamedPolygonFromKML("Polygon.kml","Polygon");
            Assert.AreEqual(true, g is GeographyPolygon);
        }
    }
}
