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
    public class MessageController : BaseEntityController<Message, MessageDTO>
    {
        IBLMessage _blMessage;
        public MessageController(IBLMessage blMessage) : base(blMessage)
        {
            _blMessage = blMessage;
        }

        [HttpGet("GetConversation")]
        public virtual IActionResult GetConversation(MessageDTO message)
        {

            return Ok(_blMessage.GetConversation(message));
        }

    }
}
