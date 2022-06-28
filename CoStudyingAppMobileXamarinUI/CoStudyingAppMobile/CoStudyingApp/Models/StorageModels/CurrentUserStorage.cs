using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class CurrentUserStorage
    {

        //THIS IS A TEMPORARY STORAGE OF USER. WILL BE REMOVED WHEN SESSION ENDS.

        public static void ClearStorage()
        {
            if (App.Current.Properties.ContainsKey("currentUser"))
                App.Current.Properties.Remove("currentUser");
        }

        public static void SetCurrentUser(User user)
        {
            App.Current.Properties["currentUser"] = user;
            App.Current.SavePropertiesAsync();
        }

        public static User GetCurrentUser()
        {
            if (App.Current.Properties.ContainsKey("currentUser"))
                return App.Current.Properties["currentUser"] as User;
            return null;
        }

        public static bool isUserExist()
        {
            return App.Current.Properties.ContainsKey("currentUser");
        }

        //public static double GetUserWallet()
        //{
        //    return GetCurrentUser().inWallet;
        //}

        //public static void UpdateUserWallet(double change)
        //{
        //    User currentUser = GetCurrentUser();
        //    currentUser.inWallet += change;
        //    if (currentUser.inWallet <= 0)
        //        currentUser.inWallet = 0;
        //    SetCurrentUser(currentUser);
        //}


        public static void SetUserAllowsLocation(bool isAllowed)
        {
           
            if (isAllowed)
                App.Current.Properties["allowsLocation"] = "True";
            else
                App.Current.Properties["allowsLocation"] = "False";
        }




    }
}
