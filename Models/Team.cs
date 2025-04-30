using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Logo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد المباريات صفر أو أكثر")]
        public int GamesPlayed { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد مرات الفوز صفر أو أكثر")]
        public int Wins { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد مرات التعادل صفر أو أكثر")]
        public int Draws { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد مرات الخسارة صفر أو أكثر")]
        public int Losses { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد الأهداف صفر أو أكثر")]
        public int GoalsFor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "يجب أن يكون عدد الأهداف صفر أو أكثر")]
        public int GoalsAgainst { get; set; }

        public int Points
        {
            get
            {
                return (Wins * 3) + Draws;
            }
            private set { }
        }

        // Navigation properties
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}