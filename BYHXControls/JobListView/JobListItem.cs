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
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for JobListItem.
	/// </summary>
	public class JobListItem : ListViewItem
	{
		public JobListItem(UIJob job,UIPreference options)
		{
			//
			// TODO: Add constructor logic here
			//
			bool bPrinted = job.Status == JobStatus.Printed;
			int headerLen = options.JobListHeaderList.Length;
			string [] items = new string[headerLen];
			for (int i=0; i<headerLen;i++)
			{
				items[i] = "";
			}
			this.SubItems.AddRange(items);

			string	passDispName = ResString.GetDisplayPass();
			for (int i=0;i<headerLen;i++)
			{
				JobListColumnHeader cur =(JobListColumnHeader)options.JobListHeaderList[i];
				switch(cur)
				{
					case JobListColumnHeader.Name:
						this.SubItems[i].Text = job.Name;
						break;
					case JobListColumnHeader.Status:
						this.SubItems[i].Text = ResString.GetEnumDisplayName(typeof(JobStatus),job.Status);
						break;
					case JobListColumnHeader.Size:
						this.SubItems[i].Text = GetJobSize((float)job.ResolutionX,(float)job.ResolutionY,job.Dimension,options);
						break;
					case JobListColumnHeader.Resolution:
						this.SubItems[i].Text =job.ResolutionX.ToString() + "x" + job.ResolutionY.ToString();
						break;
					case JobListColumnHeader.Passes:
						this.SubItems[i].Text = job.PassNumber.ToString() + " " + passDispName;
						break;
					case JobListColumnHeader.BiDirection:
						this.SubItems[i].Text =ResString.GetEnumDisplayName(typeof(PrintDirection),(PrintDirection)job.PrintingDirection);
						break;
					case JobListColumnHeader.Copies:
						this.SubItems[i].Text =job.Copies.ToString();
						break;

					case JobListColumnHeader.PrintedPasses:
						this.SubItems[i].Text = job.PassNumber.ToString() + " " + passDispName;
						break;
					case JobListColumnHeader.PrintedDate:
						if(bPrinted)
						{
							string timeInfo = job.PrintedDate.ToString("u",DateTimeFormatInfo.InvariantInfo);
							int len = timeInfo.Length;
							if(len > 0 && !char.IsDigit(timeInfo,len-1))
								timeInfo = timeInfo.Substring(0,len-1);
							this.SubItems[i].Text = timeInfo;
						}
						break;
					case JobListColumnHeader.Location:
						this.SubItems[i].Text = job.FileLocation;
						break;
				}
			}
			this.Tag = job;
            ToolTipText = GetTooltipString(job, options);
			if(bPrinted)
			{
//				this.ForeColor = Color.LightGray;
				this.Font = new Font(this.Font.FontFamily,this.Font.Size,FontStyle.Bold);
			}
			if(!File.Exists(job.FileLocation))
			{
				this.ForeColor = Color.LightGray;
				this.Font = new Font(this.Font.FontFamily,this.Font.Size,FontStyle.Italic);
			}
		}

		public string GetJobSize(float xRes,float yRes,Size jobSize,UIPreference options)
		{
			float width = 0;
			float height = 0;
			if(xRes != 0)
				width = options.ToDisplayLength((float)jobSize.Width/xRes);
			if(yRes != 0)
				height = options.ToDisplayLength((float)jobSize.Height/yRes);

			return width.ToString("f2") + "x" + height.ToString("f2") + " " + ResString.GetUnitSuffixDispName(options.Unit);
		}


		private TimeSpan m_AllCopesTime = TimeSpan.Zero;
		private DateTime m_StartTime = DateTime.Now;

        //public string ToolTipString = string.Empty;
	    public string GetTooltipString(UIJob job,UIPreference options)
		{
			string newtip = string.Empty;
			bool bPrinted = job.Status == JobStatus.Printed;
			int colLen = options.JobListHeaderList.Length;
			for (int i=0; i<colLen;i++)				
			{
				JobListColumnHeader cur =(JobListColumnHeader)options.JobListHeaderList[i];
				string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader),cur) + " : ";
				int index = options.IndexOf(cur);
				cmode += this.SubItems[index].Text;
				newtip += cmode+ Environment.NewLine;
			}
			return newtip;
		}

		public void RefreshDis(UIJob job,UIPreference m_Preference,string strCopies)
		{
			JobStatus jobStatus = job.Status;
			int IndexOfStatus = m_Preference.IndexOf(JobListColumnHeader.Status);
			if(jobStatus == JobStatus.Printed)
			{
				if(IndexOfStatus>0)
					this.SubItems[IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus),jobStatus);
				job.PrintedDate			= DateTime.Now;
				int IndexOfDate = m_Preference.IndexOf(JobListColumnHeader.PrintedDate);
				if(IndexOfDate>0)
				{
					string timeInfo = job.PrintedDate.ToString("u",DateTimeFormatInfo.InvariantInfo);
					int len = timeInfo.Length;
					if(len > 0 && !char.IsDigit(timeInfo,len-1))
						timeInfo = timeInfo.Substring(0,len-1);
					this.SubItems[IndexOfDate].Text = timeInfo;
				}

				int IndexOfTime = m_Preference.IndexOf(JobListColumnHeader.PrintTime);
				if(IndexOfTime>0)
				{
					TimeSpan time = DateTime.Now - m_StartTime;
					m_AllCopesTime += time;
					string strTime = string.Empty;
					strTime = m_AllCopesTime.Hours.ToString() +":" +m_AllCopesTime.Minutes.ToString() +":" + m_AllCopesTime.Seconds.ToString();
					this.SubItems[IndexOfTime].Text = strTime;

//					string strCopies = m_PrintJobTask.GetCopiesString();
					if(strCopies != "")
					{
						string[] sss= strCopies.Split(new char[]{'/'});
						if(sss.Length == 2 && int.Parse(sss[0]) == int.Parse(sss[1]))
							m_AllCopesTime = TimeSpan.Zero;
					}
				}
#if false
				string	passDispName = ResString.GetDisplayPass();
				int IndexOfPass = m_Preference.IndexOf(JobListColumnHeader.PrintedPasses);
				this.SubItems[i].Text = job.PassNumber.ToString() + " " + passDispName;
#endif
//				this.ForeColor = Color.LightGray;
				this.Font = new Font(this.Font.FontFamily,this.Font.Size,FontStyle.Bold);
			}
			else
			{

				//if(job != m_PrintJobTask.GetPrintingJob())
//			{
				if(IndexOfStatus>0)
				{
					if(jobStatus == JobStatus.Printing)
						this.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus),jobStatus) + strCopies;
					else
						this.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus),jobStatus);
				}
