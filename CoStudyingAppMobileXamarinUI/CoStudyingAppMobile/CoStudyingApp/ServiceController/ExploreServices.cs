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
using YeatyAppMobile.Models.DBModels.CoStudyingDBModels;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.Models.StorageModels;

namespace YeatyAppMobile.ServiceController
{
    public class ExploreServices
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

        public async Task<List<UserRoomResponseModel>> GetMyUserRooms()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetMyUserRooms";
                var result = await client.GetStringAsync(input);
                List<UserRoomResponseModel> response = JsonConvert.DeserializeObject<List<UserRoomResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<List<Room>> GetSuggestedGroups()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetSuggestedGroups";
                var result = await client.GetStringAsync(input);
                List<Room> response = JsonConvert.DeserializeObject<List<Room>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<FeedPageResponseModel> GetFeedPage()
        {
            try
            {
                var client = await GetFirstClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetFeedPage";
                var result = await client.GetStringAsync(input);
                FeedPageResponseModel response = JsonConvert.DeserializeObject<FeedPageResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<RoomDetailsResponseModel> SearchStudyRoom(string searchKey)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/SearchStudyRoom?searchKey=" + searchKey;
                var result = await client.GetStringAsync(input);
                RoomDetailsResponseModel response = JsonConvert.DeserializeObject<RoomDetailsResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<RoomDetailsResponseModel> GetStudyRoom(int roomId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetStudyRoom?roomId=" + roomId;
                var result = await client.GetStringAsync(input);
                RoomDetailsResponseModel response = JsonConvert.DeserializeObject<RoomDetailsResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }


        public async Task<List<TaskResponseModel>> GetMyTasks()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetMyTasks";
                var result = await client.GetStringAsync(input);
                List<TaskResponseModel> response = JsonConvert.DeserializeObject<List<TaskResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<List<UserRoomTask>> GetUserRoomTasks(int roomId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetUserRoomTasks?roomId=" + roomId;
                var result = await client.GetStringAsync(input);
                List<UserRoomTask> response = JsonConvert.DeserializeObject<List<UserRoomTask>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<List<Faculty>> GetFaculties()
        {
            try
            {
                var client = await GetFirstClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetFaculties";
                var result = await client.GetStringAsync(input);
                List<Faculty> response = JsonConvert.DeserializeObject<List<Faculty>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {               
                return new List<Faculty>();
            }
        }

        public async Task<List<AnnouncementResponseModel>> GetAnnouncementResponseModels()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetAnnouncementResponseModels";
                var result = await client.GetStringAsync(input);
                List<AnnouncementResponseModel> response = JsonConvert.DeserializeObject<List<AnnouncementResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        public async Task<List<University>> GetUniversities()
        {
            try
            {
                var client = await GetFirstClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Explore/GetUniversities";
                var result = await client.GetStringAsync(input);
                List<University> response = JsonConvert.DeserializeObject<List<University>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                return new List<University>();
            }
        }

        //Type1(general), Type2(Coffee), Type3(Dinner), Type4(Breakfast), Type5(Dessert), Type6(OpenAir)
        public async Task<List<Restaurant>> GetExploreResultNearbyRestaurants(LatLongInfoModel model, int type)
        {

            try
            {
                List<Restaurant> modelToReturn = new List<Restaurant>();
                var client = await GetJsonContentClient();
                var uri = new Uri(ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetExploreResultNearbyRestaurants?type=" + type);
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
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<Restaurant> modelToReturn = new List<Restaurant>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetExploreResultNearbyRestaurants(model, type);
                    return modelToReturn;
                }
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

        public async Task<Campaign> GetRegularCampaign()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRegularCampaign";
                var result = await client.GetStringAsync(input);
                Campaign response = JsonConvert.DeserializeObject<Campaign>((result));
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


        public async Task<List<RestaurantMenuItemResponseModel>> GetExploreResultMenuItems(List<int> restaurantIds, int type)
        {

            try
            {
                List<RestaurantMenuItemResponseModel> modelToReturn = new List<RestaurantMenuItemResponseModel>();
                var client = await GetJsonContentClient();
                var uri = new Uri(ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetExploreResultMenuItems?type=" + type);
                string serializedObject = JsonConvert.SerializeObject(restaurantIds);
                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                if (response.IsSuccessStatusCode)
                {

                    var data = await response.Content.ReadAsStringAsync();
                    modelToReturn = JsonConvert.DeserializeObject<List<RestaurantMenuItemResponseModel>>(data);
                    return modelToReturn;
                }
                throw new HttpRequestException();
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<RestaurantMenuItemResponseModel> modelToReturn = new List<RestaurantMenuItemResponseModel>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetExploreResultMenuItems(restaurantIds, type);
                    return modelToReturn;
                }
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

        public async Task<RestaurantProfileResponseModel> GetRestaurantProfile(LatLongInfoModel model, int restaurantId)
        {
            try
            {
                RestaurantProfileResponseModel modelToReturn = new RestaurantProfileResponseModel();
                var client = await GetJsonContentClient();
                var uri = new Uri(ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRestaurantProfile?restaurantId=" + restaurantId);
                string serializedObject = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                if (response.IsSuccessStatusCode)
                {

                    var data = await response.Content.ReadAsStringAsync();
                    modelToReturn = JsonConvert.DeserializeObject<RestaurantProfileResponseModel>(data);
                    return modelToReturn;
                }
                throw new HttpRequestException();
            }
            catch (HttpRequestException ex)
            {

                return null;
            }

        }

        public async Task<List<MenuItem>> GetRestaurantMenuItems(int restaurantId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRestaurantMenuItems?restaurantId=" + restaurantId;
                var result = await client.GetStringAsync(input);
                List<MenuItem> response = JsonConvert.DeserializeObject<List<MenuItem>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<MenuItem> modelToReturn = new List<MenuItem>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetRestaurantMenuItems(restaurantId);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<CampaignRestaurantProfileResponseModel>> GetRestaurantCampaigns(int restaurantId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRestaurantCampaigns?restaurantId=" + restaurantId;
                var result = await client.GetStringAsync(input);
                List<CampaignRestaurantProfileResponseModel> response = JsonConvert.DeserializeObject<List<CampaignRestaurantProfileResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    return null;
                    //List<Campaign> modelToReturn = new List<Campaign>();
                    //ManageServices service = new ManageServices();
                    //await service.GetToken();
                    //modelToReturn = await GetRestaurantCampaigns(restaurantId);
                    //return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<RestaurantPropertyList> GetRestaurantProperties(int restaurantId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRestaurantProperties?restaurantId=" + restaurantId;
                var result = await client.GetStringAsync(input);
                RestaurantPropertyList response = JsonConvert.DeserializeObject<RestaurantPropertyList>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    RestaurantPropertyList modelToReturn = new RestaurantPropertyList();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetRestaurantProperties(restaurantId);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<CampaignDetailsResponseModel> GetCampaignDetails(int campaignId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetCampaignDetails?campaignId=" + campaignId;
                var result = await client.GetStringAsync(input);
                CampaignDetailsResponseModel response = JsonConvert.DeserializeObject<CampaignDetailsResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    CampaignDetailsResponseModel modelToReturn = new CampaignDetailsResponseModel();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetCampaignDetails(campaignId);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<CampaignIndexResponseModel>> GetCampaignsIndex()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetCampaignsIndex";
                var result = await client.GetStringAsync(input);
                List<CampaignIndexResponseModel> response = JsonConvert.DeserializeObject<List<CampaignIndexResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<CampaignIndexResponseModel> modelToReturn = new List<CampaignIndexResponseModel>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetCampaignsIndex();
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<StorePageMenuItemsResponseModel>> GetStorePageItemNumbers()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetStorePageItemNumbers";
                var result = await client.GetStringAsync(input);
                List<StorePageMenuItemsResponseModel> response = JsonConvert.DeserializeObject<List<StorePageMenuItemsResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<StorePageMenuItemsResponseModel> modelToReturn = new List<StorePageMenuItemsResponseModel>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetStorePageItemNumbers();
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<List<QRCodesIndexResponseModel>> GetOwnedItems()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetOwnedItems";
                var result = await client.GetStringAsync(input);
                List<QRCodesIndexResponseModel> response = JsonConvert.DeserializeObject<List<QRCodesIndexResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<QRCodesIndexResponseModel> modelToReturn = new List<QRCodesIndexResponseModel>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetOwnedItems();
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }


        public async Task<List<PreviousActivitiesWalletResponseModel>> GetPreviousActivitiesWallet()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetPreviousActivitiesWallet";
                var result = await client.GetStringAsync(input);
                List<PreviousActivitiesWalletResponseModel> response = JsonConvert.DeserializeObject<List<PreviousActivitiesWalletResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    List<PreviousActivitiesWalletResponseModel> modelToReturn = new List<PreviousActivitiesWalletResponseModel>();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await GetPreviousActivitiesWallet();
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<UserProfileResponseModel> GetUserProfile(int userId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetUserProfile?userId=" + userId ;
                var result = await client.GetStringAsync(input);
                UserProfileResponseModel response = JsonConvert.DeserializeObject<UserProfileResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }         
            catch (Exception cx)
            {
                return null;
            }
        }

        public async Task<List<Review>> GetRestaurantReviews(int RestaurantId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetRestaurantReviews?RestaurantId=" + RestaurantId;
                var result = await client.GetStringAsync(input);
                List<Review> response = JsonConvert.DeserializeObject<List<Review>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (Exception cx)
            {
                return null;
            }
        }

        public async Task<List<FavoriteMenuItemResponseModel>> GetFavoriteMenuItems()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetFavoriteMenuItems";
                var result = await client.GetStringAsync(input);
                List<FavoriteMenuItemResponseModel> response = JsonConvert.DeserializeObject<List<FavoriteMenuItemResponseModel>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (Exception cx)
            {
                return null;
            }
        }

        public async Task<List<Restaurant>> GetFavoriteRestaurants()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Explore/GetFavoriteRestaurants";
                var result = await client.GetStringAsync(input);
                List<Restaurant> response = JsonConvert.DeserializeObject<List<Restaurant>>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (Exception cx)
            {
                return null;
            }
        }
















    }
}
