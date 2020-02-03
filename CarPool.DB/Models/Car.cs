using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.DB.Models
{
    public class Car
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NumberPlate { get; set; }
        public DateTime? RowModifiedOn { get; set; }
        public DateTime? RowDeletedOn { get; set; }
    }
}
