using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMap.Model
{
    class Settings
    {
        public bool ShowMapGrid { get; set; }
        public string BottomAxes { get; set; }
        public bool MDInverse { get; set; }
        public bool CDInverse { get; set; }
        public List<Legend> Legends { get; set; }
    }
}
