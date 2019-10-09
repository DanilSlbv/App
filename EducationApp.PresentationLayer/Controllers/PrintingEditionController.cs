using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Price = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;
using Currency = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Currency;
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
        public async Task<IActionResult> GetAllItems(int page, Type type, Price price,Currency currency, float minPrice, float maxPrice)
        {
            var items =await _printingEditionService.GetAllWithAuthorAsync(page,type, price,currency, minPrice, maxPrice);
            return Ok(items);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetWithAuthorById(int id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(PrintingEditionModelItem editPrintingEditionModelItem)
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
        public async Task<IActionResult> EditItem(PrintingEditionModelItem editPrintingEditionModelItem)
        {
            await _printingEditionService.EditAsync(editPrintingEditionModelItem);
            return Ok(true);
        }
    }
}
