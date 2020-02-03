using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Interfaces
{
    public interface ICarServices
    {
        bool UpdateCar(string userID, CarViewModel car);
        bool IsUserHaveCar(string userID);
        bool IsCarExist(string numberPlate);
    }
}
