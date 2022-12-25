using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Reflection;

namespace BenfordSet.Common
{
    internal static class Log
    {
        internal static ILog Logger { get; } = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static string LogFilePath { get; private set; }

        static Log()
        {
            InitializeLogger("Benford-App");
        }

        internal static void InitializeLogger(string appname)
        {
            LogFilePath = GetLogFilePath(appname);
            var entryAssembly = Assembly.GetEntryAssembly();
            var hierarchy = (Hierarchy)LogManager.GetRepository(entryAssembly);

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
            };

            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = false,
                File = LogFilePath,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };

            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            var memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }

        #region Methods

        internal static void Error(object message)
        {
            Log.Error(message);
        }

        internal static void Debug(object message)
        {
            Log.Debug(message);
        }

        internal static void Info(object message)
        {
            if(message == null)
            {
                message = string.Empty;
            }
            Log.Info(message);
            System.Diagnostics.Trace.WriteLine(message);
        }

        internal static void Info(object source, object message)
        {
            Log.Info(message);
        }

        private static string GetLogFilePath(string appName)
        {
            var folder = @"C:\Users\Lenovo\Desktop";
            var path = Path.Combine(folder,$"{appName}.log");
            return path;
        }

        public static void WriteException(Exception ex)
            => Logger.Info(ex.ToString());

        #endregion
    }
}
