using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Filter;

namespace Cynet.Infra.Helpers.Log4Net.Filter
{
    public class FilterDLLLevel : CustomFilter
    {
        private Level s_Level;
        private string s_DllPath;
        public FilterDLLLevel(string dllPath, Level level)
        {
            s_DllPath = dllPath;
            s_Level = level;
        }
        public override IFilter Filter
        {
            get { return this; }
        }

        public override FilterDecision Decide(LoggingEvent loggingEvent)
        {
            FilterDecision retVal = FilterDecision.Neutral;
            if ((loggingEvent.Level < s_Level) && ((loggingEvent.LocationInformation.ClassName.IndexOf(s_DllPath)) > -1))
                retVal = FilterDecision.Deny;
            return retVal;
        }

        public override IFilter Next
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void ActivateOptions()
        {
            throw new NotImplementedException();
        }
    }
}
