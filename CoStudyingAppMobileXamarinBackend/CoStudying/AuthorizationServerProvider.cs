using Microsoft.Owin.Security.OAuth;
using CoStudying.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CoStudying
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //BURADA SAKLANAN VERİLER METHODLARDA CURRENT USERA ERİŞMEK İÇİNDİR.USERINFOMETHOD ARACILIĞIYLA ERİŞİLECEK.
            using (DatabaseContext DbContext = new DatabaseContext())
            {
                var user = DbContext.Users.FirstOrDefault(x => x.Username.Equals(context.UserName, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    context.SetError("Oturum Hatası", "Kullanıcı adı ve şifre hatalıdır");
                }
                else
                {
                    var inputPass = ManageHashing.SHA256(context.Password, user.Salt); // LOGIN DENENİYOR,SORASIYLA BASIC,FACEBBOK,GOOGLE
                    if (user.Password == inputPass)
                    {
                        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim("UserName", user.Username));
                        identity.AddClaim(new Claim("Role", "User"));
                        identity.AddClaim(new Claim("Password", user.Id.ToString())); // actually  it stores the userId                                                                                    
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("Oturum Hatası", "Kullanıcı adı ve şifre hatalıdır");
                    }
                }


            }
        }


    }
}