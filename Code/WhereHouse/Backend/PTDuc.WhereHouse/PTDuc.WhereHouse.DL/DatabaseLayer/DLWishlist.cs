using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLWishlist : DLBase<Wishlist, WishlistDTO>, IDLWishlist
    {
        public DLWishlist(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
