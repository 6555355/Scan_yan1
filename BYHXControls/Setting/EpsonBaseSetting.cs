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

using BYHXPrinterManager;
namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for BaseSetting.
	/// </summary>
	public class EpsonBaseSetting : BYHXUserControl//System.Windows.Forms.UserControl
	{
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private const int  M_AUTOINDENT = 56;
		private float m_RealMediaWidth = 0;
		private bool m_bGongzheng = false;
		private bool isDirty = false;

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
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxClean;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownAutoClean;
		private System.Windows.Forms.Label m_LabelAutoClean;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxPrintSetting;
		private System.Windows.Forms.CheckBox m_CheckBoxAutoJumpWhite;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownJobSpace;
		private System.Windows.Forms.Label m_LabelJobSpace;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxZ;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownZSpace;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownThickness;
		private System.Windows.Forms.Label m_Label1;
		private System.Windows.Forms.Label m_LabelZSpace;
		private System.Windows.Forms.Button m_ButtonZAuto;
		private System.Windows.Forms.Button m_ButtonZManual;
		private System.Windows.Forms.CheckBox m_CheckBoxYContinue;
		private System.Windows.Forms.Label m_LabelStepTime;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStepTime;
		private System.Windows.Forms.CheckBox m_CheckBoxMixedType;
		private System.Windows.Forms.CheckBox m_CheckBoxHeightWithImageType;
		private System.Windows.Forms.Label m_LabelMargin;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownMargin;
		private System.Windows.Forms.ComboBox m_ComboBoxFeatherType;
		private System.Windows.Forms.Label label_FeatherType;
		private System.Windows.Forms.CheckBox m_CheckBoxMirror;
		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxSpray;
		private System.Windows.Forms.Label m_LabelAutoSpray;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownM;
		private System.Windows.Forms.Label m_LabelSprayTimes;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownK;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownC;
		private System.Windows.Forms.Label m_LabelSprayCycle;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownY;
		private System.Windows.Forms.Label m_labelPrintPrespraytime;
		private System.Windows.Forms.CheckBox checkBoxAutoSpray;
		private System.Windows.Forms.ComboBox cboAutoCleanWay;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_ComboBoxDiv;
		private System.Windows.Forms.CheckBox m_CheckBoxKillBidir;
		private System.Windows.Forms.ComboBox cbo_MultipleInk;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;

		public EpsonBaseSetting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			ToolTipInit();
			
//			m_NmericUpDownWaveLen.Location = m_NumericUpDownFeatherPercent.Location;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpsonBaseSetting));
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
            this.m_LabelLeftEdge = new System.Windows.Forms.Label();
            this.m_NumericUpDownLeftEdge = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.m_LabelWidth = new System.Windows.Forms.Label();
            this.m_GroupBoxInkStripe = new BYHXPrinterManager.GradientControls.Grouper();
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
            this.m_LabelMargin = new System.Windows.Forms.Label();
            this.m_NumericUpDownMargin = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxClean = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxAutoSpray = new System.Windows.Forms.CheckBox();
            this.cboAutoCleanWay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_NumericUpDownAutoClean = new System.Windows.Forms.NumericUpDown();
            this.m_LabelAutoClean = new System.Windows.Forms.Label();
            this.m_GroupBoxPrintSetting = new BYHXPrinterManager.GradientControls.Grouper();
            this.cbo_MultipleInk = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_ComboBoxDiv = new System.Windows.Forms.ComboBox();
            this.m_CheckBoxMirror = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxFeatherType = new System.Windows.Forms.ComboBox();
            this.label_FeatherType = new System.Windows.Forms.Label();
            this.m_CheckBoxAutoJumpWhite = new System.Windows.Forms.CheckBox();
            this.m_NumericUpDownJobSpace = new System.Windows.Forms.NumericUpDown();
            this.m_LabelJobSpace = new System.Windows.Forms.Label();
            this.m_CheckBoxYContinue = new System.Windows.Forms.CheckBox();
            this.m_LabelStepTime = new System.Windows.Forms.Label();
            this.m_NumericUpDownStepTime = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxZ = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_NumericUpDownZSpace = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownThickness = new System.Windows.Forms.NumericUpDown();
            this.m_Label1 = new System.Windows.Forms.Label();
            this.m_LabelZSpace = new System.Windows.Forms.Label();
            this.m_ButtonZAuto = new System.Windows.Forms.Button();
            this.m_ButtonZManual = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_GroupBoxSpray = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_LabelAutoSpray = new System.Windows.Forms.Label();
            this.m_NumericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.m_LabelSprayTimes = new System.Windows.Forms.Label();
            this.m_NumericUpDownK = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDownC = new System.Windows.Forms.NumericUpDown();
            this.m_LabelSprayCycle = new System.Windows.Forms.Label();
            this.m_NumericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.m_labelPrintPrespraytime = new System.Windows.Forms.Label();
            this.m_CheckBoxKillBidir = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).BeginInit();
            this.m_GroupBoxInkStripe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).BeginInit();
            this.m_GroupBoxMedia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).BeginInit();
            this.m_GroupBoxClean.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).BeginInit();
            this.m_GroupBoxPrintSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).BeginInit();
            this.m_GroupBoxZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).BeginInit();
            this.m_GroupBoxSpray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).BeginInit();
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
            this.m_NumericUpDownLeftEdge.ValueChanged += new System.EventHandler(this.m_NumericUpDownLeftEdge_ValueChanged);
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
            this.m_GroupBoxInkStripe.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxInkStripe.BorderThickness = 1F;
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxNormalType);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_ComboBoxPlace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_NumericUpDownStripeSpace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_NumericUpDownStripeWidth);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripeWidth);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripeSpace);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_LabelStripePos);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxMixedType);
            this.m_GroupBoxInkStripe.Controls.Add(this.m_CheckBoxHeightWithImageType);
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxInkStripe.GradientColors = style1;
            this.m_GroupBoxInkStripe.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxInkStripe, "m_GroupBoxInkStripe");
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
            // m_CheckBoxNormalType
            // 
            this.m_CheckBoxNormalType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxNormalType, "m_CheckBoxNormalType");
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
            this.m_CheckBoxMixedType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxMixedType, "m_CheckBoxMixedType");
            this.m_CheckBoxMixedType.Name = "m_CheckBoxMixedType";
            this.m_CheckBoxMixedType.UseVisualStyleBackColor = false;
            this.m_CheckBoxMixedType.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxHeightWithImageType
            // 
            this.m_CheckBoxHeightWithImageType.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxHeightWithImageType, "m_CheckBoxHeightWithImageType");
            this.m_CheckBoxHeightWithImageType.Name = "m_CheckBoxHeightWithImageType";
            this.m_CheckBoxHeightWithImageType.UseVisualStyleBackColor = false;
            this.m_CheckBoxHeightWithImageType.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_GroupBoxMedia
            // 
            this.m_GroupBoxMedia.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxMedia.BorderThickness = 1F;
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownLeftEdge);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownWidth);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelWidth);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelLeftEdge);
            this.m_GroupBoxMedia.Controls.Add(this.m_LabelMargin);
            this.m_GroupBoxMedia.Controls.Add(this.m_NumericUpDownMargin);
            style3.Color1 = System.Drawing.SystemColors.Control;
            style3.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxMedia.GradientColors = style3;
            this.m_GroupBoxMedia.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxMedia, "m_GroupBoxMedia");
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
            // m_GroupBoxClean
            // 
            this.m_GroupBoxClean.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxClean.BorderThickness = 1F;
            this.m_GroupBoxClean.Controls.Add(this.checkBoxAutoSpray);
            this.m_GroupBoxClean.Controls.Add(this.cboAutoCleanWay);
            this.m_GroupBoxClean.Controls.Add(this.label1);
            this.m_GroupBoxClean.Controls.Add(this.m_NumericUpDownAutoClean);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelAutoClean);
            style5.Color1 = System.Drawing.SystemColors.Control;
            style5.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxClean.GradientColors = style5;
            this.m_GroupBoxClean.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxClean, "m_GroupBoxClean");
            this.m_GroupBoxClean.Name = "m_GroupBoxClean";
            this.m_GroupBoxClean.PaintGroupBox = false;
            this.m_GroupBoxClean.RoundCorners = 10;
            this.m_GroupBoxClean.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxClean.ShadowControl = false;
            this.m_GroupBoxClean.ShadowThickness = 3;
            this.m_GroupBoxClean.TabStop = false;
            style6.Color1 = System.Drawing.Color.LightBlue;
            style6.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxClean.TitileGradientColors = style6;
            this.m_GroupBoxClean.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxClean.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxAutoSpray
            // 
            this.checkBoxAutoSpray.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxAutoSpray, "checkBoxAutoSpray");
            this.checkBoxAutoSpray.Name = "checkBoxAutoSpray";
            this.checkBoxAutoSpray.UseVisualStyleBackColor = false;
            // 
            // cboAutoCleanWay
            // 
            this.cboAutoCleanWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cboAutoCleanWay, "cboAutoCleanWay");
            this.cboAutoCleanWay.Name = "cboAutoCleanWay";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
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
            this.m_NumericUpDownAutoClean.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelAutoClean
            // 
            resources.ApplyResources(this.m_LabelAutoClean, "m_LabelAutoClean");
            this.m_LabelAutoClean.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoClean.Name = "m_LabelAutoClean";
            // 
            // m_GroupBoxPrintSetting
            // 
            this.m_GroupBoxPrintSetting.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxPrintSetting.BorderThickness = 1F;
            this.m_GroupBoxPrintSetting.Controls.Add(this.cbo_MultipleInk);
            this.m_GroupBoxPrintSetting.Controls.Add(this.label3);
            this.m_GroupBoxPrintSetting.Controls.Add(this.label2);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_ComboBoxDiv);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxMirror);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_ComboBoxFeatherType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.label_FeatherType);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxAutoJumpWhite);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownJobSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelJobSpace);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_CheckBoxYContinue);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_LabelStepTime);
            this.m_GroupBoxPrintSetting.Controls.Add(this.m_NumericUpDownStepTime);
            style7.Color1 = System.Drawing.SystemColors.Control;
            style7.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxPrintSetting.GradientColors = style7;
            this.m_GroupBoxPrintSetting.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxPrintSetting, "m_GroupBoxPrintSetting");
            this.m_GroupBoxPrintSetting.Name = "m_GroupBoxPrintSetting";
            this.m_GroupBoxPrintSetting.PaintGroupBox = false;
            this.m_GroupBoxPrintSetting.RoundCorners = 10;
            this.m_GroupBoxPrintSetting.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxPrintSetting.ShadowControl = false;
            this.m_GroupBoxPrintSetting.ShadowThickness = 3;
            this.m_GroupBoxPrintSetting.TabStop = false;
            style8.Color1 = System.Drawing.Color.LightBlue;
            style8.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxPrintSetting.TitileGradientColors = style8;
            this.m_GroupBoxPrintSetting.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxPrintSetting.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // m_ComboBoxDiv
            // 
            this.m_ComboBoxDiv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxDiv, "m_ComboBoxDiv");
            this.m_ComboBoxDiv.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxDiv.Items"),
            resources.GetString("m_ComboBoxDiv.Items1"),
            resources.GetString("m_ComboBoxDiv.Items2"),
            resources.GetString("m_ComboBoxDiv.Items3")});
            this.m_ComboBoxDiv.Name = "m_ComboBoxDiv";
            this.m_ComboBoxDiv.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxMirror
            // 
            this.m_CheckBoxMirror.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxMirror, "m_CheckBoxMirror");
            this.m_CheckBoxMirror.Name = "m_CheckBoxMirror";
            this.m_CheckBoxMirror.UseVisualStyleBackColor = false;
            // 
            // m_ComboBoxFeatherType
            // 
            this.m_ComboBoxFeatherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxFeatherType, "m_ComboBoxFeatherType");
            this.m_ComboBoxFeatherType.Name = "m_ComboBoxFeatherType";
            this.m_ComboBoxFeatherType.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxFeatherType_SelectedIndexChanged);
            // 
            // label_FeatherType
            // 
            resources.ApplyResources(this.label_FeatherType, "label_FeatherType");
            this.label_FeatherType.BackColor = System.Drawing.Color.Transparent;
            this.label_FeatherType.Name = "label_FeatherType";
            // 
            // m_CheckBoxAutoJumpWhite
            // 
            this.m_CheckBoxAutoJumpWhite.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxAutoJumpWhite, "m_CheckBoxAutoJumpWhite");
            this.m_CheckBoxAutoJumpWhite.Name = "m_CheckBoxAutoJumpWhite";
            this.m_CheckBoxAutoJumpWhite.UseVisualStyleBackColor = false;
            this.m_CheckBoxAutoJumpWhite.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownJobSpace
            // 
            this.m_NumericUpDownJobSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownJobSpace, "m_NumericUpDownJobSpace");
            this.m_NumericUpDownJobSpace.Name = "m_NumericUpDownJobSpace";
            this.m_NumericUpDownJobSpace.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelJobSpace
            // 
            resources.ApplyResources(this.m_LabelJobSpace, "m_LabelJobSpace");
            this.m_LabelJobSpace.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelJobSpace.Name = "m_LabelJobSpace";
            // 
            // m_CheckBoxYContinue
            // 
            this.m_CheckBoxYContinue.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.m_CheckBoxYContinue, "m_CheckBoxYContinue");
            this.m_CheckBoxYContinue.Name = "m_CheckBoxYContinue";
            this.m_CheckBoxYContinue.UseVisualStyleBackColor = false;
            this.m_CheckBoxYContinue.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelStepTime
            // 
            resources.ApplyResources(this.m_LabelStepTime, "m_LabelStepTime");
            this.m_LabelStepTime.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStepTime.Name = "m_LabelStepTime";
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
            // m_GroupBoxZ
            // 
            this.m_GroupBoxZ.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxZ.BorderThickness = 1F;
            this.m_GroupBoxZ.Controls.Add(this.m_NumericUpDownZSpace);
            this.m_GroupBoxZ.Controls.Add(this.m_NumericUpDownThickness);
            this.m_GroupBoxZ.Controls.Add(this.m_Label1);
            this.m_GroupBoxZ.Controls.Add(this.m_LabelZSpace);
            this.m_GroupBoxZ.Controls.Add(this.m_ButtonZAuto);
            this.m_GroupBoxZ.Controls.Add(this.m_ButtonZManual);
            style9.Color1 = System.Drawing.SystemColors.Control;
            style9.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxZ.GradientColors = style9;
            this.m_GroupBoxZ.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxZ, "m_GroupBoxZ");
            this.m_GroupBoxZ.Name = "m_GroupBoxZ";
            this.m_GroupBoxZ.PaintGroupBox = false;
            this.m_GroupBoxZ.RoundCorners = 10;
            this.m_GroupBoxZ.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxZ.ShadowControl = false;
            this.m_GroupBoxZ.ShadowThickness = 3;
            this.m_GroupBoxZ.TabStop = false;
            style10.Color1 = System.Drawing.Color.LightBlue;
            style10.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxZ.TitileGradientColors = style10;
            this.m_GroupBoxZ.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxZ.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_NumericUpDownZSpace
            // 
            this.m_NumericUpDownZSpace.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownZSpace, "m_NumericUpDownZSpace");
            this.m_NumericUpDownZSpace.Name = "m_NumericUpDownZSpace";
            this.m_NumericUpDownZSpace.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_NumericUpDownThickness
            // 
            this.m_NumericUpDownThickness.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDownThickness, "m_NumericUpDownThickness");
            this.m_NumericUpDownThickness.Name = "m_NumericUpDownThickness";
            this.m_NumericUpDownThickness.ValueChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_Label1
            // 
            resources.ApplyResources(this.m_Label1, "m_Label1");
            this.m_Label1.BackColor = System.Drawing.Color.Transparent;
            this.m_Label1.Name = "m_Label1";
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
            // m_GroupBoxSpray
            // 
            this.m_GroupBoxSpray.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_GroupBoxSpray.BorderThickness = 1F;
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelAutoSpray);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownM);
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelSprayTimes);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownK);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownC);
            this.m_GroupBoxSpray.Controls.Add(this.m_LabelSprayCycle);
            this.m_GroupBoxSpray.Controls.Add(this.m_NumericUpDownY);
            this.m_GroupBoxSpray.Controls.Add(this.m_labelPrintPrespraytime);
            style11.Color1 = System.Drawing.SystemColors.Control;
            style11.Color2 = System.Drawing.Color.Gold;
            this.m_GroupBoxSpray.GradientColors = style11;
            this.m_GroupBoxSpray.GroupImage = null;
            resources.ApplyResources(this.m_GroupBoxSpray, "m_GroupBoxSpray");
            this.m_GroupBoxSpray.Name = "m_GroupBoxSpray";
            this.m_GroupBoxSpray.PaintGroupBox = false;
            this.m_GroupBoxSpray.RoundCorners = 10;
            this.m_GroupBoxSpray.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_GroupBoxSpray.ShadowControl = false;
            this.m_GroupBoxSpray.ShadowThickness = 3;
            this.m_GroupBoxSpray.TabStop = false;
            style12.Color1 = System.Drawing.Color.LightBlue;
            style12.Color2 = System.Drawing.Color.SteelBlue;
            this.m_GroupBoxSpray.TitileGradientColors = style12;
            this.m_GroupBoxSpray.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.m_GroupBoxSpray.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_LabelAutoSpray
            // 
            resources.ApplyResources(this.m_LabelAutoSpray, "m_LabelAutoSpray");
            this.m_LabelAutoSpray.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelAutoSpray.Name = "m_LabelAutoSpray";
            // 
            // m_NumericUpDownM
            // 
            resources.ApplyResources(this.m_NumericUpDownM, "m_NumericUpDownM");
            this.m_NumericUpDownM.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_NumericUpDownM.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownM.Name = "m_NumericUpDownM";
            // 
            // m_LabelSprayTimes
            // 
            resources.ApplyResources(this.m_LabelSprayTimes, "m_LabelSprayTimes");
            this.m_LabelSprayTimes.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelSprayTimes.Name = "m_LabelSprayTimes";
            // 
            // m_NumericUpDownK
            // 
            resources.ApplyResources(this.m_NumericUpDownK, "m_NumericUpDownK");
            this.m_NumericUpDownK.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_NumericUpDownK.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownK.Name = "m_NumericUpDownK";
            // 
            // m_NumericUpDownC
            // 
            resources.ApplyResources(this.m_NumericUpDownC, "m_NumericUpDownC");
            this.m_NumericUpDownC.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_NumericUpDownC.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownC.Name = "m_NumericUpDownC";
            // 
            // m_LabelSprayCycle
            // 
            resources.ApplyResources(this.m_LabelSprayCycle, "m_LabelSprayCycle");
            this.m_LabelSprayCycle.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelSprayCycle.Name = "m_LabelSprayCycle";
            // 
            // m_NumericUpDownY
            // 
            resources.ApplyResources(this.m_NumericUpDownY, "m_NumericUpDownY");
            this.m_NumericUpDownY.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.m_NumericUpDownY.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownY.Name = "m_NumericUpDownY";
            // 
            // m_labelPrintPrespraytime
            // 
            resources.ApplyResources(this.m_labelPrintPrespraytime, "m_labelPrintPrespraytime");
            this.m_labelPrintPrespraytime.BackColor = System.Drawing.Color.Transparent;
            this.m_labelPrintPrespraytime.Name = "m_labelPrintPrespraytime";
            // 
            // m_CheckBoxKillBidir
            // 
            resources.ApplyResources(this.m_CheckBoxKillBidir, "m_CheckBoxKillBidir");
            this.m_CheckBoxKillBidir.Name = "m_CheckBoxKillBidir";
            // 
            // EpsonBaseSetting
            // 
            this.Controls.Add(this.m_CheckBoxKillBidir);
            this.Controls.Add(this.m_GroupBoxSpray);
            this.Controls.Add(this.m_GroupBoxInkStripe);
            this.Controls.Add(this.m_GroupBoxClean);
            this.Controls.Add(this.m_GroupBoxPrintSetting);
            this.Controls.Add(this.m_GroupBoxMedia);
            this.Controls.Add(this.m_GroupBoxZ);
            resources.ApplyResources(this, "$this");
            this.Name = "EpsonBaseSetting";
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownLeftEdge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWidth)).EndInit();
            this.m_GroupBoxInkStripe.ResumeLayout(false);
            this.m_GroupBoxInkStripe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStripeWidth)).EndInit();
            this.m_GroupBoxMedia.ResumeLayout(false);
            this.m_GroupBoxMedia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownMargin)).EndInit();
            this.m_GroupBoxClean.ResumeLayout(false);
            this.m_GroupBoxClean.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownAutoClean)).EndInit();
            this.m_GroupBoxPrintSetting.ResumeLayout(false);
            this.m_GroupBoxPrintSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownJobSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStepTime)).EndInit();
            this.m_GroupBoxZ.ResumeLayout(false);
            this.m_GroupBoxZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownZSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownThickness)).EndInit();
            this.m_GroupBoxSpray.ResumeLayout(false);
            this.m_GroupBoxSpray.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownY)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
			bool bEnabled = (status == JetStatusEnum.Ready);
			m_ButtonZAuto.Enabled = bEnabled;
			m_ButtonZManual.Enabled = bEnabled;
			bEnabled = (status == JetStatusEnum.Ready || status == JetStatusEnum.Spraying );
            this.isDirty = false;

		}
		private void ToolTipInit()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseSetting));

			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownAutoClean.ToolTip"),this.m_NumericUpDownAutoClean,this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownStepTime.ToolTip"),this.m_NumericUpDownStepTime,this.m_ToolTip);

