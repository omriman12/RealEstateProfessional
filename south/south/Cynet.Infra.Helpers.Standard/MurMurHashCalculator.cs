using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Murmur;

namespace Cynet.Infra.Helpers
{
    public static class MurMurHashCalculator
    {
        public static byte[] CalceHash128(string str)
        {
            byte[] retVal = null;

            if (String.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("input can't be null or empty for murmur hash computation");
            }

            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] bytes = unicodeEncoding.GetBytes(str);

            HashAlgorithm murmur128 = MurmurHash.Create128(managed: false);
            retVal = murmur128.ComputeHash(bytes);


            return retVal;
        }

    }
}
