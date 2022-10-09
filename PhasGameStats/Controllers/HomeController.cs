using PhasGameStats.Utilities;
using PhasGameStats.ViewModels;
using Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;

namespace PhasGameStats.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }



        public ActionResult Dashboard()
        {
            Utility.ClearSession();

            DashboardVM model = new DashboardVM();

            model.Message = null;

            ConfigureModel(model);

            return View(model);
        }


        private void ConfigureModel(DashboardVM model)
        {
            using (var service = new GameStatsService())
            {
                //model.AllSports = service.GetAllSports(model.SelectedRecentYear);
                model.AllSports = service.GetAllSportTypes();
            }

            model.RecentYears = Enumerable.Empty<SelectListItem>();
            model.SeasonOrGame = null;
            //model.Message = (int)(Session["SelectedYear"] != null ? Session["SelectedYear"] : 0);
            model.HistoricalYears = Enumerable.Empty<SelectListItem>();


        }


        public JsonResult FetchHistoricalYearsList(string sporttype)
        {
            using (var service = new GameStatsService())
            {
                List<int> historicalseasons = new List<int>();

                historicalseasons = service.GetSeasonsForSportType(sporttype);

                return Json(historicalseasons, JsonRequestBehavior.AllowGet);
            }
        }



        [WebMethod]
        public JsonResult FetchYearsList(string sporttype)
        {
            using (var service = new GameStatsService())
            {
                List<int> yearList = new List<int>();

                yearList = service.GetSeasonsForSportType(sporttype);

                return Json(yearList, JsonRequestBehavior.AllowGet);

            }
        }


        [WebMethod]
        public JsonResult FetchNewSeasonBySportType(string sportType)
        {
            int year = 0;

            if (sportType != null)
            {
                using (var service = new GameStatsService())
                {
                    year = service.NewSeasonBySportType(sportType);
                }
            }

            return Json(new { data = year }, JsonRequestBehavior.AllowGet);
        }



        private int SaveSeason(DashboardVM model)
        {
            //return 0;

            using (var service = new GameStatsService())
            {
                //int sportTypeId = service.GetSportTypeId(model.SelectedSportsTypeForRecentYears.sportstypecd);

                //string seasonstartdt = model.SelectedRecentYear + "/" + service.GetSportTypeStartDate(sportTypeId).ToString() + "/01";
                //DateTime startdt = Convert.ToDateTime(seasonstartdt);

                //int seasonId = service.SaveSeason(model.SelectedSportsTypeForRecentYears.sportstypecd, startdt);
            }

            return 0;
        }

        public JsonResult SaveSeason(string sportsTypeCd, string selectedYear)
        {
            DashboardVM st = new DashboardVM();
            //st.SelectedSportsTypeForRecentYears = 
            //st.SelectedSportsTypeForRecentYears.sportstypecd = sportsTypeCd;
            //st.SelectedRecentYear = int.Parse(selectedYear);

            int seasonId = 0; // SaveSeason(st);

            return Json(new { data = seasonId }, JsonRequestBehavior.AllowGet);



            //using (var service = new GameStatsService())
            //{

            //}


            //int sportsid = db.SportsTypes.Where(x => x.sportstypecd == sportsTypeCd).FirstOrDefault().id;
            //string startmonth = db.SportsTypes.Where(x => x.id == sportsid).FirstOrDefault().seasonstartmonth.ToString();
            //string seasonstartdt = selectedYear + "/" + startmonth.ToString() + "/01";
            //DateTime startdt = Convert.ToDateTime(seasonstartdt);

            //Season SeasonModel = new Season();

            //SeasonModel.sportstypecd = sportsTypeCd;
            //SeasonModel.startdate = startdt;
            //SeasonModel.mod_user = "admin";
            //SeasonModel.mod_date = System.DateTime.Now;

            //db.Seasons.Add(SeasonModel);

            //try
            //{
            //    db.SaveChanges();

            //    int newSeasonId = db.Seasons.Where(x => x.sportstypecd == SeasonModel.sportstypecd
            //        && SeasonModel.startdate == startdt).OrderByDescending(y => y.startdate).FirstOrDefault().seasonid;

            //    //Session.Add("SavedMsg", "Successfully Created The " + SeasonModel.startdate.Year + " " + SeasonModel.sportstypecd + " Season");

            //    int seasonid = db.Seasons.Where(x => x.sportstypecd == SeasonModel.sportstypecd
            //        && SeasonModel.startdate == startdt).Any() ? db.Seasons.Where(x => x.sportstypecd == SeasonModel.sportstypecd
            //        && SeasonModel.startdate == startdt).OrderByDescending(y => y.startdate).FirstOrDefault().seasonid : 0;

            //    return Json(new { data = seasonid }, JsonRequestBehavior.AllowGet);
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{
            //    Exception raise = dbEx;
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);

            //            // raise a new exception nesting the current instance as InnerException
            //            raise = new InvalidOperationException(message, raise);
            //        }
            //    }
            //    throw raise;
            //}

        }



        public ActionResult NewSeasonGame(DashboardVM viewmodel)
        {
            if (viewmodel.SelectedSportsTypeForRecentYears != null)
            {
                if (viewmodel.SeasonOrGame.Equals("Season"))
                {
                    int seasonid = SaveSeason(viewmodel);
                    Utility.SetSeasonID(seasonid);
                }
                else
                {
                    //SeasonGameVM newgame = new SeasonGameVM();
                    //newgame = ConfigGame(viewmodel);
                    //return View(newgame);
                }
            }

            return RedirectToAction("Dashboard");
        }



        //public ActionResult NewGame()
        //{
        //    SportsTypeVM viewmodel = new SportsTypeVM();

        //    string sporttype = (string)Session["SelectedSportsType"];
        //    int sportyear = (int)Session["SelectedYear"];

        //    viewmodel.SelectedSportsTypeForGame.sportstypecd = sporttype;
        //    viewmodel.SelectedRecentYear = sportyear;

        //    SeasonGameVM newgame = ConfigGame(viewmodel);

        //    return View(newgame);
        //}






        //private SeasonGameVM ConfigGame(SportsTypeVM viewmodel)
        //{
        //    if (viewmodel != null)
        //    {
        //        string sporttype = viewmodel.SelectedSportsTypeForRecentYears.sportstypecd;
        //        int selectedyear = viewmodel.SelectedRecentYear;

        //        using (var service = new GameStatsService())
        //        {
        //            SeasonGameVM newgame = new SeasonGameVM();
        //            newgame.HomeTeams = service.GetAllTeams(selectedyear, sporttype);
        //            newgame.VisitorTeams = service.GetAllTeams(selectedyear, sporttype);
        //            newgame.statuses = service.GetGameStatuses();
        //            newgame.AMPM = Utility.GetAMPM();
        //            newgame.minutes = Utility.GetMinuets();
        //            newgame.hours = Utility.GetHours();
        //            newgame.scores = Utility.GetAllScores();
        //            newgame.OTList = Utility.OvertimeList();
        //            newgame.SelectedStatus = "NS";
        //            newgame.SelectedHomeScore = "0";
        //            newgame.SelectedVisitorScore = "0";

        //            newgame.Game = new GameVM();
        //            newgame.Game.gameDtm = DateTime.Now;

        //            return newgame;
        //        } 
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}






    }

}



