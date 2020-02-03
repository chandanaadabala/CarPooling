using CarPool.Models;
using CarPool.Services;
using CarPool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.UI
{
    //This class contains functionality for user who takes a ride
    class RideTakerView
    {
        IBookingServices _bookingServices;
        public RideTakerView()
        {
            _bookingServices = new BookingServices();
        }

        internal void DisplayBookings(string userID)
        {
            List<BookingViewModel> bookings = _bookingServices.GetUserBookings(userID);
            if (bookings.Count == 0)
                Console.WriteLine("No bookings!");
            else
            {
                Console.WriteLine("Created bookings:\n");

                foreach (BookingViewModel booking in bookings)
                {
                    Console.WriteLine("Ride details:");
                    Console.WriteLine(" Ride creator name:" + booking.Ride.RideBy.Name +
                                       "\n  Contact Details:" +
                                       "\n   Email:" + booking.Ride.RideBy.Email +
                                       "\n   Mobile Number:" + booking.Ride.RideBy.MobileNum +
                                       "\n Booking details:" +
                                       "\n  Boarding Station:" + booking.BoardingStation.Name +
                                       "\n  Dropping Station:" + booking.DroppingStation.Name +
                                       "\n  Estimated Boarding Time:" + booking.BoardingStation.Time +
                                       "\n  Estimated Dropping Time:" + booking.DroppingStation.Time +
                                       "\n Seats booked:" + booking.Seats);
                    Console.Write(" Approval Status: ");
                    switch (booking.Approval)
                    {
                        case true:
                            Console.WriteLine("Confirmed");
                            break;
                        case false:
                            Console.WriteLine("Rejected");
                            break;
                        case null:
                            Console.WriteLine("Not yet approved");
                            break;
                    }
                    Console.WriteLine("*******************************************************************************");
                }

            }
        }

      
    }
}
