using CoStudying;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CoStudying
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); //ConfigureAuth(app);
            //ConfigureOAuth(app);
            HttpConfiguration configuration = new HttpConfiguration();
            Configure(app);

            WebApiConfig.Register(configuration);


            app.UseWebApi(configuration);
        }

        private void Configure(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/getToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3), //3 saat
                AllowInsecureHttp = true,
                Provider = new AuthorizationServerProvider()
            };
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}