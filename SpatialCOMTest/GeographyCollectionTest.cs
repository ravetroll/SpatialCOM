using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpatialCOM;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyCollectionTest
    {
        [TestMethod]
        public void TestAdd()
        {
            GeographyPoint p = new GeographyPoint();
            p.Initialize(1d, 2d);
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(1d, 3d);
            GeographyLineString l = new GeographyLineString();
            l.Initialize(p1, p2);
            GeographyCollection coll = new GeographyCollection
            {
                p,
                l
            };
            Assert.AreEqual("GeometryCollection", coll.STGeometryType());
            Assert.AreEqual("GEOMETRYCOLLECTION (POINT (2 1), LINESTRING (2 1, 3 1))", coll.STAsText());
        }
    }
}

