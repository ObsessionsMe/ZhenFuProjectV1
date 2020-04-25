using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitieEntity
{
    /// <summary>
    ///  商品辅助类，可根据实际情况扩展
    /// </summary>
    public class GoodsViewTypeModel
    {
        public string GoodsId { get; set; }

        public string GoodsName { get; set; }

        public string GoodsDescribe { get; set; }

        public string GoodsMainImg { get; set; }

        public string GoodsDetailsImg { get; set; }

        public int UnitPrice { get; set; }
    }
}
