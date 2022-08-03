const PTDConstants = {
    "StatusPost": // Trạng thái bài đăng 
    {
        "Created": 1,//Được tạo chưa phê duyệt
        "Accepted": 2, //bài dăng đã được duyệt 
        "Closed": 3, //Đóng bài đăng khi đã có người thuê
    },
    "StatusUser": // Trạng thái người dùng 
    {
        "Active": 1,//Người dùng hoạt động bình thường 
        "Blocked": 2, //Bị chặn bởi admin
    },
    "Role": // Vai trò trên trang
    {
        "User": 1,//Người dùng bình thường 
        "Admin": 2, //Quản trị hệ thống 
    }
}
export default PTDConstants;