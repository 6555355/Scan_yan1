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
using System.Threading;
using System.Globalization;

namespace BYHXPrinterManager.Setting
{
    /// <summary>
    /// Summary description for KonicTemperature1.
    /// </summary>
    public class CombinedSettings : BYHXUserControl//System.Windows.Forms.UserControl
    {
        enum EnumVoltageTemp
        {
            TemperatureCur2 = 7,
            TemperatureSet = 6,
            TemperatureCur = 5,
            PulseWidth = 1,
            VoltageBase = 3,
            VoltageCurrent = 0,
            VoltageAjust = 2, //Adjust
        }
        private SPrinterProperty m_rsPrinterPropery;//Only read for color order

        private int Voltage_LR_Num = 0;
        private int m_HeadNum = 0;
        private int m_TempNum = 0;
        private int m_HeadVoltageNum = 0;
        private const int VOLCOUNTPERPOLARISHEAD = 2;
        private byte m_StartHeadIndex = 0;
        private byte[] m_pMap;
        private bool m_bSpectra = false;
        private bool m_bKonic512 = false;
        private bool m_bSupportHeadHeat = false;
        private bool m_bXaar382 = false;
        private bool m_bPolaris = false;
        private bool m_bExcept = false;
        private bool isDirty = false;
        private bool isT_VDirty = false;
        private int MaxHeightT_V = 0;
        public event EventHandler ApplyButtonClicked;
        //LogWriter loger = new LogWriter();
        private const float MAXPULSEWIDTH = 12;
        private const float MINPULSEWIDTH = 6;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }
        public bool IsT_VDirty
        {
            get { return isT_VDirty; }
            set { isT_VDirty = value; }
        }
        //private System.Windows.Forms.Label[] m_LabelLRIndex;
        //private System.Windows.Forms.Label[] m_LabelHorHeadIndex;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadSet;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadCur;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownHeadTeam;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageSet;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageCur;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageBase;
        //private System.Windows.Forms.NumericUpDown[] m_NumericUpDownPulseWidth;
        private bool[] WhichRowVisble = new bool[7];

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private System.Windows.Forms.Button m_ButtonToBoard;
        private System.Windows.Forms.Button m_ButtonRefresh;
        private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxTemperature;
        private System.Windows.Forms.Timer m_TimerRefresh;
        private System.Windows.Forms.CheckBox m_CheckBoxAutoRefresh;
        private System.Windows.Forms.Button m_ButtonDefault;
        private CheckBox m_CheckBoxAutoJumpWhite;
        private NumericUpDown m_NumericUpDownJobSpace;
        private NumericUpDown m_NumericUpDownSprayTimes;
        private ToolTip m_ToolTip;
        private Label m_LabelJobSpace;
        private Label m_LabelLeftEdge;
        private NumericUpDown m_NumericUpDownFeather;
        private Label m_LabelFeather;
        private Label m_LabelAutoSpray;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxPrintSetting;
        private CheckBox m_CheckBoxYContinue;
        private Label m_LabelStepTime;
        private NumericUpDown m_NumericUpDownStepTime;
        private Label m_LabelSprayTimes;
        private NumericUpDown m_NumericUpDownAutoSpray;
        private NumericUpDown m_NumericUpDownSprayCycle;
        private Label m_LabelSprayCycle;
        private CheckBox m_CheckBoxIdleSpray;
        private NumericUpDown m_NumericUpDownZSpace;
        private Label m_Label1;
        private CheckBox m_CheckBoxKillBidir;
        private CheckBox m_CheckBoxAutoYCalibrate;
        private NumericUpDown m_NumericUpDownThickness;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxZ;
        private Label m_LabelZSpace;
        private Button m_ButtonZAuto;
        private Button m_ButtonZManual;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxWhiteInk;
        private ComboBox m_ComboBoxWhiteInk;
        private NumericUpDown m_NumericUpDownPTASpraying;
        private CheckBox m_CheckBoxNormalType;
        private Label m_LabelAutoClean;
        private ComboBox m_ComboBoxPlace;
        private System.Windows.Forms.Panel m_GroupBoxMedia;
        private CheckBox m_CheckBoxMeasureBeforePrint;
        private NumericUpDown m_NumericUpDownLeftEdge;
        private NumericUpDown m_NumericUpDownWidth;
        private Label m_LabelWidth;
        private Label m_LabelY;
        private NumericUpDown m_NumericUpDownY;
        private Label m_LabelHeight;
        private NumericUpDown m_NumericUpDownHeight;
        private Button m_ButtonMeasure;
        private Label m_LabelMargin;
        private NumericUpDown m_NumericUpDownMargin;
        private NumericUpDown m_NumericUpDownStripeSpace;
        private Label m_LabelStripeWidth;
        private System.Windows.Forms.Panel m_GroupBoxInkStripe;
        private NumericUpDown m_NumericUpDownStripeWidth;
        private Label m_LabelStripeSpace;
        private Label m_LabelStripePos;
        private CheckBox m_CheckBoxMixedType;
        private CheckBox m_CheckBoxHeightWithImageType;
        private Label m_labelPrintPrespraytime;
        private CheckBox m_CheckBoxSprayBeforePrint;
        private NumericUpDown m_NumericUpDownAutoClean;
        private NumericUpDown m_NumericUpDownCleanTimes;
        private Label m_LabelCleanTimes;
        private Label m_labelPauseTimeAfterCleaning;
        private NumericUpDown m_NumericUpDownPTACleaning;
        private BYHXPrinterManager.GradientControls.CrystalPanel m_GroupBoxClean;
        private BYHXPrinterManager.GradientControls.CrystalPanel grouperPreferenceSetting;
        private ComboBox m_ComboBoxUnit;
        private ComboBox m_ComboBoxLang;
        private Label m_labelLang;
        private Label m_labelUnit;
        private Panel panel1;
        private Panel panel2;
        private DataGridView dataGridViewTempAndVol;
        private Label labelWhiteInk;
        private Panel panel3;
        private Panel panelBottomButtons;
        private Button buttonApply;
        private DataGridViewTextBoxColumn Column_SetTemp;
        private DataGridViewTextBoxColumn ColumnNozzleTemp;
        private DataGridViewTextBoxColumn ColumnHeatingTemp;
        private DataGridViewTextBoxColumn ColumnVoltageAdjust;
        private DataGridViewTextBoxColumn ColumnCurrentVoltage;
        private DataGridViewTextBoxColumn ColumnBaseVoltage;
        private DataGridViewTextBoxColumn ColumnPulseWidth;
        private NumericUpDown m_NumericUpDownFeatherPercent;
        private ComboBox m_ComboBoxFeatherType;
        private Label label_FeatherType;
        private NumericUpDown m_NmericUpDownWaveLen;
        private System.ComponentModel.IContainer components;