//			UIPreference.NumericUpDownToolTip(resources.GetString("m_NumericUpDownFeatherPercent.ToolTip"),this.m_NumericUpDownFeatherPercent,this.m_ToolTip);
//			UIPreference.NumericUpDownToolTip(resources.GetString("m_NmericUpDownWaveLen.ToolTip"),this.m_NmericUpDownWaveLen,this.m_ToolTip);
		}

	    private SPrinterProperty _sPrinterProperty;
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
		    _sPrinterProperty = sp;
			m_NumericUpDownLeftEdge.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
			m_NumericUpDownLeftEdge.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperWidth));
			m_NumericUpDownWidth.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
			m_NumericUpDownWidth.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperWidth));

			
			m_NumericUpDownMargin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,-sp.fMaxPaperWidth/2));
			m_NumericUpDownMargin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperWidth/2));
	
//			m_NumericUpDownY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
//			m_NumericUpDownY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperHeight));

//			m_NmericUpDownWaveLen.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0.39f));//一厘米
//			m_NmericUpDownWaveLen.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,4.0f));//十厘米

			m_ComboBoxPlace.Items.Clear();
			foreach(InkStrPosEnum place in Enum.GetValues(typeof(InkStrPosEnum)))
			{
				string cmode = ResString.GetEnumDisplayName(typeof(InkStrPosEnum),place);
				//if(place != InkStrPosEnum.None)
				m_ComboBoxPlace.Items.Add(cmode);
			}

			m_ComboBoxFeatherType.Items.Clear();
			uint uiHtype = 0;
			CoreInterface.GetUIHeadType(ref uiHtype);
			m_bGongzheng = (uiHtype&0x20)!=0;
			if(m_bGongzheng)
			{
				label_FeatherType.Text = ResString.GetResString("EpsonFeatherType_Label");
				m_ComboBoxFeatherType.Items.Add(ResString.GetResString("EpsonFeatherType_GZ_LOW"));
				m_ComboBoxFeatherType.Items.Add(ResString.GetResString("EpsonFeatherType_GZ_MID"));
				m_ComboBoxFeatherType.Items.Add(ResString.GetResString("EpsonFeatherType_GZ_HIGH"));
			}
			else
			{
				foreach (EpsonFeatherType place in Enum.GetValues(typeof(EpsonFeatherType)))
				{
					string cmode = ResString.GetEnumDisplayName(typeof(EpsonFeatherType), place);
					m_ComboBoxFeatherType.Items.Add(cmode);
				}
			}
			cboAutoCleanWay.Items.Clear();
			foreach (EpsonAutoCleanWay place in Enum.GetValues(typeof(EpsonAutoCleanWay)))
			{
				string cmode = ResString.GetEnumDisplayName(typeof(EpsonAutoCleanWay), place);
				cboAutoCleanWay.Items.Add(cmode);
			}

			this.m_ComboBoxDiv.Items.Clear();
            bool isSuperUser = (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser);
			foreach (XResDivMode place in Enum.GetValues(typeof(XResDivMode)))
			{
				string cmode = ResString.GetEnumDisplayName(typeof(XResDivMode), place);
                if ((!isSuperUser && place < XResDivMode.PrintMode3) || isSuperUser)
					m_ComboBoxDiv.Items.Add(cmode);
			}
			
            //cbo_MultipleInk.Items.Clear();
            //foreach (MultipleInkEnum place in Enum.GetValues(typeof(MultipleInkEnum)))
            //{
            //    string cmode = ResString.GetEnumDisplayName(typeof(MultipleInkEnum), place);
            //    cbo_MultipleInk.Items.Add(cmode);
            //}

			//右侧列布局
            bool bZMeasurSensorSupport = sp.IsZMeasurSupport;//PubFunc.IsVender92();
			m_GroupBoxZ.Visible =sp.bSupportZMotion && !bZMeasurSensorSupport;
            //左侧列布局
            if(sp.nMediaType == 0)
            {
                m_CheckBoxYContinue.Visible = false;
            }
            else
            {
                m_CheckBoxYContinue.Visible = true;
            }
			bool isMicolorEpson = SPrinterProperty.IsEpson(sp.ePrinterHead) && PubFunc.IsMicolorOrAllwin();
			m_LabelAutoClean.Visible = m_NumericUpDownAutoClean.Visible = !isMicolorEpson;
			label1.Visible = cboAutoCleanWay.Visible =	!isMicolorEpson;
			checkBoxAutoSpray.Visible =	sp.IsSurportAutoSpray();
			if(isMicolorEpson)
				checkBoxAutoSpray.Location = m_LabelAutoClean.Location;

            this.isDirty = false;
		}

		public void OnPrinterSettingChange( SPrinterSetting ss,EpsonExAllParam epsonAllparam)
		{
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStripeSpace,m_CurrentUnit,ss.sBaseSetting.sStripeSetting.fStripeOffset);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStripeWidth,m_CurrentUnit,ss.sBaseSetting.sStripeSetting.fStripeWidth);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownJobSpace,m_CurrentUnit,ss.sBaseSetting.fJobSpace);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownStepTime,ss.sBaseSetting.fStepTime);
//			m_NumericUpDownLeftEdge.Value		=	new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fLeftMargin));
//			Decimal curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fPaperWidth)); 
//			ClampWithMinMax(m_NumericUpDownWidth.Minimum,m_NumericUpDownWidth.Maximum,ref curvalue);
//			m_NumericUpDownWidth.Value			=	curvalue;
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownY,m_CurrentUnit,ss.sBaseSetting.fTopMargin);
			
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownZSpace,m_CurrentUnit,ss.sBaseSetting.fZSpace);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownThickness,m_CurrentUnit,ss.sBaseSetting.fPaperThick);
//			m_NumericUpDownMargin.Value     =  new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fMeasureMargin));

