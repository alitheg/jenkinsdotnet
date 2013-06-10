using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JenkinsDotNet.Model;

namespace JenkinsDotNet.Interfaces
{
    public interface IJenkinsDataService
    {
        Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey,
                                             params object[] parameters) where T : JenkinsModel, new();
    }
}
