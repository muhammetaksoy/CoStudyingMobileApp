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
    public class QRServices
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


        public async Task<string> CreatePaybackQR()
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/QR/CreatePaybackQR";
                var result = await client.GetStringAsync(input);
                string response = JsonConvert.DeserializeObject<string>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    string modelToReturn = "";
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await CreatePaybackQR();
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<string> CreatePurchaseMenuItemQR(int purchaseMenuItemId, bool isLastPurchase)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/QR/CreatePurchaseMenuItemQR?purchaseMenuItemId="+purchaseMenuItemId
                    + "&isLastPurchase=" + isLastPurchase;
                var result = await client.GetStringAsync(input);
                string response = JsonConvert.DeserializeObject<string>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    string modelToReturn = "";
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await CreatePurchaseMenuItemQR(purchaseMenuItemId, isLastPurchase);
                    return modelToReturn;
                }
                return null;
            }
            catch (TaskCanceledException cx)
            {
                return null;
            }
        }

        public async Task<string> CreatePurchaseCampaignQR(int purchaseCampaignId, bool isLastPurchase)
        {
            try
            {
                var client = await GetClient();
                var input = ServiceInfos.ServiceUrlPrefix + "/api/QR/CreatePurchaseCampaignQR?purchaseCampaignId=" + purchaseCampaignId
                    + "&isLastPurchase=" + isLastPurchase;
                var result = await client.GetStringAsync(input);
                string response = JsonConvert.DeserializeObject<string>((result));
                if (response != null)
                    return response;
                return null;
            }
            catch (HttpRequestException ex)
            {
                if (!String.IsNullOrEmpty(LoginStorage.Username) && !String.IsNullOrEmpty(LoginStorage.Password))
                {
                    string modelToReturn = "";
                    ManageServices service = new ManageServices();
                    await service.GetToken();
                    modelToReturn = await CreatePurchaseCampaignQR(purchaseCampaignId, isLastPurchase);
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
