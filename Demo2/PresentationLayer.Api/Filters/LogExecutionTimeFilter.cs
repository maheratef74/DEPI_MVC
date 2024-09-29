using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace PresentationLayer.Api.Filters
{
    public class LogExecutionTimeFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogExecutionTimeFilter> _logger;
        private Stopwatch _stopWatch;

        public LogExecutionTimeFilter(ILogger<LogExecutionTimeFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action Execution started .....");
            _stopWatch = Stopwatch.StartNew();
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action Execution started .....");
            _stopWatch.Stop();
            _logger.LogInformation($"Action executed in {_stopWatch.ElapsedMilliseconds} ms");
            if(context.Result is ObjectResult objectResult)
            {
                _logger.LogInformation($"Status Code is {objectResult.StatusCode}");
            }
        }
    }
}
