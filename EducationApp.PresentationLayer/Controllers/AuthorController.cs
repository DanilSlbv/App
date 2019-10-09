using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

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
        public async Task<IActionResult> GetAllAuthorWithPrintingEdition(int page,AscendingDescending sortBy)
        {
            var resultItems = await _authorService.GetAllSortedAsync(page, sortBy);
            return Ok(resultItems);
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
        public async Task<IActionResult> EditAuthor(AuthorModelItem editAuthorModelItem)
        {
            await _authorService.EditAsync(editAuthorModelItem);
            return Ok(true);
        }
    }
}
