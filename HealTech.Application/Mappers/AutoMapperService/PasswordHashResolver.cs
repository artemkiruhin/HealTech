using AutoMapper;
using HealTech.Application.Dto;
using HealTech.Application.HashServices.Base;
using HealTech.Core.Models;

namespace HealTech.Application.Mappers.AutoMapperService;

public class PasswordHashResolver : 
    IValueResolver<CustomerCreateDto, Customer, string>,
    IValueResolver<EmployeeCreateDto, Employee, string>
{
    private readonly IHashService _hashService;

    public PasswordHashResolver(IHashService hashService)
    {
        _hashService = hashService;
    }

    public string Resolve(CustomerCreateDto source, Customer destination, string destMember, ResolutionContext context)
    {
        return _hashService.ComputeHash(source.Password);
    }

    public string Resolve(EmployeeCreateDto source, Employee destination, string destMember, ResolutionContext context)
    {
        return _hashService.ComputeHash(source.Password);
    }
}