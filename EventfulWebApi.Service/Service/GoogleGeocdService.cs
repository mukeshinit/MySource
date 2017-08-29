using EventfulWebApi.Models.Models;
using EventfulWebApi.Service.Interface;
using System;

namespace EventfulWebApi.Service.Service
{
    public class GoogleGeocdService : IGoogleGeocdService
    {
        public GoogleGeocd GetGoogleGeocdResults(string address)
        {
            try
            {
                var manager = new ServiceManager();
                return manager.GetLatLongByAddress(address);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
