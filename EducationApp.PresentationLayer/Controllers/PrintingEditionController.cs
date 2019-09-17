using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/printingedition")]
    public class PrintingEditionController:ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        [HttpGet("/getallitems")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _printingEditionService.GetAllAsync();
            return Ok(items);
        }
        [HttpGet("/getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpGet("/getbyprice")]
        public async Task<IActionResult> GetByPriceAsync(float min , float max)
        {
            var item = await _printingEditionService.GetByPriceAsync(min,max);
            return Ok(item);
        }
        [HttpGet("/getbytype")]
        public async Task<IActionResult> GetByTypeAsync(Type typeModel)
        {
            var items = await _printingEditionService.GetByTypeAsync(typeModel);
            return Ok(items);
        }
        [HttpGet("/sortbypriceasc")]
        public async Task<IActionResult> SortByPriceAscendingAsync()
        {
            var items = await _printingEditionService.SortByPriceAscendingAsync();
            return Ok(items);
        }
        [HttpGet("/sortbypricedesc")]
        public async Task<IActionResult> SortByPriceDescendingAsync()
        {
            var items = await _printingEditionService.SortByPriceDescendingAsync();
            return Ok(items);
        }
        [HttpPost("/additem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(PrintingEditionModelItem printingEditionItemModel)
        {
            await _printingEditionService.AddAsync(printingEditionItemModel);
            return Ok(true);
        }
        [HttpPost("/deleteitem/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _printingEditionService.DeleteAsync(id);
            return Ok(true);
        }
        [HttpPost("/edititem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditItem(PrintingEditionModelItem printingEditionItemModel)
        {
            await _printingEditionService.EditAsync(printingEditionItemModel);
            return Ok(true);
        }
    }
}
