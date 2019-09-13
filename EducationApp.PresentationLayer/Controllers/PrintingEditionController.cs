using EducationApp.BusinessLogicLayer.Models.Enums;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("printingedition")]
    public class PrintingEditionController:ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;
        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }
        [HttpGet]
        [Route("getallitems")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _printingEditionService.GetAllAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("getbyid")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _printingEditionService.GetByIdAsync(id);
            return Ok(item);
        }
        [HttpGet]
        [Route("getbyprice")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetByPriceAsync(float min , float max)
        {
            var item = await _printingEditionService.GetItemsByPriceAsync(min,max);
            return Ok(item);
        }
        [HttpGet]
        [Route("getbytype")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetByTypeAsync(TypeModel typeModel)
        {
            var items = await _printingEditionService.GetItemsByTypeAsync(typeModel);
            return Ok(items);
        }
        [HttpGet]
        [Route("sortbypriceasc")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> SortByPriceAscAsync()
        {
            var items = await _printingEditionService.SortItemsByPriceAscAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("sortbypricedesc")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> SortByPriceDescAsync()
        {
            var items = await _printingEditionService.SortItemsByPriceDescAsync();
            return Ok(items);
        }
        [HttpPost]
        [Route("additem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem(PrintingEditionItemModel printingEditionItemModel)
        {
            await _printingEditionService.AddItemAsync(printingEditionItemModel);
            return Ok("AddItem");
        }
        [HttpPost]
        [Route("deleteitem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _printingEditionService.DeleteItemAsync(id);
            return Ok("DeleteItem");
        }
        [HttpPost]
        [Route("edititem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditItem(PrintingEditionItemModel printingEditionItemModel)
        {
            await _printingEditionService.EditItemAsync(printingEditionItemModel);
            return Ok("EditItem");
        }
    }
}
