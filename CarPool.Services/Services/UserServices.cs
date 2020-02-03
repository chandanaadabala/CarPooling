using CarPool.Models;
using CarPool.Services.Interfaces;
using CarPool.DB.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPool.Services;

namespace CarPool.Services
{
    public class UserServices:IUserServices
    {
        /// <summary>
        /// This method add user details to database
        /// </summary> 
        public string AddUser(UserViewModel user)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                using (var _context = new CarpoolContext())
                {
                    User userEntity = new User
                    {
                        ID = guid.ToString(),
                        Email = user.Email,
                        Name = user.Name,
                        MobileNum = user.MobileNum,
                        Password = user.Password

                    };
                    _context.Users.Add(userEntity);
                    _context.SaveChanges();
                    if (user.Car != null)
                    {
                        CarServices carServices = new CarServices();
                        carServices.AddCar(userEntity.ID, user.Car);
                    }
                  
                    return userEntity.ID;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// This method returns user details by taking userID as parameter
        /// </summary> 
        public UserViewModel GetProfile(string ID)
        {
            try
            {
                using var _context = new CarpoolContext();
                User userEntity = _context.Users.SingleOrDefault(u => u.ID == ID && u.RowDeletedOn == null);
                CarServices carServices = new CarServices();
                CarViewModel car = carServices.GetCar(ID);
                UserViewModel user = new UserViewModel(userEntity.Name, userEntity.Email, userEntity.Password, userEntity.MobileNum, car);
                return user;
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        /// <summary>
        /// This method checks if the email already exist in the database or not
        /// </summary> 
        public bool IsEmailExist(string email)
        {
            try
            {
                using var _context = new CarpoolContext();
                User userEntity = _context.Users.SingleOrDefault(u => u.Email == email  && u.RowDeletedOn == null);
                return (userEntity != null);
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// This method authenticate the user with by taking email and password as parameters
        /// </summary> 
        public string Login(string email, string password)
        {
            try
            {
                using var _context = new CarpoolContext();
                User userEntity = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password && u.RowDeletedOn == null);
                return (userEntity == null) ? null : userEntity.ID;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        /// <summary>
        /// This method takes field name and value to update as parameters and update the user details
        /// </summary>        
        public bool UpdateProfile(string ID, string field, string value)
        {
            try
            {
                using var _context = new CarpoolContext();
                User userEntity = _context.Users.SingleOrDefault(u => u.ID == ID);
                switch (field)
                {
                    case "Name":
                        userEntity.Name = value;
                        userEntity.RowModifiedOn = DateTime.Now;
                        break;
                    case "Email":
                        userEntity.Email = value;
                        userEntity.RowModifiedOn = DateTime.Now;
                        break;
                    case "MobileNum":
                        userEntity.MobileNum = value;
                        userEntity.RowModifiedOn = DateTime.Now;
                        break;
                    case "Password":
                        userEntity.Password = value;
                        userEntity.RowModifiedOn = DateTime.Now;
                        break;
                    default:
                        return false;
                }
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
