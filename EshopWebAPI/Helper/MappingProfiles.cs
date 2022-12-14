using AutoMapper;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;

namespace EshopWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<CategoryDto, Category>();
            CreateMap<ProductDto, Product>();

        }

    }
}
