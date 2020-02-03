using CarPool.DB.Models;
using CarPool.Models;
using CarPool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CarPool.Services
{
    public class BookingServices : IBookingServices
    {
        /// <summary>
        /// This method used to approve the booking of the user by the ride creator
        /// </summary>
        public bool ApproveBooking(string ID)
        {
            try
            {
                using var _context = new CarpoolContext();
                Booking bookingEntity = _context.Bookings.SingleOrDefault(b => b.ID == ID && b.RowDeletedOn==null);
                if (bookingEntity == null) return false;
                bookingEntity.Approval = true;
                bookingEntity.RowModifiedOn = DateTime.Now;
                Ride rideEntity = _context.Rides.SingleOrDefault(r => r.ID == bookingEntity.RideID && r.RowDeletedOn == null);
                if (rideEntity == null) return false;
                if (rideEntity.AvailableSeats >= bookingEntity.Seats)
                    rideEntity.AvailableSeats -= bookingEntity.Seats;
                else
                    rideEntity.AvailableSeats = 0;
                rideEntity.RowModifiedOn = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        /// <summary>
        /// This method creates booking of the user for the corresponding ride
        /// </summary>
        public string CreateBooking(BookingViewModel booking)
        {
            try
            {
                using var _context = new CarpoolContext();
                Guid guid = Guid.NewGuid();
                Booking bookingEntity = new Booking
                {
                    ID = guid.ToString(),
                    RideID = booking.Ride.ID,
                    UserID = booking.BookedBy.ID,
                    BoardingID = booking.BoardingStation.ID,
                    DroppingID = booking.DroppingStation.ID,
                    Seats = booking.Seats,
                    Approval = null
                };
                _context.Add(bookingEntity);
                _context.SaveChanges();
                return bookingEntity.ID;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// This method deletes the booking of the user
        /// </summary>
        public bool DeleteBooking(string ID)
        {
            try
            {
                using var _context = new CarpoolContext();
                Booking bookingEntity = _context.Bookings.SingleOrDefault(b => b.ID == ID && b.RowDeletedOn==null);
                if (bookingEntity == null) return false;
                bookingEntity.RowDeletedOn = DateTime.Now;
                Ride rideEntity = _context.Rides.SingleOrDefault(r => r.ID == bookingEntity.RideID && r.RowDeletedOn == null);
                if (rideEntity == null) return false;
                rideEntity.AvailableSeats += bookingEntity.Seats;
                rideEntity.RowModifiedOn = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
                
        }
        /// <summary>
        /// This method returns list bookings of an user
        /// </summary>
        public List<BookingViewModel> GetUserBookings(string userID)
        {
            try
            {
                using var _context = new CarpoolContext();
                List<BookingViewModel> bookings
                    = _context.Bookings.Where(b => b.UserID == userID && b.RowDeletedOn==null)
                    .Select(b => new BookingViewModel 
                                    { ID = b.ID,
                                      Ride = _context.Rides.Where(r=>r.ID==b.RideID).Select(r=>new RideViewModel
                                      {
                                          ID=b.RideID,
                                          NumOfStopOvers = r.NumOfStopOvers,
                                          PricePerKM = r.PricePerKM,
                                          RideBy = _context.Users.Where(u => u.ID == b.UserID).Select(u => new UserViewModel { ID = u.ID, Name = u.Name, Email = u.Email, MobileNum = u.MobileNum }).ToList()[0],
                                          Stations = _context.Stations.Where(s=>s.RideID==r.ID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList(),                                      
                                      }).ToList()[0],
                                      BoardingStation = _context.Stations.Where(s => s.ID == b.BoardingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                                      DroppingStation = _context.Stations.Where(s => s.ID == b.DroppingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                                      Seats = b.Seats,
                                      Approval = b.Approval
                                      
                    }).ToList();

                return bookings;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// This method returns list of approved booking of a ride
        /// </summary>
        public List<BookingViewModel> GetApprovedBookingsOfRide(string rideID)
        {
            try
            {
                using var _context = new CarpoolContext();
                List<BookingViewModel> bookings
                    = _context.Bookings.Where(b => b.RideID == rideID && b.RowDeletedOn == null && b.Approval==true)
                    .Select(b => new BookingViewModel
                    {
                        ID = b.ID,
                        Seats = b.Seats,
                        Approval = b.Approval,
                        BoardingStation = _context.Stations.Where(s => s.ID == b.BoardingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                        DroppingStation = _context.Stations.Where(s => s.ID == b.DroppingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                        BookedBy = _context.Users.Where(u => u.ID == b.UserID).Select(u => new UserViewModel { ID = u.ID, Name = u.Name, Email = u.Email, MobileNum = u.MobileNum }).ToList()[0],

                    }).ToList();

                return bookings;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// This method returns unapproved bookings of a ride
        /// </summary>
        public List<BookingViewModel> GetUnApprovedBookingsOfRide(string rideID)
        {
            try
            {
                using var _context = new CarpoolContext();
                List<BookingViewModel> bookings
                    = _context.Bookings.Where(b => b.RideID == rideID && b.RowDeletedOn == null && b.Approval==null)
                    .Select(b => new BookingViewModel
                    {
                        ID = b.ID,
                        Seats = b.Seats,
                        Approval = b.Approval,
                        BoardingStation = _context.Stations.Where(s => s.ID == b.BoardingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                        DroppingStation = _context.Stations.Where(s => s.ID == b.DroppingID).Select(s => new StationViewModel { ID = s.ID, Name = s.Name, DistanceFromSrc = s.Distance, OrderNo = s.OrderNo, Time = s.Time }).ToList()[0],
                        BookedBy = _context.Users.Where(u => u.ID == b.UserID).Select(u => new UserViewModel { ID = u.ID, Name = u.Name, Email = u.Email, MobileNum = u.MobileNum }).ToList()[0]

                    }).ToList();

                return bookings;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// This method rejects the booking of the user of a particular ride by making it approval status false
        /// </summary>
        public bool RejectBooking(string ID)
        {
            try
            {
                using var __context = new CarpoolContext();
                Booking bookingEntity = __context.Bookings.SingleOrDefault(b => b.ID == ID);
                if (bookingEntity == null) return false;
                bookingEntity.Approval = false;
                bookingEntity.RowModifiedOn = DateTime.Now;
                __context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
