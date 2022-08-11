using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLUser :IDLBase<User, UserDTO>

    {
        User GetUserByUserName(string userName);
        List<UserDTO> GetAllForAdmin();
    }
}
