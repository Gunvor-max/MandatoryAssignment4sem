using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLib.Logging
{
    public class LogIt
    {
        #region Instance Fields
        private static readonly Lazy<LogIt> instance = new Lazy<LogIt>(() => new LogIt(defaultLogFilePath));
        private TraceSource tc;
        private static string defaultLogFilePath = "log.txt";
        #endregion

        #region Static Property
        public static LogIt Instance => instance.Value;
        #endregion

        #region Constructor
        private LogIt(string logFilePath)
        {
            tc = new TraceSource("Log");
            tc.Switch = new SourceSwitch("SourceSwitchLog", SourceLevels.All.ToString());
            tc.Listeners.Add(new TextWriterTraceListener(logFilePath ?? defaultLogFilePath));
            Trace.AutoFlush = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method for initializing the LogIt class with a self chosen logpath
        /// </summary>
        /// <param name="logFilePath">A self chosen logpath</param>
        /// <exception cref="InvalidOperationException">Throws an exception if method is called after the class have been initialized</exception>
        public static void Configure(string logFilePath)
        {
            if (instance.IsValueCreated)
            {
                throw new InvalidOperationException("LogIt has already been initialized. Configure must be called before the first use of the Instance.");
            }
            defaultLogFilePath = logFilePath;
        }

        /// <summary>
        /// Method for logging an event
        /// </summary>
        /// <param name="eventType">The type/catagory of event etc. information or critical</param>
        /// <param name="logInfo">The info that needs to be logged</param>
        public void LogEvent(TraceEventType eventType, string logInfo)
        {
            tc.TraceEvent(eventType, 1000, logInfo);
        }


        /// <summary>
        /// Method for adding a new TraceListener
        /// </summary>
        /// <param name="listener">The TraceListener</param>
        public void AddListener(TraceListener listener)
        {
            tc.Listeners.Add(listener);
        }

        /// <summary>
        /// Method for removing an existing TraceListener
        /// </summary>
        /// <param name="listenerName">The TraceListener</param>
        public void RemoveListener(string listenerName)
        {
            var listener = tc.Listeners[listenerName];
            if (listener != null)
            {
                tc.Listeners.Remove(listener);
            }
        }
        #endregion
    }


}
