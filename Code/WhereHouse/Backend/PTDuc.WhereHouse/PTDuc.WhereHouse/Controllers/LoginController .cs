using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using PTDuc.WhereHouse.BL.Interfaces.Login;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        IBLLogin _bLLogin;
        public LoginController(IBLLogin bLLogin)
        {
            _bLLogin = bLLogin;
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] LoginParam loginParam)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = _bLLogin.ChangePassword(userId,loginParam.Password,loginParam.NewPassword);
                if (res.StatusCode == (int)Enumeration.ResultCode.Success)
                {
                    return Ok(res);
                }
                else {
                    res.Data = false;
                    return BadRequest(res);
                }
            }
            return BadRequest(new ServiceResult() { Data = false });
        }



        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginParam param)
        {
            var res = new ServiceResult();
            try
            {
                res = _bLLogin.Login(param);
            }
            catch (Exception e)
            {
                res.Messenger = e.Message;
            }
            return Ok(res);
        }
    }
}
