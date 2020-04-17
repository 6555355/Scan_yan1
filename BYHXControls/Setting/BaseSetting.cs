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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using BYHXPrinterManager;
using BYHXPrinterManager.GradientControls;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for BaseSetting.
	/// </summary>
	public class BaseSetting : BYHXUserControl//System.Windows.Forms.UserControl
	{
        private SPrinterSetting _printerSetting;
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private const int  M_AUTOINDENT = 56;
        private bool isDirty = false;
        private List<RadioButton> cRadioButtons;
	    private bool isAllwin512IHighSpeed = false;
	    private bool bDoubleYAxis = false;
        private bool bSupportUv = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private System.Windows.Forms.Label m_LabelLeftEdge;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownLeftEdge;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWidth;
		private System.Windows.Forms.Label m_LabelWidth;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxInkStripe;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStripeSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStripeWidth;
		private System.Windows.Forms.Label m_LabelStripeWidth;
		private System.Windows.Forms.Label m_LabelStripeSpace;
		private System.Windows.Forms.Label m_LabelStripePos;
		private System.Windows.Forms.ComboBox m_ComboBoxPlace;
		private System.Windows.Forms.CheckBox m_CheckBoxNormalType;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxMedia;
		private System.Windows.Forms.Label m_LabelAutoSpray;
		private System.Windows.Forms.CheckBox m_CheckBoxIdleSpray;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxPrintSetting;
		private System.Windows.Forms.CheckBox m_CheckBoxAutoJumpWhite;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownJobSpace;
		private System.Windows.Forms.Label m_LabelJobSpace;
		private System.Windows.Forms.Label m_LabelSprayCycle;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownAutoSpray;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownSprayCycle;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownFeather;
		private System.Windows.Forms.Label m_LabelFeather;
		private System.Windows.Forms.Label m_LabelY;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownY;
		private System.Windows.Forms.Label m_LabelHeight;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownHeight;
		private System.Windows.Forms.Button m_ButtonMeasure;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxZ;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownZSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownThickness;
		private System.Windows.Forms.Label m_LabelMediaThickness;
        private System.Windows.Forms.Label m_LabelZSpace;
		private System.Windows.Forms.Button m_ButtonZManual;
		private System.Windows.Forms.CheckBox m_CheckBoxKillBidir;
		private System.Windows.Forms.CheckBox m_CheckBoxYContinue;
		private System.Windows.Forms.Label m_LabelStepTime;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStepTime;
		private System.Windows.Forms.CheckBox m_CheckBoxMixedType;
		private System.Windows.Forms.CheckBox m_CheckBoxHeightWithImageType;
		private System.Windows.Forms.CheckBox m_CheckBoxAutoYCalibrate;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownSprayTimes;
		private System.Windows.Forms.Label m_LabelSprayTimes;
		private System.Windows.Forms.Label m_LabelMargin;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownMargin;
		private System.Windows.Forms.CheckBox m_CheckBoxMeasureBeforePrint;
        private System.Windows.Forms.CheckBox m_CheckBoxSprayBeforePrint;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownPTASpraying;
		private System.Windows.Forms.Label m_labelPrintPrespraytime;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxSpray;
		private System.Windows.Forms.CheckBox m_CheckBoxNozzleClogging;
		private System.Windows.Forms.ComboBox m_ComboBoxFeatherType;
		private System.Windows.Forms.Label label_FeatherType;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownFeatherPercent;
        private System.Windows.Forms.NumericUpDown m_NmericUpDownWaveLen;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_MultipleInk;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox m_ComboBoxDiv;
		private System.Windows.Forms.Panel panelXDiv;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxZMeasure;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.NumericUpDown numericUpDownHeadToPaper;
		private System.Windows.Forms.NumericUpDown numericUpDownMesureHeight;
        private GradientControls.Grouper grouper1;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Panel panelFlatDistanceY;
        private NumericUpDown numFlatDistanceY;
        private Label label14;
		private System.ComponentModel.IContainer components;
        private NumericUpDown numZWorkPos;
        private Label labelZWorkPos;
        private CheckBox checkBoxPixelStep;
        private Panel panelStablecolorPixelStep;
        private ComboBox comboBoxFeatherPercent;
        private TextBox textBoxFeather;
        private CheckBox checkBoxFixPos;
        private GradientControls.Grouper grouperEliminateYMotorGap;
        private Label label11;
        private NumericUpDown numericMotorgap;
        private Panel panel13;
        private GradientControls.Grouper grouperManualSpray;
        private Label labelManualSprayFreq;
        private NumericUpDown numManualSprayFreq;
        private NumericUpDown numManualSprayPeriod;
        private Label labelManualSprayPeriod;
        private FlowLayoutPanel flowLayoutPanel1;
        private GradientControls.Grouper m_GroupBoxClean;
        private FloraParamControl floraParamControl1;
        private NumericUpDown numCleanPosZ;
        private Label label10;
        private NumericUpDown numCleanPosY;
        private Label label9;
        private NumericUpDown numCleanPosX;
        private Label label8;
        private NumericUpDown m_NumAutoCleanPosMov;
        private NumericUpDown m_NumAutoCleanPosLen;
        private Label lblEndPos;
        private Label lblAutoCleanPosMov;
        private NumericUpDown m_NumericUpDownPTACleaning;
        private Label m_labelPauseTimeAfterCleaning;
        private NumericUpDown m_NumericUpDownAutoClean;
        private NumericUpDown m_NumericUpDownCleanTimes;
        private Label m_LabelCleanTimes;
        private Label m_LabelAutoClean;
        private CheckBox checkBoxClosedLoopControl;
        private SPrinterProperty m_sPrinterProperty;
        private Panel panel3;
        private Panel panel5;
        private Button buttonMoveToWorkPos;
        private Panel panelZWorkPos;
        private Panel panelZMove;
        private CheckBox checkBoxExquisiteFeather;
        private Panel panelFeatherPram;
        private NumericUpDown numHumidInterval;
        private Label labelHumidInterval;
        private NumericUpDown numHumidPos;
        private Label labelHumidPos;
        private NumericUpDown numSuctionEndPos;
        private Label labelSuctionEndPos;
        private NumericUpDown numSuctionStartPos;
        private Label labelSuctionStartPos;
        private NumericUpDown numSuctionTimes;
        private Label labelSuctionTimes;
        private Panel panelYuDaBeltMachineParam;
        private NumericUpDown numericPlatformCorrect;
        private Label labelPlatformCorrect;
        private GradientControls.Grouper grouper2;
        private Label labelRollLength;
        private NumericUpDown numRollLength;
        private Button buttonFastCalculate;
        private CheckBox checkBoxEnableDetectRemainingRoll;
        private Panel panelUVoffsetDistance;
        private NumericUpDown numUVoffsetDistance;
        private Label labelUVoffsetDistance;
        private Panel panel2;
        private NumericUpDown numericUpDownBSZPos;
        private Label label5;
        private NumericUpDown numericUpDownCleanZPos;
        private Label label6;
        private GradientControls.Grouper m_Grouper_DoubleAxis;
        private NumericUpDown numDrvEncRatio2;
        private Label label7;
        private NumericUpDown numDrvEncRatio1;
        private Label label12;
        private NumericUpDown numDoubeYRatio;
        private Label label13;
        private NumericUpDown m_NumericUpDown_MaxTolerancePos;
        private Label m_Label_MaxTolerancePos;
        private CheckBox m_CheckBox_CorrectOffset;
        private Panel panelZWorkPos2;
        private Button buttonMoveToWorkPos2;
        private NumericUpDown numZWorkPos2;
        private Label label17;
        private AutoBackHomeSetting autoBackHomeSetting1;
        private CheckBox checkBoxBSUseSpray;
        private CheckBox checkBoxUseHighParam;
        private Grouper grouperWhiteInkMixing;
        private NumericUpDown numCycleTime;
        private Label labelCycleTime;
        private NumericUpDown numPulseTime;
        private Label labelPulseTime;
        private NumericUpDown numStirCycleTime;
        private Label labelStirCycleTime;
        private NumericUpDown numStirPulseTime;
        private Label labelStirPulseTime;
        private Panel panelUVLightInAdvance;
        private NumericUpDown numUVLightInAdvance;
        private Label labelUVLightInAdvance;
        private Grouper grouperDrySetting;
        private CheckBox checkBoxBackBeforPrint;
        private CheckBox checkBoxRunAfterPrinting;
        private ComboBox m_cmbMoveXSpeed;
        private NumericUpDown numDistanceAfterPrint;
        private Label label15;
        private Label label16;
        private Grouper grouperUVCureSetting;
        private CheckBox checkBoxBackBeforPrintUV;
        private CheckBox checkBoxRunAfterPrintingUV;
        private CheckBox checkBoxConstantStep;
        private CheckBox checkBoxJointFeather;
        private Panel panelZCleanAndWet;
        private Label label18;
        private NumericUpDown numZMaxLen;
        private Button m_ButtonMeasureMaxZ;
        private Label label19;
        private NumericUpDown numericUpDown_SensorPos;
        private CheckBox checkBoxOneStepSkipWhite;
        private Grouper grouperLingFeng_RollUVCappingPara;
        private CheckBox cbxCappingEnable;
        private CheckBox cbxZaxisEnable;
        private CheckBox checkBoxzCaliNoStep;
        private Grouper m_grouperPrintDir;
        private CheckBox CheckBoxReversePrint;
        private CheckBox m_CheckBoxMirror;
        private Panel panel1;
        private Grouper grouperMisc;
        private CheckBox checkBoxCalibrationNoStep;
        private ComboBox m_ComboBoxInkPercent;
        private Label m_LabelInkPercent;
        private Grouper grouper_HumanCleanSetting;
        private NumericUpDown numericUpDown_HumanCleanXpos;
        private Label label_HumanCleanXpos;
        private NumericUpDown numericUpDown_HumanScarperLen;
        private Label label_HumanScarperLen;
        private NumericUpDown numericUpDown_HumanRecoverTime;
        private Label label_HumanRecoverTime;
        private NumericUpDown numericUpDown_HumanPressInkTime;
        private Label label_HumanPressInkTime;
        private Label labelNozzleFeather;
        private NumericUpDown NumFeatherNozzle;
	    private List<FeatherType> featherTypeUiList;
	    public BaseSetting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
	        if (PubFunc.IsInDesignMode())
	            return;
            ToolTipInit();
			
			m_NmericUpDownWaveLen.Location = m_NumericUpDownFeatherPercent.Location;
#if !LIYUUSB
			m_LabelSprayTimes.Visible = false;
			m_NumericUpDownSprayTimes.Visible = false;

            int offsettop = m_labelPrintPrespraytime.Top - m_LabelSprayTimes.Top;
            m_labelPrintPrespraytime.Top -= offsettop;
            m_NumericUpDownPTASpraying.Top -= offsettop;
            m_CheckBoxIdleSpray.Top -= offsettop;
            m_CheckBoxSprayBeforePrint.Top -= offsettop;
            checkBoxUseHighParam.Top -= offsettop;
            checkBoxBSUseSpray.Top -= offsettop;
            m_GroupBoxSpray.Height -= offsettop;
#else
            m_CheckBoxHeightWithImageType.Visible = false;
            m_CheckBoxKillBidir.Visible = false;
//            m_labelPrintPrespraytime.Visible = false;
//			m_NumericUpDownPTASpraying.Visible = false;
			m_labelPauseTimeAfterCleaning.Visible = false;
			m_NumericUpDownPTACleaning.Visible = false;

            int offsettop1 = m_GroupBoxClean.Height - m_labelPauseTimeAfterCleaning.Top;
            int offsettop2 = m_CheckBoxIdleSpray.Top - m_labelPrintPrespraytime.Top;
            int offsettop = offsettop1 + offsettop2;
            m_GroupBoxClean.Height = m_labelPauseTimeAfterCleaning.Top;
            m_GroupBoxSpray.Top -= offsettop1;
            m_GroupBoxSpray.Height -= offsettop2;
            m_CheckBoxIdleSpray.Top -= offsettop2;
            m_CheckBoxSprayBeforePrint.Top -= offsettop2;
#endif
            //this.panel1.Top -= offsettop;
//            m_CheckBoxKillBidir.Top -= offsettop;
//            m_CheckBoxAutoYCalibrate.Top -= offsettop;
            cRadioButtons = new List<RadioButton>(){radioButton1,radioButton2,radioButton3,radioButton4};

            featherTypeUiList = new List<FeatherType>()
            {
               FeatherType.Gradient,
                FeatherType.Uniform,
                FeatherType.Wave,
               FeatherType. Advance,
               FeatherType. Uv,
                //FeatherType.Joint,
                //FeatherType.Debug,
            };
            if (PubFunc.GetUserPermission() == (int)(UserPermission.SupperUser))
	        {
                featherTypeUiList.Add(FeatherType.Debug);
	        }
            //foreach (FeatherType place in Enum.GetValues(typeof(FeatherType)))
            //{
            //    featherTypeUiList.Add(place);
            //}
        }

	    private void SetFeatherTypeToUi(FeatherType type)
	    {
	        m_ComboBoxFeatherType.SelectedIndex = featherTypeUiList.IndexOf(type);
	    }
	    private FeatherType GetFeatherTypeFromUi()
	    {
            return featherTypeUiList[m_ComboBoxFeatherType.SelectedIndex];
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSetting));
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
            BYHXPrinterManager.Style style13 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style14 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style15 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style16 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style17 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style18 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style19 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style20 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style21 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style22 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style23 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style24 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style25 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style26 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style27 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style28 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style29 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style30 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style31 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style32 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style33 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style34 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style35 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style36 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style37 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style38 = new BYHXPrinterManager.Style();
            this.m_LabelLeftEdge = new System.Windows.Forms.Label();
            this.m_NumericUpDownLeftEdge = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.m_GroupBoxInkStripe = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_ComboBoxInkPercent = new System.Windows.Forms.ComboBox();
            this.m_LabelInkPercent = new System.Windows.Forms.Label();
            this.checkBoxFixPos = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxNormalType = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxPlace = new System.Windows.Forms.ComboBox();
            this.m_NumericUpDownStripeSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownStripeWidth = new System.Windows.Forms.NumericUpDown();
            this.m_LabelStripeWidth = new System.Windows.Forms.Label();
            this.m_LabelStripeSpace = new System.Windows.Forms.Label();
            this.m_LabelStripePos = new System.Windows.Forms.Label();
            this.m_CheckBoxMixedType = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxHeightWithImageType = new System.Windows.Forms.CheckBox();
            this.m_GroupBoxMedia = new BYHXPrinterManager.GradientControls.Grouper();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown_SensorPos = new System.Windows.Forms.NumericUpDown();
            this.m_LabelY = new System.Windows.Forms.Label();
            this.m_NumericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHeight = new System.Windows.Forms.Label();
            this.m_NumericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonMeasure = new System.Windows.Forms.Button();
            this.m_LabelMargin = new System.Windows.Forms.Label();
            this.m_NumericUpDownMargin = new System.Windows.Forms.NumericUpDown();
            this.m_CheckBoxMeasureBeforePrint = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownPTASpraying = new System.Windows.Forms.NumericUpDown();
            this.m_labelPrintPrespraytime = new System.Windows.Forms.Label();
            this.m_CheckBoxSprayBeforePrint = new System.Windows.Forms.CheckBox();
            this.m_LabelAutoSpray = new System.Windows.Forms.Label();
            this.m_NumericUpDownAutoSpray = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownSprayCycle = new System.Windows.Forms.NumericUpDown();
            this.m_LabelSprayCycle = new System.Windows.Forms.Label();
            this.m_CheckBoxIdleSpray = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownSprayTimes = new System.Windows.Forms.NumericUpDown();
            this.m_LabelSprayTimes = new System.Windows.Forms.Label();
            this.m_GroupBoxPrintSetting = new BYHXPrinterManager.GradientControls.Grouper();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxzCaliNoStep = new System.Windows.Forms.CheckBox();
            this.checkBoxOneStepSkipWhite = new System.Windows.Forms.CheckBox();
            this.checkBoxConstantStep = new System.Windows.Forms.CheckBox();
            this.panelFeatherPram = new System.Windows.Forms.Panel();
            this.labelNozzleFeather = new System.Windows.Forms.Label();
            this.NumFeatherNozzle = new System.Windows.Forms.NumericUpDown();
            this.textBoxFeather = new System.Windows.Forms.TextBox();
            this.label_FeatherType = new System.Windows.Forms.Label();
            this.comboBoxFeatherPercent = new System.Windows.Forms.ComboBox();
            this.m_LabelFeather = new System.Windows.Forms.Label();
            this.m_NumericUpDownFeather = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxFeatherType = new System.Windows.Forms.ComboBox();
            this.m_NmericUpDownWaveLen = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownFeatherPercent = new System.Windows.Forms.NumericUpDown();
            this.checkBoxJointFeather = new System.Windows.Forms.CheckBox();
            this.panelYuDaBeltMachineParam = new System.Windows.Forms.Panel();
            this.numericPlatformCorrect = new System.Windows.Forms.NumericUpDown();
            this.labelPlatformCorrect = new System.Windows.Forms.Label();
            this.m_LabelJobSpace = new System.Windows.Forms.Label();
            this.checkBoxExquisiteFeather = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownJobSpace = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_MultipleInk = new System.Windows.Forms.ComboBox();
            this.m_CheckBoxAutoJumpWhite = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxYContinue = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownStepTime = new System.Windows.Forms.NumericUpDown();
            this.m_LabelStepTime = new System.Windows.Forms.Label();
            this.panelUVoffsetDistance = new System.Windows.Forms.Panel();
            this.numUVoffsetDistance = new System.Windows.Forms.NumericUpDown();
            this.labelUVoffsetDistance = new System.Windows.Forms.Label();
            this.m_GroupBoxZ = new BYHXPrinterManager.GradientControls.Grouper();
            this.panelZCleanAndWet = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownBSZPos = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCleanZPos = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panelZWorkPos2 = new System.Windows.Forms.Panel();
            this.buttonMoveToWorkPos2 = new System.Windows.Forms.Button();
            this.numZWorkPos2 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.panelZWorkPos = new System.Windows.Forms.Panel();
            this.buttonMoveToWorkPos = new System.Windows.Forms.Button();
            this.numZWorkPos = new System.Windows.Forms.NumericUpDown();
            this.labelZWorkPos = new System.Windows.Forms.Label();
            this.panelZMove = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.numZMaxLen = new System.Windows.Forms.NumericUpDown();
            this.m_LabelZSpace = new System.Windows.Forms.Label();
            this.m_NumericUpDownZSpace = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonZManual = new System.Windows.Forms.Button();
            this.m_NumericUpDownThickness = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonMeasureMaxZ = new System.Windows.Forms.Button();
            this.m_LabelMediaThickness = new System.Windows.Forms.Label();
            this.m_CheckBoxKillBidir = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxAutoYCalibrate = new System.Windows.Forms.CheckBox();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.numManualSprayFreq = new System.Windows.Forms.NumericUpDown();
            this.numManualSprayPeriod = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownCleanTimes = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownAutoClean = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownPTACleaning = new System.Windows.Forms.NumericUpDown();
            this.numSuctionTimes = new System.Windows.Forms.NumericUpDown();
            this.numHumidInterval = new System.Windows.Forms.NumericUpDown();
            this.numCycleTime = new System.Windows.Forms.NumericUpDown();
            this.numPulseTime = new System.Windows.Forms.NumericUpDown();
            this.numStirCycleTime = new System.Windows.Forms.NumericUpDown();
            this.numStirPulseTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_HumanCleanXpos = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_HumanScarperLen = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_HumanRecoverTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_HumanPressInkTime = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxSpray = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxBSUseSpray = new System.Windows.Forms.CheckBox();
            this.checkBoxUseHighParam = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxDiv = new System.Windows.Forms.ComboBox();
            this.m_CheckBoxNozzleClogging = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelFlatDistanceY = new System.Windows.Forms.Panel();
            this.numFlatDistanceY = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.panelXDiv = new System.Windows.Forms.Panel();
            this.panelStablecolorPixelStep = new System.Windows.Forms.Panel();
            this.checkBoxPixelStep = new System.Windows.Forms.CheckBox();
            this.m_GroupBoxZMeasure = new BYHXPrinterManager.GradientControls.Grouper();
            this.numericUpDownHeadToPaper = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMesureHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grouper1 = new BYHXPrinterManager.GradientControls.Grouper();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.grouperManualSpray = new BYHXPrinterManager.GradientControls.Grouper();
            this.labelManualSprayFreq = new System.Windows.Forms.Label();
            this.labelManualSprayPeriod = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.m_grouperPrintDir = new BYHXPrinterManager.GradientControls.Grouper();
            this.CheckBoxReversePrint = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxMirror = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grouper2 = new BYHXPrinterManager.GradientControls.Grouper();
            this.labelRollLength = new System.Windows.Forms.Label();
            this.numRollLength = new System.Windows.Forms.NumericUpDown();
            this.buttonFastCalculate = new System.Windows.Forms.Button();
            this.checkBoxEnableDetectRemainingRoll = new System.Windows.Forms.CheckBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBoxClosedLoopControl = new System.Windows.Forms.CheckBox();
            this.grouperUVCureSetting = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxBackBeforPrintUV = new System.Windows.Forms.CheckBox();
            this.checkBoxRunAfterPrintingUV = new System.Windows.Forms.CheckBox();
            this.panelUVLightInAdvance = new System.Windows.Forms.Panel();
            this.numUVLightInAdvance = new System.Windows.Forms.NumericUpDown();
            this.labelUVLightInAdvance = new System.Windows.Forms.Label();
            this.grouperEliminateYMotorGap = new BYHXPrinterManager.GradientControls.Grouper();
            this.label11 = new System.Windows.Forms.Label();
            this.numericMotorgap = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.m_GroupBoxClean = new BYHXPrinterManager.GradientControls.Grouper();
            this.floraParamControl1 = new BYHXPrinterManager.Setting.FloraParamControl();
            this.numSuctionEndPos = new System.Windows.Forms.NumericUpDown();
            this.labelSuctionEndPos = new System.Windows.Forms.Label();
            this.numSuctionStartPos = new System.Windows.Forms.NumericUpDown();
            this.labelSuctionStartPos = new System.Windows.Forms.Label();
            this.labelSuctionTimes = new System.Windows.Forms.Label();
            this.labelHumidInterval = new System.Windows.Forms.Label();
            this.numHumidPos = new System.Windows.Forms.NumericUpDown();
            this.labelHumidPos = new System.Windows.Forms.Label();
            this.numCleanPosZ = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numCleanPosY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numCleanPosX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.m_NumAutoCleanPosMov = new System.Windows.Forms.NumericUpDown();
            this.m_NumAutoCleanPosLen = new System.Windows.Forms.NumericUpDown();
            this.lblEndPos = new System.Windows.Forms.Label();
            this.lblAutoCleanPosMov = new System.Windows.Forms.Label();
            this.m_labelPauseTimeAfterCleaning = new System.Windows.Forms.Label();
            this.m_LabelCleanTimes = new System.Windows.Forms.Label();
            this.m_LabelAutoClean = new System.Windows.Forms.Label();
            this.grouperLingFeng_RollUVCappingPara = new BYHXPrinterManager.GradientControls.Grouper();
            this.cbxCappingEnable = new System.Windows.Forms.CheckBox();
            this.cbxZaxisEnable = new System.Windows.Forms.CheckBox();
            this.autoBackHomeSetting1 = new BYHXPrinterManager.Setting.AutoBackHomeSetting();
            this.grouperWhiteInkMixing = new BYHXPrinterManager.GradientControls.Grouper();
            this.labelStirCycleTime = new System.Windows.Forms.Label();
            this.labelStirPulseTime = new System.Windows.Forms.Label();
            this.labelCycleTime = new System.Windows.Forms.Label();
            this.labelPulseTime = new System.Windows.Forms.Label();
            this.m_Grouper_DoubleAxis = new BYHXPrinterManager.GradientControls.Grouper();
            this.numDrvEncRatio2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numDrvEncRatio1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numDoubeYRatio = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.m_NumericUpDown_MaxTolerancePos = new System.Windows.Forms.NumericUpDown();
            this.m_Label_MaxTolerancePos = new System.Windows.Forms.Label();
            this.m_CheckBox_CorrectOffset = new System.Windows.Forms.CheckBox();
            this.grouperDrySetting = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxBackBeforPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxRunAfterPrinting = new System.Windows.Forms.CheckBox();
            this.m_cmbMoveXSpeed = new System.Windows.Forms.ComboBox();
            this.numDistanceAfterPrint = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.grouperMisc = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxCalibrationNoStep = new System.Windows.Forms.CheckBox();
            this.grouper_HumanCleanSetting = new BYHXPrinterManager.GradientControls.Grouper();
            this.label_HumanCleanXpos = new System.Windows.Forms.Label();
            this.label_HumanScarperLen = new System.Windows.Forms.Label();
            this.label_HumanRecoverTime = new System.Windows.Forms.Label();
            this.label_HumanPressInkTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).BeginInit();
            this.m_GroupBoxInkStripe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).BeginInit();
            this.m_GroupBoxMedia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SensorPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTASpraying)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoSpray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayTimes)).BeginInit();
            this.m_GroupBoxPrintSetting.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelFeatherPram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumFeatherNozzle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NmericUpDownWaveLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeatherPercent)).BeginInit();
            this.panelYuDaBeltMachineParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlatformCorrect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).BeginInit();
            this.panelUVoffsetDistance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVoffsetDistance)).BeginInit();
            this.m_GroupBoxZ.SuspendLayout();
            this.panelZCleanAndWet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBSZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCleanZPos)).BeginInit();
            this.panelZWorkPos2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos2)).BeginInit();
            this.panelZWorkPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos)).BeginInit();
            this.panelZMove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZMaxLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManualSprayFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManualSprayPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCleanTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTACleaning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStirCycleTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStirPulseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanCleanXpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanScarperLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanRecoverTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanPressInkTime)).BeginInit();
            this.m_GroupBoxSpray.SuspendLayout();
            this.panelFlatDistanceY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistanceY)).BeginInit();
            this.panelXDiv.SuspendLayout();
            this.panelStablecolorPixelStep.SuspendLayout();
            this.m_GroupBoxZMeasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadToPaper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureHeight)).BeginInit();
            this.grouper1.SuspendLayout();
            this.grouperManualSpray.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.m_grouperPrintDir.SuspendLayout();
            this.grouper2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRollLength)).BeginInit();
            this.grouperUVCureSetting.SuspendLayout();
            this.panelUVLightInAdvance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVLightInAdvance)).BeginInit();
            this.grouperEliminateYMotorGap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorgap)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.m_GroupBoxClean.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionEndPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionStartPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumAutoCleanPosMov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumAutoCleanPosLen)).BeginInit();
            this.grouperLingFeng_RollUVCappingPara.SuspendLayout();
            this.grouperWhiteInkMixing.SuspendLayout();
            this.m_Grouper_DoubleAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvEncRatio2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvEncRatio1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoubeYRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_MaxTolerancePos)).BeginInit();
            this.grouperDrySetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceAfterPrint)).BeginInit();
            this.grouperMisc.SuspendLayout();
            this.grouper_HumanCleanSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelLeftEdge
            // 
            resources.ApplyResources(this.m_LabelLeftEdge, "m_LabelLeftEdge");
            this.m_LabelLeftEdge.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLeftEdge.Name = "m_LabelLeftEdge";
            // 
            // m_NumericUpDownLeftEdge
            // 
            this.m_NumericUpDownLeftEdge.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownLeftEdge, "m_NumericUpDownLeftEdge");
            this.m_NumericUpDownLeftEdge.Name = "m_NumericUpDownLeftEdge";
            this.m_NumericUpDownLeftEdge.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownWidth
            // 
            this.m_NumericUpDownWidth.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownWidth, "m_NumericUpDownWidth");
            this.m_NumericUpDownWidth.Name = "m_NumericUpDownWidth";
            this.m_NumericUpDownWidth.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelWidth
            // 
            resources.ApplyResources(this.m_LabelWidth, "m_LabelWidth");
            this.m_LabelWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelWidth.Name = "m_LabelWidth";
            // 
            // m_GroupBoxInkStripe
            // 
            resources.ApplyResources(this.m_GroupBoxInkStripe, "m_GroupBoxInkStripe");
            this.m_GroupBoxInkStripe.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxInkStripe.BorderThickness = 1F;
            this.m_GroupBoxInkStripe.Controls.Add(this.m_ComboBoxInkPercent);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelInkPercent);
            this.m_GroupBoxInkStripe.Controls.Add(this.checkBoxFixPos);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxNormalType);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_ComboBoxPlace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_NumericUpDownStripeSpace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_NumericUpDownStripeWidth);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripeSpace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripePos);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxMixedType);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxHeightWithImageType);
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInkStripe.GradientColors = style1;
            this.m_GroupBoxInkStripe.GroupImage = null;
            this.m_GroupBoxInkStripe.Name = "m_GroupBoxInkStripe";
            this.m_GroupBoxInkStripe.PaintGroupBox = false;
            this.m_GroupBoxInkStripe.RoundCorners = 10;
            this.m_GroupBoxInkStripe.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxInkStripe.ShadowControl = false;
            this.m_GroupBoxInkStripe.ShadowThickness = 3;
            this.m_GroupBoxInkStripe.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxInkStripe.TitileGradientColors = style2;
            this.m_GroupBoxInkStripe.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxInkStripe.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_ComboBoxInkPercent
            // 
            this.m_ComboBoxInkPercent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxInkPercent, "m_ComboBoxInkPercent");
            this.m_ComboBoxInkPercent.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxInkPercent.Items"),
            resources.GetString("m_ComboBoxInkPercent.Items1"),
            resources.GetString("m_ComboBoxInkPercent.Items2"),
            resources.GetString("m_ComboBoxInkPercent.Items3")});
            this.m_ComboBoxInkPercent.Name = "m_ComboBoxInkPercent";
            // 
            // m_LabelInkPercent
            // 
            resources.ApplyResources(this.m_LabelInkPercent, "m_LabelInkPercent");
            this.m_LabelInkPercent.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelInkPercent.Name = "m_LabelInkPercent";
            // 
            // checkBoxFixPos
            // 
            resources.ApplyResources(this.checkBoxFixPos, "checkBoxFixPos");
            this.checkBoxFixPos.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFixPos.Name = "checkBoxFixPos";
            this.checkBoxFixPos.UseVisualStyleBackColor = false;
            this.checkBoxFixPos.CheckedChanged += new System.EventHandler(this.checkBoxFixPos_CheckedChanged);
            // 
            // m_CheckBoxNormalType
            // 
            resources.ApplyResources(this.m_CheckBoxNormalType, "m_CheckBoxNormalType");
            this.m_CheckBoxNormalType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxNormalType.Name = "m_CheckBoxNormalType";
            this.m_CheckBoxNormalType.UseVisualStyleBackColor = false;
            this.m_CheckBoxNormalType.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxPlace
            // 
            this.m_ComboBoxPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPlace, "m_ComboBoxPlace");
            this.m_ComboBoxPlace.Name = "m_ComboBoxPlace";
            this.m_ComboBoxPlace.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownStripeSpace
            // 
            this.m_NumericUpDownStripeSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownStripeSpace, "m_NumericUpDownStripeSpace");
            this.m_NumericUpDownStripeSpace.Name = "m_NumericUpDownStripeSpace";
            this.m_NumericUpDownStripeSpace.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownStripeWidth
            // 
            this.m_NumericUpDownStripeWidth.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownStripeWidth, "m_NumericUpDownStripeWidth");
            this.m_NumericUpDownStripeWidth.Name = "m_NumericUpDownStripeWidth";
            this.m_NumericUpDownStripeWidth.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelStripeWidth
            // 
            resources.ApplyResources(this.m_LabelStripeWidth, "m_LabelStripeWidth");
            this.m_LabelStripeWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeWidth.Name = "m_LabelStripeWidth";
            // 
            // m_LabelStripeSpace
            // 
            resources.ApplyResources(this.m_LabelStripeSpace, "m_LabelStripeSpace");
            this.m_LabelStripeSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeSpace.Name = "m_LabelStripeSpace";
            // 
            // m_LabelStripePos
            // 
            resources.ApplyResources(this.m_LabelStripePos, "m_LabelStripePos");
            this.m_LabelStripePos.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripePos.Name = "m_LabelStripePos";
            // 
            // m_CheckBoxMixedType
            // 
            resources.ApplyResources(this.m_CheckBoxMixedType, "m_CheckBoxMixedType");
            this.m_CheckBoxMixedType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxMixedType.Name = "m_CheckBoxMixedType";
            this.m_CheckBoxMixedType.UseVisualStyleBackColor = false;
            this.m_CheckBoxMixedType.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxHeightWithImageType
            // 
            resources.ApplyResources(this.m_CheckBoxHeightWithImageType, "m_CheckBoxHeightWithImageType");
            this.m_CheckBoxHeightWithImageType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxHeightWithImageType.Name = "m_CheckBoxHeightWithImageType";
            this.m_CheckBoxHeightWithImageType.UseVisualStyleBackColor = false;
            this.m_CheckBoxHeightWithImageType.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_GroupBoxMedia
            // 
            resources.ApplyResources(this.m_GroupBoxMedia, "m_GroupBoxMedia");
            this.m_GroupBoxMedia.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxMedia.BorderThickness = 1F;
            this.m_GroupBoxMedia.Controls.Add(this.label19);
            this.m_GroupBoxMedia.Controls.Add(this.numericUpDown_SensorPos);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownLeftEdge);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownWidth);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelWidth);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelLeftEdge);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelY);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownY);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelHeight);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownHeight);
            this.m_GroupBoxMedia.Controls.Add(this.m_ButtonMeasure);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelMargin);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownMargin);
            this.m_GroupBoxMedia.Controls.Add(this.m_CheckBoxMeasureBeforePrint);
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxMedia.GradientColors = style3;
            this.m_GroupBoxMedia.GroupImage = null;
            this.m_GroupBoxMedia.Name = "m_GroupBoxMedia";
            this.m_GroupBoxMedia.PaintGroupBox = false;
            this.m_GroupBoxMedia.RoundCorners = 10;
            this.m_GroupBoxMedia.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxMedia.ShadowControl = false;
            this.m_GroupBoxMedia.ShadowThickness = 3;
            this.m_GroupBoxMedia.TabStop = false;
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxMedia.TitileGradientColors = style4;
            this.m_GroupBoxMedia.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxMedia.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Name = "label19";
            // 
            // numericUpDown_SensorPos
            // 
            this.numericUpDown_SensorPos.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDown_SensorPos, "numericUpDown_SensorPos");
            this.numericUpDown_SensorPos.Name = "numericUpDown_SensorPos";
            // 
            // m_LabelY
            // 
            resources.ApplyResources(this.m_LabelY, "m_LabelY");
            this.m_LabelY.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelY.Name = "m_LabelY";
            // 
            // m_NumericUpDownY
            // 
            this.m_NumericUpDownY.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownY, "m_NumericUpDownY");
            this.m_NumericUpDownY.Name = "m_NumericUpDownY";
            this.m_NumericUpDownY.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelHeight
            // 
            resources.ApplyResources(this.m_LabelHeight, "m_LabelHeight");
            this.m_LabelHeight.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHeight.Name = "m_LabelHeight";
            // 
            // m_NumericUpDownHeight
            // 
            this.m_NumericUpDownHeight.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownHeight, "m_NumericUpDownHeight");
            this.m_NumericUpDownHeight.Name = "m_NumericUpDownHeight";
            this.m_NumericUpDownHeight.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ButtonMeasure
            // 
            resources.ApplyResources(this.m_ButtonMeasure, "m_ButtonMeasure");
            this.m_ButtonMeasure.Name = "m_ButtonMeasure";
            this.m_ButtonMeasure.Click += new System.EventHandler(this.m_ButtonMeasure_Click);
            // 
            // m_LabelMargin
            // 
            resources.ApplyResources(this.m_LabelMargin, "m_LabelMargin");
            this.m_LabelMargin.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelMargin.Name = "m_LabelMargin";
            // 
            // m_NumericUpDownMargin
            // 
            this.m_NumericUpDownMargin.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownMargin, "m_NumericUpDownMargin");
            this.m_NumericUpDownMargin.Name = "m_NumericUpDownMargin";
            this.m_NumericUpDownMargin.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxMeasureBeforePrint
            // 
            resources.ApplyResources(this.m_CheckBoxMeasureBeforePrint, "m_CheckBoxMeasureBeforePrint");
            this.m_CheckBoxMeasureBeforePrint.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxMeasureBeforePrint.Name = "m_CheckBoxMeasureBeforePrint";
            this.m_CheckBoxMeasureBeforePrint.UseVisualStyleBackColor = false;
            this.m_CheckBoxMeasureBeforePrint.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownPTASpraying
            // 
            this.m_NumericUpDownPTASpraying.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumericUpDownPTASpraying, "m_NumericUpDownPTASpraying");
            this.m_NumericUpDownPTASpraying.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_NumericUpDownPTASpraying.Name = "m_NumericUpDownPTASpraying";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownPTASpraying, resources.GetString("m_NumericUpDownPTASpraying.ToolTip"));
            // 
            // m_labelPrintPrespraytime
            // 
            resources.ApplyResources(this.m_labelPrintPrespraytime, "m_labelPrintPrespraytime");
            this.m_labelPrintPrespraytime.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPrintPrespraytime.Name = "m_labelPrintPrespraytime";
            // 
            // m_CheckBoxSprayBeforePrint
            // 
            resources.ApplyResources(this.m_CheckBoxSprayBeforePrint, "m_CheckBoxSprayBeforePrint");
            this.m_CheckBoxSprayBeforePrint.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxSprayBeforePrint.Name = "m_CheckBoxSprayBeforePrint";
            this.m_CheckBoxSprayBeforePrint.UseVisualStyleBackColor = false;
            this.m_CheckBoxSprayBeforePrint.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelAutoSpray
            // 
            resources.ApplyResources(this.m_LabelAutoSpray, "m_LabelAutoSpray");
            this.m_LabelAutoSpray.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoSpray.Name = "m_LabelAutoSpray";
            // 
            // m_NumericUpDownAutoSpray
            // 
            resources.ApplyResources(this.m_NumericUpDownAutoSpray, "m_NumericUpDownAutoSpray");
            this.m_NumericUpDownAutoSpray.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.m_NumericUpDownAutoSpray.Name = "m_NumericUpDownAutoSpray";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownAutoSpray, resources.GetString("m_NumericUpDownAutoSpray.ToolTip"));
            this.m_NumericUpDownAutoSpray.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownSprayCycle
            // 
            resources.ApplyResources(this.m_NumericUpDownSprayCycle, "m_NumericUpDownSprayCycle");
            this.m_NumericUpDownSprayCycle.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_NumericUpDownSprayCycle.Name = "m_NumericUpDownSprayCycle";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownSprayCycle, resources.GetString("m_NumericUpDownSprayCycle.ToolTip"));
            this.m_NumericUpDownSprayCycle.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelSprayCycle
            // 
            resources.ApplyResources(this.m_LabelSprayCycle, "m_LabelSprayCycle");
            this.m_LabelSprayCycle.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelSprayCycle.Name = "m_LabelSprayCycle";
            // 
            // m_CheckBoxIdleSpray
            // 
            resources.ApplyResources(this.m_CheckBoxIdleSpray, "m_CheckBoxIdleSpray");
            this.m_CheckBoxIdleSpray.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxIdleSpray.Name = "m_CheckBoxIdleSpray";
            this.m_CheckBoxIdleSpray.UseVisualStyleBackColor = false;
            this.m_CheckBoxIdleSpray.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownSprayTimes
            // 
            resources.ApplyResources(this.m_NumericUpDownSprayTimes, "m_NumericUpDownSprayTimes");
            this.m_NumericUpDownSprayTimes.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.m_NumericUpDownSprayTimes.Name = "m_NumericUpDownSprayTimes";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownSprayTimes, resources.GetString("m_NumericUpDownSprayTimes.ToolTip"));
            this.m_NumericUpDownSprayTimes.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelSprayTimes
            // 
            resources.ApplyResources(this.m_LabelSprayTimes, "m_LabelSprayTimes");
            this.m_LabelSprayTimes.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelSprayTimes.Name = "m_LabelSprayTimes";
            // 
            // m_GroupBoxPrintSetting
            // 
            resources.ApplyResources(this.m_GroupBoxPrintSetting, "m_GroupBoxPrintSetting");
            this.m_GroupBoxPrintSetting.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxPrintSetting.BorderThickness = 1F;
            this.m_GroupBoxPrintSetting.Controls.Add(this.panel2);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxAutoJumpWhite);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxYContinue);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownStepTime);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStepTime);
            style5.Color1 = System.Drawing.Color.LightBlue;
            style5.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxPrintSetting.GradientColors = style5;
            this.m_GroupBoxPrintSetting.GroupImage = null;
            this.m_GroupBoxPrintSetting.Name = "m_GroupBoxPrintSetting";
            this.m_GroupBoxPrintSetting.PaintGroupBox = false;
            this.m_GroupBoxPrintSetting.RoundCorners = 10;
            this.m_GroupBoxPrintSetting.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxPrintSetting.ShadowControl = false;
            this.m_GroupBoxPrintSetting.ShadowThickness = 3;
            this.m_GroupBoxPrintSetting.TabStop = false;
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxPrintSetting.TitileGradientColors = style6;
            this.m_GroupBoxPrintSetting.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxPrintSetting.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.checkBoxzCaliNoStep);
            this.panel2.Controls.Add(this.checkBoxOneStepSkipWhite);
            this.panel2.Controls.Add(this.checkBoxConstantStep);
            this.panel2.Controls.Add(this.panelFeatherPram);
            this.panel2.Controls.Add(this.checkBoxJointFeather);
            this.panel2.Controls.Add(this.panelYuDaBeltMachineParam);
            this.panel2.Controls.Add(this.m_LabelJobSpace);
            this.panel2.Controls.Add(this.checkBoxExquisiteFeather);
            this.panel2.Controls.Add(this.m_NumericUpDownJobSpace);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbo_MultipleInk);
            this.panel2.Name = "panel2";
            // 
            // checkBoxzCaliNoStep
            // 
            resources.ApplyResources(this.checkBoxzCaliNoStep, "checkBoxzCaliNoStep");
            this.checkBoxzCaliNoStep.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxzCaliNoStep.Name = "checkBoxzCaliNoStep";
            this.checkBoxzCaliNoStep.UseVisualStyleBackColor = false;
            // 
            // checkBoxOneStepSkipWhite
            // 
            resources.ApplyResources(this.checkBoxOneStepSkipWhite, "checkBoxOneStepSkipWhite");
            this.checkBoxOneStepSkipWhite.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxOneStepSkipWhite.Name = "checkBoxOneStepSkipWhite";
            this.checkBoxOneStepSkipWhite.UseVisualStyleBackColor = false;
            // 
            // checkBoxConstantStep
            // 
            resources.ApplyResources(this.checkBoxConstantStep, "checkBoxConstantStep");
            this.checkBoxConstantStep.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxConstantStep.Name = "checkBoxConstantStep";
            this.checkBoxConstantStep.UseVisualStyleBackColor = false;
            // 
            // panelFeatherPram
            // 
            this.panelFeatherPram.BackColor = System.Drawing.Color.Transparent;
            this.panelFeatherPram.Controls.Add(this.labelNozzleFeather);
            this.panelFeatherPram.Controls.Add(this.NumFeatherNozzle);
            this.panelFeatherPram.Controls.Add(this.textBoxFeather);
            this.panelFeatherPram.Controls.Add(this.label_FeatherType);
            this.panelFeatherPram.Controls.Add(this.comboBoxFeatherPercent);
            this.panelFeatherPram.Controls.Add(this.m_LabelFeather);
            this.panelFeatherPram.Controls.Add(this.m_NumericUpDownFeather);
            this.panelFeatherPram.Controls.Add(this.m_ComboBoxFeatherType);
            this.panelFeatherPram.Controls.Add(this.m_NmericUpDownWaveLen);
            this.panelFeatherPram.Controls.Add(this.m_NumericUpDownFeatherPercent);
            resources.ApplyResources(this.panelFeatherPram, "panelFeatherPram");
            this.panelFeatherPram.Name = "panelFeatherPram";
            // 
            // labelNozzleFeather
            // 
            resources.ApplyResources(this.labelNozzleFeather, "labelNozzleFeather");
            this.labelNozzleFeather.BackColor = System.Drawing.Color.Transparent;
            this.labelNozzleFeather.Name = "labelNozzleFeather";
            // 
            // NumFeatherNozzle
            // 
            resources.ApplyResources(this.NumFeatherNozzle, "NumFeatherNozzle");
            this.NumFeatherNozzle.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.NumFeatherNozzle.Name = "NumFeatherNozzle";
            this.m_ToolTip.SetToolTip(this.NumFeatherNozzle, resources.GetString("NumFeatherNozzle.ToolTip"));
            // 
            // textBoxFeather
            // 
            resources.ApplyResources(this.textBoxFeather, "textBoxFeather");
            this.textBoxFeather.Name = "textBoxFeather";
            // 
            // label_FeatherType
            // 
            resources.ApplyResources(this.label_FeatherType, "label_FeatherType");
            this.label_FeatherType.BackColor = System.Drawing.Color.Transparent;
            this.label_FeatherType.Name = "label_FeatherType";
            // 
            // comboBoxFeatherPercent
            // 
            this.comboBoxFeatherPercent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxFeatherPercent, "comboBoxFeatherPercent");
            this.comboBoxFeatherPercent.Name = "comboBoxFeatherPercent";
            this.comboBoxFeatherPercent.SelectedIndexChanged += new System.EventHandler(this.comboBoxFeatherPercent_SelectedIndexChanged);
            // 
            // m_LabelFeather
            // 
            resources.ApplyResources(this.m_LabelFeather, "m_LabelFeather");
            this.m_LabelFeather.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelFeather.Name = "m_LabelFeather";
            // 
            // m_NumericUpDownFeather
            // 
            resources.ApplyResources(this.m_NumericUpDownFeather, "m_NumericUpDownFeather");
            this.m_NumericUpDownFeather.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.m_NumericUpDownFeather.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownFeather.Name = "m_NumericUpDownFeather";
            this.m_NumericUpDownFeather.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxFeatherType
            // 
            this.m_ComboBoxFeatherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxFeatherType, "m_ComboBoxFeatherType");
            this.m_ComboBoxFeatherType.Name = "m_ComboBoxFeatherType";
            this.m_ComboBoxFeatherType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxFeatherType_SelectedIndexChanged);
            // 
            // m_NmericUpDownWaveLen
            // 
            this.m_NmericUpDownWaveLen.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NmericUpDownWaveLen, "m_NmericUpDownWaveLen");
            this.m_NmericUpDownWaveLen.Name = "m_NmericUpDownWaveLen";
            this.m_ToolTip.SetToolTip(this.m_NmericUpDownWaveLen, resources.GetString("m_NmericUpDownWaveLen.ToolTip"));
            // 
            // m_NumericUpDownFeatherPercent
            // 
            resources.ApplyResources(this.m_NumericUpDownFeatherPercent, "m_NumericUpDownFeatherPercent");
            this.m_NumericUpDownFeatherPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownFeatherPercent.Name = "m_NumericUpDownFeatherPercent";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownFeatherPercent, resources.GetString("m_NumericUpDownFeatherPercent.ToolTip"));
            // 
            // checkBoxJointFeather
            // 
            resources.ApplyResources(this.checkBoxJointFeather, "checkBoxJointFeather");
            this.checkBoxJointFeather.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxJointFeather.Name = "checkBoxJointFeather";
            this.checkBoxJointFeather.UseVisualStyleBackColor = false;
            // 
            // panelYuDaBeltMachineParam
            // 
            this.panelYuDaBeltMachineParam.BackColor = System.Drawing.Color.Transparent;
            this.panelYuDaBeltMachineParam.Controls.Add(this.numericPlatformCorrect);
            this.panelYuDaBeltMachineParam.Controls.Add(this.labelPlatformCorrect);
            resources.ApplyResources(this.panelYuDaBeltMachineParam, "panelYuDaBeltMachineParam");
            this.panelYuDaBeltMachineParam.Name = "panelYuDaBeltMachineParam";
            // 
            // numericPlatformCorrect
            // 
            this.numericPlatformCorrect.DecimalPlaces = 2;
            resources.ApplyResources(this.numericPlatformCorrect, "numericPlatformCorrect");
            this.numericPlatformCorrect.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericPlatformCorrect.Name = "numericPlatformCorrect";
            // 
            // labelPlatformCorrect
            // 
            resources.ApplyResources(this.labelPlatformCorrect, "labelPlatformCorrect");
            this.labelPlatformCorrect.Name = "labelPlatformCorrect";
            // 
            // m_LabelJobSpace
            // 
            resources.ApplyResources(this.m_LabelJobSpace, "m_LabelJobSpace");
            this.m_LabelJobSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelJobSpace.Name = "m_LabelJobSpace";
            // 
            // checkBoxExquisiteFeather
            // 
            resources.ApplyResources(this.checkBoxExquisiteFeather, "checkBoxExquisiteFeather");
            this.checkBoxExquisiteFeather.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxExquisiteFeather.Name = "checkBoxExquisiteFeather";
            this.checkBoxExquisiteFeather.UseVisualStyleBackColor = false;
            this.checkBoxExquisiteFeather.CheckedChanged += new System.EventHandler(this.checkBoxExquisiteFeather_CheckedChanged);
            // 
            // m_NumericUpDownJobSpace
            // 
            this.m_NumericUpDownJobSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownJobSpace, "m_NumericUpDownJobSpace");
            this.m_NumericUpDownJobSpace.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownJobSpace.Name = "m_NumericUpDownJobSpace";
            this.m_NumericUpDownJobSpace.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // cbo_MultipleInk
            // 
            resources.ApplyResources(this.cbo_MultipleInk, "cbo_MultipleInk");
            this.cbo_MultipleInk.Items.AddRange(new object[] {
            resources.GetString("cbo_MultipleInk.Items"),
            resources.GetString("cbo_MultipleInk.Items1"),
            resources.GetString("cbo_MultipleInk.Items2"),
            resources.GetString("cbo_MultipleInk.Items3"),
            resources.GetString("cbo_MultipleInk.Items4"),
            resources.GetString("cbo_MultipleInk.Items5"),
            resources.GetString("cbo_MultipleInk.Items6"),
            resources.GetString("cbo_MultipleInk.Items7")});
            this.cbo_MultipleInk.Name = "cbo_MultipleInk";
            // 
            // m_CheckBoxAutoJumpWhite
            // 
            this.m_CheckBoxAutoJumpWhite.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxAutoJumpWhite, "m_CheckBoxAutoJumpWhite");
            this.m_CheckBoxAutoJumpWhite.Name = "m_CheckBoxAutoJumpWhite";
            this.m_CheckBoxAutoJumpWhite.UseVisualStyleBackColor = false;
            this.m_CheckBoxAutoJumpWhite.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxYContinue
            // 
            this.m_CheckBoxYContinue.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxYContinue, "m_CheckBoxYContinue");
            this.m_CheckBoxYContinue.Name = "m_CheckBoxYContinue";
            this.m_CheckBoxYContinue.UseVisualStyleBackColor = false;
            this.m_CheckBoxYContinue.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownStepTime
            // 
            this.m_NumericUpDownStepTime.DecimalPlaces = 1;
            this.m_NumericUpDownStepTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.m_NumericUpDownStepTime, "m_NumericUpDownStepTime");
            this.m_NumericUpDownStepTime.Name = "m_NumericUpDownStepTime";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownStepTime, resources.GetString("m_NumericUpDownStepTime.ToolTip"));
            this.m_NumericUpDownStepTime.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelStepTime
            // 
            resources.ApplyResources(this.m_LabelStepTime, "m_LabelStepTime");
            this.m_LabelStepTime.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStepTime.Name = "m_LabelStepTime";
            // 
            // panelUVoffsetDistance
            // 
            this.panelUVoffsetDistance.BackColor = System.Drawing.Color.Transparent;
            this.panelUVoffsetDistance.Controls.Add(this.numUVoffsetDistance);
            this.panelUVoffsetDistance.Controls.Add(this.labelUVoffsetDistance);
            resources.ApplyResources(this.panelUVoffsetDistance, "panelUVoffsetDistance");
            this.panelUVoffsetDistance.Name = "panelUVoffsetDistance";
            // 
            // numUVoffsetDistance
            // 
            this.numUVoffsetDistance.DecimalPlaces = 2;
            resources.ApplyResources(this.numUVoffsetDistance, "numUVoffsetDistance");
            this.numUVoffsetDistance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numUVoffsetDistance.Name = "numUVoffsetDistance";
            this.numUVoffsetDistance.ValueChanged += new System.EventHandler(this.numUVoffsetDistance_ValueChanged);
            // 
            // labelUVoffsetDistance
            // 
            resources.ApplyResources(this.labelUVoffsetDistance, "labelUVoffsetDistance");
            this.labelUVoffsetDistance.Name = "labelUVoffsetDistance";
            // 
            // m_GroupBoxZ
            // 
            resources.ApplyResources(this.m_GroupBoxZ, "m_GroupBoxZ");
            this.m_GroupBoxZ.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxZ.BorderThickness = 1F;
            this.m_GroupBoxZ.Controls.Add(this.panelZCleanAndWet);
            this.m_GroupBoxZ.Controls.Add(this.panelZWorkPos2);
            this.m_GroupBoxZ.Controls.Add(this.panelZWorkPos);
            this.m_GroupBoxZ.Controls.Add(this.panelZMove);
            style7.Color1 = System.Drawing.Color.LightBlue;
            style7.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxZ.GradientColors = style7;
            this.m_GroupBoxZ.GroupImage = null;
            this.m_GroupBoxZ.Name = "m_GroupBoxZ";
            this.m_GroupBoxZ.PaintGroupBox = false;
            this.m_GroupBoxZ.RoundCorners = 10;
            this.m_GroupBoxZ.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxZ.ShadowControl = false;
            this.m_GroupBoxZ.ShadowThickness = 3;
            this.m_GroupBoxZ.TabStop = false;
            style8.Color1 = System.Drawing.Color.LightBlue;
            style8.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxZ.TitileGradientColors = style8;
            this.m_GroupBoxZ.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxZ.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // panelZCleanAndWet
            // 
            this.panelZCleanAndWet.BackColor = System.Drawing.Color.Transparent;
            this.panelZCleanAndWet.Controls.Add(this.label6);
            this.panelZCleanAndWet.Controls.Add(this.numericUpDownBSZPos);
            this.panelZCleanAndWet.Controls.Add(this.numericUpDownCleanZPos);
            this.panelZCleanAndWet.Controls.Add(this.label5);
            resources.ApplyResources(this.panelZCleanAndWet, "panelZCleanAndWet");
            this.panelZCleanAndWet.Name = "panelZCleanAndWet";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // numericUpDownBSZPos
            // 
            this.numericUpDownBSZPos.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownBSZPos, "numericUpDownBSZPos");
            this.numericUpDownBSZPos.Name = "numericUpDownBSZPos";
            // 
            // numericUpDownCleanZPos
            // 
            this.numericUpDownCleanZPos.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownCleanZPos, "numericUpDownCleanZPos");
            this.numericUpDownCleanZPos.Name = "numericUpDownCleanZPos";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // panelZWorkPos2
            // 
            this.panelZWorkPos2.BackColor = System.Drawing.Color.Transparent;
            this.panelZWorkPos2.Controls.Add(this.buttonMoveToWorkPos2);
            this.panelZWorkPos2.Controls.Add(this.numZWorkPos2);
            this.panelZWorkPos2.Controls.Add(this.label17);
            resources.ApplyResources(this.panelZWorkPos2, "panelZWorkPos2");
            this.panelZWorkPos2.Name = "panelZWorkPos2";
            // 
            // buttonMoveToWorkPos2
            // 
            resources.ApplyResources(this.buttonMoveToWorkPos2, "buttonMoveToWorkPos2");
            this.buttonMoveToWorkPos2.Name = "buttonMoveToWorkPos2";
            this.buttonMoveToWorkPos2.Click += new System.EventHandler(this.buttonMoveToWorkPos2_Click);
            // 
            // numZWorkPos2
            // 
            this.numZWorkPos2.DecimalPlaces = 1;
            resources.ApplyResources(this.numZWorkPos2, "numZWorkPos2");
            this.numZWorkPos2.Name = "numZWorkPos2";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Name = "label17";
            // 
            // panelZWorkPos
            // 
            this.panelZWorkPos.BackColor = System.Drawing.Color.Transparent;
            this.panelZWorkPos.Controls.Add(this.buttonMoveToWorkPos);
            this.panelZWorkPos.Controls.Add(this.numZWorkPos);
            this.panelZWorkPos.Controls.Add(this.labelZWorkPos);
            resources.ApplyResources(this.panelZWorkPos, "panelZWorkPos");
            this.panelZWorkPos.Name = "panelZWorkPos";
            // 
            // buttonMoveToWorkPos
            // 
            resources.ApplyResources(this.buttonMoveToWorkPos, "buttonMoveToWorkPos");
            this.buttonMoveToWorkPos.Name = "buttonMoveToWorkPos";
            this.buttonMoveToWorkPos.Click += new System.EventHandler(this.buttonMoveToWorkPos_Click);
            // 
            // numZWorkPos
            // 
            this.numZWorkPos.DecimalPlaces = 1;
            resources.ApplyResources(this.numZWorkPos, "numZWorkPos");
            this.numZWorkPos.Name = "numZWorkPos";
            // 
            // labelZWorkPos
            // 
            resources.ApplyResources(this.labelZWorkPos, "labelZWorkPos");
            this.labelZWorkPos.BackColor = System.Drawing.Color.Transparent;
            this.labelZWorkPos.Name = "labelZWorkPos";
            // 
            // panelZMove
            // 
            this.panelZMove.BackColor = System.Drawing.Color.Transparent;
            this.panelZMove.Controls.Add(this.label18);
            this.panelZMove.Controls.Add(this.numZMaxLen);
            this.panelZMove.Controls.Add(this.m_LabelZSpace);
            this.panelZMove.Controls.Add(this.m_NumericUpDownZSpace);
            this.panelZMove.Controls.Add(this.m_ButtonZManual);
            this.panelZMove.Controls.Add(this.m_NumericUpDownThickness);
            this.panelZMove.Controls.Add(this.m_ButtonMeasureMaxZ);
            this.panelZMove.Controls.Add(this.m_LabelMediaThickness);
            resources.ApplyResources(this.panelZMove, "panelZMove");
            this.panelZMove.Name = "panelZMove";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Name = "label18";
            // 
            // numZMaxLen
            // 
            this.numZMaxLen.DecimalPlaces = 1;
            resources.ApplyResources(this.numZMaxLen, "numZMaxLen");
            this.numZMaxLen.Name = "numZMaxLen";
            // 
            // m_LabelZSpace
            // 
            resources.ApplyResources(this.m_LabelZSpace, "m_LabelZSpace");
            this.m_LabelZSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelZSpace.Name = "m_LabelZSpace";
            // 
            // m_NumericUpDownZSpace
            // 
            this.m_NumericUpDownZSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownZSpace, "m_NumericUpDownZSpace");
            this.m_NumericUpDownZSpace.Name = "m_NumericUpDownZSpace";
            this.m_NumericUpDownZSpace.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ButtonZManual
            // 
            resources.ApplyResources(this.m_ButtonZManual, "m_ButtonZManual");
            this.m_ButtonZManual.Name = "m_ButtonZManual";
            this.m_ButtonZManual.Click += new System.EventHandler(this.m_ButtonZManual_Click);
            // 
            // m_NumericUpDownThickness
            // 
            this.m_NumericUpDownThickness.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownThickness, "m_NumericUpDownThickness");
            this.m_NumericUpDownThickness.Name = "m_NumericUpDownThickness";
            this.m_NumericUpDownThickness.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ButtonMeasureMaxZ
            // 
            resources.ApplyResources(this.m_ButtonMeasureMaxZ, "m_ButtonMeasureMaxZ");
            this.m_ButtonMeasureMaxZ.Name = "m_ButtonMeasureMaxZ";
            this.m_ButtonMeasureMaxZ.Click += new System.EventHandler(this.m_ButtonZAuto_Click);
            // 
            // m_LabelMediaThickness
            // 
            resources.ApplyResources(this.m_LabelMediaThickness, "m_LabelMediaThickness");
            this.m_LabelMediaThickness.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelMediaThickness.Name = "m_LabelMediaThickness";
            // 
            // m_CheckBoxKillBidir
            // 
            resources.ApplyResources(this.m_CheckBoxKillBidir, "m_CheckBoxKillBidir");
            this.m_CheckBoxKillBidir.Name = "m_CheckBoxKillBidir";
            this.m_CheckBoxKillBidir.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxAutoYCalibrate
            // 
            resources.ApplyResources(this.m_CheckBoxAutoYCalibrate, "m_CheckBoxAutoYCalibrate");
            this.m_CheckBoxAutoYCalibrate.Name = "m_CheckBoxAutoYCalibrate";
            this.m_CheckBoxAutoYCalibrate.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // numManualSprayFreq
            // 
            resources.ApplyResources(this.numManualSprayFreq, "numManualSprayFreq");
            this.numManualSprayFreq.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numManualSprayFreq.Name = "numManualSprayFreq";
            this.m_ToolTip.SetToolTip(this.numManualSprayFreq, resources.GetString("numManualSprayFreq.ToolTip"));
            // 
            // numManualSprayPeriod
            // 
            resources.ApplyResources(this.numManualSprayPeriod, "numManualSprayPeriod");
            this.numManualSprayPeriod.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numManualSprayPeriod.Name = "numManualSprayPeriod";
            this.m_ToolTip.SetToolTip(this.numManualSprayPeriod, resources.GetString("numManualSprayPeriod.ToolTip"));
            // 
            // m_NumericUpDownCleanTimes
            // 
            resources.ApplyResources(this.m_NumericUpDownCleanTimes, "m_NumericUpDownCleanTimes");
            this.m_NumericUpDownCleanTimes.Name = "m_NumericUpDownCleanTimes";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownCleanTimes, resources.GetString("m_NumericUpDownCleanTimes.ToolTip"));
            this.m_NumericUpDownCleanTimes.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownAutoClean
            // 
            resources.ApplyResources(this.m_NumericUpDownAutoClean, "m_NumericUpDownAutoClean");
            this.m_NumericUpDownAutoClean.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.m_NumericUpDownAutoClean.Name = "m_NumericUpDownAutoClean";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownAutoClean, resources.GetString("m_NumericUpDownAutoClean.ToolTip"));
            this.m_NumericUpDownAutoClean.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownPTACleaning
            // 
            resources.ApplyResources(this.m_NumericUpDownPTACleaning, "m_NumericUpDownPTACleaning");
            this.m_NumericUpDownPTACleaning.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_NumericUpDownPTACleaning.Name = "m_NumericUpDownPTACleaning";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownPTACleaning, resources.GetString("m_NumericUpDownPTACleaning.ToolTip"));
            // 
            // numSuctionTimes
            // 
            resources.ApplyResources(this.numSuctionTimes, "numSuctionTimes");
            this.numSuctionTimes.Name = "numSuctionTimes";
            this.m_ToolTip.SetToolTip(this.numSuctionTimes, resources.GetString("numSuctionTimes.ToolTip"));
            // 
            // numHumidInterval
            // 
            resources.ApplyResources(this.numHumidInterval, "numHumidInterval");
            this.numHumidInterval.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numHumidInterval.Name = "numHumidInterval";
            this.m_ToolTip.SetToolTip(this.numHumidInterval, resources.GetString("numHumidInterval.ToolTip"));
            // 
            // numCycleTime
            // 
            this.numCycleTime.DecimalPlaces = 2;
            resources.ApplyResources(this.numCycleTime, "numCycleTime");
            this.numCycleTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numCycleTime.Name = "numCycleTime";
            this.m_ToolTip.SetToolTip(this.numCycleTime, resources.GetString("numCycleTime.ToolTip"));
            // 
            // numPulseTime
            // 
            this.numPulseTime.DecimalPlaces = 2;
            resources.ApplyResources(this.numPulseTime, "numPulseTime");
            this.numPulseTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numPulseTime.Name = "numPulseTime";
            this.m_ToolTip.SetToolTip(this.numPulseTime, resources.GetString("numPulseTime.ToolTip"));
            // 
            // numStirCycleTime
            // 
            resources.ApplyResources(this.numStirCycleTime, "numStirCycleTime");
            this.numStirCycleTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numStirCycleTime.Name = "numStirCycleTime";
            this.m_ToolTip.SetToolTip(this.numStirCycleTime, resources.GetString("numStirCycleTime.ToolTip"));
            // 
            // numStirPulseTime
            // 
            resources.ApplyResources(this.numStirPulseTime, "numStirPulseTime");
            this.numStirPulseTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numStirPulseTime.Name = "numStirPulseTime";
            this.m_ToolTip.SetToolTip(this.numStirPulseTime, resources.GetString("numStirPulseTime.ToolTip"));
            // 
            // numericUpDown_HumanCleanXpos
            // 
            resources.ApplyResources(this.numericUpDown_HumanCleanXpos, "numericUpDown_HumanCleanXpos");
            this.numericUpDown_HumanCleanXpos.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown_HumanCleanXpos.Name = "numericUpDown_HumanCleanXpos";
            this.m_ToolTip.SetToolTip(this.numericUpDown_HumanCleanXpos, resources.GetString("numericUpDown_HumanCleanXpos.ToolTip"));
            // 
            // numericUpDown_HumanScarperLen
            // 
            resources.ApplyResources(this.numericUpDown_HumanScarperLen, "numericUpDown_HumanScarperLen");
            this.numericUpDown_HumanScarperLen.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown_HumanScarperLen.Name = "numericUpDown_HumanScarperLen";
            this.m_ToolTip.SetToolTip(this.numericUpDown_HumanScarperLen, resources.GetString("numericUpDown_HumanScarperLen.ToolTip"));
            // 
            // numericUpDown_HumanRecoverTime
            // 
            this.numericUpDown_HumanRecoverTime.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDown_HumanRecoverTime, "numericUpDown_HumanRecoverTime");
            this.numericUpDown_HumanRecoverTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown_HumanRecoverTime.Name = "numericUpDown_HumanRecoverTime";
            this.m_ToolTip.SetToolTip(this.numericUpDown_HumanRecoverTime, resources.GetString("numericUpDown_HumanRecoverTime.ToolTip"));
            // 
            // numericUpDown_HumanPressInkTime
            // 
            this.numericUpDown_HumanPressInkTime.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDown_HumanPressInkTime, "numericUpDown_HumanPressInkTime");
            this.numericUpDown_HumanPressInkTime.Maximum = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.numericUpDown_HumanPressInkTime.Name = "numericUpDown_HumanPressInkTime";
            this.m_ToolTip.SetToolTip(this.numericUpDown_HumanPressInkTime, resources.GetString("numericUpDown_HumanPressInkTime.ToolTip"));
            // 
            // m_GroupBoxSpray
            // 
            resources.ApplyResources(this.m_GroupBoxSpray, "m_GroupBoxSpray");
            this.m_GroupBoxSpray.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxSpray.BorderThickness = 1F;
            this.m_GroupBoxSpray.Controls.Add(this.checkBoxBSUseSpray);
            this.m_GroupBoxSpray.Controls.Add(this.checkBoxUseHighParam);
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelAutoSpray);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownSprayTimes);
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelSprayTimes);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownAutoSpray);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownSprayCycle);
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelSprayCycle);
            this.m_GroupBoxSpray.Controls.Add(this.m_CheckBoxIdleSpray);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownPTASpraying);
            this.m_GroupBoxSpray.Controls.Add(this.m_labelPrintPrespraytime);
            this.m_GroupBoxSpray.Controls.Add(this.m_CheckBoxSprayBeforePrint);
            style9.Color1 = System.Drawing.Color.LightBlue;
            style9.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxSpray.GradientColors = style9;
            this.m_GroupBoxSpray.GroupImage = null;
            this.m_GroupBoxSpray.Name = "m_GroupBoxSpray";
            this.m_GroupBoxSpray.PaintGroupBox = false;
            this.m_GroupBoxSpray.RoundCorners = 10;
            this.m_GroupBoxSpray.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxSpray.ShadowControl = false;
            this.m_GroupBoxSpray.ShadowThickness = 3;
            this.m_GroupBoxSpray.TabStop = false;
            style10.Color1 = System.Drawing.Color.LightBlue;
            style10.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxSpray.TitileGradientColors = style10;
            this.m_GroupBoxSpray.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxSpray.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxBSUseSpray
            // 
            resources.ApplyResources(this.checkBoxBSUseSpray, "checkBoxBSUseSpray");
            this.checkBoxBSUseSpray.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxBSUseSpray.Name = "checkBoxBSUseSpray";
            this.checkBoxBSUseSpray.UseVisualStyleBackColor = false;
            // 
            // checkBoxUseHighParam
            // 
            resources.ApplyResources(this.checkBoxUseHighParam, "checkBoxUseHighParam");
            this.checkBoxUseHighParam.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseHighParam.Name = "checkBoxUseHighParam";
            this.checkBoxUseHighParam.UseVisualStyleBackColor = false;
            // 
            // m_ComboBoxDiv
            // 
            resources.ApplyResources(this.m_ComboBoxDiv, "m_ComboBoxDiv");
            this.m_ComboBoxDiv.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxDiv.Items"),
            resources.GetString("m_ComboBoxDiv.Items1")});
            this.m_ComboBoxDiv.Name = "m_ComboBoxDiv";
            this.m_ComboBoxDiv.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxNozzleClogging
            // 
            this.m_CheckBoxNozzleClogging.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxNozzleClogging, "m_CheckBoxNozzleClogging");
            this.m_CheckBoxNozzleClogging.Name = "m_CheckBoxNozzleClogging";
            this.m_CheckBoxNozzleClogging.UseVisualStyleBackColor = false;
            this.m_CheckBoxNozzleClogging.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panelFlatDistanceY
            // 
            this.panelFlatDistanceY.BackColor = System.Drawing.Color.Transparent;
            this.panelFlatDistanceY.Controls.Add(this.numFlatDistanceY);
            this.panelFlatDistanceY.Controls.Add(this.label14);
            resources.ApplyResources(this.panelFlatDistanceY, "panelFlatDistanceY");
            this.panelFlatDistanceY.Name = "panelFlatDistanceY";
            // 
            // numFlatDistanceY
            // 
            resources.ApplyResources(this.numFlatDistanceY, "numFlatDistanceY");
            this.numFlatDistanceY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numFlatDistanceY.Name = "numFlatDistanceY";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Name = "label14";
            // 
            // panelXDiv
            // 
            this.panelXDiv.BackColor = System.Drawing.Color.Transparent;
            this.panelXDiv.Controls.Add(this.m_ComboBoxDiv);
            this.panelXDiv.Controls.Add(this.label2);
            resources.ApplyResources(this.panelXDiv, "panelXDiv");
            this.panelXDiv.Name = "panelXDiv";
            // 
            // panelStablecolorPixelStep
            // 
            this.panelStablecolorPixelStep.BackColor = System.Drawing.Color.Transparent;
            this.panelStablecolorPixelStep.Controls.Add(this.checkBoxPixelStep);
            this.panelStablecolorPixelStep.Controls.Add(this.m_CheckBoxKillBidir);
            resources.ApplyResources(this.panelStablecolorPixelStep, "panelStablecolorPixelStep");
            this.panelStablecolorPixelStep.Name = "panelStablecolorPixelStep";
            // 
            // checkBoxPixelStep
            // 
            resources.ApplyResources(this.checkBoxPixelStep, "checkBoxPixelStep");
            this.checkBoxPixelStep.Name = "checkBoxPixelStep";
            this.checkBoxPixelStep.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_GroupBoxZMeasure
            // 
            resources.ApplyResources(this.m_GroupBoxZMeasure, "m_GroupBoxZMeasure");
            this.m_GroupBoxZMeasure.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxZMeasure.BorderThickness = 1F;
            this.m_GroupBoxZMeasure.Controls.Add(this.numericUpDownHeadToPaper);
            this.m_GroupBoxZMeasure.Controls.Add(this.numericUpDownMesureHeight);
            this.m_GroupBoxZMeasure.Controls.Add(this.label3);
            this.m_GroupBoxZMeasure.Controls.Add(this.label4);
            style11.Color1 = System.Drawing.Color.LightBlue;
            style11.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxZMeasure.GradientColors = style11;
            this.m_GroupBoxZMeasure.GroupImage = null;
            this.m_GroupBoxZMeasure.Name = "m_GroupBoxZMeasure";
            this.m_GroupBoxZMeasure.PaintGroupBox = false;
            this.m_GroupBoxZMeasure.RoundCorners = 10;
            this.m_GroupBoxZMeasure.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxZMeasure.ShadowControl = false;
            this.m_GroupBoxZMeasure.ShadowThickness = 3;
            this.m_GroupBoxZMeasure.TabStop = false;
            style12.Color1 = System.Drawing.Color.LightBlue;
            style12.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxZMeasure.TitileGradientColors = style12;
            this.m_GroupBoxZMeasure.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxZMeasure.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // numericUpDownHeadToPaper
            // 
            this.numericUpDownHeadToPaper.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownHeadToPaper, "numericUpDownHeadToPaper");
            this.numericUpDownHeadToPaper.Name = "numericUpDownHeadToPaper";
            // 
            // numericUpDownMesureHeight
            // 
            this.numericUpDownMesureHeight.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownMesureHeight, "numericUpDownMesureHeight");
            this.numericUpDownMesureHeight.Name = "numericUpDownMesureHeight";
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
            // grouper1
            // 
            resources.ApplyResources(this.grouper1, "grouper1");
            this.grouper1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.radioButton4);
            this.grouper1.Controls.Add(this.radioButton3);
            this.grouper1.Controls.Add(this.radioButton2);
            this.grouper1.Controls.Add(this.radioButton1);
            style13.Color1 = System.Drawing.Color.LightBlue;
            style13.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper1.GradientColors = style13;
            this.grouper1.GroupImage = null;
            this.grouper1.Name = "grouper1";
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.TabStop = false;
            style14.Color1 = System.Drawing.Color.LightBlue;
            style14.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper1.TitileGradientColors = style14;
            this.grouper1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper1.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // radioButton4
            // 
            this.radioButton4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButton4, "radioButton4");
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.TabStop = true;
            this.radioButton4.UseVisualStyleBackColor = false;
            // 
            // radioButton3
            // 
            this.radioButton3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // radioButton1
            // 
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // grouperManualSpray
            // 
            resources.ApplyResources(this.grouperManualSpray, "grouperManualSpray");
            this.grouperManualSpray.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperManualSpray.BorderThickness = 1F;
            this.grouperManualSpray.Controls.Add(this.labelManualSprayFreq);
            this.grouperManualSpray.Controls.Add(this.numManualSprayFreq);
            this.grouperManualSpray.Controls.Add(this.numManualSprayPeriod);
            this.grouperManualSpray.Controls.Add(this.labelManualSprayPeriod);
            style15.Color1 = System.Drawing.Color.LightBlue;
            style15.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperManualSpray.GradientColors = style15;
            this.grouperManualSpray.GroupImage = null;
            this.grouperManualSpray.Name = "grouperManualSpray";
            this.grouperManualSpray.PaintGroupBox = false;
            this.grouperManualSpray.RoundCorners = 10;
            this.grouperManualSpray.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperManualSpray.ShadowControl = false;
            this.grouperManualSpray.ShadowThickness = 3;
            this.grouperManualSpray.TabStop = false;
            style16.Color1 = System.Drawing.Color.LightBlue;
            style16.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperManualSpray.TitileGradientColors = style16;
            this.grouperManualSpray.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperManualSpray.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // labelManualSprayFreq
            // 
            resources.ApplyResources(this.labelManualSprayFreq, "labelManualSprayFreq");
            this.labelManualSprayFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelManualSprayFreq.Name = "labelManualSprayFreq";
            // 
            // labelManualSprayPeriod
            // 
            resources.ApplyResources(this.labelManualSprayPeriod, "labelManualSprayPeriod");
            this.labelManualSprayPeriod.BackColor = System.Drawing.Color.Transparent;
            this.labelManualSprayPeriod.Name = "labelManualSprayPeriod";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.m_grouperPrintDir);
            this.panelLeft.Controls.Add(this.panel1);
            this.panelLeft.Controls.Add(this.grouper2);
            this.panelLeft.Controls.Add(this.panel13);
            this.panelLeft.Controls.Add(this.panel5);
            this.panelLeft.Controls.Add(this.m_GroupBoxMedia);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Controls.Add(this.m_GroupBoxPrintSetting);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // m_grouperPrintDir
            // 
            resources.ApplyResources(this.m_grouperPrintDir, "m_grouperPrintDir");
            this.m_grouperPrintDir.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_grouperPrintDir.BorderThickness = 1F;
            this.m_grouperPrintDir.Controls.Add(this.CheckBoxReversePrint);
            this.m_grouperPrintDir.Controls.Add(this.m_CheckBoxMirror);
            style17.Color1 = System.Drawing.Color.LightBlue;
            style17.Color2 = System.Drawing.Color.SteelBlue;
            this.m_grouperPrintDir.GradientColors = style17;
            this.m_grouperPrintDir.GroupImage = null;
            this.m_grouperPrintDir.Name = "m_grouperPrintDir";
            this.m_grouperPrintDir.PaintGroupBox = false;
            this.m_grouperPrintDir.RoundCorners = 10;
            this.m_grouperPrintDir.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_grouperPrintDir.ShadowControl = false;
            this.m_grouperPrintDir.ShadowThickness = 3;
            this.m_grouperPrintDir.TabStop = false;
            style18.Color1 = System.Drawing.Color.LightBlue;
            style18.Color2 = System.Drawing.Color.SteelBlue;
            this.m_grouperPrintDir.TitileGradientColors = style18;
            this.m_grouperPrintDir.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_grouperPrintDir.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // CheckBoxReversePrint
            // 
            this.CheckBoxReversePrint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.CheckBoxReversePrint, "CheckBoxReversePrint");
            this.CheckBoxReversePrint.Name = "CheckBoxReversePrint";
            this.CheckBoxReversePrint.UseVisualStyleBackColor = false;
            // 
            // m_CheckBoxMirror
            // 
            this.m_CheckBoxMirror.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxMirror, "m_CheckBoxMirror");
            this.m_CheckBoxMirror.Name = "m_CheckBoxMirror";
            this.m_CheckBoxMirror.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // grouper2
            // 
            this.grouper2.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.labelRollLength);
            this.grouper2.Controls.Add(this.numRollLength);
            this.grouper2.Controls.Add(this.buttonFastCalculate);
            this.grouper2.Controls.Add(this.checkBoxEnableDetectRemainingRoll);
            resources.ApplyResources(this.grouper2, "grouper2");
            style19.Color1 = System.Drawing.Color.LightBlue;
            style19.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper2.GradientColors = style19;
            this.grouper2.GroupImage = null;
            this.grouper2.Name = "grouper2";
            this.grouper2.PaintGroupBox = false;
            this.grouper2.RoundCorners = 10;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.TabStop = false;
            style20.Color1 = System.Drawing.Color.LightBlue;
            style20.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper2.TitileGradientColors = style20;
            this.grouper2.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper2.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // labelRollLength
            // 
            resources.ApplyResources(this.labelRollLength, "labelRollLength");
            this.labelRollLength.BackColor = System.Drawing.Color.Transparent;
            this.labelRollLength.Name = "labelRollLength";
            // 
            // numRollLength
            // 
            this.numRollLength.DecimalPlaces = 3;
            resources.ApplyResources(this.numRollLength, "numRollLength");
            this.numRollLength.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numRollLength.Name = "numRollLength";
            // 
            // buttonFastCalculate
            // 
            resources.ApplyResources(this.buttonFastCalculate, "buttonFastCalculate");
            this.buttonFastCalculate.Name = "buttonFastCalculate";
            this.buttonFastCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // checkBoxEnableDetectRemainingRoll
            // 
            resources.ApplyResources(this.checkBoxEnableDetectRemainingRoll, "checkBoxEnableDetectRemainingRoll");
            this.checkBoxEnableDetectRemainingRoll.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableDetectRemainingRoll.Name = "checkBoxEnableDetectRemainingRoll";
            this.checkBoxEnableDetectRemainingRoll.UseVisualStyleBackColor = false;
            this.checkBoxEnableDetectRemainingRoll.CheckedChanged += new System.EventHandler(this.checkBoxEnableDetectRemainingRoll_CheckedChanged);
            // 
            // panel13
            // 
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // checkBoxClosedLoopControl
            // 
            this.checkBoxClosedLoopControl.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxClosedLoopControl, "checkBoxClosedLoopControl");
            this.checkBoxClosedLoopControl.Name = "checkBoxClosedLoopControl";
            this.checkBoxClosedLoopControl.UseVisualStyleBackColor = false;
            // 
            // grouperUVCureSetting
            // 
            this.grouperUVCureSetting.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperUVCureSetting.BorderThickness = 1F;
            this.grouperUVCureSetting.Controls.Add(this.checkBoxBackBeforPrintUV);
            this.grouperUVCureSetting.Controls.Add(this.checkBoxRunAfterPrintingUV);
            this.grouperUVCureSetting.Controls.Add(this.panelUVLightInAdvance);
            this.grouperUVCureSetting.Controls.Add(this.panelUVoffsetDistance);
            style21.Color1 = System.Drawing.Color.LightBlue;
            style21.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperUVCureSetting.GradientColors = style21;
            this.grouperUVCureSetting.GroupImage = null;
            resources.ApplyResources(this.grouperUVCureSetting, "grouperUVCureSetting");
            this.grouperUVCureSetting.Name = "grouperUVCureSetting";
            this.grouperUVCureSetting.PaintGroupBox = false;
            this.grouperUVCureSetting.RoundCorners = 10;
            this.grouperUVCureSetting.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperUVCureSetting.ShadowControl = false;
            this.grouperUVCureSetting.ShadowThickness = 3;
            this.grouperUVCureSetting.TabStop = false;
            style22.Color1 = System.Drawing.Color.LightBlue;
            style22.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperUVCureSetting.TitileGradientColors = style22;
            this.grouperUVCureSetting.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperUVCureSetting.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxBackBeforPrintUV
            // 
            this.checkBoxBackBeforPrintUV.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxBackBeforPrintUV, "checkBoxBackBeforPrintUV");
            this.checkBoxBackBeforPrintUV.Name = "checkBoxBackBeforPrintUV";
            this.checkBoxBackBeforPrintUV.UseVisualStyleBackColor = false;
            // 
            // checkBoxRunAfterPrintingUV
            // 
            this.checkBoxRunAfterPrintingUV.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxRunAfterPrintingUV, "checkBoxRunAfterPrintingUV");
            this.checkBoxRunAfterPrintingUV.Name = "checkBoxRunAfterPrintingUV";
            this.checkBoxRunAfterPrintingUV.UseVisualStyleBackColor = false;
            this.checkBoxRunAfterPrintingUV.CheckedChanged += new System.EventHandler(this.checkBoxRunAfterPrinting_CheckedChanged);
            // 
            // panelUVLightInAdvance
            // 
            this.panelUVLightInAdvance.BackColor = System.Drawing.Color.Transparent;
            this.panelUVLightInAdvance.Controls.Add(this.numUVLightInAdvance);
            this.panelUVLightInAdvance.Controls.Add(this.labelUVLightInAdvance);
            resources.ApplyResources(this.panelUVLightInAdvance, "panelUVLightInAdvance");
            this.panelUVLightInAdvance.Name = "panelUVLightInAdvance";
            // 
            // numUVLightInAdvance
            // 
            this.numUVLightInAdvance.DecimalPlaces = 2;
            resources.ApplyResources(this.numUVLightInAdvance, "numUVLightInAdvance");
            this.numUVLightInAdvance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numUVLightInAdvance.Name = "numUVLightInAdvance";
            // 
            // labelUVLightInAdvance
            // 
            resources.ApplyResources(this.labelUVLightInAdvance, "labelUVLightInAdvance");
            this.labelUVLightInAdvance.Name = "labelUVLightInAdvance";
            // 
            // grouperEliminateYMotorGap
            // 
            this.grouperEliminateYMotorGap.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperEliminateYMotorGap.BorderThickness = 1F;
            this.grouperEliminateYMotorGap.Controls.Add(this.label11);
            this.grouperEliminateYMotorGap.Controls.Add(this.numericMotorgap);
            style23.Color1 = System.Drawing.Color.LightBlue;
            style23.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperEliminateYMotorGap.GradientColors = style23;
            this.grouperEliminateYMotorGap.GroupImage = null;
            resources.ApplyResources(this.grouperEliminateYMotorGap, "grouperEliminateYMotorGap");
            this.grouperEliminateYMotorGap.Name = "grouperEliminateYMotorGap";
            this.grouperEliminateYMotorGap.PaintGroupBox = false;
            this.grouperEliminateYMotorGap.RoundCorners = 10;
            this.grouperEliminateYMotorGap.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEliminateYMotorGap.ShadowControl = false;
            this.grouperEliminateYMotorGap.ShadowThickness = 3;
            this.grouperEliminateYMotorGap.TabStop = false;
            style24.Color1 = System.Drawing.Color.LightBlue;
            style24.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperEliminateYMotorGap.TitileGradientColors = style24;
            this.grouperEliminateYMotorGap.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperEliminateYMotorGap.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // numericMotorgap
            // 
            resources.ApplyResources(this.numericMotorgap, "numericMotorgap");
            this.numericMotorgap.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericMotorgap.Name = "numericMotorgap";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.m_GroupBoxClean);
            this.flowLayoutPanel1.Controls.Add(this.m_GroupBoxSpray);
            this.flowLayoutPanel1.Controls.Add(this.grouper1);
            this.flowLayoutPanel1.Controls.Add(this.m_GroupBoxInkStripe);
            this.flowLayoutPanel1.Controls.Add(this.grouperLingFeng_RollUVCappingPara);
            this.flowLayoutPanel1.Controls.Add(this.m_GroupBoxZ);
            this.flowLayoutPanel1.Controls.Add(this.m_GroupBoxZMeasure);
            this.flowLayoutPanel1.Controls.Add(this.grouperManualSpray);
            this.flowLayoutPanel1.Controls.Add(this.grouperEliminateYMotorGap);
            this.flowLayoutPanel1.Controls.Add(this.autoBackHomeSetting1);
            this.flowLayoutPanel1.Controls.Add(this.grouperWhiteInkMixing);
            this.flowLayoutPanel1.Controls.Add(this.m_Grouper_DoubleAxis);
            this.flowLayoutPanel1.Controls.Add(this.grouperDrySetting);
            this.flowLayoutPanel1.Controls.Add(this.grouperUVCureSetting);
            this.flowLayoutPanel1.Controls.Add(this.grouperMisc);
            this.flowLayoutPanel1.Controls.Add(this.grouper_HumanCleanSetting);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // m_GroupBoxClean
            // 
            resources.ApplyResources(this.m_GroupBoxClean, "m_GroupBoxClean");
            this.m_GroupBoxClean.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxClean.BorderThickness = 1F;
            this.m_GroupBoxClean.Controls.Add(this.floraParamControl1);
            this.m_GroupBoxClean.Controls.Add(this.numSuctionEndPos);
            this.m_GroupBoxClean.Controls.Add(this.labelSuctionEndPos);
            this.m_GroupBoxClean.Controls.Add(this.numSuctionStartPos);
            this.m_GroupBoxClean.Controls.Add(this.labelSuctionStartPos);
            this.m_GroupBoxClean.Controls.Add(this.numSuctionTimes);
            this.m_GroupBoxClean.Controls.Add(this.labelSuctionTimes);
            this.m_GroupBoxClean.Controls.Add(this.numHumidInterval);
            this.m_GroupBoxClean.Controls.Add(this.labelHumidInterval);
            this.m_GroupBoxClean.Controls.Add(this.numHumidPos);
            this.m_GroupBoxClean.Controls.Add(this.labelHumidPos);
            this.m_GroupBoxClean.Controls.Add(this.numCleanPosZ);
            this.m_GroupBoxClean.Controls.Add(this.label10);
            this.m_GroupBoxClean.Controls.Add(this.numCleanPosY);
            this.m_GroupBoxClean.Controls.Add(this.label9);
            this.m_GroupBoxClean.Controls.Add(this.numCleanPosX);
            this.m_GroupBoxClean.Controls.Add(this.label8);
            this.m_GroupBoxClean.Controls.Add(this.m_NumAutoCleanPosMov);
            this.m_GroupBoxClean.Controls.Add(this.m_NumAutoCleanPosLen);
            this.m_GroupBoxClean.Controls.Add(this.lblEndPos);
            this.m_GroupBoxClean.Controls.Add(this.lblAutoCleanPosMov);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownPTACleaning);
            this.m_GroupBoxClean.Controls.Add(this.m_labelPauseTimeAfterCleaning);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownAutoClean);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownCleanTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelCleanTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelAutoClean);
            style25.Color1 = System.Drawing.Color.LightBlue;
            style25.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxClean.GradientColors = style25;
            this.m_GroupBoxClean.GroupImage = null;
            this.m_GroupBoxClean.Name = "m_GroupBoxClean";
            this.m_GroupBoxClean.PaintGroupBox = false;
            this.m_GroupBoxClean.RoundCorners = 10;
            this.m_GroupBoxClean.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxClean.ShadowControl = false;
            this.m_GroupBoxClean.ShadowThickness = 3;
            this.m_GroupBoxClean.TabStop = false;
            style26.Color1 = System.Drawing.Color.LightBlue;
            style26.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxClean.TitileGradientColors = style26;
            this.m_GroupBoxClean.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxClean.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // floraParamControl1
            // 
            resources.ApplyResources(this.floraParamControl1, "floraParamControl1");
            this.floraParamControl1.BackColor = System.Drawing.Color.Transparent;
            this.floraParamControl1.Name = "floraParamControl1";
            // 
            // numSuctionEndPos
            // 
            this.numSuctionEndPos.DecimalPlaces = 2;
            resources.ApplyResources(this.numSuctionEndPos, "numSuctionEndPos");
            this.numSuctionEndPos.Name = "numSuctionEndPos";
            // 
            // labelSuctionEndPos
            // 
            resources.ApplyResources(this.labelSuctionEndPos, "labelSuctionEndPos");
            this.labelSuctionEndPos.BackColor = System.Drawing.Color.Transparent;
            this.labelSuctionEndPos.Name = "labelSuctionEndPos";
            // 
            // numSuctionStartPos
            // 
            this.numSuctionStartPos.DecimalPlaces = 2;
            resources.ApplyResources(this.numSuctionStartPos, "numSuctionStartPos");
            this.numSuctionStartPos.Name = "numSuctionStartPos";
            // 
            // labelSuctionStartPos
            // 
            resources.ApplyResources(this.labelSuctionStartPos, "labelSuctionStartPos");
            this.labelSuctionStartPos.BackColor = System.Drawing.Color.Transparent;
            this.labelSuctionStartPos.Name = "labelSuctionStartPos";
            // 
            // labelSuctionTimes
            // 
            resources.ApplyResources(this.labelSuctionTimes, "labelSuctionTimes");
            this.labelSuctionTimes.BackColor = System.Drawing.Color.Transparent;
            this.labelSuctionTimes.Name = "labelSuctionTimes";
            // 
            // labelHumidInterval
            // 
            resources.ApplyResources(this.labelHumidInterval, "labelHumidInterval");
            this.labelHumidInterval.BackColor = System.Drawing.Color.Transparent;
            this.labelHumidInterval.Name = "labelHumidInterval";
            // 
            // numHumidPos
            // 
            this.numHumidPos.DecimalPlaces = 2;
            resources.ApplyResources(this.numHumidPos, "numHumidPos");
            this.numHumidPos.Name = "numHumidPos";
            // 
            // labelHumidPos
            // 
            resources.ApplyResources(this.labelHumidPos, "labelHumidPos");
            this.labelHumidPos.BackColor = System.Drawing.Color.Transparent;
            this.labelHumidPos.Name = "labelHumidPos";
            // 
            // numCleanPosZ
            // 
            this.numCleanPosZ.DecimalPlaces = 2;
            resources.ApplyResources(this.numCleanPosZ, "numCleanPosZ");
            this.numCleanPosZ.Name = "numCleanPosZ";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // numCleanPosY
            // 
            this.numCleanPosY.DecimalPlaces = 2;
            resources.ApplyResources(this.numCleanPosY, "numCleanPosY");
            this.numCleanPosY.Name = "numCleanPosY";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // numCleanPosX
            // 
            this.numCleanPosX.DecimalPlaces = 2;
            resources.ApplyResources(this.numCleanPosX, "numCleanPosX");
            this.numCleanPosX.Name = "numCleanPosX";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // m_NumAutoCleanPosMov
            // 
            this.m_NumAutoCleanPosMov.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumAutoCleanPosMov, "m_NumAutoCleanPosMov");
            this.m_NumAutoCleanPosMov.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumAutoCleanPosMov.Name = "m_NumAutoCleanPosMov";
            this.m_NumAutoCleanPosMov.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumAutoCleanPosLen
            // 
            this.m_NumAutoCleanPosLen.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumAutoCleanPosLen, "m_NumAutoCleanPosLen");
            this.m_NumAutoCleanPosLen.Name = "m_NumAutoCleanPosLen";
            this.m_NumAutoCleanPosLen.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // lblEndPos
            // 
            resources.ApplyResources(this.lblEndPos, "lblEndPos");
            this.lblEndPos.BackColor = System.Drawing.Color.Transparent;
            this.lblEndPos.Name = "lblEndPos";
            // 
            // lblAutoCleanPosMov
            // 
            resources.ApplyResources(this.lblAutoCleanPosMov, "lblAutoCleanPosMov");
            this.lblAutoCleanPosMov.BackColor = System.Drawing.Color.Transparent;
            this.lblAutoCleanPosMov.Name = "lblAutoCleanPosMov";
            // 
            // m_labelPauseTimeAfterCleaning
            // 
            resources.ApplyResources(this.m_labelPauseTimeAfterCleaning, "m_labelPauseTimeAfterCleaning");
            this.m_labelPauseTimeAfterCleaning.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPauseTimeAfterCleaning.Name = "m_labelPauseTimeAfterCleaning";
            // 
            // m_LabelCleanTimes
            // 
            resources.ApplyResources(this.m_LabelCleanTimes, "m_LabelCleanTimes");
            this.m_LabelCleanTimes.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCleanTimes.Name = "m_LabelCleanTimes";
            // 
            // m_LabelAutoClean
            // 
            resources.ApplyResources(this.m_LabelAutoClean, "m_LabelAutoClean");
            this.m_LabelAutoClean.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoClean.Name = "m_LabelAutoClean";
            // 
            // grouperLingFeng_RollUVCappingPara
            // 
            resources.ApplyResources(this.grouperLingFeng_RollUVCappingPara, "grouperLingFeng_RollUVCappingPara");
            this.grouperLingFeng_RollUVCappingPara.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperLingFeng_RollUVCappingPara.BorderThickness = 1F;
            this.grouperLingFeng_RollUVCappingPara.Controls.Add(this.cbxCappingEnable);
            this.grouperLingFeng_RollUVCappingPara.Controls.Add(this.cbxZaxisEnable);
            style27.Color1 = System.Drawing.Color.LightBlue;
            style27.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperLingFeng_RollUVCappingPara.GradientColors = style27;
            this.grouperLingFeng_RollUVCappingPara.GroupImage = null;
            this.grouperLingFeng_RollUVCappingPara.Name = "grouperLingFeng_RollUVCappingPara";
            this.grouperLingFeng_RollUVCappingPara.PaintGroupBox = false;
            this.grouperLingFeng_RollUVCappingPara.RoundCorners = 10;
            this.grouperLingFeng_RollUVCappingPara.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperLingFeng_RollUVCappingPara.ShadowControl = false;
            this.grouperLingFeng_RollUVCappingPara.ShadowThickness = 3;
            this.grouperLingFeng_RollUVCappingPara.TabStop = false;
            style28.Color1 = System.Drawing.Color.LightBlue;
            style28.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperLingFeng_RollUVCappingPara.TitileGradientColors = style28;
            this.grouperLingFeng_RollUVCappingPara.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperLingFeng_RollUVCappingPara.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // cbxCappingEnable
            // 
            this.cbxCappingEnable.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.cbxCappingEnable, "cbxCappingEnable");
            this.cbxCappingEnable.Name = "cbxCappingEnable";
            this.cbxCappingEnable.UseVisualStyleBackColor = false;
            // 
            // cbxZaxisEnable
            // 
            this.cbxZaxisEnable.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.cbxZaxisEnable, "cbxZaxisEnable");
            this.cbxZaxisEnable.Name = "cbxZaxisEnable";
            this.cbxZaxisEnable.UseVisualStyleBackColor = false;
            // 
            // autoBackHomeSetting1
            // 
            resources.ApplyResources(this.autoBackHomeSetting1, "autoBackHomeSetting1");
            this.autoBackHomeSetting1.Name = "autoBackHomeSetting1";
            // 
            // grouperWhiteInkMixing
            // 
            this.grouperWhiteInkMixing.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperWhiteInkMixing.BorderThickness = 1F;
            this.grouperWhiteInkMixing.Controls.Add(this.numStirCycleTime);
            this.grouperWhiteInkMixing.Controls.Add(this.labelStirCycleTime);
            this.grouperWhiteInkMixing.Controls.Add(this.numStirPulseTime);
            this.grouperWhiteInkMixing.Controls.Add(this.labelStirPulseTime);
            this.grouperWhiteInkMixing.Controls.Add(this.numCycleTime);
            this.grouperWhiteInkMixing.Controls.Add(this.labelCycleTime);
            this.grouperWhiteInkMixing.Controls.Add(this.numPulseTime);
            this.grouperWhiteInkMixing.Controls.Add(this.labelPulseTime);
            style29.Color1 = System.Drawing.Color.LightBlue;
            style29.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperWhiteInkMixing.GradientColors = style29;
            this.grouperWhiteInkMixing.GroupImage = null;
            resources.ApplyResources(this.grouperWhiteInkMixing, "grouperWhiteInkMixing");
            this.grouperWhiteInkMixing.Name = "grouperWhiteInkMixing";
            this.grouperWhiteInkMixing.PaintGroupBox = false;
            this.grouperWhiteInkMixing.RoundCorners = 10;
            this.grouperWhiteInkMixing.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperWhiteInkMixing.ShadowControl = false;
            this.grouperWhiteInkMixing.ShadowThickness = 3;
            this.grouperWhiteInkMixing.TabStop = false;
            style30.Color1 = System.Drawing.Color.LightBlue;
            style30.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperWhiteInkMixing.TitileGradientColors = style30;
            this.grouperWhiteInkMixing.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperWhiteInkMixing.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // labelStirCycleTime
            // 
            resources.ApplyResources(this.labelStirCycleTime, "labelStirCycleTime");
            this.labelStirCycleTime.BackColor = System.Drawing.Color.Transparent;
            this.labelStirCycleTime.Name = "labelStirCycleTime";
            // 
            // labelStirPulseTime
            // 
            resources.ApplyResources(this.labelStirPulseTime, "labelStirPulseTime");
            this.labelStirPulseTime.BackColor = System.Drawing.Color.Transparent;
            this.labelStirPulseTime.Name = "labelStirPulseTime";
            // 
            // labelCycleTime
            // 
            resources.ApplyResources(this.labelCycleTime, "labelCycleTime");
            this.labelCycleTime.BackColor = System.Drawing.Color.Transparent;
            this.labelCycleTime.Name = "labelCycleTime";
            // 
            // labelPulseTime
            // 
            resources.ApplyResources(this.labelPulseTime, "labelPulseTime");
            this.labelPulseTime.BackColor = System.Drawing.Color.Transparent;
            this.labelPulseTime.Name = "labelPulseTime";
            // 
            // m_Grouper_DoubleAxis
            // 
            this.m_Grouper_DoubleAxis.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_Grouper_DoubleAxis.BorderThickness = 1F;
            this.m_Grouper_DoubleAxis.Controls.Add(this.numDrvEncRatio2);
            this.m_Grouper_DoubleAxis.Controls.Add(this.label7);
            this.m_Grouper_DoubleAxis.Controls.Add(this.numDrvEncRatio1);
            this.m_Grouper_DoubleAxis.Controls.Add(this.label12);
            this.m_Grouper_DoubleAxis.Controls.Add(this.numDoubeYRatio);
            this.m_Grouper_DoubleAxis.Controls.Add(this.label13);
            this.m_Grouper_DoubleAxis.Controls.Add(this.m_NumericUpDown_MaxTolerancePos);
            this.m_Grouper_DoubleAxis.Controls.Add(this.m_Label_MaxTolerancePos);
            this.m_Grouper_DoubleAxis.Controls.Add(this.m_CheckBox_CorrectOffset);
            style31.Color1 = System.Drawing.Color.LightBlue;
            style31.Color2 = System.Drawing.Color.SteelBlue;
            this.m_Grouper_DoubleAxis.GradientColors = style31;
            this.m_Grouper_DoubleAxis.GroupImage = null;
            resources.ApplyResources(this.m_Grouper_DoubleAxis, "m_Grouper_DoubleAxis");
            this.m_Grouper_DoubleAxis.Name = "m_Grouper_DoubleAxis";
            this.m_Grouper_DoubleAxis.PaintGroupBox = false;
            this.m_Grouper_DoubleAxis.RoundCorners = 10;
            this.m_Grouper_DoubleAxis.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_Grouper_DoubleAxis.ShadowControl = false;
            this.m_Grouper_DoubleAxis.ShadowThickness = 3;
            this.m_Grouper_DoubleAxis.TabStop = false;
            style32.Color1 = System.Drawing.Color.LightBlue;
            style32.Color2 = System.Drawing.Color.SteelBlue;
            this.m_Grouper_DoubleAxis.TitileGradientColors = style32;
            this.m_Grouper_DoubleAxis.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_Grouper_DoubleAxis.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // numDrvEncRatio2
            // 
            this.numDrvEncRatio2.DecimalPlaces = 1;
            resources.ApplyResources(this.numDrvEncRatio2, "numDrvEncRatio2");
            this.numDrvEncRatio2.Name = "numDrvEncRatio2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // numDrvEncRatio1
            // 
            this.numDrvEncRatio1.DecimalPlaces = 1;
            resources.ApplyResources(this.numDrvEncRatio1, "numDrvEncRatio1");
            this.numDrvEncRatio1.Name = "numDrvEncRatio1";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Name = "label12";
            // 
            // numDoubeYRatio
            // 
            this.numDoubeYRatio.DecimalPlaces = 4;
            resources.ApplyResources(this.numDoubeYRatio, "numDoubeYRatio");
            this.numDoubeYRatio.Name = "numDoubeYRatio";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Name = "label13";
            // 
            // m_NumericUpDown_MaxTolerancePos
            // 
            this.m_NumericUpDown_MaxTolerancePos.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDown_MaxTolerancePos, "m_NumericUpDown_MaxTolerancePos");
            this.m_NumericUpDown_MaxTolerancePos.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.m_NumericUpDown_MaxTolerancePos.Name = "m_NumericUpDown_MaxTolerancePos";
            // 
            // m_Label_MaxTolerancePos
            // 
            resources.ApplyResources(this.m_Label_MaxTolerancePos, "m_Label_MaxTolerancePos");
            this.m_Label_MaxTolerancePos.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_MaxTolerancePos.Name = "m_Label_MaxTolerancePos";
            // 
            // m_CheckBox_CorrectOffset
            // 
            resources.ApplyResources(this.m_CheckBox_CorrectOffset, "m_CheckBox_CorrectOffset");
            this.m_CheckBox_CorrectOffset.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBox_CorrectOffset.Name = "m_CheckBox_CorrectOffset";
            this.m_CheckBox_CorrectOffset.UseVisualStyleBackColor = false;
            this.m_CheckBox_CorrectOffset.CheckedChanged += new System.EventHandler(this.m_CheckBox_CorrectOffset_CheckedChanged);
            // 
            // grouperDrySetting
            // 
            this.grouperDrySetting.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperDrySetting.BorderThickness = 1F;
            this.grouperDrySetting.Controls.Add(this.checkBoxBackBeforPrint);
            this.grouperDrySetting.Controls.Add(this.checkBoxRunAfterPrinting);
            this.grouperDrySetting.Controls.Add(this.m_cmbMoveXSpeed);
            this.grouperDrySetting.Controls.Add(this.numDistanceAfterPrint);
            this.grouperDrySetting.Controls.Add(this.label15);
            this.grouperDrySetting.Controls.Add(this.label16);
            style33.Color1 = System.Drawing.Color.LightBlue;
            style33.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperDrySetting.GradientColors = style33;
            this.grouperDrySetting.GroupImage = null;
            resources.ApplyResources(this.grouperDrySetting, "grouperDrySetting");
            this.grouperDrySetting.Name = "grouperDrySetting";
            this.grouperDrySetting.PaintGroupBox = false;
            this.grouperDrySetting.RoundCorners = 10;
            this.grouperDrySetting.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDrySetting.ShadowControl = false;
            this.grouperDrySetting.ShadowThickness = 3;
            this.grouperDrySetting.TabStop = false;
            style34.Color1 = System.Drawing.Color.LightBlue;
            style34.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperDrySetting.TitileGradientColors = style34;
            this.grouperDrySetting.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperDrySetting.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxBackBeforPrint
            // 
            resources.ApplyResources(this.checkBoxBackBeforPrint, "checkBoxBackBeforPrint");
            this.checkBoxBackBeforPrint.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxBackBeforPrint.Name = "checkBoxBackBeforPrint";
            this.checkBoxBackBeforPrint.UseVisualStyleBackColor = false;
            // 
            // checkBoxRunAfterPrinting
            // 
            resources.ApplyResources(this.checkBoxRunAfterPrinting, "checkBoxRunAfterPrinting");
            this.checkBoxRunAfterPrinting.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRunAfterPrinting.Name = "checkBoxRunAfterPrinting";
            this.checkBoxRunAfterPrinting.UseVisualStyleBackColor = false;
            this.checkBoxRunAfterPrinting.CheckedChanged += new System.EventHandler(this.checkBoxRunAfterPrinting_CheckedChanged);
            // 
            // m_cmbMoveXSpeed
            // 
            this.m_cmbMoveXSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_cmbMoveXSpeed, "m_cmbMoveXSpeed");
            this.m_cmbMoveXSpeed.Items.AddRange(new object[] {
            resources.GetString("m_cmbMoveXSpeed.Items"),
            resources.GetString("m_cmbMoveXSpeed.Items1"),
            resources.GetString("m_cmbMoveXSpeed.Items2"),
            resources.GetString("m_cmbMoveXSpeed.Items3"),
            resources.GetString("m_cmbMoveXSpeed.Items4"),
            resources.GetString("m_cmbMoveXSpeed.Items5"),
            resources.GetString("m_cmbMoveXSpeed.Items6")});
            this.m_cmbMoveXSpeed.Name = "m_cmbMoveXSpeed";
            // 
            // numDistanceAfterPrint
            // 
            this.numDistanceAfterPrint.DecimalPlaces = 1;
            resources.ApplyResources(this.numDistanceAfterPrint, "numDistanceAfterPrint");
            this.numDistanceAfterPrint.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numDistanceAfterPrint.Name = "numDistanceAfterPrint";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Name = "label16";
            // 
            // grouperMisc
            // 
            resources.ApplyResources(this.grouperMisc, "grouperMisc");
            this.grouperMisc.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperMisc.BorderThickness = 1F;
            this.grouperMisc.Controls.Add(this.checkBoxCalibrationNoStep);
            this.grouperMisc.Controls.Add(this.panelStablecolorPixelStep);
            this.grouperMisc.Controls.Add(this.panelXDiv);
            this.grouperMisc.Controls.Add(this.panelFlatDistanceY);
            this.grouperMisc.Controls.Add(this.checkBoxClosedLoopControl);
            this.grouperMisc.Controls.Add(this.m_CheckBoxAutoYCalibrate);
            this.grouperMisc.Controls.Add(this.m_CheckBoxNozzleClogging);
            style35.Color1 = System.Drawing.Color.LightBlue;
            style35.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperMisc.GradientColors = style35;
            this.grouperMisc.GroupImage = null;
            this.grouperMisc.Name = "grouperMisc";
            this.grouperMisc.PaintGroupBox = false;
            this.grouperMisc.RoundCorners = 10;
            this.grouperMisc.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMisc.ShadowControl = false;
            this.grouperMisc.ShadowThickness = 3;
            this.grouperMisc.TabStop = false;
            style36.Color1 = System.Drawing.Color.LightBlue;
            style36.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperMisc.TitileGradientColors = style36;
            this.grouperMisc.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperMisc.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxCalibrationNoStep
            // 
            resources.ApplyResources(this.checkBoxCalibrationNoStep, "checkBoxCalibrationNoStep");
            this.checkBoxCalibrationNoStep.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxCalibrationNoStep.Name = "checkBoxCalibrationNoStep";
            this.checkBoxCalibrationNoStep.UseVisualStyleBackColor = false;
            // 
            // grouper_HumanCleanSetting
            // 
            this.grouper_HumanCleanSetting.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouper_HumanCleanSetting.BorderThickness = 1F;
            this.grouper_HumanCleanSetting.Controls.Add(this.numericUpDown_HumanCleanXpos);
            this.grouper_HumanCleanSetting.Controls.Add(this.label_HumanCleanXpos);
            this.grouper_HumanCleanSetting.Controls.Add(this.numericUpDown_HumanScarperLen);
            this.grouper_HumanCleanSetting.Controls.Add(this.label_HumanScarperLen);
            this.grouper_HumanCleanSetting.Controls.Add(this.numericUpDown_HumanRecoverTime);
            this.grouper_HumanCleanSetting.Controls.Add(this.label_HumanRecoverTime);
            this.grouper_HumanCleanSetting.Controls.Add(this.numericUpDown_HumanPressInkTime);
            this.grouper_HumanCleanSetting.Controls.Add(this.label_HumanPressInkTime);
            style37.Color1 = System.Drawing.Color.LightBlue;
            style37.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper_HumanCleanSetting.GradientColors = style37;
            this.grouper_HumanCleanSetting.GroupImage = null;
            resources.ApplyResources(this.grouper_HumanCleanSetting, "grouper_HumanCleanSetting");
            this.grouper_HumanCleanSetting.Name = "grouper_HumanCleanSetting";
            this.grouper_HumanCleanSetting.PaintGroupBox = false;
            this.grouper_HumanCleanSetting.RoundCorners = 10;
            this.grouper_HumanCleanSetting.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper_HumanCleanSetting.ShadowControl = false;
            this.grouper_HumanCleanSetting.ShadowThickness = 3;
            this.grouper_HumanCleanSetting.TabStop = false;
            style38.Color1 = System.Drawing.Color.LightBlue;
            style38.Color2 = System.Drawing.Color.SteelBlue;
            this.grouper_HumanCleanSetting.TitileGradientColors = style38;
            this.grouper_HumanCleanSetting.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper_HumanCleanSetting.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // label_HumanCleanXpos
            // 
            resources.ApplyResources(this.label_HumanCleanXpos, "label_HumanCleanXpos");
            this.label_HumanCleanXpos.BackColor = System.Drawing.Color.Transparent;
            this.label_HumanCleanXpos.Name = "label_HumanCleanXpos";
            // 
            // label_HumanScarperLen
            // 
            resources.ApplyResources(this.label_HumanScarperLen, "label_HumanScarperLen");
            this.label_HumanScarperLen.BackColor = System.Drawing.Color.Transparent;
            this.label_HumanScarperLen.Name = "label_HumanScarperLen";
            // 
            // label_HumanRecoverTime
            // 
            resources.ApplyResources(this.label_HumanRecoverTime, "label_HumanRecoverTime");
            this.label_HumanRecoverTime.BackColor = System.Drawing.Color.Transparent;
            this.label_HumanRecoverTime.Name = "label_HumanRecoverTime";
            // 
            // label_HumanPressInkTime
            // 
            resources.ApplyResources(this.label_HumanPressInkTime, "label_HumanPressInkTime");
            this.label_HumanPressInkTime.BackColor = System.Drawing.Color.Transparent;
            this.label_HumanPressInkTime.Name = "label_HumanPressInkTime";
            // 
            // BaseSetting
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelLeft);
            resources.ApplyResources(this, "$this");
            this.Name = "BaseSetting";
            this.Load += new System.EventHandler(this.BaseSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).EndInit();
            this.m_GroupBoxInkStripe.ResumeLayout(false);
            this.m_GroupBoxInkStripe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).EndInit();
            this.m_GroupBoxMedia.ResumeLayout(false);
            this.m_GroupBoxMedia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SensorPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTASpraying)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoSpray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayTimes)).EndInit();
            this.m_GroupBoxPrintSetting.ResumeLayout(false);
            this.m_GroupBoxPrintSetting.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelFeatherPram.ResumeLayout(false);
            this.panelFeatherPram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumFeatherNozzle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NmericUpDownWaveLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeatherPercent)).EndInit();
            this.panelYuDaBeltMachineParam.ResumeLayout(false);
            this.panelYuDaBeltMachineParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPlatformCorrect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).EndInit();
            this.panelUVoffsetDistance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUVoffsetDistance)).EndInit();
            this.m_GroupBoxZ.ResumeLayout(false);
            this.panelZCleanAndWet.ResumeLayout(false);
            this.panelZCleanAndWet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBSZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCleanZPos)).EndInit();
            this.panelZWorkPos2.ResumeLayout(false);
            this.panelZWorkPos2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos2)).EndInit();
            this.panelZWorkPos.ResumeLayout(false);
            this.panelZWorkPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZWorkPos)).EndInit();
            this.panelZMove.ResumeLayout(false);
            this.panelZMove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZMaxLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManualSprayFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numManualSprayPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCleanTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTACleaning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStirCycleTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStirPulseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanCleanXpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanScarperLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanRecoverTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HumanPressInkTime)).EndInit();
            this.m_GroupBoxSpray.ResumeLayout(false);
            this.m_GroupBoxSpray.PerformLayout();
            this.panelFlatDistanceY.ResumeLayout(false);
            this.panelFlatDistanceY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatDistanceY)).EndInit();
            this.panelXDiv.ResumeLayout(false);
            this.panelXDiv.PerformLayout();
            this.panelStablecolorPixelStep.ResumeLayout(false);
            this.m_GroupBoxZMeasure.ResumeLayout(false);
            this.m_GroupBoxZMeasure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadToPaper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMesureHeight)).EndInit();
            this.grouper1.ResumeLayout(false);
            this.grouperManualSpray.ResumeLayout(false);
            this.grouperManualSpray.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.m_grouperPrintDir.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRollLength)).EndInit();
            this.grouperUVCureSetting.ResumeLayout(false);
            this.panelUVLightInAdvance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUVLightInAdvance)).EndInit();
            this.grouperEliminateYMotorGap.ResumeLayout(false);
            this.grouperEliminateYMotorGap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorgap)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.m_GroupBoxClean.ResumeLayout(false);
            this.m_GroupBoxClean.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionEndPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuctionStartPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCleanPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumAutoCleanPosMov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumAutoCleanPosLen)).EndInit();
            this.grouperLingFeng_RollUVCappingPara.ResumeLayout(false);
            this.grouperWhiteInkMixing.ResumeLayout(false);
            this.grouperWhiteInkMixing.PerformLayout();
            this.m_Grouper_DoubleAxis.ResumeLayout(false);
            this.m_Grouper_DoubleAxis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvEncRatio2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvEncRatio1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoubeYRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_MaxTolerancePos)).EndInit();
            this.grouperDrySetting.ResumeLayout(false);
            this.grouperDrySetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceAfterPrint)).EndInit();
            this.grouperMisc.ResumeLayout(false);
            this.grouperMisc.PerformLayout();
            this.grouper_HumanCleanSetting.ResumeLayout(false);
            this.grouper_HumanCleanSetting.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
			bool bEnabled = (status == JetStatusEnum.Ready);
			m_ButtonMeasureMaxZ.Enabled = bEnabled;
			m_ButtonZManual.Enabled = bEnabled;
			bEnabled = (status == JetStatusEnum.Ready || status == JetStatusEnum.Spraying );
			m_ButtonMeasure.Enabled = bEnabled;
            this.isDirty = false;

		}
		private void ToolTipInit()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseSetting));

			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownAutoClean.ToolTip"),this.m_NumericUpDownAutoClean,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownCleanTimes.ToolTip"),this.m_NumericUpDownCleanTimes,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownAutoSpray.ToolTip"),this.m_NumericUpDownAutoSpray,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownSprayCycle.ToolTip"),this.m_NumericUpDownSprayCycle,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownStepTime.ToolTip"),this.m_NumericUpDownStepTime,this.m_ToolTip);

			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownPTACleaning.ToolTip"),this.m_NumericUpDownPTACleaning,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownPTASpraying.ToolTip"),this.m_NumericUpDownPTASpraying,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownFeatherPercent.ToolTip"),this.m_NumericUpDownFeatherPercent,this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NmericUpDownWaveLen.ToolTip"), this.m_NmericUpDownWaveLen, this.m_ToolTip);
		}

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            //是否支持双轴判定
            bDoubleYAxis = PubFunc.IsDoubleYAxis();
            bSupportUv = sp.bSupportUV;

            m_sPrinterProperty = sp;
            isAllwin512IHighSpeed = false; //sp.IsALLWIN_512i_HighSpeed();
            numericMotorgap.Minimum = 0;
            numericMotorgap.Maximum = 1000;
            numZMaxLen.Minimum = 0;
            numZMaxLen.Maximum = decimal.MaxValue;
            numericUpDown_SensorPos.Minimum = decimal.MinValue;
            numericUpDown_SensorPos.Maximum = decimal.MaxValue;

            m_NumericUpDownFeather.Maximum = new Decimal(sp.GetFeatherPercent());

            m_NumericUpDownLeftEdge.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            m_NumericUpDownLeftEdge.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            m_NumericUpDownWidth.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            m_NumericUpDownWidth.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));


            m_NumericUpDownMargin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, -sp.fMaxPaperWidth / 2));
            m_NumericUpDownMargin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth / 2));

            m_NumericUpDownY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            m_NumericUpDownY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));
            m_NumericUpDownHeight.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            m_NumericUpDownHeight.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));

            m_NmericUpDownWaveLen.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0.39f));//一厘米
            m_NmericUpDownWaveLen.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 4.0f));//十厘米

            m_NumAutoCleanPosMov.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, -2.0f));// -5厘米
            m_NumAutoCleanPosMov.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 2.0f));//5厘米

            m_NumAutoCleanPosLen.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, -2.0f));// -5厘米
            m_NumAutoCleanPosLen.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 2.0f));//5厘米
            numSuctionStartPos.Minimum =
                numSuctionEndPos.Minimum =
                    numCleanPosX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numSuctionEndPos.Maximum =
                numSuctionStartPos.Maximum =
                    numCleanPosX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numCleanPosX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperWidth));
            numCleanPosY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, 0));
            numCleanPosY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, sp.fMaxPaperHeight));

            numericPlatformCorrect.Minimum = 0;
            numericPlatformCorrect.Maximum = decimal.MaxValue;
            bool isSuperUser = (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser);
            bool isSimpleUV = SPrinterProperty.IsSimpleUV();
            bool isInwearSimpleUi = SPrinterProperty.IsInwearSimpleUi();
            bool isFhzl3D = PubFunc.IsFhzl3D();//峰华卓立
            comboBoxFeatherPercent.Items.Clear();
            foreach (EpsonFeatherType place in Enum.GetValues(typeof(EpsonFeatherType)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(EpsonFeatherType), place);
                comboBoxFeatherPercent.Items.Add(cmode);
            }

            InitStripePlace(false);

            m_ComboBoxFeatherType.Items.Clear();
            for (int i = 0; i < featherTypeUiList.Count; i++)
            {
                string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), featherTypeUiList[i]);
                m_ComboBoxFeatherType.Items.Add(cmode);
            }

            //cbo_MultipleInk.Items.Clear();
            //foreach (MultipleInkEnum place in Enum.GetValues(typeof(MultipleInkEnum)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(MultipleInkEnum), place);
            //    cbo_MultipleInk.Items.Add(cmode);
            //}

            this.m_ComboBoxDiv.Items.Clear();
            foreach (XResDivMode place in Enum.GetValues(typeof(XResDivMode)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(XResDivMode), place);
                if ((/*!isfacuser &&*/ place < XResDivMode.PrintMode3) /*|| isfacuser*/)
                    m_ComboBoxDiv.Items.Add(cmode);
            }


            //右侧列布局
            this.grouper1.Visible = isAllwin512IHighSpeed;
            this.m_GroupBoxInkStripe.Visible = !isAllwin512IHighSpeed;

            //labelZWorkPos.Visible = numZWorkPos.Visible = m_sPrinterProperty.IsAOJET_UV();
            m_GroupBoxZMeasure.Visible = sp.IsZMeasurSupport && !isFhzl3D && !isInwearSimpleUi && !PubFunc.IsDocan_Belt() && !PubFunc.IsDOCAN_FLAT_UV();
            m_grouperPrintDir.Visible = true;//!(sp.bSupportWhiteInk || sp.bSupportWhiteInkYoffset);

            m_LabelStepTime.Visible = m_NumericUpDownStepTime.Visible = !isSimpleUV;

            label19.Visible =numericUpDown_SensorPos.Visible =
            m_LabelMargin.Visible = m_NumericUpDownMargin.Visible =
            m_CheckBoxMeasureBeforePrint.Visible =
            m_ButtonMeasure.Visible = sp.bSupportPaperSensor && !sp.IsZMeasurSupport && !isSimpleUV;
            panelStablecolorPixelStep.Visible = panelXDiv.Visible = !isSimpleUV && !isInwearSimpleUi;
            //panelStablecolorPixelStep.Visible = true;

            grouperEliminateYMotorGap.Visible = !isSimpleUV && !PubFunc.IsZhuoZhan() && !isInwearSimpleUi;
            checkBoxJointFeather.Visible = sp.nHeadNumPerGroupY >= 2 && !isInwearSimpleUi; //目前只支持2组
            checkBoxConstantStep.Visible = !isInwearSimpleUi; 
            //左侧列布局
            if (sp.nMediaType == 0)
            {
                grouper2.Visible = true; //纸长监测
                panelFlatDistanceY.Visible =
                    m_LabelY.Visible = false;
                m_NumericUpDownY.Visible = false;
                m_LabelHeight.Visible = false;
                m_NumericUpDownHeight.Visible = false;
                //				m_GroupBoxPrintSetting.Height -= 20;
                m_CheckBoxYContinue.Visible = false;
                if ((sp.bSupportPaperSensor && !isSimpleUV) == false)
                {
                    m_GroupBoxMedia.Height -= 100;
                }
                else
                {
                    m_GroupBoxMedia.Height -= M_AUTOINDENT;
                    m_CheckBoxMeasureBeforePrint.Location = new Point(m_CheckBoxMeasureBeforePrint.Location.X, m_CheckBoxMeasureBeforePrint.Location.Y - M_AUTOINDENT);
                    m_ButtonMeasure.Location = new Point(m_ButtonMeasure.Location.X, m_ButtonMeasure.Location.Y - M_AUTOINDENT);
                }
            }
            else
            {
                grouper2.Visible = false; //纸长监测
                panelFlatDistanceY.Visible =
                m_LabelY.Visible = true;
                m_NumericUpDownY.Visible = true;
                m_LabelHeight.Visible = true;
                m_NumericUpDownHeight.Visible = true;

                m_CheckBoxYContinue.Visible = true;

                if ((sp.bSupportPaperSensor && !isSimpleUV) == false)
                {
                    m_GroupBoxMedia.Height -= M_AUTOINDENT;
                }
            }
            //是否为DoubleYAxis
            this.m_Grouper_DoubleAxis.Visible = bDoubleYAxis;

            m_CheckBoxAutoYCalibrate.Visible = sp.bSupportYEncoder && (sp.nMediaType == 0); //只有卷材，同时支持编码器才支持这个
            bool bsupportDiv = (SPrinterProperty.IsHighResolution(sp.ePrinterHead) || sp.bSupportUV);
            panelXDiv.Visible = bsupportDiv;

            bool isTlhg = m_sPrinterProperty.IsTLHG();
            label8.Visible = label9.Visible = label10.Visible = labelHumidInterval.Visible = labelHumidPos.Visible =
                labelSuctionEndPos.Visible = labelSuctionStartPos.Visible = labelSuctionTimes.Visible =
                numCleanPosX.Visible = numCleanPosY.Visible = numCleanPosZ.Visible = numHumidPos.Visible = numHumidInterval.Visible =
                numSuctionEndPos.Visible = numSuctionStartPos.Visible = numSuctionTimes.Visible = isTlhg;

            panelUVoffsetDistance.Visible = (sp.bSupportUV && isInwearSimpleUi) || !isInwearSimpleUi;
            panelUVLightInAdvance.Visible = !isInwearSimpleUi;
            if (!sp.bSupportUV && !isInwearSimpleUi)
            {
                // 无论是否uv都显示uv 偏移距离参数,只是uv时标签显示"uv偏移距离",溶剂时显示"烘干偏移距离"
                labelUVoffsetDistance.Text = ResString.GetResString("StrDryOffsetDistance");
                labelUVLightInAdvance.Text = ResString.GetResString("StrDryInAdvance");
            }
            bool isFlora = SPrinterProperty.IsFloraT50OrT180();
            floraParamControl1.Visible = isFlora && !PubFunc.IsZhuoZhan();
            checkBoxClosedLoopControl.Visible = SPrinterProperty.IsAllPrint();
            this.grouperManualSpray.Visible = !isInwearSimpleUi;

            SwitchToAdvancedMode(PubFunc.IsKingColorAdvancedMode, sp);
            m_GroupBoxClean.Visible = PubFunc.IsKingColorAdvancedMode && !isSimpleUV && !isFhzl3D && !isInwearSimpleUi;
            checkBoxFixPos.Visible = !isSimpleUV;

            m_GroupBoxMedia.Visible =
        m_GroupBoxInkStripe.Visible = !isFhzl3D;
            m_CheckBoxKillBidir.Visible = panelXDiv.Visible = !isFhzl3D;
            if (isFhzl3D)
            {
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPlace,
                 (int)InkStrPosEnum.None);
            }

            textBoxFeather.Location = m_NumericUpDownFeather.Location;
            textBoxFeather.Size = m_NumericUpDownFeather.Size;
            floraParamControl1.OnPrinterPropertyChange(sp);
            autoBackHomeSetting1.OnPrinterPropertyChange(sp);

            panelYuDaBeltMachineParam.Visible = sp.SurpportJobSpaceAsOriginY();
            checkBoxUseHighParam.Visible = checkBoxBSUseSpray.Visible =
                m_CheckBoxAutoJumpWhite.Visible =
                m_LabelStepTime.Visible = m_NumericUpDownStepTime.Visible =
            m_LabelLeftEdge.Visible = m_NumericUpDownLeftEdge.Visible = !isInwearSimpleUi;
            if (isInwearSimpleUi)
            {
                // 隐藏跳白时间时,下部控件上移到跳白时间位置
                panel2.Location = m_LabelStepTime.Location;
            }
            autoBackHomeSetting1.Visible = SPrinterProperty.IsSurportCapping() || PubFunc.IsSupportKeepwet();
            if (PubFunc.IsHuiLiCai())
            {
                autoBackHomeSetting1.grouperAutoHoming.Visible = false;
                autoBackHomeSetting1.Size = new Size(autoBackHomeSetting1.Size.Width, 190);
            }
            panelZWorkPos2.Visible = PubFunc.IsZhuoZhan();
            grouperWhiteInkMixing.Visible = sp.nWhiteInkNum > 0;
            this.isDirty = false;
            grouperUVCureSetting.Visible = sp.bSupportUV;
            grouperDrySetting.Visible = !sp.bSupportUV;

            m_ButtonMeasureMaxZ.Visible = sp.bSupportZendPointSensor || SPrinterProperty.IsGongZeng();

            grouperLingFeng_RollUVCappingPara.Visible = SPrinterProperty.IsLingFeng_RollUV_16H();
            grouper_HumanCleanSetting.Visible = SPrinterProperty.IsHuman();
        }

        private void SwitchToAdvancedMode(bool bAdvancedMode,SPrinterProperty sp)
        {
            m_GroupBoxClean.Visible = bAdvancedMode;// && !sp.bSupportUV;
            bool bZMeasurSensorSupport = sp.IsZMeasurSupport;//PubFunc.IsVender92();
            bool showZmove = (sp.bSupportZMotion && bAdvancedMode);
            showZmove = true;
            m_GroupBoxZ.Visible = showZmove;
            if (showZmove)
            {
                numZMaxLen.Enabled = true; //!sp.bSupportZendPointSensor;
                panelZMove.Visible = !PubFunc.IsZhuoZhan();//sp.bSupportZendPointSensor;
                panelZWorkPos.Visible = PubFunc.IsZhuoZhan();// !sp.bSupportZendPointSensor;
                if (PubFunc.IsZhuoZhan())
                {
                    panelZCleanAndWet.Location = new Point(panelZCleanAndWet.Location.X, panelZCleanAndWet.Location.Y - panelZMove.Height);
                    panelZWorkPos.Location = new Point(panelZWorkPos.Location.X, panelZWorkPos.Location.Y - panelZMove.Height);
                    panelZWorkPos2.Location = new Point(panelZWorkPos2.Location.X, panelZWorkPos2.Location.Y - panelZMove.Height);
                }

                if (SPrinterProperty.IsYUEDA())
                {
                    panelZCleanAndWet.Visible = false;
                    panelZWorkPos.Visible = true;
                    panelZWorkPos.Location = new Point(panelZWorkPos.Location.X, panelZCleanAndWet.Location.Y);
                    panelZWorkPos2.Visible = false;
                }
            }
            m_GroupBoxMedia.Visible = bAdvancedMode;
            checkBoxzCaliNoStep.Visible = false;
            //panel1.Visible = bAdvancedMode;
        }

        const int FeatherPercentLarge = 101;//dll超过100 的按最大
        const int FeatherPercentMedium = 66;
        const int FeatherPercentSmall = 33;
        const int GzFeatherPercentMedium = 200;
        const int GzFeatherPercentSmall = 100;
        const int FeatherPercentNone = 0;

	    public void OnPrinterSettingChange(SPrinterSetting ss)
	    {
            _printerSetting = ss;
            LogWriter.SaveOptionLog(string.Format("OnPrinterSettingChange bLoaded={0};bAutoMeasure={1};fMesureMaxLen={2};zMaxLength={3}",
         bLoaded, bAutoMeasure, ss.ZSetting.fMesureMaxLen, ss.sExtensionSetting.zMaxLength));
            if (bLoaded && bAutoMeasure && ss.ZSetting.fMesureMaxLen > 0)
	        {
                SZSetting zseting = ss.ZSetting;
	            //更新z轴总行程
                numZMaxLen.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, zseting.fMesureMaxLen);

                bAutoMeasure = false;
	        }
	        else
	        {
	            checkBoxFixPos.Checked = ss.sBaseSetting.sStripeSetting.StripeType == 1;
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStripeSpace, m_CurrentUnit,
	                ss.sBaseSetting.sStripeSetting.fStripeOffset);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStripeWidth, m_CurrentUnit,
	                ss.sBaseSetting.sStripeSetting.fStripeWidth);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownJobSpace, m_CurrentUnit, ss.sBaseSetting.fJobSpace);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStepTime, ss.sBaseSetting.fStepTime);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownLeftEdge, m_CurrentUnit, ss.sBaseSetting.fLeftMargin);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownWidth, m_CurrentUnit, ss.sBaseSetting.fPaperWidth);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownY, m_CurrentUnit, ss.sBaseSetting.fTopMargin);
	            m_CheckBoxMeasureBeforePrint.Checked = ss.sBaseSetting.bMeasureBeforePrint;

	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownHeight, m_CurrentUnit, ss.sBaseSetting.fPaperHeight);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownZSpace, m_CurrentUnit, ss.sBaseSetting.fZSpace);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownThickness, m_CurrentUnit,
	                ss.sBaseSetting.fPaperThick);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownMargin, m_CurrentUnit,
	                ss.sBaseSetting.fMeasureMargin);

	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownAutoClean, ss.sCleanSetting.nCleanerPassInterval);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownCleanTimes, ss.sCleanSetting.nCleanerTimes);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownAutoSpray, ss.sCleanSetting.nSprayPassInterval);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownSprayCycle, ss.sCleanSetting.nSprayFireInterval);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownSprayTimes, ss.sCleanSetting.nSprayTimes);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPTASpraying,
	                ss.sCleanSetting.nPauseTimeAfterSpraying/1000f);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPTACleaning,
	                ss.sCleanSetting.nPauseTimeAfterCleaning/1000f);
	            UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownFeather, ss.sBaseSetting.nFeatherPercent);
	            m_CheckBoxAutoYCalibrate.Checked = ss.sBaseSetting.bAutoYCalibration;
	            m_CheckBoxKillBidir.Checked = (ss.nKillBiDirBanding == 1);
	            checkBoxPixelStep.Checked = (ss.nKillBiDirBanding == 2);
	            byte type = ss.sBaseSetting.sStripeSetting.bNormalStripeType;
	            m_CheckBoxNormalType.Checked = ((type & ((byte) EnumStripeType.Normal)) != 0);
	            m_CheckBoxMixedType.Checked = ((type & ((byte) EnumStripeType.ColorMixed)) != 0);
	            m_CheckBoxHeightWithImageType.Checked = ((type & ((byte) EnumStripeType.HeightWithImage)) != 0);
                int InkPercentIndex=0;
                byte InkPercent=ss.sBaseSetting.sStripeSetting.nStripInkPercent;
                if (InkPercent == 75) InkPercentIndex = 1;
                else if (InkPercent == 50) InkPercentIndex = 2;
                else if (InkPercent == 25) InkPercentIndex = 3;
                    m_ComboBoxInkPercent.SelectedIndex = InkPercentIndex;
	            m_CheckBoxIdleSpray.Checked = ss.sCleanSetting.bSprayWhileIdle;
	            m_CheckBoxSprayBeforePrint.Checked = ss.sCleanSetting.bSprayBeforePrint;
	            checkBoxUseHighParam.Checked = ss.sExtensionSetting.idleFlashUseStrongParams == 0 ? false : true;
	            checkBoxBSUseSpray.Checked = ss.sExtensionSetting.flashInWetStatus == 0 ? false : true;
	            m_CheckBoxAutoJumpWhite.Checked = (!ss.sBaseSetting.bIgnorePrintWhiteX) &&
	                                              (!ss.sBaseSetting.bIgnorePrintWhiteY);
	            if (SPrinterProperty.IsInwearSimpleUi())
	                m_CheckBoxAutoJumpWhite.Checked = !SPrinterProperty.IsInwearSimpleUi(); //英威简化界面强制不跳白
