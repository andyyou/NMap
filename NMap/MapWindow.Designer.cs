namespace NMap
{
    partial class MapWindow
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.chartControl = new DevExpress.XtraCharts.ChartControl();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblNotice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl
            // 
            this.chartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl.BackColor = System.Drawing.Color.White;
            this.chartControl.Location = new System.Drawing.Point(15, 40);
            this.chartControl.Name = "chartControl";
            this.chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisible = true;
            this.chartControl.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.chartControl.Size = new System.Drawing.Size(620, 793);
            this.chartControl.TabIndex = 0;
            this.chartControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chartControl_MouseDoubleClick);
            this.chartControl.QueryCursor += new DevExpress.XtraCharts.QueryCursorEventHandler(this.chartControl_QueryCursor);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(560, 6);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 29);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "Settings";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(479, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 29);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "恢復原尺寸";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = true;
            this.lblNotice.BackColor = System.Drawing.Color.Transparent;
            this.lblNotice.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNotice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNotice.Location = new System.Drawing.Point(13, 21);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(252, 12);
            this.lblNotice.TabIndex = 4;
            this.lblNotice.Text = "※ 按住 Shift 及滑鼠左鍵並拖曳可局部放大";
            // 
            // MapWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NMap.Properties.Resources.BrushedSteel00;
            this.Controls.Add(this.lblNotice);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.chartControl);
            this.Name = "MapWindow";
            this.Size = new System.Drawing.Size(650, 848);
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblNotice;

    }
}
