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

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for PasswordForm.
	/// </summary>
	public class PasswordForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_ButtonExit;
		private System.Windows.Forms.Button m_ButtonOK;
		private System.Windows.Forms.Label m_LabelPassword;
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Label label1;
		private BYHXControls.SerialNoControl m_SerialNoControlTime;
		private BYHXControls.SerialNoControl m_SerialNoControlLang;
		private System.Windows.Forms.Button m_ButtonSetLang;
		private System.Windows.Forms.Button m_ButtonSetTime;
		private BYHXControls.SerialNoControl m_serialNoControl_ink;
		private System.Windows.Forms.Label label_ink;
		private System.Windows.Forms.Button m_ButtonSetInk;
		private BYHXControls.SerialNoControl m_serialNoControl_uv;
		private System.Windows.Forms.Button m_ButtonSetUv;
		private System.Windows.Forms.Label label_uv;
        private Panel panelTimePassword;
        private Panel panelLanPassWord;
        private Panel panelInkPassword;
        private Panel panelUvPassword;
#if UvAutoGeneration
		private System.Windows.Forms.Label label_uv;
		private System.Windows.Forms.Button m_ButtonSetUv;
		private BYHXControls.SerialNoControl m_serialNoControl_uv;
#endif
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PasswordForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if LIYUUSB
            m_LabelPassword.Text = this.Text;
#endif
#if !LIYUUSB
            panelLanPassWord.Visible =
            label1.Visible =
            m_SerialNoControlLang.Visible = 
            m_ButtonSetLang.Visible = 
                true;
#if INK_COUNTER
            panelInkPassword.Visible =
            label_ink.Visible =
            m_serialNoControl_ink.Visible =
            m_ButtonSetInk.Visible =
                true;
#endif
#endif
#if UV_LIMIT
            panelUvPassword.Visible =
            label_uv.Visible =
            m_serialNoControl_uv.Visible =
            m_ButtonSetUv.Visible =
                true;
