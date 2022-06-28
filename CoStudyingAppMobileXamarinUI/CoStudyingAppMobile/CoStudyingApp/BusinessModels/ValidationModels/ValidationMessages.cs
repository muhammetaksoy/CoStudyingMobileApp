using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.StorageModels;

namespace YeatyAppMobile.BusinessModels.ValidationModels
{
    public class ValidationMessages
    {

        private static string currentLang
        {
            get
            {
                return AppPropertyStorage.GetLanguageCode();
            }
        }

        public static string ShortPass
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Şifre en az 6 karakter içermelidir.";
                else
                    return "Password must contain at least 6 characters";
            }
        }

        public static string LongPass
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Şifre en fazla 24 karakter içermelidir.";
                else
                    return "Password must contain at most 24 characters";
            }
        }

        public static string ShortUsername
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kullanıcı adı en az 3 karakter içermelidir.";
                else
                    return "Username must contain at least 3 characters";
            }
        }

        public static string LongUsername
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kullanıcı adı en fazla 14 karakter içermelidir.";
                else
                    return "Username must contain at least 14 characters";
            }
        }


        public static string InvalidUsername
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Bu kullanıcı adı kullanılamaz.";
                else
                    return "This username cannot be used";
            }
        }

        public static string EmptyUsername
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kullanıcı adı alanı boş geçilemez.";
                else
                    return "Username cannot be null";
            }
        }

        public static string EmptyPassword
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Şifre alanı boş geçilemez.";
                else
                    return "Password cannot be null";
            }
        }


        public static string InvalidEmail
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Email geçersiz";
                else
                    return "Invalid email";
            }
        }

        public static string WrongPassword
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Kullanıcı adı veya şifre hatalı";
                else
                    return "Wrong username or password";
            }
        }

        public static string TakenUsername
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Bu kullanıcı adı zaten kullanılıyor";
                else
                    return "This username is already taken";
            }
        }

        public static string TakenEmail
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Bu eposta adresi zaten kullanılıyor";
                else
                    return "This email address is already used";
            }
        }

        public static string DifferentPasswords
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Girdiğiniz şifreler aynı değil";
                else
                    return "The passwords you entered are not same";
            }
        }

        public static string DefaultMessage
        {
            get
            {
                if (currentLang == AppLanguages.Turkish)
                    return "Bir hata oluştu";
                else
                    return "An error occured";
            }
        }







    }
}
