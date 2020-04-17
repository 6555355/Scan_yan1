using System;
using System.IO;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using  System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using ZedGraph;

namespace BYHXPrinterManager.Setting
{
	public class XaarTempertureSetting : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel2;
		private ZedGraph.ZedGraphControl zg1;
		private System.Data.DataView dataView1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components = null;

		private int m_HeadNum =0;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button buttonSaveAs;
		private System.Windows.Forms.Button buttonImportCurve;
		private System.Windows.Forms.CheckBox checkBoxDisablechannelmode;

		public bool IsDirty
		{
			get //{ return isDirty; }
			{
				try
				{
					bool isDirty = false;
					for(int i = 0;i<this.dataSet1.Tables.Count;i++)
					{
						DataTable dt = this.dataSet1.Tables[i].GetChanges();
						if(dt!=null && dt.Rows.Count > 0)
						{
							isDirty = true;
						}
					}
					int j = this.oldSelecetedIndex;
					Array values = Enum.GetValues(typeof(Xaar382TempMode));

					isDirty = isDirty || (float)this.numericUpDownTargetTemp.Value != this.m_SRT_382.cTargetTemp[j] 
						|| (this.cmbTempControlMode.SelectedIndex!=-1&&((int)((Xaar382TempMode[])values)[this.cmbTempControlMode.SelectedIndex]) != this.m_SRT_382.cTempControlMode[j]);
					return isDirty;
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
					return false;
				}
			}
		}
		private Color mSelectedCureColor = Color.Red;
		private Color mSelectedDefoultColor = Color.LimeGreen;
		private Curve382Header[] m_Curve382Header;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.NumericUpDown numericUpDownChMode;
		private System.Windows.Forms.NumericUpDown numericUpDownVTrim;
		private System.Windows.Forms.NumericUpDown numericUpDownTemperature;
		private System.Windows.Forms.NumericUpDown numericUpDownVoltage;
		private System.Windows.Forms.Label labelVoltage;
		private bool bIniting = false;
		private SRealTimeCurrentInfo_382 m_SRT_382 = new SRealTimeCurrentInfo_382();
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownTargetTemp;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbTempControlMode;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbTempControlModeAll;
		private SPrinterProperty m_sPrinterProperty;
		private System.Windows.Forms.Timer timerRefresh;
		private System.Windows.Forms.CheckBox ckb_AutoGetTemp;
        private Panel panel14;
        private NumericUpDown numTemperature4;
        private System.Windows.Forms.Label label10;
        private Panel panel13;
        private NumericUpDown numTemperature3;
        private System.Windows.Forms.Label label9;
        private Panel panel12;
        private NumericUpDown numTemperature2;
        private System.Windows.Forms.Label label8;
        private Panel panel11;
        private NumericUpDown numTemperature1;
        private System.Windows.Forms.Label label7;
        private int oldSelecetedIndex = 0;
        private const int TEMP_NUM_PER_HEAD = 3;
		public XaarTempertureSetting()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			// Get a reference to the GraphPane instance in the ZedGraphControl
			GraphPane myPane = zg1.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "Temperature-VTrim curve";
			myPane.XAxis.Title.Text = "Temperture";
			myPane.YAxis.Title.Text = "VTrim";

			myPane.XAxis.Scale.FontSpec.FontColor = Color.Blue;
			myPane.XAxis.Title.FontSpec.FontColor = Color.Blue;
			// Show the x axis grid
			myPane.XAxis.MajorGrid.IsVisible = true;
			// Manually set the axis range
			myPane.XAxis.Scale.MajorStep = 5;
			myPane.XAxis.Scale.Min = 0;
			myPane.XAxis.Scale.Max = 50;

			// Make the Y axis scale red
			myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			myPane.YAxis.MajorTic.IsOpposite = false;
			myPane.YAxis.MinorTic.IsOpposite = false;
			// Don't display the Y zero line
			myPane.YAxis.MajorGrid.IsZeroLine = true;
			// Align the Y axis labels so they are flush to the axis
			myPane.YAxis.Scale.Align = AlignP.Inside;
			// Manually set the axis range
			myPane.YAxis.Scale.Min = -150;
			myPane.YAxis.Scale.Max = 150;
			// Show the y axis grid
			myPane.YAxis.MajorGrid.IsVisible = true;

