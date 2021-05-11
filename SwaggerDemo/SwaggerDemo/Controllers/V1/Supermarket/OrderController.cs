using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Dto;
using Model.ViewModel;

namespace SwaggerDemo.Controllers.V1.Supermarket
{

    public class OrderController : ApiControllerBaseV1
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">每页几条</param>
        /// <returns>订单的信息列表</returns>
        [HttpGet]
        [Obsolete]
        [ProducesResponseType(typeof(ResponseResult<List<OrderInfoViewModel>>), 200)]
        public IActionResult List(int page = 1, int size = 20)
        {
            var list = new List<OrderInfoViewModel>()
            {
                new OrderInfoViewModel()
                {
                     Count = 1,
                     CreateTime = DateTime.Now.AddDays(-5),
                     Desc = "麻烦送到家里111",
                }
            };
            return Ok(ResponseResult.Success(list));
        }


        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseResult<SaveOrderDto>), 200)]
        public IActionResult CreateOrder(SaveOrderDto dto)
        {
            return Ok(ResponseResult.Success(dto));
        }


        /// <summary>
        /// 设置订单状态已完成
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetComplete(string orderId, SaveOrderDto dto)
        {
            return Ok(new { dto, orderId });
        }



        /// <summary>
        /// 设置订单状态已完成
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[Route("/order/evaluate")]
        [HttpPost("/order/evaluate")]
        public IActionResult Comment(string orderId, SaveOrderDto dto)
        {
            return Ok(new { dto, orderId });
        }
    }


}
