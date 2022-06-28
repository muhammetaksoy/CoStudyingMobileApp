using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    public class CustomTime
    {
        TimeZone timeZone;

        public static DateTime GetNowTime()
        {

            TimeZoneInfo timeZone;
            timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            DateTime NowTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
            return NowTime;

        }

        public static string GetDateTimeString(DateTime dateTime)
        {
            Dictionary<int, string> monthDict = new Dictionary<int, string>()
            {
                {1,"Ocak" },{2,"Şubat"},{3,"Mart"},{4,"Nisan"},{5,"Mayıs"},{6,"Haziran"},{7,"Temmuz"},{8,"Ağustos"},{9,"Eylül"}
                ,{10,"Ekim"},{11,"Kasım"},{12,"Aralık"}
            };

            return dateTime.Day.ToString() + " " + monthDict[dateTime.Month] + " " + dateTime.Year.ToString();
        }

        public static string DefineDateTime(DateTime dateTime)
        {
            string minStr, hourStr, dayStr, monthStr;
            if (dateTime.Minute < 10)
                minStr = "0" + dateTime.Minute;
            else
                minStr = dateTime.Minute.ToString();
            if (dateTime.Hour < 10)
                hourStr = "0" + dateTime.Hour;
            else
                hourStr = dateTime.Hour.ToString();
            if (dateTime.Day < 10)
                dayStr = "0" + dateTime.Day;
            else
                dayStr = dateTime.Day.ToString();
            if (dateTime.Month < 10)
                monthStr = "0" + dateTime.Month;
            else
                monthStr = dateTime.Month.ToString();

            return dayStr + "." + monthStr + "." + dateTime.Year + " " + hourStr + ":" + minStr;
        }

    }
}