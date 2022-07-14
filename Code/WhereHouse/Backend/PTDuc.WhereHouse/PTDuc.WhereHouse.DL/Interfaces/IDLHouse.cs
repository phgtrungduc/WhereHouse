using PTDuc.WhereHouse.DBContext.Models;
using System.Collections;
using System.Collections.Generic;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLHouse : IDLBase<House, HouseDTO>
    {
        IEnumerable<House> GetDeepData();
    }
}
