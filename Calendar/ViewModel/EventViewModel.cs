using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Calendar.ViewModel
{
    /// <summary>
    /// Event viewmodel 
    /// </summary>
    public class EventViewModel
    {

        public int Id { get; set; }
        [Required, StringLength(25), Display(Name = "Title")]
        public string Title { get; set; }
        [StringLength(150), Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public string SubDescription { get { return (Description.Length > 15) ? Description.Substring(0, 15) + "....." : Description; } }
        public string StartDateMonth { get { return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(StartDate.Month) + " " + StartDate.Day; } }
        public string StartDateFormated { get { return StartDate.ToString("MM/dd/yyyy h:mm tt"); } }
        public string EndDateStringFormated { get { return EndDate.ToString("MM/dd/yyyy h:mm tt"); } }

    }
}