using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual view
    /// </summary>
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