#endif
            ReDesignKeyForm();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
            this.m_LabelPassword = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_ButtonSetLang = new System.Windows.Forms.Button();
            this.m_ButtonSetTime = new System.Windows.Forms.Button();
            this.m_ButtonSetInk = new System.Windows.Forms.Button();
            this.label_ink = new System.Windows.Forms.Label();
            this.m_ButtonSetUv = new System.Windows.Forms.Button();
            this.label_uv = new System.Windows.Forms.Label();
            this.panelTimePassword = new System.Windows.Forms.Panel();
            this.m_SerialNoControlTime = new BYHXControls.SerialNoControl();
            this.panelLanPassWord = new System.Windows.Forms.Panel();
            this.m_SerialNoControlLang = new BYHXControls.SerialNoControl();
            this.panelInkPassword = new System.Windows.Forms.Panel();
            this.m_serialNoControl_ink = new BYHXControls.SerialNoControl();
            this.panelUvPassword = new System.Windows.Forms.Panel();
            this.m_serialNoControl_uv = new BYHXControls.SerialNoControl();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.m_ButtonExit = new System.Windows.Forms.Button();
            this.panelTimePassword.SuspendLayout();
            this.panelLanPassWord.SuspendLayout();
            this.panelInkPassword.SuspendLayout();
            this.panelUvPassword.SuspendLayout();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_LabelPassword
            // 
            resources.ApplyResources(this.m_LabelPassword, "m_LabelPassword");
            this.m_LabelPassword.Name = "m_LabelPassword";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // m_ButtonSetLang
            // 
            resources.ApplyResources(this.m_ButtonSetLang, "m_ButtonSetLang");
            this.m_ButtonSetLang.Name = "m_ButtonSetLang";
            this.m_ButtonSetLang.Click += new System.EventHandler(this.m_ButtonSetLang_Click);
            // 
            // m_ButtonSetTime
            // 
            resources.ApplyResources(this.m_ButtonSetTime, "m_ButtonSetTime");
            this.m_ButtonSetTime.Name = "m_ButtonSetTime";
            this.m_ButtonSetTime.Click += new System.EventHandler(this.m_ButtonSetTime_Click);
            // 
            // m_ButtonSetInk
            // 
            resources.ApplyResources(this.m_ButtonSetInk, "m_ButtonSetInk");
            this.m_ButtonSetInk.Name = "m_ButtonSetInk";
            this.m_ButtonSetInk.Click += new System.EventHandler(this.m_ButtonSetInk_Click);
            // 
            // label_ink
            // 
            resources.ApplyResources(this.label_ink, "label_ink");
            this.label_ink.Name = "label_ink";
            // 
            // m_ButtonSetUv
            // 
            resources.ApplyResources(this.m_ButtonSetUv, "m_ButtonSetUv");
            this.m_ButtonSetUv.Name = "m_ButtonSetUv";
            this.m_ButtonSetUv.Click += new System.EventHandler(this.m_ButtonSetUv_Click);
            // 
            // label_uv
            // 
            resources.ApplyResources(this.label_uv, "label_uv");
            this.label_uv.Name = "label_uv";
            // 
            // panelTimePassword
            // 
            this.panelTimePassword.Controls.Add(this.m_ButtonSetTime);
            this.panelTimePassword.Controls.Add(this.m_LabelPassword);
            this.panelTimePassword.Controls.Add(this.m_SerialNoControlTime);
            resources.ApplyResources(this.panelTimePassword, "panelTimePassword");
            this.panelTimePassword.Name = "panelTimePassword";
            // 
            // m_SerialNoControlTime
            // 
            this.m_SerialNoControlTime.CharsPerUnit = 4;
            this.m_SerialNoControlTime.Count = 4;
            resources.ApplyResources(this.m_SerialNoControlTime, "m_SerialNoControlTime");
            this.m_SerialNoControlTime.Name = "m_SerialNoControlTime";
            this.m_SerialNoControlTime.SeparateString = "-";
            // 
            // panelLanPassWord
            // 
            this.panelLanPassWord.Controls.Add(this.m_SerialNoControlLang);
            this.panelLanPassWord.Controls.Add(this.label1);
            this.panelLanPassWord.Controls.Add(this.m_ButtonSetLang);
            resources.ApplyResources(this.panelLanPassWord, "panelLanPassWord");
            this.panelLanPassWord.Name = "panelLanPassWord";
            // 
            // m_SerialNoControlLang
            // 
            this.m_SerialNoControlLang.CharsPerUnit = 4;
            this.m_SerialNoControlLang.Count = 4;
            resources.ApplyResources(this.m_SerialNoControlLang, "m_SerialNoControlLang");
            this.m_SerialNoControlLang.Name = "m_SerialNoControlLang";
            this.m_SerialNoControlLang.SeparateString = "-";
            // 
            // panelInkPassword
            // 
            this.panelInkPassword.Controls.Add(this.label_ink);
            this.panelInkPassword.Controls.Add(this.m_ButtonSetInk);
            this.panelInkPassword.Controls.Add(this.m_serialNoControl_ink);
            resources.ApplyResources(this.panelInkPassword, "panelInkPassword");
            this.panelInkPassword.Name = "panelInkPassword";
            // 
            // m_serialNoControl_ink
            // 
            this.m_serialNoControl_ink.CharsPerUnit = 4;
            this.m_serialNoControl_ink.Count = 4;
            resources.ApplyResources(this.m_serialNoControl_ink, "m_serialNoControl_ink");
            this.m_serialNoControl_ink.Name = "m_serialNoControl_ink";
            this.m_serialNoControl_ink.SeparateString = "-";
            // 
            // panelUvPassword
            // 
            this.panelUvPassword.Controls.Add(this.m_ButtonSetUv);
            this.panelUvPassword.Controls.Add(this.label_uv);
            this.panelUvPassword.Controls.Add(this.m_serialNoControl_uv);
            resources.ApplyResources(this.panelUvPassword, "panelUvPassword");
            this.panelUvPassword.Name = "panelUvPassword";
            // 
            // m_serialNoControl_uv
            // 
            this.m_serialNoControl_uv.CharsPerUnit = 4;
            this.m_serialNoControl_uv.Count = 4;
            resources.ApplyResources(this.m_serialNoControl_uv, "m_serialNoControl_uv");
            this.m_serialNoControl_uv.Name = "m_serialNoControl_uv";
            this.m_serialNoControl_uv.SeparateString = "-";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            this.dividerPanel1.Controls.Add(this.m_ButtonExit);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // m_ButtonExit
            // 
            this.m_ButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonExit, "m_ButtonExit");
            this.m_ButtonExit.Name = "m_ButtonExit";
            // 
            // PasswordForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panelUvPassword);
            this.Controls.Add(this.panelInkPassword);
            this.Controls.Add(this.panelLanPassWord);
            this.Controls.Add(this.panelTimePassword);
            this.Controls.Add(this.dividerPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordForm";
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            this.panelTimePassword.ResumeLayout(false);
            this.panelTimePassword.PerformLayout();
            this.panelLanPassWord.ResumeLayout(false);
            this.panelLanPassWord.PerformLayout();
            this.panelInkPassword.ResumeLayout(false);
            this.panelInkPassword.PerformLayout();
            this.panelUvPassword.ResumeLayout(false);
            this.panelUvPassword.PerformLayout();
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_ButtonOK_Click(object sender, System.EventArgs e)
		{
		}

	    private void SetPassword(string strMainPwd, int bLang)
	    {
	        int iRet = 0;
	        // 

	        //validCheck should add here
	        if (strMainPwd != null && strMainPwd.Length != CoreConst.MAX_PASSWORD_LEN)
	        {
	            string sFilterString = strMainPwd.Replace("-", null);
	            iRet = CoreInterface.SetPassword(sFilterString.ToUpper(), sFilterString.Length, (ushort) BoardEnum.CoreBoard, bLang);
	        }
	        if (iRet == 0)
	        {
	            MessageBox.Show(ResString.GetEnumDisplayName(typeof (UIError), UIError.SetPasswordFail),
	                            ResString.GetProductName(),
	                            MessageBoxButtons.OK,
	                            MessageBoxIcon.Asterisk);
	        }
	        else
	        {
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SetPassword),
	                            ResString.GetProductName(),
	                            MessageBoxButtons.OK,
	                            MessageBoxIcon.Information);
	        }
	    }

	    private void PasswordForm_Load(object sender, System.EventArgs e)
		{
			string strTemp = "";
			int passLen = CoreConst.MAX_PASSWORD_LEN;
			byte[] info = new byte[passLen];
			int iRet = CoreInterface.GetPassword(info, ref passLen,(ushort)BoardEnum.CoreBoard,0);
			if(iRet != 0)
			{
				strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
			}
			else
			{
				strTemp = "";
			}
			m_SerialNoControlTime.SetText(strTemp);
#if !LIYUUSB
			iRet = CoreInterface.GetPassword(info, ref passLen,(ushort)BoardEnum.CoreBoard,1);
			if(iRet != 0)
			{
				strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
			}
			else
			{
				strTemp = "";
			}
			m_SerialNoControlLang.SetText(strTemp);
			//ink
			//iRet = CoreInterface.GetPassword(info, ref passLen,0,2);
			iRet = CoreInterface.GetPassword(info, ref passLen,(ushort)BoardEnum.CoreBoard,2);
			if(iRet != 0)
			{
				strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
			}
			else
			{
				strTemp = "";
			}
			m_serialNoControl_ink.SetText(strTemp);
			//UV
			//iRet = CoreInterface.GetPassword(info, ref passLen,0,3);
			iRet = CoreInterface.GetPassword(info, ref passLen,(ushort)BoardEnum.CoreBoard,3);
			if(iRet != 0)
			{
				strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
			}
			else
			{
				strTemp = "";
			}
			m_serialNoControl_uv.SetText(strTemp);
#if INK_COUNTER
			iRet = CoreInterface.GetPassword(info, ref passLen,(ushort)BoardEnum.CoreBoard,2);
			if(iRet != 0)
			{
				strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
			}
			else
			{
				strTemp = "";
			}
			m_serialNoControl_ink.SetText(strTemp);
#endif
#endif
		}

		private void m_ButtonSetTime_Click(object sender, System.EventArgs e)
		{
			string strMainPwd = m_SerialNoControlTime.GetText();
			SetPassword(strMainPwd,0);
		}

		private void m_ButtonSetLang_Click(object sender, System.EventArgs e)
		{
			string strMainPwd = m_SerialNoControlLang.GetText();
			SetPassword(strMainPwd,1);
		}
		private void m_ButtonSetInk_Click(object sender, System.EventArgs e)
		{
			string strMainPwd = m_serialNoControl_ink.GetText();
			SetPassword(strMainPwd,2);
		}

		private void m_ButtonSetUv_Click(object sender, System.EventArgs e)
		{
			string strMainPwd = m_serialNoControl_uv.GetText();
			SetPassword(strMainPwd,3);
		}
		private void ReDesignKeyForm()
		{
#if UvAutoGeneration
			int pith = this.label_ink.Location.Y - this.label1.Location.Y;
			this.Height += pith;

			m_ButtonSetUv = new System.Windows.Forms.Button();
			m_serialNoControl_uv = new BYHXControls.SerialNoControl();
			label_uv = new System.Windows.Forms.Label();

			label_uv.Text = ResString.GetResString("label_uv.Text");
			m_ButtonSetUv.Text = ResString.GetResString("m_ButtonSetUv.Text");
//			label_uv.Text = "UV Password:";
//			m_ButtonSetUv.Text = "Set";
		
			label_uv.Location = new Point(this.label_ink.Location.X,this.label_ink.Location.Y + pith);
			label_uv.Size = this.label_ink.Size;

			m_serialNoControl_uv.Location = new Point(this.m_serialNoControl_ink.Location.X,label_uv.Location.Y);
			m_serialNoControl_uv.Size = this.m_serialNoControl_ink.Size;
			m_serialNoControl_uv.CharsPerUnit = 4;
			m_serialNoControl_uv.Count = 4;

			m_ButtonSetUv.Location = new Point(this.m_ButtonSetInk.Location.X,label_uv.Location.Y);
			m_ButtonSetUv.Size = this.m_ButtonSetInk.Size;

			m_ButtonSetUv.Click += new System.EventHandler(this.m_ButtonSetUv_Click);
			this.Controls.Add(this.m_ButtonSetUv);
			this.Controls.Add(this.m_serialNoControl_uv);
			this.Controls.Add(this.label_uv);
#endif
		}

	}
}
