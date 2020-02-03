using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Interfaces
{
    public interface IBookingServices
    {
        string CreateBooking(BookingViewModel booking);
        bool DeleteBooking(string ID);
        List<BookingViewModel> GetUserBookings(string userID);
        bool ApproveBooking(string ID);
        bool RejectBooking(string ID);
        List<BookingViewModel> GetApprovedBookingsOfRide(string rideID);
        List<BookingViewModel> GetUnApprovedBookingsOfRide(string rideID);
    }
}
