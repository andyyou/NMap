using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NMap
{
    public partial class Settings : Form
    {
        public Settings()
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

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
    }
}
