using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class Schedules
    {
        public Schedules()
        {
            if (CreatedDate == DateTime.MinValue)
                CreatedDate = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        public int ID { get; set; }
        public int ModelNameID { get; set; }
        public int ModelNoID { get; set; }
        public int ArticleNoID { get; set; }
        public int ArtProcessID { get; set; }
        public int ObjectInkID { get; set; }
        public int InkTblObjectID { get; set; }

        public bool ApprovalStatus { get; set; }
        public bool FinishedStatus { get; set; }
        public int ApprovalBy { get; set; }
        public int CreatedBy { get; set; }
        public string Season { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? EstablishDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? UpdateTime { get; set; }

        public InkTblObject InkTblObject { get; set; }
        // public ICollection<Part> Parts { get; set; }

    }
}
