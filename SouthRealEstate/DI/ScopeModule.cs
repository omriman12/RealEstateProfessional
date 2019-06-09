using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SouthRealEstate.DAL;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.Helpers.Security;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Logic;

namespace ClientDataSyncAPI.WebAPI.Logic.DI
{
    public class ScopeModule
    {
        private const string ENCRYPTION_TOKEN = "cy" + "1n" + "33" + "e7t";

        const string APP_SETTING_FILE = "appsettings.json";
        const string MYSQL_DB_CONN_STRING_CONFIG_KEY = "MySQLConnString";


        public static void Load(IServiceCollection services)
        {
            IEncryptionService aesEncryptionService = new AesEncryptionService(ENCRYPTION_TOKEN);
            services.AddSingleton<IEncryptionService>(new AesEncryptionService(ENCRYPTION_TOKEN));

            //// DAL
            var builder = new ConfigurationBuilder().AddJsonFile(APP_SETTING_FILE);
            var configuration = builder.Build();
            string encryptedConnStr = configuration.GetConnectionString(MYSQL_DB_CONN_STRING_CONFIG_KEY);
            string decodedConStr;
            aesEncryptionService.Decrypt(encryptedConnStr, out decodedConStr);

            services.AddTransient<IRealEstateDbServices>(s => new RealEstateDbServices(decodedConStr));
            services.AddTransient<IPropertiesLogic, PropertiesLogic>();
        }
    }
}
