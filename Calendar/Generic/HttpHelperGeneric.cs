using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Calender.Models;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;

namespace Calender.Generic
{
    public class HttpHelperGeneric<T>
    {

        /// <summary>
        /// Calling API to get data using HttpClient
        /// </summary>
        /// <param name="pTitle"></param>
        /// <returns>Retreiving all Data</returns>
        public static async Task<List<T>> getEntities(string pBaseurl, string pRequestURI, List<KeyValue<string, string>> pHeaderKeyValuePairs)
        {


            using (var client = new HttpClient())
            {
                List<T> events = new List<T>();

                //Passing service base url  
                client.BaseAddress = new Uri(pBaseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Adding the header key values
                foreach (var item in pHeaderKeyValuePairs)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                // check internet connection
                if (testInternet())
                {
                    try
                    {
                        //Sending request to find web api REST service resource GetAllEvents using HttpClient  
                        HttpResponseMessage result = await client.GetAsync(pRequestURI);
                        //Checking the response is successful or not which is sent using HttpClient  
                        if (result.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var eventResponse = result.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api 
                            events = JsonConvert.DeserializeObject<List<T>>(eventResponse);
                        }

                        return events;
                    }
                    catch (Exception)
                    {
                        throw new HttpException(521, "HTTP/1.1 521  Remote server is down");

                    }
                }
                else
                {
                    throw new HttpException(522, "HTTP/1.1 522  No Connection");

                }


            }

        }
        /// <summary>
        /// Calling API to get data using HttpClient using Entity ID
        /// </summary>
        /// <param name="pEventId"></param>
        /// <returns> an Entity that matches the Entity id param</returns>
        public static async Task<T> getEntity_Id(string pBaseurl, string pRequestURI, int pEventId, List<KeyValue<string, string>> pHeaderKeyValuePairs)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(pBaseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Adding the header key values
                foreach (var item in pHeaderKeyValuePairs)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                // Check internet connection
                if (testInternet())
                {
                    try
                    {
                        //Sending request to find web api REST service resource GetAllEvents using HttpClient  
                        HttpResponseMessage result = await client.GetAsync(pRequestURI + pEventId);

                        //Checking the response is successful or not which is sent using HttpClient  
                        if (result.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var eventResponse = result.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api 
                            return JsonConvert.DeserializeObject<T>(eventResponse);

                        }
                    }
                    catch (Exception)
                    {
                        throw new HttpException(521, "HTTP/1.1 521  Remote server is down");

                    }
                }
                else
                {
                    throw new HttpException(522, "HTTP/1.1 522  No Connection");

                }
                return (T)Activator.CreateInstance(typeof(T));
            }
        }
        /// <summary>
        /// This function is diong a HttpClient request for a specified APi and responsible of creating new entity.
        /// </summary>
        /// <param name="pEvent"></param>
        /// <returns>the created entity</returns>
        public static async Task<T> Create(string pBaseurl, string pApiURL, FormUrlEncodedContent pContent, List<KeyValue<string, string>> pHeaderKeyValuePairs)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(pBaseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Adding the api_key to header
                foreach (var item in pHeaderKeyValuePairs)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                // check internet connection
                if (testInternet())
                {
                    try
                    {
                        // Send post request to the API
                        var result = await client.PostAsync(pApiURL, pContent);

                        if (result.IsSuccessStatusCode)
                        {
                            var eventResponse = result.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api 
                            T eventEntity = JsonConvert.DeserializeObject<T>(eventResponse);

                            return eventEntity;

                        }
                    }
                    catch (Exception)
                    {
                        throw new HttpException(521, "HTTP/1.1 521  Remote server is down");
                    }
                }
                else
                {
                    throw new HttpException(522, "HTTP/1.1 522  No Connection");

                }
                return (T)Activator.CreateInstance(typeof(T));
            }
        }
        /// <summary>
        /// pinging goolge to test internet connection
        /// </summary>
        /// <returns>true if there is a connection, false otherwise </returns>
        static bool testInternet()
        {
            Ping ping = new Ping();

            try
            {
                PingReply pingStatus =
               ping.Send(IPAddress.Parse("64.233.185.147"), 1000);

                if (pingStatus.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {

                return false;

            }
            return false;

        }

    }
}