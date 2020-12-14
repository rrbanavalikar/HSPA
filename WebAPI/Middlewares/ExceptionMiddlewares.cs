using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddlewares
    {
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddlewares> logger;

    public ExceptionMiddlewares(RequestDelegate next, ILogger<ExceptionMiddlewares> logger)
    {
      this.next = next;
      this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);  //means all of the code is in try catch block
      }
      catch (Exception ex)
      {
          logger.LogError(ex, ex.Message);
          context.Response.StatusCode = 500;
          await context.Response.WriteAsync(ex.Message);
      }
    }
  }
}
