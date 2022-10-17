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
using PTDuc.WhereHouse.EntityModels;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLHouse : BLBase<House, HouseDTO>, IBLHouse
    {
        IDLHouse _dlHouse;
        IBLWishlist _bLWishlist;
        IBLPost _bLPost;
        public BLHouse(IDLHouse dLHouse, IBLWishlist bLWishlist, IMapper mapper, IBLPost bLPost) : base(dLHouse, mapper)
        {
            _dlHouse = dLHouse;
            _bLWishlist = bLWishlist;
            _bLPost = bLPost;
        }

        public bool AddNewPost(HouseDTO dataForPost)
        {
            var res = false;
            if (this.Validate(dataForPost))
            {
                var houseId = Guid.NewGuid();
                dataForPost.HouseId = houseId;
                res = this.Insert(dataForPost);
                dataForPost.CreatedDate = DateTime.Now;
                if (res)
                {
                    PostDTO post = new PostDTO { Title = dataForPost.Title, Descrtiption = dataForPost.Descrtiption, UserId = dataForPost.UserOwnerId, HouseId = houseId };
                    res = _bLPost.Insert(post);
                }
                return res;
            }
            return res;
        }

        public IEnumerable<House> GetDeepData()
        {
            return _dlHouse.GetDeepData();
        }

        public ServiceResult UpdatePost(string userid, HouseDTO dataForPost)
        {
            var res = new ServiceResult();
            var post = _bLPost.GetByID(dataForPost.PostId.ToString());
            if (post != null && post.UserId.ToString() == userid)
            {
                post.Descrtiption = dataForPost.Descrtiption;
                post.Title = dataForPost.Title;
                var postSuccess = _bLPost.Update(post, post.PostId.ToString());

                var house = this.GetByID(dataForPost.HouseId.ToString());
                house.HouseImageId = dataForPost.HouseImageId;
                house.HouseName = dataForPost.HouseName;
                house.HouseTypeId = dataForPost.HouseTypeId;
                house.Address = dataForPost.Address;
                house.AddressByGoogle = dataForPost.AddressByGoogle;
                house.Area = dataForPost.Area;
                house.Vertical = dataForPost.Vertical;
                house.Horizontal = dataForPost.Horizontal;
                house.NumberOfBedroom = dataForPost.NumberOfBedroom;
                house.TotalOfFloor = dataForPost.TotalOfFloor;
                house.Price = dataForPost.Price;
                var houseSuccess = this.Update(house, house.HouseId.ToString());
                if (postSuccess || houseSuccess)
                {
                    res.Data = true;
                }
                else
                {
                    res.StatusCode = (int)Enumeration.ResultCode.Failed;
                    res.Data = false;
                }
            }
            else
            {
                res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                res.Data = false;
            }

            return res;
        }
    }
}
