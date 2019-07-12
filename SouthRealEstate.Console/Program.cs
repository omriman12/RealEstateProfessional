using SouthRealEstate.Helpers.Security;
using System;

namespace SouthRealEstate.Console
{
    class Program
    {
        private const string ENCRYPTION_TOKEN = "cy" + "1n" + "33" + "e7t";
        static void Main(string[] args)
        {
            string conStr = "xyxyzyzy";
            IEncryptionService aesEncryptionService = new AesEncryptionService(ENCRYPTION_TOKEN);

            string decodedConStr;
            aesEncryptionService.Encrypt(conStr, out string hh);



        }
    }
}
