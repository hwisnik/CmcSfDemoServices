namespace Unit_Tests.DependencyResolution
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using StructureMap.Web.Pipeline;
    using Unit_Tests.App_Start;

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
            context.EndRequest += (sender, e) => {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}
