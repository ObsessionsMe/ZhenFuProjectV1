using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUI.App_Start.RequestEntity
{
   public  class PaginationParam
    {
        public Pagination pagination { get; set; }
        public string keyword { set; get; }
        public int ftype { set; get; }
    }
}
