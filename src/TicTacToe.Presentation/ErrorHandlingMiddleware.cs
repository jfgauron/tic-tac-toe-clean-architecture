using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TicTacToe.Presentation;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (InvalidOperationException ex)
      {
        await HandleBadRequestException(context, ex);
      }
      catch (Exception ex)
      {
        await HandleUnknownException(context, ex);
      }
    }

    private Task HandleBadRequestException(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

      var errorResponse = new
      {
        Message = exception.Message
      };

      return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }

    private Task HandleUnknownException(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      var errorResponse = new
      {
        Message = "An unexpected error occurred."
      };

      return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }
}