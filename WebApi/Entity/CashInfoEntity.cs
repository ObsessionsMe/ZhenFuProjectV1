using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_cash_info")]
    public class CashInfoEntity
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 兑现类型 1:个人 2:团队
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 兑现提交时间
        /// </summary>
        public DateTime? Date { get; set; }

        
        /// <summary>
        /// 支付方式类型
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PayTypeName { get; set; }

        /// <summary>
        /// 银行卡类型
        /// </summary>
        public int BankType { get; set; }

        /// <summary>
        /// 银行卡类型名称
        /// </summary>
        public string BankTypeName { get; set; }

        /// <summary>
        /// 银行卡开户人姓名
        /// </summary>
        public string BankUserName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 可提现比例
        /// </summary>
        public double DeductRate { get; set; }

        /// <summary>
        /// 兑现积分数
        /// </summary>
        public int Deduct { get; set; }

        /// <summary>
        /// 兑现状态 1:完成 0:待兑现,2已驳回
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorUser { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperatorDate { get; set; }
    }
}
