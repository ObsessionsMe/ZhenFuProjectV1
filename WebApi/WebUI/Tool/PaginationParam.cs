﻿
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUI.Tool
{
   public  class PaginationParam
    {
        public Pagination pagination { get; set; }
        public string keyword { set; get; }
        public DateTime? beginDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