//			}
			}
            int copyIndex = m_Preference.IndexOf(JobListColumnHeader.Copies);
            if (copyIndex > 0)
                this.SubItems[copyIndex].Text = job.Copies.ToString();
            int sizeIndex = m_Preference.IndexOf(JobListColumnHeader.Size);
            if (sizeIndex > 0)
                this.SubItems[sizeIndex].Text = GetJobSize((float)job.ResolutionX, (float)job.ResolutionY, job.Dimension, m_Preference);
            //更新ToolTip
            this.ToolTipText = GetTooltipString(job, m_Preference);
		}

		public void OnPrintStart(UIPreference m_Preference)
		{
			m_StartTime = DateTime.Now;
			SPrtFileInfo jobInfo = new SPrtFileInfo();
			if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
			{
				string	passDispName = ResString.GetDisplayPass();
				int IndexOfPass = m_Preference.IndexOf(JobListColumnHeader.PrintedPasses);
				if(IndexOfPass>0)
					this.SubItems[IndexOfPass].Text = jobInfo.sFreSetting.nPass.ToString() + " " + passDispName;
			}
			//更新ToolTip
            ToolTipText = GetTooltipString(this.Tag as UIJob, m_Preference);

		}
        public void RefreshSize(UIPreference m_Preference)
        {
            UIJob job = (UIJob)this.Tag;
            int IndexOfSize = m_Preference.IndexOf(JobListColumnHeader.Size);
            this.SubItems[IndexOfSize].Text = GetJobSize((float)job.ResolutionX, (float)job.ResolutionY, job.Dimension, m_Preference);
            this.ToolTipText = GetTooltipString(job, m_Preference);
 
        }
	}

}
