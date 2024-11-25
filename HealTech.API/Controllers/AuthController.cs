using HealTech.API.RequestModels;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.HashServices.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHashService _hasher;
        private readonly ICustomerService _customerService;

        public AuthController(IAuthService authService, IHashService hasher, ICustomerService customerService)
        {
            _authService = authService;
            _hasher = hasher;
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var token = await _authService.Login(model.Username, _hasher.ComputeHash(model.Password));
                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _customerService.Register(model.FirstName, model.Surname, model.Username, _hasher.ComputeHash(model.Password), model.TaxNumber, model.Email, model.Phone, model.Address);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
