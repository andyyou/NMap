﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRPlugIn;
using NMap.Helper;
using NMap.Properties;

namespace NMap
{
    public partial class FlawForm : Form
    {
        #region Loacal Variables

        private DataTable _flawData;
        private DataRow _drFlaw;
        private Dictionary<string, NMap.Model.Unit> _currentUnitList = new Dictionary<string, NMap.Model.Unit>();
        private decimal _currentCdConversion;
        private decimal _currentMdConversion;
        private PictureBox[] _pb;
        private double[] _pbRatio;
        private Image[] _srcImages;
        private IList<IImageInfo> _imageList;

        #endregion

        #region Constructor

        public FlawForm(DataRow flaw, Dictionary<string, NMap.Model.Unit> currentUnitList)
        {
            InitializeComponent();

            // Initialize datatable struct
            _flawData = new DataTable();
            _flawData.Columns.Add("FlawID", typeof(string));
            _flawData.Columns.Add("FlawType", typeof(int));
            _flawData.Columns.Add("FlawClass", typeof(string));
            _flawData.Columns.Add("Area", typeof(string));
            _flawData.Columns.Add("CD", typeof(decimal));
            _flawData.Columns.Add("MD", typeof(decimal));
            _flawData.Columns.Add("Width", typeof(decimal));
            _flawData.Columns.Add("Length", typeof(decimal));

            _flawData.ImportRow(flaw);
            _drFlaw = _flawData.Rows[0];
            _currentCdConversion = currentUnitList["FlawMapCD"].Conversion;
            _currentMdConversion = currentUnitList["FlawMapMD"].Conversion;
            _currentUnitList = currentUnitList;
            _drFlaw["CD"] = Convert.ToDecimal(_drFlaw["CD"]) / _currentCdConversion;
            _drFlaw["MD"] = Convert.ToDecimal(_drFlaw["MD"]) / _currentMdConversion;
            DataHelper dh = new DataHelper();
            _imageList = dh.GetFlawImageFromDb(_drFlaw);
        }

        #endregion

        #region R Methods

        // 計算圖片比例和置放圖片.
        public double Init_Image(Bitmap bmp, TabPage tp, PictureBox pb)
        {
            double Width_d = (double)bmp.Width / (double)tp.ClientSize.Width * 1.1;
            double Height_d = (double)bmp.Height / (double)tp.ClientSize.Height * 1.1;
            double ratio = 1.0;
            if (Width_d > 1 || Height_d > 1)
            {
                if (Width_d > Height_d)
                {
                    ratio = Width_d;
                }
                else
                {
                    ratio = Height_d;
                }
            }
            else if (Width_d < 1 && Height_d < 1)
            {
                if (Width_d > Height_d)
                    ratio = Width_d;
                else
                    ratio = Height_d;
            }
            pb.Width = (int)Math.Round(bmp.Width / ratio);
            pb.Height = (int)Math.Round(bmp.Height / ratio);

            Image src = bmp;
            Bitmap dest = new Bitmap(pb.Width, pb.Height);

            Graphics g = Graphics.FromImage(dest);
            g.DrawImage(src, new Rectangle(0, 0, dest.Width, dest.Height));
            pb.Height = dest.Height;
            pb.Width = dest.Width;
            pb.Image = dest;
            return ratio;
        }

        // Zoom In, Zoom Out
        public void PicZoomByPercent(int ZoomPercent)
        {
            PictureBox pb = null;
            foreach (Control control in tabImages.SelectedTab.Controls)
            {
                if (control.GetType().Name == "PictureBox")
                {
                    pb = (PictureBox)control;
                    break;
                }
            }

            if (pb != null)
            {
                Image src = pb.Image;
                Bitmap dest = null;

                int newWidth = (int)(((double)_srcImages[tabImages.SelectedIndex].Width / _pbRatio[tabImages.SelectedIndex]) * ((double)ZoomPercent / 100));
                int newHeight = (int)(((double)_srcImages[tabImages.SelectedIndex].Height / _pbRatio[tabImages.SelectedIndex]) * ((double)ZoomPercent / 100));
                dest = new Bitmap(newWidth, newHeight);

                Graphics g = Graphics.FromImage(dest);
                g.DrawImage(_srcImages[tabImages.SelectedIndex], new Rectangle(0, 0, dest.Width, dest.Height));
                pb.Height = dest.Height;
                pb.Width = dest.Width;
                pb.Image = dest;
            }
        }

        #endregion

        #region Action Methods

