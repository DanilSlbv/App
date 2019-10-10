using EducationApp.BusinessLogicLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace EducationApp.PresentationLayer.Controllers
{
    [ApiController]
    [Route("printingedition")]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        
        [HttpPost("getall/{page}")]
        public async Task<IActionResult> GetAllItems(int page, PrintingEditionFilterModel filterModel)
        {
            var items =await _printingEditionService.GetAllWithAuthorAsync(page, filterModel);
            return Ok(items);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost("create")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> CreateItem(PrintingEditionModelItem printingEdition)
        {
            var result = await _printingEditionService.CreateAsync(printingEdition);
            return Ok(result);
        }

        [HttpGet("remove/{id}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var result = await _printingEditionService.RemoveAsync(id);
            return Ok(result);
        }

        [HttpPost("edit")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> EditItem(PrintingEditionModelItem editPrintingEditionModelItem)
        {
            var result = await _printingEditionService.EditAsync(editPrintingEditionModelItem);
            return Ok(result);
        }
    }
}
