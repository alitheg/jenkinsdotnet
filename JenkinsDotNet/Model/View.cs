using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual view
    /// </summary>
    public class View : JenkinsModel<View>
    {
        /// <summary>
        /// Gets the name of this view
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the URL of this view
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Gets the list of jobs in this view
        /// </summary>
        public IList<Job> Jobs { get; private set; }

        /// <summary>
        /// Parses a view from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a view</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}