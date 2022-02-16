using AutoMapper;
using Final.Project.Core.Auth;
using Final.Project.Core.ProductServices;
using Final.Project.Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace Final.Project.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Authentication
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            // Product service
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductInput, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }

        private string RemoveHTMLTags(string HTMLCode)
        {
            return Regex.Replace(HTMLCode, @"<[^>]*>", String.Empty);
        }

    }
}
