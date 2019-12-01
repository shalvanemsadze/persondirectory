using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersonDirectory.API.HelperTypes;
using PersonDirectory.Shared.Helper_Types.Exceptions;

namespace PersonDirectory.API.Middlewares
{
    public class ExceptionLogger
    {
        private readonly RequestDelegate _next;

        public ExceptionLogger(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ApiResponse<object> { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message, Result = new { error = ex.ToString() } };

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.Code;
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionLoggerExtensions
    {
        public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLogger>();
        }
    }
}
