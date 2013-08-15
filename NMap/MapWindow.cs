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
using Newtonsoft.Json;

namespace NMap
{
    [Export(typeof(IWRPlugIn))]
    public partial class MapWindow : UserControl, IWRPlugIn, IWRMapWindow, IOnFlaws, IOnJobLoaded, IOnJobStarted, IOnClassifyFlaw
    {
        private List<FlawLegend> _legend;

        public MapWindow()
        {
            InitializeComponent();

            Properties.Settings.Default.Reset();
        }

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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            //Random rnd = new Random();
            //Series series = new Series("", ViewType.Point);
            //series.Points.Add(new SeriesPoint(rnd.Next(0, 2), rnd.Next(0, 2)));
            //series.ArgumentScaleType = ScaleType.Numerical;
            //series.ValueScaleType = ScaleType.Numerical;
            //chartControl.Series.Add(series);
            //var obj = JsonConvert.SerializeObject(_legend, Formatting.Indented);
            Settings setting = new Settings();
            setting.ShowDialog();
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
        }

        #endregion

        #region IOnJobLoaded 成員

        public void OnJobLoaded(IList<IFlawTypeName> flawTypes, IList<ILaneInfo> lanes, IList<ISeverityInfo> severityInfo, IJobInfo jobInfo)
        {
            btnSetting.Enabled = true;
        }

        #endregion

        #region IOnJobStarted 成員

        public void OnJobStarted(int jobKey)
        {
            // TODO: Save settings to databases
        }

        #endregion

        #region IOnClassifyFlaw 成員

        public void OnClassifyFlaw(ref IFlawInfo flaw, ref bool deleteFlaw)
        {
            //throw new NotImplementedException();
        }

        public void OnSetFlawLegend(List<FlawLegend> legend)
        {
            _legend = legend;
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitialChart();
        }
    }
}
