using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class InkTblObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProcessID { get; set; }
        public Process Process { get; set; }
        public ICollection<Schedules> Schedules { get; set; }
        // public ICollection<ModelNo> ModelNos { get; set; }
    }
}
