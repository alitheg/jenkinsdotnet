using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual job
    /// </summary>
    public class Job : JenkinsModel<Job>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; private set; }
        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; private set; }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the upstream projects.
        /// </summary>
        /// <value>
        /// The upstream projects.
        /// </value>
        public IList<Job> UpstreamProjects { get; private set; }
        /// <summary>
        /// Gets the downstream projects.
        /// </summary>
        /// <value>
        /// The downstream projects.
        /// </value>
        public IList<Job> DownstreamProjects { get; private set; }
        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        public IList<Build> Builds { get; private set; }
        /// <summary>
        /// Gets the last build.
        /// </summary>
        /// <value>
        /// The last build.
        /// </value>
        public Build LastBuild { get; private set; }
        /// <summary>
        /// Gets the last completed build.
        /// </summary>
        /// <value>
        /// The last completed build.
        /// </value>
        public Build LastCompletedBuild { get; private set; }
        /// <summary>
        /// Gets the last failed build.
        /// </summary>
        /// <value>
        /// The last failed build.
        /// </value>
        public Build LastFailedBuild { get; private set; }
        /// <summary>
        /// Gets the last stable build.
        /// </summary>
        /// <value>
        /// The last stable build.
        /// </value>
        public Build LastStableBuild { get; private set; }
        /// <summary>
        /// Gets the last successful build.
        /// </summary>
        /// <value>
        /// The last successful build.
        /// </value>
        public Build LastSuccessfulBuild { get; private set; }
        /// <summary>
        /// Gets the last unsuccessful build.
        /// </summary>
        /// <value>
        /// The last unsuccessful build.
        /// </value>
        public Build LastUnsuccessfulBuild { get; private set; }
        //TODO: Add other properties

        protected override bool ParseFromXml(XElement element)
        {
            if (element == null) return false;
            var elements = element.Elements().ToList();
            // Name
            Name = elements.First(x => x.Name == "name").Value;
            // Url
            Url = elements.First(x => x.Name == "url").Value;
            // Color
            Color = elements.First(x => x.Name == "color").Value;
            // DisplayName
            DisplayName = elements.Where(x => x.Name == "displayName").Select(x=>x.Value).FirstOrDefault();
            // Description
            Description = elements.Where(x => x.Name == "description").Select(x => x.Value).FirstOrDefault();
            // UpstreamProjects
            UpstreamProjects = elements.Where(x => x.Name == "upstreamProject").Select(FromXml).ToList();
            // DownstreamProjects
            DownstreamProjects = elements.Where(x => x.Name == "downstreamProject").Select(FromXml).ToList();
            // Builds
            Builds = elements.Where(x => x.Name == "build").Select(JenkinsModel<Build>.FromXml).ToList();
            // LastBuild
            LastBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastBuild"));
            // LastCompletedBuild
            LastCompletedBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastCompletedBuild"));
            // LastFailedBuild
            LastFailedBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastFailedBuild"));
            // LastStableBuild
            LastStableBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastStableBuild"));
            // LastSuccessfulBuild
            LastSuccessfulBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastSuccessfulBuild"));
            // LastUnsuccessfulBuild
            LastUnsuccessfulBuild = JenkinsModel<Build>.FromXml(elements.FirstOrDefault(x => x.Name == "lastUnsuccessfulBuild"));
            return true;
        }
    }
}