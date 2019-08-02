using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.IOUtils.Interfaces
{
    public interface I_IISUtils
    {
        void RestartISSServer(int timeoutMS);
        void RestartISSWeb(string websiteName, int timeoutMS);
    }
}
