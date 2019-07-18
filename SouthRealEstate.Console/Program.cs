using SouthRealEstate.Helpers.Security;
using System;

namespace SouthRealEstate.Console
{
    class Program
    {
        private const string ENCRYPTION_TOKEN = "cy" + "1n" + "33" + "e7t";
        static void Main(string[] args)
        {
            string conStr = "FDmOLsve0HFMuZp3vmoCRJstExGz7xFSQVvyTxdIkbDWmtPBy/obfImvFT41snijbVqFFx5TUfMDQyWnfPHQVuM3onKcR84axk1331SvckRewKgrgSDFdBYxAaHHEJb8GE/E5Rw2JlFkkVJMClM93MerqSFW+l/1Tqp/lVWc9kZZoS6BNOIOUEUIvsKxln4Qh4eNIed+uumn6N9OC+OzoLWGtaUlvKpkUOYytsxs7yVtS1MRJLQGZOWpU5sjLgxJPvUaDNsQ1Cf6j6xHj2+wcQ==";
            IEncryptionService aesEncryptionService = new AesEncryptionService(ENCRYPTION_TOKEN);

            string decodedConStr;
            aesEncryptionService.Decrypt(conStr, out string hh);
            hh = "server=mysql6002.site4now.net;port=3306;user id=a4b20a_reales; password=1qaz2wsx; database=db_a4b20a_reales; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720";
            aesEncryptionService.Encrypt(hh, out string hh2);

         


        }
    }
}
