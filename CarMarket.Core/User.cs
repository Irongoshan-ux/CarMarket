﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Core
{
    public class User
    {
        public string UserName { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