//			m_CheckBoxAutoJumpWhite.Enabled      = (m_ComboBoxWhiteInk.SelectedIndex == 0);
	            m_CheckBoxYContinue.Checked = ss.sBaseSetting.bYPrintContinue;
                checkBoxOneStepSkipWhite.Checked = ss.sExtensionSetting.OneStepSkipWhite == 1;
	            if (PubFunc.IsFhzl3D())
	            {
	                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPlace,
	                    (int) InkStrPosEnum.None);
	            }
	            else
	            {
	                if (ss.sBaseSetting.sStripeSetting.StripeType == 1)
	                {
	                    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPlace,
	                        (int) ss.sBaseSetting.sStripeSetting.eStripePosition - (int) InkStrPosEnum.Left);
	                }
	                else
	                {
	                    UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPlace,
	                        (int) ss.sBaseSetting.sStripeSetting.eStripePosition);
	                }
	            }
	            cRadioButtons[m_ComboBoxPlace.SelectedIndex].Checked = true;

	            if (ss.sBaseSetting.nXResutionDiv < (byte) XResDivMode.HighPrecision ||
	                ss.sBaseSetting.nXResutionDiv > (byte) XResDivMode.PrintMode4)
	                ss.sBaseSetting.nXResutionDiv = 1;
	            UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxDiv, (int) ss.sBaseSetting.nXResutionDiv - 1);
	            this.m_CheckBoxNozzleClogging.Checked = ss.sBaseSetting.bNozzleBlock;
	            SetFeatherTypeToUi((FeatherType) ss.sBaseSetting.nFeatherType);
	            //UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxFeatherType, ss.sBaseSetting.nFeatherType);
	            UIPreference.SetValueAndClampWithMinMax(m_NmericUpDownWaveLen, m_CurrentUnit,
	                ss.sBaseSetting.fFeatherWavelength);
	            UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDownFeatherPercent,
	                ss.sBaseSetting.nAdvanceFeatherPercent);
	            CheckBoxReversePrint.Checked = ss.sBaseSetting.bReversePrint;
	            this.m_CheckBoxMirror.Checked = ss.sBaseSetting.bMirrorX;
	            UIPreference.SetValueAndClampWithMinMax(m_NumAutoCleanPosMov, m_CurrentUnit,
	                ss.sBaseSetting.fAutoCleanPosMov);
	            UIPreference.SetValueAndClampWithMinMax(this.m_NumAutoCleanPosLen, m_CurrentUnit,
	                ss.sBaseSetting.fAutoCleanPosLen);
	            UIPreference.SetSelectIndexAndClampWithMax(this.cbo_MultipleInk, ss.sBaseSetting.multipleInk);
	            if (ss.sBaseSetting.bFeatherMaxNew != 0)
	            {
	                if (ss.sBaseSetting.bFeatherMaxNew == 1 || ss.sBaseSetting.nFeatherPercent == FeatherPercentLarge)
	                    comboBoxFeatherPercent.SelectedIndex = (int) EpsonFeatherType.Large;
	                //if (ss.sBaseSetting.bFeatherMaxNew == 2)
	                //    comboBoxFeatherPercent.SelectedIndex = (int)EpsonFeatherType.SuperLarge;
	            }
	            else
	            {
	                bool isGzUv = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID();
	                if (ss.sBaseSetting.nFeatherPercent == FeatherPercentMedium
	                    || (isGzUv && ss.sBaseSetting.nFeatherPercent == GzFeatherPercentMedium))
	                {
	                    comboBoxFeatherPercent.SelectedIndex = (int) EpsonFeatherType.Medium;
	                }
	                else if (ss.sBaseSetting.nFeatherPercent == FeatherPercentSmall
	                         || (isGzUv && ss.sBaseSetting.nFeatherPercent == GzFeatherPercentSmall))
	                {
	                    comboBoxFeatherPercent.SelectedIndex = (int) EpsonFeatherType.Small;
	                }
                    else if (ss.sBaseSetting.nFeatherPercent == FeatherPercentNone && ss.sExtensionSetting.FeatherNozzle == 0)
	                {
	                    comboBoxFeatherPercent.SelectedIndex = (int) EpsonFeatherType.None;
	                }
	                else
	                {
	                    comboBoxFeatherPercent.SelectedIndex = (int) EpsonFeatherType.Custom;
                        NumFeatherNozzle.Enabled = true;
	                }
	            }

                NumFeatherNozzle.Value = (Decimal)ss.sExtensionSetting.FeatherNozzle;

	            SZSetting zseting = ss.ZSetting;
	            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownHeadToPaper, m_CurrentUnit, zseting.fHeadToPaper);
	            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownMesureHeight, m_CurrentUnit,
	                zseting.fMesureHeight);

	            //双轴矫正
	            if (bDoubleYAxis)
	            {
	                this.numDrvEncRatio1.Value = new decimal(ss.sExtensionSetting.sDouble_Yaxis.DrvEncRatio1);
	                this.numDrvEncRatio2.Value = new decimal(ss.sExtensionSetting.sDouble_Yaxis.DrvEncRatio2);
	                this.numDoubeYRatio.Value = new decimal(ss.sExtensionSetting.sDouble_Yaxis.DoubeYRatio);
	                this.m_CheckBox_CorrectOffset.Checked = (ss.sExtensionSetting.sDouble_Yaxis.bCorrectoffset == 1)
	                    ? true
	                    : false;
	                if (ss.sExtensionSetting.sDouble_Yaxis.bCorrectoffset == 0)
	                {
	                    UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_MaxTolerancePos, m_CurrentUnit, 0);
	                    this.m_NumericUpDown_MaxTolerancePos.Enabled = false;
	                }
	                else
	                {
	                    this.m_NumericUpDown_MaxTolerancePos.Enabled = true;
	                    UIPreference.SetValueAndClampWithMinMax(this.m_NumericUpDown_MaxTolerancePos, m_CurrentUnit,
	                        ss.sExtensionSetting.sDouble_Yaxis.fMaxTolerancepos);
	                }
	            }
	            UIPreference.SetValueAndClampWithMinMax(numFlatDistanceY, m_CurrentUnit, ss.sExtensionSetting.FlatSpaceY);
	            JetStatusEnum status = CoreInterface.GetBoardStatus();
	            if (status != JetStatusEnum.PowerOff)
	            {
	                if (m_sPrinterProperty.IsAOJET_UV())
	                {
	                    AojetParam param = new AojetParam();
	                    if (ScorpionCoreInterface.GetAojetParam(out param))
	                    {
	                        uint Flag = BitConverter.ToUInt32(new byte[] {(byte) 'A', (byte) 'J', (byte) 'E', (byte) 'T'},
	                            0);
	                        if (param.Flag == Flag && m_sPrinterProperty.fPulsePerInchZ != 0)
	                        {
	                            UIPreference.SetValueAndClampWithMinMax(numZWorkPos, m_CurrentUnit,
	                                param.zWorkPos/m_sPrinterProperty.fPulsePerInchZ);
	                        }
	                    }
	                }
	                else
	                {
	                    ByhxZMoveParam param = new ByhxZMoveParam();
	                    if (ScorpionCoreInterface.GetByhxZMoveParam(out param))
	                    {
	                        uint Flag = BitConverter.ToUInt32(new byte[] {(byte) 'B', (byte) 'Y', (byte) 'H', (byte) 'X'},
	                            0);
	                        if (param.Flag == Flag && m_sPrinterProperty.fPulsePerInchZ != 0)
	                        {
	                            UIPreference.SetValueAndClampWithMinMax(numZWorkPos, m_CurrentUnit,
	                                (param.HeadToPaper + param.PaperThick)/m_sPrinterProperty.fPulsePerInchZ);

	                            UIPreference.SetValueAndClampWithMinMax(numericUpDownBSZPos, m_CurrentUnit,
	                                (param.WetPlace)/m_sPrinterProperty.fPulsePerInchZ);

	                            UIPreference.SetValueAndClampWithMinMax(numericUpDownCleanZPos, m_CurrentUnit,
	                                (param.CleanPlace)/m_sPrinterProperty.fPulsePerInchZ);
	                        }
	                    }
	                }
	            }

	            if (status != JetStatusEnum.PowerOff && m_sPrinterProperty.IsTLHG())
	            {
	                TlhgParam param = new TlhgParam();
	                if (EpsonLCD.GetTlhgParam(ref param))
	                {
	                    UIPreference.SetValueAndClampWithMinMax(numCleanPosX, m_CurrentUnit,
	                        param.XCleanPos/m_sPrinterProperty.fPulsePerInchX);
	                    UIPreference.SetValueAndClampWithMinMax(numCleanPosY, m_CurrentUnit,
	                        param.YCleanPos/m_sPrinterProperty.fPulsePerInchY);
	                    UIPreference.SetValueAndClampWithMinMax(numCleanPosZ, m_CurrentUnit,
	                        param.ZCleanPos/m_sPrinterProperty.fPulsePerInchZ);
	                    UIPreference.SetValueAndClampWithMinMax(numHumidPos, m_CurrentUnit,
	                        param.addWetPos/m_sPrinterProperty.fPulsePerInchX);
	                    numHumidInterval.Value = param.addWetInterval;
	                    numSuctionTimes.Value = param.suckTimes;
	                    UIPreference.SetValueAndClampWithMinMax(numSuctionStartPos, m_CurrentUnit,
	                        param.suckStartPos/m_sPrinterProperty.fPulsePerInchX);
	                    UIPreference.SetValueAndClampWithMinMax(numSuctionEndPos, m_CurrentUnit,
	                        param.suckEndPos/m_sPrinterProperty.fPulsePerInchX);

	                }
	            }
	            numManualSprayFreq.Value = ss.sExtensionSetting.ManualSprayFrequency;
	            numManualSprayPeriod.Value = ss.sExtensionSetting.ManualSprayTime;

	            if (status != JetStatusEnum.PowerOff && SPrinterProperty.IsAllPrint())
	            {
	                AllprintParam param = new AllprintParam();
	                if (EpsonLCD.GetAllprintParam(ref param))
	                {
	                    checkBoxClosedLoopControl.Checked = param.bIsYCloseLoop != 0;
	                }
	            }
	            checkBoxExquisiteFeather.Checked = ss.sExtensionSetting.bExquisiteFeather;
	            checkBoxJointFeather.Checked = ss.sExtensionSetting.bJointFeather;
	            checkBoxConstantStep.Checked = ss.sExtensionSetting.bConstantStep;
	            
	            if (bSupportUv)
	            {
	                checkBoxRunAfterPrintingUV.Checked = ss.sExtensionSetting.AutoRunAfterPrint;
	                checkBoxBackBeforPrintUV.Checked = ss.sExtensionSetting.BackBeforePrint;
                    //UV固化
                    UIPreference.SetValueAndClampWithMinMax(numUVoffsetDistance, m_CurrentUnit,
                        ss.sExtensionSetting.fRunDistanceAfterPrint);
	            }
	            else
	            {
	                checkBoxRunAfterPrinting.Checked = ss.sExtensionSetting.AutoRunAfterPrint;
	                checkBoxBackBeforPrint.Checked = ss.sExtensionSetting.BackBeforePrint;
                    //溶剂干燥
                    UIPreference.SetValueAndClampWithMinMax(this.numDistanceAfterPrint, m_CurrentUnit,
                        ss.sExtensionSetting.fRunDistanceAfterPrint);
                }
                UIPreference.SetValueAndClampWithMinMax(numUVLightInAdvance, m_CurrentUnit,
                    ss.sExtensionSetting.UVLightInAdvance);
                UIPreference.SetSelectIndexAndClampWithMax(this.m_cmbMoveXSpeed, ss.sExtensionSetting.RunSpeed - 1);

	            autoBackHomeSetting1.OnPrinterSettingChange(ss);
	            if (PubFunc.IsZhuoZhan())
	            {
	                ZhuoZhanParam param = new ZhuoZhanParam();
	                if (EpsonLCD.GetZhuoZhanParam(ref param))
	                {
	                    if (m_sPrinterProperty.fPulsePerInchZ != 0)
	                    {
	                        UIPreference.SetValueAndClampWithMinMax(numZWorkPos2, m_CurrentUnit,
	                            param.zWorkPos/m_sPrinterProperty.fPulsePerInchZ);
	                    }
	                }
	            }
	            numZMaxLen.Value = (decimal) UIPreference.ToDisplayLength(m_CurrentUnit, ss.sExtensionSetting.zMaxLength);

	            UIPreference.SetValueAndClampWithMinMax(numericUpDown_SensorPos, m_CurrentUnit,
	                ss.sExtensionSetting.MeasureWidthSensorPos);

                //checkBoxzCaliNoStep.Checked = ss.sExtensionSetting.bIsNewCalibration == 1 ? true : false;
                checkBoxCalibrationNoStep.Checked = ss.sExtensionSetting.bCalibrationNoStep == 1 ? true : false;

                if (SPrinterProperty.IsLingFeng_RollUV_16H())
                {
                    LingFengParam lfPrarm = new LingFengParam();
                    if (EpsonLCD.GetLingFengPara(ref lfPrarm))
                    {
                        //cbxZaxisEnable.Checked = lfPrarm.ZEnable == 1 ? true : false;
                        cbxCappingEnable.Checked = lfPrarm.WetEnable == 1 ? true : false;
                    }
                }

                if (SPrinterProperty.IsHuman())
                {
                    HumanParam param = new HumanParam();
                    if (EpsonLCD.GetHumanParam(ref param))
                    {
                        numericUpDown_HumanPressInkTime.Value = (decimal)(param.PressInkOnTime / 1000d);
                        numericUpDown_HumanRecoverTime.Value = (decimal)(param.PressInkRecoverTime / 1000d);
                        numericUpDown_HumanScarperLen.Value = param.ScarperLen;
                        numericUpDown_HumanCleanXpos.Value = param.CleanXpos;
                    }
                }
	            this.isDirty = false;
	        }
	    }

	    public void OnGetPrinterSetting(ref SPrinterSetting ss)
	    {
            ss.sExtensionSetting.zMaxLength = UIPreference.ToInchLength(m_CurrentUnit,
    decimal.ToSingle(numZMaxLen.Value));
            
            ss.sExtensionSetting.UVLightInAdvance = UIPreference.ToInchLength(m_CurrentUnit,
                 decimal.ToSingle(numUVLightInAdvance.Value));
	        ss.sBaseSetting.sStripeSetting.fStripeOffset = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownStripeSpace.Value));
	        ss.sBaseSetting.sStripeSetting.fStripeWidth = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownStripeWidth.Value));
	        ss.sBaseSetting.fJobSpace = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownJobSpace.Value));
	        ss.sBaseSetting.fStepTime = Decimal.ToSingle(m_NumericUpDownStepTime.Value);
	        ss.sBaseSetting.fLeftMargin = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownLeftEdge.Value));
	        ss.sBaseSetting.fPaperWidth = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownWidth.Value));
	        ss.sBaseSetting.fTopMargin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownY.Value));
	        ss.sBaseSetting.fPaperHeight = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownHeight.Value));
	        ss.sBaseSetting.fZSpace = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownZSpace.Value));
	        ss.sBaseSetting.fPaperThick = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownThickness.Value));
	        ss.sBaseSetting.fMeasureMargin = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(m_NumericUpDownMargin.Value));

	        ss.sCleanSetting.nCleanerPassInterval = Decimal.ToInt32(m_NumericUpDownAutoClean.Value);
	        ss.sCleanSetting.nCleanerTimes = Decimal.ToInt32(m_NumericUpDownCleanTimes.Value);
	        ss.sCleanSetting.nSprayPassInterval = Decimal.ToInt32(m_NumericUpDownAutoSpray.Value);
	        ss.sCleanSetting.nSprayFireInterval = Decimal.ToInt32(m_NumericUpDownSprayCycle.Value);

	        if (m_sPrinterProperty.IsInwearKm512iMab_c()
                && (ss.sCleanSetting.nSprayFireInterval > 0 && ss.sCleanSetting.nSprayFireInterval < 200))
	        {
	            // 保护喷头,防止频率过高烧喷头
	            ss.sCleanSetting.nSprayFireInterval = 200;
	        }
	        ss.sCleanSetting.nSprayTimes = Decimal.ToInt32(m_NumericUpDownSprayTimes.Value);
	        ss.sCleanSetting.nPauseTimeAfterSpraying = Decimal.ToUInt16(m_NumericUpDownPTASpraying.Value*1000);
	        ss.sCleanSetting.nPauseTimeAfterCleaning = Decimal.ToUInt16(m_NumericUpDownPTACleaning.Value*1000);
	        //ss.sMoveSetting.nXMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
	        //ss.sMoveSetting.nYMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
	        //ss.sMoveSetting.usMotorEncoder						=	Decimal.ToUInt16(m_NumericUpDownEncoder.Value);		
	        if (m_CheckBoxKillBidir.Checked)
	            ss.nKillBiDirBanding = 1;
	        else if (checkBoxPixelStep.Checked)
	            ss.nKillBiDirBanding = 2;
	        else
	        {
	            ss.nKillBiDirBanding = 0;
	        }
	        byte NormalType = 0;
	        if (m_CheckBoxNormalType.Checked)
	        {
	            NormalType |= (byte) EnumStripeType.Normal;
	        }
	        if (m_CheckBoxMixedType.Checked)
	        {
	            NormalType |= (byte) EnumStripeType.ColorMixed;
	        }
	        if (m_CheckBoxHeightWithImageType.Checked)
	        {
	            NormalType |= (byte) EnumStripeType.HeightWithImage;
	        }
            
	        ss.sBaseSetting.sStripeSetting.bNormalStripeType = NormalType;
            byte inkPercent = 100;
            if (byte.TryParse(m_ComboBoxInkPercent.Text.Replace("%", "").Trim(), out inkPercent)) {
                ss.sBaseSetting.sStripeSetting.nStripInkPercent = inkPercent;
            }
	        ss.sBaseSetting.sStripeSetting.StripeType = (byte) (checkBoxFixPos.Checked ? 1 : 0);

	        ss.sCleanSetting.bSprayWhileIdle = m_CheckBoxIdleSpray.Checked;
	        ss.sCleanSetting.bSprayBeforePrint = m_CheckBoxSprayBeforePrint.Checked;
	        ss.sBaseSetting.bIgnorePrintWhiteX =
	            ss.sBaseSetting.bIgnorePrintWhiteY = !m_CheckBoxAutoJumpWhite.Checked;
	        ss.sBaseSetting.bYPrintContinue = m_CheckBoxYContinue.Checked;
	        ss.sBaseSetting.bAutoYCalibration = m_CheckBoxAutoYCalibrate.Checked;
	        ss.sBaseSetting.bMeasureBeforePrint = m_CheckBoxMeasureBeforePrint.Checked;
            ss.sExtensionSetting.OneStepSkipWhite = (byte)(checkBoxOneStepSkipWhite.Checked ? 1 : 0);
	        if (isAllwin512IHighSpeed)
	        {
	            for (int i = 0; i < cRadioButtons.Count; i++)
	            {
	                if (cRadioButtons[i].Checked)
	                    ss.sBaseSetting.sStripeSetting.eStripePosition = (InkStrPosEnum) i;
	            }
	        }
	        else
	        {
	            if (checkBoxFixPos.Checked)
	            {
	                if (m_ComboBoxPlace.SelectedIndex == 0)
	                {
	                    ss.sBaseSetting.sStripeSetting.eStripePosition = InkStrPosEnum.Left;
	                }
	                else
	                {
	                    ss.sBaseSetting.sStripeSetting.eStripePosition = InkStrPosEnum.Right;
	                }
	            }
	            else
	            {
	                ss.sBaseSetting.sStripeSetting.eStripePosition = (InkStrPosEnum) m_ComboBoxPlace.SelectedIndex;
	            }
	        }

	        ss.sBaseSetting.nXResutionDiv = (byte) (this.m_ComboBoxDiv.SelectedIndex + 1);
	        ss.sBaseSetting.bNozzleBlock = this.m_CheckBoxNozzleClogging.Checked;
	        ss.sBaseSetting.nFeatherType = (byte) GetFeatherTypeFromUi(); //(byte)this.m_ComboBoxFeatherType.SelectedIndex;
	        ss.sBaseSetting.fFeatherWavelength = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(this.m_NmericUpDownWaveLen.Value));
	        ss.sBaseSetting.nAdvanceFeatherPercent = (byte) this.m_NumericUpDownFeatherPercent.Value;
	        ss.sBaseSetting.bMirrorX = this.m_CheckBoxMirror.Checked;
	        ss.sBaseSetting.bReversePrint = CheckBoxReversePrint.Checked;
	        ss.sBaseSetting.fAutoCleanPosMov = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(this.m_NumAutoCleanPosMov.Value));
	        ss.sBaseSetting.fAutoCleanPosLen = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(this.m_NumAutoCleanPosLen.Value));
	        ss.sBaseSetting.multipleInk = (byte) this.cbo_MultipleInk.SelectedIndex;
	        EpsonFeatherType featherPercentType = (EpsonFeatherType) comboBoxFeatherPercent.SelectedIndex;
	        ss.sBaseSetting.nFeatherPercent = GetFeatherPercent(featherPercentType, ref ss.sBaseSetting.bFeatherMaxNew);
            ss.sExtensionSetting.idleFlashUseStrongParams = (byte)(checkBoxUseHighParam.Checked ? 1 : 0);
            ss.sExtensionSetting.flashInWetStatus = (byte)(checkBoxBSUseSpray.Checked ? 1 : 0);
	        float temp = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownHeadToPaper.Value));
	        if (temp != ss.ZSetting.fHeadToPaper)
	        {
	            ss.ZSetting.fHeadToPaper = temp;
	        }
	        temp = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownMesureHeight.Value));
	        if (temp != ss.ZSetting.fMesureHeight)
	        {
	            ss.ZSetting.fMesureHeight = temp;
	        }

	        if (m_sPrinterProperty.IsAOJET_UV())
	        {
	            AojetParam param = new AojetParam();
	            param.Flag = BitConverter.ToUInt32(new byte[] {(byte) 'A', (byte) 'J', (byte) 'E', (byte) 'T'}, 0);
	            param.zWorkPos =
	                (uint)
	                    ((UIPreference.ToInchLength(m_CurrentUnit, (float) numZWorkPos.Value))*
	                     m_sPrinterProperty.fPulsePerInchZ);
	            ScorpionCoreInterface.SetAojetParam(param);
	        }

            UpdateZWorkPosToBoard();

	        if (m_sPrinterProperty.IsTLHG())
	        {
	            TlhgParam param = new TlhgParam();
	            if (EpsonLCD.GetTlhgParam(ref param))
	            {
	                param.Flag = new char[] {'T', 'L', 'H', 'G'};
	                param.XCleanPos =
	                    (uint)
	                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numCleanPosX.Value))*
	                                   m_sPrinterProperty.fPulsePerInchX);
	                param.YCleanPos =
	                    (uint)
	                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numCleanPosY.Value))*
	                                   m_sPrinterProperty.fPulsePerInchY);
	                param.ZCleanPos =
	                    (uint)
	                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numCleanPosZ.Value))*
	                                   m_sPrinterProperty.fPulsePerInchZ);
	                param.addWetPos = (uint)
	                    Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numHumidPos.Value))*
	                               m_sPrinterProperty.fPulsePerInchX);
	                param.addWetInterval = (byte) numHumidInterval.Value;
	                param.suckTimes = (byte) numSuctionTimes.Value;
	                param.suckStartPos =
	                    (uint)
	                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numSuctionStartPos.Value))*
	                                   m_sPrinterProperty.fPulsePerInchX);
	                param.suckEndPos =
	                    (uint)
	                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float) numSuctionEndPos.Value))*
	                                   m_sPrinterProperty.fPulsePerInchX);
	                EpsonLCD.SetTlhgParam(param);
	            }
	        }
	        if (SPrinterProperty.IsAllPrint())
	        {
	            AllprintParam param = new AllprintParam();
	            param.bIsYCloseLoop = (byte) (checkBoxClosedLoopControl.Checked ? 1 : 0);
	            EpsonLCD.SetAllprintParam(param);
	        }
	        //双轴矫正
	        if (bDoubleYAxis)
	        {
	            ss.sExtensionSetting.sDouble_Yaxis.DrvEncRatio1 = (float) numDrvEncRatio1.Value;
	            ss.sExtensionSetting.sDouble_Yaxis.DrvEncRatio2 = (float) numDrvEncRatio2.Value;
	            ss.sExtensionSetting.sDouble_Yaxis.DoubeYRatio = (float) numDoubeYRatio.Value;
	            ss.sExtensionSetting.sDouble_Yaxis.bCorrectoffset = Convert.ToByte(this.m_CheckBox_CorrectOffset.Checked);
	            if (ss.sExtensionSetting.sDouble_Yaxis.bCorrectoffset == 0)
	            {
	                ss.sExtensionSetting.sDouble_Yaxis.fMaxTolerancepos = UIPreference.ToInchLength(m_CurrentUnit,
	                    Decimal.ToSingle(0));
	                this.m_NumericUpDown_MaxTolerancePos.Enabled = false;
	            }
	            else
	            {
	                this.m_NumericUpDown_MaxTolerancePos.Enabled = true;
	                ss.sExtensionSetting.sDouble_Yaxis.fMaxTolerancepos = UIPreference.ToInchLength(m_CurrentUnit,
	                    Decimal.ToSingle(this.m_NumericUpDown_MaxTolerancePos.Value));
	            }
	        }
	        ss.sExtensionSetting.FlatSpaceY = UIPreference.ToInchLength(m_CurrentUnit,
	            Decimal.ToSingle(numFlatDistanceY.Value));
	        ss.sExtensionSetting.ManualSprayFrequency = Decimal.ToUInt16(numManualSprayFreq.Value);
	        ss.sExtensionSetting.ManualSprayTime = Decimal.ToUInt16(numManualSprayPeriod.Value);
            ss.sExtensionSetting.bExquisiteFeather = checkBoxExquisiteFeather.Checked;
            ss.sExtensionSetting.bJointFeather = m_sPrinterProperty.nHeadNumPerGroupY>=2 && checkBoxJointFeather.Checked;
            ss.sExtensionSetting.bConstantStep = checkBoxConstantStep.Checked;

	        autoBackHomeSetting1.OnGetPrinterSetting(ref ss);
	        if (PubFunc.IsZhuoZhan())
	        {
	            ZhuoZhanParam param = new ZhuoZhanParam();
	            param.zWorkPos = (uint) (UIPreference.ToInchLength(m_CurrentUnit, (float) numZWorkPos2.Value)*
	                                     m_sPrinterProperty.fPulsePerInchZ);
	            EpsonLCD.SetZhuoZhanParam(param);
	        }

            ss.sExtensionSetting.RunSpeed = m_cmbMoveXSpeed.SelectedIndex + 1;
            if (bSupportUv)
            {
                ss.sExtensionSetting.BackBeforePrint = checkBoxBackBeforPrintUV.Checked;
                ss.sExtensionSetting.AutoRunAfterPrint = checkBoxRunAfterPrintingUV.Checked;
                ss.sExtensionSetting.fRunDistanceAfterPrint = UIPreference.ToInchLength(m_CurrentUnit,
                decimal.ToSingle(numUVoffsetDistance.Value));
            }
            else
            {
                ss.sExtensionSetting.BackBeforePrint = checkBoxBackBeforPrint.Checked;
                ss.sExtensionSetting.AutoRunAfterPrint = checkBoxRunAfterPrinting.Checked;
                ss.sExtensionSetting.fRunDistanceAfterPrint = UIPreference.ToInchLength(m_CurrentUnit, decimal.ToSingle(numDistanceAfterPrint.Value));
            }
            ss.sExtensionSetting.MeasureWidthSensorPos = UIPreference.ToInchLength(m_CurrentUnit,
                Decimal.ToSingle(numericUpDown_SensorPos.Value));

            //ss.sExtensionSetting.bIsNewCalibration = (byte)(checkBoxzCaliNoStep.Checked ? 1 : 0);
            ss.sExtensionSetting.bCalibrationNoStep = (byte)(checkBoxCalibrationNoStep.Checked ? 1 : 0);

            if (SPrinterProperty.IsLingFeng_RollUV_16H())
            {
                LingFengParam lfPrarm = new LingFengParam();
                //lfPrarm.ZEnable = (byte)(cbxZaxisEnable.Checked ? 1 : 0);
                lfPrarm.WetEnable = (byte)(cbxCappingEnable.Checked ? 1 : 0);
                EpsonLCD.SetLingFengParam(lfPrarm);
            }

            if (SPrinterProperty.IsHuman())
            {
                HumanParam param = new HumanParam();
                param.PressInkOnTime = (uint)(numericUpDown_HumanPressInkTime.Value * 1000);
                param.PressInkRecoverTime = (uint)(numericUpDown_HumanRecoverTime.Value * 1000);
                param.ScarperLen = (int)numericUpDown_HumanScarperLen.Value;
                param.CleanXpos = (int)numericUpDown_HumanCleanXpos.Value;
                EpsonLCD.SetHumanParam(param);
            }

            if ((EpsonFeatherType)Convert.ToInt32(comboBoxFeatherPercent.SelectedIndex) == EpsonFeatherType.Custom)
            {
                ss.sExtensionSetting.FeatherNozzle = (float)NumFeatherNozzle.Value;
            }
            else
            {
                ss.sExtensionSetting.FeatherNozzle = 0;
            }
	    }

	    private PeripheralExtendedSettings _peripheralExtendedSettings;
        public void OnExtendedSettingsChange(PeripheralExtendedSettings ss)
        {
            _peripheralExtendedSettings = ss;
            UIPreference.SetValueAndClampWithMinMax(numericMotorgap, m_CurrentUnit, ss.BackAndForthLen);
            checkBoxEnableDetectRemainingRoll.Checked=
            numRollLength.Enabled = buttonFastCalculate.Enabled = ss.bEnableDetectRollLength;
            UIPreference.SetValueAndClampWithMinMax(numRollLength, m_CurrentUnit, ss.fCalculateRollLength);
            bool isFlora = SPrinterProperty.IsFloraT50OrT180();
            if (isFlora)
            {
                floraParamControl1.OnSettingChanged(ss.FloraParam);
            }
            UIPreference.SetValueAndClampWithMinMax(numericPlatformCorrect, m_CurrentUnit, ss.PlatformCorrect);
            if (m_sPrinterProperty.nWhiteInkNum > 0)
            {
                JetStatusEnum status = CoreInterface.GetBoardStatus();
                if (status != JetStatusEnum.PowerOff)
                {
                    WhiteInkCycle whiteInkCycle = new WhiteInkCycle();
                    if (EpsonLCD.GetWhiteInkCycleParam(ref whiteInkCycle))
                    {
                        _peripheralExtendedSettings.WhiteInkMixing = whiteInkCycle;
                    }
                }
                //numCycleTime.Value = (decimal) (_peripheralExtendedSettings.WhiteInkMixing.CycTime / 1000f);
                numCycleTime.Value = (decimal)((float)(_peripheralExtendedSettings.WhiteInkMixing.CycTime - _peripheralExtendedSettings.WhiteInkMixing.PulseTime) / 1000f);
                numPulseTime.Value = (decimal) (_peripheralExtendedSettings.WhiteInkMixing.PulseTime / 1000f);

                //numStirCycleTime.Value = _peripheralExtendedSettings.WhiteInkMixing.StirCyc;
                numStirCycleTime.Value = _peripheralExtendedSettings.WhiteInkMixing.StirCyc - _peripheralExtendedSettings.WhiteInkMixing.StirPulse;
                numStirPulseTime.Value = _peripheralExtendedSettings.WhiteInkMixing.StirPulse;
            }
        }
        public void OnGetExtendedSettingsChange(ref PeripheralExtendedSettings ss)
        {
            _peripheralExtendedSettings.BackAndForthLen = UIPreference.ToInchLength(m_CurrentUnit, (float)numericMotorgap.Value);
            bool isFlora = SPrinterProperty.IsFloraT50OrT180();
            if (isFlora)
            {
                floraParamControl1.OnGetSettings(ref _peripheralExtendedSettings.FloraParam);
            }
            _peripheralExtendedSettings.fCalculateRollLength = UIPreference.ToInchLength(m_CurrentUnit,
                (float) numRollLength.Value);
            _peripheralExtendedSettings.bEnableDetectRollLength = checkBoxEnableDetectRemainingRoll.Checked;
            if (m_sPrinterProperty.nWhiteInkNum > 0)
            {
                _peripheralExtendedSettings.WhiteInkMixing.CycTime = (uint)((float)numCycleTime.Value * 1000f + (float)numPulseTime.Value * 1000f);
                _peripheralExtendedSettings.WhiteInkMixing.PulseTime = (uint)((float)numPulseTime.Value * 1000f);
                _peripheralExtendedSettings.WhiteInkMixing.StirCyc = (ushort)(numStirCycleTime.Value + numStirPulseTime.Value);
                _peripheralExtendedSettings.WhiteInkMixing.StirPulse = (ushort)(numStirPulseTime.Value);

                //白墨搅拌参数的合法化
                if (_peripheralExtendedSettings.WhiteInkMixing.CycTime <= 0) _peripheralExtendedSettings.WhiteInkMixing.CycTime = 1000;
                if (_peripheralExtendedSettings.WhiteInkMixing.PulseTime <= 0) _peripheralExtendedSettings.WhiteInkMixing.PulseTime = 1000;
                if (_peripheralExtendedSettings.WhiteInkMixing.StirCyc <= 0) _peripheralExtendedSettings.WhiteInkMixing.StirCyc = 1;
                if (_peripheralExtendedSettings.WhiteInkMixing.StirPulse <= 0) _peripheralExtendedSettings.WhiteInkMixing.StirPulse = 1;
                

                JetStatusEnum status = CoreInterface.GetBoardStatus();
                if (status != JetStatusEnum.PowerOff)
                {
                    EpsonLCD.SetWhiteInkCycleParam(_peripheralExtendedSettings.WhiteInkMixing);
                }
            }
            _peripheralExtendedSettings.PlatformCorrect = UIPreference.ToInchLength(m_CurrentUnit,
            Decimal.ToSingle(numericPlatformCorrect.Value));
            ss = _peripheralExtendedSettings;
        }

		public void OnPreferenceChange( UIPreference up)
		{
//			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
                floraParamControl1.OnPreferenceChange(up);
                autoBackHomeSetting1.OnPreferenceChange(up);
                this.isDirty = false;
			}
		    _upLangIndex = up.LangIndex;
		}
 
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownStripeSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownStripeWidth);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownJobSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownLeftEdge);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownWidth);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownY);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownHeight);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownZSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownThickness);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NmericUpDownWaveLen);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumAutoCleanPosLen);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumAutoCleanPosMov);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownHeadToPaper);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownMesureHeight);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numFlatDistanceY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numZWorkPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCleanPosX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCleanPosY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numCleanPosZ);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericMotorgap);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSuctionStartPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numSuctionEndPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericPlatformCorrect);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numRollLength);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numUVoffsetDistance);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numUVLightInAdvance);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownBSZPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownCleanZPos);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numZMaxLen);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDown_SensorPos);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownStripeSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownStripeWidth, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownJobSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownLeftEdge, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownWidth, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownHeight, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownZSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownThickness, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis,this.m_NmericUpDownWaveLen,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumAutoCleanPosLen, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis,this.m_NumAutoCleanPosMov,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownHeadToPaper, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownMesureHeight, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numFlatDistanceY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numZWorkPos, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownBSZPos, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownCleanZPos, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanPosX, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanPosY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numCleanPosZ, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericMotorgap, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numSuctionStartPos, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numSuctionEndPos, this.m_ToolTip);

            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericPlatformCorrect, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numRollLength, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numUVoffsetDistance, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numUVLightInAdvance, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numZMaxLen, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown_SensorPos, this.m_ToolTip);
            this.isDirty = false;
		}

		private void m_ButtonMeasure_Click(object sender, System.EventArgs e)
		{
			CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,0);
		}

		private void m_ButtonZAuto_Click(object sender, System.EventArgs e)
		{
            MessageBox.Show(ResString.GetResString("StrMeasureZMaxMsg"), ResString.GetProductName(), MessageBoxButtons.OK,
		        MessageBoxIcon.Information);
		    float fMeshHight = 0;//UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numMeshHeight.Value));
            this.OnGetPrinterSetting(ref this._printerSetting);
            CoreInterface.SetPrinterSetting(ref this._printerSetting);

            CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 1);
		    bAutoMeasure = true;
		}

	    private void m_ButtonZManual_Click(object sender, System.EventArgs e)
	    {
	        UpdateZWorkPosToBoard();
	        //移动
	        int zSpeed = _printerSetting.sMoveSetting.nZMoveSpeed;
            float zMaxPos1 = UIPreference.ToInchLength(m_CurrentUnit, (float)numZMaxLen.Value);
            float fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownZSpace.Value));
            float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownThickness.Value));
            float fZWorkPos = zMaxPos1 - fZSpace - fPaperThick;
	        CoreInterface.MoveToPosCmd((int) (fZWorkPos*m_sPrinterProperty.fPulsePerInchZ), (int) AxisDir.Z, zSpeed);
	    }

	    /// <summary>
        /// 根据是否支持z终点传感器,按不同逻辑更新主板zworkpos参数
        /// </summary>
	    private void UpdateZWorkPosToBoard()
	    {
	        ByhxZMoveParam param;
	        if (ScorpionCoreInterface.GetByhxZMoveParam(out param))
	        {
	            float zWorkPos = UIPreference.ToInchLength(m_CurrentUnit, (float) numZWorkPos.Value);
                float zMaxPos1 = UIPreference.ToInchLength(m_CurrentUnit,(float) numZMaxLen.Value);
                float fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownZSpace.Value));
                float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit,
                    Decimal.ToSingle(m_NumericUpDownThickness.Value));
                zWorkPos = zMaxPos1 - (fZSpace + fPaperThick);
                if (zWorkPos < 0) 
                    zWorkPos = 0;
	            param.Flag = BitConverter.ToUInt32(new byte[] {(byte) 'B', (byte) 'Y', (byte) 'H', (byte) 'X'}, 0);
	            param.HeadToPaper =
	                (uint)
                        (zWorkPos * m_sPrinterProperty.fPulsePerInchZ);
                param.CleanPlace =
                    (int)
                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float)numericUpDownCleanZPos.Value)) *
                                   m_sPrinterProperty.fPulsePerInchZ);
                param.WetPlace =
                    (int)
                        Math.Round((UIPreference.ToInchLength(m_CurrentUnit, (float)numericUpDownBSZPos.Value)) *
                                   m_sPrinterProperty.fPulsePerInchZ);
                param.activeLen = 8;
                param.PaperThick = 0;
	            ScorpionCoreInterface.SetByhxZMoveParam(param);
                LogWriter.SaveOptionLog(string.Format("Basesetting zWorkPos={0};CleanPlace={1};WetPlace={2};fPulsePerInchZ={3}", param.HeadToPaper, param.CleanPlace, param.WetPlace, m_sPrinterProperty.fPulsePerInchZ));
	        }
            else
            {
                MessageBox.Show("获取ZMoveParam失败.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
	    }

//		private void m_ComboBoxWhiteInk_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			if(m_ComboBoxWhiteInk.SelectedIndex != 0)
//			{
//				m_CheckBoxAutoJumpWhite.Checked = false;
//			}
//			m_CheckBoxAutoJumpWhite.Enabled = (m_ComboBoxWhiteInk.SelectedIndex == 0);
//            isDirty = true;
//		}

        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBoxPixelStep && checkBoxPixelStep.Checked)
                m_CheckBoxKillBidir.Checked = false;
            if (sender == m_CheckBoxKillBidir && m_CheckBoxKillBidir.Checked)
                checkBoxPixelStep.Checked = false;
            isDirty = true;
        }

		private void m_ComboBoxFeatherType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    var type = GetFeatherTypeFromUi();
            this.m_NumericUpDownFeatherPercent.Visible = type == FeatherType.Advance || type == FeatherType.Uv;
            this.m_NmericUpDownWaveLen.Visible = type == FeatherType.Wave;
			isDirty = true;
		}

		private void CheckBoxReversePrint_CheckedChanged(object sender, System.EventArgs e)
		{
            //			if(bisSetting) return;
            //if (!SPrinterProperty.IsGongZengMeasureBeforPrint())
            //{
            //    this.m_CheckBoxMirror.Checked = CheckBoxReversePrint.Checked;
            //}
		}
        private void m_CheckBox_CorrectOffset_CheckedChanged(object sender, EventArgs e)
        {
            this.m_NumericUpDown_MaxTolerancePos.Enabled = m_CheckBox_CorrectOffset.Checked;
            isDirty = true;
        }


	    private int _upLangIndex = -1;
        private bool bAutoMeasure = false;
        private bool bLoaded = false;
        private void BaseSetting_Load(object sender, EventArgs e)
        {
            if (PubFunc.IsInDesignMode())
                return;
            //彩神英文"y连续"显示为"Header Disable"
            bool isFLORA = false;
            ushort pid, vid;
            pid = vid = 0;
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                if (vid == (ushort)VenderID.FLORA
                    || vid == (ushort)VenderID.FLORA_FLAT_UV
                    || vid == (ushort)VenderID.FLORA_BELT_TEXTILE
                    || vid == (ushort)VenderID.FLORA_FLAT_TEXTILE
)
                    isFLORA = true;
            }

            CultureInfo nutrual = new CultureInfo("en-US");
            if (_upLangIndex == nutrual.LCID && isFLORA)
            {
                m_CheckBoxYContinue.Text = "Header Disable";
            }
            bLoaded = true;
        }

	    private void comboBoxFeatherPercent_SelectedIndexChanged(object sender, EventArgs e)
	    {
	        EpsonFeatherType featherPercentType = (EpsonFeatherType) comboBoxFeatherPercent.SelectedIndex;
	        m_NumericUpDownFeather.Visible = featherPercentType == EpsonFeatherType.Custom;
            NumFeatherNozzle.Enabled = featherPercentType == EpsonFeatherType.Custom;
	        textBoxFeather.Visible = featherPercentType != EpsonFeatherType.Custom;
	        byte bmax = 0;
	        int percent = GetFeatherPercent(featherPercentType, ref bmax);
            textBoxFeather.Text =
            m_NumericUpDownFeather.Text =
                bmax == 1 ? "MAX" : string.Format("{0}%", percent); //特殊标记值101时显示为100%
            //bool isGz = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID();
            //if (isGz)
            //{
            //}
            //else
            //{
            //    textBoxFeather.Text =
            //        m_NumericUpDownFeather.Text =
            //            (percent%100 > 0 && percent > 100) ? "100%" : string.Format("{0}%", percent); //特殊标记值101时显示为100%
            //}
	    }

	    private int GetFeatherPercent(EpsonFeatherType featherPercentType,ref byte bMax)
	    {
	        int ret = 0;
	        bMax = 0;
            bool isGz = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID();
	        switch (featherPercentType)
	        {
	            case EpsonFeatherType.None:
	                ret = FeatherPercentNone;
	                break;
	            case EpsonFeatherType.Small:
	                ret = FeatherPercentSmall;
	                if (isGz)
	                    ret = GzFeatherPercentSmall;
	                break;
	            case EpsonFeatherType.Medium:
	                ret = FeatherPercentMedium;
	                if (isGz)
	                    ret = GzFeatherPercentMedium;
	                break;
	            case EpsonFeatherType.Large:
	                ret = FeatherPercentLarge;
	                bMax = 1;
	                break;
	            case EpsonFeatherType.Custom:
	                ret = Decimal.ToInt32(m_NumericUpDownFeather.Value);
	                break;
	            //case EpsonFeatherType.SuperLarge:
	            //    bMax = 2;
	            //    break;
	        }
	        return ret;
	    }

	    private void checkBoxFixPos_CheckedChanged(object sender, EventArgs e)
	    {
	        InitStripePlace(checkBoxFixPos.Checked);
	        m_ComboBoxPlace.SelectedIndex = 0;
	    }

	    private void InitStripePlace(bool bFixPosStripe)
	    {
            m_ComboBoxPlace.Items.Clear();
            int index = 0;
            foreach (InkStrPosEnum place in Enum.GetValues(typeof(InkStrPosEnum)))
            {
                InkStrPosEnum placeTemp = place;
#if DOUBLE_SIDE_PRINT_HAPOND// hapond 左右彩条反向
			    if (place == InkStrPosEnum.Left)
                    placeTemp = InkStrPosEnum.Right;
                if (place == InkStrPosEnum.Right)
                    placeTemp = InkStrPosEnum.Left;
#endif
                string cmode = ResString.GetEnumDisplayName(typeof(InkStrPosEnum), placeTemp);
                if (!bFixPosStripe)
                {
                    m_ComboBoxPlace.Items.Add(cmode);
                }
                else
                {
                    if (placeTemp == InkStrPosEnum.Left || placeTemp == InkStrPosEnum.Right) 
                        m_ComboBoxPlace.Items.Add(cmode);
                }
                if (index < cRadioButtons.Count)
                    cRadioButtons[index++].Text = cmode;
            }
	    }

	    private void buttonMoveToWorkPos_Click(object sender, EventArgs e)
	    {
	        if (m_sPrinterProperty.IsAOJET_UV())
	        {
	            AojetParam param = new AojetParam();
	            param.Flag = BitConverter.ToUInt32(new byte[] {(byte) 'A', (byte) 'J', (byte) 'E', (byte) 'T'}, 0);
	            param.zWorkPos =
	                (uint)
	                    ((UIPreference.ToInchLength(m_CurrentUnit, (float) numZWorkPos.Value))*
	                     m_sPrinterProperty.fPulsePerInchZ);
	            ScorpionCoreInterface.SetAojetParam(param);
	        }
	        else  //if(m_sPrinterProperty.IsDocan())
            {
                UpdateZWorkPosToBoard();

                //移动
	            int zSpeed = _printerSetting.sMoveSetting.nZMoveSpeed;
	            float fZWorkPos = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numZWorkPos.Value));
	            CoreInterface.MoveToPosCmd((int) (fZWorkPos*m_sPrinterProperty.fPulsePerInchZ), (int) AxisDir.Z, zSpeed);
            }
 	    }

        private void checkBoxExquisiteFeather_CheckedChanged(object sender, EventArgs e)
        {
            panelFeatherPram.Enabled = !checkBoxExquisiteFeather.Checked;
        }

	    private void buttonCalculate_Click(object sender, EventArgs e)
	    {
	        CalculateRollLengthForm calculateRollLengthForm = new CalculateRollLengthForm();
	        calculateRollLengthForm.OnExtendedSettingsChange(_peripheralExtendedSettings);
	        calculateRollLengthForm.StartPosition = FormStartPosition.CenterScreen;
	        DialogResult dr = calculateRollLengthForm.ShowDialog();
	        if (dr == DialogResult.OK)
	        {
                calculateRollLengthForm.OnGetExtendedSettingsChange(ref _peripheralExtendedSettings);
                numRollLength.Value = (decimal)UIPreference.ToDisplayLength(m_CurrentUnit, _peripheralExtendedSettings.fCalculateRollLength);
	        }
	    }

        private void checkBoxEnableDetectRemainingRoll_CheckedChanged(object sender, EventArgs e)
        {
            numRollLength.Enabled = buttonFastCalculate.Enabled = checkBoxEnableDetectRemainingRoll.Checked ;
        }

        private void buttonMoveToWorkPos2_Click(object sender, EventArgs e)
        {
            if (PubFunc.IsZhuoZhan())
            {
                ZhuoZhanParam param = new ZhuoZhanParam();
                param.zWorkPos =
                    (uint)
                        ((UIPreference.ToInchLength(m_CurrentUnit, (float)numZWorkPos2.Value)) *
                         m_sPrinterProperty.fPulsePerInchZ);
                EpsonLCD.SetZhuoZhanParam(param);
            }
            //移动
            int zSpeed = _printerSetting.sMoveSetting.nZMoveSpeed;
            float fZWorkPos = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(numZWorkPos2.Value));
            CoreInterface.MoveToPosCmd((int)(fZWorkPos * m_sPrinterProperty.fPulsePerInchZ), (int)AxisDir.Axis_5, zSpeed);
        }

        private void numUVoffsetDistance_ValueChanged(object sender, EventArgs e)
        {
            numUVLightInAdvance.Maximum = numUVoffsetDistance.Value;
        }

        private void checkBoxRunAfterPrinting_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.Name == "checkBoxRunAfterPrinting")
                {
                    if (cb.Checked)
                    {
                        checkBoxBackBeforPrint.Enabled = true;
                    }
                    else
                    {
                        checkBoxBackBeforPrint.Enabled = false;
                        checkBoxBackBeforPrint.Checked = false;
                    }
                }
                else 
                {
                    if (cb.Checked)
                    {
                        checkBoxBackBeforPrintUV.Enabled = true;
                    }
                    else
                    {
                        checkBoxBackBeforPrintUV.Enabled = false;
                        checkBoxBackBeforPrintUV.Checked = false;
                    }
                }
            }
        }

	    ///
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}
}
