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
    public class ManageServices
    {

        // THIS SERVICE WILL BE USED ON AUTH ACTIONS( LOGIN, REGISTER ETC.) 
        // using on situations that require Authorization information
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

        public async Task<bool> GetToken()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://www.costudyingapp.online/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri("http://costudyingapp.online/getToken");
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("userName", LoginStorage.Username);
                    dict.Add("password", LoginStorage.Password);
                    dict.Add("grant_type", "password");
                    HttpContent content = new FormUrlEncodedContent(dict);
                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        if (!String.IsNullOrEmpty(LoginStorage.Token))
                        {
                            LoginStorage.Token = "";
                        }
                        TokenModel model = new TokenModel();
                        var data = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<TokenModel>(data);
                        LoginStorage.Token = model.access_token;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (TaskCanceledException cx)
            {
                return false;
            }

        }


        public async Task<string> OnRegisterValidateUsernameControl(string userName)
        {
            try
            {
                var client = await GetFirstClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Logon/ValidateUsername?userName=" + userName;
                HttpResponseMessage result = await client.GetAsync(input);
                if (result.StatusCode == HttpStatusCode.OK)
                    return "Success";
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                    return "Invalid";
                else if (result.StatusCode == HttpStatusCode.Conflict)
                    return "Taken";
                else
                    return "Unknown";
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }

        }

        public async Task<string> OnRegisterValidateEmailControl(string email)
        {
            try
            {
                var client = await GetFirstClient();
                var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Logon/ValidateEmail?email=" + email;
                HttpResponseMessage result = await client.GetAsync(input);
                if (result.StatusCode == HttpStatusCode.OK)
                    return "Success";
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                    return "Invalid";
                else if (result.StatusCode == HttpStatusCode.Conflict)
                    return "Taken";
                else
                    return "Unknown";
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }

        }

        public async Task<RegisterResponseModel> BasicRegister(CoStudyApp.Models.DBModels.User user)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    RegisterResponseModel model = new RegisterResponseModel();
                    client.BaseAddress = new Uri("http://www.costudyingapp.online/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri("http://www.costudyingapp.online/api/Logon/BasicRegister");

                    //   string serializedObject = JsonConvert.SerializeObject(model);
                    string serializedObject = JsonConvert.SerializeObject(user);
                    HttpContent contentPost = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                    if (response.IsSuccessStatusCode)
                    {
                        RegisterResponseModel modelToReturn = new RegisterResponseModel();
                        var data = await response.Content.ReadAsStringAsync();
                        modelToReturn = JsonConvert.DeserializeObject<RegisterResponseModel>(data);
                        return modelToReturn;
                    }
                    return new RegisterResponseModel() { isSuccess = false, ReturnMessage = "Hata Oluştu.", User = null };
                }
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }

        }

        public async Task<LoginModel> Login()
        {
            try
            {
                if (LoginStorage.Token != null)
                {
                    var client = await GetClient();
                    var input = ServiceInfos.CoStudyServiceUrlPrefix + "/api/Logon/Login";
                    var result = await client.GetStringAsync(input);
                    LoginModel response = JsonConvert.DeserializeObject<LoginModel>((result));
                    LoginStorage.ID = response.currentUser.Id.ToString();
                    LoginStorage.countUnreadNotifications = response.countUnreadNotifications.ToString();
                    //FavoritesStorage.ClearStorage();
                    //if( response.currentUser.FavoriteRestaurants.Count > 0)
                    //{
                    //    List<int> ids = response.currentUser.FavoriteRestaurants.Select(x => x.RestaurantId).ToList();
                    //    FavoritesStorage.AddFavoriteRestaurant(ids);
                    //}
                    //if (response.currentUser.FavoriteMenuItems.Count > 0)
                    //{
                    //    List<int> ids = response.currentUser.FavoriteMenuItems.Select(x => x.MenuItemId).ToList();
                    //    FavoritesStorage.AddFavoriteMenuItem(ids);
                    //}
                    //if (response.currentUser.FavoriteRecipes.Count > 0)
                    //{
                    //    List<int> ids = response.currentUser.FavoriteRecipes.Select(x => x.RecipeId).ToList();
                    //    FavoritesStorage.AddFavoriteRecipe(ids);
                    //}
                    return response;
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


    }
}
