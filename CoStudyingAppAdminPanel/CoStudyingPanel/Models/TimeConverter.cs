using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudyingPanel.Models
{
    public class TimeConverter
    {
        public static string DateTimeToTimeStamps(DateTime time)
        {
            string year = time.Year.ToString();
            string month = time.Month.ToString();
            string day = time.Day.ToString();
            string hour = time.Hour.ToString();
            string minute = time.Minute.ToString();
            if (  time.Month.ToString().Length < 2)
            {
                month = "0" + time.Month.ToString();
            }
            if (day.Length < 2)
                day = "0" + day;
            if (hour.Length < 2)
                hour = "0" + hour;
            if (minute.Length < 2)
                minute = "0" + minute;
            return year + "-" + month + "-" + day + "T" + hour + ":" + minute;

        }


    }
}