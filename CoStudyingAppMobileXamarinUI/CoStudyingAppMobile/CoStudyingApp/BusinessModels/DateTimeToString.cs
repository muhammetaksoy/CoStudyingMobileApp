using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.BusinessModels
{
    public class DateTimeToString
    {
        public static Dictionary<int, string> monthsDict = new Dictionary<int, string>()
            {
                {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4, "Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},{9,"Eylül"},
                {10,"Ekim" },{11,"Kasım"},{12,"Aralık" }
            };

        public static string ConvertDateTimeToStr(DateTime DATE)
        {

            return DATE.Day.ToString() + " " + monthsDict[DATE.Month] + " " + DATE.Year.ToString();
        }

        public static string ConvertTimeToStr(DateTime DATE)
        {

            return DATE.Hour.ToString() + "." + DATE.Minute.ToString();
        }

        public static string GetMonthAndYearStr(DateTime DATE)
        {
            Dictionary<int, string> monthsDict = new Dictionary<int, string>()
            {
                {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4, "Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},{9,"Eylül"},
                {10,"Ekim" },{11,"Kasım"},{12,"Aralık" }
            };

            return monthsDict[DATE.Month] + " " + DATE.Year.ToString();
        }

        public static string DefinePastTime(DateTime msgTime)
        {
            TimeSpan difference = DateTime.Now - msgTime;
            if (difference.TotalDays < 1)
            {
                if (difference.TotalHours < 1)
                {
                    if (difference.TotalSeconds <= 60)
                    {
                        return "1 dakika önce";
                    }
                    return Convert.ToInt16(difference.TotalMinutes).ToString() + " dakika önce";

                }
                else
                {
                    return Convert.ToInt16(difference.TotalHours).ToString() + " saat önce";
                }
            }
            else if (difference.TotalDays < 30)
            {
                if (difference.TotalDays < 7)
                {
                    return Convert.ToInt16(difference.TotalDays).ToString() + " gün önce";
                }
                if (difference.TotalDays < 14)
                {
                    return "1 hafta önce";
                }
                if ( difference.TotalDays < 21 )
                {
                    return "2 hafta önce";
                }
                else
                {
                    return "3 hafta önce";
                }        
            }
            else if (difference.TotalDays < 365)
            {
                int months = Convert.ToInt32(difference.TotalDays / 30);
                return months.ToString() + " ay önce";
            }
            else
            {
                int year = Convert.ToInt32(difference.TotalDays / 365);
                return year.ToString() + " yıl önce";
            }

        }

    }
}
