using Core.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Core.Configurations
{
    public static class ApplicationConfigurations
    {
        public static IApplicationBuilder UseTokenParser(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenParserMiddleware>();
        }

        public static IApplicationBuilder UseHttpLoggingSerilog(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging();
        }

        public static void StopSerilog()
        {
            Log.CloseAndFlush();
        }
    }
}
