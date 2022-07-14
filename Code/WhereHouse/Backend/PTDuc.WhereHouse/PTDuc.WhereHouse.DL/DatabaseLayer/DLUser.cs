using Microsoft.Extensions.Configuration;
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
    public class DLUser : DLBase<User, UserDTO>, IDLUser
    {
        public DLUser(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override User GetByID(string Id)
        {
            _dbSet = _context.Set<User>();
            var data = _dbSet.Where(x => x.UserId == Guid.Parse(Id)).Include(x => x.Avatar).FirstOrDefault();
            return data;
        }
    }
}
