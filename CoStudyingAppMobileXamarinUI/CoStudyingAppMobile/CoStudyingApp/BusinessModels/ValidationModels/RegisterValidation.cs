using System;
using System.Collections.Generic;
using System.Text;
using YeatyAppMobile.Models.AppConstants;

namespace YeatyAppMobile.BusinessModels.ValidationModels
{
    public class RegisterValidation
    {

        string lettersCapitals = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";


        public static ValidationModel CheckUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
                return new ValidationModel() { isValid = false, Message = ValidationMessages.EmptyUsername };
            else if (username.Length < 3)
                return new ValidationModel() { isValid = false, Message = ValidationMessages.ShortUsername };
            else if (username.Length > 14)
                return new ValidationModel() { isValid = false, Message = ValidationMessages.LongUsername };
            else
            {
                foreach (string city in CitiesAndTowns.Cities)
                    if (city.ToLower() == username.ToLower())
                        return new ValidationModel() { isValid = false, Message = ValidationMessages.InvalidUsername };
                foreach (string city in CitiesAndTowns.IstanbulTowns)
                    if (city.ToLower() == username.ToLower())
                        return new ValidationModel() { isValid = false, Message = ValidationMessages.InvalidUsername };
                foreach (string city in CitiesAndTowns.IzmirTowns)
                    if (city.ToLower() == username.ToLower())
                        return new ValidationModel() { isValid = false, Message = ValidationMessages.InvalidUsername };
                return new ValidationModel() { isValid = true, Message = "Valid" };
            }
        }

        public static ValidationModel CheckPassword(string pass, string pass2)
        {
            if (String.IsNullOrEmpty(pass))
                return new ValidationModel() { isValid = false, Message = ValidationMessages.EmptyPassword };
            else if (String.IsNullOrEmpty(pass2))
                return new ValidationModel() { isValid = false, Message = ValidationMessages.EmptyPassword };
            else if ( pass.ToLower() != pass2.ToLower() )
                return new ValidationModel() { isValid = false, Message = ValidationMessages.DifferentPasswords };
            else if (pass.Length < 6)
                return new ValidationModel() { isValid = false, Message = ValidationMessages.ShortPass };
            else if (pass.Length > 24)
                return new ValidationModel() { isValid = false, Message = ValidationMessages.LongPass };
            else
                return new ValidationModel() { isValid = true, Message = "Valid" };
        }


        public static ValidationModel CheckEmail(string mail)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(mail);
                return new ValidationModel() { isValid = true, Message = "Valid" };
            }
            catch
            {
                return new ValidationModel() { isValid = false, Message = ValidationMessages.InvalidEmail };
            }

        }

        






    }
}
