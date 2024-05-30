using log4net;
using RtpcrCustomerApp.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RtpcrCustomerApp.Common.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        //private static readonly string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"];
        private static ConcurrentDictionary<string, ILog> Loggers { get; set; } = new ConcurrentDictionary<string, ILog>(StringComparer.InvariantCultureIgnoreCase);

        //static LoggerFactory()
        //{
        //    if (!Directory.Exists(LogDirectory)) Directory.CreateDirectory(LogDirectory);
        //}

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILog GetLogger(Type type)
        {
            if (Loggers.ContainsKey(type.FullName)) return Loggers[type.FullName];
            else
            {
                var fileName = GetLogModuleName(type);
                var logger = GetLoggerInstance(fileName, type);
                Loggers.AddOrUpdate(type.FullName, logger, (key, existingLogger) => { return existingLogger; });
                return logger;
            }

        }

        private string GetLogModuleName(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(ModuleAttribute), true);
            if (attrs != null && attrs.Any())
            {
                return ((ModuleAttribute)attrs.First()).Name;
            }
            else
            {
                return string.Empty;
            }
        }

        private ILog GetLoggerInstance(string moduleName, Type type)
        {
            if (!string.IsNullOrEmpty(moduleName)) return new Logger(LogManager.GetLogger(moduleName).Logger);
            else return new Logger(LogManager.GetLogger(type.FullName).Logger);

            #region check
            //var logfilePath = $"{LogDirectory}\\{fileName}.txt";

            //PatternLayout layout = new PatternLayout("%date %level - %message%newline");
            //LevelMatchFilter filter = new LevelMatchFilter
            //{
            //    LevelToMatch = Level.All
            //};
            //filter.ActivateOptions();

            //string repositoryName = string.Format("{0}_Repository", fileName);
            //ILoggerRepository repository;

            //var repos = LoggerManager.GetAllRepositories();
            //var repoExists = false;

            //foreach (var operationRepo in repos)
            //{
            //    if (repositoryName == operationRepo.Name)
            //    {
            //        repoExists = true;
            //        break;
            //    }
            //}
            //RollingFileAppender appender = new RollingFileAppender();

            //if (!repoExists)
            //{
            //    repository = LoggerManager.CreateRepository(repositoryName);

            //    appender = new RollingFileAppender
            //    {
            //        File =  string.Concat(LogDirectory, string.Format("{0}.log", fileName)),
            //        ImmediateFlush = true,
            //        AppendToFile = true,
            //        RollingStyle = RollingFileAppender.RollingMode.Date,
            //        DatePattern = "-yyyy-MM-dd",
            //        LockingModel = new FileAppender.MinimalLock(),
            //        Name = string.Format("{0}Appender", fileName)
            //    };
            //    appender.AddFilter(filter);
            //    appender.Layout = layout;
            //    appender.ActivateOptions();
            //}
            //else
            //{
            //    repository = LoggerManager.GetRepository(repositoryName);
            //}

            //string loggerName = string.Format("{0}Logger", fileName);
            //BasicConfigurator.Configure(repository, appender);
            //ILog logger = LogManager.GetLogger(repositoryName, loggerName);

            //return logger;

            #endregion
        }

    }
}
