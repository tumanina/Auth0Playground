using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using ClientApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ClientApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        private readonly Auth0Configuration _configuration;

        public LoginController(IOptions<Auth0Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authenticationApiClient = new AuthenticationApiClient(_configuration.Domain);

            var token = await authenticationApiClient.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                ClientId = _configuration.ClientId,
                Username = request.UserName,
                Password = request.Password,
                Scope = "openid"
            });

            return new OkObjectResult(token);
        }
    }
}