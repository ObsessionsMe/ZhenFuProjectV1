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
    [Table("zf_user_product_framework")]
    public class UserProductFrameworkEntity
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
        /// 产品编号
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTelephone { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// 推荐人手机号
        /// </summary>
        public string ReferrerTelephone { get; set; }

        /// <summary>
        /// 第一次购买时间
        /// </summary>
        public string Addtime { get; set; }

    }
}
