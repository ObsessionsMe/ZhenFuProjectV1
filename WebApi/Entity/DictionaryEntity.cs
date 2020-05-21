using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_dictionary")]
    public class DictionaryEntity
    {
        public int Id { get; set; }
        public int PId { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string Enable { get; set; }

        public DateTime AddTime { get; set; }

        public string AddUserId { get; set; }

    }
}
