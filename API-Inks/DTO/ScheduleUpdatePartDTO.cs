using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class ScheduleUpdatePartDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ScheduleID { get; set; }
        public bool Status { get; set; }
        

    }
}
