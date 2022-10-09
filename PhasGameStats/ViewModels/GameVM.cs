using System;

namespace PhasGameStats.ViewModels
{
    public class GameVM
    {
        public int gameId { get; set; }

        public int seasonId { get; set; }

        public bool preSeason { get; set; }

        public int? overTime { get; set; }

        public DateTime gameDtm { get; set; }

        public string gameStatus { get; set; }

        public string visitorTeam { get; set; }

        public string homeTeam { get; set; }

        public int visitorScore { get; set; }

        public int homeScore { get; set; }

        public string mod_user { get; set; }

        public DateTime mod_date { get; set; }

        public int playOffRnd { get; set; }

    }
}