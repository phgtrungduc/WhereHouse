using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLPost : BLBase<Post, PostDTO>, IBLPost
    {
        IDLPost _DLPost;
        public BLPost(IDLPost DLPost, IMapper mapper) : base(DLPost, mapper)
        {
            _DLPost = DLPost;
        }


        public ServiceResult GetUserPost(Guid userId)
        {
            var res = new ServiceResult();
            res.Data = _DLPost.GetUserPost(userId);
            return res;
        }
    }
}
