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
        public bool isToRecolor { get; set; } = true;

        public bool isToReduce { get; set; } = false;
        public string folder18 { get; set; } = "";
        public string folder120 { get; set; } = "";
        public override string ToString()
        {
            return name;
        }
    }
}
