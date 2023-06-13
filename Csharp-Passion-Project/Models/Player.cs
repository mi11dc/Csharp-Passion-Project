using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public decimal BasePrice { get; set; }
    }
}