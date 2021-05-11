using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Dto;
using Model.ViewModel;

namespace SwaggerDemo.Controllers.V2.Supermarket
{


    public class OrderController : ApiControllerBaseV2
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">每页几条</param>
        /// <returns>订单的信息列表</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseResult<List<OrderInfoViewModel>>), 200)]
        [Route("{page}/{size}")]
        public IActionResult List(int page = 1, int size = 20)
        {
            var list = new List<OrderInfoViewModel>()
            {
                new OrderInfoViewModel()
                {
                     Count = 1,
                     CreateTime = DateTime.Now.AddDays(-5),
                     Desc = "麻烦送到家里2222",
                }
            };
            return Ok(ResponseResult.Success(new { list, page, size }));
        }

        
        [HttpPut("{orderId}")]
        public IActionResult SetOrderStatus(string orderId,OrderStatus status)
        {
            return Ok(ResponseResult.Success(new { orderId, status }));
        }
    }


}
