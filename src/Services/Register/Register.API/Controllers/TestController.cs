using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Net;


namespace Register.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    //[Authorize]
    public class TestController : ControllerBase
    {
        public TestController()
        {
            
        }

        [HttpGet("~/test/api")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<string>> TestAPI()
        {
            try
            {
                return Ok($"Teste OK: {User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
