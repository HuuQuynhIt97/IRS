using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class ChemicalForImportExcelDto
    {
        public ChemicalForImportExcelDto()
        {
            this.CreatedDate = DateTime.Now;
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string MaterialNO { get; set; }
        public string Unit { get; set; }
        public double Units { get; set; }
        public string VOC { get; set; }
        public int DaysToExpiration { get; set; }
        public string Supplier { get; set; }
        public int SupplierID { get; set; }
        public string Process { get; set; }
        public int ProcessID { get; set; }
        public bool Modify { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
