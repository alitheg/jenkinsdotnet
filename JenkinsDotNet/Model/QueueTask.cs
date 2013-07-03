using System;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    internal class QueueTask : JenkinsModel<QueueTask>
    {
        /// <summary>
        /// Gets the name of this task
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the URL of this task
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Gets whether thistask is currently blocked
        /// </summary>
        public bool Blocked { get; private set; }

        /// <summary>
        /// Parses a queue task from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a queue task</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}