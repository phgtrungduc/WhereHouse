using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLPost : IBLBase<Post, PostDTO>
    {
        public ServiceResult GetUserPost(Guid userId);
        public ServiceResult DeletePostUser(string postId, string userId);
        public ServiceResult AcceptPost(string postId, string userId);
        public ServiceResult GetListPostForAdmin(string adminId);
    }
}
