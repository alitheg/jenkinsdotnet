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
        /// Requests the async.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component">The component.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<T> RequestAsync<T>(URL component, string baseUrl, string userName, string apiKey,
                                params object[] parameters) where T : JenkinsModel<T>, new();
    }
}