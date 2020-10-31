using System;
using System.Security.Claims;
using System.Threading.Tasks;
using INK_API.Helpers;
using INK_API._Services.Interface;
using INK_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Linq;

namespace INK_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IChemicalService _chemicalService;
        private readonly IScheduleService _scheduleService;
        private readonly IInkService _inkService;
        public ScheduleController(IScheduleService scheduleService, IChemicalService chemicalService, IInkService inkService)
        {
            _inkService = inkService;
            _chemicalService = chemicalService;
            _scheduleService = scheduleService;
        }

        [HttpPost("{scheduleID}/{userid}")]
        public async Task<IActionResult> Reject(int scheduleID, int userid)
        {
            return Ok(await _scheduleService.Reject(scheduleID, userid));

        }

        [HttpPost("{scheduleID}/{userid}")]
        public async Task<IActionResult> Release(int scheduleID, int userid)
        {
            return Ok(await _scheduleService.Release(scheduleID, userid));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailSchedule(int id)
        {
            var detail = await _scheduleService.GetDetailSchedule(id);
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> EditSchedule(ScheduleUpdateEditDto obj)
        {
            var detail = await _scheduleService.EditSchedule(obj);
            return Ok(detail);
        }
        

        [HttpPost]
        public async Task<IActionResult> EditPartSchedule(ScheduleUpdatePartDTO obj)
        {
            var detail = await _scheduleService.EditPartSchedule(obj);
            return Ok(detail);
        }

        [HttpPost("{bpfcID}")]
        public async Task<IActionResult> Done(int bpfcID)
        {
            return Ok(await _scheduleService.Done(bpfcID));
        }

        [HttpPost("{value}/{scheduleID}")]
        public async Task<IActionResult> UpdateProductionDate(string value , int scheduleID)
        {
            return Ok(await _scheduleService.UpdateProductionDate(value,scheduleID));
        }

        [HttpPost("{scheduleID}/{userid}")]
        public async Task<IActionResult> Approve(int scheduleID, int userid)
        {
            return Ok(await _scheduleService.Approve(scheduleID, userid));
        }
        
        [HttpPost]
        public async Task<ActionResult> Import([FromForm] IFormFile file2)
        {
            IFormFile file = Request.Form.Files["UploadedFile"];
            object createdBy = Request.Form["CreatedBy"];
            var dataList = new List<ScheduleDtoForImportExcel>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if ((file != null) && (file.Length > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                int userid = createdBy.ToInt();
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {
                        var obj = workSheet.Cells[rowIterator, 7].Value;
                        var proDate = DateTime.MinValue;
                        if (obj != null)
                        {
                            if (obj.GetType() == typeof(string))
                            {
                                proDate = obj.ToSafetyString().ToDateTime();
                            }
                            else if (obj.GetType() == typeof(DateTime))
                            {
                                proDate = (DateTime)obj;
                            }
                            else
                            {
                                proDate = DateTime.FromOADate(obj.ToLong());
                            }

                            dataList.Add(new ScheduleDtoForImportExcel()
                            {
                                ModelName = workSheet.Cells[rowIterator, 1].Value.ToSafetyString(),
                                ModelNo = workSheet.Cells[rowIterator, 2].Value.ToSafetyString(),
                                ArticleNo = workSheet.Cells[rowIterator, 3].Value.ToSafetyString(),
                                Process = workSheet.Cells[rowIterator, 4].Value.ToSafetyString(),
                                Object = workSheet.Cells[rowIterator, 5].Value.ToSafetyString(),
                                Part = workSheet.Cells[rowIterator, 6].Value.ToSafetyString(),
                                ProductionDate = proDate,
                            });
                        }
                    }
                }
                dataList.ForEach(item =>
                {
                    item.CreatedBy = userid;
                });

                await _scheduleService.ImportExcel(dataList);
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ExcelExport()
        {
            string filename = "ScheduleTemplate.xlsx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/excelTemplate", filename);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/octet-stream"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inks = await _scheduleService.GetAllAsync();
            return Ok(inks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChemicalDto entity)
        {
            return Ok(await _chemicalService.Add(entity));
            // if (await _ingredientService.CheckExists(entity.ID))
            //     return BadRequest("Ingredient ID already exists!");
            // if (await _ingredientService.CheckBarCodeExists(entity.Code))
            //     return BadRequest("Ingredient Barcode already exists!");
            // if (await _ingredientService.CheckExistsName(entity.Name))
            //     return BadRequest("Ingredient Name already exists!");
            // entity.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss tt");
            // if (await _ingredientService.Add1(entity))
            // {
            //     return NoContent();
            // }

            // throw new Exception("Creating the brand failed on save");
        }

        [HttpPost]
        public async Task<object> CreateSchedule(ScheduleDtoForImportExcel entity)
        {
            return Ok( await _scheduleService.CreateSchedule(entity));
            // if (await _ingredientService.CheckExists(entity.ID))
            //     return BadRequest("Ingredient ID already exists!");
            // if (await _ingredientService.CheckBarCodeExists(entity.Code))
            //     return BadRequest("Ingredient Barcode already exists!");
            // if (await _ingredientService.CheckExistsName(entity.Name))
            //     return BadRequest("Ingredient Name already exists!");
            // entity.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss tt");
            // if (await _ingredientService.Add1(entity))
            // {
            //     return NoContent();
            // }

            // throw new Exception("Creating the brand failed on save");
        }


        // [HttpPut]
        // public async Task<IActionResult> Update(KindDto update)
        // {
        //     //if (await _chemicalService.Update(update))
        //     //    return NoContent();
        //     return BadRequest($"Updating Kind {update.ID} failed on save");
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _scheduleService.Delete(id));
            //if (await _chemicalService.Delete(id))
            //    return NoContent();
            throw new Exception("Error deleting the Kind");
        }
    }
}