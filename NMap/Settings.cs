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
                                         "\\..\\Parameter Files\\NMap\\legends.xml";
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="legends"></param>
        public Settings(List<NMap.Model.Legend> legends)
        {
            InitializeComponent();

            if (Properties.Settings.Default.Initial == true)
            {
                cmbShowMapGrid.SelectedItem = "On";
                cmbBottomAxes.SelectedItem = "CD";
                chkMDInverse.Checked = false;
                chkCDInverse.Checked = false;
            }
            else
            {
                cmbShowMapGrid.SelectedItem = Properties.Settings.Default.ShowMapGrid ? "On" : "Off";
                cmbBottomAxes.SelectedItem = Properties.Settings.Default.BottomAxes;
                chkMDInverse.Checked = Properties.Settings.Default.MDInverse;
                chkCDInverse.Checked = Properties.Settings.Default.CDInverse;
            }

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
                //if (c.Name == "Shape")
                //{
                    // UNDONE
                    //DataGridViewComboBoxColumn cmbShape = new DataGridViewComboBoxColumn();
                    //cmbShape.HeaderText = c.Name;
                    //cmbShape.DisplayIndex = c.Index;
                    //cmbShape.DataPropertyName = c.Name;
                    //cmbShape.Width = c.Width;
                    //cmbShape.DataSource = new BindingSource(shapes, null);
                    //cmbShape.DisplayMember = "Value";
                    //cmbShape.ValueMember = "Key";
                    //dgvFlawLegends.Columns.Add(cmbShape);
                //}
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
            dgvFlawLegends.MultiSelect = false;
            dgvFlawLegends.AutoGenerateColumns = false;

            // Load legends
            _legends = legends;
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
            foreach (DataGridViewRow item in dgvFlawLegends.Rows)
            {
                XElement el = new XElement("Legend");
                el.SetAttributeValue("ClassID", item.Cells[0].Value);
                el.SetAttributeValue("Color", item.Cells[2].Value);
                el.SetAttributeValue("Shape", item.Cells[3].Value);
                xdoc.Root.Add(el);
            }
            xdoc.Save(_xmlPath);
            MessageBox.Show("Success");

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Save settings to dll setting as temporary setting
            Properties.Settings.Default.Initial = false;
            Properties.Settings.Default.ShowMapGrid = cmbShowMapGrid.SelectedItem.ToString() == "On" ? true : false;
            Properties.Settings.Default.BottomAxes = cmbBottomAxes.SelectedItem.ToString();
            Properties.Settings.Default.MDInverse = chkMDInverse.Checked;
            Properties.Settings.Default.CDInverse = chkCDInverse.Checked;

            Properties.Settings.Default.Save();
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
    }
}
