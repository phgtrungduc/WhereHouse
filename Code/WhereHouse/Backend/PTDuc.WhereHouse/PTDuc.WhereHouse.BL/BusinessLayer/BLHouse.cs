using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer 
{
    public class BLHouse : BLBase<House,HouseDTO>, IBLHouse
    {
        IDLHouse _dlHouse;
        IBLWishlist _bLWishlist;
        public BLHouse(IDLHouse dLHouse,IBLWishlist bLWishlist, IMapper mapper) : base(dLHouse, mapper)
        {
            _dlHouse = dLHouse;
            _bLWishlist = bLWishlist;
        }

        public bool AddNewPost(HouseDTO dataForPost)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<House> GetDeepData()
        {
            return _dlHouse.GetDeepData();
        }

    }
}
