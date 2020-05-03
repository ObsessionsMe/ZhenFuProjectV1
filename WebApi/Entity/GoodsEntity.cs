using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
     [Table("zf_goods_info")]
    /// <summary>
    /// 商品信息表
    /// </summary>
    public class GoodsEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        ///  是否是产品(N-否，Y-是)
        /// </summary>
        public string isProduct { get; set; }

        /// <summary>
        /// 商品种类--(0-个人清洁,1-美妆护肤,2-厨房用品,3-家用电器,4-家具家纺,5-手机数码,6-配饰背包,7-汽车用品)
        /// </summary>
        public int GoodsType { get; set; }

        /// <summary>
        ///  商品单价(积分)
        /// </summary>
        public int UnitPrice { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDescribe { get; set; }

        /// <summary>
        /// 商品主图(用户轮播时和首页中显示)
        /// </summary>
        public string GoodsMainImg { get; set; }

        /// <summary>
        /// 商品附加图，多个用;分隔开
        /// </summary>
        public string GoodsDetailsImg { get; set; }

        /// <summary>
        /// 持仓后每日(工作日)每件产生积分(如第一支42分)
        /// </summary>
        public int ItemPoints { get; set; }

        /// <summary>
        /// 直接分享，每日(工作日)每件产生积分(如第一支7分)
        /// </summary>
        public int DirectPoints { get; set; }

        /// <summary>
        /// 间接分享，每日(工作日)每件产生积分(如第一支4分)
        /// </summary>
        public int IndirectPoints { get; set; }

        /// <summary>
        /// 库存数量(默认10000)
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// 商品级别
        /// </summary>
        public int GoodsLevel { get; set; }

        /// <summary>
        ///  Enable(N-无效，Y-有效)
        /// </summary>
        public string Enable { get; set; }

        /// <summary>
        /// 上架时间
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
    }
}
