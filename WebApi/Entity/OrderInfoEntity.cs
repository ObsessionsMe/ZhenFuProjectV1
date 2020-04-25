/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    /// <summary>
    ///  订单表
    /// </summary>
    [Table("zf_order_info")]
    public class OrderInfoEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 订单编号(唯一)
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
  
        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public string GoodsUnitPrice { get; set; }

        /// <summary>
        /// 购买商品数量
        /// </summary>
        public string BuyGoodsNums { get; set; }

        /// <summary>
        ///  订单总金额(所有商品的单价乘以商品价格所得)
        /// </summary>
        public string PayCount { get; set; }

        /// <summary>
        ///  订单状态【0-待付款，1-待发货，3-待收货，2-已完成】
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string Addtime { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Exterd1 { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Exterd2 { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Exterd3 { get; set; }

    }
}
