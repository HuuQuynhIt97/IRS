using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INK_API.Models
{
    public class Role
    {
        public Role()
        {
            this.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
    }
}