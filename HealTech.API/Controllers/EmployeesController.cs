using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.HashServices.Base;
using HealTech.Application.Jwt.Base;
using HealTech.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;

        public EmployeesController(IEmployeeService service, IMapper mapper, IJwtService jwtService, IHashService hashService)
        {
            _service = service;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> All(string? username, DateTime? hired, bool? isAdmin, decimal? salary, string? firstname, string? surname, bool? isActive)
        {
            try
            {
                var employees = await _service.GetByFilter(username, hired, isAdmin, salary, firstname, surname, isActive);
                var dtos = employees.Select(x => _mapper.Map<EmployeeDto>(x)).ToList();

                return Ok(dtos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] EmployeeRegisterModel request)
        {
            try
            {
                string jwtToken = HttpContext.Request.Cookies["jwtToken"];

                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Unauthorized("Пользователь не авторизован: токен отсутствует.");
                }

                var (id, role) = _jwtService.GetIdAndRoleFromClaims(_jwtService.ValidateToken(jwtToken));

                if (role != UserRole.Employee)
                {
                    return Unauthorized("Несоответствие пользователей");
                }

                if (!id.HasValue)
                {
                    return Unauthorized("Пользователь не авторизован");
                }

                var employee = await _service.GetById(id.Value);

                if (employee == null)
                {
                    return NotFound("Сотрудник не найден");
                }

                if (!employee.IsAdmin)
                {
                    return BadRequest("Недостаточно прав для выполнения операции");
                }

                await _service.Register(request.FirstName, request.Surname, request.Username, _hashService.ComputeHash(request.Password), request.Salary, request.IsAdmin, true);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("mock-reg")]
        public async Task<IActionResult> Mock([FromBody] EmployeeRegisterModel request)
        {
            try
            {
                await _service.Register(request.FirstName, request.Surname, request.Username, _hashService.ComputeHash(request.Password), request.Salary, request.IsAdmin, true);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
