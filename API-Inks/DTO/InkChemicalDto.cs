using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class InkChemicalDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }

        public int percentage { get; set; }

        public bool modify { get; set; }
        public string Code { get; set; }

    }
}
