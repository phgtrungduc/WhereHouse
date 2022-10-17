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

        public User GetUserByUserName(string userName)
        {
            _dbSet = _context.Set<User>();
            var data = _dbSet.Where(x => x.UserName == userName).Include(user=>user.Avatar).FirstOrDefault();
            return data;
        }

        public override IEnumerable<UserDTO> GetAll()
        {
            _dbSet = _context.Set<User>();
            var data = _dbSet.Include(user => user.Avatar)?.ToList();
            var dataMapper = _mapper.Map<List<User>, List<UserDTO>>(data);
            dataMapper.ForEach(x =>
            {
                if ((x.Avatar != null))
                {
                    if (!string.IsNullOrEmpty(x.Avatar.FilePath))
                    {
                        x.AvatarPath = x.Avatar.FilePath;
                    }
                }
            });
            return dataMapper;
        }

        public List<UserDTO> GetAllForAdmin()
        {
            _dbSet = _context.Set<User>();
            var data = _dbSet.Include(user => user.Avatar).Include(user=>user.Posts)?.ToList();
            var dataMapper = _mapper.Map<List<User>, List<UserDTO>>(data);
            dataMapper.ForEach(x =>
            {
                if ((x.Avatar != null))
                {
                    if (!string.IsNullOrEmpty(x.Avatar.FilePath))
                    {
                        x.AvatarPath = x.Avatar.FilePath;
                    }
                }
                if (x.Posts != null) {
                    x.NumberOfPosts = x.Posts.Count();
                }
            });
            return dataMapper;
        }
    }
}
