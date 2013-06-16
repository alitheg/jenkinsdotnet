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
        public string Description { get; private set; }
        public string Name { get; private set; }
        public int NumExecutors { get; private set; }
        public IList<Job> Jobs { get; private set; }
        public LoadStatistics LoadStatistics { get; private set; }
        public IList<View> Views { get; private set; }

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