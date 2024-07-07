using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FoodRestaurantApp_BE.Filters
{
    public class LoggingFilter(ILogger<LoggingFilter> logger) : ActionFilterAttribute
    {
        private readonly ILogger<LoggingFilter> _logger = logger;

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            HttpRequest request = actionContext.HttpContext.Request;
            _logger.LogInformation("Executing endpoint: {Method} {Path}", request.Path, request.Method);
            base.OnActionExecuting(actionContext);
        }

        public override void OnResultExecuted(ResultExecutedContext resultContext)
        {
            HttpContext context = resultContext.HttpContext;
            if(resultContext.Exception == null) {
                _logger.LogInformation("Finish executing endpoint: {Method} {Path} - Status {statusCode}", context.Request.Method,
                                                                                                           context.Request.Path,
                                                                                                           Enum.Parse(typeof(HttpStatusCode), context.Response.StatusCode.ToString()));
            }
            base.OnResultExecuted(resultContext);
        }
    }
}
