using CarPool.DB.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CarPool.Services
{
    public class StationServices: IStationServices
    {
        /// <summary>
        /// This method add stations of a ride to database
        /// </summary> 
        internal bool AddStations(string rideID, List<StationViewModel> stations)
        {
            try
            {
                using var _context = new CarpoolContext();
                List<Station> stationEntities = new List<Station>();
                foreach (StationViewModel station in stations)
                {
                    Guid guid = Guid.NewGuid();
                    Station stationEntity = new Station
                    {
                        ID = guid.ToString(),
                        RideID = rideID,
                        Name = station.Name,
                        OrderNo = station.OrderNo,
                        Distance = station.DistanceFromSrc,
                        Time = station.Time
                    };
                    stationEntities.Add(stationEntity);
                }
                _context.AddRange(stationEntities);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
