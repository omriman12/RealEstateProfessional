using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Security.Interfaces
{
    public interface IEncryptionService
    {
        void Encrypt(string text, out string cipher);
        void Decrypt(string cipher, out string text);
    }
}
