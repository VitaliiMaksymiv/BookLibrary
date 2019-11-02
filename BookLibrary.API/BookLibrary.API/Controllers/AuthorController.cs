using System.Threading.Tasks;
using BookLibrary.BLL.DTOs;
using BookLibrary.BLL.Factory;
using BookLibrary.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IServiceFactory serviceFactory)
            : base()
        {
            _authorService = serviceFactory.AuthorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _authorService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _authorService.GetAsync(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _authorService.SearchAsync(search));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorDTO authorDTO)
        {
            return CreatedAtAction(nameof(Create), await _authorService.CreateAsync(authorDTO));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDTO authorDTO)
        {
            authorDTO.Id = id;
            return Json(await _authorService.UpdateAsync(authorDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}