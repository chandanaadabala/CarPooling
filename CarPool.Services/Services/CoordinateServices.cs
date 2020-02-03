using CarPool.Models;
using CarPool.Services.Interfaces;
using CarPool.Services.MockData;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services
{
    public class CoordinateServices:ICoordinateServices
    {
        /// <summary>
        /// This method calculate distance from station to station and return that value.
        /// </summary>
        internal double GetDistance(string source, string destination)
        {
            double sLatitude = Coordinate.Locations[source].Lattitude;
            double sLongitude = Coordinate.Locations[source].Longitude;
            double dLatitude = Coordinate.Locations[destination].Lattitude;
            double dLongitude = Coordinate.Locations[destination].Longitude;
            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(dLatitude, dLongitude);
            return sCoord.GetDistanceTo(eCoord)/1000;
        }

        /// <summary>
        /// This method  assign distance value to each corresponding station
        /// </summary>
        public void CalculateDistance(ref List<StationViewModel> stations)
        {
            double distance = 0;
            for(int i=0;i<stations.Count-1;i++)
            {
                distance += GetDistance(stations[i].Name, stations[i + 1].Name);
                stations[i + 1].DistanceFromSrc = distance;
            }
        }
    }
}
