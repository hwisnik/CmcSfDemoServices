using CmcSfDemoServices.Filters;
using CmcSfDemoServices.Owin;
using DataAccess.Infrastructure;
using DataAccess.Repositories;
using Shared.Logger;
using log4net;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using CmcSfDemoServices.Providers;
using Shared.Commands;
using Shared.Handlers;

[assembly: OwinStartup(typeof(Unit_Tests.Helpers.Startup))]
namespace Unit_Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {

        public void Configuration(IAppBuilder appBuilder)
        {

            //HttpConfiguration httpConfiguration = new HttpConfiguration
            //{
            //    DependencyResolver = new StructureMapWebApiDependencyResolver(container)
            //};

            HttpConfiguration httpConfiguration = new HttpConfiguration
            {

            };
            //Log1(appBuilder);
            appBuilder.Use<GlobalExceptionMiddlewareUnitTest>();

            object[] args = new object[2];
            args[0] = new HttpLoggerRepository(new ConnectionFactory());
            args[1] = Convert.ToBoolean(ConfigurationManager.AppSettings["HttpLoggerEnabled"]);

            appBuilder.Use<OwinMiddlewareLogger>(appBuilder, args);
            //Must Configure OAth prior to WebApiConfig
            ConfigureOAuth(appBuilder);
            Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);

        }

        [ExcludeFromCodeCoverage]
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //	name: "AllCustomers",
            //	routeTemplate: "customers",
            //	defaults: new { controller = "Customers", action = "Get"}
            //	);

            // Web API routes
            config.MapHttpAttributeRoutes();
            //Only allow Https, not Http 
            config.Filters.Add(new HttpsValidator());
            //Following didn't log everything that I needed See OwinMiddlewareLogger for replacement
            //config.Filters.Add(new LogResponseBodyInterceptorAttribute());
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionLogger());
        }

        public static void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //If you are running in IIS Express for OWIN or OAUTH Development/Testing using Http uncomment the following line
                //TO debug OWIN change to IIS Express
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ADAuthorizationServerProvider(new SecurityRepository(new ConnectionFactory(), new LoggingCommandHandlerDecorator<LogCommand>()))
            };

            //OAuthServerOptions.Provider.
            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
        [ExcludeFromCodeCoverage]
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

        //public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }
        //public static void End()
        //{
        //    StructureMapDependencyScope.Dispose();
        //}

        //public void Configuration(IAppBuilder app)
        //{
        //    var moqConnectionFactory = new Mock<IConnectionFactory>();
        //    var moqHttpLoggerRepo = new HttpLoggerRepository(moqConnectionFactory.Object);
        //    //appBuilder.Use<OwinMiddlewareLogger>(appBuilder, args);

        //    object[] args = new object[2];
        //    args[0] = moqHttpLoggerRepo;
        //    args[1] = false;

        //    app.Use<OwinMiddlewareLogger>(app, args);

        //    HttpConfiguration config = new HttpConfiguration();

        //    Registry registry = new Registry();

        //    var connectionFactory = new ConnectionFactory();
        //    var ermsRepo = new ErmsUserRepository(connectionFactory);
        //    var log = AppLogger.Initialize();
        //    LoggingCommandHandlerDecorator<LogCommand> loggingCommandHandlerDecorator = new LoggingCommandHandlerDecorator<LogCommand>();
        //    var ermsService = new BusinessLogic.Services.ErmsUserService(ermsRepo, log, loggingCommandHandlerDecorator);
        //    // Container cntnr = new Container(new ErmsUserController(ermsService, loggingCommandHandlerDecorator, log));

        //    IContainer container = IoC.Initialize();
        //    Debug.WriteLine(container.WhatDoIHave());
        //    container.AssertConfigurationIsValid();


        //    //            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);

        //    HttpConfiguration httpConfiguration = new HttpConfiguration
        //    {
        //        DependencyResolver = new StructureMapWebApiDependencyResolver(container)
        //    };


        //    app.Properties.Add("HttpContext.Current.Timestamp", DateTime.Now);
        //    //config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());
        //    config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionLogger());
        //    config.MapHttpAttributeRoutes();
        //    app.UseWebApi(config);
        //}
    }

    [ExcludeFromCodeCoverage]
    public class GlobalExceptionMiddlewareUnitTest : Microsoft.Owin.OwinMiddleware
    {
        private readonly static ILog _loggingInstance = AppLogger.Initialize();

        public GlobalExceptionMiddlewareUnitTest(Microsoft.Owin.OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception("Exception CaughtByMiddleWare", ex);
                throw;
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class TestWebApiResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(ApiController).Assembly };
            //return new List<Assembly> { typeof(CmcSfRestServices.Controllers.ErmsUserController).Assembly };
        }

    }
}
