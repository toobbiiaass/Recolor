using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recolor
{
    class Config
    {
        public string pathToRecolor { get; set; } = "";
        public string pathToSave { get; set; } = "";
        public int red { get; set; } = 0;
        public int green { get; set; } = 0;
        public int blue { get; set; } = 0;
        public bool isBrownColorFilterOn { get; set; } = true;
        public List<string> whichCheckboxesOn { get; set; } = new List<string>();
        public string version { get; set; } = "-1";
    }
}
