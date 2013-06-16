// ***********************************************************************
// Assembly         : JenkinsDotNet
// Author           : Alastair
// Created          : 06-15-2013
//
// Last Modified By : Alastair
// Last Modified On : 06-16-2013
// ***********************************************************************
// <copyright file="Build.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Xml.Linq;

namespace JenkinsDotNet.Model
{
    /// <summary>
    /// Represents an individual job
    /// </summary>
    public class Build : JenkinsModel<Build>
    {
        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; private set; }
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; private set; }
        /// <summary>
        /// Gets the short description.
        /// </summary>
        /// <value>The short description.</value>
        public string ShortDescription { get; private set; }
        /// <summary>
        /// Gets the full name of the display.
        /// </summary>
        /// <value>The full name of the display.</value>
        public string FullDisplayName { get; private set; }
        //TODO:Add other fields

        /// <summary>
        /// Parses from XML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected override bool ParseFromXml(XElement element)
        {
            if (element == null) return false;
            var elements = element.Elements().ToList();
            // Number
            Number = elements.First(x => x.Name == "number").Value;
            // Url
            Url = elements.First(x => x.Name == "url").Value;
            // ShortDescription
            ShortDescription = elements.Where(x => x.Name == "shortDescription").Select(x => x.Value).FirstOrDefault();
            // FullName
            FullDisplayName = elements.Where(x => x.Name == "fullDisplayName").Select(x => x.Value).FirstOrDefault();
            return true;
        }
    }
}