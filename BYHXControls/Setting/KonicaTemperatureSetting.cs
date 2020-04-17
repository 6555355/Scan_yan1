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

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for KonicaTemperatureSetting.
	/// </summary>
	public class KonicaTemperatureSetting : BYHXUserControl//System.Windows.Forms.UserControl
	{
		private const int MAX_CHANAL = 16;
		private int m_HeadNum =0;
		private byte m_StartHeadIndex = 0;
		private byte[] m_pMap;

		private System.Windows.Forms.Label   []m_LabelHorHeadIndex;
		private System.Windows.Forms.TextBox []m_TextBoxHeadSet;
		private System.Windows.Forms.TextBox []m_TextBoxHeadCur;
		private System.Windows.Forms.TextBox []m_TextBoxVoltageSet;
		private System.Windows.Forms.TextBox []m_TextBoxVoltageCur;
		private System.Windows.Forms.TextBox []m_TextBoxVoltageBase;
		//private System.Windows.Forms.TextBox []m_TextBoxPulseWidthSet;
		//private System.Windows.Forms.TextBox []m_TextBoxPulseWidthCur;

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////


		private BYHXPrinterManager.GradientControls.Grouper m_GroupBoxTemperature;
		private System.Windows.Forms.TextBox m_TextBoxLeftSample;
		private System.Windows.Forms.Label m_LabelLeft;
		private System.Windows.Forms.Label m_LabelRight;
		private System.Windows.Forms.Label m_LabelHead;
		private System.Windows.Forms.Label m_LabelHorSample;
		private System.Windows.Forms.TextBox m_TextBoxRightSample;
		private System.Windows.Forms.Label m_LabelTankSet;
		private System.Windows.Forms.Label m_LabelTankCur;
		private System.Windows.Forms.TextBox m_TextBoxPulseWidthCurSample;
		private System.Windows.Forms.TextBox m_TextBoxPulseWidthSetSample;
		private System.Windows.Forms.Button m_ButtonRefresh;
		private System.Windows.Forms.Label m_labelVolSet;
		private System.Windows.Forms.Label m_labelVolCur;
		private System.Windows.Forms.TextBox m_TextBoxVoltageCurSample;
		private System.Windows.Forms.TextBox m_TextBoxVoltageSetSample;
		private System.Windows.Forms.Label m_LabelBaseVol;
		private System.Windows.Forms.TextBox m_TextBoxVoltageBaseSample;
		private System.Windows.Forms.Button m_ButtonToBoard;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public KonicaTemperatureSetting()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(KonicaTemperatureSetting));
			this.m_GroupBoxTemperature = new BYHXPrinterManager.GradientControls.Grouper();
			this.m_TextBoxLeftSample = new System.Windows.Forms.TextBox();
			this.m_LabelLeft = new System.Windows.Forms.Label();
			this.m_LabelRight = new System.Windows.Forms.Label();
			this.m_LabelHead = new System.Windows.Forms.Label();
			this.m_LabelHorSample = new System.Windows.Forms.Label();
			this.m_TextBoxRightSample = new System.Windows.Forms.TextBox();
			this.m_LabelTankSet = new System.Windows.Forms.Label();
			this.m_LabelTankCur = new System.Windows.Forms.Label();
			this.m_TextBoxPulseWidthCurSample = new System.Windows.Forms.TextBox();
			this.m_TextBoxPulseWidthSetSample = new System.Windows.Forms.TextBox();
			this.m_labelVolSet = new System.Windows.Forms.Label();
			this.m_labelVolCur = new System.Windows.Forms.Label();
			this.m_TextBoxVoltageCurSample = new System.Windows.Forms.TextBox();
			this.m_TextBoxVoltageSetSample = new System.Windows.Forms.TextBox();
			this.m_LabelBaseVol = new System.Windows.Forms.Label();
			this.m_TextBoxVoltageBaseSample = new System.Windows.Forms.TextBox();
			this.m_ButtonRefresh = new System.Windows.Forms.Button();
			this.m_ButtonToBoard = new System.Windows.Forms.Button();
			this.m_GroupBoxTemperature.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_GroupBoxTemperature
			// 
			this.m_GroupBoxTemperature.AccessibleDescription = resources.GetString("m_GroupBoxTemperature.AccessibleDescription");
			this.m_GroupBoxTemperature.AccessibleName = resources.GetString("m_GroupBoxTemperature.AccessibleName");
			this.m_GroupBoxTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_GroupBoxTemperature.Anchor")));
			this.m_GroupBoxTemperature.BackgroundGradientMode = BYHXPrinterManager.GradientControls.Grouper.GroupBoxGradientMode.Vertical;
			this.m_GroupBoxTemperature.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_GroupBoxTemperature.BackgroundImage")));
			this.m_GroupBoxTemperature.BorderColor = System.Drawing.Color.Black;
			this.m_GroupBoxTemperature.BorderThickness = 1F;
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxLeftSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelLeft);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelRight);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHead);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelHorSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxRightSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelTankSet);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelTankCur);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxPulseWidthCurSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxPulseWidthSetSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_labelVolSet);
			this.m_GroupBoxTemperature.Controls.Add(this.m_labelVolCur);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageCurSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageSetSample);
			this.m_GroupBoxTemperature.Controls.Add(this.m_LabelBaseVol);
			this.m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageBaseSample);
			this.m_GroupBoxTemperature.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_GroupBoxTemperature.Dock")));
			this.m_GroupBoxTemperature.Enabled = ((bool)(resources.GetObject("m_GroupBoxTemperature.Enabled")));
			this.m_GroupBoxTemperature.Font = ((System.Drawing.Font)(resources.GetObject("m_GroupBoxTemperature.Font")));
			this.m_GroupBoxTemperature.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.Gold);
			this.m_GroupBoxTemperature.GroupImage = null;
			this.m_GroupBoxTemperature.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_GroupBoxTemperature.ImeMode")));
			this.m_GroupBoxTemperature.Location = ((System.Drawing.Point)(resources.GetObject("m_GroupBoxTemperature.Location")));
			this.m_GroupBoxTemperature.Name = "m_GroupBoxTemperature";
			this.m_GroupBoxTemperature.PaintGroupBox = false;
			this.m_GroupBoxTemperature.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_GroupBoxTemperature.RightToLeft")));
			this.m_GroupBoxTemperature.RoundCorners = 10;
			this.m_GroupBoxTemperature.ShadowColor = System.Drawing.Color.DarkGray;
			this.m_GroupBoxTemperature.ShadowControl = false;
			this.m_GroupBoxTemperature.ShadowThickness = 3;
			this.m_GroupBoxTemperature.Size = ((System.Drawing.Size)(resources.GetObject("m_GroupBoxTemperature.Size")));
			this.m_GroupBoxTemperature.TabIndex = ((int)(resources.GetObject("m_GroupBoxTemperature.TabIndex")));
			this.m_GroupBoxTemperature.TabStop = false;
			this.m_GroupBoxTemperature.Text = resources.GetString("m_GroupBoxTemperature.Text");
			this.m_GroupBoxTemperature.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.m_GroupBoxTemperature.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
			this.m_GroupBoxTemperature.Visible = ((bool)(resources.GetObject("m_GroupBoxTemperature.Visible")));
			// 
			// m_TextBoxLeftSample
			// 
			this.m_TextBoxLeftSample.AccessibleDescription = resources.GetString("m_TextBoxLeftSample.AccessibleDescription");
			this.m_TextBoxLeftSample.AccessibleName = resources.GetString("m_TextBoxLeftSample.AccessibleName");
			this.m_TextBoxLeftSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxLeftSample.Anchor")));
			this.m_TextBoxLeftSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxLeftSample.AutoSize")));
			this.m_TextBoxLeftSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxLeftSample.BackgroundImage")));
			this.m_TextBoxLeftSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxLeftSample.Dock")));
			this.m_TextBoxLeftSample.Enabled = ((bool)(resources.GetObject("m_TextBoxLeftSample.Enabled")));
			this.m_TextBoxLeftSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxLeftSample.Font")));
			this.m_TextBoxLeftSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxLeftSample.ImeMode")));
			this.m_TextBoxLeftSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxLeftSample.Location")));
			this.m_TextBoxLeftSample.MaxLength = ((int)(resources.GetObject("m_TextBoxLeftSample.MaxLength")));
			this.m_TextBoxLeftSample.Multiline = ((bool)(resources.GetObject("m_TextBoxLeftSample.Multiline")));
			this.m_TextBoxLeftSample.Name = "m_TextBoxLeftSample";
			this.m_TextBoxLeftSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxLeftSample.PasswordChar")));
			this.m_TextBoxLeftSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxLeftSample.RightToLeft")));
			this.m_TextBoxLeftSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxLeftSample.ScrollBars")));
			this.m_TextBoxLeftSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxLeftSample.Size")));
			this.m_TextBoxLeftSample.TabIndex = ((int)(resources.GetObject("m_TextBoxLeftSample.TabIndex")));
			this.m_TextBoxLeftSample.Text = resources.GetString("m_TextBoxLeftSample.Text");
			this.m_TextBoxLeftSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxLeftSample.TextAlign")));
			this.m_TextBoxLeftSample.Visible = ((bool)(resources.GetObject("m_TextBoxLeftSample.Visible")));
			this.m_TextBoxLeftSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxLeftSample.WordWrap")));
			this.m_TextBoxLeftSample.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
			// 
			// m_LabelLeft
			// 
			this.m_LabelLeft.AccessibleDescription = resources.GetString("m_LabelLeft.AccessibleDescription");
			this.m_LabelLeft.AccessibleName = resources.GetString("m_LabelLeft.AccessibleName");
			this.m_LabelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelLeft.Anchor")));
			this.m_LabelLeft.AutoSize = ((bool)(resources.GetObject("m_LabelLeft.AutoSize")));
			this.m_LabelLeft.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelLeft.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelLeft.Dock")));
			this.m_LabelLeft.Enabled = ((bool)(resources.GetObject("m_LabelLeft.Enabled")));
			this.m_LabelLeft.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelLeft.Font")));
			this.m_LabelLeft.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelLeft.Image")));
			this.m_LabelLeft.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelLeft.ImageAlign")));
			this.m_LabelLeft.ImageIndex = ((int)(resources.GetObject("m_LabelLeft.ImageIndex")));
			this.m_LabelLeft.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelLeft.ImeMode")));
			this.m_LabelLeft.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelLeft.Location")));
			this.m_LabelLeft.Name = "m_LabelLeft";
			this.m_LabelLeft.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelLeft.RightToLeft")));
			this.m_LabelLeft.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelLeft.Size")));
			this.m_LabelLeft.TabIndex = ((int)(resources.GetObject("m_LabelLeft.TabIndex")));
			this.m_LabelLeft.Text = resources.GetString("m_LabelLeft.Text");
			this.m_LabelLeft.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelLeft.TextAlign")));
			this.m_LabelLeft.Visible = ((bool)(resources.GetObject("m_LabelLeft.Visible")));
			// 
			// m_LabelRight
			// 
			this.m_LabelRight.AccessibleDescription = resources.GetString("m_LabelRight.AccessibleDescription");
			this.m_LabelRight.AccessibleName = resources.GetString("m_LabelRight.AccessibleName");
			this.m_LabelRight.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelRight.Anchor")));
			this.m_LabelRight.AutoSize = ((bool)(resources.GetObject("m_LabelRight.AutoSize")));
			this.m_LabelRight.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelRight.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelRight.Dock")));
			this.m_LabelRight.Enabled = ((bool)(resources.GetObject("m_LabelRight.Enabled")));
			this.m_LabelRight.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelRight.Font")));
			this.m_LabelRight.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelRight.Image")));
			this.m_LabelRight.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelRight.ImageAlign")));
			this.m_LabelRight.ImageIndex = ((int)(resources.GetObject("m_LabelRight.ImageIndex")));
			this.m_LabelRight.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelRight.ImeMode")));
			this.m_LabelRight.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelRight.Location")));
			this.m_LabelRight.Name = "m_LabelRight";
			this.m_LabelRight.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelRight.RightToLeft")));
			this.m_LabelRight.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelRight.Size")));
			this.m_LabelRight.TabIndex = ((int)(resources.GetObject("m_LabelRight.TabIndex")));
			this.m_LabelRight.Text = resources.GetString("m_LabelRight.Text");
			this.m_LabelRight.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelRight.TextAlign")));
			this.m_LabelRight.Visible = ((bool)(resources.GetObject("m_LabelRight.Visible")));
			// 
			// m_LabelHead
			// 
			this.m_LabelHead.AccessibleDescription = resources.GetString("m_LabelHead.AccessibleDescription");
			this.m_LabelHead.AccessibleName = resources.GetString("m_LabelHead.AccessibleName");
			this.m_LabelHead.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelHead.Anchor")));
			this.m_LabelHead.AutoSize = ((bool)(resources.GetObject("m_LabelHead.AutoSize")));
			this.m_LabelHead.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelHead.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelHead.Dock")));
			this.m_LabelHead.Enabled = ((bool)(resources.GetObject("m_LabelHead.Enabled")));
			this.m_LabelHead.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelHead.Font")));
			this.m_LabelHead.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelHead.Image")));
			this.m_LabelHead.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelHead.ImageAlign")));
			this.m_LabelHead.ImageIndex = ((int)(resources.GetObject("m_LabelHead.ImageIndex")));
			this.m_LabelHead.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelHead.ImeMode")));
			this.m_LabelHead.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelHead.Location")));
			this.m_LabelHead.Name = "m_LabelHead";
			this.m_LabelHead.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelHead.RightToLeft")));
			this.m_LabelHead.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelHead.Size")));
			this.m_LabelHead.TabIndex = ((int)(resources.GetObject("m_LabelHead.TabIndex")));
			this.m_LabelHead.Text = resources.GetString("m_LabelHead.Text");
			this.m_LabelHead.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelHead.TextAlign")));
			this.m_LabelHead.Visible = ((bool)(resources.GetObject("m_LabelHead.Visible")));
			// 
			// m_LabelHorSample
			// 
			this.m_LabelHorSample.AccessibleDescription = resources.GetString("m_LabelHorSample.AccessibleDescription");
			this.m_LabelHorSample.AccessibleName = resources.GetString("m_LabelHorSample.AccessibleName");
			this.m_LabelHorSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelHorSample.Anchor")));
			this.m_LabelHorSample.AutoSize = ((bool)(resources.GetObject("m_LabelHorSample.AutoSize")));
			this.m_LabelHorSample.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelHorSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelHorSample.Dock")));
			this.m_LabelHorSample.Enabled = ((bool)(resources.GetObject("m_LabelHorSample.Enabled")));
			this.m_LabelHorSample.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelHorSample.Font")));
			this.m_LabelHorSample.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelHorSample.Image")));
			this.m_LabelHorSample.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelHorSample.ImageAlign")));
			this.m_LabelHorSample.ImageIndex = ((int)(resources.GetObject("m_LabelHorSample.ImageIndex")));
			this.m_LabelHorSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelHorSample.ImeMode")));
			this.m_LabelHorSample.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelHorSample.Location")));
			this.m_LabelHorSample.Name = "m_LabelHorSample";
			this.m_LabelHorSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelHorSample.RightToLeft")));
			this.m_LabelHorSample.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelHorSample.Size")));
			this.m_LabelHorSample.TabIndex = ((int)(resources.GetObject("m_LabelHorSample.TabIndex")));
			this.m_LabelHorSample.Text = resources.GetString("m_LabelHorSample.Text");
			this.m_LabelHorSample.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelHorSample.TextAlign")));
			this.m_LabelHorSample.Visible = ((bool)(resources.GetObject("m_LabelHorSample.Visible")));
			// 
			// m_TextBoxRightSample
			// 
			this.m_TextBoxRightSample.AccessibleDescription = resources.GetString("m_TextBoxRightSample.AccessibleDescription");
			this.m_TextBoxRightSample.AccessibleName = resources.GetString("m_TextBoxRightSample.AccessibleName");
			this.m_TextBoxRightSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxRightSample.Anchor")));
			this.m_TextBoxRightSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxRightSample.AutoSize")));
			this.m_TextBoxRightSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxRightSample.BackgroundImage")));
			this.m_TextBoxRightSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxRightSample.Dock")));
			this.m_TextBoxRightSample.Enabled = ((bool)(resources.GetObject("m_TextBoxRightSample.Enabled")));
			this.m_TextBoxRightSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxRightSample.Font")));
			this.m_TextBoxRightSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxRightSample.ImeMode")));
			this.m_TextBoxRightSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxRightSample.Location")));
			this.m_TextBoxRightSample.MaxLength = ((int)(resources.GetObject("m_TextBoxRightSample.MaxLength")));
			this.m_TextBoxRightSample.Multiline = ((bool)(resources.GetObject("m_TextBoxRightSample.Multiline")));
			this.m_TextBoxRightSample.Name = "m_TextBoxRightSample";
			this.m_TextBoxRightSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxRightSample.PasswordChar")));
			this.m_TextBoxRightSample.ReadOnly = true;
			this.m_TextBoxRightSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxRightSample.RightToLeft")));
			this.m_TextBoxRightSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxRightSample.ScrollBars")));
			this.m_TextBoxRightSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxRightSample.Size")));
			this.m_TextBoxRightSample.TabIndex = ((int)(resources.GetObject("m_TextBoxRightSample.TabIndex")));
			this.m_TextBoxRightSample.Text = resources.GetString("m_TextBoxRightSample.Text");
			this.m_TextBoxRightSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxRightSample.TextAlign")));
			this.m_TextBoxRightSample.Visible = ((bool)(resources.GetObject("m_TextBoxRightSample.Visible")));
			this.m_TextBoxRightSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxRightSample.WordWrap")));
			// 
			// m_LabelTankSet
			// 
			this.m_LabelTankSet.AccessibleDescription = resources.GetString("m_LabelTankSet.AccessibleDescription");
			this.m_LabelTankSet.AccessibleName = resources.GetString("m_LabelTankSet.AccessibleName");
			this.m_LabelTankSet.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelTankSet.Anchor")));
			this.m_LabelTankSet.AutoSize = ((bool)(resources.GetObject("m_LabelTankSet.AutoSize")));
			this.m_LabelTankSet.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelTankSet.Dock")));
			this.m_LabelTankSet.Enabled = ((bool)(resources.GetObject("m_LabelTankSet.Enabled")));
			this.m_LabelTankSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_LabelTankSet.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelTankSet.Font")));
			this.m_LabelTankSet.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelTankSet.Image")));
			this.m_LabelTankSet.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelTankSet.ImageAlign")));
			this.m_LabelTankSet.ImageIndex = ((int)(resources.GetObject("m_LabelTankSet.ImageIndex")));
			this.m_LabelTankSet.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelTankSet.ImeMode")));
			this.m_LabelTankSet.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelTankSet.Location")));
			this.m_LabelTankSet.Name = "m_LabelTankSet";
			this.m_LabelTankSet.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelTankSet.RightToLeft")));
			this.m_LabelTankSet.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelTankSet.Size")));
			this.m_LabelTankSet.TabIndex = ((int)(resources.GetObject("m_LabelTankSet.TabIndex")));
			this.m_LabelTankSet.Text = resources.GetString("m_LabelTankSet.Text");
			this.m_LabelTankSet.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelTankSet.TextAlign")));
			this.m_LabelTankSet.Visible = ((bool)(resources.GetObject("m_LabelTankSet.Visible")));
			// 
			// m_LabelTankCur
			// 
			this.m_LabelTankCur.AccessibleDescription = resources.GetString("m_LabelTankCur.AccessibleDescription");
			this.m_LabelTankCur.AccessibleName = resources.GetString("m_LabelTankCur.AccessibleName");
			this.m_LabelTankCur.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelTankCur.Anchor")));
			this.m_LabelTankCur.AutoSize = ((bool)(resources.GetObject("m_LabelTankCur.AutoSize")));
			this.m_LabelTankCur.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelTankCur.Dock")));
			this.m_LabelTankCur.Enabled = ((bool)(resources.GetObject("m_LabelTankCur.Enabled")));
			this.m_LabelTankCur.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_LabelTankCur.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelTankCur.Font")));
			this.m_LabelTankCur.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelTankCur.Image")));
			this.m_LabelTankCur.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelTankCur.ImageAlign")));
			this.m_LabelTankCur.ImageIndex = ((int)(resources.GetObject("m_LabelTankCur.ImageIndex")));
			this.m_LabelTankCur.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelTankCur.ImeMode")));
			this.m_LabelTankCur.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelTankCur.Location")));
			this.m_LabelTankCur.Name = "m_LabelTankCur";
			this.m_LabelTankCur.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelTankCur.RightToLeft")));
			this.m_LabelTankCur.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelTankCur.Size")));
			this.m_LabelTankCur.TabIndex = ((int)(resources.GetObject("m_LabelTankCur.TabIndex")));
			this.m_LabelTankCur.Text = resources.GetString("m_LabelTankCur.Text");
			this.m_LabelTankCur.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelTankCur.TextAlign")));
			this.m_LabelTankCur.Visible = ((bool)(resources.GetObject("m_LabelTankCur.Visible")));
			// 
			// m_TextBoxPulseWidthCurSample
			// 
			this.m_TextBoxPulseWidthCurSample.AccessibleDescription = resources.GetString("m_TextBoxPulseWidthCurSample.AccessibleDescription");
			this.m_TextBoxPulseWidthCurSample.AccessibleName = resources.GetString("m_TextBoxPulseWidthCurSample.AccessibleName");
			this.m_TextBoxPulseWidthCurSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxPulseWidthCurSample.Anchor")));
			this.m_TextBoxPulseWidthCurSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxPulseWidthCurSample.AutoSize")));
			this.m_TextBoxPulseWidthCurSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxPulseWidthCurSample.BackgroundImage")));
			this.m_TextBoxPulseWidthCurSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxPulseWidthCurSample.Dock")));
			this.m_TextBoxPulseWidthCurSample.Enabled = ((bool)(resources.GetObject("m_TextBoxPulseWidthCurSample.Enabled")));
			this.m_TextBoxPulseWidthCurSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxPulseWidthCurSample.Font")));
			this.m_TextBoxPulseWidthCurSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxPulseWidthCurSample.ImeMode")));
			this.m_TextBoxPulseWidthCurSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxPulseWidthCurSample.Location")));
			this.m_TextBoxPulseWidthCurSample.MaxLength = ((int)(resources.GetObject("m_TextBoxPulseWidthCurSample.MaxLength")));
			this.m_TextBoxPulseWidthCurSample.Multiline = ((bool)(resources.GetObject("m_TextBoxPulseWidthCurSample.Multiline")));
			this.m_TextBoxPulseWidthCurSample.Name = "m_TextBoxPulseWidthCurSample";
			this.m_TextBoxPulseWidthCurSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxPulseWidthCurSample.PasswordChar")));
			this.m_TextBoxPulseWidthCurSample.ReadOnly = true;
			this.m_TextBoxPulseWidthCurSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxPulseWidthCurSample.RightToLeft")));
			this.m_TextBoxPulseWidthCurSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxPulseWidthCurSample.ScrollBars")));
			this.m_TextBoxPulseWidthCurSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxPulseWidthCurSample.Size")));
			this.m_TextBoxPulseWidthCurSample.TabIndex = ((int)(resources.GetObject("m_TextBoxPulseWidthCurSample.TabIndex")));
			this.m_TextBoxPulseWidthCurSample.Text = resources.GetString("m_TextBoxPulseWidthCurSample.Text");
			this.m_TextBoxPulseWidthCurSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxPulseWidthCurSample.TextAlign")));
			this.m_TextBoxPulseWidthCurSample.Visible = ((bool)(resources.GetObject("m_TextBoxPulseWidthCurSample.Visible")));
			this.m_TextBoxPulseWidthCurSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxPulseWidthCurSample.WordWrap")));
			// 
			// m_TextBoxPulseWidthSetSample
			// 
			this.m_TextBoxPulseWidthSetSample.AccessibleDescription = resources.GetString("m_TextBoxPulseWidthSetSample.AccessibleDescription");
			this.m_TextBoxPulseWidthSetSample.AccessibleName = resources.GetString("m_TextBoxPulseWidthSetSample.AccessibleName");
			this.m_TextBoxPulseWidthSetSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxPulseWidthSetSample.Anchor")));
			this.m_TextBoxPulseWidthSetSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxPulseWidthSetSample.AutoSize")));
			this.m_TextBoxPulseWidthSetSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxPulseWidthSetSample.BackgroundImage")));
			this.m_TextBoxPulseWidthSetSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxPulseWidthSetSample.Dock")));
			this.m_TextBoxPulseWidthSetSample.Enabled = ((bool)(resources.GetObject("m_TextBoxPulseWidthSetSample.Enabled")));
			this.m_TextBoxPulseWidthSetSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxPulseWidthSetSample.Font")));
			this.m_TextBoxPulseWidthSetSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxPulseWidthSetSample.ImeMode")));
			this.m_TextBoxPulseWidthSetSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxPulseWidthSetSample.Location")));
			this.m_TextBoxPulseWidthSetSample.MaxLength = ((int)(resources.GetObject("m_TextBoxPulseWidthSetSample.MaxLength")));
			this.m_TextBoxPulseWidthSetSample.Multiline = ((bool)(resources.GetObject("m_TextBoxPulseWidthSetSample.Multiline")));
			this.m_TextBoxPulseWidthSetSample.Name = "m_TextBoxPulseWidthSetSample";
			this.m_TextBoxPulseWidthSetSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxPulseWidthSetSample.PasswordChar")));
			this.m_TextBoxPulseWidthSetSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxPulseWidthSetSample.RightToLeft")));
			this.m_TextBoxPulseWidthSetSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxPulseWidthSetSample.ScrollBars")));
			this.m_TextBoxPulseWidthSetSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxPulseWidthSetSample.Size")));
			this.m_TextBoxPulseWidthSetSample.TabIndex = ((int)(resources.GetObject("m_TextBoxPulseWidthSetSample.TabIndex")));
			this.m_TextBoxPulseWidthSetSample.Text = resources.GetString("m_TextBoxPulseWidthSetSample.Text");
			this.m_TextBoxPulseWidthSetSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxPulseWidthSetSample.TextAlign")));
			this.m_TextBoxPulseWidthSetSample.Visible = ((bool)(resources.GetObject("m_TextBoxPulseWidthSetSample.Visible")));
			this.m_TextBoxPulseWidthSetSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxPulseWidthSetSample.WordWrap")));
			// 
			// m_labelVolSet
			// 
			this.m_labelVolSet.AccessibleDescription = resources.GetString("m_labelVolSet.AccessibleDescription");
			this.m_labelVolSet.AccessibleName = resources.GetString("m_labelVolSet.AccessibleName");
			this.m_labelVolSet.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_labelVolSet.Anchor")));
			this.m_labelVolSet.AutoSize = ((bool)(resources.GetObject("m_labelVolSet.AutoSize")));
			this.m_labelVolSet.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_labelVolSet.Dock")));
			this.m_labelVolSet.Enabled = ((bool)(resources.GetObject("m_labelVolSet.Enabled")));
			this.m_labelVolSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_labelVolSet.Font = ((System.Drawing.Font)(resources.GetObject("m_labelVolSet.Font")));
			this.m_labelVolSet.Image = ((System.Drawing.Image)(resources.GetObject("m_labelVolSet.Image")));
			this.m_labelVolSet.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labelVolSet.ImageAlign")));
			this.m_labelVolSet.ImageIndex = ((int)(resources.GetObject("m_labelVolSet.ImageIndex")));
			this.m_labelVolSet.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_labelVolSet.ImeMode")));
			this.m_labelVolSet.Location = ((System.Drawing.Point)(resources.GetObject("m_labelVolSet.Location")));
			this.m_labelVolSet.Name = "m_labelVolSet";
			this.m_labelVolSet.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_labelVolSet.RightToLeft")));
			this.m_labelVolSet.Size = ((System.Drawing.Size)(resources.GetObject("m_labelVolSet.Size")));
			this.m_labelVolSet.TabIndex = ((int)(resources.GetObject("m_labelVolSet.TabIndex")));
			this.m_labelVolSet.Text = resources.GetString("m_labelVolSet.Text");
			this.m_labelVolSet.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labelVolSet.TextAlign")));
			this.m_labelVolSet.Visible = ((bool)(resources.GetObject("m_labelVolSet.Visible")));
			// 
			// m_labelVolCur
			// 
			this.m_labelVolCur.AccessibleDescription = resources.GetString("m_labelVolCur.AccessibleDescription");
			this.m_labelVolCur.AccessibleName = resources.GetString("m_labelVolCur.AccessibleName");
			this.m_labelVolCur.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_labelVolCur.Anchor")));
			this.m_labelVolCur.AutoSize = ((bool)(resources.GetObject("m_labelVolCur.AutoSize")));
			this.m_labelVolCur.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_labelVolCur.Dock")));
			this.m_labelVolCur.Enabled = ((bool)(resources.GetObject("m_labelVolCur.Enabled")));
			this.m_labelVolCur.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_labelVolCur.Font = ((System.Drawing.Font)(resources.GetObject("m_labelVolCur.Font")));
			this.m_labelVolCur.Image = ((System.Drawing.Image)(resources.GetObject("m_labelVolCur.Image")));
			this.m_labelVolCur.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labelVolCur.ImageAlign")));
			this.m_labelVolCur.ImageIndex = ((int)(resources.GetObject("m_labelVolCur.ImageIndex")));
			this.m_labelVolCur.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_labelVolCur.ImeMode")));
			this.m_labelVolCur.Location = ((System.Drawing.Point)(resources.GetObject("m_labelVolCur.Location")));
			this.m_labelVolCur.Name = "m_labelVolCur";
			this.m_labelVolCur.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_labelVolCur.RightToLeft")));
			this.m_labelVolCur.Size = ((System.Drawing.Size)(resources.GetObject("m_labelVolCur.Size")));
			this.m_labelVolCur.TabIndex = ((int)(resources.GetObject("m_labelVolCur.TabIndex")));
			this.m_labelVolCur.Text = resources.GetString("m_labelVolCur.Text");
			this.m_labelVolCur.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labelVolCur.TextAlign")));
			this.m_labelVolCur.Visible = ((bool)(resources.GetObject("m_labelVolCur.Visible")));
			// 
			// m_TextBoxVoltageCurSample
			// 
			this.m_TextBoxVoltageCurSample.AccessibleDescription = resources.GetString("m_TextBoxVoltageCurSample.AccessibleDescription");
			this.m_TextBoxVoltageCurSample.AccessibleName = resources.GetString("m_TextBoxVoltageCurSample.AccessibleName");
			this.m_TextBoxVoltageCurSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxVoltageCurSample.Anchor")));
			this.m_TextBoxVoltageCurSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxVoltageCurSample.AutoSize")));
			this.m_TextBoxVoltageCurSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxVoltageCurSample.BackgroundImage")));
			this.m_TextBoxVoltageCurSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxVoltageCurSample.Dock")));
			this.m_TextBoxVoltageCurSample.Enabled = ((bool)(resources.GetObject("m_TextBoxVoltageCurSample.Enabled")));
			this.m_TextBoxVoltageCurSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxVoltageCurSample.Font")));
			this.m_TextBoxVoltageCurSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxVoltageCurSample.ImeMode")));
			this.m_TextBoxVoltageCurSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxVoltageCurSample.Location")));
			this.m_TextBoxVoltageCurSample.MaxLength = ((int)(resources.GetObject("m_TextBoxVoltageCurSample.MaxLength")));
			this.m_TextBoxVoltageCurSample.Multiline = ((bool)(resources.GetObject("m_TextBoxVoltageCurSample.Multiline")));
			this.m_TextBoxVoltageCurSample.Name = "m_TextBoxVoltageCurSample";
			this.m_TextBoxVoltageCurSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxVoltageCurSample.PasswordChar")));
			this.m_TextBoxVoltageCurSample.ReadOnly = true;
			this.m_TextBoxVoltageCurSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxVoltageCurSample.RightToLeft")));
			this.m_TextBoxVoltageCurSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxVoltageCurSample.ScrollBars")));
			this.m_TextBoxVoltageCurSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxVoltageCurSample.Size")));
			this.m_TextBoxVoltageCurSample.TabIndex = ((int)(resources.GetObject("m_TextBoxVoltageCurSample.TabIndex")));
			this.m_TextBoxVoltageCurSample.Text = resources.GetString("m_TextBoxVoltageCurSample.Text");
			this.m_TextBoxVoltageCurSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxVoltageCurSample.TextAlign")));
			this.m_TextBoxVoltageCurSample.Visible = ((bool)(resources.GetObject("m_TextBoxVoltageCurSample.Visible")));
			this.m_TextBoxVoltageCurSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxVoltageCurSample.WordWrap")));
			// 
			// m_TextBoxVoltageSetSample
			// 
			this.m_TextBoxVoltageSetSample.AccessibleDescription = resources.GetString("m_TextBoxVoltageSetSample.AccessibleDescription");
			this.m_TextBoxVoltageSetSample.AccessibleName = resources.GetString("m_TextBoxVoltageSetSample.AccessibleName");
			this.m_TextBoxVoltageSetSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxVoltageSetSample.Anchor")));
			this.m_TextBoxVoltageSetSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxVoltageSetSample.AutoSize")));
			this.m_TextBoxVoltageSetSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxVoltageSetSample.BackgroundImage")));
			this.m_TextBoxVoltageSetSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxVoltageSetSample.Dock")));
			this.m_TextBoxVoltageSetSample.Enabled = ((bool)(resources.GetObject("m_TextBoxVoltageSetSample.Enabled")));
			this.m_TextBoxVoltageSetSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxVoltageSetSample.Font")));
			this.m_TextBoxVoltageSetSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxVoltageSetSample.ImeMode")));
			this.m_TextBoxVoltageSetSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxVoltageSetSample.Location")));
			this.m_TextBoxVoltageSetSample.MaxLength = ((int)(resources.GetObject("m_TextBoxVoltageSetSample.MaxLength")));
			this.m_TextBoxVoltageSetSample.Multiline = ((bool)(resources.GetObject("m_TextBoxVoltageSetSample.Multiline")));
			this.m_TextBoxVoltageSetSample.Name = "m_TextBoxVoltageSetSample";
			this.m_TextBoxVoltageSetSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxVoltageSetSample.PasswordChar")));
			this.m_TextBoxVoltageSetSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxVoltageSetSample.RightToLeft")));
			this.m_TextBoxVoltageSetSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxVoltageSetSample.ScrollBars")));
			this.m_TextBoxVoltageSetSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxVoltageSetSample.Size")));
			this.m_TextBoxVoltageSetSample.TabIndex = ((int)(resources.GetObject("m_TextBoxVoltageSetSample.TabIndex")));
			this.m_TextBoxVoltageSetSample.Text = resources.GetString("m_TextBoxVoltageSetSample.Text");
			this.m_TextBoxVoltageSetSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxVoltageSetSample.TextAlign")));
			this.m_TextBoxVoltageSetSample.Visible = ((bool)(resources.GetObject("m_TextBoxVoltageSetSample.Visible")));
			this.m_TextBoxVoltageSetSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxVoltageSetSample.WordWrap")));
			// 
			// m_LabelBaseVol
			// 
			this.m_LabelBaseVol.AccessibleDescription = resources.GetString("m_LabelBaseVol.AccessibleDescription");
			this.m_LabelBaseVol.AccessibleName = resources.GetString("m_LabelBaseVol.AccessibleName");
			this.m_LabelBaseVol.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelBaseVol.Anchor")));
			this.m_LabelBaseVol.AutoSize = ((bool)(resources.GetObject("m_LabelBaseVol.AutoSize")));
			this.m_LabelBaseVol.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelBaseVol.Dock")));
			this.m_LabelBaseVol.Enabled = ((bool)(resources.GetObject("m_LabelBaseVol.Enabled")));
			this.m_LabelBaseVol.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_LabelBaseVol.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelBaseVol.Font")));
			this.m_LabelBaseVol.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelBaseVol.Image")));
			this.m_LabelBaseVol.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelBaseVol.ImageAlign")));
			this.m_LabelBaseVol.ImageIndex = ((int)(resources.GetObject("m_LabelBaseVol.ImageIndex")));
			this.m_LabelBaseVol.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelBaseVol.ImeMode")));
			this.m_LabelBaseVol.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelBaseVol.Location")));
			this.m_LabelBaseVol.Name = "m_LabelBaseVol";
			this.m_LabelBaseVol.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelBaseVol.RightToLeft")));
			this.m_LabelBaseVol.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelBaseVol.Size")));
			this.m_LabelBaseVol.TabIndex = ((int)(resources.GetObject("m_LabelBaseVol.TabIndex")));
			this.m_LabelBaseVol.Text = resources.GetString("m_LabelBaseVol.Text");
			this.m_LabelBaseVol.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelBaseVol.TextAlign")));
			this.m_LabelBaseVol.Visible = ((bool)(resources.GetObject("m_LabelBaseVol.Visible")));
			// 
			// m_TextBoxVoltageBaseSample
			// 
			this.m_TextBoxVoltageBaseSample.AccessibleDescription = resources.GetString("m_TextBoxVoltageBaseSample.AccessibleDescription");
			this.m_TextBoxVoltageBaseSample.AccessibleName = resources.GetString("m_TextBoxVoltageBaseSample.AccessibleName");
			this.m_TextBoxVoltageBaseSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxVoltageBaseSample.Anchor")));
			this.m_TextBoxVoltageBaseSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxVoltageBaseSample.AutoSize")));
			this.m_TextBoxVoltageBaseSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxVoltageBaseSample.BackgroundImage")));
			this.m_TextBoxVoltageBaseSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxVoltageBaseSample.Dock")));
			this.m_TextBoxVoltageBaseSample.Enabled = ((bool)(resources.GetObject("m_TextBoxVoltageBaseSample.Enabled")));
			this.m_TextBoxVoltageBaseSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxVoltageBaseSample.Font")));
			this.m_TextBoxVoltageBaseSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxVoltageBaseSample.ImeMode")));
			this.m_TextBoxVoltageBaseSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxVoltageBaseSample.Location")));
			this.m_TextBoxVoltageBaseSample.MaxLength = ((int)(resources.GetObject("m_TextBoxVoltageBaseSample.MaxLength")));
			this.m_TextBoxVoltageBaseSample.Multiline = ((bool)(resources.GetObject("m_TextBoxVoltageBaseSample.Multiline")));
			this.m_TextBoxVoltageBaseSample.Name = "m_TextBoxVoltageBaseSample";
			this.m_TextBoxVoltageBaseSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxVoltageBaseSample.PasswordChar")));
			this.m_TextBoxVoltageBaseSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxVoltageBaseSample.RightToLeft")));
			this.m_TextBoxVoltageBaseSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxVoltageBaseSample.ScrollBars")));
			this.m_TextBoxVoltageBaseSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxVoltageBaseSample.Size")));
			this.m_TextBoxVoltageBaseSample.TabIndex = ((int)(resources.GetObject("m_TextBoxVoltageBaseSample.TabIndex")));
			this.m_TextBoxVoltageBaseSample.Text = resources.GetString("m_TextBoxVoltageBaseSample.Text");
			this.m_TextBoxVoltageBaseSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxVoltageBaseSample.TextAlign")));
			this.m_TextBoxVoltageBaseSample.Visible = ((bool)(resources.GetObject("m_TextBoxVoltageBaseSample.Visible")));
			this.m_TextBoxVoltageBaseSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxVoltageBaseSample.WordWrap")));
			// 
			// m_ButtonRefresh
			// 
			this.m_ButtonRefresh.AccessibleDescription = resources.GetString("m_ButtonRefresh.AccessibleDescription");
			this.m_ButtonRefresh.AccessibleName = resources.GetString("m_ButtonRefresh.AccessibleName");
			this.m_ButtonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonRefresh.Anchor")));
			this.m_ButtonRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonRefresh.BackgroundImage")));
			this.m_ButtonRefresh.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonRefresh.Dock")));
			this.m_ButtonRefresh.Enabled = ((bool)(resources.GetObject("m_ButtonRefresh.Enabled")));
			this.m_ButtonRefresh.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonRefresh.FlatStyle")));
			this.m_ButtonRefresh.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonRefresh.Font")));
			this.m_ButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonRefresh.Image")));
			this.m_ButtonRefresh.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonRefresh.ImageAlign")));
			this.m_ButtonRefresh.ImageIndex = ((int)(resources.GetObject("m_ButtonRefresh.ImageIndex")));
			this.m_ButtonRefresh.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonRefresh.ImeMode")));
			this.m_ButtonRefresh.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonRefresh.Location")));
			this.m_ButtonRefresh.Name = "m_ButtonRefresh";
			this.m_ButtonRefresh.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonRefresh.RightToLeft")));
			this.m_ButtonRefresh.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonRefresh.Size")));
			this.m_ButtonRefresh.TabIndex = ((int)(resources.GetObject("m_ButtonRefresh.TabIndex")));
			this.m_ButtonRefresh.Text = resources.GetString("m_ButtonRefresh.Text");
			this.m_ButtonRefresh.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonRefresh.TextAlign")));
			this.m_ButtonRefresh.Visible = ((bool)(resources.GetObject("m_ButtonRefresh.Visible")));
			this.m_ButtonRefresh.Click += new System.EventHandler(this.m_ButtonRefresh_Click);
			// 
			// m_ButtonToBoard
			// 
			this.m_ButtonToBoard.AccessibleDescription = resources.GetString("m_ButtonToBoard.AccessibleDescription");
			this.m_ButtonToBoard.AccessibleName = resources.GetString("m_ButtonToBoard.AccessibleName");
			this.m_ButtonToBoard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonToBoard.Anchor")));
			this.m_ButtonToBoard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonToBoard.BackgroundImage")));
			this.m_ButtonToBoard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonToBoard.Dock")));
			this.m_ButtonToBoard.Enabled = ((bool)(resources.GetObject("m_ButtonToBoard.Enabled")));
			this.m_ButtonToBoard.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonToBoard.FlatStyle")));
			this.m_ButtonToBoard.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonToBoard.Font")));
			this.m_ButtonToBoard.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonToBoard.Image")));
			this.m_ButtonToBoard.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonToBoard.ImageAlign")));
			this.m_ButtonToBoard.ImageIndex = ((int)(resources.GetObject("m_ButtonToBoard.ImageIndex")));
			this.m_ButtonToBoard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonToBoard.ImeMode")));
			this.m_ButtonToBoard.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonToBoard.Location")));
			this.m_ButtonToBoard.Name = "m_ButtonToBoard";
			this.m_ButtonToBoard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonToBoard.RightToLeft")));
			this.m_ButtonToBoard.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonToBoard.Size")));
			this.m_ButtonToBoard.TabIndex = ((int)(resources.GetObject("m_ButtonToBoard.TabIndex")));
			this.m_ButtonToBoard.Text = resources.GetString("m_ButtonToBoard.Text");
			this.m_ButtonToBoard.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonToBoard.TextAlign")));
			this.m_ButtonToBoard.Visible = ((bool)(resources.GetObject("m_ButtonToBoard.Visible")));
			this.m_ButtonToBoard.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
			// 
			// KonicaTemperatureSetting
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.m_ButtonToBoard);
			this.Controls.Add(this.m_ButtonRefresh);
			this.Controls.Add(this.m_GroupBoxTemperature);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "KonicaTemperatureSetting";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.m_GroupBoxTemperature.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			//m_HeadNum = 12;
			if(CheckComponentChange(sp))
			{
				//m_ColorNum = sp.nColorNum;
				//m_GroupNum = sp.nHeadNum/sp.nColorNum;
				m_HeadNum = sp.nHeadNum;
				byte nMinHead = 0xff;
				for (int i = 0 ; i<m_HeadNum;i++)
				{
					if(sp.pElectricMap[i] < nMinHead )
					{
						nMinHead = sp.pElectricMap[i];
					}
				}
				//m_StartHeadIndex = nMinHead;
				m_StartHeadIndex = 0;
				//m_pMap = sp.pElectricMap;
				//int nStartIndex = 2;
				m_pMap = (byte[])sp.pElectricMap.Clone();
				for (int i = 0 ; i<MAX_CHANAL;i++)
				{
						m_pMap[i] =(byte)( MAX_CHANAL - 1 - sp.pElectricMap[i]);//(byte)(i*3 +  nStartIndex);
				}
				CreateComponent();
				LayoutComponent();
				AppendComponent();
			}
			//Refresh the Realtime info from Jet
			OnRealTimeChange();
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
		}
		
		private void OnGetRealTimeFromUI(ref SRealTimeCurrentInfo sRT)
		{
			for (int i=0;i<m_HeadNum;i++)
			{
				int nMap = m_pMap[i];
				sRT.cTemperatureSet[nMap] = Convert.ToSingle(m_TextBoxHeadSet[i].Text);
				//sRT.cPulseWidth[nMap] = Convert.ToSingle(m_TextBoxPulseWidthSet[i].Text);
				sRT.cVoltage[nMap] = Convert.ToSingle(m_TextBoxVoltageSet[i].Text);
				sRT.cVoltageBase[nMap] = Convert.ToSingle(m_TextBoxVoltageBase[i].Text);
			}
			//sRT.bAutoVoltage = m_CheckBoxAutoVoltage.Checked ;
		}
		public void OnRealTimeChange()
		{
			SRealTimeCurrentInfo info = new SRealTimeCurrentInfo();
			if(CoreInterface.GetRealTimeInfo(ref info)!=0)
			{
				for (int i=0;i<m_HeadNum;i++)
				{
					int nMap = m_pMap[i];
					
					string t = "";
					t = info.cTemperatureCur[nMap].ToString();
					m_TextBoxHeadCur[i].Text = t;

//					t = info.cPulseWidth[nMap].ToString();
//					m_TextBoxPulseWidthCur[i].Text = t;

					t = info.cVoltageCurrent[nMap].ToString();
					m_TextBoxVoltageCur[i].Text = t;

					t = info.cVoltageBase[nMap].ToString();
					m_TextBoxVoltageBase[i].Text = t;

					t = info.cVoltage[nMap].ToString();
					m_TextBoxVoltageSet[i].Text = t;
				}
			}
		}
		private bool CheckComponentChange(SPrinterProperty sp)
		{
			if(m_HeadNum != sp.nHeadNum)
				return true;
			return false;
		}
		private void CreateComponent()
		{
			this.m_LabelHorHeadIndex = new Label[m_HeadNum];
			this.m_TextBoxHeadSet = new TextBox[m_HeadNum];
			this.m_TextBoxHeadCur = new TextBox[m_HeadNum];
			this.m_TextBoxVoltageSet = new TextBox[m_HeadNum];
			this.m_TextBoxVoltageCur = new TextBox[m_HeadNum];
			this.m_TextBoxVoltageBase = new TextBox[m_HeadNum];
			//this.m_TextBoxPulseWidthSet = new TextBox[m_HeadNum];
			//this.m_TextBoxPulseWidthCur = new TextBox[m_HeadNum];
			 

			for(int i = 0; i < m_HeadNum; i ++)
			{
				this.m_LabelHorHeadIndex[i] = new Label();
				this.m_TextBoxHeadSet[i] = new TextBox();
				this.m_TextBoxHeadCur[i] = new TextBox();
				this.m_TextBoxVoltageSet[i] = new TextBox();
				this.m_TextBoxVoltageCur[i] = new TextBox();
				this.m_TextBoxVoltageBase[i] = new TextBox();

				//this.m_TextBoxPulseWidthSet[i] = new TextBox();
				//this.m_TextBoxPulseWidthCur[i] = new TextBox();
			}
			this.SuspendLayout();
		}

		private void AppendComponent()
		{
			for (int i=0; i<m_HeadNum; i++)
			{
				m_GroupBoxTemperature.Controls.Add(this.m_LabelHorHeadIndex[i]); 
				m_GroupBoxTemperature.Controls.Add(this.m_TextBoxHeadSet[i]); 
				m_GroupBoxTemperature.Controls.Add(this.m_TextBoxHeadCur[i]); 
				m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageSet[i]); 
				m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageCur[i]); 
				m_GroupBoxTemperature.Controls.Add(this.m_TextBoxVoltageBase[i]); 

				//m_GroupBoxTemperature.Controls.Add(this.m_TextBoxPulseWidthSet[i]); 
				//m_GroupBoxTemperature.Controls.Add(this.m_TextBoxPulseWidthCur[i]); 
			
			}
			
			m_GroupBoxTemperature.ResumeLayout(false);
			
			this.ResumeLayout(false);
		}
		private void LayoutComponent()
		{
				
			m_GroupBoxTemperature.SuspendLayout();
			this.SuspendLayout();

			///True Layout
			///
			int start_x, start_y,end_x,space_x,width_con;
			start_x = this.m_TextBoxLeftSample.Left;
			start_y = this.m_TextBoxLeftSample.Top;
			end_x = this.m_GroupBoxTemperature.Width;
			width_con = this.m_TextBoxLeftSample.Width;
			CalculateHorNum(m_HeadNum,start_x,end_x,ref width_con,out space_x);
			for (int i=0; i<m_HeadNum; i++)
			{
				Label label = this.m_LabelHorHeadIndex[i];
				ControlClone.LabelClone(label,this.m_LabelHorSample);
				
				label.Location = new Point(this.m_LabelHorSample.Left + space_x * i,this.m_LabelHorSample.Top );
				label.Width = width_con;
				label.Text = (i+m_StartHeadIndex).ToString();
				label.Visible = true;


				TextBox textBox = this.m_TextBoxHeadSet[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxLeftSample);

				textBox.Location = new Point(this.m_TextBoxLeftSample.Left+ space_x * i,this.m_TextBoxLeftSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex =  i;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
				textBox.Visible = false;


				textBox = this.m_TextBoxHeadCur[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxRightSample);

				textBox.Location = new Point(this.m_TextBoxRightSample.Left+ space_x * i,this.m_TextBoxRightSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum *2 +i;
				textBox.TabStop = false;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);

#if false
				textBox = this.m_TextBoxPulseWidthSet[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxPulseWidthSetSample);

				textBox.Location = new Point(this.m_TextBoxPulseWidthSetSample.Left+ space_x * i,this.m_TextBoxPulseWidthSetSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum + i;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);

				textBox = this.m_TextBoxPulseWidthCur[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxPulseWidthCurSample);

				textBox.Location = new Point(this.m_TextBoxPulseWidthCurSample.Left+ space_x * i,this.m_TextBoxPulseWidthCurSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum *3 +i;
				textBox.TabStop = false;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
#endif	
				textBox = this.m_TextBoxVoltageSet[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxVoltageSetSample);

				textBox.Location = new Point(this.m_TextBoxVoltageSetSample.Left+ space_x * i,this.m_TextBoxVoltageSetSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum*2 + i;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);

				textBox = this.m_TextBoxVoltageCur[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxVoltageCurSample);

				textBox.Location = new Point(this.m_TextBoxVoltageCurSample.Left+ space_x * i,this.m_TextBoxVoltageCurSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum *3 +i;
				textBox.TabStop = false;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);

				textBox = this.m_TextBoxVoltageBase[i];
				ControlClone.TextBoxClone(textBox,this.m_TextBoxVoltageBaseSample);

				textBox.Location = new Point(this.m_TextBoxVoltageBaseSample.Left+ space_x * i,this.m_TextBoxVoltageBaseSample.Top);
				textBox.Width = width_con;
				textBox.TabIndex = m_HeadNum*4 + i;
				textBox.Text = "0";
				textBox.Visible = true;
				textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
				textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBoxLeftSample_KeyPress);
				textBox.Leave += new System.EventHandler(this.m_CheckBoxControl_Leave);
			}

		}
		private void CalculateHorNum(int num,int start_x,int end_x,ref int width,out int space)
		{
			const int m_HorGap = 4;
			const int m_Margin = 8;

			space = m_HorGap + width;
			if(num > 1)
				space = (end_x - start_x - m_Margin - width)/(num -1) ;
			if((width + m_HorGap) > space)
				width = space - m_HorGap;
		}

		private void m_CheckBoxControl_Leave(object sender, System.EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			bool isValidNumber = true;
			try
			{
				float val = float.Parse(textBox.Text);
				//????????????????????????????????????????????
				//Note:Should not Update All Data, 
				//Should only renew the newest control value.				;
				UpdateToLocal();
			}
			catch(Exception )
			{
				//Console.WriteLine(ex.Message);
				isValidNumber = false;
			}

			if(!isValidNumber)
			{
				SystemCall.Beep(200,50);
				textBox.Focus();
				textBox.SelectAll();
			}
		}
		private void UpdateToLocal()
		{
		}

		private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
			{
				m_CheckBoxControl_Leave(sender,e);
			}
		}

		private void m_ButtonRefresh_Click(object sender, System.EventArgs e)
		{
			if(m_ButtonRefresh.Enabled == false) return;
			m_ButtonRefresh.Enabled = false;
			OnRealTimeChange();
			m_ButtonRefresh.Enabled = true;
		}

		private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
		{
			if(m_ButtonToBoard.Enabled == false) return;
			m_ButtonToBoard.Enabled = false;

			SRealTimeCurrentInfo sRT = new SRealTimeCurrentInfo();
			sRT.cTemperatureCur = new float[CoreConst.MAX_HEAD_NUM];
			sRT.cTemperatureSet = new float[CoreConst.MAX_HEAD_NUM];
			sRT.cPulseWidth = new float[CoreConst.MAX_HEAD_NUM];
			sRT.cVoltage = new float[CoreConst.MAX_HEAD_NUM];
			sRT.cVoltageBase = new float[CoreConst.MAX_HEAD_NUM];
			OnGetRealTimeFromUI(ref sRT);
			if(CoreInterface.SetRealTimeInfo(ref sRT)!=0)
			{
			}
			else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));

			m_ButtonToBoard.Enabled = true;
		}

		private void m_TextBoxLeftSample_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
#if true
			if(!Char.IsLetter(e.KeyChar) && !(Char.IsPunctuation(e.KeyChar) && e.KeyChar != '.'&&e.KeyChar != '-' ))
			{
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
			}
#endif		
		}
	}
}