//			m_NumericUpDownAutoClean.Value		=	ss.sCleanSetting.nCleanerPassInterval;
//			m_NumericUpDownFeather.Value		=	ss.sBaseSetting.nFeatherPercent;
			byte type = ss.sBaseSetting.sStripeSetting.bNormalStripeType;
			m_CheckBoxNormalType.Checked		=	((type&((byte)EnumStripeType.Normal)) != 0);
			m_CheckBoxMixedType.Checked         = ((type&((byte)EnumStripeType.ColorMixed)) != 0);
			m_CheckBoxHeightWithImageType.Checked = ((type&((byte)EnumStripeType.HeightWithImage)) != 0);
			m_CheckBoxAutoJumpWhite.Checked		=	(!ss.sBaseSetting.bIgnorePrintWhiteX)&&(!ss.sBaseSetting.bIgnorePrintWhiteY);
//			m_CheckBoxAutoJumpWhite.Enabled      = (m_ComboBoxWhiteInk.SelectedIndex == 0);
			m_CheckBoxYContinue.Checked = ss.sBaseSetting.bYPrintContinue;

			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPlace,(int)ss.sBaseSetting.sStripeSetting.eStripePosition);
//			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxWhiteInk,(int)ss.sBaseSetting.eWhiteInkPrintMode);
//			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownConcentration,ss.sBaseSetting.nWhiteGray*100/255);
		
