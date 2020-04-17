/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for Updater.
	/// </summary>
	public class Updater : System.Windows.Forms.Form
	{
		private IPrinterChange m_iPrinterChange;
		private string m_UpdaterFileName;
		private uint   m_MessageUpdater = SystemCall.RegisterWindowMessage("MESSAGE_UPDATE_BYHXNAME");
		private bool   m_bMotion = false;
		private System.Windows.Forms.Button m_ButtonUpdate;
		private System.Windows.Forms.Button m_ButtonExit;
		private System.Windows.Forms.ProgressBar m_ProgressBarPercent;
		private System.Windows.Forms.Label m_LabelPercent;
		private System.Windows.Forms.TextBox m_TextBoxFileInfo;
		private System.Windows.Forms.Label m_LabelFileInfo;
		private System.Windows.Forms.Label m_LabelStatus;
		private System.Windows.Forms.TextBox m_TextBoxStatus;
		private DividerPanel.DividerPanel dividerPanel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Updater()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Updater));
			this.m_ProgressBarPercent = new System.Windows.Forms.ProgressBar();
			this.m_ButtonUpdate = new System.Windows.Forms.Button();
			this.m_ButtonExit = new System.Windows.Forms.Button();
			this.m_TextBoxFileInfo = new System.Windows.Forms.TextBox();
			this.m_TextBoxStatus = new System.Windows.Forms.TextBox();
			this.m_LabelPercent = new System.Windows.Forms.Label();
			this.m_LabelFileInfo = new System.Windows.Forms.Label();
			this.m_LabelStatus = new System.Windows.Forms.Label();
			this.dividerPanel1 = new DividerPanel.DividerPanel();
			this.dividerPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_ProgressBarPercent
			// 
			this.m_ProgressBarPercent.AccessibleDescription = resources.GetString("m_ProgressBarPercent.AccessibleDescription");
			this.m_ProgressBarPercent.AccessibleName = resources.GetString("m_ProgressBarPercent.AccessibleName");
			this.m_ProgressBarPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ProgressBarPercent.Anchor")));
			this.m_ProgressBarPercent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ProgressBarPercent.BackgroundImage")));
			this.m_ProgressBarPercent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ProgressBarPercent.Dock")));
			this.m_ProgressBarPercent.Enabled = ((bool)(resources.GetObject("m_ProgressBarPercent.Enabled")));
			this.m_ProgressBarPercent.Font = ((System.Drawing.Font)(resources.GetObject("m_ProgressBarPercent.Font")));
			this.m_ProgressBarPercent.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ProgressBarPercent.ImeMode")));
			this.m_ProgressBarPercent.Location = ((System.Drawing.Point)(resources.GetObject("m_ProgressBarPercent.Location")));
			this.m_ProgressBarPercent.Name = "m_ProgressBarPercent";
			this.m_ProgressBarPercent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ProgressBarPercent.RightToLeft")));
			this.m_ProgressBarPercent.Size = ((System.Drawing.Size)(resources.GetObject("m_ProgressBarPercent.Size")));
			this.m_ProgressBarPercent.Step = 1;
			this.m_ProgressBarPercent.TabIndex = ((int)(resources.GetObject("m_ProgressBarPercent.TabIndex")));
			this.m_ProgressBarPercent.Text = resources.GetString("m_ProgressBarPercent.Text");
			this.m_ProgressBarPercent.Visible = ((bool)(resources.GetObject("m_ProgressBarPercent.Visible")));
			// 
			// m_ButtonUpdate
			// 
			this.m_ButtonUpdate.AccessibleDescription = resources.GetString("m_ButtonUpdate.AccessibleDescription");
			this.m_ButtonUpdate.AccessibleName = resources.GetString("m_ButtonUpdate.AccessibleName");
			this.m_ButtonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonUpdate.Anchor")));
			this.m_ButtonUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonUpdate.BackgroundImage")));
			this.m_ButtonUpdate.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonUpdate.Dock")));
			this.m_ButtonUpdate.Enabled = ((bool)(resources.GetObject("m_ButtonUpdate.Enabled")));
			this.m_ButtonUpdate.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonUpdate.FlatStyle")));
			this.m_ButtonUpdate.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonUpdate.Font")));
			this.m_ButtonUpdate.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonUpdate.Image")));
			this.m_ButtonUpdate.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonUpdate.ImageAlign")));
			this.m_ButtonUpdate.ImageIndex = ((int)(resources.GetObject("m_ButtonUpdate.ImageIndex")));
			this.m_ButtonUpdate.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonUpdate.ImeMode")));
			this.m_ButtonUpdate.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonUpdate.Location")));
			this.m_ButtonUpdate.Name = "m_ButtonUpdate";
			this.m_ButtonUpdate.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonUpdate.RightToLeft")));
			this.m_ButtonUpdate.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonUpdate.Size")));
			this.m_ButtonUpdate.TabIndex = ((int)(resources.GetObject("m_ButtonUpdate.TabIndex")));
			this.m_ButtonUpdate.Text = resources.GetString("m_ButtonUpdate.Text");
			this.m_ButtonUpdate.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonUpdate.TextAlign")));
			this.m_ButtonUpdate.Visible = ((bool)(resources.GetObject("m_ButtonUpdate.Visible")));
			this.m_ButtonUpdate.Click += new System.EventHandler(this.m_ButtonUpdate_Click);
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
			// m_TextBoxFileInfo
			// 
			this.m_TextBoxFileInfo.AccessibleDescription = resources.GetString("m_TextBoxFileInfo.AccessibleDescription");
			this.m_TextBoxFileInfo.AccessibleName = resources.GetString("m_TextBoxFileInfo.AccessibleName");
			this.m_TextBoxFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxFileInfo.Anchor")));
			this.m_TextBoxFileInfo.AutoSize = ((bool)(resources.GetObject("m_TextBoxFileInfo.AutoSize")));
			this.m_TextBoxFileInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxFileInfo.BackgroundImage")));
			this.m_TextBoxFileInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxFileInfo.Dock")));
			this.m_TextBoxFileInfo.Enabled = ((bool)(resources.GetObject("m_TextBoxFileInfo.Enabled")));
			this.m_TextBoxFileInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxFileInfo.Font")));
			this.m_TextBoxFileInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxFileInfo.ImeMode")));
			this.m_TextBoxFileInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxFileInfo.Location")));
			this.m_TextBoxFileInfo.MaxLength = ((int)(resources.GetObject("m_TextBoxFileInfo.MaxLength")));
			this.m_TextBoxFileInfo.Multiline = ((bool)(resources.GetObject("m_TextBoxFileInfo.Multiline")));
			this.m_TextBoxFileInfo.Name = "m_TextBoxFileInfo";
			this.m_TextBoxFileInfo.PasswordChar = ((char)(resources.GetObject("m_TextBoxFileInfo.PasswordChar")));
			this.m_TextBoxFileInfo.ReadOnly = true;
			this.m_TextBoxFileInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxFileInfo.RightToLeft")));
			this.m_TextBoxFileInfo.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxFileInfo.ScrollBars")));
			this.m_TextBoxFileInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxFileInfo.Size")));
			this.m_TextBoxFileInfo.TabIndex = ((int)(resources.GetObject("m_TextBoxFileInfo.TabIndex")));
			this.m_TextBoxFileInfo.Text = resources.GetString("m_TextBoxFileInfo.Text");
			this.m_TextBoxFileInfo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxFileInfo.TextAlign")));
			this.m_TextBoxFileInfo.Visible = ((bool)(resources.GetObject("m_TextBoxFileInfo.Visible")));
			this.m_TextBoxFileInfo.WordWrap = ((bool)(resources.GetObject("m_TextBoxFileInfo.WordWrap")));
			// 
			// m_TextBoxStatus
			// 
			this.m_TextBoxStatus.AccessibleDescription = resources.GetString("m_TextBoxStatus.AccessibleDescription");
			this.m_TextBoxStatus.AccessibleName = resources.GetString("m_TextBoxStatus.AccessibleName");
			this.m_TextBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxStatus.Anchor")));
			this.m_TextBoxStatus.AutoSize = ((bool)(resources.GetObject("m_TextBoxStatus.AutoSize")));
			this.m_TextBoxStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxStatus.BackgroundImage")));
			this.m_TextBoxStatus.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxStatus.Dock")));
			this.m_TextBoxStatus.Enabled = ((bool)(resources.GetObject("m_TextBoxStatus.Enabled")));
			this.m_TextBoxStatus.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxStatus.Font")));
			this.m_TextBoxStatus.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxStatus.ImeMode")));
			this.m_TextBoxStatus.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxStatus.Location")));
			this.m_TextBoxStatus.MaxLength = ((int)(resources.GetObject("m_TextBoxStatus.MaxLength")));
			this.m_TextBoxStatus.Multiline = ((bool)(resources.GetObject("m_TextBoxStatus.Multiline")));
			this.m_TextBoxStatus.Name = "m_TextBoxStatus";
			this.m_TextBoxStatus.PasswordChar = ((char)(resources.GetObject("m_TextBoxStatus.PasswordChar")));
			this.m_TextBoxStatus.ReadOnly = true;
			this.m_TextBoxStatus.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxStatus.RightToLeft")));
			this.m_TextBoxStatus.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxStatus.ScrollBars")));
			this.m_TextBoxStatus.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxStatus.Size")));
			this.m_TextBoxStatus.TabIndex = ((int)(resources.GetObject("m_TextBoxStatus.TabIndex")));
			this.m_TextBoxStatus.Text = resources.GetString("m_TextBoxStatus.Text");
			this.m_TextBoxStatus.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxStatus.TextAlign")));
			this.m_TextBoxStatus.Visible = ((bool)(resources.GetObject("m_TextBoxStatus.Visible")));
			this.m_TextBoxStatus.WordWrap = ((bool)(resources.GetObject("m_TextBoxStatus.WordWrap")));
			// 
			// m_LabelPercent
			// 
			this.m_LabelPercent.AccessibleDescription = resources.GetString("m_LabelPercent.AccessibleDescription");
			this.m_LabelPercent.AccessibleName = resources.GetString("m_LabelPercent.AccessibleName");
			this.m_LabelPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelPercent.Anchor")));
			this.m_LabelPercent.AutoSize = ((bool)(resources.GetObject("m_LabelPercent.AutoSize")));
			this.m_LabelPercent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelPercent.Dock")));
			this.m_LabelPercent.Enabled = ((bool)(resources.GetObject("m_LabelPercent.Enabled")));
			this.m_LabelPercent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_LabelPercent.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelPercent.Font")));
			this.m_LabelPercent.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelPercent.Image")));
			this.m_LabelPercent.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPercent.ImageAlign")));
			this.m_LabelPercent.ImageIndex = ((int)(resources.GetObject("m_LabelPercent.ImageIndex")));
			this.m_LabelPercent.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelPercent.ImeMode")));
			this.m_LabelPercent.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelPercent.Location")));
			this.m_LabelPercent.Name = "m_LabelPercent";
			this.m_LabelPercent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelPercent.RightToLeft")));
			this.m_LabelPercent.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelPercent.Size")));
			this.m_LabelPercent.TabIndex = ((int)(resources.GetObject("m_LabelPercent.TabIndex")));
			this.m_LabelPercent.Text = resources.GetString("m_LabelPercent.Text");
			this.m_LabelPercent.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPercent.TextAlign")));
			this.m_LabelPercent.Visible = ((bool)(resources.GetObject("m_LabelPercent.Visible")));
			// 
			// m_LabelFileInfo
			// 
			this.m_LabelFileInfo.AccessibleDescription = resources.GetString("m_LabelFileInfo.AccessibleDescription");
			this.m_LabelFileInfo.AccessibleName = resources.GetString("m_LabelFileInfo.AccessibleName");
			this.m_LabelFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelFileInfo.Anchor")));
			this.m_LabelFileInfo.AutoSize = ((bool)(resources.GetObject("m_LabelFileInfo.AutoSize")));
			this.m_LabelFileInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelFileInfo.Dock")));
			this.m_LabelFileInfo.Enabled = ((bool)(resources.GetObject("m_LabelFileInfo.Enabled")));
			this.m_LabelFileInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelFileInfo.Font")));
			this.m_LabelFileInfo.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelFileInfo.Image")));
			this.m_LabelFileInfo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelFileInfo.ImageAlign")));
			this.m_LabelFileInfo.ImageIndex = ((int)(resources.GetObject("m_LabelFileInfo.ImageIndex")));
			this.m_LabelFileInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelFileInfo.ImeMode")));
			this.m_LabelFileInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelFileInfo.Location")));
			this.m_LabelFileInfo.Name = "m_LabelFileInfo";
			this.m_LabelFileInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelFileInfo.RightToLeft")));
			this.m_LabelFileInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelFileInfo.Size")));
			this.m_LabelFileInfo.TabIndex = ((int)(resources.GetObject("m_LabelFileInfo.TabIndex")));
			this.m_LabelFileInfo.Text = resources.GetString("m_LabelFileInfo.Text");
			this.m_LabelFileInfo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelFileInfo.TextAlign")));
			this.m_LabelFileInfo.Visible = ((bool)(resources.GetObject("m_LabelFileInfo.Visible")));
			// 
			// m_LabelStatus
			// 
			this.m_LabelStatus.AccessibleDescription = resources.GetString("m_LabelStatus.AccessibleDescription");
			this.m_LabelStatus.AccessibleName = resources.GetString("m_LabelStatus.AccessibleName");
			this.m_LabelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelStatus.Anchor")));
			this.m_LabelStatus.AutoSize = ((bool)(resources.GetObject("m_LabelStatus.AutoSize")));
			this.m_LabelStatus.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelStatus.Dock")));
			this.m_LabelStatus.Enabled = ((bool)(resources.GetObject("m_LabelStatus.Enabled")));
			this.m_LabelStatus.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelStatus.Font")));
			this.m_LabelStatus.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelStatus.Image")));
			this.m_LabelStatus.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelStatus.ImageAlign")));
			this.m_LabelStatus.ImageIndex = ((int)(resources.GetObject("m_LabelStatus.ImageIndex")));
			this.m_LabelStatus.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelStatus.ImeMode")));
			this.m_LabelStatus.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelStatus.Location")));
			this.m_LabelStatus.Name = "m_LabelStatus";
			this.m_LabelStatus.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelStatus.RightToLeft")));
			this.m_LabelStatus.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelStatus.Size")));
			this.m_LabelStatus.TabIndex = ((int)(resources.GetObject("m_LabelStatus.TabIndex")));
			this.m_LabelStatus.Text = resources.GetString("m_LabelStatus.Text");
			this.m_LabelStatus.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelStatus.TextAlign")));
			this.m_LabelStatus.Visible = ((bool)(resources.GetObject("m_LabelStatus.Visible")));
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
			this.dividerPanel1.Controls.Add(this.m_ButtonUpdate);
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
			// Updater
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.dividerPanel1);
			this.Controls.Add(this.m_LabelFileInfo);
			this.Controls.Add(this.m_LabelPercent);
			this.Controls.Add(this.m_TextBoxFileInfo);
			this.Controls.Add(this.m_TextBoxStatus);
			this.Controls.Add(this.m_ProgressBarPercent);
			this.Controls.Add(this.m_LabelStatus);
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
			this.Name = "Updater";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.dividerPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}
		public void SetUpdaterFile(string name)
		{
			m_UpdaterFileName = name;
			m_TextBoxFileInfo.Text = GetFileInfo();
			m_TextBoxStatus.Text = "";
			if(Path.GetFileName(m_UpdaterFileName).ToUpper().StartsWith("DSPMOTION"))
				m_bMotion = true;
			else
				m_bMotion = false;

		}
		private string GetFileInfo()
		{
			try 
			{
				const int Updater_File_Comment_Length = 512 * 4;
				char []  readBuf = new char[Updater_File_Comment_Length];
				readBuf[0] = (char)0;
				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				TextReader	txtReader	= new StreamReader(fileStream);
				int readlen = txtReader.Read(readBuf,0,Updater_File_Comment_Length);
				txtReader.Close();
				fileStream.Close();
				return new String(readBuf);
			}
			catch{}
			return "";
		}
		protected override void WndProc(ref System.Windows.Forms.Message msg)
		{
			const string LineEnd = "\r\n";
			// Dispose user message.
			if(msg.Msg == m_MessageUpdater)
			{
				int nMsg = (int)msg.WParam;
				CoreMsgEnum coremsg = (CoreMsgEnum)nMsg;
				switch(coremsg)
				{
					case CoreMsgEnum.Status_Change:
					{
						int status = msg.LParam.ToInt32();

//						int portid = (int)msg.LParam; 
//						BoardEnum board = (BoardEnum)portid;
//						m_TextBoxStatus.Text +=  ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Status)+ 
//							ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Begin) +
//							board.ToString();

						m_TextBoxStatus.Text +=  status.ToString();
						m_TextBoxStatus.Text +=LineEnd;
					}
						break;
					case CoreMsgEnum.Percentage:
					{
						int percent = (int)msg.LParam;
						if(percent > 100)
						{
							percent = 100;
						}
						if(percent < 0)
						{
							percent = 0;
						}
						m_ProgressBarPercent.Value = (int)percent;
#if false
						m_TextBoxStatus.Text += ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Status) + percent.ToString() +
							ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Percentage);
						m_TextBoxStatus.Text +=LineEnd;
#endif				
					}
						break;
					case CoreMsgEnum.ErrorCode:
					{
						int errorcode = (int)msg.LParam;
//						m_TextBoxStatus.Text += ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Status) + 
//							ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Failed);
						m_TextBoxStatus.Text += SErrorCode.GetInfoFromErrCode(errorcode);
						m_TextBoxStatus.Text += "["+errorcode.ToString("X")+"]";
						m_TextBoxStatus.Text +=LineEnd;

						SErrorCode serrorcode = new SErrorCode(errorcode);
						ErrorCause cause = (ErrorCause)serrorcode.nErrorCause;
						if(cause == ErrorCause.CoreBoard && (ErrorAction)serrorcode.nErrorAction == ErrorAction.Updating)
						{
							if(0 != serrorcode.n16ErrorCode)
							{
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearUpdatingStatus,0);
							}
							if(serrorcode.n16ErrorCode == 1)
							{
								string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.UpdateSuccess);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
							else
							{
								string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.UpdateFail);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
						}
					}
					break;
					case CoreMsgEnum.Job_Begin:
						m_TextBoxStatus.Text += ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Status) + 
							ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Begin);
						m_TextBoxStatus.Text +=LineEnd;
						break;
					case CoreMsgEnum.Job_End:
						m_TextBoxStatus.Text += ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Status) + 
							ResString.GetEnumDisplayName(typeof(UpdateDisplay),UpdateDisplay.Success);
						m_TextBoxStatus.Text +=LineEnd;
						break;
				}
				return;
			}
			
			base.WndProc(ref msg);
		}

		private void m_ButtonUpdate_Click(object sender, System.EventArgs e)
		{
#if LIYUUSB
			MillingBoard();
#else
			if(m_bMotion)
				UpdateMotion();
			else
				UpdateCoreBoard();
#endif
		}
		private void MillingBoard()
		{
			CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
			CoreInterface.BeginMilling(m_UpdaterFileName);

		}
		private void UpdateCoreBoard()
		{
			bool bRead = false;
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				const int  USB_EP2_MIN_PACKAGESIZE =  1024;

				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				int				buffersize = (fileLen + USB_EP2_MIN_PACKAGESIZE -1)/USB_EP2_MIN_PACKAGESIZE * USB_EP2_MIN_PACKAGESIZE;
				buffer			= new byte[buffersize];
				int				readBytes		= 0;

				fileStream.Seek(0,SeekOrigin.Begin);
				readBytes	= binaryReader.Read(buffer,0,fileLen);
				Debug.Assert(fileLen == readBytes);

				binaryReader.Close();
				fileStream.Close();
				bRead = true;
			}
			catch{}
			if(bRead)
			{
				CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
				CoreInterface.BeginUpdating(buffer,fileLen);
			}

		}

		private void UpdateMotion()
		{
			ThreadStart	threadStart	= new ThreadStart(SendPackageProc);
			Thread mPrintThread = new Thread(threadStart);
			mPrintThread.IsBackground = true;
			mPrintThread.Start();
		}

		private void SendPackageProc()
		{
			bool bRead = false;
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				int				buffersize = fileLen;
				buffer			= new byte[buffersize];
				int				readBytes		= 0;

				fileStream.Seek(0,SeekOrigin.Begin);
				readBytes	= binaryReader.Read(buffer,0,fileLen);
				Debug.Assert(fileLen == readBytes);

				binaryReader.Close();
				fileStream.Close();
				bRead = true;
			}
			catch{}
			if(bRead)
			{
				const int port = 1;
				const byte PRINTER_PIPECMDSIZE = 26;
				byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
				//First Send Begin Updater
				m_pData[0] = 2;
				m_pData[1] = 0x3d;
				CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);

				int srcOffset = 0;
				
				while(fileLen>0)
				{
					byte curLen = (fileLen>=PRINTER_PIPECMDSIZE)?PRINTER_PIPECMDSIZE:(byte)fileLen;
					fileLen-= curLen;
					m_pData[0] = (byte)(curLen+2);
					//m_pData[1] = 0x36;
					m_pData[1] = 0x3e;
					Buffer.BlockCopy(buffer,srcOffset,m_pData,2,curLen);
					srcOffset += curLen;
					CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
					Thread.Sleep(300);
				}
				//Last Send End Updater
				m_pData[0] = 2;
				m_pData[1] = 0x3f;
				CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);

				//CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
				//CoreInterface.BeginUpdating(buffer,fileLen);
			}
		}


	}
}
