using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLHouseType : BLBase<HouseType, HouseTypeDTO>, IBLHouseType
    {
        IDLHouseType _DLHouseType;
        public BLHouseType(IDLHouseType DLHouseType, IMapper mapper) : base(DLHouseType, mapper)
        {
            _DLHouseType = DLHouseType;
        }
    }
}
