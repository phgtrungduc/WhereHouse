using PTDuc.WhereHouse.DBContext.Models;
using System.Collections;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLHouse : IDLBase<House>
    {
        IEnumerable<House> GetDeepData();
    }
}
