using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace WorkflowAutomation.Server.Extensions.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(
                                new ResponseModel()
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = "Internal Server Error."
                                }
                                .ToString());
                        }
                    });
                });
        }

        public class ResponseModel
        {
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            public override string ToString() => JsonSerializer.Serialize(this);
        }
    }
}
