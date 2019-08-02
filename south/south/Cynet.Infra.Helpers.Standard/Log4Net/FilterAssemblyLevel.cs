using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynet.Infra.Helpers.Log4Net.Filter;
using log4net.Core;
using log4net.Filter;

namespace Cynet.Infra.Helpers.Log4Net
{
    public class FilterAssemblyLevel : FilterSkeleton
    {
        private bool acceptOnMatch;
        private readonly IList<IFilter> filters = new List<IFilter>();

        public override FilterDecision Decide(LoggingEvent loggingEvent)
        {
            try
            {
                if (loggingEvent == null)
                    throw new ArgumentNullException("loggingEvent");
                foreach (IFilter filter in filters)
                {
                    if (filter.Decide(loggingEvent) == FilterDecision.Deny)
                        return FilterDecision.Deny;
                }
            }
            catch
            {

            }
            if (acceptOnMatch)
                return FilterDecision.Accept;
            else
                return FilterDecision.Deny;
        }

        public IFilter Filter
        {
            set
            {
                string keyLevel = value.GetType().GetProperty("Key").GetValue(value).ToString().ToLower();
                string dllPath = value.GetType().GetProperty("StringToMatch").GetValue(value).ToString();
                filters.Add(new FilterDLLLevel(dllPath, GetLogLevel(keyLevel)));
            }
        }

        public bool AcceptOnMatch
        {
            get { return acceptOnMatch; }
            set { acceptOnMatch = value; }
        }

        static LevelMap levelMap = new LevelMap();

        public Level GetLogLevel(string logLevel)
        {
            switch (logLevel)
            {
                case "alert":
                    return Level.Alert;
                case "all":
                    return Level.All;
                case "critical":
                    return Level.Critical;
                case "debug":
                    return Level.Debug;
                case "emergency":
                    return Level.Emergency;
                case "error":
                    return Level.Error;
                case "fatal":
                    return Level.Fatal;
                case "fine":
                    return Level.Fine;
                case "finer":
                    return Level.Finer;
                case "finest":
                    return Level.Finest;
                case "info":
                    return Level.Info;
                case "log4net_debug":
                    return Level.Log4Net_Debug;
                case "notice":
                    return Level.Notice;
                case "off":
                    return Level.Off;
                case "severe":
                    return Level.Severe;
                case "trace":
                    return Level.Trace;
                case "verbose":
                    return Level.Verbose;
                case "warn":
                    return Level.Warn;
                default:
                    return Level.All;
            }
        }
    }
}
