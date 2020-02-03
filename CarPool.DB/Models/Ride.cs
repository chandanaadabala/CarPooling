using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.DB.Models
{
    public class Ride
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }           
        [Required]
        public int NumOfStopOvers { get; set; }      
        [Required]
        public decimal PricePerKM { get; set; }       
        [Required]
        public int AvailableSeats { get; set; }
        public DateTime? RowModifiedOn { get; set; }
        public DateTime? RowDeletedOn { get; set; }
    }
}
