using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRPlugIn;
using DevExpress.XtraCharts;
using System.IO;
using System.Reflection;
using System.Xml.Linq;


namespace NMap
{
    [Export(typeof(IWRPlugIn))]
    public partial class MapWindow : UserControl, IWRPlugIn, IWRMapWindow, IOnFlaws, IOnJobLoaded, IOnJobStarted, IOnClassifyFlaw
    {
        private List<NMap.Model.Legend> _legends = new List<NMap.Model.Legend>();
        private static string _xmlPath = Path.GetDirectoryName(
                                         Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + 
                                         "\\..\\Parameter Files\\NMap\\legends.xml";
        public MapWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.Reset();
        }

        #region Refactoring Method

        /// <summary>
        /// 初始化圖表
        /// </summary>
        private void InitialChart()
        {
            chartControl.RuntimeHitTesting = true;
            chartControl.Legend.Visible = false;

            Series series = new Series();
            chartControl.Series.Add(series);

            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            // Setting AxisX format
            diagram.EnableAxisXZooming = true;
            diagram.EnableAxisXScrolling = true;
            diagram.AxisX.Range.MinValue = 0;
            //diagram.AxisX.Range.MaxValue = 100;
            //diagram.AxisX.Range.ScrollingRange.SetMinMaxValues(0, );
            diagram.AxisX.NumericOptions.Format = NumericFormat.Number;
            diagram.AxisX.NumericOptions.Precision = 6;
            diagram.AxisX.Reverse = Properties.Settings.Default.CDInverse;
            diagram.AxisX.GridLines.Visible = Properties.Settings.Default.ShowMapGrid;
            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisX.GridSpacingAuto = false;

            // Setting AxisY format
            diagram.EnableAxisYZooming = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisY.Range.MinValue = 0;
            //diagram.AxisY.Range.MaxValue = ;
            //diagram.AxisY.Range.ScrollingRange.SetMinMaxValues(0, );
            diagram.AxisY.NumericOptions.Format = NumericFormat.Number;
            diagram.AxisY.NumericOptions.Precision = 6;
            diagram.AxisY.Reverse = Properties.Settings.Default.MDInverse;
            diagram.AxisY.GridLines.Visible = Properties.Settings.Default.ShowMapGrid;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisY.GridSpacingAuto = false;

            chartControl.Series.Clear();
        }

        #endregion

        #region WRPlugin 介面

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

        #region IOnFlaws 成員

        public void OnFlaws(IList<IFlawInfo> flaws)
        {

            foreach (var flaw in flaws)
            {
                Series series = new Series(flaw.FlawID.ToString(), ViewType.Point);
                series.Points.Add(new SeriesPoint(flaw.CD, flaw.MD));
                series.ArgumentScaleType = ScaleType.Numerical;
                series.ValueScaleType = ScaleType.Numerical;
                series.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
                series.Label.PointOptions.PointView = PointView.SeriesName;
                chartControl.Series.Add(series);
            }
            // UNDONE
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.Range.ScrollingRange.MaxValue = diagram.AxisY.Range.MaxValue;
        }

        #endregion

        #region IOnJobLoaded 成員

        public void OnJobLoaded(IList<IFlawTypeName> flawTypes, IList<ILaneInfo> lanes, IList<ISeverityInfo> severityInfo, IJobInfo jobInfo)
        {
            btnSetting.Enabled = true;
            InitialChart();
        }

        #endregion

        #region IOnClassifyFlaw 成員

        public void OnClassifyFlaw(ref IFlawInfo flaw, ref bool deleteFlaw)
        {
            //throw new NotImplementedException();
        }

        public void OnSetFlawLegend(List<FlawLegend> legend)
        {
            foreach (var item in legend)
            {
                NMap.Model.Legend l = new NMap.Model.Legend();
                l.ClassID = item.ClassID.ToString();
                l.Color = item.Color.ToString();
                l.Name = item.Name;
                l.OriginLegend = item;
                l.Shape = "None";
                _legends.Add(l);
            }

        }

        #endregion

        #region IOnJobStarted 成員

        public void OnJobStarted(int jobKey)
        {
            // TODO: Save settings to databases
        }

        #endregion

        #endregion

        #region Events 

        /// <summary>
        /// 測試用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitialChart();
        }

        /// <summary>
        /// 調整 Map Zoom 回復 1 : 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisX.Range.Auto = true;
            diagram.AxisX.Range.ScrollingRange.Auto = true;
            //
            diagram.AxisX.Range.Auto = false;
            diagram.AxisX.Range.ScrollingRange.Auto = false;

        }

        /// <summary>
        /// 開啟設定視窗 設定 Map 相關參數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            //Random rnd = new Random();
            //Series series = new Series("", ViewType.Point);
            //series.Points.Add(new SeriesPoint(rnd.Next(0, 2), rnd.Next(0, 2)));
            //series.ArgumentScaleType = ScaleType.Numerical;
            //series.ValueScaleType = ScaleType.Numerical;
            //chartControl.Series.Add(series);
            // var obj = JsonConvert.SerializeObject(_legend, Formatting.Indented);
            #region Read XML
            XDocument xdoc = XDocument.Load(_xmlPath);
            // IEnumerable<XElement> elLegends =   from el in doc.Elements() select el;
            foreach (var item in _legends)
            {
                XElement xmlLegend = xdoc.Root.Elements("Legend").Where(el => (string)el.Attribute("ClassID") == item.ClassID).FirstOrDefault();
                item.Shape = xmlLegend.Attribute("Shape").Value;
                item.Color = xmlLegend.Attribute("Color").Value;

            }

            #endregion

            Settings setting = new Settings(_legends);
            setting.ShowDialog();
        }
        #endregion
    }
}
