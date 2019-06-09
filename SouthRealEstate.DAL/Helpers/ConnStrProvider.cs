using Microsoft.Extensions.Configuration;
using SouthRealEstate.Helpers.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace SouthRealEstate.DAL
{
    class ConnStrProvider
    {
        private static readonly string s_Token = "cy" + "1n" + "33" + "e7t";
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(ConnStrProvider));
        private static IEncryptionService s_EncryptionServices = null;

        /* constnts */
        private const string APP_SETTINGS_FILE = @"appsettings.json";

        /// <summary>
        /// needed for the designer to create the db first migrations
        /// </summary>
        static ConnStrProvider()
        {
            s_EncryptionServices = new AesEncryptionService(s_Token);
        }
        public static string GetConnectionStr()
        {
            string retVal = null;

            try
            {
                var builder = new ConfigurationBuilder().AddJsonFile(APP_SETTINGS_FILE);
                var configuration = builder.Build();
                var encryptedConnStr = configuration["MySQLConnString"];
                string decoded = null;
                s_EncryptionServices.Decrypt(encryptedConnStr, out decoded);
                retVal = decoded;
            }
            catch (Exception ex)
            {
                s_Logger.Error("failed to read connection string", ex);
                throw;
            }

            return retVal;
        }
    }
}
