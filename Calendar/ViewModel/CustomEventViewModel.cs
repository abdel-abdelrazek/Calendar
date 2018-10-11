using Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calendar.ViewModel
{
    /// <summary>
    /// containg list of events and a single event entity
    /// </summary>
    public class CustomEventViewModel
    {
        public IList<EventViewModel> Events { get; set; }
        public EventViewModel EventEntity { get; set; }

    }
}