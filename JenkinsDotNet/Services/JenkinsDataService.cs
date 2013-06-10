using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JenkinsDotNet.Interfaces;
using JenkinsDotNet.Model;

namespace JenkinsDotNet.Services
{
    public sealed class JenkinsDataService :IJenkinsDataService
    {
        private static readonly JenkinsDataService instance = new JenkinsDataService();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static JenkinsDataService()
        {
        }

        private JenkinsDataService()
        {
        }

        public static JenkinsDataService Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey, params object[] parameters) where T : JenkinsModel, new()
        {
            var request = ComposeMessage(baseUrl + component.Url(parameters), userName, apiKey);
            var task = SendMessage(request);
            var readTask = (await task).Content.ReadAsStringAsync();
            return await readTask.ContinueWith(task1 => GetObject<T>(XElement.Parse(task1.Result)));
        }

        private static HttpRequestMessage ComposeMessage(string url, string userName, string apiKey, bool authenticated = true)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (authenticated) AddAuthenticationHeader(request, userName, apiKey);
            return request;
        }

        private static async Task<HttpResponseMessage> SendMessage(HttpRequestMessage request)
        {
            HttpMessageHandler handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            return await httpClient.SendAsync(request);
        }

        private static void AddAuthenticationHeader(HttpRequestMessage request, string userName, string apiKey)
        {
            var encodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + apiKey));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);
        }

        private static T GetObject<T>(XElement xml) where T : JenkinsModel, new()
        {
            return JenkinsModel.FromXml<T>(xml);
        }
    }
}
