using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Identity.API.Controllers
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController()
        {
            
        }

        //[HttpPost("~/connect/token")]
        //public async Task<IActionResult> Exchange()
        //{
        //    var request = HttpContext.GetOpenIddictServerRequest() ??
        //                  throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        //    // Note: the client credentials are automatically validated by OpenIddict:
        //    if (!request.IsClientCredentialsGrantType())
        //    {
        //        // if client_id or client_secret are invalid.
        //        return Forbid();
        //    }

        //    //var claimsPrincipal = await _clientCredentialFlowService.Exchange(request);

        //    // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
        //    return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        //}
    }

}
