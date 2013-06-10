using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using JenkinsDotNet.Model;
using JenkinsDotNet.Services;

namespace JenkinsDotNet
{
    public class JenkinsServer
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        private readonly JenkinsDataService _jenkinsDataService;

        public JenkinsServer(string url, string username, string apikey, string name = "Jenkins Server")
        {
            Url = url;
            UserName = username;
            ApiKey = apikey;
            Name = name;
            _jenkinsDataService = JenkinsDataService.Instance;
        }

        public Node GetNodeDetails()
        {
            var returnVal = _jenkinsDataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
            returnVal.Wait();
            return returnVal.Result;
        }
        public async Task<Node> GetNodeDetailsAsync()
        {
            return await _jenkinsDataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
        }

        public Job GetJobDetails(string jobName)
        {
            var returnVal = _jenkinsDataService.RequestAsync<Job>(URL.Job, Url, UserName, ApiKey, jobName);
            returnVal.Wait();
            return returnVal.Result;
        }
        public async Task<Job> GetJobDetailsAsync(string jobName)
        {
            return await _jenkinsDataService.RequestAsync<Job>(URL.Job, Url, UserName, ApiKey, jobName);
        }
    }
}
