using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Filter;

namespace Cynet.Infra.Helpers.Log4Net.Filter
{
    public abstract class CustomFilter : IFilter
    {
        public abstract IFilter Filter { get; }

        public abstract FilterDecision Decide(LoggingEvent loggingEvent);

        public abstract IFilter Next { get; set; }

        public abstract void ActivateOptions();
    }
}
