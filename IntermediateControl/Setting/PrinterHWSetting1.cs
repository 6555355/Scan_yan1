/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using System.Diagnostics;

using BYHXPrinterManager.Main;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for PrinterHWSetting.
	/// </summary>
	public class PrinterHWSetting1 : BYHXUserControl
	{
		private UIPreference m_CurrentPreference;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Inch;
		const int MAX_HEAD_NUM = 16;
		byte [] m_WidthList = new byte []{18,25,32,33,35,50,55,60,100};	
		byte [] m_ColorNumList = new byte []{4,6,1,2,5,7};	
		byte [] m_InkType = new byte[]{0xA,0xB,0xC};
		private const string sPrinterProductList = "PrinterProductList_";
		ArrayList m_PrinterList = new ArrayList();	
		int m_nProductId = 0;
        public event EventHandler OKButtonClicked;


        private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button m_ButtonOK;
        private BYHXPrinterManager.GradientControls.CrystalPanel groupBox1;
		private System.Windows.Forms.RadioButton m_RadioButtonEncoder;
		private System.Windows.Forms.RadioButton m_RadioButtonServoEncoder;
		private System.Windows.Forms.Label m_LabelHighSpeed;
		private System.Windows.Forms.Label m_LabelColorNum;
		private System.Windows.Forms.Label m_LabelStripeSpace;
		private System.Windows.Forms.Label m_LabelStripeWidth;
		private System.Windows.Forms.ComboBox m_ComboBoxGroupNumber;
		private System.Windows.Forms.ComboBox m_ComboBoxColorNumber;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownGroupSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownColorSpace;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.ComboBox m_ComboBoxHeadType;
		private System.Windows.Forms.Label m_LabelHeadType;
		private System.Windows.Forms.Label m_LabelWidth;
		private System.Windows.Forms.ComboBox m_ComboBoxWidth;
		private System.Windows.Forms.Button m_ButtonClear;
		private System.Windows.Forms.ComboBox m_ComboBoxPrinterList;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxVender;
		private System.Windows.Forms.Button m_ButtonAdd;
		private System.Windows.Forms.Label m_LabelYSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownYSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownAngle;
		private System.Windows.Forms.Label m_LabelAngle;
		private System.Windows.Forms.ContextMenu m_ContextMenu;
		private System.Windows.Forms.MenuItem m_menuItemDelete;
		private System.Windows.Forms.Label LableInkType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_comboBoxInkType;
		private System.Windows.Forms.ComboBox m_comboBoxJetSpeed;
		private System.Windows.Forms.Button m_ButtonWriteInkCurve;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxInk;
		private System.Windows.Forms.CheckBox m_CheckBoxOneHeadDivider;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label m_LabelWhiteInkNum;
		private System.Windows.Forms.Label m_LabelCoatColorNum;
		private System.Windows.Forms.CheckBox m_CheckBoxIsHeadLeft;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWhiteColorNum;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownCoatColorNum;
		private System.ComponentModel.IContainer components;

		public PrinterHWSetting1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if !LIYUUSB
			m_GroupBoxInk.Visible = false;
			m_ButtonWriteInkCurve.Visible = false;
#else
			bool bfac = PubFunc.IsFactoryUser();
			if(!bfac)
			{
				groupBox1.Visible = false;
				if (m_GroupBoxInk.Visible)
				{
					m_GroupBoxInk.Top = m_GroupBoxVender.Top;
					m_GroupBoxInk.Height = m_GroupBoxVender.Height;
				}
				m_CheckBoxOneHeadDivider.Visible = false;
				m_CheckBoxIsHeadLeft.Visible = false;
				m_LabelYSpace.Visible = false;
				m_NumericUpDownYSpace.Visible = false;
				m_LabelAngle.Visible = false;
				m_NumericUpDownAngle.Visible = false;
				m_LabelWhiteInkNum.Visible = false;
				m_NumericUpDownWhiteColorNum.Visible = false;
				m_LabelCoatColorNum.Visible = false;
				m_NumericUpDownCoatColorNum.Visible = false;
			}
            m_ComboBoxPrinterList.Visible = false;
            m_ButtonAdd.Visible = false;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterHWSetting));
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonClear = new System.Windows.Forms.Button();
            this.m_ButtonAdd = new System.Windows.Forms.Button();
            this.m_ButtonWriteInkCurve = new System.Windows.Forms.Button();
            this.groupBox1 = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.m_RadioButtonServoEncoder = new System.Windows.Forms.RadioButton();
            this.m_RadioButtonEncoder = new System.Windows.Forms.RadioButton();
            this.m_LabelHighSpeed = new System.Windows.Forms.Label();
            this.m_ComboBoxGroupNumber = new System.Windows.Forms.ComboBox();
            this.m_LabelColorNum = new System.Windows.Forms.Label();
            this.m_ComboBoxColorNumber = new System.Windows.Forms.ComboBox();
            this.m_LabelStripeSpace = new System.Windows.Forms.Label();
            this.m_LabelStripeWidth = new System.Windows.Forms.Label();
            this.m_NumericUpDownGroupSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownColorSpace = new System.Windows.Forms.NumericUpDown();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_ComboBoxHeadType = new System.Windows.Forms.ComboBox();
            this.m_LabelHeadType = new System.Windows.Forms.Label();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.m_ComboBoxWidth = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxVender = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.m_CheckBoxOneHeadDivider = new System.Windows.Forms.CheckBox();
            this.m_LabelYSpace = new System.Windows.Forms.Label();
            this.m_NumericUpDownYSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.m_LabelAngle = new System.Windows.Forms.Label();
            this.m_NumericUpDownWhiteColorNum = new System.Windows.Forms.NumericUpDown();
            this.m_LabelWhiteInkNum = new System.Windows.Forms.Label();
            this.m_NumericUpDownCoatColorNum = new System.Windows.Forms.NumericUpDown();
            this.m_LabelCoatColorNum = new System.Windows.Forms.Label();
            this.m_CheckBoxIsHeadLeft = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxPrinterList = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxInk = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.LableInkType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_comboBoxInkType = new System.Windows.Forms.ComboBox();
            this.m_comboBoxJetSpeed = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.m_ContextMenu = new System.Windows.Forms.ContextMenu();
            this.m_menuItemDelete = new System.Windows.Forms.MenuItem();
            this.dividerPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownGroupSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColorSpace)).BeginInit();
            this.m_GroupBoxVender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWhiteColorNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCoatColorNum)).BeginInit();
            this.m_GroupBoxInk.SuspendLayout();
            this.SuspendLayout();
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            this.dividerPanel1.Controls.Add(this.m_ButtonClear);
            this.dividerPanel1.Controls.Add(this.m_ButtonAdd);
            this.dividerPanel1.Controls.Add(this.m_ButtonWriteInkCurve);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonOK
            // 
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonClear
            // 
            this.m_ButtonClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonClear, "m_ButtonClear");
            this.m_ButtonClear.Name = "m_ButtonClear";
            this.m_ButtonClear.Click += new System.EventHandler(this.m_ButtonClear_Click);
            // 
            // m_ButtonAdd
            // 
            resources.ApplyResources(this.m_ButtonAdd, "m_ButtonAdd");
            this.m_ButtonAdd.Name = "m_ButtonAdd";
            this.m_ButtonAdd.Click += new System.EventHandler(this.m_ButtonAdd_Click);
            // 
            // m_ButtonWriteInkCurve
            // 
            resources.ApplyResources(this.m_ButtonWriteInkCurve, "m_ButtonWriteInkCurve");
            this.m_ButtonWriteInkCurve.Name = "m_ButtonWriteInkCurve";
            this.m_ButtonWriteInkCurve.Click += new System.EventHandler(this.m_ButtonWriteInkCurve_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_RadioButtonServoEncoder);
            this.groupBox1.Controls.Add(this.m_RadioButtonEncoder);
            this.groupBox1.Divider = true;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.groupBox1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // m_RadioButtonServoEncoder
            // 
            this.m_RadioButtonServoEncoder.BackColor = System.Drawing.Color.Transparent;
            this.m_RadioButtonServoEncoder.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.m_RadioButtonServoEncoder, "m_RadioButtonServoEncoder");
            this.m_RadioButtonServoEncoder.Name = "m_RadioButtonServoEncoder";
            this.m_RadioButtonServoEncoder.UseVisualStyleBackColor = false;
            // 
            // m_RadioButtonEncoder
            // 
            this.m_RadioButtonEncoder.BackColor = System.Drawing.Color.Transparent;
            this.m_RadioButtonEncoder.Checked = true;
            this.m_RadioButtonEncoder.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.m_RadioButtonEncoder, "m_RadioButtonEncoder");
            this.m_RadioButtonEncoder.Name = "m_RadioButtonEncoder";
            this.m_RadioButtonEncoder.TabStop = true;
            this.m_RadioButtonEncoder.UseVisualStyleBackColor = false;
            // 
            // m_LabelHighSpeed
            // 
            this.m_LabelHighSpeed.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelHighSpeed, "m_LabelHighSpeed");
            this.m_LabelHighSpeed.Name = "m_LabelHighSpeed";
            // 
            // m_ComboBoxGroupNumber
            // 
            this.m_ComboBoxGroupNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxGroupNumber, "m_ComboBoxGroupNumber");
            this.m_ComboBoxGroupNumber.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxGroupNumber.Items"),
            resources.GetString("m_ComboBoxGroupNumber.Items1"),
            resources.GetString("m_ComboBoxGroupNumber.Items2")});
            this.m_ComboBoxGroupNumber.Name = "m_ComboBoxGroupNumber";
            // 
            // m_LabelColorNum
            // 
            this.m_LabelColorNum.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelColorNum, "m_LabelColorNum");
            this.m_LabelColorNum.Name = "m_LabelColorNum";
            // 
            // m_ComboBoxColorNumber
            // 
            this.m_ComboBoxColorNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxColorNumber, "m_ComboBoxColorNumber");
            this.m_ComboBoxColorNumber.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxColorNumber.Items"),
            resources.GetString("m_ComboBoxColorNumber.Items1")});
            this.m_ComboBoxColorNumber.Name = "m_ComboBoxColorNumber";
            this.m_ComboBoxColorNumber.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxColorNumber_SelectedIndexChanged);
            // 
            // m_LabelStripeSpace
            // 
            this.m_LabelStripeSpace.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelStripeSpace, "m_LabelStripeSpace");
            this.m_LabelStripeSpace.Name = "m_LabelStripeSpace";
            // 
            // m_LabelStripeWidth
            // 
            this.m_LabelStripeWidth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelStripeWidth, "m_LabelStripeWidth");
            this.m_LabelStripeWidth.Name = "m_LabelStripeWidth";
            // 
            // m_NumericUpDownGroupSpace
            // 
            this.m_NumericUpDownGroupSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownGroupSpace, "m_NumericUpDownGroupSpace");
            this.m_NumericUpDownGroupSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownGroupSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownGroupSpace.Name = "m_NumericUpDownGroupSpace";
            // 
            // m_NumericUpDownColorSpace
            // 
            this.m_NumericUpDownColorSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownColorSpace, "m_NumericUpDownColorSpace");
            this.m_NumericUpDownColorSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownColorSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownColorSpace.Name = "m_NumericUpDownColorSpace";
            // 
            // m_ComboBoxHeadType
            // 
            this.m_ComboBoxHeadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxHeadType, "m_ComboBoxHeadType");
            this.m_ComboBoxHeadType.Name = "m_ComboBoxHeadType";
            this.m_ComboBoxHeadType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxHeadType_SelectedIndexChanged);
            // 
            // m_LabelHeadType
            // 
            this.m_LabelHeadType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelHeadType, "m_LabelHeadType");
            this.m_LabelHeadType.Name = "m_LabelHeadType";
            // 
            // m_LabelWidth
            // 
            this.m_LabelWidth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelWidth, "m_LabelWidth");
            this.m_LabelWidth.Name = "m_LabelWidth";
            // 
            // m_ComboBoxWidth
            // 
            this.m_ComboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxWidth, "m_ComboBoxWidth");
            this.m_ComboBoxWidth.Name = "m_ComboBoxWidth";
            // 
            // m_GroupBoxVender
            // 
            this.m_GroupBoxVender.Controls.Add(this.m_CheckBoxOneHeadDivider);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelHighSpeed);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownGroupSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownColorSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxGroupNumber);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxColorNumber);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelStripeSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxVender.Controls.Add(this.m_ComboBoxHeadType);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelHeadType);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelYSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownYSpace);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownAngle);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelAngle);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownWhiteColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelWhiteInkNum);
            this.m_GroupBoxVender.Controls.Add(this.m_NumericUpDownCoatColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_LabelCoatColorNum);
            this.m_GroupBoxVender.Controls.Add(this.m_CheckBoxIsHeadLeft);
            this.m_GroupBoxVender.Divider = true;
            this.m_GroupBoxVender.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxVender.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            resources.ApplyResources(this.m_GroupBoxVender, "m_GroupBoxVender");
            this.m_GroupBoxVender.Name = "m_GroupBoxVender";
            this.m_GroupBoxVender.TabStop = false;
            // 
            // m_CheckBoxOneHeadDivider
            // 
            this.m_CheckBoxOneHeadDivider.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxOneHeadDivider, "m_CheckBoxOneHeadDivider");
            this.m_CheckBoxOneHeadDivider.Name = "m_CheckBoxOneHeadDivider";
            this.m_CheckBoxOneHeadDivider.UseVisualStyleBackColor = false;
            // 
            // m_LabelYSpace
            // 
            this.m_LabelYSpace.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelYSpace, "m_LabelYSpace");
            this.m_LabelYSpace.Name = "m_LabelYSpace";
            // 
            // m_NumericUpDownYSpace
            // 
            this.m_NumericUpDownYSpace.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownYSpace, "m_NumericUpDownYSpace");
            this.m_NumericUpDownYSpace.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownYSpace.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownYSpace.Name = "m_NumericUpDownYSpace";
            // 
            // m_NumericUpDownAngle
            // 
            this.m_NumericUpDownAngle.DecimalPlaces = 5;
            resources.ApplyResources(this.m_NumericUpDownAngle, "m_NumericUpDownAngle");
            this.m_NumericUpDownAngle.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownAngle.Name = "m_NumericUpDownAngle";
            // 
            // m_LabelAngle
            // 
            this.m_LabelAngle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelAngle, "m_LabelAngle");
            this.m_LabelAngle.Name = "m_LabelAngle";
            // 
            // m_NumericUpDownWhiteColorNum
            // 
            resources.ApplyResources(this.m_NumericUpDownWhiteColorNum, "m_NumericUpDownWhiteColorNum");
            this.m_NumericUpDownWhiteColorNum.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.m_NumericUpDownWhiteColorNum.Name = "m_NumericUpDownWhiteColorNum";
            // 
            // m_LabelWhiteInkNum
            // 
            this.m_LabelWhiteInkNum.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelWhiteInkNum, "m_LabelWhiteInkNum");
            this.m_LabelWhiteInkNum.Name = "m_LabelWhiteInkNum";
            // 
            // m_NumericUpDownCoatColorNum
            // 
            resources.ApplyResources(this.m_NumericUpDownCoatColorNum, "m_NumericUpDownCoatColorNum");
            this.m_NumericUpDownCoatColorNum.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.m_NumericUpDownCoatColorNum.Name = "m_NumericUpDownCoatColorNum";
            // 
            // m_LabelCoatColorNum
            // 
            this.m_LabelCoatColorNum.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_LabelCoatColorNum, "m_LabelCoatColorNum");
            this.m_LabelCoatColorNum.Name = "m_LabelCoatColorNum";
            // 
            // m_CheckBoxIsHeadLeft
            // 
            this.m_CheckBoxIsHeadLeft.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxIsHeadLeft, "m_CheckBoxIsHeadLeft");
            this.m_CheckBoxIsHeadLeft.Name = "m_CheckBoxIsHeadLeft";
            this.m_CheckBoxIsHeadLeft.UseVisualStyleBackColor = false;
            // 
            // m_ComboBoxPrinterList
            // 
            this.m_ComboBoxPrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPrinterList, "m_ComboBoxPrinterList");
            this.m_ComboBoxPrinterList.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxPrinterList.Items"),
            resources.GetString("m_ComboBoxPrinterList.Items1"),
            resources.GetString("m_ComboBoxPrinterList.Items2")});
            this.m_ComboBoxPrinterList.Name = "m_ComboBoxPrinterList";
            this.m_ComboBoxPrinterList.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxPrinterList_SelectedIndexChanged);
            this.m_ComboBoxPrinterList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxPrinterList_KeyDown);
            // 
            // m_GroupBoxInk
            // 
            this.m_GroupBoxInk.Controls.Add(this.LableInkType);
            this.m_GroupBoxInk.Controls.Add(this.label2);
            this.m_GroupBoxInk.Controls.Add(this.m_comboBoxInkType);
            this.m_GroupBoxInk.Controls.Add(this.m_comboBoxJetSpeed);
            this.m_GroupBoxInk.Divider = true;
            this.m_GroupBoxInk.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_GroupBoxInk.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxInk.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            resources.ApplyResources(this.m_GroupBoxInk, "m_GroupBoxInk");
            this.m_GroupBoxInk.Name = "m_GroupBoxInk";
            this.m_GroupBoxInk.TabStop = false;
            // 
            // LableInkType
            // 
            this.LableInkType.BackColor = System.Drawing.Color.Transparent;
            this.LableInkType.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.LableInkType, "LableInkType");
            this.LableInkType.Name = "LableInkType";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // m_comboBoxInkType
            // 
            this.m_comboBoxInkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_comboBoxInkType, "m_comboBoxInkType");
            this.m_comboBoxInkType.Items.AddRange(new object[] {
            resources.GetString("m_comboBoxInkType.Items"),
            resources.GetString("m_comboBoxInkType.Items1"),
            resources.GetString("m_comboBoxInkType.Items2"),
            resources.GetString("m_comboBoxInkType.Items3"),
            resources.GetString("m_comboBoxInkType.Items4"),
            resources.GetString("m_comboBoxInkType.Items5")});
            this.m_comboBoxInkType.Name = "m_comboBoxInkType";
            // 
            // m_comboBoxJetSpeed
            // 
            this.m_comboBoxJetSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_comboBoxJetSpeed, "m_comboBoxJetSpeed");
            this.m_comboBoxJetSpeed.Items.AddRange(new object[] {
            resources.GetString("m_comboBoxJetSpeed.Items"),
            resources.GetString("m_comboBoxJetSpeed.Items1"),
            resources.GetString("m_comboBoxJetSpeed.Items2")});
            this.m_comboBoxJetSpeed.Name = "m_comboBoxJetSpeed";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // m_ContextMenu
            // 
            this.m_ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemDelete});
            // 
            // m_menuItemDelete
            // 
            this.m_menuItemDelete.Index = 0;
            resources.ApplyResources(this.m_menuItemDelete, "m_menuItemDelete");
            // 
            // PrinterHWSetting
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.m_GroupBoxVender);
            this.Controls.Add(this.m_ComboBoxPrinterList);
            this.Controls.Add(this.m_GroupBoxInk);
            this.Controls.Add(this.progressBar1);
            resources.ApplyResources(this, "$this");
            this.Name = "PrinterHWSetting";
            this.Load += new System.EventHandler(this.PrinterHWSetting_Load);
            this.dividerPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownGroupSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownColorSpace)).EndInit();
            this.m_GroupBoxVender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownYSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWhiteColorNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCoatColorNum)).EndInit();
            this.m_GroupBoxInk.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_ButtonOK_Click(object sender, System.EventArgs e)
		{
			bool bSet = true;
			int value1 = m_RadioButtonEncoder.Checked ? 1: 0;
			SFWFactoryData fwData = new SFWFactoryData();
#if !LIYUUSB
			fwData.m_nValidSize = 62;
#endif
			fwData.m_nEncoder = (byte)value1;

#if !LIYUUSB
			VenderPrinterConfig config = (VenderPrinterConfig)m_PrinterList[m_ComboBoxPrinterList.SelectedIndex];
			fwData.m_nHeadType = (byte)config.nHeadType;
			fwData.m_nWidth = (byte)config.nWidth;
#else
            VenderPrinterConfig config;
            if (!OnGetVenderPrinterConfig(out config))
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.NullFactoryData);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            fwData.m_nHeadType = (byte)config.nHeadType;
            fwData.m_nWidth = (ushort)config.nWidth;