        private void FlawForm_Load(object sender, EventArgs e)
        {
            Boolean isSelectedTab = false;
            //// get current using unit
            //NowUnit ucd = _units.Find(x => x.ComponentName == "Flaw List CD");
            //NowUnit umd = _units.Find(x => x.ComponentName == "Flaw List MD");
            // set local variables.
            _pb = new PictureBox[JobHelper.JobInfo.NumberOfStations];
            _pbRatio = new double[JobHelper.JobInfo.NumberOfStations];
            _srcImages = new Image[JobHelper.JobInfo.NumberOfStations];
            // initialize all labels
            lblFlawIDVal.Text = _drFlaw["FlawID"].ToString();
            lblFlawClassVal.Text = _drFlaw["FlawClass"].ToString();
            lblFlawTypeVal.Text = _drFlaw["FlawType"].ToString();

            decimal cd = Convert.ToDecimal(_drFlaw["CD"]) * _currentUnitList["FlawListCD"].Conversion;
            decimal md = Convert.ToDecimal(_drFlaw["MD"]) * _currentUnitList["FlawListMD"].Conversion;
            decimal length = Convert.ToDecimal(_drFlaw["Length"]) * _currentUnitList["FlawListHeight"].Conversion;
            decimal width = Convert.ToDecimal(_drFlaw["Width"]) * _currentUnitList["FlawListWidth"].Conversion;
            decimal area = Convert.ToDecimal(_drFlaw["Area"]) * _currentUnitList["FlawListArea"].Conversion;

            lblCDVal.Text = string.Format("{0:0.000000} {1}", cd, _currentUnitList["FlawListCD"].Symbol);
            lblMDVal.Text = string.Format("{0:0.000000} {1}", md, _currentUnitList["FlawListMD"].Symbol);
            lblLengthVal.Text = string.Format("{0:0.000000} {1}", length, _currentUnitList["FlawListHeight"].Symbol);
            lblWidthVal.Text = string.Format("{0:0.000000} {1}", width, _currentUnitList["FlawListWidth"].Symbol);
            lblAreaVal.Text = string.Format("{0:0.000000} {1}", area, _currentUnitList["FlawListArea"].Symbol);

            // set about images
            for (int i = 0; i < JobHelper.JobInfo.NumberOfStations; i++)
            {
                tabImages.TabPages.Add("S" + ((i + 1).ToString()));
                _pb[i] = new PictureBox();
                _pb[i].SizeMode = PictureBoxSizeMode.Zoom;
                _pb[i].Location = new Point(0, 0);
                _pb[i].BackColor = Color.Transparent;
                _pb[i].MouseClick += new MouseEventHandler(pb_Click);
                tabImages.TabPages[i].AutoScroll = true;
                tabImages.TabPages[i].Controls.Add(_pb[i]);
                tabImages.TabPages[i].BackColor = Color.Transparent;
                tabImages.TabPages[i].Tag = 100;  // Zoom Multiplier value.
            }
            // deal if images return null show no-image.
            if (_imageList.Count > 0)
            {
                foreach (IImageInfo image in _imageList)
                {
                    _srcImages[image.Station] = image.Image;
                    _pbRatio[image.Station] = Init_Image(image.Image, tabImages.TabPages[image.Station], _pb[image.Station]);
                    if (!isSelectedTab)
                    {
                        tabImages.SelectedTab = tabImages.TabPages[image.Station];
                        isSelectedTab = true;
                    }
                }
                for (int i = 0; i < JobHelper.JobInfo.NumberOfStations; i++)
                {
                    if (_srcImages[i] == null)
                    {
                        _srcImages[i] = Resources.NoImage;
                        _pbRatio[i] = Init_Image(Resources.NoImage, tabImages.TabPages[i], _pb[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < JobHelper.JobInfo.NumberOfStations; i++)
                {
                    _srcImages[i] = Resources.NoImage;
                    _pbRatio[i] = Init_Image(Resources.NoImage, tabImages.TabPages[i], _pb[i]);
                }
            }
        }

        public void pb_Click(object sender, MouseEventArgs e)
        {
            ftbImage.Focus();
        }

        private void ftbImage_Scroll(object sender, EventArgs e)
        {
            PicZoomByPercent(ftbImage.Value);
        }

        #endregion

        private void tlpFlawInfo_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var panel = sender as TableLayoutPanel;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var rectangle = e.CellBounds;
            using (var pen = new Pen(Color.Black, 1))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                if (e.Row == (panel.RowCount - 1))
                {
                    rectangle.Height -= 1;
                }

                if (e.Column == (panel.ColumnCount - 1))
                {
                    rectangle.Width -= 1;
                }

                e.Graphics.DrawRectangle(pen, rectangle);
            }
        }



    }
}
