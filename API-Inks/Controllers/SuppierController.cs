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
    public class SuppierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetLines([FromQuery]PaginationParams param)
        //{
        //    var lines = await _lineService.GetWithPaginations(param);
        //    Response.AddPagination(lines.CurrentPage,lines.PageSize,lines.TotalCount,lines.TotalPages);
        //    return Ok(lines);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lines = await _supplierService.GetAllAsync();
            return Ok(lines);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithName()
        {
            var lines = await _supplierService.GetAllWithName();
            return Ok(lines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByTreatment(int id)
        {
            var lines = await _supplierService.GetAllByTreatment(id);
            return Ok(lines);
        }

        //[HttpGet("{text}")]
        //public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        //{
        //    var lists = await _lineService.Search(param, text);
        //    Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
        //    return Ok(lists);
        //}
       
        [HttpPost]
        public async Task<IActionResult> Create(SuppilerDto create)
        {

            if (_supplierService.GetById(create.ID) != null)
                return BadRequest("Line ID already exists!");
            //create.CreatedDate = DateTime.Now;
            if (await _supplierService.Add(create))
            {
                return NoContent();
            }

            throw new Exception("Creating the model name failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> Update(SuppilerDto update)
        {
            if (await _supplierService.Update(update))
                return NoContent();
            return BadRequest($"Updating model name {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _supplierService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the model name");
        }
    }
}