using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents a single Jenkins node
    /// </summary>
    public class Node : JenkinsModel<Node>
    {
        /// <summary>
        /// Gets the description of this node
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the name of this node
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the number of available executors on this node
        /// </summary>
        public int NumExecutors { get; private set; }
        /// <summary>
        /// Gets the jobs configured on this node
        /// </summary>
        public IList<Job> Jobs { get; private set; }
        /// <summary>
        /// Gets the overall load of this node
        /// </summary>
        public LoadStatistics LoadStatistics { get; private set; }
        /// <summary>
        /// Gets the views associated with this node
        /// </summary>
        public IList<View> Views { get; private set; }

        /// <summary>
        /// Parses a node from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a node</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            if (element == null) return false;
            var elements = element.Elements().ToList();

            // Name and description
            Name = elements.First(x => x.Name == "nodeName").Value;
            Description = elements.First(x => x.Name == "nodeDescription").Value;

            // Num executors
            int numExecutors;
            int.TryParse(elements.First(x => x.Name == "numExecutors").Value, out numExecutors);
            NumExecutors = numExecutors;

            // Jobs
            Jobs = elements.Where(x => x.Name == "job").Select(JenkinsModel<Job>.FromXml).ToList();

            return true;
        }
    }
}