using EventfulWebApi.Models;
using EventfulWebApi.Models.Models;
using EventfulWebApi.Service.Utils;
using System;
using System.Collections.Generic;


namespace EventfulWebApi.Service.Httpclients
{
    public partial class ApiHttpClient
    {
        public EventSearchResults GetEventsAsync(String geocd, int radius, DateTime startDate, DateTime endDate, string eventcategory, string clientkey)
        {
            // These parameters must be in this order, or else the API will return nothing
            var queryParams = new Dictionary<string, string>(){
                {"where", geocd },
                {"within", radius.ToString()},
                {"category", eventcategory },
                {"units" ,"km"},                
                {"page_size" ,"20" },
                {"page_number" ,"10" },
                {"date", startDate.ToString("yyyyMMdd00" , System.Globalization.CultureInfo.InvariantCulture)
                + "-" + endDate.ToString("yyyyMMdd00", System.Globalization.CultureInfo.InvariantCulture)},
                {"app_key", clientkey }
            };

            // If either service or component is invalid, then don't send either parameter
            if (string.IsNullOrWhiteSpace(radius.ToString()) || string.IsNullOrWhiteSpace(eventcategory))
            {
                // If they are both invalid, then remove them both
                if (string.IsNullOrWhiteSpace(radius.ToString()) && string.IsNullOrWhiteSpace(eventcategory))
                {
                    queryParams.Remove("radius");
                    queryParams.Remove("units");
                    queryParams.Remove("eventcategory");
                }
                else
                {
                    throw new ArgumentException("radius and eventcategory must both be specified, or both omitted.");
                }
            }

            string queryString = queryParams.ToQueryStringAsync();
            //Task<int> task = GetObjectAsync();
            //task.Wait();
            //var x = task.Result;
            //try
            //{
            //    var t = GetObjectAsync<EventSearchResults>($"json/events/search?{queryString}");
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}           

            return GetObjectAsync<EventSearchResults>($"json/events/search?{queryString}");
        }

        public EventCategories GetEventCategoryAsync(string clientkey)
        {
            // These parameters must be in this order, or else the API will return nothing
            var queryParams = new Dictionary<string, string>(){               
                {"app_key", clientkey }
            };

            // If they are both invalid, then remove them both
            if (string.IsNullOrWhiteSpace(clientkey))
            {
                throw new ArgumentException("radius and eventcategory must both be specified, or both omitted.");
            }               
 
            string queryString = queryParams.ToQueryStringAsync();
            return GetObjectAsync<EventCategories>($"json/categories/list?{queryString}");
        }

        public GoogleGeocd GetGooglecdresultAsync(string address, string clientkey)
        {
            // These parameters must be in this order, or else the API will return nothing
            var queryParams = new Dictionary<string, string>(){
                {"address", address },
                {"key", clientkey }
            };
        
            // If they are both invalid, then remove them both
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Address must both be specified, or both omitted.");
            }

            string queryString = queryParams.ToQueryStringAsync();
            return GetObjectAsync<GoogleGeocd>($"geocode/json?{queryString}");
        }
    }
}
