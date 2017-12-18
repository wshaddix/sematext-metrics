using System;
using System.Net;

namespace Sematext.Exceptions
{
    public class SematextApiException : Exception
    {
        public string Content { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public SematextApiException(HttpStatusCode statusCode, string content) : base($"Status Code: {statusCode}")
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}