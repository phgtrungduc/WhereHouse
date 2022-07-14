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
    public class DLConversation : DLBase<Conversation, ConversationDTO>, IDLConversation
    {
        public DLConversation(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
        public override Conversation GetByID(string Id)
        {
            _dbSet = _context.Set<Conversation>();
            var data = _dbSet.Where(x => x.ConversationId == Guid.Parse(Id)).Include(x => x.Messages).FirstOrDefault();
            return data;
        }
    }
}
