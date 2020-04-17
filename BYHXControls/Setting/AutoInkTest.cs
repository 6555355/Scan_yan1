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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for KonicTemperature1.
	/// </summary>
	public class AutoInkTest : BYHXUserControl//System.Windows.Forms.UserControl
	{
		private SPrinterProperty m_rsPrinterPropery;//Only read for color order

		private int Voltage_LR_Num = 0; 
		private int m_HeadNum =0;
		private int m_TempNum = 0;
		private int m_HeadVoltageNum =0;
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
		private bool m_bSupportHeadHeat = false;
		private bool m_bXaar382 = false;
		private bool m_bPolaris = false;
		private	bool m_bExcept = false;
		private bool m_Konic512_1head2color = false;
		private bool m_bPolaris_V5_8 = false;
		private bool m_bPolaris_V5_8_Emerald = false;
		private bool m_bPolaris_V7_16 = false;
        private bool m_bSpectra_SG1024_Gray = false;
        private bool m_bKonic1024i_Gray = false;
        private bool m_bPolaris_V7_16_Emerald = false;
        private bool m_bPolaris_V7_16_Polaris = false;
		private bool m_bRicoHead  = false;
        private bool m_bXaar501 = false;
        private bool m_bKyocera = false;
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

		private System.Windows.Forms.Label   []m_LabelLRIndex;
		private System.Windows.Forms.Label   []m_LabelHorHeadIndex;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownHeadSet;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownHeadCur;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownHeadTeam;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownVoltageSet;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownVoltageCur;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownVoltageBase;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownPulseWidth;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownInkTempSet;
		private System.Windows.Forms.NumericUpDown []m_NumericUpDownInkTempCur;
        //xaar501
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownVoltageInk;
        private System.Windows.Forms.NumericUpDown[] m_NumericUpDownAutoVoltageInkOffset;
		private bool[] WhichRowVisble = new bool[9];
        //private bool[] WhichRowVisble = new bool[11];

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private GroupBox m_GroupBoxTemperature;
        private System.Windows.Forms.Label m_LabelBaseVol;
		private System.Windows.Forms.NumericUpDown numVolStep1;
        private System.Windows.Forms.Timer m_TimerRefresh;
		private System.Windows.Forms.Label m_LabelCurPulseWidth;
        private System.Windows.Forms.NumericUpDown numPulseWidthStep1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button buttonStartPrint;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private Label label3;
        private NumericUpDown numVolStep3;
        private Label label2;
        private NumericUpDown numVolStep2;
        private Label label7;
        private NumericUpDown numPulseWidthStep5;
        private Label label6;
        private NumericUpDown numPulseWidthStep4;
        private Label label5;
        private NumericUpDown numPulseWidthStep3;
        private Label label4;
        private NumericUpDown numPulseWidthStep2;
        private Label label9;
        private NumericUpDown numVolNumPerHead;
        private Label label10;
        private Label label15;
        private NumericUpDown numPulseWidthStep8;
        private Label label16;
        private NumericUpDown numPulseWidthStep7;
        private Label label17;
        private NumericUpDown numPulseWidthStep6;
        private Label label11;
        private NumericUpDown numVolStep8;
        private Label label12;
        private NumericUpDown numVolStep7;
        private Label label13;
        private NumericUpDown numVolStep6;
        private Label label14;
        private NumericUpDown numVolStep5;
        private Label label8;
        private NumericUpDown numVolStep4;
        private NumericUpDown numPlusNumPerHead;
        private Label label18;
        private NumericUpDown numHeadNum;
		private System.ComponentModel.IContainer components;

		public AutoInkTest()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoInkTest));
            this.m_TimerRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.numHeadNum = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numVolNumPerHead = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.m_GroupBoxTemperature = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numPulseWidthStep8 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numPulseWidthStep7 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numPulseWidthStep6 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numVolStep8 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numVolStep7 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numVolStep6 = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numVolStep5 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numVolStep4 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numPulseWidthStep5 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numPulseWidthStep4 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numPulseWidthStep3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numPulseWidthStep2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numVolStep3 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numVolStep2 = new System.Windows.Forms.NumericUpDown();
            this.m_LabelBaseVol = new System.Windows.Forms.Label();
            this.numVolStep1 = new System.Windows.Forms.NumericUpDown();
            this.m_LabelCurPulseWidth = new System.Windows.Forms.Label();
            this.numPulseWidthStep1 = new System.Windows.Forms.NumericUpDown();
            this.numPlusNumPerHead = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStartPrint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeadNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolNumPerHead)).BeginInit();
            this.m_GroupBoxTemperature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlusNumPerHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_TimerRefresh
            // 
            this.m_TimerRefresh.Interval = 1000;
            this.m_TimerRefresh.Tick += new System.EventHandler(this.m_TimerRefresh_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.numHeadNum);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numVolNumPerHead);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.m_GroupBoxTemperature);
            this.groupBox1.Controls.Add(this.numPlusNumPerHead);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Name = "label18";
            // 
            // numHeadNum
            // 
            this.numHeadNum.DecimalPlaces = 1;
            this.numHeadNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numHeadNum.InterceptArrowKeys = false;
            resources.ApplyResources(this.numHeadNum, "numHeadNum");
            this.numHeadNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeadNum.Name = "numHeadNum";
            this.numHeadNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Name = "label9";
            // 
            // numVolNumPerHead
            // 
            this.numVolNumPerHead.DecimalPlaces = 1;
            this.numVolNumPerHead.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolNumPerHead.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolNumPerHead, "numVolNumPerHead");
            this.numVolNumPerHead.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolNumPerHead.Name = "numVolNumPerHead";
            this.numVolNumPerHead.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Name = "label10";
            // 
            // m_GroupBoxTemperature
            // 
            this.m_GroupBoxTemperature.Controls.Add(this.label15);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep8);
            this.m_GroupBoxTemperature.Controls.Add(this.label16);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep7);
            this.m_GroupBoxTemperature.Controls.Add(this.label17);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep6);
            this.m_GroupBoxTemperature.Controls.Add(this.label11);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep8);
            this.m_GroupBoxTemperature.Controls.Add(this.label12);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep7);
            this.m_GroupBoxTemperature.Controls.Add(this.label13);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep6);
            this.m_GroupBoxTemperature.Controls.Add(this.label14);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep5);
            this.m_GroupBoxTemperature.Controls.Add(this.label8);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep4);
            this.m_GroupBoxTemperature.Controls.Add(this.label7);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep5);
            this.m_GroupBoxTemperature.Controls.Add(this.label6);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep4);
            this.m_GroupBoxTemperature.Controls.Add(this.label5);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep3);
            this.m_GroupBoxTemperature.Controls.Add(this.label4);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep2);
            this.m_GroupBoxTemperature.Controls.Add(this.label3);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep3);
            this.m_GroupBoxTemperature.Controls.Add(this.label2);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep2);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelBaseVol);
            this.m_GroupBoxTemperature.Controls.Add(this.numVolStep1);
            this.m_GroupBoxTemperature.Controls.Add(this.m_LabelCurPulseWidth);
            this.m_GroupBoxTemperature.Controls.Add(this.numPulseWidthStep1);
            resources.ApplyResources(this.m_GroupBoxTemperature, "m_GroupBoxTemperature");
            this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
            this.m_GroupBoxTemperature.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Name = "label15";
            // 
            // numPulseWidthStep8
            // 
            this.numPulseWidthStep8.DecimalPlaces = 1;
            this.numPulseWidthStep8.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep8.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep8, "numPulseWidthStep8");
            this.numPulseWidthStep8.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep8.Name = "numPulseWidthStep8";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Name = "label16";
            // 
            // numPulseWidthStep7
            // 
            this.numPulseWidthStep7.DecimalPlaces = 1;
            this.numPulseWidthStep7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep7.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep7, "numPulseWidthStep7");
            this.numPulseWidthStep7.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep7.Name = "numPulseWidthStep7";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Name = "label17";
            // 
            // numPulseWidthStep6
            // 
            this.numPulseWidthStep6.DecimalPlaces = 1;
            this.numPulseWidthStep6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep6.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep6, "numPulseWidthStep6");
            this.numPulseWidthStep6.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep6.Name = "numPulseWidthStep6";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Name = "label11";
            // 
            // numVolStep8
            // 
            this.numVolStep8.DecimalPlaces = 1;
            this.numVolStep8.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep8.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep8, "numVolStep8");
            this.numVolStep8.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep8.Name = "numVolStep8";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Name = "label12";
            // 
            // numVolStep7
            // 
            this.numVolStep7.DecimalPlaces = 1;
            this.numVolStep7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep7.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep7, "numVolStep7");
            this.numVolStep7.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep7.Name = "numVolStep7";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Name = "label13";
            // 
            // numVolStep6
            // 
            this.numVolStep6.DecimalPlaces = 1;
            this.numVolStep6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep6.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep6, "numVolStep6");
            this.numVolStep6.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep6.Name = "numVolStep6";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Name = "label14";
            // 
            // numVolStep5
            // 
            this.numVolStep5.DecimalPlaces = 1;
            this.numVolStep5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep5.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep5, "numVolStep5");
            this.numVolStep5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep5.Name = "numVolStep5";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // numVolStep4
            // 
            this.numVolStep4.DecimalPlaces = 1;
            this.numVolStep4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep4.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep4, "numVolStep4");
            this.numVolStep4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep4.Name = "numVolStep4";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // numPulseWidthStep5
            // 
            this.numPulseWidthStep5.DecimalPlaces = 1;
            this.numPulseWidthStep5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep5.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep5, "numPulseWidthStep5");
            this.numPulseWidthStep5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep5.Name = "numPulseWidthStep5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // numPulseWidthStep4
            // 
            this.numPulseWidthStep4.DecimalPlaces = 1;
            this.numPulseWidthStep4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep4.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep4, "numPulseWidthStep4");
            this.numPulseWidthStep4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep4.Name = "numPulseWidthStep4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // numPulseWidthStep3
            // 
            this.numPulseWidthStep3.DecimalPlaces = 1;
            this.numPulseWidthStep3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep3.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep3, "numPulseWidthStep3");
            this.numPulseWidthStep3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep3.Name = "numPulseWidthStep3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // numPulseWidthStep2
            // 
            this.numPulseWidthStep2.DecimalPlaces = 1;
            this.numPulseWidthStep2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep2.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep2, "numPulseWidthStep2");
            this.numPulseWidthStep2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep2.Name = "numPulseWidthStep2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // numVolStep3
            // 
            this.numVolStep3.DecimalPlaces = 1;
            this.numVolStep3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep3.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep3, "numVolStep3");
            this.numVolStep3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep3.Name = "numVolStep3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // numVolStep2
            // 
            this.numVolStep2.DecimalPlaces = 1;
            this.numVolStep2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep2.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep2, "numVolStep2");
            this.numVolStep2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep2.Name = "numVolStep2";
            // 
            // m_LabelBaseVol
            // 
            resources.ApplyResources(this.m_LabelBaseVol, "m_LabelBaseVol");
            this.m_LabelBaseVol.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelBaseVol.Name = "m_LabelBaseVol";
            // 
            // numVolStep1
            // 
            this.numVolStep1.DecimalPlaces = 1;
            this.numVolStep1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVolStep1.InterceptArrowKeys = false;
            resources.ApplyResources(this.numVolStep1, "numVolStep1");
            this.numVolStep1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolStep1.Name = "numVolStep1";
            this.numVolStep1.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelCurPulseWidth
            // 
            resources.ApplyResources(this.m_LabelCurPulseWidth, "m_LabelCurPulseWidth");
            this.m_LabelCurPulseWidth.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelCurPulseWidth.Name = "m_LabelCurPulseWidth";
            // 
            // numPulseWidthStep1
            // 
            this.numPulseWidthStep1.DecimalPlaces = 1;
            this.numPulseWidthStep1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPulseWidthStep1.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPulseWidthStep1, "numPulseWidthStep1");
            this.numPulseWidthStep1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPulseWidthStep1.Name = "numPulseWidthStep1";
            this.numPulseWidthStep1.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // numPlusNumPerHead
            // 
            this.numPlusNumPerHead.DecimalPlaces = 1;
            this.numPlusNumPerHead.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPlusNumPerHead.InterceptArrowKeys = false;
            resources.ApplyResources(this.numPlusNumPerHead, "numPlusNumPerHead");
            this.numPlusNumPerHead.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPlusNumPerHead.Name = "numPlusNumPerHead";
            this.numPlusNumPerHead.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.InterceptArrowKeys = false;
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // buttonStartPrint
            // 
            resources.ApplyResources(this.buttonStartPrint, "buttonStartPrint");
            this.buttonStartPrint.Name = "buttonStartPrint";
            this.buttonStartPrint.UseVisualStyleBackColor = true;
            this.buttonStartPrint.Click += new System.EventHandler(this.buttonStartPrint_Click);
            // 
            // AutoInkTest
            // 
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStartPrint);
            resources.ApplyResources(this, "$this");
            this.Name = "AutoInkTest";
            this.Load += new System.EventHandler(this.AutoInkTest_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeadNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolNumPerHead)).EndInit();
            this.m_GroupBoxTemperature.ResumeLayout(false);
            this.m_GroupBoxTemperature.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolStep1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPulseWidthStep1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPlusNumPerHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_rsPrinterPropery = sp;
