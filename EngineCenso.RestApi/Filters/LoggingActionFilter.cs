using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineCenso.RestApi.Filters
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation(await FormatRequest(context.HttpContext.Request));
            await next();
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Stream originalResponseBody = context.HttpContext.Response.Body;

            try
            {
                using (var loggableResponseBodyStream = new MemoryStream())
                {
                    context.HttpContext.Response.Body = loggableResponseBodyStream;

                    await next();

                    loggableResponseBodyStream.Position = 0;
                    _logger.LogInformation(await FormatResponse(new StreamReader(loggableResponseBodyStream), context.HttpContext.Response.StatusCode));

                    loggableResponseBodyStream.Position = 0;
                    await loggableResponseBodyStream.CopyToAsync(originalResponseBody);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                // Pass on to exception handling middleware
                throw;
            }
            finally
            {
                context.HttpContext.Response.Body = originalResponseBody;
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            request.Body.Seek(0, SeekOrigin.Begin);

            var messageObjToLog = new { scheme = request.Scheme, host = request.Host, path = request.Path, queryString = request.Query, requestBody = bodyAsText };

            return JsonConvert.SerializeObject(messageObjToLog);
        }

        private static async Task<string> FormatResponse(StreamReader loggableResponseStream, int statusCode)
        {
            var messageObjectToLog = new { responseBody = await loggableResponseStream.ReadToEndAsync(), statusCode = statusCode };

            return JsonConvert.SerializeObject(messageObjectToLog);
        }
    }
}
