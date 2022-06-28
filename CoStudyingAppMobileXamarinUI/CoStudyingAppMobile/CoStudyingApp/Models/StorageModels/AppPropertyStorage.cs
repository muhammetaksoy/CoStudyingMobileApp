using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.AppConstants;

namespace YeatyAppMobile.Models.StorageModels
{
    public class AppPropertyStorage
    {
        private static string KEY_PROPERTY = "currentProperty";


        public static AppPropertyModel GetCurrentProperties()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(KEY_PROPERTY))
            {
                string str = Xamarin.Essentials.Preferences.Get(KEY_PROPERTY, null);
                AppPropertyModel model = JsonConvert.DeserializeObject<AppPropertyModel>(str);
                return model;
            }
            else
            {
                AppPropertyModel model = new AppPropertyModel();
                var jsonResult = JsonConvert.SerializeObject(model);
                Xamarin.Essentials.Preferences.Set(KEY_PROPERTY, jsonResult);
                return model;
            }
        }

        public static void SaveProperties(AppPropertyModel model)
        {
            var jsonResult = JsonConvert.SerializeObject(model);
            Xamarin.Essentials.Preferences.Set(KEY_PROPERTY, jsonResult);
        }

        public static void SetLanguage(string langCode)
        {
            AppPropertyModel model = GetCurrentProperties();
            if (langCode.ToLower() == AppLanguages.English)
                model.LangCode = AppLanguages.English;
            else if (langCode.ToLower() == AppLanguages.Arabic)
                model.LangCode = AppLanguages.Arabic;
            else
                model.LangCode = AppLanguages.Turkish;
            SaveProperties(model);

        }

        public static string GetLanguageCode()
        {
            AppPropertyModel currentModel = GetCurrentProperties();
            if ( !String.IsNullOrEmpty(currentModel.LangCode) )
            {
                if (currentModel.LangCode.ToLower() != AppLanguages.Turkish && currentModel.LangCode.ToLower() != AppLanguages.English 
                    && currentModel.LangCode.ToLower() != AppLanguages.Arabic)
                    return AppLanguages.Turkish;
                else
                    return currentModel.LangCode.ToLower();

            }
            else
            {
                SetLanguage(AppLanguages.Turkish);
                return AppLanguages.Turkish;
            }
        }

        public static void SetUserAllowsLocation(bool isAllowed, LatLongInfoModel model)
        {
            AppPropertyModel currentModel = GetCurrentProperties();
            currentModel.isAllowedLocation = isAllowed;
            currentModel.LastLocation = model;
            SaveProperties(currentModel);
        }

        public static void UpdateLocation(LatLongInfoModel model)
        {
            AppPropertyModel currentModel = GetCurrentProperties();
            currentModel.LastLocation = model;
            SaveProperties(currentModel);
        }

        public static LatLongInfoModel GetUserLastLocation()
        {
            AppPropertyModel currentModel = GetCurrentProperties();
            if ( currentModel.LastLocation != null )
            {
                return currentModel.LastLocation;
            }
            else
            {
                return new LatLongInfoModel() { Lat = 0, Long = 0 };
            }
        }

        public static bool isUserAllowedLocation()
        {
            AppPropertyModel currentModel = GetCurrentProperties();
            return currentModel.isAllowedLocation;
        }




    }
}
