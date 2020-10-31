using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isShow { get; set; }
        public int ModifiedBy { get; set; }
        public int ProcessID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Process Process { get; set; }
        public ICollection<Process> Processes { get; set; }
    }
}
