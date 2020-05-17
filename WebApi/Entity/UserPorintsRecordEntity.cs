using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 用户积分记录表
    /// </summary>
    [Table("user_porints_record")]
   public  class UserPorintsRecordEntity
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
        /// 产品赠送积分
        /// </summary>
        public int ProductPorints { get; set; }

        /// <summary>
        /// 直接分享所得积分
        /// </summary>
        public int DirectPorints { get; set; }

        /// <summary>
        /// 间接分享所得积分
        /// </summary>
        public int IndirectPorints { get; set; }

        /// <summary>
        /// 团队积分
        /// </summary>
        public int TreamPorints { get; set; }

        /// <summary>
        /// 积分类型(1:赠送积分,2兑现积分,3每日收益积分)
        /// </summary>
        public int PorintsType { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public string Addtime { get; set; }
    }
}
