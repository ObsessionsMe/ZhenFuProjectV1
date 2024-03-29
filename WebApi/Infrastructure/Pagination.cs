﻿/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/

namespace Infrastructure
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        public int pageBegin
        {
            get
            {
                return ((page - 1) * rows) + 1;
            }
        }

        public int pageEnd
        {
            get { return page * rows; }
        }

        /// <summary>
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        /// 排序列
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    if (this.rows == 0)
                    {
                        return 1;
                    }
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