			// Fill the axis background with a gradient
			myPane.Chart.Fill = new Fill( Color.White, Color.LightGray, 45.0f );

			// Add a text box with instructions
			//			TextObj text = new TextObj(
			//				"Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse",
			//				0.05f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom );
			//			text.FontSpec.StringAlignment = StringAlignment.Near;
			//			myPane.GraphObjList.Add( text );

			// Enable scrollbars if needed
			//			zg1.IsShowHScrollBar = true;
			//			zg1.IsShowVScrollBar = true;
			//			zg1.IsAutoScrollRange = true;
			//			zg1.IsScrollY2 = true;

			// OPTIONAL: Show tooltips when the mouse hovers over a point
			zg1.IsShowPointValues = true;
			zg1.PointValueEvent += new ZedGraphControl.PointValueHandler( MyPointValueHandler );
			// Size the control to fit the window
			SetSize();

			// Tell ZedGraph to calculate the axis ranges
			// Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
			// up the proper scrolling parameters
			zg1.AxisChange();
			// Make sure the Graph gets redrawn
			zg1.Invalidate();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XaarTempertureSetting));
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataView1 = new System.Data.DataView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.numTemperature4 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.numTemperature3 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.numTemperature2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.numTemperature1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTempControlMode = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.numericUpDownVoltage = new System.Windows.Forms.NumericUpDown();
            this.labelVoltage = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.numericUpDownTargetTemp = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.numericUpDownTemperature = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.numericUpDownVTrim = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numericUpDownChMode = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxDisablechannelmode = new System.Windows.Forms.CheckBox();
            this.buttonImportCurve = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckb_AutoGetTemp = new System.Windows.Forms.CheckBox();
            this.cmbTempControlModeAll = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature4)).BeginInit();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature3)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature2)).BeginInit();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature1)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVoltage)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetTemp)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemperature)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVTrim)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChMode)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // zg1
            // 
            resources.ApplyResources(this.zg1, "zg1");
            this.zg1.IsEnableHPan = false;
            this.zg1.IsEnableHZoom = false;
            this.zg1.IsEnableVPan = false;
            this.zg1.IsEnableVZoom = false;
            this.zg1.IsEnableWheelZoom = false;
            this.zg1.IsPrintFillPage = false;
            this.zg1.IsPrintKeepAspectRatio = false;
            this.zg1.IsPrintScaleAll = false;
            this.zg1.IsShowContextMenu = false;
            this.zg1.IsShowCopyMessage = false;
            this.zg1.IsShowPointValues = true;
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGrid1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel3);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowSorting = false;
            this.dataGrid1.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGrid1.BackColor = System.Drawing.Color.Gainsboro;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataGrid1.CaptionBackColor = System.Drawing.Color.DarkKhaki;
            resources.ApplyResources(this.dataGrid1, "dataGrid1");
            this.dataGrid1.CaptionForeColor = System.Drawing.Color.Black;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.DataSource = this.dataView1;
            this.dataGrid1.FlatMode = true;
            this.dataGrid1.ForeColor = System.Drawing.Color.Black;
            this.dataGrid1.GridLineColor = System.Drawing.Color.Silver;
            this.dataGrid1.HeaderBackColor = System.Drawing.Color.Black;
            this.dataGrid1.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.HeaderForeColor = System.Drawing.Color.White;
            this.dataGrid1.LinkColor = System.Drawing.Color.DarkSlateBlue;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.LightGray;
            this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
            this.dataGrid1.RowHeaderWidth = 20;
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.Firebrick;
            this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGrid1.Resize += new System.EventHandler(this.dataGrid1_Resize);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.checkBoxDisablechannelmode);
            this.panel2.Controls.Add(this.buttonImportCurve);
            this.panel2.Controls.Add(this.buttonApply);
            this.panel2.Controls.Add(this.buttonSaveAs);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel14);
            this.panel8.Controls.Add(this.panel13);
            this.panel8.Controls.Add(this.panel12);
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel7);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Controls.Add(this.panel6);
            this.panel8.Controls.Add(this.panel5);
            this.panel8.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.numTemperature4);
            this.panel14.Controls.Add(this.label10);
            resources.ApplyResources(this.panel14, "panel14");
            this.panel14.Name = "panel14";
            // 
            // numTemperature4
            // 
            resources.ApplyResources(this.numTemperature4, "numTemperature4");
            this.numTemperature4.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numTemperature4.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numTemperature4.Name = "numTemperature4";
            this.numTemperature4.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.numTemperature3);
            this.panel13.Controls.Add(this.label9);
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // numTemperature3
            // 
            resources.ApplyResources(this.numTemperature3, "numTemperature3");
            this.numTemperature3.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numTemperature3.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numTemperature3.Name = "numTemperature3";
            this.numTemperature3.ReadOnly = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.numTemperature2);
            this.panel12.Controls.Add(this.label8);
            resources.ApplyResources(this.panel12, "panel12");
            this.panel12.Name = "panel12";
            // 
            // numTemperature2
            // 
            resources.ApplyResources(this.numTemperature2, "numTemperature2");
            this.numTemperature2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numTemperature2.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numTemperature2.Name = "numTemperature2";
            this.numTemperature2.ReadOnly = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.numTemperature1);
            this.panel11.Controls.Add(this.label7);
            resources.ApplyResources(this.panel11, "panel11");
            this.panel11.Name = "panel11";
            // 
            // numTemperature1
            // 
            resources.ApplyResources(this.numTemperature1, "numTemperature1");
            this.numTemperature1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numTemperature1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numTemperature1.Name = "numTemperature1";
            this.numTemperature1.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label5);
            this.panel10.Controls.Add(this.cmbTempControlMode);
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Name = "panel10";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cmbTempControlMode
            // 
            resources.ApplyResources(this.cmbTempControlMode, "cmbTempControlMode");
            this.cmbTempControlMode.Name = "cmbTempControlMode";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.numericUpDownVoltage);
            this.panel7.Controls.Add(this.labelVoltage);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // numericUpDownVoltage
            // 
            resources.ApplyResources(this.numericUpDownVoltage, "numericUpDownVoltage");
            this.numericUpDownVoltage.Name = "numericUpDownVoltage";
            this.numericUpDownVoltage.ReadOnly = true;
            // 
            // labelVoltage
            // 
            resources.ApplyResources(this.labelVoltage, "labelVoltage");
            this.labelVoltage.Name = "labelVoltage";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.numericUpDownTargetTemp);
            this.panel9.Controls.Add(this.label1);
            resources.ApplyResources(this.panel9, "panel9");
            this.panel9.Name = "panel9";
            // 
            // numericUpDownTargetTemp
            // 
            resources.ApplyResources(this.numericUpDownTargetTemp, "numericUpDownTargetTemp");
            this.numericUpDownTargetTemp.Name = "numericUpDownTargetTemp";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.numericUpDownTemperature);
            this.panel6.Controls.Add(this.label4);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // numericUpDownTemperature
            // 
            resources.ApplyResources(this.numericUpDownTemperature, "numericUpDownTemperature");
            this.numericUpDownTemperature.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownTemperature.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDownTemperature.Name = "numericUpDownTemperature";
            this.numericUpDownTemperature.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.numericUpDownVTrim);
            this.panel5.Controls.Add(this.label3);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // numericUpDownVTrim
            // 
            resources.ApplyResources(this.numericUpDownVTrim, "numericUpDownVTrim");
            this.numericUpDownVTrim.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numericUpDownVTrim.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.numericUpDownVTrim.Name = "numericUpDownVTrim";
            this.numericUpDownVTrim.ReadOnly = true;
            this.numericUpDownVTrim.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.numericUpDownChMode);
            this.panel4.Controls.Add(this.label2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // numericUpDownChMode
            // 
            resources.ApplyResources(this.numericUpDownChMode, "numericUpDownChMode");
            this.numericUpDownChMode.Name = "numericUpDownChMode";
            this.numericUpDownChMode.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBoxDisablechannelmode
            // 
            resources.ApplyResources(this.checkBoxDisablechannelmode, "checkBoxDisablechannelmode");
            this.checkBoxDisablechannelmode.Name = "checkBoxDisablechannelmode";
            // 
            // buttonImportCurve
            // 
            resources.ApplyResources(this.buttonImportCurve, "buttonImportCurve");
            this.buttonImportCurve.Name = "buttonImportCurve";
            this.buttonImportCurve.Click += new System.EventHandler(this.buttonImportCurve_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonSaveAs
            // 
            resources.ApplyResources(this.buttonSaveAs, "buttonSaveAs");
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ckb_AutoGetTemp);
            this.panel3.Controls.Add(this.cmbTempControlModeAll);
            this.panel3.Controls.Add(this.label6);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // ckb_AutoGetTemp
            // 
            this.ckb_AutoGetTemp.Checked = true;
            this.ckb_AutoGetTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.ckb_AutoGetTemp, "ckb_AutoGetTemp");
            this.ckb_AutoGetTemp.Name = "ckb_AutoGetTemp";
            this.ckb_AutoGetTemp.CheckedChanged += new System.EventHandler(this.ckb_AutoGetTemp_CheckedChanged);
            // 
            // cmbTempControlModeAll
            // 
            resources.ApplyResources(this.cmbTempControlModeAll, "cmbTempControlModeAll");
            this.cmbTempControlModeAll.Name = "cmbTempControlModeAll";
            this.cmbTempControlModeAll.SelectedIndexChanged += new System.EventHandler(this.cmbTempControlModeAll_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 5000;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // XaarTempertureSetting
            // 
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.panel1);
            this.Name = "XaarTempertureSetting";
            resources.ApplyResources(this, "$this");
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature4)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature3)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature2)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTemperature1)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVoltage)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetTemp)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemperature)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVTrim)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChMode)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// On resize action, resize the ZedGraphControl to fill most of the Form, with a small
		/// margin around the outside
		/// </summary>
		private void Form1_Resize( object sender, EventArgs e )
		{
			SetSize();
		}

		private void SetSize()
		{
			zg1.Location = new Point( 10, 10 );
			// Leave a small margin around the outside of the control
			zg1.Size = new Size( this.ClientRectangle.Width - 20,
				this.ClientRectangle.Height - 20 );
		}

		/// <summary>
		/// Display customized tooltips when the mouse hovers over a point
		/// </summary>
		private string MyPointValueHandler( ZedGraphControl control, GraphPane pane,
			CurveItem curve, int iPt )
		{
			// Get the PointPair that is under the mouse
			PointPair pt = curve[iPt];
			return curve.Label.Text + ":" + pane.YAxis.Title.Text+"=" + pt.Y.ToString( "f2" ) + ";" 
											+ pane.XAxis.Title.Text+"="+pt.X.ToString( "f1" );
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_sPrinterProperty = sp;
			m_HeadNum = sp.nHeadNum;
			m_Curve382Header = new Curve382Header[m_HeadNum];
			this.tabControl1.TabPages.Clear();
			this.dataSet1.Tables.Clear();	
			for(int i =0;i<m_HeadNum;i++)
			{
				TabPage tp = new TabPage("CH" + i.ToString());
				tp.BackColor = m_sPrinterProperty.GetButtonColor(i);
				this.tabControl1.TabPages.Add(tp);
//				DataColumn dataColumnIndex = new System.Data.DataColumn();
				DataColumn dataColumnTemp = new System.Data.DataColumn();
				DataColumn dataColumnVTrim = new System.Data.DataColumn();
				//				DataColumn dataColumnVol = new System.Data.DataColumn();
				DataTable tempDT = new DataTable();
				tempDT.Columns.AddRange(new DataColumn[] {
//															 dataColumnIndex
//															 ,
															 dataColumnTemp
															 ,dataColumnVTrim
															 //															 ,dataColumnVol
														 });
				tempDT.TableName = "Table"+i.ToString();
				// 
				// dataColumnIndex
				// 
//				dataColumnIndex.Caption = "Index";
//				dataColumnIndex.ColumnName = "Index";
//				dataColumnIndex.ReadOnly = true;
				// 
				// dataColumnTemp
				// 
				dataColumnTemp.Caption = "TempPoint";
				dataColumnTemp.ColumnName = "TempPoint";
				dataColumnTemp.AllowDBNull = false;
				dataColumnTemp.DataType = typeof(float);
				dataColumnTemp.Unique = true;
				// 
				// dataColumnVTrim
				// 
				dataColumnVTrim.Caption = "VTrim";
				dataColumnVTrim.ColumnName = "VTrim";
				dataColumnVTrim.AllowDBNull = false;				
				dataColumnVTrim.DataType = typeof(float);

				this.dataSet1.Tables.Add(tempDT);
				tempDT.RowChanged+=new DataRowChangeEventHandler(tempDT_RowChanged);
				tempDT.RowDeleted+=new DataRowChangeEventHandler(tempDT_RowDeleted);

			}
			this.dataGrid1.PreferredColumnWidth = (this.dataGrid1.Width-this.dataGrid1.RowHeaderWidth)/this.dataSet1.Tables[0].Columns.Count;
			this.cmbTempControlMode.Items.Clear();
			foreach(Xaar382TempMode mode in Enum.GetValues(typeof(Xaar382TempMode)))
			{
				this.cmbTempControlMode.Items.Add(mode);
				this.cmbTempControlModeAll.Items.Add(mode);
			}
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
//			this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
		}
		private void OnRenewRealTime(byte[] buf,int index)
		{
			bIniting = true;
			int bufsize = buf.Length;
			int hdIndex = this.tabControl1.SelectedIndex;
			int headsize = Marshal.SizeOf(typeof(Curve382Header));
			try
			{
				m_Curve382Header[hdIndex].m_cReserve = BitConverter.ToUInt16(buf,0);
				m_Curve382Header[hdIndex].m_cCrc = BitConverter.ToUInt16(buf,2);
				m_Curve382Header[hdIndex].m_bUse = buf[4];
				m_Curve382Header[hdIndex].m_nReserve2 = buf[5];

				this.dataSet1.Tables[index].Rows.Clear();
				PointPairList list = new PointPairList();
				for(int j =headsize;j<bufsize;j+=2)
				{
					DataRow dr = this.dataSet1.Tables[index].NewRow();
					double val1 = (buf[j]>127?(buf[j]-256):buf[j]);
					double val2 = (buf[j+1]>127?(buf[j+1]-256):buf[j+1]);
					dr.ItemArray = new object[]{
												   //											   (j-headsize)/2
												   //											   ,
												   val1
												   ,val2
												   //,"vol"
											   };
					list.Add( val1, val2);
					this.dataSet1.Tables[index].Rows.Add(dr);
				}
				UpdateCurveList(list,index);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			this.checkBoxDisablechannelmode.Checked = m_Curve382Header[hdIndex].m_bUse==0;
			bIniting = false;

		}

		public void OnRealTimeChange_Old382()
		{
			JetStatusEnum status = CoreInterface.GetBoardStatus();
			if(status == JetStatusEnum.PowerOff )
				return;
			// 每次都重新抓取,温度值更接近真实值
			m_SRT_382 =  new SRealTimeCurrentInfo_382();
			if(CoreInterface.Get382RealTimeInfo(ref m_SRT_382)!=0)
			{
                On382RealTimeInfoChanged(m_SRT_382);	
			}
			else
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.GetRealTimeInfoFail),ResString.GetProductName());
			}
