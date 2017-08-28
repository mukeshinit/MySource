using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventfulWebApi.Models;
using EventfulWebApi.Service.Utils;
using Newtonsoft.Json;


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
                    queryParams.Remove("eventcategory");
                }
                else
                {
                    throw new ArgumentException("radius and eventcategory must both be specified, or both omitted.");
                }
            }

            string queryString = queryParams.ToQueryStringAsync();
            return GetObjectAsync<EventSearchResults>($"json/events/search?{queryString}");
        }
    }
}
