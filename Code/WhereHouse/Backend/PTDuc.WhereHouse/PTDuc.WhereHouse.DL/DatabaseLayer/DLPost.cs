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
                .Include(x => x.User).Include(x => x.House).ThenInclude(house => house.HouseType)
                .Include(z => z.House).ThenInclude(i => i.HouseImage)
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
                var data = _dbSet.Skip(skip).Take(pageSize).Include(x => x.House).ThenInclude(y => y.HouseImage).Where(post => post.Status == (int)Enumeration.StatusPost.Accepted)?.ToList();
                var dataDTO = _mapper.Map<List<PostDTO>>(data);
                dataDTO.ForEach(x =>
                {
                    if (x.House != null && x.House.HouseImage != null)
                    {
                        x.HouseImageUrl = x.House.HouseImage.FilePath;
                    }
                });
                res.Data = new { TotalRecords = totalRecords, Data = dataDTO.OrderByDescending(x => x.CreatedDate) };
            }
            return res;
        }

        public IEnumerable<Post> GetListPostForAdmin()
        {
            _dbSet = _context.Set<Post>();
            var data = _dbSet.Include(x => x.User).Include(x => x.House).ThenInclude(house => house.HouseType)
                .Include(z => z.House).ThenInclude(i => i.HouseImage);
            return data?.OrderByDescending(x => x.CreatedDate);
        }

        public List<Post> GetSearchResult(string search)
        {
            var res = new List<Post>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                _dbSet = _context.Set<Post>();
                var data = _dbSet.Include(post => post.House).ThenInclude(house=>house.HouseImage).Include(post => post.User).ToList();
                data = data.Where(post => (post.Title.ToLower().Contains(search) ||
                post.User.UserName.ToLower().Contains(search) ||
                post.User.FullName.ToLower().Contains(search))&&post.Status==(int)Enumeration.StatusPost.Accepted).ToList();
                data.ForEach(x=> {
                    x.User = null;
                    x.House.UserOwner = null;
                });
                res = data?.OrderByDescending(x=>x.CreatedDate).ToList();
            }
            return res;
        }

        public List<Post> GetUserPost(Guid userId)
        {
            var res = new List<Post>();
            _dbSet = _context.Set<Post>();
            var data = _dbSet.Where(x => x.UserId == userId);
            if (data != null && data.Count() > 0)
            {
                res = data.Include(x => x.House).ThenInclude(y => y.HouseImage)?.ToList();
            }
            return res?.OrderByDescending(x => x.CreatedDate).ToList();


        }
    }
}
