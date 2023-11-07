using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Middleware
{
    public class TokenParserMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenParserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string bearerToken = httpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(bearerToken))
            {
                var token = bearerToken.Replace("Bearer ", "");

                // TODO: Remove log in future
                Console.WriteLine($"Token: {token}");
                var tokenParsed = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var userIdentity = new ClaimsIdentity(tokenParsed.Claims, ClaimTypes.Name);

                httpContext.User = new ClaimsPrincipal(userIdentity);
            }

            await _next(httpContext);
        }
    }
}
