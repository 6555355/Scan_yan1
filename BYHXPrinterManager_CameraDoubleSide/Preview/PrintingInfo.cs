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
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics ;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Setting;
using System.IO;
using BYHXPrinterManager.Main;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for PrintingInfo.
	/// </summary>
	public class PrintingInfo : BYHXUserControl
	{
		DateTime m_StartTime = DateTime.Now;
		private string m_sJobInfo = "";
		private float  m_fArea = 0;

		private IPrinterChange m_iPrinterChange =null;
		private bool m_bCreateImageWithPercent = false;
		private bool m_bPrintingPreview = true;
		private UILengthUnit m_curUnit;
		private UIJob		m_curJob;
	    private int m_JobID;
	    private bool m_bJobIDVisible = true;

		private BYHXPrinterManager.Preview.PrintingPreview m_PrintPreview;
		private BYHXPrinterManager.GradientControls.CrystalLabel m_LabelPrintingJobInfo;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter2;
		private Label crystalLabel_Status;
        private Panel panel2;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingInfo));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_LabelPrintingJobInfo = new BYHXPrinterManager.GradientControls.CrystalLabel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.crystalLabel_Status = new System.Windows.Forms.Label();
            this.m_PrintPreview = new BYHXPrinterManager.Preview.PrintingPreview();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.crystalLabel_Status);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_LabelPrintingJobInfo);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // m_LabelPrintingJobInfo
            // 
            resources.ApplyResources(this.m_LabelPrintingJobInfo, "m_LabelPrintingJobInfo");
            this.m_LabelPrintingJobInfo.GradientColors = null;
            this.m_LabelPrintingJobInfo.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_LabelPrintingJobInfo.Name = "m_LabelPrintingJobInfo";
            this.m_LabelPrintingJobInfo.TabStop = false;
            this.m_LabelPrintingJobInfo.TextAlignment = System.Drawing.StringAlignment.Center;
            this.m_LabelPrintingJobInfo.Click += new System.EventHandler(this.m_LabelPrintingJobInfo_Click);
            this.m_LabelPrintingJobInfo.DoubleClick += new System.EventHandler(this.m_LabelPrintingJobInfo_DoubleClick);
            // 
            // splitter2
            // 
            this.splitter2.Cursor = System.Windows.Forms.Cursors.HSplit;
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // crystalLabel_Status
            // 
            this.crystalLabel_Status.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.crystalLabel_Status, "crystalLabel_Status");
            this.crystalLabel_Status.ForeColor = System.Drawing.Color.Lime;
            this.crystalLabel_Status.Name = "crystalLabel_Status";
            this.crystalLabel_Status.Click += new System.EventHandler(this.m_LabelPrintingJobInfo_Click);
            // 
            // m_PrintPreview
            // 
            this.m_PrintPreview.BackColor = System.Drawing.SystemColors.Control;
            this.m_PrintPreview.Divider = false;
            resources.ApplyResources(this.m_PrintPreview, "m_PrintPreview");
            this.m_PrintPreview.GradientColors = null;
            this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_PrintPreview.Name = "m_PrintPreview";
            this.m_PrintPreview.DoubleClick += new System.EventHandler(this.m_PrintPreview_DoubleClick);
            // 
            // PrintingInfo
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.m_PrintPreview);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "PrintingInfo";
            this.DoubleClick += new System.EventHandler(this.PreviewAndInfo_DoubleClick);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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

	    public int JobID
	    {
            get { return m_JobID; }
	    }

        public void SetGroupBoxStyle(Grouper ts)
        {
        }

	    public void OnPrintingStart()
		{
			m_StartTime = DateTime.Now;
		}

	    public void OnPrinterPropertyChange(SPrinterProperty sp)
	    {
	    }

	    public void SetStatusData(byte[] buf)
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
        public void SetPrinterStatusChanged(JetStatusEnum status, bool waitingPauseBetweenLayers=false)
		{
			string strtext = ResString.GetEnumDisplayName(typeof(JetStatusEnum),status);
            if (PubFunc.Is3DPrintMachine() && waitingPauseBetweenLayers && status == JetStatusEnum.Ready)
            {
                status = JetStatusEnum.Pause;
                strtext = ResString.GetResString("PauseBetweenLayersStatus");// 层间暂停
            }

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
				UpdatePreviewImage("");
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
                string path = "";
                if (job.IsClipOrTile)
                    path = PubFunc.GetFullPreviewPath(job.TilePreViewFile);
                else
                    path = PubFunc.GetFullPreviewPath(job.PreViewFile);
                UpdatePreviewImage(path);
                UpdateJobSizeInfo(job.JobSize);
				UpdateJobInfo(m_curUnit,job);
			}
			else
			{
				//No one job in joblist
				UpdatePreviewImage("");
				UpdateJobSizeInfo(SizeF.Empty);
				UpdateJobInfo(m_curUnit,null);
			}
			m_curJob = job;
		}

	    private string curPreview = string.Empty;
        private void UpdatePreviewImage(string preview)
        {
            if (curPreview != preview)
            {
                curPreview = preview;
                Image preImage = null;
                if (File.Exists(preview))
                {
                    try
                    {
                        Stream file = new FileStream(preview, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        preImage = new Bitmap(file);
                        file.Close();
                    }
                    catch
                    {
                        preImage = null;
                    }
                    m_PrintPreview.UpdatePreviewImage(preImage);
                }
            }
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

        /// <summary>
        /// 更新JobID
        /// </summary>
        /// <param name="_jobID"></param>
        public void UpdateJobID(int _jobID)
        {
            m_JobID = _jobID;
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
			UpdateJobInfoText( unit,  width, height, (int) (jobInfo.sFreSetting.nResolutionX*jobInfo.sImageInfo.nImageResolutionX),  (int) (jobInfo.sFreSetting.nResolutionY * jobInfo.sImageInfo.nImageResolutionY),
                jobInfo.sImageInfo.nImageColorDeep, jobInfo.sFreSetting.nPass, jobInfo.sFreSetting.nBidirection, null, 0);
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
		    string sjobId = "JobID:";
            if (PubFunc.Is3DPrintMachine())
                sjobId = ResString.GetResString("JobID3D");
            if (m_bJobIDVisible)
		    {
		        this.m_LabelPrintingJobInfo.Text = strTime
		                                           + "\n" + strPercentage
		                                           + "\n" + strArea
		                                           + "\n" + strEff
                                                   + "\n" + sjobId + m_JobID.ToString()
		                                           + "\n" + m_sJobInfo;
		    }
		    else
		    {
                this.m_LabelPrintingJobInfo.Text = strTime
                                                   + "\n" + strPercentage
                                                   + "\n" + strArea
                                                   + "\n" + strEff
                                                   + "\n" + m_sJobInfo;
		    }
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
			m_LabelPrintingJobInfo.BackgroundImage = imgbgk;
			m_PrintPreview.BackgroundImage = imgbgkmain;
		}

		public void SetDisStyle(bool bstatus,Color color1,Color color2)
		{
			crystalLabel_Status.Visible = bstatus;
			if(bstatus == false)
			{
				m_LabelPrintingJobInfo.GradientColors = new Style(color2,color2);
				m_PrintPreview.GradientColors = new Style(color1,color1);
				splitter1.Dock = DockStyle.Right;
				panel1.Dock = DockStyle.Right;
			}		
			else
			{
				m_LabelPrintingJobInfo.GradientColors = m_PrintPreview.GradientColors = new Style(color1,color2);
				splitter1.Dock = DockStyle.Left;
				panel1.Dock = DockStyle.Left;
			}
		}

	}

}
