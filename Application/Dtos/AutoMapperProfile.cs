using AutoMapper;
using BlogCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Application.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<Post, PostDetailDto>();
            CreateMap<Tag, TagDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
