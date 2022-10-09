using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PhasGameStats.ViewModels
{
    public class DashboardVM
    {
        //[Display(Name = "Sport Type")] this does nothing to the page
        public string SelectedSportsTypeForHistoralYears { get; set; }

        [Required(ErrorMessage = "A Selected Sport Type Is Required!")]
        public string SelectedSportsTypeForRecentYears { get; set; }

        [Required(ErrorMessage = "Selecting A Historical Year Is Required!")]
        public int SelectedHistoricalYear { get; set; }

        [Display(Name = "Recent Years")]
        [Required(ErrorMessage = "Selecting A Recent Year Is Required!")]
        public int SelectedRecentYear { get; set; }

        [Display(Name = "Season Or Game")]
        public string SeasonOrGame { get; set; }

        public string Message { get; set; }

        public IEnumerable<SelectListItem> AllSports { get; set; }

        public IEnumerable<SelectListItem> RecentYears { get; set; }

        public IEnumerable<SelectListItem> HistoricalYears { get; set; }

    }
}