/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
#define GONGZENG_DOUBLE  //默认为公正双脉宽-2011-03-25 11:54修正

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for KonicTemperature1.
    /// </summary>
    public class KonicTemperature1 : BYHXUserControl//System.Windows.Forms.UserControl
    {
        private SPrinterProperty m_rsPrinterPropery;//Only read for color order

        private int Voltage_LR_Num = 0;
        private int m_HeadNum = 0;
        private int m_TempNum = 0;
        private int m_HeadVoltageNum = 0;
        private int m_HeadPulseWidthNum = 0;
        private int m_InkBaoTempNum = 0;
        private int m_InkVolageNum = 0;
        private int m_VolageOffsetNum = 0;

        private float ADJUST_VOL_PER_HEAD = 8;
        private float VOL_COUNT_PER_HEAD = 1;
        private float Plus_COUNT_PER_HEAD = 1;
        //private const int VOL_COUNT_PER_POLARIS_HEAD = 2;
        //private const int VOL_COUNT_PER_SG1024_GRAY = 3;
        //private const int VOL_COUNT_PER_KM1024I_GRAY = 6;
        private byte m_StartHeadIndex = 0;
        private byte[] m_pMap;
        private bool m_bSpectra = false;
        private bool m_bKonic512 = false;
        private bool m_bKonic1800i = false;
        private bool m_bKonicM600 = false;
        private bool m_bSupportHeadHeat = false;
        private bool m_bXaar382 = false;
        private bool m_bPolaris = false;
        /// <summary>
        /// 是否是Polaris_GZ.
        /// </summary>
        private bool m_bExcept = false;
        private bool m_Konic512_1head2color = false;
        private bool m_bPolaris_V5_8 = false;
        private bool m_bPolaris_V5_8_Emerald = false;
        private bool m_bPolaris_V7_16 = false;
        private bool m_bSpectra_SG1024_Gray = false;
        private bool m_bKonic1024i_Gray = false;
        private bool m_bPolaris_V7_16_Emerald = false;
        private bool m_bPolaris_V7_16_Polaris = false;
        private bool m_bRicoHead = false;
        private bool m_bXaar501 = false;
        private bool m_bKyocera = false;
        private bool m_bKyocera300 = false;
        private bool m_bGma990 = false;
        private byte m_nTempCoff = 5;
        private bool m_b16BitTemp = false;
        /// <summary>
        /// 是否为垂直排列
        /// </summary>
        private bool m_bVerArrangement = false;
        private bool m_bMirrorArrangement = false;
        private bool m_b1head2color = false;
        private bool isDirty = false;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        private System.Windows.Forms.Label[] m_LabelLRIndex;
        private System.Windows.Forms.Label[] m_LabelHorHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadSet;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadCur;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadTeam;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageSet;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageCur;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageBase;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownPulseWidth;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownInkTempSet;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownInkTempCur;
        //xaar501
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageInk;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownAutoVoltageInkOffset;
        private System.Windows.Forms.Button[] m_ButtonWaveformDownload;
        private System.Windows.Forms.TextBox[] m_LabelSerialNumber;
        private System.Windows.Forms.TextBox[] m_TextBoxWaveName;
        private bool[] WhichRowVisble = new bool[9];

        int SNSendCount = 0;
        int WNSendCount = 0;
        int CurrSNSend = 0;
        int CurrWNSend = 0;
        List<string> SNinfoList = new List<string>();
        List<string> WNinfoList = new List<string>();
        //private bool[] WhichRowVisble = new bool[11];

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private System.Windows.Forms.Button m_ButtonToBoard;
        private System.Windows.Forms.Button m_ButtonRefresh;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxTemperature;
        private System.Windows.Forms.Label m_LabelLeft;
        private System.Windows.Forms.Label m_LabelRight;
        private System.Windows.Forms.Label m_LabelHead;
        private System.Windows.Forms.Label m_LabelHorSample;
        private System.Windows.Forms.Label m_labelVolSet;
        private System.Windows.Forms.Label m_labelVolCur;
        private System.Windows.Forms.Label m_LabelBaseVol;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownTempSetSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownTempCurSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownVolAdjustSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownVolCurSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownVolBaseSample;
        private System.Windows.Forms.Timer m_TimerRefresh;
        private System.Windows.Forms.CheckBox m_CheckBoxAutoRefresh;
        private System.Windows.Forms.Label m_LabelCurPulseWidth;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownPulseWidthSample;
        private System.Windows.Forms.Button m_ButtonDefault;
        private System.Windows.Forms.Label m_LabelRightMax;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownHeatTempSample;
        private System.Windows.Forms.Label m_labelHeatingTemp;
        private System.Windows.Forms.Label lblInkCartridgesTemp;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownInkTempSetSample;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownInkTempCurSample;
        private System.Windows.Forms.Label m_LabelInkTempSet;
        private NumericUpDown numericUpDown_VoltageInkSample;
        private Label label_VoltageInkSample;
        private NumericUpDown numericUpDown_AutoVoltageInkSample;
        private Label label_AutoVoltageSample;
        private Button buttonExport;
        private Button buttonImport;
        private Button button_WaveformDownloadSample;
        private Label label_WaveformDownloadSample;
        private TextBox label_SerialNumber;
        private Label label_SerialNumberSample;
        private TextBox textBox_WaveNameSample;
        private Label label_WaveNameSample;
        private System.ComponentModel.IContainer components;

        public KonicTemperature1()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            this.Load += new EventHandler(KonicTemperature1_Load);
        }
        bool bloaded = false;
        void KonicTemperature1_Load(object sender, EventArgs e)
        {
            m_ButtonDefault.Visible = true;
            buttonExport.Visible = 
            buttonImport.Visible = !m_bXaar382;
            bloaded = true;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KonicTemperature1));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            this.m_ButtonToBoard = new System.Windows.Forms.Button();
            this.m_ButtonRefresh = new System.Windows.Forms.Button();
            this.m_GroupBoxTemperature = new BYHXPrinterManager.GradientControls.Grouper();
            this.textBox_WaveNameSample = new System.Windows.Forms.TextBox();
            this.label_WaveNameSample = new System.Windows.Forms.Label();
            this.label_SerialNumber = new System.Windows.Forms.TextBox();
            this.label_SerialNumberSample = new System.Windows.Forms.Label();
            this.button_WaveformDownloadSample = new System.Windows.Forms.Button();
            this.label_WaveformDownloadSample = new System.Windows.Forms.Label();
            this.numericUpDown_VoltageInkSample = new System.Windows.Forms.NumericUpDown();
            this.label_VoltageInkSample = new System.Windows.Forms.Label();
            this.numericUpDown_AutoVoltageInkSample = new System.Windows.Forms.NumericUpDown();
            this.label_AutoVoltageSample = new System.Windows.Forms.Label();
            this.m_NumericUpDownInkTempSetSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelInkTempSet = new System.Windows.Forms.Label();
            this.m_NumericUpDownInkTempCurSample = new System.Windows.Forms.NumericUpDown();
            this.lblInkCartridgesTemp = new System.Windows.Forms.Label();
            this.m_labelHeatingTemp = new System.Windows.Forms.Label();
            this.m_NumericUpDownHeatTempSample = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownTempSetSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelLeft = new System.Windows.Forms.Label();
            this.m_LabelRight = new System.Windows.Forms.Label();
            this.m_LabelHead = new System.Windows.Forms.Label();
            this.m_LabelHorSample = new System.Windows.Forms.Label();
            this.m_labelVolSet = new System.Windows.Forms.Label();
            this.m_labelVolCur = new System.Windows.Forms.Label();
            this.m_LabelBaseVol = new System.Windows.Forms.Label();
            this.m_NumericUpDownTempCurSample = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownVolAdjustSample = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownVolCurSample = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownVolBaseSample = new System.Windows.Forms.NumericUpDown();
            this.m_LabelCurPulseWidth = new System.Windows.Forms.Label();
            this.m_NumericUpDownPulseWidthSample = new System.Windows.Forms.NumericUpDown();
            this.m_TimerRefresh = new System.Windows.Forms.Timer(this.components);
            this.m_CheckBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.m_ButtonDefault = new System.Windows.Forms.Button();
            this.m_LabelRightMax = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.m_GroupBoxTemperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VoltageInkSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoVoltageInkSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownInkTempSetSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownInkTempCurSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeatTempSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTempSetSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTempCurSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolAdjustSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolCurSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolBaseSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPulseWidthSample)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ButtonToBoard
            // 
            resources.ApplyResources(this.m_ButtonToBoard, "m_ButtonToBoard");
            this.m_ButtonToBoard.Name = "m_ButtonToBoard";
            this.m_ButtonToBoard.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
            // 
            // m_ButtonRefresh
            // 
            resources.ApplyResources(this.m_ButtonRefresh, "m_ButtonRefresh");
            this.m_ButtonRefresh.Name = "m_ButtonRefresh";
            this.m_ButtonRefresh.Click += new System.EventHandler(this.m_ButtonRefresh_Click);
            // 
            // m_GroupBoxTemperature
            // 
            this.m_GroupBoxTemperature.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxTemperature.BorderThickness = 1F;
            this.m_GroupBoxTemperature.Controls.Add(this.textBox_WaveNameSample);
            this.m_GroupBoxTemperature.Controls.Add(this.label_WaveNameSample);
            this.m_GroupBoxTemperature.Controls.Add(this.label_SerialNumber);
            this.m_GroupBoxTemperature.Controls.Add(this.label_SerialNumberSample);
            this.m_GroupBoxTemperature.Controls.Add(this.button_WaveformDownloadSample);
            this.m_GroupBoxTemperature.Controls.Add(this.label_WaveformDownloadSample);
            this.m_GroupBoxTemperature.Controls.Add(this.numericUpDown_VoltageInkSample);
            this.m_GroupBoxTemperature.Controls.Add(this.label_VoltageInkSample);
            this.m_GroupBoxTemperature.Controls.Add(this.numericUpDown_AutoVoltageInkSample);
            this.m_GroupBoxTemperature.Controls.Add(this.label_AutoVoltageSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownInkTempSetSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelInkTempSet);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownInkTempCurSample);
            this.m_GroupBoxTemperature.Controls.Add(this.lblInkCartridgesTemp);
            this.m_GroupBoxTemperature.Controls.Add(this.m_labelHeatingTemp);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownHeatTempSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownTempSetSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelLeft);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelRight);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHead);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHorSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_labelVolSet);
            this.m_GroupBoxTemperature.Controls.Add(this.m_labelVolCur);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelBaseVol);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownTempCurSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVolAdjustSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVolCurSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVolBaseSample);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelCurPulseWidth);
            this.m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownPulseWidthSample);
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxTemperature.GradientColors = style1;
            this.m_GroupBoxTemperature.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxTemperature, "m_GroupBoxTemperature");
            this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
            this.m_GroupBoxTemperature.PaintGroupBox = false;
            this.m_GroupBoxTemperature.RoundCorners = 10;
            this.m_GroupBoxTemperature.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxTemperature.ShadowControl = false;
            this.m_GroupBoxTemperature.ShadowThickness = 3;
            this.m_GroupBoxTemperature.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxTemperature.TitileGradientColors = style2;
            this.m_GroupBoxTemperature.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxTemperature.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // textBox_WaveNameSample
            // 
            resources.ApplyResources(this.textBox_WaveNameSample, "textBox_WaveNameSample");
            this.textBox_WaveNameSample.Name = "textBox_WaveNameSample";
            this.textBox_WaveNameSample.ReadOnly = true;
            // 
            // label_WaveNameSample
            // 
            resources.ApplyResources(this.label_WaveNameSample, "label_WaveNameSample");
            this.label_WaveNameSample.BackColor = System.Drawing.Color.Transparent;
            this.label_WaveNameSample.Name = "label_WaveNameSample";
            // 
            // label_SerialNumber
            // 
            resources.ApplyResources(this.label_SerialNumber, "label_SerialNumber");
            this.label_SerialNumber.Name = "label_SerialNumber";
            this.label_SerialNumber.ReadOnly = true;
            // 
            // label_SerialNumberSample
            // 
            resources.ApplyResources(this.label_SerialNumberSample, "label_SerialNumberSample");
            this.label_SerialNumberSample.BackColor = System.Drawing.Color.Transparent;
            this.label_SerialNumberSample.Name = "label_SerialNumberSample";
            // 
            // button_WaveformDownloadSample
            // 
            resources.ApplyResources(this.button_WaveformDownloadSample, "button_WaveformDownloadSample");
            this.button_WaveformDownloadSample.Name = "button_WaveformDownloadSample";
            this.button_WaveformDownloadSample.UseVisualStyleBackColor = true;
            // 
            // label_WaveformDownloadSample
            // 
            resources.ApplyResources(this.label_WaveformDownloadSample, "label_WaveformDownloadSample");
            this.label_WaveformDownloadSample.BackColor = System.Drawing.Color.Transparent;
            this.label_WaveformDownloadSample.Name = "label_WaveformDownloadSample";
            // 
            // numericUpDown_VoltageInkSample
            // 
            this.numericUpDown_VoltageInkSample.DecimalPlaces = 1;
            this.numericUpDown_VoltageInkSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_VoltageInkSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.numericUpDown_VoltageInkSample, "numericUpDown_VoltageInkSample");
            this.numericUpDown_VoltageInkSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_VoltageInkSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_VoltageInkSample.Name = "numericUpDown_VoltageInkSample";
            // 
            // label_VoltageInkSample
            // 
            resources.ApplyResources(this.label_VoltageInkSample, "label_VoltageInkSample");
            this.label_VoltageInkSample.BackColor = System.Drawing.Color.Transparent;
            this.label_VoltageInkSample.CausesValidation = false;
            this.label_VoltageInkSample.Name = "label_VoltageInkSample";
            // 
            // numericUpDown_AutoVoltageInkSample
            // 
            this.numericUpDown_AutoVoltageInkSample.DecimalPlaces = 1;
            this.numericUpDown_AutoVoltageInkSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.numericUpDown_AutoVoltageInkSample, "numericUpDown_AutoVoltageInkSample");
            this.numericUpDown_AutoVoltageInkSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_AutoVoltageInkSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown_AutoVoltageInkSample.Name = "numericUpDown_AutoVoltageInkSample";
            // 
            // label_AutoVoltageSample
            // 
            resources.ApplyResources(this.label_AutoVoltageSample, "label_AutoVoltageSample");
            this.label_AutoVoltageSample.BackColor = System.Drawing.Color.Transparent;
            this.label_AutoVoltageSample.Name = "label_AutoVoltageSample";
            // 
            // m_NumericUpDownInkTempSetSample
            // 
            this.m_NumericUpDownInkTempSetSample.DecimalPlaces = 1;
            this.m_NumericUpDownInkTempSetSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_NumericUpDownInkTempSetSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownInkTempSetSample, "m_NumericUpDownInkTempSetSample");
            this.m_NumericUpDownInkTempSetSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownInkTempSetSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownInkTempSetSample.Name = "m_NumericUpDownInkTempSetSample";
            // 
            // m_LabelInkTempSet
            // 
            resources.ApplyResources(this.m_LabelInkTempSet, "m_LabelInkTempSet");
            this.m_LabelInkTempSet.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelInkTempSet.Name = "m_LabelInkTempSet";
            // 
            // m_NumericUpDownInkTempCurSample
            // 
            this.m_NumericUpDownInkTempCurSample.DecimalPlaces = 1;
            this.m_NumericUpDownInkTempCurSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownInkTempCurSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownInkTempCurSample, "m_NumericUpDownInkTempCurSample");
            this.m_NumericUpDownInkTempCurSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownInkTempCurSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownInkTempCurSample.Name = "m_NumericUpDownInkTempCurSample";
            this.m_NumericUpDownInkTempCurSample.ReadOnly = true;
            // 
            // lblInkCartridgesTemp
            // 
            resources.ApplyResources(this.lblInkCartridgesTemp, "lblInkCartridgesTemp");
            this.lblInkCartridgesTemp.BackColor = System.Drawing.Color.Transparent;
            this.lblInkCartridgesTemp.Name = "lblInkCartridgesTemp";
            // 
            // m_labelHeatingTemp
            // 
            resources.ApplyResources(this.m_labelHeatingTemp, "m_labelHeatingTemp");
            this.m_labelHeatingTemp.BackColor = System.Drawing.Color.Transparent;
            this.m_labelHeatingTemp.Name = "m_labelHeatingTemp";
            // 
            // m_NumericUpDownHeatTempSample
            // 
            this.m_NumericUpDownHeatTempSample.DecimalPlaces = 1;
            this.m_NumericUpDownHeatTempSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownHeatTempSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownHeatTempSample, "m_NumericUpDownHeatTempSample");
            this.m_NumericUpDownHeatTempSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownHeatTempSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownHeatTempSample.Name = "m_NumericUpDownHeatTempSample";
            this.m_NumericUpDownHeatTempSample.ReadOnly = true;
            // 
            // m_NumericUpDownTempSetSample
            // 
            this.m_NumericUpDownTempSetSample.DecimalPlaces = 1;
            this.m_NumericUpDownTempSetSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_NumericUpDownTempSetSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownTempSetSample, "m_NumericUpDownTempSetSample");
            this.m_NumericUpDownTempSetSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownTempSetSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownTempSetSample.Name = "m_NumericUpDownTempSetSample";
            this.m_NumericUpDownTempSetSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            this.m_NumericUpDownTempSetSample.Enter += new System.EventHandler(this.m_NumericUpDownTempSetSample_Enter);
            // 
            // m_LabelLeft
            // 
            resources.ApplyResources(this.m_LabelLeft, "m_LabelLeft");
            this.m_LabelLeft.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLeft.Name = "m_LabelLeft";
            // 
            // m_LabelRight
            // 
            resources.ApplyResources(this.m_LabelRight, "m_LabelRight");
            this.m_LabelRight.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelRight.Name = "m_LabelRight";
            // 
            // m_LabelHead
            // 
            resources.ApplyResources(this.m_LabelHead, "m_LabelHead");
            this.m_LabelHead.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHead.Name = "m_LabelHead";
            // 
            // m_LabelHorSample
            // 
            resources.ApplyResources(this.m_LabelHorSample, "m_LabelHorSample");
            this.m_LabelHorSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelHorSample.Name = "m_LabelHorSample";
            // 
            // m_labelVolSet
            // 
            resources.ApplyResources(this.m_labelVolSet, "m_labelVolSet");
            this.m_labelVolSet.BackColor = System.Drawing.Color.Transparent;
            this.m_labelVolSet.Name = "m_labelVolSet";
            // 
            // m_labelVolCur
            // 
            resources.ApplyResources(this.m_labelVolCur, "m_labelVolCur");
            this.m_labelVolCur.BackColor = System.Drawing.Color.Transparent;
            this.m_labelVolCur.Name = "m_labelVolCur";
            // 
            // m_LabelBaseVol
            // 
            resources.ApplyResources(this.m_LabelBaseVol, "m_LabelBaseVol");
            this.m_LabelBaseVol.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelBaseVol.Name = "m_LabelBaseVol";
            // 
            // m_NumericUpDownTempCurSample
            // 
            this.m_NumericUpDownTempCurSample.DecimalPlaces = 1;
            this.m_NumericUpDownTempCurSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownTempCurSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownTempCurSample, "m_NumericUpDownTempCurSample");
            this.m_NumericUpDownTempCurSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownTempCurSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownTempCurSample.Name = "m_NumericUpDownTempCurSample";
            this.m_NumericUpDownTempCurSample.ReadOnly = true;
            this.m_NumericUpDownTempCurSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownVolAdjustSample
            // 
            this.m_NumericUpDownVolAdjustSample.DecimalPlaces = 1;
            this.m_NumericUpDownVolAdjustSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_NumericUpDownVolAdjustSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownVolAdjustSample, "m_NumericUpDownVolAdjustSample");
            this.m_NumericUpDownVolAdjustSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownVolAdjustSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownVolAdjustSample.Name = "m_NumericUpDownVolAdjustSample";
            this.m_NumericUpDownVolAdjustSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownVolCurSample
            // 
            this.m_NumericUpDownVolCurSample.DecimalPlaces = 1;
            this.m_NumericUpDownVolCurSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownVolCurSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownVolCurSample, "m_NumericUpDownVolCurSample");
            this.m_NumericUpDownVolCurSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownVolCurSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownVolCurSample.Name = "m_NumericUpDownVolCurSample";
            this.m_NumericUpDownVolCurSample.ReadOnly = true;
            this.m_NumericUpDownVolCurSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownVolBaseSample
            // 
            this.m_NumericUpDownVolBaseSample.DecimalPlaces = 1;
            this.m_NumericUpDownVolBaseSample.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_NumericUpDownVolBaseSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownVolBaseSample, "m_NumericUpDownVolBaseSample");
            this.m_NumericUpDownVolBaseSample.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownVolBaseSample.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownVolBaseSample.Name = "m_NumericUpDownVolBaseSample";
            this.m_NumericUpDownVolBaseSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelCurPulseWidth
            // 
            resources.ApplyResources(this.m_LabelCurPulseWidth, "m_LabelCurPulseWidth");
            this.m_LabelCurPulseWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCurPulseWidth.Name = "m_LabelCurPulseWidth";
            // 
            // m_NumericUpDownPulseWidthSample
            // 
            this.m_NumericUpDownPulseWidthSample.DecimalPlaces = 1;
            this.m_NumericUpDownPulseWidthSample.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.InterceptArrowKeys = false;
            resources.ApplyResources(this.m_NumericUpDownPulseWidthSample, "m_NumericUpDownPulseWidthSample");
            this.m_NumericUpDownPulseWidthSample.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.Name = "m_NumericUpDownPulseWidthSample";
            this.m_NumericUpDownPulseWidthSample.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.m_NumericUpDownPulseWidthSample.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_TimerRefresh
            // 
            this.m_TimerRefresh.Interval = 1000;
            this.m_TimerRefresh.Tick += new System.EventHandler(this.m_TimerRefresh_Tick);
            // 
            // m_CheckBoxAutoRefresh
            // 
            resources.ApplyResources(this.m_CheckBoxAutoRefresh, "m_CheckBoxAutoRefresh");
            this.m_CheckBoxAutoRefresh.Name = "m_CheckBoxAutoRefresh";
            this.m_CheckBoxAutoRefresh.CheckedChanged += new System.EventHandler(this.m_CheckBoxAutoRefresh_CheckedChanged);
            // 
            // m_ButtonDefault
            // 
            resources.ApplyResources(this.m_ButtonDefault, "m_ButtonDefault");
            this.m_ButtonDefault.Name = "m_ButtonDefault";
            this.m_ButtonDefault.Click += new System.EventHandler(this.m_ButtonDefault_Click);
            // 
            // m_LabelRightMax
            // 
            this.m_LabelRightMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelRightMax, "m_LabelRightMax");
            this.m_LabelRightMax.Name = "m_LabelRightMax";
            // 
            // buttonExport
            // 
            resources.ApplyResources(this.buttonExport, "buttonExport");
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // KonicTemperature1
            // 
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.m_CheckBoxAutoRefresh);
            this.Controls.Add(this.m_ButtonToBoard);
            this.Controls.Add(this.m_ButtonRefresh);
            this.Controls.Add(this.m_GroupBoxTemperature);
            this.Controls.Add(this.m_ButtonDefault);
            this.Controls.Add(this.m_LabelRightMax);
            resources.ApplyResources(this, "$this");
            this.Name = "KonicTemperature1";
            this.m_GroupBoxTemperature.ResumeLayout(false);
            this.m_GroupBoxTemperature.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VoltageInkSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AutoVoltageInkSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownInkTempSetSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownInkTempCurSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeatTempSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTempSetSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownTempCurSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolAdjustSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolCurSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownVolBaseSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPulseWidthSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_rsPrinterPropery = sp;
            m_b16BitTemp = ScorpionCoreInterface.GetIsSupport16bitTemp();
            SFWFactoryData fwData = new SFWFactoryData();
            bool bGet = (CoreInterface.GetFWFactoryData(ref fwData) > 0);
            if (bGet)
            {
                if (fwData.m_nTempCoff > 0)
                    m_nTempCoff = fwData.m_nTempCoff;
                LogWriter.SaveOptionLog(string.Format("***********m_nTempCoff = {0}", fwData.m_nTempCoff));
            }

