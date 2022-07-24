using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer 
{
    public class BLHouse : BLBase<House,HouseDTO>, IBLHouse
    {
        IDLHouse _dlHouse;
        IBLWishlist _bLWishlist;
        IBLPost _bLPost;
        public BLHouse(IDLHouse dLHouse,IBLWishlist bLWishlist, IMapper mapper, IBLPost bLPost) : base(dLHouse, mapper)
        {
            _dlHouse = dLHouse;
            _bLWishlist = bLWishlist;
            _bLPost = bLPost;
        }

        public bool AddNewPost(HouseDTO dataForPost)
        {
            var res = false;
            if (this.Validate(dataForPost)) {
                var houseId = Guid.NewGuid();
                dataForPost.HouseId = houseId;
                res = this.Insert(dataForPost);
                dataForPost.CreatedDate = DateTime.Now;
                if (res) {
                    PostDTO post = new PostDTO { Title = dataForPost.Title, Descrtiption = dataForPost.Description, UserId = dataForPost.UserOwnerId,HouseId = houseId };
                    res =  _bLPost.Insert(post);
                }
                return res;
            }
            return res;
        }

        public IEnumerable<House> GetDeepData()
        {
            return _dlHouse.GetDeepData();
        }

    }
}
