using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.HttpClient.Interfaces
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage requestMessage,
            CancellationToken cancellationToken,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> PostAsync(
           string body,
           string url,
           string contentType,
           List<KeyValuePair<string, string>> headers = null,
           int timeoutMili = 30000,
           bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> PostJsonAsync(
               string contentAsJson,
               string url,
               List<KeyValuePair<string, string>> headers = null,
               int timeoutMili = 30000,
               bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> GetAsync(
            string url,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> PutAsync(
            string body,
            string url,
            string contentType,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> DeleteAsync(
            string body,
            string url,
            string contentType,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false);

        Task<HttpResponseMessage> UploadFile(
            string url,
            byte[] fileContent,
            string fileName,
            List<KeyValuePair<string, string>> headers = null,
            int timeoutMili = 30000,
            bool ignoreCertificateValidation = false);
    }
}
