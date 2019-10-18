using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.PresentationLayer.Controllers
{

    [Route("author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result= await _authorService.GetAll();
            return Ok(result);
        }

        [HttpGet("getallsorted/{page}")]
        public async Task<IActionResult> GetAllSorted(int page, AscendingDescending sortBy)
        {
            var resultItems = await _authorService.GetAllSortedAsync(page, sortBy);
            return Ok(resultItems);
        }


        [HttpPost("createauthor/{authorname}")]
        public async Task<IActionResult> CreateAuthor(string authorName)
        {
            var result = await _authorService.CreateAsync(authorName);
            return Ok(result);
        }

        [HttpPost("removeauthor/{id}")]
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
