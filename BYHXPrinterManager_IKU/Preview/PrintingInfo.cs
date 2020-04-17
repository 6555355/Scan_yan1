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
		public BYHXPrinterManager.GradientControls.CrystalLabel m_LabelPrintingJobInfo;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		public BYHXPrinterManager.GradientControls.CrystalLabel crystalLabel1;
		private System.Windows.Forms.Splitter splitter2;
//		private ErrorViewForm mErrorlistForm;
		public event EventHandler JobInfoChanged;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintingInfo));
			this.m_PrintPreview = new BYHXPrinterManager.Preview.PrintingPreview();
			this.m_LabelPrintingJobInfo = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.crystalLabel1 = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.panel1.SuspendLayout();
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
			this.m_PrintPreview.BackColor = System.Drawing.SystemColors.Control;
			this.m_PrintPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PrintPreview.BackgroundImage")));
			this.m_PrintPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PrintPreview.Dock")));
			this.m_PrintPreview.Enabled = ((bool)(resources.GetObject("m_PrintPreview.Enabled")));
			this.m_PrintPreview.Font = ((System.Drawing.Font)(resources.GetObject("m_PrintPreview.Font")));
			this.m_PrintPreview.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.m_PrintPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PrintPreview.ImeMode")));
			this.m_PrintPreview.Location = ((System.Drawing.Point)(resources.GetObject("m_PrintPreview.Location")));
			this.m_PrintPreview.Name = "m_PrintPreview";
			this.m_PrintPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PrintPreview.RightToLeft")));
			this.m_PrintPreview.Size = ((System.Drawing.Size)(resources.GetObject("m_PrintPreview.Size")));
			this.m_PrintPreview.TabIndex = ((int)(resources.GetObject("m_PrintPreview.TabIndex")));
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
			this.m_LabelPrintingJobInfo.Text = resources.GetString("m_LabelPrintingJobInfo.Text");
			this.m_LabelPrintingJobInfo.TextAlignment = System.Drawing.StringAlignment.Center;
			this.m_LabelPrintingJobInfo.Visible = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.Visible")));
			this.m_LabelPrintingJobInfo.TextChanged += new System.EventHandler(this.m_LabelPrintingJobInfo_TextChanged);
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
			// panel1
			// 
			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.splitter2);
			this.panel1.Controls.Add(this.m_LabelPrintingJobInfo);
			this.panel1.Controls.Add(this.crystalLabel1);
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
			// crystalLabel1
			// 
			this.crystalLabel1.AccessibleDescription = resources.GetString("crystalLabel1.AccessibleDescription");
			this.crystalLabel1.AccessibleName = resources.GetString("crystalLabel1.AccessibleName");
			this.crystalLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("crystalLabel1.Anchor")));
			this.crystalLabel1.AutoScroll = ((bool)(resources.GetObject("crystalLabel1.AutoScroll")));
			this.crystalLabel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.AutoScrollMargin")));
			this.crystalLabel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.AutoScrollMinSize")));
			this.crystalLabel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("crystalLabel1.BackgroundImage")));
			this.crystalLabel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("crystalLabel1.Dock")));
			this.crystalLabel1.Enabled = ((bool)(resources.GetObject("crystalLabel1.Enabled")));
			this.crystalLabel1.Font = ((System.Drawing.Font)(resources.GetObject("crystalLabel1.Font")));
			this.crystalLabel1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.crystalLabel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.crystalLabel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("crystalLabel1.ImeMode")));
			this.crystalLabel1.Location = ((System.Drawing.Point)(resources.GetObject("crystalLabel1.Location")));
			this.crystalLabel1.Name = "crystalLabel1";
			this.crystalLabel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("crystalLabel1.RightToLeft")));
			this.crystalLabel1.Size = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.Size")));
			this.crystalLabel1.TabIndex = ((int)(resources.GetObject("crystalLabel1.TabIndex")));
			this.crystalLabel1.Text = resources.GetString("crystalLabel1.Text");
			this.crystalLabel1.TextAlignment = System.Drawing.StringAlignment.Center;
			this.crystalLabel1.Visible = ((bool)(resources.GetObject("crystalLabel1.Visible")));
			this.crystalLabel1.TextChanged += new System.EventHandler(this.m_LabelPrintingJobInfo_TextChanged);
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
			this.Controls.Add(this.panel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "PrintingInfo";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.DoubleClick += new System.EventHandler(this.PreviewAndInfo_DoubleClick);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		public bool LableVisble
		{
			get{return this.panel1.Visible;}
			set
			{
				this.panel1.Visible = value;
				this.splitter1.Visible = value;
			}
		}
		public void OnPrintingStart()
		{
			m_StartTime = DateTime.Now;
		}

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
		}
		public void OnPreferenceChange( UIPreference up)
		{
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
			this.crystalLabel1.Text = strtext;
		}
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
			m_PrintPreview.SetPrinterChange(ic);
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
				float width = this.CalcRealJobWidth(unit,jobInfo.JobSize.Width);
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
				width = this.CalcRealJobWidth(unit,width);
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

		private void m_LabelPrintingJobInfo_TextChanged(object sender, System.EventArgs e)
		{
			if(this.JobInfoChanged != null)
				this.JobInfoChanged(sender,e);
		}

		private float CalcRealJobWidth(UILengthUnit unit, float jobwidth)
		{
			float ret = 0;
			AllParam allparam = this.m_iPrinterChange.GetAllParam();
			float orginX = allparam.PrinterSetting.sFrequencySetting.fXOrigin;
			float colorbarspace = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeOffset;
			float colorbarwidth = allparam.PrinterSetting.sBaseSetting.sStripeSetting.fStripeWidth;
			InkStrPosEnum place = allparam.PrinterSetting.sBaseSetting.sStripeSetting.eStripePosition;
			float pw = allparam.PrinterSetting.sBaseSetting.fPaperWidth;
			switch (place)
			{
				case InkStrPosEnum.Both:
					ret = (colorbarwidth+colorbarspace)*2 + jobwidth;
					if(orginX + ret>pw)
						ret = pw-orginX;
					break;
				case InkStrPosEnum.Left:
				case InkStrPosEnum.Right:
					ret = (colorbarwidth+colorbarspace) + jobwidth;
					if(orginX + ret>pw)
						ret = pw-orginX;
					break;
				case InkStrPosEnum.None:
					ret =jobwidth;
					if( orginX+ret>pw)
						ret = pw-orginX;
					break;
			}
			return ret;
		}
	}

}
