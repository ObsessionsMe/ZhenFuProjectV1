using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_AliNotify_Info")]
    /// <summary>
    /// 支付宝支付异步通知
    /// </summary>
    public class AliNotify_Entity
    {

        public int Id { get; set; }

        public string user_id { get; set; }

        /// <summary>
        ///  支付宝交易号
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 交易状态(WAIT_BUYER_PAY:交易创建，等待买家付款， TRADE_CLOSED:未付款交易超时关闭，或支付完成后全额退款，TRADE_SUCCESS:交易支付成功，TRADE_FINISHED:交易结束，不可退款)
        /// </summary>
        public string trade_status { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public string receipt_amount { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        public string gmt_close { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public string notify_time { get; set; }

    }
}
