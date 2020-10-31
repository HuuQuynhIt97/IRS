using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class SchedulePartDto
    {
        public int ID { get; set; }
        public int ScheduleID { get; set; }
        public int PartID { get; set; }
    }
}
