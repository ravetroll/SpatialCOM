using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;


namespace SpatialCOM.Internal
{
    public static class SQLToGeography
    {
        public static IGeography LoadSqlGeography(SqlGeography sqlgeog)
        {
            if (sqlgeog != null)
            {
                return GetGeography(sqlgeog);
            }
            return null;
        }

        private static IGeography GetGeography(SqlGeography sqlgeog)
        {
            // Types that can be returned
            // Point, LineString, CircularString, CompoundCurve, Polygon, CurvePolygon, GeometryCollection, MultiPoint, MultiLineString, and MultiPolygon
            IGeography geog = null;
            switch (sqlgeog.STGeometryType().Value)
            {
                case "Point":
                    geog = new GeographyPoint
                    {
                        Geography = sqlgeog
                    };
                    break;
                case "LineString":
                    geog = new GeographyLineString
                    {
                        Geography = sqlgeog
                    };
                    break;
                case "Polygon":
                    geog = new GeographyPolygon
                    {
                        Geography = sqlgeog
                    };
                    break;
                case "MultiPoint":
                    geog = new GeographyMultiPoint
                    {
                        Geography = sqlgeog
                    };
                    break;
                case "MultiLineString":
                    geog = new GeographyMultiLineString
                    {
                        Geography = sqlgeog
                    };
                    break;
                case "GeometryCollection":
                    geog = new GeographyCollection
                    {
                        Geography = sqlgeog
                    };
                    break;
                default:
                    break;
            }
            return geog;
            
        }
    }
}
