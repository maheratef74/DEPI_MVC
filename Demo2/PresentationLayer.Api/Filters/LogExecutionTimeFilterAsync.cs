using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace PresentationLayer.Api.Filters
{
    public class LogExecutionTimeFilterAsync : IAsyncActionFilter
    {
        private readonly ILogger<LogExecutionTimeFilter> _logger;
        private Stopwatch _stopWatch;

        public LogExecutionTimeFilterAsync(ILogger<LogExecutionTimeFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Action Execution started .....");
            _stopWatch = Stopwatch.StartNew();

            var resultContext = await next();

            _logger.LogInformation("Action Execution started .....");
            _stopWatch.Stop();
            _logger.LogInformation($"Action executed in {_stopWatch.ElapsedMilliseconds} ms");
            if (resultContext.Result is ObjectResult objectResult)
            {
                _logger.LogInformation($"Status Code is {objectResult.StatusCode}");
            }
        }
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    _logger.LogInformation("Action Execution started .....");
        //    _stopWatch = Stopwatch.StartNew();
        //}
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    _logger.LogInformation("Action Execution started .....");
        //    _stopWatch.Stop();
        //    _logger.LogInformation($"Action executed in {_stopWatch.ElapsedMilliseconds} ms");
        //    if(context.Result is ObjectResult objectResult)
        //    {
        //        _logger.LogInformation($"Status Code is {objectResult.StatusCode}");
        //    }
        //}
    }
}