//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult SaveGame(SeasonGameVM viewmodel)
//{
//    return null;

//Game newgame = new Game();

//newgame.seasonid = (int)Session["SeasonID"];
//newgame.preseason = viewmodel.Game.preseason;

//if (viewmodel.timeunknown == true)
//{
//    newgame.gamedtm = Convert.ToDateTime(viewmodel.Game.gamedtm);
//}
//else
//{
//    int hour = Int32.Parse(viewmodel.hour);
//    int minute = Int32.Parse(viewmodel.minute);
//    double time = 60 * hour + minute;
//    newgame.gamedtm = Convert.ToDateTime(viewmodel.Game.gamedtm).AddMinutes(time);
//}

//newgame.gamestatus = viewmodel.SelectedStatus;

//newgame.hometeam = viewmodel.Game.hometeam;
//newgame.visitor = viewmodel.Game.visitor;

//newgame.hometeamscore = Int32.Parse(viewmodel.SelectedHomeScore);
//newgame.visitorscore = Int32.Parse(viewmodel.SelectedVisitorScore);
//newgame.overtime = Int32.Parse(viewmodel.SelectedOT);
//newgame.mod_user = "admin";
//newgame.mod_date = System.DateTime.Now;

//db.Games.Add(newgame);

//string Msg = null;

//try
//{
//    db.SaveChanges();

//    string hometeam = db.Teams.Where(x => x.team_cd == newgame.hometeam).FirstOrDefault().teamname;
//    string visitorteam = db.Teams.Where(x => x.team_cd == newgame.visitor).FirstOrDefault().teamname;

//    Msg = "Game between " + hometeam + " and " + visitorteam + " has been successfully saved.";
//    Session.Add("Msg", Msg);
//}
//catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
//{
//    Msg = "Game between " + newgame.hometeam + " and " + newgame.visitor + " has not been saved.";
//    Session.Add("Msg", Msg);

//    Exception raise = dbEx;
//    foreach (var validationErrors in dbEx.EntityValidationErrors)
//    {
//        foreach (var validationError in validationErrors.ValidationErrors)
//        {
//            string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);

//            // raise a new exception nesting current instance as InnerException
//            raise = new InvalidOperationException(message, raise);
//        }
//    }
//    throw raise;
//}

//return RedirectToAction("NewGame");
//}




//[WebMethod]
//public JsonResult FetchHistoricalYearsList(string sporttype)
//{
//    using (db)
//    {
//        List<int> historicalseasons = new List<int>();

