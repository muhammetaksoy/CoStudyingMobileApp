using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying.Models.BusinessModels
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


    }
}