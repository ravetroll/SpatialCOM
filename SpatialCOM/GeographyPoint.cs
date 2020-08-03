using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SpatialCOM
{
    [ComVisible(true)]   
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("0C319EA8-BDC5-408C-A542-D483567DFD4D")]
    public class GeographyPoint : IGeographyPoint
    {
        private Microsoft.SqlServer.Types.SqlGeography p;
        private string name = "";
        private string description = "";
        public GeographyPoint()
        {
            var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
            b.SetSrid(4326);
            b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);           
            b.EndGeography();
            p = b.ConstructedGeography;
        }

        public Microsoft.SqlServer.Types.SqlGeography Geography
        {
            get
            {
                return p;
            }
            set
            {
                if (value.STGeometryType() == "Point")
                {
                    p = value;
                }
            }
        }

        public double Latitude
        {
            get
            {
                if (STIsEmpty())
                {
                    return double.MinValue;
                }
                else
                    return p.Lat.Value;
            }
        }

        public double Longitude
        {
            get
            {
                if (STIsEmpty())
                {
                    return double.MinValue;
                }
                else
                    return p.Long.Value;
            }
        }

        public double Z
        {
            get
            {
                if (STIsEmpty() || p.Z.IsNull)
                {
                    return double.MinValue;
                }
                else
                    return p.Z.Value;
            }
        }

        public double M
        {
            get
            {
                if (STIsEmpty() || p.M.IsNull)
                {
                    return double.MinValue;
                }
                else
                    return p.M.Value;
            }
        }

        public int STSrid => p.STSrid.Value;

        public string Name { get { return name ?? ""; } set { name = value; } }
        public string Description { get { return description ?? ""; } set { description = value; } }
        public bool STIsEmpty()  => p.STIsEmpty().Value; 

        

        public void Initialize(double lat, double lon, double z = double.MinValue, double m = double.MinValue, int srid = 4326)
        {
            double? _z = z == double.MinValue ? (double?)null : z;
            double? _m = m == double.MinValue ? (double?)null : m;
            if (STIsEmpty())
            {
                var b = new Microsoft.SqlServer.Types.SqlGeographyBuilder();
                b.SetSrid(srid);
                b.BeginGeography(Microsoft.SqlServer.Types.OpenGisGeographyType.Point);                 
                b.BeginFigure(lat, lon, _z, _m);
                b.EndFigure();
                b.EndGeography();
                p = b.ConstructedGeography;
            }
        }

        public double STArea() => p.STArea().Value;
        

        public double STDistance(IGeography geography) => p.STDistance(geography.Geography).Value;
       

        public string STAsText() => new string(p.STAsText().Value);
       

        public bool STIsValid() => p.STIsValid().Value;


        public string STGeometryType() => p.STGeometryType().Value;

        public IGeography STBuffer(double distance)
        {
            var buffered = p.STBuffer(distance);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public bool STContains(IGeography geog)
        {
            return p.STContains(geog.Geography).Value;
        }

        public IGeography STConvexHull()
        {
           
            return Internal.SQLToGeography.LoadSqlGeography(p.STConvexHull());
        }

        public IGeography STDifference(IGeography geog)
        {
            var difference = p.STDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public int STDimension()
        {
            
            return p.STDimension().Value;
        }

        public bool STDisjoint(IGeography geog)
        {            
            return p.STDisjoint(geog.Geography).Value;
        }

        public IGeographyPoint STEndPoint()
        {
            
            var ep = p.STEndPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public bool STEquals(IGeography geog)
        {
            
            return p.STEquals(geog.Geography).Value;
        }

        public IGeography STGeometryN(int number)
        {
            
            var buffered = p.STGeometryN(number);
            return Internal.SQLToGeography.LoadSqlGeography(buffered);
        }

        public IGeography STIntersection(IGeography geog)
        {
            
            var difference = p.STIntersection(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STIntersects(IGeography geog)
        {
           
            return p.STIntersects(geog.Geography).Value;
        }

        public bool STIsClosed()
        {
            
            return p.STIsClosed().Value;
        }

        public double STLength()
        {
            
            return p.STLength().Value;
        }

        public int STNumGeometries()
        {
           
            return p.STNumGeometries().Value;
        }

        public int STNumPoints()
        {
            
            return p.STNumPoints().Value;
        }

        public bool STOverlaps(IGeography geog)
        {
            
            return p.STOverlaps(geog.Geography).Value;
        }

        public IGeographyPoint STPointN(int number)
        {
            
            var point = p.STPointN(number);
            return (IGeographyPoint)Internal.SQLToGeography.LoadSqlGeography(point);
        }

        public IGeographyPoint STStartPoint()
        {
            
            var ep = p.STStartPoint();
            return (IGeographyPoint)(ep == null ? null : Internal.SQLToGeography.LoadSqlGeography(ep));
        }

        public IGeography STSymDifference(IGeography geog)
        {
            
            var difference = p.STSymDifference(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public IGeography STUnion(IGeography geog)
        {
            
            var difference = p.STUnion(geog.Geography);
            return Internal.SQLToGeography.LoadSqlGeography(difference);
        }

        public bool STWithin(IGeography geog)
        {
            
            return p.STWithin(geog.Geography).Value;
        }
    }
}
