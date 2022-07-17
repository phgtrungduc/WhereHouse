using PTDuc.WhereHouse.DBContext.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class UserDTO
    {

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Guid? AvatarId { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public virtual File Avatar { get; set; }
        public bool? IsOnline { get; set; }

    }
}
