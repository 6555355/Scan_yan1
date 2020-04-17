namespace BYHXPrinterManager.Setting
{
    partial class RealTimeChart
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RealTimeChart));
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Temperature = new System.Windows.Forms.TabPage();
            this.chart_Temperature = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.timer_RealTime = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage_Temperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Temperature)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Temperature);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage_Temperature
            // 
            this.tabPage_Temperature.Controls.Add(this.chart_Temperature);
            this.tabPage_Temperature.Controls.Add(this.checkedListBox1);
            resources.ApplyResources(this.tabPage_Temperature, "tabPage_Temperature");
            this.tabPage_Temperature.Name = "tabPage_Temperature";
            this.tabPage_Temperature.UseVisualStyleBackColor = true;
            // 
            // chart_Temperature
            // 
            chartArea2.AxisX.Interval = 60D;
            chartArea2.AxisX.Maximum = 600D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "时间（秒）";
            chartArea2.AxisY.Title = "温度（摄氏度）";
            chartArea2.Name = "ChartArea1";
            this.chart_Temperature.ChartAreas.Add(chartArea2);
            resources.ApplyResources(this.chart_Temperature, "chart_Temperature");
            legend2.Name = "Legend1";
            this.chart_Temperature.Legends.Add(legend2);
            this.chart_Temperature.Name = "chart_Temperature";
            // 
            // checkedListBox1
            // 
            resources.ApplyResources(this.checkedListBox1, "checkedListBox1");
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Name = "checkedListBox1";
            // 
            // timer_RealTime
            // 
            this.timer_RealTime.Enabled = true;
            this.timer_RealTime.Interval = 1000;
            this.timer_RealTime.Tick += new System.EventHandler(this.timer_RealTime_Tick);
            // 
            // RealTimeChart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "RealTimeChart";
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RealTimeChart_FormClosed);
            this.Load += new System.EventHandler(this.RealTimeChart_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Temperature.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Temperature)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Temperature;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Temperature;
        private System.Windows.Forms.Timer timer_RealTime;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}