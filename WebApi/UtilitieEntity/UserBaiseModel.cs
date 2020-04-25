/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/
using System;

namespace UtilitieEntity
{
   /// <summary>
   /// 用户基础信息实体，在登陆时返回给前端，其中包括用户基础信息，以及积分情况，还有订单状态等...
   /// </summary>
    public class UserBaiseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

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
        /// 用户产品积分
        /// </summary>
        public string UserGoodsPorints { get; set; }

        /// <summary>
        /// 团队积分
        /// </summary>
        public string TreamPorints { get; set; }

    }
}
