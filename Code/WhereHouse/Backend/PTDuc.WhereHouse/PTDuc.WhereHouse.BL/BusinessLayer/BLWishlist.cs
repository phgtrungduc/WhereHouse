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
    public class BLWishlist : BLBase<Wishlist, WishlistDTO>, IBLWishlist
    {
        IDLWishlist _DLWishlist;
        public BLWishlist(IDLWishlist DLWishlist, IMapper mapper) : base(DLWishlist, mapper)
        {
            _DLWishlist = DLWishlist;
        }
    }
}
