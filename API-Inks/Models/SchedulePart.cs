using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class SchedulePart
    {
        public SchedulePart()
        {
            this.CreatedDate = DateTime.Now;
        }

        public int ID { get; set; }
        public int ScheduleID { get; set; }
        public int PartID { get; set; }
        public DateTime CreatedDate { get; set; }
   
        // public ICollection<Part> Parts { get; set; }

    }
}
