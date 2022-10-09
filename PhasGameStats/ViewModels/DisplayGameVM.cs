namespace PhasGameStats.ViewModels
{
    public class DisplayGameVM
    {
        public int HomeFranchiseId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorFranchiseId { get; set; }
        public string VisitorTeam { get; set; }
        public int VisitorTeamScore { get; set; }
        public bool OverTime { get; set; }
        public string gameday { get; set; }
    }
}