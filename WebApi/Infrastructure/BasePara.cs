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
        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UserId { get; set; }

        public Dictionary<string,object> KeyVals { get; set; }
    }
}
