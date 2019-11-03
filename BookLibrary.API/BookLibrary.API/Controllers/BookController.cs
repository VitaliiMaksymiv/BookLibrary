using System;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.BLL.DTOs;
using BookLibrary.BLL.Factory;
using BookLibrary.BLL.Paginating;
using BookLibrary.BLL.Services.Interfaces;
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
        public async Task<IActionResult> Get([FromQuery] SampleFilterModel sampleFilterModel)
        {
            var result = new PagedCollectionResponse<BookDTO>();
            result.Items = await _bookService.GetPageAsync(sampleFilterModel);

            SampleFilterModel nextFilter = sampleFilterModel.Clone() as SampleFilterModel;
            nextFilter.Page += 1;
            String nextUrl = !_bookService.GetPageAsync(nextFilter).Result.Any() ? null : this.Url.Action("Get", null, nextFilter, Request.Scheme);

            SampleFilterModel previousFilter = sampleFilterModel.Clone() as SampleFilterModel;
            previousFilter.Page -= 1;
            String previousUrl = previousFilter.Page <= 0 ? null : this.Url.Action("Get", null, previousFilter, Request.Scheme);

            result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
            result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _bookService.GetAsync(id));
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