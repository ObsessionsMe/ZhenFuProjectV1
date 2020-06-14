using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// 参数基类
    /// </summary>
    public class BasePara : Pagination
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 可扩展键值条件
        /// </summary>
        public Dictionary<string, object> KeyVals { get; set; }
    }
}
