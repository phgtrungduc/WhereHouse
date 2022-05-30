using PTDuc.WhereHouse.DBContext.Models;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLHouse : IBLBase<House>
    {
        IEnumerable<House> GetDeepData();
    }
}

