using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YeatyAppMobile.Models;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;


namespace YeatyAppMobile.ServiceController
{
    public class DecreaseServices
    {

        public async Task<HttpClient> GetClient()
        {
            try
            {
                string token = LoginStorage.Token;
                HttpClient client = new HttpClient();
                //  client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);        
                client.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return client;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }

        }

        // using on situations that don't require Authorization information
        public async Task<HttpClient> GetFirstClient()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }

        }

        private async Task<HttpClient> GetJsonContentClient()
        {
            try
            {
                string token = LoginStorage.Token;
                HttpClient client = new HttpClient();
                //  client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);        
                client.DefaultRequestHeaders.Add("ContentType", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return client;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }

        public async Task<bool> RemoveFavoriteRestaurant(int restaurantId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Decrease/RemoveFavoriteRestaurant?restaurantId=" + restaurantId;
                var result = await client.GetAsync(input);
                if (result.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (HttpRequestException ex)
            {

                return false;
            }
        }

        public async Task<bool> RemoveFavoriteMenuItem(int menuItemId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Decrease/RemoveFavoriteMenuItem?menuItemId=" + menuItemId;
                var result = await client.GetAsync(input);
                if (result.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (HttpRequestException ex)
            {

                return false;
            }
        }
    }
}
