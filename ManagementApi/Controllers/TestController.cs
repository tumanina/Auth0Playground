using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Token works!");
        }
    }
}