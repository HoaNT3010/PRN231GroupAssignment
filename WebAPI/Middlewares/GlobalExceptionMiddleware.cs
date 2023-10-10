using Application.ErrorHandlers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace WebAPI.Middlewares
{
    /// <summary>
    /// Handle unexpected exceception in the pipeline
    /// </summary>
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Update response header
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);

            // Update response message
            ErrorDetail detail = new()
            {
                StatusCode = GetStatusCode(exception),
                Title = GetTitle(exception),
                Message = exception.Message
            };
            string json = JsonConvert.SerializeObject(detail);
            await context.Response.WriteAsync(json);
        }

        private static int GetStatusCode(Exception exception)
        {
            if (exception is BaseException baseException)
            {
                return baseException.StatusCode;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        private static string GetTitle(Exception exception)
        {
            if (exception is BaseException baseException)
            {
                return baseException?.Title ?? "Internal server error";
            }
            return "Internal server error";
        }
    }
}
