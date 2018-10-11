using System;
using System.ComponentModel.DataAnnotations;


namespace Calendar.Models
{
    public class EventModels
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

    }
}