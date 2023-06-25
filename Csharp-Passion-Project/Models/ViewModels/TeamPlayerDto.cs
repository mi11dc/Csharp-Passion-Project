using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class TeamPlayerDto
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        
        public string PlayerName { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public DateTime JoinedDate { get; set; }

        public decimal JoinedPrice { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}