//            this.m_ComboBoxFeatherType.SelectedIndex = ss.sBaseSetting.nFeatherType;
//			Decimal curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fFeatherWavelength)); 
//			ClampWithMinMax(m_NmericUpDownWaveLen.Minimum,m_NmericUpDownWaveLen.Maximum,ref curvalue);
//			this.m_NmericUpDownWaveLen.Value = curvalue;
//			this.m_NumericUpDownFeatherPercent.Value = ss.sBaseSetting.nAdvanceFeatherPercent;
			this.m_CheckBoxMirror.Checked = ss.sBaseSetting.bMirrorX;
			
			this.OnEpsonCleaningOptionChange(epsonAllparam);

			if(ss.sBaseSetting.nXResutionDiv < (byte)XResDivMode.HighPrecision &&ss.sBaseSetting.nXResutionDiv >(byte)XResDivMode.PrintMode4)
				ss.sBaseSetting.nXResutionDiv = 1;
			UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxDiv,(int)ss.sBaseSetting.nXResutionDiv-1);
			m_CheckBoxKillBidir.Checked =	(ss.nKillBiDirBanding != 0);
			UIPreference.SetSelectIndexAndClampWithMax(this.cbo_MultipleInk,ss.sBaseSetting.multipleInk);

			this.isDirty = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss,ref EpsonExAllParam epsonAllparam)
		{
			ss.sBaseSetting.sStripeSetting.fStripeOffset		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownStripeSpace.Value));
			ss.sBaseSetting.sStripeSetting.fStripeWidth			=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownStripeWidth.Value));	
			ss.sBaseSetting.fJobSpace							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownJobSpace.Value));		
			ss.sBaseSetting.fStepTime							=	Decimal.ToSingle(m_NumericUpDownStepTime.Value);		
			ss.sBaseSetting.fLeftMargin							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownLeftEdge.Value));		
            ss.sBaseSetting.fPaperWidth							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownWidth.Value));			
            //ss.sBaseSetting.fTopMargin							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownY.Value));		
			ss.sBaseSetting.fZSpace							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownZSpace.Value));		
			ss.sBaseSetting.fPaperThick						=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownThickness.Value));			
			ss.sBaseSetting.fMeasureMargin                  =  UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownMargin.Value));
															
