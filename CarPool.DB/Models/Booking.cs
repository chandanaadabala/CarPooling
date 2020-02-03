using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.DB.Models
{
    public class Booking
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string RideID { get; set; }
        [ForeignKey("RideID")]
        public Ride Ride { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [Required]
        public string BoardingID { get; set; }
        [ForeignKey("BoardingID")]
        public Station BoardingStation { get; set; }
        [Required]
        public string DroppingID { get; set; }
        [ForeignKey("DroppingID")]
        public Station DroppingStation { get; set; }
        [Required]
        public int Seats { get; set; }
        public bool? Approval { get; set; }
        public DateTime? RowModifiedOn { get; set; }
        public DateTime? RowDeletedOn { get; set; }
    }
}
