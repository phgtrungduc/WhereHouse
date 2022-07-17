using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLUser : IBLBase<User, UserDTO>
    {
        void AddUserToListOnline(UserDTO user);
        void RemoveUserFromListOnline(UserDTO user);
        List<UserDTO> GetListUserOnline();
    }
}
