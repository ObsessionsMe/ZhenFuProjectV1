using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewEntity
{
    [Table("zf_view_getUserPorintList")]
    public class UserPorintListEntity
    {
        public int Id { get; set; }

        public int PorintsSurplus { get; set; }

        public int PecialItemPorints { get; set; }

        public int TreamPorints { get; set; }

        public int ProductPorints { get; set; }

        public int TourismPorints { get; set; }

        public int FieldsPorints { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserTelephone { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public string Referrer { get; set; }

        /// <summary>
        /// 推荐人手机号
        /// </summary>
        public string ReferrerTelephone { get; set; }

        public int UserType { get; set; }

        public string Enable { get; set; }

        public string IsAdmin { get; set; }

        public string Addtime { get; set; }

    }
}
