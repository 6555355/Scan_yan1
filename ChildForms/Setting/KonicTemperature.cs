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
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Main;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for KonicTemperature.
	/// </summary>
	public class KonicTemperature : System.Windows.Forms.Form
	{
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private BYHXPrinterManager.Setting.XaarTempertureSetting xaarTempertureSetting1;
        private KonicTemperature_Layout m_KonicTemperatureSetting1;

		private bool m_bNewXaar382 = false;
		public KonicTemperature()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KonicTemperature));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            this.m_KonicTemperatureSetting1 = new BYHXPrinterManager.Setting.KonicTemperature_Layout();
            this.xaarTempertureSetting1 = new BYHXPrinterManager.Setting.XaarTempertureSetting();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_KonicTemperatureSetting1
            // 
            this.m_KonicTemperatureSetting1.Divider = false;
            resources.ApplyResources(this.m_KonicTemperatureSetting1, "m_KonicTemperatureSetting1");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.m_KonicTemperatureSetting1.GradientColors = style1;
            this.m_KonicTemperatureSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_KonicTemperatureSetting1.GrouperTitleStyle = null;
            this.m_KonicTemperatureSetting1.IsDirty = false;
            this.m_KonicTemperatureSetting1.Name = "m_KonicTemperatureSetting1";
            // 
            // xaarTempertureSetting1
            // 
            this.xaarTempertureSetting1.Divider = false;
            style2.Color1 = System.Drawing.SystemColors.Control;
            style2.Color2 = System.Drawing.SystemColors.Control;
            this.xaarTempertureSetting1.GradientColors = style2;
            this.xaarTempertureSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.xaarTempertureSetting1.GrouperTitleStyle = null;
            resources.ApplyResources(this.xaarTempertureSetting1, "xaarTempertureSetting1");
            this.xaarTempertureSetting1.Name = "xaarTempertureSetting1";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonCancel);
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonOK_Click);
            // 
            // KonicTemperature
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_KonicTemperatureSetting1);
            this.Controls.Add(this.xaarTempertureSetting1);
            this.Controls.Add(this.dividerPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "KonicTemperature";
            this.Closed += new System.EventHandler(this.KonicTemperature_Closed);
            this.Load += new System.EventHandler(this.KonicTemperature_Load);
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


	    private SPrinterProperty _printerProperty;
        public void OnPrinterPropertyChange( SPrinterProperty sp)
        {
            _printerProperty = sp;
			m_bNewXaar382 = SPrinterProperty.IsNewXaar382(sp.ePrinterHead);
			if(m_bNewXaar382)
			{
				m_KonicTemperatureSetting1.Visible = false;
				this.xaarTempertureSetting1.Visible = true;
				this.xaarTempertureSetting1.Dock = DockStyle.Fill;
				this.xaarTempertureSetting1.OnPrinterPropertyChange(sp);
			}
			else
			{
				m_KonicTemperatureSetting1.Visible = true;
				this.xaarTempertureSetting1.Visible = false;
//				this.m_KonicTemperatureSetting1.Dock = DockStyle.Fill;
				m_KonicTemperatureSetting1.OnPrinterPropertyChange(sp);
				this.Width = m_KonicTemperatureSetting1.Width+8;
			}

        }
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.OnPrinterSettingChange(ss);
			else
				m_KonicTemperatureSetting1.OnPrinterSettingChange(ss);
		}
		public void OnPreferenceChange( UIPreference up)
		{
			//m_KonicaTemperatureSetting.OnPreferenceChange(up);
		}
		public void OnGetPrinterSetting(ref AllParam allParam,ref bool bChangeProperty)
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.OnGetPrinterSetting(ref allParam.PrinterSetting);
			else
				m_KonicTemperatureSetting1.OnGetPrinterSetting(ref allParam.PrinterSetting);

		}
		public void OnRealTimeChange()
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.OnRealTimeChange();
			else
				m_KonicTemperatureSetting1.OnRealTimeChange();

            this.Width = m_KonicTemperatureSetting1.Width + 8;
		}
		public void ApplyToBoard()
		{
			if(m_bNewXaar382)
			{
//				this.xaarTempertureSetting1.ApplyToBoard();
			}
			else
				m_KonicTemperatureSetting1.ApplyToBoard();
		}
		private void m_ButtonOK_Click(object sender, System.EventArgs e)
		{
			if(m_bNewXaar382 && this.xaarTempertureSetting1.IsDirty)
			{
				DialogResult dr =MessageBox.Show(ResString.GetResString("Xaar382_UnsavedWarning"),ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question);
				if(dr == DialogResult.No)
					this.DialogResult = DialogResult.None;
			}
		}
		public void SetGroupBoxStyle(Grouper ts)
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.GrouperTitleStyle = ts;
			else
				this.m_KonicTemperatureSetting1.GrouperTitleStyle = ts;
		}

		private void KonicTemperature_Closed(object sender, System.EventArgs e)
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.StopTimer();
		}

		private void KonicTemperature_Load(object sender, System.EventArgs e)
		{
			if(m_bNewXaar382)
				this.xaarTempertureSetting1.StartTimer();

            if (SPrinterProperty.IsKonic1800i(_printerProperty.ePrinterHead))
            {
                this.Location = new Point(0, 0);
                this.WindowState = FormWindowState.Maximized;
            }

		}
        public void OnEp6DataChanged(int ep6Cmd, int index, byte[] buf)
        {
            if (!m_bNewXaar382)
                m_KonicTemperatureSetting1.OnEp6DataChanged(ep6Cmd, index, buf);
        }
	}
}
