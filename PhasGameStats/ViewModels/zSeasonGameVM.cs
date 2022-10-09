//using PhasGameStats.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace PhasGameStats.ViewModels
{
    public class zSeasonGameVM
    {
        public GameVM Game { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Enter the Game Status")]
        public string SelectedStatus { get; set; }

        [Display(Name = "Home")]
        public string SelectedHomeScore { get; set; }

        [Display(Name = "Visitor")]
        public string SelectedVisitorScore { get; set; }

        public string SelectedOT { get; set; }

        public bool timeunknown { get; set; }

        [Display(Name = "Hr.")]
        public string hour { get; set; }

        [Display(Name = "Min.")]
        public string minute { get; set; }

        [Display(Name = "AM / PM")]
        public string amORpm { get; set; }

        public IEnumerable<SelectListItem> statuses { get; set; }
        public IEnumerable<SelectListItem> hours { get; set; }
        public IEnumerable<SelectListItem> minutes { get; set; }
        public IEnumerable<SelectListItem> AMPM { get; set; }

        public IEnumerable<SelectListItem> scores { get; set; }
        public IEnumerable<SelectListItem> OTList { get; set; }

        public IEnumerable<SelectListItem> HomeTeams { get; set; }
        public IEnumerable<SelectListItem> VisitorTeams { get; set; }

    }
}