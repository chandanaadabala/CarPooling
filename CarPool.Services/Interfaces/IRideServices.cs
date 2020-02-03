using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Interfaces
{
    public interface IRideServices
    {
        bool CreateRide(RideViewModel ride);
        List<RideViewModel> GetUserCreatedRides(string userID);
        List<RideViewModel> GetAvailableRides(string source, string destination, DateTime date, string userID);
    }
}
