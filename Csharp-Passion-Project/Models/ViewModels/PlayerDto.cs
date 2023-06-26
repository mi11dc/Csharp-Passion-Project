using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime DOB { get; set; }

        public string SDOB { get; set; }

        public string Country { get; set; }

        public decimal BasePrice { get; set; }

        public int TeamId { get; set; }
    }
}