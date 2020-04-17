#define ThreeSpeed  //gzw 20160121 以后默认三个速度档
/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

using BYHXPrinterManager.JobListView;
using System.IO;
using System.Xml;
using PrinterStubC.CInterface;
using PrinterStubC.Common;
namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for ToolbarSetting.
	/// </summary>
	public class ToolbarSetting : BYHXUserControl //System.Windows.Forms.UserControl
	{
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private IPrinterChange m_iPrinterChange;
		private bool m_bInitControl = false;
		private bool m_bDuringPrinting = false;
        private JobModes m_jobConfigList = new JobModes();
        private JobMediaModes m_mediaConfigList = new JobMediaModes();
        private SPrinterProperty m_sPrinterProperty;

		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStep;
		private System.Windows.Forms.Label m_LabelOrigin;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownOrigin;
		private System.Windows.Forms.CheckBox m_CheckBoxUsePrinterSetting;
		private System.Windows.Forms.CheckBox m_CheckBoxBidirection;
		private System.Windows.Forms.ComboBox m_ComboBoxPass;
		private System.Windows.Forms.ComboBox m_ComboBoxSpeed;
		private System.Windows.Forms.Label m_LabelStep;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownOriginY;
		private System.Windows.Forms.Label m_LabelOriginY;
		private System.Windows.Forms.ComboBox m_ComboBoxMediumSpeed;
		private System.Windows.Forms.Label labelSpeed;
		private System.Windows.Forms.Label labelMediumSpeed;
		private System.Windows.Forms.CheckBox m_CheckBoxHDPrint;
		private System.Windows.Forms.CheckBox checkBoxAlternatePrint;
		private System.Windows.Forms.Panel panelOriginX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panelOriginY;
		private System.Windows.Forms.Panel panelSteps;
		private System.Windows.Forms.Panel panelPass;
		private System.Windows.Forms.Panel panelSpeed;
		private System.Windows.Forms.Panel panelMediaSpeed;
		private System.Windows.Forms.Panel panel1;
        private CheckBox checkBoxAutoCenterPrint;
        private CheckBox checkBoxPauseBetweenLayers;
        private Panel panelOriginZ;
        private NumericUpDown numZWorkPos;
        private Label label2;
        private CheckBox checkBoxYBackOrigin;
        private Panel panelTshirtPlat;
        private RadioButton radioButtonAuto;
        private RadioButton radioButtonB;
        private RadioButton radioButtonA;
        private Label labelplatform;
        private Panel panelJobMode;
        private Label labelJobMode;
        public ComboBox comboBoxPringConfigList;
        private Panel panelOriginY2;
        private NumericUpDown numericOriginY2;
        private Label labelOriginY2;
        private Panel panelJobSpace;
        private NumericUpDown numJobSpace;
        private Label labelJobSpace;
        private CheckBox checkBoxFlatMode;
        private Panel panelMediaModes;
        public ComboBox comboBoxMediaMode;
        private Label labelMediaMode;
        private Panel panelStartPrintDir;
        public ComboBox comboBoxStartPrintDir;
        private Label labelStartPrintDir;
        private Panel panelYSpeed;
        private Label labelYSpeed;
        private Panel panelLayoutSetting;
        public ComboBox comboBoxLayout;
        private Label labelLayout;
        private Panel panelKeepwet;
        public Button btnMoisturize;
        private NumericUpDown m_NumericUpYSpeed;
        private Panel panelMotorSetting;
        public ComboBox comboBoxMotorControl;
        private Label labelMotorControl;
		private System.ComponentModel.IContainer components;

		public ToolbarSetting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            // TODO: Add any initialization after the InitializeComponent call
            InitComboBoxPass();
#if LIYUUSB
			m_CheckBoxUsePrinterSetting.Visible = false;
#endif
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolbarSetting));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_NumericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.m_LabelOrigin = new System.Windows.Forms.Label();
            this.m_NumericUpDownOrigin = new System.Windows.Forms.NumericUpDown();
            this.m_CheckBoxUsePrinterSetting = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxBidirection = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxPass = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelStep = new System.Windows.Forms.Label();
            this.m_NumericUpDownOriginY = new System.Windows.Forms.NumericUpDown();
            this.m_LabelOriginY = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.m_ComboBoxMediumSpeed = new System.Windows.Forms.ComboBox();
            this.labelMediumSpeed = new System.Windows.Forms.Label();
            this.m_CheckBoxHDPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxAlternatePrint = new System.Windows.Forms.CheckBox();
            this.panelOriginX = new System.Windows.Forms.Panel();
            this.panelOriginY = new System.Windows.Forms.Panel();
            this.panelSteps = new System.Windows.Forms.Panel();
            this.panelPass = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSpeed = new System.Windows.Forms.Panel();
            this.panelMediaSpeed = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelOriginY2 = new System.Windows.Forms.Panel();
            this.numericOriginY2 = new System.Windows.Forms.NumericUpDown();
            this.labelOriginY2 = new System.Windows.Forms.Label();
            this.panelMediaModes = new System.Windows.Forms.Panel();
            this.comboBoxMediaMode = new System.Windows.Forms.ComboBox();
            this.labelMediaMode = new System.Windows.Forms.Label();
            this.panelTshirtPlat = new System.Windows.Forms.Panel();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.radioButtonB = new System.Windows.Forms.RadioButton();
            this.radioButtonA = new System.Windows.Forms.RadioButton();
            this.labelplatform = new System.Windows.Forms.Label();
            this.checkBoxYBackOrigin = new System.Windows.Forms.CheckBox();
            this.checkBoxPauseBetweenLayers = new System.Windows.Forms.CheckBox();
            this.panelKeepwet = new System.Windows.Forms.Panel();
            this.btnMoisturize = new System.Windows.Forms.Button();
            this.panelLayoutSetting = new System.Windows.Forms.Panel();
            this.comboBoxLayout = new System.Windows.Forms.ComboBox();
            this.labelLayout = new System.Windows.Forms.Label();
            this.panelJobMode = new System.Windows.Forms.Panel();
            this.comboBoxPringConfigList = new System.Windows.Forms.ComboBox();
            this.labelJobMode = new System.Windows.Forms.Label();
            this.panelYSpeed = new System.Windows.Forms.Panel();
            this.m_NumericUpYSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelYSpeed = new System.Windows.Forms.Label();
            this.panelStartPrintDir = new System.Windows.Forms.Panel();
            this.comboBoxStartPrintDir = new System.Windows.Forms.ComboBox();
            this.labelStartPrintDir = new System.Windows.Forms.Label();
            this.panelOriginZ = new System.Windows.Forms.Panel();
            this.numZWorkPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panelJobSpace = new System.Windows.Forms.Panel();
            this.checkBoxFlatMode = new System.Windows.Forms.CheckBox();
            this.numJobSpace = new System.Windows.Forms.NumericUpDown();
            this.labelJobSpace = new System.Windows.Forms.Label();
            this.checkBoxAutoCenterPrint = new System.Windows.Forms.CheckBox();
            this.panelMotorSetting = new System.Windows.Forms.Panel();
            this.comboBoxMotorControl = new System.Windows.Forms.ComboBox();
            this.labelMotorControl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOriginY)).BeginInit();
            this.panelOriginX.SuspendLayout();
            this.panelOriginY.SuspendLayout();
            this.panelSteps.SuspendLayout();
            this.panelPass.SuspendLayout();
            this.panelSpeed.SuspendLayout();
            this.panelMediaSpeed.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelOriginY2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOriginY2)).BeginInit();
            this.panelMediaModes.SuspendLayout();
            this.panelTshirtPlat.SuspendLayout();
            this.panelKeepwet.SuspendLayout();
            this.panelLayoutSetting.SuspendLayout();
            this.panelJobMode.SuspendLayout();
            this.panelYSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpYSpeed)).BeginInit();
            this.panelStartPrintDir.SuspendLayout();
            this.panelOriginZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos)).BeginInit();
            this.panelJobSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobSpace)).BeginInit();
            this.panelMotorSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_NumericUpDownStep
            // 
            this.m_NumericUpDownStep.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownStep, "m_NumericUpDownStep");
            this.m_NumericUpDownStep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownStep.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownStep.Name = "m_NumericUpDownStep";
            this.m_NumericUpDownStep.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.m_NumericUpDownStep.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownStep.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_NumericUpDownStep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.m_NumericUpDownStep.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // m_LabelOrigin
            // 
            resources.ApplyResources(this.m_LabelOrigin, "m_LabelOrigin");
            this.m_LabelOrigin.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelOrigin.Name = "m_LabelOrigin";
            this.m_LabelOrigin.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // m_NumericUpDownOrigin
            // 
            this.m_NumericUpDownOrigin.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownOrigin, "m_NumericUpDownOrigin");
            this.m_NumericUpDownOrigin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownOrigin.Name = "m_NumericUpDownOrigin";
            this.m_NumericUpDownOrigin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownOrigin.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOrigin.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_NumericUpDownOrigin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.m_NumericUpDownOrigin.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // m_CheckBoxUsePrinterSetting
            // 
            resources.ApplyResources(this.m_CheckBoxUsePrinterSetting, "m_CheckBoxUsePrinterSetting");
            this.m_CheckBoxUsePrinterSetting.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxUsePrinterSetting.Checked = true;
            this.m_CheckBoxUsePrinterSetting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_CheckBoxUsePrinterSetting.Name = "m_CheckBoxUsePrinterSetting";
            this.m_CheckBoxUsePrinterSetting.UseVisualStyleBackColor = false;
            this.m_CheckBoxUsePrinterSetting.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_CheckBoxUsePrinterSetting.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_CheckBoxUsePrinterSetting.Click += new System.EventHandler(this.m_CheckBoxUsePrinterSetting_Click);
            // 
            // m_CheckBoxBidirection
            // 
            resources.ApplyResources(this.m_CheckBoxBidirection, "m_CheckBoxBidirection");
            this.m_CheckBoxBidirection.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxBidirection.Checked = true;
            this.m_CheckBoxBidirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_CheckBoxBidirection.Name = "m_CheckBoxBidirection";
            this.m_CheckBoxBidirection.UseVisualStyleBackColor = false;
            this.m_CheckBoxBidirection.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_CheckBoxBidirection.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // m_ComboBoxPass
            // 
            this.m_ComboBoxPass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPass, "m_ComboBoxPass");
            this.m_ComboBoxPass.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxPass.Items"),
            resources.GetString("m_ComboBoxPass.Items1"),
            resources.GetString("m_ComboBoxPass.Items2"),
            resources.GetString("m_ComboBoxPass.Items3"),
            resources.GetString("m_ComboBoxPass.Items4"),
            resources.GetString("m_ComboBoxPass.Items5"),
            resources.GetString("m_ComboBoxPass.Items6"),
            resources.GetString("m_ComboBoxPass.Items7"),
            resources.GetString("m_ComboBoxPass.Items8"),
            resources.GetString("m_ComboBoxPass.Items9"),
            resources.GetString("m_ComboBoxPass.Items10")});
            this.m_ComboBoxPass.Name = "m_ComboBoxPass";
            this.m_ComboBoxPass.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxPass_SelectedIndexChanged);
            this.m_ComboBoxPass.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_ComboBoxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxPass_KeyDown);
            // 
            // m_ComboBoxSpeed
            // 
            this.m_ComboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxSpeed, "m_ComboBoxSpeed");
            this.m_ComboBoxSpeed.Name = "m_ComboBoxSpeed";
            this.m_ComboBoxSpeed.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxSpeed_SelectedIndexChanged);
            this.m_ComboBoxSpeed.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_ComboBoxSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxSpeed_KeyDown);
            // 
            // m_LabelStep
            // 
            resources.ApplyResources(this.m_LabelStep, "m_LabelStep");
            this.m_LabelStep.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStep.Name = "m_LabelStep";
            this.m_LabelStep.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // m_NumericUpDownOriginY
            // 
            this.m_NumericUpDownOriginY.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownOriginY, "m_NumericUpDownOriginY");
            this.m_NumericUpDownOriginY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownOriginY.Name = "m_NumericUpDownOriginY";
            this.m_NumericUpDownOriginY.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOriginY.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_NumericUpDownOriginY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.m_NumericUpDownOriginY.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // m_LabelOriginY
            // 
            resources.ApplyResources(this.m_LabelOriginY, "m_LabelOriginY");
            this.m_LabelOriginY.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelOriginY.Name = "m_LabelOriginY";
            this.m_LabelOriginY.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // labelSpeed
            // 
            resources.ApplyResources(this.labelSpeed, "labelSpeed");
            this.labelSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // m_ComboBoxMediumSpeed
            // 
            this.m_ComboBoxMediumSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxMediumSpeed, "m_ComboBoxMediumSpeed");
            this.m_ComboBoxMediumSpeed.Name = "m_ComboBoxMediumSpeed";
            this.m_ComboBoxMediumSpeed.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxSpeed_SelectedIndexChanged);
            this.m_ComboBoxMediumSpeed.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.m_ComboBoxMediumSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxSpeed_KeyDown);
            // 
            // labelMediumSpeed
            // 
            resources.ApplyResources(this.labelMediumSpeed, "labelMediumSpeed");
            this.labelMediumSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelMediumSpeed.Name = "labelMediumSpeed";
            this.labelMediumSpeed.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // m_CheckBoxHDPrint
            // 
            resources.ApplyResources(this.m_CheckBoxHDPrint, "m_CheckBoxHDPrint");
            this.m_CheckBoxHDPrint.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxHDPrint.Name = "m_CheckBoxHDPrint";
            this.m_CheckBoxHDPrint.UseVisualStyleBackColor = false;
            this.m_CheckBoxHDPrint.CheckStateChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_CheckBoxHDPrint.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // checkBoxAlternatePrint
            // 
            resources.ApplyResources(this.checkBoxAlternatePrint, "checkBoxAlternatePrint");
            this.checkBoxAlternatePrint.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAlternatePrint.Name = "checkBoxAlternatePrint";
            this.checkBoxAlternatePrint.UseVisualStyleBackColor = false;
            this.checkBoxAlternatePrint.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.checkBoxAlternatePrint.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            // 
            // panelOriginX
            // 
            this.panelOriginX.BackColor = System.Drawing.Color.Transparent;
            this.panelOriginX.Controls.Add(this.m_NumericUpDownOrigin);
            this.panelOriginX.Controls.Add(this.m_LabelOrigin);
            resources.ApplyResources(this.panelOriginX, "panelOriginX");
            this.panelOriginX.Name = "panelOriginX";
            // 
            // panelOriginY
            // 
            this.panelOriginY.BackColor = System.Drawing.Color.Transparent;
            this.panelOriginY.Controls.Add(this.m_NumericUpDownOriginY);
            this.panelOriginY.Controls.Add(this.m_LabelOriginY);
            resources.ApplyResources(this.panelOriginY, "panelOriginY");
            this.panelOriginY.Name = "panelOriginY";
            // 
            // panelSteps
            // 
            this.panelSteps.BackColor = System.Drawing.Color.Transparent;
            this.panelSteps.Controls.Add(this.m_NumericUpDownStep);
            this.panelSteps.Controls.Add(this.m_LabelStep);
            resources.ApplyResources(this.panelSteps, "panelSteps");
            this.panelSteps.Name = "panelSteps";
            // 
            // panelPass
            // 
            this.panelPass.BackColor = System.Drawing.Color.Transparent;
            this.panelPass.Controls.Add(this.m_ComboBoxPass);
            this.panelPass.Controls.Add(this.label1);
            resources.ApplyResources(this.panelPass, "panelPass");
            this.panelPass.Name = "panelPass";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // panelSpeed
            // 
            this.panelSpeed.BackColor = System.Drawing.Color.Transparent;
            this.panelSpeed.Controls.Add(this.m_ComboBoxSpeed);
            this.panelSpeed.Controls.Add(this.labelSpeed);
            resources.ApplyResources(this.panelSpeed, "panelSpeed");
            this.panelSpeed.Name = "panelSpeed";
            // 
            // panelMediaSpeed
            // 
            this.panelMediaSpeed.BackColor = System.Drawing.Color.Transparent;
            this.panelMediaSpeed.Controls.Add(this.m_ComboBoxMediumSpeed);
            this.panelMediaSpeed.Controls.Add(this.labelMediumSpeed);
            resources.ApplyResources(this.panelMediaSpeed, "panelMediaSpeed");
            this.panelMediaSpeed.Name = "panelMediaSpeed";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panelMotorSetting);
            this.panel1.Controls.Add(this.panelOriginY2);
            this.panel1.Controls.Add(this.panelMediaModes);
            this.panel1.Controls.Add(this.panelTshirtPlat);
            this.panel1.Controls.Add(this.checkBoxYBackOrigin);
            this.panel1.Controls.Add(this.checkBoxPauseBetweenLayers);
            this.panel1.Controls.Add(this.checkBoxAlternatePrint);
            this.panel1.Controls.Add(this.m_CheckBoxHDPrint);
            this.panel1.Controls.Add(this.panelKeepwet);
            this.panel1.Controls.Add(this.panelLayoutSetting);
            this.panel1.Controls.Add(this.panelJobMode);
            this.panel1.Controls.Add(this.m_CheckBoxUsePrinterSetting);
            this.panel1.Controls.Add(this.m_CheckBoxBidirection);
            this.panel1.Controls.Add(this.panelYSpeed);
            this.panel1.Controls.Add(this.panelStartPrintDir);
            this.panel1.Controls.Add(this.panelMediaSpeed);
            this.panel1.Controls.Add(this.panelSpeed);
            this.panel1.Controls.Add(this.panelPass);
            this.panel1.Controls.Add(this.panelSteps);
            this.panel1.Controls.Add(this.panelOriginZ);
            this.panel1.Controls.Add(this.panelJobSpace);
            this.panel1.Controls.Add(this.panelOriginY);
            this.panel1.Controls.Add(this.panelOriginX);
            this.panel1.Controls.Add(this.checkBoxAutoCenterPrint);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panelOriginY2
            // 
            this.panelOriginY2.BackColor = System.Drawing.Color.Transparent;
            this.panelOriginY2.Controls.Add(this.numericOriginY2);
            this.panelOriginY2.Controls.Add(this.labelOriginY2);
            resources.ApplyResources(this.panelOriginY2, "panelOriginY2");
            this.panelOriginY2.Name = "panelOriginY2";
            // 
            // numericOriginY2
            // 
            this.numericOriginY2.InterceptArrowKeys = false;
            resources.ApplyResources(this.numericOriginY2, "numericOriginY2");
            this.numericOriginY2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericOriginY2.Name = "numericOriginY2";
            this.numericOriginY2.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.numericOriginY2.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.numericOriginY2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.numericOriginY2.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // labelOriginY2
            // 
            resources.ApplyResources(this.labelOriginY2, "labelOriginY2");
            this.labelOriginY2.BackColor = System.Drawing.Color.Transparent;
            this.labelOriginY2.Name = "labelOriginY2";
            // 
            // panelMediaModes
            // 
            this.panelMediaModes.Controls.Add(this.comboBoxMediaMode);
            this.panelMediaModes.Controls.Add(this.labelMediaMode);
            resources.ApplyResources(this.panelMediaModes, "panelMediaModes");
            this.panelMediaModes.Name = "panelMediaModes";
            // 
            // comboBoxMediaMode
            // 
            this.comboBoxMediaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMediaMode.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxMediaMode, "comboBoxMediaMode");
            this.comboBoxMediaMode.Name = "comboBoxMediaMode";
            // 
            // labelMediaMode
            // 
            resources.ApplyResources(this.labelMediaMode, "labelMediaMode");
            this.labelMediaMode.Name = "labelMediaMode";
            // 
            // panelTshirtPlat
            // 
            resources.ApplyResources(this.panelTshirtPlat, "panelTshirtPlat");
            this.panelTshirtPlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTshirtPlat.Controls.Add(this.radioButtonAuto);
            this.panelTshirtPlat.Controls.Add(this.radioButtonB);
            this.panelTshirtPlat.Controls.Add(this.radioButtonA);
            this.panelTshirtPlat.Controls.Add(this.labelplatform);
            this.panelTshirtPlat.Name = "panelTshirtPlat";
            // 
            // radioButtonAuto
            // 
            resources.ApplyResources(this.radioButtonAuto, "radioButtonAuto");
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.TabStop = true;
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            this.radioButtonAuto.CheckedChanged += new System.EventHandler(this.radioButtonA_CheckedChanged);
            // 
            // radioButtonB
            // 
            resources.ApplyResources(this.radioButtonB, "radioButtonB");
            this.radioButtonB.Name = "radioButtonB";
            this.radioButtonB.TabStop = true;
            this.radioButtonB.UseVisualStyleBackColor = true;
            this.radioButtonB.CheckedChanged += new System.EventHandler(this.radioButtonA_CheckedChanged);
            // 
            // radioButtonA
            // 
            resources.ApplyResources(this.radioButtonA, "radioButtonA");
            this.radioButtonA.Name = "radioButtonA";
            this.radioButtonA.TabStop = true;
            this.radioButtonA.UseVisualStyleBackColor = true;
            this.radioButtonA.CheckedChanged += new System.EventHandler(this.radioButtonA_CheckedChanged);
            // 
            // labelplatform
            // 
            this.labelplatform.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelplatform, "labelplatform");
            this.labelplatform.Name = "labelplatform";
            // 
            // checkBoxYBackOrigin
            // 
            resources.ApplyResources(this.checkBoxYBackOrigin, "checkBoxYBackOrigin");
            this.checkBoxYBackOrigin.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxYBackOrigin.Checked = true;
            this.checkBoxYBackOrigin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxYBackOrigin.Name = "checkBoxYBackOrigin";
            this.checkBoxYBackOrigin.UseVisualStyleBackColor = false;
            this.checkBoxYBackOrigin.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // checkBoxPauseBetweenLayers
            // 
            resources.ApplyResources(this.checkBoxPauseBetweenLayers, "checkBoxPauseBetweenLayers");
            this.checkBoxPauseBetweenLayers.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPauseBetweenLayers.Checked = true;
            this.checkBoxPauseBetweenLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPauseBetweenLayers.Name = "checkBoxPauseBetweenLayers";
            this.checkBoxPauseBetweenLayers.UseVisualStyleBackColor = false;
            this.checkBoxPauseBetweenLayers.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // panelKeepwet
            // 
            this.panelKeepwet.Controls.Add(this.btnMoisturize);
            resources.ApplyResources(this.panelKeepwet, "panelKeepwet");
            this.panelKeepwet.Name = "panelKeepwet";
            // 
            // btnMoisturize
            // 
            resources.ApplyResources(this.btnMoisturize, "btnMoisturize");
            this.btnMoisturize.Name = "btnMoisturize";
            this.btnMoisturize.UseVisualStyleBackColor = true;
            this.btnMoisturize.Click += new System.EventHandler(this.btnMoisturize_Click);
            // 
            // panelLayoutSetting
            // 
            resources.ApplyResources(this.panelLayoutSetting, "panelLayoutSetting");
            this.panelLayoutSetting.Controls.Add(this.comboBoxLayout);
            this.panelLayoutSetting.Controls.Add(this.labelLayout);
            this.panelLayoutSetting.Name = "panelLayoutSetting";
            // 
            // comboBoxLayout
            // 
            this.comboBoxLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayout.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLayout, "comboBoxLayout");
            this.comboBoxLayout.Name = "comboBoxLayout";
            this.comboBoxLayout.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayout_SelectedIndexChanged);
            // 
            // labelLayout
            // 
            resources.ApplyResources(this.labelLayout, "labelLayout");
            this.labelLayout.Name = "labelLayout";
            // 
            // panelJobMode
            // 
            resources.ApplyResources(this.panelJobMode, "panelJobMode");
            this.panelJobMode.Controls.Add(this.comboBoxPringConfigList);
            this.panelJobMode.Controls.Add(this.labelJobMode);
            this.panelJobMode.Name = "panelJobMode";
            // 
            // comboBoxPringConfigList
            // 
            this.comboBoxPringConfigList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPringConfigList.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxPringConfigList, "comboBoxPringConfigList");
            this.comboBoxPringConfigList.Name = "comboBoxPringConfigList";
            this.comboBoxPringConfigList.SelectedIndexChanged += new System.EventHandler(this.comboBoxJobMode_SelectedIndexChanged);
            // 
            // labelJobMode
            // 
            resources.ApplyResources(this.labelJobMode, "labelJobMode");
            this.labelJobMode.Name = "labelJobMode";
            // 
            // panelYSpeed
            // 
            this.panelYSpeed.Controls.Add(this.m_NumericUpYSpeed);
            this.panelYSpeed.Controls.Add(this.labelYSpeed);
            resources.ApplyResources(this.panelYSpeed, "panelYSpeed");
            this.panelYSpeed.Name = "panelYSpeed";
            // 
            // m_NumericUpYSpeed
            // 
            this.m_NumericUpYSpeed.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpYSpeed, "m_NumericUpYSpeed");
            this.m_NumericUpYSpeed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_NumericUpYSpeed.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.m_NumericUpYSpeed.Name = "m_NumericUpYSpeed";
            this.m_NumericUpYSpeed.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.m_NumericUpYSpeed.ValueChanged += new System.EventHandler(this.m_NumericUpYSpeed_ValueChanged);
            // 
            // labelYSpeed
            // 
            resources.ApplyResources(this.labelYSpeed, "labelYSpeed");
            this.labelYSpeed.Name = "labelYSpeed";
            // 
            // panelStartPrintDir
            // 
            this.panelStartPrintDir.Controls.Add(this.comboBoxStartPrintDir);
            this.panelStartPrintDir.Controls.Add(this.labelStartPrintDir);
            resources.ApplyResources(this.panelStartPrintDir, "panelStartPrintDir");
            this.panelStartPrintDir.Name = "panelStartPrintDir";
            // 
            // comboBoxStartPrintDir
            // 
            this.comboBoxStartPrintDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartPrintDir.FormattingEnabled = true;
            this.comboBoxStartPrintDir.Items.AddRange(new object[] {
            resources.GetString("comboBoxStartPrintDir.Items"),
            resources.GetString("comboBoxStartPrintDir.Items1")});
            resources.ApplyResources(this.comboBoxStartPrintDir, "comboBoxStartPrintDir");
            this.comboBoxStartPrintDir.Name = "comboBoxStartPrintDir";
            this.comboBoxStartPrintDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxStartPrintDir_SelectedIndexChanged);
            this.comboBoxStartPrintDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxSpeed_KeyDown);
            // 
            // labelStartPrintDir
            // 
            resources.ApplyResources(this.labelStartPrintDir, "labelStartPrintDir");
            this.labelStartPrintDir.Name = "labelStartPrintDir";
            // 
            // panelOriginZ
            // 
            this.panelOriginZ.BackColor = System.Drawing.Color.Transparent;
            this.panelOriginZ.Controls.Add(this.numZWorkPos);
            this.panelOriginZ.Controls.Add(this.label2);
            resources.ApplyResources(this.panelOriginZ, "panelOriginZ");
            this.panelOriginZ.Name = "panelOriginZ";
            // 
            // numZWorkPos
            // 
            this.numZWorkPos.InterceptArrowKeys = false;
            resources.ApplyResources(this.numZWorkPos, "numZWorkPos");
            this.numZWorkPos.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numZWorkPos.Name = "numZWorkPos";
            this.numZWorkPos.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.numZWorkPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.numZWorkPos.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // panelJobSpace
            // 
            this.panelJobSpace.BackColor = System.Drawing.Color.Transparent;
            this.panelJobSpace.Controls.Add(this.checkBoxFlatMode);
            this.panelJobSpace.Controls.Add(this.numJobSpace);
            this.panelJobSpace.Controls.Add(this.labelJobSpace);
            resources.ApplyResources(this.panelJobSpace, "panelJobSpace");
            this.panelJobSpace.Name = "panelJobSpace";
            // 
            // checkBoxFlatMode
            // 
            resources.ApplyResources(this.checkBoxFlatMode, "checkBoxFlatMode");
            this.checkBoxFlatMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFlatMode.Checked = true;
            this.checkBoxFlatMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFlatMode.Name = "checkBoxFlatMode";
            this.checkBoxFlatMode.UseVisualStyleBackColor = false;
            this.checkBoxFlatMode.CheckedChanged += new System.EventHandler(this.checkBoxFlatMode_CheckedChanged);
            // 
            // numJobSpace
            // 
            this.numJobSpace.InterceptArrowKeys = false;
            resources.ApplyResources(this.numJobSpace, "numJobSpace");
            this.numJobSpace.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numJobSpace.Name = "numJobSpace";
            this.numJobSpace.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.numJobSpace.VisibleChanged += new System.EventHandler(this.ToolbarSetting_VisibleChanged);
            this.numJobSpace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            this.numJobSpace.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // labelJobSpace
            // 
            resources.ApplyResources(this.labelJobSpace, "labelJobSpace");
            this.labelJobSpace.BackColor = System.Drawing.Color.Transparent;
            this.labelJobSpace.Name = "labelJobSpace";
            // 
            // checkBoxAutoCenterPrint
            // 
            resources.ApplyResources(this.checkBoxAutoCenterPrint, "checkBoxAutoCenterPrint");
            this.checkBoxAutoCenterPrint.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoCenterPrint.Checked = true;
            this.checkBoxAutoCenterPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoCenterPrint.Name = "checkBoxAutoCenterPrint";
            this.checkBoxAutoCenterPrint.UseVisualStyleBackColor = false;
            this.checkBoxAutoCenterPrint.CheckedChanged += new System.EventHandler(this.checkBoxAutoCenterPrint_CheckedChanged);
            // 
            // panelMotorSetting
            // 
            this.panelMotorSetting.Controls.Add(this.comboBoxMotorControl);
            this.panelMotorSetting.Controls.Add(this.labelMotorControl);
            resources.ApplyResources(this.panelMotorSetting, "panelMotorSetting");
            this.panelMotorSetting.Name = "panelMotorSetting";
            // 
            // comboBoxMotorControl
            // 
            this.comboBoxMotorControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMotorControl.FormattingEnabled = true;
            this.comboBoxMotorControl.Items.AddRange(new object[] {
            resources.GetString("comboBoxMotorControl.Items"),
            resources.GetString("comboBoxMotorControl.Items1")});
            resources.ApplyResources(this.comboBoxMotorControl, "comboBoxMotorControl");
            this.comboBoxMotorControl.Name = "comboBoxMotorControl";
            this.comboBoxMotorControl.SelectedIndexChanged += new System.EventHandler(this.comboBoxMotorControl_SelectedIndexChanged);
            // 
            // labelMotorControl
            // 
            resources.ApplyResources(this.labelMotorControl, "labelMotorControl");
            this.labelMotorControl.Name = "labelMotorControl";
            // 
            // ToolbarSetting
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.DividerSide = System.Windows.Forms.Border3DSide.Top;
            resources.ApplyResources(this, "$this");
            this.Name = "ToolbarSetting";
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOriginY)).EndInit();
            this.panelOriginX.ResumeLayout(false);
            this.panelOriginX.PerformLayout();
            this.panelOriginY.ResumeLayout(false);
            this.panelOriginY.PerformLayout();
            this.panelSteps.ResumeLayout(false);
            this.panelSteps.PerformLayout();
            this.panelPass.ResumeLayout(false);
            this.panelPass.PerformLayout();
            this.panelSpeed.ResumeLayout(false);
            this.panelSpeed.PerformLayout();
            this.panelMediaSpeed.ResumeLayout(false);
            this.panelMediaSpeed.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelOriginY2.ResumeLayout(false);
            this.panelOriginY2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOriginY2)).EndInit();
            this.panelMediaModes.ResumeLayout(false);
            this.panelMediaModes.PerformLayout();
            this.panelTshirtPlat.ResumeLayout(false);
            this.panelTshirtPlat.PerformLayout();
            this.panelKeepwet.ResumeLayout(false);
            this.panelLayoutSetting.ResumeLayout(false);
            this.panelLayoutSetting.PerformLayout();
            this.panelJobMode.ResumeLayout(false);
            this.panelJobMode.PerformLayout();
            this.panelYSpeed.ResumeLayout(false);
            this.panelYSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpYSpeed)).EndInit();
            this.panelStartPrintDir.ResumeLayout(false);
            this.panelStartPrintDir.PerformLayout();
            this.panelOriginZ.ResumeLayout(false);
            this.panelOriginZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos)).EndInit();
            this.panelJobSpace.ResumeLayout(false);
            this.panelJobSpace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numJobSpace)).EndInit();
            this.panelMotorSetting.ResumeLayout(false);
            this.panelMotorSetting.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
        public void LayoutSetting_Bind()
        {
            try
            {
                LayoutSettingClassList m_LayoutSettingList = new LayoutSettingClassList();

                if (File.Exists(CoreConst.LayoutFileName))
                {
                    var doc = new XmlDocument();
                    doc.Load(CoreConst.LayoutFileName);
                    m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));
                }

                comboBoxLayout.Items.Clear();

                for (int i = 0; i < m_LayoutSettingList.Items.Count; i++)
                {
                    comboBoxLayout.Items.Add(m_LayoutSettingList.Items[i].Name.Trim());
                }

                if (CoreInterface.LayoutIndex >= 0 && CoreInterface.LayoutIndex < comboBoxLayout.Items.Count)
                {
                    comboBoxLayout.SelectedIndex = CoreInterface.LayoutIndex;
                }
                else
                {
                    if (comboBoxLayout.Items.Count > 0)
                    {
                        comboBoxLayout.SelectedIndex = 0;
                        CoreInterface.LayoutIndex = 0;
                    }
                }
            }
            catch { }
        }

        public void JobConfigList_Bind(bool IsSelect)
        {
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (comboBoxPringConfigList.Text == "" || comboBoxPringConfigList.Text == "Default")
            {
                oldSelectConfigName = "Default";
            }
            else
            {
                oldSelectConfigName = comboBoxPringConfigList.Text;
            }

            try
            {
                m_jobConfigList = PubFunc.LoadJobModesFromFile();

                //comboBoxPringConfigList.ItemsSource = m_jobConfigList.Items;
                //comboBoxPringConfigList.DisplayMemberPath = "Name";

                comboBoxPringConfigList.Items.Clear();
                //if (!Misc.IsGongZeng)
                //{
                //comboBoxPringConfigList.Items.Add("Default");
                //}
                foreach (JobMode item in m_jobConfigList.Items)
                {
                    comboBoxPringConfigList.Items.Add(item.Name);
                    if (oldSelectConfigName == item.Name)
                    {
                        bExistConfigName = true;
                    }
                }

                if (IsSelect)
                {

                    if (bExistConfigName)
                    {
                        comboBoxPringConfigList.Text = oldSelectConfigName;
                    }
                    else
                    {
                        if (comboBoxPringConfigList.Items.Count > 0)
                            comboBoxPringConfigList.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Load JobConfig Error:" + ex.Message);
            }
        }

        public JobMode GetJobCofigByName(string name = "")
        {
            JobMode jobConfig = new JobMode();

            if (name == "")
            {
                name = comboBoxPringConfigList.Text.Trim();
            }

            if (name.ToLower() != "default" && m_jobConfigList != null && m_jobConfigList.Items.Count > 0)
            {
                foreach (JobMode item in m_jobConfigList.Items)
                {
                    if (item.Name == name)
                    {
                        jobConfig = item;
                        break;
                    }
                }
            }

            return jobConfig;
        }

        public void JobMediaConfigList_Bind(bool IsSelect)
        {
            string oldSelectConfigName = "";
            bool bExistConfigName = false;

            if (comboBoxMediaMode.Text == "" || comboBoxMediaMode.Text == "Default")
            {
                oldSelectConfigName = "Default";
            }
            else
            {
                oldSelectConfigName = comboBoxMediaMode.Text;
            }

            try
            {
                m_mediaConfigList = PubFunc.LoadMediaModesFromFile();

                //comboBoxPringConfigList.ItemsSource = m_jobConfigList.Items;
                //comboBoxPringConfigList.DisplayMemberPath = "Name";

                comboBoxMediaMode.Items.Clear();
                //if (!Misc.IsGongZeng)
                //{
                comboBoxMediaMode.Items.Add("Default");
                //}
                foreach (JobMediaMode item in m_mediaConfigList.Items)
                {
                    comboBoxMediaMode.Items.Add(item.Name);
                    if (oldSelectConfigName == item.Name)
                    {
                        bExistConfigName = true;
                    }
                }

                if (IsSelect)
                {

                    if (bExistConfigName)
                    {
                        comboBoxMediaMode.Text = oldSelectConfigName;
                    }
                    else
                    {
                        comboBoxMediaMode.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Load JobConfig Error:" + ex.Message);
            }
        }

        public JobMediaMode GetJobMediaCofigByName(string name = "")
        {
            JobMediaMode jobConfig = new JobMediaMode();

            if (name == "")
            {
                name = comboBoxMediaMode.Text.Trim();
            }

            if (name.ToLower() != "default" && m_mediaConfigList != null && m_mediaConfigList.Items.Count > 0)
            {
                foreach (JobMediaMode item in m_mediaConfigList.Items)
                {
                    if (item.Name == name)
                    {
                        jobConfig = item;
                        break;
                    }
                }
            }

            return jobConfig;
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
			bool bEnabled = (m_curStatus == JetStatusEnum.Ready);

			m_CheckBoxUsePrinterSetting.Enabled = bEnabled;
			m_NumericUpDownOrigin.Enabled = bEnabled && !checkBoxAutoCenterPrint.Checked;
			m_NumericUpDownOriginY.Enabled = bEnabled;
			m_NumericUpDownStep.Enabled = true;
			m_CheckBoxHDPrint.Enabled = bEnabled;
            m_ComboBoxMediumSpeed.Enabled = bEnabled;

			bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
            panelStartPrintDir.Enabled=m_CheckBoxBidirection.Enabled = bEnabled;
            comboBoxMediaMode.Enabled =
            comboBoxPringConfigList.Enabled = bEnabled;
            checkBoxPauseBetweenLayers.Enabled = true;
            numericOriginY2.Enabled = bEnabled;
		    numJobSpace.Enabled = checkBoxFlatMode.Checked;
            m_NumericUpYSpeed.Enabled = bEnabled;
            comboBoxLayout.Enabled = bEnabled;
            comboBoxMotorControl.Enabled = bEnabled;
		}
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
            m_sPrinterProperty = sp;
            m_bInitControl = true;
			m_NumericUpDownOrigin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
			m_NumericUpDownOrigin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,Math.Max(0,sp.fMaxPaperWidth-1)));
			m_NumericUpDownOriginY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
            m_NumericUpDownOriginY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,Math.Max(0, sp.fMaxPaperHeight-1)));
            numericOriginY2.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numericOriginY2.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));
            numZWorkPos.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));

            //m_ComboBoxUnit
			m_ComboBoxSpeed.Items.Clear();
			foreach(SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
			{
				if(mode == SpeedEnum.CustomSpeed) 
				{
					if(!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
						continue;
				}
				string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum),mode);
				if(SPrinterProperty.IsY1Epson(sp.ePrinterHead))
                    {
                        if (SPrinterProperty.IsXAAR1201(sp.ePrinterHead) && (int)mode > 2)
                        {
                            continue;
                        }
					cmode = "VSD_"+ ((int)mode + 1).ToString();
                    }
				m_ComboBoxSpeed.Items.Add(cmode);
			}

			m_ComboBoxMediumSpeed.Items.Clear();
			foreach(SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
			{
				if(mode == SpeedEnum.CustomSpeed) 
				{
//					if(!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
						continue;
				}
#if !ThreeSpeed
				if(mode == SpeedEnum.HighSpeed) 
					continue;
#endif
				string cmode =string.Empty;
				if(mode == SpeedEnum.MiddleSpeed)
					cmode = ResString.GetResString("SpeedEnum_MiddleSpeed_1");
				else
					cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum),mode);
				m_ComboBoxMediumSpeed.Items.Add(cmode);
			}

            panelOriginY.Visible = sp.nMediaType != 0;
			panelMediaSpeed.Visible = (sp.nDspInfo& 0x01) != 0;
		    SwitchToAdvancedMode(PubFunc.IsKingColorAdvancedMode);
            checkBoxPauseBetweenLayers.Visible = PubFunc.Is3DPrintMachine();// && PubFunc.IsWitColor_Flat_UV();
		    panelOriginZ.Visible = sp.bSupportZMotion && m_sPrinterProperty.IsTLHG();
            checkBoxYBackOrigin.Visible = sp.Y_BackToOrgControlBySw();
            panelTshirtPlat.Visible = SPrinterProperty.SurpportDoublePlatform();

            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                GlobalSetting.Instance.Init(sBoardInfo.m_nBoardManufatureID.ToString("X4") +
                                            sBoardInfo.m_nBoardProductID.ToString("X4"), m_sPrinterProperty.bSupportUV, m_CurrentUnit);
            }
            bool isJobConfigModesXml = PubFunc.IsJobConfigModesXml() && (UIFunctionOnOff.SupportPrintMode || SPrinterProperty.IsDocanPrintMode() || PubFunc.IsPuQi());
            panelJobMode.Visible = isJobConfigModesXml;
            panelPass.Visible = !isJobConfigModesXml;
            panelSpeed.Visible = !isJobConfigModesXml && (sp.ePrinterHead != PrinterHeadEnum.Epson_S2840 && sp.ePrinterHead != PrinterHeadEnum.Epson_S2840_WaterInk && sp.ePrinterHead != PrinterHeadEnum.EPSON_S1600_RC_UV);
            if (!sp.IsDocan())
            {
                m_CheckBoxBidirection.Visible = !isJobConfigModesXml;
            }
            panelMediaModes.Visible = PubFunc.IsMediaConfigModesXml() && UIFunctionOnOff.SupportMediaMode;

            panelOriginY2.Visible = PubFunc.IsZhuoZhan();
            panelJobSpace.Visible = sp.SurpportJobSpaceAsOriginY();// 只有越达显示jobspace,且界面标签显示为y原点
		    if (UIFunctionOnOff.SupportGlogalAlternatingPrint)
		    {
		        if (SPrinterProperty.IsAllPrint())
		        {
		            this.checkBoxAlternatePrint.Visible = sp.nMediaType != 0;
		        }
		        else
		        {
		            this.checkBoxAlternatePrint.Visible = true;
		        }
		    }

            panelMotorSetting.Visible = (PubFunc.IsDoubleYAxis() && SPrinterProperty.IsGongZeng() && sp.nMediaType == 0);

		    m_bInitControl = false;
            panelSteps.Visible = !SPrinterProperty.IsFlora();
            if (sp.IsDocan())
            {
                panelStartPrintDir.Visible = UIFunctionOnOff.Support4PrintDir;
            }
            else
            {
                panelStartPrintDir.Visible = UIFunctionOnOff.Support4PrintDir && !isJobConfigModesXml;
            }

            if (UIFunctionOnOff.CanSetYSpeed)
            {
                int ySpeed = 0;
                if (EpsonLCD.GetYStepSpeed(ref ySpeed) && ySpeed <= m_NumericUpYSpeed.Maximum && ySpeed >= m_NumericUpYSpeed.Minimum)
                {
                    m_NumericUpYSpeed.Value = ySpeed;
                }
                else if (m_iPrinterChange.GetAllParam().ExtendedSettings.YSpeed <= m_NumericUpYSpeed.Maximum && m_iPrinterChange.GetAllParam().ExtendedSettings.YSpeed >= m_NumericUpYSpeed.Minimum)
                {
                    EpsonLCD.SetYStepSpeed(m_iPrinterChange.GetAllParam().ExtendedSettings.YSpeed);
                    m_NumericUpYSpeed.Value = m_iPrinterChange.GetAllParam().ExtendedSettings.YSpeed;
                }
                else
                {
                    EpsonLCD.SetYStepSpeed(5000);
                    m_NumericUpYSpeed.Value = 5000;
                }
                panelYSpeed.Visible = true;
            }
            else 
            {
                panelYSpeed.Visible = false;
            }

            if (PubFunc.IsSupportKeepwet())
            {
                panelKeepwet.Visible = true;
            }
            else
            {
                panelKeepwet.Visible = false;
            }

		}
	    private void SwitchToAdvancedMode(bool bAdvancedMode)
        {
           panelSteps.Visible = bAdvancedMode;
            m_CheckBoxUsePrinterSetting.Visible = bAdvancedMode;
            //panelPass.Visible = PubFunc.SupportKingColorSimpleMode? false:true;
        }

		private void InitComboBoxPass()
		{
			string passStr = (string)m_ComboBoxPass.SelectedItem;
			m_ComboBoxPass.Items.Clear();
#if false
			int PassListNum;
			int [] PassList;
			sp.GetPassListNumber(out PassListNum,out PassList);
			string spass = ResString.GetDisplayPass();
			for(int i = 0;i <PassListNum; i++)
			{
				int passNum = PassList[i];
				string dispPass = PassList[i].ToString() + " " + spass;
				m_ComboBoxPass.Items.Add(dispPass);
			}
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPass,FoundMatchPass(passStr));
#else
			string sPass = ResString.GetDisplayPass();
			for (int i=0; i< CoreConst.MAX_PASS_NUM;i++)
			{
				//int passNum = PassList[i];
				string dispPass = (i+1).ToString() + " " + sPass;
				m_ComboBoxPass.Items.Add(dispPass);
			}
