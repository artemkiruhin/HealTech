using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.Jwt.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public ProductsController(IProductService productService, IMapper mapper, IJwtService jwtService)
        {
            _productService = productService;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll(string? name, int? quantity, string? categoryName, decimal? price)
        {
            try
            {
                var products = await _productService.GetByFilter(name, quantity, categoryName, price);
                var dtos = products.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                return Ok(dtos);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] ProductAddModel request)
        {
            try
            {
                await _productService.Add(request.Name, request.Quantity, request.Price, request.CategoryId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ProductUpdateModel request)
        {
            try
            {

                await _productService.Update(request.Id, request.Name, request.Price, request.Quantity, request.CategoryId);
                return Ok("Заказ успешно изменен");
            }
            catch
            {
                return BadRequest("Не удалось изменить заказ");
            }
        }

    }
}
