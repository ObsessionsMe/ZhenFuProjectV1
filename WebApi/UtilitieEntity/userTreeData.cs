﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitieEntity
{
    public class UserTreeData
    {
        public string id { get; set; }

        public string label { get; set; }

        public List<UserTreeData>? children { get; set; }
    }
}