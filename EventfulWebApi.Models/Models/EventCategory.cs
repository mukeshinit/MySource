using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventfulWebApi.Models
{
 
    public class EventCategory
    {
        public string name { get; set; }
        public string id { get; set; }
    }
    public class EventCategories
    {
        public List<EventCategory> category { get; set; }
    }

}
