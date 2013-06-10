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
    public sealed class JenkinsDataService : IJenkinsDataService
    {
        private static readonly JenkinsDataService instance = new JenkinsDataService();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        /// <summary>
        /// Initializes the <see cref="JenkinsDataService"/> class.
        /// </summary>
        static JenkinsDataService()
        {
        }

        private JenkinsDataService()
        {
        }

        public static JenkinsDataService Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Requests the async.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component">The component.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey,
                                             params object[] parameters) where T : JenkinsModel, new()
        {
            HttpRequestMessage request = ComposeMessage(baseUrl + component.Url(parameters), userName, apiKey);
            Task<HttpResponseMessage> task = SendMessage(request);
            Task<string> readTask = (await task).Content.ReadAsStringAsync();
            return await readTask.ContinueWith(task1 => GetObject<T>(XElement.Parse(task1.Result)));
        }

        private static HttpRequestMessage ComposeMessage(string url, string userName, string apiKey,
                                                         bool authenticated = true)
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
            string encodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + apiKey));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedKey);
        }

        private static T GetObject<T>(XElement xml) where T : JenkinsModel, new()
        {
            return JenkinsModel.FromXml<T>(xml);
        }
    }
}