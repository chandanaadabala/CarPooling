using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPool.DB.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string MobileNum { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? RowModifiedOn { get; set; }
        public DateTime? RowDeletedOn { get; set; }

    }
}
