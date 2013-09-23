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
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;

namespace NMap
{
    [Export(typeof(IWRPlugIn))]
    public partial class MapWindow : UserControl, IWRPlugIn, IWRMapWindow, IOnFlaws, IOnJobLoaded, IOnJobStarted, IOnClassifyFlaw, IOnWebDBConnected, IOnUnitsChanged
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

        private List<Unit> _units;
        private string _xmlUnitsPath;
        private Unit _currentFlawMapCD;
        private Unit _currentFlawMapMD;

        public MapWindow()
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
            //diagram.AxisX.Range.MaxValue = ;
            diagram.AxisX.Range.MaxValue = Convert.ToDecimal(JobHelper.Lanes.LastOrDefault().StopCD) * _currentFlawMapCD.Conversion;
            //diagram.AxisX.Range.ScrollingRange.SetMinMaxValues(0, );
            diagram.AxisX.NumericOptions.Format = NumericFormat.Number;
            diagram.AxisX.NumericOptions.Precision = 2;
            diagram.AxisX.Reverse = _config.CDInverse;
            diagram.AxisX.GridLines.Visible = _config.ShowMapGrid == "On" ? true : false;
            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            //diagram.AxisX.GridSpacingAuto = false;
            diagram.AxisX.Range.Auto = true;
            diagram.AxisX.Range.ScrollingRange.Auto = true;

            // Setting AxisY format
            diagram.EnableAxisYZooming = true;
            diagram.EnableAxisYScrolling = true;
            diagram.AxisY.Range.MinValue = 0;
            //diagram.AxisY.Range.MaxValue = ;
            //diagram.AxisY.Range.ScrollingRange.SetMinMaxValues(0, );
            diagram.AxisY.NumericOptions.Format = NumericFormat.Number;
            diagram.AxisY.NumericOptions.Precision = 2;
            diagram.AxisY.Reverse = _config.MDInverse;
            diagram.AxisY.GridLines.Visible = _config.ShowMapGrid == "On" ? true : false;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            //diagram.AxisY.GridSpacingAuto = false;

            if (_config.BottomAxes == "CD")
            {
                diagram.Rotated = false;
            }
            else
            {
                diagram.Rotated = true;
            }

            chartControl.Series.Clear();
            _flawData.Clear();
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

        // 將xml單位換算值儲存至 _units 物件
        private void LoadXmlToUnitsObject(string xml)
        {
            _units = new List<Unit>();
            // load xml data to dictionary.
            XmlDocument document = new XmlDocument();
            document.Load(xml);
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator node = navigator.Select("//Components/Component");

            while (node.MoveNext())
            {
                int unitIndex = Convert.ToInt32(node.Current.SelectSingleNode("@unit").Value) + 1; // Xpath's index start from 1.
                string expr_conversion = String.Format("//Units/Unit[{0}]/@conversion", unitIndex);
                string expr_symbol = String.Format("//Units/Unit[{0}]/@symbol", unitIndex);
                decimal convertion = Convert.ToDecimal(navigator.SelectSingleNode(expr_conversion).Value);
                string symbol = navigator.SelectSingleNode(expr_symbol).Value;
                string componentName = node.Current.SelectSingleNode("@name").Value;
                Unit unit = new Unit(componentName, symbol, convertion);
                _units.Add(unit);
            }
        }

