using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Infrastructure
{
    public class RestCountriesException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public RestCountriesException() { }

        public RestCountriesException(HttpStatusCode statusCode)
            => StatusCode = statusCode;

        public RestCountriesException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;

        public RestCountriesException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
            => StatusCode = statusCode;
    }
}