#endif
		}
		private int FoundMatchPass(string dispPass)
		{
			for (int i = 0; i< m_ComboBoxPass.Items.Count;i++)
			{
				if(string.Compare((string)m_ComboBoxPass.Items[i] , dispPass)==0)
					return i;
			}
			return -1;
		}

        public void OnPrinterSettingChange(AllParam mAllParam, EpsonExAllParam epsonAllparam)
        {
            SPrinterSetting ss = mAllParam.PrinterSetting;
			m_bInitControl = true;
            if (m_sPrinterProperty.EPSONLCD_DEFINED)
			{
				m_NumericUpDownOrigin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				m_NumericUpDownOrigin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,(epsonAllparam.sUSB_RPT_Media_Info.MediaWidth + epsonAllparam.sUSB_RPT_MainUI_Param.PrintOrigin)));
			}
			else
			{
				if(ss.sBaseSetting.fLeftMargin > ss.sFrequencySetting.fXOrigin)
					ss.sFrequencySetting.fXOrigin = ss.sBaseSetting.fLeftMargin; //打印原点最小值为纸左边界
				m_NumericUpDownOrigin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fLeftMargin));
				m_NumericUpDownOrigin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,(ss.sBaseSetting.fLeftMargin + ss.sBaseSetting.fPaperWidth)));
			}
			m_CheckBoxUsePrinterSetting.Checked		=	ss.sFrequencySetting.bUsePrinterSetting == 0;
            m_CheckBoxBidirection.Checked = ss.sFrequencySetting.nBidirection == (int)PrintDirection.Bidirection 
                || ss.sFrequencySetting.nBidirection == (int)PrintDirection.BidirectionBackWard;
            comboBoxStartPrintDir.SelectedIndex = (ss.sFrequencySetting.nBidirection == (int)PrintDirection.Unidirection
                || ss.sFrequencySetting.nBidirection == (int)PrintDirection.Bidirection)?0:1;
			if(m_ComboBoxSpeed.Items.Count <= (int)ss.sFrequencySetting.nSpeed)
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxSpeed,m_ComboBoxSpeed.Items.Count-1);
			else
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxSpeed,(int)ss.sFrequencySetting.nSpeed);

			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPass,(int)FoundMatchPass(ss.sFrequencySetting.nPass.ToString()+" "+ResString.GetDisplayPass()));

			float curvalue = ss.sFrequencySetting.fXOrigin;
            if (m_sPrinterProperty.EPSONLCD_DEFINED)
				curvalue = epsonAllparam.sUSB_RPT_MainUI_Param.PrintOrigin;
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownOrigin,m_CurrentUnit,curvalue);
            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownOriginY, m_CurrentUnit, ss.sBaseSetting.fYOrigin);
            UIPreference.SetValueAndClampWithMinMax(numJobSpace, m_CurrentUnit, mAllParam.ExtendedSettings.fRoll2FlatJobSpace);
            UIPreference.SetValueAndClampWithMinMax(numericOriginY2, m_CurrentUnit, mAllParam.ExtendedSettings.fYOrigin2);
			if(this.m_bDuringPrinting)
			{
				SPrtFileInfo jobInfo = new SPrtFileInfo();
				if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
				{
					int realpass = jobInfo.sFreSetting.nPass;
					if(ss.nKillBiDirBanding != 0)
						UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[(realpass+1)/2 -1]);
					else
						UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[realpass -1]);
				}
			}
			else
			{
				if(ss.nKillBiDirBanding != 0)
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[(ss.sFrequencySetting.nPass+1)/2 -1]);
				else
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1]);
			}
			//m_NumericUpDownStep.Value		=	ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1];

			bool bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
			m_CheckBoxBidirection.Enabled = bEnabled;
            comboBoxMediaMode.Enabled =
