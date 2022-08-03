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

        public Conversation GetConversation(string userId1, string userId2)
        {
            _dbSet = _context.Set<Conversation>();
            var id1 = Guid.Parse(userId1);
            var id2 = Guid.Parse(userId2);
            var data = _dbSet.Where(x => (x.UserId1 == id1 && x.UserId2 == id2)|| (x.UserId1 == id2 && x.UserId2 == id1)).FirstOrDefault();
            return data;
        }
    }
}
