using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime FormedOn { get; set; }

        public string SFormedOn { get; set; }

        public List<TeamPlayerDetailsWithPlayerDto> teamPlayers { get; set; }
    }
}