#if true
            uint uiHtype = 0;
            CoreInterface.GetUIHeadType(ref uiHtype);
            m_bKonic512 = (uiHtype & 0x01) != 0;
            m_bXaar382 = (uiHtype & 0x02) != 0; ;
            m_bSpectra = (uiHtype & 0x04) != 0;
            m_bPolaris = (uiHtype & 0x08) != 0;
            m_bPolaris_V5_8 = (uiHtype & 0x10) != 0; ;
            m_bExcept = (uiHtype & 0x20) != 0;
            m_bPolaris_V7_16 = (uiHtype & 0x40) != 0;
            m_bKonic1024i_Gray = (uiHtype & 0x80) != 0;
            m_bSpectra_SG1024_Gray = (uiHtype & 0x100) != 0;
            m_bXaar501 = (uiHtype & 0x200) != 0;//pan dan Xaar501?
#else
			m_bSpectra = SPrinterProperty.IsSpectra(sp.ePrinterHead);
			m_bKonic512 = SPrinterProperty.IsKonica512 (sp.ePrinterHead);
			m_bXaar382 = (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl||sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_60pl);			
			m_bPolaris = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			//			m_bPolaris_V5_8 = SPrinterProperty.IsPolaris(sp.ePrinterHead);
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
				m_bExcept = (sBoardInfo.m_nBoardManufatureID == 0xB || sBoardInfo.m_nBoardManufatureID == 0x8b);
			}
