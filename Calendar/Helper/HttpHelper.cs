using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Calendar.Models;
using Calender.Generic;
using Calender.Models;

namespace Calendar.Helper
{
    public static class HttpHelper
    {
        //Hosted web API REST Service base url  
        static string Baseurl = ConfigurationManager.AppSettings["EventsUrl"];
        static string apiKey = ConfigurationManager.AppSettings["EventsApiKey"];
        static List<KeyValue<string, string>> headerKeyValues = new List<KeyValue<string, string>>() { new KeyValue<string, string>() { Key = "api_key", Value = apiKey } };


        /// <summary>
        /// Calling APIHelper to get data using 
        /// </summary>
        /// <param name="pTitle"></param>
        /// <returns>
        /// Retreiving events that has title contains the pTitle param if it is not null, 
        /// excluding 6 events which were created with invalid data
        /// if pTitle is not null, result will be filtered using it.
        /// </returns>
        public static async Task<List<EventModels>> getEvents(string pTitle = null)
        {
            List<EventModels> events = await HttpHelperGeneric<EventModels>.getEntities(Baseurl, "api/Event", headerKeyValues);

            //returning the Event list   
            if (String.IsNullOrEmpty(pTitle))
            {
                events = (from eventEnity in events where (eventEnity.StartDate.Year != 1991) select eventEnity).OrderBy(x => x.StartDate).ToList();
                return events;
            }
            else
            {
                // filtering by pTitle
                events = (from eventEnity in events where (eventEnity.StartDate.Year != 1991 && (eventEnity.Title.Contains(pTitle))) select eventEnity).OrderBy(x => x.StartDate).ToList();
                return events;

            }
        }
        /// <summary>
        /// Calling APIHelper to get data using Event ID
        /// </summary>
        /// <param name="pEventId"></param>
        /// <returns> 
        /// an event that matches the pEventId param, 
        /// or new entity in case of failure
        /// </returns>
        public static async Task<EventModels> getEvent_Id(int pEventId)
        {

            EventModels eventEntity = await HttpHelperGeneric<EventModels>.getEntity_Id(Baseurl, "api/Event/", pEventId, headerKeyValues);

            return eventEntity;

        }
        /// <summary>
        /// This function is calling GenericHttpHelper for  creating new event.
        /// </summary>
        /// <param name="pEvent"></param>
        /// <returns>the created event or new enity in case of failure</returns>
        public static async Task<EventModels> Create(EventModels pEvent)
        {
            var content = new FormUrlEncodedContent(new[]
                  {

                        new KeyValuePair<string, string>("Title", pEvent.Title),
                        new KeyValuePair<string, string>("Description", pEvent.Description),
                        new KeyValuePair<string, string>("StartDate", pEvent.StartDate.ToString()),
                        new KeyValuePair<string, string>("EndDate", pEvent.EndDate.ToString())
                      });

            EventModels eventEntity = await HttpHelperGeneric<EventModels>.Create(Baseurl, "api/Event", content, headerKeyValues);

            return eventEntity;
        }
    }
}