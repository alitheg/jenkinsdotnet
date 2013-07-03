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
        /// <summary>
        /// The _jenkins data service
        /// </summary>
        private readonly IJenkinsDataService _jenkinsDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsServer" /> class.
        /// </summary>
        /// <param name="url">The URL of the server</param>
        /// <param name="username">A valid username</param>
        /// <param name="apikey">The user's API key</param>
        /// <param name="name">Friendly name of this server</param>
        public JenkinsServer(string url, string username, string apikey, string name = "Jenkins Server")
        {
            Url = url;
            UserName = username;
            ApiKey = apikey;
            Name = name;
            _jenkinsDataService = JenkinsDataService.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsServer" /> class.
        /// </summary>
        /// <param name="dataService"><see cref="IJenkinsDataService"/> available to this server</param>
        /// <param name="url">The URL of the server</param>
        /// <param name="username">A valid username</param>
        /// <param name="apikey">The user's API key</param>
        /// <param name="name">Friendly name of this server</param>
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
        /// Gets or sets the URL of the server.
        /// </summary>
        /// <value>The URL</value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the friendly name of this server
        /// </summary>
        /// <value>The name</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the username used to connect to this server
        /// </summary>
        /// <value>The username</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the API key of the configured user.
        /// </summary>
        /// <value>The API key</value>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets details of a node.
        /// </summary>
        /// <returns>Node model</returns>
        public Node GetNodeDetails()
        {
            var returnVal = GetNodeDetailsAsync();
            returnVal.Wait();
            return returnVal.Result;
        }

        /// <summary>
        /// Asynchronously gets the details of a node.
        /// </summary>
        /// <returns>Task to retrieve the node</returns>
        public async Task<Node> GetNodeDetailsAsync()
        {
            return await _jenkinsDataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
        }

        /// <summary>
        /// Gets details of a job.
        /// </summary>
        /// <param name="jobName">Name of the job requested</param>
        /// <returns>The requested job</returns>
        public Job GetJobDetails(string jobName)
        {
            var returnVal = GetJobDetailsAsync(jobName);
            returnVal.Wait();
            return returnVal.Result;
        }

        /// <summary>
        /// Asynchronously gets details of a job
        /// </summary>
        /// <param name="jobName">Name of the job requested</param>
        /// <returns>Task to retrieve the job</returns>
        public async Task<Job> GetJobDetailsAsync(string jobName)
        {
            return await _jenkinsDataService.RequestAsync<Job>(URL.Job, Url, UserName, ApiKey, jobName);
        }

        /// <summary>
        /// Gets details of a build
        /// </summary>
        /// <param name="jobName">Name of the job to get a build from</param>
        /// <param name="buildNumber">The build number requested</param>
        /// <returns>The requested build</returns>
        public Build GetBuildDetails(string jobName, string buildNumber)
        {
            var returnVal = GetBuildDetailsAsync(jobName, buildNumber);
            returnVal.Wait();
            return returnVal.Result;
        }

        /// <summary>
        /// Asynchronously gets details of a build
        /// </summary>
        /// <param name="jobName">Name of the job to get a build from</param>
        /// <param name="buildNumber">The build number requested</param>
        /// <returns>Task to retrieve the requested build</returns>
        public async Task<Build> GetBuildDetailsAsync(string jobName, string buildNumber)
        {
            return
                await
                _jenkinsDataService.RequestAsync<Build>(URL.Build, Url, UserName, ApiKey, jobName, buildNumber)
                                   .ContinueWith(t =>
                                   {
                                       t.Result.Job = new Job { Name = jobName };
                                       return t.Result;
                                   });

        }

        /// <summary>
        /// Gets details of a build for a <see cref="Job"/> object
        /// </summary>
        /// <param name="job">Job to get a build from</param>
        /// <param name="buildNumber">The build number requested</param>
        /// <returns>The requested build</returns>
        public Build GetBuildDetails(Job job, string buildNumber)
        {
            return GetBuildDetails(job.Name, buildNumber);
        }

        /// <summary>
        /// Asynchronously gets details of a build for a <see cref="Job"/> object
        /// </summary>
        /// <param name="job">Job to get a build from</param>
        /// <param name="buildNumber">The build number requested</param>
        /// <returns>Task to retrieve the requested build</returns>
        public async Task<Build> GetBuildDetailsAsync(Job job, string buildNumber)
        {
            return await GetBuildDetailsAsync(job.Name, buildNumber);
        }
    }
}