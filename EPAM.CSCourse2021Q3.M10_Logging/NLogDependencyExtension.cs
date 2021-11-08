using EPAM.CSCourse2021Q3.M10_Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using EPAM.CSCourse2021Q3.M10_IO.Extension;
using System;

namespace EPAM.CSCourse2021Q3.M10_Logging
{    
    //    • Ошибки, произошедшие внутри библиотеки должны логироваться. (+)
    //    • Библиотека не должна иметь зависимости на конкретный логгер. (+)
    //    • Реализация логгера и настройки логгера должны определяться потребителем библиотеки. (+)

    public static class NLogInjector
    {
        public static T InjectNLog<T>() where T : class, ILogable => BuildDi<T>().GetRequiredService<T>();

        public static Microsoft.Extensions.Logging.ILogger CreateLogger(string currentDirectoryPath)
        {
            StaticLog staticLog = new StaticLog(currentDirectoryPath);
            return staticLog;
        }
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        }
        public static IServiceProvider BuildDi<T>(IConfiguration config = null) where T : class
        {
            if(config == null)
                config = GetConfiguration();
            return new ServiceCollection()
               .AddTransient<T>() // T is the custom class
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
