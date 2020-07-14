using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UtilitieEntity
{
    public class UserTreeData
    {
        public UserTreeData(DataRow dataRow)
        {
            id = dataRow.Field<string>("UserTelephone");
            name = dataRow.Field<string>("Name");
            buyGoodsCount = dataRow.Field<int>("BuyGoodsCount");
            telephone = dataRow.Field<string>("UserTelephone");
            label = name + "(" + telephone + "):"+ buyGoodsCount.ToString();
            children = new List<UserTreeData>();
        }
        public UserTreeData()
        {

        }

        public string id { get; set; }

        public string name { get; set; }

        public string telephone { get; set; }

        public string goodsLevelNames { get; set; }

        public int buyGoodsCount { get; set; }

        public string label { get; set; }

        public List<UserTreeData> children { get; set; }
    }
}
