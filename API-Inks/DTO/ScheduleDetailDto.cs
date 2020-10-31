using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class ScheduleDetailDto
    {
        public int ID { get; set; }
        public int ModelNameID { get; set; }
        public int ModelNoID { get; set; }
        public int ArticleNoID { get; set; }
        public int ArtProcessID { get; set; }
        public int ObjectInkID { get; set; }
        public int InkTblObjectID { get; set; }
        public int ProcessID { get; set; }

        public int PartID { get; set; }
        public string ModelName { get; set; }
        public string Treatment { get; set; }
        public string ObjectInk { get; set; }
        public string Part { get; set; }
        public string ModelNo { get; set; }
        public string ArticleNo { get; set; }
        public bool ApprovalStatus { get; set; }
        public bool FinishedStatus { get; set; }
        public int ApprovalBy { get; set; }
        public int CreatedBy { get; set; }

        public string Season { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductionDate { get; set; }
        public DateTime? EstablishDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? UpdateTime { get; set; }

        public List<Supplier> Supplier { get; set; }
        public List<Part> Parts { get; set; }
    }
}
