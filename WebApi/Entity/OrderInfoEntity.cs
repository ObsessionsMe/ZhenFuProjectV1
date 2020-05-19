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
        /// 用户收货地址Id
        /// </summary>
        public string AddressId { get; set; }
        

        /// <summary>
        /// 商品单价
        /// </summary>
        public int GoodsUnitPrice { get; set; }

        /// <summary>
        /// 购买商品数量
        /// </summary>
        public int BuyGoodsNums { get; set; }

        /// <summary>
        ///  订单总金额(所有商品的单价乘以商品价格所得)
        /// </summary>
        public int PayCount { get; set; }

        /// <summary>
        ///  订单状态【0-待付款，1-待发货，3-待收货，2-已完成】
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        ///  使用的积分类型【1-余额积分，2-团队积分，3-专项积分】
        /// </summary>
        public int UsePorintsType { get; set; }

        /// <summary>
        ///  支付方式【1-微信支付，2-支付宝，0-其他】
        /// </summary>
        public int PayMethod { get; set; }

        //订单运费
         public int GoodsFreight { get; set; }

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
