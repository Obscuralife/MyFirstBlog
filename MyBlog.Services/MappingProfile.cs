using AutoMapper;
using MyBlog.DataAccessLayer.Models;
using MyBlog.DataAccessLayer.Models.Identity;
using MyBlog.Services.Models;
using MyBlog.Services.Models.Identity;
using System;

namespace MyBlog.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntryRequest, Entry>()
                .ForMember(i => i.CreatedOn, opt => opt.MapFrom(p => DateTime.UtcNow));

            CreateMap<CommentRequest, Comment>()
                .ForMember(i => i.CreatedOn, opt => opt.MapFrom(p => DateTime.UtcNow));

            CreateMap<RegisterRequest, DbUser>()
                .ForMember(i => i.UserName, opt => opt.MapFrom(p => $"{p.Name} {p.LastName}"));
         
        }
    }
}
