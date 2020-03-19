using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;

namespace Shared.Logger
{

    /// <summary>
    /// Log4net wrapper.  Invoke Initialize method to get a loggingInstance. Use or inject this instance 
    /// into each method / class to log exceptions or debug messages to a database.  
    /// </summary>
    public static class AppLogger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AppLogger));
        internal static bool isInitialized;
        public static Guid CorrelationId { set => LogicalThreadContext.Properties["CorrelationId"] = value; }
        public static string ApplicationUserName { set => LogicalThreadContext.Properties["AppUserName"] = value; }
        public static string ApplicationName { set => LogicalThreadContext.Properties["ApplicationName"] = value; }

        /// <summary>
        /// Initialize Method setups up Log4net properties / appenders.  Note that the client must call this method before logging
        /// </summary>
        /// <returns></returns>
        public static ILog Initialize()
        {
            if (isInitialized) { return Log; }

            //Exposing ApplicationName property for setting by client 
            //Given that WebApi apps show the Assembly.FullName as Anonymously Hosted Dynamic Methods
            LogicalThreadContext.Properties["ApplicationName"] = Assembly.GetCallingAssembly().FullName;

            //Debated whether to use LogicalThreadContext.Properties["CorrelationId"] or custom GlobalContext property GlobalContext.Properties["CorrelationId"]
            //Decided to switch to threadsafe LogicalThreadContext.
            CorrelationId = Guid.NewGuid();

            //Calling ConfigureAnd Watch with filename derived by using AppDomain.CurrentDomain.BaseDirectory means 
            //that you don't need a reference to the log4net.config in your app.config or web.cong.
            //Location: bin/Configuration(ex: Debug)/Framework (ex:net461/EntloggerConfig/log4net.config

            //FileInfo log4netFile = ConfigFileFinder.FindLog4NetConfigFile(AppDomain.CurrentDomain.BaseDirectory, "EntloggerConfig", "log4net.config");

            var log4NetFile = new FileInfo(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));

            XmlConfigurator.ConfigureAndWatch(Log.Logger.Repository, log4NetFile);

            //XmlConfigurator.Configure();        //uses log4net.config    

            isInitialized = true;
            return Log;
        }

        /// <summary>
        /// Logs Exception, no need to supply optional CallerXXX parameters, (memberName etc.) See CompilerServices namespace
        /// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callermembernameattribute?view=netframework-4.7.1
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void LogException(
            ILog appLogger,
            Object message,
            Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)

        {
            //If Not initialized, throw exception.  
            if (appLogger == null || !isInitialized)
            {
                throw new NullReferenceException("Applogger not initialized, need to call initialize method one time before calling LogException");
            }

            LogicalThreadContext.Properties["MemberName"] = memberName;
            LogicalThreadContext.Properties["SourceFilePath"] = sourceFilePath;
            LogicalThreadContext.Properties["SourceLineNumber"] = sourceLineNumber;
            appLogger.Error(message, exception);

        }

        /// <summary>
        /// Logs Debug data, no need to supply optional CallerXXX parameters, (memberName etc.) See CompilerServices namespace
        /// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callermembernameattribute?view=netframework-4.7.1
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="message"></param>
        /// <param name="replayId"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public static void LogDebug(
            ILog appLogger,
            Object message,
            int replayId,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)

        {
            //Debug Flag in web.config Set AppLoggerEnabled to true for detailed logging

            if (ConfigurationManager.AppSettings["AppLoggerEnabled"].ToLower() == "false") return;
            //If Not initialized, throw exception.  
            if (appLogger == null || !isInitialized)
            {
                throw new NullReferenceException("Applogger not initialized, need to call initialize method one time before calling LogException");
            }
            LogicalThreadContext.Properties["ReplayId"] = replayId;
            LogicalThreadContext.Properties["MemberName"] = memberName;
            LogicalThreadContext.Properties["SourceFilePath"] = sourceFilePath;
            LogicalThreadContext.Properties["SourceLineNumber"] = sourceLineNumber;

            if (string.IsNullOrEmpty(memberName) || string.IsNullOrEmpty(sourceFilePath) || sourceLineNumber == 0)
            {
                var callStack = new StackFrame(1, true);

                if (string.IsNullOrEmpty(memberName))
                {
                      LogicalThreadContext.Properties["MemberName"] = callStack.GetMethod().DeclaringType.AssemblyQualifiedName;
                }
    
                if (string.IsNullOrEmpty(sourceFilePath))
                {
                    LogicalThreadContext.Properties["SourceFilePath"] = callStack.GetFileName();
                }

                if (sourceLineNumber == 0)
                {
                    LogicalThreadContext.Properties["SourceLineNumber"] = callStack.GetFileLineNumber();
                }
            }
            appLogger.Debug(message);
        }

    }

}

