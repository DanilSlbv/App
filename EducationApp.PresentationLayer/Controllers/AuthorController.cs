using EducationApp.BusinessLogicLayer.Common.Pagination;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("author")]
    [ApiController]
    public class AuthorController:ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("authorspage/{page}")]
        public async Task<IActionResult> GetAllAuthorWithPrintingEdition(int page)
        {
            var items = await _authorService.GetAuthorsWithPrintingEditions();
            var itemsWithPagination = new ItemsWithPagination<AuthorInPrintingEditionsModelItem>();
            var resultItems=itemsWithPagination.GetItems(page,items.Items);
            return Ok(resultItems);
        }

        [HttpGet]
        [Route("getbyname/{name}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetByName(string name)
        {
            var item = await _authorService.GetByNameASync(name);
            return Ok(item);
        }
        [HttpPost("addauthor/{authorname}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAuthor(string authorName)
        {
            var result=await _authorService.AddAsync(authorName);
            return Ok(true);
        }
        [HttpPost("deleteauthor/{id}")]
        
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.RemoveAsync(id);
            return Ok(true);
        }
        [HttpPost("editauthor")]
        public async Task<IActionResult> EditAuthor(EditAuthorModelItem editAuthorModelItem)
        {
            await _authorService.EditAsync(editAuthorModelItem);
            return Ok(true);
        }
    }
}
