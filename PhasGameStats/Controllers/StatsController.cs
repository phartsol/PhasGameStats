using PhasGameStats.Utilities;
using PhasGameStats.ViewModels;
using Service.Services;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
//using Service.Services;


namespace PhasGameStats.Controllers
{
    public class StatsController : Controller
    {
        //SportsEntities db = new SportsEntities();

        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ViewStatsBySportByYear(DashboardVM model)
        {
            if (model != null)
            {
                if (model.SelectedSportsTypeForHistoralYears != null)
                {
                    if (!string.IsNullOrEmpty(model.SelectedSportsTypeForHistoralYears) && model.SelectedHistoricalYear > 0)
                    {
                        using (var service = new GameStatsService())
                        {
                            SeasonVM season = new SeasonVM();

                            season.SportType = model.SelectedSportsTypeForHistoralYears;
                            season.Year = model.SelectedHistoricalYear;

                            season.SeasonId = Utility.SetSeasonID(season.Year, season.SportType);

                            //if (service.CheckForGames(season.SeasonId))
                            //{
                            //    season.MaxGameDate = service.GetMaxGameDate(season.SeasonId).ToString();
                            //}

                            GetDivision(season);

                            return View(season);
                        }                      
                    }
                    else
                    {
                        //ViewBag.Message("Sport Type or Year Not Selected");
                    }
                }
                else
                {
                    //ViewBag.Message("No Games Found"); // ViewBag will be null on redirection.
                }
            }

            return RedirectToAction("Dashboard", "Home");
        }




        
        private SeasonVM GetDivision(SeasonVM model)
        {
            model.Division = new List<TeamRecordVM>();

            int seasonid = Session["SeasonID"] != null ? (int)Session["SeasonID"] : Utility.SetSeasonID(model.Year, model.SportType);

            string confcode = "";
            string divcode = "";

            switch (model.SportType)
            {
                case "NBA": confcode = "ETC"; divcode = "BEA"; break;
                case "NFL": confcode = "NFL_AFC"; divcode = "AFC_EST"; break;
                default: confcode = ""; divcode = ""; break;
            }

            model.Division = SetTeamRec(seasonid, false, confcode, divcode);

            return (model);
        }


        private List<TeamRecordVM> SetTeamRec(int seasonid, bool preseason, string confcode, string divcode)
        {
            using (var service = new GameStatsService())
            {
                List<TeamRecordVM> teamRecList = new List<TeamRecordVM>();

                var division = service.GetDivisionData(seasonid, preseason, confcode, divcode);

                foreach (var item in division)
                {
                    TeamRecordVM teamrec = new TeamRecordVM();

                    teamrec.FranchiseId = item.franchiseid;
                    teamrec.Team = item.team;
                    teamrec.Wins = (int)item.WINS;
                    teamrec.Loses = (int)item.LOSES;
                    teamrec.GB = (double)item.GB;
                    teamrec.percent = SetPercent((double)item.PERCT);
                    teamrec.HomeRec = item.HOMEREC;
                    teamrec.AwayRec = item.AWAYREC;

                    teamRecList.Add(teamrec);
                }

                return teamRecList;
            }
        }



        private string SetPercent(double percent)
        {
            string strPrcnt = percent.ToString();
            if (strPrcnt.Contains("."))
            {
                int i = strPrcnt.IndexOf(".");
                string str = strPrcnt.Substring(i + 1);
                int t = str.Length;
                if (t < 3)
                {
                    if (t == 1)
                    {
                        strPrcnt = strPrcnt + "00";
                    }
                    else if (t == 2)
                    {
                        strPrcnt = strPrcnt + "0";
                    }
                }
            }
            return strPrcnt;
        }




        [WebMethod]
        public JsonResult FetchConfDivision(string sportyear, string sportcode, string confcode, string divcode)
        {
            int year = Int32.Parse(sportyear);

            int seasonid = Session["SeasonID"] != null ? (int)Session["SeasonID"] : Utility.SetSeasonID(year, sportcode);

            List<TeamRecordVM> TeamRecordList = SetTeamRec(seasonid, false, confcode, divcode);

            return Json(new { data = TeamRecordList }, JsonRequestBehavior.AllowGet);



            //    if (string.IsNullOrEmpty(divcode))
            //    {
            //        var conference = db.ViewAllTeamRecordsPerctGamesBehinds.Where(x => x.seasonid == seasonid && x.preseason == false &&
            //                    x.conf_cd == confcode).OrderByDescending(y => y.PERCT).ToList();

            //        foreach (var item in conference)
            //        {
            //            TeamRecord teamrec = new TeamRecord();

            //            teamrec.TeamCd = item.team_cd;
            //            teamrec.Team = item.team;
            //            teamrec.Wins = (int)item.WINS;
            //            teamrec.Loses = (int)item.LOSES;
            //            teamrec.GB = (double)item.GB;
            //            teamrec.percent = SetPercent((double)item.PERCT);
            //            teamrec.HomeRec = item.HOMEREC;
            //            teamrec.AwayRec = item.AWAYREC;

            //            TeamRecordList.Add(teamrec);
            //        }
            //    }
            //    else
            //    {
            //        var division = db.ViewAllTeamRecordsPerctGamesBehinds.Where(x => x.seasonid == seasonid && x.preseason == false &&
            //                    x.conf_cd == confcode && x.div_cd == divcode).OrderByDescending(y => y.PERCT).ToList();

            //        foreach (var item in division)
            //        {
            //            TeamRecord teamrec = new TeamRecord();

            //            teamrec.TeamCd = item.team_cd;
            //            teamrec.Team = item.team;
            //            teamrec.Wins = (int)item.WINS;
            //            teamrec.Loses = (int)item.LOSES;
            //            teamrec.GB = (double)item.GB;
            //            teamrec.percent = SetPercent((double)item.PERCT);
            //            teamrec.HomeRec = item.HOMEREC;
            //            teamrec.AwayRec = item.AWAYREC;

            //            TeamRecordList.Add(teamrec);
            //        }
            //    }

            //return Json(new { data = TeamRecordList }, JsonRequestBehavior.AllowGet);
            //}
        }



