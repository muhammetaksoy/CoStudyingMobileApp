using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying
{
    public class CustomDateString
    {
        new static List<string> months = new List<string>() {"Ocak","Şubat","Mart","Nisan","Mayıs","Haziran","Temmuz","Ağustos","Eylül","Ekim",
        "Kasım","Aralık"};

        public static string TurnDateTimeToStr(DateTime CreatedOn)
        {
            return CreatedOn.Day + " " + months[CreatedOn.Month - 1] + " " + CreatedOn.Year;
        }

        

    }
}