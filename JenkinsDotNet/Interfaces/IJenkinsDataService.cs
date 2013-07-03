using System.Threading.Tasks;
using JenkinsDotNet.Model;

namespace JenkinsDotNet.Interfaces
{
    /// <summary>
    /// Data service for connecting to Jenkins.
    /// Provides only parameterised (generic) type access.
    /// Use <see cref="JenkinsServer"/> class for type-specific methods.
    /// </summary>
    public interface IJenkinsDataService
    {
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
        Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey,
                                params object[] parameters) where T : JenkinsModel<T>, new();
    }
}