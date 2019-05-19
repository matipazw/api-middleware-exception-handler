using Microsoft.AspNetCore.Builder;

namespace api.middleware.exception.handler
{
    public static class ExceptionHandlerMiddlewareExtensions
    {   
		public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder, ExceptionHandlerOptions options = null)
        {
			return builder.UseMiddleware<ExceptionHandlerMiddleware>(options);
        }

    }
}
