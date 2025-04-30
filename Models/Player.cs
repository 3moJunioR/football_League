using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        public int Number { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

        // Foreign key
        public int TeamId { get; set; }
        
        // Navigation property
        public virtual Team Team { get; set; }
    }
}