//			ss.sCleanSetting.nCleanerPassInterval				=	Decimal.ToInt32(m_NumericUpDownAutoClean.Value);	
			switch(this.m_ComboBoxFeatherType.SelectedIndex)
			{
				case (byte)EpsonFeatherType.None:
					ss.sBaseSetting.nFeatherPercent                     =   0;
					break;
				case (byte)EpsonFeatherType.Small:
					ss.sBaseSetting.nFeatherPercent                     =   20;
					break;
				case (byte)EpsonFeatherType.Medium:
					ss.sBaseSetting.nFeatherPercent                     =   40;
					break;
				case (byte)EpsonFeatherType.Large:
					ss.sBaseSetting.nFeatherPercent                     =   100;
					break;
			}
			//ss.sMoveSetting.nXMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
			//ss.sMoveSetting.nYMoveSpeed							=	Decimal.ToByte(m_NumericUpDownMoveXSpeed.Value);		
			//ss.sMoveSetting.usMotorEncoder						=	Decimal.ToUInt16(m_NumericUpDownEncoder.Value);		
																													
			byte NormalType = 0;
			if(m_CheckBoxNormalType.Checked)
			{
				NormalType |= (byte)EnumStripeType.Normal;
			}
			if(m_CheckBoxMixedType.Checked)
			{
				NormalType |= (byte)EnumStripeType.ColorMixed;
			}
			if(m_CheckBoxHeightWithImageType.Checked)
			{
				NormalType |= (byte)EnumStripeType.HeightWithImage;
			}


			ss.sBaseSetting.sStripeSetting.bNormalStripeType	=	NormalType;		
			ss.sBaseSetting.bIgnorePrintWhiteX					=
			ss.sBaseSetting.bIgnorePrintWhiteY					=	!m_CheckBoxAutoJumpWhite.Checked;		
			ss.sBaseSetting.bYPrintContinue					=	m_CheckBoxYContinue.Checked;			
																												
			ss.sBaseSetting.sStripeSetting.eStripePosition		=	(InkStrPosEnum)m_ComboBoxPlace.SelectedIndex;		