#endif

            m_bVerArrangement = ((sp.bSupportBit1 & 2) != 0);
            m_bMirrorArrangement = m_rsPrinterPropery.IsMirrorArrangement();
            m_b1head2color = (m_rsPrinterPropery.nOneHeadDivider == 2);
            m_Konic512_1head2color = m_b1head2color && m_bKonic512;
            m_bPolaris_V7_16_Emerald = m_bPolaris_V7_16 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
            m_bPolaris_V7_16_Polaris = m_bPolaris_V7_16 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_80pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_80pl);
            m_bPolaris_V5_8_Emerald = m_bPolaris_V5_8 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
            m_bRicoHead = sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
                || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
                || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl;
            m_bKyocera = SPrinterProperty.IsKyocera(sp.ePrinterHead);
            m_bKyocera300 = SPrinterProperty.IsKyocera300(sp.ePrinterHead);
            m_bKonic1800i = SPrinterProperty.IsKonic1800i(sp.ePrinterHead);
            m_bKonicM600 = sp.ePrinterHead == PrinterHeadEnum.Konica_M600SH_2C;
            m_bGma990 = sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA9905300_5pl || sp.ePrinterHead == PrinterHeadEnum.Fujifilm_GMA3305300_5pl;
            if (m_bPolaris)
                VOL_COUNT_PER_HEAD = 2;
            if (m_bSpectra_SG1024_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bKonic1024i_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bPolaris_V7_16_Polaris)
                VOL_COUNT_PER_HEAD = 1f / 2f;
            if (m_bKyocera)
                VOL_COUNT_PER_HEAD = 2;
            if (m_bKonic1800i || m_bKonicM600)
                VOL_COUNT_PER_HEAD = 4;

            if (m_bKonic512)
                Voltage_LR_Num = 6;
            else
                Voltage_LR_Num = 0;

            int rowNumPerHead = sp.nHeadNumPerColor * m_rsPrinterPropery.nOneHeadDivider;
            if (m_bMirrorArrangement)
                rowNumPerHead /= 2;
            //if(CheckComponentChange(sp))
            {
                //m_ColorNum = sp.nColorNum;
                //m_GroupNum = sp.nHeadNum/sp.nColorNum;

                m_HeadNum = sp.nHeadNum;
                m_TempNum = sp.nHeadNum;
                m_HeadVoltageNum = sp.nHeadNum * (int)VOL_COUNT_PER_HEAD;
                if (m_bKonic512)
                {
                    m_HeadNum /= rowNumPerHead;//sp.nHeadNumPerColor;
                    m_TempNum = m_HeadNum;
                }
                else if (m_bXaar501)//501
                {
                    this.m_InkVolageNum = m_HeadNum;
                    this.m_VolageOffsetNum = m_HeadNum;
                    this.m_HeadVoltageNum = m_HeadNum * (int)ADJUST_VOL_PER_HEAD;
                    this.m_TempNum = m_HeadNum * 3;
                }
                else if (m_bPolaris)
                {
                    if (!m_bExcept)
                    {
                        if (m_bPolaris_V5_8)
                        {
                            if (m_bPolaris_V5_8_Emerald)
                            {
                                m_HeadNum = m_HeadNum / sp.nHeadNumPerColor;// * VOLCOUNTPERPOLARISHEAD;
                                m_TempNum = m_HeadNum;
                                m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                            }
                            else
                            {
                                m_HeadNum = m_HeadNum / 4;
                                m_HeadVoltageNum = m_HeadNum;
                                m_TempNum = (int)(m_HeadVoltageNum * VOL_COUNT_PER_HEAD);
                            }
                        }
                        else if (m_bPolaris_V7_16)
                        {
                            if (m_bPolaris_V7_16_Emerald)
                            {
                                m_HeadVoltageNum = m_HeadNum;
                                m_TempNum = m_HeadNum;// * VOLCOUNTPERPOLARISHEAD;
                            }
                            else if (m_bPolaris_V7_16_Polaris)
                            {
                                m_HeadNum = m_HeadNum / 4;
                                m_TempNum = m_HeadNum;
                                m_HeadVoltageNum = m_HeadNum / 2;
                            }
                            else
                            {
                                m_HeadNum = m_HeadNum / 4;
                                m_TempNum = m_HeadNum;
                                m_HeadVoltageNum = (int)(m_HeadNum / VOL_COUNT_PER_HEAD);
                            }
                        }
                        else
                        {
                            //if ((sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl
                            //     || sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl))
                            //{
                            //    m_HeadVoltageNum = m_HeadNum;
                            //    m_TempNum = m_HeadNum;// * VOLCOUNTPERPOLARISHEAD;
                            //}
                            //else
                            {
                                m_HeadNum = m_HeadNum / 4;
                                m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                                m_TempNum = m_HeadVoltageNum;
                            }
                        }
                    }
                    else
                    {
                        m_HeadNum = sp.nHeadNum / 4;
                        //						if(m_b1head2color)//是否为一头两色
                        //						{
                        //							if(m_HeadNum <sp.nColorNum)
                        //								m_HeadNum = sp.nColorNum;
                        //						}
#if GONGZENG_DOUBLE
                        m_HeadVoltageNum = m_HeadNum * 2;
#else
						m_HeadVoltageNum = m_HeadNum;
#endif
                    }
                }
                else if (m_bRicoHead)
                {
                    m_InkBaoTempNum = sp.nColorNum;
                    m_HeadNum = sp.nHeadNum / 2;
                    m_TempNum = sp.nHeadNum;
                    m_HeadVoltageNum = sp.nHeadNum;
                }
                else if (m_bKonic1024i_Gray)
                {
                    if (CoreInterface.IsKm1024I_AS_4HEAD())
                        m_HeadNum = sp.nHeadNum / 2;
                    else
                        m_HeadNum = sp.nHeadNum;
                    m_TempNum = m_HeadNum / 2;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }
                else if (m_bSpectra_SG1024_Gray)
                {
                    if (CoreInterface.IsSG1024_AS_8_HEAD())
                        m_HeadNum = sp.nHeadNum / rowNumPerHead;
                    else
                        m_HeadNum = sp.nHeadNum;

                    m_TempNum = m_HeadNum;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }
                else if (m_bKyocera)
                {
                    m_HeadNum = (byte)(sp.nHeadNum / rowNumPerHead);
                    m_TempNum = m_HeadNum;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }
                else if (m_bKonic1800i)
                {
                    // 1800i 8个电压按俩列排列,一列太长
                    m_HeadNum = sp.nHeadNum*2/rowNumPerHead;
                    m_TempNum = sp.nHeadNum/rowNumPerHead;
                    m_HeadVoltageNum = (int) (m_HeadNum*VOL_COUNT_PER_HEAD);
                }
                else
                {
                    m_HeadNum = (byte)(sp.nHeadNum / rowNumPerHead);
                    m_TempNum = m_HeadNum;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }

                Plus_COUNT_PER_HEAD = VOL_COUNT_PER_HEAD;
                m_HeadPulseWidthNum = m_HeadVoltageNum;
                if (m_bSpectra_SG1024_Gray)
                {
                    Plus_COUNT_PER_HEAD = 5;
                    m_HeadPulseWidthNum = (int)(m_HeadNum * Plus_COUNT_PER_HEAD);
                    if (!CoreInterface.IsSG1024_AS_8_HEAD())
                    {
                        if (m_b1head2color)
                        {
                            m_HeadNum /= 2;
                            m_TempNum /= 2;
                            m_HeadVoltageNum /= 2;
                            m_HeadPulseWidthNum /= 2;
                        }
                    }
                }

                if (m_HeadNum > 12 || m_bKonic1800i || m_bGma990)
                {
                    //Change the group width = dialog width *2
                    const int control_margin = 8;
                    int deta = (LayoutHelper.MinSpace) * (m_HeadNum - 12);
                    if (deta > 0)
                    {
                        m_GroupBoxTemperature.Width += deta;
                        this.Width += deta;
                    }
                    m_LabelRightMax.Left = m_GroupBoxTemperature.Right + control_margin - m_LabelRightMax.Width;
                }
                m_StartHeadIndex = 0;
                //m_pMap = (byte[])sp.pElectricMap.Clone();
                //CoreInterface.GetHeadMap(m_pMap,m_pMap.Length);
                int nmap_input = 1;
                if (sp.ePrinterHead == PrinterHeadEnum.Spectra_S_128)
                {
                    nmap_input = 2;
                }
                int imax = Math.Max(m_HeadVoltageNum, m_TempNum);//修改原因:北极星v5-8时电压数小于温度数时温度map会出错
                imax = Math.Max(imax, m_HeadPulseWidthNum);
                m_pMap = new byte[imax];
                for (int i = 0; i < imax; i++)
                {
                    m_pMap[i] = (byte)i;
                }
                m_bSupportHeadHeat = sp.bSupportHeadHeat || m_bRicoHead || sp.IsALLWIN_512i_HighSpeed() || m_bXaar501 || m_bKonic1800i || m_bKonicM600;
                //this.WhichRowVisble={设置温度,当前温度,加热器温度,修正电压,当前基准电压,基准电压,脉宽,墨水设置温度,墨盒当前温度,墨水电压,墨水自动电压,喷头序列号,波形名称,波形下载}
                if (m_bSpectra)
                {
                    m_bSupportHeadHeat = true;
                    if (m_bPolaris)
                    {
                        if (m_bExcept)
                            this.WhichRowVisble = new bool[13] { false, false, false, false, false, false, true, false, false, false, false, false, false };
                        else if (m_bPolaris_V5_8_Emerald || m_bPolaris_V7_16_Emerald)
                            this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false, false, false, false };
                        else
                            this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, true, true, true, true, true, false, false, false, false, false, false };
                    }
                    else if (m_bSpectra_SG1024_Gray)
                    {
                        this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false, false, false, false };
                    }
                    else
                        this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false, false, false, false };
                }
                else if (m_bKonic512)
                {
                    this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, false, false, false, false, false, false, false };
                }
                else if (m_bXaar382)
                {
                    this.WhichRowVisble = new bool[13] { false, true, false, true, true, false, false, false, false, false, false, false, false };
                }
                else if (m_bXaar501)
                {
                    this.WhichRowVisble = new bool[13] { true, true, false, true, false, false, false, false, true, true, false, false, false };
                }
                else if (m_bRicoHead)
                {
                    this.WhichRowVisble = new bool[13] { true, true, false, false, false, false, false, true, true, false, false, false, false };
                    m_labelHeatingTemp.Text = lblInkCartridgesTemp.Text;
                }
                else if (m_bGma990)// ||
                {
                    this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, false, false, false, false, true, true, true };
                }
                else if (m_bKyocera)
                {
                    //this.WhichRowVisble={设置温度,当前温度,加热器温度,修正电压,当前基准电压,基准电压,脉宽,墨水设置温度,墨盒当前温度,墨水电压,墨水自动电压,喷头序列号,波形名称,波形下载}
                    //fw没修正bug前,先关掉电压设置功能
                    //this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, false, false, false, false, false, false, false, false, false, false };

                    if (EpsonLCD.IsSupportVoltage_KY())
                    {
                        bool isSupportVoltage = sp.IsDocan() ? PubFunc.IsFactoryUser() : true;
                        this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, isSupportVoltage, false, isSupportVoltage, false, false, false, false, false, false, false };
                    }
                    else
                    {
                        this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, false, false, false, false, false, false, false, false, false, false };

                        if (!SPrinterProperty.IsFlora() && !SPrinterProperty.IsYUEDA())//彩神不要提示, 越达不要提示
                        {
                            MessageBox.Show(SErrorCode.GetResString("Software_MainBoardNoSupportSetKyVol"), ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }
                    }
                    
                }
                else if (m_bKonic1800i)
                {
                    this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser), true, false, false, false, false, false, false, false };
                }
                else
                {
                    this.WhichRowVisble = new bool[13] { m_bSupportHeadHeat, true, false, true, true, true, false, false, false, false, false, false, false };
                }

                if (m_bXaar382)
                {
                    m_labelVolSet.Text = "Vtrim";
                    m_labelVolCur.Text = "PWM";
                }

                m_LabelLeft.Visible = this.WhichRowVisble[0];
                m_LabelRight.Visible = this.WhichRowVisble[1];
                m_labelHeatingTemp.Visible = this.WhichRowVisble[2];
                m_labelVolSet.Visible = this.WhichRowVisble[3];
                m_labelVolCur.Visible = this.WhichRowVisble[4];
                m_LabelBaseVol.Visible = this.WhichRowVisble[5];
                m_LabelCurPulseWidth.Visible = this.WhichRowVisble[6];
                m_LabelInkTempSet.Visible = this.WhichRowVisble[7];
                //m_LabelInkTempSet.Visible = false;
                lblInkCartridgesTemp.Visible = false;
                //新功能替代掉原有位置epson独有两个属性
                if (m_bXaar501)
                {
                    label_VoltageInkSample.Visible = this.WhichRowVisble[8];
                }
                else
                {
                    lblInkCartridgesTemp.Visible = this.WhichRowVisble[8];
                }
                label_AutoVoltageSample.Visible = this.WhichRowVisble[9];
                label_SerialNumberSample.Visible = this.WhichRowVisble[10];
                label_WaveNameSample.Visible = this.WhichRowVisble[11];
                label_WaveformDownloadSample.Visible = this.WhichRowVisble[12];
                if(m_bKonicM600)
                {
                    m_NumericUpDownVolBaseSample.ReadOnly = true;
                }
                ClearComponent();
                CreateComponent();
                LayoutComponent();
                AppendComponent();
                this.isDirty = false;
            }
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            this.isDirty = false;
        }
        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
        }

        private void OnGetRealTimeFromUI(ref SRealTimeCurrentInfo sRT)
        {
            for (int i = 0; i < m_TempNum; i++)
            {
                int nMap = m_pMap[i];
                if (m_bSupportHeadHeat && (!m_bRicoHead || (m_bRicoHead && i < m_InkBaoTempNum)) && (!m_bXaar501 || (m_bXaar501 && i < m_HeadNum)))
                {
                    sRT.cTemperatureSet[nMap] = Decimal.ToSingle(m_NumericUpDownHeadSet[i].Value);
                }
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cVoltage[nMap] = Decimal.ToSingle(m_NumericUpDownVoltageSet[i].Value);
                sRT.cVoltageBase[nMap] = Decimal.ToSingle(m_NumericUpDownVoltageBase[i].Value);
            }
            for (int i = 0; i < m_HeadPulseWidthNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cPulseWidth[nMap] = Decimal.ToSingle(m_NumericUpDownPulseWidth[i].Value);
            }
            for (int i = 0; i < m_InkVolageNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cXaarVoltageInk[nMap] = Decimal.ToSingle(m_NumericUpDownVoltageInk[i].Value);
            }
            for (int i = 0; i < m_VolageOffsetNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cXaarVoltageOffset[nMap] = Decimal.ToSingle(m_NumericUpDownAutoVoltageInkOffset[i].Value);
            }
        }
        public void OnRenewRealTime(SRealTimeCurrentInfo info)
        {
            if ((m_Konic512_1head2color
                || (m_bExcept && m_b1head2color)
                //				|| m_bRicoHead
                )
                && !m_bVerArrangement
                )
            {
                ReMapVolAndTempToUI(info);
            }
            else if (m_bPolaris_V7_16_Polaris)
            {
#if false
                LogWriter.WriteLog(new string[] { string.Format("m_bPolaris_V7_16_Polaris=[{0}];m_bMirrorArrangement=[{1}]", m_bPolaris_V7_16_Polaris, m_bMirrorArrangement) }, true);
                LogWriter.WriteLog(new string[] { string.Format("cTemperatureCur before remap=[{0}]", string.Join(",", info.cTemperatureCur)) }, true);
                ReMapTempBufferForV7_16_Polaris(ref info.cTemperatureCur, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cTemperatureCur after remap=[{0}]", string.Join(",", info.cTemperatureCur)) }, true);

                LogWriter.WriteLog(new string[] { string.Format("cTemperatureCur2 before remap=[{0}]", string.Join(",", info.cTemperatureCur2)) }, true);
                ReMapTempBufferForV7_16_Polaris(ref info.cTemperatureCur2, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cTemperatureCur2 after remap=[{0}]", string.Join(",", info.cTemperatureCur2)) }, true);

                LogWriter.WriteLog(new string[] { string.Format("cTemperatureSet before remap=[{0}]", string.Join(",", info.cTemperatureSet)) }, true);
                ReMapTempBufferForV7_16_Polaris(ref info.cTemperatureSet, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cTemperatureSet after remap=[{0}]", string.Join(",", info.cTemperatureSet)) }, true);
#endif
                LogWriter.WriteLog(new string[] { string.Format("cVoltage before remap=[{0}]", string.Join(",", info.cVoltage)) }, true);
                ReMapVolBufferForV7_16_Polaris(ref info.cVoltage, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cVoltage after remap=[{0}]", string.Join(",", info.cVoltage)) }, true);

                LogWriter.WriteLog(new string[] { string.Format("cVoltageBase before remap=[{0}]", string.Join(",", info.cVoltageBase)) }, true);
                ReMapVolBufferForV7_16_Polaris(ref info.cVoltageBase, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cVoltageBase after remap=[{0}]", string.Join(",", info.cVoltageBase)) }, true);

                LogWriter.WriteLog(new string[] { string.Format("cVoltageCurrent before remap=[{0}]", string.Join(",", info.cVoltageCurrent)) }, true);
                ReMapVolBufferForV7_16_Polaris(ref info.cVoltageCurrent, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cVoltageCurrent after remap=[{0}]", string.Join(",", info.cVoltageCurrent)) }, true);

                LogWriter.WriteLog(new string[] { string.Format("cPulseWidth before remap=[{0}]", string.Join(",", info.cPulseWidth)) }, true);
                ReMapVolBufferForV7_16_Polaris(ref info.cPulseWidth, m_rsPrinterPropery.nColorNum, true);
                LogWriter.WriteLog(new string[] { string.Format("cPulseWidth after remap=[{0}]", string.Join(",", info.cPulseWidth)) }, true);
            }
            else if (m_bMirrorArrangement && m_rsPrinterPropery.nColorNum > 1)
            {
                HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
                if (hbType != HEAD_BOARD_TYPE.SG1024_4H && 
                    hbType != HEAD_BOARD_TYPE.SG1024_4H_GRAY &&
                    hbType != HEAD_BOARD_TYPE.M600SH_4HEAD)
                {
                    ReMapBufferFor_Mirror(ref info.cTemperatureCur, m_rsPrinterPropery.nColorNum, true, m_b1head2color, 1);
                    ReMapBufferFor_Mirror(ref info.cTemperatureCur2, m_rsPrinterPropery.nColorNum, true, m_b1head2color, 1);
                    ReMapBufferFor_Mirror(ref info.cTemperatureSet, m_rsPrinterPropery.nColorNum, true, m_b1head2color, 1);
                    ReMapBufferFor_Mirror(ref info.cVoltage, m_rsPrinterPropery.nColorNum, true, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                    ReMapBufferFor_Mirror(ref info.cVoltageBase, m_rsPrinterPropery.nColorNum, true, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                    ReMapBufferFor_Mirror(ref info.cVoltageCurrent, m_rsPrinterPropery.nColorNum, true, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                    ReMapBufferFor_Mirror(ref info.cPulseWidth, m_rsPrinterPropery.nColorNum, true, m_b1head2color, 1);
                }
            }
            for (int i = 0; i < m_TempNum; i++)
            {
                int nMap = m_pMap[i];
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownHeadCur[i], info.cTemperatureCur[nMap]);
                if (m_bSupportHeadHeat)
                {
                    if (((!m_bRicoHead && !m_bXaar501) || ((m_bRicoHead || m_bXaar501) && i < m_HeadNum)))
                        UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownHeadSet[i], info.cTemperatureSet[nMap]);
                    UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownHeadTeam[i], info.cTemperatureCur2[nMap]);
                }
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];

                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageCur[i], info.cVoltageCurrent[nMap]);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageBase[i], info.cVoltageBase[nMap]);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageSet[i], info.cVoltage[nMap]);
            }
            for (int i = 0; i < m_InkVolageNum; i++)
            {
                int nMap = m_pMap[i];
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageInk[i], info.cXaarVoltageInk[nMap]);
            }
            for (int i = 0; i < m_VolageOffsetNum; i++)
            {
                int nMap = m_pMap[i];
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownAutoVoltageInkOffset[i], info.cXaarVoltageOffset[nMap]);
            }
            for (int i = 0; i < m_HeadPulseWidthNum; i++)
            {
                int nMap = m_pMap[i];
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownPulseWidth[i], info.cPulseWidth[nMap]);
            }
