using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMap.Model
{
    public class Column
    {
        public int Index { set; get; }
        public string Name { set; get; }
        public int Width { set; get; }

        public Column(int index, string column, int width)
        {
            this.Index = index;
            this.Name = column;
            this.Width = width;
        }
    }
}
