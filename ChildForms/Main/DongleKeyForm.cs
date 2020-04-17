using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text;
using BYHXPrinterManager;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// DongleKeyForm 的摘要说明。
	/// </summary>
	public class DongleKeyForm : System.Windows.Forms.Form
	{
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button m_ButtonOK;
		private System.Windows.Forms.Button m_ButtonExit;
		private BYHXControls.SerialNoControl m_SerialNoControlTime;
		private System.Windows.Forms.Label m_LabelPassword;
		private System.Windows.Forms.Button m_ButtonSetTime;
		public string mDongleKey = "";
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelDeviceID;
		private System.Windows.Forms.Label labelSerialNum;
		private System.Windows.Forms.Label labelLockStyle;
		private System.Windows.Forms.Label labelValidDate;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label labelLefttime;
		private System.Windows.Forms.Label labelLT;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelVenderID;
		private DividerPanel.DividerPanel panel1;
		private DateTime StartTime = new DateTime(1970,1,1);

		public DongleKeyForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DongleKeyForm));
			this.dividerPanel1 = new DividerPanel.DividerPanel();
			this.m_ButtonOK = new System.Windows.Forms.Button();
			this.m_ButtonExit = new System.Windows.Forms.Button();
			this.m_SerialNoControlTime = new BYHXControls.SerialNoControl();
			this.m_LabelPassword = new System.Windows.Forms.Label();
			this.m_ButtonSetTime = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel1 = new DividerPanel.DividerPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.labelSerialNum = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelLefttime = new System.Windows.Forms.Label();
			this.labelValidDate = new System.Windows.Forms.Label();
			this.labelLT = new System.Windows.Forms.Label();
			this.labelVenderID = new System.Windows.Forms.Label();
			this.labelLockStyle = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelDeviceID = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.dividerPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.dividerPanel1.Controls.Add(this.m_ButtonOK);
			this.dividerPanel1.Controls.Add(this.m_ButtonExit);
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
			this.dividerPanel1.Visible = ((bool)(resources.GetObject("dividerPanel1.Visible")));
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
			this.m_ButtonOK.Visible = ((bool)(resources.GetObject("m_ButtonOK.Visible")));
			this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
			// 
			// m_ButtonExit
			// 
			this.m_ButtonExit.AccessibleDescription = resources.GetString("m_ButtonExit.AccessibleDescription");
			this.m_ButtonExit.AccessibleName = resources.GetString("m_ButtonExit.AccessibleName");
			this.m_ButtonExit.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonExit.Anchor")));
			this.m_ButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonExit.BackgroundImage")));
			this.m_ButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_ButtonExit.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonExit.Dock")));
			this.m_ButtonExit.Enabled = ((bool)(resources.GetObject("m_ButtonExit.Enabled")));
			this.m_ButtonExit.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonExit.FlatStyle")));
			this.m_ButtonExit.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonExit.Font")));
			this.m_ButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonExit.Image")));
			this.m_ButtonExit.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonExit.ImageAlign")));
			this.m_ButtonExit.ImageIndex = ((int)(resources.GetObject("m_ButtonExit.ImageIndex")));
			this.m_ButtonExit.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonExit.ImeMode")));
			this.m_ButtonExit.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonExit.Location")));
			this.m_ButtonExit.Name = "m_ButtonExit";
			this.m_ButtonExit.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonExit.RightToLeft")));
			this.m_ButtonExit.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonExit.Size")));
			this.m_ButtonExit.TabIndex = ((int)(resources.GetObject("m_ButtonExit.TabIndex")));
			this.m_ButtonExit.Text = resources.GetString("m_ButtonExit.Text");
			this.m_ButtonExit.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonExit.TextAlign")));
			this.m_ButtonExit.Visible = ((bool)(resources.GetObject("m_ButtonExit.Visible")));
			// 
			// m_SerialNoControlTime
			// 
			this.m_SerialNoControlTime.AccessibleDescription = resources.GetString("m_SerialNoControlTime.AccessibleDescription");
			this.m_SerialNoControlTime.AccessibleName = resources.GetString("m_SerialNoControlTime.AccessibleName");
			this.m_SerialNoControlTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SerialNoControlTime.Anchor")));
			this.m_SerialNoControlTime.AutoScroll = ((bool)(resources.GetObject("m_SerialNoControlTime.AutoScroll")));
			this.m_SerialNoControlTime.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.AutoScrollMargin")));
			this.m_SerialNoControlTime.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.AutoScrollMinSize")));
			this.m_SerialNoControlTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SerialNoControlTime.BackgroundImage")));
			this.m_SerialNoControlTime.CharsPerUnit = 4;
			this.m_SerialNoControlTime.Count = 4;
			this.m_SerialNoControlTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SerialNoControlTime.Dock")));
			this.m_SerialNoControlTime.Enabled = ((bool)(resources.GetObject("m_SerialNoControlTime.Enabled")));
			this.m_SerialNoControlTime.Font = ((System.Drawing.Font)(resources.GetObject("m_SerialNoControlTime.Font")));
			this.m_SerialNoControlTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SerialNoControlTime.ImeMode")));
			this.m_SerialNoControlTime.Location = ((System.Drawing.Point)(resources.GetObject("m_SerialNoControlTime.Location")));
			this.m_SerialNoControlTime.Name = "m_SerialNoControlTime";
			this.m_SerialNoControlTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SerialNoControlTime.RightToLeft")));
			this.m_SerialNoControlTime.SeparateString = "-";
			this.m_SerialNoControlTime.Size = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.Size")));
			this.m_SerialNoControlTime.TabIndex = ((int)(resources.GetObject("m_SerialNoControlTime.TabIndex")));
			this.m_SerialNoControlTime.Visible = ((bool)(resources.GetObject("m_SerialNoControlTime.Visible")));
			// 
			// m_LabelPassword
			// 
			this.m_LabelPassword.AccessibleDescription = resources.GetString("m_LabelPassword.AccessibleDescription");
			this.m_LabelPassword.AccessibleName = resources.GetString("m_LabelPassword.AccessibleName");
			this.m_LabelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelPassword.Anchor")));
			this.m_LabelPassword.AutoSize = ((bool)(resources.GetObject("m_LabelPassword.AutoSize")));
			this.m_LabelPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelPassword.Dock")));
			this.m_LabelPassword.Enabled = ((bool)(resources.GetObject("m_LabelPassword.Enabled")));
			this.m_LabelPassword.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelPassword.Font")));
			this.m_LabelPassword.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelPassword.Image")));
			this.m_LabelPassword.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPassword.ImageAlign")));
			this.m_LabelPassword.ImageIndex = ((int)(resources.GetObject("m_LabelPassword.ImageIndex")));
			this.m_LabelPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelPassword.ImeMode")));
			this.m_LabelPassword.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelPassword.Location")));
			this.m_LabelPassword.Name = "m_LabelPassword";
			this.m_LabelPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelPassword.RightToLeft")));
			this.m_LabelPassword.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelPassword.Size")));
			this.m_LabelPassword.TabIndex = ((int)(resources.GetObject("m_LabelPassword.TabIndex")));
			this.m_LabelPassword.Text = resources.GetString("m_LabelPassword.Text");
			this.m_LabelPassword.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPassword.TextAlign")));
			this.m_LabelPassword.Visible = ((bool)(resources.GetObject("m_LabelPassword.Visible")));
			// 
			// m_ButtonSetTime
			// 
			this.m_ButtonSetTime.AccessibleDescription = resources.GetString("m_ButtonSetTime.AccessibleDescription");
			this.m_ButtonSetTime.AccessibleName = resources.GetString("m_ButtonSetTime.AccessibleName");
			this.m_ButtonSetTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonSetTime.Anchor")));
			this.m_ButtonSetTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetTime.BackgroundImage")));
			this.m_ButtonSetTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonSetTime.Dock")));
			this.m_ButtonSetTime.Enabled = ((bool)(resources.GetObject("m_ButtonSetTime.Enabled")));
			this.m_ButtonSetTime.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonSetTime.FlatStyle")));
			this.m_ButtonSetTime.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonSetTime.Font")));
			this.m_ButtonSetTime.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetTime.Image")));
			this.m_ButtonSetTime.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetTime.ImageAlign")));
			this.m_ButtonSetTime.ImageIndex = ((int)(resources.GetObject("m_ButtonSetTime.ImageIndex")));
			this.m_ButtonSetTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonSetTime.ImeMode")));
			this.m_ButtonSetTime.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonSetTime.Location")));
			this.m_ButtonSetTime.Name = "m_ButtonSetTime";
			this.m_ButtonSetTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonSetTime.RightToLeft")));
			this.m_ButtonSetTime.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonSetTime.Size")));
			this.m_ButtonSetTime.TabIndex = ((int)(resources.GetObject("m_ButtonSetTime.TabIndex")));
			this.m_ButtonSetTime.Text = resources.GetString("m_ButtonSetTime.Text");
			this.m_ButtonSetTime.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetTime.TextAlign")));
			this.m_ButtonSetTime.Visible = ((bool)(resources.GetObject("m_ButtonSetTime.Visible")));
			this.m_ButtonSetTime.Click += new System.EventHandler(this.m_ButtonSetTime_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.labelLockStyle);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.labelDeviceID);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.labelVersion);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.AllowDrop = true;
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labelSerialNum);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.labelLefttime);
			this.panel1.Controls.Add(this.labelValidDate);
			this.panel1.Controls.Add(this.labelLT);
			this.panel1.Controls.Add(this.labelVenderID);
			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
			this.panel1.Name = "panel1";
			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
			this.panel1.Text = resources.GetString("panel1.Text");
			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// labelSerialNum
			// 
			this.labelSerialNum.AccessibleDescription = resources.GetString("labelSerialNum.AccessibleDescription");
			this.labelSerialNum.AccessibleName = resources.GetString("labelSerialNum.AccessibleName");
			this.labelSerialNum.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelSerialNum.Anchor")));
			this.labelSerialNum.AutoSize = ((bool)(resources.GetObject("labelSerialNum.AutoSize")));
			this.labelSerialNum.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelSerialNum.Dock")));
			this.labelSerialNum.Enabled = ((bool)(resources.GetObject("labelSerialNum.Enabled")));
			this.labelSerialNum.Font = ((System.Drawing.Font)(resources.GetObject("labelSerialNum.Font")));
			this.labelSerialNum.Image = ((System.Drawing.Image)(resources.GetObject("labelSerialNum.Image")));
			this.labelSerialNum.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelSerialNum.ImageAlign")));
			this.labelSerialNum.ImageIndex = ((int)(resources.GetObject("labelSerialNum.ImageIndex")));
			this.labelSerialNum.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelSerialNum.ImeMode")));
			this.labelSerialNum.Location = ((System.Drawing.Point)(resources.GetObject("labelSerialNum.Location")));
			this.labelSerialNum.Name = "labelSerialNum";
			this.labelSerialNum.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelSerialNum.RightToLeft")));
			this.labelSerialNum.Size = ((System.Drawing.Size)(resources.GetObject("labelSerialNum.Size")));
			this.labelSerialNum.TabIndex = ((int)(resources.GetObject("labelSerialNum.TabIndex")));
			this.labelSerialNum.Text = resources.GetString("labelSerialNum.Text");
			this.labelSerialNum.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelSerialNum.TextAlign")));
			this.labelSerialNum.Visible = ((bool)(resources.GetObject("labelSerialNum.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// labelLefttime
			// 
			this.labelLefttime.AccessibleDescription = resources.GetString("labelLefttime.AccessibleDescription");
			this.labelLefttime.AccessibleName = resources.GetString("labelLefttime.AccessibleName");
			this.labelLefttime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelLefttime.Anchor")));
			this.labelLefttime.AutoSize = ((bool)(resources.GetObject("labelLefttime.AutoSize")));
			this.labelLefttime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelLefttime.Dock")));
			this.labelLefttime.Enabled = ((bool)(resources.GetObject("labelLefttime.Enabled")));
			this.labelLefttime.Font = ((System.Drawing.Font)(resources.GetObject("labelLefttime.Font")));
			this.labelLefttime.Image = ((System.Drawing.Image)(resources.GetObject("labelLefttime.Image")));
			this.labelLefttime.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLefttime.ImageAlign")));
			this.labelLefttime.ImageIndex = ((int)(resources.GetObject("labelLefttime.ImageIndex")));
			this.labelLefttime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelLefttime.ImeMode")));
			this.labelLefttime.Location = ((System.Drawing.Point)(resources.GetObject("labelLefttime.Location")));
			this.labelLefttime.Name = "labelLefttime";
			this.labelLefttime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelLefttime.RightToLeft")));
			this.labelLefttime.Size = ((System.Drawing.Size)(resources.GetObject("labelLefttime.Size")));
			this.labelLefttime.TabIndex = ((int)(resources.GetObject("labelLefttime.TabIndex")));
			this.labelLefttime.Text = resources.GetString("labelLefttime.Text");
			this.labelLefttime.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLefttime.TextAlign")));
			this.labelLefttime.Visible = ((bool)(resources.GetObject("labelLefttime.Visible")));
			// 
			// labelValidDate
			// 
			this.labelValidDate.AccessibleDescription = resources.GetString("labelValidDate.AccessibleDescription");
			this.labelValidDate.AccessibleName = resources.GetString("labelValidDate.AccessibleName");
			this.labelValidDate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelValidDate.Anchor")));
			this.labelValidDate.AutoSize = ((bool)(resources.GetObject("labelValidDate.AutoSize")));
			this.labelValidDate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelValidDate.Dock")));
			this.labelValidDate.Enabled = ((bool)(resources.GetObject("labelValidDate.Enabled")));
			this.labelValidDate.Font = ((System.Drawing.Font)(resources.GetObject("labelValidDate.Font")));
			this.labelValidDate.Image = ((System.Drawing.Image)(resources.GetObject("labelValidDate.Image")));
			this.labelValidDate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelValidDate.ImageAlign")));
			this.labelValidDate.ImageIndex = ((int)(resources.GetObject("labelValidDate.ImageIndex")));
			this.labelValidDate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelValidDate.ImeMode")));
			this.labelValidDate.Location = ((System.Drawing.Point)(resources.GetObject("labelValidDate.Location")));
			this.labelValidDate.Name = "labelValidDate";
			this.labelValidDate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelValidDate.RightToLeft")));
			this.labelValidDate.Size = ((System.Drawing.Size)(resources.GetObject("labelValidDate.Size")));
			this.labelValidDate.TabIndex = ((int)(resources.GetObject("labelValidDate.TabIndex")));
			this.labelValidDate.Text = resources.GetString("labelValidDate.Text");
			this.labelValidDate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelValidDate.TextAlign")));
			this.labelValidDate.Visible = ((bool)(resources.GetObject("labelValidDate.Visible")));
			// 
			// labelLT
			// 
			this.labelLT.AccessibleDescription = resources.GetString("labelLT.AccessibleDescription");
			this.labelLT.AccessibleName = resources.GetString("labelLT.AccessibleName");
			this.labelLT.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelLT.Anchor")));
			this.labelLT.AutoSize = ((bool)(resources.GetObject("labelLT.AutoSize")));
			this.labelLT.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelLT.Dock")));
			this.labelLT.Enabled = ((bool)(resources.GetObject("labelLT.Enabled")));
			this.labelLT.Font = ((System.Drawing.Font)(resources.GetObject("labelLT.Font")));
			this.labelLT.Image = ((System.Drawing.Image)(resources.GetObject("labelLT.Image")));
			this.labelLT.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLT.ImageAlign")));
			this.labelLT.ImageIndex = ((int)(resources.GetObject("labelLT.ImageIndex")));
			this.labelLT.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelLT.ImeMode")));
			this.labelLT.Location = ((System.Drawing.Point)(resources.GetObject("labelLT.Location")));
			this.labelLT.Name = "labelLT";
			this.labelLT.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelLT.RightToLeft")));
			this.labelLT.Size = ((System.Drawing.Size)(resources.GetObject("labelLT.Size")));
			this.labelLT.TabIndex = ((int)(resources.GetObject("labelLT.TabIndex")));
			this.labelLT.Text = resources.GetString("labelLT.Text");
			this.labelLT.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLT.TextAlign")));
			this.labelLT.Visible = ((bool)(resources.GetObject("labelLT.Visible")));
			// 
			// labelVenderID
			// 
			this.labelVenderID.AccessibleDescription = resources.GetString("labelVenderID.AccessibleDescription");
			this.labelVenderID.AccessibleName = resources.GetString("labelVenderID.AccessibleName");
			this.labelVenderID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelVenderID.Anchor")));
			this.labelVenderID.AutoSize = ((bool)(resources.GetObject("labelVenderID.AutoSize")));
			this.labelVenderID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelVenderID.Dock")));
			this.labelVenderID.Enabled = ((bool)(resources.GetObject("labelVenderID.Enabled")));
			this.labelVenderID.Font = ((System.Drawing.Font)(resources.GetObject("labelVenderID.Font")));
			this.labelVenderID.Image = ((System.Drawing.Image)(resources.GetObject("labelVenderID.Image")));
			this.labelVenderID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelVenderID.ImageAlign")));
			this.labelVenderID.ImageIndex = ((int)(resources.GetObject("labelVenderID.ImageIndex")));
			this.labelVenderID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelVenderID.ImeMode")));
			this.labelVenderID.Location = ((System.Drawing.Point)(resources.GetObject("labelVenderID.Location")));
			this.labelVenderID.Name = "labelVenderID";
			this.labelVenderID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelVenderID.RightToLeft")));
			this.labelVenderID.Size = ((System.Drawing.Size)(resources.GetObject("labelVenderID.Size")));
			this.labelVenderID.TabIndex = ((int)(resources.GetObject("labelVenderID.TabIndex")));
			this.labelVenderID.Text = resources.GetString("labelVenderID.Text");
			this.labelVenderID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelVenderID.TextAlign")));
			this.labelVenderID.Visible = ((bool)(resources.GetObject("labelVenderID.Visible")));
			// 
			// labelLockStyle
			// 
			this.labelLockStyle.AccessibleDescription = resources.GetString("labelLockStyle.AccessibleDescription");
			this.labelLockStyle.AccessibleName = resources.GetString("labelLockStyle.AccessibleName");
			this.labelLockStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelLockStyle.Anchor")));
			this.labelLockStyle.AutoSize = ((bool)(resources.GetObject("labelLockStyle.AutoSize")));
			this.labelLockStyle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelLockStyle.Dock")));
			this.labelLockStyle.Enabled = ((bool)(resources.GetObject("labelLockStyle.Enabled")));
			this.labelLockStyle.Font = ((System.Drawing.Font)(resources.GetObject("labelLockStyle.Font")));
			this.labelLockStyle.Image = ((System.Drawing.Image)(resources.GetObject("labelLockStyle.Image")));
			this.labelLockStyle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLockStyle.ImageAlign")));
			this.labelLockStyle.ImageIndex = ((int)(resources.GetObject("labelLockStyle.ImageIndex")));
			this.labelLockStyle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelLockStyle.ImeMode")));
			this.labelLockStyle.Location = ((System.Drawing.Point)(resources.GetObject("labelLockStyle.Location")));
			this.labelLockStyle.Name = "labelLockStyle";
			this.labelLockStyle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelLockStyle.RightToLeft")));
			this.labelLockStyle.Size = ((System.Drawing.Size)(resources.GetObject("labelLockStyle.Size")));
			this.labelLockStyle.TabIndex = ((int)(resources.GetObject("labelLockStyle.TabIndex")));
			this.labelLockStyle.Text = resources.GetString("labelLockStyle.Text");
			this.labelLockStyle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelLockStyle.TextAlign")));
			this.labelLockStyle.Visible = ((bool)(resources.GetObject("labelLockStyle.Visible")));
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
			// 
			// labelDeviceID
			// 
			this.labelDeviceID.AccessibleDescription = resources.GetString("labelDeviceID.AccessibleDescription");
			this.labelDeviceID.AccessibleName = resources.GetString("labelDeviceID.AccessibleName");
			this.labelDeviceID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelDeviceID.Anchor")));
			this.labelDeviceID.AutoSize = ((bool)(resources.GetObject("labelDeviceID.AutoSize")));
			this.labelDeviceID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelDeviceID.Dock")));
			this.labelDeviceID.Enabled = ((bool)(resources.GetObject("labelDeviceID.Enabled")));
			this.labelDeviceID.Font = ((System.Drawing.Font)(resources.GetObject("labelDeviceID.Font")));
			this.labelDeviceID.Image = ((System.Drawing.Image)(resources.GetObject("labelDeviceID.Image")));
			this.labelDeviceID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelDeviceID.ImageAlign")));
			this.labelDeviceID.ImageIndex = ((int)(resources.GetObject("labelDeviceID.ImageIndex")));
			this.labelDeviceID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelDeviceID.ImeMode")));
			this.labelDeviceID.Location = ((System.Drawing.Point)(resources.GetObject("labelDeviceID.Location")));
			this.labelDeviceID.Name = "labelDeviceID";
			this.labelDeviceID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelDeviceID.RightToLeft")));
			this.labelDeviceID.Size = ((System.Drawing.Size)(resources.GetObject("labelDeviceID.Size")));
			this.labelDeviceID.TabIndex = ((int)(resources.GetObject("labelDeviceID.TabIndex")));
			this.labelDeviceID.Text = resources.GetString("labelDeviceID.Text");
			this.labelDeviceID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelDeviceID.TextAlign")));
			this.labelDeviceID.Visible = ((bool)(resources.GetObject("labelDeviceID.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// labelVersion
			// 
			this.labelVersion.AccessibleDescription = resources.GetString("labelVersion.AccessibleDescription");
			this.labelVersion.AccessibleName = resources.GetString("labelVersion.AccessibleName");
			this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("labelVersion.Anchor")));
			this.labelVersion.AutoSize = ((bool)(resources.GetObject("labelVersion.AutoSize")));
			this.labelVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("labelVersion.Dock")));
			this.labelVersion.Enabled = ((bool)(resources.GetObject("labelVersion.Enabled")));
			this.labelVersion.Font = ((System.Drawing.Font)(resources.GetObject("labelVersion.Font")));
			this.labelVersion.Image = ((System.Drawing.Image)(resources.GetObject("labelVersion.Image")));
			this.labelVersion.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelVersion.ImageAlign")));
			this.labelVersion.ImageIndex = ((int)(resources.GetObject("labelVersion.ImageIndex")));
			this.labelVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("labelVersion.ImeMode")));
			this.labelVersion.Location = ((System.Drawing.Point)(resources.GetObject("labelVersion.Location")));
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("labelVersion.RightToLeft")));
			this.labelVersion.Size = ((System.Drawing.Size)(resources.GetObject("labelVersion.Size")));
			this.labelVersion.TabIndex = ((int)(resources.GetObject("labelVersion.TabIndex")));
			this.labelVersion.Text = resources.GetString("labelVersion.Text");
			this.labelVersion.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("labelVersion.TextAlign")));
			this.labelVersion.Visible = ((bool)(resources.GetObject("labelVersion.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// DongleKeyForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dividerPanel1);
			this.Controls.Add(this.m_SerialNoControlTime);
			this.Controls.Add(this.m_LabelPassword);
			this.Controls.Add(this.m_ButtonSetTime);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "DongleKeyForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.DongleKeyForm_Load);
			this.dividerPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DongleKeyForm_Load(object sender, System.EventArgs e)
		{
			BYHX_SL_RetValue ret;
            ret = BYHXSoftLock.GetPassWord(ref mDongleKey);//DongleKeyForm.LoadKeyFromFile();
            if (ret == BYHX_SL_RetValue.SUCSESS)
            {
                this.m_SerialNoControlTime.SetText(mDongleKey);
            }
            GetDongleInfo();
        }

		private void m_ButtonSetTime_Click(object sender, System.EventArgs e)
		{
			//int leftt = 0;
			string key =this.m_SerialNoControlTime.GetText();
			if(key!="" && key != null)
				key =key.Replace("-","");
			bool isValid = key!="" && key != null && (key.Length ==m_SerialNoControlTime.Count*m_SerialNoControlTime.CharsPerUnit);
			bool bSucces = false;
			if(isValid)
			{
				for(int i = 0; i < key.Length; i++)
					if(!char.IsLetterOrDigit(key,i))
					{
						isValid = false;
						break;
					}
			}
			if (isValid)
				bSucces =(BYHXSoftLock.SetPassWord(key) == BYHX_SL_RetValue.SUCSESS);

			if (bSucces)
			{
				mDongleKey = key;
				GetDongleInfo();
				//SaveKeyToFlie(mDongleKey);
			}
			else
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SetPasswordFail),
					ResString.GetProductName(),
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk);
				this.m_SerialNoControlTime.SetText(mDongleKey);
			}
		}

		unsafe private void GetDongleInfo()
		{
			try
			{
				byte[] infos = new byte[19];
				BYHX_SL_RetValue ret = BYHXSoftLock.GetDongleInfo(ref infos);
				if (ret == BYHX_SL_RetValue.SUCSESS)
				{
 					byte[] dtV = new byte[4];
					Buffer.BlockCopy(infos, 0, dtV, 0, dtV.Length);
					int Verion = BitConverter.ToInt32(dtV, 0);
					Buffer.BlockCopy(infos, 4, dtV, 0, dtV.Length);
					int SerialNum = BitConverter.ToInt32(dtV, 0);
					Buffer.BlockCopy(infos, 8, dtV, 0, dtV.Length);
					int lefttime = BitConverter.ToInt32(dtV, 0);
					Buffer.BlockCopy(infos, 12, dtV, 0, dtV.Length);
					int deadtime = BitConverter.ToInt32(dtV, 0);
					dtV = new byte[2];
					Buffer.BlockCopy(infos, 16, dtV, 0, dtV.Length);
					ushort venderID = BitConverter.ToUInt16(dtV,0);
					//S4_GET_SERIAL_NUMBER
					this.labelSerialNum.Text = SerialNum.ToString();
#if venderIDNum
                    labelVenderID.Text = venderID.ToString("X");
#else
                    string VenderName = ((VenderID)venderID).ToString();
                    if (VenderName.Contains("GONGZENG"))
				    {
                        labelVenderID.Text = VenderName.Replace("GONGZENG", "GONGZHENG");
                    }
                    else
                    {
                        labelVenderID.Text = VenderName;
                    }
#endif

                    if (lefttime>0 && deadtime == int.MaxValue)
					{
						this.labelValidDate.Text = this.labelLefttime.Text = ResString.GetResString("EncryptDog_Fatalpwd");
						this.labelValidDate.ForeColor = this.labelLefttime.ForeColor = Color.Green;
					}
					else if(lefttime>1)
					{
						this.labelValidDate.Text =StartTime.AddMinutes(deadtime).ToShortDateString();// +":"	+
						this.labelLefttime.Text = (lefttime/60).ToString() + ResString.GetResString("DisplayTime_Hour");
						this.labelValidDate.ForeColor = this.labelLefttime.ForeColor = Color.Green;
					}
					else
					{
						this.labelValidDate.Text = this.labelLefttime.Text =ResString.GetResString("EncryptDog_Expired");
						this.labelValidDate.ForeColor = this.labelLefttime.ForeColor = Color.Red;
					}
				}
				else
				{
					this.labelValidDate.Text = this.labelLefttime.Text =ResString.GetResString("EncryptDog_Expired");
					this.labelValidDate.ForeColor = this.labelLefttime.ForeColor = Color.Red;
				}

				SENSE4_CONTEXT stS4Ctx = new SENSE4_CONTEXT();
				uint dwResult = 0;
				uint pdwBytesReturned = 0;
				byte[] bDeviceType =new byte[1];
				if (Common.OpenS4ByIndex( BYHXSoftLock.FIRST_S4_INDEX, ref stS4Ctx) != BYHX_SL_RetValue.SUCSESS)
					return;
				string strDeviceID = "";
				for(int i = 0; i<stS4Ctx.bID.Length;i++)
					strDeviceID += stS4Ctx.bID[i].ToString("X").PadLeft(2,'0').ToUpper();
				this.labelDeviceID.Text = strDeviceID;

				string strVersion = "";
				byte[] ver  = BitConverter.GetBytes(stS4Ctx.dwVersion);
				for(int i = ver.Length-1; i>=0;i--)
					strVersion += ver[i].ToString("X").PadLeft(2,'0').ToUpper()+".";
				this.labelVersion.Text = strVersion.Substring(0,strVersion.Length -1);

				dwResult = S4_API.S4Control(ref stS4Ctx,S4_API.S4_GET_DEVICE_TYPE,null, 0, bDeviceType, (uint)bDeviceType.Length,ref pdwBytesReturned);
				if (dwResult == S4_API.S4_SUCCESS) 
				{
					switch(bDeviceType[0]) 
					{
						case S4_API.S4_LOCAL_DEVICE:
							this.labelLockStyle.Text =ResString.GetResString("EncryptDog_Locktype_LOCAL");
							break;
						case S4_API.S4_MASTER_DEVICE:
							this.labelLockStyle.Text =ResString.GetResString("EncryptDog_Locktype_MASTER");
							break;
						case S4_API.S4_SLAVE_DEVICE:
							this.labelLockStyle.Text =ResString.GetResString("EncryptDog_Locktype_SLAVE");
							break;
					}
				}
				//S4_GET_SERIAL_NUMBER
				if (ret != BYHX_SL_RetValue.SUCSESS)
					this.labelSerialNum.Text = BitConverter.ToInt32(stS4Ctx.bID,0).ToString();
				Common.ResetAndCloseS4(stS4Ctx);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void m_ButtonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
