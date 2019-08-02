using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynet.Infra.Helpers.SSDEEP.Interfaces;

namespace Cynet.Infra.Helpers.SSDEEP
{
    public class SsdeepUtils : ISsdeepUtils
    {
        private const char SSDEEP__PARTS_DIVIDER = ':';
        private const int NUMBER_OF_SSDEEP_PARTS = 3;
        public int ExtractChunkSize(string ssdeep)
        {
            int retVal;
            if (String.IsNullOrEmpty(ssdeep))
            {
                throw new ArgumentException("ssdeep can't be null or empty", "ssdeep");
            }

            try
            {
                var ssdeepParts = ssdeep.Split(SSDEEP__PARTS_DIVIDER);
                if (ssdeepParts.Count() != NUMBER_OF_SSDEEP_PARTS)
                {
                    throw new FormatException(String.Format("invalid ssdeep format:{0}", ssdeep));
                }

                retVal = int.Parse(ssdeepParts[0]);
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;

        }
    }
}
