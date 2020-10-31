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
    public class InkController : ControllerBase
    {
        private readonly IInkService _inkService;
        public InkController( IInkService inkService)
        {
            _inkService = inkService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Import([FromForm] IFormFile file2)
        {
            IFormFile file = Request.Form.Files["UploadedFile"];
            object createdBy = Request.Form["CreatedBy"];
            var datasList = new List<InkForImportExcelDto>();
            //var datasList2 = new List<UploadDataVM2>();
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
                        datasList.Add(new InkForImportExcelDto()
                        {
                            Supplier = workSheet.Cells[rowIterator, 1].Value.ToSafetyString(),
                            MaterialNO = workSheet.Cells[rowIterator, 2].Value.ToSafetyString(),
                            Name = workSheet.Cells[rowIterator, 3].Value.ToSafetyString(),
                            Code = workSheet.Cells[rowIterator, 4].Value.ToSafetyString(),
                            Process = workSheet.Cells[rowIterator, 5].Value.ToSafetyString(),
                            VOC = workSheet.Cells[rowIterator, 6].Value.ToSafetyString(),
                            Units = workSheet.Cells[rowIterator, 7].Value.ToDouble(),
                            DaysToExpiration = workSheet.Cells[rowIterator, 8].Value.ToInt(),
                        });
                    }
                }
                datasList.ForEach(item =>
                {
                    item.CreatedBy = userid;
                });

                await _inkService.ImportExcel(datasList);
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
            string filename = "InkTemplate.xlsx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/excelTemplate", filename);
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
            var inks = await _inkService.GetAllAsync();
            return Ok(inks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InkDto entity)
        {
            return Ok(await _inkService.Add(entity));
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


        [HttpPut]
        public async Task<IActionResult> Update(InkUpdateDto update)
        {
            if (await _inkService.UpdateAsync(update))
                return NoContent();
            return BadRequest($"Updating Kind {update.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _inkService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the Kind");
        }
    }
}