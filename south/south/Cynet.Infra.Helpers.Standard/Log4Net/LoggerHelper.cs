using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynet.Infra.Helpers.Log4Net.Filter;
using log4net;
using log4net.Core;
using log4net.Filter;

namespace Cynet.Infra.Helpers.Log4Net
{
    public class LoggerHelper
    {
        public static ILog GetLogger(Type type)
        {
            ILog s_Logger = LogManager.GetLogger(type);
            log4net.ThreadContext.Properties["requestId"] = System.Guid.NewGuid().ToString();
            return s_Logger;
        }
    }
}
