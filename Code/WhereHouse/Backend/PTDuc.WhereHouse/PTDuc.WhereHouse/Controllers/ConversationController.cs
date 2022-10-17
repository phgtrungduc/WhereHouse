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
    public class ConversationController : BaseEntityController<Conversation, ConversationDTO>
    {
        IBLConversation _blConversation;
        public ConversationController(IBLConversation blConversation) : base(blConversation)
        {
            _blConversation = blConversation;
        }
        [HttpGet("InitChat")]
        public virtual IActionResult InitChat([FromQuery] string userReceivedId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = new ServiceResult();
                res.Data = _blConversation.InitChat(userId, userReceivedId);
                return Ok(res);
            }
            return null;


        }
        [HttpGet("GetAllConversationUser")]
        public virtual IActionResult GetAllConversationUser([FromQuery] string userReceivedId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var res = new ServiceResult();
                res.Data = _blConversation.GetAllConversationUser(userId);
                return Ok(res);
            }
            return null;


        }
    }


}
