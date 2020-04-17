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
using  System.Diagnostics;
using BYHXPrinterManager.GradientControls;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for PrinterHWSetting.
	/// </summary>
	public class PrinterHWSettingSimple : System.Windows.Forms.Form
	{
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button m_ButtonCancel;
		private System.Windows.Forms.Button m_ButtonOK;
		private BYHXPrinterManager.GradientControls.Grouper groupBox1;
		private System.Windows.Forms.RadioButton m_RadioButtonEncoder;
		private System.Windows.Forms.RadioButton m_RadioButtonServoEncoder;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.CheckBox m_CheckBoxFlatBed;
		private System.Windows.Forms.CheckBox m_CheckBoxUseYEncoder;
		private System.ComponentModel.IContainer components;

        public PrinterHWSettingSimple()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrinterHWSettingSimple));
			this.dividerPanel1 = new DividerPanel.DividerPanel();
			this.m_ButtonCancel = new System.Windows.Forms.Button();
			this.m_ButtonOK = new System.Windows.Forms.Button();
			this.groupBox1 = new BYHXPrinterManager.GradientControls.Grouper();
			this.m_RadioButtonServoEncoder = new System.Windows.Forms.RadioButton();
			this.m_RadioButtonEncoder = new System.Windows.Forms.RadioButton();
			this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.m_CheckBoxFlatBed = new System.Windows.Forms.CheckBox();
			this.m_CheckBoxUseYEncoder = new System.Windows.Forms.CheckBox();
			this.dividerPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dividerPanel1
			// 
			this.dividerPanel1.AccessibleDescription = resources.GetString("dividerPanel1.AccessibleDescription");
			this.dividerPanel1.AccessibleName = resources.GetString("dividerPanel1.AccessibleName");
			this.dividerPanel1.AllowDrop = true;
			this.dividerPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dividerPanel1.Anchor")));
			this.dividerPanel1.AutoScroll = ((bool)(resources.GetObject("dividerPanel1.AutoScroll")));
			this.dividerPanel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.AutoScrollMargin")));
			this.dividerPanel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.AutoScrollMinSize")));
			this.dividerPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dividerPanel1.BackgroundImage")));
			this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
			this.dividerPanel1.Controls.Add(this.m_ButtonCancel);
			this.dividerPanel1.Controls.Add(this.m_ButtonOK);
			this.dividerPanel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dividerPanel1.Dock")));
			this.dividerPanel1.Enabled = ((bool)(resources.GetObject("dividerPanel1.Enabled")));
			this.dividerPanel1.Font = ((System.Drawing.Font)(resources.GetObject("dividerPanel1.Font")));
			this.dividerPanel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dividerPanel1.ImeMode")));
			this.dividerPanel1.Location = ((System.Drawing.Point)(resources.GetObject("dividerPanel1.Location")));
			this.dividerPanel1.Name = "dividerPanel1";
			this.dividerPanel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dividerPanel1.RightToLeft")));
			this.dividerPanel1.Size = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.Size")));
			this.dividerPanel1.TabIndex = ((int)(resources.GetObject("dividerPanel1.TabIndex")));
			this.dividerPanel1.Text = resources.GetString("dividerPanel1.Text");
			this.m_ToolTip.SetToolTip(this.dividerPanel1, resources.GetString("dividerPanel1.ToolTip"));
			this.dividerPanel1.Visible = ((bool)(resources.GetObject("dividerPanel1.Visible")));
			// 
			// m_ButtonCancel
			// 
			this.m_ButtonCancel.AccessibleDescription = resources.GetString("m_ButtonCancel.AccessibleDescription");
			this.m_ButtonCancel.AccessibleName = resources.GetString("m_ButtonCancel.AccessibleName");
			this.m_ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonCancel.Anchor")));
			this.m_ButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonCancel.BackgroundImage")));
			this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_ButtonCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonCancel.Dock")));
			this.m_ButtonCancel.Enabled = ((bool)(resources.GetObject("m_ButtonCancel.Enabled")));
			this.m_ButtonCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonCancel.FlatStyle")));
			this.m_ButtonCancel.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonCancel.Font")));
			this.m_ButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonCancel.Image")));
			this.m_ButtonCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonCancel.ImageAlign")));
			this.m_ButtonCancel.ImageIndex = ((int)(resources.GetObject("m_ButtonCancel.ImageIndex")));
			this.m_ButtonCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonCancel.ImeMode")));
			this.m_ButtonCancel.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonCancel.Location")));
			this.m_ButtonCancel.Name = "m_ButtonCancel";
			this.m_ButtonCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonCancel.RightToLeft")));
			this.m_ButtonCancel.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonCancel.Size")));
			this.m_ButtonCancel.TabIndex = ((int)(resources.GetObject("m_ButtonCancel.TabIndex")));
			this.m_ButtonCancel.Text = resources.GetString("m_ButtonCancel.Text");
			this.m_ButtonCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonCancel.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_ButtonCancel, resources.GetString("m_ButtonCancel.ToolTip"));
			this.m_ButtonCancel.Visible = ((bool)(resources.GetObject("m_ButtonCancel.Visible")));
			// 
			// m_ButtonOK
			// 
			this.m_ButtonOK.AccessibleDescription = resources.GetString("m_ButtonOK.AccessibleDescription");
			this.m_ButtonOK.AccessibleName = resources.GetString("m_ButtonOK.AccessibleName");
			this.m_ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonOK.Anchor")));
			this.m_ButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonOK.BackgroundImage")));
			this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ButtonOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonOK.Dock")));
			this.m_ButtonOK.Enabled = ((bool)(resources.GetObject("m_ButtonOK.Enabled")));
			this.m_ButtonOK.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonOK.FlatStyle")));
			this.m_ButtonOK.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonOK.Font")));
			this.m_ButtonOK.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonOK.Image")));
			this.m_ButtonOK.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonOK.ImageAlign")));
			this.m_ButtonOK.ImageIndex = ((int)(resources.GetObject("m_ButtonOK.ImageIndex")));
			this.m_ButtonOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonOK.ImeMode")));
			this.m_ButtonOK.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonOK.Location")));
			this.m_ButtonOK.Name = "m_ButtonOK";
			this.m_ButtonOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonOK.RightToLeft")));
			this.m_ButtonOK.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonOK.Size")));
			this.m_ButtonOK.TabIndex = ((int)(resources.GetObject("m_ButtonOK.TabIndex")));
			this.m_ButtonOK.Text = resources.GetString("m_ButtonOK.Text");
			this.m_ButtonOK.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonOK.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_ButtonOK, resources.GetString("m_ButtonOK.ToolTip"));
			this.m_ButtonOK.Visible = ((bool)(resources.GetObject("m_ButtonOK.Visible")));
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.BorderThickness = 1F;
			this.groupBox1.Controls.Add(this.m_RadioButtonServoEncoder);
			this.groupBox1.Controls.Add(this.m_RadioButtonEncoder);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.Gold);
			this.groupBox1.GroupImage = null;
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.PaintGroupBox = false;
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.RoundCorners = 10;
			this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
			this.groupBox1.ShadowControl = false;
			this.groupBox1.ShadowThickness = 3;
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.groupBox1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
			this.groupBox1.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
			this.m_ToolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// m_RadioButtonServoEncoder
			// 
			this.m_RadioButtonServoEncoder.AccessibleDescription = resources.GetString("m_RadioButtonServoEncoder.AccessibleDescription");
			this.m_RadioButtonServoEncoder.AccessibleName = resources.GetString("m_RadioButtonServoEncoder.AccessibleName");
			this.m_RadioButtonServoEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_RadioButtonServoEncoder.Anchor")));
			this.m_RadioButtonServoEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_RadioButtonServoEncoder.Appearance")));
			this.m_RadioButtonServoEncoder.BackColor = System.Drawing.Color.Transparent;
			this.m_RadioButtonServoEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonServoEncoder.BackgroundImage")));
			this.m_RadioButtonServoEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.CheckAlign")));
			this.m_RadioButtonServoEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_RadioButtonServoEncoder.Dock")));
			this.m_RadioButtonServoEncoder.Enabled = ((bool)(resources.GetObject("m_RadioButtonServoEncoder.Enabled")));
			this.m_RadioButtonServoEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_RadioButtonServoEncoder.FlatStyle")));
			this.m_RadioButtonServoEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_RadioButtonServoEncoder.Font")));
			this.m_RadioButtonServoEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonServoEncoder.Image")));
			this.m_RadioButtonServoEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.ImageAlign")));
			this.m_RadioButtonServoEncoder.ImageIndex = ((int)(resources.GetObject("m_RadioButtonServoEncoder.ImageIndex")));
			this.m_RadioButtonServoEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_RadioButtonServoEncoder.ImeMode")));
			this.m_RadioButtonServoEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_RadioButtonServoEncoder.Location")));
			this.m_RadioButtonServoEncoder.Name = "m_RadioButtonServoEncoder";
			this.m_RadioButtonServoEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_RadioButtonServoEncoder.RightToLeft")));
			this.m_RadioButtonServoEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_RadioButtonServoEncoder.Size")));
			this.m_RadioButtonServoEncoder.TabIndex = ((int)(resources.GetObject("m_RadioButtonServoEncoder.TabIndex")));
			this.m_RadioButtonServoEncoder.Text = resources.GetString("m_RadioButtonServoEncoder.Text");
			this.m_RadioButtonServoEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_RadioButtonServoEncoder, resources.GetString("m_RadioButtonServoEncoder.ToolTip"));
			this.m_RadioButtonServoEncoder.Visible = ((bool)(resources.GetObject("m_RadioButtonServoEncoder.Visible")));
			// 
			// m_RadioButtonEncoder
			// 
			this.m_RadioButtonEncoder.AccessibleDescription = resources.GetString("m_RadioButtonEncoder.AccessibleDescription");
			this.m_RadioButtonEncoder.AccessibleName = resources.GetString("m_RadioButtonEncoder.AccessibleName");
			this.m_RadioButtonEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_RadioButtonEncoder.Anchor")));
			this.m_RadioButtonEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_RadioButtonEncoder.Appearance")));
			this.m_RadioButtonEncoder.BackColor = System.Drawing.Color.Transparent;
			this.m_RadioButtonEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonEncoder.BackgroundImage")));
			this.m_RadioButtonEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.CheckAlign")));
			this.m_RadioButtonEncoder.Checked = true;
			this.m_RadioButtonEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_RadioButtonEncoder.Dock")));
			this.m_RadioButtonEncoder.Enabled = ((bool)(resources.GetObject("m_RadioButtonEncoder.Enabled")));
			this.m_RadioButtonEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_RadioButtonEncoder.FlatStyle")));
			this.m_RadioButtonEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_RadioButtonEncoder.Font")));
			this.m_RadioButtonEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonEncoder.Image")));
			this.m_RadioButtonEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.ImageAlign")));
			this.m_RadioButtonEncoder.ImageIndex = ((int)(resources.GetObject("m_RadioButtonEncoder.ImageIndex")));
			this.m_RadioButtonEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_RadioButtonEncoder.ImeMode")));
			this.m_RadioButtonEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_RadioButtonEncoder.Location")));
			this.m_RadioButtonEncoder.Name = "m_RadioButtonEncoder";
			this.m_RadioButtonEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_RadioButtonEncoder.RightToLeft")));
			this.m_RadioButtonEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_RadioButtonEncoder.Size")));
			this.m_RadioButtonEncoder.TabIndex = ((int)(resources.GetObject("m_RadioButtonEncoder.TabIndex")));
			this.m_RadioButtonEncoder.TabStop = true;
			this.m_RadioButtonEncoder.Text = resources.GetString("m_RadioButtonEncoder.Text");
			this.m_RadioButtonEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_RadioButtonEncoder, resources.GetString("m_RadioButtonEncoder.ToolTip"));
			this.m_RadioButtonEncoder.Visible = ((bool)(resources.GetObject("m_RadioButtonEncoder.Visible")));
			// 
			// m_CheckBoxFlatBed
			// 
			this.m_CheckBoxFlatBed.AccessibleDescription = resources.GetString("m_CheckBoxFlatBed.AccessibleDescription");
			this.m_CheckBoxFlatBed.AccessibleName = resources.GetString("m_CheckBoxFlatBed.AccessibleName");
			this.m_CheckBoxFlatBed.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_CheckBoxFlatBed.Anchor")));
			this.m_CheckBoxFlatBed.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_CheckBoxFlatBed.Appearance")));
			this.m_CheckBoxFlatBed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxFlatBed.BackgroundImage")));
			this.m_CheckBoxFlatBed.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.CheckAlign")));
			this.m_CheckBoxFlatBed.Checked = true;
			this.m_CheckBoxFlatBed.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_CheckBoxFlatBed.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_CheckBoxFlatBed.Dock")));
			this.m_CheckBoxFlatBed.Enabled = ((bool)(resources.GetObject("m_CheckBoxFlatBed.Enabled")));
			this.m_CheckBoxFlatBed.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_CheckBoxFlatBed.FlatStyle")));
			this.m_CheckBoxFlatBed.Font = ((System.Drawing.Font)(resources.GetObject("m_CheckBoxFlatBed.Font")));
			this.m_CheckBoxFlatBed.Image = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxFlatBed.Image")));
			this.m_CheckBoxFlatBed.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.ImageAlign")));
			this.m_CheckBoxFlatBed.ImageIndex = ((int)(resources.GetObject("m_CheckBoxFlatBed.ImageIndex")));
			this.m_CheckBoxFlatBed.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_CheckBoxFlatBed.ImeMode")));
			this.m_CheckBoxFlatBed.Location = ((System.Drawing.Point)(resources.GetObject("m_CheckBoxFlatBed.Location")));
			this.m_CheckBoxFlatBed.Name = "m_CheckBoxFlatBed";
			this.m_CheckBoxFlatBed.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_CheckBoxFlatBed.RightToLeft")));
			this.m_CheckBoxFlatBed.Size = ((System.Drawing.Size)(resources.GetObject("m_CheckBoxFlatBed.Size")));
			this.m_CheckBoxFlatBed.TabIndex = ((int)(resources.GetObject("m_CheckBoxFlatBed.TabIndex")));
			this.m_CheckBoxFlatBed.Text = resources.GetString("m_CheckBoxFlatBed.Text");
			this.m_CheckBoxFlatBed.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_CheckBoxFlatBed, resources.GetString("m_CheckBoxFlatBed.ToolTip"));
			this.m_CheckBoxFlatBed.Visible = ((bool)(resources.GetObject("m_CheckBoxFlatBed.Visible")));
			// 
			// m_CheckBoxUseYEncoder
			// 
			this.m_CheckBoxUseYEncoder.AccessibleDescription = resources.GetString("m_CheckBoxUseYEncoder.AccessibleDescription");
			this.m_CheckBoxUseYEncoder.AccessibleName = resources.GetString("m_CheckBoxUseYEncoder.AccessibleName");
			this.m_CheckBoxUseYEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_CheckBoxUseYEncoder.Anchor")));
			this.m_CheckBoxUseYEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_CheckBoxUseYEncoder.Appearance")));
			this.m_CheckBoxUseYEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxUseYEncoder.BackgroundImage")));
			this.m_CheckBoxUseYEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.CheckAlign")));
			this.m_CheckBoxUseYEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_CheckBoxUseYEncoder.Dock")));
			this.m_CheckBoxUseYEncoder.Enabled = ((bool)(resources.GetObject("m_CheckBoxUseYEncoder.Enabled")));
			this.m_CheckBoxUseYEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_CheckBoxUseYEncoder.FlatStyle")));
			this.m_CheckBoxUseYEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_CheckBoxUseYEncoder.Font")));
			this.m_CheckBoxUseYEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxUseYEncoder.Image")));
			this.m_CheckBoxUseYEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.ImageAlign")));
			this.m_CheckBoxUseYEncoder.ImageIndex = ((int)(resources.GetObject("m_CheckBoxUseYEncoder.ImageIndex")));
			this.m_CheckBoxUseYEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_CheckBoxUseYEncoder.ImeMode")));
			this.m_CheckBoxUseYEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_CheckBoxUseYEncoder.Location")));
			this.m_CheckBoxUseYEncoder.Name = "m_CheckBoxUseYEncoder";
			this.m_CheckBoxUseYEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_CheckBoxUseYEncoder.RightToLeft")));
			this.m_CheckBoxUseYEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_CheckBoxUseYEncoder.Size")));
			this.m_CheckBoxUseYEncoder.TabIndex = ((int)(resources.GetObject("m_CheckBoxUseYEncoder.TabIndex")));
			this.m_CheckBoxUseYEncoder.Text = resources.GetString("m_CheckBoxUseYEncoder.Text");
			this.m_CheckBoxUseYEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.TextAlign")));
			this.m_ToolTip.SetToolTip(this.m_CheckBoxUseYEncoder, resources.GetString("m_CheckBoxUseYEncoder.ToolTip"));
			this.m_CheckBoxUseYEncoder.Visible = ((bool)(resources.GetObject("m_CheckBoxUseYEncoder.Visible")));
			// 
			// PrinterHWSettingSimple
			// 
			this.AcceptButton = this.m_ButtonOK;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.m_ButtonCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.m_CheckBoxUseYEncoder);
			this.Controls.Add(this.m_CheckBoxFlatBed);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dividerPanel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "PrinterHWSettingSimple";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.m_ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.dividerPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void SetLineEncoder(bool bEncoder)
		{
			m_RadioButtonEncoder.Checked = bEncoder;
			m_RadioButtonServoEncoder.Checked = !bEncoder;
		}
		public bool GetLineEncoder()
		{
			bool LineEncoder = m_RadioButtonEncoder.Checked;
			return LineEncoder;
		}
		public void SetFlat(bool bEncoder)
		{
			m_CheckBoxFlatBed.Checked = bEncoder;
		}
		public bool GetFlat()
		{
			bool LineEncoder = m_CheckBoxFlatBed.Checked;
			return LineEncoder;
		}
		public void SetUseYEncoder(bool bEncoder)
		{
			m_CheckBoxUseYEncoder.Checked = bEncoder;
		}
		public bool GetUseYEncoder()
		{
			bool LineEncoder = m_CheckBoxUseYEncoder.Checked;
			return LineEncoder;
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			if(sp.IsDocan()|| sp.bSupportDoubleMachine)
			{
				m_CheckBoxFlatBed.Visible = true;
			}
			else
			{
				m_CheckBoxFlatBed.Visible = false;
			}
//			if(sp.bSupportYEncoder)
//			{
//				m_CheckBoxUseYEncoder.Visible = true;
//			}
//			else
			{
				m_CheckBoxUseYEncoder.Visible = false;
			}

		}
		public void SetGroupBoxStyle(Grouper ts)
		{
			this.groupBox1.CloneGrouperStyle(ts);
		}
	}
}
