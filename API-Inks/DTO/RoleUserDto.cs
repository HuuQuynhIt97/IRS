using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INK_API.DTO
{
    public class RoleUserDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public bool? Status { get; set; }
    }
}
