using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Client.AspNetCore;

namespace Core.Middlewares
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationDelegatingHandler> _logger;

        public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationDelegatingHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                var token = await _httpContextAccessor.HttpContext!.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);

                // TODO: Remove log in future
                _logger.LogInformation($"DelegatingHandler token: {token}");

                if (String.IsNullOrEmpty(token))
                {
                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpResponseMessage = await base.SendAsync(request, cancellationToken);
                // TODO: Improve logs of failed status code
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);

                throw;
            }

            return httpResponseMessage;
        }
    }
}
