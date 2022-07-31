using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.ChatHub;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.Controllers
{
    public class PostController : BaseEntityController<Post, PostDTO>
    {
        IBLPost _blPost;
        public PostController(IBLPost blPost) : base(blPost)
        {
            _blPost = blPost;

        }

        [AllowAnonymous]
        public override IActionResult Get()
        {
            return base.Get();
        }

        [AllowAnonymous]
        public override IActionResult GetByPaging(int page, int pageSize)
        {
            return base.GetByPaging(page, pageSize);
        }

        [AllowAnonymous]
        public override IActionResult GetByID(string id)
        {
            return base.GetByID(id);
        }

        [HttpGet("GetUserPost")]
        public IActionResult GetUserPost()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.GetUserPost(Guid.Parse(userId));
                return this.HandleResponse(res);
            }
            return Ok(null);
        }

        [HttpDelete("DeletePostUser/{id}")]
        public IActionResult DeletePostUser(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.DeletePostUser(id, userId);
                return this.HandleResponse(res);
            }
            return Ok(null);
        }
        [HttpPost("AcceptPost/{id}")]
        public IActionResult AcceptPost(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.DeletePostUser(id, userId);
                return this.HandleResponse(res);
            }
            return Ok(null);
        }
    }
}