using System;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual job 
    /// </summary>
    public class Job : JenkinsModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }

        protected override void ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}