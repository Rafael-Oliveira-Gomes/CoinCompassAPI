using System.Net;

namespace CoinCompassAPI.Infrastructure.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode Status
        {
            get; private set;
        }
        public HttpResponseException(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
        }
    }
}
