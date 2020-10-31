using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INK_API.Models
{
    public class RoleUser
    {

        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool? Status { get; set; }
    }
}