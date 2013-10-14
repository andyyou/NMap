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
            this.btnClose = new System.Windows.Forms.Button();
            this.grbMapSettings = new System.Windows.Forms.GroupBox();
            this.txtYPrecision = new System.Windows.Forms.TextBox();
            this.lblYPrecision = new System.Windows.Forms.Label();
            this.txtXPrecision = new System.Windows.Forms.TextBox();
            this.lblXPrecision = new System.Windows.Forms.Label();
            this.lblGridColor = new System.Windows.Forms.Label();
            this.lblBackgroundColor = new System.Windows.Forms.Label();
            this.lblGrid = new System.Windows.Forms.Label();
            this.lblBackground = new System.Windows.Forms.Label();
            this.cmbShowMapGrid = new System.Windows.Forms.ComboBox();
            this.chkCDInverse = new System.Windows.Forms.CheckBox();
            this.chkMDInverse = new System.Windows.Forms.CheckBox();
            this.cmbBottomAxes = new System.Windows.Forms.ComboBox();
            this.lblBottomAxie = new System.Windows.Forms.Label();
            this.lblMapGridShow = new System.Windows.Forms.Label();
            this.gbSeriesSetting = new System.Windows.Forms.GroupBox();
            this.dgvFlawLegends = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMapSize = new System.Windows.Forms.Label();
            this.txtMapSize = new System.Windows.Forms.TextBox();
            this.grbMapSettings.SuspendLayout();
            this.gbSeriesSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlawLegends)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(377, 473);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grbMapSettings
            // 
            this.grbMapSettings.Controls.Add(this.txtMapSize);
            this.grbMapSettings.Controls.Add(this.lblMapSize);
            this.grbMapSettings.Controls.Add(this.txtYPrecision);
            this.grbMapSettings.Controls.Add(this.lblYPrecision);
            this.grbMapSettings.Controls.Add(this.txtXPrecision);
            this.grbMapSettings.Controls.Add(this.lblXPrecision);
            this.grbMapSettings.Controls.Add(this.lblGridColor);
            this.grbMapSettings.Controls.Add(this.lblBackgroundColor);
            this.grbMapSettings.Controls.Add(this.lblGrid);
            this.grbMapSettings.Controls.Add(this.lblBackground);
            this.grbMapSettings.Controls.Add(this.cmbShowMapGrid);
            this.grbMapSettings.Controls.Add(this.chkCDInverse);
            this.grbMapSettings.Controls.Add(this.chkMDInverse);
            this.grbMapSettings.Controls.Add(this.cmbBottomAxes);
            this.grbMapSettings.Controls.Add(this.lblBottomAxie);
            this.grbMapSettings.Controls.Add(this.lblMapGridShow);
            this.grbMapSettings.Location = new System.Drawing.Point(12, 12);
            this.grbMapSettings.Name = "grbMapSettings";
            this.grbMapSettings.Size = new System.Drawing.Size(440, 122);
            this.grbMapSettings.TabIndex = 0;
            this.grbMapSettings.TabStop = false;
            this.grbMapSettings.Text = "Map Setting";
            // 
            // txtYPrecision
            // 
            this.txtYPrecision.Location = new System.Drawing.Point(355, 68);
            this.txtYPrecision.MaxLength = 1;
            this.txtYPrecision.Name = "txtYPrecision";
            this.txtYPrecision.Size = new System.Drawing.Size(33, 22);
            this.txtYPrecision.TabIndex = 13;
            // 
            // lblYPrecision
            // 
            this.lblYPrecision.AutoSize = true;
            this.lblYPrecision.Location = new System.Drawing.Point(257, 73);
            this.lblYPrecision.Name = "lblYPrecision";
            this.lblYPrecision.Size = new System.Drawing.Size(94, 12);
            this.lblYPrecision.TabIndex = 12;
            this.lblYPrecision.Text = "Y Axis Precision：";
            // 
            // txtXPrecision
            // 
            this.txtXPrecision.Location = new System.Drawing.Point(108, 68);
            this.txtXPrecision.MaxLength = 1;
            this.txtXPrecision.Name = "txtXPrecision";
            this.txtXPrecision.Size = new System.Drawing.Size(33, 22);
            this.txtXPrecision.TabIndex = 11;
            // 
            // lblXPrecision
            // 
            this.lblXPrecision.AutoSize = true;
            this.lblXPrecision.Location = new System.Drawing.Point(10, 73);
            this.lblXPrecision.Name = "lblXPrecision";
            this.lblXPrecision.Size = new System.Drawing.Size(94, 12);
            this.lblXPrecision.TabIndex = 10;
            this.lblXPrecision.Text = "X Axis Precision：";
            // 
            // lblGridColor
            // 
            this.lblGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGridColor.Location = new System.Drawing.Point(403, 48);
            this.lblGridColor.Name = "lblGridColor";
            this.lblGridColor.Size = new System.Drawing.Size(12, 12);
            this.lblGridColor.TabIndex = 9;
            this.lblGridColor.Click += new System.EventHandler(this.lblGridColor_Click);
            // 
            // lblBackgroundColor
            // 
            this.lblBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackgroundColor.Location = new System.Drawing.Point(334, 48);
            this.lblBackgroundColor.Name = "lblBackgroundColor";
            this.lblBackgroundColor.Size = new System.Drawing.Size(12, 12);
            this.lblBackgroundColor.TabIndex = 7;
            this.lblBackgroundColor.Click += new System.EventHandler(this.lblBackgroundColor_Click);
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Location = new System.Drawing.Point(363, 48);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(38, 12);
            this.lblGrid.TabIndex = 8;
            this.lblGrid.Text = "Grid：";
            // 
            // lblBackground
            // 
            this.lblBackground.AutoSize = true;
            this.lblBackground.Location = new System.Drawing.Point(257, 48);
            this.lblBackground.Name = "lblBackground";
            this.lblBackground.Size = new System.Drawing.Size(75, 12);
            this.lblBackground.TabIndex = 6;
            this.lblBackground.Text = "Background：";
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
            this.cmbShowMapGrid.TabIndex = 1;
            // 
            // chkCDInverse
            // 
            this.chkCDInverse.AutoSize = true;
            this.chkCDInverse.Location = new System.Drawing.Point(108, 46);
            this.chkCDInverse.Name = "chkCDInverse";
            this.chkCDInverse.Size = new System.Drawing.Size(77, 16);
            this.chkCDInverse.TabIndex = 5;
            this.chkCDInverse.Text = "CD Inverse";
            this.chkCDInverse.UseVisualStyleBackColor = true;
            // 
            // chkMDInverse
            // 
            this.chkMDInverse.AutoSize = true;
            this.chkMDInverse.Location = new System.Drawing.Point(12, 46);
            this.chkMDInverse.Name = "chkMDInverse";
            this.chkMDInverse.Size = new System.Drawing.Size(79, 16);
            this.chkMDInverse.TabIndex = 4;
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
            this.cmbBottomAxes.TabIndex = 3;
            // 
            // lblBottomAxie
            // 
            this.lblBottomAxie.AutoSize = true;
            this.lblBottomAxie.Location = new System.Drawing.Point(257, 23);
            this.lblBottomAxie.Name = "lblBottomAxie";
            this.lblBottomAxie.Size = new System.Drawing.Size(78, 12);
            this.lblBottomAxie.TabIndex = 2;
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
            this.gbSeriesSetting.Location = new System.Drawing.Point(12, 140);
            this.gbSeriesSetting.Name = "gbSeriesSetting";
            this.gbSeriesSetting.Size = new System.Drawing.Size(440, 327);
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
            this.dgvFlawLegends.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFlawLegends_CellDoubleClick);
            this.dgvFlawLegends.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFlawLegends_CellFormatting);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(296, 473);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblMapSize
            // 
            this.lblMapSize.AutoSize = true;
            this.lblMapSize.Location = new System.Drawing.Point(10, 96);
            this.lblMapSize.Name = "lblMapSize";
            this.lblMapSize.Size = new System.Drawing.Size(60, 12);
            this.lblMapSize.TabIndex = 14;
            this.lblMapSize.Text = "Map Size：";
            // 
            // txtMapSize
            // 
            this.txtMapSize.Location = new System.Drawing.Point(73, 91);
            this.txtMapSize.MaxLength = 4;
            this.txtMapSize.Name = "txtMapSize";
            this.txtMapSize.Size = new System.Drawing.Size(33, 22);
            this.txtMapSize.TabIndex = 15;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 506);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbSeriesSetting);
            this.Controls.Add(this.grbMapSettings);
            this.Controls.Add(this.btnClose);
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

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grbMapSettings;
        private System.Windows.Forms.CheckBox chkCDInverse;
        private System.Windows.Forms.CheckBox chkMDInverse;
        private System.Windows.Forms.ComboBox cmbBottomAxes;
        private System.Windows.Forms.Label lblBottomAxie;
        private System.Windows.Forms.Label lblMapGridShow;
        private System.Windows.Forms.GroupBox gbSeriesSetting;
        private System.Windows.Forms.DataGridView dgvFlawLegends;
        private System.Windows.Forms.ComboBox cmbShowMapGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblGridColor;
        private System.Windows.Forms.Label lblBackgroundColor;
        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.Label lblBackground;
        private System.Windows.Forms.Label lblXPrecision;
        private System.Windows.Forms.TextBox txtXPrecision;
        private System.Windows.Forms.TextBox txtYPrecision;
        private System.Windows.Forms.Label lblYPrecision;
        private System.Windows.Forms.TextBox txtMapSize;
        private System.Windows.Forms.Label lblMapSize;
    }
}