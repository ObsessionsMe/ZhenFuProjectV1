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
    [Table("zf_product_cfg")]
    public class ProductCfgEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GoodsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxBuysCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int DirectPointsUsersCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IndirectPointsUsersCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsAllIndirectUsers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double ProductPorintsCashPoundage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double TreamPorintsCashPoundage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int EarningsDays { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string IsWorkingDays { get; set; }


        public string isGivePorint { get; set; }

        public int isGiveDefalutPorint { get; set; }
    }
}
