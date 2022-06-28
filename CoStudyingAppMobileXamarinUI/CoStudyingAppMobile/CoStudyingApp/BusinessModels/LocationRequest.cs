using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using YeatyAppMobile.Models;

namespace YeatyAppMobile.BusinessModels
{
    public class LocationRequest
    {
         

        public static async Task<LatLongInfoModel> GetCurrentLocation()
        {
            GeolocationRequest locationRequest;
            Location currentLocation = new Location();
            LatLongInfoModel latLong = new LatLongInfoModel();
            try
            {
                locationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                currentLocation = await Geolocation.GetLocationAsync(locationRequest);
            }           
            catch (Exception ex)
            {
                return latLong;
            }
            if (currentLocation == null)
                return latLong;
            else
            {
                latLong.Lat = currentLocation.Latitude;
                latLong.Long = currentLocation.Longitude;
                return latLong;
            }


        }



    }
}
