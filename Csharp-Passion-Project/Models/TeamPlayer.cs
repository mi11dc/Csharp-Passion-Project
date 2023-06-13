using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Csharp_Passion_Project.Models
{
    public class TeamPlayer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Players")]
        public int PlayerId { get; set; }

        [ForeignKey("Teams")]
        public int TeamId { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required]
        public decimal JoinedPrice { get; set; }

        // Reference Tables
        public virtual Team Teams { get; set; }

        public virtual Player Players { get; set; }
    }

    public class TeamPlayerDto
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public DateTime JoinedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal JoinedPrice { get; set; }
    }
}