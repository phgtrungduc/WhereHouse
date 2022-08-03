using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.Controllers
{
    public class WishlistController : BaseEntityController<Wishlist, WishlistDTO>
    {
        IBLWishlist _blWishlist;
        public WishlistController(IBLWishlist blWishlist) : base(blWishlist)
        {
            _blWishlist = blWishlist;
        }
        [HttpGet("GetWishlistByUserId")]
        public IActionResult GetWishlistByUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                return Ok(_blWishlist.GetWishlistByUserId(Guid.Parse(userId)));
            }
            return Ok(null);
        }
        [HttpDelete("DeletePostWishlist/{id}")]
        public IActionResult DeletePostWishlist(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                return Ok(_blWishlist.DeletePostWishlist(id, userId));
            }
            return Ok(null);
        }


    }
}
