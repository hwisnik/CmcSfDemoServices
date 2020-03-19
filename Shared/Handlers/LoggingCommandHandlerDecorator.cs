using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using log4net;
using Shared.Commands;
using Shared.Logger;

//using StructureMap.Building.Interception;
//using StructureMap.Graph;


namespace Shared.Handlers
{
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        public int ReplayId { get; set; }

        public LoggingCommandHandlerDecorator() 
        {
        }

        public void HandleLog(TCommand command,  [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            
            {
            LogCommand logCmd = command as LogCommand;

            //Only logging Debug messages here
            if (logCmd != null && !logCmd.LoggingInstance.Logger.IsEnabledFor(log4net.Core.Level.Debug)) return;

            var adUserName = ((ClaimsIdentity)logCmd.User.Identity).Claims.FirstOrDefault(x => x.Type.ToUpper() == "SFADUSERNAME")?.Value;
            var corrId = ((ClaimsIdentity)logCmd.User.Identity).Claims.FirstOrDefault(x => x.Type.ToUpper() == "SFCORRELATIONID")?.Value;
            Guid.TryParse(corrId, out Guid correlationId);
            LogicalThreadContext.Properties["CorrelationId"] = correlationId;
            LogicalThreadContext.Properties["AppUserName"] = adUserName;
            var assy = Assembly.GetCallingAssembly().FullName;
            assy = assy.Substring(0, assy.IndexOf("Culture=", StringComparison.CurrentCulture) - 3);
            LogicalThreadContext.Properties["ApplicationName"] = assy;

            //Need to log optional params (memberName, sourceFilePath, sourceLineNumber) because the Log would always show this class which is doing the logging
            //in the database,  instead of the class that is calling the Logging handler which is what we really want to see
            AppLogger.LogDebug(logCmd.LoggingInstance, logCmd.LogMessage, ReplayId, memberName,sourceFilePath,sourceLineNumber);

        }

        public void Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}



