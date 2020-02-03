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
    public class CarServices:ICarServices
    {
        /// <summary>
        /// This method add car of a particular user
        /// </summary>
        internal bool AddCar(string UserID, CarViewModel car)
        {
            try
            {
                using var _context = new CarpoolContext();
                Guid guid = Guid.NewGuid();
                Car carEntity = new Car
                {
                    ID = guid.ToString(),
                    Name = car.Name,
                    NumberPlate = car.NumberPlate,
                    UserID = UserID

                };
                _context.Add(carEntity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// This method returns car details of an user as object
        /// </summary>
        internal CarViewModel GetCar(string UserID)
        {
            try
            {
                using var _context = new CarpoolContext();
                List<CarViewModel> car = _context.Cars.Where(c => c.UserID == UserID && c.RowDeletedOn==null).Select(c=>new CarViewModel(c.ID,c.Name,c.NumberPlate)).ToList();
                if (car.Count == 0) return null;
                return car[0];
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        /// <summary>
        /// This method updates the car details
        /// </summary>
        public bool UpdateCar(string userID, CarViewModel car)
        {
            try
            {
                using var _context = new CarpoolContext();
                Car carEntity = _context.Cars.SingleOrDefault(c => c.UserID == userID && c.RowDeletedOn==null);
                if (carEntity == null) return AddCar(userID, car);
                carEntity.Name = car.Name;
                carEntity.NumberPlate = car.NumberPlate;
                carEntity.RowModifiedOn = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// This method checks if a particular user have car or not
        /// </summary>
        public bool IsUserHaveCar(string userID)
        {
            try
            {
                using var _context = new CarpoolContext();
                string carID = _context.Cars.SingleOrDefault(c => c.UserID == userID && c.RowDeletedOn==null).ID;
                return !string.IsNullOrEmpty(carID);
            }
            catch(Exception e)
            {
                return false;
            }
           
        }

        /// <summary>
        /// This method checks if that particular car already registered or not
        /// </summary>
        public bool IsCarExist(string numberPlate)
        {

            try
            {
                using var _context = new CarpoolContext();
                string carID = _context.Cars.SingleOrDefault(c => c.NumberPlate == numberPlate && c.RowDeletedOn == null).ID;
                return !string.IsNullOrEmpty(carID);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
