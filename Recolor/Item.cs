﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolor
{
    class Item
    {
        public string name { get; set; } = "";
        public string itempath { get; set; } = "";
        public string savepath { get; set; } = "";
        public override string ToString()
        {
            return name;
        }
    }
}
