using System;
using System.Threading.Tasks;
using BookLibrary.BLL.DTOs;
using BookLibrary.BLL.Factory;
using BookLibrary.BLL.Paginating;
using BookLibrary.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

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
        public async Task<IActionResult> Get([FromQuery] SampleFilterModel sampleFilterModel)
        {
            var result = new PagedCollectionResponse<AuthorDTO>();
            result.Items = await _authorService.GetPageAsync(sampleFilterModel);

            SampleFilterModel nextFilter = sampleFilterModel.Clone() as SampleFilterModel;
            nextFilter.Page += 1;
            String nextUrl = !_authorService.GetPageAsync(nextFilter).Result.Any() ? null : this.Url.Action("Get", null, nextFilter, Request.Scheme);

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
            return Json(await _authorService.GetAsync(id));
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