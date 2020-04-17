using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Preview;
using BYHXPrinterManager.GradientControls;
using PrinterStubC.Common;

namespace BYHXPrinterManager.JobListView
{
	public class EditJobForm : Form
	{
		const string m_PreviewFolder = "Preview";
		public const string CLIPPREVIEWFILEENDDING = "_clip.bmp";

		#region 变量

		public Image m_image;
		private BYHXPrinterManager.Preview.UserRect m_CliPRect;
		//		private bool m_PreviewChanged = true;
		private UIJob m_EditJob = new UIJob();
		private UIJob m_PreviewJob = new UIJob();
		private IPrinterChange m_iPrinterChange;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private bool isSetting = false;

		#endregion

		private System.Windows.Forms.Panel Panel_buttons;
		private System.Windows.Forms.RadioButton radioButton_Simplemode;
        private System.Windows.Forms.RadioButton radioButton_AdvancedMode;
		private System.Windows.Forms.ToolTip m_ToolTip;
        private Grouper groupBoxRegion;
        private Label label4;
        private NumericUpDown numericUpDownH;
        private Label label3;
        private NumericUpDown numericUpDownY;
        private Label label2;
        private NumericUpDown numericUpDownW;
        private Label label1;
        private NumericUpDown numericUpDownX;
        private Grouper groupBox1;
        private NumericUpDown m_NumericUpDownCopy;
        private CheckBox CheckBoxAlternatingprint;
        private Grouper groupBoxOthers;
        private CheckBox checkBoxVoltage;
        private CheckBox checkBoxFilepath;
        private CheckBox checkBoxDirection;
        private CheckBox checkBoxPassNum;
        private CheckBox checkBoxResolution;
        private CheckBox checkBoxJobSize;
        private Label label11;
        private ComboBox comboBoxNotePos;
        private Button button_Font;
        private TextBox textBoxFootNote;
        private Label label_noteDis;
        private NumericUpDown numericUpDown_noteMargin;
        private PrintingInfo printingInfo1;
        private Panel panel_AddsrcList;
        private TextBox textBox_srcPath;
        private Panel panel_clip;
        private PrintingPreview m_OldPrintingPreview;
        private Panel panel_clipBottom;
        private CheckBox checkBox_MultiCopy;
        private CheckBox checkBoxRegion;
        private CheckBox CheckBoxMirrorPrint;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private NumericUpDown numNegMaxGray;
        private Label label10;
        private Grouper groupBoxMultiCopy;
        private Panel panel2;
        private RadioButton rbtnY2;
        private RadioButton rbtnY1;
        private NumericUpDown numericUpDownYLen;
        private NumericUpDown numericUpDownYCnt;
        private Label label5;
        private Label label7;
        private NumericUpDown numericUpDownXDis;
        private NumericUpDown numericUpDownYDis;
        private Panel panel1;
        private Label label6;
        private NumericUpDown numericUpDownXLen;
        private NumericUpDown numericUpDownXCnt;
        private NumericUpDown numericUpDownXDis2;
        private Label label9;


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditJobForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style5 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style6 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style7 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style8 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style9 = new BYHXPrinterManager.Style();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel_clip = new System.Windows.Forms.Panel();
            this.m_OldPrintingPreview = new BYHXPrinterManager.Preview.PrintingPreview();
            this.panel_clipBottom = new System.Windows.Forms.Panel();
            this.checkBox_MultiCopy = new System.Windows.Forms.CheckBox();
            this.checkBoxRegion = new System.Windows.Forms.CheckBox();
            this.CheckBoxMirrorPrint = new System.Windows.Forms.CheckBox();
            this.panel_AddsrcList = new System.Windows.Forms.Panel();
            this.textBox_srcPath = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.printingInfo1 = new BYHXPrinterManager.Preview.PrintingInfo();
            this.groupBoxMultiCopy = new BYHXPrinterManager.GradientControls.Grouper();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnY2 = new System.Windows.Forms.RadioButton();
            this.rbtnY1 = new System.Windows.Forms.RadioButton();
            this.numericUpDownYLen = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownYCnt = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownXDis = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownYDis = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownXLen = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownXCnt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownXDis2 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numNegMaxGray = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxOthers = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxVoltage = new System.Windows.Forms.CheckBox();
            this.checkBoxFilepath = new System.Windows.Forms.CheckBox();
            this.checkBoxDirection = new System.Windows.Forms.CheckBox();
            this.checkBoxPassNum = new System.Windows.Forms.CheckBox();
            this.checkBoxResolution = new System.Windows.Forms.CheckBox();
            this.checkBoxJobSize = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxNotePos = new System.Windows.Forms.ComboBox();
            this.button_Font = new System.Windows.Forms.Button();
            this.textBoxFootNote = new System.Windows.Forms.TextBox();
            this.label_noteDis = new System.Windows.Forms.Label();
            this.numericUpDown_noteMargin = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_NumericUpDownCopy = new System.Windows.Forms.NumericUpDown();
            this.CheckBoxAlternatingprint = new System.Windows.Forms.CheckBox();
            this.groupBoxRegion = new BYHXPrinterManager.GradientControls.Grouper();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownW = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.Panel_buttons = new System.Windows.Forms.Panel();
            this.radioButton_AdvancedMode = new System.Windows.Forms.RadioButton();
            this.radioButton_Simplemode = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel_clip.SuspendLayout();
            this.panel_clipBottom.SuspendLayout();
            this.panel_AddsrcList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBoxMultiCopy.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYDis)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXDis2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNegMaxGray)).BeginInit();
            this.groupBoxOthers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_noteMargin)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCopy)).BeginInit();
            this.groupBoxRegion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            this.Panel_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel_clip);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel_AddsrcList);
            // 
            // panel_clip
            // 
            this.panel_clip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_clip.Controls.Add(this.m_OldPrintingPreview);
            this.panel_clip.Controls.Add(this.panel_clipBottom);
            resources.ApplyResources(this.panel_clip, "panel_clip");
            this.panel_clip.Name = "panel_clip";
            // 
            // m_OldPrintingPreview
            // 
            this.m_OldPrintingPreview.BackColor = System.Drawing.Color.White;
            this.m_OldPrintingPreview.Divider = false;
            resources.ApplyResources(this.m_OldPrintingPreview, "m_OldPrintingPreview");
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_OldPrintingPreview.GradientColors = style1;
            this.m_OldPrintingPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_OldPrintingPreview.Name = "m_OldPrintingPreview";
            this.m_OldPrintingPreview.Rotate = System.Drawing.RotateFlipType.RotateNoneFlipNone;
            // 
            // panel_clipBottom
            // 
            this.panel_clipBottom.Controls.Add(this.checkBox_MultiCopy);
            this.panel_clipBottom.Controls.Add(this.checkBoxRegion);
            this.panel_clipBottom.Controls.Add(this.CheckBoxMirrorPrint);
            resources.ApplyResources(this.panel_clipBottom, "panel_clipBottom");
            this.panel_clipBottom.Name = "panel_clipBottom";
            // 
            // checkBox_MultiCopy
            // 
            resources.ApplyResources(this.checkBox_MultiCopy, "checkBox_MultiCopy");
            this.checkBox_MultiCopy.Name = "checkBox_MultiCopy";
            this.checkBox_MultiCopy.CheckedChanged += new System.EventHandler(this.checkBox_MultiCopy_CheckedChanged);
            // 
            // checkBoxRegion
            // 
            resources.ApplyResources(this.checkBoxRegion, "checkBoxRegion");
            this.checkBoxRegion.Name = "checkBoxRegion";
            this.checkBoxRegion.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CheckBoxMirrorPrint
            // 
            resources.ApplyResources(this.CheckBoxMirrorPrint, "CheckBoxMirrorPrint");
            this.CheckBoxMirrorPrint.Name = "CheckBoxMirrorPrint";
            // 
            // panel_AddsrcList
            // 
            this.panel_AddsrcList.Controls.Add(this.textBox_srcPath);
            resources.ApplyResources(this.panel_AddsrcList, "panel_AddsrcList");
            this.panel_AddsrcList.Name = "panel_AddsrcList";
            // 
            // textBox_srcPath
            // 
            this.textBox_srcPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox_srcPath, "textBox_srcPath");
            this.textBox_srcPath.Name = "textBox_srcPath";
            this.textBox_srcPath.ReadOnly = true;
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.printingInfo1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBoxMultiCopy);
            this.splitContainer3.Panel2.Controls.Add(this.numNegMaxGray);
            this.splitContainer3.Panel2.Controls.Add(this.label10);
            this.splitContainer3.Panel2.Controls.Add(this.groupBoxOthers);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel2.Controls.Add(this.groupBoxRegion);
            // 
            // printingInfo1
            // 
            this.printingInfo1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.printingInfo1, "printingInfo1");
            this.printingInfo1.Name = "printingInfo1";
            // 
            // groupBoxMultiCopy
            // 
            this.groupBoxMultiCopy.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.groupBoxMultiCopy.BorderThickness = 1F;
            this.groupBoxMultiCopy.Controls.Add(this.panel2);
            this.groupBoxMultiCopy.Controls.Add(this.label5);
            this.groupBoxMultiCopy.Controls.Add(this.label7);
            this.groupBoxMultiCopy.Controls.Add(this.numericUpDownXDis);
            this.groupBoxMultiCopy.Controls.Add(this.numericUpDownYDis);
            this.groupBoxMultiCopy.Controls.Add(this.panel1);
            this.groupBoxMultiCopy.Controls.Add(this.numericUpDownXDis2);
            this.groupBoxMultiCopy.Controls.Add(this.label9);
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxMultiCopy.GradientColors = style2;
            this.groupBoxMultiCopy.GroupImage = null;
            resources.ApplyResources(this.groupBoxMultiCopy, "groupBoxMultiCopy");
            this.groupBoxMultiCopy.Name = "groupBoxMultiCopy";
            this.groupBoxMultiCopy.PaintGroupBox = false;
            this.groupBoxMultiCopy.RoundCorners = 5;
            this.groupBoxMultiCopy.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBoxMultiCopy.ShadowControl = false;
            this.groupBoxMultiCopy.ShadowThickness = 3;
            this.groupBoxMultiCopy.TabStop = false;
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxMultiCopy.TitileGradientColors = style3;
            this.groupBoxMultiCopy.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.groupBoxMultiCopy.TitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.rbtnY2);
            this.panel2.Controls.Add(this.rbtnY1);
            this.panel2.Controls.Add(this.numericUpDownYLen);
            this.panel2.Controls.Add(this.numericUpDownYCnt);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // rbtnY2
            // 
            resources.ApplyResources(this.rbtnY2, "rbtnY2");
            this.rbtnY2.Checked = true;
            this.rbtnY2.Name = "rbtnY2";
            this.rbtnY2.TabStop = true;
            this.rbtnY2.UseVisualStyleBackColor = true;
            this.rbtnY2.CheckedChanged += new System.EventHandler(this.rbtnY1_CheckedChanged);
            // 
            // rbtnY1
            // 
            resources.ApplyResources(this.rbtnY1, "rbtnY1");
            this.rbtnY1.Name = "rbtnY1";
            this.rbtnY1.UseVisualStyleBackColor = true;
            this.rbtnY1.CheckedChanged += new System.EventHandler(this.rbtnY1_CheckedChanged);
            // 
            // numericUpDownYLen
            // 
            this.numericUpDownYLen.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownYLen, "numericUpDownYLen");
            this.numericUpDownYLen.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownYLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownYLen.Name = "numericUpDownYLen";
            this.numericUpDownYLen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownYLen.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // numericUpDownYCnt
            // 
            resources.ApplyResources(this.numericUpDownYCnt, "numericUpDownYCnt");
            this.numericUpDownYCnt.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownYCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownYCnt.Name = "numericUpDownYCnt";
            this.numericUpDownYCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownYCnt.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownYCnt.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // numericUpDownXDis
            // 
            resources.ApplyResources(this.numericUpDownXDis, "numericUpDownXDis");
            this.numericUpDownXDis.Name = "numericUpDownXDis";
            this.numericUpDownXDis.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownXDis.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // numericUpDownYDis
            // 
            resources.ApplyResources(this.numericUpDownYDis, "numericUpDownYDis");
            this.numericUpDownYDis.Name = "numericUpDownYDis";
            this.numericUpDownYDis.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownYDis.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.numericUpDownXLen);
            this.panel1.Controls.Add(this.numericUpDownXCnt);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // numericUpDownXLen
            // 
            this.numericUpDownXLen.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownXLen, "numericUpDownXLen");
            this.numericUpDownXLen.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownXLen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXLen.Name = "numericUpDownXLen";
            this.numericUpDownXLen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXLen.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // numericUpDownXCnt
            // 
            resources.ApplyResources(this.numericUpDownXCnt, "numericUpDownXCnt");
            this.numericUpDownXCnt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXCnt.Name = "numericUpDownXCnt";
            this.numericUpDownXCnt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownXCnt.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownXCnt.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // numericUpDownXDis2
            // 
            resources.ApplyResources(this.numericUpDownXDis2, "numericUpDownXDis2");
            this.numericUpDownXDis2.Name = "numericUpDownXDis2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // numNegMaxGray
            // 
            resources.ApplyResources(this.numNegMaxGray, "numNegMaxGray");
            this.numNegMaxGray.Name = "numNegMaxGray";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // groupBoxOthers
            // 
            this.groupBoxOthers.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.groupBoxOthers.BorderThickness = 1F;
            this.groupBoxOthers.Controls.Add(this.checkBoxVoltage);
            this.groupBoxOthers.Controls.Add(this.checkBoxFilepath);
            this.groupBoxOthers.Controls.Add(this.checkBoxDirection);
            this.groupBoxOthers.Controls.Add(this.checkBoxPassNum);
            this.groupBoxOthers.Controls.Add(this.checkBoxResolution);
            this.groupBoxOthers.Controls.Add(this.checkBoxJobSize);
            this.groupBoxOthers.Controls.Add(this.label11);
            this.groupBoxOthers.Controls.Add(this.comboBoxNotePos);
            this.groupBoxOthers.Controls.Add(this.button_Font);
            this.groupBoxOthers.Controls.Add(this.textBoxFootNote);
            this.groupBoxOthers.Controls.Add(this.label_noteDis);
            this.groupBoxOthers.Controls.Add(this.numericUpDown_noteMargin);
            resources.ApplyResources(this.groupBoxOthers, "groupBoxOthers");
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxOthers.GradientColors = style4;
            this.groupBoxOthers.GroupImage = null;
            this.groupBoxOthers.Name = "groupBoxOthers";
            this.groupBoxOthers.PaintGroupBox = false;
            this.groupBoxOthers.RoundCorners = 5;
            this.groupBoxOthers.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBoxOthers.ShadowControl = false;
            this.groupBoxOthers.ShadowThickness = 3;
            this.groupBoxOthers.TabStop = false;
            style5.Color1 = System.Drawing.Color.LightBlue;
            style5.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxOthers.TitileGradientColors = style5;
            this.groupBoxOthers.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.groupBoxOthers.TitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // checkBoxVoltage
            // 
            this.checkBoxVoltage.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxVoltage, "checkBoxVoltage");
            this.checkBoxVoltage.Name = "checkBoxVoltage";
            this.checkBoxVoltage.UseVisualStyleBackColor = false;
            this.checkBoxVoltage.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // checkBoxFilepath
            // 
            this.checkBoxFilepath.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxFilepath, "checkBoxFilepath");
            this.checkBoxFilepath.Name = "checkBoxFilepath";
            this.checkBoxFilepath.UseVisualStyleBackColor = false;
            this.checkBoxFilepath.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // checkBoxDirection
            // 
            this.checkBoxDirection.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxDirection, "checkBoxDirection");
            this.checkBoxDirection.Name = "checkBoxDirection";
            this.checkBoxDirection.UseVisualStyleBackColor = false;
            this.checkBoxDirection.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // checkBoxPassNum
            // 
            this.checkBoxPassNum.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxPassNum, "checkBoxPassNum");
            this.checkBoxPassNum.Name = "checkBoxPassNum";
            this.checkBoxPassNum.UseVisualStyleBackColor = false;
            this.checkBoxPassNum.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // checkBoxResolution
            // 
            this.checkBoxResolution.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxResolution, "checkBoxResolution");
            this.checkBoxResolution.Name = "checkBoxResolution";
            this.checkBoxResolution.UseVisualStyleBackColor = false;
            this.checkBoxResolution.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // checkBoxJobSize
            // 
            this.checkBoxJobSize.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxJobSize, "checkBoxJobSize");
            this.checkBoxJobSize.Name = "checkBoxJobSize";
            this.checkBoxJobSize.UseVisualStyleBackColor = false;
            this.checkBoxJobSize.CheckedChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // comboBoxNotePos
            // 
            resources.ApplyResources(this.comboBoxNotePos, "comboBoxNotePos");
            this.comboBoxNotePos.Items.AddRange(new object[] {
            resources.GetString("comboBoxNotePos.Items"),
            resources.GetString("comboBoxNotePos.Items1"),
            resources.GetString("comboBoxNotePos.Items2"),
            resources.GetString("comboBoxNotePos.Items3")});
            this.comboBoxNotePos.Name = "comboBoxNotePos";
            this.comboBoxNotePos.SelectedIndexChanged += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // button_Font
            // 
            resources.ApplyResources(this.button_Font, "button_Font");
            this.button_Font.Name = "button_Font";
            this.button_Font.Click += new System.EventHandler(this.button_Font_Click);
            // 
            // textBoxFootNote
            // 
            resources.ApplyResources(this.textBoxFootNote, "textBoxFootNote");
            this.textBoxFootNote.Name = "textBoxFootNote";
            this.textBoxFootNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFootNote_KeyDown);
            this.textBoxFootNote.Leave += new System.EventHandler(this.OnClipSettingChanged);
            // 
            // label_noteDis
            // 
            resources.ApplyResources(this.label_noteDis, "label_noteDis");
            this.label_noteDis.BackColor = System.Drawing.Color.Transparent;
            this.label_noteDis.Name = "label_noteDis";
            // 
            // numericUpDown_noteMargin
            // 
            resources.ApplyResources(this.numericUpDown_noteMargin, "numericUpDown_noteMargin");
            this.numericUpDown_noteMargin.Name = "numericUpDown_noteMargin";
            this.numericUpDown_noteMargin.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDown_noteMargin.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.m_NumericUpDownCopy);
            this.groupBox1.Controls.Add(this.CheckBoxAlternatingprint);
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBox1.GradientColors = style6;
            this.groupBox1.GroupImage = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.PaintGroupBox = false;
            this.groupBox1.RoundCorners = 5;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 3;
            this.groupBox1.TabStop = false;
            style7.Color1 = System.Drawing.Color.LightBlue;
            style7.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBox1.TitileGradientColors = style7;
            this.groupBox1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.groupBox1.TitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // m_NumericUpDownCopy
            // 
            resources.ApplyResources(this.m_NumericUpDownCopy, "m_NumericUpDownCopy");
            this.m_NumericUpDownCopy.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownCopy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownCopy.Name = "m_NumericUpDownCopy";
            this.m_NumericUpDownCopy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumericUpDownCopy.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // CheckBoxAlternatingprint
            // 
            this.CheckBoxAlternatingprint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.CheckBoxAlternatingprint, "CheckBoxAlternatingprint");
            this.CheckBoxAlternatingprint.Name = "CheckBoxAlternatingprint";
            this.CheckBoxAlternatingprint.UseVisualStyleBackColor = false;
            // 
            // groupBoxRegion
            // 
            this.groupBoxRegion.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.groupBoxRegion.BorderThickness = 1F;
            this.groupBoxRegion.Controls.Add(this.label4);
            this.groupBoxRegion.Controls.Add(this.numericUpDownH);
            this.groupBoxRegion.Controls.Add(this.label3);
            this.groupBoxRegion.Controls.Add(this.numericUpDownY);
            this.groupBoxRegion.Controls.Add(this.label2);
            this.groupBoxRegion.Controls.Add(this.numericUpDownW);
            this.groupBoxRegion.Controls.Add(this.label1);
            this.groupBoxRegion.Controls.Add(this.numericUpDownX);
            resources.ApplyResources(this.groupBoxRegion, "groupBoxRegion");
            style8.Color1 = System.Drawing.Color.LightBlue;
            style8.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxRegion.GradientColors = style8;
            this.groupBoxRegion.GroupImage = null;
            this.groupBoxRegion.Name = "groupBoxRegion";
            this.groupBoxRegion.PaintGroupBox = false;
            this.groupBoxRegion.RoundCorners = 5;
            this.groupBoxRegion.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBoxRegion.ShadowControl = false;
            this.groupBoxRegion.ShadowThickness = 3;
            this.groupBoxRegion.TabStop = false;
            style9.Color1 = System.Drawing.Color.LightBlue;
            style9.Color2 = System.Drawing.Color.SteelBlue;
            this.groupBoxRegion.TitileGradientColors = style9;
            this.groupBoxRegion.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.groupBoxRegion.TitleTextColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // numericUpDownH
            // 
            resources.ApplyResources(this.numericUpDownH, "numericUpDownH");
            this.numericUpDownH.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownH.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // numericUpDownY
            // 
            resources.ApplyResources(this.numericUpDownY, "numericUpDownY");
            this.numericUpDownY.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownY.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // numericUpDownW
            // 
            resources.ApplyResources(this.numericUpDownW, "numericUpDownW");
            this.numericUpDownW.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownW.Name = "numericUpDownW";
            this.numericUpDownW.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownW.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // numericUpDownX
            // 
            resources.ApplyResources(this.numericUpDownX, "numericUpDownX");
            this.numericUpDownX.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.OnClipSettingChanged);
            this.numericUpDownX.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // Panel_buttons
            // 
            this.Panel_buttons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel_buttons.Controls.Add(this.radioButton_AdvancedMode);
            this.Panel_buttons.Controls.Add(this.radioButton_Simplemode);
            this.Panel_buttons.Controls.Add(this.buttonCancel);
            this.Panel_buttons.Controls.Add(this.buttonOk);
            resources.ApplyResources(this.Panel_buttons, "Panel_buttons");
            this.Panel_buttons.Name = "Panel_buttons";
            // 
            // radioButton_AdvancedMode
            // 
            resources.ApplyResources(this.radioButton_AdvancedMode, "radioButton_AdvancedMode");
            this.radioButton_AdvancedMode.Name = "radioButton_AdvancedMode";
            this.radioButton_AdvancedMode.CheckedChanged += new System.EventHandler(this.radioButton_Simplemode_CheckedChanged);
            // 
            // radioButton_Simplemode
            // 
            this.radioButton_Simplemode.Checked = true;
            resources.ApplyResources(this.radioButton_Simplemode, "radioButton_Simplemode");
            this.radioButton_Simplemode.Name = "radioButton_Simplemode";
            this.radioButton_Simplemode.TabStop = true;
            this.radioButton_Simplemode.CheckedChanged += new System.EventHandler(this.radioButton_Simplemode_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // EditJobForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Panel_buttons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditJobForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditJobForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel_clip.ResumeLayout(false);
            this.panel_clipBottom.ResumeLayout(false);
            this.panel_AddsrcList.ResumeLayout(false);
            this.panel_AddsrcList.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBoxMultiCopy.ResumeLayout(false);
            this.groupBoxMultiCopy.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYDis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXDis2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNegMaxGray)).EndInit();
            this.groupBoxOthers.ResumeLayout(false);
            this.groupBoxOthers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_noteMargin)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCopy)).EndInit();
            this.groupBoxRegion.ResumeLayout(false);
            this.groupBoxRegion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            this.Panel_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;

		public EditJobForm()
		{
			InitializeComponent();

		    if (PubFunc.IsInDesignMode())//设计时支持
		        return;
			m_CliPRect = new UserRect(new Rectangle(0, 0, 100, 100));
			m_CliPRect.SetPictureBox(this.m_OldPrintingPreview.ImagePictureBox);
			m_CliPRect.OnClipRecChanged += new UserRect.OnClipRecChangedEventHandler(CliPRect_OnClipRecChanged);
			//if (m_image != null)
			//{
			//    this.m_OldPrintingPreview.UpdatePreviewImage(this.m_image);
			//}
			//			this.comboBox1.Items.Clear();
			//			foreach(string al in Enum.GetNames(typeof(ContentAlignment)))
			//			this.comboBox1.Items.AddRange(Enum.GetNames(typeof(ContentAlignment)));
            numericUpDownXDis2.Minimum=
			this.numericUpDownXDis.Minimum = this.numericUpDownYDis.Minimum = 0;
            numericUpDownXDis2.Maximum =
			this.numericUpDownXDis.Maximum = this.numericUpDownYDis.Maximum = new decimal(int.MaxValue);

//#if GLOGAL_ALTERNATINGPRINT
            CheckBoxMirrorPrint.Visible = CheckBoxAlternatingprint.Visible = false;
//#endif
            bool isNkt = SPrinterProperty.IsTILE_PRINT_ID();
		    numericUpDownXDis2.Visible = label9.Visible = isNkt;
            //m_NumericUpDownCopy.Enabled = false; //10280307 英威 kip版本临时要求禁用
            if (SPrinterProperty.IsBiHong() && UIFunctionOnOff.SwapXwithY)
            {
                m_OldPrintingPreview.Rotate = RotateFlipType.Rotate90FlipNone;
            }
		    if (UIFunctionOnOff.PreviewRotate180)
            {
                m_OldPrintingPreview.Rotate = RotateFlipType.Rotate180FlipNone;
            }

            this.rbtnY1.Checked = true;
            rbtnY1_CheckedChanged(null, null);

            if (PubFunc.IsSupportJobTileHeight())
            {
                rbtnY2.Visible = true;
                numericUpDownYLen.Visible = true;
            }
            else
            {
                rbtnY2.Visible = false;
                numericUpDownYLen.Visible = false;

                panel2.Height -= 30;

                Point lblxDisLoc = label7.Location;
                Point numxDisLoc = numericUpDownXDis.Location;
                Point lblyDisLoc = label5.Location;
                Point numyDisLoc = numericUpDownYDis.Location;

                lblxDisLoc.Y -= 26;
                numxDisLoc.Y -= 26;
                lblyDisLoc.Y -= 26;
                numyDisLoc.Y -= 26;

                label7.Location = lblxDisLoc;
                numericUpDownXDis.Location = numxDisLoc;
                label5.Location = lblyDisLoc;
                numericUpDownYDis.Location = numyDisLoc;
            }

		}


		#region 属性

		private bool isDirty = false;

		public bool IsDirty
		{
			get { return isDirty; }
			set { isDirty = value; }
		}

		public UIJob EditJob
		{
			get 
			{
				m_EditJob.sJobSetting.bReversePrint = CheckBoxMirrorPrint.Checked;
				m_EditJob.sJobSetting.bAlternatingPrint = CheckBoxAlternatingprint.Checked;
			    m_EditJob.sJobSetting.cNegMaxGray = (byte) ((float)numNegMaxGray.Value/100f*255f);
                if (PubFunc.IsSupportJobTileHeight())
                {
                    m_EditJob.IsHeight = rbtnY2.Checked ? true : false;
                }
				return m_EditJob; 
			}
			set 
			{ 
				m_EditJob = value;

				isSetting = true;
				m_PreviewJob = m_EditJob.Clone();
				//				SPrtFileInfo ss = new SPrtFileInfo();
				//				int bret = CoreInterface.Printer_GetFileInfo(m_PreviewJob.FileLocation,ref this.m_PreviewJob.Clips.PrtFileInfo,0);
				m_PreviewJob.Clips.PrtFileInfo = m_PreviewJob.PrtFileInfo;
				if(!m_PreviewJob.IsClipOrTile)
				{
					if(this.m_PreviewJob.Miniature==null)
						EnableUI(false);
					else
						this.m_PreviewJob.Clips.SrcMiniature = this.m_PreviewJob.Miniature;
				}
				else
				{
					if(this.m_PreviewJob.Clips.SrcMiniature==null)
						EnableUI(false);
				}
			    string path = PubFunc.GetFullPreviewPath(this.m_PreviewJob.PreViewFile);
                if (File.Exists(path))
			    {
                    Image srcImage = new Bitmap(path);
                    this.m_OldPrintingPreview.UpdatePreviewImage(srcImage);
                }

				bool bclip = m_PreviewJob.IsClip;
				bool btile = m_PreviewJob.IsTile;
				m_PreviewJob.IsClip = m_PreviewJob.IsTile = false;
                //SizeF srcjobsize = this.m_PreviewJob.JobSize;
                Size Dimension = new Size(m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth, m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight);
                int ResolutionX = m_PreviewJob.PrtFileInfo.sFreSetting.nResolutionX * m_PreviewJob.PrtFileInfo.sImageInfo.nImageResolutionX;
                int ResolutionY = m_PreviewJob.PrtFileInfo.sFreSetting.nResolutionY * m_PreviewJob.PrtFileInfo.sImageInfo.nImageResolutionY;
                float width = (float)Dimension.Width / (float)ResolutionX;
                float height = (float)Dimension.Height / (float)ResolutionY;
                SizeF srcjobsize = new SizeF(width, height);
                this.m_OldPrintingPreview.UpdateJobSizeInfo(srcjobsize);
				numericUpDownX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Width));
				numericUpDownY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Height));
				numericUpDownH.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownH.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Height));
				numericUpDownW.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownW.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Width));
				m_PreviewJob.IsClip = bclip;
				m_PreviewJob.IsTile = btile;

				//			m_PreviewJob.Clips.Miniature = m_EditJob.Miniature;

				this.textBox_srcPath.Text  =this.m_PreviewJob.FileLocation;
                if (m_PreviewJob.sJobSetting.bIsDouble4CJob)
                    this.textBox_srcPath.Text +=Environment.NewLine+ this.m_PreviewJob.FileLocation2;

				this.printingInfo1.UpdateUIJobPreview(this.m_PreviewJob);
				if(this.m_PreviewJob.IsClip)
				{
					RectangleF UIClip = this.CalculateUIClipRecByReal(m_PreviewJob.Clips.ClipRect);
					UIClip.Intersect(new Rectangle(0,0,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height));
					this.m_CliPRect.SetClipRectangle(UIClip);

					RectangleF ClipRec = this.CalculateDisClipRec(UIClip);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownX,ClipRec.X);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownY,ClipRec.Y);
					
					if((decimal)ClipRec.Width< numericUpDownW.Minimum)
						ClipRec.Width = (float)numericUpDownW.Minimum;
					if((decimal)ClipRec.Width>numericUpDownW.Maximum)
						ClipRec.Width = (float)numericUpDownW.Maximum;
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownW,ClipRec.Width);

                    if ((decimal)ClipRec.Height < numericUpDownH.Minimum)
                        ClipRec.Height = (float)numericUpDownH.Minimum;
                    if ((decimal)ClipRec.Height > numericUpDownH.Maximum)
                        ClipRec.Height = (float)numericUpDownH.Maximum;
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH,ClipRec.Height);
					this.m_OldPrintingPreview.Refresh();
				}
				if(this.m_PreviewJob.IsTile)
				{
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt,m_PreviewJob.Clips.XCnt);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYCnt,m_PreviewJob.Clips.YCnt);
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXDis, m_CurrentUnit, (float)m_PreviewJob.Clips.XDis / m_PreviewJob.ResolutionX);
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXDis2, m_CurrentUnit, (float)m_PreviewJob.Clips.XDis2 / m_PreviewJob.ResolutionX);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYDis,m_CurrentUnit, (float)m_PreviewJob.Clips.YDis/m_PreviewJob.ResolutionY);
				}
				checkBoxJobSize.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00001) > 0;
				checkBoxResolution.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00010)> 0;
				checkBoxPassNum.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00100)> 0;
				checkBoxDirection.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x01000)> 0;
				checkBoxFilepath.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x10000)> 0;
                checkBoxVoltage.Checked=(m_PreviewJob.Clips.AddtionInfoMask & 0x100000)>0;

				this.textBoxFootNote.Text = m_PreviewJob.Clips.Note;
				this.textBoxFootNote.Font = m_PreviewJob.Clips.GetNoteFont();
				UIPreference.SetSelectIndexAndClampWithMax(this.comboBoxNotePos,m_PreviewJob.Clips.NotePosition);
				UIPreference.SetValueAndClampWithMinMax(this.numericUpDown_noteMargin,m_CurrentUnit, (float)m_PreviewJob.Clips.NoteMargin/m_PreviewJob.ResolutionY);
				this.radioButton_Simplemode.Checked = m_PreviewJob.IsSimpleMode;
				this.checkBoxRegion.Checked = m_PreviewJob.IsClip;
				this.checkBox_MultiCopy.Checked = m_PreviewJob.IsTile;
				CheckBoxMirrorPrint.Checked = m_PreviewJob.sJobSetting.bReversePrint;
				CheckBoxAlternatingprint.Checked = m_PreviewJob.sJobSetting.bAlternatingPrint;
                numNegMaxGray.Value = (decimal)(m_EditJob.sJobSetting.cNegMaxGray/255f*100);

                if (this.m_PreviewJob.IsHeight)
                {
                    rbtnY2.Checked = true;
                }
                else
                {
                    rbtnY1.Checked = true;
                }
                //m_PreviewJob.SetWidth = this.m_PreviewJob.JobSize.Width;
                //m_PreviewJob.SetHeight = this.m_PreviewJob.JobSize.Height;
                UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXLen, m_CurrentUnit, this.m_PreviewJob.SetWidth);
                UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYLen, m_CurrentUnit, this.m_PreviewJob.SetHeight);

                isSetting = false;
            }
		}

		public int Copies
		{
			get
			{
				return Decimal.ToInt32(m_NumericUpDownCopy.Value);
			}
			set
			{
				m_NumericUpDownCopy.Value = value;
			}
		}

		#endregion

		#region 事件

		private void CliPRect_OnClipRecChanged(object sender, OnClipRecChangedEventArgs e)
		{
			//生成剪切后的作业预览并更新预览
			//			UIJob clipjob = m_EditJob.Clone();
			m_PreviewJob.Clips.noClip = m_CliPRect.clipAll;

			if(m_PreviewJob.Clips.noClip)
				m_PreviewJob.Clips.ClipRect = new Rectangle(0,0, m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageWidth,m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageHeight);
			else
				m_PreviewJob.Clips.ClipRect = this.CalculateRealClipRec(e.ClipRectangle);
			RectangleF ClipRec = this.CalculateDisClipRec(m_PreviewJob.Clips.ClipRect);

			isSetting = true;
			UIPreference.SetValueAndClampWithMinMax(this.numericUpDownX,ClipRec.X);
			UIPreference.SetValueAndClampWithMinMax(this.numericUpDownY,ClipRec.Y);
			if((decimal)ClipRec.Width< numericUpDownW.Minimum)
				ClipRec.Width = (float)numericUpDownW.Minimum;
			if((decimal)ClipRec.Width>numericUpDownW.Maximum)
				ClipRec.Width = (float)numericUpDownW.Maximum;
			this.numericUpDownW.Value = (decimal)ClipRec.Width;
			if((decimal)ClipRec.Width< numericUpDownH.Minimum)
				ClipRec.Width = (float)numericUpDownH.Minimum;
			if((decimal)ClipRec.Width>numericUpDownH.Maximum)
				ClipRec.Width = (float)numericUpDownH.Maximum;
			UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH,ClipRec.Height);
			isSetting = false;

			UpdatePreviewAndClipBox();
		}


		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if(isSetting) return;
			//this.m_OldPreviewPictureBox1.Enabled = this.checkBoxRegion.Checked;
			this.groupBoxRegion.Enabled = this.checkBoxRegion.Checked;
			this.m_PreviewJob.IsClip = this.checkBoxRegion.Checked;
			this.m_PreviewJob.Clips.noClip = !this.m_PreviewJob.IsClip;

			if(this.checkBoxRegion.Checked)
			{
				this.m_CliPRect.rect.Intersect(new Rectangle(0,0,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height));
				m_PreviewJob.Clips.ClipRect = CalculateRealClipRec(this.m_CliPRect.rect);
			}
			else
			{

				//					DialogResult dr = MessageBox.Show(this,"该操作会丢失前面的设置,您确定这么做吗?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
				//					if(dr == DialogResult.Yes)
				//						m_PreviewJob = m_EditJob.Clone();
				//					else
				//						checkBoxRegion.Checked = true;
			}

			this.m_CliPRect.Visble = this.checkBoxRegion.Checked;
			UpdatePreviewAndClipBox();	
			this.m_OldPrintingPreview.Refresh();
		}


		private void EditJobForm_Load(object sender, EventArgs e)
		{
			this.groupBoxMultiCopy.Enabled = this.checkBox_MultiCopy.Checked;
			this.groupBoxRegion.Enabled = this.checkBoxRegion.Checked;
            if (this.m_PreviewJob.IsClipOrTile || m_PreviewJob.Clips.XCnt > 1 || m_PreviewJob.Clips.YCnt > 1)
                this.UpdatePreviewAndClipBox();

			// 某些机器上showdialog前会引发sizechangged;造成clipbox显示不正常
			// 奇怪的是我的开发机器上确不引发sizechangged;
			this.OnClipSettingChanged(sender,e);
		    this.m_CliPRect.Visble = checkBoxRegion.Checked;
		}


		private void m_AnyControl_ValueChanged(object sender, System.EventArgs e)
		{
			((NumericUpDown)sender).EndInit();
		}


		private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_AnyControl_ValueChanged(sender, e);
		}

		private void OnClipSettingChanged(object sender, System.EventArgs e)
		{
			if(isSetting) return;

            m_PreviewJob.IsSimpleMode = this.radioButton_Simplemode.Checked;
			m_PreviewJob.Clips.XCnt = Convert.ToInt32(this.numericUpDownXCnt.Value);
			m_PreviewJob.Clips.YCnt = Convert.ToInt32(this.numericUpDownYCnt.Value);
            m_PreviewJob.Clips.XDis = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownXDis.Value) * m_PreviewJob.ResolutionX);
            m_PreviewJob.Clips.XDis2 = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownXDis2.Value) * m_PreviewJob.ResolutionX);
			m_PreviewJob.Clips.YDis = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownYDis.Value)*m_PreviewJob.ResolutionY);
			
            int infomask =0;
			if(checkBoxJobSize.Checked)
				infomask |= 0x00001;
			else
				infomask &= ~0x00001;
			if(checkBoxResolution.Checked)
				infomask |= 0x00010;
			else
				infomask &= ~0x00010;
			if(checkBoxPassNum.Checked)
				infomask |= 0x00100;
			else
				infomask &= ~0x00100;
			if(checkBoxDirection.Checked)
				infomask |= 0x01000;
			else
				infomask &= ~0x01000;
			if(checkBoxFilepath.Checked)
				infomask |= 0x10000;
			else
				infomask &= ~0x10000;
            if (checkBoxVoltage.Checked)
                infomask |= 0x100000;
            else
                infomask &= ~0x100000;

			m_PreviewJob.Clips.AddtionInfoMask = infomask;
			m_PreviewJob.Clips.Note = this.textBoxFootNote.Text;
			//			m_PreviewJob.Clips.NoteFontName = this.textBoxFootNote.Font.Name;
			//			m_PreviewJob.Clips.NoteFontSize = this.textBoxFootNote.Font.Size;
			m_PreviewJob.Clips.NoteFont = this.textBoxFootNote.Font;
			m_PreviewJob.Clips.NotePosition = this.comboBoxNotePos.SelectedIndex;

			if(m_PreviewJob.Clips.NotePosition == 1 || m_PreviewJob.Clips.NotePosition == 3)
				m_PreviewJob.Clips.NoteMargin =Convert.ToInt32( UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDown_noteMargin.Value)*m_PreviewJob.ResolutionY);
			else
				m_PreviewJob.Clips.NoteMargin = Convert.ToInt32( UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDown_noteMargin.Value)*m_PreviewJob.ResolutionX);
			if(m_PreviewJob.IsClip)
			{
				RectangleF disrect = new RectangleF((float)this.numericUpDownX.Value,(float)this.numericUpDownY.Value,
					(float)this.numericUpDownW.Value,(float)this.numericUpDownH.Value);
				RectangleF uirect = CalculateUIClipRec(disrect);
				RectangleF uirect1 = uirect;
				uirect1.Intersect(m_OldPrintingPreview.ImagePictureBox.ClientRectangle);
				if(!this.m_CliPRect.SetClipRectangle(uirect1))//(uirect1 != uirect)
				{
					RectangleF ClipRec = this.CalculateDisClipRec(m_CliPRect.rect);
					isSetting =true;
					this.numericUpDownX.Value = (decimal)ClipRec.X;
					this.numericUpDownY.Value = (decimal)ClipRec.Y;
					if((decimal)ClipRec.Width< numericUpDownW.Minimum)
						ClipRec.Width = (float)numericUpDownW.Minimum;
					if((decimal)ClipRec.Width>numericUpDownW.Maximum)
						ClipRec.Width = (float)numericUpDownW.Maximum;
					this.numericUpDownW.Value = (decimal)ClipRec.Width;
					if((decimal)ClipRec.Width< numericUpDownH.Minimum)
						ClipRec.Width = (float)numericUpDownH.Minimum;
					if((decimal)ClipRec.Width>numericUpDownH.Maximum)
						ClipRec.Width = (float)numericUpDownH.Maximum;
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH,ClipRec.Height);
					isSetting =false;
				}
				//				this.m_CliPRect.SetClipRectangle(uirect1);
				this.m_OldPrintingPreview.Refresh();

				m_PreviewJob.Clips.ClipRect = CalculateRealClipRec(m_CliPRect.rect);
			}
			if(m_PreviewJob.IsTile)
			{
                AllParam allp = m_iPrinterChange.GetAllParam();
                float pgW = allp.PrinterSetting.sBaseSetting.fPaperWidth;
                float pgH = allp.PrinterSetting.sBaseSetting.fPaperHeight;
                float realPgW = PubFunc.CalcRealJobWidth(pgW, this.m_iPrinterChange.GetAllParam());
                float JobW = this.m_PreviewJob.JobSize.Width;

                Size originSize = new Size(m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageWidth, m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageHeight);
                if (m_PreviewJob.IsClip)
                {
                    originSize = new Size(m_PreviewJob.Clips.ClipRect.Width, m_PreviewJob.Clips.ClipRect.Height);
                }


                if (true)
                {
                    if (JobW > realPgW)
                    {
                        isSetting = true;
                        UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt, m_PreviewJob.Clips.CalcXMaxCount(Convert.ToInt32(realPgW * this.m_PreviewJob.ResolutionX)));
                        isSetting = false;
                        m_PreviewJob.Clips.XCnt = Convert.ToInt32(this.numericUpDownXCnt.Value);
                    }

                    this.m_PreviewJob.SetWidth = this.m_PreviewJob.JobSize.Width;

                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXLen, m_CurrentUnit, this.m_PreviewJob.SetWidth);

                }
                else
                {
                    JobW = UIPreference.ToInchLength(m_CurrentUnit, (float)numericUpDownXLen.Value);
                    if (JobW > realPgW)
                    {
                        isSetting = true;
                        JobW = realPgW;
                        UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXLen, JobW);
                        isSetting = false;
                    }

                    m_PreviewJob.SetWidth = UIPreference.ToInchLength(m_CurrentUnit, (float)numericUpDownXLen.Value);

                    float width = (float)originSize.Width / (float)m_PreviewJob.ResolutionX;

                    if (Math.Round(m_PreviewJob.SetWidth % width, 3) != 0 && (m_PreviewJob.SetWidth > width))
                    {
                        UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt, m_PreviewJob.SetWidth / width + 1);
                    }
                    else
                    {
                        UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt, Math.Max(m_PreviewJob.SetWidth / width, 1));
                    }


                }

                if (rbtnY1.Checked)
                {
                    //this.m_PreviewJob.SetHeight = this.m_PreviewJob.JobSize.Height;
                    //UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYLen, m_CurrentUnit, this.m_PreviewJob.SetHeight);
                    m_PreviewJob.IsHeight = false;
                }
                else
                {

                    float hh = (float)originSize.Height / (float)m_PreviewJob.ResolutionY;
                    m_PreviewJob.SetHeight = UIPreference.ToInchLength(m_CurrentUnit, (float)numericUpDownYLen.Value);
                    m_PreviewJob.IsHeight = true;

                    int YCnt = 0;
                    if (m_PreviewJob.SetHeight % hh != 0)
                        YCnt = (int)(m_PreviewJob.SetHeight / hh) + 1;
                    else
                        YCnt = (int)(m_PreviewJob.SetHeight / hh);

                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYCnt, YCnt);
                }

			}
			UpdatePreviewAndClipBox();

            //GC.Collect();
        }


		private void radioButton_Simplemode_CheckedChanged(object sender, System.EventArgs e)
		{
            //this.button_Addsrc.Visible = panel_srcList.Visible = this.radioButton_AdvancedMode.Checked;
			if(this.radioButton_AdvancedMode.Checked)
			{
				this.textBox_srcPath.BorderStyle = BorderStyle.Fixed3D;
				this.textBox_srcPath.ReadOnly = false;
			}
			else
			{
				this.textBox_srcPath.BorderStyle = BorderStyle.None;
				this.textBox_srcPath.ReadOnly = true;
			}
			OnClipSettingChanged(sender, e);
		}


		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(this.m_PreviewJob.Clips.XCnt > 1 && !this.InvalidCheck())
				{
					//this.tabControl_setting.SelectedIndex = 0;
					this.numericUpDownX.Focus();
					return;
				}
				this.m_EditJob = this.m_PreviewJob.Clone();
                if (this.m_EditJob.IsClipOrTile)
                {
                    m_EditJob.TilePreViewFile = m_EditJob.GeneratePreviewName(true);
                    if (this.m_image != null)
                    {
                        try
                        {
                            this.m_image.Save(PubFunc.GetFullPreviewPath(m_EditJob.TilePreViewFile));
                            m_image.Dispose();
                        }
                        catch { }
                    }
                }
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.DialogResult = DialogResult.OK;
		}

		private void checkBox_MultiCopy_CheckedChanged(object sender, System.EventArgs e)
		{
			if(isSetting) return;
			this.m_PreviewJob.IsTile = this.checkBox_MultiCopy.Checked;
			//			this.m_PreviewJob.Clips.noClip = this.m_PreviewJob.IsTile && !this.m_PreviewJob.IsClip;
			this.m_PreviewJob.Clips.noClip = !this.m_PreviewJob.IsClip;
			if(!this.checkBox_MultiCopy.Checked)
			{
				this.m_PreviewJob.Clips.XCnt = this.m_PreviewJob.Clips.YCnt = 1;
				UpdatePreviewAndClipBox();		
			}
			else
			{
				OnClipSettingChanged(sender,e);
			}

			this.groupBoxMultiCopy.Enabled = this.checkBox_MultiCopy.Checked;
		}

		private void textBoxFootNote_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
				OnClipSettingChanged(sender,e);
		}

		private void button_Font_Click(object sender, System.EventArgs e)
		{
			FontDialog fontDialog1 = new FontDialog();
			fontDialog1.Font = this.textBoxFootNote.Font;// new Font(font.Name, size);
			fontDialog1.AllowSimulations = false;
			fontDialog1.AllowVectorFonts = false;
			fontDialog1.AllowVerticalFonts = false;
			fontDialog1.FontMustExist = true;
			try
			{
				if (fontDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBoxFootNote.Font = fontDialog1.Font;
					OnClipSettingChanged(sender,e);
				}
			}
			catch
			{
				MessageBox.Show(ResString.GetResString("RulerConstantSeting_UnsupportedFont"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        private void rbtnY1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnY1.Checked)
            {
                numericUpDownYCnt.Enabled = true;
                numericUpDownYLen.Enabled = false;
            }
            else
            {
                numericUpDownYCnt.Enabled = false;
                numericUpDownYLen.Enabled = true;
                if (numericUpDownYLen.Value > 0)
                {
                    OnClipSettingChanged(null, null);
                }
            }
        }

		#endregion

		#region 方法

        public void OnPrinterSettingChange(SPrinterSetting ps)
        {
            this.numericUpDownXCnt.Minimum = 1;
            this.numericUpDownXCnt.Maximum = new Decimal(ps.sBaseSetting.fPaperWidth);

            numericUpDownXLen.Minimum = 0;
            numericUpDownXLen.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit, ps.sBaseSetting.fPaperWidth));

            numericUpDownYLen.Minimum = 0;
            numericUpDownYLen.Maximum = Decimal.MaxValue;

            this.numericUpDownYCnt.Minimum = 1;
            this.numericUpDownYCnt.Maximum = Decimal.MaxValue;
            this.numericUpDownXDis2.Minimum = 0;
            this.numericUpDownXDis2.Maximum = new Decimal(ps.sBaseSetting.fPaperWidth);
            this.numericUpDownXDis.Minimum = 0;
            this.numericUpDownXDis.Maximum = new Decimal(ps.sBaseSetting.fPaperWidth);
            this.numericUpDownYDis.Minimum = 0;
            this.numericUpDownYDis.Maximum = Decimal.MaxValue;
        }

		public void OnPreferenceChange( UIPreference up)
		{
			this.printingInfo1.OnPreferenceChange(up);
			if(m_CurrentUnit != up.Unit)
			{
				isSetting =true;
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
				isSetting = false;
			}
		}


		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDownX);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDownY);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDownW);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDownH);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownXDis);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownXDis2);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDownYDis);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,numericUpDown_noteMargin);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownXLen);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownYLen);

			string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownX, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownY, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownW, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownH, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownXDis, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownXDis2, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownYDis, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown_noteMargin, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownXLen, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownYLen, this.m_ToolTip);


			//			this.isDirty = false;
		}


		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
			this.printingInfo1.SetPrinterChange(ic);
			m_OldPrintingPreview.SetPrinterChange(ic);
		}


		/// <summary>
		/// 把缩略图上的剪切矩形按比例计算出作业实际的剪切大小(单位为像素)
		/// </summary>
		/// <param name="rect">缩略图上的剪切矩形</param>
		/// <returns>业实际的剪切大小(单位为像素)</returns>
		private Rectangle CalculateRealClipRec(RectangleF rect)
		{
			// to do
            //float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width);
            //float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height);
            //Rectangle clip = new Rectangle(Convert.ToInt32(rect.Left*parx),Convert.ToInt32(rect.Top*pary),
            //    Convert.ToInt32(rect.Width*parx),Convert.ToInt32(rect.Height*pary));
            //return clip;
            int cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            int cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate270FlipNone)
            {
                //rect = new RectangleF(cH - rect.Top - rect.Height, cW - rect.Left - rect.Width, rect.Height, rect.Width);
                rect = new RectangleF(cH - rect.Top - rect.Height, rect.Left, rect.Height, rect.Width);
                cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
                cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate90FlipNone)
            {
                rect = new RectangleF(rect.Top, cW - rect.Left - rect.Width, rect.Height, rect.Width);
                // rect = new RectangleF(cH - rect.Top - rect.Height, rect.Left, rect.Height, rect.Width);
                cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
                cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate180FlipNone)
            {
                rect = new RectangleF(cW - rect.Width - rect.Left, cH - rect.Height - rect.Top, rect.Width, rect.Height);
            }
            float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cW;
            float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cH;
            Rectangle clip = new Rectangle(Convert.ToInt32(rect.Left * parx), Convert.ToInt32(rect.Top * pary),
                Convert.ToInt32(rect.Width * parx), Convert.ToInt32(rect.Height * pary));
            return clip;
		}


		/// <summary>
		/// 把作业实际的剪切大小(单位为像素)按比例换算成缩略图上的剪切矩形
		/// </summary>
		/// <param name="rect">业实际的剪切大小(单位为像素)</param>
		/// <returns>缩略图上的剪切矩形</returns>
		private RectangleF CalculateUIClipRecByReal(RectangleF rect)
		{
			// to do
            //float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width);
            //float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height);
            //RectangleF clip = new RectangleF(rect.Left/parx,rect.Top/pary,rect.Width/parx,rect.Height/pary);
            //return clip;
            int cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            int cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
            float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cW;
            float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cH;
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate270FlipNone)
            {
                rect = new RectangleF(rect.Top,
                    this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth - rect.Left - rect.Width,
                    rect.Height, rect.Width);
                parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cW;
                pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cH;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate90FlipNone)
            {
                //20171121
                rect = new RectangleF(this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight - rect.Top - rect.Height,
                    rect.Left,
                    rect.Height, rect.Width);
                parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cW;
                pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cH;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate180FlipNone)
            {
                rect = new RectangleF(this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth - rect.Left - rect.Width,
                    m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight - rect.Top - rect.Height, rect.Width, rect.Height);
            }
            RectangleF clip = new RectangleF(rect.Left / parx, rect.Top / pary, rect.Width / parx, rect.Height / pary);
            return clip;
		}


		/// <summary>
		/// 把缩略图上的剪切矩形换算成按照界面显示的单位
		/// </summary>
		/// <param name="ScreenRec">缩略图上的剪切矩形</param>
		/// <returns>换算后的矩形</returns>
		private RectangleF CalculateDisClipRec(RectangleF ScreenRec)
		{
			// to do
			RectangleF scliprect = CalculateRealClipRec(this.m_CliPRect.rect);
			RectangleF ret = new RectangleF(
				UIPreference.ToDisplayLength(m_CurrentUnit,scliprect.Left/m_PreviewJob.ResolutionX),
				UIPreference.ToDisplayLength(m_CurrentUnit,scliprect.Top/m_PreviewJob.ResolutionY),
				UIPreference.ToDisplayLength(m_CurrentUnit,scliprect.Width/m_PreviewJob.ResolutionX),
				UIPreference.ToDisplayLength(m_CurrentUnit,scliprect.Height/m_PreviewJob.ResolutionY)
				);
			return ret;
		}


		/// <summary>
		/// 把界面输入的剪切矩形大小数据换算成UI缩略图上的剪切矩形
		/// </summary>
		/// <param name="ScreenRec">界面输入的剪切矩形</param>
		/// <returns>换算后的矩形</returns>
		private RectangleF CalculateUIClipRec(RectangleF ScreenRec)
		{
			// to do
			RectangleF ret = new RectangleF(
				UIPreference.ToInchLength(m_CurrentUnit,ScreenRec.Left)*m_PreviewJob.ResolutionX,
				UIPreference.ToInchLength(m_CurrentUnit,ScreenRec.Top)*m_PreviewJob.ResolutionY,
				UIPreference.ToInchLength(m_CurrentUnit,ScreenRec.Width)*m_PreviewJob.ResolutionX,
				UIPreference.ToInchLength(m_CurrentUnit,ScreenRec.Height)*m_PreviewJob.ResolutionY
				);
			return CalculateUIClipRecByReal(ret);
		}


		private void UpdatePreviewAndClipBox()
		{
		    this.groupBoxOthers.Enabled =true;//this.m_PreviewJob.IsClipOrTile;
			m_image = this.m_PreviewJob.Clips.CreateClipsMiniature();
            this.printingInfo1.UpdateUIJobPreview(m_PreviewJob, m_image);
		}


		public void UpdateClipBox(Image img,Image imgsrc)
		{
            //this.m_EditJob.Miniature = this.m_PreviewJob.Miniature =img;
            //this.m_EditJob.Clips.SrcMiniature = this.m_PreviewJob.Clips.SrcMiniature = imgsrc;
			this.m_OldPrintingPreview.UpdatePreviewImage(imgsrc);
			EnableUI(img != null);
		}


		private void EnableUI(bool enable)
		{
            this.groupBoxOthers.Enabled = true;
            this.groupBoxMultiCopy.Enabled =
				this.groupBoxRegion.Enabled = this.panel_clip.Enabled =  enable;
		}

		public void SetGroupBoxStyle(Grouper ts)
		{
			if( ts == null)
				return;
            //foreach(Control con in this.tabPage_setting.Controls)
            foreach (Control con in this.splitContainer3.Controls)
            {
				if(con is Grouper)
				{
					(con as Grouper).CloneGrouperStyle(ts);
				}
			}
			//			this.groupBox1.CloneGrouperStyle(ts);
			//			this.groupBoxMultiCopy.CloneGrouperStyle(ts);
			//			this.groupBoxOthers.CloneGrouperStyle(ts);
			//			this.groupBoxRegion.CloneGrouperStyle(ts);
		}

		private bool InvalidCheck()
		{
			AllParam allp = m_iPrinterChange.GetAllParam();
			float pgW = allp.PrinterSetting.sBaseSetting.fPaperWidth;
			float pgH = allp.PrinterSetting.sBaseSetting.fPaperHeight;
			float realPgW = PubFunc.CalcRealJobWidth(pgW,this.m_iPrinterChange.GetAllParam());

            float JobW = this.m_PreviewJob.JobSize.Width;
		    string info = string.Empty;
            if (JobW > realPgW)
			{
                info += SErrorCode.GetResString("Software_MediaTooSmall");
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
		    return true;
		}

    # endregion
    }
}
