using System;

namespace JenkinsDotNet
{
// ReSharper disable InconsistentNaming
    public sealed class URL
// ReSharper restore InconsistentNaming
    {
        public static readonly URL LoadStatistics = new URL("/overallLoad/api/xml");
        public static readonly URL BuildQueue = new URL("/queue/api/xml");
        public static readonly URL Api = new URL("/api/xml");
        public static readonly URL Job = new URL("/job/{0}/api/xml");
        public static readonly URL Build = new URL("/job/{0}/{1}/api/xml");
        private readonly String _url;

        private URL(String url)
        {
            _url = url;
        }

        public String Url(params object[] parameters)
        {
            return String.Format(ToString(), parameters);
        }

        public override String ToString()
        {
            return _url;
        }
    }
}