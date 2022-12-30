using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ManagementApi.Controllers
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
        public async Task<IActionResult> Login()
        {
            var authenticationApiClient = new AuthenticationApiClient(_configuration.Domain);

            var token = await authenticationApiClient.GetTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _configuration.ClientId,
                ClientSecret = _configuration.ClientSecret,
                Audience = _configuration.Audience
            });

            return new OkObjectResult(token);
        }
    }
}