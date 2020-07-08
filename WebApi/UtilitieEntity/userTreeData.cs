using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitieEntity
{
    public class UserTreeData
    {
        public string id { get; set; }

        public string name { get; set; }

        public string telephone { get; set; }

        public string goodsLevelNames { get; set; }
        
        public int buyGoodsCount { get; set; }

        public string label { get; set; }

        public List<UserTreeData>? children { get; set; }
    }
}
