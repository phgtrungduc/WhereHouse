using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLUser : BLBase<User,UserDTO>,IBLUser
    {
        IDLUser _dlUser;
        List<UserDTO> userOnline;
        public BLUser(IDLUser dLUser, IMapper mapper) : base(dLUser, mapper)
        {
            _dlUser = dLUser;
            userOnline = new List<UserDTO>();
        }

        public override bool BeforeInsert(ref UserDTO entity)
        {
            var res = false;
            if (!string.IsNullOrEmpty(entity.Password)) {
                LoginParam loginParam = new LoginParam();
                loginParam = PasswordHash.HashPassword(entity.Password);
                if (loginParam != null) {
                    entity.Password = loginParam.HashPassword;
                    entity.Salt = loginParam.Salt;
                    res =  true;
                }
            }
            return res;
        }
        public void AddUserToListOnline(UserDTO user)
        {
            if (user != null) {
                var isExist =  userOnline.FindIndex(x => x.UserId == user.UserId);
                if (isExist == -1) {
                    userOnline.Add(user);
                }
            }
        }

        public void RemoveUserFromListOnline(UserDTO user)
        {
            if (user != null) {
                userOnline.RemoveAll(x => x.UserId == user.UserId);
            }
        }

        public List<UserDTO> GetListUserOnline()
        {
            return this.userOnline;
        }
    }
}
