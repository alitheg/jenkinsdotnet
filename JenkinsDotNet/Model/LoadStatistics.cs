using System;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents the overallload of a Jenkins node.
    /// </summary>
    public class LoadStatistics : JenkinsModel
    {
        public float QueueLength { get; set; }
        public float BusyExecutors { get; set; }
        public float TotalExecutors { get; set; }

        protected override void ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}