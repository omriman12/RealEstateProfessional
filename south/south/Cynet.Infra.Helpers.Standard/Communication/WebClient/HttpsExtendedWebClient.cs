using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.Communication.WebClient
{
    public class HttpsExtendedWebClient : System.Net.WebClient
    {
        private readonly int m_Timeout;
        private readonly WebProxy m_Proxy;
        private readonly SecurityProtocolType m_SecurityProtocolType;


        public HttpsExtendedWebClient(WebProxy proxy,
            SecurityProtocolType securityProtocolType = SecurityProtocolType.Tls12,
            int timeoutMS = 100 * Constants.Milliseconds.SECOND)
        {
            m_Proxy = proxy;
            m_SecurityProtocolType = securityProtocolType;
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

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = m_SecurityProtocolType;

            byte[] response = await UploadFileTaskAsync(address, fileName);
            return Encoding.Default.GetString(response);
        }
    }
}
