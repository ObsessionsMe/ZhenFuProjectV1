using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_WeiXinNotify_Info")]
    /// <summary>
    /// 支付宝支付异步通知
    /// </summary>
    public class WeiXinNotify_Entity
    {

        public int Id { get; set; }

        public string userId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string orderNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int orderStatus { get; set; }

        public int payNum { get; set; }
  
        /// <summary>
        /// 
        /// </summary>
        public string notifyTime { get; set; }

    }
}