        private void SetScrollingRange()
        {
            double scrollingRangeMax;
            double scrollingRangeMin;
            XYDiagram diagram = (XYDiagram)chartControl.Diagram;
            diagram.AxisY.Range.Auto = false;
            if (_currentFlawMapMD.Symbol.Equals("m"))
            {
                scrollingRangeMax = Convert.ToDouble(_flawData.Rows[_flawData.Rows.Count - 1]["MD"]) + 0.1;
                scrollingRangeMin = scrollingRangeMax - 0.2 < 0 ? 0 : (scrollingRangeMax - 0.2);
            }
            else if (_currentFlawMapMD.Symbol.Equals("mm"))
            {
                scrollingRangeMax = Convert.ToDouble(_flawData.Rows[_flawData.Rows.Count - 1]["MD"]) + 100;
                scrollingRangeMin = scrollingRangeMax - 200 < 0 ? 0 : (scrollingRangeMax - 200);
            }
            else
            {
                scrollingRangeMax = Convert.ToDouble(_flawData.Rows[_flawData.Rows.Count - 1]["MD"]) + 1;
                scrollingRangeMin = scrollingRangeMax - 2 < 0 ? 0 : (scrollingRangeMax - 2);
            }
            diagram.AxisY.Range.SetInternalMinMaxValues(scrollingRangeMin, scrollingRangeMax);
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
            _xmlUnitsPath = unitsXMLPath;
            LoadXmlToUnitsObject(unitsXMLPath); // Save units value to "_units"
            _currentFlawMapCD = _units.Find(x => x.ComponentName == "Flaw Map CD");
            _currentFlawMapMD = _units.Find(x => x.ComponentName == "Flaw Map MD");
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
                row["CD"] = Convert.ToDecimal(flaw.CD) * _currentFlawMapCD.Conversion;
                row["MD"] = Convert.ToDecimal(flaw.MD) * _currentFlawMapMD.Conversion;
                row["Width"] = Convert.ToDecimal(flaw.Width);
                row["Length"] = Convert.ToDecimal(flaw.Length);
                _flawData.Rows.Add(row);
            }

            foreach (Series series in chartControl.Series)
            {
                series.DataSource = QueryDataTable(_flawData, "FlawClass = '" + series.Name + "'", ""); ;
            }

            SetScrollingRange();
            //diagram.AxisY.Range.ScrollingRange.MaxValue = diagram.AxisY.Range.MaxValue;

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
            JobHelper.FlawTypes = flawTypes;
            JobHelper.JobInfo = jobInfo;
            JobHelper.Lanes = lanes;
            JobHelper.SeverityInfo = severityInfo;

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
            ConfigHelper ch = new ConfigHelper();
            Config configFile = ch.GetConfigFile();
            Config newConfig = new Config() { Legends = new List<NMap.Model.Legend>() };

            List<NMap.Model.Legend> mcsLegends = new List<NMap.Model.Legend>();
            foreach (var item in legend)
            {
                NMap.Model.Legend l = new NMap.Model.Legend();
                l.ClassID = item.ClassID.ToString();
                l.Color = String.Format("#{0:X2}{1:X2}{2:X2}",
                              ColorTranslator.FromWin32((int)item.Color).R,
                              ColorTranslator.FromWin32((int)item.Color).G,
                              ColorTranslator.FromWin32((int)item.Color).B);
                l.Shape = "Circle";
                l.Name = item.Name;
                l.OriginLegend = item;
                mcsLegends.Add(l);
            }

            // Compare legends in config file and mcs file
            if (configFile.Legends.Count() != 0)
            {
                if (configFile.Legends.Count() == mcsLegends.Count())
                {
                    int i = 0;
                    foreach (var item in mcsLegends)
                    {
                        configFile.Legends[i].ClassID = item.ClassID;
                        configFile.Legends[i].Name = item.Name;
                        i++;
                    }
                }
                else if (configFile.Legends.Count() > mcsLegends.Count())
                {
                    int i = 0;
                    foreach (var item in mcsLegends)
                    {
                        configFile.Legends[i].ClassID = item.ClassID;
                        configFile.Legends[i].Name = item.Name;
                        i++;
                    }
                    configFile.Legends.RemoveRange(i, configFile.Legends.Count() - i);
                }
                else
                {
                    int i = 0;
                    int legendQuantity = configFile.Legends.Count();
                    foreach (var item in mcsLegends)
                    {
                        if (legendQuantity > i)
                        {
                            configFile.Legends[i].ClassID = item.ClassID;
                            configFile.Legends[i].Name = item.Name;
                        }
                        else
                        {
                            configFile.Legends.Add(item);
                        }
                        i++;
                    }
                }
            }
            else
            {
                configFile.Legends = mcsLegends;
            }

