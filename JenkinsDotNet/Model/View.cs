using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    public class View : JenkinsModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IList<Job> Jobs { get; set; }
        protected override void ParseFromXml(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}
