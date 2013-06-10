using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JenkinsDotNet
{
// ReSharper disable InconsistentNaming
    public sealed class URL
// ReSharper restore InconsistentNaming
    {
        private readonly String _url;

        public static readonly URL LoadStatistics = new URL("/overallLoad/api/xml");
        public static readonly URL BuildQueue = new URL("/queue/api/xml");
        public static readonly URL Api = new URL("/api/xml");
        public static readonly URL Job = new URL("/job/{0}/api/xml");

        private URL(String url)
        {
            this._url = url;
        }

        public String Url(params object[] parameters)
        {
            return String.Format(this.ToString(), parameters);
        }

        public override String ToString()
        {
            return _url;
        }
    }
}
