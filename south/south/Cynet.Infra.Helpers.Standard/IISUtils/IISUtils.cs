using Cynet.Infra.Helpers.IOUtils.Interfaces;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.IOUtils
{
    public class IISUtils : I_IISUtils
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(IOUtils));

        public void RestartISSServer(int timeoutMS)
        {
            var process = new Process
            {
                StartInfo =
                {
                    Verb = "runas",
                    WorkingDirectory = @"C:\Windows\System32\",
                    FileName = @"iisreset.exe"
                }
            };
            process.StartInfo.UseShellExecute = false;
            process.Start();
            bool didSucceed = process.WaitForExit(timeoutMS);
            if (!didSucceed)
            {
                throw new ApplicationException("A timeout occured while trying to restart IIS server");
            }
        }

        public void RestartISSWeb(string websiteName, int timeoutMS)
        {
            var server = new ServerManager();
            var site = server.Sites.FirstOrDefault(s => s.Name == websiteName);
            if (site != null)
            {
                site.Stop();
                if (site.State != ObjectState.Stopped)
                {
                    throw new InvalidOperationException(string.Format("Could not stop website: {0}!", websiteName));
                }
                site.Start();
            }
            else
            {
                throw new InvalidOperationException(string.Format("Could not find website: {0}!", websiteName));
            }
        }

    }
}
