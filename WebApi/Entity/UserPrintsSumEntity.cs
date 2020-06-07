using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 用户积分汇总表，每个用户只有一条记录
    /// </summary>
    [Table("user_porints_sum")]
    public class UserPrintsSumEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 产品积分
        /// </summary>
        public int ProductPorints { get; set; }

        /// <summary>
        /// 团队积分
        /// </summary>
        public int TreamPorints { get; set; }

        /// <summary>
        /// 持仓天数
        /// </summary>
        public int HoldingDays { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public string Addtime { get; set; }
    }
}
