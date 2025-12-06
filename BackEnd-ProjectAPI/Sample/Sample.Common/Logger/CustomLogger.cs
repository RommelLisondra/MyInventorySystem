using log4net;

namespace Sample.Common.Logger
{
    public class CustomLogger : ILogCentral
    {
        private ILog log4netLogger;

        public CustomLogger(ILog logger)
        {
            log4netLogger = logger;
        }

        public void Debug(object message, Exception exception)
        {
            log4netLogger.Debug(message, exception);
        }

        public void Debug(object message)
        {
            log4netLogger.Debug(message);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.DebugFormat(format, arg0, arg1, arg2);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            log4netLogger.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0)
        {
            log4netLogger.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            log4netLogger.DebugFormat(format, args);
        }

        public void Error(object message, Exception exception)
        {
            log4netLogger.Error(message, exception);
        }

        public void Error(object message)
        {
            log4netLogger.Error(message);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            log4netLogger.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0)
        {
            log4netLogger.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            log4netLogger.ErrorFormat(format, args);
        }

        public void Fatal(object message, Exception exception)
        {
            log4netLogger.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            log4netLogger.Fatal(message);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.FatalFormat(format, arg0, arg1, arg2);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            log4netLogger.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0)
        {
            log4netLogger.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            log4netLogger.FatalFormat(format, args);
        }

        public void Info(object message, Exception exception)
        {
            log4netLogger.Info(message, exception);
        }

        public void Info(object message)
        {
            log4netLogger.Info(message);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.InfoFormat(format, arg0, arg1, arg2);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            log4netLogger.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0)
        {
            log4netLogger.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            log4netLogger.InfoFormat(format, args);
        }

        public bool IsDebugEnabled
        {
            get { return log4netLogger.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return log4netLogger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return log4netLogger.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log4netLogger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log4netLogger.IsWarnEnabled; }
        }

        public void Warn(object message, Exception exception)
        {
            log4netLogger.Warn(message, exception);
        }

        public void Warn(object message)
        {
            log4netLogger.Warn(message);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            log4netLogger.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            log4netLogger.WarnFormat(format, arg0, arg1, arg2);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            log4netLogger.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0)
        {
            log4netLogger.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            log4netLogger.WarnFormat(format, args);
        }

        public log4net.Core.ILogger Logger
        {
            get { return log4netLogger.Logger; }
        }
    }
}
