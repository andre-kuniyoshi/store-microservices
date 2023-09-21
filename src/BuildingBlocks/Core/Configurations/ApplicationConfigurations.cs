using Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Configurations
{
    public static class ApplicationConfigurations
    {
        public static IApplicationBuilder UseTokenParser(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenParserMiddleware>();
        }
    }
}
