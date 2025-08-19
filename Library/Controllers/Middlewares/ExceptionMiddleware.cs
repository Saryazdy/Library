using System.Net;
using System.Text.Json;
using Library.Application.Responses;
using Library.Application.Exceptions;

namespace Library.API.Controllers.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail(ex.Errors);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail("یک خطای داخلی رخ داد.");
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
