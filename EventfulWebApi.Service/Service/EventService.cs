using EventfulWebApi.Models;
using EventfulWebApi.Service.Interface;
using System;

namespace EventfulWebApi.Service.Service
{
    public class EventService : IEventResultService
    {
         
        public EventSearchResults GetEventSearchResults(string address, int radius, DateTime startdate, DateTime enddate,
            string eventcategory)
        {
            try
            {
                var manager = new ServiceManager();
                return manager.GetEvents(address, radius, startdate, enddate, eventcategory);
            }
            catch(Exception ex)
            {
                return null;
            }
            }
            
    }
}
