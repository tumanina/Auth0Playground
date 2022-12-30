using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ClientApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {
        private readonly Auth0Configuration _configuration;

        public ClientController(IOptions<Auth0Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        [Route("info")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var authorizationHeader = Request.Headers["Authorization"];

            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var token = headerValue.Parameter;
                var authenticationApiClient = new AuthenticationApiClient(_configuration.Domain);

                var info = await authenticationApiClient.GetUserInfoAsync(token);

                return new OkObjectResult(info);
            }

            return Unauthorized();
        }
    }
}