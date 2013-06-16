// ***********************************************************************
// Assembly         : JenkinsDotNet
// Author           : Alastair
// Created          : 06-10-2013
//
// Last Modified By : Alastair
// Last Modified On : 06-16-2013
// ***********************************************************************
// <copyright file="JenkinsModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Reflection;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Base class for models, providing a common functionality
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class JenkinsModel<T> where T : JenkinsModel<T>,new()
    {
        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>`0.</returns>
        public static T FromXml(XElement element)
        {
            var newObj = new T();
            return newObj.ParseFromXml(element) ? newObj : null;
        }

        /// <summary>
        /// Parses from XML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected abstract bool ParseFromXml(XElement element);

        /// <summary>
        /// Updates from.
        /// </summary>
        /// <param name="source">The source.</param>
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