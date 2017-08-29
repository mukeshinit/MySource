using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventfulWebApi.Models.Models;
namespace EventfulWebApi.Service.Interface
{
    public interface IGoogleGeocdService
    {
        GoogleGeocd GetGoogleGeocdResults(string address);
    }
}
