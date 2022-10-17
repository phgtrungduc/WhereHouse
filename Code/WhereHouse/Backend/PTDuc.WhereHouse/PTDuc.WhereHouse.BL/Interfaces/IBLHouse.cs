using PTDuc.WhereHouse.DBContext.Models;
using System.Collections.Generic;
using PTDuc.WhereHouse.EntityModels.DTO;
using PTDuc.WhereHouse.EntityModels;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLHouse : IBLBase<House,HouseDTO>
    {
        IEnumerable<House> GetDeepData();

        bool AddNewPost(HouseDTO dataForPost);
        ServiceResult UpdatePost(string userid, HouseDTO dataForPost);
    }
    
}

