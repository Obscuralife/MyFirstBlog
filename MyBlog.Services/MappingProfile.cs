using AutoMapper;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Models;
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
         
        }
    }
}
