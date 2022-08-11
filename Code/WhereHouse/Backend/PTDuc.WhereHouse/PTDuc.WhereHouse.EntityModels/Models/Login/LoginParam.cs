using PTDuc.WhereHouse.EntityModels.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.EntityModels
{
    public class LoginParam:BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Salt { get; set; }
        public string HashPassword { get; set; }
    }
}
