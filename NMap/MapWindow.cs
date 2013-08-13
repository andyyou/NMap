using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRPlugIn;

namespace NMap
{
    public partial class MapWindow : UserControl, IWRPlugIn, IWRMapWindow
    {
        public MapWindow()
        {
            InitializeComponent();
        }

        #region IWRMapWindow 成員

        public void GetMapControlHandle(out IntPtr hndl)
        {
            hndl = Handle;
        }

        public void SetMapPosition(int w, int h)
        {
            SetBounds(0, 0, w, h);
        }

        #endregion

        #region IWRPlugIn 成員

        public void GetControlHandle(out IntPtr hndl)
        {
            hndl = Handle;
        }

        public void GetName(e_Language language, out string Name)
        {
            Name = "New Map";
        }

        public void Initialize(string unitsXMLPath)
        {
            // No Imemented;
        }

        public void SetPosition(int w, int h)
        {
            SetBounds(0, 0, w, h);
        }

        public void Unplug()
        {
            // No Imemented;
        }

        #endregion
    }
}
