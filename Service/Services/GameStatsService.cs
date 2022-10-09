using Data.Model;
using Service.Factory;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Service.Services
{
    public partial class GameStatsService : IDisposable
    {
        private IGameStatsUnitOfWork _unitOfWork;


        public GameStatsService() : this(new GameStatsUnitOfWork())
        {

        }


        public GameStatsService(IGameStatsUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public void Dispose()
        {
            try
            {
                _unitOfWork.Dispose();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }


        public IEnumerable<SelectListItem> GetAllSportTypes()
        {
            var x = _unitOfWork.GameStatsRepository.GetAllSportTypes(); 

            //IEnumerable<SelectListItem> sportTypeList = new List<SelectListItem>();

            List<SelectListItem> sportTypeList = new List<SelectListItem>();

            foreach (var item in x)
            {
                sportTypeList.Add(new SelectListItem() { Text = item.SportTypeShortName, Value = item.SportTypeCode });
            }

            return sportTypeList;
        }


        public List<int> GetSeasonsForSportType(string sporttype)
        {
            List<int> x = new List<int>();

            IEnumerable<Season> seasons = _unitOfWork.GameStatsRepository.GetSeasonsForSportType(sporttype);

            foreach (var item in seasons)
            {
                int season = item.startdate.Year;
                x.Add(season);
            }

            return x;
        }


        public int NewSeasonBySportType(string sporttype)
        {
            return _unitOfWork.GameStatsRepository.NewSeasonBySportType(sporttype);
        }


        //public bool CheckForGames(int seasonid)
        //{
        //    return _unitOfWork.GameStatsRepository.CheckForGames(seasonid);
        //}


        //public DateTime GetMaxGameDate(int seasonid)
        //{
        //    return _unitOfWork.GameStatsRepository.GetMaxGameDate(seasonid);
        //}


        public List<AllTeamRecordsPerctGamesBehindView> GetDivisionData(int seasonid, bool preseason, string confcode, string divcode)
        {
            var division = _unitOfWork.GameStatsRepository.GetDivisionData(seasonid, preseason, confcode, divcode);

            return division;
        }


        public int GetSeasonID(int year, string sporttype)
        {
            return _unitOfWork.GameStatsRepository.GetSeasonID(year, sporttype);
        }


        public string GetSeasonSportType(int seasonid)
        {
            return _unitOfWork.GameStatsRepository.GetSeasonSportType(seasonid);
        }


        public IEnumerable<AllGamesView> GetAllGames(string sporttype)
        {
            return _unitOfWork.GameStatsRepository.GetAllGamesBySportType(sporttype).ToList();
        }


        public IEnumerable<AllGamesView> GetRecentGamesByTeamBySeason(int seasonId, string sporttype, int franchiseId)
        {
            return _unitOfWork.GameStatsRepository.GetAllGamesBySeasonBySportType(seasonId, sporttype).Where(x => x.HomeFranchiseId.Equals(franchiseId) || x.VisitorFranchiseId.Equals(franchiseId)).ToList();
        }

        public string GetSportTypeBySeason(int seasonid)
        {
            return _unitOfWork.GameStatsRepository.GetSportTypeBySeason(seasonid);
        }


        //public List<GameListVM> GetTeamVTeamGames(string teamCd1, string teamCd2)
        public IEnumerable<GameList> GetTeamVTeamGames(string sporttype, int homeFranchiseId, int visitorFranchiseId)
        {
            var q = GetAllGames(sporttype).Where(x => (x.HomeFranchiseId.Equals(homeFranchiseId) && x.VisitorFranchiseId.Equals(visitorFranchiseId)) ||
                                                      (x.HomeFranchiseId.Equals(visitorFranchiseId) && x.VisitorFranchiseId.Equals(homeFranchiseId))).ToList();

            List<GameList> TeamVTeamList = new List<GameList>();

            foreach (var item in q)
            {
                GameList TeamVTeam = new GameList();
                TeamVTeam.HomeFranchiseId = item.HomeFranchiseId;
                TeamVTeam.HomeTeam = item.HomeTeam;
                TeamVTeam.HomeScore = (int)item.HomeScore;
                TeamVTeam.VisitorFranchiseId = item.VisitorFranchiseId;
                TeamVTeam.VisitorTeam = item.VisitorTeam;
                TeamVTeam.VisitorScore = (int)item.VisitorScore;
                TeamVTeam.gameday = item.GameDtm;

                TeamVTeamList.Add(TeamVTeam);
            }

            return TeamVTeamList;
        }


        public List<GameList> GetTeamVTeamGamesHomeAway(string sporttype, int homeFranchiseId, int visitorFranchiseId)
        {
            return GetTeamVTeamGames(sporttype, homeFranchiseId, visitorFranchiseId).Where(x => x.HomeFranchiseId.Equals(homeFranchiseId) && x.VisitorFranchiseId.Equals(visitorFranchiseId)).ToList();
        }

        public IEnumerable<SelectListItem> GetAllTeams(int year, string sporttype)
        {
            return _unitOfWork.GameStatsRepository.GetAllTeams(year, sporttype);
        }


        public IEnumerable<SelectListItem> GetGameStatuses()
        {
            return _unitOfWork.GameStatsRepository.GetGameStatuses();
        }



        public int GetSportTypeId(string sporttype)
        {
            return _unitOfWork.GameStatsRepository.GetSportTypeId(sporttype);
        }


        public int GetSportTypeStartDate(string sportType)
        {
            return _unitOfWork.GameStatsRepository.GetSportTypeStartDate(sportType);
        }


        public int SaveSeason(string sportType, DateTime seasonStartDate)
        {
           return  _unitOfWork.GameStatsRepository.SaveSeason(sportType, seasonStartDate);
        }












    }
}
