using System;
namespace INK_API.Models
{
    public class PartInkChemical
    {
        public PartInkChemical()
        {
            this.CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int InkID { get; set; }
        public int GlueID { get; set; }
        public int ChemicalID { get; set; }
        public int Percentage { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
