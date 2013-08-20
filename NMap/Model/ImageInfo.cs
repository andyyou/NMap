using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WRPlugIn;

namespace NMap.Model
{
    public class ImageInfo : IImageInfo
    {
        public System.Drawing.Bitmap Image { set; get; }
        public int Station { set; get; }

        public ImageInfo(System.Drawing.Bitmap image, int station)
        {
            this.Image = image;
            this.Station = station;
        }
    }
}
