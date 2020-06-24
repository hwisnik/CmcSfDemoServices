#pragma warning disable 1591
using Shared.Logger;
using log4net;
using Microsoft.Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CmcSfDemoServices.Owin
{
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        private static readonly ILog LoggingInstance = AppLogger.Initialize();
 
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                AppLogger.ApplicationName = Assembly.GetExecutingAssembly().FullName;
                AppLogger.LogException(LoggingInstance,"Unhandled Global Exception caught by OWin GlobalExceptionMiddleware", ex);
            }
        }
    }
}