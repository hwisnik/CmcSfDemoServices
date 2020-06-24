using CmcSfDemoServices.Filters;
using Shared.Logger;
using log4net;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Xml;
using System.Xml.Serialization;

namespace CmcSfDemoServices.App_Start
{
#pragma warning disable 1591

    [ExcludeFromCodeCoverage]
    public class WebApiConfig
	{
        public static void Register(HttpConfiguration config)
		{
            //config.Routes.MapHttpRoute(
            //	name: "AllCustomers",
            //	routeTemplate: "customers",
            //	defaults: new { controller = "Customers", action = "Get"}
            //	);

            // Web API routes
            config.MapHttpAttributeRoutes();
            
		    if (!ConfigurationManager.AppSettings["DeployType"].ToLower().StartsWith("dev"))  //|| ConfigurationManager.AppSettings["DeployType"].ToLower() != "devdeploy")
		    {
                //Only allow Https, not Http in Prod or Test environments
		        config.Filters.Add(new HttpsValidator());
		    }
            
            //Following didn't log everything that I needed See OwinMiddlewareLogger for replacement
            //config.Filters.Add(new LogResponseBodyInterceptorAttribute());
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionLogger());
        }
	}

    /// <summary>
    /// Purpose of this class is to log any unhandled webapi exception that may get swallowed.
    /// </summary>
    public class UnhandledExceptionLogger : ExceptionHandler
    {
        private readonly ILog _loggingInstance;

        public UnhandledExceptionLogger()
        {
           if (_loggingInstance == null) _loggingInstance = AppLogger.Initialize();
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            //StringBuilder sb = new StringBuilder();
            //DataContractSerializer xmlserializer = new DataContractSerializer(context.Exception.GetType());
            //using (XmlWriter writer = XmlWriter.Create(sb))
            //{
            //    xmlserializer.WriteObject(writer, context.Exception);
            //    writer.Close();
            //}
            AppLogger.LogException(_loggingInstance, "WebApi: Logging from WebApi UnhandledExceptionLogger", context.Exception);
            base.Handle(context);
        }
    }
}
