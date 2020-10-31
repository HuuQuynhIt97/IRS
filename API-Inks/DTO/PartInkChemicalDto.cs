using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class PartInkChemicalDto
    {
        public string name { get; set; }
        public int glueID { get; set; }
        public int partID { get; set; }
        public int treatmentWayID { get; set; }

        public List<PartInkChemicals> listAdd;

    }

    public class PartInkChemicals
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string subname { get; set; }
        public int percentage { get; set; }

    }
    
}
