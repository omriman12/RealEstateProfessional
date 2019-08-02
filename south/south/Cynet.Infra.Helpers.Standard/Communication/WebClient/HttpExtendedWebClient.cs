using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Communication.WebClient
{
    public class HttpExtendedWebClient : System.Net.WebClient
    {
        private readonly int m_Timeout;
        private readonly WebProxy m_Proxy;


        public HttpExtendedWebClient(WebProxy proxy,
            int timeoutMS = 100 * Constants.Milliseconds.SECOND)
        {
            m_Proxy = proxy;
            m_Timeout = timeoutMS;
        }
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest request = base.GetWebRequest(uri);
            if (request != null)
            {
                request.Timeout = m_Timeout;

                if (m_Proxy != null)
                {
                    request.Proxy = m_Proxy;
                }
            }

            return request;
        }

        public async Task<string> ExtendedUploadFileAsync(string address, string fileName, List<KeyValuePair<string, string>> headers)
        {
            if (headers != null)
            {
                this.Headers.Clear();
                foreach (var header in headers)
                {
                    this.Headers.Add(header.Key, header.Value);
                }
            }

            byte[] response = await UploadFileTaskAsync(address, fileName);
            return Encoding.Default.GetString(response);
        }
    }
}