comboBoxPringConfigList.Enabled = bEnabled;
#if ThreeSpeed
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxMediumSpeed,(int)ss.sBaseSetting.nYPrintSpeed);
#else
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxMediumSpeed,(int)(ss.sBaseSetting.nYPrintSpeed==0?ss.sBaseSetting.nYPrintSpeed:ss.sBaseSetting.nYPrintSpeed-1));
#endif
//			this.m_CheckBoxHDPrint.Checked = ss.sBaseSetting.nFeatherPercent==100;
		    this.checkBoxAutoCenterPrint.Checked = ss.sBaseSetting.bAutoCenterPrint;
		    checkBoxPauseBetweenLayers.Checked = ss.sExtensionSetting.bAutoPausePerPage;
            if (m_sPrinterProperty.IsTLHG())
            {
                TlhgParam param = new TlhgParam();
                if (EpsonLCD.GetTlhgParam(ref param))
                {
                    if(m_sPrinterProperty.fPulsePerInchZ > 0)
                    UIPreference.SetValueAndClampWithMinMax(numZWorkPos, m_CurrentUnit,
            param.zWorkPos / m_sPrinterProperty.fPulsePerInchZ);
                }
            }
            checkBoxYBackOrigin.Checked = ss.sExtensionSetting.bYBackOrigin;


            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                GlobalSetting.Instance.Init(sBoardInfo.m_nBoardManufatureID.ToString("X4") +
                                            sBoardInfo.m_nBoardProductID.ToString("X4"), m_sPrinterProperty.bSupportUV, m_CurrentUnit);
            }

            LayoutSetting_Bind();

            JobConfigList_Bind(false);
            if (comboBoxPringConfigList.Items.Count > (int)mAllParam.Preference.JobModeIndex)
                comboBoxPringConfigList.SelectedIndex = (int)mAllParam.Preference.JobModeIndex;
            JobMediaConfigList_Bind(false);
            if (comboBoxMediaMode.Items.Count > (int)mAllParam.Preference.MediaModeIndex)
                comboBoxMediaMode.SelectedIndex = (int)mAllParam.Preference.MediaModeIndex;

            checkBoxFlatMode.Checked = mAllParam.ExtendedSettings.IsFlatMode;

            if ((PubFunc.IsDoubleYAxis() && SPrinterProperty.IsGongZeng()))
            {
                try
                {
                    HapondMotorParam_Auto data = new HapondMotorParam_Auto();
                    if (EpsonLCD.GetMotionSetting(ref data))
                    {
                        if (data.sysMode < comboBoxMotorControl.Items.Count)
                        {
                            comboBoxMotorControl.SelectedIndex = data.sysMode;
                        }
                        else
                        {
                            comboBoxMotorControl.SelectedIndex = 0;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            m_bInitControl = false;
		}
        public void OnGetPrinterSetting(ref AllParam mAllParam, ref EpsonExAllParam epsonAllparam)
        {
            mAllParam.PrinterSetting.sBaseSetting.bAutoCenterPrint = this.checkBoxAutoCenterPrint.Checked;
            mAllParam.PrinterSetting.sFrequencySetting.bUsePrinterSetting = m_CheckBoxUsePrinterSetting.Checked ? 0 : 1;
            if (comboBoxStartPrintDir.SelectedIndex == 0)
            {
                mAllParam.PrinterSetting.sFrequencySetting.nBidirection = (byte)(m_CheckBoxBidirection.Checked ? PrintDirection.Bidirection : PrintDirection.Unidirection);
            }
            else
            {
                mAllParam.PrinterSetting.sFrequencySetting.nBidirection = (byte)(m_CheckBoxBidirection.Checked ? PrintDirection.BidirectionBackWard : PrintDirection.UnidirectionBackWard);                
            }
            mAllParam.PrinterSetting.sFrequencySetting.nSpeed = (SpeedEnum)m_ComboBoxSpeed.SelectedIndex;		

			string PassString = m_ComboBoxPass.Text;
			string[] split = PassString.Split(new char[] {' '});
			Debug.Assert(split != null && split.Length == 2);
            mAllParam.PrinterSetting.sFrequencySetting.nPass = Convert.ToByte(split[0]);

            mAllParam.PrinterSetting.sFrequencySetting.fXOrigin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownOrigin.Value));
            if (m_sPrinterProperty.EPSONLCD_DEFINED)
			{
				float m_RealMediaWidth = epsonAllparam.sUSB_RPT_Media_Info.MediaOrigin + epsonAllparam.sUSB_RPT_Media_Info.MediaWidth;
                epsonAllparam.sUSB_RPT_Media_Info.MediaOrigin = epsonAllparam.sUSB_RPT_MainUI_Param.PrintOrigin = mAllParam.PrinterSetting.sFrequencySetting.fXOrigin;
                mAllParam.PrinterSetting.sBaseSetting.fLeftMargin = mAllParam.PrinterSetting.sFrequencySetting.fXOrigin;
                epsonAllparam.sUSB_RPT_Media_Info.MediaWidth = mAllParam.PrinterSetting.sBaseSetting.fPaperWidth = m_RealMediaWidth - mAllParam.PrinterSetting.sBaseSetting.fLeftMargin;
			}
            mAllParam.PrinterSetting.sBaseSetting.fYOrigin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownOriginY.Value));
            mAllParam.ExtendedSettings.fRoll2FlatJobSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numJobSpace.Value));
            mAllParam.ExtendedSettings.fYOrigin1 = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownOriginY.Value));
            mAllParam.ExtendedSettings.fYOrigin2 = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numericOriginY2.Value));
            mAllParam.ExtendedSettings.IsFlatMode = checkBoxFlatMode.Checked;
