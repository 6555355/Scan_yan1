/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
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
		TimeSpan m_spendTime =new TimeSpan(0);
		private string m_sJobInfo = "";
		private float  m_fArea = 0;

		private IPrinterChange m_iPrinterChange =null;
		private bool m_bCreateImageWithPercent = false;
		private bool m_bPrintingPreview = true;
		private UILengthUnit m_curUnit;
		private UIJob		m_curJob;

		private BYHXPrinterManager.Preview.PrintingPreview m_PrintPreview;
		private System.Windows.Forms.Label m_LabelPrintingJobInfo;
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
			this.m_LabelPrintingJobInfo = new System.Windows.Forms.Label();
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
			this.m_PrintPreview.BackColor = System.Drawing.Color.White;
			this.m_PrintPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PrintPreview.BackgroundImage")));
			this.m_PrintPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PrintPreview.Dock")));
			this.m_PrintPreview.Enabled = ((bool)(resources.GetObject("m_PrintPreview.Enabled")));
			this.m_PrintPreview.Font = ((System.Drawing.Font)(resources.GetObject("m_PrintPreview.Font")));
			this.m_PrintPreview.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.White, System.Drawing.Color.White);
			this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
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
			this.m_LabelPrintingJobInfo.AutoSize = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.AutoSize")));
			this.m_LabelPrintingJobInfo.BackColor = System.Drawing.Color.Gray;
			this.m_LabelPrintingJobInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelPrintingJobInfo.Dock")));
			this.m_LabelPrintingJobInfo.Enabled = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.Enabled")));
			this.m_LabelPrintingJobInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelPrintingJobInfo.Font")));
			this.m_LabelPrintingJobInfo.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelPrintingJobInfo.Image")));
			this.m_LabelPrintingJobInfo.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPrintingJobInfo.ImageAlign")));
			this.m_LabelPrintingJobInfo.ImageIndex = ((int)(resources.GetObject("m_LabelPrintingJobInfo.ImageIndex")));
			this.m_LabelPrintingJobInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelPrintingJobInfo.ImeMode")));
			this.m_LabelPrintingJobInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelPrintingJobInfo.Location")));
			this.m_LabelPrintingJobInfo.Name = "m_LabelPrintingJobInfo";
			this.m_LabelPrintingJobInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelPrintingJobInfo.RightToLeft")));
			this.m_LabelPrintingJobInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelPrintingJobInfo.Size")));
			this.m_LabelPrintingJobInfo.TabIndex = ((int)(resources.GetObject("m_LabelPrintingJobInfo.TabIndex")));
			this.m_LabelPrintingJobInfo.Text = resources.GetString("m_LabelPrintingJobInfo.Text");
			this.m_LabelPrintingJobInfo.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPrintingJobInfo.TextAlign")));
			this.m_LabelPrintingJobInfo.Visible = ((bool)(resources.GetObject("m_LabelPrintingJobInfo.Visible")));
			this.m_LabelPrintingJobInfo.Click += new System.EventHandler(this.m_LabelPrintingJobInfo_Click);
			this.m_LabelPrintingJobInfo.DoubleClick += new System.EventHandler(this.m_LabelPrintingJobInfo_DoubleClick);
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
			this.Controls.Add(this.m_LabelPrintingJobInfo);
			this.Controls.Add(this.m_PrintPreview);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "PrintingInfo";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.DoubleClick += new System.EventHandler(this.PreviewAndInfo_DoubleClick);
			this.ResumeLayout(false);

		}
		#endregion

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
//				float width = this.CalcRealJobWidth(unit,jobInfo.JobSize.Width);
				UpdateJobInfoText( unit,  jobInfo.JobSize.Width, jobInfo.JobSize.Height,  jobInfo.ResolutionX,  jobInfo.ResolutionY,
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
			width = this.CalcRealJobWidth(unit,width);
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
			m_sJobInfo= 
				strSize 
				+ "\n" + strRes 
				+ "\n" + strDeep
				+ "\n" + strPass
				+ "\n" + strDir
				+ "\n" + sLangID
				+ "\n" + filepath;

			unitStr	= ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
			string strEff=string.Empty;
			float  efficient = 0;
			if(m_spendTime.TotalSeconds != 0)
				efficient =  (float)(m_fArea)/(float)m_spendTime.TotalHours ;
			if(efficient > 0)
				strEff =  efficient.ToString()  +  " "+ unitStr + "2/h";
			this.m_LabelPrintingJobInfo.Text = strEff + "\n" + m_sJobInfo;
		}
		private void UpdatePrintingJobInfo(UILengthUnit unit, SPrtFileInfo jobInfo)
		{
			float width = 0;
			if(jobInfo.sFreSetting.nResolutionX != 0)
			{
				width = (float)jobInfo.sImageInfo.nImageWidth / (float)(jobInfo.sFreSetting.nResolutionX);
//				width = this.CalcRealJobWidth(unit,width);
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
			m_spendTime = DateTime.Now - m_StartTime;
			string strTime =m_spendTime.Hours.ToString() +":" +m_spendTime.Minutes.ToString() +":" + m_spendTime.Seconds.ToString();
			string strPercentage = percent.ToString() + "%";
			
			string unitStr	= ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
			string strArea = (m_fArea * percent/100.0f).ToString() + " " + unitStr+ "2";
			float  efficient = 0;
			if(m_spendTime.TotalSeconds != 0)
				efficient =  (float)(m_fArea*percent/100.0f)/(float)m_spendTime.TotalHours ;
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
