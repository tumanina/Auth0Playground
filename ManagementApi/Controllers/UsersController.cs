using Auth0.Core.Exceptions;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly Auth0Configuration _configuration;

        public UsersController(IOptions<Auth0Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            var authorizationHeader = Request.Headers["Authorization"];

            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var token = headerValue.Parameter;
                var managementApiClient = new ManagementApiClient(token, _configuration.Domain);

                try
                {
                    var user = await managementApiClient.Users.GetAsync(userId);

                    return new OkObjectResult(user);
                }
                catch(ErrorApiException ex) when (ex.Message.Contains("The user does not exist"))
                {
                    return NotFound();
                }
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]string email)
        {
            var authorizationHeader = Request.Headers["Authorization"];

            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var headerValue))
            {
                var token = headerValue.Parameter;
                var managementApiClient = new ManagementApiClient(token, _configuration.Domain);

                var users = await managementApiClient.Users.GetUsersByEmailAsync(email);

                return new OkObjectResult(users);
            }

            return Unauthorized();
        }
    }
}