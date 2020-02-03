using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(string name, string email, string password, string mobileNum, CarViewModel car )
        {
            Name = name;
            Email = email;
            Password = password;
            MobileNum = mobileNum;
            Car = car;
        
        }
        public string ID { get; set; }      
        public string Name { get; set; }      
        public string Email { get; set; }
        public string MobileNum { get; set; }      
        public string Password { get; set; }
        public CarViewModel Car { get; set; } = new CarViewModel();
    }
}
