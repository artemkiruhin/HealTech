using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.HashServices.Base;
using HealTech.Application.Jwt.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;

        public CustomersController(ICustomerService service, IMapper mapper, IJwtService jwtService, IHashService hashService)
        {
            _service = service;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> All(string? username, DateTime? registered, string? email, string? phone, string? address, string? taxNumber, string? firstname, string? surname, bool? isActive)
        {
            try
            {


                var employees = await _service.GetByFilter(username, registered, email, phone, address, taxNumber, firstname, surname, isActive);
                var dtos = employees.Select(x => _mapper.Map<CustomerDto>(x)).ToList();

                return Ok(dtos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            try
            {
                var employee = await _service.GetById(id);
                return Ok(_mapper.Map<CustomerDto>(employee));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
