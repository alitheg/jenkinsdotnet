using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
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
            var elements = element.Elements().ToList();
            Name = elements.First(x => x.Name == "nodeName").Value;
            Description = elements.First(x => x.Name == "nodeDescription").Value;
            int numExecutors;
            int.TryParse(elements.First(x => x.Name == "numExecutors").Value, out numExecutors);
            NumExecutors = numExecutors;

        }
    }
}
