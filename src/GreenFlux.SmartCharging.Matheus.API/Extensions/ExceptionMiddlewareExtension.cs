using GreenFlux.SmartCharging.Matheus.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GreenFlux.SmartCharging.Matheus.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
