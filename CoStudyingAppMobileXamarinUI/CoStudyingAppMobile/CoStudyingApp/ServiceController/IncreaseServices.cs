using CoStudyApp.Models.DBModels;
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
    public class IncreaseServices
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

        public async Task<Room> CreateRoom(string roomName, bool isPrivateRoom, string description)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Increase/CreateRoom?roomName=" + roomName + "&isPrivateRoom=" + isPrivateRoom + "&description=" + description;
                var result = await client.GetStringAsync(input);
                Room response = JsonConvert.DeserializeObject<Room>((result));
                return response;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<bool> JoinGroup(int groupId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Increase/JoinGroup?groupId=" + groupId;
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

        public async Task<bool> CreatePersonalTask(int month, int day, int year, string text)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Increase/CreatePersonalTask?month=" + month + "&day=" + day +
                    "&year=" + year + "&text=" + text;
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

        public async Task<bool> CreateAnnouncement(int roomId, string text)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Increase/CreateAnnouncement?roomId=" + roomId + "&text=" + text;
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

        //public async Task<bool> AddFavoriteRestaurant(int restaurantId)
        //{
        //    try
        //    {
        //        var client = await GetClient();
        //        var input = ServiceInfos.ServiceUrlPrefix + "/api/Increase/AddFavoriteRestaurant?restaurantId=" + restaurantId;
        //        var result = await client.GetAsync(input);
        //        if (result.IsSuccessStatusCode)
        //            return true;
        //        return false;
        //    }
        //    catch (HttpRequestException ex)
        //    {

        //        return false;
        //    }           
        //}

        //public async Task<bool> AddFavoriteMenuItem(int menuItemId)
        //{
        //    try
        //    {
        //        var client = await GetClient();
        //        var input = ServiceInfos.ServiceUrlPrefix + "/api/Increase/AddFavoriteMenuItem?menuItemId=" + menuItemId;
        //        var result = await client.GetAsync(input);
        //        if (result.IsSuccessStatusCode)
        //            return true;
        //        return false;
        //    }
        //    catch (HttpRequestException ex)
        //    {

        //        return false;
        //    }
        //}

        //public async Task<bool> AddRestaurantRegisterForm(string contact, string location, string restaurantName)
        //{
        //    try
        //    {
        //        var client = await GetClient();
        //        var input = ServiceInfos.ServiceUrlPrefix + "/api/Increase/AddRestaurantRegisterForm?contact=" + contact + "&location=" + location +
        //            "&restaurantName=" + restaurantName;
        //        var result = await client.GetAsync(input);
        //        if (result.IsSuccessStatusCode)
        //            return true;
        //        return false;
        //    }
        //    catch (HttpRequestException ex)
        //    {

        //        return false;
        //    }
        //}


    }
}
