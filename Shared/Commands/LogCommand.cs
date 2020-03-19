using System.Security.Principal;
using log4net;

namespace Shared.Commands
{
    public class LogCommand
    {
        public IPrincipal User { get; set; }
        public ILog LoggingInstance { get; set; }
        public string LogMessage { get; set; }
        ///// <summary>
        ///// ControllerName property only needs to be set by controllers
        ///// A side effect of setting controller name is that this will set the application name is set too.  Code smell!!  This is due to the fact
        ///// that Assembly.GetCallingAssembly().FullName will return "Anonymously hosted ...." for a controller, so Assembly needs to be determined
        ///// in the LoggingCommandHandler rather than in the controller.  LoggingCommandHandler will clear ControllerName
        ///// </summary>
        //public string ControllerName { get; set; }
        ///// <summary>
        ///// MemberName has priority over ControllerName. 
        ///// </summary>
        //public string MemberName { get; set; }
    }
}