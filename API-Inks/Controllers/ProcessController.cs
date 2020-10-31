using INK_API._Services.Interface;
using INK_API.DTO;
using INK_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;
        private readonly ITreatmentWayService _treatmentWayService;
        public ProcessController(IProcessService processService, ITreatmentWayService treatmentWayService)
        {
            _processService = processService;
            _treatmentWayService = treatmentWayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var processes = await _processService.GetAllAsync();
            return Ok(processes);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTreatmentWay()
        {
            var processes = await _treatmentWayService.GetAllAsync();
            return Ok(processes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTreatmentWay(TreatmentWayDto create)
        {

            if (_treatmentWayService.GetById(create.ID) != null)
                return BadRequest("Treatment ID already exists!");
            //create.CreatedDate = DateTime.Now;
            if (await _treatmentWayService.Add(create))
            {
                return NoContent();
            }

            throw new Exception("Creating the Treatment failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTreatmentWay(TreatmentWayDto update)
        {
            if (await _treatmentWayService.Update(update))
                return NoContent();
            return BadRequest($"Updating Treatment Way {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentWay(int id)
        {
            if (await _treatmentWayService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the Treatment Way");
        }


     
    }
}
