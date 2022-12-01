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
        }

    }
}
