using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    public abstract class JenkinsModel
    {

        public static T FromXml<T>(XElement element) where T : JenkinsModel, new()
        {
            var newObj = new T();
            newObj.ParseFromXml(element);
            return newObj;
        }

        protected abstract void ParseFromXml(XElement element);

    }
}
