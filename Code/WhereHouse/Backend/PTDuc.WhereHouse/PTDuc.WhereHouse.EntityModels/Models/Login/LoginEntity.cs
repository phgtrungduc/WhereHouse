using PTDuc.WhereHouse.DBContext;
using PTDuc.WhereHouse.EntityModels.Attribute;

namespace PTDuc.WhereHouse.EntityModels
{
    public class LoginEntity : BaseEntity
    {
        [Required]
        [System.ComponentModel.DisplayName("Tên đăng nhập")]
        public string Username { get; set; }
        [Required]
        [System.ComponentModel.DisplayName("Mật khẩu")]
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
