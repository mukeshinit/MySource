using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventfulWebApi.Models;
namespace EventfulWebApi.Service.Interface
{

    public interface IEventResultService
    {
        EventSearchResults GetEventSearchResults(string address , int radius , DateTime startdate, DateTime enddate, string eventCategory);

    }
}
