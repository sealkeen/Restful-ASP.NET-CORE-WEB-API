using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using EPAM.CSCourse2021Q3.M10_IO.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using EPAM.CSCourse2021Q3.M10_Domain;

namespace EPAM.CSCourse2021Q3.M10_Logging
{
    public class StaticLog : Microsoft.Extensions.Logging.ILogger
    {
        public static readonly NLog.Logger StaticNLog = NLog.LogManager.GetCurrentClassLogger();
        public static string _logfileName = "log.txt";
        public StaticLog(string currentDirectoryPath)
        {
            if (!currentDirectoryPath.DirectoryExists())
                StaticLog._logfileName = $"{Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar}log.txt";
            else
                StaticLog._logfileName = $"{currentDirectoryPath + System.IO.Path.DirectorySeparatorChar}log.txt";
            StaticLog.InitializeLogger();
        }
        ~StaticLog()
        {   
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }

        public void LogError(string error)
        {
            StaticNLog?.Error(error);
        }
        public static void Error(string message)
        {
            StaticNLog?.Error(message);
        }

        public static NLog.Config.LoggingConfiguration InitializeLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = _logfileName };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);
            NLog.LogManager.Configuration = config;
            return config;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception = null, Func<TState, Exception, string> formatter = null)
        {
            StaticNLog.Error(state.ToString());
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
