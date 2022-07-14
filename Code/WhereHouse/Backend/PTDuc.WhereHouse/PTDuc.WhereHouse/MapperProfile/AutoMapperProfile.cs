using AutoMapper;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<File, FileDTO>();
            CreateMap<House, HouseDTO>();
            CreateMap<HouseType, HouseTypeDTO>();
            CreateMap<Post, PostDTO>();
            CreateMap<Wishlist, WishlistDTO>(); ;
            CreateMap<Message, MessageDTO>(); ;
            CreateMap<Conversation, ConversationDTO>(); ;
        }
    }
}
