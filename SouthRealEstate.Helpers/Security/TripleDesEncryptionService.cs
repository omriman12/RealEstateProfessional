using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SouthRealEstate.Helpers.Security
{
    public class TripleDesEncryptionService : IEncryptionService
    {
        private readonly string m_Key;
        private const int k_KeyLength = 16;

        public TripleDesEncryptionService(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "encryption key can not be null or empty");
            }

            if (key.Length != k_KeyLength)
            {
                throw new ArgumentException("encryption key must be in length: " + k_KeyLength);
            }

            m_Key = key;
        }

        #region IEncryptionService Members

        public void Encrypt(string text, out string cipher)
        {
            if (text == null)
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            var cryptoProvider = new TripleDESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var bytes = ASCIIEncoding.ASCII.GetBytes(m_Key);
            var cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(text);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            cipher = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public void Decrypt(string cipher, out string text)
        {
            if (String.IsNullOrEmpty(cipher))
            {
                throw new ArgumentNullException
                    ("The string which needs to be decrypted can not be null.");
            }
            var cryptoProvider = new TripleDESCryptoServiceProvider();
            var memoryStream = new MemoryStream
                (Convert.FromBase64String(cipher));
            var bytes = ASCIIEncoding.ASCII.GetBytes(m_Key);
            var cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);

            text = reader.ReadToEnd();
        }

        #endregion

    }
}
