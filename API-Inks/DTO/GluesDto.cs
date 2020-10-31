using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class GluesDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PartID { get; set; }
        public int TreatmentWayID { get; set; }
        public int ScheduleID { get; set; }
        public string Part { get; set; }
        public string TreatmentWay { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
