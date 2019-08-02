using System;
using System.Collections.Generic;
using System.Text;

namespace Cynet.Infra.Helpers.Standard.ExtensionMethods
{
    public static class DateTimeExtensionMethods
    {
        public static string ToMySql(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
