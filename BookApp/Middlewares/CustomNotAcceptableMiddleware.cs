using System.Text.Json;

namespace BookApp.Middlewares
{
    public class CustomNotAcceptableMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status406NotAcceptable)
            {
                var acceptHeader = context.Request.Headers.Accept.ToString();
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Code = StatusCodes.Status406NotAcceptable,
                    ErrorMessage = $"Please check the provided requested format {acceptHeader} is not accepted."
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
