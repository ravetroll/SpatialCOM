using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM.Internal
{
    public static class KMLToGeography
    {
        public static IGeographyCollection LoadFeatures(IEnumerable<Feature> fs)
        {
            if (fs != null)
            {
                GeographyCollection collection = new GeographyCollection();
                foreach (Feature f in fs)
                {
                    Placemark pl = (Placemark)f;
                    Geometry geom = pl.Geometry;
                    collection.Add(GetGeography(geom));
                }
                return collection;
            }
            return null;
        }

        public static IGeography LoadFeature(Feature f)
        {
            if (f != null)
            {
                Placemark pl = (Placemark)f;
                Geometry geom = pl.Geometry;
                return GetGeography(geom);
            }
            return null;
        }

        private static IGeography GetGeography(Geometry geom)
        {
            IGeography geog = null;
            if (geom is SharpKml.Dom.LineString)
            {
                LineString l = (LineString)geom;
                GeographyLineString gl = new GeographyLineString();
                GeographyMultiPoint mp = new GeographyMultiPoint();
                foreach (Vector v in l.Coordinates)
                {
                    GeographyPoint p = new GeographyPoint();
                    p.Initialize(v.Latitude, v.Longitude, v.Altitude ?? double.MinValue);
                    mp.Add(p);
                }
                gl.InitializeFromPoints(mp);                
                geog = gl;
            }
            else if (geom is SharpKml.Dom.Point)
            {
                Point p = (Point)geom;
                GeographyPoint gp = new GeographyPoint();
                gp.Initialize(p.Coordinate.Latitude, p.Coordinate.Longitude, p.Coordinate.Altitude ?? double.MinValue);                
                geog = gp;
            }
            else if (geom is SharpKml.Dom.Polygon)
            {
                Polygon p = (Polygon)geom;
                GeographyPolygon gp = new GeographyPolygon();
                GeographyMultiPoint mp = new GeographyMultiPoint();
                foreach (Vector v in p.OuterBoundary.LinearRing.Coordinates)
                {
                    GeographyPoint pt = new GeographyPoint();
                    pt.Initialize(v.Latitude, v.Longitude, v.Altitude ?? double.MinValue);
                    mp.Add(pt);
                }
                gp.InitializeFromPoints(mp);
                foreach (InnerBoundary ib in p.InnerBoundary)
                {
                    mp = new GeographyMultiPoint();
                    foreach (Vector v in ib.LinearRing.Coordinates)
                    {
                        GeographyPoint pt = new GeographyPoint();
                        pt.Initialize(v.Latitude, v.Longitude, v.Altitude ?? double.MinValue);
                        mp.Add(pt);
                    }
                    gp.AddInnerFromPoints(mp);
                }
                geog = gp;

            }
            else if (geom is SharpKml.Dom.MultipleGeometry)
            {
                MultipleGeometry mg = (MultipleGeometry)geom;
                GeographyCollection gcol = new GeographyCollection();
                foreach (var g in mg.Geometry)
                {
                    gcol.Add(GetGeography(g));
                }
                geog = gcol;
            }
            else
            {
                throw new NotImplementedException(geom.GetType().FullName);
            }
            geog.Description = ((Placemark)geom.Parent).Description?.Text;
            geog.Name = ((Placemark)geom.Parent).Name;
            return geog;
        }
    }
}
