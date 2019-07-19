
using SouthRealEstate.DAL;
using SouthRealEstate.Helpers.Security;
using System;

namespace SouthRealEstate.Console
{
    class Program
    {
        private const string ENCRYPTION_TOKEN = "cy" + "1n" + "33" + "e7t";
        static void Main(string[] args)
        {
            string conStr = "KutIpdFzSUJh1CBUdfhQwf+vLURzZxE1R3IZBg07wEQq0jd4qYfwDoI6Hy8elNboAE9iB56ZGQQorB6MAnxyk+5YbOeyM8rJhdDZCE9xQWVv7QENqcdcUIHjjwZSRZW6OIn12C5UWieorgPSa81WWBB0hRLTZ6nqoRtjLXqIeIaKl/FgYCRoQBHE1ae9rtEW/ytdT/UqOTvD0GRPR9TFTyHaES+Yak9zvINpCGkg2gx8W1mEQ6incdjBorU4widW1OKLtGmk93VdZkvf3nNdNg==";
            IEncryptionService aesEncryptionService = new AesEncryptionService(ENCRYPTION_TOKEN);

            string decodedConStr;
            aesEncryptionService.Decrypt(conStr, out decodedConStr);
            //decodedConStr = "server=mysql6002.site4now.net;port=3306;user id=a4b20a_reales; password=1qaz2wsx; database=db_a4b20a_reales; pooling=true; CharSet=utf8; Allow User Variables=True; Convert Zero Datetime=True; default command timeout=720;";
            //aesEncryptionService.Encrypt(decodedConStr, out string hh2);

            try
            {
                var realEstateDbServices = new RealEstateDbServices(decodedConStr);
                var res = realEstateDbServices.GetAllResidentalPropertiesAsync().Result;

                //MySqlConnection connection = new MySqlConnection(decodedConStr);
                //MySqlCommand command = new MySqlCommand();

                //connection.Open();
                //string selectQuery = "SELECT id FROM db_a4b20a_reales.properties_residental;";
                //command.Connection = connection;
                //command.CommandText = selectQuery;
                //var PersonName = command.ExecuteScalar();
           

            }
            catch (Exception)
            {

            }


        }
    }
}