#if true
			uint uiHtype = 0;
		    CoreInterface.GetUIHeadType(ref uiHtype);
            m_bKonic512 = (uiHtype & 0x01) != 0;
			m_bXaar382 = (uiHtype&0x02)!=0;;			
			m_bSpectra = (uiHtype&0x04)!=0;
			m_bPolaris = (uiHtype&0x08)!=0;
			m_bPolaris_V5_8 = (uiHtype&0x10)!=0;;
			m_bExcept = (uiHtype&0x20)!=0;
			m_bPolaris_V7_16 = (uiHtype&0x40)!=0;
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
			m_bVerArrangement =((sp.bSupportBit1&2)!=0);
            m_bMirrorArrangement = m_rsPrinterPropery.IsMirrorArrangement();
			m_b1head2color = (m_rsPrinterPropery.nOneHeadDivider==2);
			m_Konic512_1head2color = m_b1head2color && m_bKonic512;
			m_bPolaris_V7_16_Emerald = m_bPolaris_V7_16&&
				(sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl 
								||sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
            m_bPolaris_V7_16_Polaris = m_bPolaris_V7_16 &&
                (sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_PolarisColor4_80pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_15pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_35pl
                                || sp.ePrinterHead == PrinterHeadEnum.Spectra_Polaris_80pl);
			m_bPolaris_V5_8_Emerald = m_bPolaris_V5_8&&
				(sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_10pl 
				||sp.ePrinterHead == PrinterHeadEnum.Spectra_Emerald_30pl);
			m_bRicoHead = sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
				|| sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
				|| sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl;
		    m_bKyocera = SPrinterProperty.IsKyocera(sp.ePrinterHead);

		    if (m_bPolaris)
		        VOL_COUNT_PER_HEAD = 2;
            if (m_bSpectra_SG1024_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bKonic1024i_Gray)
                VOL_COUNT_PER_HEAD = 3;
            if (m_bPolaris_V7_16_Polaris)
                VOL_COUNT_PER_HEAD = 1f/2f;
            if (m_bKyocera)
                VOL_COUNT_PER_HEAD = 2;

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
				if(m_bKonic512)
				{
					m_HeadNum /= 2;//sp.nHeadNumPerColor;
					m_TempNum = m_HeadNum;
				}
                else if (m_bXaar501)//501
                {
                    this.m_InkVolageNum = m_HeadNum;
                    this.m_VolageOffsetNum = m_HeadNum;
                    this.m_HeadVoltageNum = m_HeadNum * (int)ADJUST_VOL_PER_HEAD;
                    this.m_TempNum = m_HeadNum*3;
                }
				else if(m_bPolaris)
				{
					if(!m_bExcept)
					{
						if(m_bPolaris_V5_8)
						{
							if(m_bPolaris_V5_8_Emerald)
							{
								m_HeadNum = m_HeadNum / sp.nHeadNumPerColor;// * VOLCOUNTPERPOLARISHEAD;
								m_TempNum = m_HeadNum;
                                m_HeadVoltageNum = (int) (m_HeadNum * VOL_COUNT_PER_HEAD);
							}
							else
							{
								m_HeadNum = m_HeadNum / 4;
								m_HeadVoltageNum = m_HeadNum;
                                m_TempNum = (int) (m_HeadVoltageNum * VOL_COUNT_PER_HEAD);
							}
						}
						else if(m_bPolaris_V7_16)
						{
							if(m_bPolaris_V7_16_Emerald)
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
                                m_HeadVoltageNum = (int) (m_HeadNum / VOL_COUNT_PER_HEAD);
							}
						}
						else
						{
							m_HeadNum = m_HeadNum / 4;
                            m_HeadVoltageNum = (int) (m_HeadNum * VOL_COUNT_PER_HEAD);
							m_TempNum = m_HeadVoltageNum;
						}
					}
					else
					{
						m_HeadNum= sp.nHeadNum/4;
						//						if(m_b1head2color)//是否为一头两色
						//						{
						//							if(m_HeadNum <sp.nColorNum)
						//								m_HeadNum = sp.nColorNum;
						//						}
#if GONGZENG_DOUBLE
						m_HeadVoltageNum = m_HeadNum*2;
#else
						m_HeadVoltageNum = m_HeadNum;
#endif
					}
				}
				else if(m_bRicoHead)
				{
					m_InkBaoTempNum = sp.nColorNum;
					m_HeadNum = sp.nHeadNum/2;
					m_TempNum = sp.nHeadNum;
					m_HeadVoltageNum = sp.nHeadNum;
				}
                else if (m_bKonic1024i_Gray)
                {
                    if (CoreInterface.IsKm1024I_AS_4HEAD())
                        m_HeadNum = sp.nHeadNum/2;
                    else
                        m_HeadNum = sp.nHeadNum;
                    m_TempNum = m_HeadNum / 2;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }
                else if (m_bSpectra_SG1024_Gray)
                {
                    if (CoreInterface.IsSG1024_AS_8_HEAD())
                        m_HeadNum = sp.nHeadNum/8;
                    else
                        m_HeadNum = sp.nHeadNum;

                    m_TempNum = m_HeadNum;
                    m_HeadVoltageNum = (int) (m_HeadNum*VOL_COUNT_PER_HEAD);
                }
                else if (m_bKyocera)
                {
                    m_HeadNum = (byte)(sp.nHeadNum / 16);
                    m_TempNum = m_HeadNum;
                    m_HeadVoltageNum = (int)(m_HeadNum * VOL_COUNT_PER_HEAD);
                }

			    Plus_COUNT_PER_HEAD = VOL_COUNT_PER_HEAD;
                m_HeadPulseWidthNum = m_HeadVoltageNum;
			    if (m_bSpectra_SG1024_Gray)
			    {
			        Plus_COUNT_PER_HEAD = 5;
			        m_HeadPulseWidthNum = (int) (m_HeadNum*Plus_COUNT_PER_HEAD);
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

				m_StartHeadIndex = 0;
				//m_pMap = (byte[])sp.pElectricMap.Clone();
				//CoreInterface.GetHeadMap(m_pMap,m_pMap.Length);
				int nmap_input = 1;
				if(sp.ePrinterHead == PrinterHeadEnum.Spectra_S_128)
				{
					nmap_input = 2;
				}
				int imax = Math.Max(m_HeadVoltageNum,m_TempNum);//修改原因:北极星v5-8时电压数小于温度数时温度map会出错
                imax = Math.Max(imax, m_HeadPulseWidthNum);
                m_pMap = new byte[imax];
                for (int i = 0 ; i< imax;i++)
				{
					m_pMap[i] = (byte)i;
				}
				m_bSupportHeadHeat = sp.bSupportHeadHeat || m_bRicoHead || sp.IsALLWIN_512i_HighSpeed()||m_bXaar501;
                if (m_bSpectra)
                {
                    m_bSupportHeadHeat = true;
                    if (m_bPolaris)
                    {
                        if (m_bExcept)
                            this.WhichRowVisble = new bool[10] { false, false, false, false, false, false, true, false, false ,false};
                        else if (m_bPolaris_V5_8_Emerald || m_bPolaris_V7_16_Emerald)
                            this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false };
                        else
                            this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, true, true, true, true, true, false, false, false };
                    }
                    else if (m_bSpectra_SG1024_Gray)
                    {
                        this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false };
                    }
                    else
                        this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, false, true, true, true, true, false, false, false };
                }
                else if (m_bKonic512)
                {
                    this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, false, true, true, true, false, false, false, false };
                }
                else if (m_bXaar382)
                {
                    this.WhichRowVisble = new bool[10] { false, true, false, true, true, false, false, false, false, false };
                }
                else if (m_bXaar501)
                {
                    this.WhichRowVisble = new bool[10] { true, true, false, true, false, false, false, false, true, true };
                }
                else if (m_bRicoHead)
                {
                    this.WhichRowVisble = new bool[10] { true, true, false, false, false, false, false, true, true, false };
                }
                else
                {
                    this.WhichRowVisble = new bool[10] { m_bSupportHeadHeat, true, false, true, true, true, false, false, false, false };
                }
                this.isDirty = false;
			}
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
            this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
		}
		
		private void m_TimerRefresh_Tick(object sender, System.EventArgs e)
		{

        }

        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }

        private void buttonStartPrint_Click(object sender, EventArgs e)
        {
            AutoInkTestHelper.Para.Enable = checkBox1.Checked;
            AutoInkTestHelper.Para.VStep[0] = (float)numVolStep1.Value;
            AutoInkTestHelper.Para.VStep[1] = (float)numVolStep2.Value;
            AutoInkTestHelper.Para.VStep[2] = (float)numVolStep3.Value;
            AutoInkTestHelper.Para.VStep[3] = (float)numVolStep4.Value;
            AutoInkTestHelper.Para.VStep[4] = (float)numVolStep5.Value;
            AutoInkTestHelper.Para.VStep[5] = (float)numVolStep6.Value;
            AutoInkTestHelper.Para.VStep[6] = (float)numVolStep7.Value;
            AutoInkTestHelper.Para.VStep[7] = (float)numVolStep8.Value;

            AutoInkTestHelper.Para.PStep[0] = (float)numPulseWidthStep1.Value;
            AutoInkTestHelper.Para.PStep[1] = (float)numPulseWidthStep2.Value;
            AutoInkTestHelper.Para.PStep[2] = (float)numPulseWidthStep3.Value;
            AutoInkTestHelper.Para.PStep[3] = (float)numPulseWidthStep4.Value;
            AutoInkTestHelper.Para.PStep[4] = (float)numPulseWidthStep5.Value;
            AutoInkTestHelper.Para.PStep[5] = (float)numPulseWidthStep6.Value;
            AutoInkTestHelper.Para.PStep[6] = (float)numPulseWidthStep7.Value;
            AutoInkTestHelper.Para.PStep[7] = (float)numPulseWidthStep8.Value;
            AutoInkTestHelper.Para.PrintTimes = (int)numericUpDown1.Value;
            AutoInkTestHelper.Para.VolNumPerHead = (int)numVolNumPerHead.Value;
            AutoInkTestHelper.Para.PlusNumPerHead = (int)numPlusNumPerHead.Value;
            AutoInkTestHelper.Para.HeadNum = (int) (numHeadNum.Value) ;
            AutoInkTestHelper.Save();
        }

        private void AutoInkTest_Load(object sender, EventArgs e)
        {
            if (PubFunc.IsInDesignMode())
                return;
            AutoInkTestHelper.Load();
            this.Visible =
            checkBox1.Checked=AutoInkTestHelper.Para.Enable;
            numVolStep1.Value=(decimal) AutoInkTestHelper.Para.VStep[0];
            numVolStep2.Value = (decimal)AutoInkTestHelper.Para.VStep[1];
            numVolStep3.Value = (decimal)AutoInkTestHelper.Para.VStep[2];
            numVolStep4.Value = (decimal)AutoInkTestHelper.Para.VStep[3];
            numVolStep5.Value = (decimal)AutoInkTestHelper.Para.VStep[4];
            numVolStep6.Value = (decimal)AutoInkTestHelper.Para.VStep[5];
            numVolStep7.Value = (decimal)AutoInkTestHelper.Para.VStep[6];
            numVolStep8.Value = (decimal)AutoInkTestHelper.Para.VStep[7];
            numPulseWidthStep1.Value = (decimal)AutoInkTestHelper.Para.PStep[0];
            numPulseWidthStep2.Value = (decimal)AutoInkTestHelper.Para.PStep[1];
            numPulseWidthStep3.Value = (decimal)AutoInkTestHelper.Para.PStep[2];
            numPulseWidthStep4.Value = (decimal)AutoInkTestHelper.Para.PStep[3];
            numPulseWidthStep5.Value = (decimal)AutoInkTestHelper.Para.PStep[4];
            numPulseWidthStep6.Value = (decimal)AutoInkTestHelper.Para.PStep[5];
            numPulseWidthStep7.Value = (decimal)AutoInkTestHelper.Para.PStep[6];
            numPulseWidthStep8.Value = (decimal)AutoInkTestHelper.Para.PStep[7];
            numericUpDown1.Value = AutoInkTestHelper.Para.PrintTimes;
            numVolNumPerHead.Value=AutoInkTestHelper.Para.VolNumPerHead;
            numPlusNumPerHead.Value=AutoInkTestHelper.Para.PlusNumPerHead;
            numHeadNum.Value = AutoInkTestHelper.Para.HeadNum;
        }

	}

    public struct AutoInkPara
    {
        /// <summary>
        /// 自动测试功能使能
        /// </summary>
        public bool Enable;

        /// <summary>
        /// 电压递增值
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public float[] VStep;

        /// <summary>
        /// 脉宽递增值
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 8)]
        public float[] PStep;

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintTimes;

        /// <summary>
        /// 每个喷头电压个数
        /// </summary>
        public int VolNumPerHead;

        /// <summary>
        /// 每个喷头脉宽个数
        /// </summary>
        public int PlusNumPerHead;

        /// <summary>
        /// 喷头个数
        /// </summary>
        public int HeadNum;
    }

    public static class AutoInkTestHelper
    {
        public const uint Rmask = (1 << (int) EnumVoltageTemp.VoltageAjust)
                                  | (1 << (int) EnumVoltageTemp.VoltageBase)
                                  | (1 << (int) EnumVoltageTemp.PulseWidth);
            public const string filename = "AutoInkTest.xml";
        public static AutoInkPara Para = new AutoInkPara();
        public static SRealTimeCurrentInfo realTimeCurrentInfo;

        /// <summary>
        /// 从文件加载自动测试功能参数
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, filename);
                if (!File.Exists(path))
                {
                    Para = new AutoInkPara()
                    {
                        VStep = new float[8],PStep=new float[8]
                    };
                    Save();
                }
                else
                {
                    SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                    doc.Load(path);
                    XmlElement root = doc.DocumentElement;
                    Para = (AutoInkPara)PubFunc.SystemConvertFromXml( root.InnerXml,typeof(AutoInkPara));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 自动测试功能参数存储到文件
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, filename);
                XmlElement root;
                SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                root = doc.CreateElement("", "AllParam", "");
                doc.AppendChild(root);

                string xml = "";
                xml += PubFunc.SystemConvertToXml(Para, typeof(AutoInkPara));
                root.InnerXml = xml;
                doc.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 启用自动墨水测试功能
        /// </summary>
        public static void Enable()
        {
            Para.Enable = true;
            Save();
        }

        /// <summary>
        /// 关闭自动墨水墨水测试功能
        /// </summary>
        public static void Disable()
        {
            Para.Enable = false;
            Save();
        }

        /// <summary>
        /// 开始自动测试前备份电压脉宽参数
        /// </summary>
        public static void BakupRealTimeInfo()
        {
            realTimeCurrentInfo = new SRealTimeCurrentInfo();
            if (CoreInterface.GetRealTimeInfo(ref realTimeCurrentInfo, Rmask) != 0)
            {
            }
            else
            {
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.GetRealTimeInfoFail), ResString.GetProductName());
            }
        }

        /// <summary>
        /// 每份打印开始前按照step递增电压脉宽参数
        /// </summary>
        /// <param name="copyIndex"></param>
        public static void SetRealTimeInfoWithStep(int copyIndex)
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

            for (int i = 0; i < realTimeCurrentInfo.cVoltageBase.Length; i++)
            {
                sRT.cVoltageBase[i] = realTimeCurrentInfo.cVoltageBase[i];
            }
            for (int i = 0; i < Para.HeadNum; i++)
            {
                for (int j = 0; j < Para.VolNumPerHead; j++)
                {
                    sRT.cVoltage[i * Para.VolNumPerHead + j] = realTimeCurrentInfo.cVoltage[i * Para.VolNumPerHead + j] + Para.VStep[j] * copyIndex;                    
                }
            }
            for (int i = 0; i < Para.HeadNum; i++)
            {
                for (int j = 0; j < Para.PlusNumPerHead; j++)
                {
                    sRT.cPulseWidth[i * Para.PlusNumPerHead + j] = realTimeCurrentInfo.cPulseWidth[i * Para.PlusNumPerHead + j] + Para.PStep[j] * copyIndex;
                }
            }

            if (CoreInterface.SetRealTimeInfo(ref sRT, Rmask) != 0)
            {
            }
            else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
        }

        /// <summary>
        /// 还原打印前备份的电压脉宽参数
        /// </summary>
        public static void RevertRealTimeInfo()
        {
            if (CoreInterface.SetRealTimeInfo(ref realTimeCurrentInfo, Rmask) != 0)
            {
            }
            else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveRealTimeFail));
        }

        public static void WaitStatusToReady()
        {
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            while (status!=JetStatusEnum.Ready)
            {
                Thread.Sleep(100);
                status = CoreInterface.GetBoardStatus();
            }
        }
    }

}
