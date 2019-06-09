using SouthRealEstate.Helpers.Security;
using System;

namespace SouthRealEstate.Console
{
    class Program
    {
        private const string ENCRYPTION_TOKEN = "cy" + "1n" + "33" + "e7t";
        static void Main(string[] args)
        {
            string conStr = "server=127.0.0.1;port=3306;user id=user1; password=1qaz2wsx; database=realestate; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720";
            IEncryptionService aesEncryptionService = new AesEncryptionService(ENCRYPTION_TOKEN);

            string decodedConStr;
            aesEncryptionService.Encrypt(conStr, out string hh);



        }
    }
}
