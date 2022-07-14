using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLPost : DLBase<Post, PostDTO>, IDLPost
    {
        public DLPost(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public override Post GetByID(string Id)
        {
            _dbSet = _context.Set<Post>();
            var post = _dbSet.Where(x => x.PostId.ToString() == Id)
                .Include(x=>x.User).Include(x => x.House).ThenInclude(house=>house.HouseType)
                .Include(z=>z.House).ThenInclude(i=>i.HouseImage)
                .FirstOrDefault();
            return post;
        }
        public override ServiceResult GetByPaging(int page, int pageSize)
        {
            _dbSet = _context.Set<Post>();
            var totalRecords = _dbSet.Count();
            var skip = (page - 1) * pageSize;
            var res = new ServiceResult();
            if (skip >= 0 && pageSize > 0)
            {
                var data = _dbSet.Skip(skip).Take(pageSize).Include(x => x.House).ThenInclude(y => y.HouseImage);
                data?.Include(x => x.House).ThenInclude(y => y.HouseImage);
                res.Data = new { TotalRecords = totalRecords, Data = data.ToList() };
            }
            return res;
        }
    }
}
