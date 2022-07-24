using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLWishlist : IBLBase<Wishlist, WishlistDTO>
    {
        public ServiceResult GetWishlistByUserId(Guid userId);
        public ServiceResult DeletePostWishlist(string wishListId, string userId);
    }
}
