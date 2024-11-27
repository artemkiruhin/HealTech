using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.Jwt;
using HealTech.Application.Jwt.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<IActionResult> Update(Guid categoryId, string name)
        {
            try
            {
                // Получаем идентификатор пользователя напрямую из Claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                foreach (var c in User.Claims)
                {
                    Console.WriteLine($"{c.Type} - {c.Value}" );
                }


                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Пользователь не авторизован");
                }

                Guid parsedUserId = Guid.Parse(userId);

                await _service.Update(categoryId, name);
                return Ok("Заказ успешно изменен");
            }
            catch (Exception ex)
            {
                // Логируем полную ошибку
                Console.WriteLine($"Update error: {ex}");
                return BadRequest($"Не удалось изменить категорию: {ex.Message}");
            }
        }

    }
}
