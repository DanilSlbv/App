using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.PresentationLayer.Controllers
{
    [Authorize]
    [Route("author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("getall/{page}")]
        public async Task<IActionResult> GetAll(int page, AscendingDescending sortBy)
        {
            var resultItems = await _authorService.GetAllSortedAsync(page, sortBy);
            return Ok(resultItems);
        }

        [HttpGet("createauthor/{author}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> CreateAuthor(string authorName)
        {
            var result = await _authorService.CreateAsync(authorName);
            return Ok(result);
        }

        [HttpGet("removeauthor/{id}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> RemoveAuthor(int id)
        {
            var result = await _authorService.RemoveAsync(id);
            return Ok(result);
        }

        [HttpPost("editauthor")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> EditAuthor(AuthorModelItem editAuthorModelItem)
        {
           var result = await _authorService.EditAsync(editAuthorModelItem);
            return Ok(result);
        }
    }
}
