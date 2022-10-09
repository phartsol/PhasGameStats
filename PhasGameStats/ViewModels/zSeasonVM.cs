using System;

namespace PhasGameStats.ViewModels
{
    public class zSeasonVM
    {

        public int seasonId { get; set; }

        public string sportTypeCd { get; set; }

        public DateTime startDate { get; set; }

        public string mod_user { get; set; }

        public DateTime mod_date { get; set; }
    }
}