using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLPost : IBLBase<Post, PostDTO>
    {
        public ServiceResult GetPublicPost(string postId);
        public ServiceResult GetDetailPost(string postId,string userId);
        public ServiceResult GetUserPost(Guid userId);
        public ServiceResult DeletePostUser(string postId, string userId);
        public ServiceResult AcceptPost(string postId, string userId);
        public ServiceResult GetListPostForAdmin(string adminId);
        public ServiceResult ReportPost(string userId, ReportDTO report);
        public ServiceResult ChangeStatusReport(string userId, string reportId);
        List<PostDTO> GetSearchResult(string search);
    }
}
