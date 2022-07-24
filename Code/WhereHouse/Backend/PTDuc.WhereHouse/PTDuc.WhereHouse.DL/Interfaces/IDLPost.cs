using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLPost : IDLBase<Post, PostDTO>

    {
        public IEnumerable<Post> GetUserPost(Guid userId);
    }
}
