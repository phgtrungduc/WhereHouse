using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLWishlist : DLBase<Wishlist, WishlistDTO>, IDLWishlist
    {
        public DLWishlist(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public bool CheckInWishlist(string userId, string postId)
        {
            _dbSet = _context.Set<Wishlist>();
            var data = _dbSet.Where(x => x.UserId == Guid.Parse(userId)&&x.PostId == Guid.Parse(postId)).FirstOrDefault();
            if (data != null) return true;
            return false;
        }

        public List<Wishlist> GetWishlistByUserId(Guid userId)
        {
            var res = default(IEnumerable<Wishlist>);
            _dbSet = _context.Set<Wishlist>();
            var data = _dbSet.Where(x => x.UserId == userId);
            if (data != null && data.Count() > 0)
            {
                res = data.Include(x => x.Post).ThenInclude(post => post.House).ThenInclude(house=>house.HouseImage);
            }
            return res?.ToList();
        }
    }
}