//			ss.sBaseSetting.eWhiteInkPrintMode = (WhiteInkPrintMode)m_ComboBoxWhiteInk.SelectedIndex;
//			byte val = (byte)(m_NumericUpDownConcentration.Value*255/100);
//			if(0<val && val < 255)
//				 val+=1;
//			ss.sBaseSetting.nWhiteGray = val;

            ss.sBaseSetting.nFeatherType = (byte)FeatherType.Gradient;// (byte)this.m_ComboBoxFeatherType.SelectedIndex;
//			ss.sBaseSetting.fFeatherWavelength = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.m_NmericUpDownWaveLen.Value));
//			ss.sBaseSetting.nAdvanceFeatherPercent = (byte)this.m_NumericUpDownFeatherPercent.Value;
			ss.sBaseSetting.bMirrorX = this.m_CheckBoxMirror.Checked;
			ss.sBaseSetting.nXResutionDiv = (byte)(this.m_ComboBoxDiv.SelectedIndex+1);
			ss.nKillBiDirBanding = m_CheckBoxKillBidir.Checked?1:0;
			ss.sBaseSetting.multipleInk = (byte)this.cbo_MultipleInk.SelectedIndex;
			this.OnGetEpsonCleaningOption(ref epsonAllparam);
		}

		public void OnPreferenceChange( UIPreference up)
		{
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
                this.isDirty = false;
			}
		}
 
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownStripeSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownStripeWidth);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownJobSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownLeftEdge);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownWidth);
//			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownY);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownZSpace);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownThickness);
//			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NmericUpDownWaveLen);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownStripeSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownStripeWidth, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownJobSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownLeftEdge, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownWidth, this.m_ToolTip);
//            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownZSpace, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.m_NumericUpDownThickness, this.m_ToolTip);
//			UIPreference.NumericUpDownToolTip(newUnitdis,this.m_NmericUpDownWaveLen,this.m_ToolTip);
            this.isDirty = false;
		}

		private void m_ButtonMeasure_Click(object sender, System.EventArgs e)
		{
			CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,0);
		}

		private void m_ButtonZAuto_Click(object sender, System.EventArgs e)
		{
			float fZSpace							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownZSpace.Value));		
			float fPaperThick						=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownThickness.Value));			
			CoreInterface.MoveZ(01,fZSpace,fPaperThick);
		}

		private void m_ButtonZManual_Click(object sender, System.EventArgs e)
		{
			float fZSpace							=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownZSpace.Value));		
			float fPaperThick						=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownThickness.Value));			
			CoreInterface.MoveZ(02,fZSpace,fPaperThick);
		}

        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }

		private void m_ComboBoxFeatherType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			this.m_NumericUpDownFeatherPercent.Visible = m_ComboBoxFeatherType.SelectedIndex==3;
