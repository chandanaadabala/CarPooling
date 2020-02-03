using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.Models
{
    public class RideViewModel
    {
        public string ID { get; set; }
        public UserViewModel RideBy { get; set; } = new UserViewModel();
        public int NumOfStopOvers { get; set; }
        public List<StationViewModel> Stations { get; set; } = new List<StationViewModel>();
        public decimal PricePerKM { get; set; } = 5;
        public int AvailableSeats { get; set; }
    }
}
