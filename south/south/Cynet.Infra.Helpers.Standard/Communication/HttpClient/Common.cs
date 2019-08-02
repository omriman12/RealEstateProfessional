using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Communication.HttpClient
{
    public static class Common
    {
        static public List<KeyValuePair<string, string>> GetDefaultRequestHeaders(int clinetId, string userAgent)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("nocache", clinetId.ToString()),
                new KeyValuePair<string, string>("user-agent", userAgent)
            };
        }
    }
}
