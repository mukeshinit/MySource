using log4net;
using log4net.Core;
using System;
using System.Reflection;

namespace EventfulWebApi.Logging
{
    public static class Logger
    {
        static readonly ILog ILog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(Level level, string message, string tag = "", Exception ex = null)
        {
            var newMessage = AppendException(message, ex);
            ILog.Logger.Log(typeof(LoggerManager), level, newMessage, ex);
        }

        private static string AppendException(string message, Exception ex)
        {
            string newMessage;
            if (message != null && ex != null)
                newMessage = string.Format("{0}; Exception: {1}", message, ex.Message);
            else if (message == null)
                newMessage = ex.Message;
            else
                newMessage = message;

            return newMessage;
        }
    }
}
