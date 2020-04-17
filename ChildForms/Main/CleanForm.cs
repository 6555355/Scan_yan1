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
	/// Summary description for CleanForm.
	/// </summary>
	public class CleanForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_ButtonExit;
		private System.Windows.Forms.Button m_ButtonManualClean;
		private BYHXPrinterManager.Main.HeadLayoutControl m_HeadLayoutControl;
		private System.Windows.Forms.Panel m_PanelLayout;
		private DividerPanel.DividerPanel dividerPanel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CleanForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CleanForm));
            this.m_ButtonExit = new System.Windows.Forms.Button();
            this.m_ButtonManualClean = new System.Windows.Forms.Button();
            this.m_PanelLayout = new System.Windows.Forms.Panel();
            this.m_HeadLayoutControl = new BYHXPrinterManager.Main.HeadLayoutControl();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_PanelLayout.SuspendLayout();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ButtonExit
            // 
            this.m_ButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonExit, "m_ButtonExit");
            this.m_ButtonExit.Name = "m_ButtonExit";
            this.m_ButtonExit.Click += new System.EventHandler(this.m_ButtonExit_Click);
            // 
            // m_ButtonManualClean
            // 
            resources.ApplyResources(this.m_ButtonManualClean, "m_ButtonManualClean");
            this.m_ButtonManualClean.Name = "m_ButtonManualClean";
            this.m_ButtonManualClean.Click += new System.EventHandler(this.m_ButtonManualClean_Click);
            // 
            // m_PanelLayout
            // 
            this.m_PanelLayout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_PanelLayout.Controls.Add(this.m_HeadLayoutControl);
            resources.ApplyResources(this.m_PanelLayout, "m_PanelLayout");
            this.m_PanelLayout.Name = "m_PanelLayout";
            // 
            // m_HeadLayoutControl
            // 
            resources.ApplyResources(this.m_HeadLayoutControl, "m_HeadLayoutControl");
            this.m_HeadLayoutControl.Name = "m_HeadLayoutControl";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonExit);
            this.dividerPanel1.Controls.Add(this.m_ButtonManualClean);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // CleanForm
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.m_ButtonExit;
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.m_PanelLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CleanForm";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CleanForm_Closing);
            this.m_PanelLayout.ResumeLayout(false);
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_HeadLayoutControl.OnPrinterPropertyChange(sp);
		}

		private void m_ButtonManualClean_Click(object sender, System.EventArgs e)
		{
			m_HeadLayoutControl.ResetAllButton();
			CoreInterface.SendJetCommand((int)JetCmdEnum.SingleClean,-1);
		}

		private void m_ButtonExit_Click(object sender, System.EventArgs e)
		{
		}

		private void CleanForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CoreInterface.SendJetCommand((int)JetCmdEnum.ExitSingleCleanMode,0);
		}

	}
}
