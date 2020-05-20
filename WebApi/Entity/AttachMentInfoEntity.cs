using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("zf_attachment_info")]
    public class AttachMentInfoEntity
    {
        /// <summary>
        /// 自增长Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 主Id，可能是商品Id或者其他类型的Id
        /// </summary>
        public string MainId { get; set; }

        /// <summary>
        /// 附件类型(0-首页轮播图,1-商品详情轮播图,2-商品详情图(多图),3-首页产品列表图,4商品列表主图,5字典表附件)
        /// </summary>
        public int AttachmentType { get; set; }

        /// <summary>
        ///  附件名称(没有带路径)
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        
    }
}
