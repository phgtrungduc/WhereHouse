using PTDuc.WhereHouse.DBContext.Models;
using System.Collections.Generic;
using PTDuc.WhereHouse.EntityModels.DTO;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLHouse : IBLBase<House,HouseDTO>
    {
        IEnumerable<House> GetDeepData();
    }
    
}

