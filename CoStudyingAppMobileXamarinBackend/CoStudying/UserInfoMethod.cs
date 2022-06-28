using CoStudying.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CoStudying
{
    public class UserInfoMethod
    {
        internal static UserInfo CurrentUser()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims;
            var result = new UserInfo()
            {
                Username = claims.First(x => x.Type == "UserName").Value,
                //Lang = claims.First(x => x.Type == "Lang").Value,

                Role = claims.First(x => x.Type == "Role").Value,
                UserId = Convert.ToInt32(claims.First(x => x.Type == "Password").Value)
            };
            return result;
        }
    }
}