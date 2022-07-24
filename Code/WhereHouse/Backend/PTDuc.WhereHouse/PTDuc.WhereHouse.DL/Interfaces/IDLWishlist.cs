﻿using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLWishlist : IDLBase<Wishlist, WishlistDTO>
    {
        public IEnumerable<Wishlist> GetWishlistByUserId(Guid userId);
    }
}
