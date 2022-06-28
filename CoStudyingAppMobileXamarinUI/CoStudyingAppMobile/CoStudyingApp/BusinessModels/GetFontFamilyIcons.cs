using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YeatyAppMobile.BusinessModels
{
    public class GetFontFamilyIcons
    {
        public static string FontAwesomeSolid
        {
            get
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    return "FontAwesome5Free-Solid";
                }
                else
                {
                    return "FontAwesome5Solid.otf#Regular";
                }
            }
        }

        public static string FontAwesomeRegular
        {
            get
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    return  "FontAwesome5Free-Regular";
                }
                else
                {
                    return "FontAwesome5Regular.otf#Regular";
                }
            }
        }

        public static string FontAwesomeLight
        {
            get
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    return "FontAwesome5Free-ProLight";
                }
                else
                {
                    return "FontAwesome5ProLight.otf#Regular";
                }
            }
        }


    }
}
