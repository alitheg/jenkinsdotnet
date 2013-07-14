using System;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual computer in a Jenkins cluster
    /// </summary>
    public class Computer : JenkinsModel<Computer>
    {
        /// <summary>
        /// Gets the build number.
        /// </summary>
        /// <value>The build number.</value>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Gets the build URL.
        /// </summary>
        /// <value>The build URL</value>
        public string Icon { get; private set; }
        /// <summary>
        /// Gets a short description of the build.
        /// </summary>
        /// <value>Short description of the build</value>
        public bool Idle { get; private set; }
        /// <summary>
        /// Gets the full display name of the build.
        /// </summary>
        /// <value>The full name of the build</value>
        public bool Offline { get; private set; }
        //TODO:Add other fields

        /// <summary>
        /// Parses a computer from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a masterComputer or slaveComputer</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            if (element == null) return false;
            var elements = element.Elements().ToList();
            // DisplayName
            DisplayName = elements.First(x => x.Name == "displayName").Value;
            // Icon
            Icon = elements.First(x => x.Name == "icon").Value;
            // Idle
            bool idle;
            bool.TryParse(elements.Where(x => x.Name == "idle").Select(x => x.Value).FirstOrDefault(),out idle);
            Idle = idle;
            // Offline
            bool offline;
            bool.TryParse(elements.Where(x => x.Name == "offline").Select(x => x.Value).FirstOrDefault(), out offline);
            Offline = offline;
            return true;
        }
    }
}