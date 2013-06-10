using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents a single Jenkins node
    /// </summary>
    public class Node : JenkinsModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int NumExecutors { get; set; }
        public IList<Job> Jobs { get; set; }
        public LoadStatistics LoadStatistics { get; set; }
        public IList<View> Views { get; set; }

        protected override void ParseFromXml(XElement element)
        {
            List<XElement> elements = element.Elements().ToList();
            Name = elements.First(x => x.Name == "nodeName").Value;
            Description = elements.First(x => x.Name == "nodeDescription").Value;
            int numExecutors;
            int.TryParse(elements.First(x => x.Name == "numExecutors").Value, out numExecutors);
            NumExecutors = numExecutors;
        }
    }
}