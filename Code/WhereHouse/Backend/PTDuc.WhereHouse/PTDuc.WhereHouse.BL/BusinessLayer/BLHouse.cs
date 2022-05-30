using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLHouse : BLBase<House>, IBLHouse
    {
        IDLHouse _dlHouse;
        public BLHouse(IDLHouse dLHouse) : base(dLHouse)
        {
            _dlHouse = dLHouse;
        }

        public IEnumerable<House> GetDeepData()
        {
            return _dlHouse.GetDeepData();
        }
    }
}
