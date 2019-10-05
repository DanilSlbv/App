using EducationApp.BusinessLogicLayer.Common.Pagination;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
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
        [HttpGet("getall/{page}")]
        public async Task<IActionResult> GetAllItems(int page)
        {
            var items = await _printingEditionService.GetAllAsync();
            var itemsWithPagination = new ItemsWithPagination<PrintingEditionModelItem>();
            var resultItems = itemsWithPagination.GetItems(page, items.Items);
            return Ok(resultItems);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(EditPrintingEditionModelItem editPrintingEditionModelItem)
        {
            await _printingEditionService.AddAsync(editPrintingEditionModelItem);
            return Ok(true);
        }
        [HttpPost("remove/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _printingEditionService.RemoveAsync(id);
            return Ok(true);
        }
        [HttpPost("edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditItem(EditPrintingEditionModelItem editPrintingEditionModelItem)
        {
            await _printingEditionService.EditAsync(editPrintingEditionModelItem);
            return Ok(true);
        }
    }
}
