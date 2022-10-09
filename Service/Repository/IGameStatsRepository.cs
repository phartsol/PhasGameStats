using Data.Model;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Service.Repository
{
    public interface IGameStatsRepository
    {

        IEnumerable<SportType> GetAllSportTypes();

        IEnumerable<Season> GetSeasonsForSportType(string sporttype);

        int NewSeasonBySportType(string sporttype);

        //bool CheckForGames(int seasonid);

        //DateTime GetMaxGameDate(int seasonid);

        List<AllTeamRecordsPerctGamesBehindView> GetDivisionData(int seasonid, bool preseason, string confcode, string divcode);

        int GetSeasonID(int year, string sporttype);

        string GetSeasonSportType(int seasonid);

        IEnumerable<AllGamesView> GetAllGamesBySportType(string sporttype);

        IEnumerable<AllGamesView> GetAllGamesBySeasonBySportType(int seasonId, string sporttype);

        string GetSportTypeBySeason(int seasonid);

        List<GameList> GetRecentTeamVTeamGames(int homeFranchiseId, int visitorFranchiseId);

        List<GameList> GetTeamVTeamGamesHomeAway(int homeFranchiseId, int visitorFranchiseId);

        IEnumerable<SelectListItem> GetAllTeams(int newsportyear, string sporttype);

        IEnumerable<SelectListItem> GetGameStatuses();

        int GetSportTypeId(string sporttype);

        int GetSportTypeStartDate(string sportType);

        int SaveSeason(string sportType, DateTime seasonStartDate);
    }
}
