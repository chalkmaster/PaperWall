using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PaperWall.Core.Repository;

namespace PaperWall.Repository.MySQL
{
    internal static class ProximityProvider
    {
        internal static string GetProximityAlgorithm(double latitude, double longitude, double precision)
        {
            const int distanceFactor = 110;
            const string command = @"set @lon1 = {1}-{2}/abs(cos(radians({0}))*{3});
                                    set @lon2 = {1}+{2}/abs(cos(radians({0}))*{3});

                                    set @lat1 = {0}-({2}/{3});
                                    set @lat2 = {0}+({2}/{3});

                                    SELECT dest.*, 6329 * 2 
	                                    * ASIN(SQRT( POWER(SIN(({0} - dest.Latitude) * pi()/180 / 2), 2) 
	                                    + COS({0} * pi()/180) * COS(dest.Latitude * pi()/180) 
	                                    * POWER(SIN(({1} - dest.Longitude) * pi()/180 / 2), 2) )) as distance 
	                                    FROM paperwall.messages dest
		                                    WHERE dest.longitude between @lon1 and @lon2 
		                                    and dest.latitude between @lat1 and @lat2 
                                    having distance < {2} ORDER BY Distance;";

            return String.Format(command, 
                latitude.ToString(CultureInfo.InvariantCulture),
                longitude.ToString(CultureInfo.InvariantCulture),
                (precision / 1000).ToString(CultureInfo.InvariantCulture),
                distanceFactor.ToString(CultureInfo.InvariantCulture));
        }
    }
}
