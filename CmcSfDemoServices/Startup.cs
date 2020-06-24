using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using CmcSfDemoServices.App_Start;
using CmcSfDemoServices.DependencyResolution;
using CmcSfDemoServices.Owin;
using DataAccess.Infrastructure;
using DataAccess.Repositories;
using Microsoft.Owin;
using Owin;
using StructureMap;

#pragma warning disable 1591
[assembly: OwinStartup(typeof(CmcSfDemoServices.Startup))]
namespace CmcSfDemoServices
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        // public object SwaggerConfig { get; private set; }

        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        public void Configuration(IAppBuilder appBuilder)
        {

            IContainer container = IoC.Initialize();
            container.AssertConfigurationIsValid();
            Debug.WriteLine(container.WhatDoIHave());
            HttpConfiguration httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new StructureMapWebApiDependencyResolver(container),
            };


            //httpConfiguration.MessageHandlers.Add(new ResponseSizeHandler());

            //Log1(appBuilder);
            appBuilder.Use<GlobalExceptionMiddleware>();

            object[] args = new object[2];
            args[0] = new HttpLoggerRepository(new ConnectionFactory());
            args[1] = Convert.ToBoolean(ConfigurationManager.AppSettings["HttpLoggerEnabled"]);

            appBuilder.Use<OwinMiddlewareLogger>(appBuilder, args);
            //Must Configure OAth prior to WebApiConfig
            OWinConfig.ConfigureOAuth(appBuilder);
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);

            //See https://stackoverflow.com/questions/31840165/swashbuckle-5-cant-find-my-apicontrollers/31886093

            //Only Display Swagger for Dev deployments  (https://localhost/CmcSfDemoServices/swagger/ui/index#/Services or https://erms2dev.cmcenergy.com/CmcSfDemoServices/swagger/ui/index#/Services)
            //If a dev or devdeploy configuration, Register Swagger with httpConfiguration used for Owin  
            if (ConfigurationManager.AppSettings["DeployType"] == "Dev") SwaggerConfig.Register(httpConfiguration);
        }
    }
}
