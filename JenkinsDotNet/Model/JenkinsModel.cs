using System.Reflection;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Base class for models, providing common functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JenkinsModel<T> where T : JenkinsModel<T>,new()
    {
        /// <summary>
        /// Read a model from an XML fragment.
        /// </summary>
        /// <param name="element">XML fragment</param>
        /// <returns>Object of the current type</returns>
        public static T FromXml(XElement element)
        {
            var newObj = new T();
            return newObj.ParseFromXml(element) ? newObj : null;
        }

        /// <summary>
        /// Parses a model from Jenkins API XML.
        /// </summary>
        /// <param name="element">XML fragment representing a model</param>
        /// <returns><c>true</c> if XML was valid, <c>false</c> otherwise</returns>
        protected abstract bool ParseFromXml(XElement element);

        /// <summary>
        /// Updates a model from another instance of the same model.
        /// </summary>
        /// <param name="source">The source object</param>
        public virtual void UpdateFrom(T source)
        {
            // Iterate the Properties of the destination instance and  
            // populate them from their source counterparts  
            var destinationProperties = this.GetType().GetProperties();
            foreach (var destinationPi in destinationProperties)
            {
                var sourcePi = source.GetType().GetProperty(destinationPi.Name);
                destinationPi.SetValue(this, sourcePi.GetValue(source, null), null);
            } 
        }
    }
}