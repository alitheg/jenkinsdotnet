using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    public class Job : JenkinsModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }

        protected override void ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }

        public override URL ObjectUrl
        {
            get { return URL.Job; }
        }
    }
}
