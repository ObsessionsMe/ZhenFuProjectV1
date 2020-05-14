using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("user_base_porints_record")]
    public  class UserBasePorintsRecordEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 主Id，可能是商品Id或者其他类型的Id，可能为空
        /// </summary>
        public string MainId { get; set; }

        /// <summary>
        /// 操作类型(1-充值，2-下单)
        /// </summary>
        public int OperateType { get; set; }

        /// <summary>
        /// 积分类型(1-余额，2-专项)
        /// </summary>
        public int PorintsType { get; set; }

        /// <summary>
        /// 充值积分数
        /// </summary>
        public int PorintsSurplus { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string Addtime { get; set; }

    }
}
