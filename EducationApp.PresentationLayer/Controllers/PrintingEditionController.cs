using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("printingedition")]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        [HttpGet("getallitems")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _printingEditionService.GetAllAsync();
            return Ok(items.Items);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpGet("getbyprice/{minPrice}&{maxPrice}")]
        public async Task<IActionResult> GetByPriceAsync(float minPrice , float maxPrice)
        {
            var item = await _printingEditionService.GetByPriceAsync(minPrice,maxPrice);
            return Ok(item.Items);
        }
        [HttpGet("getbytype/{typeModel}")]
        public async Task<IActionResult> GetByTypeAsync(Type typeModel)
        {
            var items = await _printingEditionService.GetByTypeAsync(typeModel);
            return Ok(items);
        }
        [HttpGet("sortbypriceasc")]
        public async Task<IActionResult> SortByPriceAscendingAsync()
        {
            var items = await _printingEditionService.SortByPriceAscendingAsync();
            return Ok(items.Items);
        }
        [HttpGet("sortbypricedesc")]
        public async Task<IActionResult> SortByPriceDescendingAsync()
        {
            var items = await _printingEditionService.SortByPriceDescendingAsync();
            return Ok(items.Items);
        }
        [HttpPost("additem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(AddPrintingEditionModelItem addPrintingEditionModelItem)
        {
            await _printingEditionService.AddAsync(addPrintingEditionModelItem);
            return Ok(true);
        }
        [HttpPost("deleteitem/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _printingEditionService.DeleteAsync(id);
            return Ok(true);
        }
        [HttpPost("edititem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditItem(EditPrintingEditionModelItem editPrintingEditionModelItem)
        {
            await _printingEditionService.EditAsync(editPrintingEditionModelItem);
            return Ok(true);
        }
    }
}
