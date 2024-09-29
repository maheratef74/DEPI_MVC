using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.Api.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            // Modify the response if needed
            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred. Please try again later."
            })
            {
                StatusCode = 500
            };

            // Mark the exception as handled
            context.ExceptionHandled = true;
        }
    }
}
