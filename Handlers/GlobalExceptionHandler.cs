using FoodRestaurantApp_BE.Exceptions;
using FoodRestaurantApp_BE.Models.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace FoodRestaurantApp_BE.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode;
            string message, title;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = (int) System.Net.HttpStatusCode.Unauthorized;
                    title = "Authentication error";
                    message = exception.Message;
                    break;
                case ArgumentException:
                    statusCode = (int) System.Net.HttpStatusCode.BadRequest;
                    title = "Invalid argument";
                    message = exception.Message;
                    break;
                case NotFoundException:
                    statusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    title = "No data available";
                    message = exception.Message;
                    break;
                default:
                    statusCode = (int) System.Net.HttpStatusCode.InternalServerError;
                    title = "Internal server error";
                    message = exception.Message + "\r\n" + exception.InnerException;
                    break;
            }

            ErrorDto error = new()
            {
                Title = title,
                Messages = [message],
                ErrorType = exception.GetType().Name,
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            _logger.LogError("Request: {Method} {Path} - Status: {status} - Error occured: {Message}", httpContext.Request.Method,
                                                                                                       httpContext.Request.Path,
                                                                                                       Enum.Parse(typeof(HttpStatusCode), httpContext.Response.StatusCode.ToString()),
                                                                                                       exception);


            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
