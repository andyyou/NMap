namespace NMap
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbMapSettings = new System.Windows.Forms.GroupBox();
            this.chkCDInverse = new System.Windows.Forms.CheckBox();
            this.chkMDInverse = new System.Windows.Forms.CheckBox();
            this.cmbBottomAxes = new System.Windows.Forms.ComboBox();
            this.lblBottomAxie = new System.Windows.Forms.Label();
            this.lblMapGridShow = new System.Windows.Forms.Label();
            this.gbSeriesSetting = new System.Windows.Forms.GroupBox();
            this.dgvFlawLegends = new System.Windows.Forms.DataGridView();
            this.cmbShowMapGrid = new System.Windows.Forms.ComboBox();
            this.grbMapSettings.SuspendLayout();
            this.gbSeriesSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlawLegends)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(377, 447);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbMapSettings
            // 
            this.grbMapSettings.Controls.Add(this.cmbShowMapGrid);
            this.grbMapSettings.Controls.Add(this.chkCDInverse);
            this.grbMapSettings.Controls.Add(this.chkMDInverse);
            this.grbMapSettings.Controls.Add(this.cmbBottomAxes);
            this.grbMapSettings.Controls.Add(this.lblBottomAxie);
            this.grbMapSettings.Controls.Add(this.lblMapGridShow);
            this.grbMapSettings.Location = new System.Drawing.Point(12, 12);
            this.grbMapSettings.Name = "grbMapSettings";
            this.grbMapSettings.Size = new System.Drawing.Size(440, 73);
            this.grbMapSettings.TabIndex = 0;
            this.grbMapSettings.TabStop = false;
            this.grbMapSettings.Text = "Map Setting";
            // 
            // chkCDInverse
            // 
            this.chkCDInverse.AutoSize = true;
            this.chkCDInverse.Location = new System.Drawing.Point(108, 48);
            this.chkCDInverse.Name = "chkCDInverse";
            this.chkCDInverse.Size = new System.Drawing.Size(77, 16);
            this.chkCDInverse.TabIndex = 6;
            this.chkCDInverse.Text = "CD Inverse";
            this.chkCDInverse.UseVisualStyleBackColor = true;
            // 
            // chkMDInverse
            // 
            this.chkMDInverse.AutoSize = true;
            this.chkMDInverse.Location = new System.Drawing.Point(12, 48);
            this.chkMDInverse.Name = "chkMDInverse";
            this.chkMDInverse.Size = new System.Drawing.Size(79, 16);
            this.chkMDInverse.TabIndex = 5;
            this.chkMDInverse.Text = "MD Inverse";
            this.chkMDInverse.UseVisualStyleBackColor = true;
            // 
            // cmbBottomAxes
            // 
            this.cmbBottomAxes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBottomAxes.FormattingEnabled = true;
            this.cmbBottomAxes.Items.AddRange(new object[] {
            "CD",
            "MD"});
            this.cmbBottomAxes.Location = new System.Drawing.Point(342, 19);
            this.cmbBottomAxes.Name = "cmbBottomAxes";
            this.cmbBottomAxes.Size = new System.Drawing.Size(82, 20);
            this.cmbBottomAxes.TabIndex = 4;
            // 
            // lblBottomAxie
            // 
            this.lblBottomAxie.AutoSize = true;
            this.lblBottomAxie.Location = new System.Drawing.Point(257, 23);
            this.lblBottomAxie.Name = "lblBottomAxie";
            this.lblBottomAxie.Size = new System.Drawing.Size(78, 12);
            this.lblBottomAxie.TabIndex = 3;
            this.lblBottomAxie.Text = "Bottom Axes：";
            // 
            // lblMapGridShow
            // 
            this.lblMapGridShow.AutoSize = true;
            this.lblMapGridShow.Location = new System.Drawing.Point(10, 23);
            this.lblMapGridShow.Name = "lblMapGridShow";
            this.lblMapGridShow.Size = new System.Drawing.Size(91, 12);
            this.lblMapGridShow.TabIndex = 0;
            this.lblMapGridShow.Text = "Show Map Grid：";
            // 
            // gbSeriesSetting
            // 
            this.gbSeriesSetting.Controls.Add(this.dgvFlawLegends);
            this.gbSeriesSetting.Location = new System.Drawing.Point(12, 98);
            this.gbSeriesSetting.Name = "gbSeriesSetting";
            this.gbSeriesSetting.Size = new System.Drawing.Size(440, 343);
            this.gbSeriesSetting.TabIndex = 1;
            this.gbSeriesSetting.TabStop = false;
            this.gbSeriesSetting.Text = "Defects Setting";
            // 
            // dgvFlawLegends
            // 
            this.dgvFlawLegends.AllowUserToAddRows = false;
            this.dgvFlawLegends.AllowUserToDeleteRows = false;
            this.dgvFlawLegends.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFlawLegends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFlawLegends.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFlawLegends.Location = new System.Drawing.Point(12, 21);
            this.dgvFlawLegends.Name = "dgvFlawLegends";
            this.dgvFlawLegends.RowHeadersVisible = false;
            this.dgvFlawLegends.RowTemplate.Height = 24;
            this.dgvFlawLegends.Size = new System.Drawing.Size(412, 305);
            this.dgvFlawLegends.TabIndex = 0;
            // 
            // cmbShowMapGrid
            // 
            this.cmbShowMapGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShowMapGrid.FormattingEnabled = true;
            this.cmbShowMapGrid.Items.AddRange(new object[] {
            "Off",
            "On"});
            this.cmbShowMapGrid.Location = new System.Drawing.Point(108, 19);
            this.cmbShowMapGrid.Name = "cmbShowMapGrid";
            this.cmbShowMapGrid.Size = new System.Drawing.Size(82, 20);
            this.cmbShowMapGrid.TabIndex = 7;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 482);
            this.ControlBox = false;
            this.Controls.Add(this.gbSeriesSetting);
            this.Controls.Add(this.grbMapSettings);
            this.Controls.Add(this.btnCancel);
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.grbMapSettings.ResumeLayout(false);
            this.grbMapSettings.PerformLayout();
            this.gbSeriesSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlawLegends)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grbMapSettings;
        private System.Windows.Forms.CheckBox chkCDInverse;
        private System.Windows.Forms.CheckBox chkMDInverse;
        private System.Windows.Forms.ComboBox cmbBottomAxes;
        private System.Windows.Forms.Label lblBottomAxie;
        private System.Windows.Forms.Label lblMapGridShow;
        private System.Windows.Forms.GroupBox gbSeriesSetting;
        private System.Windows.Forms.DataGridView dgvFlawLegends;
        private System.Windows.Forms.ComboBox cmbShowMapGrid;
    }
}