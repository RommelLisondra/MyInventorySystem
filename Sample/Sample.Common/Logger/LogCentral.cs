using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Logger
{
    public static class LogCentral
    {
        public static ILogCentral GetLogger(Type type)
        {
            // If no loggers have been created, load our own.
            if (LogManager.GetCurrentLoggers().Length == 0)
            {
                LoadConfig();
            }

            ILogCentral logger = null;
            try
            {
                ILog log4netLogger = log4net.LogManager.GetLogger(type);
                CustomLogger customLogger = new CustomLogger(log4netLogger);
                logger = customLogger;
            }
            catch (Exception)
            {
                logger = null;
            }
            return logger;
        }

        private static void LoadConfig()
        { 
            //XmlConfigurator.ConfigureAndWatch(new FileInfo(LOG_CONFIG_FILE));
            XmlConfigurator.Configure();
        }    
    }
}
