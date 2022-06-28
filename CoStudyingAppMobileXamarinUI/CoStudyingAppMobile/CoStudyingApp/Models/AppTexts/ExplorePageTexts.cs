using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.StorageModels;

namespace YeatyAppMobile.Models.AppTexts
{
    public class ExplorePageTexts
    {
        private static string currentLang
        {
            get
            {
                return AppPropertyStorage.GetLanguageCode();
            }
        }

        public static string ButtonTopMenuForYouText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Size özel";
                else
                    return "For you";
            }
        }

        public static string ButtonTopMenuCofeeText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kahve";
                else
                    return "Coffee";
            }
        }

        public static string ButtonTopMenuDinnerText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Akşam yemeği";
                else
                    return "Dinner";
            }
        }

        public static string ButtonTopMenuBreakfastText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kahvaltı";
                else
                    return "Breakfast";
            }
        }

        public static string ButtonTopMenuSweetText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Tatlı";
                else
                    return "Sweet";
            }
        }

        public static string ButtonTopMenuOpenAirText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Açık hava";
                else
                    return "Open air";
            }
        }

        public static string SeeAllRestaurantsText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Tümünü gör";
                else
                    return "See all";
            }
        }

        public static string SeeAllMenuItemsText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Tüm ürünlere gözat";
                else
                    return "See all items";
            }
        }

        public static string Header1ForYouText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Çevrenizde Popüler";
                else
                    return "Popular places nearby";
            }
        }

        public static string Header1CoffeeText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "En popüler kahveciler";
                else
                    return "Popular Coffee Shops";
            }
        }

        public static string Header1DinnerText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Yemeği nerede yesek?";
                else
                    return "Where to eat";
            }
        }

        public static string Header1BreakfastText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kahvaltı mekanları";
                else
                    return "Best for breakfast";
            }
        }

        public static string Header1SweetText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Tatlı diyince akla gelenler";
                else
                    return "Too sweet to eat";
            }
        }

        public static string Header1OpenAirText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Açık hava mekanları";
                else
                    return "Open air places";
            }
        }

        public static string SubTitleForYouText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Birçok Yeaty kullanıcısının tercih ettiği, eşsiz deneyimler sunan kafe ve restoranları keşfedin.";
                else
                    return "Discover cafes and restaurants that offer unique experiences, favored by many Yeaty users.";
            }
        }


        public static string SubTitleBreakfastText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Birbirinden güzel kahvaltı mekanlarında Yeaty ile güne güzel bir başlangıç yapın. ";
                else
                    return "Perfect start of a day. Explore the delicious breakfast options on Yeaty.";
            }
        }

        public static string SubTitleCoffeeText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Bir kahve molası vererek günün yorgunluğunu ve stresinden kurtulun. Yeaty'de kahve mekanlarını keşfedin.";
                else
                    return "Take a coffee break and get rid of the daily stress. Discover coffee places in Yeaty.";
            }
        }

        public static string SubTitleDinnerText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Yeaty kullanıcılarına özel fırsatlar ile keyifli bir yemeğe ne dersiniz?";
                else
                    return "How about a pleasant meal with special offers for Yeaty users?";
            }
        }

        public static string SubTitleSweetText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Birbirinden güzel tatlıcılar ile gününüze neşe katın. Yeaty fırsatlarını keşfedşin.";
                else
                    return "Add some joy to your day with beautiful desserts. Discover Yeaty deals.";
            }
        }

        public static string SubTitleOpenAirText
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Şehrin keşmekeşinden kurtulup rahat bir nefes alırken Yeaty fırsatlarının tadını çıkarın.";
                else
                    return "Get away from the hustle of the city, enjoy your meal with Yeaty.";
            }
        }







    }
}
