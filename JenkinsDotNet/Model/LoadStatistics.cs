using System;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents the overall load of a Jenkins node.
    /// </summary>
    public class LoadStatistics : JenkinsModel<LoadStatistics>
    {
        /// <summary>
        /// Gets the queue length of this node
        /// </summary>
        public float QueueLength { get; private set; }
        /// <summary>
        /// Gets the current number of busy executors
        /// </summary>
        public float BusyExecutors { get; private set; }
        /// <summary>
        /// Gets the total available executors 
        /// </summary>
        public float TotalExecutors { get; private set; }

        /// <summary>
        /// Parses a LoadStatistics object from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a node's LoadStatistics</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}