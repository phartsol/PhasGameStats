using System;

namespace PhasGameStats.ViewModels
{
    public class ViewAllGamesVM
    {

        public int seasonid { get; set; }

        public string sporttypecd { get; set; }

        public int gameid { get; set; }

        public string homeTeamCode { get; set; }

        public string homeTeam { get; set; }

        public int homeTeamScore { get; set; }

        public string vistorTeamCode { get; set; }

        public string visitorTeam { get; set; }

        public int visitorTeamScore { get; set; }

        public DateTime gamedtm { get; set; }
    }
}