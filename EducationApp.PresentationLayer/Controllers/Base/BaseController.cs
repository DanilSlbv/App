using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers.Base
{

    public class BaseController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Get");
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok("Post");
        }
        [HttpPut]
        public async Task<IActionResult> Put()
        {
            return Ok("Put");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok("Delete");
        }
    }
}
