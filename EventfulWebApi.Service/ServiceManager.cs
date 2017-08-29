using EventfulWebApi.Config;
using EventfulWebApi.Logging;
using EventfulWebApi.Models;
using EventfulWebApi.Service.Httpclients;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace EventfulWebApi.Service
{
    public class ServiceManager
    {
        private string EventfulApiUrl { get; set; }
        private Stopwatch RequestTime { get; set; }

        public ServiceManager()
        {
            EventfulApiUrl = !string.IsNullOrWhiteSpace(ConfigProvider.Instance.EventfulApiUrl) ?
                ConfigProvider.Instance.EventfulApiUrl : @"http://api.eventful.com/";
        }

        /// <summary>
        /// Function call that gets Events from Event API
        /// </summary>
        /// <param name="address">subscription id for usage that is being called for</param>
        /// <param name="radius">Start Date of when to grab the usage data for</param>
        /// <param name="date">End Date of when to grabe the usage for</param>
        /// <returns></returns>
        public EventSearchResults GetEvents(string address, int radius, DateTime startdate, DateTime enddate,
            string eventcategory)
        {
            var retry = true;
            var retryCount = 0;

            while (retry == true)
            {
                try
                {
                    using (var client = new ApiHttpClient(new Uri(EventfulApiUrl)))
                    {
                        EventSearchResults eventSearchResults = client.GetEventsAsync
                            (address, radius, startdate, enddate, eventcategory,
                            ConfigProvider.Instance.EventFulClientKey);

                        //Verifies that the the server
                        if (eventSearchResults == null)
                        {
                            if (retryCount < ConfigProvider.Instance.NumberofRetriesForFailures)
                            {
                                Logger.Log(Level.Error,
                                     "Error getting usage data from Eventful Rest API with unsuccessful status code. Attempt "
                                     + retryCount + " out of " + ConfigProvider.Instance.NumberofRetriesForFailures
                                     + ". Sleeping for " + ConfigProvider.Instance.MillisecondsToWaitBewteenRetries
                                     + " ms and then will try agian. ", null);

                                Thread.Sleep(ConfigProvider.Instance.MillisecondsToWaitBewteenRetries);
                                retryCount++;
                                retry = false;
                            }
                            else
                            {
                                Logger.Log(Level.Error,
                                     "Error getting usage data from Eventful Rest API with unsuccessful status code." +
                                     " This was the last attempt and will not retry.", null);

                                //Setting retry to false as this is the last attempt
                                retry = false;
                            }
                        }

                        retry = false;
                        return eventSearchResults;
                    }
                }
                catch (Exception e)
                {
                    if (retryCount <= ConfigProvider.Instance.NumberofRetriesForFailures)
                    {
                        Logger.Log(Level.Warn,
                             "Error getting event search data. Attempt " + retryCount + " out of "
                             + ConfigProvider.Instance.NumberofRetriesForFailures
                             + ". Sleeping for " + ConfigProvider.Instance.NumberofRetriesForFailures
                             + "ms and then will try agian." + Environment.NewLine +
                            "Error message: " + e.Message + Environment.NewLine +
                            "StackTrace: " + e.StackTrace, null, e);

                        Thread.Sleep(ConfigProvider.Instance.MillisecondsToWaitBewteenRetries);
                        retryCount++;
                        retry = true;
                    }
                    else
                    {
                        Logger.Log(Level.Error,
                            "Error getting event search data. This was the last attempt and will not try agian." + Environment.NewLine +
                            "Error message: " + e.Message + Environment.NewLine +
                            "StackTrace: " + e.StackTrace, null, e);

                        retry = false;
                    }
                }
            }
            return null;
        }
        public EventCategories GetCategory()
        {
            try
            {
                using (var client = new ApiHttpClient(new Uri(EventfulApiUrl)))
                {
                    EventCategories eventCategory = client.GetEventCategoryAsync(ConfigProvider.Instance.EventFulClientKey);
                    //Verifies that the the server
                    if (eventCategory != null)
                    {
                        return eventCategory;
                    }                    
                }
            }
            catch (Exception e)
            {
                Logger.Log(Level.Error,
                        "Error getting event search data. This was the last attempt and will not try agian." + Environment.NewLine +
                        "Error message: " + e.Message + Environment.NewLine +
                        "StackTrace: " + e.StackTrace, null, e);
                 
            }
            return null;
        }
    }
}
