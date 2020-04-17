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
using System.Reflection;
using System.IO;
using BYHXControls;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Setting;



namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for CaliWizard.
	/// </summary>
	public class TipsWizard : System.Windows.Forms.Form
	{
		//根据主窗口大小改变当前窗口大小
//		private int width;
//		private int height;
//		private FormStartPosition StartPosition;
		private int m_subStep = 0;
		private SPrinterSetting m_CaliSetting;
		private IPrinterChange m_iPrinterChange;
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;

		private BYHXControls.Wizard m_WizardCalibration;
		private BYHXControls.WizardPage m_WizardPageWelcom;
		private BYHXControls.WizardPage m_WizardPageFinish;
		private BYHXControls.WizardPage m_WizardPageGuide;
		private BYHXControls.WizardPage m_WizardPageMechanic;
		private BYHXControls.WizardPage m_WizardPage1;
		private BYHXControls.WizardPage m_WizardPage2;
		private BYHXControls.WizardPage m_WizardPage3;
		//private BYHXControls.WizardPage m_WizardPage4;

		private System.Windows.Forms.CheckBox checkBoxShowAttention;
		private System.Windows.Forms.Label m_labTitle;
		private System.Windows.Forms.Label m_labConten;
		private BYHXControls.WizardPage m_WizardPageCali;
		
		//获取全局程序集设置
		private AllParam m_param;
		//private System.Windows.Forms.CheckBox checkBoxShowAttention;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TipsWizard(AllParam parentParam)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//SetScale();
			this.Width = Screen.PrimaryScreen.Bounds.Width - 200;

			this.Height = Screen.PrimaryScreen.Bounds.Height - 200 ;
			InitializeLocalize();	
			
			checkBoxShowAttention.Checked = !parentParam.Preference.bShowAttentionOnLoad;
			m_param = parentParam;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TipsWizard));
			this.m_WizardCalibration = new BYHXControls.Wizard();
			this.m_WizardPageWelcom = new BYHXControls.WizardPage();
			this.checkBoxShowAttention = new System.Windows.Forms.CheckBox();
			this.m_WizardPageFinish = new BYHXControls.WizardPage();
			this.m_WizardPage3 = new BYHXControls.WizardPage();
			this.m_WizardPage2 = new BYHXControls.WizardPage();
			this.m_WizardPage1 = new BYHXControls.WizardPage();
			this.m_WizardPageCali = new BYHXControls.WizardPage();
			this.m_WizardPageMechanic = new BYHXControls.WizardPage();
			this.m_labTitle = new System.Windows.Forms.Label();
			this.m_labConten = new System.Windows.Forms.Label();
			this.m_WizardCalibration.SuspendLayout();
			this.m_WizardPageWelcom.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_WizardCalibration
			// 
			this.m_WizardCalibration.AccessibleDescription = resources.GetString("m_WizardCalibration.AccessibleDescription");
			this.m_WizardCalibration.AccessibleName = resources.GetString("m_WizardCalibration.AccessibleName");
			this.m_WizardCalibration.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardCalibration.Anchor")));
			this.m_WizardCalibration.AutoScroll = ((bool)(resources.GetObject("m_WizardCalibration.AutoScroll")));
			this.m_WizardCalibration.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardCalibration.AutoScrollMargin")));
			this.m_WizardCalibration.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardCalibration.AutoScrollMinSize")));
			this.m_WizardCalibration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardCalibration.BackgroundImage")));
			this.m_WizardCalibration.Controls.Add(this.m_WizardPageWelcom);
			this.m_WizardCalibration.Controls.Add(this.m_WizardPageFinish);
			this.m_WizardCalibration.Controls.Add(this.m_WizardPage3);
			this.m_WizardCalibration.Controls.Add(this.m_WizardPage2);
			this.m_WizardCalibration.Controls.Add(this.m_WizardPage1);
			this.m_WizardCalibration.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardCalibration.Dock")));
			this.m_WizardCalibration.Enabled = ((bool)(resources.GetObject("m_WizardCalibration.Enabled")));
			this.m_WizardCalibration.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardCalibration.Font")));
			this.m_WizardCalibration.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_WizardCalibration.HeaderTitleFont = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_WizardCalibration.HelpEnabled = true;
			this.m_WizardCalibration.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardCalibration.ImeMode")));
			this.m_WizardCalibration.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardCalibration.Location")));
			this.m_WizardCalibration.Name = "m_WizardCalibration";
			this.m_WizardCalibration.Pages.AddRange(new BYHXControls.WizardPage[] {
																					  this.m_WizardPageWelcom,
																					  this.m_WizardPage1,
																					  this.m_WizardPage2,
																					  this.m_WizardPage3,
																					  this.m_WizardPageFinish});
			this.m_WizardCalibration.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardCalibration.RightToLeft")));
			this.m_WizardCalibration.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardCalibration.Size")));
			this.m_WizardCalibration.TabIndex = ((int)(resources.GetObject("m_WizardCalibration.TabIndex")));
			this.m_WizardCalibration.Visible = ((bool)(resources.GetObject("m_WizardCalibration.Visible")));
			this.m_WizardCalibration.WelcomeFont = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_WizardCalibration.BeforeSwitchPages += new BYHXControls.Wizard.BeforeSwitchPagesEventHandler(this.m_WizardCalibration_BeforeSwitchPages);
			this.m_WizardCalibration.Help += new System.EventHandler(this.m_WizardCalibration_Help);
			this.m_WizardCalibration.AfterSwitchPages += new BYHXControls.Wizard.AfterSwitchPagesEventHandler(this.m_WizardCalibration_AfterSwitchPages);
			this.m_WizardCalibration.Finish += new System.EventHandler(this.m_WizardCalibration_Finish);
			this.m_WizardCalibration.Cancel += new System.ComponentModel.CancelEventHandler(this.m_WizardCalibration_Cancel);
			this.m_WizardCalibration.Save += new System.EventHandler(this.m_WizardCalibration_Save);
			// 
			// m_WizardPageWelcom
			// 
			this.m_WizardPageWelcom.AccessibleDescription = resources.GetString("m_WizardPageWelcom.AccessibleDescription");
			this.m_WizardPageWelcom.AccessibleName = resources.GetString("m_WizardPageWelcom.AccessibleName");
			this.m_WizardPageWelcom.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPageWelcom.Anchor")));
			this.m_WizardPageWelcom.AutoScroll = ((bool)(resources.GetObject("m_WizardPageWelcom.AutoScroll")));
			this.m_WizardPageWelcom.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPageWelcom.AutoScrollMargin")));
			this.m_WizardPageWelcom.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPageWelcom.AutoScrollMinSize")));
			this.m_WizardPageWelcom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPageWelcom.BackgroundImage")));
			this.m_WizardPageWelcom.Controls.Add(this.checkBoxShowAttention);
			this.m_WizardPageWelcom.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPageWelcom.Dock")));
			this.m_WizardPageWelcom.Enabled = ((bool)(resources.GetObject("m_WizardPageWelcom.Enabled")));
			this.m_WizardPageWelcom.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPageWelcom.Font")));
			this.m_WizardPageWelcom.ForeColor = System.Drawing.Color.Black;
			this.m_WizardPageWelcom.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPageWelcom.ImeMode")));
			this.m_WizardPageWelcom.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPageWelcom.Location")));
			this.m_WizardPageWelcom.Name = "m_WizardPageWelcom";
			this.m_WizardPageWelcom.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPageWelcom.RightToLeft")));
			this.m_WizardPageWelcom.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPageWelcom.Size")));
			this.m_WizardPageWelcom.TabIndex = ((int)(resources.GetObject("m_WizardPageWelcom.TabIndex")));
			this.m_WizardPageWelcom.Text = resources.GetString("m_WizardPageWelcom.Text");
			this.m_WizardPageWelcom.Visible = ((bool)(resources.GetObject("m_WizardPageWelcom.Visible")));
			// 
			// checkBoxShowAttention
			// 
			this.checkBoxShowAttention.AccessibleDescription = resources.GetString("checkBoxShowAttention.AccessibleDescription");
			this.checkBoxShowAttention.AccessibleName = resources.GetString("checkBoxShowAttention.AccessibleName");
			this.checkBoxShowAttention.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("checkBoxShowAttention.Anchor")));
			this.checkBoxShowAttention.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("checkBoxShowAttention.Appearance")));
			this.checkBoxShowAttention.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("checkBoxShowAttention.BackgroundImage")));
			this.checkBoxShowAttention.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("checkBoxShowAttention.CheckAlign")));
			this.checkBoxShowAttention.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("checkBoxShowAttention.Dock")));
			this.checkBoxShowAttention.Enabled = ((bool)(resources.GetObject("checkBoxShowAttention.Enabled")));
			this.checkBoxShowAttention.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("checkBoxShowAttention.FlatStyle")));
			this.checkBoxShowAttention.Font = ((System.Drawing.Font)(resources.GetObject("checkBoxShowAttention.Font")));
			this.checkBoxShowAttention.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxShowAttention.Image")));
			this.checkBoxShowAttention.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("checkBoxShowAttention.ImageAlign")));
			this.checkBoxShowAttention.ImageIndex = ((int)(resources.GetObject("checkBoxShowAttention.ImageIndex")));
			this.checkBoxShowAttention.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("checkBoxShowAttention.ImeMode")));
			this.checkBoxShowAttention.Location = ((System.Drawing.Point)(resources.GetObject("checkBoxShowAttention.Location")));
			this.checkBoxShowAttention.Name = "checkBoxShowAttention";
			this.checkBoxShowAttention.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("checkBoxShowAttention.RightToLeft")));
			this.checkBoxShowAttention.Size = ((System.Drawing.Size)(resources.GetObject("checkBoxShowAttention.Size")));
			this.checkBoxShowAttention.TabIndex = ((int)(resources.GetObject("checkBoxShowAttention.TabIndex")));
			this.checkBoxShowAttention.Text = resources.GetString("checkBoxShowAttention.Text");
			this.checkBoxShowAttention.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("checkBoxShowAttention.TextAlign")));
			this.checkBoxShowAttention.Visible = ((bool)(resources.GetObject("checkBoxShowAttention.Visible")));
			// 
			// m_WizardPageFinish
			// 
			this.m_WizardPageFinish.AccessibleDescription = resources.GetString("m_WizardPageFinish.AccessibleDescription");
			this.m_WizardPageFinish.AccessibleName = resources.GetString("m_WizardPageFinish.AccessibleName");
			this.m_WizardPageFinish.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPageFinish.Anchor")));
			this.m_WizardPageFinish.AutoScroll = ((bool)(resources.GetObject("m_WizardPageFinish.AutoScroll")));
			this.m_WizardPageFinish.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPageFinish.AutoScrollMargin")));
			this.m_WizardPageFinish.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPageFinish.AutoScrollMinSize")));
			this.m_WizardPageFinish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPageFinish.BackgroundImage")));
			this.m_WizardPageFinish.Description = "Operation Wizard has been finished!";
			this.m_WizardPageFinish.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPageFinish.Dock")));
			this.m_WizardPageFinish.Enabled = ((bool)(resources.GetObject("m_WizardPageFinish.Enabled")));
			this.m_WizardPageFinish.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPageFinish.Font")));
			this.m_WizardPageFinish.ForeColor = System.Drawing.Color.Black;
			this.m_WizardPageFinish.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPageFinish.ImeMode")));
			this.m_WizardPageFinish.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPageFinish.Location")));
			this.m_WizardPageFinish.Name = "m_WizardPageFinish";
			this.m_WizardPageFinish.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPageFinish.RightToLeft")));
			this.m_WizardPageFinish.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPageFinish.Size")));
			this.m_WizardPageFinish.Style = BYHXControls.WizardPageStyle.Finish;
			this.m_WizardPageFinish.TabIndex = ((int)(resources.GetObject("m_WizardPageFinish.TabIndex")));
			this.m_WizardPageFinish.Text = resources.GetString("m_WizardPageFinish.Text");
			this.m_WizardPageFinish.Title = "Operation Wizard";
			this.m_WizardPageFinish.Visible = ((bool)(resources.GetObject("m_WizardPageFinish.Visible")));
			// 
			// m_WizardPage3
			// 
			this.m_WizardPage3.AccessibleDescription = resources.GetString("m_WizardPage3.AccessibleDescription");
			this.m_WizardPage3.AccessibleName = resources.GetString("m_WizardPage3.AccessibleName");
			this.m_WizardPage3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPage3.Anchor")));
			this.m_WizardPage3.AutoScroll = ((bool)(resources.GetObject("m_WizardPage3.AutoScroll")));
			this.m_WizardPage3.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPage3.AutoScrollMargin")));
			this.m_WizardPage3.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPage3.AutoScrollMinSize")));
			this.m_WizardPage3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPage3.BackgroundImage")));
			this.m_WizardPage3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPage3.Dock")));
			this.m_WizardPage3.Enabled = ((bool)(resources.GetObject("m_WizardPage3.Enabled")));
			this.m_WizardPage3.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPage3.Font")));
			this.m_WizardPage3.ForeColor = System.Drawing.Color.Black;
			this.m_WizardPage3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPage3.ImeMode")));
			this.m_WizardPage3.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPage3.Location")));
			this.m_WizardPage3.Name = "m_WizardPage3";
			this.m_WizardPage3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPage3.RightToLeft")));
			this.m_WizardPage3.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPage3.Size")));
			this.m_WizardPage3.TabIndex = ((int)(resources.GetObject("m_WizardPage3.TabIndex")));
			this.m_WizardPage3.Text = resources.GetString("m_WizardPage3.Text");
			this.m_WizardPage3.Visible = ((bool)(resources.GetObject("m_WizardPage3.Visible")));
			// 
			// m_WizardPage2
			// 
			this.m_WizardPage2.AccessibleDescription = resources.GetString("m_WizardPage2.AccessibleDescription");
			this.m_WizardPage2.AccessibleName = resources.GetString("m_WizardPage2.AccessibleName");
			this.m_WizardPage2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPage2.Anchor")));
			this.m_WizardPage2.AutoScroll = ((bool)(resources.GetObject("m_WizardPage2.AutoScroll")));
			this.m_WizardPage2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPage2.AutoScrollMargin")));
			this.m_WizardPage2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPage2.AutoScrollMinSize")));
			this.m_WizardPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPage2.BackgroundImage")));
			this.m_WizardPage2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPage2.Dock")));
			this.m_WizardPage2.Enabled = ((bool)(resources.GetObject("m_WizardPage2.Enabled")));
			this.m_WizardPage2.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPage2.Font")));
			this.m_WizardPage2.ForeColor = System.Drawing.Color.Black;
			this.m_WizardPage2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPage2.ImeMode")));
			this.m_WizardPage2.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPage2.Location")));
			this.m_WizardPage2.Name = "m_WizardPage2";
			this.m_WizardPage2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPage2.RightToLeft")));
			this.m_WizardPage2.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPage2.Size")));
			this.m_WizardPage2.TabIndex = ((int)(resources.GetObject("m_WizardPage2.TabIndex")));
			this.m_WizardPage2.Text = resources.GetString("m_WizardPage2.Text");
			this.m_WizardPage2.Visible = ((bool)(resources.GetObject("m_WizardPage2.Visible")));
			// 
			// m_WizardPage1
			// 
			this.m_WizardPage1.AccessibleDescription = resources.GetString("m_WizardPage1.AccessibleDescription");
			this.m_WizardPage1.AccessibleName = resources.GetString("m_WizardPage1.AccessibleName");
			this.m_WizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPage1.Anchor")));
			this.m_WizardPage1.AutoScroll = ((bool)(resources.GetObject("m_WizardPage1.AutoScroll")));
			this.m_WizardPage1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPage1.AutoScrollMargin")));
			this.m_WizardPage1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPage1.AutoScrollMinSize")));
			this.m_WizardPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPage1.BackgroundImage")));
			this.m_WizardPage1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPage1.Dock")));
			this.m_WizardPage1.Enabled = ((bool)(resources.GetObject("m_WizardPage1.Enabled")));
			this.m_WizardPage1.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPage1.Font")));
			this.m_WizardPage1.ForeColor = System.Drawing.Color.Black;
			this.m_WizardPage1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPage1.ImeMode")));
			this.m_WizardPage1.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPage1.Location")));
			this.m_WizardPage1.Name = "m_WizardPage1";
			this.m_WizardPage1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPage1.RightToLeft")));
			this.m_WizardPage1.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPage1.Size")));
			this.m_WizardPage1.TabIndex = ((int)(resources.GetObject("m_WizardPage1.TabIndex")));
			this.m_WizardPage1.Text = resources.GetString("m_WizardPage1.Text");
			this.m_WizardPage1.Visible = ((bool)(resources.GetObject("m_WizardPage1.Visible")));
			// 
			// m_WizardPageCali
			// 
			this.m_WizardPageCali.AccessibleDescription = resources.GetString("m_WizardPageCali.AccessibleDescription");
			this.m_WizardPageCali.AccessibleName = resources.GetString("m_WizardPageCali.AccessibleName");
			this.m_WizardPageCali.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPageCali.Anchor")));
			this.m_WizardPageCali.AutoScroll = ((bool)(resources.GetObject("m_WizardPageCali.AutoScroll")));
			this.m_WizardPageCali.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPageCali.AutoScrollMargin")));
			this.m_WizardPageCali.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPageCali.AutoScrollMinSize")));
			this.m_WizardPageCali.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPageCali.BackgroundImage")));
			this.m_WizardPageCali.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPageCali.Dock")));
			this.m_WizardPageCali.Enabled = ((bool)(resources.GetObject("m_WizardPageCali.Enabled")));
			this.m_WizardPageCali.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPageCali.Font")));
			this.m_WizardPageCali.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPageCali.ImeMode")));
			this.m_WizardPageCali.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPageCali.Location")));
			this.m_WizardPageCali.Name = "m_WizardPageCali";
			this.m_WizardPageCali.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPageCali.RightToLeft")));
			this.m_WizardPageCali.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPageCali.Size")));
			this.m_WizardPageCali.TabIndex = ((int)(resources.GetObject("m_WizardPageCali.TabIndex")));
			this.m_WizardPageCali.Text = resources.GetString("m_WizardPageCali.Text");
			this.m_WizardPageCali.Visible = ((bool)(resources.GetObject("m_WizardPageCali.Visible")));
			// 
			// m_WizardPageMechanic
			// 
			this.m_WizardPageMechanic.AccessibleDescription = resources.GetString("m_WizardPageMechanic.AccessibleDescription");
			this.m_WizardPageMechanic.AccessibleName = resources.GetString("m_WizardPageMechanic.AccessibleName");
			this.m_WizardPageMechanic.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WizardPageMechanic.Anchor")));
			this.m_WizardPageMechanic.AutoScroll = ((bool)(resources.GetObject("m_WizardPageMechanic.AutoScroll")));
			this.m_WizardPageMechanic.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WizardPageMechanic.AutoScrollMargin")));
			this.m_WizardPageMechanic.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WizardPageMechanic.AutoScrollMinSize")));
			this.m_WizardPageMechanic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WizardPageMechanic.BackgroundImage")));
			this.m_WizardPageMechanic.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WizardPageMechanic.Dock")));
			this.m_WizardPageMechanic.Enabled = ((bool)(resources.GetObject("m_WizardPageMechanic.Enabled")));
			this.m_WizardPageMechanic.Font = ((System.Drawing.Font)(resources.GetObject("m_WizardPageMechanic.Font")));
			this.m_WizardPageMechanic.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WizardPageMechanic.ImeMode")));
			this.m_WizardPageMechanic.Location = ((System.Drawing.Point)(resources.GetObject("m_WizardPageMechanic.Location")));
			this.m_WizardPageMechanic.Name = "m_WizardPageMechanic";
			this.m_WizardPageMechanic.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WizardPageMechanic.RightToLeft")));
			this.m_WizardPageMechanic.Size = ((System.Drawing.Size)(resources.GetObject("m_WizardPageMechanic.Size")));
			this.m_WizardPageMechanic.TabIndex = ((int)(resources.GetObject("m_WizardPageMechanic.TabIndex")));
			this.m_WizardPageMechanic.Text = resources.GetString("m_WizardPageMechanic.Text");
			this.m_WizardPageMechanic.Visible = ((bool)(resources.GetObject("m_WizardPageMechanic.Visible")));
			// 
			// m_labTitle
			// 
			this.m_labTitle.AccessibleDescription = resources.GetString("m_labTitle.AccessibleDescription");
			this.m_labTitle.AccessibleName = resources.GetString("m_labTitle.AccessibleName");
			this.m_labTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_labTitle.Anchor")));
			this.m_labTitle.AutoSize = ((bool)(resources.GetObject("m_labTitle.AutoSize")));
			this.m_labTitle.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_labTitle.Dock")));
			this.m_labTitle.Enabled = ((bool)(resources.GetObject("m_labTitle.Enabled")));
			this.m_labTitle.Font = ((System.Drawing.Font)(resources.GetObject("m_labTitle.Font")));
			this.m_labTitle.Image = ((System.Drawing.Image)(resources.GetObject("m_labTitle.Image")));
			this.m_labTitle.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labTitle.ImageAlign")));
			this.m_labTitle.ImageIndex = ((int)(resources.GetObject("m_labTitle.ImageIndex")));
			this.m_labTitle.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_labTitle.ImeMode")));
			this.m_labTitle.Location = ((System.Drawing.Point)(resources.GetObject("m_labTitle.Location")));
			this.m_labTitle.Name = "m_labTitle";
			this.m_labTitle.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_labTitle.RightToLeft")));
			this.m_labTitle.Size = ((System.Drawing.Size)(resources.GetObject("m_labTitle.Size")));
			this.m_labTitle.TabIndex = ((int)(resources.GetObject("m_labTitle.TabIndex")));
			this.m_labTitle.Text = resources.GetString("m_labTitle.Text");
			this.m_labTitle.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labTitle.TextAlign")));
			this.m_labTitle.Visible = ((bool)(resources.GetObject("m_labTitle.Visible")));
			// 
			// m_labConten
			// 
			this.m_labConten.AccessibleDescription = resources.GetString("m_labConten.AccessibleDescription");
			this.m_labConten.AccessibleName = resources.GetString("m_labConten.AccessibleName");
			this.m_labConten.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_labConten.Anchor")));
			this.m_labConten.AutoSize = ((bool)(resources.GetObject("m_labConten.AutoSize")));
			this.m_labConten.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_labConten.Dock")));
			this.m_labConten.Enabled = ((bool)(resources.GetObject("m_labConten.Enabled")));
			this.m_labConten.Font = ((System.Drawing.Font)(resources.GetObject("m_labConten.Font")));
			this.m_labConten.Image = ((System.Drawing.Image)(resources.GetObject("m_labConten.Image")));
			this.m_labConten.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labConten.ImageAlign")));
			this.m_labConten.ImageIndex = ((int)(resources.GetObject("m_labConten.ImageIndex")));
			this.m_labConten.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_labConten.ImeMode")));
			this.m_labConten.Location = ((System.Drawing.Point)(resources.GetObject("m_labConten.Location")));
			this.m_labConten.Name = "m_labConten";
			this.m_labConten.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_labConten.RightToLeft")));
			this.m_labConten.Size = ((System.Drawing.Size)(resources.GetObject("m_labConten.Size")));
			this.m_labConten.TabIndex = ((int)(resources.GetObject("m_labConten.TabIndex")));
			this.m_labConten.Text = resources.GetString("m_labConten.Text");
			this.m_labConten.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_labConten.TextAlign")));
			this.m_labConten.Visible = ((bool)(resources.GetObject("m_labConten.Visible")));
			// 
			// TipsWizard
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.m_WizardCalibration);
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
			this.Name = "TipsWizard";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.CaliWizard_Closing);
			this.Load += new System.EventHandler(this.TipsWizard_Load);
			this.m_WizardCalibration.ResumeLayout(false);
			this.m_WizardPageWelcom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void SetScale()
		{	
			SizeF sizef = new SizeF();
			sizef = Form.GetAutoScaleSize(this.Font);

			Size size = sizef.ToSize();
			this.AutoScaleBaseSize = size;

		}	
		private void m_WizardCalibration_AfterSwitchPages(object sender, BYHXControls.Wizard.AfterSwitchPagesEventArgs e)
		{

			WizardPage newPage = this.m_WizardCalibration.Pages[e.NewIndex];


			if (newPage == this.m_WizardPageWelcom)
			{
				//SetContenLableValue("Conten_wellcomeWord");
				this.m_WizardPageWelcom.Title = ResString.GetResString("Title_wellcomeWord");
				newPage.Enabled = true;
			}
			if (m_WizardPage1 == newPage)
			{
				SetContenLableValue("Conten_firstPage");
				this.m_WizardPage1.Title = ResString.GetResString("Title_firstPage");
				newPage.Enabled = true;
				
			}
			if (m_WizardPage2 == newPage)
			{
				SetContenLableValue("Conten_secondPage");
				this.m_WizardPage2.Title = ResString.GetResString("Title_secondPage");
				newPage.Enabled = true;
			}
			if (m_WizardPage3 == newPage)
			{
				SetContenLableValue("Conten_thirdPage");
				this.m_WizardPage3.Title = ResString.GetResString("Title_thirdPage");
				newPage.Enabled = true;
			}
			if (m_WizardPageFinish == newPage)
			{
				this.m_labConten.Visible =false;
				newPage.Enabled = true;
				newPage.Title = ResString.GetResString("Title_finishPage");
				newPage.Description =ResString.GetResString("Description_finishPage");
				if(m_WizardPageFinish.Controls.Contains(m_labConten))
				{
					m_WizardPageFinish.Controls.Remove(m_labConten);
				}
			}
			WizardPage oldPage = this.m_WizardCalibration.Pages[e.OldIndex];
			if(oldPage.Controls.Contains(checkBoxShowAttention))
				oldPage.Controls.Remove(checkBoxShowAttention);
			if(!newPage.Controls.Contains(checkBoxShowAttention))
				newPage.Controls.Add(checkBoxShowAttention);
			//添加显示内容
			if(oldPage.Controls.Contains(m_labConten))
				oldPage.Controls.Remove(m_labConten);
			if(!newPage.Controls.Contains(m_labConten))
				newPage.Controls.Add(m_labConten);
			
		}

		private void m_WizardCalibration_BeforeSwitchPages(object sender, BYHXControls.Wizard.BeforeSwitchPagesEventArgs e)
		{
			// get wizard page already displayed
			WizardPage oldPage = this.m_WizardCalibration.Pages[e.OldIndex];
			// check if we're going forward from options page
			if (oldPage == this.m_WizardPageGuide && e.NewIndex > e.OldIndex)
			{
				// check if user selected one option
				if (m_subStep< (int)CalibrationSubStep.All-1)
				{
					m_subStep++;
					e.NewIndex--;
				}
			}
			else if(oldPage == this.m_WizardPageGuide && e.NewIndex < e.OldIndex)
			{
				if (m_subStep>(int)(((CalibrationSubStep[])Enum.GetValues(typeof(CalibrationSubStep)))[0]))
				{
					m_subStep--;
					e.NewIndex++;
				}
			}
		}
		
		private void m_WizardCalibration_Cancel(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}
		//导航设置页完成
		private void m_WizardCalibration_Finish(object sender, System.EventArgs e)
		{
			//保存设置
			OnGetPreference(ref m_param.Preference);
			
		}

		private void m_WizardCalibration_Help(object sender, System.EventArgs e)
		{
		}


		public void OnGetPreference(ref UIPreference up)
		{
			up.bShowAttentionOnLoad = !this.checkBoxShowAttention.Checked;
		}

		private void CaliWizard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{


		}

		private void m_WizardCalibration_Save(object sender, System.EventArgs e)
		{
		}

		private void InitializeLocalize()
		{
			this.Text = "matters needing attention";
//			m_WizardPageWelcom.Description = resources.GetString("Title_wellcomeWord");
//			m_WizardPage1.Description = resources.GetString("Title_firstPage");
//			m_WizardPage2.Description = resources.GetString("Title_secondPage");
//			m_WizardPage3.Description =  resources.GetString("Title_thirdPage");
			//m_WizardPage1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

			//初始化第一页
			SetContenLableValue("Conten_wellcomeWord");
			m_WizardPageWelcom.Enabled = true;
			//this.m_labConten.ForeColor = System.Drawing.Color.FromArgb(255,0,0);
			this.m_WizardPageWelcom.Title = ResString.GetResString("Title_wellcomeWord");
		
			if (!m_WizardPageWelcom.Controls.Contains(m_labConten))
			{
				m_WizardPageWelcom.Controls.Add(m_labConten);
			}
			
			Assembly myAssembly = Assembly.GetExecutingAssembly();
			string[] names = myAssembly.GetManifestResourceNames();

            Stream myStream1 = myAssembly.GetManifestResourceStream("BYHXPrinterManager.Calibration.app.png");
			this.m_WizardCalibration.HeaderImage = new Bitmap(myStream1);
			Stream myStream2 = myAssembly.GetManifestResourceStream("BYHXPrinterManager.Calibration.welcomImage.bmp");
			this.m_WizardCalibration.WelcomeImage = new Bitmap(myStream2);
		}


		public void SetGroupBoxStyle(Grouper ts)
		{

		}

		private void TipsWizard_Load(object sender, System.EventArgs e)
		{
			
		}
		private void SetLabTextView(string strName,bool bTitle)
		{
			
		}
		private void SetContenLableValue(string strValueName)
		{
			this.m_labConten.Text = ResString.GetResString(strValueName);
			this.m_labConten.Height = this.Height*2/3;
			this.m_labConten.Width  = this.Width*2/3;
			this.m_labConten.Location = new Point(this.Width/5,this.Height/5);
			//this.BackColor = System.Drawing.Color.White;
			this.m_labConten.BackColor = this.BackColor;
			this.m_labConten.Font = new System.Drawing.Font("SimSun",14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			//this.m_labConten.AutoSize = true;
			this.m_labConten.Visible = true;
			//this.m_labConten.ForeColor=Color.FromName("#99FF00");
			//this.m_labConten.ForeColor = System.Drawing.Color.FromArgb(255,255,255);
		}
	}  
}
