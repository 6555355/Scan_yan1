/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
//#define YUTAI_RES_MAP
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using BYHXControls;
using System.IO;
using System.Xml;
using PrinterStubC.Interface;
using System.Collections.ObjectModel;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for CalibrationSetting.
    /// </summary>
    public class CalibrationSetting_Layout : BYHXUserControl//System.Windows.Forms.UserControl
    {
        private SPrinterProperty m_rsPrinterPropery;//Only read for color order
        public event EventHandler ApplyButtonClicked;
        public event EventHandler CalWizardButtonClicked;

        private IPrinterChange m_iPrinterChange;
        private bool isDirty = false;
        private int m_ColorNum = 0;
        private int m_nDisplaySpeedNum = CoreConst.MAX_SPEED_NUM;

        private string grouptext = string.Empty;
        private int groupWidth = 0;

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }
        private int[] m_nResList = null;
        private SCalibrationSetting m_sCalibrationSetting;
        private SCalibrationHorizonArrayUI m_NewHorizonArrayUI;
        private SCalibrationGroupUI m_CalibrationGroupUILeft;
        private SCalibrationGroupUI m_CalibrationGroupUIRight;

        private int m_nBaseResX = 0;
        private int m_Yinterleave = 0;
        private int m_HeadNum = 0;
        private int m_PassListNum = 0;
        private int[] m_PassList = null;
        private int m_CurPassSelectIndex;
        private int m_GroupDefWidth = 0;
        public bool IsUseGroup = false;

        private int m_ColorNumAndColumnNum = 0;
        private int m_HorDefWidth = 0;
        public int m_HorColumnNum = 1;
        private int m_GroupNum = 0;
        private int m_RealGroupNum = 0;

        private System.Windows.Forms.Label[] m_LabelHorHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxHorLeft;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxHorRight;
        private int[] ColorIDList;


        private System.Windows.Forms.Label[] m_LabelVerHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxVer;

        private System.Windows.Forms.Label[] m_LabelOverLapHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxOverLap;

        private System.Windows.Forms.Label[] m_LabelGroupHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxGroupLeft;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxGroupRight;

        //private System.Windows.Forms.Label   []m_LabelPassList;
        //private System.Windows.Forms.TextBox []m_TextBoxStep;
        //private int m_PassSelectIndex;
        //private int m_SpeedSelectIndex;
        private CalibrationSubStep m_SubStep;
        private bool bNeedLoadPrinterModeFromXML = false;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private System.Windows.Forms.TextBox m_NumUDBidirection;
        private System.Windows.Forms.NumericUpDown m_TextBoxVerSample;
        private System.Windows.Forms.Label m_LabelVerValue;
        private System.Windows.Forms.Label m_LabelVerHead;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxStep;
        private System.Windows.Forms.NumericUpDown m_TextBoxStepSample;
        private System.Windows.Forms.Label m_LabelStep;
        private System.Windows.Forms.Label m_LabelBase;
        private System.Windows.Forms.NumericUpDown m_TextBoxBaseStep;
        private System.Windows.Forms.ComboBox m_ComboBoxSpeed;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxVer;
        private System.Windows.Forms.Label m_LabelVerSample;
        private System.Windows.Forms.ComboBox m_ComboBoxPass;
        private System.Windows.Forms.Label m_LabelPixel;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownRevise;
        private System.Windows.Forms.Button m_ButtonCalculate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAutoCopy;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.Button m_ButtonPrecision;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private BYHXPrinterManager.GradientControls.Grouper grouperOverLap;
        private System.Windows.Forms.NumericUpDown textBoxOverLapSample;
        private GradientControls.Grouper grouperOrigin;
        private NumericUpDown textBoxCustomSpeed;
        private NumericUpDown textBoxLowSpeed;
        private NumericUpDown textBoxMidSpeed;
        private ComboBox m_ComboBoxResX;
        private Label label2;
        private NumericUpDown textBoxHightSpeed;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private GradientControls.Grouper grouperGroup;
        private NumericUpDown m_TextBoxGroupRightSample;
        private Label labelGroupRight;
        private NumericUpDown m_TextBoxGroupLeftSample;
        private Label labelGroupLeft;
        private Label labelGroup;
        private Label m_LabelGroupSample;
        private NumericUpDown m_NumUDBidirection;
        private NumericUpDown m_TextBoxRightSample;
        private Label m_LabelHorSample;
        private Label m_LabelHead;
        private Label m_LabelRight;
        private Label m_LabelLeft;
        private Label m_LabelStripeWidth;
        private NumericUpDown m_TextBoxLeftSample;
        private CheckBox checkBoxQuickMode;
        private Label labelColor;
        private ComboBox comboBoxColor;
        private ComboBox comboBoxCaliMode;
        private Label labelLineWidth;
        private NumericUpDown numericUpDownLineWidth;
        private GradientControls.Grouper m_GroupBoxHor;
        private Panel panel1;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public CalibrationSetting_Layout()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            initialSupportCaliSubSteps();
            // TODO: Add any initialization after the InitializeComponent call
            m_SubStep = CalibrationSubStep.All;
            m_GroupDefWidth = this.m_GroupBoxHor.Width;
            m_HorDefWidth = this.m_GroupBoxVer.Width;
            m_iPrinterChange = null;
            grouperOrigin.Visible = false;
            caliSubStepSupport.Remove(CalibrationSubStep.Origin);
        }

        public byte Platform { get; set; }

        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationSetting_Layout));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style5 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style6 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style7 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style8 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style9 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style10 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style11 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style12 = new BYHXPrinterManager.Style();
            this.buttonAutoCopy = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.m_ComboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxVer = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_TextBoxVerSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelVerValue = new System.Windows.Forms.Label();
            this.m_LabelVerHead = new System.Windows.Forms.Label();
            this.m_LabelVerSample = new System.Windows.Forms.Label();
            this.m_GroupBoxStep = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_ButtonCalculate = new System.Windows.Forms.Button();
            this.m_LabelPixel = new System.Windows.Forms.Label();
            this.m_NumericUpDownRevise = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxPass = new System.Windows.Forms.ComboBox();
            this.m_TextBoxStepSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelStep = new System.Windows.Forms.Label();
            this.m_TextBoxBaseStep = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonPrecision = new System.Windows.Forms.Button();
            this.m_LabelBase = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grouperOverLap = new BYHXPrinterManager.GradientControls.Grouper();
            this.textBoxOverLapSample = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grouperOrigin = new BYHXPrinterManager.GradientControls.Grouper();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCustomSpeed = new System.Windows.Forms.NumericUpDown();
            this.textBoxLowSpeed = new System.Windows.Forms.NumericUpDown();
            this.textBoxMidSpeed = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxResX = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHightSpeed = new System.Windows.Forms.NumericUpDown();
            this.grouperGroup = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_TextBoxGroupRightSample = new System.Windows.Forms.NumericUpDown();
            this.labelGroupRight = new System.Windows.Forms.Label();
            this.m_TextBoxGroupLeftSample = new System.Windows.Forms.NumericUpDown();
            this.labelGroupLeft = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.m_LabelGroupSample = new System.Windows.Forms.Label();
            this.m_NumUDBidirection = new System.Windows.Forms.NumericUpDown();
            this.m_TextBoxRightSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHorSample = new System.Windows.Forms.Label();
            this.m_LabelHead = new System.Windows.Forms.Label();
            this.m_LabelRight = new System.Windows.Forms.Label();
            this.m_LabelLeft = new System.Windows.Forms.Label();
            this.m_LabelStripeWidth = new System.Windows.Forms.Label();
            this.m_TextBoxLeftSample = new System.Windows.Forms.NumericUpDown();
            this.checkBoxQuickMode = new System.Windows.Forms.CheckBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.comboBoxCaliMode = new System.Windows.Forms.ComboBox();
            this.labelLineWidth = new System.Windows.Forms.Label();
            this.numericUpDownLineWidth = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxHor = new BYHXPrinterManager.GradientControls.Grouper();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_GroupBoxVer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxVerSample)).BeginInit();
            this.m_GroupBoxStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRevise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxStepSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxBaseStep)).BeginInit();
            this.grouperOverLap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOverLapSample)).BeginInit();
            this.grouperOrigin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCustomSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxLowSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMidSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxHightSpeed)).BeginInit();
            this.grouperGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxGroupRightSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxGroupLeftSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumUDBidirection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxRightSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxLeftSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).BeginInit();
            this.m_GroupBoxHor.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAutoCopy
            // 
            this.buttonAutoCopy.ContextMenu = this.contextMenu1;
            resources.ApplyResources(this.buttonAutoCopy, "buttonAutoCopy");
            this.buttonAutoCopy.Name = "buttonAutoCopy";
            this.buttonAutoCopy.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonAutoCopy_MouseUp);
            // 
            // m_ComboBoxSpeed
            // 
            this.m_ComboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxSpeed, "m_ComboBoxSpeed");
            this.m_ComboBoxSpeed.Name = "m_ComboBoxSpeed";
            this.m_ComboBoxSpeed.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxSpeed_SelectedIndexChanged);
            // 
            // m_GroupBoxVer
            // 
            this.m_GroupBoxVer.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxVer.BorderThickness = 1F;
            this.m_GroupBoxVer.Controls.Add(this.m_TextBoxVerSample);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerValue);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerHead);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerSample);
            resources.ApplyResources(this.m_GroupBoxVer, "m_GroupBoxVer");
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxVer.GradientColors = style1;
            this.m_GroupBoxVer.GroupImage = null;
            this.m_GroupBoxVer.Name = "m_GroupBoxVer";
            this.m_GroupBoxVer.PaintGroupBox = false;
            this.m_GroupBoxVer.RoundCorners = 10;
            this.m_GroupBoxVer.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxVer.ShadowControl = false;
            this.m_GroupBoxVer.ShadowThickness = 3;
            this.m_GroupBoxVer.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxVer.TitileGradientColors = style2;
            this.m_GroupBoxVer.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxVer.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_TextBoxVerSample
            // 
            resources.ApplyResources(this.m_TextBoxVerSample, "m_TextBoxVerSample");
            this.m_TextBoxVerSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxVerSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxVerSample.Name = "m_TextBoxVerSample";
            this.m_TextBoxVerSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_TextBoxVerSample.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_TextBoxVerSample.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBoxVerSample.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_LabelVerValue
            // 
            resources.ApplyResources(this.m_LabelVerValue, "m_LabelVerValue");
            this.m_LabelVerValue.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerValue.Name = "m_LabelVerValue";
            // 
            // m_LabelVerHead
            // 
            resources.ApplyResources(this.m_LabelVerHead, "m_LabelVerHead");
            this.m_LabelVerHead.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerHead.Name = "m_LabelVerHead";
            // 
            // m_LabelVerSample
            // 
            resources.ApplyResources(this.m_LabelVerSample, "m_LabelVerSample");
            this.m_LabelVerSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerSample.Name = "m_LabelVerSample";
            // 
            // m_GroupBoxStep
            // 
            this.m_GroupBoxStep.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxStep.BorderThickness = 1F;
            this.m_GroupBoxStep.Controls.Add(this.m_ButtonCalculate);
            this.m_GroupBoxStep.Controls.Add(this.m_LabelPixel);
            this.m_GroupBoxStep.Controls.Add(this.m_NumericUpDownRevise);
            this.m_GroupBoxStep.Controls.Add(this.m_ComboBoxPass);
            this.m_GroupBoxStep.Controls.Add(this.m_TextBoxStepSample);
            this.m_GroupBoxStep.Controls.Add(this.m_LabelStep);
            this.m_GroupBoxStep.Controls.Add(this.m_TextBoxBaseStep);
            this.m_GroupBoxStep.Controls.Add(this.m_ButtonPrecision);
            this.m_GroupBoxStep.Controls.Add(this.m_LabelBase);
            resources.ApplyResources(this.m_GroupBoxStep, "m_GroupBoxStep");
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxStep.GradientColors = style3;
            this.m_GroupBoxStep.GroupImage = null;
            this.m_GroupBoxStep.Name = "m_GroupBoxStep";
            this.m_GroupBoxStep.PaintGroupBox = false;
            this.m_GroupBoxStep.RoundCorners = 10;
            this.m_GroupBoxStep.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxStep.ShadowControl = false;
            this.m_GroupBoxStep.ShadowThickness = 3;
            this.m_GroupBoxStep.TabStop = false;
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxStep.TitileGradientColors = style4;
            this.m_GroupBoxStep.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxStep.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_ButtonCalculate
            // 
            resources.ApplyResources(this.m_ButtonCalculate, "m_ButtonCalculate");
            this.m_ButtonCalculate.Name = "m_ButtonCalculate";
            this.m_ButtonCalculate.Click += new System.EventHandler(this.m_ButtonCalculate_Click);
            // 
            // m_LabelPixel
            // 
            resources.ApplyResources(this.m_LabelPixel, "m_LabelPixel");
            this.m_LabelPixel.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelPixel.Name = "m_LabelPixel";
            // 
            // m_NumericUpDownRevise
            // 
            this.m_NumericUpDownRevise.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumericUpDownRevise, "m_NumericUpDownRevise");
            this.m_NumericUpDownRevise.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownRevise.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownRevise.Name = "m_NumericUpDownRevise";
            this.m_NumericUpDownRevise.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxPass
            // 
            this.m_ComboBoxPass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPass, "m_ComboBoxPass");
            this.m_ComboBoxPass.Name = "m_ComboBoxPass";
            this.m_ComboBoxPass.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxPass_SelectedIndexChanged);
            // 
            // m_TextBoxStepSample
            // 
            resources.ApplyResources(this.m_TextBoxStepSample, "m_TextBoxStepSample");
            this.m_TextBoxStepSample.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_TextBoxStepSample.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.m_TextBoxStepSample.Name = "m_TextBoxStepSample";
            this.m_TextBoxStepSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_TextBoxStepSample.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_TextBoxStepSample.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBoxStepSample.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_LabelStep
            // 
            resources.ApplyResources(this.m_LabelStep, "m_LabelStep");
            this.m_LabelStep.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStep.Name = "m_LabelStep";
            // 
            // m_TextBoxBaseStep
            // 
            resources.ApplyResources(this.m_TextBoxBaseStep, "m_TextBoxBaseStep");
            this.m_TextBoxBaseStep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_TextBoxBaseStep.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_TextBoxBaseStep.Name = "m_TextBoxBaseStep";
            this.m_TextBoxBaseStep.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_TextBoxBaseStep.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_TextBoxBaseStep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBoxBaseStep.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_ButtonPrecision
            // 
            resources.ApplyResources(this.m_ButtonPrecision, "m_ButtonPrecision");
            this.m_ButtonPrecision.Name = "m_ButtonPrecision";
            this.m_ButtonPrecision.Click += new System.EventHandler(this.m_ButtonPrecision_Click);
            // 
            // m_LabelBase
            // 
            resources.ApplyResources(this.m_LabelBase, "m_LabelBase");
            this.m_LabelBase.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelBase.Name = "m_LabelBase";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // grouperOverLap
            // 
            this.grouperOverLap.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperOverLap.BorderThickness = 1F;
            this.grouperOverLap.Controls.Add(this.textBoxOverLapSample);
            this.grouperOverLap.Controls.Add(this.label3);
            this.grouperOverLap.Controls.Add(this.label4);
            resources.ApplyResources(this.grouperOverLap, "grouperOverLap");
            style5.Color1 = System.Drawing.Color.LightBlue;
            style5.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperOverLap.GradientColors = style5;
            this.grouperOverLap.GroupImage = null;
            this.grouperOverLap.Name = "grouperOverLap";
            this.grouperOverLap.PaintGroupBox = false;
            this.grouperOverLap.RoundCorners = 10;
            this.grouperOverLap.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperOverLap.ShadowControl = false;
            this.grouperOverLap.ShadowThickness = 3;
            this.grouperOverLap.TabStop = false;
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperOverLap.TitileGradientColors = style6;
            this.grouperOverLap.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperOverLap.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // textBoxOverLapSample
            // 
            resources.ApplyResources(this.textBoxOverLapSample, "textBoxOverLapSample");
            this.textBoxOverLapSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxOverLapSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.textBoxOverLapSample.Name = "textBoxOverLapSample";
            this.textBoxOverLapSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // grouperOrigin
            // 
            this.grouperOrigin.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperOrigin.BorderThickness = 1F;
            this.grouperOrigin.Controls.Add(this.label8);
            this.grouperOrigin.Controls.Add(this.label7);
            this.grouperOrigin.Controls.Add(this.label6);
            this.grouperOrigin.Controls.Add(this.label5);
            this.grouperOrigin.Controls.Add(this.textBoxCustomSpeed);
            this.grouperOrigin.Controls.Add(this.textBoxLowSpeed);
            this.grouperOrigin.Controls.Add(this.textBoxMidSpeed);
            this.grouperOrigin.Controls.Add(this.m_ComboBoxResX);
            this.grouperOrigin.Controls.Add(this.label2);
            this.grouperOrigin.Controls.Add(this.textBoxHightSpeed);
            resources.ApplyResources(this.grouperOrigin, "grouperOrigin");
            style7.Color1 = System.Drawing.Color.LightBlue;
            style7.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperOrigin.GradientColors = style7;
            this.grouperOrigin.GroupImage = null;
            this.grouperOrigin.Name = "grouperOrigin";
            this.grouperOrigin.PaintGroupBox = false;
            this.grouperOrigin.RoundCorners = 10;
            this.grouperOrigin.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperOrigin.ShadowControl = false;
            this.grouperOrigin.ShadowThickness = 3;
            this.grouperOrigin.TabStop = false;
            style8.Color1 = System.Drawing.Color.LightBlue;
            style8.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperOrigin.TitileGradientColors = style8;
            this.grouperOrigin.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperOrigin.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // textBoxCustomSpeed
            // 
            resources.ApplyResources(this.textBoxCustomSpeed, "textBoxCustomSpeed");
            this.textBoxCustomSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxCustomSpeed.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.textBoxCustomSpeed.Name = "textBoxCustomSpeed";
            this.textBoxCustomSpeed.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // textBoxLowSpeed
            // 
            resources.ApplyResources(this.textBoxLowSpeed, "textBoxLowSpeed");
            this.textBoxLowSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxLowSpeed.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.textBoxLowSpeed.Name = "textBoxLowSpeed";
            this.textBoxLowSpeed.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // textBoxMidSpeed
            // 
            resources.ApplyResources(this.textBoxMidSpeed, "textBoxMidSpeed");
            this.textBoxMidSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxMidSpeed.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.textBoxMidSpeed.Name = "textBoxMidSpeed";
            this.textBoxMidSpeed.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_ComboBoxResX
            // 
            this.m_ComboBoxResX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxResX, "m_ComboBoxResX");
            this.m_ComboBoxResX.Name = "m_ComboBoxResX";
            this.m_ComboBoxResX.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxResX_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // textBoxHightSpeed
            // 
            resources.ApplyResources(this.textBoxHightSpeed, "textBoxHightSpeed");
            this.textBoxHightSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.textBoxHightSpeed.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.textBoxHightSpeed.Name = "textBoxHightSpeed";
            this.textBoxHightSpeed.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // grouperGroup
            // 
            this.grouperGroup.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperGroup.BorderThickness = 1F;
            this.grouperGroup.Controls.Add(this.m_TextBoxGroupRightSample);
            this.grouperGroup.Controls.Add(this.labelGroupRight);
            this.grouperGroup.Controls.Add(this.m_TextBoxGroupLeftSample);
            this.grouperGroup.Controls.Add(this.labelGroupLeft);
            this.grouperGroup.Controls.Add(this.labelGroup);
            this.grouperGroup.Controls.Add(this.m_LabelGroupSample);
            resources.ApplyResources(this.grouperGroup, "grouperGroup");
            style9.Color1 = System.Drawing.Color.LightBlue;
            style9.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperGroup.GradientColors = style9;
            this.grouperGroup.GroupImage = null;
            this.grouperGroup.Name = "grouperGroup";
            this.grouperGroup.PaintGroupBox = false;
            this.grouperGroup.RoundCorners = 10;
            this.grouperGroup.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperGroup.ShadowControl = false;
            this.grouperGroup.ShadowThickness = 3;
            this.grouperGroup.TabStop = false;
            style10.Color1 = System.Drawing.Color.LightBlue;
            style10.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperGroup.TitileGradientColors = style10;
            this.grouperGroup.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperGroup.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_TextBoxGroupRightSample
            // 
            resources.ApplyResources(this.m_TextBoxGroupRightSample, "m_TextBoxGroupRightSample");
            this.m_TextBoxGroupRightSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxGroupRightSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxGroupRightSample.Name = "m_TextBoxGroupRightSample";
            this.m_TextBoxGroupRightSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelGroupRight
            // 
            resources.ApplyResources(this.labelGroupRight, "labelGroupRight");
            this.labelGroupRight.BackColor = System.Drawing.Color.Transparent;
            this.labelGroupRight.Name = "labelGroupRight";
            // 
            // m_TextBoxGroupLeftSample
            // 
            resources.ApplyResources(this.m_TextBoxGroupLeftSample, "m_TextBoxGroupLeftSample");
            this.m_TextBoxGroupLeftSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxGroupLeftSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxGroupLeftSample.Name = "m_TextBoxGroupLeftSample";
            this.m_TextBoxGroupLeftSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelGroupLeft
            // 
            resources.ApplyResources(this.labelGroupLeft, "labelGroupLeft");
            this.labelGroupLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelGroupLeft.Name = "labelGroupLeft";
            // 
            // labelGroup
            // 
            resources.ApplyResources(this.labelGroup, "labelGroup");
            this.labelGroup.BackColor = System.Drawing.Color.Transparent;
            this.labelGroup.Name = "labelGroup";
            // 
            // m_LabelGroupSample
            // 
            resources.ApplyResources(this.m_LabelGroupSample, "m_LabelGroupSample");
            this.m_LabelGroupSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelGroupSample.Name = "m_LabelGroupSample";
            // 
            // m_NumUDBidirection
            // 
            resources.ApplyResources(this.m_NumUDBidirection, "m_NumUDBidirection");
            this.m_NumUDBidirection.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.m_NumUDBidirection.Minimum = new decimal(new int[] {
            200000,
            0,
            0,
            -2147483648});
            this.m_NumUDBidirection.Name = "m_NumUDBidirection";
            this.m_NumUDBidirection.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_NumUDBidirection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_NumUDBidirection.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_TextBoxRightSample
            // 
            resources.ApplyResources(this.m_TextBoxRightSample, "m_TextBoxRightSample");
            this.m_TextBoxRightSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxRightSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxRightSample.Name = "m_TextBoxRightSample";
            this.m_TextBoxRightSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_TextBoxRightSample.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_TextBoxRightSample.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBoxRightSample.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // m_LabelHorSample
            // 
            resources.ApplyResources(this.m_LabelHorSample, "m_LabelHorSample");
            this.m_LabelHorSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHorSample.Name = "m_LabelHorSample";
            // 
            // m_LabelHead
            // 
            resources.ApplyResources(this.m_LabelHead, "m_LabelHead");
            this.m_LabelHead.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHead.Name = "m_LabelHead";
            // 
            // m_LabelRight
            // 
            resources.ApplyResources(this.m_LabelRight, "m_LabelRight");
            this.m_LabelRight.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelRight.Name = "m_LabelRight";
            // 
            // m_LabelLeft
            // 
            resources.ApplyResources(this.m_LabelLeft, "m_LabelLeft");
            this.m_LabelLeft.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLeft.Name = "m_LabelLeft";
            // 
            // m_LabelStripeWidth
            // 
            resources.ApplyResources(this.m_LabelStripeWidth, "m_LabelStripeWidth");
            this.m_LabelStripeWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeWidth.Name = "m_LabelStripeWidth";
            // 
            // m_TextBoxLeftSample
            // 
            resources.ApplyResources(this.m_TextBoxLeftSample, "m_TextBoxLeftSample");
            this.m_TextBoxLeftSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxLeftSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxLeftSample.Name = "m_TextBoxLeftSample";
            this.m_TextBoxLeftSample.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_TextBoxLeftSample.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_TextBoxLeftSample.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBoxLeftSample.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            // 
            // checkBoxQuickMode
            // 
            resources.ApplyResources(this.checkBoxQuickMode, "checkBoxQuickMode");
            this.checkBoxQuickMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxQuickMode.ForeColor = System.Drawing.Color.White;
            this.checkBoxQuickMode.Name = "checkBoxQuickMode";
            this.checkBoxQuickMode.UseVisualStyleBackColor = false;
            this.checkBoxQuickMode.CheckedChanged += new System.EventHandler(this.checkBoxQuickMode_CheckedChanged);
            // 
            // labelColor
            // 
            resources.ApplyResources(this.labelColor, "labelColor");
            this.labelColor.BackColor = System.Drawing.Color.Transparent;
            this.labelColor.Name = "labelColor";
            // 
            // comboBoxColor
            // 
            this.comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColor.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxColor, "comboBoxColor");
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxColor_SelectedIndexChanged);
            // 
            // comboBoxCaliMode
            // 
            this.comboBoxCaliMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCaliMode.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCaliMode, "comboBoxCaliMode");
            this.comboBoxCaliMode.Name = "comboBoxCaliMode";
            this.comboBoxCaliMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCaliMode_SelectedIndexChanged);
            // 
            // labelLineWidth
            // 
            resources.ApplyResources(this.labelLineWidth, "labelLineWidth");
            this.labelLineWidth.BackColor = System.Drawing.Color.Transparent;
            this.labelLineWidth.Name = "labelLineWidth";
            // 
            // numericUpDownLineWidth
            // 
            resources.ApplyResources(this.numericUpDownLineWidth, "numericUpDownLineWidth");
            this.numericUpDownLineWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownLineWidth.Name = "numericUpDownLineWidth";
            // 
            // m_GroupBoxHor
            // 
            this.m_GroupBoxHor.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxHor.BorderThickness = 1F;
            this.m_GroupBoxHor.Controls.Add(this.numericUpDownLineWidth);
            this.m_GroupBoxHor.Controls.Add(this.labelLineWidth);
            this.m_GroupBoxHor.Controls.Add(this.comboBoxCaliMode);
            this.m_GroupBoxHor.Controls.Add(this.comboBoxColor);
            this.m_GroupBoxHor.Controls.Add(this.labelColor);
            this.m_GroupBoxHor.Controls.Add(this.checkBoxQuickMode);
            this.m_GroupBoxHor.Controls.Add(this.m_TextBoxLeftSample);
            this.m_GroupBoxHor.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxHor.Controls.Add(this.m_LabelLeft);
            this.m_GroupBoxHor.Controls.Add(this.m_LabelRight);
            this.m_GroupBoxHor.Controls.Add(this.m_LabelHead);
            this.m_GroupBoxHor.Controls.Add(this.m_LabelHorSample);
            this.m_GroupBoxHor.Controls.Add(this.m_TextBoxRightSample);
            this.m_GroupBoxHor.Controls.Add(this.m_NumUDBidirection);
            resources.ApplyResources(this.m_GroupBoxHor, "m_GroupBoxHor");
            style11.Color1 = System.Drawing.Color.LightBlue;
            style11.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxHor.GradientColors = style11;
            this.m_GroupBoxHor.GroupImage = null;
            this.m_GroupBoxHor.Name = "m_GroupBoxHor";
            this.m_GroupBoxHor.PaintGroupBox = false;
            this.m_GroupBoxHor.RoundCorners = 10;
            this.m_GroupBoxHor.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxHor.ShadowControl = false;
            this.m_GroupBoxHor.ShadowThickness = 3;
            this.m_GroupBoxHor.TabStop = false;
            style12.Color1 = System.Drawing.Color.LightBlue;
            style12.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxHor.TitileGradientColors = style12;
            this.m_GroupBoxHor.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxHor.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_ComboBoxSpeed);
            this.panel1.Controls.Add(this.buttonAutoCopy);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // CalibrationSetting_Layout
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.grouperOrigin);
            this.Controls.Add(this.m_GroupBoxStep);
            this.Controls.Add(this.grouperOverLap);
            this.Controls.Add(this.m_GroupBoxVer);
            this.Controls.Add(this.m_GroupBoxHor);
            this.Controls.Add(this.grouperGroup);
            this.Controls.Add(this.panel1);
            this.Name = "CalibrationSetting_Layout";
            this.Load += new System.EventHandler(this.CalibrationSetting_Load);
            this.m_GroupBoxVer.ResumeLayout(false);
            this.m_GroupBoxVer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxVerSample)).EndInit();
            this.m_GroupBoxStep.ResumeLayout(false);
            this.m_GroupBoxStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownRevise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxStepSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxBaseStep)).EndInit();
            this.grouperOverLap.ResumeLayout(false);
            this.grouperOverLap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOverLapSample)).EndInit();
            this.grouperOrigin.ResumeLayout(false);
            this.grouperOrigin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxCustomSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxLowSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMidSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxHightSpeed)).EndInit();
            this.grouperGroup.ResumeLayout(false);
            this.grouperGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxGroupRightSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxGroupLeftSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumUDBidirection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxRightSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxLeftSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).EndInit();
            this.m_GroupBoxHor.ResumeLayout(false);
            this.m_GroupBoxHor.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///
        Dictionary<int, List<int>> yutaiResMap = new Dictionary<int, List<int>>();
        List<int> m_MapedResList = new List<int>();
        private SPrinterSetting _printerSetting;
        private List<CalibrationSubStep> caliSubStepSupport;

        private ReadOnlyCollection<CalibrationSubStep> supportList;

        /// <summary>
        /// 校准步骤支持字典
        /// </summary>
        public ReadOnlyCollection<CalibrationSubStep> CaliSubStepSupport
        {
            get
            {
                if (supportList == null)
                {
                    supportList = new ReadOnlyCollection<CalibrationSubStep>(caliSubStepSupport);
                }
                return supportList;
            }
        }

        void initialSupportCaliSubSteps()
        {
            caliSubStepSupport = new List<CalibrationSubStep>();
            foreach (CalibrationSubStep item in Enum.GetValues(typeof(CalibrationSubStep)))
            {
                caliSubStepSupport.Add(item);
            }
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_HorColumnNum = CoreInterface.GetMaxColumnNum();
            if (m_HorColumnNum < 1) m_HorColumnNum = 1;
            m_GroupNum = CoreInterface.GetCaliGroupNum();
            m_RealGroupNum = sp.nHeadNumPerGroupY;

            m_rsPrinterPropery = sp;
            //m_ColorNum = sp.nColorNum;
            m_ColorNum = CoreInterface.GetLayoutColorNum();
            m_HeadNum = NewLayoutFun.GetLineNum();
            m_Yinterleave = CoreInterface.GetYinterleavePerRow(0);
            m_ColorNumAndColumnNum = m_ColorNum * m_HorColumnNum;

            //if(CheckComponentChange(sp))
            if (true)
            {
                m_nResList = new int[sp.nResNum];
                int len = sp.nResNum;
                CoreInterface.GetResXList(m_nResList, ref len);
                m_ComboBoxSpeed.Items.Clear();
                m_ComboBoxResX.Items.Clear();
                this.contextMenu1.MenuItems.Clear();
#if !YUTAI_RES_MAP
                for (int i = 0; i < sp.nResNum; i++)
                {
                    string res_mode = (m_nResList[i]).ToString() + "DPI";
                    m_ComboBoxResX.Items.Add(res_mode);

                    m_nDisplaySpeedNum = 0;
                    foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
                    {
                        if (mode == SpeedEnum.CustomSpeed)
                        {
                            if (!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
                                continue;
                        }
                        string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                        if (SPrinterProperty.IsY1Epson(sp.ePrinterHead))
                            cmode = "VSD_" + ((int)mode + 1).ToString();
                        m_ComboBoxSpeed.Items.Add(cmode + "_" + res_mode);
                        this.contextMenu1.MenuItems.Add(cmode + "_" + res_mode, new EventHandler(this.contextMenu1Item_Click));
                        m_nDisplaySpeedNum++;
                    }
                }
#else
                yutaiResMap = new Dictionary<int, List<int>>();
                for (int i = 0; i < m_nResList.Length; i++)
                {
                    switch (m_nResList[i])
                    {
                        case 423:
                            yutaiResMap.Add(m_nResList[i], new List<int>() { 846 });
                            break;
                        case 362:
                            yutaiResMap.Add(m_nResList[i], new List<int>() { 362 });
                            break;
                        case 317:
                            yutaiResMap.Add(m_nResList[i], new List<int>() { 1270, 635 });
                            break;
                        default:
                            yutaiResMap.Add(m_nResList[i], new List<int>() { m_nResList[i] });
                            break;
                    }
                }
                List<int>  resList = new List<int>();
                foreach (KeyValuePair<int, List<int>> keyValuePair in yutaiResMap)
                {
                    resList.AddRange(keyValuePair.Value);
                }
                resList.Sort();
                resList.Reverse();
                m_MapedResList = resList;
                for (int i = 0; i < m_MapedResList.Count; i++)
                {
                    string res_mode = (m_MapedResList[i]).ToString() + "DPI";

                    m_nDisplaySpeedNum = 0;
                    foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
                    {
                        if (mode == SpeedEnum.CustomSpeed)
                        {
                            if (!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
                                continue;
                        }
                        string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                        if (SPrinterProperty.IsEpson(sp.ePrinterHead))
                            cmode = "VSD_" + ((int)mode + 1).ToString();
                        m_ComboBoxSpeed.Items.Add(cmode + "_" + res_mode);
                        this.contextMenu1.MenuItems.Add(cmode + "_" + res_mode, new EventHandler(this.contextMenu1Item_Click));
                        m_nDisplaySpeedNum++;
                    }
                }
#endif
                bNeedLoadPrinterModeFromXML =
                    (sp.ePrinterHead == PrinterHeadEnum.Konica_KM512M_14pl
                    || sp.ePrinterHead == PrinterHeadEnum.Konica_KM512_SH_4pl
                    || sp.ePrinterHead == PrinterHeadEnum.Konica_KM512MAX_14pl
                    || sp.ePrinterHead == PrinterHeadEnum.Konica_KM1024M_14pl
                    || sp.ePrinterHead == PrinterHeadEnum.Konica_KM1024S_6pl
                    || sp.ePrinterHead == PrinterHeadEnum.Konica_KM3688_6pl

                    )
                    && sp.bSupportUV;
                if (bNeedLoadPrinterModeFromXML)
                {
                    DataTable source = this.ReadPrintModeListFromXML(sp);
                    if (source != null)
                    {
                        this.m_ComboBoxSpeed.DisplayMember = source.Columns[0].ColumnName;
                        this.m_ComboBoxSpeed.ValueMember = source.Columns[1].ColumnName;
                        this.m_ComboBoxSpeed.DataSource = source;
                        this.contextMenu1.MenuItems.Clear();
                        for (int i = 0; i < source.Rows.Count; i++)
                            this.contextMenu1.MenuItems.Add(source.Rows[i][0].ToString(), new EventHandler(this.contextMenu1Item_Click));
                    }
                }

                m_nBaseResX = sp.nResX;

                int PassListNum = sp.nPassListNum;
                m_PassListNum = PassListNum;
                ReCalcPanelSize(m_HeadNum);
                m_ComboBoxPass.Items.Clear();
                //				for(int i = 0;i <PassListNum; i++)
                //				{
                //					int passNum = PassList[i];
                //					string dispPass = PassList[i].ToString() + " " + ResString.GetDisplayPass();
                //					m_ComboBoxPass.Items.Add(dispPass);
                //				}
                string spass = ResString.GetDisplayPass();
                for (int i = 0; i < CoreConst.MAX_PASS_NUM; i++)
                {
                    //int passNum = PassList[i];
                    string dispPass = (i + 1).ToString() + " " + spass;
                    m_ComboBoxPass.Items.Add(dispPass);
                }
                if (m_RealGroupNum == 1)
                {
                    this.grouperOverLap.Visible = false;
                    caliSubStepSupport.Remove(CalibrationSubStep.Overlap);
                    this.grouperOrigin.Location = grouperOverLap.Location;
                }

                //if (!SPrinterProperty.IsHanTuo() || m_rsPrinterPropery.nHeadNumPerGroupY == 1)
                //{
                //    IsUseGroup = false;
                //    grouperGroup.Visible = false;
                //    caliSubStepSupport.Remove(CalibrationSubStep.GroupLeft);
                //    caliSubStepSupport.Remove(CalibrationSubStep.GroupRight);
                //}
                //else
                //{
                //    IsUseGroup = true;
                //    int groupHeight = grouperGroup.Height;
                //    grouperGroup.Visible = true;
                //    grouperGroup.Location = m_GroupBoxHor.Location;

                //    m_GroupBoxHor.Location = new Point(m_GroupBoxHor.Location.X,m_GroupBoxHor.Location.Y + groupHeight + 5);
                //    m_GroupBoxStep.Location = new Point(m_GroupBoxStep.Location.X, m_GroupBoxStep.Location.Y + groupHeight + 5);
                //    m_GroupBoxVer.Location = new Point(m_GroupBoxVer.Location.X, m_GroupBoxVer.Location.Y + groupHeight + 5);
                //    grouperOverLap.Location = new Point(grouperOverLap.Location.X, grouperOverLap.Location.Y + groupHeight + 5);
                //    grouperOrigin.Location = new Point(grouperOrigin.Location.X, grouperOrigin.Location.Y + groupHeight + 5);

                //    //m_ComboBoxSpeed.Location = new Point(m_ComboBoxSpeed.Location.X, grouperGroup.Location.Y);
                //    //buttonAutoCopy.Location = new Point(buttonAutoCopy.Location.X, grouperGroup.Location.Y);
                //    //comboBoxCaliMode.Location = new Point(comboBoxCaliMode.Location.X, grouperGroup.Location.Y);
                //}

                comboBoxColor.Items.Clear();
                //int colorcount = sp.nColorNum - ((sp.nWhiteInkNum & 0x0F) + (sp.nWhiteInkNum >> 4));
                //for (int i = 0; i < colorcount; i++)
                //{
                //    ColorEnum color = (ColorEnum)sp.eColorOrder[i];
                //    string cmode = ResString.GetEnumDisplayName(typeof(ColorEnum), color);
                //    comboBoxColor.Items.Add(cmode);
                //}
                for (int i = 0; i < m_ColorNum; i++)
                {

                    int colorID = CoreInterface.GetLayoutColorID(i);
                    string cmode = NewLayoutFun.GetColorName(colorID);
                    if (!String.IsNullOrEmpty(cmode) && (cmode.IndexOf("W") < 0 && cmode.IndexOf("V") < 0))
                        comboBoxColor.Items.Add(cmode);
                }




                //comboBoxCaliMode.Items.Clear();
                //foreach (HorizontalCalibrationMode mode in Enum.GetValues(typeof(HorizontalCalibrationMode)))
                //{
                //    string cmode = ResString.GetEnumDisplayName(typeof(HorizontalCalibrationMode),mode);
                //    if (mode > HorizontalCalibrationMode.Full)
                //    {
                //        if (IsUseGroup)
                //        {
                //            comboBoxCaliMode.Items.Add(cmode);
                //        }
                //    }
                //    else
                //    {
                //        comboBoxCaliMode.Items.Add(cmode);
                //    }
                //}


                ClearComponent();
                CreateComponent();
                LayoutComponent();
                AppendComponent();

                if (comboBoxColor.Items.Count > 0)
                    comboBoxColor.SelectedIndex = 0;
                this.isDirty = false;
            }
        }
        public void OnPrinterSettingChange(AllParam ss)
        {
            _printerSetting = ss.PrinterSetting;

            if (ss.PrinterSetting.sExtensionSetting.bIsNewCalibration == 1) //如果是组校准，不显示水平校准模式
            {
                IsUseGroup = true;
                grouperGroup.Visible = true;
                grouperGroup.Location = m_GroupBoxHor.Location;
            }
            else
            {
                grouperGroup.Visible = false;
                IsUseGroup = false;
                caliSubStepSupport.Remove(CalibrationSubStep.GroupLeft);
                caliSubStepSupport.Remove(CalibrationSubStep.GroupRight);
            }

            DataTable dtCalibrationMode = new DataTable();
            dtCalibrationMode.Columns.Add("Value", typeof(int));
            dtCalibrationMode.Columns.Add("Name", typeof(string));
            foreach (HorizontalCalibrationMode mode in Enum.GetValues(typeof(HorizontalCalibrationMode)))
            {
                if (IsUseGroup)
                {
                    if (mode < HorizontalCalibrationMode.GroupQuick)
                        continue;
                }
                else
                {
                    if (mode > HorizontalCalibrationMode.Full)
                        continue;
                }

                int cvalue = (int)mode;
                string cname = ResString.GetEnumDisplayName(typeof(HorizontalCalibrationMode), mode);

                DataRow dr = dtCalibrationMode.NewRow();
                dr[0] = cvalue;
                dr[1] = cname;
                dtCalibrationMode.Rows.Add(dr);
            }
            comboBoxCaliMode.ValueMember = "Value";
            comboBoxCaliMode.DisplayMember = "Name";
            comboBoxCaliMode.DataSource = dtCalibrationMode;

            //m_PassSelectIndex = FoundMatchPass(ss.sFrequencySetting.nPass + " " + ResString.GetDisplayPass());
            //m_SpeedSelectIndex = (int)ss.sFrequencySetting.nSpeed;
            if (ss.PrinterProperty.EPSONLCD_DEFINED)
                EpsonLCD.GetCalibrationSetting(ref m_sCalibrationSetting, true);
            else
            {
                m_sCalibrationSetting = ss.PrinterSetting.sCalibrationSetting;
                m_NewHorizonArrayUI = ss.NewCalibrationHorizonArray;
                m_CalibrationGroupUILeft = ss.CalibrationGroupUILeft;
                m_CalibrationGroupUIRight = ss.CalibrationGroupUIRight;
            }
            numericUpDownLineWidth.Value = ss.PrinterSetting.sExtensionSetting.LineWidth;

            UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPass, FoundMatchPass(ss.PrinterSetting.sFrequencySetting.nPass + " " + ResString.GetDisplayPass()));
            UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxSpeed, (int)ss.PrinterSetting.sFrequencySetting.nSpeed);
            UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxResX, 0);
            m_CurPassSelectIndex = m_ComboBoxPass.SelectedIndex;

            m_TextBoxBaseStep.Text = m_sCalibrationSetting.nStepPerHead.ToString();
            m_TextBoxStepSample.Text = m_sCalibrationSetting.nPassStepArray[m_ComboBoxPass.SelectedIndex].ToString();
            //			for (int i = 0; i<m_PassListNum;i++)
            //			{
            //				m_TextBoxStep[i].Text = m_sCalibrationSetting.nPassStepArray[m_PassList[i]-1].ToString();
            //			}
            OnPrinterSettingChangeSpeed(false);
            this.isDirty = false;

        }
        public void OnGetPrinterSetting(AllParam ss)
        {
            RenewStepValue();
            UpdateToLocal();
            ss.PrinterSetting.sCalibrationSetting = m_sCalibrationSetting;
            ss.NozzleOverlap = SynchCali(ss);
            ss.PrinterSetting.sExtensionSetting.OriginCaliValue = _printerSetting.sExtensionSetting.OriginCaliValue;
            ss.PrinterSetting.sExtensionSetting.LineWidth = (byte)numericUpDownLineWidth.Value;
            ss.NewCalibrationHorizonArray = m_NewHorizonArrayUI;
            ss.CalibrationGroupUILeft = m_CalibrationGroupUILeft;
            ss.CalibrationGroupUIRight = m_CalibrationGroupUIRight;
        }

        private SNozzleOverlap SynchCali(AllParam ss)
        {
            SNozzleOverlap n = ss.NozzleOverlap;
            int rowNum = CoreInterface.GetRowNum();
            if (rowNum > 1) rowNum--;
            int MaxColorNum = 32;//SNozzleOverlap结构体为32色*32组
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    n.OverLapTotalNozzle[j * MaxColorNum + i] = (ushort)(ss.PrinterSetting.sCalibrationSetting.nVerticalArray[(j + 1) * m_ColorNumAndColumnNum + i]);
                }
            }
            return n;
        }
        private void OnPreferenceChange(UIPreference up)
        {
            this.isDirty = false;
        }
        public int GetPass()
        {
            return m_ComboBoxPass.SelectedIndex + 1;
            //return m_PassList[m_ComboBoxPass.SelectedIndex];
        }
        public int GetSpeed()
        {
            if (bNeedLoadPrinterModeFromXML)
            {
                try
                {
                    string strvalue = m_ComboBoxSpeed.SelectedValue.ToString();
                    string[] vals = strvalue.Split(new char[] { ',' });
                    SpeedEnum curSpeed = (SpeedEnum)Enum.Parse(typeof(SpeedEnum), vals[1], true); ;
                    return (int)curSpeed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            else
                return m_ComboBoxSpeed.SelectedIndex % m_nDisplaySpeedNum;
        }
        public int GetResIndex()
        {
            //if (m_SubStep != CalibrationSubStep.Origin)
            {
                if (bNeedLoadPrinterModeFromXML)
                {
                    try
                    {
                        string strvalue = m_ComboBoxSpeed.SelectedValue.ToString();
                        string[] vals = strvalue.Split(new char[] { ',' });
                        return int.Parse(vals[0]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return 0;
                    }
                }
                return m_nResList[(m_ComboBoxSpeed.SelectedIndex / m_nDisplaySpeedNum)];
            }
            return m_nResList[(m_ComboBoxResX.SelectedIndex)];
        }

        public int Convert_Speed_SelectedIndex_To_Param_Index(ref int resIndex, ref int speedIndex)
        {
            if (bNeedLoadPrinterModeFromXML)
            {
                try
                {
                    string strvalue = m_ComboBoxSpeed.SelectedValue.ToString();
                    string[] vals = strvalue.Split(new char[] { ',' });
                    int curres = int.Parse(vals[0]);
                    int curSpeed = this.GetSpeed();
                    for (int i = 0; i < this.m_rsPrinterPropery.nResNum; i++)
                    {
                        if (m_nResList[i] == curres)
                        {
                            resIndex = i;
                            return i * CoreConst.MAX_SPEED_NUM + curSpeed;
                        }
                    }
                    speedIndex = curSpeed;
                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            else
            {
#if YUTAI_RES_MAP
                int curres = m_MapedResList[m_ComboBoxSpeed.SelectedIndex / m_nDisplaySpeedNum];
                foreach (KeyValuePair<int, List<int>> keyValuePair in yutaiResMap)
                {
                    bool bfind = false;
                    for (int i = 0; i < keyValuePair.Value.Count; i++ )
                        if (keyValuePair.Value[i] == curres)
                        {
                            curres = keyValuePair.Key;
                            bfind = true;
                        }
                    if (bfind)
                        break;
                }
			    int selectedIndex = -1;
                for (int i = 0; i < this.m_rsPrinterPropery.nResNum; i++)
                {
                    if (m_nResList[i] == curres)
                    {
                        selectedIndex = i * m_nDisplaySpeedNum + m_ComboBoxSpeed.SelectedIndex % m_nDisplaySpeedNum;
                        break;
                    }
                }
#else
                int selectedIndex = m_ComboBoxSpeed.SelectedIndex;
#endif
                resIndex = selectedIndex / m_nDisplaySpeedNum;
                speedIndex = this.GetSpeed();
                return (selectedIndex / m_nDisplaySpeedNum) * CoreConst.MAX_SPEED_NUM
                    + (selectedIndex % m_nDisplaySpeedNum);
            }
        }
        public void SetCalibrationStep(CalibrationSubStep step)
        {
            m_SubStep = step;
#if LIYUUSB
            SetGroupsLocation(step);
			switch(step)
			{
				case CalibrationSubStep.Left:
					m_GroupBoxHor.Visible = true;
                    m_GroupBoxHor.Width = groupWidth;
                    m_LabelHead.Visible = true;
                    m_LabelLeft.Visible = true;
                    m_LabelRight.Visible = false;
                    m_GroupBoxHor.Text =grouptext + ":" + m_LabelLeft.Text;
                    m_LabelLeft.Top = m_LabelStripeWidth.Top;
					for (int i=0; i<m_TextBoxHorLeft.Length;i++)
					{
                        m_LabelHorHeadIndex[i].Visible = true;
                        m_TextBoxHorLeft[i].Visible = true;
                        m_TextBoxHorRight[i].Visible = false;
                        m_TextBoxHorLeft[i].Top = m_NumUDBidirection.Top;
					}
                    m_LabelStripeWidth.Visible = false;
                    m_NumUDBidirection.Visible = false;

                    m_GroupBoxVer.Visible = false;
                    m_GroupBoxStep.Visible = false;
					break;
				case CalibrationSubStep.Right:
                    m_GroupBoxHor.Visible = true;
                    m_GroupBoxHor.Width = groupWidth;
                    m_LabelHead.Visible = true;
                    m_LabelLeft.Visible = false;
                    m_LabelRight.Visible = true;
                    m_GroupBoxHor.Text = grouptext + ":" + m_LabelRight.Text;
                    m_LabelRight.Top = m_LabelStripeWidth.Top;
					for (int i=0; i<m_TextBoxHorLeft.Length;i++)
					{
                        m_LabelHorHeadIndex[i].Visible = true;
                        m_TextBoxHorLeft[i].Visible = false;
                        m_TextBoxHorRight[i].Visible = true;
                        m_TextBoxHorRight[i].Top = m_LabelStripeWidth.Top;
					}
                    m_LabelStripeWidth.Visible = false;
                    m_NumUDBidirection.Visible = false;

                    m_GroupBoxVer.Visible = false;
                    m_GroupBoxStep.Visible = false;
					break;
				case CalibrationSubStep.Bidirection:
                    m_GroupBoxHor.Visible = true;
                    m_GroupBoxHor.Width = this.m_GroupBoxStep.Width;
                    m_LabelHead.Visible = false;
                    m_LabelLeft.Visible = false;
                    m_LabelRight.Visible = false;
                    m_GroupBoxHor.Text = grouptext + ":" + m_LabelStripeWidth.Text;
					for (int i=0; i<m_TextBoxHorLeft.Length;i++)
					{
                        m_LabelHorHeadIndex[i].Visible = false;
                        m_TextBoxHorLeft[i].Visible = false;
                        m_TextBoxHorRight[i].Visible = false;
					}
                    m_LabelStripeWidth.Visible = true;
                    m_NumUDBidirection.Visible = true;
//                    m_NumUDBidirection.Top = m_LabelStripeWidth.Top = m_LabelHead.Top;

                    m_GroupBoxVer.Visible = false;
                    m_GroupBoxStep.Visible = false;
					break;
				case CalibrationSubStep.Vertical:
                    this.m_GroupBoxVer.Top = m_GroupBoxHor.Top;
                    m_GroupBoxHor.Visible = false;
                    m_GroupBoxVer.Visible = true;
                    m_GroupBoxStep.Visible = false;

					break;
				case CalibrationSubStep.Step:
                    this.m_GroupBoxStep.Top = m_GroupBoxHor.Top;
                    m_GroupBoxHor.Visible = false;
                    m_GroupBoxVer.Visible = false;
                    m_GroupBoxStep.Visible = true;

//					for (int i=0;i<m_TextBoxStep.Length;i++)
//					{
//						m_TextBoxStep[i].Enabled = false;
//					}
//					m_TextBoxStep[m_ComboBoxPass.SelectedIndex].Enabled = true;
					break;

				default:
					break;
			}
			this.ScrollControlIntoView(this.label1);
#else
            switch (step)
            {
                case CalibrationSubStep.GroupLeft:
                    m_GroupBoxHor.Enabled = false;
                    m_ComboBoxSpeed.Enabled = true;
                    buttonAutoCopy.Enabled = false;

                    for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
                    {
                        m_TextBoxGroupLeft[i].Enabled = true;
                        m_TextBoxGroupRight[i].Enabled = false;
                    }
                    grouperGroup.Enabled = true;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = false;
                    break;
                case CalibrationSubStep.GroupRight:
                    m_GroupBoxHor.Enabled = false;
                    m_ComboBoxSpeed.Enabled = true;
                    buttonAutoCopy.Enabled = false;

                    for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
                    {
                        m_TextBoxGroupLeft[i].Enabled = false;
                        m_TextBoxGroupRight[i].Enabled = true;
                    }
                    grouperGroup.Enabled = true;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = false;
                    break;
                case CalibrationSubStep.Left:
                    comboBoxCaliMode.Enabled = true;
                    m_GroupBoxHor.Enabled = true;
                    buttonAutoCopy.Enabled = true;
                    for (int i = 0; i < m_TextBoxHorLeft.Length; i++)
                    {
                        m_TextBoxHorLeft[i].Enabled = true;
                        m_TextBoxHorRight[i].Enabled = false;
                    }
                    m_NumUDBidirection.Enabled = false;
                    numericUpDownLineWidth.Enabled = true;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = grouperGroup.Enabled = false;
                    break;
                case CalibrationSubStep.Right:
                    comboBoxCaliMode.Enabled = true;
                    buttonAutoCopy.Enabled = true;
                    m_GroupBoxHor.Enabled = true;
                    for (int i = 0; i < m_TextBoxHorLeft.Length; i++)
                    {
                        m_TextBoxHorLeft[i].Enabled = false;
                        m_TextBoxHorRight[i].Enabled = true;
                    }
                    m_NumUDBidirection.Enabled = false;
                    numericUpDownLineWidth.Enabled = true;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = grouperGroup.Enabled = false;
                    break;
                case CalibrationSubStep.Bidirection:
                    comboBoxCaliMode.Enabled = true;
                    buttonAutoCopy.Enabled = true;
                    m_GroupBoxHor.Enabled = true;
                    for (int i = 0; i < m_TextBoxHorLeft.Length; i++)
                    {
                        m_TextBoxHorLeft[i].Enabled = false;
                        m_TextBoxHorRight[i].Enabled = false;
                    }
                    m_NumUDBidirection.Enabled = true;
                    numericUpDownLineWidth.Enabled = false;
                    m_ButtonPrecision.Visible = false;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = grouperGroup.Enabled = false;
                    break;
                case CalibrationSubStep.Vertical:
                    m_GroupBoxHor.Enabled = false;
                    m_GroupBoxVer.Enabled = true;
                    grouperOverLap.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = grouperGroup.Enabled = false;
                    m_ButtonPrecision.Visible = false;
                    break;
                case CalibrationSubStep.Step:
                    grouperOrigin.Enabled = m_GroupBoxHor.Enabled = false;
                    m_GroupBoxVer.Enabled = false;
                    grouperOverLap.Enabled = false;
                    m_GroupBoxStep.Enabled = true;
                    grouperGroup.Enabled = false;
                    m_ButtonPrecision.Visible = true;
                    //					for (int i=0;i<m_TextBoxStep.Length;i++)
                    //					{
                    //						m_TextBoxStep[i].Enabled = false;
                    //					}
                    //					m_TextBoxStep[m_ComboBoxPass.SelectedIndex].Enabled = true;
                    break;
                case CalibrationSubStep.Overlap:
                    m_GroupBoxHor.Enabled = false;
                    m_GroupBoxVer.Enabled = false;
                    grouperOrigin.Enabled = m_GroupBoxStep.Enabled = false;
                    grouperOverLap.Enabled = true;
                    grouperGroup.Enabled = false;
                    break;
                //case CalibrationSubStep.Origin:
                //    m_GroupBoxHor.Enabled = false;
                //    m_GroupBoxVer.Enabled = false;
                //    m_GroupBoxStep.Enabled = false;
                //    grouperOverLap.Enabled = false;
                //    grouperOrigin.Enabled = true;
                //    break;
                default:
                    break;
            }
#endif
        }

        public CalibrationSubStep GetCalibrationStep()
        {
            return m_SubStep;
        }
        private bool CheckComponentChange(SPrinterProperty sp)
        {
            if (m_HeadNum != sp.nHeadNum)
                return true;
            //int PassListNum;
            //int [] PassList;
            //sp.GetPassListNumber(out PassListNum,out PassList);
            //if(m_PassListNum != PassListNum || m_PassList == null)
            //    return true;
            //for (int i=0;i<m_PassListNum;i++)
            //{
            //    if(m_PassList[i] != PassList[i])
            //        return true;
            //}
            return false;
        }
        private int FoundMatchPass(string dispPass)
        {
            for (int i = 0; i < m_ComboBoxPass.Items.Count; i++)
            {
                if (string.Compare((string)m_ComboBoxPass.Items[i], dispPass) == 0)
                    return i;
            }
            return -1;
        }
        private void ClearComponent()
        {
            if (m_LabelHorHeadIndex == null || m_LabelHorHeadIndex.Length == 0)
                return;

            foreach (Control c in this.m_LabelHorHeadIndex)
            {
                m_GroupBoxHor.Controls.Remove(c);
            }
            foreach (NumericUpDown c in this.m_TextBoxHorLeft)
            {
                c.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                c.Leave -= new System.EventHandler(this.m_CheckBoxControl_Leave);
                c.TextChanged -= new EventHandler(this.m_CheckBox_CheckedChanged);
                m_GroupBoxHor.Controls.Remove(c);
            }
            foreach (NumericUpDown c in this.m_TextBoxHorRight)
            {
                c.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                c.Leave -= new System.EventHandler(this.m_CheckBoxControl_Leave);
                c.TextChanged -= new EventHandler(this.m_CheckBox_CheckedChanged);
                m_GroupBoxHor.Controls.Remove(c);
            }

            foreach (Control c in this.m_LabelVerHeadIndex)
            {
                m_GroupBoxVer.Controls.Remove(c);
            }
            foreach (Control c in this.m_TextBoxVer)
            {
                m_GroupBoxVer.Controls.Remove(c);
            }

            foreach (Control c in this.m_LabelOverLapHeadIndex)
            {
                grouperOverLap.Controls.Remove(c);
            }
            foreach (Control c in this.m_TextBoxOverLap)
            {
                grouperOverLap.Controls.Remove(c);
            }

            foreach (Control c in this.m_LabelGroupHeadIndex)
            {
                grouperGroup.Controls.Remove(c);
            }
            foreach (Control c in this.m_TextBoxGroupLeft)
            {
                grouperGroup.Controls.Remove(c);
            }
            foreach (Control c in this.m_TextBoxGroupRight)
            {
                grouperGroup.Controls.Remove(c);
            }
        }
        private void CreateComponent()
        {
            this.m_LabelHorHeadIndex = new Label[m_HeadNum];
            this.m_TextBoxHorLeft = new NumericUpDown[m_HeadNum];
            this.m_TextBoxHorRight = new NumericUpDown[m_HeadNum];

            ColorIDList = new int[m_HeadNum];

            for (int i = 0; i < m_HeadNum; i++)
            {
                this.m_LabelHorHeadIndex[i] = new Label() { Visible = false };
                this.m_TextBoxHorLeft[i] = new NumericUpDown() { Visible = false, Text = "0" };
                this.m_TextBoxHorRight[i] = new NumericUpDown() { Visible = false, Text = "0" };
            }

            this.m_LabelVerHeadIndex = new Label[m_ColorNumAndColumnNum];
            this.m_TextBoxVer = new NumericUpDown[m_ColorNumAndColumnNum];
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                this.m_LabelVerHeadIndex[i] = new Label();
                this.m_TextBoxVer[i] = new NumericUpDown();
            }

            //			this.m_LabelPassList = new Label[m_PassListNum];
            //			this.m_TextBoxStep   = new TextBox[m_PassListNum];
            //			for(int i = 0; i < m_PassListNum; i ++)
            //			{
            //				this.m_LabelPassList[i] = new Label();
            //				this.m_TextBoxStep[i] = new TextBox();
            //			}

            this.m_LabelOverLapHeadIndex = new Label[m_ColorNumAndColumnNum];
            this.m_TextBoxOverLap = new NumericUpDown[m_ColorNumAndColumnNum * (m_RealGroupNum - 1)];
            for (int i = 0; i < m_LabelOverLapHeadIndex.Length; i++)
            {
                this.m_LabelOverLapHeadIndex[i] = new Label();
            }
            for (int i = 0; i < m_TextBoxOverLap.Length; i++)
            {
                this.m_TextBoxOverLap[i] = new NumericUpDown();
            }

            this.m_LabelGroupHeadIndex = new Label[m_GroupNum];
            this.m_TextBoxGroupLeft = new NumericUpDown[m_GroupNum];
            this.m_TextBoxGroupRight = new NumericUpDown[m_GroupNum];
            for (int i = 0; i < m_LabelGroupHeadIndex.Length; i++)
            {
                this.m_LabelGroupHeadIndex[i] = new Label();
            }
            for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
            {
                this.m_TextBoxGroupLeft[i] = new NumericUpDown();
            }
            for (int i = 0; i < m_TextBoxGroupRight.Length; i++)
            {
                this.m_TextBoxGroupRight[i] = new NumericUpDown();
            }

            this.SuspendLayout();
        }

        private void AppendComponent()
        {
            for (int i = 0; i < m_HeadNum; i++)
            {
                m_GroupBoxHor.Controls.Add(this.m_LabelHorHeadIndex[i]);
                m_GroupBoxHor.Controls.Add(this.m_TextBoxHorLeft[i]);
                m_GroupBoxHor.Controls.Add(this.m_TextBoxHorRight[i]);
            }
            for (int i = 0; i < m_LabelVerHeadIndex.Length; i++)
            {
                m_GroupBoxVer.Controls.Add(this.m_LabelVerHeadIndex[i]);
                m_GroupBoxVer.Controls.Add(this.m_TextBoxVer[i]);
            }
            //			for(int i = 0; i < m_PassListNum; i ++)
            //			{
            //				m_GroupBoxStep.Controls.Add(this.m_LabelPassList[i]); 
            //				m_GroupBoxStep.Controls.Add(this.m_TextBoxStep[i]); 
            //			}
            for (int i = 0; i < m_LabelOverLapHeadIndex.Length; i++)
            {
                grouperOverLap.Controls.Add(this.m_LabelOverLapHeadIndex[i]);
            }
            for (int i = 0; i < m_TextBoxOverLap.Length; i++)
            {
                grouperOverLap.Controls.Add(this.m_TextBoxOverLap[i]);
            }

            for (int i = 0; i < m_LabelGroupHeadIndex.Length; i++)
            {
                grouperGroup.Controls.Add(this.m_LabelGroupHeadIndex[i]);
            }
            for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
            {
                grouperGroup.Controls.Add(this.m_TextBoxGroupLeft[i]);
            }
            for (int i = 0; i < m_TextBoxGroupRight.Length; i++)
            {
                grouperGroup.Controls.Add(this.m_TextBoxGroupRight[i]);
            }

            m_GroupBoxHor.ResumeLayout(false);
            m_GroupBoxVer.ResumeLayout(false);
            m_GroupBoxStep.ResumeLayout(false);
            grouperOverLap.ResumeLayout(false);
            grouperGroup.ResumeLayout(false);

            this.ResumeLayout(false);
        }


        /// <summary>
        /// 水平校准模式（0-全校准，1-颜色校准，2-快速校准）
        /// </summary>
        public int HorCalibrationMode { get; set; }

        /// <summary>
        /// 颜色校准颜色索引（对应PrinterProperty中的ColorOrder）
        /// </summary>
        public int ColorCalibrationIndex { get; set; }

        /// <summary>
        /// 指示是否是快速水平校准
        /// </summary>
        public bool IsQuickHorCalibration
        {
            get;
            set;
        }

        /// <summary>
        /// 快速水平校准显示的前几个喷头排孔数
        /// </summary>
        public int QuickModeShowNum
        {
            get
            {
                bool isMirror = ((m_rsPrinterPropery.bSupportBit1 & (uint)INTBIT.Bit_6) != 0);
                if (isMirror)
                    return m_rsPrinterPropery.GetRealColorNum() * m_rsPrinterPropery.nHeadNumPerGroupY * 2;
                return m_rsPrinterPropery.GetRealColorNum() * m_rsPrinterPropery.nHeadNumPerGroupY;
            }
        }

        /// <summary>
        /// 重算水平groupbox控件大小
        /// </summary>
        private void ReCalcPanelSize(int disHeadNum)
        {
	        int start_x, space_x, width_con;
	        start_x = this.m_TextBoxLeftSample.Left;
	        width_con = this.m_TextBoxLeftSample.Width;
            /////True Layout
            space_x = Math.Max(LayoutHelper.MinSpace, width_con);

            int HorWidth = Math.Max(start_x + (space_x) * disHeadNum, m_GroupDefWidth) + 140;
            int VerWidht = Math.Max(start_x + (70) * m_ColorNumAndColumnNum, m_HorDefWidth) + 140;

            m_GroupBoxHor.Width =
            grouperGroup.Width =
            m_GroupBoxVer.Width =
            m_GroupBoxStep.Width =
            grouperOverLap.Width = Math.Max(HorWidth, VerWidht);

        }

        private void LayoutComponent()
        {
            m_GroupBoxHor.SuspendLayout();
            m_GroupBoxVer.SuspendLayout();
            m_GroupBoxStep.SuspendLayout();
            grouperOverLap.SuspendLayout();
            grouperGroup.SuspendLayout();
            this.SuspendLayout();

            ///True Layout
            ///
            int start_x, start_y, end_x, space_x, width_con;
            start_x = this.m_TextBoxLeftSample.Left;
            start_y = this.m_TextBoxLeftSample.Top;
            end_x = this.m_GroupBoxHor.Width;
            width_con = this.m_TextBoxLeftSample.Width;
            //LayoutHelper.CalculateHorNum(m_HeadNum, start_x, end_x, ref width_con, out space_x);
            space_x = 70;
            int xi = 0;
            int yi = 0;
            for (int i = 0; i < m_HeadNum; i++)
            {
                if (xi >= 16)
                {
                    xi = 0;
                    yi ++;
                }
                Label label = this.m_LabelHorHeadIndex[i];
                ControlClone.LabelClone(label, this.m_LabelHorSample);
                label.Location = new Point(start_x + space_x * xi, this.m_LabelHorSample.Top + yi * 96);
                label.Width = width_con;

                NozzleLineData lineData = new NozzleLineData();
                CoreInterface.GetlineIDtoNozzleline(i + 1, ref lineData);
                string temp = NewLayoutFun.GetColorName((int)lineData.ColorID);
                label.Text = (i + 1).ToString() + "  (" + temp + ")";
                //label.Visible = (!IsQuickHorCalibration) ? true : i >= QuickModeShowNum ? false : true;
                ColorIDList[i] = (int)lineData.ColorID;

                NumericUpDown textBox = this.m_TextBoxHorLeft[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxLeftSample);

                textBox.Location = new Point(start_x + space_x * xi, this.m_TextBoxLeftSample.Top + yi*96);
                textBox.Width = width_con;
                textBox.TabIndex = i;
                textBox.Text = "0";
                //textBox.Visible = (!IsQuickHorCalibration) ? true : i >= QuickModeShowNum ? false : true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);

                textBox = this.m_TextBoxHorRight[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxRightSample);

                textBox.Location = new Point(start_x + space_x * xi, this.m_TextBoxRightSample.Top + yi * 96);
                textBox.Width = width_con;
                textBox.TabIndex = m_HeadNum + i;
                textBox.Text = "0";
                //textBox.Visible = (!IsQuickHorCalibration) ? true : i >= QuickModeShowNum ? false : true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);
                if (label.Visible && textBox.Visible)
                    xi++;
            }
            if (yi != 0)
            {
                m_GroupBoxHor.Height = 145 + yi * 96;
                for (int i = 1; i <= yi; i++)
                {
                    Label lh = new Label();
                    ControlClone.LabelClone(lh, m_LabelHead);
                    lh.Location = new Point(m_LabelHead.Location.X, m_LabelHead.Location.Y + i * 96);
                    lh.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(lh)) m_GroupBoxHor.Controls.Add(lh);
                    Label ll = new Label();
                    ControlClone.LabelClone(ll, m_LabelLeft);
                    ll.Location = new Point(m_LabelLeft.Location.X, m_LabelLeft.Location.Y + i * 96);
                    ll.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(ll)) m_GroupBoxHor.Controls.Add(ll);
                    Label lr = new Label();
                    ControlClone.LabelClone(lr, m_LabelRight);
                    lr.Location = new Point(m_LabelRight.Location.X, m_LabelRight.Location.Y + i * 96);
                    lr.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(lr)) m_GroupBoxHor.Controls.Add(lr);
                }
            }
            else
            {
                m_GroupBoxHor.Height = 145;
            }
            Control[] c = m_GroupBoxHor.Controls.Find("OH", false);
            foreach (var item in c)
            {
                item.Visible = yi != 0;
            }
            //LayoutHorCalibrationComponent();

            start_x = this.m_TextBoxVerSample.Left;
            start_y = this.m_TextBoxVerSample.Top;
            end_x = this.m_GroupBoxVer.Width;
            width_con = this.m_TextBoxVerSample.Width;
            //LayoutHelper.CalculateHorNum(m_ColorNum,start_x,end_x,ref width_con,out space_x);
            space_x = 70;
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                int hg = i / m_ColorNum;
                Label label = this.m_LabelVerHeadIndex[i];
                ControlClone.LabelClone(label, this.m_LabelVerSample);

                label.Location = new Point(start_x + space_x * i, this.m_LabelVerSample.Top);
                label.Width = width_con;
                int colorID = CoreInterface.GetLayoutColorID(i % m_ColorNum);
                if (m_HorColumnNum != 1)
                {
                    label.Text = hg.ToString() + " (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                else
                {
                    label.Text = " (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                label.Visible = true;

                NumericUpDown textBox = this.m_TextBoxVer[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxVerSample);

                textBox.Location = new Point(start_x + space_x * i, this.m_TextBoxVerSample.Top);
                textBox.Width = width_con;
                textBox.TabIndex = m_HeadNum * 2 + i;
                textBox.Text = "0";
                textBox.Visible = true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);

            }
            m_GroupBoxVer.ResumeLayout(false);


            start_x = this.textBoxOverLapSample.Left;
            start_y = this.textBoxOverLapSample.Top;
            end_x = this.grouperOverLap.Width;
            width_con = this.textBoxOverLapSample.Width;
            //LayoutHelper.CalculateHorNum(m_ColorNum,start_x,end_x,ref width_con,out space_x);
            space_x = 70;
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                int hg = i / m_ColorNum;
                Label label = this.m_LabelOverLapHeadIndex[i];
                ControlClone.LabelClone(label, this.label3);

                label.Location = new Point(start_x + space_x * i, label3.Top);
                label.Width = width_con;
                int colorID = CoreInterface.GetLayoutColorID(i % m_ColorNum);
                if (m_HorColumnNum != 1)
                {
                    label.Text = hg.ToString() + "  (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                else
                {
                    label.Text = "  (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                label.Visible = true;
            }
            for (int i = 0; i < m_TextBoxOverLap.Length; i++)
            {
                NumericUpDown textBox = this.m_TextBoxOverLap[i];
                ControlClone.NumericUpDownClone(textBox, this.textBoxOverLapSample);
                if (i >= m_ColorNumAndColumnNum && i % m_ColorNumAndColumnNum == 0)
                    start_y += textBoxOverLapSample.Height + 8;
                textBox.Location = new Point(start_x + space_x * (i % m_ColorNumAndColumnNum), start_y);
                textBox.Width = width_con;
                textBox.TabIndex = m_HeadNum * 2 + i;
                textBox.Text = "0";
                textBox.Visible = true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);
            }
            grouperOverLap.Height = start_y + textBoxOverLapSample.Height + 8;
            grouperOverLap.ResumeLayout(false);

            start_x = this.m_TextBoxGroupLeftSample.Left;
            start_y = this.m_TextBoxGroupLeftSample.Top;
            end_x = this.grouperOverLap.Width;
            width_con = this.m_TextBoxGroupLeftSample.Width;
            for (int i = 0; i < m_LabelGroupHeadIndex.Length; i++)
            {
                Label label = this.m_LabelGroupHeadIndex[i];
                ControlClone.LabelClone(label, this.m_LabelGroupSample);

                label.Location = new Point(start_x + space_x * i, m_LabelGroupSample.Top);
                label.Width = width_con;
                label.Text = "G" + i.ToString();
                label.Visible = true;
            }
            for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
            {
                NumericUpDown textBox = this.m_TextBoxGroupLeft[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxGroupLeftSample);
                textBox.Location = new Point(start_x + space_x * i, start_y);
                textBox.Width = width_con;
                //textBox.TabIndex = m_HeadNum * 2 + i;
                textBox.Text = "0";
                textBox.Visible = true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);
            }
            start_x = this.m_TextBoxGroupRightSample.Left;
            start_y = this.m_TextBoxGroupRightSample.Top;
            width_con = this.m_TextBoxGroupRightSample.Width;
            for (int i = 0; i < m_TextBoxGroupRight.Length; i++)
            {
                NumericUpDown textBox = this.m_TextBoxGroupRight[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxGroupRightSample);
                textBox.Location = new Point(start_x + space_x * i, start_y);
                textBox.Width = width_con;
                //textBox.TabIndex = m_HeadNum * 2 + i;
                textBox.Text = "0";
                textBox.Visible = true;
                textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
                textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
                textBox.TextChanged += new EventHandler(this.m_CheckBox_CheckedChanged);
            }
            grouperGroup.ResumeLayout(false);

            this.ResumeLayout(false);

        }

        private void LayoutHorComponent()
        {
            m_GroupBoxHor.SuspendLayout();
            this.SuspendLayout();
            ///True Layout
            ///
            int start_x, start_y, end_x, space_x, width_con;
            start_x = this.m_TextBoxLeftSample.Left;
            start_y = this.m_TextBoxLeftSample.Top;
            end_x = this.m_GroupBoxHor.Width;
            width_con = this.m_TextBoxLeftSample.Width;
            //LayoutHelper.CalculateHorNum(displayNum, start_x, end_x, ref width_con, out space_x);
            space_x = 70;
            int xi = 0;
            int yi = 0;
            for (int i = 0; i < m_HeadNum; i++)
            {
                if (xi >= 16)
                {
                    xi = 0;
                    yi++;
                }
                Label label = this.m_LabelHorHeadIndex[i];
                label.Location = new Point(start_x + space_x * xi, this.m_LabelHorSample.Top + yi * 96);

                NumericUpDown textBox = this.m_TextBoxHorLeft[i];
                textBox.Location = new Point(start_x + space_x * xi, this.m_TextBoxLeftSample.Top + yi * 96);

                textBox = this.m_TextBoxHorRight[i];
                textBox.Location = new Point(start_x + space_x * xi, this.m_TextBoxRightSample.Top + yi * 96);
                if (label.Visible && textBox.Visible)
                    xi++;
            }
            if (yi != 0)
            {
                m_GroupBoxHor.Height = 145 + yi * 96;
                for (int i = 1; i <= yi; i++)//多行水平较准是的行头，处理
                {
                    Label lh = new Label();
                    ControlClone.LabelClone(lh, m_LabelHead);
                    lh.Location = new Point(m_LabelHead.Location.X, m_LabelHead.Location.Y + i * 96);
                    lh.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(lh)) m_GroupBoxHor.Controls.Add(lh);
                    Label ll = new Label();
                    ControlClone.LabelClone(ll, m_LabelLeft);
                    ll.Location = new Point(m_LabelLeft.Location.X, m_LabelLeft.Location.Y + i * 96);
                    ll.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(ll)) m_GroupBoxHor.Controls.Add(ll);
                    Label lr = new Label();
                    ControlClone.LabelClone(lr, m_LabelRight);
                    lr.Location = new Point(m_LabelRight.Location.X, m_LabelRight.Location.Y + i * 96);
                    lr.Name = "OH";
                    if (!m_GroupBoxHor.Controls.Contains(lr)) m_GroupBoxHor.Controls.Add(lr);
                }
            }
            else
            {
                m_GroupBoxHor.Height = 145;
            }
            Control[] c = m_GroupBoxHor.Controls.Find("OH", false);
            foreach (var item in c)
            {
                item.Visible = yi != 0;
            }
            m_GroupBoxHor.ResumeLayout(false);
            this.ResumeLayout(true);
        }

        private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //m_SpeedSelectIndex = m_ComboBoxSpeed.SelectedIndex;
            OnPrinterSettingChangeSpeed(false);
            //this.isDirty = true;
        }

        private void m_ComboBoxPass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RenewStepValue();
            m_CurPassSelectIndex = m_ComboBoxPass.SelectedIndex;
            //m_PassSelectIndex = m_ComboBoxPass.SelectedIndex;
            //			if(m_SubStep == CalibrationSubStep.Step)
            //			{
            //				for (int i=0;i<m_TextBoxStep.Length;i++)
            //				{
            //					m_TextBoxStep[i].Enabled = false;
            //				}
            //				m_TextBoxStep[m_ComboBoxPass.SelectedIndex].Enabled = true;
            //			}
            m_TextBoxStepSample.Text = m_sCalibrationSetting.nPassStepArray[m_ComboBoxPass.SelectedIndex].ToString();
            //this.isDirty = true;
        }

        private void OnPrinterSettingChangeSpeed(bool bSave)
        {
            try
            {
                int resIndex = 0, speedIndex = 0;
                int curParamIndex = Convert_Speed_SelectedIndex_To_Param_Index(ref resIndex, ref speedIndex);
                SCalibrationHorizonSetting curSpeed = m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex];
                SCalibrationHorizonSettingUI curSpeedNew = m_NewHorizonArrayUI[curParamIndex];
                if (bSave)
                {
                    bool checkResAndSpeed = curSpeedNew.ResIndex == resIndex && curSpeedNew.SpeedIndex == speedIndex;
                    Debug.Assert(checkResAndSpeed, "curSpeedNew.ResIndex == resIndex && curSpeedNew.SpeedIndex == speedIndex");
                    //curSpeedNew.ResIndex = resIndex;
                    //curSpeedNew.SpeedIndex = speedIndex;
                    curSpeed.nBidirRevise = Convert.ToInt32(m_NumUDBidirection.Text);
                    for (int i = 0; i < m_HeadNum; i++)
                    {
                        curSpeedNew.XLeftArray[i] = Convert.ToSByte(m_TextBoxHorLeft[i].Text);
                        curSpeedNew.XRightArray[i] = Convert.ToSByte(m_TextBoxHorRight[i].Text);
                        if (curSpeed.XLeftArray.Length > i)
                            curSpeed.XLeftArray[i] = curSpeedNew.XLeftArray[i];
                        if (curSpeed.XRightArray.Length > i)
                            curSpeed.XRightArray[i] = curSpeedNew.XRightArray[i];
                    }
                    m_NewHorizonArrayUI[curParamIndex] = curSpeedNew;
                    for (int i = 0; i < m_ColorNumAndColumnNum; i++)
                    {
                        m_sCalibrationSetting.nVerticalArray[i] = Convert.ToInt16(m_TextBoxVer[i].Text);
                    }

                    for (int i = 0; i < m_TextBoxOverLap.Length; i++)
                    {
                        m_sCalibrationSetting.nVerticalArray[m_ColorNumAndColumnNum + i] = Convert.ToInt16(m_TextBoxOverLap[i].Text);
                    }
                    m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex] = curSpeed;

                    int index = m_ComboBoxResX.SelectedIndex;
                    if (index < 0) return;
                    _printerSetting.sExtensionSetting.OriginCaliValue[index * 4] = Convert.ToSByte(textBoxHightSpeed.Text);
                    _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 1] = Convert.ToSByte(textBoxMidSpeed.Text);
                    _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 2] = Convert.ToSByte(textBoxLowSpeed.Text);
                    _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 3] = Convert.ToSByte(textBoxCustomSpeed.Text);


                    for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
                    {
                        m_CalibrationGroupUILeft.GCValue[curParamIndex * 32 + i] = Convert.ToInt16(m_TextBoxGroupLeft[i].Text);
                    }
                    for (int i = 0; i < m_TextBoxGroupRight.Length; i++)
                    {
                        m_CalibrationGroupUIRight.GCValue[curParamIndex * 32 + i] = Convert.ToInt16(m_TextBoxGroupRight[i].Text);
                    }
                }
                else
                {
                    m_NumUDBidirection.Text = curSpeed.nBidirRevise.ToString();
                    for (int i = 0; i < m_HeadNum; i++)
                    {
                        if (curSpeed.XLeftArray.Length > i)
                        {
                            m_TextBoxHorLeft[i].Text = curSpeed.XLeftArray[i].ToString();
                        }
                        else
                        {
                            m_TextBoxHorLeft[i].Text = curSpeedNew.XLeftArray[i].ToString();
                        }
                        if (curSpeed.XRightArray.Length > i)
                        {
                            m_TextBoxHorRight[i].Text = curSpeed.XRightArray[i].ToString();
                        }
                        else
                        {
                            m_TextBoxHorRight[i].Text = curSpeedNew.XRightArray[i].ToString();
                        }
                    }
                    for (int i = 0; i < m_ColorNumAndColumnNum; i++)
                    {
                        m_TextBoxVer[i].Text = m_sCalibrationSetting.nVerticalArray[i].ToString();
                    }

                    for (int i = 0; i < m_TextBoxOverLap.Length; i++)
                    {
                        m_TextBoxOverLap[i].Text = m_sCalibrationSetting.nVerticalArray[m_ColorNumAndColumnNum + i].ToString();
                    }

                    int index = m_ComboBoxResX.SelectedIndex;
                    if (index < 0) return;
                    textBoxHightSpeed.Text = _printerSetting.sExtensionSetting.OriginCaliValue[index * 4].ToString();
                    textBoxMidSpeed.Text = _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 1].ToString();
                    textBoxLowSpeed.Text = _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 2].ToString();
                    textBoxCustomSpeed.Text = _printerSetting.sExtensionSetting.OriginCaliValue[index * 4 + 3].ToString();

                    for (int i = 0; i < m_TextBoxGroupLeft.Length; i++)
                    {
                        m_TextBoxGroupLeft[i].Text = m_CalibrationGroupUILeft.GCValue[curParamIndex * 32 + i].ToString();
                    }
                    for (int i = 0; i < m_TextBoxGroupRight.Length; i++)
                    {
                        m_TextBoxGroupRight[i].Text = m_CalibrationGroupUIRight.GCValue[curParamIndex * 32 + i].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnPrinterSettingChangeSpeed Error:" + ex.Message);
            }
        }
        private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
        {
            NumericUpDown NumUD = (NumericUpDown)sender;
            bool isValidNumber = true;
            try
            {
                int val = int.Parse(NumUD.Text);
                //????????????????????????????????????????????
                //Note:Should not Update All Data, 
                //Should only renew the newest control value.				;
                UpdateToLocal();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                isValidNumber = false;
            }

            if (!isValidNumber)
            {
                SystemCall.Beep(200, 50);
                NumUD.Focus();
            }
        }
        private void UpdateToLocal()
        {
            m_sCalibrationSetting.nStepPerHead = Convert.ToInt32(m_TextBoxBaseStep.Text);
            //			for (int i = 0; i<m_PassListNum;i++)
            //			{
            //				m_sCalibrationSetting.nPassStepArray[m_PassList[i]-1] =	Convert.ToInt32(m_TextBoxStepSample[i].Text);//???i
            //			}
            m_sCalibrationSetting.nPassStepArray[m_ComboBoxPass.SelectedIndex] = Convert.ToInt32(m_TextBoxStepSample.Text);
            OnPrinterSettingChangeSpeed(true);

        }
        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                m_CheckBoxControl_Leave(sender, e);
            }
        }
        private void RenewStepValue()
        {
            int bOnePass = 0;
            if (m_CurPassSelectIndex == 0)
                bOnePass = 1;
            float Revise = Decimal.ToSingle(m_NumericUpDownRevise.Value);
            if (Math.Abs(Revise) > 0.00001f)
            {
                m_NumericUpDownRevise.Value = 0;
                int nNew = CoreInterface.GetStepReviseValue(Revise, m_CurPassSelectIndex + 1, ref m_sCalibrationSetting, bOnePass);

                if (bOnePass == 1)
                {
                    m_sCalibrationSetting.nStepPerHead = nNew;
                    m_TextBoxBaseStep.Text = m_sCalibrationSetting.nStepPerHead.ToString();
                }
                else
                {
                    m_sCalibrationSetting.nPassStepArray[m_CurPassSelectIndex] = nNew;
                    m_TextBoxStepSample.Text = m_sCalibrationSetting.nPassStepArray[m_CurPassSelectIndex].ToString();
                }
            }
        }
        private void m_ButtonCalculate_Click(object sender, System.EventArgs e)
        {
            RenewStepValue();
        }
        public void ClearStepCurrentValue()
        {
            m_sCalibrationSetting.nPassStepArray[m_CurPassSelectIndex] = 0;
            m_TextBoxStepSample.Text = m_sCalibrationSetting.nPassStepArray[m_CurPassSelectIndex].ToString();
        }
        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }

        private void buttonApply_Click(object sender, System.EventArgs e)
        {
            if (this.ApplyButtonClicked != null)
                this.ApplyButtonClicked(sender, e);
        }

        private void buttonCalWizard_Click(object sender, System.EventArgs e)
        {
            if (this.CalWizardButtonClicked != null)
                this.CalWizardButtonClicked(sender, e);
        }

        public void SetButtonVisible(bool bVisible)
        {
            //            this.buttonApply.Visible = this.buttonCalWizard.Visible = bVisible;
        }

        public void SetGroupsTop()
        {
            this.m_GroupBoxVer.Top = this.m_GroupBoxStep.Top = this.m_GroupBoxHor.Top;
            //            this.buttonApply.Top = this.buttonCalWizard.Top = this.m_LabelRightMax.Top = m_GroupBoxHor.Bottom;
            m_GroupBoxHor.Height = m_GroupBoxVer.Height;
        }

        public void SetGroupsLocation(CalibrationSubStep step)
        {
            switch (step)
            {
                case CalibrationSubStep.Left:
                case CalibrationSubStep.Right:
                    this.Dock = DockStyle.Bottom;
                    this.Height = this.Parent.Height - WizardPage.HEADER_AREA_HEIGHT;
                    break;
                case CalibrationSubStep.Bidirection:
                case CalibrationSubStep.Vertical:
                case CalibrationSubStep.Step:
                    this.Dock = DockStyle.None;
                    this.Location = new Point(0, WizardPage.HEADER_AREA_HEIGHT);
                    this.Height = this.Parent.Height;
                    break;
                default:
                    break;
            }
        }

        private void CalibrationSetting_Load(object sender, EventArgs e)
        {
            grouptext = m_GroupBoxHor.Text;
            groupWidth = m_GroupBoxHor.Width;
            LayoutHorComponent();
        }

        private void contextMenu1Item_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            int resIndex = 0, speedIndex = 0;
            int curParamIndex = Convert_Speed_SelectedIndex_To_Param_Index(ref resIndex, ref speedIndex);
            int dstindex = (mi.Index / m_nDisplaySpeedNum) * CoreConst.MAX_SPEED_NUM + (mi.Index % m_nDisplaySpeedNum);
            m_sCalibrationSetting.sCalibrationHorizonArray[dstindex] = m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex];
        }

        private void buttonAutoCopy_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button != null && e.Button == MouseButtons.Left)
                    this.contextMenu1.Show(this.buttonAutoCopy, new Point(e.X, e.Y));
            }
            catch { }
        }


        private void m_ButtonPrecision_Click(object sender, System.EventArgs e)
        {
#if true
            SPrinterSetting mCaliSetting = new SPrinterSetting();
            AllParam allparam = m_iPrinterChange.GetAllParam();
            mCaliSetting = allparam.PrinterSetting;
            //mCaliSetting.sFrequencySetting.fXOrigin = allparam.PrinterSetting.sFrequencySetting.fXOrigin;
            //mCaliSetting.sBaseSetting.fYOrigin = allparam.PrinterSetting.sBaseSetting.fYOrigin;
            
            OnGetPrinterSetting(allparam);
            mCaliSetting.sFrequencySetting.nPass = (byte)GetPass();
            mCaliSetting.sFrequencySetting.nSpeed = (SpeedEnum)GetSpeed();
            mCaliSetting.sFrequencySetting.nResolutionX = GetResIndex();

            //GetPrinterSetting(&sPrinterSetting);
            //GetOriginValue();

            int patternNum = 0;
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.StepCmd, patternNum, ref mCaliSetting, true, Platform);
            //CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.EngStepCmd,patternNum,ref m_CaliSetting);
