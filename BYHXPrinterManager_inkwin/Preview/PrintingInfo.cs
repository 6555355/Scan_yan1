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
using System.Globalization;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics ;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for PrintingInfo.
	/// </summary>
	public class PrintingInfo : System.Windows.Forms.UserControl
	{
		DateTime m_StartTime = DateTime.Now;
		private string m_sJobInfo = "";
		private float  m_fArea = 0;

		private IPrinterChange m_iPrinterChange =null;
		private bool m_bCreateImageWithPercent = false;
		private bool m_bPrintingPreview = true;
		private UILengthUnit m_curUnit;
		private UIJob		m_curJob;

		private BYHXPrinterManager.Preview.PrintingPreview m_PrintPreview;
		private BYHXPrinterManager.GradientControls.CrystalLabel m_LabelPrintingJobInfo;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private BYHXPrinterManager.GradientControls.CrystalLabel crystalLabel_Status;
		private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.Panel panelRight;
		private BYHXPrinterManager.Setting.ToolbarSetting toolbarSetting1;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		public PrintingInfo()
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintingInfo));
			this.m_PrintPreview = new BYHXPrinterManager.Preview.PrintingPreview();
			this.m_LabelPrintingJobInfo = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.toolbarSetting1 = new BYHXPrinterManager.Setting.ToolbarSetting();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.crystalLabel_Status = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.panelRight = new System.Windows.Forms.Panel();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panelLeft.SuspendLayout();
			this.panelRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_PrintPreview
			// 
			this.m_PrintPreview.AccessibleDescription = resources.GetString("m_PrintPreview.AccessibleDescription");
			this.m_PrintPreview.AccessibleName = resources.GetString("m_PrintPreview.AccessibleName");
			this.m_PrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PrintPreview.Anchor")));
			this.m_PrintPreview.AutoScroll = ((bool)(resources.GetObject("m_PrintPreview.AutoScroll")));
			this.m_PrintPreview.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PrintPreview.AutoScrollMargin")));
			this.m_PrintPreview.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PrintPreview.AutoScrollMinSize")));
			this.m_PrintPreview.BackColor = System.Drawing.Color.Transparent;
			this.m_PrintPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PrintPreview.BackgroundImage")));
			this.m_PrintPreview.Divider = false;
			this.m_PrintPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PrintPreview.Dock")));
			this.m_PrintPreview.Enabled = ((bool)(resources.GetObject("m_PrintPreview.Enabled")));
			this.m_PrintPreview.Font = ((System.Drawing.Font)(resources.GetObject("m_PrintPreview.Font")));
			this.m_PrintPreview.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.m_PrintPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PrintPreview.ImeMode")));
			this.m_PrintPreview.Location = ((System.Drawing.Point)(resources.GetObject("m_PrintPreview.Location")));
			this.m_PrintPreview.Name = "m_PrintPreview";
			this.m_PrintPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PrintPreview.RightToLeft")));
			this.m_PrintPreview.Size = ((System.Drawing.Size)(resources.GetObject("m_PrintPreview.Size")));
			this.m_PrintPreview.TabIndex = ((int)(resources.GetObject("m_PrintPreview.TabIndex")));
			this.m_PrintPreview.TransparentMode = true;
			this.m_PrintPreview.Visible = ((bool)(resources.GetObject("m_PrintPreview.Visible")));
			this.m_PrintPreview.DoubleClick += new System.EventHandler(this.m_PrintPreview_DoubleClick);
			// 
			// m_LabelPrintingJobInfo
			// 
			this.m_LabelPrintingJobInfo.AccessibleDescription = resources.GetString("m_LabelPrintingJobInfo.AccessibleDescription");
			this.m_LabelPrintingJobInfo.AccessibleName = resources.GetString("m_LabelPrintingJobInfo.AccessibleName");
			this.m_LabelPrintingJobInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelPrintingJobInfo.Anchor")));
			this.m_LabelPrintingJobInfo.AutoScroll = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.AutoScroll")));
			this.m_LabelPrintingJobInfo.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_LabelPrintingJobInfo.AutoScrollMargin")));
			this.m_LabelPrintingJobInfo.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_LabelPrintingJobInfo.AutoScrollMinSize")));
			this.m_LabelPrintingJobInfo.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelPrintingJobInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_LabelPrintingJobInfo.BackgroundImage")));
			this.m_LabelPrintingJobInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelPrintingJobInfo.Dock")));
			this.m_LabelPrintingJobInfo.Enabled = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.Enabled")));
			this.m_LabelPrintingJobInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelPrintingJobInfo.Font")));
			this.m_LabelPrintingJobInfo.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.m_LabelPrintingJobInfo.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.m_LabelPrintingJobInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelPrintingJobInfo.ImeMode")));
			this.m_LabelPrintingJobInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelPrintingJobInfo.Location")));
			this.m_LabelPrintingJobInfo.Name = "m_LabelPrintingJobInfo";
			this.m_LabelPrintingJobInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelPrintingJobInfo.RightToLeft")));
			this.m_LabelPrintingJobInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelPrintingJobInfo.Size")));
			this.m_LabelPrintingJobInfo.TabIndex = ((int)(resources.GetObject("m_LabelPrintingJobInfo.TabIndex")));
			this.m_LabelPrintingJobInfo.TabStop = false;
			this.m_LabelPrintingJobInfo.TextAlignment = System.Drawing.StringAlignment.Center;
			this.m_LabelPrintingJobInfo.TransparentMode = true;
			this.m_LabelPrintingJobInfo.Visible = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.Visible")));
			this.m_LabelPrintingJobInfo.Click += new System.EventHandler(this.m_LabelPrintingJobInfo_Click);
			this.m_LabelPrintingJobInfo.DoubleClick += new System.EventHandler(this.m_LabelPrintingJobInfo_DoubleClick);
			// 
			// splitter1
			// 
			this.splitter1.AccessibleDescription = resources.GetString("splitter1.AccessibleDescription");
			this.splitter1.AccessibleName = resources.GetString("splitter1.AccessibleName");
			this.splitter1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("splitter1.Anchor")));
			this.splitter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitter1.BackgroundImage")));
			this.splitter1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("splitter1.Dock")));
			this.splitter1.Enabled = ((bool)(resources.GetObject("splitter1.Enabled")));
			this.splitter1.Font = ((System.Drawing.Font)(resources.GetObject("splitter1.Font")));
			this.splitter1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("splitter1.ImeMode")));
			this.splitter1.Location = ((System.Drawing.Point)(resources.GetObject("splitter1.Location")));
			this.splitter1.MinExtra = ((int)(resources.GetObject("splitter1.MinExtra")));
			this.splitter1.MinSize = ((int)(resources.GetObject("splitter1.MinSize")));
			this.splitter1.Name = "splitter1";
			this.splitter1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("splitter1.RightToLeft")));
			this.splitter1.Size = ((System.Drawing.Size)(resources.GetObject("splitter1.Size")));
			this.splitter1.TabIndex = ((int)(resources.GetObject("splitter1.TabIndex")));
			this.splitter1.TabStop = false;
			this.splitter1.Visible = ((bool)(resources.GetObject("splitter1.Visible")));
			// 
			// panelLeft
			// 
			this.panelLeft.AccessibleDescription = resources.GetString("panelLeft.AccessibleDescription");
			this.panelLeft.AccessibleName = resources.GetString("panelLeft.AccessibleName");
			this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panelLeft.Anchor")));
			this.panelLeft.AutoScroll = ((bool)(resources.GetObject("panelLeft.AutoScroll")));
			this.panelLeft.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panelLeft.AutoScrollMargin")));
			this.panelLeft.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panelLeft.AutoScrollMinSize")));
			this.panelLeft.BackColor = System.Drawing.Color.Transparent;
			this.panelLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLeft.BackgroundImage")));
			this.panelLeft.Controls.Add(this.toolbarSetting1);
			this.panelLeft.Controls.Add(this.splitter2);
			this.panelLeft.Controls.Add(this.crystalLabel_Status);
			this.panelLeft.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panelLeft.Dock")));
			this.panelLeft.Enabled = ((bool)(resources.GetObject("panelLeft.Enabled")));
			this.panelLeft.Font = ((System.Drawing.Font)(resources.GetObject("panelLeft.Font")));
			this.panelLeft.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panelLeft.ImeMode")));
			this.panelLeft.Location = ((System.Drawing.Point)(resources.GetObject("panelLeft.Location")));
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panelLeft.RightToLeft")));
			this.panelLeft.Size = ((System.Drawing.Size)(resources.GetObject("panelLeft.Size")));
			this.panelLeft.TabIndex = ((int)(resources.GetObject("panelLeft.TabIndex")));
			this.panelLeft.Text = resources.GetString("panelLeft.Text");
			this.panelLeft.Visible = ((bool)(resources.GetObject("panelLeft.Visible")));
			// 
			// toolbarSetting1
			// 
			this.toolbarSetting1.AccessibleDescription = resources.GetString("toolbarSetting1.AccessibleDescription");
			this.toolbarSetting1.AccessibleName = resources.GetString("toolbarSetting1.AccessibleName");
			this.toolbarSetting1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("toolbarSetting1.Anchor")));
			this.toolbarSetting1.AutoScroll = ((bool)(resources.GetObject("toolbarSetting1.AutoScroll")));
			this.toolbarSetting1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("toolbarSetting1.AutoScrollMargin")));
			this.toolbarSetting1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("toolbarSetting1.AutoScrollMinSize")));
			this.toolbarSetting1.BackColor = System.Drawing.Color.Transparent;
			this.toolbarSetting1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolbarSetting1.BackgroundImage")));
			this.toolbarSetting1.Divider = false;
			this.toolbarSetting1.DividerSide = System.Windows.Forms.Border3DSide.Top;
			this.toolbarSetting1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("toolbarSetting1.Dock")));
			this.toolbarSetting1.Enabled = ((bool)(resources.GetObject("toolbarSetting1.Enabled")));
			this.toolbarSetting1.Font = ((System.Drawing.Font)(resources.GetObject("toolbarSetting1.Font")));
			this.toolbarSetting1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.toolbarSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.toolbarSetting1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("toolbarSetting1.ImeMode")));
			this.toolbarSetting1.Location = ((System.Drawing.Point)(resources.GetObject("toolbarSetting1.Location")));
			this.toolbarSetting1.Name = "toolbarSetting1";
			this.toolbarSetting1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("toolbarSetting1.RightToLeft")));
			this.toolbarSetting1.Size = ((System.Drawing.Size)(resources.GetObject("toolbarSetting1.Size")));
			this.toolbarSetting1.TabIndex = ((int)(resources.GetObject("toolbarSetting1.TabIndex")));
			this.toolbarSetting1.TransparentMode = true;
			this.toolbarSetting1.VerticalDirection = true;
			this.toolbarSetting1.Visible = ((bool)(resources.GetObject("toolbarSetting1.Visible")));
			// 
			// splitter2
			// 
			this.splitter2.AccessibleDescription = resources.GetString("splitter2.AccessibleDescription");
			this.splitter2.AccessibleName = resources.GetString("splitter2.AccessibleName");
			this.splitter2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("splitter2.Anchor")));
			this.splitter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitter2.BackgroundImage")));
			this.splitter2.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("splitter2.Dock")));
			this.splitter2.Enabled = ((bool)(resources.GetObject("splitter2.Enabled")));
			this.splitter2.Font = ((System.Drawing.Font)(resources.GetObject("splitter2.Font")));
			this.splitter2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("splitter2.ImeMode")));
			this.splitter2.Location = ((System.Drawing.Point)(resources.GetObject("splitter2.Location")));
			this.splitter2.MinExtra = ((int)(resources.GetObject("splitter2.MinExtra")));
			this.splitter2.MinSize = ((int)(resources.GetObject("splitter2.MinSize")));
			this.splitter2.Name = "splitter2";
			this.splitter2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("splitter2.RightToLeft")));
			this.splitter2.Size = ((System.Drawing.Size)(resources.GetObject("splitter2.Size")));
			this.splitter2.TabIndex = ((int)(resources.GetObject("splitter2.TabIndex")));
			this.splitter2.TabStop = false;
			this.splitter2.Visible = ((bool)(resources.GetObject("splitter2.Visible")));
			// 
			// crystalLabel_Status
			// 
			this.crystalLabel_Status.AccessibleDescription = resources.GetString("crystalLabel_Status.AccessibleDescription");
			this.crystalLabel_Status.AccessibleName = resources.GetString("crystalLabel_Status.AccessibleName");
			this.crystalLabel_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("crystalLabel_Status.Anchor")));
			this.crystalLabel_Status.AutoScroll = ((bool)(resources.GetObject("crystalLabel_Status.AutoScroll")));
			this.crystalLabel_Status.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("crystalLabel_Status.AutoScrollMargin")));
			this.crystalLabel_Status.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("crystalLabel_Status.AutoScrollMinSize")));
			this.crystalLabel_Status.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("crystalLabel_Status.BackgroundImage")));
			this.crystalLabel_Status.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("crystalLabel_Status.Dock")));
			this.crystalLabel_Status.Enabled = ((bool)(resources.GetObject("crystalLabel_Status.Enabled")));
			this.crystalLabel_Status.Font = ((System.Drawing.Font)(resources.GetObject("crystalLabel_Status.Font")));
			this.crystalLabel_Status.ForeColor = System.Drawing.Color.Lime;
			this.crystalLabel_Status.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.Black, System.Drawing.Color.Black);
			this.crystalLabel_Status.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.crystalLabel_Status.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("crystalLabel_Status.ImeMode")));
			this.crystalLabel_Status.Location = ((System.Drawing.Point)(resources.GetObject("crystalLabel_Status.Location")));
			this.crystalLabel_Status.Name = "crystalLabel_Status";
			this.crystalLabel_Status.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("crystalLabel_Status.RightToLeft")));
			this.crystalLabel_Status.Size = ((System.Drawing.Size)(resources.GetObject("crystalLabel_Status.Size")));
			this.crystalLabel_Status.TabIndex = ((int)(resources.GetObject("crystalLabel_Status.TabIndex")));
			this.crystalLabel_Status.TabStop = false;
			this.crystalLabel_Status.TextAlignment = System.Drawing.StringAlignment.Center;
			this.crystalLabel_Status.TreeColorGradient = true;
			this.crystalLabel_Status.Visible = ((bool)(resources.GetObject("crystalLabel_Status.Visible")));
			this.crystalLabel_Status.Click += new System.EventHandler(this.m_LabelPrintingJobInfo_Click);
			// 
			// panelRight
			// 
			this.panelRight.AccessibleDescription = resources.GetString("panelRight.AccessibleDescription");
			this.panelRight.AccessibleName = resources.GetString("panelRight.AccessibleName");
			this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panelRight.Anchor")));
			this.panelRight.AutoScroll = ((bool)(resources.GetObject("panelRight.AutoScroll")));
			this.panelRight.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panelRight.AutoScrollMargin")));
			this.panelRight.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panelRight.AutoScrollMinSize")));
			this.panelRight.BackColor = System.Drawing.Color.Transparent;
			this.panelRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelRight.BackgroundImage")));
			this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelRight.Controls.Add(this.m_LabelPrintingJobInfo);
			this.panelRight.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panelRight.Dock")));
			this.panelRight.Enabled = ((bool)(resources.GetObject("panelRight.Enabled")));
			this.panelRight.Font = ((System.Drawing.Font)(resources.GetObject("panelRight.Font")));
			this.panelRight.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panelRight.ImeMode")));
			this.panelRight.Location = ((System.Drawing.Point)(resources.GetObject("panelRight.Location")));
			this.panelRight.Name = "panelRight";
			this.panelRight.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panelRight.RightToLeft")));
			this.panelRight.Size = ((System.Drawing.Size)(resources.GetObject("panelRight.Size")));
			this.panelRight.TabIndex = ((int)(resources.GetObject("panelRight.TabIndex")));
			this.panelRight.Text = resources.GetString("panelRight.Text");
			this.panelRight.Visible = ((bool)(resources.GetObject("panelRight.Visible")));
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = ((System.Drawing.Size)(resources.GetObject("imageList1.ImageSize")));
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PrintingInfo
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.m_PrintPreview);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panelLeft);
			this.Controls.Add(this.panelRight);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "PrintingInfo";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.DoubleClick += new System.EventHandler(this.PreviewAndInfo_DoubleClick);
			this.panelLeft.ResumeLayout(false);
			this.panelRight.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		public bool LableVisble
		{
			get{return this.panelLeft.Visible;}
			set
			{
				this.panelLeft.Visible = value;
				this.splitter1.Visible = value;
			}
		}
		public void OnPrintingStart()
		{
			m_StartTime = DateTime.Now;
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			this.toolbarSetting1.OnPrinterPropertyChange(sp);
		}
		public void OnPrinterSettingChange( SPrinterSetting ss,EpsonExAllParam epsonAllparam)
		{
			this.toolbarSetting1.OnPrinterSettingChange(ss,epsonAllparam);
		}
		public void OnPreferenceChange( UIPreference up)
		{
			this.toolbarSetting1.OnPreferenceChange(up);
			UpdateJobInfo(up.Unit,m_curJob);
			if(m_curUnit != up.Unit)
			{
				m_curUnit = up.Unit;
				if(m_curJob != null)
				{
					UpdateJobInfo(m_curUnit,m_curJob);
				}
				else if(m_bPrintingPreview && m_bCreateImageWithPercent)
				{
					OnSwitchToPrintingPreview();
				}
			}
		}
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			string strtext = ResString.GetEnumDisplayName(typeof(JetStatusEnum),status);
			if(status == JetStatusEnum.Error)
			{
				strtext += "\n" + "[" + CoreInterface.GetBoardError().ToString("X8") + "]";
			}
			this.crystalLabel_Status.Text = strtext;
		}
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
			m_PrintPreview.SetPrinterChange(ic);
			this.toolbarSetting1.SetPrinterChange(ic);
		}
		public void SetCreateImageWithPercent( bool bPre)
		{
			m_bCreateImageWithPercent = bPre;
		}

		public void SetPrintingPreview( bool bPre)
		{
			m_bPrintingPreview = bPre;
			m_PrintPreview.SetPrintingPreview(bPre);
		}
		public void OnUpdatePrintingInfo(Image preview)
		{
			UpdatePreviewImage(preview);

			SPrtFileInfo jobInfo = new SPrtFileInfo();
			if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
			{
				UpdatePrintingJobInfo(m_curUnit,jobInfo);
				SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth,jobInfo.sImageInfo.nImageHeight,jobInfo.sFreSetting.nResolutionX,jobInfo.sFreSetting.nResolutionY);
				UpdateJobSizeInfo(previewsize);
			}
			else
			{
				//??????????????????????????????????????????  should do it
				UpdatePrintingJobInfo(m_curUnit,jobInfo);
				UpdateJobSizeInfo(SizeF.Empty);
			}
		}
		public void OnSwitchToPrintingPreview()
		{
			bool bPrinting = m_bPrintingPreview;
			SPrtFileInfo jobInfo = new SPrtFileInfo();
			if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
			{
				UpdatePrintingJobInfo(m_curUnit,jobInfo);
				CreatePrintingPreviewAsPercentage(0);
				SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth,jobInfo.sImageInfo.nImageHeight,jobInfo.sFreSetting.nResolutionX,jobInfo.sFreSetting.nResolutionY);
				UpdateJobSizeInfo(previewsize);
			}
			else
			{
				//??????????????????????????????????????????  should do it
				UpdatePrintingJobInfo(m_curUnit,jobInfo);
				UpdatePreviewImage(null);
				UpdateJobSizeInfo(SizeF.Empty);
			}
		}
		public void UpdatePercentage(int percent)
		{
			m_PrintPreview.UpdatePercentage(percent);
			///////////////////////////////////////////////
			///this should only call by the third RIP software
			if(m_bCreateImageWithPercent && m_bPrintingPreview)
				CreatePrintingPreviewAsPercentage(percent);
			if(percent>0)
			{
				UpdateJobInfoTextWith(percent);
			}
		}
		public void UpdateUIJobPreview(UIJob job)
		{
			if(job != null)
			{
				UpdatePreviewImage(job.Miniature);
				UpdateJobSizeInfo(job.JobSize);
				UpdateJobInfo(m_curUnit,job);
			}
			else
			{
				//No one job in joblist
				UpdatePreviewImage(null);
				UpdateJobSizeInfo(SizeF.Empty);
				UpdateJobInfo(m_curUnit,null);
			}
			m_curJob = job;
		}


		private void UpdatePreviewImage(Image preview)
		{
			m_PrintPreview.UpdatePreviewImage(preview);
		}
		private void UpdateJobSizeInfo(SizeF size)
		{
			m_PrintPreview.UpdateJobSizeInfo(size);
		}
		private void UpdateJobInfo(UILengthUnit unit,UIJob jobInfo)
		{
			if(jobInfo == null )
			{
				InitNull();
			}
			else
			{
				float width = jobInfo.JobSize.Width;//PubFunc.CalcRealJobWidth(jobInfo.JobSize.Width,this.m_iPrinterChange.GetAllParam());
				UpdateJobInfoText( unit,  width, jobInfo.JobSize.Height,  jobInfo.ResolutionX,  jobInfo.ResolutionY,
					jobInfo.ColorDeep,  jobInfo.PassNumber,  jobInfo.PrintingDirection,   jobInfo.FileLocation,jobInfo.LangID);
			}
		}

		private void InitNull()
		{
			this.m_LabelPrintingJobInfo.Text = null;
		}

		private void UpdateJobInfoText(UILengthUnit unit, float width,float height, int resX, int resY,
			int deep, int passnum, int direction, string  filepath,int LangID)
		{

			string unitStr	= ResString.GetUnitSuffixDispName(unit);
			string pass = ResString.GetDisplayPass();

			string strSize = string.Format("{0}x{1} {2}",
				UIPreference.ToDisplayLength(unit,width).ToString("f1"),
				UIPreference.ToDisplayLength(unit,height).ToString("f1"),unitStr);
//			width = this.CalcRealJobWidth(unit,width);
			m_fArea = UIPreference.ToDisplayLength(UILengthUnit.Meter,width) * UIPreference.ToDisplayLength(UILengthUnit.Meter,height);

			string strRes = string.Format("{0}x{1}",
				(resX),
				(resY));

			string strDeep;
			if (1 == deep) 
			{
				strDeep = "Normal";
			} 
			else 
			{
				strDeep = "High Quality";
			}


			string strPass = string.Format("{0} {1}",passnum,pass);
			string strDir = ResString.GetEnumDisplayName(typeof(PrintDirection),(PrintDirection)direction);
			string sLangID = "";
			if(LangID != 0xffff)
			{
				try
				{
					foreach ( CultureInfo cInfo in CultureInfo.GetCultures( CultureTypes.AllCultures ) )  
					{
						if ( cInfo.LCID == LangID )  
						{
							sLangID = cInfo.EnglishName;
							break;
						}
					}
					
				}
				catch(Exception)
				{
					sLangID = "";
				}
			}
			//DisplayName EnglishName

			this.m_LabelPrintingJobInfo.Text= 
				m_sJobInfo= 
				strSize 
				+ "\n" + strRes 
				+ "\n" + strDeep
				+ "\n" + strPass
				+ "\n" + strDir
				+ "\n" + sLangID
				+ "\n" + filepath;
		}
		private void UpdatePrintingJobInfo(UILengthUnit unit, SPrtFileInfo jobInfo)
		{
			float width = 0;
			if(jobInfo.sFreSetting.nResolutionX != 0)
			{
				width = (float)jobInfo.sImageInfo.nImageWidth / (float)(jobInfo.sFreSetting.nResolutionX);
				width = PubFunc.CalcRealJobWidth(width,this.m_iPrinterChange.GetAllParam());
				//width = UIPreference.ToDisplayLength(unit,width);
			}
			float height = 0;
			if(jobInfo.sFreSetting.nResolutionY != 0)
			{
				height = (float)jobInfo.sImageInfo.nImageHeight/(float)(jobInfo.sFreSetting.nResolutionY);
				//height = UIPreference.ToDisplayLength(unit,height);
			}
			UpdateJobInfoText( unit,  width, height, jobInfo.sFreSetting.nResolutionX*jobInfo.sImageInfo.nImageResolutionX,  jobInfo.sFreSetting.nResolutionY * jobInfo.sImageInfo.nImageResolutionY,
				jobInfo.sImageInfo.nImageColorDeep,  jobInfo.sFreSetting.nPass,  jobInfo.sFreSetting.nBidirection,   null,0);
		}
		
		private void UpdateJobInfoTextWith(int percent)
		{
			TimeSpan time = DateTime.Now - m_StartTime;
			string strTime =time.Hours.ToString() +":" +time.Minutes.ToString() +":" + time.Seconds.ToString();
			string strPercentage = percent.ToString() + "%";
			
			string unitStr	= ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
			string strArea = (m_fArea * percent/100.0f).ToString() + " " + unitStr+ "2";
			float  efficient = 0;
			if(time.TotalSeconds != 0)
				efficient =  (float)(m_fArea*percent/100.0f)/(float)time.TotalHours ;
			string strEff =  efficient.ToString()  +  " "+ unitStr + "2/h";
			this.m_LabelPrintingJobInfo.Text = strTime
				+ "\n" + strPercentage 
				+ "\n" + strArea
				+ "\n" + strEff
				+ "\n" + m_sJobInfo;
		}


		private SizeF GetJobSize( int imgWidth ,int imgHeight, int resX,int resY)
		{
			float width = 0;
				
			if(resX != 0)
			{
				width = (float)imgWidth / (float)resX;
			}
			float height = 0;
			if(resY != 0)
			{
				height = (float)imgHeight/(float)resY;
			}
			return new SizeF(width,height);
		}
		unsafe private void CreatePrintingPreviewAsPercentage(int percentage)
		{
			SPrtFileInfo jobInfo = new SPrtFileInfo();
			if(CoreInterface.Printer_GetJobInfo(ref jobInfo) <= 0) return;
			IntPtr handle = (IntPtr)jobInfo.sImageInfo.nImageData;
			if(handle == IntPtr.Zero) return;
			SPrtImagePreview	previewData	= (SPrtImagePreview)Marshal.PtrToStructure(handle,typeof(SPrtImagePreview));
			Bitmap image = SerialFunction.CreateImageWithPreview(previewData);
					
			if(image != null)
			{
				int clipy = (image.Height * percentage / 100);
				int clipheight = image.Height - clipy;
							
				if(clipheight > 0)
				{
					BitmapData data = image.LockBits(new Rectangle(0,clipy,image.Width,clipheight),ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb);

					byte* buf = (byte*) data.Scan0;
					int  size = data.Stride * clipheight; 

					for (int i = 0;i < size; i++)	
					{
						buf[i] = 0xff;
					}

					image.UnlockBits(data);
				}
							
				UpdatePreviewImage(image);
			}
		}

		private void PreviewAndInfo_DoubleClick(object sender, System.EventArgs e)
		{
			m_iPrinterChange.OnSwitchPreview();
		}

		private void m_PrintPreview_DoubleClick(object sender, System.EventArgs e)
		{
			m_iPrinterChange.OnSwitchPreview();
		}

		private void m_LabelPrintingJobInfo_DoubleClick(object sender, System.EventArgs e)
		{
			m_iPrinterChange.OnSwitchPreview();
		}

		private void m_LabelPrintingJobInfo_Click(object sender, System.EventArgs e)
		{
			this.m_PrintPreview.Focus();
		}

		public void SetBackgroundimage(Image imgbgk,Image imgbgkmain)
		{
//			m_LabelPrintingJobInfo.BackgroundImage = imgbgk;
//			m_PrintPreview.BackgroundImage = imgbgkmain;
		}

		public void SetDisStyle(bool bstatus,Color color1,Color color2,bool bInkwin)
		{
			crystalLabel_Status.Visible = bstatus;
//			if(bstatus == false)
//			{
//				m_LabelPrintingJobInfo.GradientColors = new Style(color2,color2);
//				m_PrintPreview.GradientColors = new Style(color1,color1);
//				splitter1.Dock = DockStyle.Right;
//				panelLeft.Dock = DockStyle.Right;
//			}		
//			else
//			{
//				m_LabelPrintingJobInfo.GradientColors = m_PrintPreview.GradientColors = new Style(color1,color2);
//				splitter1.Dock = DockStyle.Left;
//				panelLeft.Dock = DockStyle.Left;
//			}

			if(bInkwin)
			{
				if(this.panelLeft.Controls.Contains(m_LabelPrintingJobInfo))
					this.panelLeft.Controls.Remove(m_LabelPrintingJobInfo);
				if(!this.panelLeft.Controls.Contains(m_LabelPrintingJobInfo))
					this.panelRight.Controls.Add(m_LabelPrintingJobInfo);
				this.toolbarSetting1.Visible = true;
				this.toolbarSetting1.VerticalDirection = true;

				toolbarSetting1.TransparentMode =
				m_LabelPrintingJobInfo.TransparentMode =
				m_PrintPreview.TransparentMode = true;
				m_PrintPreview.BackColor =
				this.toolbarSetting1.BackColor = this.m_LabelPrintingJobInfo.BackColor =
				this.panelLeft.BackColor = this.panelRight.BackColor = Color.Transparent;
				this.BackgroundImage = imageList1.Images[0];
			}
			else
			{
				if(!this.panelLeft.Controls.Contains(m_LabelPrintingJobInfo))
					this.panelLeft.Controls.Add(m_LabelPrintingJobInfo);
				if(this.panelLeft.Controls.Contains(m_LabelPrintingJobInfo))
					this.panelRight.Controls.Remove(m_LabelPrintingJobInfo);
				this.toolbarSetting1.Visible = false;
				this.toolbarSetting1.VerticalDirection = true;
			}
		}

		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
			this.toolbarSetting1.OnPrinterStatusChanged(status);
		}
		
		public void OnGetPreference(ref UIPreference up)
		{
			this.toolbarSetting1.OnGetPreference(ref up);
		}

		public void OnGetPrinterSetting(ref SPrinterSetting ss,ref EpsonExAllParam epsonAllparam)
		{
			this.toolbarSetting1.OnGetPrinterSetting(ref ss,ref epsonAllparam);
		}


	}

}
