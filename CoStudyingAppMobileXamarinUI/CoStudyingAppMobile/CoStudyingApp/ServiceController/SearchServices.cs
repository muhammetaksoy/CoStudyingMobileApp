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
using YeatyAppMobile.Models.DBModels;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;


namespace YeatyAppMobile.ServiceController
{
    public class SearchServices
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

        public async Task<List<SearchIndexDistrictsResponseModel>> GetSearchIndexDistricts()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Search/GetSearchIndexDistricts";
                var result = await client.GetStringAsync(input);
                List<SearchIndexDistrictsResponseModel> response = JsonConvert.DeserializeObject<List<SearchIndexDistrictsResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    return null;

                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<RecentSearch>> GetRecentSearches()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Search/GetRecentSearches";
                var result = await client.GetStringAsync(input);
                List<RecentSearch> response = JsonConvert.DeserializeObject<List<RecentSearch>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    return null;

                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<Restaurant>> GetSuggestedRestaurantsIndex()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Search/GetSuggestedRestaurantsIndex";
                var result = await client.GetStringAsync(input);
                List<Restaurant> response = JsonConvert.DeserializeObject<List<Restaurant>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    return null;

                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<SearchTopicResultResponseModel>> GetSearchTopicResults(string keyString)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Search/GetSearchTopicResults?keyString=" + keyString;
                var result = await client.GetStringAsync(input);
                List<SearchTopicResultResponseModel> response = JsonConvert.DeserializeObject<List<SearchTopicResultResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    return null;

                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<Restaurant>> GetSearchRestaurants(LatLongInfoModel model, string keyword)
        {
            try
            {
                List<Restaurant> modelToReturn = new List<Restaurant>();
                var client = await GetJsonContentClient();
                var uri = new Uri(ServiceInfos.ServiceUrlPrefix + "/api/Search/GetSearchRestaurants?keyword=" + keyword);
                string serializedObject = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                if (response.IsSuccessStatusCode)
                {

                    var data = await response.Content.ReadAsStringAsync();
                    modelToReturn = JsonConvert.DeserializeObject<List<Restaurant>>(data);
                    return modelToReturn;
                }
                throw new HttpRequestException();
            }
            catch (HttpRequestException ex)
            {              
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
