using DataAccess.Infrastructure;
using DataAccess.Repositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using CmcSfRestServices.Providers;
using Shared.Commands;
using Shared.Handlers;

// ReSharper disable CheckNamespace check - Resharper not happy with namespace even though it is correct

namespace CmcSfRestServices.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public static class OWinConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureOAuth(IAppBuilder app)
        {
            var allowInsecureHttp = ConfigurationManager.AppSettings["DeployType"] == "Dev";

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                // you can try to run IIS Express for OWIN or OAUTH Development as breakpoints not set in IIS or you can
                // Use Debug Attach to process w3wp.exe (attach to appropriate appPool)  Slow to find all processes but works more reliably than IIS Express and then you may be able to debug as normal after you attach once.

                //#if DEBUG   
                AllowInsecureHttp = allowInsecureHttp,
                //#endif
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ADAuthorizationServerProvider(new SecurityRepository(new ConnectionFactory(), new LoggingCommandHandlerDecorator<LogCommand>())),
                //AuthenticationMode = AuthenticationMode.Active
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
