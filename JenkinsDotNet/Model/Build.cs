using System;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual job
    /// </summary>
    public class Build : JenkinsModel<Build>
    {
        /// <summary>
        /// Gets the build number.
        /// </summary>
        /// <value>The build number.</value>
        public string Number { get; private set; }
        /// <summary>
        /// Gets the build URL.
        /// </summary>
        /// <value>The build URL</value>
        public string Url { get; private set; }
        /// <summary>
        /// Gets a short description of the build.
        /// </summary>
        /// <value>Short description of the build</value>
        public string ShortDescription { get; private set; }
        /// <summary>
        /// Gets the full display name of the build.
        /// </summary>
        /// <value>The full name of the build</value>
        public string FullDisplayName { get; private set; }
        /// <summary>
        /// Gets or sets the job this build belongs to.
        /// </summary>
        /// <value>The job</value>
        public Job Job { get; set; }
        //TODO:Add other fields

        /// <summary>
        /// Parses a build from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a build</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            if (element == null) return false;
            var elements = element.Elements().ToList();
            // Number
            Number = elements.First(x => x.Name == "number").Value;
            // Url
            Url = elements.First(x => x.Name == "url").Value;
            // ShortDescription
            ShortDescription = elements.Where(x => x.Name == "shortDescription").Select(x => x.Value).FirstOrDefault();
            // FullName
            FullDisplayName = elements.Where(x => x.Name == "fullDisplayName").Select(x => x.Value).FirstOrDefault();
            return true;
        }
    }
}