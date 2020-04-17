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

using BYHXPrinterManager;
namespace FactoryWriter
{
	/// <summary>
	/// Summary description for FormHeadBoard.
	/// </summary>
	public class FormHeadBoard : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownHBInt1;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownHBInt2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_ButtonHBRead;
		private System.Windows.Forms.Button m_ButtonCancel;
		private System.Windows.Forms.Button m_ButtonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormHeadBoard()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormHeadBoard));
			this.m_ButtonCancel = new System.Windows.Forms.Button();
			this.m_ButtonOK = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_NumericUpDownHBInt1 = new System.Windows.Forms.NumericUpDown();
			this.m_NumericUpDownHBInt2 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_ButtonHBRead = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHBInt1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHBInt2)).BeginInit();
			this.SuspendLayout();
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
			this.m_ButtonCancel.Visible = ((bool)(resources.GetObject("m_ButtonCancel.Visible")));
			// 
			// m_ButtonOK
			// 
			this.m_ButtonOK.AccessibleDescription = resources.GetString("m_ButtonOK.AccessibleDescription");
			this.m_ButtonOK.AccessibleName = resources.GetString("m_ButtonOK.AccessibleName");
			this.m_ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonOK.Anchor")));
			this.m_ButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonOK.BackgroundImage")));
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
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.m_NumericUpDownHBInt1);
			this.groupBox1.Controls.Add(this.m_NumericUpDownHBInt2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.m_ButtonHBRead);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			// m_NumericUpDownHBInt1
			// 
			this.m_NumericUpDownHBInt1.AccessibleDescription = resources.GetString("m_NumericUpDownHBInt1.AccessibleDescription");
			this.m_NumericUpDownHBInt1.AccessibleName = resources.GetString("m_NumericUpDownHBInt1.AccessibleName");
			this.m_NumericUpDownHBInt1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownHBInt1.Anchor")));
			this.m_NumericUpDownHBInt1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownHBInt1.Dock")));
			this.m_NumericUpDownHBInt1.Enabled = ((bool)(resources.GetObject("m_NumericUpDownHBInt1.Enabled")));
			this.m_NumericUpDownHBInt1.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownHBInt1.Font")));
			this.m_NumericUpDownHBInt1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownHBInt1.ImeMode")));
			this.m_NumericUpDownHBInt1.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownHBInt1.Location")));
			this.m_NumericUpDownHBInt1.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownHBInt1.Name = "m_NumericUpDownHBInt1";
			this.m_NumericUpDownHBInt1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownHBInt1.RightToLeft")));
			this.m_NumericUpDownHBInt1.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownHBInt1.Size")));
			this.m_NumericUpDownHBInt1.TabIndex = ((int)(resources.GetObject("m_NumericUpDownHBInt1.TabIndex")));
			this.m_NumericUpDownHBInt1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownHBInt1.TextAlign")));
			this.m_NumericUpDownHBInt1.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownHBInt1.ThousandsSeparator")));
			this.m_NumericUpDownHBInt1.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownHBInt1.UpDownAlign")));
			this.m_NumericUpDownHBInt1.Visible = ((bool)(resources.GetObject("m_NumericUpDownHBInt1.Visible")));
			// 
			// m_NumericUpDownHBInt2
			// 
			this.m_NumericUpDownHBInt2.AccessibleDescription = resources.GetString("m_NumericUpDownHBInt2.AccessibleDescription");
			this.m_NumericUpDownHBInt2.AccessibleName = resources.GetString("m_NumericUpDownHBInt2.AccessibleName");
			this.m_NumericUpDownHBInt2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownHBInt2.Anchor")));
			this.m_NumericUpDownHBInt2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownHBInt2.Dock")));
			this.m_NumericUpDownHBInt2.Enabled = ((bool)(resources.GetObject("m_NumericUpDownHBInt2.Enabled")));
			this.m_NumericUpDownHBInt2.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownHBInt2.Font")));
			this.m_NumericUpDownHBInt2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownHBInt2.ImeMode")));
			this.m_NumericUpDownHBInt2.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownHBInt2.Location")));
			this.m_NumericUpDownHBInt2.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownHBInt2.Name = "m_NumericUpDownHBInt2";
			this.m_NumericUpDownHBInt2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownHBInt2.RightToLeft")));
			this.m_NumericUpDownHBInt2.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownHBInt2.Size")));
			this.m_NumericUpDownHBInt2.TabIndex = ((int)(resources.GetObject("m_NumericUpDownHBInt2.TabIndex")));
			this.m_NumericUpDownHBInt2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownHBInt2.TextAlign")));
			this.m_NumericUpDownHBInt2.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownHBInt2.ThousandsSeparator")));
			this.m_NumericUpDownHBInt2.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownHBInt2.UpDownAlign")));
			this.m_NumericUpDownHBInt2.Visible = ((bool)(resources.GetObject("m_NumericUpDownHBInt2.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
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
			// m_ButtonHBRead
			// 
			this.m_ButtonHBRead.AccessibleDescription = resources.GetString("m_ButtonHBRead.AccessibleDescription");
			this.m_ButtonHBRead.AccessibleName = resources.GetString("m_ButtonHBRead.AccessibleName");
			this.m_ButtonHBRead.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonHBRead.Anchor")));
			this.m_ButtonHBRead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonHBRead.BackgroundImage")));
			this.m_ButtonHBRead.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonHBRead.Dock")));
			this.m_ButtonHBRead.Enabled = ((bool)(resources.GetObject("m_ButtonHBRead.Enabled")));
			this.m_ButtonHBRead.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonHBRead.FlatStyle")));
			this.m_ButtonHBRead.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonHBRead.Font")));
			this.m_ButtonHBRead.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonHBRead.Image")));
			this.m_ButtonHBRead.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonHBRead.ImageAlign")));
			this.m_ButtonHBRead.ImageIndex = ((int)(resources.GetObject("m_ButtonHBRead.ImageIndex")));
			this.m_ButtonHBRead.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonHBRead.ImeMode")));
			this.m_ButtonHBRead.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonHBRead.Location")));
			this.m_ButtonHBRead.Name = "m_ButtonHBRead";
			this.m_ButtonHBRead.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonHBRead.RightToLeft")));
			this.m_ButtonHBRead.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonHBRead.Size")));
			this.m_ButtonHBRead.TabIndex = ((int)(resources.GetObject("m_ButtonHBRead.TabIndex")));
			this.m_ButtonHBRead.Text = resources.GetString("m_ButtonHBRead.Text");
			this.m_ButtonHBRead.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonHBRead.TextAlign")));
			this.m_ButtonHBRead.Visible = ((bool)(resources.GetObject("m_ButtonHBRead.Visible")));
			this.m_ButtonHBRead.Click += new System.EventHandler(this.m_ButtonHBRead_Click);
			// 
			// FormHeadBoard
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
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_ButtonCancel);
			this.Controls.Add(this.m_ButtonOK);
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
			this.Name = "FormHeadBoard";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Load += new System.EventHandler(this.FormHeadBoard_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHBInt1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHBInt2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private void m_ButtonHBRead_Click(object sender, System.EventArgs e)
		{
			SWriteHeadBoardInfo hbInfo = new SWriteHeadBoardInfo();
			if(CoreInterface.GetHWHeadBoardInfo(ref hbInfo) != 0)
			{
				OnHeadBoardInfoChange(hbInfo);
				MessageBox.Show(this,"Read sucessfully!");
			}
			else
				MessageBox.Show(this,"Read Error!");
		}

		void OnHeadBoardInfoChange(SWriteHeadBoardInfo hbInfo)
		{
			m_NumericUpDownHBInt1.Value = hbInfo.m_nHeadFeature1;
			m_NumericUpDownHBInt2.Value = hbInfo.m_nHeadFeature2;
		}
		bool GetDefaultValue(out SWriteHeadBoardInfo hbInfo)
		{
			hbInfo = new SWriteHeadBoardInfo();
			hbInfo.m_nHeadFeature1 = 0;
			hbInfo.m_nHeadFeature2 = 0;
			return true;
		}

		private void FormHeadBoard_Load(object sender, System.EventArgs e)
		{
			SWriteHeadBoardInfo hb;
			GetDefaultValue(out hb);
			OnHeadBoardInfoChange(hb);
		}

	}
}
