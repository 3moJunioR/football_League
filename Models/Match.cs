using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime MatchDate { get; set; }

        // Foreign keys
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        [StringLength(100)]
        public string Stadium { get; set; }

        public string Status { get; set; } // Scheduled, In Progress, Completed, Postponed

        // Navigation properties
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
    }
}