            // Reset config
            ch.CreateConfigFile(configFile);
        }

        #endregion

        #region IOnJobStarted 成員

        public void OnJobStarted(int jobKey)
        {
            btnSetting.Enabled = false;
            JobHelper.JobKey = jobKey;

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
                seriesView.PointMarkerOptions.Size = 15;
                seriesView.PointMarkerOptions.Kind = _dicSeriesShape[legend.Shape];
                seriesView.Color = System.Drawing.ColorTranslator.FromHtml(legend.Color);

                chartControl.Series.Add(series);
            }
        }

        #endregion

        #region IOnWebDBConnected 成員

        public void OnWebDBConnected(IWebDBConnectionInfo info)
        {
            if (info.UseTrustedConnection == 1)
            {
                JobHelper.DbConnectString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=true;", info.ServerName, info.DatabaseName);
            }
            else
            {
                JobHelper.DbConnectString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", info.ServerName, info.DatabaseName, info.UserName, info.Password);
            }
        }

        #endregion

        #region IOnUnitsChanged 成員

        public void OnUnitsChanged()
        {
            if (!String.IsNullOrEmpty(_xmlUnitsPath))
            {
                // 讀取單位及換算資料
                LoadXmlToUnitsObject(_xmlUnitsPath);
                Unit newFlawMapCD = _units.Find(x => x.ComponentName == "Flaw Map CD");
                Unit newFlawMapMD = _units.Find(x => x.ComponentName == "Flaw Map MD");

                // 重新計算 map 上各點座標資料
                foreach (DataRow row in _flawData.Rows)
                {
                    row["CD"] = Convert.ToDecimal(row["CD"]) / _currentFlawMapCD.Conversion * newFlawMapCD.Conversion;
                    row["MD"] = Convert.ToDecimal(row["MD"]) / _currentFlawMapMD.Conversion * newFlawMapMD.Conversion;
                }

                _currentFlawMapCD = newFlawMapCD;
                _currentFlawMapMD = newFlawMapMD;

                chartControl.Series.Clear();
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
                    seriesView.PointMarkerOptions.Size = 15;
                    seriesView.PointMarkerOptions.Kind = _dicSeriesShape[legend.Shape];
                    seriesView.Color = System.Drawing.ColorTranslator.FromHtml(legend.Color);

                    chartControl.Series.Add(series);
                }

                foreach (Series series in chartControl.Series)
                {
                    series.DataSource = QueryDataTable(_flawData, "FlawClass = '" + series.Name + "'", ""); ;
                }

                SetScrollingRange();

                XYDiagram diagram = (XYDiagram)chartControl.Diagram;
                diagram.AxisX.Range.MinValue = 0;
                diagram.AxisX.Range.MaxValue = Convert.ToDecimal(JobHelper.Lanes.LastOrDefault().StopCD) / _currentFlawMapCD.Conversion * newFlawMapCD.Conversion;
                diagram.AxisX.Range.Auto = true;
                diagram.AxisX.Range.ScrollingRange.Auto = true;
            }
        }

        #endregion

        #endregion

        #region Events 

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
            //diagram.AxisX.Range.Auto = false;
            //diagram.AxisX.Range.ScrollingRange.Auto = false;

        }

        /// <summary>
        /// 開啟設定視窗 設定 Map 相關參數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            // Read XML
            ConfigHelper ch = new ConfigHelper();
            _config = ch.GetConfigFile();

            Settings setting = new Settings(_config);
            setting.ShowDialog();
        }

        private void chartControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ChartHitInfo hi = chartControl.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hi.SeriesPoint;

                if (point != null)
                {
                    decimal cd = Convert.ToDecimal(point.Argument);
                    decimal md = Convert.ToDecimal(point.Values.FirstOrDefault());

                    string condifion = string.Format("CD = {0} AND MD = {1}", cd, md);
                    DataRow flaw = _flawData.Select(condifion).FirstOrDefault();

                    FlawForm flawForm = new FlawForm(flaw, _currentFlawMapCD.Conversion, _currentFlawMapMD.Conversion);
                    flawForm.ShowDialog();
                }
                else
                {

                }
            }
        }

        #endregion
    }
}
