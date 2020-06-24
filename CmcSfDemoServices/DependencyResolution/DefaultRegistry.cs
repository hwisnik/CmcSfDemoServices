// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Shared.Handlers;

namespace CmcSfDemoServices.DependencyResolution
{
    using Shared.Logger;
    using log4net;
    using StructureMap;
    using System.Web;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
    [ExcludeFromCodeCoverage]
    public class DefaultRegistry : Registry
    {

        #region Constructors and Destructors
        //Add new projects here
        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    //Add new projects here using AssemblyName
                    scan.Assembly("BusinessLogic");
                    scan.Assembly("DataAccess");
                    scan.Assembly("Shared");
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            //For<IExample>().Use<Example>();

            //For(typeof(ICommandHandler<>)).Use(new GenericCommandHandlerInstanceFactory());
            //For(typeof(IGenericRepository<>)).Use(new GenericRepositoryInstanceFactory());
            For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));

            //Registers the Static Log4Net instance after it is initialized
            For<ILog>().AlwaysUnique().Use(ctx => AppLogger.Initialize());
         }

        #endregion
    }




 
}
