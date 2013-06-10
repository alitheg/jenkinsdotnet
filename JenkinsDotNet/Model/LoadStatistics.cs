using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
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
