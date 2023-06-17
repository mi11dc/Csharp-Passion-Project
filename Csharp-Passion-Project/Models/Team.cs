using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime? FormedOn { get; set; }
    }

    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime FormedOn { get; set; }
    }
}