using System.Net;

namespace api.middleware.exception.handler
{
    public class ExceptionHandlerInfo
    {
        public string Code { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static ExceptionHandlerInfo Default() {
            return new ExceptionHandlerInfo { 
                StatusCode = HttpStatusCode.InternalServerError,
                Code  = "S001"
            };
        }

        public static ExceptionHandlerInfo Create(HttpStatusCode  statusCode, string code) {
            return new ExceptionHandlerInfo
            {
                StatusCode = statusCode,
                Code = code
            };
        }
    }
}
