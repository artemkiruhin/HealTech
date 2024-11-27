using AutoMapper;
using HealTech.Application.Dto;
using HealTech.Core.Models;

namespace HealTech.Application.Mappers.AutoMapperService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User -> UserDto
        CreateMap<User, UserDto>();

        // Employee -> EmployeeDto
        CreateMap<Employee, EmployeeDto>();

        // Customer -> CustomerDto
        CreateMap<Customer, CustomerDto>();

        // ProductCategory -> ProductCategoryDto
        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        // Product -> ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

        // Order -> OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null
                ? $"{src.Customer.FirstName} {src.Customer.Surname}"
                : "Unknown"))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null
                ? src.Product.Name
                : "Unknown"));
            //.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product != null
            //    ? src.Product.Price
            //    : 0m));
    }
}