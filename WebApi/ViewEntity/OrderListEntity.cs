using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ViewEntity
{
    [Table("zf_view_getOrderList")]
    public class OrderListEntity
    {
        public int Id { get; set; }
        
        public string OrderNumber { get; set; }

        public string Name { get; set; }
        public string GoodsName { get; set; }
        
        public int GoodsUnitPrice { get; set; }

        public int BuyGoodsNums { get; set; }

        public int PayCount { get; set; }

        public int PayMethod { get; set; }

        public string AddressId { get; set; }

        public string UsePorintsType { get; set; }

        public string ReceiveUser { get; set; }

        public string ReceiveTelephone { get; set; }

        public string ReceiveProvinceName { get; set; }

        public string ReceiveCityName { get; set; }

        public string ReceiveAreaName { get; set; }

        public string DetailsAddress { get; set; }

        public string orderStatus { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public string Enable { get; set; }

        public string AddTime { get; set; }

    }
}