#endif
        }
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable ReadPrintModeListFromXML(SPrinterProperty sp)
        {
            try
            {
                const string col1Name = "DisplayName";
                const string col2Name = "Internal";
                const string col3Name = "Comment";

                DataTable dt = new DataTable();
                dt.Columns.Add(col1Name, typeof(string));
                dt.Columns.Add(col2Name, typeof(string));
                dt.Columns.Add(col3Name, typeof(string));

                string fileName = Path.Combine(Application.StartupPath, "PrintModeList.bin");
                if (!File.Exists(fileName))
                {
                    bNeedLoadPrinterModeFromXML = false;
                    return null;
                }
                SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                doc.Load(fileName);
                XmlElement root = (XmlElement)doc.DocumentElement;
                string nodeName = sp.ePrinterHead.ToString() + (sp.bSupportUV ? "_UV" : "");
                foreach (XmlNode xn in root.ChildNodes)
                {
                    if (xn.Name == nodeName)
                    {
                        XmlNode XmlJob = xn.FirstChild;
                        while (XmlJob != null)
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < XmlJob.Attributes.Count; i++)
                            {
                                if (XmlJob.Attributes[i].Name == col1Name)
                                {
                                    string strspeed = XmlJob.Attributes[i].Value;
                                    string[] strs = strspeed.Split(new char[] { ',' });
                                    string res_mode = strs[0] + "DPI";
                                    SpeedEnum mode = SpeedEnum.CustomSpeed;
                                    string cmode = string.Empty;
                                    if (strs[1] != "*")
                                    {
                                        mode = (SpeedEnum)Enum.Parse(typeof(SpeedEnum), strs[1], true);
                                        cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                                    }
                                    else
                                        cmode = ResString.GetResString("FixedSpeed");
                                    string colname = cmode + "_";
                                    for (int j = 2; j < strs.Length; j++)
                                    {
                                        if (strs[j] == "*")
                                        {
                                            colname = cmode + "_AllPass";
                                            break;
                                        }
                                        if (j + 1 == strs.Length)
                                            colname += strs[j] + "Pass";
                                        else
                                            colname += strs[j] + "/";
                                    }
                                    dr[col1Name] = colname + "_" + res_mode;
                                }
                                if (XmlJob.Attributes[i].Name == col2Name)
                                    dr[col2Name] = XmlJob.Attributes[i].Value;
                                if (XmlJob.Attributes[i].Name == col3Name)
                                    dr[col3Name] = XmlJob.Attributes[i].Value;
                            }
                            dt.Rows.Add(dr);
                            XmlJob = XmlJob.NextSibling;
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bNeedLoadPrinterModeFromXML = false;
                return null;
            }
        }


        private void m_ComboBoxResX_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPrinterSettingChangeSpeed(false);
        }

        private void checkBoxQuickMode_CheckedChanged(object sender, EventArgs e)
        {
            this.IsQuickHorCalibration = checkBoxQuickMode.Checked;
            UpdateHorCalibrationUI();
        }

        int displayNum = 0;
        private void UpdateHorCalibrationUI()
        {
            displayNum = 0;
            for (int i = 0; i < m_HeadNum; i++)
            {
                bool bShow = true;
                switch ((HorizontalCalibrationMode)HorCalibrationMode)
                {
                    case HorizontalCalibrationMode.Quick:
                    case HorizontalCalibrationMode.GroupQuick:
                        bShow = CoreInterface.GetLineIndexInHead(i + 1) == 0 ? true : false;
                        //bShow = i % m_Yinterleave == 0 ? true : false;
                        break;
                    case HorizontalCalibrationMode.Color:
                    case HorizontalCalibrationMode.GroupColor:
                        //bShow = ColorIDList[i] == ColorCalibrationIndex + 1;
                        bShow = ColorIDList[i] == CoreInterface.GetLayoutColorID(ColorCalibrationIndex);
                        break;
                    case HorizontalCalibrationMode.Full:
                    case HorizontalCalibrationMode.GroupFull:
                        break;
                }
                this.m_LabelHorHeadIndex[i].Visible = bShow;
                this.m_TextBoxHorLeft[i].Visible = bShow;
                this.m_TextBoxHorRight[i].Visible = bShow;
                if (bShow) displayNum++;
            }
            ReCalcPanelSize(displayNum);
            LayoutHorComponent();
        }

        private void comboBoxCaliMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCaliMode.SelectedIndex == -1) return;
            HorCalibrationMode = (int)comboBoxCaliMode.SelectedValue;
            switch ((HorizontalCalibrationMode)HorCalibrationMode)
            {
                case HorizontalCalibrationMode.Full:
                case HorizontalCalibrationMode.GroupFull:
                    labelColor.Visible = comboBoxColor.Visible = false;
                    this.IsQuickHorCalibration = false;
                    break;
                case HorizontalCalibrationMode.Color:
                case HorizontalCalibrationMode.GroupColor:
                    labelColor.Visible = comboBoxColor.Visible = true;
                    this.IsQuickHorCalibration = false;
                    break;
                case HorizontalCalibrationMode.Quick:
                case HorizontalCalibrationMode.GroupQuick:
                    labelColor.Visible = comboBoxColor.Visible = false;
                    this.IsQuickHorCalibration = true;
                    break;
                default:
                    break;
            }
            UpdateHorCalibrationUI();
        }

        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxColor.SelectedIndex == -1) return;
            ColorCalibrationIndex = comboBoxColor.SelectedIndex;
            UpdateHorCalibrationUI();
        }

    }

}
