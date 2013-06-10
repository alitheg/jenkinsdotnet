using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Base class for models, providing a common functionality
    /// </summary>
    public abstract class JenkinsModel
    {
        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static T FromXml<T>(XElement element) where T : JenkinsModel, new()
        {
            var newObj = new T();
            newObj.ParseFromXml(element);
            return newObj;
        }

        /// <summary>
        /// Parses from XML.
        /// </summary>
        /// <param name="element">The element.</param>
        protected abstract void ParseFromXml(XElement element);
    }
}