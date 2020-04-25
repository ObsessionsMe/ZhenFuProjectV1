using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_user_receive_address")]
    public class ReceiveAddressEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string  UserId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiveUser{ get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiveTelephone { get; set; }

        /// <summary>
        /// 收货省编号
        /// </summary>
        public string ReceiveAreaCode { get; set; }

        /// <summary>
        /// 收货区名称
        /// </summary>
        public string ReceiveAreaName{ get; set; }

        /// <summary>
        /// 收货区名称
        /// </summary>
        public string ReceiveCityName { get; set; }

        /// <summary>
        /// 收货市编号
        /// </summary>
        public string ReceiveCityCode { get; set; }

        /// <summary>
        /// 收货省名称
        /// </summary>
        public string ReceiveProvinceName { get; set; }

        /// <summary>
        /// 收货省编号
        /// </summary>
        public string ReceiveProvinceCode { get; set; }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public string DetailsAddress { get; set; }

        /// <summary>
        /// 是否为默认地址(新增时录入第一条时为默认地址)
        /// </summary>
        public string isDefalut { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public string Addtime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdateTime { get; set; }
    }
}
