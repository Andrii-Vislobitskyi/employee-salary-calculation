
using System.Net;

namespace EMP.Common.Exceptions
{
    public class CustomResponseException: Exception
    {
        private HttpStatusCode _status;
        private string _reason;
        public CustomResponseException(HttpStatusCode status, string reason) : base(reason)
        {
            this._status = status;
            this._reason = reason;
        }
        public HttpStatusCode GetStatus() => _status;
        
        public string GetReason() => _reason;    
    }
}
