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
    public class DLMessage : DLBase<Message, MessageDTO>, IDLMessage
    {
        IDLUser _dLUser;
        public DLMessage(WhereHouseContext context, IMapper mapper, IDLUser dLUser) : base(context, mapper)
        {
            _dLUser = dLUser;
        }

        public object GetConversation(MessageDTO message)
        {
            //var userSendId = message.SenderId;
            //var userReceivedId = message.ReceiverId;
            //_dbSet = _context.Set<Message>();
            //var dataMessage = _dbSet.Where(x => (x.SenderId == userSendId && x.ReceiverId == userReceivedId) || (x.SenderId == userReceivedId && x.ReceiverId == userSendId));
            //var resMessage = _mapper.Map<List<Message>, IEnumerable<MessageDTO>>(dataMessage.ToList());
            //var userReceived = _dLUser.GetByID(userReceivedId.ToString());
            //var res = new { Message = resMessage.OrderBy(x => x.Time), UserReceived = userReceived };
            return null;
        }
    }
}
