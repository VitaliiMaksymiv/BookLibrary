using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IServiceFactory serviceFactory)
            : base()
        {
            _bookService = serviceFactory.BookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _bookService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _bookService.GetAsync(id));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _bookService.SearchAsync(search));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO bookDTO)
        {
            return CreatedAtAction(nameof(Create), await _bookService.CreateAsync(bookDTO));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDTO bookDTO)
        {
            bookDTO.Id = id;
            return Json(await _bookService.UpdateAsync(bookDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}