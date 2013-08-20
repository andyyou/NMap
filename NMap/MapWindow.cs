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
using NMap.Helper;
using NMap.Model;

namespace NMap
{
    [Export(typeof(IWRPlugIn))]
    public partial class MapWindow : UserControl, IWRPlugIn, IWRMapWindow, IOnFlaws, IOnJobLoaded, IOnJobStarted, IOnClassifyFlaw
    {
        private Config _config = new Config() { Legends = new List<NMap.Model.Legend>() };
        private static string _xmlPath = Path.GetDirectoryName(
                                         Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + 
                                         @"\..\Parameter Files\NMap\legends.xml";
        private Dictionary<string, MarkerKind> _dicSeriesShape = new Dictionary<string, MarkerKind>()
        {
            { "Triangle", MarkerKind.Triangle },
            { "InvertedTriangle", MarkerKind.InvertedTriangle },
            { "Square", MarkerKind.Square },
            { "Circle", MarkerKind.Circle },
            { "Plus", MarkerKind.Plus },
            { "Cross", MarkerKind.Cross },
            { "Star", MarkerKind.Star }
        };
        private DataTable _flawData;
        private IJobInfo _jobInfo;

        public MapWindow()
        {
            InitializeComponent();

            // Initialize datatable struct
            _flawData = new DataTable();
            _flawData.Columns.Add("FlawID", typeof(string));
            _flawData.Columns.Add("FlawType", typeof(int));
            _flawData.Columns.Add("FlawClass", typeof(string));
            _flawData.Columns.Add("Area", typeof(string));
            _flawData.Columns.Add("CD", typeof(double));
            _flawData.Columns.Add("MD", typeof(double));
            _flawData.Columns.Add("Width", typeof(double));
            _flawData.Columns.Add("Length", typeof(double));
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
            diagram.AxisX.Reverse = _config.CDInverse;
            diagram.AxisX.GridLines.Visible = _config.ShowMapGrid == "On" ? true : false;
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
            diagram.AxisY.Reverse = _config.MDInverse;
            diagram.AxisY.GridLines.Visible = _config.ShowMapGrid == "On" ? true : false;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisY.GridSpacingAuto = false;

            if (_config.BottomAxes == "CD")
            {
                diagram.Rotated = false;
            }
            else
            {
                diagram.Rotated = true;
            }

            chartControl.Series.Clear();
        }

        private DataTable QueryDataTable(DataTable dt, string condition, string sortstr)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            DataRow[] dr = dt.Select(condition, sortstr);
            for (int i = 0; i < dr.Length; i++)
            {
                newdt.ImportRow((DataRow)dr[i]);
            }
            return newdt;
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
            //foreach (var flaw in flaws)
            //{
            //    Series series = new Series(flaw.FlawID.ToString(), ViewType.Point);
            //    series.Points.Add(new SeriesPoint(flaw.CD, flaw.MD));
            //    series.ArgumentScaleType = ScaleType.Numerical;
            //    series.ValueScaleType = ScaleType.Numerical;
            //    series.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            //    series.Label.PointOptions.PointView = PointView.SeriesName;

            //    NMap.Model.Legend legend = _config.Legends.Where(l => l.Name == flaw.FlawClass).FirstOrDefault();
            //    string shape = legend.Shape;
            //    string color = legend.Color;
            
            //    PointSeriesView pointView = (PointSeriesView)series.View;
            //    pointView.PointMarkerOptions.Kind = _dicSeriesShape[shape];
            //    pointView.Color = System.Drawing.ColorTranslator.FromHtml(color);
                
            //    chartControl.Series.Add(series);
            //}
            //// UNDONE
            //XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            //diagram.AxisY.Range.ScrollingRange.MaxValue = diagram.AxisY.Range.MaxValue;

            foreach (var flaw in flaws)
            {
                DataRow row = null;
                row = _flawData.NewRow();
                row["FlawID"] = flaw.FlawID;
                row["FlawType"] = flaw.FlawType;
                row["FlawClass"] = flaw.FlawClass;
                row["Area"] = flaw.Area;
                row["CD"] = flaw.CD;
                row["MD"] = flaw.MD;
                row["Width"] = flaw.Width;
                row["Length"] = flaw.Length;
                _flawData.Rows.Add(row);
            }

            foreach (Series series in chartControl.Series)
            {
                series.DataSource = QueryDataTable(_flawData, "FlawClass = '" + series.Name + "'", ""); ;
            }

            // Add each legend to chart
            //List<Series> seriesList = new List<Series>();
            //foreach (var flaw in flaws)
            //{
            //    Series series = new Series(flaw.FlawID.ToString(), ViewType.Point);
            //    series.Points.Add(new SeriesPoint(flaw.CD, flaw.MD));
            //    series.ArgumentScaleType = ScaleType.Numerical;
            //    series.ValueScaleType = ScaleType.Numerical;
            //    series.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            //    NMap.Model.Legend legend = _config.Legends.Where(l => l.Name == flaw.FlawClass).FirstOrDefault();

            //    PointSeriesView seriesView = (PointSeriesView)series.View;
            //    seriesView.PointMarkerOptions.Kind = _dicSeriesShape[legend.Shape];
            //    seriesView.Color = System.Drawing.ColorTranslator.FromHtml(legend.Color);

            //    seriesList.Add(series);

            //    //chartControl.Series.Add(series);
            //}
            //chartControl.Series.AddRange(seriesList.ToArray());
        }