#if ThreeSpeed
            mAllParam.PrinterSetting.sBaseSetting.nYPrintSpeed = (byte)m_ComboBoxMediumSpeed.SelectedIndex;
#else
			ss.sBaseSetting.nYPrintSpeed = (byte)(m_ComboBoxMediumSpeed.SelectedIndex+1);
#endif
//			ss.sBaseSetting.nFeatherPercent = this.m_CheckBoxHDPrint.Checked?100:10;
			//ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1]				=	Decimal.ToInt32(m_NumericUpDownStep.Value);	
			if(this.m_bDuringPrinting)
			{
				SPrtFileInfo jobInfo = new SPrtFileInfo();
				if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
				{
					int realpass = jobInfo.sFreSetting.nPass;
					epsonAllparam.sUSB_RPT_MainUI_Param.PassNum = realpass;
					epsonAllparam.sUSB_RPT_MainUI_Param.StepModify = Decimal.ToInt32(m_NumericUpDownStep.Value);

                    if (mAllParam.PrinterSetting.nKillBiDirBanding != 0)
                        mAllParam.PrinterSetting.sCalibrationSetting.nPassStepArray[(realpass + 1) / 2 - 1] = Decimal.ToInt32(m_NumericUpDownStep.Value);	
					else
                        mAllParam.PrinterSetting.sCalibrationSetting.nPassStepArray[realpass - 1] = Decimal.ToInt32(m_NumericUpDownStep.Value);	
				}
			}
			else
			{
                epsonAllparam.sUSB_RPT_MainUI_Param.PassNum = mAllParam.PrinterSetting.sFrequencySetting.nPass;
				epsonAllparam.sUSB_RPT_MainUI_Param.StepModify = Decimal.ToInt32(m_NumericUpDownStep.Value);

                if (mAllParam.PrinterSetting.nKillBiDirBanding != 0)
                    mAllParam.PrinterSetting.sCalibrationSetting.nPassStepArray[(mAllParam.PrinterSetting.sFrequencySetting.nPass + 1) / 2 - 1] = Decimal.ToInt32(m_NumericUpDownStep.Value);	
				else
                    mAllParam.PrinterSetting.sCalibrationSetting.nPassStepArray[mAllParam.PrinterSetting.sFrequencySetting.nPass - 1] = Decimal.ToInt32(m_NumericUpDownStep.Value);	
			}
            mAllParam.PrinterSetting.sExtensionSetting.bAutoPausePerPage = checkBoxPauseBetweenLayers.Checked;
            mAllParam.PrinterSetting.sExtensionSetting.bYBackOrigin = checkBoxYBackOrigin.Checked;
            if (m_sPrinterProperty.IsTLHG())
		    {
		        TlhgParam param = new TlhgParam();
		        if (EpsonLCD.GetTlhgParam(ref param))
		        {
		            param.Flag = new char[] {'T', 'L', 'H', 'G'};
		            param.zWorkPos =
		                (uint)
		                    ((UIPreference.ToInchLength(m_CurrentUnit, (float) numZWorkPos.Value))*
		                     m_sPrinterProperty.fPulsePerInchZ);
		            EpsonLCD.SetTlhgParam(param);
		        }
		    }
            mAllParam.Preference.JobModeIndex = (byte)comboBoxPringConfigList.SelectedIndex;
            mAllParam.Preference.MediaModeIndex = (byte)comboBoxMediaMode.SelectedIndex;
            // todo:此段逻辑应始终最后执行; 因有调用OnPrinterSettingChange,会导致其他参数无法应用或修改问题
            {
                JobMode curJobMode = GetJobCofigByName();
                if (curJobMode != null)
                {
                    ModeConfig layer = curJobMode.Item;

                    byte GlobalPass = mAllParam.PrinterSetting.sFrequencySetting.nPass;
                    byte GlobalPrintMode = mAllParam.PrinterSetting.sBaseSetting.nXResutionDiv;
                    SpeedEnum GlobalSpeed = mAllParam.PrinterSetting.sFrequencySetting.nSpeed;

                    //Pass
                    if (layer.Pass != "Global")
                    {
                        //TODO 照搬ToolBarSetting::OnGetPrinterSetting
                        PassString = layer.Pass;
                        split = PassString.Split(new char[] {' '});
                        Debug.Assert(split != null && split.Length == 2);
                        mAllParam.PrinterSetting.sFrequencySetting.nPass = Convert.ToByte(split[0]);
                    }
                    else
                    {
                        mAllParam.PrinterSetting.sFrequencySetting.nPass = GlobalPass;
                    }

                    //PrintMode
                    if (layer.PrintMode != "Global")
                    {
                        foreach (XResDivMode place in Enum.GetValues(typeof (XResDivMode)))
                        {
                            string cmode = ResString.GetEnumDisplayName(typeof (XResDivMode), place);
                            if (cmode == layer.PrintMode)
                            {
                                mAllParam.PrinterSetting.sBaseSetting.nXResutionDiv = (byte) place;
                                break;
                            }
                        }
                    }
                    else
                    {
                        mAllParam.PrinterSetting.sBaseSetting.nXResutionDiv = GlobalPrintMode;
                    }

                    //速度
                    if (layer.Speed != "Global")
                    {
                        mAllParam.PrinterSetting.sFrequencySetting.nSpeed = GetSpeedEnum(layer.Speed);
                    }
                    else
                    {
                        mAllParam.PrinterSetting.sFrequencySetting.nSpeed = GlobalSpeed;
                    }

                    OnPrinterSettingChange(mAllParam, epsonAllparam);
                }
            }
        }
        private SpeedEnum GetSpeedEnum(string SpeedDescription)
        {
            SpeedEnum Ret = (SpeedEnum)0;
            foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
            {
                string cmode = mode.ToString();
                if (cmode == SpeedDescription)
                {
                    Ret = mode;
                    break;
                }
            }

            //if ((int)Ret >= 10) Ret = (SpeedEnum)((int)Ret - 10);

            return Ret;

        }

		public void OnGetPreference(ref UIPreference up)
		{
			up.bAlternatingPrint = checkBoxAlternatePrint.Checked;
		    if (radioButtonA.Checked)
                up.ScanningAxis = CoreConst.AXIS_X;
            if (radioButtonB.Checked)
                up.ScanningAxis = CoreConst.AXIS_4;
            if (radioButtonAuto.Checked)
                up.ScanningAxis = 0xff;
		}
		public void OnPreferenceChange( UIPreference up)
		{
			m_bInitControl = true;
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
			    GlobalSetting.Instance.Unit = m_CurrentUnit;
			}

		    if (UIFunctionOnOff.SupportGlogalAlternatingPrint)
		    {
		        this.checkBoxAlternatePrint.Checked = up.bAlternatingPrint;
		    }

		    switch (up.ScanningAxis)
		    {
                case CoreConst.AXIS_X:
		            radioButtonA.Checked = true;
		            break;
                case CoreConst.AXIS_4:
                    radioButtonB.Checked = true;
                    break;
                default:
                    radioButtonAuto.Checked = true;
                    break;
            }
			m_bInitControl = false;
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownOrigin);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownOrigin,this.m_ToolTip);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numZWorkPos);

            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numJobSpace);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numJobSpace, this.m_ToolTip);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownOriginY);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.m_NumericUpDownOriginY, this.m_ToolTip);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericOriginY2);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numericOriginY2, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnit.ToString(), this.numZWorkPos, this.m_ToolTip);

		}
		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
		}
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}

	    private bool bSetting = false;
		private void m_AnyControl_ValueChanged(object sender, System.EventArgs e)
		{
		    if (bSetting) return;
		    bSetting = true;
            if (sender == checkBoxAlternatePrint && checkBoxAlternatePrint.Checked)
            {
                checkBoxYBackOrigin.Checked = false;
            }
            if (sender == checkBoxYBackOrigin && checkBoxYBackOrigin.Checked)
            {
                checkBoxAlternatePrint.Checked = false;
            }
            bSetting = false;
            if (m_bInitControl == false)
				m_iPrinterChange.NotifyUIParamChanged();
		}

		private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_ComboBoxPass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            //int PassListNum;
            //int [] PassList;
            //m_iPrinterChange.GetAllParam().PrinterProperty.GetPassListNumber(out PassListNum,out PassList);
			int [] PassStepArray = m_iPrinterChange.GetAllParam().PrinterSetting.sCalibrationSetting.nPassStepArray;
			//this.m_NumericUpDownStep.Value = PassStepArray[PassList[m_ComboBoxPass.SelectedIndex]-1];
			//this.m_NumericUpDownStep.Value = PassStepArray[m_ComboBoxPass.SelectedIndex];
			if(this.m_iPrinterChange.GetAllParam().PrinterSetting.nKillBiDirBanding != 0)
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,PassStepArray[m_ComboBoxPass.SelectedIndex/2]);
			else
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,PassStepArray[m_ComboBoxPass.SelectedIndex]);

			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_CheckBoxUsePrinterSetting_Click(object sender, System.EventArgs e)
		{
			bool bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
            m_CheckBoxBidirection.Enabled = bEnabled;
            comboBoxMediaMode.Enabled =
            comboBoxPringConfigList.Enabled = bEnabled;
			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_ComboBoxPass_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
		}

		private void m_ComboBoxSpeed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
		
		}

		private void m_NumericUpDownOrigin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
			else if(e.KeyCode == Keys.Enter)
				m_AnyControl_ValueChanged(sender,e);
		}

		private void ToolbarSetting_VisibleChanged(object sender, System.EventArgs e)
		{
#if false
			int margin1 = 5;
			int margin2 = 8;
			int left = 0;
//			int top = m_LabelOrigin.Location.Y;
			if(m_LabelOrigin.Visible && m_NumericUpDownOrigin.Visible)
			{
				left+=margin2;
				m_LabelOrigin.Location = new Point(left,m_LabelOrigin.Top);
				left+=m_LabelOrigin.Width+margin1;
				m_NumericUpDownOrigin.Location = new Point(left,m_NumericUpDownOrigin.Top);
				left+=m_NumericUpDownOrigin.Width+margin2;
			}
			if(m_LabelOriginY.Visible && m_NumericUpDownOriginY.Visible)
			{
				m_LabelOriginY.Location = new Point(left,m_LabelOriginY.Top);
				left+=m_LabelOriginY.Width+margin1;
				m_NumericUpDownOriginY.Location = new Point(left,m_NumericUpDownOriginY.Top);
				left+=m_NumericUpDownOriginY.Width+margin2;
			}	
			if(m_LabelStep.Visible && m_NumericUpDownStep.Visible)
			{
				m_LabelStep.Location = new Point(left,m_LabelStep.Top);
				left+=m_LabelStep.Width+margin1;
				m_NumericUpDownStep.Location = new Point(left,m_NumericUpDownStep.Top);
				left+=m_NumericUpDownStep.Width+margin2;
			}	
			if(m_ComboBoxPass.Visible)
			{
				m_ComboBoxPass.Location = new Point(left,m_ComboBoxPass.Top);
				left+=m_ComboBoxPass.Width+margin2;
			}	
			if(labelSpeed.Visible && m_ComboBoxSpeed.Visible)
			{
				labelSpeed.Location = new Point(left,labelSpeed.Top);
				left+=labelSpeed.Width+margin1;
				m_ComboBoxSpeed.Location = new Point(left,m_ComboBoxSpeed.Top);
				left+=m_ComboBoxSpeed.Width+margin2;
			}	
			if(labelMediumSpeed.Visible && m_ComboBoxMediumSpeed.Visible)
			{
				labelMediumSpeed.Location = new Point(left,labelMediumSpeed.Top);
				left+=labelMediumSpeed.Width+margin1;
				m_ComboBoxMediumSpeed.Location = new Point(left,m_ComboBoxMediumSpeed.Top);
				left+=m_ComboBoxMediumSpeed.Width+margin2;
			}	
			if(m_CheckBoxBidirection.Visible)
			{
				m_CheckBoxBidirection.Location = new Point(left,m_CheckBoxBidirection.Top);
				left+=m_CheckBoxBidirection.Width+margin2;
			}				
			if(m_CheckBoxUsePrinterSetting.Visible)
			{
				m_CheckBoxUsePrinterSetting.Location = new Point(left,m_CheckBoxUsePrinterSetting.Top);
				left+=m_CheckBoxUsePrinterSetting.Width+margin2;
			}	
			if(m_CheckBoxHDPrint.Visible)
			{
				m_CheckBoxHDPrint.Location = new Point(left,m_CheckBoxHDPrint.Top);
				left+=m_CheckBoxHDPrint.Width+margin2;
			}
			if(checkBoxAlternatePrint.Visible)
			{
				checkBoxAlternatePrint.Location = new Point(left,checkBoxAlternatePrint.Top);
				left+=checkBoxAlternatePrint.Width+margin2;
			}
#endif
		}

		public void OnPrintingStart()
		{
			m_bDuringPrinting = true;
			SPrtFileInfo jobInfo = new SPrtFileInfo();
			if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
			{
				m_bInitControl = true;
				int realpass = jobInfo.sFreSetting.nPass;
				SPrinterSetting ss = this.m_iPrinterChange.GetAllParam().PrinterSetting;
				if(ss.nKillBiDirBanding != 0)
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[(realpass+1)/2 -1]);
				else
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[realpass -1]);
				m_bInitControl = false;
			}
		}
		public void OnPrintingEnd()
		{
			m_bDuringPrinting = false;
			SPrinterSetting ss = this.m_iPrinterChange.GetAllParam().PrinterSetting;
			m_bInitControl = true;
			if(ss.nKillBiDirBanding != 0)
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[(ss.sFrequencySetting.nPass+1)/2 -1]);
			else
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStep,ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1]);
			m_bInitControl = false;
		}
		///
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private bool bVerticalDirection = false;
		public bool VerticalDirection
		{
			get{return bVerticalDirection;}
			set
			{
				bVerticalDirection = value;
				if(bVerticalDirection)
				{
					panelOriginX.Dock =
						panelOriginY.Dock=
						panelSteps.Dock=
						panelPass.Dock =
						panelSpeed.Dock =
						panelMediaSpeed.Dock= 
						m_CheckBoxBidirection.Dock =
						m_CheckBoxUsePrinterSetting.Dock =
						m_CheckBoxHDPrint.Dock=
						checkBoxAlternatePrint.Dock = DockStyle.Top;
                    //m_NumericUpDownOrigin.Dock =
                    //m_NumericUpDownOriginY.Dock =
                    //m_NumericUpDownStep.Dock =
                    //m_ComboBoxPass.Dock =
                    //m_ComboBoxSpeed.Dock =
                    //m_ComboBoxMediumSpeed.Dock =DockStyle.Right;
					panel1.Dock = DockStyle.Fill;
				}
				else
				{
					panelOriginX.Dock =
						panelOriginY.Dock=
						panelSteps.Dock=
						panelPass.Dock =
						panelSpeed.Dock =
						panelMediaSpeed.Dock= 
						m_CheckBoxBidirection.Dock =
						m_CheckBoxUsePrinterSetting.Dock =
						m_CheckBoxHDPrint.Dock=
						checkBoxAlternatePrint.Dock = DockStyle.Left;

                    //m_NumericUpDownOrigin.Dock =
                    //    m_NumericUpDownOriginY.Dock =
                    //    m_NumericUpDownStep.Dock =
                    //    m_ComboBoxPass.Dock =
                    //    m_ComboBoxSpeed.Dock =
                    //    m_ComboBoxMediumSpeed.Dock =DockStyle.Left;
				}
			}
		}

        private void checkBoxAutoCenterPrint_CheckedChanged(object sender, EventArgs e)
        {
            SetPrinterStatusChanged(m_curStatus);
            if (m_bInitControl == false)
                m_iPrinterChange.NotifyUIParamChanged();
        }

        private void radioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            SetPrinterStatusChanged(m_curStatus);
            if (m_bInitControl == false)
                m_iPrinterChange.NotifyUIParamChanged();
        }

        private void comboBoxJobMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_AnyControl_ValueChanged(sender, e);
        }

        private void checkBoxFlatMode_CheckedChanged(object sender, EventArgs e)
        {
            SetPrinterStatusChanged(m_curStatus);
            if (m_bInitControl == false)
                m_iPrinterChange.NotifyUIParamChanged();
        }

        private void comboBoxLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            CoreInterface.LayoutIndex = comboBoxLayout.SelectedIndex;
        }

        private void btnMoisturize_Click(object sender, EventArgs e)
        {
            if (EpsonLCD.GetKeepwat() == 1)
            {
                EpsonLCD.SetKeepwat(0);
            }
            else
            {
                EpsonLCD.SetKeepwat(1);
            }
        }

        private void comboBoxStartPrintDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_AnyControl_ValueChanged(sender,e);
        }

        private void m_NumericUpYSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (m_bInitControl == false)
            {
                int ySpeed = (int)m_NumericUpYSpeed.Value;
                EpsonLCD.SetYStepSpeed(ySpeed);
                m_iPrinterChange.GetAllParam().ExtendedSettings.YSpeed = ySpeed;
            }
        }

        private void comboBoxMotorControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bInitControl) return;

            try
            {
                HapondMotorParam_Auto data = new HapondMotorParam_Auto();
                if (EpsonLCD.GetMotionSetting(ref data))
                {
                    data.Flag = new char[] { 'Y', 'T', 'O', 'Q' };
                    data.sysMode = (byte)(comboBoxMotorControl.SelectedIndex);

                    EpsonLCD.SetMotionSetting(data);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
	}
}
