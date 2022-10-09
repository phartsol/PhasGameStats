//using PhasGameStats.Models;

namespace PhasGameStats.ViewModels
{
    public class TeamRecordVM
    {
        public int FranchiseId { get; set; }
        public string Team { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public double GB { get; set; }
        public string percent { get; set; }
        public string HomeRec { get; set; }
        public string AwayRec { get; set; }
    }
}