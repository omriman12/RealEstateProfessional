using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.SSDEEP.Interfaces
{
    public interface ISsdeepComparer
    {
        int Compare(string ssdeep1, string ssdeep2);
        int CompareMultiple(string ssdeep, string[] ssdeeps);
        Tuple<int, string> CompareMultiple(string ssdeep, string[] ssdeeps, int? threshold = null);

    }
}