        [WebMethod]
        public JsonResult FetchRecentGamesByTeam(string seasonId, string franchiseId)
        {
            int season = int.Parse(seasonId);
            int franchise = int.Parse(franchiseId);

            //bool preseason = 0 == 0 ? false : true;

            using (var service = new GameStatsService())
            {
                string sporttype = service.GetSportTypeBySeason(season);

                var q = service.GetRecentGamesByTeamBySeason(season, sporttype, franchise).ToList();

                List<DisplayGameVM> TeamGameList = new List<DisplayGameVM>();

                foreach (var item in q.Where(x => x.HomeFranchiseId.Equals(franchise) || x.VisitorFranchiseId.Equals(franchise)))
                {
                    string datestr = item.GameDtm.ToString("MM/dd/yyyy");

                    DisplayGameVM game = new DisplayGameVM();
                    game.HomeFranchiseId = item.HomeFranchiseId;
                    game.HomeTeam = item.HomeTeam;
                    game.HomeTeamScore = (int)item.HomeScore;
                    game.VisitorFranchiseId = item.VisitorFranchiseId;
                    game.VisitorTeam = item.VisitorTeam;
                    game.VisitorTeamScore = (int)item.VisitorScore;
                    game.gameday = datestr;
                    TeamGameList.Add(game);
                }

                return Json(new { data = TeamGameList }, JsonRequestBehavior.AllowGet);


                //foreach (var item in q)
                //{
                //    GameVM game = new GameVM();
                //    game.gameId = item.gameid;
                //    game.seasonId = (int)item.seasonid;
                //    game.preSeason = item.preseason;
                //    game.gameDtm = item.gamedtm;
                //    game.visitorTeam = item.VisitorTeam;
                //    game.visitorScore = item.visitorscore;
                //    game.homeTeam = item.HomeTeam;
                //    game.homeScore = item.hometeamscore;
                //}

                //List<AllGamesView> 
                //var games = service.GetAllGames(seasonid, sporttype).Where(x => x.Preseason == preseason && x.SeasonId == seasonid && (x.HomeTeamCode.Equals(TeamCd) || x.VisitorTeamCode.Equals(TeamCd))).OrderByDescending(y => y.GameDtm).ToList();

                //List<GameVM> gameList = new List<GameVM>();


                //var q = service.GetAllGames(seasonid, sporttype).Where(x => x.Preseason == preseason && x.SeasonId == seasonid &&
                //        (x.HomeTeamCode.Equals(TeamCd) || x.VisitorTeamCode.Equals(TeamCd))).OrderByDescending(y => y.GameDtm).ToList();
            }
        }



        [WebMethod]
        public JsonResult FetchRecentTeamVTeamGames(string seasonId, string sportType, string homeTeam, string visitorTeam)
        {
            using (var service = new GameStatsService())
            {
                int searchSeasonId = int.Parse(seasonId);
                int homeFranchiseId = int.Parse(homeTeam);
                int visitorFranchiseId = int.Parse(visitorTeam);

                var teamGameList = service.GetTeamVTeamGames(sportType, homeFranchiseId, visitorFranchiseId);

                List<DisplayGameVM> localList = new List<DisplayGameVM>();

                foreach (var item in teamGameList)
                {
                    string datestr = item.gameday.ToString("MM/dd/yyyy");

                    DisplayGameVM RecentGame = new DisplayGameVM();
                    RecentGame.HomeFranchiseId = item.HomeFranchiseId;
                    RecentGame.HomeTeam = item.HomeTeam;
                    RecentGame.HomeTeamScore = item.HomeScore;
                    RecentGame.VisitorFranchiseId = item.VisitorFranchiseId;
                    RecentGame.VisitorTeam = item.VisitorTeam;
                    RecentGame.VisitorTeamScore = item.VisitorScore;
                    RecentGame.gameday = datestr;
                    localList.Add(RecentGame);
                }

                return Json(new { data = localList }, JsonRequestBehavior.AllowGet);
            }
        }


        [WebMethod]
        public JsonResult FetchRecentTeamVTeamGamesHomeAway(string seasonId, string sportType, string homeTeam, string visitorTeam)
        {
            using (var service = new GameStatsService())
            {
                int searchSeasonId = int.Parse(seasonId);
                int homeFranchiseId = int.Parse(homeTeam);
                int visitorFranchiseId = int.Parse(visitorTeam);

                List<GameList> teamGameList = service.GetTeamVTeamGamesHomeAway(sportType, homeFranchiseId, visitorFranchiseId);

                List<DisplayGameVM> localList = new List<DisplayGameVM>();

                foreach (var item in teamGameList)
                {
                    string datestr = item.gameday.ToString("MM/dd/yyyy");

                    DisplayGameVM RecentGame = new DisplayGameVM();
                    RecentGame.HomeFranchiseId = item.HomeFranchiseId;
                    RecentGame.HomeTeam = item.HomeTeam;
                    RecentGame.HomeTeamScore = item.HomeScore;
                    RecentGame.VisitorFranchiseId = item.VisitorFranchiseId;
                    RecentGame.VisitorTeam = item.VisitorTeam;
                    RecentGame.VisitorTeamScore = item.VisitorScore;
                    RecentGame.gameday = datestr;
                    localList.Add(RecentGame);
                }

                return Json(new { data = localList }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}