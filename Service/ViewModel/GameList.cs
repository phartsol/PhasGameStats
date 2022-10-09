using System;
using System.Collections.Generic;

namespace Service.ViewModel
{
    public class GameList
    {

        public int HomeFranchiseId { get; set; }
        public string HomeTeam { get; set; }
        public int HomeScore { get; set; }
        public int VisitorFranchiseId { get; set; }
        public string VisitorTeam { get; set; }
        public int VisitorScore { get; set; }
        public DateTime gameday { get; set; }


    public static implicit operator List<object>(GameList v)
        {
            throw new NotImplementedException();
        }
    }
}
