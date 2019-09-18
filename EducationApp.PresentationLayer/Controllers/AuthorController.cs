using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("author")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorController:ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _authorService.GetAllAsync();
            return Ok(items);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _authorService.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpGet]
        [Route("getbyname/{name}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetByName(string name)
        {
            var item = await _authorService.GetByNameASync(name);
            return Ok(item);
        }
        [HttpPost("addauthor")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAuthor(AddAuthorModelItem addAuthorModelItem)
        {
            await _authorService.AddAsync(addAuthorModelItem);
            return Ok(true);
        }
        [HttpPost("deleteauthor/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            await _authorService.DeleteAsync(id);
            return Ok(true);
        }
        [HttpPost("editauthor")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditAuthor(EditAuthorModelItem editAuthorModelItem)
        {
            await _authorService.EditAsync(editAuthorModelItem);
            return Ok(true);
        }
    }
}
