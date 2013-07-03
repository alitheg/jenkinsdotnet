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
    /// <summary>
    /// Data service for connecting to Jenkins.
    /// Provides only parameterised (generic) type access.
    /// Use <see cref="JenkinsServer"/> class for type-specific methods.
    /// </summary>
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
        /// Requests data for a given model type from the Jenkins server
        /// </summary>
        /// <typeparam name="T">JenkinsModel object required</typeparam>
        /// <param name="component">The URL of the object required</param>
        /// <param name="baseUrl">The base URL of the server</param>
        /// <param name="userName">Name of the user</param>
        /// <param name="apiKey">The user's API key</param>
        /// <param name="parameters">Any parameters required for the specified URL</param>
        /// <returns>Task representing the retrieval of requested data</returns>
        public async Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey,
                                             params object[] parameters) where T : JenkinsModel<T>, new()
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

        private static T GetObject<T>(XElement xml) where T : JenkinsModel<T>, new()
        {
            return JenkinsModel<T>.FromXml(xml);
        }
    }
}