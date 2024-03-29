﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewEntity
{
    [Table("zf_view_getCashList")]
    public class CashListEntity
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
        /// 用户ID
        /// </summary>
        public string userTelephone { get; set; }

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
        public string Type { get; set; }

        /// <summary>
        /// 兑现提交时间
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PayTypeName { get; set; }

        /// <summary>
        /// 银行卡类型名称
        /// </summary>
        public string BankTypeName { get; set; }

        /// <summary>
        /// 银行卡开户人姓名
        /// </summary>
        public string BankUserName { get; set; }


        public string ProvinceName { get; set; }


        public string CityName { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ProvinceCityName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int Integral { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        /// <summary>
        /// 可提现比例
        /// </summary>
        public string DeductRate { get; set; }

        /// <summary>
        /// 兑现积分数
        /// </summary>
        public int Deduct { get; set; }

        /// <summary>
        /// 提现手续费
        /// </summary>
        public double PoundageRate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double ActualDeduct { get; set; }


        /// <summary>
        /// 兑现状态 1:完成 0:待兑现
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Name { get; set; }

        public int ProductPorints { get; set; }

        public int TreamPorints { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Addtime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperatorUser { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperatorDate { get; set; }

        
    }
}
