using AutoMapper;
using HealTech.Application.Dto;
using HealTech.Core.Models;

namespace HealTech.Application.Mappers.AutoMapperService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Customer mappings
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dest => dest.PasswordHash, 
                opt => opt.MapFrom<PasswordHashResolver>())
            .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src => nameof(UserRole.Customer)));

        // Employee mappings
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeCreateDto, Employee>()
            .ForMember(dest => dest.PasswordHash, 
                opt => opt.MapFrom<PasswordHashResolver>())
            .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src => nameof(UserRole.Employee)));

        // Остальные маппинги остаются без изменений
        CreateMap<Order, OrderDto>();
        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
        

        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>();

        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategoryCreateDto, ProductCategory>();
    }
}