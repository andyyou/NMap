using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMap.Model
{
    public class Unit
    {
        public string ComponentName { get; set; }
        public string Symbol { get; set; }
        public decimal Conversion { get; set; }
        public Unit(string name, string symbol, decimal conversion)
        {
            this.ComponentName = name;
            this.Conversion = conversion;
            this.Symbol = symbol;
        }
    }
}
