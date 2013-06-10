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

namespace JenkinsDotNet
{
    public class JenkinsServer
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        private readonly DataService dataService;

        public JenkinsServer(string url, string username, string apikey, string name = "Jenkins Server")
        {
            Url = url;
            UserName = username;
            ApiKey = apikey;
            Name = name;
            dataService = DataService.Instance;
        }

        public Node GetNodeDetails()
        {
            var returnVal = dataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
            returnVal.Wait();
            return returnVal.Result;
        }
        public async Task<Node> GetNodeDetailsAsync()
        {
            return await dataService.RequestAsync<Node>(URL.Api, Url, UserName, ApiKey);
        }
    }
}
