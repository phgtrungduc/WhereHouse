using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.EntityModels
{
    public class Enumeration
    {
        public enum ResultCode
        {
            Failed = 0,
            Success = 1,    
            Authenticated = 204,   
            UserNotFound = 205,    
            PasswordNotCorrect = 206,
            NotTrueParam = 199,
            NotExistFile  = 207,
            NotExistFolder = 208,
            NotHaveRight = 209,
            PostNotPay = 210,

        }

        public enum HouseType
        {
            Street =1,//nhà mặt phố
            Alley = 2 , //Ngõ, hẻm
            Villa = 3, //Nhà biệt thự
            AdjoiningStreet =4 // Nhà phố liền kề
        }
        public enum StatusPost // Trạng thái bài đăng 
        {
            Created = 1,//Được tạo chưa phê duyệt
            Pay = 2,//Được tạo chưa phê duyệt
            Accepted = 3, //bài dăng đã được duyệt 
            Closed = 4, //Đóng bài đăng khi đã có người thuê
        }
        public enum StatusUser // Trạng thái người dùng 
        {
            Active = 1,//Người dùng hoạt động bình thường 
            Blocked = 2, //Bị chặn bởi admin
        }

        public enum Role // Vai trò trên trang
        {
            User = 1,//Người dùng bình thường 
            Admin = 2, //Quản trị hệ thống 
        }

        public enum StatusReport // Trạng thái báo cáo 
        {
            Created = 1,//Chưa giải quyết 
            Solved = 2, //Đã giải quyết 
        }
    }
}
