using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("author")]
    [ApiController]
    [Authorize]
    public class AuthorController:ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("getallitems")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _authorService.GetAllAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("getallitems")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(string id)
        {
            var items = await _authorService.GetByIdAsync(id);
            return Ok(items);
        }
        [HttpGet]
        [Route("getallitems")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetByName(string name)
        {
            var items = await _authorService.GetByNameASync(name);
            return Ok(items);
        }
        [HttpPost]
        [Route("addauthor")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> AddAuthor(AuthorItemModel authorItemModel)
        {
            await _authorService.AddItemAsync(authorItemModel);
            return Ok("AddAuthor");
        }
        [HttpPost]
        [Route("deleteauthor")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            await _authorService.DeleteItemAsync(id);
            return Ok("DeleteAuthor");
        }
        [HttpPost]
        [Route("editauthor")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> EditAuthor(AuthorItemModel authorItemModel)
        {
            await _authorService.EditItemAsync(authorItemModel);
            return Ok("EditAuthor");
        }
    }
}
