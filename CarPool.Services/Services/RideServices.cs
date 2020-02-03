using CarPool.Models;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CarPool.DB.Models;
using System.Linq;
using CarPool.Services;
using System.Data;

namespace CarPool.Services
{
    public class RideServices:IRideServices
    {
       /// <summary>
       /// This method add ride to the application
       /// </summary>       
        public bool CreateRide(RideViewModel ride)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                using var _context = new CarpoolContext();
                Ride rideEntity = new Ride()
                {
                    ID = guid.ToString(),
                    UserID = ride.RideBy.ID,
                    NumOfStopOvers = ride.NumOfStopOvers,
                    PricePerKM = ride.PricePerKM,
                    AvailableSeats = ride.AvailableSeats
                };
                _context.Add(rideEntity);
                _context.SaveChanges();
                StationServices stationServices = new StationServices();
                if (stationServices.AddStations(rideEntity.ID, ride.Stations))
                    return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
         

        }

        /// <summary>
        /// This method returns list of available rides to the user
        /// </summary> 
        public List<RideViewModel> GetAvailableRides(string source, string destination, DateTime date, string userID)
        {
           
            using var _context = new CarpoolContext();
            var groupedStations = _context.Stations.Where(s => s.RowDeletedOn == null).ToLookup(s => s.RideID);

            List<RideViewModel> rides = new List<RideViewModel>();
            foreach (var stations in groupedStations)
            {
                string rideID = stations.Key;
                Station srcStation = stations.SingleOrDefault(s => s.Name == source && s.Time.ToShortDateString() == date.ToShortDateString());
                Station desStation = stations.SingleOrDefault(s => s.Name == destination);
                if (srcStation == null || desStation == null) continue;
                if (srcStation.OrderNo<desStation.OrderNo)
                {
                    Ride rideEntity = _context.Rides.SingleOrDefault(r => r.ID == rideID && r.UserID!=userID);
                    if (rideEntity == null) continue;
                    RideViewModel ride = new RideViewModel
                    {
                        ID = rideID,
                        Stations = stations.Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList(),
                        AvailableSeats = rideEntity.AvailableSeats,
                        NumOfStopOvers = rideEntity.NumOfStopOvers,
                        PricePerKM = rideEntity.PricePerKM,
                        RideBy = _context.Users.Where(u => u.ID == rideEntity.UserID).Select(u=>new UserViewModel { ID=u.ID,Name=u.Name,Email=u.Email,MobileNum=u.MobileNum}).ToList()[0]
                    };
                    ride.AvailableSeats = GetAvailableSeats(rideID,srcStation,desStation, ride.AvailableSeats);
                    if(ride.AvailableSeats>0)
                        rides.Add(ride);
                }
            }
            return rides;
        }

        /// <summary>
        /// This method caluculates and return available seats for a particular ride
        /// </summary> 
        private int GetAvailableSeats(string rideID, Station srcStation, Station desStation, int presentAvailableSeats)
        {
            using var _context = new CarpoolContext();
            int availableSeats = 0;
            List<BookingViewModel> bookings = _context.Bookings.Where(b => b.RideID == rideID && b.Approval == true && b.RowDeletedOn == null)
                                            .Select(b => new BookingViewModel
                                            {
                                                ID = b.ID,
                                                Seats = b.Seats,
                                                BoardingStation = new StationViewModel { OrderNo = b.BoardingStation.OrderNo },
                                                DroppingStation = new StationViewModel { OrderNo = b.DroppingStation.OrderNo },
                                               
                                            }).ToList();
            foreach(BookingViewModel booking in bookings)
            {
                if (booking.BoardingStation.OrderNo >= desStation.OrderNo || booking.DroppingStation.OrderNo <= srcStation.OrderNo)
                    availableSeats += booking.Seats;
                
            }

            return presentAvailableSeats+availableSeats;

        }

        /// <summary>
        /// This method returns list of rides created by user
        /// </summary> 
        public List<RideViewModel> GetUserCreatedRides(string userID)
        {
            using var _context = new CarpoolContext();
            List<RideViewModel> rides = _context.Rides.Where(r => r.UserID == userID && r.RowDeletedOn == null)
                                    .Select(r => new RideViewModel
                                    {
                                        ID = r.ID,
                                        AvailableSeats = r.AvailableSeats,
                                        NumOfStopOvers = r.NumOfStopOvers,
                                        PricePerKM = r.PricePerKM,
                                        Stations = _context.Stations.Where(s=>s.RideID==r.ID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList(),
                                    }).ToList();
            return rides;
        }
    }
}
