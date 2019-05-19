using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api.middleware.exception.handler
{
    public class ExceptionHandlerMiddleware
    {
        readonly RequestDelegate _next;
        readonly ExceptionHandlerOptions _options;

        const string ApplicationJsonContentType = "application/json";
        const string TextPlainContentType = "text/plain";
        const string DefaultExceptionError = "Opps!!!! Internal error Server.";

        public ExceptionHandlerMiddleware(RequestDelegate next, ExceptionHandlerOptions options)
        {
            _options = options;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
			try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    throw;
                }

                ClearResponse(context);

                var thereIsConfiguration = GetConfiguration(ex, out ExceptionHandlerInfo info);

                if (thereIsConfiguration)
				{
                    await ConfigureResponse(context, info, ApplicationJsonContentType, GetJsonResponse(ex.Message, info.Code));
                }
                else
				{
                    await ConfigureResponse(context, ExceptionHandlerInfo.Default(), TextPlainContentType, DefaultExceptionError);
                }

                return;
            }

			return;
        }

        async Task ConfigureResponse(HttpContext context, ExceptionHandlerInfo info, string contentType, string message)
        {
            context.Response.StatusCode = (int)info.StatusCode;
            context.Response.ContentType = contentType;
			context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            await context.Response.WriteAsync(message);
        }
        
        string GetJsonResponse(string message , string errorCode)
        {
            return $"{{ \"code\" : \"{errorCode}\" , \"message\" : \"{message}\"}}";
        }

        bool GetConfiguration(Exception exception, out ExceptionHandlerInfo info)
        {
			if (_options == null)
			{
				info = ExceptionHandlerInfo.Default();
				return false;        
			}	

            return _options.Maps.TryGetValue(exception.GetType(), out info);
        }

        static void ClearResponse(HttpContext context)
        {
            context.Response.Clear();
        }
    }
}
