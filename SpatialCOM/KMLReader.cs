using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using SpatialCOM.Internal;

namespace SpatialCOM
{
    public class KMLReader
    {
        public KMLReader()
        {

        }

        public IGeography LoadFirstPlacemarkFromKML(string path)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            Placemark p = (Placemark)d.Features.First();
            return KMLToGeography.LoadFeature(p);
        }

        public IGeography LoadNamedPlacemarkFromKML(string path, string name)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            Feature result = null; ;
            foreach(var feature in d.Features)
            {
                if (feature is Placemark)
                {
                    Placemark pm = (Placemark)feature;
                    if (pm?.Name?.ToLower() == name.ToLower() )
                    {
                        if (result == null) result = pm;
                        
                    }
                }
            }
            return KMLToGeography.LoadFeature(result);
        }

        public IGeographyPolygon LoadNamedPolygonFromKML(string path, string name)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            Feature result = null; ;
            foreach (var feature in d.Features)
            {
                if (feature is Placemark)
                {
                    Placemark pm = (Placemark)feature;
                    if (pm?.Name?.ToLower() == name.ToLower())
                    {
                        if (pm.Geometry is Polygon)
                            if (result == null) result = pm;

                    }
                }
            }
            return (IGeographyPolygon)KMLToGeography.LoadFeature(result);
        }

        public IGeographyLineString LoadNamedLineStringFromKML(string path, string name)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            Feature result = null; ;
            foreach (var feature in d.Features)
            {
                if (feature is Placemark)
                {
                    Placemark pm = (Placemark)feature;
                    if (pm?.Name?.ToLower() == name.ToLower())
                    {
                        if (pm.Geometry is LineString)
                            if (result == null) result = pm;

                    }
                }
            }
            return (IGeographyLineString)KMLToGeography.LoadFeature(result);
        }

        public IGeographyPoint LoadNamedPointFromKML(string path, string name)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            Feature result = null; ;
            foreach (var feature in d.Features)
            {
                if (feature is Placemark)
                {
                    Placemark pm = (Placemark)feature;
                    if (pm?.Name?.ToLower() == name.ToLower())
                    {
                        if (pm.Geometry is Point)
                            if (result == null) result = pm;

                    }
                }
            }
            return (IGeographyPoint)KMLToGeography.LoadFeature(result);
        }

        public IGeographyCollection LoadAllPlacemarksFromKML(string path)
        {
            Stream s = File.OpenRead(path);
            KmlFile f = KmlFile.Load(s);
            Kml k = (Kml)f.Root;
            Document d = (Document)k.Feature;
            var p = d.Features;
            return KMLToGeography.LoadFeatures(p);
        }
    }
}
