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
        [HttpGet("GetPublicPost/{id}")]
        public IActionResult GetPublicPost(string id)
        {
            var res = _blPost.GetPublicPost(id);
            return this.HandleResponse(res);
        }

        [HttpGet("GetDetailPost/{id}")]
        public IActionResult GetDetailPost(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.GetDetailPost(id,userId);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
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
            return BadRequest(new ServiceResult() { Data = false });
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
            return BadRequest(new ServiceResult() { Data = false });
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
                var res = _blPost.AcceptPost(id, userId);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpGet("GetListPostForAdmin")]
        public IActionResult GetListPostForAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.GetListPostForAdmin(userId);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpPost("ReportPost")]
        public IActionResult ReportPost([FromBody] ReportDTO report)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.ReportPost(userId, report);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpPost("ChangeStatusReport")]
        public IActionResult ChangeStatusReport([FromBody] ReportDTO report)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _blPost.ChangeStatusReport(userId, report.ReportId.ToString());
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }
    }
}