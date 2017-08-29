using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EventfulWebApi.Service.Httpclients
{

    public partial class ApiHttpClient : IDisposable
    {
        private readonly HttpClient m_client;
        private const long httpClientTimeout = 60000;

        public ApiHttpClient(Uri baseUrl)
        {
            m_client = new HttpClient();
            m_client.BaseAddress = baseUrl;
            m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            m_client.Timeout = new TimeSpan(httpClientTimeout);
            
        }

        /// <summary>
        /// The base address.
        /// </summary>
        public Uri BaseAddress
        {
            get
            {
                return m_client.BaseAddress;
            }
        }

        /// <summary>
        /// The inner client.
        /// </summary>
        protected HttpClient Client
        {
            get
            {
                return m_client;
            }
        }

        /// <summary>
        /// Sends a GET request to the specified Uri and 
        /// returns the array under the root json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
 
        protected IReadOnlyCollection<T> GetCollectionAsync<T>(string resourcePath)
        {
            //string jsonString = await SendAsync(HttpMethod.Get, resourcePath);
            string jsonString = SendRequest(resourcePath);
            // Unpack the root object and dig out the json array
            JToken root = JObject.Parse(jsonString);
            string objectString = root.First.First.ToString();
            return   JsonConvert.DeserializeObject<IReadOnlyCollection<T>>(objectString) ;
        }

        /// <summary>
        /// Sends a GET request to the specified Uri and returns the array under the root json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        
        protected async Task<IReadOnlyCollection<T>> GetArrayCollectionAsync<T>(string resourcePath)
        {
            string jsonString = await SendAsync(HttpMethod.Get, resourcePath);

            JArray jsonArray = JArray.Parse(jsonString);
            string objectString = jsonArray.Root.ToString();
            return await Task.Run(() => JsonConvert.DeserializeObject<IReadOnlyCollection<T>>(objectString));
        }

        /// <summary>
        /// Sends a GET request to the specified Uri and returns json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourcePath"></param>
        /// <returns></returns>       
       
        protected T GetObjectAsync<T>(string resourcePath)
        {
            
            string jsonString = SendRequest(resourcePath);
            //string jsonString = await SendAsync1(HttpMethod.Get, resourcePath);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore  
            }; 
            return JsonConvert.DeserializeObject<T>(jsonString, settings);

        }
         
        /// <summary>
        /// Sends a GET request to the specified Uri and returns a single object under the root json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        protected async Task<T> GetOneAsync<T>(string resourcePath)
        {
            string jsonString = await SendAsync(HttpMethod.Get, resourcePath);           

            JToken root = JObject.Parse(jsonString);
            string objectString = root.First.First.First.ToString();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(objectString));
        }

        /// <summary>
        /// Sends a HTTP request using the current base address
        /// </summary>
        /// <param name="method"></param>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        protected async Task<string> SendAsync(HttpMethod method, string resourcePath)
        {
            HttpResponseMessage response = null;
            HttpClient client = new HttpClient();
            try
            {
                Uri baseUri = new Uri(BaseAddress.AbsoluteUri);
                Uri absoluteUri = new Uri(baseUri, resourcePath);

                HttpRequestMessage request = new HttpRequestMessage(method, absoluteUri.AbsoluteUri);

                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                return null;
            }
            finally {
                
            }

        }
        protected string SendRequest(string resourcePath)
        {
            HttpClient client = new HttpClient();
            string res = "";
            try
            {
                Uri baseUri = new Uri(BaseAddress.AbsoluteUri);
                Uri absoluteUri = new Uri(baseUri, resourcePath);

                var response = client.GetAsync(absoluteUri).Result;
                 
                using (HttpContent content = response.Content)
                {
                    // Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = result.Result;
                }
            }
            catch(Exception ex)
            {
                return null;
            }

            return res;
        }


        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        ////This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.m_client.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}

