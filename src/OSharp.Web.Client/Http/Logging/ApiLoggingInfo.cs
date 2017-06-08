using System.Collections.Generic;
using System.Net;


namespace OSharp.Web.Http.Logging
{
    public class ApiLoggingInfo
    {
        private readonly List<string> _headers = new List<string>();

        public string HttpMethod { get; set; }
        public string UriAccessed { get; set; }
        public string BodyContent { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseStatusMessage { get; set; }
        public string IpAddress { get; set; }
        public HttpMessageType MessageType { get; set; }
        public LoggingLevel LoggingLevel { get; set; }

        public List<string> Headers
        {
            get { return _headers; }
        }
    }


    public enum LoggingLevel
    {
        Debug,
        Trace,
        Info,
        Warn,
        Error,
        Fault
    }


    public enum HttpMessageType
    {
        Request,
        Response
    }
}
