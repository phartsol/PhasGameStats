//using PhasGameStats.Models;
using Service.Services;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PhasGameStats.Utilities
{
    public class Utility
    {

        //static SportsEntities db = new SportsEntities();

        public static int SetSeasonID(int year, string sporttype)
        {
            using (var service = new GameStatsService())
            {
                int seasonid = service.GetSeasonID(year, sporttype);
                SetSessionValues(seasonid, year, sporttype);

                HttpContext.Current.Session.Add("SelectedYear", year);
                HttpContext.Current.Session.Add("SelectedSportsType", sporttype);

                return (seasonid);
            }
        }

        public static int SetSeasonID(int seasonid)
        {
            using (var service = new GameStatsService())
            {
                SetSessionValues(seasonid);

                return (seasonid);
            }
        }

        public static int SetSessionValues(int seasonid, int year, string sporttype)
        {
            HttpContext.Current.Session.Remove("SeasonID");
            HttpContext.Current.Session.Add("SeasonID", seasonid);

            HttpContext.Current.Session.Add("SelectedYear", year);
            HttpContext.Current.Session.Add("SelectedSportsType", sporttype);

            return (seasonid);
        }

        public static int SetSessionValues(int seasonid)
        {
            HttpContext.Current.Session.Remove("SeasonID");
            HttpContext.Current.Session.Add("SeasonID", seasonid);

            return (seasonid);
        }


        public static void ClearSession()
        {
            HttpContext.Current.Session.Remove("SelectedYear");
            HttpContext.Current.Session.Remove("SelectedSportsType");
            HttpContext.Current.Session.Remove("SeasonID");
            HttpContext.Current.Session.Remove("Msg");
            HttpContext.Current.Session.Remove("SavedSeason");
        }



        public static IEnumerable<SelectListItem> GetAMPM()
        {
            List<SelectListItem> ampm = new List<SelectListItem>();

            ampm.Add(new SelectListItem() { Text = "AM", Value = "1" });
            ampm.Add(new SelectListItem() { Text = "PM", Value = "2" });

            return ampm;
        }


        public static IEnumerable<SelectListItem> GetMinuets()
        {
            List<SelectListItem> minutes = new List<SelectListItem>();

            minutes.Add(new SelectListItem() { Text = "00", Value = "1" });
            minutes.Add(new SelectListItem() { Text = "05", Value = "2" });
            minutes.Add(new SelectListItem() { Text = "10", Value = "3" });
            minutes.Add(new SelectListItem() { Text = "15", Value = "4" });
            minutes.Add(new SelectListItem() { Text = "20", Value = "5" });
            minutes.Add(new SelectListItem() { Text = "25", Value = "6" });
            minutes.Add(new SelectListItem() { Text = "30", Value = "7" });
            minutes.Add(new SelectListItem() { Text = "35", Value = "8" });
            minutes.Add(new SelectListItem() { Text = "40", Value = "9" });
            minutes.Add(new SelectListItem() { Text = "45", Value = "10" });
            minutes.Add(new SelectListItem() { Text = "50", Value = "11" });
            minutes.Add(new SelectListItem() { Text = "55", Value = "12" });

            return minutes;
        }


        public static IEnumerable<SelectListItem> GetHours()
        {
            List<SelectListItem> hours = new List<SelectListItem>();

            hours.Add(new SelectListItem() { Text = "1", Value = "1" });
            hours.Add(new SelectListItem() { Text = "2", Value = "2" });
            hours.Add(new SelectListItem() { Text = "3", Value = "3" });
            hours.Add(new SelectListItem() { Text = "4", Value = "4" });
            hours.Add(new SelectListItem() { Text = "5", Value = "5" });
            hours.Add(new SelectListItem() { Text = "6", Value = "6" });
            hours.Add(new SelectListItem() { Text = "7", Value = "7" });
            hours.Add(new SelectListItem() { Text = "8", Value = "8" });
            hours.Add(new SelectListItem() { Text = "9", Value = "9" });
            hours.Add(new SelectListItem() { Text = "10", Value = "10" });
            hours.Add(new SelectListItem() { Text = "11", Value = "11" });
            hours.Add(new SelectListItem() { Text = "12", Value = "12" });

            return hours;
        }


        public static IEnumerable<SelectListItem> GetAllScores()
        {
            int MaxScore = 250;

            List<SelectListItem> scores = new List<SelectListItem>();

            for (int i = 0; i < MaxScore; i++)
            {
                scores.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

            return scores;
        }


        public static IEnumerable<SelectListItem> OvertimeList()
        {
            List<SelectListItem> OTList = new List<SelectListItem>();

            for (int i = 0; i < 5; i++)
            {
                OTList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

            return OTList;
        }









    }
}