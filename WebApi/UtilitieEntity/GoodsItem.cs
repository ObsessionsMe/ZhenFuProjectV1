using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitieEntity
{
    public class GoodsItem
    {
        public string imageURL { get; set; }

        public string title { get; set; }
        //单价
        public string price { get; set; }
        //数量
        public int quantity { get; set; }
    }
}
