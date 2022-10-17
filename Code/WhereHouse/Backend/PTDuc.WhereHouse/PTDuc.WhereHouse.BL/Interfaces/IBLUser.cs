using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLUser : IBLBase<User, UserDTO>
    {
        void AddUserToListOnline(UserDTO user);
        void RemoveUserFromListOnline(UserDTO user);
        List<UserDTO> GetListUserOnline();
        public ServiceResult ChangeStatus(string blockUserId, string adminId);
        public ServiceResult InsertAdmin(UserDTO user, string adminId);
        public ServiceResult InsertUser(UserDTO user);
        public ServiceResult GetListUserForAdmin(string adminId);
        public ServiceResult UpdateUser(string userId,UserDTO user);
        
    }
}
