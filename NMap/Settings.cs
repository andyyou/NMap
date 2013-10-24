using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRPlugIn;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using NMap.Model;

namespace NMap
{
    public partial class Settings : Form
    {
        private List<NMap.Model.Legend> _legends = new List<NMap.Model.Legend>();
        private static string _xmlPath = Path.GetDirectoryName(
                                         Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) +
                                         @"\..\PlugIns\NMap\legends.xml";
        private Dictionary<string, string> shapes = new Dictionary<string, string>(){
            { "Triangle", "▲" },
            { "InvertedTriangle", "▼" },
            { "Square", "■" },
            { "Circle", "●" },
            { "Plus", "✚" },
            { "Cross", "✖" },
            { "Star", "★" }
        };

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="legends"></param>
        public Settings(Config config)
        {
            InitializeComponent();

            cmbShowMapGrid.SelectedItem = config.ShowMapGrid;
            cmbBottomAxes.SelectedItem = config.BottomAxes;
            chkMDInverse.Checked = config.MDInverse;
            chkCDInverse.Checked = config.CDInverse;
            lblBackgroundColor.BackColor = System.Drawing.ColorTranslator.FromHtml(config.BackgroundColor);
            lblGridColor.BackColor = System.Drawing.ColorTranslator.FromHtml(config.GridColor);
            txtXPrecision.Text = config.XPrecision;
            txtYPrecision.Text = config.YPrecision;
            txtScrollingRange.Text = config.ScrollingRange;

            // Initialize DataGridView
            List<Column> columns = new List<Column>();
            Column classId = new Column(0, "ClassID", 60);
            Column name = new Column(1, "Name", 120);
            Column color = new Column(2, "Color", 80);
            Column shape = new Column(3, "Shape", 80);
            columns.Add(classId);
            columns.Add(name);
            columns.Add(color);
            columns.Add(shape);
            foreach (Column c in columns)
            {
                if (c.Name == "Shape")
                {
                    DataGridViewComboBoxColumn cmbShape = new DataGridViewComboBoxColumn();
                    cmbShape.HeaderText = c.Name;
                    cmbShape.DisplayIndex = c.Index;
                    cmbShape.DataPropertyName = c.Name;
                    cmbShape.Width = c.Width;
                    cmbShape.DataSource = new BindingSource(shapes, null);
                    cmbShape.DisplayMember = "Value";
                    cmbShape.ValueMember = "Key";
                    dgvFlawLegends.Columns.Add(cmbShape);
                }
                else
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    DataGridViewColumn column = new DataGridViewColumn();
                    column.CellTemplate = cell;
                    column.Name = c.Name;
                    column.HeaderText = c.Name;
                    column.Width = c.Width;
                    column.DataPropertyName = c.Name;
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    if (c.Name == "ClassID" || c.Name == "Name" || c.Name == "Color")
                    {
                        column.ReadOnly = true;
                    }
                    dgvFlawLegends.Columns.Add(column);
                }
            }
            dgvFlawLegends.MultiSelect = false;
            dgvFlawLegends.AutoGenerateColumns = false;

            // Load legends
            _legends = config.Legends;
            dgvFlawLegends.DataSource = _legends;
        }

        /// <summary>
        /// 儲存設定檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save xml
            XDocument xdoc = XDocument.Load(_xmlPath);
            xdoc.Root.Elements().ToList().ForEach(el => el.Remove()); ;

            // Add Map setting
            XElement element = new XElement("Map");
            element.SetAttributeValue("ShowGrid", cmbShowMapGrid.SelectedItem);
            element.SetAttributeValue("BottomAxes", cmbBottomAxes.SelectedItem);
            element.SetAttributeValue("MDInverse", chkMDInverse.Checked);
            element.SetAttributeValue("CDInverse", chkCDInverse.Checked);
            element.SetAttributeValue("BackgroundColor", String.Format("#{0:X2}{1:X2}{2:X2}", lblBackgroundColor.BackColor.R, lblBackgroundColor.BackColor.G, lblBackgroundColor.BackColor.B));
            element.SetAttributeValue("GridColor", String.Format("#{0:X2}{1:X2}{2:X2}", lblGridColor.BackColor.R, lblGridColor.BackColor.G, lblGridColor.BackColor.B));
            element.SetAttributeValue("XPrecision", txtXPrecision.Text);
            element.SetAttributeValue("YPrecision", txtYPrecision.Text);
            element.SetAttributeValue("ScrollingRange", txtScrollingRange.Text);
            xdoc.Root.Add(element);

            // Add defects setting
            foreach (DataGridViewRow item in dgvFlawLegends.Rows)
            {
                XElement el = new XElement("Legend");
                el.SetAttributeValue("ClassID", item.Cells[0].Value);
                el.SetAttributeValue("Name", item.Cells[1].Value);
                el.SetAttributeValue("Color", item.Cells[2].Value);
                el.SetAttributeValue("Shape", item.Cells[3].Value);
                xdoc.Root.Add(el);
            }
            xdoc.Save(_xmlPath);
            MessageBox.Show("Success");

        }

        /// <summary>
        /// 關閉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 調整 Display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFlawLegends_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string color = e.Value.ToString();
                e.CellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(color);
                e.CellStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                e.Value = "";
            }
        }

        /// <summary>
        /// 改顏色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFlawLegends_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ColorDialog cd = new ColorDialog();
                DialogResult dr = cd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    dgvFlawLegends.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = String.Format("#{0:X2}{1:X2}{2:X2}", cd.Color.R, cd.Color.G, cd.Color.B);
                    dgvFlawLegends.EndEdit();
                    dgvFlawLegends.ClearSelection();
                }
            }
        }

        /// <summary>
        /// 選擇 Map 背景顏色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            
            if (colorDlg.ShowDialog() != DialogResult.Cancel)
            {
                lblBackgroundColor.BackColor = colorDlg.Color;
            }
        }

        /// <summary>
        /// 選擇 Map 格線顏色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGridColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();

            if (colorDlg.ShowDialog() != DialogResult.Cancel)
            {
                lblGridColor.BackColor = colorDlg.Color;
            }
        }
    }
}
