using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class Part
    {
       
        public int ID { get; set; }
        public string Name { get; set; }
        public int ObjectID { get; set; }

        public bool Status { get; set; }
        public int ScheduleID { get; set; }
        // public SchedulesUpdate SchedulesUpdate { get; set; }
        // public ICollection<SchedulesUpdate> SchedulesUpdates { get; set; }

        // public int MyProperty { get; set; }

    }
}
