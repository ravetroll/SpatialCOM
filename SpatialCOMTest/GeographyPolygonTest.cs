using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpatialCOM;

namespace SpatialCOMTest
{
    [TestClass]
    public class GeographyPolygonTest
    {
        [TestMethod]
        public void TestInitializeFromPoints()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(1d, 2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(1d, 3d);
            GeographyPoint p3 = new GeographyPoint();
            p3.Initialize(3d, 3d);
            GeographyPoint p4 = new GeographyPoint();
            p4.Initialize(1d, 2d);
            GeographyMultiPoint gp = new GeographyMultiPoint();
            gp.Add(p1);
            gp.Add(p2);
            gp.Add(p3);
            gp.Add(p4);

            GeographyPolygon pg = new GeographyPolygon();
            pg.InitializeFromPoints(gp);
            Assert.AreEqual(true, pg.STIsValid());
            Assert.AreEqual("Polygon", pg.STGeometryType());

        }

        [TestMethod]
        public void TestArea()
        {
            GeographyPoint p1 = new GeographyPoint();
            p1.Initialize(0.1d, 0.2d);
            GeographyPoint p2 = new GeographyPoint();
            p2.Initialize(0.1d, 0.3d);
            GeographyPoint p3 = new GeographyPoint();
            p3.Initialize(0.3d, 0.3d);
            GeographyPoint p4 = new GeographyPoint();
            p4.Initialize(0.1d, 0.2d);
            GeographyMultiPoint gp = new GeographyMultiPoint();
            gp.Add(p1);
            gp.Add(p2);
            gp.Add(p3);
            gp.Add(p4);

            GeographyPolygon pg = new GeographyPolygon();
            pg.InitializeFromPoints(gp);
            Assert.AreEqual(true, pg.STIsValid());
            Assert.AreEqual(123090704, pg.STArea(),1d);
            GeographyPolygon pgbuffered = (GeographyPolygon)pg.STBuffer(1000);
            Assert.AreEqual(184235008.801918, pgbuffered.STArea(),1d);

        }
    }
}
