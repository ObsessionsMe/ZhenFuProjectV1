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
    [Table("zf_user_info")]
    public class UserInfoEntity
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
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  用户类别（-1管理员,0:总部,1-经销商(默认),2-代理商,3-市级代理,4-省级代理,5-分公司,6-合伙人）
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        ///  专项福豆
        /// </summary>
        public int TourismPorints { get; set; }

        /// <summary>
        ///  福豆余额
        /// </summary>
        public int PorintsSurplus { get; set; }

        /// <summary>
        ///  可用福豆
        /// </summary>
        public int PecialItemPorints { get; set; }

        /// <summary>
        /// 是否为管理员（N-否，Y-是）
        /// </summary>
        public string IsAdmin { get; set; }

        /// <summary>
        /// 是否有效（N-无效，Y-有效）
        /// </summary>
        public string Enable { get; set; }

        /// <summary>
        /// 是否持仓（N-没有，Y-有）
        /// </summary>
        public string isHold { get; set; }

        /// <summary>
        /// 注册时间
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

        /// <summary>
        /// 团队福豆
        /// </summary>
        public int TreamPorints { get; set; }


        /// <summary>
        /// 福豆田 
        /// </summary>
        public int FieldsPorints { get; set; }

    }
}
