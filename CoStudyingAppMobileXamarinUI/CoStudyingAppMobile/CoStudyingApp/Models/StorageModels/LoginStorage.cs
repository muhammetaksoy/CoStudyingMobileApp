using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class LoginStorage
    {
        private static string KEY_EMAIL = "localEmail";
        private static string KEY_PASSWORD = "localPassword";
        private static string KEY_USERID = "localUserId";
        private static string TOKEN = "token";
        private static string KEY_TOKEN_DATE = "localTokenDate";
        private static string KEY_USERNAME = "localUsername";
        public static string ID
        {
            get
            {
                return (string)GetLocalData<string>(KEY_USERID) ?? "";
            }
            set
            {
                SetLocalData(KEY_USERID, value);
            }
        }

        public static string Token
        {
            get
            {
                return (string)GetLocalData<string>(TOKEN) ?? "";
            }
            set
            {
                SetLocalData(TOKEN, value);
            }
        }

        public static string TokenDate
        {
            get
            {
                return (string)GetLocalData<string>(KEY_TOKEN_DATE) ?? "";
            }
            set
            {
                SetLocalData(KEY_TOKEN_DATE, value);
            }
        }

        public static string Password
        {
            get
            {
                return (string)GetLocalData<string>(KEY_PASSWORD) ?? "";
            }
            set
            {
                SetLocalData(KEY_PASSWORD, value);
            }
        }

        //public static string Email
        //{
        //    get
        //    {
        //        return (string)GetLocalData<string>(KEY_EMAIL) ?? "";
        //    }
        //    set
        //    {
        //        SetLocalData(KEY_EMAIL, value);
        //    }
        //}

        public static string Username
        {
            get
            {
                return (string)GetLocalData<string>(KEY_USERNAME) ?? "";
            }
            set
            {
                SetLocalData(KEY_USERNAME, value);
            }
        }

        public static string TargetPage
        {
            get
            {
                return (string)GetLocalData<string>("targetPage") ?? "";
            }
            set
            {
                SetLocalData("targetPage", value);
            }
        }

        //NEW
        public static string TargetId
        {
            get
            {
                return (string)GetLocalData<string>("targetId") ?? "";
            }
            set
            {
                SetLocalData("targetId", value);
            }
        }

        

        public static string countUnreadNotifications
        {
            get
            {
                return (string)GetLocalData<string>("countUnreadNotifications") ?? "";
            }
            set
            {
                SetLocalData("countUnreadNotifications", value);
            }
        }

        public static string NeedUpdateStr
        {
            get
            {
                return (string)GetLocalData<string>("needUpdateStr") ?? "";
            }
            set
            {
                SetLocalData("needUpdateStr", value);
            }
        }

    

        public static string HashKey
        {
            get
            {
                return (string)GetLocalData<string>("hashkey") ?? "";
            }
            set
            {
                SetLocalData("hashkey", value);
            }
        }

        public static string HashKey2
        {
            get
            {
                return (string)GetLocalData<string>("hashkey2") ?? "";
            }
            set
            {
                SetLocalData("hashkey2", value);
            }
        }



        public static string LanguageCode
        {
            get
            {
                return (string)GetLocalData<string>("languageCode") ?? "";
            }
            set
            {
                SetLocalData("languageCode", value);
            }
        }


        // tl, euro, dolar..
        public static string CurrencyStr
        {
            get
            {
                return (string)GetLocalData<string>("currencyStr") ?? "";
            }
            set
            {
                SetLocalData("currencyStr", value);
            }
        }

        public static string EuroToTlStr
        {
            get
            {
                return (string)GetLocalData<string>("euroToTlStr") ?? "";
            }
            set
            {
                SetLocalData("euroToTlStr", value);
            }
        }


        static void SetLocalData(string key, object data)
        {
            if (!Xamarin.Essentials.Preferences.ContainsKey(key))
            {
                //Xamarin.Forms.Application.Current.Properties.Add(key, data.ToString());
                Xamarin.Essentials.Preferences.Set(key, data.ToString());
            }
            else
            {
                //Xamarin.Forms.Application.Current.Properties[key] = data.ToString();
                Xamarin.Essentials.Preferences.Set(key, data.ToString());
            }
            Xamarin.Forms.Application.Current.SavePropertiesAsync();
        }

        static object GetLocalData<T>(string key)
        {
            //if (Xamarin.Forms.Application.Current.Properties.ContainsKey(key))
            //    return Xamarin.Forms.Application.Current.Properties[key];
            //else
            //    return null;

            if (Xamarin.Essentials.Preferences.ContainsKey(key))
                return Xamarin.Essentials.Preferences.Get(key, null);
            else
                return null;
        }



        public static void ResetAllData()
        {
            //Xamarin.Forms.Application.Current.Properties.Clear();
            //Xamarin.Forms.Application.Current.SavePropertiesAsync();
            Xamarin.Essentials.Preferences.Clear();

        }

        public static void RemoveData(string key)
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(key))
                Xamarin.Essentials.Preferences.Remove(key);
        }

    }
}
