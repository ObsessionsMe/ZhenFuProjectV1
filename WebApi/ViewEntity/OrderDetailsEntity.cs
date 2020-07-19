using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewEntity
{
    [Table("zf_view_getOrderDetailsInfo")]
    public class OrderDetailsEntity
    {
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiveUser { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiveTelephone { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string DetailsAddress { get; set; }

        /// <summary>
        /// 订单编号(唯一)
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string Addtime { get; set; }

        /// <summary>
        ///  订单状态【0-待付款，1-待发货，2-待收货，3-已完成】
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        ///  订单总金额(所有商品的单价乘以商品价格所得)
        /// </summary>
        public int PayCount { get; set; }

        //订单运费
        public int GoodsFreight { get; set; }

        //订单备注
        public int OrderRemark { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        ///  商品单价(积分)
        /// </summary>
        public int UnitPrice { get; set; }

        /// <summary>
        /// 购买商品数量
        /// </summary>
        public int BuyGoodsNums { get; set; }

        /// <summary>
        ///  附件名称(没有带路径)
        /// </summary>
        public string AttachmentName { get; set; }

    }
}
