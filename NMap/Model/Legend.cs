using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRPlugIn;

namespace NMap.Model
{
    public class Legend
    {

        public string ClassID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public FlawLegend OriginLegend { get; set; }
    }
}
