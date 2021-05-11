using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Dto
{
    /// <summary>
    /// 保存订单
    /// </summary>
    public class SaveOrderDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Addresss { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string Mobile { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }

    public enum OrderStatus
    {
        None,
        Pay,
        Complete,
        Comment

    }
}
