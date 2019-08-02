
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers
{
    public static class Constants
    {

        public static byte[] ZEROES_ARRAY = Enumerable.Repeat(0, 32).Select(i => (byte)0).ToArray();
        public static class Milliseconds
        {
            public const int SECOND = 1000;
            public const int MINUTE = 1000 * 60;
            public const int HOUR = 1000 * 60 * 60;
            public const int DAY = 1000 * 60 * 60 * 24;
        }

        public static class Seconds
        {
            public const int MINUTE = 60;
            public const int HOUR = 60 * 60;
            public const int DAY = 24 * 60 * 60;
            public const int WEEK = 7 * 24 * 60 * 60;
            public const int MONTH = 30 * 24 * 60 * 60;
            public const int YEAR = 365 * 24 * 60 * 60;
        }
        public static class Minutes
        {
            public const int HOUR = 60;
            public const int DAY = 24 * 60;
            public const int WEEK = 7 * 24 * 60;
            public const int MONTH = 30 * 24 * 60;
            public const int YEAR = 365 * 24 * 60;
        }
    }
}
