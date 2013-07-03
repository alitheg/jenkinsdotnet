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
        /// Gets the name of the job.
        /// </summary>
        /// <value>
        /// The name
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets the URL of the job.
        /// </summary>
        /// <value>
        /// The URL
        /// </value>
        public string Url { get; private set; }
        /// <summary>
        /// Gets the color (status) of the job.
        /// </summary>
        /// <value>
        /// The color - blue = success, red = failure
        /// </value>
        public string Color { get; private set; }
        /// <summary>
        /// Gets the display name of the job.
        /// </summary>
        /// <value>
        /// The display name
        /// </value>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Gets the description of the job.
        /// </summary>
        /// <value>
        /// The description
        /// </value>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the upstream projects from this job
        /// </summary>
        /// <value>
        /// List of upstream projects
        /// </value>
        public IList<Job> UpstreamProjects { get; private set; }
        /// <summary>
        /// Gets the downstream projects from this job.
        /// </summary>
        /// <value>
        /// List of downstream projects
        /// </value>
        public IList<Job> DownstreamProjects { get; private set; }
        /// <summary>
        /// Gets the past builds of this job.
        /// </summary>
        /// <value>
        /// List of builds
        /// </value>
        public IList<Build> Builds { get; private set; }
        /// <summary>
        /// Gets the last run build of this job.
        /// </summary>
        /// <value>
        /// The last build
        /// </value>
        public Build LastBuild { get; private set; }
        /// <summary>
        /// Gets the last completed build of this job.
        /// </summary>
        /// <value>
        /// The last completed build
        /// </value>
        public Build LastCompletedBuild { get; private set; }
        /// <summary>
        /// Gets the last failed build of this job.
        /// </summary>
        /// <value>
        /// The last failed build
        /// </value>
        public Build LastFailedBuild { get; private set; }
        /// <summary>
        /// Gets the last stable build of this job.
        /// </summary>
        /// <value>
        /// The last stable build
        /// </value>
        public Build LastStableBuild { get; private set; }
        /// <summary>
        /// Gets the last successful build of this job.
        /// </summary>
        /// <value>
        /// The last successful build
        /// </value>
        public Build LastSuccessfulBuild { get; private set; }
        /// <summary>
        /// Gets the last unsuccessful build of this job.
        /// </summary>
        /// <value>
        /// The last unsuccessful build
        /// </value>
        public Build LastUnsuccessfulBuild { get; private set; }
        //TODO: Add other properties

        /// <summary>
        /// Parses a job from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a job</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
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