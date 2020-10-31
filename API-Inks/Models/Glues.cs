using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class Glues
    {
        public Glues()
        {
            this.CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int PartID { get; set; }
        public int TreatmentWayID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
