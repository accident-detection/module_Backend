using Loggly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public static class LogglyLog
    {
        private static ILogglyClient _loggly;

        static LogglyLog()
        {
            _loggly = new LogglyClient();
        }

        public static async Task Log(string msg)
        {
            var logEvent = new LogglyEvent();

            logEvent.Data.Add("message", msg);
            await _loggly.Log(logEvent);
        }

        public static async Task Log(object msg)
        {
            var logEvent = new LogglyEvent();

            logEvent.Data.Add("context", msg);
            await _loggly.Log(logEvent);
        }
    }
}
