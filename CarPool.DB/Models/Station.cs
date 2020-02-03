using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.DB.Models
{
    public class Station
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string RideID { get; set; }
        [ForeignKey("RideID")]
        public Ride Ride { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public double Distance  { get; set; }
        [Required]
        public int OrderNo { get; set; }
        public DateTime? RowModifiedOn { get; set; }
        public DateTime? RowDeletedOn { get; set; }

    }
}
