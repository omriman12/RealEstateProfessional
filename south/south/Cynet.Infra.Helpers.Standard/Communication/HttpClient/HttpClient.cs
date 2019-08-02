using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cynet.Infra.Helpers.HttpClient.Interfaces;

namespace Cynet.Infra.Helpers.HttpClient
{
    public class HttpClient : IHttpClient
    {
        /* static */
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(HttpClient));

        private readonly WebProxy m_Proxy;
        public HttpClient(WebProxy proxy = null)
        {
            m_Proxy = proxy;
        }

        public HttpClient()
        {
        }
        
        public async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage,
            CancellationToken cancellationToken,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (var client = new System.Net.Http.HttpClient(httpClientHandler))
                {
                    //send the request
                    client.Timeout = TimeSpan.FromMilliseconds(timeoutMili);
                    retVal = await client.SendAsync(requestMessage, cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http SEND msg to: {0}", requestMessage.RequestUri), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> PostAsync(
            string body,
            string url,
            string contentType,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = (httpClientHandler != null) ? new System.Net.Http.HttpClient(httpClientHandler) : new System.Net.Http.HttpClient())
                {
                    using (var content = new StringContent(body, Encoding.UTF8, contentType))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                        request.Content = content;

                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        //send the request
                        client.Timeout = TimeSpan.FromMilliseconds(timeoutMili);
                        retVal = await client.SendAsync(request);
                    }
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http POST msg to:{0}", url), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> PostJsonAsync(
            string contentAsJson,
            string url,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = (httpClientHandler != null) ? new System.Net.Http.HttpClient(httpClientHandler) : new System.Net.Http.HttpClient())
                {
                    using (var content = new StringContent(contentAsJson, Encoding.UTF8, "application/json"))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                        request.Content = content;

                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        //send the request
                        client.Timeout = TimeSpan.FromMilliseconds(timeoutMili);
                        retVal = await client.SendAsync(request);
                    }
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http POST msg to:{0}", url), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> GetAsync(
            string url,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = (httpClientHandler != null) ? new System.Net.Http.HttpClient(httpClientHandler) : new System.Net.Http.HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.Add(header.Key, header.Value);
                        }
                    }

                    //send the request
                    var cancelToken = new CancellationTokenSource(timeoutMili);
                    retVal = await client.SendAsync(request, cancelToken.Token);
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http GET msg to:{0}", url), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> PutAsync(
            string body,
            string url,
            string contentType,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false
            )
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = (httpClientHandler != null) ? new System.Net.Http.HttpClient(httpClientHandler) : new System.Net.Http.HttpClient())
                {
                    using (var content = new StringContent(body, Encoding.UTF8, contentType))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
                        request.Content = content;

                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        //send the request
                        client.Timeout = TimeSpan.FromMilliseconds(timeoutMili);
                        retVal = await client.SendAsync(request);
                    }
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http PUT msg to:{0}", url), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string body,
            string url,
            string contentType,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;

            try
            {
                HttpClientHandler httpClientHandler = null;

                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = (httpClientHandler != null) ? new System.Net.Http.HttpClient(httpClientHandler) : new System.Net.Http.HttpClient())
                {
                    using (var content = new StringContent(body, Encoding.UTF8, contentType))
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
                        request.Content = content;

                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        //send the request
                        client.Timeout = TimeSpan.FromMilliseconds(timeoutMili);
                        retVal = await client.SendAsync(request);
                    }
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send http DELETE msg to:{0}", url), ex);
                throw;
            }

            return retVal;
        }

        public async Task<HttpResponseMessage> UploadFile(
            string url,
            byte[] fileContent,
            string fileName,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false)
        {
            HttpResponseMessage retVal = null;
            HttpClientHandler httpClientHandler = null;

            try
            {
                //proxy
                if (m_Proxy != null)
                {
                    httpClientHandler = new HttpClientHandler()
                    {
                        Proxy = m_Proxy
                    };
                }

                //create the request
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                        content.Add(new StreamContent(new MemoryStream(fileContent)), fileName, fileName);
                        request.Content = content;

                        if (headers != null)
                        {
                            foreach (var header in headers)
                            {
                                request.Headers.Add(header.Key, header.Value);
                            }
                        }

                        //send the request
                        var cancelToken = new CancellationTokenSource(timeoutMili);
                        retVal = await client.SendAsync(request, cancelToken.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(String.Format("eror occured during send upload file to:{0}", url), ex);
                throw;
            }

            return retVal;
        }
    }
}
