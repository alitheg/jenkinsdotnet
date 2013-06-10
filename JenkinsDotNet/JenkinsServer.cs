using System.Threading.Tasks;
using JenkinsDotNet.Interfaces;
using JenkinsDotNet.Model;
using JenkinsDotNet.Services;

namespace JenkinsDotNet
{
    /// <summary>
    /// Jenkins Server representation, includes login information
    /// </summary>
    public class JenkinsServer
    {
        private readonly IJenkinsDataService _jenkinsDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsServer"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="apikey">The apikey.</param>
        /// <param name="name">The name.</param>
        public JenkinsServer(string url, string username, string apikey, string name = "Jenkins Server")
        {
            Url = url;
            UserName = username;
            ApiKey = apikey;
            Name = name;
            _jenkinsDataService = JenkinsDataService.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsServer"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="apikey">The apikey.</param>
        /// <param name="name">The name.</param>
        public JenkinsServer(IJenkinsDataService dataService, string url, string username, string apikey,
                             string name = "Jenkins Server")
        {
            Url = url;
            UserName = username;
            ApiKey = apikey;
            Name = name;
            _jenkinsDataService = dataService;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets the node details.
        /// </summary>
        /// <returns></returns>
        public Node GetNodeDetails()
        {
            Task<Node> returnVal = _jenkinsDataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
            returnVal.Wait();
            return returnVal.Result;
        }

        /// <summary>
        /// Gets the node details async.
        /// </summary>
        /// <returns></returns>
        public async Task<Node> GetNodeDetailsAsync()
        {
            return await _jenkinsDataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
        }

        /// <summary>
        /// Gets the job details.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <returns></returns>
        public Job GetJobDetails(string jobName)
        {
            Task<Job> returnVal = _jenkinsDataService.RequestAsync<Job>(URL.Job, Url, UserName, ApiKey, jobName);
            returnVal.Wait();
            return returnVal.Result;
        }

        /// <summary>
        /// Gets the job details async.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <returns></returns>
        public async Task<Job> GetJobDetailsAsync(string jobName)
        {
            return await _jenkinsDataService.RequestAsync<Job>(URL.Job, Url, UserName, ApiKey, jobName);
        }
    }
}