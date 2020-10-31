using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class ScheduleDtoForImportExcel
    {
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public string ArticleNo { get; set; }
        public string Process { get; set; }
        public string Object { get; set; }
        public string Part { get; set; }
        public List<listPart> listPart;
        // public DateTime ProductionDate { get; set; }
        public DateTime ProductionDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class listPart
    {
        public string display { get; set; }
        public string value { get; set; }

    }
}
