using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpatialCOM;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyMultiPointTest
    {
        [TestMethod]
        public void TestWKT()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(1d, 3d);            
            GeographyPoint p3 = new GeographyPoint();
            p3.Initialize(3d, 2d);
            GeographyPoint p4 = new GeographyPoint();
            p4.Initialize(4d, 3d);          
            
            GeographyMultiPoint gp = new GeographyMultiPoint();
            gp.Add(p1);
            gp.Add(p2);
            gp.Add(p3);
            gp.Add(p4);
            Assert.AreEqual("MULTIPOINT ((2 1), (3 1), (2 3), (3 4))", gp.STAsText());
            Assert.AreEqual("MultiPoint", gp.STGeometryType());


        }
    }
}
