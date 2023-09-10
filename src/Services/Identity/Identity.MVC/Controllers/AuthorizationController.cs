using System.Collections.Immutable;
using System.Security.Claims;
using System.Web;
using Identity.Application.Domain;
using Identity.MVC.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.MVC.Controllers;

public class AuthorizationController : Controller
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly AuthorizationService _authService;

    public AuthorizationController(
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictScopeManager scopeManager,
            AuthorizationService authService)
    {
        _applicationManager = applicationManager;
        _scopeManager = scopeManager;
        _authService = authService;
    }

    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var application = await _applicationManager.FindByClientIdAsync(request.ClientId) ??
                          throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

        if (await _applicationManager.GetConsentTypeAsync(application) != ConsentTypes.Explicit)
        {
            return Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidClient,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "Only clients with explicit consent type are allowed."
                }));
        }

        var parameters = _authService.ParseOAuthParameters(HttpContext, new List<string> { Parameters.Prompt });

        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!_authService.IsAuthenticated(result, request))
        {
            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = _authService.BuildRedirectUrl(HttpContext.Request, parameters)
            }, new[] { CookieAuthenticationDefaults.AuthenticationScheme });
        }

        if (request.HasPrompt(Prompts.Login))
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = _authService.BuildRedirectUrl(HttpContext.Request, parameters)
            }, new[] { CookieAuthenticationDefaults.AuthenticationScheme });
        }

        var consentClaim = result.Principal.GetClaim(Consts.ConsentNaming);

        // it might be extended in a way that consent claim will contain list of allowed client ids.
        if (consentClaim != Consts.GrantAccessValue || request.HasPrompt(Prompts.Consent))
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var returnUrl = HttpUtility.UrlEncode(_authService.BuildRedirectUrl(HttpContext.Request, parameters));
            var consentRedirectUrl = $"/Consent?returnUrl={returnUrl}";

            return Redirect(consentRedirectUrl);
        }

        var userId = result.Principal.FindFirst(ClaimTypes.Email)!.Value;

        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        identity.SetClaim(Claims.Subject, userId)
            .SetClaim(Claims.Email, userId)
            .SetClaim(Claims.Name, userId)
            .SetClaims(Claims.Role, new List<string> { "user", "admin" }.ToImmutableArray());

        identity.SetScopes(request.GetScopes());
        identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
        identity.SetDestinations(c => AuthorizationService.GetDestinations(identity, c));

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ??
                      throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (!request.IsAuthorizationCodeGrantType() && !request.IsRefreshTokenGrantType())
            throw new InvalidOperationException("The specified grant type is not supported.");

        var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        var userId = result.Principal.GetClaim(Claims.Subject);

        if (string.IsNullOrEmpty(userId))
        {
            return Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "Cannot find user from the token."
                }));
        }

        var identity = new ClaimsIdentity(result.Principal.Claims,
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        identity.SetClaim(Claims.Subject, userId)
            .SetClaim(Claims.Email, userId)
            .SetClaim(Claims.Name, userId)
            .SetClaims(Claims.Role, new List<string> { "user", "admin" }.ToImmutableArray());

        identity.SetDestinations(c => AuthorizationService.GetDestinations(identity, c));

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        // Note: by default, claims are NOT automatically included in the access and identity tokens.
        // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
        // whether they should be included in access tokens, in identity tokens or in both.

        switch (claim.Type)
        {
            case Claims.Name:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Profile))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Email:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Email))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Role:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Roles))
                    yield return Destinations.IdentityToken;

                yield break;

            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            case "AspNet.Identity.SecurityStamp": yield break;

            default:
                yield return Destinations.AccessToken;
                yield break;
        }
    }
}
