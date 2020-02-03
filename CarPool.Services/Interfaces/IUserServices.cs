using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Interfaces
{
    public interface IUserServices
    {
        string AddUser(UserViewModel user);
        string Login(string email, string password);
        UserViewModel GetProfile(string ID);
        bool UpdateProfile(string ID, string field, string value);
        bool IsEmailExist(string email);

               
    }
}