//			SHeadInfoType_382 sPrinterSetting = new SHeadInfoType_382();
//			int nheadIndex = this.tabControl1.SelectedIndex;
//			if(CoreInterface.Get382HeadInfo(ref sPrinterSetting,nheadIndex) == 0)
//			{
//				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.GetRealTimeInfoFail),ResString.GetProductName());
//			}
//			else
//			{
//				m_NumericUpDownSerNo.Value = sPrinterSetting.SerNo;
//			}
		}
		public void OnRealTimeChange()
		{
			OnRealTimeChange_Old382();

			this.zg1.GraphPane.CurveList.Clear();
			int headsize = Marshal.SizeOf(typeof(Curve382Header));
			for(int i =0;i<m_HeadNum;i++)
			{
				int bufsize = 2048;
				byte[] buffer = new byte[bufsize];
				int ret = CoreInterface.Get382VtrimCurve(buffer,ref bufsize,i);
				if(ret!=0 && bufsize > 0)
				{
					byte[] buf = new byte[bufsize];
					Buffer.BlockCopy(buffer,0,buf,0,bufsize);
					OnRenewRealTime(buf,i);
				}
				this.dataSet1.Tables[i].AcceptChanges();
			}
			bIniting = true;
			this.tabControl1.SelectedIndex = 0;
			tabControl1_SelectedIndexChanged(this.tabControl1,null);
			bIniting = false;
		}

	
		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(this.tabControl1.SelectedIndex<0)
					return;
				for(int i =0; i<this.dataSet1.Tables.Count;i++)
					if(this.dataSet1.Tables[i].GetChanges()!=null)
						this.dataSet1.Tables[i].AcceptChanges();

				if(IsDirty && !this.bIniting)
				{
					DialogResult dr = MessageBox.Show(this,ResString.GetResString("SettingPage_Question"),ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question);
					if(dr == DialogResult.Yes)
						this.ApplyToBoard(true);
				}

				string strlabel = "CH"+this.tabControl1.SelectedIndex.ToString();
				this.dataView1.Table = null;
				this.dataView1.Table =  this.dataSet1.Tables[this.tabControl1.SelectedIndex];
				for(int i = 0; i< this.zg1.GraphPane.CurveList.Count;i++)
				{
					if(strlabel.Equals(this.zg1.GraphPane.CurveList[i].Label.Text))
					{
						//					this.zg1.GraphPane.CurveList[i].IsSelected = true;
						(this.zg1.GraphPane.CurveList[i] as LineItem).Line.Width = 3;
					}
					else
					{
						//					this.zg1.GraphPane.CurveList[i].IsSelected = false;
						(this.zg1.GraphPane.CurveList[i] as LineItem).Line.Width = 1;
					}
				}		
				this.zg1.Refresh();
				OnRealTimeChange_Old382();
				oldSelecetedIndex = this.tabControl1.SelectedIndex;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private byte[] OnGetRealTimeFromUI()
		{
			//			this.dataView1.Table.AcceptChanges();
			int hdIndex = this.tabControl1.SelectedIndex;
			string strlabel = "CH"+hdIndex.ToString();

			m_Curve382Header[hdIndex].m_cReserve = 0;
			m_Curve382Header[hdIndex].m_cCrc = 0;
			m_Curve382Header[hdIndex].m_bUse =(byte)(checkBoxDisablechannelmode.Checked?0:1);
			m_Curve382Header[hdIndex].m_nReserve2 = 0;

			int plen = this.dataView1.Table.Rows.Count;
			int headsize = Marshal.SizeOf(typeof(Curve382Header));
			byte[] buf = new byte[headsize + plen*2];
			if(plen >0)
			{
				PointPairList list = (PointPairList)(this.zg1.GraphPane.CurveList[strlabel].Points.Clone());
				list.Sort(SortType.XValues);
				for(int i = 0; i< list.Count;i++)
				{
					PointPair pp = list[i];
					buf[headsize+i*2]=(byte)(pp.X>0?pp.X:(256+pp.X));
					buf[headsize+i*2+1]=(byte)(pp.Y>0?pp.Y:(256+pp.Y));
					m_Curve382Header[hdIndex].m_cCrc+=(ushort)(pp.X+pp.Y);
				}
			}
			Buffer.BlockCopy(BitConverter.GetBytes(m_Curve382Header[hdIndex].m_cReserve),0,buf,0,2);
			Buffer.BlockCopy(BitConverter.GetBytes(m_Curve382Header[hdIndex].m_cCrc),0,buf,2,2);
			buf[4] = m_Curve382Header[hdIndex].m_bUse;
			buf[5] = m_Curve382Header[hdIndex].m_nReserve2;
			return buf;
		}
		public void ApplyToBoard(bool isTipApply)
		{
			int i = this.tabControl1.SelectedIndex;
			if(isTipApply)
				i = this.oldSelecetedIndex;
			this.dataSet1.Tables[i].AcceptChanges();
			byte[] buf = OnGetRealTimeFromUI();
			int ret = CoreInterface.Set382VtrimCurve(buf,buf.Length,i);
			if(ret==0)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
			}
			m_SRT_382.cTargetTemp[i] = (float)this.numericUpDownTargetTemp.Value;	

			Array values = Enum.GetValues(typeof(Xaar382TempMode));
			if(this.cmbTempControlMode.SelectedIndex != -1)
				m_SRT_382.cTempControlMode[i] = (int)((Xaar382TempMode[])values)[this.cmbTempControlMode.SelectedIndex];	
			if(CoreInterface.Set382RealTimeInfo(ref m_SRT_382)==0)
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
		}

		private void buttonImportCurve_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.CheckFileExists = true;
			ofd.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Tvc);
			DialogResult dr = ofd.ShowDialog();
			if(dr == DialogResult.OK)
			{
				byte[] buf = LoadCurveFromFile(ofd.FileName);
				this.OnRenewRealTime(buf,this.tabControl1.SelectedIndex);
			}
		}

		private void buttonSaveAs_Click(object sender, System.EventArgs e)
		{
			SaveCurveToFile();
		}

		private void buttonApply_Click(object sender, System.EventArgs e)
		{
			ApplyToBoard(false);
			this.OnRealTimeChange_Old382();
		}

		private void tempDT_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			RefreshCurve(e);
		}

		private void tempDT_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			RefreshCurve(e);
		}

		private void RefreshCurve(DataRowChangeEventArgs e)
		{
			if(bIniting)
				return;
//			bIniting = true;
//			switch(e.Action)
//			{
//				case DataRowAction.Add:
//				{
//					DataTable dt = e.Row.Table;
//					dt.Columns[0].ReadOnly = false;
//					e.Row[0] = dt.Rows.Count;
//					dt.Columns[0].ReadOnly = true;			
//				}
//					break;
//				case DataRowAction.Delete:
//				{
//					DataTable dt = e.Row.Table;
//					dt.Columns[0].ReadOnly = false;
//					for(int j =0;j<this.dataView1.Count;j++)
//						dt.Rows[j][0] = j;
//					dt.Columns[0].ReadOnly = true;			
//				}
//					break;
//			}
//			this.dataView1.Table.AcceptChanges();
//			bIniting = false;

			try
			{
				int hdIndex = this.oldSelecetedIndex;
				PointPairList list = new PointPairList();
				for(int j =0;j<this.dataView1.Count;j++)
				{
					DataRowView dr = this.dataView1[j];
					list.Add( double.Parse( dr[0].ToString()),double.Parse( dr[1].ToString()) );
				}
				UpdateCurveList(list,hdIndex);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void UpdateCurveList(PointPairList list,int headindex)
		{
			string strlabel = "CH"+headindex.ToString();
			CurveItem myCurve=this.zg1.GraphPane.CurveList[strlabel];
			if(myCurve!=null)
			{
				myCurve.Points = list;
			}
			else
			{
				// Generate a red curve with diamond symbols, and "Alpha" in the legend
				Color curverColor = m_sPrinterProperty.GetButtonColor(headindex);
				LineItem myCurve1 = this.zg1.GraphPane.AddCurve( strlabel,list, curverColor, SymbolType.Default);
				// Fill the symbols with white
				myCurve1.Symbol.Fill = new Fill( Color.White );
			}
			this.zg1.Refresh();
		}
		private void SaveCurveToFile()
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.DefaultExt = ".tvc";
			sfd.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Tvc);
			DialogResult dr = sfd.ShowDialog();
			if(dr == DialogResult.OK)
			{
				try
				{
					FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.Read);
					// Create the writer for data.
					BinaryWriter w = new BinaryWriter(fs);
					// Write data to Test.data.
					byte[] buf = OnGetRealTimeFromUI();
					w.Write(buf,0,buf.Length);
					w.Close();
					fs.Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private byte[] LoadCurveFromFile(string filepath)
		{
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				FileStream		fileStream		= new FileStream(filepath, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				buffer			= new byte[fileLen];
				int				readBytes		= 0;
				fileStream.Seek(0,SeekOrigin.Begin);
				readBytes	= binaryReader.Read(buffer,0,fileLen);
				Debug.Assert(fileLen == readBytes);
				binaryReader.Close();
				fileStream.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return buffer;
		}

		private void dataGrid1_Resize(object sender, System.EventArgs e)
		{
			if(this.dataSet1.Tables.Count > 0)
				this.dataGrid1.PreferredColumnWidth = this.dataGrid1.Width/this.dataSet1.Tables[0].Columns.Count;
		}

		private void cmbTempControlModeAll_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Array values = Enum.GetValues(typeof(Xaar382TempMode));
			int heatmode = (int)((Xaar382TempMode[])values)[this.cmbTempControlModeAll.SelectedIndex];
			for(int i = 0; i < this.tabControl1.TabPages.Count; i++)
				m_SRT_382.cTempControlMode[i] = heatmode;	
			if(CoreInterface.Set382RealTimeInfo(ref m_SRT_382)==0)
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));

			OnRealTimeChange_Old382();
		}

		private void timerRefresh_Tick(object sender, System.EventArgs e)
		{
			m_SRT_382 =  new SRealTimeCurrentInfo_382();
			if(CoreInterface.Get382RealTimeInfo(ref m_SRT_382)!=0)
			{
                On382RealTimeInfoChanged(m_SRT_382);

				string temp = string.Empty;
				for(int k = 0; k < m_SRT_382.cTemperature.Length;k++)
				{
					temp += string.Format("({0},{1})",m_SRT_382.cTargetTemp[k],m_SRT_382.cTemperature[k]);
				}
				LogWriter.WriteTemperatureLog(new string[]{temp},true);
			}
		}

        private void On382RealTimeInfoChanged(SRealTimeCurrentInfo_382 srt382)
	    {
            int i = this.tabControl1.SelectedIndex;
            int headnum = tabControl1.TabPages.Count;
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownTemperature, srt382.cTemperature[i]);
            //UIPreference.SetValueAndClampWithMinMax(this.numTemperature1, m_SRT_382.cTemperature[headnum + i * TEMP_NUM_PER_HEAD]);
            UIPreference.SetValueAndClampWithMinMax(this.numTemperature2, srt382.cTemperature[headnum + i * TEMP_NUM_PER_HEAD + 0]);
            UIPreference.SetValueAndClampWithMinMax(this.numTemperature3, srt382.cTemperature[headnum + i * TEMP_NUM_PER_HEAD + 1]);
            UIPreference.SetValueAndClampWithMinMax(this.numTemperature4, srt382.cTemperature[headnum + i * TEMP_NUM_PER_HEAD + 2]);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownVoltage, srt382.cPWM[i]);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownVTrim, srt382.cVtrim[i]);
            Debug.WriteLine(string.Format("OnRealTimeChange_Old382 m_SRT_382.cVtrim[{0}] = {1}", i, srt382.cVtrim[i]));
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownTargetTemp, srt382.cTargetTemp[i]);
            int j = 0;
            foreach (Xaar382TempMode mode in Enum.GetValues(typeof(Xaar382TempMode)))
            {
                if ((int)mode == srt382.cTempControlMode[i])
                    break;
                else
                    j++;
            }
            UIPreference.SetSelectIndexAndClampWithMax(this.cmbTempControlMode, j);	
	    }

	    public void StartTimer()
		{
			this.timerRefresh.Start();
			LogWriter.WriteTemperatureLog(new string[]{string.Empty},false);
		}
		public void StopTimer()
		{
			this.timerRefresh.Stop();
		}

		private void ckb_AutoGetTemp_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.ckb_AutoGetTemp.Checked)
				this.StartTimer();
			else
				this.StopTimer();
		}
	}
}