        public CombinedSettings()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            ToolTipInit();
            m_NmericUpDownWaveLen.Location = m_NumericUpDownFeatherPercent.Location;

#if !LIYUUSB
			m_LabelSprayTimes.Visible = false;
			m_NumericUpDownSprayTimes.Visible = false;
#else
            //m_CheckBoxHeightWithImageType.Visible = false;
            m_CheckBoxKillBidir.Visible = false;
            //m_labelPrintPrespraytime.Visible = false;
            //m_NumericUpDownPTASpraying.Visible = false;
            m_labelPauseTimeAfterCleaning.Visible = false;
            m_NumericUpDownPTACleaning.Visible = false;
            m_ButtonToBoard.Visible = false;
#endif
            CalcT_V_MaxHeight();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombinedSettings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_ButtonToBoard = new System.Windows.Forms.Button();
            this.m_ButtonRefresh = new System.Windows.Forms.Button();
            this.m_GroupBoxTemperature = new BYHXPrinterManager.GradientControls.Grouper();
            this.dataGridViewTempAndVol = new System.Windows.Forms.DataGridView();
            this.Column_SetTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNozzleTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHeatingTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVoltageAdjust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCurrentVoltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBaseVoltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPulseWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBottomButtons = new System.Windows.Forms.Panel();
            this.m_CheckBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.m_ButtonDefault = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.m_TimerRefresh = new System.Windows.Forms.Timer(this.components);
            this.m_CheckBoxAutoJumpWhite = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownJobSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownSprayTimes = new System.Windows.Forms.NumericUpDown();
            this.m_LabelJobSpace = new System.Windows.Forms.Label();
            this.m_LabelLeftEdge = new System.Windows.Forms.Label();
            this.m_NumericUpDownFeather = new System.Windows.Forms.NumericUpDown();
            this.m_LabelFeather = new System.Windows.Forms.Label();
            this.m_LabelAutoSpray = new System.Windows.Forms.Label();
            this.m_GroupBoxPrintSetting = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.m_CheckBoxMeasureBeforePrint = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxNormalType = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownLeftEdge = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxPlace = new System.Windows.Forms.ComboBox();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.m_NumericUpDownStripeSpace = new System.Windows.Forms.NumericUpDown();
            this.m_LabelY = new System.Windows.Forms.Label();
            this.m_NumericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownStripeWidth = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHeight = new System.Windows.Forms.Label();
            this.m_NumericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.m_LabelStripeWidth = new System.Windows.Forms.Label();
            this.m_ButtonMeasure = new System.Windows.Forms.Button();
            this.m_LabelMargin = new System.Windows.Forms.Label();
            this.m_LabelStripeSpace = new System.Windows.Forms.Label();
            this.m_NumericUpDownMargin = new System.Windows.Forms.NumericUpDown();
            this.m_LabelStripePos = new System.Windows.Forms.Label();
            this.m_CheckBoxYContinue = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxMixedType = new System.Windows.Forms.CheckBox();
            this.m_LabelStepTime = new System.Windows.Forms.Label();
            this.m_CheckBoxHeightWithImageType = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownStepTime = new System.Windows.Forms.NumericUpDown();
            this.label_FeatherType = new System.Windows.Forms.Label();
            this.m_NmericUpDownWaveLen = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownFeatherPercent = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxFeatherType = new System.Windows.Forms.ComboBox();
            this.m_LabelSprayTimes = new System.Windows.Forms.Label();
            this.m_NumericUpDownAutoSpray = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownSprayCycle = new System.Windows.Forms.NumericUpDown();
            this.m_LabelSprayCycle = new System.Windows.Forms.Label();
            this.m_CheckBoxIdleSpray = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownZSpace = new System.Windows.Forms.NumericUpDown();
            this.m_Label1 = new System.Windows.Forms.Label();
            this.m_CheckBoxKillBidir = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxAutoYCalibrate = new System.Windows.Forms.CheckBox();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_NumericUpDownPTASpraying = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownAutoClean = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownCleanTimes = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownPTACleaning = new System.Windows.Forms.NumericUpDown();
            this.m_LabelAutoClean = new System.Windows.Forms.Label();
            this.m_LabelCleanTimes = new System.Windows.Forms.Label();
            this.m_labelPauseTimeAfterCleaning = new System.Windows.Forms.Label();
            this.m_NumericUpDownThickness = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxZ = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.m_LabelZSpace = new System.Windows.Forms.Label();
            this.m_ButtonZAuto = new System.Windows.Forms.Button();
            this.m_ButtonZManual = new System.Windows.Forms.Button();
            this.m_GroupBoxWhiteInk = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.labelWhiteInk = new System.Windows.Forms.Label();
            this.m_ComboBoxWhiteInk = new System.Windows.Forms.ComboBox();
            this.m_GroupBoxMedia = new System.Windows.Forms.Panel();
            this.m_GroupBoxInkStripe = new System.Windows.Forms.Panel();
            this.m_labelPrintPrespraytime = new System.Windows.Forms.Label();
            this.m_CheckBoxSprayBeforePrint = new System.Windows.Forms.CheckBox();
            this.m_GroupBoxClean = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.grouperPreferenceSetting = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.m_ComboBoxUnit = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxLang = new System.Windows.Forms.ComboBox();
            this.m_labelLang = new System.Windows.Forms.Label();
            this.m_labelUnit = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_GroupBoxTemperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTempAndVol)).BeginInit();
            this.panelBottomButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeather)).BeginInit();
            this.m_GroupBoxPrintSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NmericUpDownWaveLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeatherPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoSpray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTASpraying)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCleanTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTACleaning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).BeginInit();
            this.m_GroupBoxZ.SuspendLayout();
            this.m_GroupBoxWhiteInk.SuspendLayout();
            this.m_GroupBoxClean.SuspendLayout();
            this.grouperPreferenceSetting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.m_GroupBoxTemperature.Controls.Add(this.dataGridViewTempAndVol);
            resources.ApplyResources(this.m_GroupBoxTemperature, "m_GroupBoxTemperature");
            this.m_GroupBoxTemperature.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxTemperature.GroupImage = null;
            this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
            this.m_GroupBoxTemperature.PaintGroupBox = false;
            this.m_GroupBoxTemperature.RoundCorners = 5;
            this.m_GroupBoxTemperature.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxTemperature.ShadowControl = false;
            this.m_GroupBoxTemperature.ShadowThickness = 3;
            this.m_GroupBoxTemperature.TabStop = false;
            this.m_GroupBoxTemperature.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxTemperature.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.m_GroupBoxTemperature.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // dataGridViewTempAndVol
            // 
            this.dataGridViewTempAndVol.AllowUserToAddRows = false;
            this.dataGridViewTempAndVol.AllowUserToDeleteRows = false;
            this.dataGridViewTempAndVol.AllowUserToResizeRows = false;
            this.dataGridViewTempAndVol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTempAndVol.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTempAndVol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTempAndVol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTempAndVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTempAndVol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_SetTemp,
            this.ColumnNozzleTemp,
            this.ColumnHeatingTemp,
            this.ColumnVoltageAdjust,
            this.ColumnCurrentVoltage,
            this.ColumnBaseVoltage,
            this.ColumnPulseWidth});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N1";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTempAndVol.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridViewTempAndVol, "dataGridViewTempAndVol");
            this.dataGridViewTempAndVol.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewTempAndVol.MultiSelect = false;
            this.dataGridViewTempAndVol.Name = "dataGridViewTempAndVol";
            this.dataGridViewTempAndVol.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewTempAndVol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewTempAndVol.RowTemplate.Height = 23;
            this.dataGridViewTempAndVol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewTempAndVol.ShowEditingIcon = false;
            this.dataGridViewTempAndVol.ShowRowErrors = false;
            this.dataGridViewTempAndVol.TabStop = false;
            this.dataGridViewTempAndVol.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTempAndVol_CellValueChanged);
            this.dataGridViewTempAndVol.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridViewTempAndVol.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridViewTempAndVol.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridViewTempAndVol.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // Column_SetTemp
            // 
            resources.ApplyResources(this.Column_SetTemp, "Column_SetTemp");
            this.Column_SetTemp.Name = "Column_SetTemp";
            this.Column_SetTemp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnNozzleTemp
            // 
            resources.ApplyResources(this.ColumnNozzleTemp, "ColumnNozzleTemp");
            this.ColumnNozzleTemp.Name = "ColumnNozzleTemp";
            this.ColumnNozzleTemp.ReadOnly = true;
            this.ColumnNozzleTemp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnHeatingTemp
            // 
            resources.ApplyResources(this.ColumnHeatingTemp, "ColumnHeatingTemp");
            this.ColumnHeatingTemp.Name = "ColumnHeatingTemp";
            this.ColumnHeatingTemp.ReadOnly = true;
            this.ColumnHeatingTemp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnVoltageAdjust
            // 
            resources.ApplyResources(this.ColumnVoltageAdjust, "ColumnVoltageAdjust");
            this.ColumnVoltageAdjust.Name = "ColumnVoltageAdjust";
            this.ColumnVoltageAdjust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnCurrentVoltage
            // 
            resources.ApplyResources(this.ColumnCurrentVoltage, "ColumnCurrentVoltage");
            this.ColumnCurrentVoltage.Name = "ColumnCurrentVoltage";
            this.ColumnCurrentVoltage.ReadOnly = true;
            this.ColumnCurrentVoltage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnBaseVoltage
            // 
            resources.ApplyResources(this.ColumnBaseVoltage, "ColumnBaseVoltage");
            this.ColumnBaseVoltage.Name = "ColumnBaseVoltage";
            this.ColumnBaseVoltage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnPulseWidth
            // 
            resources.ApplyResources(this.ColumnPulseWidth, "ColumnPulseWidth");
            this.ColumnPulseWidth.Name = "ColumnPulseWidth";
            this.ColumnPulseWidth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelBottomButtons
            // 
            this.panelBottomButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelBottomButtons.Controls.Add(this.m_CheckBoxAutoRefresh);
            this.panelBottomButtons.Controls.Add(this.m_ButtonDefault);
            this.panelBottomButtons.Controls.Add(this.m_ButtonToBoard);
            this.panelBottomButtons.Controls.Add(this.m_ButtonRefresh);
            this.panelBottomButtons.Controls.Add(this.buttonApply);
            resources.ApplyResources(this.panelBottomButtons, "panelBottomButtons");
            this.panelBottomButtons.Name = "panelBottomButtons";
            // 
            // m_CheckBoxAutoRefresh
            // 
            resources.ApplyResources(this.m_CheckBoxAutoRefresh, "m_CheckBoxAutoRefresh");
            this.m_CheckBoxAutoRefresh.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxAutoRefresh.Name = "m_CheckBoxAutoRefresh";
            this.m_CheckBoxAutoRefresh.UseVisualStyleBackColor = false;
            this.m_CheckBoxAutoRefresh.CheckedChanged += new System.EventHandler(this.m_CheckBoxAutoRefresh_CheckedChanged);
            // 
            // m_ButtonDefault
            // 
            resources.ApplyResources(this.m_ButtonDefault, "m_ButtonDefault");
            this.m_ButtonDefault.Name = "m_ButtonDefault";
            this.m_ButtonDefault.Click += new System.EventHandler(this.m_ButtonDefault_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // m_TimerRefresh
            // 
            this.m_TimerRefresh.Interval = 1000;
            this.m_TimerRefresh.Tick += new System.EventHandler(this.m_TimerRefresh_Tick);
            // 
            // m_CheckBoxAutoJumpWhite
            // 
            resources.ApplyResources(this.m_CheckBoxAutoJumpWhite, "m_CheckBoxAutoJumpWhite");
            this.m_CheckBoxAutoJumpWhite.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxAutoJumpWhite.Name = "m_CheckBoxAutoJumpWhite";
            this.m_CheckBoxAutoJumpWhite.UseVisualStyleBackColor = false;
            this.m_CheckBoxAutoJumpWhite.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownJobSpace
            // 
            this.m_NumericUpDownJobSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownJobSpace, "m_NumericUpDownJobSpace");
            this.m_NumericUpDownJobSpace.Name = "m_NumericUpDownJobSpace";
            this.m_NumericUpDownJobSpace.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownSprayTimes
            // 
            resources.ApplyResources(this.m_NumericUpDownSprayTimes, "m_NumericUpDownSprayTimes");
            this.m_NumericUpDownSprayTimes.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.m_NumericUpDownSprayTimes.Name = "m_NumericUpDownSprayTimes";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownSprayTimes, resources.GetString("m_NumericUpDownSprayTimes.ToolTip"));
            this.m_NumericUpDownSprayTimes.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelJobSpace
            // 
            resources.ApplyResources(this.m_LabelJobSpace, "m_LabelJobSpace");
            this.m_LabelJobSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelJobSpace.Name = "m_LabelJobSpace";
            // 
            // m_LabelLeftEdge
            // 
            resources.ApplyResources(this.m_LabelLeftEdge, "m_LabelLeftEdge");
            this.m_LabelLeftEdge.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelLeftEdge.Name = "m_LabelLeftEdge";
            // 
            // m_NumericUpDownFeather
            // 
            resources.ApplyResources(this.m_NumericUpDownFeather, "m_NumericUpDownFeather");
            this.m_NumericUpDownFeather.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownFeather.Name = "m_NumericUpDownFeather";
            this.m_NumericUpDownFeather.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelFeather
            // 
            resources.ApplyResources(this.m_LabelFeather, "m_LabelFeather");
            this.m_LabelFeather.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelFeather.Name = "m_LabelFeather";
            // 
            // m_LabelAutoSpray
            // 
            resources.ApplyResources(this.m_LabelAutoSpray, "m_LabelAutoSpray");
            this.m_LabelAutoSpray.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoSpray.Name = "m_LabelAutoSpray";
            // 
            // m_GroupBoxPrintSetting
            // 
            this.m_GroupBoxPrintSetting.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxPrintSetting, "m_GroupBoxPrintSetting");
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxMeasureBeforePrint);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxNormalType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownLeftEdge);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownWidth);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_ComboBoxPlace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelWidth);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownJobSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelLeftEdge);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownStripeSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelY);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelJobSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownY);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownStripeWidth);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelHeight);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownFeather);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownHeight);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_ButtonMeasure);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelFeather);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelMargin);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStripeSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownMargin);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStripePos);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxYContinue);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxMixedType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStepTime);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxHeightWithImageType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownStepTime);
            this.m_GroupBoxPrintSetting.Controls.Add(this.label_FeatherType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxAutoJumpWhite);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NmericUpDownWaveLen);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownFeatherPercent);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_ComboBoxFeatherType);
            this.m_GroupBoxPrintSetting.Divider = true;
            this.m_GroupBoxPrintSetting.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxPrintSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_GroupBoxPrintSetting.Name = "m_GroupBoxPrintSetting";
            // 
            // m_CheckBoxMeasureBeforePrint
            // 
            resources.ApplyResources(this.m_CheckBoxMeasureBeforePrint, "m_CheckBoxMeasureBeforePrint");
            this.m_CheckBoxMeasureBeforePrint.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxMeasureBeforePrint.Name = "m_CheckBoxMeasureBeforePrint";
            this.m_CheckBoxMeasureBeforePrint.UseVisualStyleBackColor = false;
            this.m_CheckBoxMeasureBeforePrint.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxNormalType
            // 
            resources.ApplyResources(this.m_CheckBoxNormalType, "m_CheckBoxNormalType");
            this.m_CheckBoxNormalType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxNormalType.Name = "m_CheckBoxNormalType";
            this.m_CheckBoxNormalType.UseVisualStyleBackColor = false;
            this.m_CheckBoxNormalType.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownLeftEdge
            // 
            this.m_NumericUpDownLeftEdge.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownLeftEdge, "m_NumericUpDownLeftEdge");
            this.m_NumericUpDownLeftEdge.Name = "m_NumericUpDownLeftEdge";
            this.m_NumericUpDownLeftEdge.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownWidth
            // 
            this.m_NumericUpDownWidth.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownWidth, "m_NumericUpDownWidth");
            this.m_NumericUpDownWidth.Name = "m_NumericUpDownWidth";
            this.m_NumericUpDownWidth.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxPlace
            // 
            this.m_ComboBoxPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPlace, "m_ComboBoxPlace");
            this.m_ComboBoxPlace.Name = "m_ComboBoxPlace";
            this.m_ComboBoxPlace.SelectedIndexChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelWidth
            // 
            resources.ApplyResources(this.m_LabelWidth, "m_LabelWidth");
            this.m_LabelWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelWidth.Name = "m_LabelWidth";
            // 
            // m_NumericUpDownStripeSpace
            // 
            this.m_NumericUpDownStripeSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownStripeSpace, "m_NumericUpDownStripeSpace");
            this.m_NumericUpDownStripeSpace.Name = "m_NumericUpDownStripeSpace";
            this.m_NumericUpDownStripeSpace.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownY.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownStripeWidth
            // 
            this.m_NumericUpDownStripeWidth.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownStripeWidth, "m_NumericUpDownStripeWidth");
            this.m_NumericUpDownStripeWidth.Name = "m_NumericUpDownStripeWidth";
            this.m_NumericUpDownStripeWidth.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownHeight.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelStripeWidth
            // 
            resources.ApplyResources(this.m_LabelStripeWidth, "m_LabelStripeWidth");
            this.m_LabelStripeWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeWidth.Name = "m_LabelStripeWidth";
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
            // m_LabelStripeSpace
            // 
            resources.ApplyResources(this.m_LabelStripeSpace, "m_LabelStripeSpace");
            this.m_LabelStripeSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripeSpace.Name = "m_LabelStripeSpace";
            // 
            // m_NumericUpDownMargin
            // 
            this.m_NumericUpDownMargin.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownMargin, "m_NumericUpDownMargin");
            this.m_NumericUpDownMargin.Name = "m_NumericUpDownMargin";
            this.m_NumericUpDownMargin.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelStripePos
            // 
            resources.ApplyResources(this.m_LabelStripePos, "m_LabelStripePos");
            this.m_LabelStripePos.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStripePos.Name = "m_LabelStripePos";
            // 
            // m_CheckBoxYContinue
            // 
            resources.ApplyResources(this.m_CheckBoxYContinue, "m_CheckBoxYContinue");
            this.m_CheckBoxYContinue.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxYContinue.Name = "m_CheckBoxYContinue";
            this.m_CheckBoxYContinue.UseVisualStyleBackColor = false;
            this.m_CheckBoxYContinue.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxMixedType
            // 
            resources.ApplyResources(this.m_CheckBoxMixedType, "m_CheckBoxMixedType");
            this.m_CheckBoxMixedType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxMixedType.Name = "m_CheckBoxMixedType";
            this.m_CheckBoxMixedType.UseVisualStyleBackColor = false;
            this.m_CheckBoxMixedType.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelStepTime
            // 
            resources.ApplyResources(this.m_LabelStepTime, "m_LabelStepTime");
            this.m_LabelStepTime.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStepTime.Name = "m_LabelStepTime";
            // 
            // m_CheckBoxHeightWithImageType
            // 
            resources.ApplyResources(this.m_CheckBoxHeightWithImageType, "m_CheckBoxHeightWithImageType");
            this.m_CheckBoxHeightWithImageType.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxHeightWithImageType.Name = "m_CheckBoxHeightWithImageType";
            this.m_CheckBoxHeightWithImageType.UseVisualStyleBackColor = false;
            this.m_CheckBoxHeightWithImageType.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownStepTime.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // label_FeatherType
            // 
            resources.ApplyResources(this.label_FeatherType, "label_FeatherType");
            this.label_FeatherType.BackColor = System.Drawing.Color.Transparent;
            this.label_FeatherType.Name = "label_FeatherType";
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
            // m_ComboBoxFeatherType
            // 
            this.m_ComboBoxFeatherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxFeatherType, "m_ComboBoxFeatherType");
            this.m_ComboBoxFeatherType.Name = "m_ComboBoxFeatherType";
            this.m_ComboBoxFeatherType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxFeatherType_SelectedIndexChanged);
            // 
            // m_LabelSprayTimes
            // 
            resources.ApplyResources(this.m_LabelSprayTimes, "m_LabelSprayTimes");
            this.m_LabelSprayTimes.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelSprayTimes.Name = "m_LabelSprayTimes";
            // 
            // m_NumericUpDownAutoSpray
            // 
            resources.ApplyResources(this.m_NumericUpDownAutoSpray, "m_NumericUpDownAutoSpray");
            this.m_NumericUpDownAutoSpray.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownAutoSpray.Name = "m_NumericUpDownAutoSpray";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownAutoSpray, resources.GetString("m_NumericUpDownAutoSpray.ToolTip"));
            this.m_NumericUpDownAutoSpray.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownSprayCycle.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_CheckBoxIdleSpray.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownZSpace
            // 
            this.m_NumericUpDownZSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownZSpace, "m_NumericUpDownZSpace");
            this.m_NumericUpDownZSpace.Name = "m_NumericUpDownZSpace";
            this.m_NumericUpDownZSpace.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_Label1
            // 
            resources.ApplyResources(this.m_Label1, "m_Label1");
            this.m_Label1.BackColor = System.Drawing.Color.Transparent;
            this.m_Label1.Name = "m_Label1";
            // 
            // m_CheckBoxKillBidir
            // 
            resources.ApplyResources(this.m_CheckBoxKillBidir, "m_CheckBoxKillBidir");
            this.m_CheckBoxKillBidir.Name = "m_CheckBoxKillBidir";
            this.m_CheckBoxKillBidir.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxAutoYCalibrate
            // 
            resources.ApplyResources(this.m_CheckBoxAutoYCalibrate, "m_CheckBoxAutoYCalibrate");
            this.m_CheckBoxAutoYCalibrate.Name = "m_CheckBoxAutoYCalibrate";
            this.m_CheckBoxAutoYCalibrate.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownPTASpraying.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownAutoClean
            // 
            resources.ApplyResources(this.m_NumericUpDownAutoClean, "m_NumericUpDownAutoClean");
            this.m_NumericUpDownAutoClean.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownAutoClean.Name = "m_NumericUpDownAutoClean";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownAutoClean, resources.GetString("m_NumericUpDownAutoClean.ToolTip"));
            this.m_NumericUpDownAutoClean.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownCleanTimes
            // 
            resources.ApplyResources(this.m_NumericUpDownCleanTimes, "m_NumericUpDownCleanTimes");
            this.m_NumericUpDownCleanTimes.Name = "m_NumericUpDownCleanTimes";
            this.m_ToolTip.SetToolTip(this.m_NumericUpDownCleanTimes, resources.GetString("m_NumericUpDownCleanTimes.ToolTip"));
            this.m_NumericUpDownCleanTimes.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
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
            this.m_NumericUpDownPTACleaning.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_LabelAutoClean
            // 
            resources.ApplyResources(this.m_LabelAutoClean, "m_LabelAutoClean");
            this.m_LabelAutoClean.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoClean.Name = "m_LabelAutoClean";
            // 
            // m_LabelCleanTimes
            // 
            resources.ApplyResources(this.m_LabelCleanTimes, "m_LabelCleanTimes");
            this.m_LabelCleanTimes.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCleanTimes.Name = "m_LabelCleanTimes";
            // 
            // m_labelPauseTimeAfterCleaning
            // 
            resources.ApplyResources(this.m_labelPauseTimeAfterCleaning, "m_labelPauseTimeAfterCleaning");
            this.m_labelPauseTimeAfterCleaning.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPauseTimeAfterCleaning.Name = "m_labelPauseTimeAfterCleaning";
            // 
            // m_NumericUpDownThickness
            // 
            this.m_NumericUpDownThickness.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownThickness, "m_NumericUpDownThickness");
            this.m_NumericUpDownThickness.Name = "m_NumericUpDownThickness";
            this.m_NumericUpDownThickness.ValueChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_GroupBoxZ
            // 
            this.m_GroupBoxZ.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxZ, "m_GroupBoxZ");
            this.m_GroupBoxZ.Controls.Add(this.m_NumericUpDownZSpace);
            this.m_GroupBoxZ.Controls.Add(this.m_NumericUpDownThickness);
            this.m_GroupBoxZ.Controls.Add(this.m_Label1);
            this.m_GroupBoxZ.Controls.Add(this.m_LabelZSpace);
            this.m_GroupBoxZ.Controls.Add(this.m_ButtonZAuto);
            this.m_GroupBoxZ.Controls.Add(this.m_ButtonZManual);
            this.m_GroupBoxZ.Divider = true;
            this.m_GroupBoxZ.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxZ.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_GroupBoxZ.Name = "m_GroupBoxZ";
            // 
            // m_LabelZSpace
            // 
            resources.ApplyResources(this.m_LabelZSpace, "m_LabelZSpace");
            this.m_LabelZSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelZSpace.Name = "m_LabelZSpace";
            // 
            // m_ButtonZAuto
            // 
            resources.ApplyResources(this.m_ButtonZAuto, "m_ButtonZAuto");
            this.m_ButtonZAuto.Name = "m_ButtonZAuto";
            this.m_ButtonZAuto.Click += new System.EventHandler(this.m_ButtonZAuto_Click);
            // 
            // m_ButtonZManual
            // 
            resources.ApplyResources(this.m_ButtonZManual, "m_ButtonZManual");
            this.m_ButtonZManual.Name = "m_ButtonZManual";
            this.m_ButtonZManual.Click += new System.EventHandler(this.m_ButtonZManual_Click);
            // 
            // m_GroupBoxWhiteInk
            // 
            this.m_GroupBoxWhiteInk.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxWhiteInk, "m_GroupBoxWhiteInk");
            this.m_GroupBoxWhiteInk.Controls.Add(this.labelWhiteInk);
            this.m_GroupBoxWhiteInk.Controls.Add(this.m_ComboBoxWhiteInk);
            this.m_GroupBoxWhiteInk.Divider = true;
            this.m_GroupBoxWhiteInk.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxWhiteInk.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_GroupBoxWhiteInk.Name = "m_GroupBoxWhiteInk";
            // 
            // labelWhiteInk
            // 
            resources.ApplyResources(this.labelWhiteInk, "labelWhiteInk");
            this.labelWhiteInk.BackColor = System.Drawing.Color.Transparent;
            this.labelWhiteInk.Name = "labelWhiteInk";
            // 
            // m_ComboBoxWhiteInk
            // 
            this.m_ComboBoxWhiteInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxWhiteInk, "m_ComboBoxWhiteInk");
            this.m_ComboBoxWhiteInk.Name = "m_ComboBoxWhiteInk";
            this.m_ComboBoxWhiteInk.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxWhiteInk_SelectedIndexChanged);
            // 
            // m_GroupBoxMedia
            // 
            this.m_GroupBoxMedia.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxMedia, "m_GroupBoxMedia");
            this.m_GroupBoxMedia.Name = "m_GroupBoxMedia";
            // 
            // m_GroupBoxInkStripe
            // 
            this.m_GroupBoxInkStripe.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxInkStripe, "m_GroupBoxInkStripe");
            this.m_GroupBoxInkStripe.Name = "m_GroupBoxInkStripe";
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
            this.m_CheckBoxSprayBeforePrint.CheckedChanged += new System.EventHandler(this.BS_m_CheckBox_CheckedChanged);
            // 
            // m_GroupBoxClean
            // 
            this.m_GroupBoxClean.AllowDrop = true;
            resources.ApplyResources(this.m_GroupBoxClean, "m_GroupBoxClean");
            this.m_GroupBoxClean.Controls.Add(this.m_LabelAutoSpray);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownSprayTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownPTACleaning);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelSprayTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_labelPauseTimeAfterCleaning);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownAutoSpray);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownAutoClean);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownSprayCycle);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownCleanTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelSprayCycle);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelCleanTimes);
            this.m_GroupBoxClean.Controls.Add(this.m_CheckBoxIdleSpray);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelAutoClean);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownPTASpraying);
            this.m_GroupBoxClean.Controls.Add(this.m_CheckBoxSprayBeforePrint);
            this.m_GroupBoxClean.Controls.Add(this.m_labelPrintPrespraytime);
            this.m_GroupBoxClean.Divider = true;
            this.m_GroupBoxClean.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.m_GroupBoxClean.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_GroupBoxClean.Name = "m_GroupBoxClean";
            // 
            // grouperPreferenceSetting
            // 
            this.grouperPreferenceSetting.AllowDrop = true;
            resources.ApplyResources(this.grouperPreferenceSetting, "grouperPreferenceSetting");
            this.grouperPreferenceSetting.Controls.Add(this.m_ComboBoxUnit);
            this.grouperPreferenceSetting.Controls.Add(this.m_ComboBoxLang);
            this.grouperPreferenceSetting.Controls.Add(this.m_labelLang);
            this.grouperPreferenceSetting.Controls.Add(this.m_labelUnit);
            this.grouperPreferenceSetting.Divider = true;
            this.grouperPreferenceSetting.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.grouperPreferenceSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.grouperPreferenceSetting.Name = "grouperPreferenceSetting";
            // 
            // m_ComboBoxUnit
            // 
            this.m_ComboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxUnit, "m_ComboBoxUnit");
            this.m_ComboBoxUnit.Name = "m_ComboBoxUnit";
            this.m_ComboBoxUnit.SelectedIndexChanged += new System.EventHandler(this.PS_m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxLang
            // 
            this.m_ComboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxLang, "m_ComboBoxLang");
            this.m_ComboBoxLang.Name = "m_ComboBoxLang";
            this.m_ComboBoxLang.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxLang_SelectedIndexChanged);
            // 
            // m_labelLang
            // 
            resources.ApplyResources(this.m_labelLang, "m_labelLang");
            this.m_labelLang.BackColor = System.Drawing.Color.Transparent;
            this.m_labelLang.Name = "m_labelLang";
            // 
            // m_labelUnit
            // 
            resources.ApplyResources(this.m_labelUnit, "m_labelUnit");
            this.m_labelUnit.BackColor = System.Drawing.Color.Transparent;
            this.m_labelUnit.Name = "m_labelUnit";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_GroupBoxPrintSetting);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_CheckBoxAutoYCalibrate);
            this.panel2.Controls.Add(this.m_CheckBoxKillBidir);
            this.panel2.Controls.Add(this.m_GroupBoxWhiteInk);
            this.panel2.Controls.Add(this.m_GroupBoxZ);
            this.panel2.Controls.Add(this.m_GroupBoxInkStripe);
            this.panel2.Controls.Add(this.m_GroupBoxClean);
            this.panel2.Controls.Add(this.m_GroupBoxMedia);
            this.panel2.Controls.Add(this.grouperPreferenceSetting);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelBottomButtons);
            this.panel3.Controls.Add(this.m_GroupBoxTemperature);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            this.panel3.ClientSizeChanged += new System.EventHandler(this.panel3_ClientSizeChanged);
            // 
            // CombinedSettings
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "CombinedSettings";
            this.m_GroupBoxTemperature.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTempAndVol)).EndInit();
            this.panelBottomButtons.ResumeLayout(false);
            this.panelBottomButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeather)).EndInit();
            this.m_GroupBoxPrintSetting.ResumeLayout(false);
            this.m_GroupBoxPrintSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NmericUpDownWaveLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFeatherPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoSpray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSprayCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTASpraying)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCleanTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownPTACleaning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).EndInit();
            this.m_GroupBoxZ.ResumeLayout(false);
            this.m_GroupBoxZ.PerformLayout();
            this.m_GroupBoxWhiteInk.ResumeLayout(false);
            this.m_GroupBoxWhiteInk.PerformLayout();
            this.m_GroupBoxClean.ResumeLayout(false);
            this.m_GroupBoxClean.PerformLayout();
            this.grouperPreferenceSetting.ResumeLayout(false);
            this.grouperPreferenceSetting.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        public void SetDefaultVisible(bool bVisible)
        {
            m_ButtonDefault.Visible = bVisible;
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_rsPrinterPropery = sp;
            BS_OnPrinterPropertyChange(sp);
            PS_OnPrinterPropertyChange(sp);
            SBoardInfo sBoardInfo = new SBoardInfo();

            m_bSpectra = SPrinterProperty.IsSpectra(sp.ePrinterHead);
            m_bKonic512 = SPrinterProperty.IsKonica512(sp.ePrinterHead);
            m_bXaar382 = (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl);
            m_bPolaris = SPrinterProperty.IsPolaris(sp.ePrinterHead);
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                m_bExcept = (sBoardInfo.m_nBoardManufatureID == 0xB || sBoardInfo.m_nBoardManufatureID == 0x8b);
            }
            if (m_bKonic512)
                Voltage_LR_Num = 6;
            else
                Voltage_LR_Num = 0;
            //if(CheckComponentChange(sp))
            {
                //m_ColorNum = sp.nColorNum;
                //m_GroupNum = sp.nHeadNum/sp.nColorNum;
                m_HeadNum = sp.nHeadNum;
                m_TempNum = sp.nHeadNum;
                m_HeadVoltageNum = sp.nHeadNum;
                if (m_bKonic512)
                {
                    m_HeadNum /= 2;//sp.nHeadNumPerColor;
                    m_TempNum = m_HeadNum;
                }
                else if (m_bPolaris)
                {
                    if (!m_bExcept)
                    {
                        m_HeadNum = m_HeadNum / sp.nHeadNumPerColor;// * VOLCOUNTPERPOLARISHEAD;
                        m_HeadVoltageNum = m_HeadNum * VOLCOUNTPERPOLARISHEAD;
                        m_TempNum = m_HeadVoltageNum;
                    }
                    else
                    {
                        m_HeadNum = sp.nHeadNum / 4;
                        if (m_rsPrinterPropery.nOneHeadDivider == 2)//是否为一头两色
                        {
                            if (m_HeadNum < sp.nColorNum)
                                m_HeadNum = sp.nColorNum;
                        }
#if GONGZENG_DOUBLE
						m_HeadVoltageNum = m_HeadNum*2;
#else
                        m_HeadVoltageNum = m_HeadNum;
#endif
                    }
                }
                byte nMinHead = 0xff;
                for (int i = 0; i < m_HeadNum; i++)
                {
                    if (sp.pElectricMap[i] < nMinHead)
                    {
                        nMinHead = sp.pElectricMap[i];
                    }
                }
                m_StartHeadIndex = 0;
                m_pMap = (byte[])sp.pElectricMap.Clone();
                CoreInterface.GetHeadMap(m_pMap, m_pMap.Length);
                int nmap_input = 1;
                if (sp.ePrinterHead == PrinterHeadEnum.Spectra_S_128)
                {
                    nmap_input = 2;
                }
                for (int i = 0; i < m_HeadVoltageNum; i++)
                {
                    m_pMap[i] = (byte)i;
                }

                m_bSupportHeadHeat = sp.bSupportHeadHeat;
                if (m_bSpectra)
                {
                    m_bSupportHeadHeat = true;
                    if (m_bPolaris)
                    {
                        if (!m_bExcept)
                            this.WhichRowVisble = new bool[7] { m_bSupportHeadHeat, true, true, true, true, true, true };
                        else
                            this.WhichRowVisble = new bool[7] { false, false, false, false, false, false, true };
                    }
                    else
                        this.WhichRowVisble = new bool[7] { m_bSupportHeadHeat, true, false, true, true, true, true };
                }
                else if (m_bKonic512)
                {
                    this.WhichRowVisble = new bool[7] { m_bSupportHeadHeat, true, false, true, true, true, false };
                }
                else if (m_bXaar382)
                {
                    this.WhichRowVisble = new bool[7] { false, true, false, true, true, false, false };
                }
                else
                {
                    this.WhichRowVisble = new bool[7] { m_bSupportHeadHeat, true, false, true, true, true, false };
                }

                if (m_bXaar382)
                {
                    ColumnVoltageAdjust.HeaderText = "Vtrim";
                    ColumnCurrentVoltage.HeaderText = "PWM";
                }

                Column_SetTemp.Visible = this.WhichRowVisble[0];
                ColumnNozzleTemp.Visible = this.WhichRowVisble[1];
                ColumnHeatingTemp.Visible = this.WhichRowVisble[2];
                ColumnVoltageAdjust.Visible = this.WhichRowVisble[3];
                ColumnCurrentVoltage.Visible = this.WhichRowVisble[4];
                ColumnBaseVoltage.Visible = this.WhichRowVisble[5];
                ColumnPulseWidth.Visible = this.WhichRowVisble[6];

                CreatSourceTable();
                this.OnRealTimeChange();
                this.isT_VDirty = this.isDirty = false;
            }
        }
        private void CreatSourceTable()
        {
            DataGridViewCellStyle Readonly = new DataGridViewCellStyle(dataGridViewTempAndVol.DefaultCellStyle);
            Readonly.BackColor = SystemColors.Control;
            this.ColumnNozzleTemp.DefaultCellStyle = Readonly;//1
            this.ColumnHeatingTemp.DefaultCellStyle = Readonly;//2
            this.ColumnCurrentVoltage.DefaultCellStyle = Readonly;//4
            foreach (DataGridViewColumn col in dataGridViewTempAndVol.Columns)
            {
                col.ValueType = typeof(float);
                col.FillWeight = 100;
                col.MinimumWidth = 30;
            }
            this.dataGridViewTempAndVol.Rows.Clear();
            //bool dis2value = m_bKonic512 || (m_bPolaris && !m_bExcept);
            int rowcount = m_HeadNum;
            if (m_bKonic512 || (m_bPolaris && !m_bExcept))
            {
                rowcount = m_HeadNum * 2;
            }
            else
            {
                rowcount = m_HeadNum;
            }
            this.dataGridViewTempAndVol.Rows.Add(rowcount);


            for (int j = 0; j < rowcount; j++)
            {
                for (int i = 3; i < dataGridViewTempAndVol.Columns.Count; i++)
                {
                    if (i == ColumnPulseWidth.Index)
                        this.dataGridViewTempAndVol[i, j].Value = 6.0f;
                    else
                        this.dataGridViewTempAndVol[i, j].Value = 0;
                }
            }
            if (m_bKonic512)
            {
                for (int i = 1; i < this.dataGridViewTempAndVol.RowCount; i += 2)
                {
                    this.dataGridViewTempAndVol[this.Column_SetTemp.Index, i].ReadOnly = true;
                    this.dataGridViewTempAndVol[this.ColumnPulseWidth.Index, i].ReadOnly = true;
                }
            }
            //for (int i = 0; i < this.dataGridViewTempAndVol.RowCount; i += 4)
            //{
            //    DataGridViewCellStyle drs = new DataGridViewCellStyle();
            //    drs.BackColor = Color.Black;
            //    drs.ForeColor = Color.White;
            //    this.dataGridViewTempAndVol.Rows[i].DefaultCellStyle = drs;
            //    drs = new DataGridViewCellStyle();
            //    drs.BackColor = Color.Cyan;
            //    this.dataGridViewTempAndVol.Rows[i + 1].DefaultCellStyle = drs;
            //    drs = new DataGridViewCellStyle();
            //    drs.BackColor = Color.Magenta;
            //    this.dataGridViewTempAndVol.Rows[i + 2].DefaultCellStyle = drs;
            //    drs = new DataGridViewCellStyle();
            //    drs.BackColor = Color.Yellow;
            //    this.dataGridViewTempAndVol.Rows[i + 3].DefaultCellStyle = drs;
            //}

            //this.dataGridViewTempAndVol.Height = this.dataGridViewTempAndVol.RowTemplate.Height * (rowcount) + this.dataGridViewTempAndVol.ColumnHeadersHeight;
            //m_CheckBoxAutoRefresh.Top = m_ButtonDefault.Top = m_ButtonToBoard.Top = m_ButtonRefresh.Top = dataGridViewTempAndVol.Bottom + 5;
            //m_GroupBoxTemperature.Height = this.dataGridViewTempAndVol.Bottom + panelBottomButtons.Height + 5;
            //int w = this.dataGridViewTempAndVol.RowTemplate.Height * (rowcount) + this.dataGridViewTempAndVol.ColumnHeadersHeight;
            //m_GroupBoxTemperature.Height = this.dataGridViewTempAndVol.Bottom + panelBottomButtons.Height + 5 ;
            CalcT_V_MaxHeight();
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            BS_OnPrinterSettingChange(ss);
            this.isDirty = false;
        }
        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            BS_OnGetPrinterSetting(ref ss);
        }

        private void OnGetRealTimeFromUI(ref SRealTimeCurrentInfo sRT)
        {
            int rowcount = this.dataGridViewTempAndVol.RowCount;
            for (int i = 0; i < m_TempNum; i++)
            {
                int nMap = m_pMap[i];
                if (m_bSupportHeadHeat)
                {
                    sRT.cTemperatureSet[nMap] = float.Parse(this.dataGridViewTempAndVol[this.Column_SetTemp.Index, i * rowcount / m_TempNum].Value.ToString());//1
                }
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cVoltage[nMap] = float.Parse(this.dataGridViewTempAndVol[this.ColumnVoltageAdjust.Index, i * rowcount / m_HeadVoltageNum].Value.ToString());//3
                sRT.cVoltageBase[nMap] = float.Parse(this.dataGridViewTempAndVol[this.ColumnBaseVoltage.Index, i * rowcount / m_HeadVoltageNum].Value.ToString());//5
                if (ColumnPulseWidth.Visible)
                    sRT.cPulseWidth[nMap] = float.Parse(this.dataGridViewTempAndVol[this.ColumnPulseWidth.Index, i * rowcount / m_HeadVoltageNum].Value.ToString());//6
            }
        }
        public void OnRenewRealTime(SRealTimeCurrentInfo info)
        {
            int rowcount = this.dataGridViewTempAndVol.RowCount;
            for (int i = 0; i < m_TempNum; i++)
            {
                int nMap = m_pMap[i];
                if (m_bKonic512 && m_rsPrinterPropery.nOneHeadDivider == 2)//是否为一头两色
                    nMap = i * rowcount / m_TempNum;
                this.dataGridViewTempAndVol[this.ColumnNozzleTemp.Index, i * rowcount / m_TempNum].Value = info.cTemperatureCur[nMap];//0
                if (m_bSupportHeadHeat)
                {
                    this.dataGridViewTempAndVol[this.Column_SetTemp.Index, i * rowcount / m_TempNum].Value = info.cTemperatureSet[nMap];//1
                    this.dataGridViewTempAndVol[this.ColumnHeatingTemp.Index, i * rowcount / m_TempNum].Value = info.cTemperatureCur2[nMap];//2
                }
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                Decimal cur = new Decimal(info.cPulseWidth[nMap]);
                this.dataGridViewTempAndVol[this.ColumnVoltageAdjust.Index, i * rowcount / m_HeadVoltageNum].Value = info.cVoltage[nMap];//3
                this.dataGridViewTempAndVol[this.ColumnCurrentVoltage.Index, i * rowcount / m_HeadVoltageNum].Value = info.cVoltageCurrent[nMap];//4
                this.dataGridViewTempAndVol[this.ColumnBaseVoltage.Index, i * rowcount / m_HeadVoltageNum].Value = info.cVoltageBase[nMap];//5
                if (cur < 6)
                    this.dataGridViewTempAndVol[this.ColumnPulseWidth.Index, i * rowcount / m_HeadVoltageNum].Value = MINPULSEWIDTH;
                else if (cur > 12)
                    this.dataGridViewTempAndVol[this.ColumnPulseWidth.Index, i * rowcount / m_HeadVoltageNum].Value = MAXPULSEWIDTH;
                else
                    this.dataGridViewTempAndVol[this.ColumnPulseWidth.Index, i * rowcount / m_HeadVoltageNum].Value = info.cPulseWidth[nMap];//6
            }
            this.isDirty = false;

        }

        private void OnGetRealTimeFromUI_382(ref SRealTimeCurrentInfo_382 sRT)
        {
            int rowcount = this.dataGridViewTempAndVol.RowCount;
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                sRT.cVtrim[nMap] = float.Parse(this.dataGridViewTempAndVol[this.ColumnNozzleTemp.Index, i * rowcount / m_HeadVoltageNum].Value.ToString());//1
            }
        }
        public void OnRenewRealTime_382(SRealTimeCurrentInfo_382 info)
        {
            int rowcount = this.dataGridViewTempAndVol.RowCount;
            for (int i = 0; i < m_HeadNum; i++)
            {
                int nMap = m_pMap[i];
                this.dataGridViewTempAndVol[this.Column_SetTemp.Index, i * rowcount / m_HeadNum].Value = info.cTemperature[nMap];//0
            }
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                this.dataGridViewTempAndVol[this.ColumnVoltageAdjust.Index, i * rowcount / m_HeadVoltageNum].Value = info.cPWM[nMap];//3
                this.dataGridViewTempAndVol[this.ColumnBaseVoltage.Index, i * rowcount / m_HeadVoltageNum].Value = info.cVtrim[nMap];//5
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
                else if (m_curStatus != JetStatusEnum.PowerOff)
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
                else if (m_curStatus != JetStatusEnum.PowerOff)
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
                }
            }
        }
        private bool CheckComponentChange(SPrinterProperty sp)
        {
            if (m_HeadNum != sp.nHeadNum / sp.nHeadNumPerColor)
                return true;
            return false;
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
        private void CalculateHorNum(int num, int start_x, int end_x, ref int width, out int space)
        {
            const int m_HorGap = 4;
            const int m_Margin = 4;

            space = m_HorGap + width;
            if (num > 1)
                space = (end_x - start_x - m_Margin - width) / (num - 1);
            if ((width + m_HorGap) > space)
            {
                width = (end_x - start_x - m_HorGap * (num - 1) - m_HorGap) / num;
                space = width + m_HorGap;
            }
        }

        private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
        {
#if true
            NumericUpDown textBox = (NumericUpDown)sender;
            bool isValidNumber = true;
            try
            {
                float val = float.Parse(textBox.Text);
                textBox.Value = new Decimal(val);
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
                isValidNumber = false;
            }

            if (!isValidNumber)
            {
                SystemCall.Beep(200, 50);
                textBox.Focus();
                textBox.Select(0, textBox.Text.Length);
            }
#endif
        }
        private void UpdateToLocal()
        {
        }

        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                m_CheckBoxControl_Leave(sender, e);
            }
        }

        private void m_ButtonRefresh_Click(object sender, System.EventArgs e)
        {
            OnRealTimeChange();
            this.isT_VDirty = false;
        }

        private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
        {
            ApplyToBoard();
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
                    string vol = PubFunc.SystemConvertToXml(sRT, sRT.GetType()).Replace("><", ">\n<");
                    LogWriter.WriteLog(new string[] { "ApplyToBoard at " + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString() + ":", vol }, true);
                }
                else
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
            }
            else
            {
                SRealTimeCurrentInfo sRT = new SRealTimeCurrentInfo();
                sRT.cTemperatureSet = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cTemperatureCur = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cPulseWidth = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cVoltage = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cVoltageBase = new float[CoreConst.MAX_HEAD_NUM];
                sRT.cVoltageCurrent = new float[CoreConst.MAX_HEAD_NUM];
                OnGetRealTimeFromUI(ref sRT);
                uint Wmask = CalcRWMask(false);
                if (CoreInterface.SetRealTimeInfo(ref sRT, Wmask) != 0)
                {
                    string vol = PubFunc.SystemConvertToXml(sRT, sRT.GetType()).Replace("><", ">\n<");
                    LogWriter.WriteLog(new string[] { "ApplyToBoard at " + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString() + ":", vol }, true);
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
            this.isT_VDirty = false;
        }

        private void m_ButtonDefault_Click(object sender, System.EventArgs e)
        {
            if (m_bXaar382)
            {
                SRealTimeCurrentInfo_382 info = new SRealTimeCurrentInfo_382();
                info.cTemperature = new float[CoreConst.MAX_HEAD_NUM];
                info.cVtrim = new float[CoreConst.MAX_HEAD_NUM];
                info.cPWM = new float[CoreConst.MAX_HEAD_NUM];

                //DefaultRealTimeValue(ref info);
                OnRenewRealTime_382(info);
            }
            else
            {
                SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
                info.cTemperatureSet = new float[CoreConst.MAX_HEAD_NUM];
                info.cTemperatureCur = new float[CoreConst.MAX_HEAD_NUM];
                info.cPulseWidth = new float[CoreConst.MAX_HEAD_NUM];
                info.cVoltage = new float[CoreConst.MAX_HEAD_NUM];
                info.cVoltageBase = new float[CoreConst.MAX_HEAD_NUM];
                info.cVoltageCurrent = new float[CoreConst.MAX_HEAD_NUM];

                DefaultRealTimeValue(ref info);
                OnRenewRealTime(info);
            }
        }
        private void DefaultRealTimeValue(ref SRealTimeCurrentInfo info)
        {
            for (int i = 0; i < m_HeadVoltageNum; i++)
            {
                int nMap = m_pMap[i];
                info.cVoltageBase[nMap] = 15.0f;
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
                    }
                }
            }
            return ret;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///BaseSetting
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///
        private JetStatusEnum m_curStatus = JetStatusEnum.PowerOff;
        private UILengthUnit m_CurrentUnit = UILengthUnit.Inch;
        private const int M_AUTOINDENT = 56;

        public void SetPrinterStatusChanged(JetStatusEnum status)
        {
            m_curStatus = status;
            bool bEnabled = (status == JetStatusEnum.Ready);
            m_ButtonZAuto.Enabled = bEnabled;
            m_ButtonZManual.Enabled = bEnabled;

            bEnabled = (status == JetStatusEnum.Ready || status == JetStatusEnum.Spraying);
            m_ButtonMeasure.Enabled = bEnabled;

            bEnabled = status != JetStatusEnum.PowerOff;
            //this.m_GroupBoxTemperature.Enabled = bEnabled;
            this.Column_SetTemp.ReadOnly = !bEnabled;
            this.ColumnPulseWidth.ReadOnly = !bEnabled;
            this.ColumnVoltageAdjust.ReadOnly = !bEnabled;
            this.ColumnBaseVoltage.ReadOnly = !bEnabled;
            m_ButtonRefresh.Enabled = m_ButtonToBoard.Enabled = m_ButtonDefault.Enabled = m_CheckBoxAutoRefresh.Enabled = bEnabled;
            //this.panelBottomButtons.Enabled = bEnabled;

            this.isDirty = false;
        }
        private void ToolTipInit()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseSetting));

            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownAutoClean.ToolTip"), this.m_NumericUpDownAutoClean, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownCleanTimes.ToolTip"), this.m_NumericUpDownCleanTimes, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownAutoSpray.ToolTip"), this.m_NumericUpDownAutoSpray, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownSprayCycle.ToolTip"), this.m_NumericUpDownSprayCycle, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownStepTime.ToolTip"), this.m_NumericUpDownStepTime, this.m_ToolTip);

            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownStepTime.ToolTip"), this.m_NumericUpDownPTACleaning, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownStepTime.ToolTip"), this.m_NumericUpDownPTASpraying, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownFeatherPercent.ToolTip"), this.m_NumericUpDownFeatherPercent, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(resources.GetString("m_NmericUpDownWaveLen.ToolTip"), this.m_NmericUpDownWaveLen, this.m_ToolTip);
        }
        public void ClampWithMinMax(Decimal min, Decimal max, ref Decimal cur)
        {
            if (cur < min)
                cur = min;
            if (cur > max)
                cur = max;
        }
        public void BS_OnPrinterPropertyChange(SPrinterProperty sp)
        {
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

            m_ComboBoxPlace.Items.Clear();
            foreach (InkStrPosEnum place in Enum.GetValues(typeof(InkStrPosEnum)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(InkStrPosEnum), place);
                //if(place != InkStrPosEnum.None)
                m_ComboBoxPlace.Items.Add(cmode);
            }

            //m_ComboBoxWhiteInk.Items.Clear();
            //foreach (WhiteInkPrintMode mode in Enum.GetValues(typeof(WhiteInkPrintMode)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(WhiteInkPrintMode), mode);
            //    m_ComboBoxWhiteInk.Items.Add(cmode);
            //}
            
            m_ComboBoxFeatherType.Items.Clear();
            foreach (FeatherType place in Enum.GetValues(typeof(FeatherType)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), place);
                m_ComboBoxFeatherType.Items.Add(cmode);
            }

            //if (sp.bSupportWhiteInk || sp.bSupportWhiteInkYoffset)
            //    m_GroupBoxWhiteInk.Visible = true;
            //else
                m_GroupBoxWhiteInk.Visible = false;
            if (sp.bSupportZMotion)
            {
                m_GroupBoxZ.Visible = true;
            }
            else
                m_GroupBoxZ.Visible = false;
            //m_GroupBoxMedia.AutoSizeEx();
            //m_GroupBoxPrintSetting.AutoSizeEx();
            if (sp.nMediaType == 0)
            {
                m_LabelY.Visible = false;
                m_NumericUpDownY.Visible = false;
                m_LabelHeight.Visible = false;
                m_NumericUpDownHeight.Visible = false;
                //m_GroupBoxPrintSetting.Height -= 20;
                m_CheckBoxYContinue.Visible = false;
                //if (sp.bSupportPaperSensor == false)
                //{
                //    m_GroupBoxMedia.Height -= 100;
                //}
                //else
                //{
                //    m_GroupBoxMedia.Height -= M_AUTOINDENT;
                //    m_CheckBoxMeasureBeforePrint.Location = new Point(m_CheckBoxMeasureBeforePrint.Location.X, m_CheckBoxMeasureBeforePrint.Location.Y - M_AUTOINDENT);
                //    m_ButtonMeasure.Location = new Point(m_ButtonMeasure.Location.X, m_ButtonMeasure.Location.Y - M_AUTOINDENT);
                //}
            }
            else
            {
                m_LabelY.Visible = true;
                m_NumericUpDownY.Visible = true;
                m_LabelHeight.Visible = true;
                m_NumericUpDownHeight.Visible = true;

                m_CheckBoxYContinue.Visible = true;

                //if (sp.bSupportPaperSensor == false)
                //{
                //    m_GroupBoxMedia.Height -= M_AUTOINDENT;
                //}
            }
            if (sp.bSupportYEncoder)
            {
                m_CheckBoxAutoYCalibrate.Visible = true;
            }
            else
            {
                m_CheckBoxAutoYCalibrate.Visible = false;
            }
            if (sp.bSupportPaperSensor == true)
            {
                m_CheckBoxMeasureBeforePrint.Visible = true;
                m_ButtonMeasure.Visible = true;
            }
            else
            {
                m_CheckBoxMeasureBeforePrint.Visible = false;
                m_ButtonMeasure.Visible = false;
            }
            this.isDirty = false;
        }

        public void BS_OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_NumericUpDownStripeSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.sStripeSetting.fStripeOffset));
            m_NumericUpDownStripeWidth.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.sStripeSetting.fStripeWidth));
            m_NumericUpDownJobSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fJobSpace));
            m_NumericUpDownStepTime.Value = new Decimal(ss.sBaseSetting.fStepTime);
            m_NumericUpDownLeftEdge.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fLeftMargin));
            Decimal curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fPaperWidth));
            ClampWithMinMax(m_NumericUpDownWidth.Minimum, m_NumericUpDownWidth.Maximum, ref curvalue);
            m_NumericUpDownWidth.Value = curvalue;
            m_NumericUpDownY.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fTopMargin));
            m_CheckBoxMeasureBeforePrint.Checked = ss.sBaseSetting.bMeasureBeforePrint;

            curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fPaperHeight));
            ClampWithMinMax(m_NumericUpDownHeight.Minimum, m_NumericUpDownHeight.Maximum, ref curvalue);
            m_NumericUpDownHeight.Value = curvalue;
            m_NumericUpDownZSpace.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fZSpace));
            m_NumericUpDownThickness.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fPaperThick));
            m_NumericUpDownMargin.Value = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fMeasureMargin));

            m_NumericUpDownAutoClean.Value = ss.sCleanSetting.nCleanerPassInterval;
            m_NumericUpDownCleanTimes.Value = ss.sCleanSetting.nCleanerTimes;
            m_NumericUpDownAutoSpray.Value = ss.sCleanSetting.nSprayPassInterval;
            m_NumericUpDownSprayCycle.Value = ss.sCleanSetting.nSprayFireInterval;
            m_NumericUpDownSprayTimes.Value = ss.sCleanSetting.nSprayTimes;
            m_NumericUpDownPTASpraying.Value = new decimal(ss.sCleanSetting.nPauseTimeAfterSpraying / 1000f);
            m_NumericUpDownPTACleaning.Value = new decimal(ss.sCleanSetting.nPauseTimeAfterCleaning / 1000f);
            m_NumericUpDownFeather.Value = ss.sBaseSetting.nFeatherPercent;
            m_CheckBoxAutoYCalibrate.Checked = ss.sBaseSetting.bAutoYCalibration;
            m_CheckBoxKillBidir.Checked = (ss.nKillBiDirBanding != 0);
            byte type = ss.sBaseSetting.sStripeSetting.bNormalStripeType;
            m_CheckBoxNormalType.Checked = ((type & ((byte)EnumStripeType.Normal)) != 0);
            m_CheckBoxMixedType.Checked = ((type & ((byte)EnumStripeType.ColorMixed)) != 0);
            m_CheckBoxHeightWithImageType.Checked = ((type & ((byte)EnumStripeType.HeightWithImage)) != 0);
            m_CheckBoxIdleSpray.Checked = ss.sCleanSetting.bSprayWhileIdle;
            m_CheckBoxSprayBeforePrint.Checked = ss.sCleanSetting.bSprayBeforePrint;
            m_CheckBoxAutoJumpWhite.Checked = (!ss.sBaseSetting.bIgnorePrintWhiteX) && (!ss.sBaseSetting.bIgnorePrintWhiteY);
            m_CheckBoxAutoJumpWhite.Enabled = (m_ComboBoxWhiteInk.SelectedIndex == 0);
            m_CheckBoxYContinue.Checked = ss.sBaseSetting.bYPrintContinue;

            m_ComboBoxPlace.SelectedIndex = (int)ss.sBaseSetting.sStripeSetting.eStripePosition;
            //m_ComboBoxWhiteInk.SelectedIndex = (int)ss.sBaseSetting.eWhiteInkPrintMode;
            this.m_ComboBoxFeatherType.SelectedIndex = ss.sBaseSetting.nFeatherType;
            curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ss.sBaseSetting.fFeatherWavelength));
            ClampWithMinMax(m_NmericUpDownWaveLen.Minimum, m_NmericUpDownWaveLen.Maximum, ref curvalue);
            this.m_NmericUpDownWaveLen.Value = curvalue;
            this.m_NumericUpDownFeatherPercent.Value = ss.sBaseSetting.nAdvanceFeatherPercent;

            this.isDirty = false;
        }
        public void BS_OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            ss.sBaseSetting.sStripeSetting.fStripeOffset = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownStripeSpace.Value));
            ss.sBaseSetting.sStripeSetting.fStripeWidth = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownStripeWidth.Value));
            ss.sBaseSetting.fJobSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownJobSpace.Value));
            ss.sBaseSetting.fStepTime = Decimal.ToSingle(m_NumericUpDownStepTime.Value);
            ss.sBaseSetting.fLeftMargin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownLeftEdge.Value));
            ss.sBaseSetting.fPaperWidth = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownWidth.Value));
            ss.sBaseSetting.fTopMargin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownY.Value));
            ss.sBaseSetting.fPaperHeight = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownHeight.Value));
            ss.sBaseSetting.fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownZSpace.Value));
            ss.sBaseSetting.fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownThickness.Value));
            ss.sBaseSetting.fMeasureMargin = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownMargin.Value));

            ss.sCleanSetting.nCleanerPassInterval = Decimal.ToInt32(m_NumericUpDownAutoClean.Value);
            ss.sCleanSetting.nCleanerTimes = Decimal.ToInt32(m_NumericUpDownCleanTimes.Value);
            ss.sCleanSetting.nSprayPassInterval = Decimal.ToInt32(m_NumericUpDownAutoSpray.Value);
            ss.sCleanSetting.nSprayFireInterval = Decimal.ToInt32(m_NumericUpDownSprayCycle.Value);
            ss.sCleanSetting.nSprayTimes = Decimal.ToInt32(m_NumericUpDownSprayTimes.Value);
            ss.sCleanSetting.nPauseTimeAfterSpraying = Decimal.ToUInt16(m_NumericUpDownPTASpraying.Value * 1000);
            ss.sCleanSetting.nPauseTimeAfterCleaning = Decimal.ToUInt16(m_NumericUpDownPTACleaning.Value * 1000);
            ss.sBaseSetting.nFeatherPercent = Decimal.ToInt32(m_NumericUpDownFeather.Value);
            //ss.sMoveSetting.nXMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
            //ss.sMoveSetting.nYMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
            //ss.sMoveSetting.usMotorEncoder						=	Decimal.ToUInt16(m_NumericUpDownEncoder.Value);		

            ss.nKillBiDirBanding = m_CheckBoxKillBidir.Checked ? 1 : 0;
            byte NormalType = 0;
            if (m_CheckBoxNormalType.Checked)
            {
                NormalType |= (byte)EnumStripeType.Normal;
            }
            if (m_CheckBoxMixedType.Checked)
            {
                NormalType |= (byte)EnumStripeType.ColorMixed;
            }
            if (m_CheckBoxHeightWithImageType.Checked)
            {
                NormalType |= (byte)EnumStripeType.HeightWithImage;
            }


            ss.sBaseSetting.sStripeSetting.bNormalStripeType = NormalType;
            ss.sCleanSetting.bSprayWhileIdle = m_CheckBoxIdleSpray.Checked;
            ss.sCleanSetting.bSprayBeforePrint = m_CheckBoxSprayBeforePrint.Checked;
            ss.sBaseSetting.bIgnorePrintWhiteX =
            ss.sBaseSetting.bIgnorePrintWhiteY = !m_CheckBoxAutoJumpWhite.Checked;
            ss.sBaseSetting.bYPrintContinue = m_CheckBoxYContinue.Checked;
            ss.sBaseSetting.bAutoYCalibration = m_CheckBoxAutoYCalibrate.Checked;
            ss.sBaseSetting.bMeasureBeforePrint = m_CheckBoxMeasureBeforePrint.Checked;


            ss.sBaseSetting.sStripeSetting.eStripePosition = (InkStrPosEnum)m_ComboBoxPlace.SelectedIndex;
            //ss.sBaseSetting.eWhiteInkPrintMode = (WhiteInkPrintMode)m_ComboBoxWhiteInk.SelectedIndex;
            ss.sBaseSetting.nFeatherType = (byte)this.m_ComboBoxFeatherType.SelectedIndex;
            ss.sBaseSetting.fFeatherWavelength = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.m_NmericUpDownWaveLen.Value));
            ss.sBaseSetting.nAdvanceFeatherPercent = (byte)this.m_NumericUpDownFeatherPercent.Value;

        }

        public void OnPreferenceChange(UIPreference up)
        {
            PS_OnPreferenceChange(up);
            if (m_CurrentUnit != up.Unit)
            {
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
                this.isDirty = false;
            }
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownStripeSpace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownStripeWidth);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownJobSpace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownLeftEdge);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownWidth);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownHeight);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownZSpace);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NumericUpDownThickness);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, m_NmericUpDownWaveLen);

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
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NmericUpDownWaveLen, this.m_ToolTip);
            this.isDirty = false;
        }

        private void m_ButtonMeasure_Click(object sender, System.EventArgs e)
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 0);
        }

        private void m_ButtonZAuto_Click(object sender, System.EventArgs e)
        {
            float fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownZSpace.Value));
            float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownThickness.Value));
            CoreInterface.MoveZ(01, fZSpace, fPaperThick);
        }

        private void m_ButtonZManual_Click(object sender, System.EventArgs e)
        {
            float fZSpace = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownZSpace.Value));
            float fPaperThick = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(m_NumericUpDownThickness.Value));
            CoreInterface.MoveZ(02, fZSpace, fPaperThick);
        }

        private void m_ComboBoxWhiteInk_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (m_ComboBoxWhiteInk.SelectedIndex != 0)
            {
                m_CheckBoxAutoJumpWhite.Checked = false;
            }
            m_CheckBoxAutoJumpWhite.Enabled = (m_ComboBoxWhiteInk.SelectedIndex == 0);
            isDirty = true;
        }

        private void BS_m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///BaseSetting
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///PreferenceSetting
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///
        private ArrayList m_LangArrayList;
        private bool m_bInitFinished;

        public void PS_OnPrinterPropertyChange(SPrinterProperty sp)
        {
        }
        public void PS_OnPreferenceChange(UIPreference up)
        {
            m_bInitFinished = false;
            Initialize();
            //m_ComboBoxViewMode.SelectedIndex = (int)up.ViewModeIndex;
            m_ComboBoxUnit.SelectedIndex = (int)up.Unit;
            //m_CheckBoxBeepBeforePrint.Checked = up.BeepBeforePrint;
            //m_CheckBoxDelJobAfterPrint.Checked = up.DelJobAfterPrint;
            //m_TextBoxWorkFolder.Text = up.WorkingFolder;
            JobListColumnHeader[] myHeaderList = (JobListColumnHeader[])up.JobListHeaderList.Clone();

            JobListColumnHeader[] array = (JobListColumnHeader[])Enum.GetValues(typeof(JobListColumnHeader));
            int count = array.Length;
            //int count = m_CheckedListBoxJobListHeader.Items.Count;
            for (int i = 0; i < count; i++)
            {
                //string item = (string)m_CheckedListBoxJobListHeader.Items[i];
                bool found = false;
                for (int j = 0; j < myHeaderList.Length; j++)
                {
                    if (myHeaderList[j] == array[i])
                    {
                        found = true;
                        break;
                    }
                }
                //if (!found)
                //{
                //    m_CheckedListBoxJobListHeader.SetItemChecked(i, false);
                //}
                //else
                //{
                //    m_CheckedListBoxJobListHeader.SetItemChecked(i, true);
                //}
            }
            bool found1 = false;
            for (int i = 0; i < m_LangArrayList.Count; i++)
            {
                if (up.LangIndex == (int)m_LangArrayList[i])
                {
                    found1 = true;
                    m_ComboBoxLang.SelectedIndex = i;
                    break;
                }
            }
            if (!found1)
            {
                m_ComboBoxLang.SelectedIndex = 0;
            }
            m_bInitFinished = true;
            this.isDirty = false;
        }
        public void OnGetPreference(ref UIPreference up)
        {
            //up.ViewModeIndex = m_ComboBoxViewMode.SelectedIndex;
            up.Unit = (UILengthUnit)m_ComboBoxUnit.SelectedIndex;
            //up.BeepBeforePrint = m_CheckBoxBeepBeforePrint.Checked;
            //up.DelJobAfterPrint = m_CheckBoxDelJobAfterPrint.Checked;
            //up.WorkingFolder = m_TextBoxWorkFolder.Text;
            //ArrayList list = new ArrayList();
            //JobListColumnHeader[] array = (JobListColumnHeader[])Enum.GetValues(typeof(JobListColumnHeader));
            //int count = array.Length;
            ////int count = m_CheckedListBoxJobListHeader.Items.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    //string item = (string)m_CheckedListBoxJobListHeader.Items[i];
            //    if (m_CheckedListBoxJobListHeader.GetItemChecked(i))
            //    {
            //        //JobListColumnHeader head = (JobListColumnHeader)Enum.Parse(typeof(JobListColumnHeader),(string)m_CheckedListBoxJobListHeader.Items[i]);
            //        list.Add(array[i]);
            //    }
            //}
            //up.JobListHeaderList = new JobListColumnHeader[list.Count];
            //up.JobListHeaderList = (JobListColumnHeader[])list.ToArray(typeof(JobListColumnHeader));
            up.LangIndex = (int)m_LangArrayList[m_ComboBoxLang.SelectedIndex];
        }

        public void Initialize()
        {
            ArrayList langlist;
            UIPreference.InitializeLanguage(out langlist);
            m_LangArrayList = langlist;
            //m_ComboBoxLang
            m_ComboBoxLang.Items.Clear();
            for (int i = 0; i < langlist.Count; i++)
            {
                CultureInfo cInfo = new CultureInfo((int)langlist[i]);
                //m_ComboBoxLang.Items.Add(cInfo.DisplayName);
                if (Thread.CurrentThread.CurrentUICulture.LCID == 0x0409)
                    m_ComboBoxLang.Items.Add(cInfo.EnglishName);
                else
                    m_ComboBoxLang.Items.Add(cInfo.NativeName);
            }
            //m_ComboBoxPass.SelectedIndex = FoundMatchPass(passStr);

            //m_ComboBoxViewMode
            //m_ComboBoxViewMode.Items.Clear();
            //foreach (UIViewMode mode in Enum.GetValues(typeof(UIViewMode)))
            //{
            //    if (mode == UIViewMode.NotifyIcon) continue;
            //    string cmode = ResString.GetEnumDisplayName(typeof(UIViewMode), mode);
            //    m_ComboBoxViewMode.Items.Add(cmode);
            //}

            //m_ComboBoxUnit
            m_ComboBoxUnit.Items.Clear();
            foreach (UILengthUnit mode in Enum.GetValues(typeof(UILengthUnit)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(UILengthUnit), mode);
                m_ComboBoxUnit.Items.Add(cmode);
            }
            //m_CheckedListBoxJobListHeader
            //m_CheckedListBoxJobListHeader.Items.Clear();
            //foreach (JobListColumnHeader mode in Enum.GetValues(typeof(JobListColumnHeader)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader), mode);
            //    m_CheckedListBoxJobListHeader.Items.Add(cmode);
            //}
        }
        private void m_ComboBoxLang_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (m_bInitFinished)
            {
                string info = ResString.GetResString("Restart_Lang");
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isDirty = true;
            }
        }

        private void m_CheckedListBoxJobListHeader_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (e.Index == 0 && e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = CheckState.Checked;
            }
            this.isDirty = true;
        }

        //private void m_ButtonWorkFolder_Click(object sender, System.EventArgs e)
        //{
        //    FolderBrowserDialog dlg = new FolderBrowserDialog();
        //    dlg.SelectedPath = m_TextBoxWorkFolder.Text;

        //    if (dlg.ShowDialog(this) == DialogResult.OK)
        //    {
        //        m_TextBoxWorkFolder.Text = Path.GetDirectoryName(dlg.SelectedPath);
        //    }

        //}
        private void PS_m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }
        ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///PreferenceSetting
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SetNextCell(e.ColumnIndex, e.RowIndex);
        }
        private void SetNextCell(int columnIndex, int rowIndex)
        {
            //for (int i = rowIndex; i < dataGridView1.RowCount; i++)
            //{
            //    for (int j = columnIndex + 1; j < dataGridView1.ColumnCount; j++)
            //    {
            //        if (dataGridView1.Columns[j].Visible && !dataGridView1.Columns[j].ReadOnly)
            //        {
            //            this.dataGridView1[j, i].Selected = true;
            //            return;
            //        }
            //    }
            //}
            //for (int i = 0; i < rowIndex; i++)
            //{
            //    for (int j = 0; j < columnIndex; j++)
            //    {
            //        if (dataGridView1.Columns[j].Visible && !dataGridView1.Columns[j].ReadOnly)
            //        {
            //            this.dataGridView1[j, i].Selected = true;
            //            return;
            //        }
            //        else
            //            continue;
            //    }
            //}
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                e.RowBounds.Location.Y,
                                dataGridViewTempAndVol.RowHeadersWidth - 4,
                                e.RowBounds.Height);
                string rowheadertext = string.Empty;

                if (m_bKonic512 && m_rsPrinterPropery.nOneHeadDivider == 2)//是否为一头两色
                {
                    if (e.RowIndex % 2 == 0)
                    {
                        rowheadertext = (e.RowIndex / 2 + m_StartHeadIndex).ToString()
                            + "  (" + m_rsPrinterPropery.Get_ColorIndex(e.RowIndex + m_StartHeadIndex) + "_R)";

                    }
                    else
                    {
                        rowheadertext =
                             "  (" + m_rsPrinterPropery.Get_ColorIndex(e.RowIndex + m_StartHeadIndex) + "_L)";
                    }
                }
                else if (m_bKonic512 || (m_bPolaris && !m_bExcept))
                {
                    if (e.RowIndex % 2 == 0)
                    {
                        rowheadertext = (e.RowIndex / 2 + m_StartHeadIndex).ToString()
                            + "  (" + m_rsPrinterPropery.Get_ColorIndex(e.RowIndex / 2 + m_StartHeadIndex) + "_R)";

                    }
                    else
                    {
                        rowheadertext =
                             "  (" + m_rsPrinterPropery.Get_ColorIndex(e.RowIndex / 2 + m_StartHeadIndex) + "_L)";
                    }
                }
                else
                {
                    rowheadertext = (e.RowIndex + m_StartHeadIndex).ToString()
                            + "  (" + m_rsPrinterPropery.Get_ColorIndex(e.RowIndex + m_StartHeadIndex) + ")";
                }
                TextRenderer.DrawText(e.Graphics, rowheadertext,
                            dataGridViewTempAndVol.RowHeadersDefaultCellStyle.Font,
                            rectangle,
                            dataGridViewTempAndVol.RowHeadersDefaultCellStyle.ForeColor,
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, this.TopLevelControl.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            float value = 0;
            if (!float.TryParse(e.FormattedValue.ToString(), out value))
                return;
            float minvalue = 0;
            float maxvalue = 0;
            float cofficient_voltage = 1.0f;
            if (m_bSpectra)
                cofficient_voltage = 10.0f;
            switch (e.ColumnIndex)
            {
                case 0://this.Column_SetTemp.Index:
                    minvalue = 0;
                    maxvalue = 51;
                    break;
                case 1://this.ColumnNozzleTemp.Index:
                case 2://this.ColumnHeatingTemp.Index:
                    return;
                case 3://this.ColumnVoltageAdjust.Index:
                    if (!m_bXaar382)
                    {
                        minvalue = -2.0f * cofficient_voltage;
                        maxvalue = 23.5f * cofficient_voltage;
                    }
                    else
                    {
                        //Vtrim
                        minvalue = -128;
                        maxvalue = 127;
                    }
                    break;
                case 4://this.ColumnCurrentVoltage.Index:
                    return;
                case 5://this.ColumnBaseVoltage.Index:
                    minvalue = 0.0f * cofficient_voltage;
                    maxvalue = 25.6f * cofficient_voltage;
                    break;
                case 6://this.ColumnPulseWidth.Index:
                    minvalue = MINPULSEWIDTH;
                    maxvalue = MAXPULSEWIDTH;
                    break;
                default:
                    return;
            }
            if (value < minvalue)
                value = minvalue;
            if (value > maxvalue)
                value = maxvalue;
            this.dataGridViewTempAndVol[e.ColumnIndex, e.RowIndex].Value = value;
            this.dataGridViewTempAndVol.RefreshEdit();
        }

        private void dataGridViewTempAndVol_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.isT_VDirty = true;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (this.ApplyButtonClicked != null)
                this.ApplyButtonClicked(sender, e);
        }

        private void panel3_ClientSizeChanged(object sender, EventArgs e)
        {
            CalcT_V_MaxHeight();
        }

        private void CalcT_V_MaxHeight()
        {
            MaxHeightT_V = this.panel3.Size.Height;

            int gridH = this.dataGridViewTempAndVol.RowCount * this.dataGridViewTempAndVol.RowTemplate.Height + this.dataGridViewTempAndVol.ColumnHeadersHeight + this.dataGridViewTempAndVol.Top + 5;
            int tboxH = gridH + panelBottomButtons.Height  + this.m_GroupBoxTemperature.Top;

            if (tboxH > MaxHeightT_V)
            {
                this.m_GroupBoxTemperature.Height = MaxHeightT_V - panelBottomButtons.Height;
                //this.dataGridViewTempAndVol.Dock = DockStyle.Fill;
            }
            else
            {
                this.m_GroupBoxTemperature.Height = gridH;
                //this.dataGridViewTempAndVol.Dock = DockStyle.Top;
                //this.dataGridViewTempAndVol.Height = gridH;
            }
        }

        private void m_ComboBoxFeatherType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.m_NumericUpDownFeatherPercent.Visible = m_ComboBoxFeatherType.SelectedIndex == 3;
            this.m_NmericUpDownWaveLen.Visible = m_ComboBoxFeatherType.SelectedIndex == 2;
            isDirty = true;
        }
    }
}
