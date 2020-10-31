using System;
using System.Security.Claims;
using System.Threading.Tasks;
using INK_API.Helpers;
using INK_API._Services.Interface;
using INK_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INK_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GlueController : ControllerBase
    {
        private readonly IGluesService _gluesService;
        public GlueController(IGluesService gluesService)
        {
            _gluesService = gluesService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGlue(GluesDto create)
        {
            return Ok(await _gluesService.Add(create));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGlue(GluesDto update)
        {
            return Ok(await _gluesService.Update(update));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGluesByScheduleID(int id)
        {
            var brands = await _gluesService.GetGluesByScheduleID(id);
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInkChemicalByGlueID(int id)
        {
            var brands = await _gluesService.GetInkChemicalByGlueID(id);
            return Ok(brands);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _gluesService.Deletes(id))
                return NoContent();
            throw new Exception("Error deleting the Part");
        }

        [HttpGet(Name = "GetGlues")]
        public async Task<IActionResult> GetAll()
        {
            var glues = await _gluesService.GetAllAsync();
            return Ok(glues);
        }

        [HttpPut]
        public async Task<IActionResult> SaveGlue(PartInkChemicalDto update)
        {
            return Ok(await _gluesService.SaveGlue(update));
            
        }

    }
}