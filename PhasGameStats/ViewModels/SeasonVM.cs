using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhasGameStats.ViewModels
{
    public class SeasonVM
    {
        public int Year { get; set; }

        public int SeasonId { get; set; }

        public string SportType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string MaxGameDate { get; set; }

        public List<TeamRecordVM> Division { get; set; }

        public List<DisplayGameVM> TeamGames { get; set; }
    }
}