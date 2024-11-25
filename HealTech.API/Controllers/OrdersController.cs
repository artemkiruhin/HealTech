using AutoMapper;
using HealTech.API.RequestModels;
using HealTech.Application.Dto;
using HealTech.Application.EntityServices.Base;
using HealTech.Application.Jwt;
using HealTech.Application.Jwt.Base;
using HealTech.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace HealTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public OrdersController(IOrderService orderService, IMapper mapper, IJwtService jwtService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _jwtService = jwtService;
        }


        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(DateTime? created, string? customerUsername, decimal? totalPrice, string? productName, int? qiantity)
        {
            try
            {
                var orders = await _orderService.GetFilteredOrders(created, customerUsername, totalPrice, productName, qiantity);

                var dtos = orders.Select(order => _mapper.Map<OrderDto>(order));

                return Ok(orders);
            }
            catch
            {
                return BadRequest("Не удалось получить заказы");
            }
        }


        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OrderAddModel model)
        {
            try
            {
                await _orderService.Add(model.CustomerId, model.ProductId, model.Quantity);


                return Ok("Заказ успешно зарегистрирован");    
            }
            catch
            {
                return BadRequest("Не удалось зарегистрировать заказ");
            }
        }


        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] OrderUpdateModel model)
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

                if (id != model.CustomerId)
                {
                    return Unauthorized("Несоответствие пользователей");
                }

                // Обновляем заказ, используя полученные данные из модели
                await _orderService.Update(model.Id, model.CustomerId, model.ProductId, model.Quantity);
                return Ok("Заказ успешно изменен");
            }
            catch
            {
                return BadRequest("Не удалось изменить заказ");
            }
        }
    }
}
