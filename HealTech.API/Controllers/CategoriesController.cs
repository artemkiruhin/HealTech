using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.Jwt.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IProductCategoryService _service;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public CategoriesController(IProductCategoryService service, IMapper mapper, IJwtService jwtService)
        {
            _service = service;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll(string? name)
        {
            try
            {
                var products = await _service.GetAll();
                
                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Where(x => x.Name.Contains(name));
                }

                var dtos = products.Select(x => _mapper.Map<ProductCategoryDto>(x)).ToList();
                return Ok(dtos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            try
            {
                await _service.Add(name);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] Guid categoryId, [FromBody] string name)
        {
            try
            {
                string jwtToken = HttpContext.Request.Cookies["jwtToken"];

                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Unauthorized("Пользователь не авторизован: токен отсутствует.");
                }

                var (id, role) = _jwtService.GetIdAndRoleFromClaims(_jwtService.ValidateToken(jwtToken));

                if (id == null)
                {
                    return Unauthorized("Пользователь не авторизован");
                }

                await _service.Update(categoryId, name);
                return Ok("Заказ успешно изменен");
            }
            catch
            {
                return BadRequest("Не удалось изменить категорию");
            }
        }

    }
}
