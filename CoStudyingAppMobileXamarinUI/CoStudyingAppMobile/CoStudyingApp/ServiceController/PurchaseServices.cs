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
    public class PurchaseServices
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

        public async Task<PurchaseMenuItemResponseModel> PurchaseMenuItemByQR(int menuItemId, double Price)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Purchase/PurchaseMenuItemByQR?menuItemId=" + menuItemId +
                    "&Price=" + Price;
                var result = await client.GetStringAsync(input);
                PurchaseMenuItemResponseModel response = JsonConvert.DeserializeObject<PurchaseMenuItemResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    PurchaseMenuItemResponseModel modelToReturn = new PurchaseMenuItemResponseModel();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await PurchaseMenuItemByQR(menuItemId, Price);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<PurchaseCampaignResponseModel> PurchaseCampaignByQR(int campaignId)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/Purchase/PurchaseCampaignByQR?campaignId=" + campaignId ;
                var result = await client.GetStringAsync(input);
                PurchaseCampaignResponseModel response = JsonConvert.DeserializeObject<PurchaseCampaignResponseModel>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    PurchaseCampaignResponseModel modelToReturn = new PurchaseCampaignResponseModel();
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await PurchaseCampaignByQR(campaignId);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }





    }
}
