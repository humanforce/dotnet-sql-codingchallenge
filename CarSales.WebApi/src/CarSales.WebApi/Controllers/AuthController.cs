using CarSales.WebApi.Models;
using CarSales.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace CarSales.WebApi.Controllers
{

    [ApiController]
    [Route("oauth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) { 
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody]OAuthRequest request)
        {
            try
            {
                return Ok(await _authService.GetToken(request));
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
