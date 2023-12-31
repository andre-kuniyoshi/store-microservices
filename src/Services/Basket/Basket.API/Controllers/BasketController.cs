﻿using Basket.API.Entities;
using Basket.API.Services;
using Core.Controllers;
using Core.NotifierErrors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class BasketController : MainController<BasketController>
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService, INotifier notifier, ILogger<BasketController> logger) : base(notifier, logger)
        {
            _basketService = basketService;
        }

        [HttpGet(Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ShoppingCart>> GetBasket()
        {
            try
            {
                var userId = GetUserId();
                var basket = await _basketService.GetBasket(userId);
                return Ok(basket ?? new ShoppingCart(userId));
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ShoppingCart>> CreateUpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                var userId = GetUserId();
                basket.UserId = userId;

                var result = await _basketService.CreateUpdateBasket(basket);
                return Ok(result);
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
        {
            try
            {
                await _basketService.DeleteBasket(userName);
                return Ok();
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
                var userId = GetUserId();
                await _basketService.CheckoutBasket(userId, basketCheckout);
                return Accepted();
            }
            catch (Exception ex)
            {

                Logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
