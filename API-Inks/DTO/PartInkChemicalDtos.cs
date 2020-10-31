using INK_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class PartInkChemicalDtos
    {
        public int ID { get; set; }
        public int PartID { get; set; }
        public int InkID { get; set; }
        public int ChemicalID { get; set; }
        public int Percentage { get; set; }


    }

    
}