//			this.m_NmericUpDownWaveLen.Visible = m_ComboBoxFeatherType.SelectedIndex==2;
			isDirty = true;
		}
		private void OnEpsonCleaningOptionChange( EpsonExAllParam ss)
		{
			m_RealMediaWidth = ss.sUSB_RPT_Media_Info.MediaOrigin + ss.sUSB_RPT_Media_Info.MediaWidth;
			if(m_bGongzheng && ss.sUSB_Print_Quality.PrintQuality > 0)
				UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxFeatherType,ss.sUSB_Print_Quality.PrintQuality - 1);
			else				
				UIPreference.SetSelectIndexAndClampWithMax(this.m_ComboBoxFeatherType,ss.sUSB_Print_Quality.PrintQuality);
			//			m_NumericUpDownFeather.Value = ss.nFeatherPercent;

			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownLeftEdge,m_CurrentUnit,ss.sUSB_RPT_Media_Info.MediaOrigin);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownWidth,m_CurrentUnit,ss.sUSB_RPT_Media_Info.MediaWidth);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownMargin,m_CurrentUnit,ss.sUSB_RPT_Media_Info.Margin);
			
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownK,ss.headParameterPercent[0]);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownC,ss.headParameterPercent[1]);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownM,ss.headParameterPercent[2]);
			UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownY,ss.headParameterPercent[3]);

			CLEANPARA cp = ss.sCLEANPARA;

            if (_sPrinterProperty.EPSONLCD_DEFINED)
				checkBoxAutoSpray.Checked		=	cp.longflash_passInterval ==1;
			else
			{
				UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownAutoClean,cp.autoClean_passInterval);
				UIPreference.SetSelectIndexAndClampWithMax(cboAutoCleanWay,cp.autoClean_way);
			}
			this.isDirty = false;
		}

		private void OnGetEpsonCleaningOption(ref EpsonExAllParam mEpsonExAll)
		{
			if(m_bGongzheng)
				mEpsonExAll.sUSB_Print_Quality.PrintQuality = this.m_ComboBoxFeatherType.SelectedIndex + 1;
			else
				mEpsonExAll.sUSB_Print_Quality.PrintQuality = this.m_ComboBoxFeatherType.SelectedIndex;
			//			mEpsonExAll.nFeatherPercent = (int)m_NumericUpDownFeather.Value;

			mEpsonExAll.sUSB_RPT_MainUI_Param.PrintOrigin = mEpsonExAll.sUSB_RPT_Media_Info.MediaOrigin		=	UIPreference.ToInchLength(m_CurrentUnit,(float)m_NumericUpDownLeftEdge.Value);
			mEpsonExAll.sUSB_RPT_Media_Info.MediaWidth			=	UIPreference.ToInchLength(m_CurrentUnit,(float)m_NumericUpDownWidth.Value);
			mEpsonExAll.sUSB_RPT_Media_Info.Margin =  UIPreference.ToInchLength(m_CurrentUnit,(float)m_NumericUpDownMargin.Value);
			
			mEpsonExAll.headParameterPercent[0]		=  (sbyte)m_NumericUpDownK.Value;
			mEpsonExAll.headParameterPercent[1]		=  (sbyte)m_NumericUpDownC.Value;
			mEpsonExAll.headParameterPercent[2]		=  (sbyte)m_NumericUpDownM.Value;
			mEpsonExAll.headParameterPercent[3]     =  (sbyte)m_NumericUpDownY.Value;

            if (_sPrinterProperty.EPSONLCD_DEFINED)
			{
				mEpsonExAll.sCLEANPARA.longflash_passInterval  = (byte)(this.checkBoxAutoSpray.Checked ? 1:0);
				mEpsonExAll.sCLEANPARA.autoClean_way = (byte)cboAutoCleanWay.SelectedIndex;
			}
			else
				mEpsonExAll.sCLEANPARA.autoClean_passInterval = (byte)m_NumericUpDownAutoClean.Value;

		}

		private void m_NumericUpDownLeftEdge_ValueChanged(object sender, System.EventArgs e)
		{
			if((sender as NumericUpDown).Name == this.m_NumericUpDownLeftEdge.Name)
			this.m_NumericUpDownWidth.Value = new decimal( UIPreference.ToDisplayLength(m_CurrentUnit ,this.m_RealMediaWidth - UIPreference.ToInchLength(m_CurrentUnit ,(float)this.m_NumericUpDownLeftEdge.Value)));
		
		}		

	}
}
