using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System;
using static PTDuc.WhereHouse.EntityModels.Enumeration;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLWishlist : BLBase<Wishlist, WishlistDTO>, IBLWishlist
    {
        IDLWishlist _DLWishlist;
        public BLWishlist(IDLWishlist DLWishlist, IMapper mapper) : base(DLWishlist, mapper)
        {
            _DLWishlist = DLWishlist;
        }

        public ServiceResult DeletePostWishlist(string wishListId, string userId)
        {
            var res = new ServiceResult();
            res.Data = false;
            var wishListItem = _DLWishlist.GetByID(wishListId);
            if (wishListItem != null)
            {
                if (wishListItem.UserId.ToString() == userId)
                {
                    var delSuccess = _DLWishlist.DeleteById(wishListId);
                    if (delSuccess)
                    {
                        res.Data = true;
                    }
                }
                else {
                    res.StatusCode = (int)ResultCode.NotHaveRight;
                }
            }
            else {
                res.StatusCode = (int)ResultCode.Failed;
            }
            return res;
        }

        public ServiceResult GetWishlistByUserId(Guid userId)
        {
            var res = new ServiceResult();
            res.Data = _DLWishlist.GetWishlistByUserId(userId);
            return res;
        }
    }
}
