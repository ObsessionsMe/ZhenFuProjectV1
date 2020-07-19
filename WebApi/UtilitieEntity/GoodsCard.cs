using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitieEntity
{
    public class GoodsCard
    {
        public string orderNumber { get; set; }
        
        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int orderStatus { get; set; }

        /// <summary>
        /// 支付总金额
        /// </summary>
        public int payCount { get; set; }

        public List<GoodsItem>products { get; set; }

    }
}
