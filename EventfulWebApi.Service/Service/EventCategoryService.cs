using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventfulWebApi.Models;
using EventfulWebApi.Service.Interface;

namespace EventfulWebApi.Service.Service
{
    public class EventCategoryService : IEventCategoryService
    {
        public EventCategories GetEventCategory()
        {
            try
            {
                var manager = new ServiceManager();
                return manager.GetCategory();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