#endif
			fwData.m_nColorNum = config.nColorNum;
			fwData.m_nGroupNum = config.nGroupNum;
			fwData.m_fHeadXColorSpace = config.fHeadXColorSpace;
			fwData.m_fHeadXGroupSpace = config.fHeadXGroupSpace;
			fwData.m_fHeadYSpace = config.fHeadYSpace;
			fwData.m_fHeadAngle = config.fHeadAngle;
#if !LIYUUSB
			fwData.m_nWhiteInkNum = config.m_nWhiteInkNum;
			fwData.m_nOverCoatInkNum = config.m_nOverCoatInkNum;
			fwData.m_nBitFlag = config.m_nBitFlag;

			fwData.m_nReserve = new byte[60-28];
#else
			byte jetSpeed = (byte)m_comboBoxJetSpeed.SelectedIndex;
			byte inkType  = m_InkType[m_comboBoxInkType.SelectedIndex];
			if(CoreInterface.SetInkParam(jetSpeed, inkType) == 0)
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.SetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
#endif
			if(bSet && CoreInterface.SetFWFactoryData(ref fwData)!= 0)
			{
				string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.SetHWSetting);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
                if (this.OKButtonClicked != null)
                    this.OKButtonClicked(sender, e);
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.SetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			SavePrinterConfig(m_nProductId,m_PrinterList);
		}

		private void PrinterHWSetting_Load(object sender, System.EventArgs e)
		{
			//int encoder = 1;
		}
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_ComboBoxHeadType.Items.Clear();
			PrinterHeadEnum[] array = (PrinterHeadEnum[])Enum.GetValues(typeof(PrinterHeadEnum));

			SSupportList list = new SSupportList();
			if(CoreInterface.GetSupportList(ref list) > 0)
			{
				for (int i=0;i<list.m_nList.Length;i++)
				{
					for (int j=0;j<array.Length;j++)
					{
						if(list.m_nList[i] == (byte)array[j])
						{
							m_ComboBoxHeadType.Items.Add(array[j].ToString());
							break;
						}
					}
				}
				//m_ComboBoxHeadType.Items.Add(PrinterHeadEnum.Spectra_Polaris.ToString());
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			m_ComboBoxColorNumber.Items.Clear();
			for (int i=0;i<m_ColorNumList.Length;i++)
			{
				m_ComboBoxColorNumber.Items.Add(m_ColorNumList[i].ToString());
			}

			m_ComboBoxWidth.Items.Clear();
			for (int i=0;i<m_WidthList.Length;i++)
			{
				float width = UIPreference.ToDisplayLength(m_CurrentUnit,(float)m_WidthList[i]*100.0f/25.4f);
				m_ComboBoxWidth.Items.Add(width.ToString());
			}
#if LIYUUSB
			m_comboBoxInkType.Items.Clear();
			for (int i=0;i<m_InkType.Length;i++)
			{
				m_comboBoxInkType.Items.Add(m_InkType[i].ToString("X"));
			}
		
			byte jetSpeed = 0;
			byte inkType = 0;
			if(CoreInterface.GetInkParam(ref jetSpeed,ref inkType) != 0)
			{
				//m_comboBoxInkType=0
				int Selected = -1;
				for (int i=0; i<m_InkType.Length;i++)
				{
					if(m_InkType[i] == inkType)
					{
						Selected = i;
						break;
					}
				}
				m_comboBoxInkType.SelectedIndex = Selected;
				m_comboBoxJetSpeed.SelectedIndex = jetSpeed;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
#endif
			SFWFactoryData fwData = new SFWFactoryData();
			bool bGet = (CoreInterface.GetFWFactoryData(ref fwData) > 0);
			if(bGet)
			{
				m_RadioButtonEncoder.Checked = (fwData.m_nEncoder == 0)?false:true;
				m_RadioButtonServoEncoder.Checked = !m_RadioButtonEncoder.Checked;
				int sIndex = -1;
				for (int i=0;i<m_ComboBoxHeadType.Items.Count;i++)
				{
					if(((PrinterHeadEnum)fwData.m_nHeadType).ToString() == m_ComboBoxHeadType.Items[i].ToString())
					{
						sIndex = i;
						break;
					}
				}
				m_ComboBoxHeadType.SelectedIndex = sIndex;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
#if !LIYUUSB
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
				m_nProductId = sBoardInfo.m_nBoardProductID;
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
#else
			m_nProductId = 0x0100;
#endif
			LoadPrinterConfig(m_nProductId,out m_PrinterList);

			m_ComboBoxPrinterList.Items.Clear();
			for (int i=0; i<m_PrinterList.Count;i++)
			{
				m_ComboBoxPrinterList.Items.Add(GetComboxName(m_nProductId,(VenderPrinterConfig)m_PrinterList[i]));
			}
			m_ComboBoxPrinterList.Items.Add("Edit...");
			bool bFound = false;
			for (int i=0; i<m_PrinterList.Count;i++)
			{
				VenderPrinterConfig config = (VenderPrinterConfig)m_PrinterList[i];
				if( bGet &&
					(byte)config.nHeadType == fwData.m_nHeadType &&
					config.nWidth == fwData.m_nWidth &&
					config.nColorNum == fwData.m_nColorNum &&
					config.nGroupNum == fwData.m_nGroupNum &&
					config.fHeadXColorSpace == fwData.m_fHeadXColorSpace &&
					config.fHeadXGroupSpace == fwData.m_fHeadXGroupSpace&&
					config.fHeadYSpace == fwData.m_fHeadYSpace &&
					config.fHeadAngle == fwData.m_fHeadAngle 
#if !LIYUUSB
                    &&
					config.m_nWhiteInkNum == fwData.m_nWhiteInkNum &&
					config.m_nOverCoatInkNum == fwData.m_nOverCoatInkNum &&
					config.m_nBitFlag == fwData.m_nBitFlag 
#endif
					)
				{
					bFound = true;
					m_ComboBoxPrinterList.SelectedIndex = i;
					OnSetVenderPrinterConfig((VenderPrinterConfig)m_PrinterList[i]);
					//m_GroupBoxVender.Enabled = false;
					break;
				}
			}
			if(!bFound)
			{
				m_ComboBoxPrinterList.SelectedIndex = m_ComboBoxPrinterList.Items.Count - 1;
				m_GroupBoxVender.Enabled = true;
				//Should be error because get value maybe illegal value
				VenderPrinterConfig config = new VenderPrinterConfig();
				if(bGet)
				{
					config.nHeadType =(PrinterHeadEnum) fwData.m_nHeadType;
					config.nWidth = fwData.m_nWidth;
					config.nColorNum = fwData.m_nColorNum;
					config.nGroupNum = fwData.m_nGroupNum;
					config.fHeadXColorSpace = fwData.m_fHeadXColorSpace;
					config.fHeadXGroupSpace = fwData.m_fHeadXGroupSpace;
					config.fHeadYSpace = fwData.m_fHeadYSpace;
					config.fHeadAngle = fwData.m_fHeadAngle;
#if !LIYUUSB
					config.m_nWhiteInkNum = fwData.m_nWhiteInkNum;
					config.m_nOverCoatInkNum = fwData.m_nOverCoatInkNum;
					config.m_nBitFlag = fwData.m_nBitFlag; 
#endif
				}
				else
				{
					config.nHeadType =(PrinterHeadEnum) sp.ePrinterHead;
					config.nWidth = (byte)(sp.fMaxPaperWidth*0.254f+0.5f);
					config.nColorNum = sp.nColorNum;
					if(SPrinterProperty.IsPolaris(config.nHeadType))
					{
						if(sp.nOneHeadDivider > 1)
							config.nGroupNum = (sbyte)(-sp.nOneHeadDivider);
						else
							config.nGroupNum = (sbyte)sp.nHeadNumPerGroupY;
					}
					else if(SPrinterProperty.IsKonica512 (config.nHeadType))
					{
						if(sp.nOneHeadDivider > 1)
							config.nGroupNum = (sbyte)(-sp.nOneHeadDivider);
						else
						config.nGroupNum = (sbyte)(sp.nHeadNumPerGroupY);
					}
					else
						config.nGroupNum = (sbyte)(sp.nHeadNumPerGroupY*sp.nHeadNumPerColor);
					config.fHeadXColorSpace = sp.fHeadXColorSpace;
					config.fHeadXGroupSpace = sp.fHeadXGroupSpace;
					config.fHeadYSpace = sp.fHeadYSpace;
					config.fHeadAngle = sp.fHeadAngle;
#if !LIYUUSB
					config.m_nWhiteInkNum = fwData.m_nWhiteInkNum;
					config.m_nOverCoatInkNum = fwData.m_nOverCoatInkNum;
					config.m_nBitFlag = fwData.m_nBitFlag; 
#endif
				}
				OnSetVenderPrinterConfig(config);

			}
		}
		private string GetVenderString(int productID)
		{
			foreach(VenderID mode in Enum.GetValues(typeof(VenderID)))
			{
				if((int)mode == productID)
				{
					return mode.ToString();
				}
			}
			return productID.ToString("X");
		}
		private string GetComboxName(int productID ,VenderPrinterConfig printer)
		{
			//const string sep = " _ ";
			const string sep = " : ";
			return  "PN_"+productID.ToString("X")+ 
					sep+ printer.nHeadType.ToString()+
					sep + "WD_"+((int)printer.nWidth*10).ToString()+
					sep+  "CN_"+printer.nColorNum.ToString()+
					sep + "GN_"+printer.nGroupNum.ToString()+
					sep + "CS_"+ UIPreference.ToDisplayLength(m_CurrentUnit,printer.fHeadXColorSpace).ToString()+
					sep + "GS_"+ UIPreference.ToDisplayLength(m_CurrentUnit,printer.fHeadXGroupSpace).ToString()+
					sep + "YS_"+ UIPreference.ToDisplayLength(m_CurrentUnit,printer.fHeadYSpace).ToString()+
					sep + "AG_"+ printer.fHeadAngle.ToString()
					;
		}
		public void OnGetProperty(ref SPrinterProperty sp,ref bool bChangeProperty)
		{
#if !LIYUUSB
			VenderPrinterConfig config = (VenderPrinterConfig)m_PrinterList[m_ComboBoxPrinterList.SelectedIndex];
#else
            VenderPrinterConfig config;
			if(!OnGetVenderPrinterConfig(out config))
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.NullFactoryData);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
#endif
			bChangeProperty = false;
			bool bChange = 
				(
				config.nWidth			!=	(int)(sp.fMaxPaperWidth*0.254f+0.5f)||
				config.nColorNum		!=	sp.nColorNum		||
				config.fHeadXColorSpace !=	sp.fHeadXColorSpace	||
				config.fHeadXGroupSpace !=	sp.fHeadXGroupSpace	||
				config.nHeadType !=	sp.ePrinterHead	            ||
				config.fHeadYSpace !=	sp.fHeadYSpace	||
				config.fHeadAngle !=	sp.fHeadAngle	
#if !LIYUUSB
				||config.m_nWhiteInkNum != (sp.nWhiteInkNum&0xf)
				||config.m_nOverCoatInkNum != ((sp.nWhiteInkNum&0xf0)>>4)
				||((config.m_nBitFlag&1) == 0) != sp.bHeadInLeft
#endif

			 );
			if(!bChange)
			{
				if(SPrinterProperty.IsPolaris(config.nHeadType))
				{
					if(config.nGroupNum>0)
					{
						if(config.nGroupNum		!=	sp.nHeadNumPerGroupY)
							bChange = true;
					}
					else
					{
						if(config.nGroupNum		!=	-sp.nOneHeadDivider)
							bChange = true;
					}
				}
				else if(SPrinterProperty.IsKonica512 (config.nHeadType))
				{
					if(config.nGroupNum		!=	sp.nHeadNumPerGroupY)
						bChange = true;
				}
				else
				{
					if(config.nGroupNum		!=	sp.nHeadNumPerGroupY*sp.nHeadNumPerColor)
						bChange = true;
				}
			}
			if(bChange)
			{
				bChangeProperty = true;
				sp.fMaxPaperWidth = (float)config.nWidth/0.254f;
				sp.nColorNum = config.nColorNum;
				if(SPrinterProperty.IsPolaris(config.nHeadType))
				{
					if(config.nGroupNum > 0)
					{
						sp.nHeadNumPerGroupY = (byte)config.nGroupNum	;
						sp.nOneHeadDivider = 1;
						sp.nHeadNumPerColor = 4	;
					}
					else
					{
						sp.nHeadNumPerGroupY =(byte)-config.nGroupNum;
						sp.nOneHeadDivider = 2;
						sp.nHeadNumPerColor = 2;
					}
				}
				else if(SPrinterProperty.IsKonica512 (config.nHeadType))
				{
					if(config.nGroupNum > 0)
					{
						sp.nHeadNumPerGroupY = (byte)config.nGroupNum	;
						sp.nHeadNumPerColor = 2	;
						sp.nOneHeadDivider = 1;
					}
					else
					{
						sp.nHeadNumPerGroupY = (byte)-config.nGroupNum	;
						sp.nHeadNumPerColor = 1	;
						sp.nOneHeadDivider = 2;
					}
				}
				else
				{
					sp.nHeadNumPerColor  = 1;
					sp.nHeadNumPerGroupY = (byte)config.nGroupNum	;
				}
				sp.fHeadXColorSpace = config.fHeadXColorSpace;
				sp.fHeadXGroupSpace = config.fHeadXGroupSpace;
				sp.nHeadNum = (byte)(sp.nColorNum * sp.nHeadNumPerGroupY * sp.nHeadNumPerColor);
				sp.nHeadNumPerRow = (byte)(sp.nColorNum *sp.nHeadNumPerColor);
				sp.ePrinterHead = config.nHeadType;
				sp.fHeadYSpace = config.fHeadYSpace;
				sp.fHeadAngle = config.fHeadAngle;
#if !LIYUUSB
				sp.nWhiteInkNum = (byte)((config.m_nOverCoatInkNum<<4) + config.m_nWhiteInkNum);
				sp.bHeadInLeft = (config.m_nBitFlag&1) == 0;
#endif
			}
#if false
			int nResX = Convert.ToInt32(m_ComboBoxResolution.Items[m_ComboBoxResolution.SelectedIndex]);
			if(sp.nResX !=  nResX)
			{
				bChangeProperty = true;
				sp.nResX = nResX;
			}
#endif
		}

		public void OnPreferenceChange( UIPreference up)
		{
			m_CurrentPreference = up;
			//m_bInitControl = true;
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
			}
			//m_bInitControl = false;
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownColorSpace);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownColorSpace,this.m_ToolTip);

			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownGroupSpace);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownGroupSpace,this.m_ToolTip);
	
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownYSpace);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownYSpace,this.m_ToolTip);
		}

		private void m_ComboBoxColorNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int colornumber = 4;
			if(m_ComboBoxColorNumber.SelectedIndex != -1)
			{
				colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
			}

			int maxgroup = MAX_HEAD_NUM/colornumber;
			int length = m_ComboBoxGroupNumber.Items.Count;
			if(maxgroup > length)
			{
				for (int i= length; i<maxgroup;i++)
					m_ComboBoxGroupNumber.Items.Add((i+1)); 
			}
			else if(maxgroup < length)
			{
				for (int i= maxgroup; i< length;i++)
					m_ComboBoxGroupNumber.Items.Remove((i+1)); 
			}
		}
		private void m_ButtonClear_Click(object sender, System.EventArgs e)
		{
			if(CoreInterface.SendJetCommand((int)JetCmdEnum.ClearFWFactoryData,0) != 0)
			{
				string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.ClearFWFactoryData);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			else
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.ClearFWFactoryData);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}

		}
		bool LoadPrinterConfig(int productID,out ArrayList list)
		{
			list = new ArrayList();
			string curFile = Application.StartupPath + 
				Path.DirectorySeparatorChar +sPrinterProductList
				+ productID.ToString("X") + ".xml";
			if(!File.Exists(curFile))
				return false;
			XmlDocument doc = new XmlDocument();
			doc.Load(curFile);
			try
			{
				XmlElement	root	= (XmlElement)doc.DocumentElement;
				try
				{
					XmlNode XmlJob = root.FirstChild;
					while(XmlJob != null)
					{
						VenderPrinterConfig job = (VenderPrinterConfig)VenderPrinterConfig.SystemConvertFromXml((XmlElement) XmlJob,typeof(VenderPrinterConfig));
						list.Add(job);
						XmlJob = XmlJob.NextSibling;
					}
				}
				catch(Exception e)
				{
					Debug.Assert(false,e.Message + e.StackTrace);
				}
				return true;
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}
		}
		bool SavePrinterConfig(int productID,ArrayList list)
		{
			string curFile = Application.StartupPath + 
				Path.DirectorySeparatorChar +sPrinterProductList
				+ productID.ToString("X") + ".xml";
			XmlElement root;
			XmlDocument doc = new XmlDocument();
			
			bool	success	= true;
			try
			{
				root = doc.CreateElement("","product_"+productID.ToString("X"),"");
				doc.AppendChild(root);
				string xml = "";
				for (int i=0; i< list.Count;i++)
				{
					VenderPrinterConfig job = ((VenderPrinterConfig)list[i]).Clone();
					xml += job.SystemConvertToXml();
				}
				root.InnerXml = xml;
			    doc.Save(curFile);
			}
			catch(Exception e)
			{
				success	= false;

				Debug.Assert(false,e.Message + e.StackTrace);
			}
			return success;
		}

		private void m_ComboBoxPrinterList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int index = m_ComboBoxPrinterList.SelectedIndex;
			if(index >=0 && index < m_PrinterList.Count)
			{
				SetToEditStatus(false);
				OnSetVenderPrinterConfig((VenderPrinterConfig)m_PrinterList[index]);
			}
			else 
			{
				SetToEditStatus(true);
			}
		}
		private bool OnGetVenderPrinterConfig(out VenderPrinterConfig config)
		{
			bool bSet = true;
			config = new VenderPrinterConfig();
			int colornumber = 4;
			if(m_ComboBoxColorNumber.SelectedIndex == -1)
				bSet = false;
			else
			{
				colornumber = m_ColorNumList[m_ComboBoxColorNumber.SelectedIndex];
			}
			config.nColorNum = (byte)colornumber;
			if(m_ComboBoxGroupNumber.SelectedIndex == -1)
				bSet = false;
			else
				config.nGroupNum = (sbyte)(m_ComboBoxGroupNumber.SelectedIndex + 1);

			config.fHeadXColorSpace = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownColorSpace.Value));
			config.fHeadXGroupSpace = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownGroupSpace.Value));
			config.fHeadYSpace = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownYSpace.Value));
			config.fHeadAngle = Decimal.ToSingle(m_NumericUpDownAngle.Value);
	
			config.m_nWhiteInkNum = Decimal.ToByte(m_NumericUpDownWhiteColorNum.Value);
			config.m_nOverCoatInkNum = Decimal.ToByte(m_NumericUpDownCoatColorNum.Value);
			uint nBitFlag = 0;
			if(m_CheckBoxIsHeadLeft.Checked)
				nBitFlag += 0x1;
			config.m_nBitFlag = nBitFlag;

			if(m_ComboBoxWidth.SelectedIndex == -1)
				bSet = false;
			else
				config.nWidth = m_WidthList[m_ComboBoxWidth.SelectedIndex];

			PrinterHeadEnum[] array = (PrinterHeadEnum[])Enum.GetValues(typeof(PrinterHeadEnum));
			if(m_ComboBoxHeadType.SelectedIndex != -1)
			{
				for (int i=0;i<array.Length;i++)
				{
					if(array[i].ToString() == m_ComboBoxHeadType.SelectedItem.ToString())
					{
						config.nHeadType = array[i];
						break;
					}
				}
			}
			else 
				bSet = false;
			if(SPrinterProperty.IsPolaris(config.nHeadType) || SPrinterProperty.IsKonica512(config.nHeadType))
			{
				if(m_CheckBoxOneHeadDivider.Checked)
					config.nGroupNum *= -1;
			}
			return bSet;
		}
		private void OnSetVenderPrinterConfig(VenderPrinterConfig config)
		{
			int selectIndex = 0;
			for (int i=0; i<m_ColorNumList.Length;i++)
			{
				if(config.nColorNum == m_ColorNumList[i])
				{
					selectIndex = i;
					break;
				}
			}
			m_ComboBoxColorNumber.SelectedIndex = selectIndex;
			m_ComboBoxGroupNumber.Items.Clear();
			int maxgroup = 3;
			//if(config.nColorNum == 4 || config.nColorNum == 6 )
			if(config.nColorNum != 0)
				maxgroup = MAX_HEAD_NUM/config.nColorNum;
			for (int i= 0; i<maxgroup;i++)
			{
				m_ComboBoxGroupNumber.Items.Add((i+1)); 
			}
			if(config.nGroupNum > 0)
			{
				m_ComboBoxGroupNumber.SelectedIndex = config.nGroupNum -1 ;
				m_CheckBoxOneHeadDivider.Checked = false;
			}
			else
			{
				m_ComboBoxGroupNumber.SelectedIndex = -(config.nGroupNum) -1 ;
				m_CheckBoxOneHeadDivider.Checked = true;
			}
			m_NumericUpDownColorSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,config.fHeadXColorSpace));
			m_NumericUpDownGroupSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,config.fHeadXGroupSpace));
			m_NumericUpDownYSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,config.fHeadYSpace));
			m_NumericUpDownAngle.Value = new Decimal(config.fHeadAngle);

			m_NumericUpDownWhiteColorNum.Value = new Decimal(config.m_nWhiteInkNum);
			m_NumericUpDownCoatColorNum.Value = new Decimal(config.m_nOverCoatInkNum);
			m_CheckBoxIsHeadLeft.Checked = ((config.m_nBitFlag &0x1) != 0);

			int sIndex = -1;
			for (int i=0;i<m_WidthList.Length;i++)
			{
				if(config.nWidth == m_WidthList[i])
				{
					sIndex = i;
					break;
				}
			}
			m_ComboBoxWidth.SelectedIndex = sIndex;

			int index = -1;
			for (int i=0; i<m_ComboBoxHeadType.Items.Count;i++)
			{
				if(m_ComboBoxHeadType.Items[i].ToString() == config.nHeadType.ToString())
				{
					index = i;
					break;
				}
			}
			m_ComboBoxHeadType.SelectedIndex = index;

		}
		private void SetToEditStatus(bool bEdit)
		{
			//m_GroupBoxVender.Enabled = bEdit;
            //m_ButtonOK.Enabled = !bEdit;
			m_ButtonAdd.Enabled = bEdit;
		}

		private void m_ButtonAdd_Click(object sender, System.EventArgs e)
		{
			VenderPrinterConfig config;
			if(!OnGetVenderPrinterConfig(out config))
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.NullFactoryData);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			m_PrinterList.Add(config);

			m_ComboBoxPrinterList.Items.Clear();
			for (int i=0; i<m_PrinterList.Count;i++)
			{
				m_ComboBoxPrinterList.Items.Add(GetComboxName(m_nProductId,(VenderPrinterConfig)m_PrinterList[i]));
			}
			m_ComboBoxPrinterList.Items.Add("Edit...");
			m_ComboBoxPrinterList.SelectedIndex = m_ComboBoxPrinterList.Items.Count - 1;
		}

		private void m_ComboBoxPrinterList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && this.m_ComboBoxPrinterList.SelectedIndex != (this.m_ComboBoxPrinterList.Items.Count -1))
			{
				int SelectedIndex_old = this.m_ComboBoxPrinterList.SelectedIndex;
				m_PrinterList.RemoveAt(this.m_ComboBoxPrinterList.SelectedIndex);

				m_ComboBoxPrinterList.Items.Clear();

				for (int i = 0; i < m_PrinterList.Count; i++)
				{
					m_ComboBoxPrinterList.Items.Add(GetComboxName(m_nProductId, (VenderPrinterConfig)m_PrinterList[i]));
				}
				m_ComboBoxPrinterList.Items.Add("Add...");
				m_ComboBoxPrinterList.SelectedIndex = SelectedIndex_old;
				//m_ComboBoxPrinterList.DroppedDown = false;
			}
		}

		private void m_ButtonWriteInkCurve_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Job Files (*.xml)|*.xml|All Files (*.*)|*.*";

			openFileDialog1.InitialDirectory = m_CurrentPreference.WorkingFolder;
			if(openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
                Cursor.Current = Cursors.WaitCursor;
                this.progressBar1.Visible = true;
                this.TopLevelControl.Refresh();
				m_CurrentPreference.WorkingFolder =  Path.GetDirectoryName(openFileDialog1.FileName);
				try
				{
					MyXmlReader mXmlReader = new MyXmlReader();
					string mFileName = openFileDialog1.FileName + ".txt";
					mXmlReader.ReadXml(openFileDialog1.FileName);

                    int i = 0;
					foreach (AREA area in mXmlReader.AREAsFromXml1)
					{
                        i++;
						byte[] areaDatas = mXmlReader.GetAREABuffer(area);
						int retc = CoreInterface.WriteHBEEprom(areaDatas, area.AreaHeader.Size, area.ADDR);
						if(retc != area.AreaHeader.Size)
						{
							MessageBox.Show(this,"Write failed, please try again!",this.Text,MessageBoxButtons.OK);
							this.progressBar1.Value = 0;
							this.progressBar1.Visible = false;
							return;
						}
						else
							this.progressBar1.Value = i * 100 / mXmlReader.AREAsFromXml1.Count;
					}

					FileStream fileStream = new FileStream(mFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite); ;
					BinaryWriter bw = new BinaryWriter(fileStream);

					foreach (AREA area in mXmlReader.AREAsFromXml1)
					{
						byte[] areaDatas = mXmlReader.GetAREABuffer(area);
						bw.Seek(area.ADDR,0);
						bw.Write(areaDatas);
					}

					bw.Flush();
					bw.Close();

                    MessageBox.Show("Write success!");
                    this.progressBar1.Value = 0;
                    this.progressBar1.Visible = false;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				Cursor.Current = Cursors.Default;
			}
		}

		private void m_ComboBoxHeadType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string select = m_ComboBoxHeadType.Items[m_ComboBoxHeadType.SelectedIndex].ToString();
			if(PrinterHeadEnum.Spectra_Polaris_15pl.ToString() == select ||
				PrinterHeadEnum.Spectra_Polaris_35pl.ToString() == select ||
				PrinterHeadEnum.Konica_KM512L_42pl.ToString() == select ||
				PrinterHeadEnum.Konica_KM512LNX_35pl.ToString() == select ||
				PrinterHeadEnum.Konica_KM512M_14pl.ToString() == select 
				)
			{
				m_CheckBoxOneHeadDivider.Visible = true;
			}
			else
			{
				m_CheckBoxOneHeadDivider.Checked = false;
				m_CheckBoxOneHeadDivider.Visible = false;
			}
		}

        private void m_ButtonCancel_Click(object sender, EventArgs e)
        {

        }

	}
}
