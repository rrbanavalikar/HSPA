using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Errors;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddlewares
    {
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddlewares> logger;
    private readonly IHostEnvironment env;

    public ExceptionMiddlewares(RequestDelegate next,
                                ILogger<ExceptionMiddlewares> logger,
                                IHostEnvironment env)
    {
      this.next = next;
      this.logger = logger;
      this.env = env;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);  //means all of the code is in try catch block
      }
      catch (Exception ex)
      {
          ApiErrors response;
          HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
          String message;
          var exceptionType = ex.GetType();

          if(exceptionType == typeof(UnauthorizedAccessException))
          {
            statusCode = HttpStatusCode.Forbidden;
            message = "You are not authorized";
          }
          else{
            statusCode = HttpStatusCode.InternalServerError;
            message = "Some unknown error occured";
          }

          if(env.IsDevelopment())
          {
            response = new ApiErrors((int)statusCode,ex.Message,ex.StackTrace.ToString());
          }
          else
          {
            response = new ApiErrors((int)statusCode,message);
          }


          logger.LogError(ex, ex.Message);
          context.Response.StatusCode = (int)statusCode;
          context.Response.ContentType = "application/json";
          await context.Response.WriteAsync(response.ToString());
      }
    }
  }
}
