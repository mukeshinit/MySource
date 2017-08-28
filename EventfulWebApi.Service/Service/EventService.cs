using EventfulWebApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventfulWebApi.Models;
using EventfulWebApi.Service.Httpclients;
using EventfulWebApi.Logging;
using log4net.Core;
using System.Threading;
using EventfulWebApi.Config;

namespace EventfulWebApi.Service.Service
{
    public class EventService : IEventResultService
    {
        public EventSearchResults eventSearchResults(string address, int radius, DateTime startdate, DateTime enddate,
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
