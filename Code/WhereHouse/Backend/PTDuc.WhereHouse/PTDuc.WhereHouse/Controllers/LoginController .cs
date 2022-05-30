using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using PTDuc.WhereHouse.BL.Interfaces.Login;
using PTDuc.WhereHouse.DL.Models;
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
        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        // or
        //        var username = identity.FindFirst(ClaimTypes.Name).Value;

        //    }
        //    return new string[] { "value1", "value2" };
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login([FromBody] LoginEntity entity)
        //{
        //    //var token = _authenticationManager.Authenticate(username, password);
        //    var res = _loginService.Login(entity, _employeeService);
        //    if (res.StatusCode == BusinessLayer.Enums.StatusCode.Success)
        //    {
        //        return Ok(res);
        //    }
        //    return BadRequest(res);
        //}

        //[HttpPost("changepassword")]
        //public IActionResult ChangePassword([FromBody] string objectPass)
        //{
        //    var res = new ServiceResult();
        //    string username = "";
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        username = identity.FindFirst(ClaimTypes.Name).Value;

        //    }
        //    var serializer = new JavaScriptSerializer();
        //    var desObject = serializer.Deserialize<dynamic>(objectPass);
        //    var updateRes = _loginService.ChangePassword(username, desObject.oldPass, desObject.newPass, res);
        //    if (res.StatusCode == BusinessLayer.Enums.StatusCode.Success)
        //    {
        //        return Ok(res);
        //    }
        //    else
        //    {
        //        return BadRequest(res);
        //    }
        //}



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
            //_bLLogin.Login();
            //var check = PasswordHash.Verify(param);
            ////var text = System.Text.Encoding.UTF8.GetString(abc.HashPassword, 0, abc.HashPassword.Length);
            //return Ok(check);
            //Check password against a stored hash
            //byte[] hashBytes = //read from store.
            //PasswordHash hash = new PasswordHash(hashBytes);
            //if (!hash.Verify("newly entered password"))
            //    throw new System.UnauthorizedAccessException();
        }
    }
}
