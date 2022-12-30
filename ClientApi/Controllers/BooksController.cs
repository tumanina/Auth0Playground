using ClientApi.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BooksController : ControllerBase
    {
        public BooksController()
        {
        }

        [Route("books")]
        [Authorize(Roles = AuthRoles.ReadBooks)]
        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok("You have access to read books");
        }


        [Route("books")]
        [Authorize(Roles = AuthRoles.CreateBook)]
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync()
        {
            return Ok("You have access to create book");
        }


        [Route("books/{id}")]
        [Authorize(Roles = AuthRoles.DeleteBook)]
        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            return Ok("You have access to delete book");
        }
    }
}