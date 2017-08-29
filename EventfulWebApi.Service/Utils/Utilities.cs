using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventfulWebApi.Service.Utils
{
    public static class Utilities
    {
        public static string ToQueryStringAsync(this Dictionary<string, string> parameters)
        {
            return new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result.ToString();
        }


    }
}