#if EpsonLcd
			byte[] buf = new byte[CoreConst.MAX_COLOR_NUM + 2];
			uint buflen = (uint)buf.Length;
			if(CoreInterface.GetEpsonEP0Cmd(0x5c,buf,ref buflen,0,9) != 0)
			{
				float[] vals = new float[m_NumericUpDownInkTempSet.Length];
				for(int i = 0; i < m_NumericUpDownInkTempSet.Length; i++)
				{
					vals[i] = buf[2+i]/5;
				}
				for (int i=0;i<m_InkBaoTempNum;i++)
				{
					int nMap = m_pMap[i];
					UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownInkTempCur[i],info.cTemperatureCur2[nMap]);
					if(m_bSupportHeadHeat)
					{
						UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownInkTempSet[i],vals[nMap]); 
					}
				}
			}
#endif
            this.isDirty = false;

        }

        private void OnGetRealTimeFromUI_382(ref SRealTimeCurrentInfo_382 sRT)
        {
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cVtrim[nMap] = Decimal.ToSingle(m_NumericUpDownVoltageSet[i].Value);
            }
        }
        public void OnRenewRealTime_382(SRealTimeCurrentInfo_382 info)
        {
            for (int i = 0; i < m_HeadNum; i++)
            {
                int nMap = m_pMap[i];

                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownHeadCur[i], info.cTemperature[nMap]);
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];

                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageCur[i], info.cPWM[nMap]);
                UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownVoltageSet[i], info.cVtrim[nMap]);
            }
        }

        public void OnRealTimeChange()
        {
            if (m_bXaar382)
            {
                SRealTimeCurrentInfo_382 info = new SRealTimeCurrentInfo_382();
                if (CoreInterface.Get382RealTimeInfo(ref info) != 0)
                {
                    OnRenewRealTime_382(info);
                }
                else
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
                }
            }
            else
            {
                SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
                uint Rmask = CalcRWMask(true);
                if (CoreInterface.GetRealTimeInfo(ref info, Rmask) != 0)
                {
                    OnRenewRealTime(info);
                }
                else
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
                }

                if (m_bGma990)
                {
                    m_ButtonRefresh.Enabled = false;
                    SNSendCount = 1;
                    WNSendCount = 1;
                    CurrSNSend = 0;
                    CurrWNSend = 0;
                    SNinfoList.Clear();
                    WNinfoList.Clear();
                    if (CoreInterface.IsS_system())
                    {
                        USER_SET_INFORMATION factoryData = new USER_SET_INFORMATION();
                        int ret = CoreInterface.GetUserSetInfo(ref factoryData);
                        if (ret != 0)
                        {
                            SNSendCount = factoryData.HeadBoardNum;
                            WNSendCount = factoryData.HeadBoardNum;
                        }
                    }
                    ReadSerialNumber(CurrSNSend);
                    CurrSNSend++;
                }

            }
        }

        /// <summary>
        /// 读取指定头板上的喷头序列号
        /// </summary>
        /// <param name="hbIndex">头板索引</param>
        private void ReadSerialNumber(int hbindex)
        {
            try
            {
                ushort value = (ushort)hbindex;
                byte[] val = new byte[8];
                val[0] = 0xC5;
                val[1] = 0x90;
                val[2] = 0;
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, value, 0) == 0)
                {
                    MessageBox.Show(@"Failed to send the read waveform command, make sure the card connection is normal and try again！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 读取指定头板上的波形名称
        /// </summary>
        /// <param name="hbIndex">头板索引</param>
        private void ReadWaveName(int hbIndex)
        {
            try
            {
                ushort value = (ushort)hbIndex;
                byte[] val = new byte[8];
                val[0] = 0xC5;
                val[1] = 0x91;
                val[2] = 0;
                uint bufsize = (uint)val.Length;
                if (CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, value, 0) == 0)
                {
                    MessageBox.Show(@"Failed to send the read waveform command, make sure the card connection is normal and try again！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 获取Gama喷头波形和喷头映射关系
        /// 假设当前配置喷头数为N,则map数组的前N个为有效数据
        /// </summary>
        /// <returns></returns>
        public List<byte> GetGamaMap()
        {
            //4色4组 接线(KKKKCCCC)(MMMMYYYY)
            if (m_rsPrinterPropery.nColorNum == 4 && m_rsPrinterPropery.nHeadNumPerGroupY == 4 )
            {
                //k1c1m1y1-k1c2m2y2-k3c3m3y3-k4c4m4y4
                return new List<byte>() {0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15};
            }
            //4色3组 接线(KKK*CCC*)(MMM*YYY*)
            else if (m_rsPrinterPropery.nColorNum == 4 && m_rsPrinterPropery.nHeadNumPerGroupY == 3)
            {
                //k1c1m1y1-k1c2m2y2-k3c3m3y3-k4c4m4y4
                return new List<byte>() { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };//
            }
            //4色2组+1W
            else if (m_rsPrinterPropery.nColorNum == 5 && m_rsPrinterPropery.nHeadNumPerGroupY == 2 && m_rsPrinterPropery.nWhiteInkNum == 1)
            {
                return new List<byte>() { 0, 1, 2, 3, 4, 8, 9, 10, 11, 12};
            }
            //4色2组+2W
            else if (m_rsPrinterPropery.nColorNum == 6 && m_rsPrinterPropery.nHeadNumPerGroupY == 2 && m_rsPrinterPropery.nWhiteInkNum == 2)
            {
                return new List<byte>() { 0, 1, 2, 3, 4, 5, 8, 9,10,11,12,13 };
            }
            //4色2组
            else if (m_rsPrinterPropery.nColorNum == 4 && m_rsPrinterPropery.nHeadNumPerGroupY == 2)
            {
                return new List<byte>() { 0, 2, 4, 6, 1, 3, 5, 7 };
            }
            //6C+2W
            else if (m_rsPrinterPropery.nColorNum == 8 && m_rsPrinterPropery.nHeadNumPerGroupY == 1)
            {
                return new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            }
            else
            {
                return new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            }
            
        }

        public void OnEp6DataChanged(int ep6Cmd, int index, byte[] buf)
        {
            if (buf != null&&(m_bGma990)&&buf.Length > 0)
            {
                List<byte> map = GetGamaMap();
                byte[] typetemp = new byte[2];
                Buffer.BlockCopy(buf, 0, typetemp, 0, 2);
                ushort type = BitConverter.ToUInt16(typetemp, 0);
                switch (type)
                {
                    case 0x90:
                        HeadSerialNumber serialNumber = GetHeadSerialNumber(buf);
                        for (int i = 0; i < Math.Min(m_HeadNum, serialNumber.head.SnNum); i++)
                        {
                            SNinfoList.Add(System.Text.Encoding.Default.GetString(serialNumber.data[i].ToArray()));
                        }

                        if (CurrSNSend == SNSendCount && CurrWNSend < WNSendCount)
                        {
                            ReadWaveName(CurrWNSend);
                            CurrWNSend++;

                            //收齐全部头板数据，一起更新
                            for (int i = 0; i < Math.Min(m_LabelSerialNumber.Length, SNinfoList.Count); i++)
                            {
                                m_LabelSerialNumber[i].Text = SNinfoList[map[i]];
                            }
                        }
                        else
                        {
                            ReadSerialNumber(CurrSNSend);
                            CurrSNSend++;
                        }

                        break;
                    case 0x91:
                        WaveName WaveNames = GetWaveNames(buf);
                        for (int i = 0; i < Math.Min(m_HeadNum, WaveNames.WaveNameHead.NameNum); i++)
                        {
                            WNinfoList.Add(WaveNames.Data[i]);
                        }

                        if (CurrWNSend < WNSendCount)
                        {
                            ReadWaveName(CurrWNSend);
                            CurrWNSend++;
                        }
                        else
                        {
                            //收齐全部头板数据，一起更新
                            for (int i = 0; i < Math.Min(m_TextBoxWaveName.Length, WNinfoList.Count); i++)
                            {
                                m_TextBoxWaveName[i].Text = WNinfoList[map[i]];
                            }
                            m_ButtonRefresh.Enabled = true;
                        }

                        break;
                }
            }
        }

        private WaveName GetWaveNames(byte[] buf)
        {
            byte[] temp = new byte[8];
            Buffer.BlockCopy(buf, 0, temp, 0, 8);
            WaveNameHead WaveNameHead = (WaveNameHead)PubFunc.BytesToStruct(temp, typeof(WaveNameHead));
            List<NameHead> NameHead = new List<NameHead>();
            for (int i = 0; i < WaveNameHead.NameNum; i++)
            {
                temp = new byte[4];
                Buffer.BlockCopy(buf, 8+4*i, temp, 0, 4);
                NameHead.Add((NameHead)PubFunc.BytesToStruct(temp, typeof(NameHead)));
            }
            List<string> Data = new List<string>();
            foreach (NameHead head in NameHead)
            {
                temp = new byte[head.Len];
                Buffer.BlockCopy(buf, head.Ptr, temp, 0, head.Len);
                Data.Add(Encoding.Default.GetString(temp));
            }
            WaveName WaveName = new WaveName()
            {
                WaveNameHead = WaveNameHead,
                NameHead = NameHead,
                Data=Data
            };
            return WaveName;
        }
        private HeadSerialNumber GetHeadSerialNumber(byte[] buf)
        {
            byte[] temp = new byte[8];
            Buffer.BlockCopy(buf, 0, temp, 0, 8);
            HeadSerialNumberHead headSerialNumberHead =
                (HeadSerialNumberHead)PubFunc.BytesToStruct(temp, typeof(HeadSerialNumberHead));
            List<List<byte>> data = new List<List<byte>>();
            for (int i = 0; i < headSerialNumberHead.SnNum; i++)
            {
                List<byte> tempList = new List<byte>();
                for (int j = 0; j < headSerialNumberHead.SnLen; j++)
                {
                    tempList.Add(buf[8 + j + headSerialNumberHead.SnLen*i]);
                }
                data.Add(tempList);
            }
            HeadSerialNumber headSerialNumber = new HeadSerialNumber()
            {
                head = headSerialNumberHead,
                data = data
            };
            return headSerialNumber;
        }

        private bool CheckComponentChange(SPrinterProperty sp)
        {
            if (m_HeadNum != sp.nHeadNum / sp.nHeadNumPerColor)
                return true;
            return false;
        }

        private void ClearComponent()
        {
            if (m_LabelHorHeadIndex == null || m_LabelHorHeadIndex.Length == 0)
                return;

            foreach (Control c in this.m_LabelHorHeadIndex)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownHeadSet)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownHeadCur)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownHeadTeam)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownVoltageSet)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownVoltageCur)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownVoltageBase)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownPulseWidth)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_LabelLRIndex)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownInkTempCur)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownInkTempSet)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownVoltageInk)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_NumericUpDownAutoVoltageInkOffset)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_ButtonWaveformDownload)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_LabelSerialNumber)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }
            foreach (Control c in this.m_TextBoxWaveName)
            {
                m_GroupBoxTemperature.Controls.Remove(c);
            }

            
        }

        private void CreateComponent()
        {
            this.m_LabelHorHeadIndex = new Label[m_HeadNum];

            this.m_NumericUpDownHeadCur = new NumericUpDown[m_TempNum];
            this.m_NumericUpDownHeadTeam = new NumericUpDown[m_TempNum];
            if (m_bRicoHead || m_bXaar501)
            {
                this.m_NumericUpDownHeadSet = new NumericUpDown[m_HeadNum];
            }
            else
            {
                this.m_NumericUpDownHeadSet = new NumericUpDown[m_TempNum];
            }

            this.m_NumericUpDownInkTempCur = new NumericUpDown[m_InkBaoTempNum];
            this.m_NumericUpDownInkTempSet = new NumericUpDown[m_InkBaoTempNum];

            this.m_NumericUpDownVoltageSet = new NumericUpDown[m_HeadVoltageNum];
            this.m_NumericUpDownVoltageCur = new NumericUpDown[m_HeadVoltageNum];
            this.m_NumericUpDownVoltageBase = new NumericUpDown[m_HeadVoltageNum];
            this.m_NumericUpDownPulseWidth = new NumericUpDown[m_HeadPulseWidthNum];
            this.m_ButtonWaveformDownload = new Button[m_HeadNum];
            this.m_LabelSerialNumber = new TextBox[m_HeadNum];
            this.m_TextBoxWaveName = new TextBox[m_HeadNum];
            //xaar501
            this.m_NumericUpDownVoltageInk = new NumericUpDown[m_InkVolageNum];
            this.m_NumericUpDownAutoVoltageInkOffset = new NumericUpDown[m_VolageOffsetNum];

            //List<byte> map = GetGamaMap();
            for (int i = 0; i < m_HeadNum; i++)
            {
                this.m_LabelHorHeadIndex[i] = new Label();
                m_ButtonWaveformDownload[i] = new Button {Tag = (byte)i};
                m_ButtonWaveformDownload[i].Click += new EventHandler(m_ButtonWaveformDownload_Click);
                m_LabelSerialNumber[i] = new TextBox();
                m_TextBoxWaveName[i]=new TextBox();
            }
            for (int i = 0; i < m_TempNum; i++)
            {
                this.m_NumericUpDownHeadCur[i] = new NumericUpDown();
                this.m_NumericUpDownHeadTeam[i] = new NumericUpDown();
                if (m_bKonic512)
                    this.m_NumericUpDownHeadCur[i].ValueChanged += new EventHandler(KonicTemperature1_ValueChanged);
                if (m_bRicoHead || m_bXaar501)
                {
                    if (i < m_HeadNum)
                    {
                        this.m_NumericUpDownHeadSet[i] = new NumericUpDown();
                    }
                }
                else
                {
                    this.m_NumericUpDownHeadSet[i] = new NumericUpDown();
                }
            }
            for (int i = 0; i < m_InkBaoTempNum; i++)
            {
                this.m_NumericUpDownInkTempCur[i] = new NumericUpDown();
                this.m_NumericUpDownInkTempSet[i] = new NumericUpDown();
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                this.m_NumericUpDownVoltageSet[i] = new NumericUpDown();
                this.m_NumericUpDownVoltageSet[i].ValueChanged += new EventHandler(km1024i_ValueChanged);
                this.m_NumericUpDownVoltageCur[i] = new NumericUpDown();
                this.m_NumericUpDownVoltageBase[i] = new NumericUpDown();
                this.m_NumericUpDownVoltageBase[i].ValueChanged += new EventHandler(km1024i_ValueChanged);
            }
            for (int i = 0; i < m_InkVolageNum; i++)
            {
                this.m_NumericUpDownVoltageInk[i] = new NumericUpDown();
            }
            for (int i = 0; i < m_VolageOffsetNum; i++)
            {
                this.m_NumericUpDownAutoVoltageInkOffset[i] = new NumericUpDown();
            }
            for (int i = 0; i < m_HeadPulseWidthNum; i++)
            {
                this.m_NumericUpDownPulseWidth[i] = new NumericUpDown();
            }
            this.m_LabelLRIndex = new Label[Voltage_LR_Num];
            for (int i = 0; i < Voltage_LR_Num; i++)
            {
                this.m_LabelLRIndex[i] = new Label();
            }
            this.SuspendLayout();
        }

        void m_ButtonWaveformDownload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = @"waveform file|*.bin;*.dat"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] info;

                    FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                    BinaryReader binReader = new BinaryReader(fs);

                    info = new byte[fs.Length];
                    binReader.Read(info, 0, (int)fs.Length);

                    binReader.Close();
                    fs.Close();
                    //

                    Button button = (Button)sender;
                    //
                    List<byte> map = GetGamaMap();
                    byte index = map[(byte)button.Tag];
                    //for (byte i = 0; i < map.Count; i++)
                    //{
                    //    if (map[i] == index)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    int headIndex = 1;
                    int nLen = 21 + info.Length;
                    byte[] val = new byte[nLen];
                    val[0] = 0x47;
                    val[1] = 0x00;
                    val[2] = (byte)index;
                    val[19] = 1;
                    Array.Copy(info, 0, val, 21, info.Length);
                    CoreInterface.Down382WaveForm(val, nLen, headIndex);
                    //MessageBox.Show(CoreInterface.Down382WaveForm(val, nLen, headIndex) != 0 ? "Download Success!" : "Download Fail!");
                    OnRealTimeChange();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace);
            }

        }

        private void AppendComponent()
        {
            for (int i = 0; i < Voltage_LR_Num; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_LabelLRIndex[i]);
            }

            for (int i = 0; i < m_HeadNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_LabelHorHeadIndex[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_ButtonWaveformDownload[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_LabelSerialNumber[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_TextBoxWaveName[i]);
            }
            for (int i = 0; i < m_TempNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownHeadCur[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownHeadTeam[i]);
                if (m_bRicoHead || m_bXaar501)
                {
                    if (i < m_HeadNum)
                    {
                        m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownHeadSet[i]);
                    }
                }
                else
                {
                    m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownHeadSet[i]);
                }
            }
            for (int i = 0; i < m_InkBaoTempNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownInkTempCur[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownInkTempSet[i]);
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVoltageSet[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVoltageCur[i]);
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVoltageBase[i]);
            }
            for (int i = 0; i < m_HeadPulseWidthNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownPulseWidth[i]);
            }

            for (int i = 0; i < m_InkVolageNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownVoltageInk[i]);
            }
            for (int i = 0; i < m_VolageOffsetNum; i++)
            {
                m_GroupBoxTemperature.Controls.Add(this.m_NumericUpDownAutoVoltageInkOffset[i]);
            }
            m_GroupBoxTemperature.ResumeLayout(false);

            this.ResumeLayout(false);
        }
        private void reCalcGroupboxSize(int start_y, int space_y, ref int new_space_y)
        {
            const int control_margin = 8;
            int end_y;
            #region recalculat groupbox size
            int labelx = m_LabelLeft.Location.X;
            int CurY = start_y + space_y;
            new_space_y = space_y;
            // 计算温度区域Y向间距
            if (
                (m_bPolaris && !m_bExcept && !m_bPolaris_V7_16)
                || m_bRicoHead
                || m_bXaar501
                )
                new_space_y = space_y * 2;
            if (this.WhichRowVisble[0])//(m_bSupportHeadHeat&& !(m_bXaar382 || m_bPolaris))
            {
                m_LabelLeft.Location = new Point(labelx, CurY);
                m_NumericUpDownTempSetSample.Location = new Point(m_NumericUpDownTempSetSample.Location.X, CurY);
                if (m_bRicoHead || (m_bPolaris_V7_16 && !m_bPolaris_V7_16_Emerald) || m_bXaar501)
                    CurY += space_y;
                else
                    CurY += new_space_y;
            }

            if (this.WhichRowVisble[1])//(!m_bPolaris)
            {
                m_LabelRight.Location = new Point(labelx, CurY);
                m_NumericUpDownTempCurSample.Location = new Point(m_NumericUpDownTempCurSample.Location.X, CurY);
                if (m_bXaar501)
                    CurY += space_y * 3;
                else
                    CurY += new_space_y;
            }
            if (this.WhichRowVisble[2])
            {
                m_labelHeatingTemp.Location = new Point(labelx, CurY);
                m_NumericUpDownHeatTempSample.Location = new Point(m_NumericUpDownHeatTempSample.Location.X, CurY);
                CurY += new_space_y;
            }

            // 计算电压脉宽区域Y向间距
            if (m_bKonic512
#if GONGZENG_DOUBLE
 || m_bExcept
#endif
 || m_bPolaris_V5_8_Emerald
                || m_bKyocera
                )
                new_space_y = space_y * 2;
            else if (m_bSpectra_SG1024_Gray || m_bKonic1024i_Gray)
                new_space_y = space_y * 3;
            else if ((m_bPolaris_V5_8 && !m_bPolaris_V5_8_Emerald) || m_bPolaris_V7_16)
                new_space_y = space_y;
            else
                new_space_y = (int)(space_y * VOL_COUNT_PER_HEAD);
            if (this.WhichRowVisble[3])//(!m_bPolaris)
            {
                m_labelVolSet.Location = new Point(labelx, CurY);
                m_NumericUpDownVolAdjustSample.Location = new Point(m_NumericUpDownVolAdjustSample.Location.X, CurY);
                if (m_bXaar501)
                {
                    CurY += space_y * (int)ADJUST_VOL_PER_HEAD;
                }
                else
                {
                    CurY += new_space_y;
                }

            }
            if (this.WhichRowVisble[4])//
            {
                m_labelVolCur.Location = new Point(labelx, CurY);
                m_NumericUpDownVolCurSample.Location = new Point(m_NumericUpDownVolCurSample.Location.X, CurY);
                CurY += new_space_y;
            }
            if (this.WhichRowVisble[5])//(!(m_bXaar382 || m_bPolaris))
            {
                m_LabelBaseVol.Location = new Point(labelx, CurY);
                m_NumericUpDownVolBaseSample.Location = new Point(m_NumericUpDownVolBaseSample.Location.X, CurY);
                CurY += new_space_y;
            }
            if (m_bSpectra_SG1024_Gray)
                new_space_y = (int)(space_y * Plus_COUNT_PER_HEAD);
            if (this.WhichRowVisble[6])//(!m_bXaar382)
            {
                m_LabelCurPulseWidth.Location = new Point(labelx, CurY);
                m_NumericUpDownPulseWidthSample.Location = new Point(m_NumericUpDownPulseWidthSample.Location.X, CurY);
                CurY += new_space_y;
            }
            if (this.WhichRowVisble[7])
            {
                m_LabelInkTempSet.Location = new Point(labelx, CurY);
                m_NumericUpDownInkTempSetSample.Location = new Point(m_NumericUpDownInkTempSetSample.Location.X, CurY);
                CurY += space_y;
            }
            if (m_bXaar501)
            {
                if (this.WhichRowVisble[8])
                {
                    label_VoltageInkSample.Location = new Point(labelx, CurY);
                    numericUpDown_VoltageInkSample.Location = new Point(numericUpDown_VoltageInkSample.Location.X, CurY);
                    CurY += space_y;
                }
            }
            else
            {
                if (this.WhichRowVisble[8])
                {
                    lblInkCartridgesTemp.Location = new Point(labelx, CurY);
                    m_NumericUpDownInkTempCurSample.Location = new Point(m_NumericUpDownInkTempCurSample.Location.X, CurY);
                    CurY += space_y;
                }
            }

            if (this.WhichRowVisble[9])
            {
                label_AutoVoltageSample.Location = new Point(labelx, CurY);
                numericUpDown_AutoVoltageInkSample.Location = new Point(numericUpDown_AutoVoltageInkSample.Location.X, CurY);
                CurY += space_y;
            }
            if (this.WhichRowVisble[10])
            {
                label_SerialNumberSample.Location = new Point(labelx, CurY);
                label_SerialNumber.Location = new Point(label_SerialNumber.Location.X, CurY);
                CurY += space_y;
            }
            if (this.WhichRowVisble[11])
            {
                label_WaveNameSample.Location = new Point(labelx, CurY);
                textBox_WaveNameSample.Location = new Point(textBox_WaveNameSample.Location.X, CurY);
                CurY += space_y;
            }
            if (this.WhichRowVisble[12])
            {
                label_WaveformDownloadSample.Location = new Point(labelx, CurY);
                button_WaveformDownloadSample.Location = new Point(button_WaveformDownloadSample.Location.X, CurY);
                CurY += space_y;
            }

            end_y = CurY + space_y;
            //			if(!m_bSpectra)
            //			{
            //				end_y -= new_space_y;
            //			}
            m_GroupBoxTemperature.Height = end_y - start_y + space_y;// -m_GroupBoxTemperature.Location.Y;
            int buttonY = m_GroupBoxTemperature.Bottom + control_margin;
            this.Height = buttonY + control_margin + m_ButtonToBoard.Height;

            m_ButtonToBoard.Location = new Point(m_ButtonToBoard.Location.X, buttonY);
            m_ButtonRefresh.Location = new Point(m_ButtonRefresh.Location.X, buttonY);
            m_ButtonDefault.Location = new Point(m_ButtonDefault.Location.X, buttonY);
            buttonExport.Location = new Point(buttonExport.Location.X, buttonY);
            buttonImport.Location = new Point(buttonImport.Location.X, buttonY);
            #endregion
        }
        private void LayoutRow_HeadIndex(Point startP, Point spaceP)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            int width_con = this.m_NumericUpDownTempSetSample.Width;
            #region Layout lables
            for (int i = 0; i < Voltage_LR_Num; i++)
            {
                int curX = start_x - 16;
                int curY = start_y + space_y * 2;
                if ((m_bSupportHeadHeat && !(m_bXaar382 || m_bExcept)))//|| m_bRicoHead
                    curY = start_y + space_y * 3;

                Label label = this.m_LabelLRIndex[i];
                ControlClone.LabelClone(label, this.m_LabelHorSample);
                label.Location = new Point(curX, curY + space_y * i);//this.m_LabelHorSample.Location.Y);
                label.Width = 12;
                label.Text = "R";
                if ((i & 1) != 0)
                    label.Text = "L";
                label.Visible = true;
            }

            int j = 0;
            for (int i = 0; i < m_HeadNum; i++)
            {
                Label label = this.m_LabelHorHeadIndex[i];
                ControlClone.LabelClone(label, this.m_LabelHorSample);
                label.Location = new Point(start_x + space_x * i, this.m_LabelHorSample.Location.Y);//start_y );
                label.Width = width_con;
                if (m_bPolaris_V7_16 && !m_bPolaris_V7_16_Emerald)
                {
                    if (m_bPolaris_V7_16_Polaris && m_bMirrorArrangement)
                    {
                        string strclolor = m_rsPrinterPropery.Get_ColorIndex((i + m_StartHeadIndex) / 2);
                        label.Text = string.Format("{0}{1}", strclolor, (((i + m_StartHeadIndex) / (m_rsPrinterPropery.nColorNum * 2)) + ((i + m_StartHeadIndex) % 2) * 2).ToString());
                    }
                    else
                    {
                        string strclolor = m_rsPrinterPropery.Get_ColorIndex((i + m_StartHeadIndex) / 2);
                        label.Text = string.Format("{0}{1}", strclolor, (((i + m_StartHeadIndex) / (m_rsPrinterPropery.nColorNum * 2)) * 2 + (i + m_StartHeadIndex) % 2).ToString());
                    }
                }
                else if (m_bKonic1024i_Gray||m_bKonic1800i)
                {
                    label.Text = string.Format("{0}{1} ({2})", (i + m_StartHeadIndex) / 2, i % 2 == 0 ? "R" : "L", m_rsPrinterPropery.Get_ColorIndex((i + m_StartHeadIndex) / 2));
                }
                else
                {
                    if (m_b1head2color)
                    {
                        if (m_bVerArrangement)
                        {
                            label.Text = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + 1 + m_StartHeadIndex));
                            j += 2;
                        }
                        else
                        {
                            if (m_bMirrorArrangement)
                            {
                                int cIndex1 = j + m_StartHeadIndex;
                                int cIndex2 = j + 1 + m_StartHeadIndex;
                                //if ((j / m_rsPrinterPropery.nColorNum) % 2 != 0)
                                //{
                                //    cIndex1 = m_rsPrinterPropery.nColorNum - cIndex1 % m_rsPrinterPropery.nColorNum - 1;
                                //    cIndex2 = m_rsPrinterPropery.nColorNum - cIndex2 % m_rsPrinterPropery.nColorNum - 1;
                                //}
                                label.Text = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(cIndex1), m_rsPrinterPropery.Get_ColorIndex(cIndex2));
                                j += 2;
                            }
                            else
                            {
                                label.Text = string.Format("{0} ({1}/{2})", (i + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + m_StartHeadIndex), m_rsPrinterPropery.Get_ColorIndex(j + 1 + m_StartHeadIndex));
                                j += 2;
                            }
                        }
                    }
                    else
                    {
                        label.Text = (i + m_StartHeadIndex).ToString()
                            + "  (" + m_rsPrinterPropery.Get_ColorIndex(i + m_StartHeadIndex) + ")";
                    }
                }
                label.Visible = true;
            }
            #endregion
        }

        private void LayoutRows_Temp(Point startP, Point spaceP, int width_con)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            int new_space_y = space_y;
            if ((m_bPolaris && !m_bExcept && !m_bPolaris_V7_16) || m_bRicoHead)
                new_space_y = space_y * 2;
            else if (m_bXaar501)
                new_space_y = space_y * 3;
            if (m_bKonic1024i_Gray || m_bKonic1800i)
                space_x *= 2;

            #region Layout Temp
            for (int i = 0; i < m_TempNum; i++)
            {
                int curX = start_x + space_x * i;
                int curY = start_y + space_y;
                if (m_bXaar501)
                {
                    curX = start_x + space_x * (i / 3);
                    curY += space_y * (i % 3);
                }
                else if ((m_bPolaris && !m_bExcept && !this.m_bPolaris_V5_8_Emerald && !m_bPolaris_V7_16) || m_bRicoHead)
                {
                    if (i >= m_TempNum / 2)
                    {
                        curX = start_x + space_x * (i - m_TempNum / 2);
                        curY += space_y;
                    }
                    else
                    {
                        curX = start_x + space_x * i;
                    }
                }
                NumericUpDown textBox = null;
                if (((!m_bRicoHead && !m_bXaar501) || ((m_bRicoHead || m_bXaar501) && i < m_HeadNum)))
                {
                    int curSetTempX = curX, curSetTempY = start_y + space_y;
                    if (m_bXaar501)
                    {
                        if (i >= m_TempNum / 2)
                        {
                            curSetTempX = start_x + space_x * (i - m_TempNum / 2);
                            curSetTempY = curY + space_y;
                        }
                        else
                        {
                            curSetTempX = start_x + space_x * i;
                        }
                    }

                    textBox = this.m_NumericUpDownHeadSet[i];
                    ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownTempSetSample);
                    textBox.Minimum = new decimal(0);
                    const int maxTempDefault = 65;
                    if (m_b16BitTemp)
                    {
                        if (m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Spectra_GALAXY_256)
                            textBox.Maximum = new decimal(100.0);
                        else if (m_bKonicM600)
                            textBox.Maximum = new decimal(75);// new decimal(50.8);
                        else 
                            textBox.Maximum = new decimal(maxTempDefault);
                    }
                    else
                    {
                        if (m_nTempCoff > 0) //支持温度倍率,且不是默认倍率时
                        {
                            if (m_rsPrinterPropery.ePrinterHead == PrinterHeadEnum.Spectra_GALAXY_256)
                                textBox.Maximum = new decimal(100.0);
                            else if (m_bKonicM600)
                                textBox.Maximum = new decimal(75);// new decimal(50.8);
                            else
                                textBox.Maximum = new decimal(Math.Min(255f / m_nTempCoff, maxTempDefault));// new decimal(50.8);
                        }
                        else
                        {
                            textBox.Maximum = new decimal(50.8);// new decimal(50.8);
                        }
                    }
                    textBox.Increment = new decimal(0.1);
                    textBox.Location = new Point(curSetTempX, curSetTempY);//(start_x+ space_x * i,this.m_NumericUpDownTempSetSample.Location.Y);//start_y + space_y);
                    textBox.TabIndex = i;
                    InitNumericUpDownBySample(textBox, width_con);
                    textBox.Visible = this.WhichRowVisble[0];
                    if (m_bKonic1024i_Gray || m_bKonic1800i)
                        textBox.Width += spaceP.X;
                }
                if (this.WhichRowVisble[0])
                {
                    if (m_bRicoHead || m_bXaar501)
                        curY += space_y;
                    else
                        curY += new_space_y;
                }
                textBox = this.m_NumericUpDownHeadCur[i];
                ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownTempCurSample);
                textBox.Location = new Point(curX, curY);//(start_x+ space_x * i,this.m_NumericUpDownTempCurSample.Location.Y);//start_y + space_y*2);
                textBox.TabIndex = m_TempNum + i;
                textBox.TabStop = false;
                InitNumericUpDownBySample(textBox, width_con);
                textBox.Visible = this.WhichRowVisble[1];
                if (m_bKonic1024i_Gray || m_bKonic1800i)
                    textBox.Width += spaceP.X;

                if (this.WhichRowVisble[1])
                    curY += new_space_y;
                textBox = this.m_NumericUpDownHeadTeam[i];
                ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownHeatTempSample);
                textBox.Location = new Point(curX, curY);//(start_x+ space_x * i,this.m_NumericUpDownTempCurSample.Location.Y);//start_y + space_y*2);
                textBox.TabIndex = m_TempNum * 2 + i;
                textBox.TabStop = false;
                InitNumericUpDownBySample(textBox, width_con);
                textBox.Visible = this.WhichRowVisble[2];
                if (m_bKonic1024i_Gray || m_bKonic1800i)
                    textBox.Width += spaceP.X;
            }
            #endregion
        }

        private void LayoutRows_VolAndPulseW(Point startP, Point spaceP, int new_space_y, int width_con)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            int cur_vol_X = 0;
            int cur_vol_Y = 0;
            #region layout Voltage and PulseWidth
            float cofficient_voltage = 1.0f;
            if (m_bSpectra)
                cofficient_voltage = 10.0f;
            if (m_bPolaris_V7_16)
            {
                if (!m_bPolaris_V7_16_Emerald)
                    width_con += space_x;
                new_space_y = space_y;
            }
            if (m_bSpectra_SG1024_Gray || m_bKonic1024i_Gray || m_bKyocera || m_bKonic1800i || m_bKonicM600)
            {
                new_space_y = (int)(space_y * VOL_COUNT_PER_HEAD);
            }
            int plus_space_y = new_space_y;
            if (m_bSpectra_SG1024_Gray)
                plus_space_y = (int)(space_y * Plus_COUNT_PER_HEAD);

            int maxNum = Math.Max(m_HeadVoltageNum, Math.Max(m_HeadPulseWidthNum, m_InkBaoTempNum));
            for (int i = 0; i < maxNum; i++)
            {
                int curX = start_x + space_x * i;
                int curY = start_y + space_y * 2;
                int curX_Plus = start_x + space_x * i;
                int curY_Plus = start_y + space_y * 2;

                if (m_bExcept)
                    curY_Plus = curY = start_y + space_y;
                if (m_bSupportHeadHeat && !(m_bXaar382 || m_bExcept) && !m_bXaar501)
                {
                    if (m_bPolaris && !m_bExcept)
                        curY_Plus = curY = start_y + space_y * 7;
                    else if (m_bRicoHead)
                        curY_Plus = curY = start_y + space_y * 4;
                    else
                        curY_Plus = curY = start_y + space_y * 3;
                }
                if (m_bPolaris_V5_8_Emerald)
                    curY_Plus = curY = start_y + space_y * 5;
                if (m_bPolaris_V7_16)
                {
                    if (m_bPolaris_V7_16_Emerald)
                        curY_Plus = curY = start_y + space_y * 3;
                    else
                        curY_Plus = curY = start_y + space_y * 4;
                }
                if (m_bKonic512
#if GONGZENG_DOUBLE
 || m_bExcept
#endif
)
                {
                    curX_Plus = curX = start_x + space_x * (i / 2);
                    if ((i & 1) != 0)
                    {
                        curY += space_y;
                        curY_Plus += space_y;
                    }
                }
                else if (m_bPolaris && !m_bExcept)
                {
                    if (i >= m_HeadVoltageNum / 2 && ((!m_bPolaris_V5_8 || m_bPolaris_V5_8_Emerald) && !m_bPolaris_V7_16))
                    {
                        curX_Plus = curX = start_x + space_x * (i - m_HeadVoltageNum / 2);
                        curY += space_y;
                        curY_Plus += space_y;
                    }
                    else if (m_bPolaris_V7_16 && !m_bPolaris_V7_16_Emerald)
                    {
                        curX_Plus = curX = start_x + space_x * 2 * i;
                    }
                    else
                    {
                        curX_Plus = curX = start_x + space_x * i;
                    }
                }
                else if (m_bSpectra_SG1024_Gray)
                {
                    curX = (int)(start_x + space_x * (i / (int)VOL_COUNT_PER_HEAD));
                    curY += (int)(i % VOL_COUNT_PER_HEAD) * space_y;
                    curX_Plus = (int)(start_x + space_x * (i / (int)Plus_COUNT_PER_HEAD));
                    curY_Plus += (int)(i % Plus_COUNT_PER_HEAD) * space_y;
                }
                else if (m_bKonic1024i_Gray || m_bKyocera || m_bKonic1800i || m_bKonicM600)
                {
                    curX = (int)(start_x + space_x * (i / (int)VOL_COUNT_PER_HEAD));
                    curY += (int)(i % ((int)VOL_COUNT_PER_HEAD)) * space_y;
                    //curX_Plus = start_x + space_x * (i * 2 / Plus_COUNT_PER_HEAD);
                    //curY_Plus += (i % Plus_COUNT_PER_HEAD) * space_y;
                }
                else if (m_bXaar501)
                {
                    curY -= space_y;
                    curX = (int)(start_x + space_x * (i / (int)ADJUST_VOL_PER_HEAD));
                    curY += (int)(i % ((int)ADJUST_VOL_PER_HEAD) + 4) * space_y;
                    cur_vol_X = start_x + space_x * i;
                    cur_vol_Y = space_y * (int)(ADJUST_VOL_PER_HEAD + 1);

                    Debug.WriteLine(string.Format("x:{0}-y:{1}", curX, curY));
                }
                //curY += space_y;
                NumericUpDown textBox = null;
                if (i < m_HeadVoltageNum)
                {
                    textBox = this.m_NumericUpDownVoltageSet[i];
                    ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownVolAdjustSample);

                    if (m_bXaar382)
                    {
                        //Vtrim
                        textBox.Minimum = new decimal(-128);
                        textBox.Maximum = new decimal(127);
                        textBox.Increment = new decimal(1);
                        textBox.DecimalPlaces = 0;
                    }
                    else if (m_bXaar501)
                    {
                        textBox.Minimum = new decimal(0);
                        textBox.Maximum = new decimal(999999);
                        textBox.Increment = new decimal(1);
                    }
                    else if (m_bKyocera)
                    {
                        textBox.Minimum = new decimal(-2);
                        textBox.Maximum = new decimal(2);
                        textBox.Increment = new decimal(1);
                    }
                    else if (m_bGma990)
                    {
                        textBox.Minimum = new decimal(-4);// 协议最大-4
                        textBox.Maximum = new decimal(4);
                        textBox.Increment = new decimal(0.2);
                    }
                    else
                    {
                        textBox.Minimum = new decimal(-2.0f * cofficient_voltage);
                        textBox.Maximum = new decimal(23.5f * cofficient_voltage);
                        textBox.Increment = new decimal(0.1f * cofficient_voltage);
                    }

                    textBox.Location = new Point(curX, curY);
                    textBox.TabIndex = m_TempNum * 3 + i;
                    InitNumericUpDownBySample(textBox, width_con);
                    textBox.Visible = this.WhichRowVisble[3];
                }
                if (this.WhichRowVisble[3]) //(m_bPolaris)
                {
                    if (m_bXaar501)
                    {
                        curY += new_space_y * (int)ADJUST_VOL_PER_HEAD;
                    }
                    else
                    {
                        curY += new_space_y;
                    }
                    curY_Plus += new_space_y;
                }
                if (i < m_HeadVoltageNum)
                {
                    textBox = this.m_NumericUpDownVoltageCur[i];
                    ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownVolCurSample);
                    if (m_bXaar382)
                    {
                        //pwm
                        textBox.Minimum = new decimal(-128);
                        textBox.Maximum = new decimal(127);
                        textBox.Increment = new decimal(1);
                        textBox.DecimalPlaces = 0;
                    }

                    textBox.Location = new Point(curX, curY);
                    textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum + i;
                    textBox.TabStop = false;
                    InitNumericUpDownBySample(textBox, width_con);
                    textBox.Visible = this.WhichRowVisble[4];
                }
                if (this.WhichRowVisble[4]) //(m_bPolaris)
                {
                    curY += new_space_y;
                    curY_Plus += new_space_y;
                }
                if (i < m_HeadVoltageNum)
                {
                    textBox = this.m_NumericUpDownVoltageBase[i];

                    ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownVolBaseSample);
                    if (m_bKonic1024i_Gray || m_bKonic1800i)
                    {
                        //textBox.Minimum = new decimal(KM1024_I_FULL_VOL_MIN * cofficient_voltage);
                        //textBox.Maximum = new decimal(KM1024_I_FULL_VOL_MAX * cofficient_voltage);
                        textBox.Minimum = new decimal(0.0f * cofficient_voltage);
                        textBox.Maximum = new decimal(25.6f * cofficient_voltage);
                    }
                    else if (m_bGma990)
                    {
                        textBox.Minimum = new decimal(13);
                        textBox.Maximum = new decimal(39);
                        textBox.Increment = new decimal(1);
                    }
                    else if (m_bKyocera)
                    {
                        textBox.Minimum = new decimal(24);
                        textBox.Maximum = new decimal(28);
                        textBox.Increment = new decimal(1);
                    }
                    else
                    {
                        textBox.Minimum = new decimal(0.0f * cofficient_voltage);
                        textBox.Maximum = new decimal(25.6f * cofficient_voltage);
                    }
                    textBox.Increment = new decimal(0.1f * cofficient_voltage);
                    textBox.Location = new Point(curX, curY);
                    textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 2 + i;
                    InitNumericUpDownBySample(textBox, width_con);
                    textBox.Visible = this.WhichRowVisble[5];
                    textBox.Enabled = !m_bGma990&&!m_bKyocera;
                }
                if (this.WhichRowVisble[5]) //(m_bXaar382 || m_bPolaris)
                {
                    curY += new_space_y;
                    curY_Plus += new_space_y;
                }
                if (i < m_HeadPulseWidthNum)
                {
                    textBox = this.m_NumericUpDownPulseWidth[i];

                    //ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
                    ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownVolBaseSample);

                    textBox.Minimum = new decimal(3f);
                    textBox.Maximum = new decimal(12.0f);
                    if (m_bSpectra_SG1024_Gray && i % Plus_COUNT_PER_HEAD >= Plus_COUNT_PER_HEAD - 2)
                    {
                        textBox.Minimum = new decimal(0);
                    }
                    textBox.Location = new Point(curX_Plus, curY_Plus);
                    textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 3 + i;
                    //textBox.TabStop = false;
                    InitNumericUpDownBySample(textBox, width_con);

                    textBox.Visible = this.WhichRowVisble[6];
                }
                if (this.WhichRowVisble[6])
                {
                    curY += new_space_y;
                    curY_Plus += new_space_y;

                }
                curY += plus_space_y;

                if (m_bXaar501)
                {
                    if (i < m_InkBaoTempNum)
                    {
                        textBox = this.m_NumericUpDownInkTempSet[i];

                        //ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
                        ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownInkTempSetSample);

                        //textBox.Minimum = new decimal(0);
                        //textBox.Maximum = new decimal(51.0f);
                        textBox.Location = new Point(curX, curY);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 3 + i;
                        InitNumericUpDownBySample(textBox, width_con);
                        //EPSON
                        if (!this.WhichRowVisble[7])
                            textBox.Visible = false;
                        else
                            curY += space_y;
                        textBox = this.m_NumericUpDownInkTempCur[i];

                        //ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
                        ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownInkTempCurSample);
                        textBox.Location = new Point(curX, curY);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 3 + i;
                        InitNumericUpDownBySample(textBox, width_con);
                        textBox.Visible = this.WhichRowVisble[7];
                    }
                    if (this.WhichRowVisble[7])
                    {
                        curY += new_space_y;
                        curY_Plus += new_space_y;

                    }
                    if (i < m_InkVolageNum)
                    {
                        textBox = this.m_NumericUpDownVoltageInk[i];
                        ControlClone.NumericUpDownClone(textBox, this.numericUpDown_VoltageInkSample);
                        //textBox.Minimum = new decimal(4.0f);
                        //textBox.Maximum = new decimal(12.0f);

                        textBox.Location = new Point(cur_vol_X, this.label_VoltageInkSample.Location.Y);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 4 + i;
                        //textBox.TabStop = false;
                        InitNumericUpDownBySample(textBox, width_con);
                        textBox.Visible = this.WhichRowVisble[8];
                    }
                    if (this.WhichRowVisble[8])
                    {
                        curY += new_space_y;
                        curY_Plus += new_space_y;
                        cur_vol_Y += new_space_y;
                    }
                    if (i < m_VolageOffsetNum)
                    {
                        textBox = this.m_NumericUpDownAutoVoltageInkOffset[i];
                        ControlClone.NumericUpDownClone(textBox, this.numericUpDown_AutoVoltageInkSample);
                        //if (!m_bXaar501)
                        //{
                        //    textBox.Minimum = new decimal(4.0f);
                        //    textBox.Maximum = new decimal(12.0f);
                        //}

                        textBox.Location = new Point(cur_vol_X, this.label_AutoVoltageSample.Location.Y);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 5 + i;
                        InitNumericUpDownBySample(textBox, width_con);
                        textBox.Visible = this.WhichRowVisble[9];
                    }
                    if (this.WhichRowVisble[9])
                    {
                        curY += new_space_y;
                        curY_Plus += new_space_y;
                    }

                }
                else
                {
                    if (i < m_InkBaoTempNum)
                    {
                        textBox = this.m_NumericUpDownInkTempSet[i];

                        //ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
                        ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownInkTempSetSample);

                        textBox.Minimum = new decimal(0);
                        textBox.Maximum = new decimal(51.0f);
                        textBox.Location = new Point(curX, curY);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 3 + i;
                        InitNumericUpDownBySample(textBox, width_con);
                        if (!this.WhichRowVisble[7])
                            textBox.Visible = false;
                        else
                            curY += space_y;
                        textBox = this.m_NumericUpDownInkTempCur[i];

                        //ControlClone.NumericUpDownClone(textBox,this.m_NumericUpDownPulseWidthSample);
                        ControlClone.NumericUpDownClone(textBox, this.m_NumericUpDownInkTempCurSample);
                        textBox.Location = new Point(curX, curY);
                        textBox.TabIndex = m_TempNum * 3 + m_HeadVoltageNum * 3 + i;
                        InitNumericUpDownBySample(textBox, width_con);
                        textBox.Visible = this.WhichRowVisble[8];
                    }
                }
            }
            #endregion
        }

        private void LayoutRows_WaveformDownload(Point startP, Point spaceP, int width_con)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            for (int i = 0; i < m_HeadNum; i++)
            {
                Button button = this.m_ButtonWaveformDownload[i];
                ControlClone.ButtonClone(button, this.button_WaveformDownloadSample);
                button.Location = new Point(start_x + space_x * i, this.button_WaveformDownloadSample.Location.Y);
                button.Width = width_con;
                button.Visible = WhichRowVisble[12];
            }
        }
        private void LayoutRows_SerialNumber(Point startP, Point spaceP, int width_con)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            for (int i = 0; i < m_HeadNum; i++)
            {
                TextBox label = this.m_LabelSerialNumber[i];
                ControlClone.TextBoxClone(label, this.label_SerialNumber);
                label.Width = width_con;
                label.Location = new Point(start_x + space_x * i, this.label_SerialNumber.Location.Y);
                label.Visible = WhichRowVisble[10];
            }
        }
        private void LayoutRows_WaveName(Point startP, Point spaceP, int width_con)
        {
            int start_x = startP.X;
            int start_y = startP.Y;
            int space_y = spaceP.Y;
            int space_x = spaceP.X;
            for (int i = 0; i < m_HeadNum; i++)
            {
                TextBox textBox = this.m_TextBoxWaveName[i];
                ControlClone.TextBoxClone(textBox, this.textBox_WaveNameSample);
                textBox.Width = width_con;
                textBox.Location = new Point(start_x + space_x * i, this.textBox_WaveNameSample.Location.Y);
                textBox.Visible = WhichRowVisble[11];
            }
        }
        private void LayoutComponent()
        {
            const int control_margin = 8;
            m_GroupBoxTemperature.SuspendLayout();
            this.SuspendLayout();

            ///True Layout
            ///
            int start_x, end_x, space_x, width_con;
            int start_y, space_y;
            start_x = this.m_NumericUpDownTempSetSample.Left;
            start_y = this.m_LabelHorSample.Top;
            space_y = this.m_NumericUpDownTempSetSample.Height + control_margin;
            end_x = this.m_GroupBoxTemperature.Width;
            width_con = this.m_NumericUpDownTempSetSample.Width;
            LayoutHelper.CalculateHorNum(m_HeadNum, start_x, end_x, ref width_con, out space_x);

            // recalculat groupbox size
            int new_space_y = space_y;
            reCalcGroupboxSize(start_y, space_y, ref new_space_y);

            // Layout lables
            Point startP = new Point(start_x, start_y);
            Point spaceP = new Point(space_x, space_y);

            //Point spaceP = new Point(0, 0);
            LayoutRow_HeadIndex(startP, spaceP);//( start_x, start_y,  space_y, space_x);

            // Layout Temp
            LayoutRows_Temp(startP, spaceP, width_con);

            //layout Voltage and PulseWidth
            LayoutRows_VolAndPulseW(startP, spaceP, new_space_y, width_con);
            LayoutRows_WaveformDownload(startP, spaceP, width_con);
            LayoutRows_SerialNumber(startP, spaceP, width_con);
            LayoutRows_WaveName(startP, spaceP, width_con);
        }

        private void InitNumericUpDownBySample(NumericUpDown textBox, int width_con)
        {
            textBox.Width = width_con;
            textBox.Text = "0";
            textBox.Visible = true;
            textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_NumericUpDownTempSetSample_KeyPress);
            textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
            textBox.Enter += new System.EventHandler(this.m_NumericUpDownTempSetSample_Enter);
            textBox.ValueChanged += new System.EventHandler(this.m_NumericUpDownVolBaseSample_ValueChanged);
        }


        private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
        {
            bool isValidNumber = true;
            NumericUpDown textBox = (NumericUpDown)sender;
            try
            {
#if true
                float val = float.Parse(textBox.Text);
                textBox.BeginInit();
                textBox.Value = new Decimal(val);
                textBox.EndInit();
#else
				float val = float.Parse(textBox.Text);
				textBox.Value = new Decimal(val);
#endif
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
                isValidNumber = false;
            }

            if (!isValidNumber)
            {
                //SystemCall.Beep(200,50);
                textBox.Focus();
                textBox.Select(0, textBox.Text.Length);
            }
        }
        private void UpdateToLocal()
        {
        }

        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                m_CheckBoxControl_Leave(sender, e);
                (sender as NumericUpDown).Select(0, 0);
            }
        }

        private void m_ButtonRefresh_Click(object sender, System.EventArgs e)
        {
            OnRealTimeChange();
        }

        private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
        {
            if (!CheckVoltValid(true))
            {
                ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException();
                MessageBox.Show(exception.Message);
            }
            else
            {
                ApplyToBoard();
            }
        }

        public void ApplyToBoard()
        {
            if (m_bXaar382)
            {
                SRealTimeCurrentInfo_382 sRT = new SRealTimeCurrentInfo_382();
                sRT.cTemperature = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cVtrim = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cPWM = new float[CoreConst.MAX_HEAD_NUM];

                OnGetRealTimeFromUI_382(ref sRT);
                if (CoreInterface.Set382RealTimeInfo(ref sRT) != 0)
                {
                }
                else
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
            }
            else
            {
                SRealTimeCurrentInfo sRT = new SRealTimeCurrentInfo();
                sRT.cTemperatureCur2 = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cTemperatureSet = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cTemperatureCur = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cPulseWidth = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cVoltage = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cVoltageBase = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cVoltageCurrent = new float[CoreConst.MAX_VOL_TEMP_NUM];
                sRT.cXaarVoltageInk = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cXaarVoltageOffset = new float[CoreConst.MAX_HEAD_NUM];
                OnGetRealTimeFromUI(ref sRT);
                if ((m_Konic512_1head2color
                    || (m_bExcept && m_b1head2color)
                    ) && !m_bVerArrangement)
                    ReMapVolAndTempToUI(sRT);
                else if (m_bPolaris_V7_16_Polaris)
                {
                    LogWriter.WriteLog(new string[] { string.Format("set cTemperatureSet before remap=[{0}]", string.Join(",", sRT.cTemperatureSet)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cVoltage before remap=[{0}]", string.Join(",", sRT.cVoltage)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cVoltageBase before remap=[{0}]", string.Join(",", sRT.cVoltageBase)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cPulseWidth before remap=[{0}]", string.Join(",", sRT.cPulseWidth)) }, true);
#if false                   
                    ReMapTempBufferForV7_16_Polaris(ref sRT.cTemperatureSet, m_rsPrinterPropery.nColorNum, false);
#endif
                    ReMapVolBufferForV7_16_Polaris(ref sRT.cVoltage, m_rsPrinterPropery.nColorNum, false);
                    ReMapVolBufferForV7_16_Polaris(ref sRT.cVoltageBase, m_rsPrinterPropery.nColorNum, false);
                    ReMapVolBufferForV7_16_Polaris(ref sRT.cPulseWidth, m_rsPrinterPropery.nColorNum, false);

                    LogWriter.WriteLog(new string[] { string.Format("set cTemperatureSet after remap=[{0}]", string.Join(",", sRT.cTemperatureSet)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cVoltage after remap=[{0}]", string.Join(",", sRT.cVoltage)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cVoltageBase after remap=[{0}]", string.Join(",", sRT.cVoltageBase)) }, true);
                    LogWriter.WriteLog(new string[] { string.Format("set cPulseWidth after remap=[{0}]", string.Join(",", sRT.cPulseWidth)) }, true);
                }
                else if (m_bMirrorArrangement && m_rsPrinterPropery.nColorNum>1)
                {
                    HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
                    if (hbType != HEAD_BOARD_TYPE.SG1024_4H &&
                        hbType != HEAD_BOARD_TYPE.SG1024_4H_GRAY &&
                        hbType != HEAD_BOARD_TYPE.M600SH_4HEAD)
                    {
                        ReMapBufferFor_Mirror(ref sRT.cTemperatureCur, m_rsPrinterPropery.nColorNum, false, m_b1head2color, 1);
                        ReMapBufferFor_Mirror(ref sRT.cTemperatureCur2, m_rsPrinterPropery.nColorNum, false, m_b1head2color, 1);
                        ReMapBufferFor_Mirror(ref sRT.cTemperatureSet, m_rsPrinterPropery.nColorNum, false, m_b1head2color, 1);
                        ReMapBufferFor_Mirror(ref sRT.cVoltage, m_rsPrinterPropery.nColorNum, false, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                        ReMapBufferFor_Mirror(ref sRT.cVoltageBase, m_rsPrinterPropery.nColorNum, false, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                        ReMapBufferFor_Mirror(ref sRT.cVoltageCurrent, m_rsPrinterPropery.nColorNum, false, m_b1head2color, (int)VOL_COUNT_PER_HEAD);
                        ReMapBufferFor_Mirror(ref sRT.cPulseWidth, m_rsPrinterPropery.nColorNum, false, m_b1head2color, 1);
                    }
                }
                uint Wmask = CalcRWMask(false);
                if (CoreInterface.SetRealTimeInfo(ref sRT, Wmask) != 0)
                {
                    if (WhichRowVisble[7])
                    {
                        byte[] buf = new byte[CoreConst.MAX_COLOR_NUM];
                        uint buflen = (uint)buf.Length;
                        for (int i = 0; i < m_NumericUpDownInkTempSet.Length; i++)
                        {
                            buf[i] = (byte)(m_NumericUpDownInkTempSet[i].Value * 5);
                        }
                        if (CoreInterface.SetEpsonEP0Cmd(0x5c, buf, ref buflen, 0, 9) == 0)
                            MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
                    }
                }
                else
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
            }
            this.isDirty = false;
        }
        private void m_NumericUpDownTempSetSample_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !(Char.IsPunctuation(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void m_NumericUpDownVolBaseSample_ValueChanged(object sender, System.EventArgs e)
        {
            NumericUpDown cur = (NumericUpDown)sender;
            cur.Text = cur.Value.ToString();
            this.isDirty = true;
        }

        private void m_NumericUpDownTempSetSample_Enter(object sender, System.EventArgs e)
        {
            NumericUpDown cur = (NumericUpDown)sender;
            cur.Select(0, cur.ToString().Length);
        }

        private void m_CheckBoxAutoRefresh_CheckedChanged(object sender, System.EventArgs e)
        {
            m_TimerRefresh.Enabled = ((CheckBox)sender).Checked;
            this.isDirty = true;
        }

        private void m_TimerRefresh_Tick(object sender, System.EventArgs e)
        {
            OnRealTimeChange();
        }

        private void m_ButtonDefault_Click(object sender, System.EventArgs e)
        {
            if (m_bXaar382)
            {
                SRealTimeCurrentInfo_382 info = new SRealTimeCurrentInfo_382();
                info.cTemperature = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cVtrim = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cPWM = new float[CoreConst.MAX_VOL_TEMP_NUM];

                //DefaultRealTimeValue(ref info);
                OnRenewRealTime_382(info);
            }
            else
            {
                SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
                info.cTemperatureCur2 = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cTemperatureSet = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cTemperatureCur = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cPulseWidth = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cVoltage = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cVoltageBase = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cVoltageCurrent = new float[CoreConst.MAX_VOL_TEMP_NUM];
                info.cXaarVoltageInk = new float[CoreConst.MAX_HEAD_NUM];
                info.cXaarVoltageOffset = new float[CoreConst.MAX_HEAD_NUM];
                DefaultRealTimeValue(ref info);
                OnRenewRealTime(info);
            }
        }
        private void DefaultRealTimeValue(ref SRealTimeCurrentInfo info)
        {
            bool bIsKM1024i = SPrinterProperty.IsKonica1024i(m_rsPrinterPropery.ePrinterHead);


            // m_pMap.Length 为温度/电压/脉宽个数的最大值
            for (int i = 0; i < m_pMap.Length; i++)
            {
                int nMap = m_pMap[i];
                if (m_bSpectra)
                {
                    info.cVoltageBase[nMap] = 100.0f;
                }
                else if (bIsKM1024i)
                {
                    info.cVoltageBase[nMap] = i % 3 == 0 ? 15 : 7.5f;
                }
                else if (m_bKonic1800i)
                {
                    info.cVoltageBase[nMap] = i % 4 == 0? 15 : 7.5f;
                }
                else if (m_bKyocera)
                {
                    info.cVoltageBase[nMap] = 24.0f;
                }
                else
                    info.cVoltageBase[nMap] = 15.0f;
            }
            for (int i = 0; i < m_pMap.Length; i++)
            {
                int nMap = m_pMap[i];
                if (m_bSpectra)
                {
                    info.cPulseWidth[nMap] = 8.0f;
                }
            }
        }

        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }
        private uint CalcRWMask(bool isRead)
        {
            uint ret = 0;
            for (int i = 0; i < this.WhichRowVisble.Length; i++)
            {
                if (this.WhichRowVisble[i])
                {
                    switch (i)
                    {
                        case 0:
                            ret |= 1 << (int)EnumVoltageTemp.TemperatureSet;
                            break;
                        case 1:
                            if (isRead)
                                ret |= 1 << (int)EnumVoltageTemp.TemperatureCur;
                            break;
                        case 2:
                            if (isRead)
                                ret |= 1 << (int)EnumVoltageTemp.TemperatureCur2;
                            break;
                        case 3:
                            ret |= 1 << (int)EnumVoltageTemp.VoltageAjust;
                            break;
                        case 4:
                            if (isRead)
                                ret |= 1 << (int)EnumVoltageTemp.VoltageCurrent;
                            break;
                        case 5:
                            ret |= 1 << (int)EnumVoltageTemp.VoltageBase;
                            break;
                        case 6:
                            ret |= 1 << (int)EnumVoltageTemp.PulseWidth;
                            break;
                        case 8:
                            ret |= 1 << (int)EnumVoltageTemp.XaarVolInk;
                            break;
                        case 9:
                            ret |= 1 << (int)EnumVoltageTemp.XaarVolOffset;
                            break;
                    }
                }
            }
            return ret;
        }

        private void ReMapVolAndTempToUI(SRealTimeCurrentInfo info)
        {
            ReMapBuffer(ref info.cTemperatureCur, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cTemperatureCur2, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cTemperatureSet, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cVoltage, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cVoltageBase, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cVoltageCurrent, m_rsPrinterPropery.nColorNum);
            ReMapBuffer(ref info.cPulseWidth, m_rsPrinterPropery.nColorNum);
        }

        private void ReMapBuffer(ref float[] buffer, int colornum)
        {
            for (int i = 0; i < buffer.Length; )
            {
                if (i / colornum % 2 == 1)
                {
                    for (int j = 0; j < colornum; j += 2)
                    {
                        if (i + j + 1 >= buffer.Length)
                            break;
                        float t = buffer[i + j];
                        buffer[i + j] = buffer[i + j + 1];
                        buffer[i + j + 1] = t;
                    }
                    i += colornum;
                }
                else
                    i++;
            }
        }

        /// <summary>
        /// 调整镜像排列时内存数组顺序,默认是按色序按组依次排列的K1C1M1Y1K2C2M2Y2...
        /// </summary>
        /// <param name="buffer">源数据数组</param>
        /// <param name="colornum">颜色数</param>
        /// <param name="bGet">是刷新还是应用</param>
        /// <param name="b1hd2Color">是否为一头俩色</param>
        private void ReMapBufferFor_Mirror(ref float[] buffer, int colornum,bool bGet,bool b1hd2Color,int rows)
        {
            if (colornum % 2 == 0)
            {
                if (b1hd2Color)
                    colornum /= 2;
                int reMapLen = buffer.Length - buffer.Length % (colornum * rows); //颜色数整数倍
                for (int i = 0; i < reMapLen; i += colornum * rows)
                {
                    if (i / (colornum * rows) % 2 == 1)
                    {
                        float[] subbuf = buffer.Skip(i).Take(colornum * rows).Reverse().ToArray();
                        for (int j = 0; j < colornum; j++)
                        {
                            float[] subbuf2 = subbuf.Skip(j * rows).Take(rows).Reverse().ToArray();
                            Array.Copy(subbuf2, 0, subbuf, j * rows, rows);
                        }
                        Array.Copy(subbuf, 0, buffer, i, subbuf.Length);
                    }
                }
            }
            else
            {
                List<float> temp = new List<float>();
                if (bGet)
                {
                    if (b1hd2Color)
                        colornum = colornum / 2 + 1;
                    int reMapLen = buffer.Length - buffer.Length % colornum; //颜色数整数倍
                    for (int i = 0; i < reMapLen; i += colornum)
                    {
                        if (i / colornum % 2 == 1)
                        {
                            float[] subbuf = buffer.Skip(i).Take(colornum).Reverse().ToArray();
                            temp.AddRange(subbuf.Skip(1));
                        }
                        else
                        {
                            temp.AddRange(buffer.Skip(i).Take(colornum));
                        }
                    }
                }
                else
                {
                    int headNumPerGroup = colornum;
                    if (b1hd2Color)
                        headNumPerGroup = colornum / 2 + 1;
                    int reMapLen = buffer.Length - buffer.Length % colornum; //颜色数整数倍
                    for (int i = 0; i < reMapLen; i += colornum)
                    {
                        float[] subbuf = buffer.Skip(i).Take(colornum).ToArray();
                        temp.AddRange(subbuf.Take(headNumPerGroup));
                        temp.AddRange(subbuf.Skip(headNumPerGroup-1).Take(headNumPerGroup).Reverse());
                    }
                }
                Array.Copy(temp.ToArray(), 0, buffer, 0, Math.Min(buffer.Length, temp.Count));
            }
        }

        private void ReMapVolBufferForV7_16_Polaris(ref float[] buffer, int colornum, bool bGet)
        {
            float[] tempBuf = new float[buffer.Length];
            if (bGet)
            {
                if (!m_bMirrorArrangement)
                {
                    for (int i = 0; i < buffer.Length; i += colornum * 2)
                    {
                        if (i + colornum >= buffer.Length) break;
                        Buffer.BlockCopy(buffer, i * 4, tempBuf, i * 4 / 2, colornum * 4);
                    }
                }
                else
                {
                    tempBuf = buffer;
                }
            }
            else
            {
                if (m_bMirrorArrangement)
                {
                    Array.Copy(buffer, 0, buffer, m_NumericUpDownVoltageBase.Length, m_NumericUpDownVoltageBase.Length);
                    tempBuf = buffer;
                }
                else
                {
                    for (int i = 0; i < buffer.Length; i += colornum * 2)
                    {
                        if (i + colornum >= buffer.Length) break;
                        Buffer.BlockCopy(buffer, i * 4 / 2, tempBuf, i * 4, colornum * 4);
                        if (i + colornum * 2 >= buffer.Length) break;
                        Buffer.BlockCopy(buffer, i * 4 / 2, tempBuf, (i + colornum) * 4, colornum * 4);
                    }
                }
            }
            buffer = tempBuf;
        }
#if false
        private void ReMapTempBufferForV7_16_Polaris(ref float[] buffer, int colornum, bool bGet)
        {
            float[] tempBuf = new float[buffer.Length];
            if (bGet)
            {
                if (m_bMirrorArrangement)
                {
                    /* 4色时
                     * 1,9,2,10,3,11,4,12,5,13,6,14,7,15,8,16 
                     * 变成
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     */
                    int step = colornum * 4;
                    for (int i = 0; i < buffer.Length; i += step)
                    {
                        for (int j = 0; j < step / 2; j++)
                        {
                            tempBuf[i + j * 2] = buffer[i + j];
                            tempBuf[i + j * 2 + 1] = buffer[i + j + colornum * 2];
                        }
                    }
                }
                else
                {
                    /* 4色时
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     * 变成
                     * 1,5,2,6,3,7,4,8,9,13,10,14,11,15,12,16 
                     */
                    for (int i = 0; i < buffer.Length; i += colornum * 2)
                    {
                        for (int j = 0; j < colornum * 2; j += 2)
                        {
                            tempBuf[i + j] = buffer[i + j / 2];
                            tempBuf[i + j + 1] = buffer[i + j / 2 + colornum];
                        }
                    }
                }
            }  
            else
            {
                if (m_bMirrorArrangement)
                {
                    /* 4色时
                     * 1,9,2,10,3,11,4,12,5,13,6,14,7,15,8,16 
                     * 变成
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     */
                    int step = colornum*4;
                    for (int i = 0; i < buffer.Length; i += step)
                    {
                        for (int j = 0; j < step/2; j++)
                        {
                            tempBuf[i + j*2] = buffer[i + j];
                            tempBuf[i + j*2 + 1] = buffer[i + j + colornum * 2];
                        }
                    }
                }
                else
                {
                    /* 1,5,2,6,3,7,4,8,9,13,10,14,11,15,12,16 
                     * 变成
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     */
                    List<float> temp = new List<float>();
                    for (int i = 0; i < buffer.Length/(colornum*2); i++)
                    {
                        List<float> group1 = new List<float>();
                        List<float> group2 = new List<float>();
                        for (int j = 0; j < colornum*2; j++)
                        {
                            if ((i + j)%2 == 0)
                                group1.Add(buffer[i*colornum*2 + j]);
                            else
                                group2.Add(buffer[i*colornum*2 + j]);
                        }
                        temp.AddRange(group1);
                        temp.AddRange(group2);
                    }
                    Buffer.BlockCopy(temp.ToArray(), 0, tempBuf, 0, temp.Count*4);
                }
            }
            buffer = tempBuf;
        }
#else
        private void ReMapTempBufferForV7_16_Polaris(ref float[] buffer, int colornum, bool bGet)
        {
            float[] tempBuf = new float[buffer.Length];
            if (bGet)
            {
                if (m_bMirrorArrangement)
                {
                    LogWriter.WriteLog(new string[] { "get hittest" }, true);
                    /* 4色时
                     * 1,9,2,10,3,11,4,12,5,13,6,14,7,15,8,16 
                     * 变成
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     */
                    byte[] map16_Get = //{0,8,1,9,2,10,3,11,4,12,5,13,6,14,7,15};
                                        { 0, 2, 4, 6, 8, 10, 12, 14, 1, 3, 5, 7, 9, 11, 13, 15 };
                    for (int i = 0; i < map16_Get.Length; i++)
                        tempBuf[map16_Get[i]] = buffer[i];
                }
                else
                {
                    /* 4色时
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     * 变成
                     * 1,5,2,6,3,7,4,8,9,13,10,14,11,15,12,16 
                     */
                    for (int i = 0; i < buffer.Length; i += colornum * 2)
                    {

                        for (int j = 0; j < colornum * 2; j += 2)
                        {
                            tempBuf[i + j] = buffer[i + j / 2];
                            tempBuf[i + j + 1] = buffer[i + j / 2 + colornum];
                        }
                    }
                }
            }
            else
            {
                if (m_bMirrorArrangement)
                {
                    LogWriter.WriteLog(new string[] { "set hittest" }, true);
                    byte[] map16_Set = //{0,8,1,9,2,10,3,11,4,12,5,13,6,14,7,15};
                                        { 0, 2, 4, 6, 8, 10, 12, 14, 1, 3, 5, 7, 9, 11, 13, 15 };
                    for (int i = 0; i < map16_Set.Length; i++)
                        tempBuf[i] = buffer[map16_Set[i]];
                }
                else
                {
                    /* 1,5,2,6,3,7,4,8,9,13,10,14,11,15,12,16 
                     * 变成
                     * 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
                     */
                    List<float> temp = new List<float>();
                    for (int i = 0; i < buffer.Length / (colornum * 2); i++)
                    {
                        List<float> group1 = new List<float>();
                        List<float> group2 = new List<float>();
                        for (int j = 0; j < colornum * 2; j++)
                        {
                            if ((i + j) % 2 == 0)
                                group1.Add(buffer[i * colornum * 2 + j]);
                            else
                                group2.Add(buffer[i * colornum * 2 + j]);
                        }
                        temp.AddRange(group1);
                        temp.AddRange(group2);
                    }
                    Buffer.BlockCopy(temp.ToArray(), 0, tempBuf, 0, temp.Count * 4);
                }
            }
            LogWriter.WriteLog(new string[] { string.Format("remaped tempbuf=[{0}]", string.Join(",", tempBuf)) }, true);

            buffer = tempBuf;
        }
#endif

        private void KonicTemperature1_ValueChanged(object sender, EventArgs e)
        {
            if (m_bKonic512 && !m_bSupportHeadHeat)
            {
                int min = 15;
                int max = 28;
                NumericUpDown num = sender as NumericUpDown;
                if (num.Value >= min && num.Value <= max)
                    num.BackColor = Color.Green;
                if (num.Value > max || num.Value < min)
                    num.BackColor = Color.Red;
            }
        }

        private void km1024i_ValueChanged(object sender, EventArgs e)
        {
            if (!bloaded)
                return;
            if (!CheckVoltValid(false))
            {
                ((NumericUpDown)sender).BackColor = Color.Red;
            }
            else
            {
                ((NumericUpDown)sender).BackColor = SystemColors.Window;
            }
        }

        private const float KM1024_I_FULL_VOL_MAX = 18f;
        private const float KM1024_I_FULL_VOL_MIN = 4f;

        private bool CheckVoltValid(bool changeBkg)
        {
#if true
            bool bIsKM1024i = SPrinterProperty.IsKonica1024i(m_rsPrinterPropery.ePrinterHead);

            if (bIsKM1024i)
            {
                //decimal halfMax = new decimal(8.5);
                //decimal halfMin = 4;
                decimal maxDelta = 2;
                bool ret = true;
                for (int head_num = 0; head_num < m_NumericUpDownVoltageBase.Length / VOL_COUNT_PER_HEAD; head_num++)
                {
                    int fullIndex = (int)(head_num * VOL_COUNT_PER_HEAD + 0);
                    int halfIndexL = (int)(head_num * VOL_COUNT_PER_HEAD + 1);
                    int halfIndexR = (int)(head_num * VOL_COUNT_PER_HEAD + 2);
                    decimal vh2 = m_NumericUpDownVoltageBase[fullIndex].Value + m_NumericUpDownVoltageSet[fullIndex].Value; //full voltage
                    decimal vh1_a = m_NumericUpDownVoltageBase[halfIndexL].Value + m_NumericUpDownVoltageSet[halfIndexL].Value; //half voltage
                    decimal vh1_b = m_NumericUpDownVoltageBase[halfIndexR].Value + m_NumericUpDownVoltageSet[halfIndexR].Value; //half voltage

                    //decimal half_vh2 = vh2/2;
                    if ((vh2 < (decimal)KM1024_I_FULL_VOL_MIN) || (vh2 > (decimal)KM1024_I_FULL_VOL_MAX))
                    {
                        ret = false;
                    }
                    //if ((vh1_a > half_vh2 + maxDelta) || (vh1_a < half_vh2 - maxDelta) || (vh1_b > half_vh2 + maxDelta) || (vh1_b < half_vh2 - maxDelta))
                    //    return false;
                    if (vh2 < (vh1_a + maxDelta) || vh2 < (vh1_b + maxDelta))
                    {
                        ret = false;
                    }
                    if (changeBkg)
                    {
                        m_NumericUpDownVoltageBase[fullIndex].BackColor = ret ? SystemColors.Window : Color.Red;
                        m_NumericUpDownVoltageSet[fullIndex].BackColor = ret ? SystemColors.Window : Color.Red;
                    }
                    if (!ret)
                    {
                        return ret;
                    }
                }
            }
#endif
            return true;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.OverwritePrompt = true;
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Env);

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
                uint Rmask = CalcRWMask(true);
                if (CoreInterface.GetRealTimeInfo(ref info, Rmask) != 0)
                {
                    XmlElement root;
                    SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                    root = doc.CreateElement("", "RealTimeParam", "");
                    doc.AppendChild(root);
                    string xml = PubFunc.SystemConvertToXml(info, info.GetType());
                    root.InnerXml = xml;
                    doc.Save(fileDialog.FileName);
                }
                else
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof (UIError), UIError.GetRealTimeInfoFail),
                        ResString.GetProductName());
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
                  OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Env);

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                doc.Load(fileDialog.FileName);
                XmlElement root = doc.DocumentElement;
                XmlElement elem_i;
                elem_i = PubFunc.GetFirstChildByName(root, typeof(SRealTimeCurrentInfo).Name);
                if (elem_i != null)
                {
                    SRealTimeCurrentInfo info =
                        (SRealTimeCurrentInfo)PubFunc.SystemConvertFromXml(elem_i.OuterXml, typeof(SRealTimeCurrentInfo));
                    OnRenewRealTime(info);
                }
            }
        }

    }
   
}
