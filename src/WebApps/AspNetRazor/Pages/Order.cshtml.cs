﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetRazor.Models;
using AspNetRazor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetRazor
{
    public class OrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderModel(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderService.GetOrdersByUserName("ahk");

            return Page();
        }
    }
}