using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Inventario.Middlewares
{
     public class ExceptionMiddleware
     {
         private readonly RequestDelegate _next;
         private readonly ILogger<ExceptionMiddleware> _logger;

         public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
         {
             _next = next;
             _logger = logger;
         }

         public async Task InvokeAsync(HttpContext context)
         {
             try{
                await _next(context);
             }
             catch (Exception ex){
                 _logger.LogError(ex, ex.Message);
                 await HandleExceptionAsync(context, ex);
             }
         }

         private static Task HandleExceptionAsync(HttpContext context, Exception exception)
         {
            var code = exception switch{
                 UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                 KeyNotFoundException => HttpStatusCode.NotFound,
                 ArgumentException => HttpStatusCode.BadRequest,
                 _ => HttpStatusCode.InternalServerError
             };

             var result = JsonSerializer.Serialize(new
             {
                 error = exception.Message,
                 status = (int)code
             });

             context.Response.ContentType = "application/json";
             context.Response.StatusCode = (int)code;
             return context.Response.WriteAsync(result);
         }
     }
}
