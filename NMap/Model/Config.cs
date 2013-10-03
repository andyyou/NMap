using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMap.Model
{
    public class Config
    {
        public string ShowMapGrid { get; set; }
        public string BottomAxes { get; set; }
        public bool MDInverse { get; set; }
        public bool CDInverse { get; set; }
        public string BackgroundColor { get; set; }
        public string GridColor { get; set; }
        public string XPrecision { get; set; }
        public string YPrecision { get; set; }
        public List<Legend> Legends { get; set; }
    }
}
