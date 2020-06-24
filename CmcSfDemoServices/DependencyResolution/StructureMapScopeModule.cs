#pragma warning disable 1591
namespace CmcSfDemoServices.DependencyResolution
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    using CmcSfDemoServices.App_Start;
    using StructureMap.Web.Pipeline;

    [ExcludeFromCodeCoverage]

    public class StructureMapScopeModule : IHttpModule
    {

        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}
