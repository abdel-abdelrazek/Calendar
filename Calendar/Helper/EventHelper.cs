using Calendar.Models;
using Calendar.ViewModel;
using System;
using System.Collections.Generic;


namespace Calendar.Helper
{
    /// <summary>
    /// This class is responsible of convertion between model and modelview entities and vice versa.
    /// </summary>
    public static class EventHelper
    {
        public static IList<EventViewModel> modelToViewModel(IList<EventModels> pEvents)
        {

            IList<EventViewModel> lstEventsViewModel = new List<EventViewModel>();
            foreach (var item in pEvents)
            {
                lstEventsViewModel.Add(new EventViewModel() { Id = item.Id, Description = item.Description, StartDate = item.StartDate, EndDate = item.EndDate, Title = item.Title });
            }

            return lstEventsViewModel;
        }

        public static EventViewModel modelToViewModel(EventModels pEvent)
        {

            EventViewModel eventsViewModel = new EventViewModel() { Id = pEvent.Id, Description = pEvent.Description, StartDate = pEvent.StartDate, EndDate = pEvent.EndDate, Title = pEvent.Title };

            return eventsViewModel;
        }

        internal static EventModels viewmodelToModel(EventViewModel pEvent)
        {
            return new EventModels() { Id = pEvent.Id, Description = pEvent.Description, StartDate = pEvent.StartDate, EndDate = pEvent.EndDate, Title = pEvent.Title }; ;
        }

        internal static CustomEventViewModel composeCustomEvent(IList<EventModels> events)
        {
            CustomEventViewModel customEventViewModel = new CustomEventViewModel();

            IList<EventViewModel> lstEventsViewModel = modelToViewModel(events);

            customEventViewModel.Events = lstEventsViewModel;
            customEventViewModel.EventEntity = new EventViewModel() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7) };

            return customEventViewModel;
        }
    }
}