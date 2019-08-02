using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.FoldersUtils
{
    public class FoldersUtils
    {
        public static void AddFoldersToAnonymouslyAccess(params string[] folderName)
        {
            //Network access: Shares that can be accessed anonymously
            RegistryKey NetworkAccess;
            NetworkAccess = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", true);
            if (NetworkAccess != null)
            {
                NetworkAccess.SetValue("NullSessionShares", folderName, RegistryValueKind.MultiString);
                NetworkAccess.Close();
            }
        }
    }
}
