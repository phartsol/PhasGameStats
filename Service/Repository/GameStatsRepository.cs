using Data.Model;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Service.Repository
{
    public class GameStatsRepository : IGameStatsRepository
    {
        GameStatEntities _entities;

        public GameStatsRepository(GameStatEntities entities)
        {
            this._entities = entities;
        }


        //public string GetHomeTeam(int gameId)
        //{
        //    throw new NotImplementedException();
        //}

        //public int GetHomeTeamScore(int gameId)
        //{
        //    throw new NotImplementedException();
        //}

        //public string GetVisitorTeam(int gameId)
        //{
        //    throw new NotImplementedException();
        //}

        //public int GetVisitorTeamScore(int gameId)
        //{
        //    throw new NotImplementedException();
        //}


        public IEnumerable<SportType> GetAllSportTypes()
        {
            return _entities.SportTypes.ToList();

            //var sporttypes = _entities.SportsTypes.OrderBy(x => x.sportstypedesc).ToList();

            //var f = (from conf in _entities.Conferences
            //         join sport in _entities.SportsTypes on conf.sportstypecd equals sport.sportstypecd
            //         orderby sport.sportstypedesc
            //         select new
            //         {
            //             typecode = sport.sportstypecd,
            //             descrip = sport.sportstypedesc
            //         }).Distinct().ToList();

            //var f = 

            //List<SelectListItem> sports = new List<SelectListItem>();

            //foreach (var item in f)
            //{
            //    sports.Add(new SelectListItem() { Text = item.descrip, Value = item.typecode });
            //}

            //return sports;

            //return null;
        }


        public IEnumerable<Season> GetSeasonsForSportType(string sportType)
        {
            var q = _entities.Seasons.Where(x => x.SportTypeCode == sportType).OrderByDescending(y => y.startdate).ToList();

            return q;
        }


        public int NewSeasonBySportType(string sportType)
        {
            return _entities.Seasons.Where(x => x.SportTypeCode.Equals(sportType)).OrderByDescending(y => y.startdate).FirstOrDefault().startdate.Year + 1;
        }


        //public bool CheckForGames(int seasonid)
        //{
        //    return _entities.Games.Where(x => x.seasonid == seasonid).Any();
        //}


        //public DateTime GetMaxGameDate(int seasonId)
        //{
        //    return _entities.Games.Where(u => u.seasonid == seasonId).OrderByDescending(x => x.gamedtm).FirstOrDefault().gamedtm;
        //}


        public List<AllTeamRecordsPerctGamesBehindView> GetDivisionData(int seasonId, bool preseason, string confCode, string divCode)
        {
            if (String.IsNullOrEmpty(divCode))
            {
                return _entities.AllTeamRecordsPerctGamesBehindViews.Where(x => x.seasonid == seasonId && x.conference == confCode).OrderByDescending(y => y.PERCT).ToList();
            }
            else
            {
                return _entities.AllTeamRecordsPerctGamesBehindViews.Where(x => x.seasonid == seasonId && x.conference == confCode && x.division == divCode).OrderByDescending(y => y.PERCT).ToList();
            }
        }


        public int GetSeasonID(int year, string sportType)
        {
            int seasonid = _entities.Seasons.Where(x => x.SportTypeCode == sportType && x.startdate.Year == year).FirstOrDefault().seasonid;
            return seasonid;
        }


        public string GetSeasonSportType(int seasonId)
        {
            return _entities.Seasons.Where(x => x.seasonid == seasonId).FirstOrDefault().SportTypeCode;
        }


        public IEnumerable<AllGamesView> GetAllGamesBySportType(string sportType)
        {
            return _entities.AllGamesViews.Where(x => x.SportTypeCode.Equals(sportType)).ToList();
        }


        public IEnumerable<AllGamesView> GetAllGamesBySeasonBySportType(int seasonId, string sportType)
        {
            //var q = (from gm in _entities.AllGamesViews
            //         join s in _entities.Seasons on gm.SeasonId equals s.seasonid
            //         //join gm2 in _entities.AllGamesViews on s.startdate.Year equals
            //         where gm.SeasonId == seasonId || gm.GameDtm.Year < s.startdate.Year
            //         ).ToList();

            //return q;

            return _entities.AllGamesViews.Where(x => x.SportTypeCode.Equals(sportType) && x.SeasonId.Equals(seasonId)).ToList();
        }
 

        public string GetSportTypeBySeason(int seasonId)
        {
            return _entities.Seasons.Where(x => x.seasonid == seasonId).FirstOrDefault().SportTypeCode;
        }


        public List<GameList> GetRecentTeamVTeamGames(int homeFranchiseId, int visitorFranchiseId)
        {
            //var q = (from gm in _entities.Games
            //         join t1 in _entities.Teams on gm.hometeam equals t1.team_cd
            //         join t2 in _entities.Teams on gm.visitor equals t2.team_cd
            //         where ((gm.hometeam == TeamCd1 || gm.visitor == TeamCd1) && (gm.hometeam == TeamCd2 || gm.visitor == TeamCd2))
            //         select new
            //         {
            //            homecode = t1.team_cd,
            //            homecity = t1.city,
            //            homename = t1.teamname,
            //            visitorcode = t2.team_cd,
            //            visitorcity = t2.city,
            //            visitorname = t2.teamname,
            //            hscore = gm.hometeamscore,
            //            vscore = gm.visitorscore,
            //            gameday = gm.gamedtm
            //         }).OrderByDescending(y => y.gameday).ToList();

            List<GameList> gameList = new List<GameList>();

            //foreach (var item in q)
            //{
            //    GameListVM recentGame = new GameListVM();
            //    recentGame.homecode = item.homecode;
            //    recentGame.homecity = item.homecity;
            //    recentGame.homename = item.homename;
            //    recentGame.visitorcode = item.visitorcode;
            //    recentGame.visitorcity = item.visitorcity;
            //    recentGame.visitorname = item.visitorname;
            //    recentGame.hscore = item.hscore;
            //    recentGame.vScore = item.vscore;
            //    recentGame.gameday = item.gameday;
            //    gameList.Add(recentGame);
            //}

            return gameList;
        }


        public List<GameList> GetTeamVTeamGamesHomeAway(int homeFranchiseId, int visitorFranchiseId)
        {
            //var q = (from gm in _entities.Games
            //         join t1 in _entities.Teams on gm.hometeam equals t1.team_cd
            //         join t2 in _entities.Teams on gm.visitor equals t2.team_cd
            //         where (gm.hometeam == TeamCd1 && gm.visitor == TeamCd2)
            //         select new
            //         {
            //             homecode = t1.team_cd,
            //             homecity = t1.city,
            //             homename = t1.teamname,
            //             visitorcode = t2.team_cd,
            //             visitorcity = t2.city,
            //             visitorname = t2.teamname,
            //             hscore = gm.hometeamscore,
            //             vscore = gm.visitorscore,
            //             gameday = gm.gamedtm
            //         }).OrderByDescending(y => y.gameday).ToList();

            List<GameList> gameList = new List<GameList>();

            //foreach (var item in q)
            //{
            //    GameListVM homeAwayGame = new GameListVM();
            //    homeAwayGame.homecode = item.homecode;
            //    homeAwayGame.homecity = item.homecity;
            //    homeAwayGame.homename = item.homename;
            //    homeAwayGame.visitorcode = item.visitorcode;
            //    homeAwayGame.visitorcity = item.visitorcity;
            //    homeAwayGame.visitorname = item.visitorname;
            //    homeAwayGame.hscore = item.hscore;
            //    homeAwayGame.vScore = item.vscore;
            //    homeAwayGame.gameday = item.gameday;
            //    gameList.Add(homeAwayGame);
            //}

            return gameList;
        }



        public IEnumerable<SelectListItem> GetAllTeams(int newSportYear, string sportType)
        {
            //var q = (from tm in _entities.Teams
            //         join div in _entities.Divisions on tm.div_cd equals div.div_cd
            //         join conf in _entities.Conferences on div.conf_cd equals conf.conf_cd
            //         where (tm.enddate == null || tm.enddate > System.DateTime.Now)
            //         where (tm.startdate.Year <= newSportYear && conf.sportstypecd == sportType)
            //         orderby tm.city
            //         select new
            //         {
            //             typecode = tm.team_cd,
            //             name = tm.teamname,
            //             towncity = tm.city
            //         }).ToList();

            List<SelectListItem> teams = new List<SelectListItem>();

            //var q;
            //foreach (var item in q)
            //{
            //    string teamcity = string.Concat(item.towncity, " ", item.name);

            //    teams.Add(new SelectListItem { Text = teamcity, Value = item.typecode });
            //}

            return teams;
        }



        public IEnumerable<SelectListItem> GetGameStatuses()
        {
            //var q = _entities.GameStatus.OrderBy(x => x.status).ToList();

            //var q;
            List<SelectListItem> statuslist = new List<SelectListItem>();

            //foreach (var item in q)
            //{
            //    statuslist.Add(new SelectListItem() { Text = item.status, Value = item.gstatus_cd });
            //}

            return statuslist;
        }


        public int GetSportTypeId(string sportType)
        {
            return 1;
        }


        public int GetSportTypeStartDate(string sportType)
        {
            return _entities.SportTypes.Where(x => x.SportTypeCode == sportType).FirstOrDefault().SeasonStartMonth;
        }


        public int SaveSeason(string sportType, DateTime seasonStartDate)
        {
            Season newSeason = new Season();

            newSeason.SportTypeCode = sportType;
            newSeason.startdate = seasonStartDate;
            newSeason.mod_user = "admin";
            newSeason.mod_date = DateTime.Now;

            _entities.Seasons.Add(newSeason);

            try
            {
                _entities.SaveChanges();

                int id = newSeason.seasonid;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);

                        // raise a new exception nesting the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return 0;
        }
    }
}
