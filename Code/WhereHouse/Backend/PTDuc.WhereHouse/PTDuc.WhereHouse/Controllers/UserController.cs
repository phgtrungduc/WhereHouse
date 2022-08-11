using PTDuc.WhereHouse.BL.BusinessLayer;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using PTDuc.WhereHouse.EntityModels;
using System;

namespace PTDuc.WhereHouse.Controllers
{
    public class UserController
        : BaseEntityController<User, UserDTO>
    {
        IBLUser _blUser;
        public UserController(IBLUser blUser) : base(blUser)
        {
            _blUser = blUser;
        }
        [AllowAnonymous]
        public override IActionResult Post([FromBody] UserDTO entity)
        {
            return base.Post(entity);
        }
        [HttpPost("InsertAdmin")]
        public IActionResult InsertAdmin([FromBody] UserDTO user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.InsertAdmin(user, userId);
                return (this.HandleResponse(res));
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpPost("ChangeStatus")]
        public IActionResult ChangeStatus([FromBody] UserDTO user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var adminId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.ChangeStatus(user.UserId.ToString(), adminId);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpGet("GetListUserForAdmin")]
        public IActionResult GetListUserForAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var adminId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.GetListUserForAdmin(adminId);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpGet("GetUserConfig")]
        public IActionResult GetUserConfig()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.GetByID(userId);
                return Ok(res);
            }
            return BadRequest(new ServiceResult() { Data = false }) ;
        }
        public override IActionResult Delete(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var res = new ServiceResult();
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                if (!string.IsNullOrEmpty(userId)) {
                    var user = _blUser.GetByID(userId);
                    try
                    {
                        if (user != null)
                        {
                            if (user.Role == (int)Enumeration.Role.Admin)
                            {
                                var delSucess = _blUser.Delete(id);
                                if (delSucess)
                                {
                                    res.Data = true;
                                    return Ok(res);
                                }
                                else
                                {
                                    res.Data = false;
                                    return BadRequest(res);
                                }
                            }
                            else
                            {
                                res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                                res.Messenger = "Tài khoản không có quyền chặn người dùng";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        res.Messenger = ex.Message;
                        res.StatusCode = (int)(Enumeration.ResultCode.Failed);
                    }
                }
                
                
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserDTO user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.UpdateUser(userId, user);
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }
    }
}