        #endregion

        #region IOnJobLoaded 成員

        public void OnJobLoaded(IList<IFlawTypeName> flawTypes, IList<ILaneInfo> lanes, IList<ISeverityInfo> severityInfo, IJobInfo jobInfo)
        {
            _jobInfo = jobInfo;
            btnSetting.Enabled = true;
            chartControl.Series.Clear();
            //InitialChart();
        }

        #endregion

        #region IOnClassifyFlaw 成員

        public void OnClassifyFlaw(ref IFlawInfo flaw, ref bool deleteFlaw)
        {
            //throw new NotImplementedException();
        }

        public void OnSetFlawLegend(List<FlawLegend> legend)
        {
            _config.Legends.Clear();
            foreach (var item in legend)
            {
                NMap.Model.Legend l = new NMap.Model.Legend();
                l.ClassID = item.ClassID.ToString();
                l.Color = String.Format("#{0:X2}{1:X2}{2:X2}",
                              ColorTranslator.FromWin32((int)item.Color).R,
                              ColorTranslator.FromWin32((int)item.Color).G,
                              ColorTranslator.FromWin32((int)item.Color).B);
                l.Name = item.Name;
                l.OriginLegend = item;
                _config.Legends.Add(l);
            }
            // Reset config
            ConfigHelper ch = new ConfigHelper();
            ch.CreateConfigFile(_config.Legends);
        }

        #endregion

        #region IOnJobStarted 成員

        public void OnJobStarted(int jobKey)
        {
            // Get config from xml file
            ConfigHelper ch = new ConfigHelper();
            _config = ch.GetConfigFile();
            InitialChart();

            // Add each legend to chart
            foreach (var legend in _config.Legends)
            {
                Series series = new Series(legend.Name, ViewType.Point);
                series.ArgumentScaleType = ScaleType.Numerical;
                series.ArgumentDataMember = "CD";
                series.ValueScaleType = ScaleType.Numerical;
                series.ValueDataMembers.AddRange(new string[] { "MD" });
                series.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

                PointSeriesView seriesView = (PointSeriesView)series.View;
                seriesView.PointMarkerOptions.Kind = _dicSeriesShape[legend.Shape];
                seriesView.Color = System.Drawing.ColorTranslator.FromHtml(legend.Color);

                chartControl.Series.Add(series);
            }
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

            ConfigHelper ch = new ConfigHelper();
            _config = ch.GetConfigFile();
            //XDocument xdoc = XDocument.Load(_xmlPath);
            //// IEnumerable<XElement> elLegends =   from el in doc.Elements() select el;
            //// Get map setting
            //XElement xmlMap = xdoc.Root.Elements("Map").FirstOrDefault();
            //_config.ShowMapGrid = xmlMap.Attribute("ShowGrid").Value;
            //_config.BottomAxes = xmlMap.Attribute("BottomAxes").Value;
            //_config.MDInverse = Convert.ToBoolean(xmlMap.Attribute("MDInverse").Value);
            //_config.CDInverse = Convert.ToBoolean(xmlMap.Attribute("CDInverse").Value);

            //// Get legend steeing
            //foreach (var item in _config.Legends)
            //{
            //    XElement xmlLegend = xdoc.Root.Elements("Legend").Where(el => (string)el.Attribute("ClassID") == item.ClassID).FirstOrDefault();
            //    if (xmlLegend != null)
            //    {
            //        item.Shape = xmlLegend.Attribute("Shape").Value;
            //        item.Color = xmlLegend.Attribute("Color").Value;
            //    }
            //}

            #endregion

            //Settings setting = new Settings(_legends);
            //setting.ShowDialog();
            Settings setting = new Settings(_config);
            setting.ShowDialog();
        }
        #endregion

        private void chartControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void chartControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    ChartHitInfo hi = chartControl.CalcHitInfo(e.X, e.Y);

            //    if (hi.SeriesPoint != null)
            //    {
            //        Series seriesPoint = (Series)hi.Series;
            //        //DataRow[] rows = _dtbFlaws.Select();
            //        //IEnumerable<DataRow> result = rows.Where(row => row["FlawID"].ToString().Equals(seriesPoint.Name));

            //        //JobHelper.Job.SetOffline();
            //        //FlawForm ff = new FlawForm(result.First(), _units);
            //        //ff.ShowDialog();
            //    }
            //}
            if (e.Button == MouseButtons.Left)
            {
                ChartHitInfo hi = chartControl.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hi.SeriesPoint;

                if (point != null)
                {
                    string condifion = string.Format("CD = {0} AND MD = {1}", point.Argument, point.Values.FirstOrDefault().ToString());
                    DataRow flaw = _flawData.Select(condifion).FirstOrDefault();

                    FlawForm flawForm = new FlawForm(_jobInfo.NumberOfStations, flaw);
                    flawForm.ShowDialog();
                    //string argument = "Argument: " + point.Argument.ToString();
                    //string values = "Value(s): " + point.Values[0].ToString();

                    //if (point.Values.Length > 1)
                    //{
                    //    for (int i = 1; i < point.Values.Length; i++)
                    //    {
                    //        values = values + ", " + point.Values[i].ToString();
                    //    }
                    //}

                    //// Show the tooltip.
                    //this.Text = argument + values;
                    //MessageBox.Show(point.Argument.ToString() + ", " + point.Values[0].ToString());
                }
                else
                {

                }
            }
        }
    }
}