//        var q = db.Seasons.Where(x => x.sportstypecd == sporttype).OrderByDescending(y => y.startdate).ToList();

//        foreach (var item in q)
//        {
//            int season = 0;
//            season = item.startdate.Year;
//            historicalseasons.Add(season);
//        }

//        return Json(historicalseasons, JsonRequestBehavior.AllowGet);
//    }
//}






//[WebMethod]
//public JsonResult FetchRecentTeamOnTeamGames(string TeamCd1, string TeamCd2)
//{
//    using (SportsEntities db = new SportsEntities())
//    {
//        List<TeamGame> TeamGameList = new List<TeamGame>();

//        var q = (from gm in db.Games
//                 join t1 in db.Teams on gm.hometeam equals t1.team_cd
//                 join t2 in db.Teams on gm.visitor equals t2.team_cd
//                 where ((gm.hometeam == TeamCd1 || gm.visitor == TeamCd1) && (gm.hometeam == TeamCd2 || gm.visitor == TeamCd2))
//                 select new
//                 {
//                     homecity = t1.city,
//                     homename = t1.teamname,
//                     visitorcity = t2.city,
//                     visitorname = t2.teamname,
//                     hscore = gm.hometeamscore,
//                     vscore = gm.visitorscore,
//                     gameday = gm.gamedtm
//                 }).ToList();

//        foreach (var item in q)
//        {
//            TeamGame RecentGame = new TeamGame();
//            RecentGame.HomeTeam = item.homecity + ' ' + item.homename;
//            RecentGame.HomeTeamScore = item.hscore;
//            RecentGame.VisitorTeam = item.visitorcity + ' ' + item.visitorname;
//            RecentGame.VisitorTeamScore = item.vscore;
//            RecentGame.gamedate = item.gameday;
//            TeamGameList.Add(RecentGame);
//        }

//        return Json(new { data = TeamGameList }, JsonRequestBehavior.AllowGet);
//    }
//}


//[WebMethod]
//public JsonResult FetchRecentGamesByTeam(string SeasonId, string TeamCd)
//{
//    int seasonid = Int32.Parse(SeasonId);

//    bool preseason = 0 == 0 ? false : true;

//    using (SportsEntities db = new SportsEntities())
//    {
//        List<TeamGame> TeamGameList = new List<TeamGame>();

//        var q = db.ViewAllGames.Where(x => x.preseason == preseason && x.seasonid == seasonid
//                    && (x.HomeTeamCode.Equals(TeamCd) || x.VisitorTeamCode.Equals(TeamCd))).ToList();

//        foreach (var item in q)
//        {
//            TeamGame RecentGame = new TeamGame();
//            RecentGame.HomeTeamCd = item.HomeTeamCode;
//            RecentGame.HomeTeam = item.HomeTeam;
//            RecentGame.HomeTeamScore = item.hometeamscore;
//            RecentGame.VisitorTeamCd = item.VisitorTeamCode;
//            RecentGame.VisitorTeam = item.VisitorTeam;
//            RecentGame.VisitorTeamScore = item.visitorscore;
//            TeamGameList.Add(RecentGame);
//        }

//        return Json(new { data = TeamGameList }, JsonRequestBehavior.AllowGet);
//    }
//}


//public JsonResult FetchNewYearList(string sportcode)
//{
//    IEnumerable<SelectListItem> data = Repository.GetNewYearsBySport(sportcode).OrderBy(x => x.Text).ToList();

//    return Json(data, JsonRequestBehavior.AllowGet);
//}





//public JsonResult FetchAvailableTeamList(string sportyear, string sportcode, string team)
//{
//    var data = Repository.GetAllTeams(2019, sportcode).Where(x => x.Value != team).OrderBy(x => x.Text);

//    return Json(data, JsonRequestBehavior.AllowGet);
//}


//}
//}





//private DashboardVM GetAllGamesByTeam(DashboardVM model)
//{
//    SportsEntities db = new SportsEntities();

//    model.TeamGames = new List<TeamGame>();

//    string SelectedTeam = "";

//    if (model.SportType == "NBA")
//    {
//        SelectedTeam = "BCT";
//    }
//    else if (model.SportType == "NFL")
//    {
//        SelectedTeam = "AFC";
//    }
//    else
//    {
//        SelectedTeam = "ETC";
//    }

//    var gamelist = db.ViewAllGames.Where(x => x.preseason == false && x.HomeTeamCode == SelectedTeam || 
//                        x.VisitorTeamCode == SelectedTeam).OrderByDescending(z => z.gameid).ToList();

//    foreach (var item in gamelist)
//    {
//        TeamGame game = new TeamGame();
//        game.HomeTeam = item.HomeTeam;
//        game.VisitorTeam = item.VisitorTeam;
//        game.HomeTeamScore = item.hometeamscore;
//        game.VisitorTeamScore = item.visitorscore;
//        model.TeamGames.Add(game);
//    }

//    return (model);
//}
