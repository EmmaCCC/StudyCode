using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrderInfoViewModel
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
