using System;
using System.ComponentModel.DataAnnotations;


namespace Calendar.Models
{
    public class EventMetadata
    {

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}