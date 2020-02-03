using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.Models
{
    public class BookingViewModel
    {
        public string ID { get; set; }    
        public RideViewModel Ride { get; set; }
        public UserViewModel BookedBy { get; set; } = new UserViewModel();
        public StationViewModel BoardingStation { get; set; } = new StationViewModel();
        public StationViewModel DroppingStation { get; set; } = new StationViewModel();
        public int Seats { get; set; }
        public bool? Approval { get; set; }
    }
}
