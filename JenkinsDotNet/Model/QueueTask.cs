using System;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    internal class QueueTask : JenkinsModel<QueueTask>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Blocked { get; set; }

        protected override bool ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}