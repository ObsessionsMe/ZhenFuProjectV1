using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.App_Start.RequestEntity
{
    /// <summary>
    ///  用户输入参数，可接收前端传递过来的复杂json数据，根据实际需要进行扩展
    /// </summary>
    public class UserParams
    {
        public UserInfoEntity userEntity { get; set; }

        //用户Id，修改时==userEntity.ID，新增时==0
        public int keyValue { set; get; }

        public string ExpireTime { set; get; }
    }
}
