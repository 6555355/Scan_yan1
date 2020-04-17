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
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;

using System.Xml;
using System.Timers;
using System.Resources;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using BYHXPrinterManager.Browser;
namespace BYHXPrinterManager.JobListView
{
    /// <summary>
    /// Summary description for JobListView.
    /// </summary>
    public class JobListForm : System.Windows.Forms.UserControl
    {
        // 选中文件夹对应得预览文件夹
        public string m_PreviewForlder = string.Empty;
		private bool m_bDuringPrinting = false;

        const string m_PreviewFolder = "Preview";
        const string m_JobListFile = "Joblist.xml";
        private PrintingInfo m_PreviewAndInfo;
        private PrintJobTask m_PrintJobTask;
        private PreviewJobTask m_PreviewTask;
        private IPrinterChange m_iPrinterChange;
        private JetStatusEnum m_LastPrinterStatus = JetStatusEnum.PowerOff;
        private JobItemOperate m_LastJobOperate;
        private UIPreference m_Preference;
		DateTime m_StartTime = DateTime.Now;
		TimeSpan m_AllCopesTime = TimeSpan.Zero;
        //private ControlPanelApp.PrintPreview m_Thumbnail;
        //private ControlPanelApp.PrintInfo m_PrintInfo;

        private System.Windows.Forms.ListView m_JobListView;
        private System.Windows.Forms.ColumnHeader m_ColumnHeaderName;
        private System.Windows.Forms.ColumnHeader m_ColumnHeaderSize;
        private ImageList imageList;
        private ToolStrip toolStrip5;
        private ToolStripButton ThumbnailStripButton;
        private ContextMenuStrip m_ContextMenuJobList;
        private ToolStripMenuItem m_MenuItemAdd;
        private ToolStripMenuItem m_MenuItemDelete;
        private ToolStripMenuItem m_MenuItemEdit;
        private ToolStripMenuItem m_MenuItemPrint;
        private ToolStripMenuItem m_MenuItemAbort;
        private ToolStripButton SelectAllButton;
        private ToolStripMenuItem m_MenuItemBPCP;
        private ToolStripMenuItem m_MenuItemNextBand;
        private ToolStripMenuItem m_MenuItemCurBand;
        private IContainer components;
        public event EventHandler SelectedIndexChanged;

        public JobListForm()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            m_PrintJobTask = new PrintJobTask(new CallbackWorkingJobFinished(OnJobStatusChanged), this.imageList.ImageSize, m_JobListView);
            m_PreviewTask = new PreviewJobTask(new CallbackWorkingJobFinished(OnPreviewChanged), this.imageList.ImageSize, m_JobListView);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobListForm));
            this.m_JobListView = new System.Windows.Forms.ListView();
            this.m_ColumnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.m_ColumnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.m_ContextMenuJobList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemAbort = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemBPCP = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemNextBand = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemCurBand = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.ThumbnailStripButton = new System.Windows.Forms.ToolStripButton();
            this.SelectAllButton = new System.Windows.Forms.ToolStripButton();
            this.m_ContextMenuJobList.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_JobListView
            // 
            resources.ApplyResources(this.m_JobListView, "m_JobListView");
            this.m_JobListView.AllowDrop = true;
            this.m_JobListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_JobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColumnHeaderName,
            this.m_ColumnHeaderSize});
            this.m_JobListView.ContextMenuStrip = this.m_ContextMenuJobList;
            this.m_JobListView.FullRowSelect = true;
            this.m_JobListView.GridLines = true;
            this.m_JobListView.HideSelection = false;
            this.m_JobListView.LargeImageList = this.imageList;
            this.m_JobListView.Name = "m_JobListView";
            this.m_JobListView.UseCompatibleStateImageBehavior = false;
            this.m_JobListView.SelectedIndexChanged += new System.EventHandler(this.m_JobListView_SelectedIndexChanged);
            this.m_JobListView.DoubleClick += new System.EventHandler(this.m_JobListView_DoubleClick);
            this.m_JobListView.Enter += new System.EventHandler(this.m_JobListView_Enter);
            this.m_JobListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragDrop);
            this.m_JobListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_JobListView_MouseDown);
            this.m_JobListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragEnter);
            this.m_JobListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_JobListView_KeyDown);
            // 
            // m_ColumnHeaderName
            // 
            resources.ApplyResources(this.m_ColumnHeaderName, "m_ColumnHeaderName");
            // 
            // m_ColumnHeaderSize
            // 
            resources.ApplyResources(this.m_ColumnHeaderSize, "m_ColumnHeaderSize");
            // 
            // m_ContextMenuJobList
            // 
            this.m_ContextMenuJobList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemAdd,
            this.m_MenuItemDelete,
            this.m_MenuItemEdit,
            this.m_MenuItemPrint,
            this.m_MenuItemAbort,
            this.m_MenuItemBPCP});
            this.m_ContextMenuJobList.Name = "m_ContextMenuJobList";
            resources.ApplyResources(this.m_ContextMenuJobList, "m_ContextMenuJobList");
            this.m_ContextMenuJobList.Opening += new System.ComponentModel.CancelEventHandler(this.m_ContextMenuJobList_Opening);
            // 
            // m_MenuItemAdd
            // 
            this.m_MenuItemAdd.Name = "m_MenuItemAdd";
            resources.ApplyResources(this.m_MenuItemAdd, "m_MenuItemAdd");
            this.m_MenuItemAdd.Click += new System.EventHandler(this.m_MenuItemAdd_Click);
            // 
            // m_MenuItemDelete
            // 
            this.m_MenuItemDelete.Name = "m_MenuItemDelete";
            resources.ApplyResources(this.m_MenuItemDelete, "m_MenuItemDelete");
            this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
            // 
            // m_MenuItemEdit
            // 
            this.m_MenuItemEdit.Name = "m_MenuItemEdit";
            resources.ApplyResources(this.m_MenuItemEdit, "m_MenuItemEdit");
            this.m_MenuItemEdit.Click += new System.EventHandler(this.m_MenuItemEdit_Click);
            // 
            // m_MenuItemPrint
            // 
            this.m_MenuItemPrint.Name = "m_MenuItemPrint";
            resources.ApplyResources(this.m_MenuItemPrint, "m_MenuItemPrint");
            this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
            // 
            // m_MenuItemAbort
            // 
            this.m_MenuItemAbort.Name = "m_MenuItemAbort";
            resources.ApplyResources(this.m_MenuItemAbort, "m_MenuItemAbort");
            this.m_MenuItemAbort.Click += new System.EventHandler(this.m_MenuItemAbort_Click);
            // 
            // m_MenuItemBPCP
            // 
            this.m_MenuItemBPCP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemNextBand,
            this.m_MenuItemCurBand});
            this.m_MenuItemBPCP.Name = "m_MenuItemBPCP";
            resources.ApplyResources(this.m_MenuItemBPCP, "m_MenuItemBPCP");
            this.m_MenuItemBPCP.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.m_MenuItemBPCP_DropDownItemClicked);
            // 
            // m_MenuItemNextBand
            // 
            this.m_MenuItemNextBand.Name = "m_MenuItemNextBand";
            resources.ApplyResources(this.m_MenuItemNextBand, "m_MenuItemNextBand");
            // 
            // m_MenuItemCurBand
            // 
            this.m_MenuItemCurBand.Name = "m_MenuItemCurBand";
            resources.ApplyResources(this.m_MenuItemCurBand, "m_MenuItemCurBand");
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "default.bmp");
            // 
            // toolStrip5
            // 
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThumbnailStripButton,
            this.SelectAllButton});
            resources.ApplyResources(this.toolStrip5, "toolStrip5");
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Stretch = true;
            // 
            // ThumbnailStripButton
            // 
            this.ThumbnailStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ThumbnailStripButton.Checked = true;
            this.ThumbnailStripButton.CheckOnClick = true;
            this.ThumbnailStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ThumbnailStripButton.Image = global::BYHXPrinterManager.Properties.Resources.view_16;
            resources.ApplyResources(this.ThumbnailStripButton, "ThumbnailStripButton");
            this.ThumbnailStripButton.Name = "ThumbnailStripButton";
            this.ThumbnailStripButton.CheckedChanged += new System.EventHandler(this.ThumbnailStripButton_CheckedChanged);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.SelectAllButton, "SelectAllButton");
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // JobListForm
            // 
            this.AllowDrop = true;
            this.ContextMenuStrip = this.m_ContextMenuJobList;
            this.Controls.Add(this.m_JobListView);
            this.Controls.Add(this.toolStrip5);
            this.DoubleBuffered = true;
            resources.ApplyResources(this, "$this");
            this.Name = "JobListForm";
            this.Load += new System.EventHandler(this.JobListForm_Load);
            this.m_ContextMenuJobList.ResumeLayout(false);
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        //private int thumbNailSize = 95;
        public Size ThumbNailSize
        {
            get { return this.imageList.ImageSize; }
            set { this.imageList.ImageSize = value; }
        }

        private Color thumbBorderColor = Color.Wheat;
        public Color ThumbBorderColor
        {
            get { return thumbBorderColor; }
            set { thumbBorderColor = value; }
        }

        public int SelectedCount
        {
            get { return this.m_JobListView.SelectedItems.Count; }
        }

        public ListViewAlignment mAlignment
        {
            get
            {
                return this.m_JobListView.Alignment;
            }
            set
            {
                this.m_JobListView.Alignment = value;
            }
        }

        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            if ((status != JetStatusEnum.Error
                || SErrorCode.IsWarningError(CoreInterface.GetBoardError())
                || SErrorCode.IsOnlyPauseError(CoreInterface.GetBoardError())) &&
                status != JetStatusEnum.Cleaning)
            {
                m_LastPrinterStatus = status;
            }

            if (!this.m_PrintJobTask.IsWorking())
            {
                return;
            }

            JobListItem jobItem = GetJobItemByJob(m_PrintJobTask.GetWorkingJob());

            if (jobItem == null)
            {
                return;
            }

            switch (m_LastPrinterStatus)
            {
                case JetStatusEnum.Busy:
                case JetStatusEnum.Pause:
                case JetStatusEnum.Aborting:
                case JetStatusEnum.Error:
                    {
                        int IndexOfStatus = m_Preference.IndexOf(JobListColumnHeader.Status);
                        if (IndexOfStatus > 0)
                        {
                            if (m_LastPrinterStatus == JetStatusEnum.Busy)
                                jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JetStatusEnum), m_LastPrinterStatus) + m_PrintJobTask.GetCopiesString();
                            else
                                jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JetStatusEnum), m_LastPrinterStatus);
                        }
                        UpdateButtonsStates();

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
        }
        public void OnPreferenceChange(UIPreference up)
        {
            bool bUnitChange = (m_Preference == null || m_Preference.Unit != up.Unit);
            bool bHeaderChange = CheckHeadIsChange(up);
            m_Preference = up.DeepCopy();
            if (bHeaderChange)
            {
                OnChangeHeader(m_Preference);
            }
            if (bUnitChange)
            {
                int IndexOfJobSize = up.IndexOf(JobListColumnHeader.Size);
                for (int i = 0; i < m_JobListView.Items.Count; i++)
                {
                    JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
                    UIJob jobInfo = (UIJob)jobItem.Tag;
                    string sizeInfo = jobItem.GetJobSize(jobInfo.ResolutionX, jobInfo.ResolutionY, jobInfo.Dimension, m_Preference);
                    if (IndexOfJobSize > 0)
                        jobItem.SubItems[(int)IndexOfJobSize].Text = sizeInfo;
                }
            }
        }

        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
			if(m_PrintJobTask != null)
				m_PrintJobTask.SetPrinterChange(ic);
			if(m_PreviewTask != null)
				m_PreviewTask.SetPrinterChange(ic);
        }
        public void SetPreviewInfo(PrintingInfo mPreviewAndInfo)
        {
            m_PreviewAndInfo = mPreviewAndInfo;
        }
        public void InitListHeader(UIPreference options)
        {
            this.m_JobListView.Items.Clear();
            this.m_JobListView.Columns.Clear();

            int colLen = options.JobListHeaderList.Length;
            ColumnHeader[] columnHeader = new ColumnHeader[colLen];
            for (int i = 0; i < colLen; i++)
            {
                columnHeader[i] = new ColumnHeader();
                JobListColumnHeader cur = (JobListColumnHeader)options.JobListHeaderList[i];
                string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader), cur);
                columnHeader[i].Text = cmode;
                columnHeader[i].Width = 150;
                columnHeader[i].TextAlign = HorizontalAlignment.Center;
            }
            //this.SuspendLayout();
            this.m_JobListView.Columns.AddRange(columnHeader);
            //this.ResumeLayout();
        }

        public void UpdateSourceFileExist()
        {
            for (int i = 0; i < m_JobListView.Items.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
                UIJob job = (UIJob)jobItem.Tag;
                if (!File.Exists(job.FileLocation))
                {
                    jobItem.ForeColor = Color.LightGray;

                    jobItem.Font = new Font(jobItem.Font.FontFamily, jobItem.Font.Size, FontStyle.Italic);
                }
            }
        }

        private void OnChangeHeader(UIPreference up)
        {
            ArrayList list = GetJobList();
            m_JobListView.Clear();
            InitListHeader(up);
            InitJobList(list);

        }
        private bool CheckHeadIsChange(UIPreference up)
        {
            if (m_Preference == null || m_Preference.JobListHeaderList == null ||
                m_Preference.JobListHeaderList.Length == 0 || up.JobListHeaderList.Length != m_Preference.JobListHeaderList.Length)
                return true;
            for (int i = 0; i < up.JobListHeaderList.Length; i++)
            {
                if (up.JobListHeaderList[i] != m_Preference.JobListHeaderList[i])
                {
                    return true;
                }
            }
            return false;
        }

		public void OnJobStatusChanged(UIJob job)
        {
            JobStatus jobStatus = job.Status;
            int IndexOfStatus = m_Preference.IndexOf(JobListColumnHeader.Status);
            JobListItem jobItem = GetJobItemByJob(job);

            if (jobItem == null)
            {
                //////This si for history job reprint design
                Debug.Assert(false);
                if (job == null)
                {
                    return;
                }
                m_JobListView.SelectedItems.Clear();
                jobItem = new JobListItem(job, m_Preference);
                jobItem.Selected = true;
                m_JobListView.Items.Add(jobItem);
				ListViewHelper.SetItemBackColor(this.m_JobListView);
            }

            if (jobStatus == JobStatus.Printed)
            {
                if (IndexOfStatus > 0)
                    jobItem.SubItems[IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus), jobStatus);
                job.PrintedDate = DateTime.Now;
				int IndexOfDate = m_Preference.IndexOf(JobListColumnHeader.PrintedDate);
				if(IndexOfDate>0)
				{
					string timeInfo = job.PrintedDate.ToString("u",DateTimeFormatInfo.InvariantInfo);
					int len = timeInfo.Length;
					if(len > 0 && !char.IsDigit(timeInfo,len-1))
						timeInfo = timeInfo.Substring(0,len-1);
					jobItem.SubItems[IndexOfDate].Text = timeInfo;
				}

				int IndexOfTime = m_Preference.IndexOf(JobListColumnHeader.PrintTime);
				if(IndexOfTime>0)
				{
					TimeSpan time = DateTime.Now - m_StartTime;
					m_AllCopesTime += time;
					string strTime = string.Empty;
					strTime = Math.Floor(m_AllCopesTime.TotalHours).ToString() +":" +m_AllCopesTime.Minutes.ToString() +":" + m_AllCopesTime.Seconds.ToString();
					jobItem.SubItems[IndexOfTime].Text = strTime;

					string strCopies = m_PrintJobTask.GetCopiesString();
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
                if (this.m_Preference.DelJobAfterPrint)
                {
                    m_JobListView.Items.Remove(jobItem);
					ListViewHelper.SetItemBackColor(this.m_JobListView);
                }

                this.UpdateButtonsStates();
                return;
            }
            else
            {

                //if(job != m_PrintJobTask.GetPrintingJob())
                {
                    if (IndexOfStatus > 0)
                    {
                        if (jobStatus == JobStatus.Printing)
                            jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus), jobStatus) + m_PrintJobTask.GetCopiesString();
                        else
                            jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JobStatus), jobStatus);
                    }
                }
                if (jobItem.Selected)
                {
                    UpdateButtonsStates();
                }
            }
        }

		public void OnPreviewChanged(UIJob job)
        {
            job.PreViewFile = GeneratePreviewName(job.Name);
            string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + job.PreViewFile;
            if (job.Miniature != null)
                job.Miniature.Save(mPreviewFolder);

            JobListItem jobItem = GetJobItemByJob(job);

            if (jobItem == null)
            {
                return;
                //////This si for history job reprint design
            }
            this.SetImageIndex(job, ref jobItem);

            //Notify UI changed 
            if (m_JobListView.SelectedItems != null && m_JobListView.SelectedItems.IndexOf(jobItem) == 0)
            {
                m_PreviewAndInfo.UpdateUIJobPreview(job);
            }
        }


        public void OnSwitchPreview()
        {
			if(!m_bDuringPrinting) return;
            OnSwitchToPrintingPreview();
        }

        public void OnPrintingStart()
        {
			m_bDuringPrinting = true;
			m_StartTime = DateTime.Now;
            OnSwitchToPrintingPreview();
            m_PreviewAndInfo.OnPrintingStart();
            //This Only Update Printed Pass

            SPrtFileInfo jobInfo = new SPrtFileInfo();
            JobListItem jobItem = GetJobItemByJob(m_PrintJobTask.GetWorkingJob());
            if (jobItem != null && CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
            {
                //UpdatePrintingJobInfo(m_curUnit,jobInfo);
                //SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth,jobInfo.sImageInfo.nImageHeight,jobInfo.sFreSetting.nResolutionX,jobInfo.sFreSetting.nResolutionY);
                //UpdateJobSizeInfo(previewsize);

                string passDispName = ResString.GetDisplayPass();
                int IndexOfPass = m_Preference.IndexOf(JobListColumnHeader.PrintedPasses);
                if (IndexOfPass > 0)
                    jobItem.SubItems[IndexOfPass].Text = jobInfo.sFreSetting.nPass.ToString() + " " + passDispName;
            }
        }
        public void OnPrintingEnd()
        {
			m_bDuringPrinting = false;
            OnSwitchToJobList();
        }

        private void OnSwitchToPrintingPreview()
        {
            m_PreviewAndInfo.SetPrintingPreview(true);
            UIJob job = m_PrintJobTask.GetWorkingJob();
            if (job == null)
            {
                m_PreviewAndInfo.SetCreateImageWithPercent(true);
                m_PreviewAndInfo.OnSwitchToPrintingPreview();
            }
            else
            {
                m_PreviewAndInfo.SetCreateImageWithPercent(false);
                //m_PreviewAndInfo.OnSwitchToPrintingPreview(); ?????Only Update info
                //m_PreviewAndInfo.UpdateUIJobPreview(job);
                m_PreviewAndInfo.OnUpdatePrintingInfo(job.Miniature);
            }
        }

        public ArrayList GetJobList()
        {
            ArrayList list = new ArrayList();
            foreach (ListViewItem oneItem in m_JobListView.Items)
            {
                if (oneItem.Tag == null)
                    continue;
                list.Add((UIJob)oneItem.Tag);
            }
            return list;
        }
        private void InitJobList(ArrayList jobInfoList)
        {
            m_JobListView.Items.Clear();

            if (jobInfoList == null)
            {
                return;
            }
            for (int i = 0; i < jobInfoList.Count; i++)
            {
                UIJob jobInfo = (UIJob)jobInfoList[i];
                string file = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + jobInfo.PreViewFile;
                try
                {
                    if (File.Exists(file))
                    {
                        FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        jobInfo.Miniature = new Bitmap(stream);
                        int imageW = Convert.ToInt32(((float)this.imageList.ImageSize.Height / (float)jobInfo.Miniature.Height) * jobInfo.Miniature.Width);
                        jobInfo.ThumbnailImage = this.CreateThumbnailImage(jobInfo.Miniature, this.imageList.ImageSize, thumbBorderColor);
                        //jobInfo.ThumbnailImage = jobInfo.Miniature.GetThumbnailImage(this.imageList.ImageSize.Width, this.imageList.ImageSize.Height, null, IntPtr.Zero);
                        stream.Close();
                    }
                }
                catch (Exception) { }
                JobListItem jobItem = new JobListItem(jobInfo, m_Preference);
                if (!File.Exists(((UIJob)jobItem.Tag).FileLocation))
                {
                    jobItem.ForeColor = Color.LightGray;
                    jobItem.Font = new Font(jobItem.Font.FontFamily, jobItem.Font.Size, FontStyle.Italic);
                }

                if (jobInfo.Miniature != null)
                {
                    this.SetImageIndex(jobInfo, ref jobItem);
                    m_JobListView.Items.Add(jobItem);
                }
                else
                    AddJob(jobInfo.FileLocation, false);

            }

            if (m_JobListView.Items.Count > 0)
            {
                m_JobListView.Items[0].Selected = true;
            }
			ListViewHelper.SetItemBackColor(this.m_JobListView);
        }

        private Image CreateThumbnailImage(Image Miniature, Size imageSize, Color penColor)
        {
            Bitmap bmp;

            int imgWidth = imageSize.Width;
            int imgHeight = imageSize.Height;
            try
            {
                bmp = (Bitmap)Miniature;
            }
            catch
            {
                bmp = new Bitmap(imgWidth, imgHeight); //If we cant load the image, create a blank one with ThumbSize
            }

            imgWidth = bmp.Width > imgWidth ? imgWidth : bmp.Width;
            imgHeight = bmp.Height > imgHeight ? imgHeight : bmp.Height;

            Bitmap retBmp = new Bitmap(imgWidth, imgHeight, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            Graphics grp = Graphics.FromImage(retBmp);


            int tnWidth = imgWidth, tnHeight = imgHeight;

            if (bmp.Width > bmp.Height)
                tnHeight = (int)(((float)bmp.Height / (float)bmp.Width) * tnWidth);
            else if (bmp.Width < bmp.Height)
                tnWidth = (int)(((float)bmp.Width / (float)bmp.Height) * tnHeight);

            int iLeft = (imgWidth / 2) - (tnWidth / 2);
            int iTop = (imgHeight / 2) - (tnHeight / 2);

            grp.PixelOffsetMode = PixelOffsetMode.None;
            grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grp.DrawImage(bmp, iLeft, iTop, tnWidth, tnHeight);

            Pen pn = new Pen(penColor, 1); //Color.Wheat
            grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);

            return retBmp;
        }

        private void FilterPreviewJobList(ArrayList jobList)
        {
            string sPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar;
            if (!Directory.Exists(sPreviewFolder))
            {
                Directory.CreateDirectory(sPreviewFolder);
            }
            DirectoryInfo di = new DirectoryInfo(sPreviewFolder);
            FileInfo[] fiArr = di.GetFiles();
            foreach (FileInfo f in fiArr)
            {
                try
                {
                    bool bFound = false;
                    for (int i = 0; i < jobList.Count; i++)
                    {
                        if (f.Name.ToUpper() == ((UIJob)jobList[i]).PreViewFile.ToUpper())
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        File.Delete(f.FullName);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        public bool SaveJobList()
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;
            ArrayList jobList = GetJobList();
            //if(jobList.Count == 0) return true;
            FilterPreviewJobList(jobList);
            XmlDocument doc = new XmlDocument();
            FilterJobStatus(jobList);
            bool success = true;
            try
            {
                XmlElement root = doc.CreateElement("JobList");
                doc.AppendChild(root);
                string xml = "";
                for (int i = 0; i < jobList.Count; i++)
                {
                    UIJob job = ((UIJob)jobList[i]).Clone();
                    job.Miniature = null;
                    job.PrtFileInfo.sImageInfo.nImageData = 0;
#if false
					XmlElement xmlJobList = DNetXmlSerializer.ClassToXml("Job",(UIJob)jobList[i],typeof(UIJob),doc);
					root.AppendChild(xmlJobList);
#else
                    //xml += PubFunc.SystemConvertToXml((UIJob)jobList[i],jobList[i].GetType());
                    xml += job.SystemConvertToXml();
#endif
                }
                root.InnerXml = xml;
                doc.Save(fileName);

            }
            catch (Exception e)
            {
                success = false;

                Debug.Assert(false, e.Message + e.StackTrace);
            }

            return success;
        }

        public bool LoadJobList()
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;
            ArrayList jobList = new ArrayList();
            if (!File.Exists(fileName))
                return false;

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fileName);
                XmlElement root = (XmlElement)doc.DocumentElement;
                try
                {
                    XmlNode XmlJob = root.FirstChild;
                    while (XmlJob != null)
                    {
#if false
						UIJob job = (UIJob)DNetXmlSerializer.ClassFromXml((XmlElement)XmlJob,typeof(UIJob));
#else
                        //UIJob job = (UIJob)PubFunc.SystemConvertFromXml(XmlJob.OuterXml,typeof(UIJob));
                        UIJob job = (UIJob)UIJob.SystemConvertFromXml((XmlElement)XmlJob, typeof(UIJob));
#endif
                        jobList.Add(job);
                        XmlJob = XmlJob.NextSibling;
                    }
                }
                catch (Exception e)
                {
                    Debug.Assert(false, e.Message + e.StackTrace);
                }
                FilterJobStatus(jobList);
                InitJobList(jobList);
                return true;
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
                return false;
            }
        }

        private void FilterJobStatus(ArrayList jobList)
        {
            for (int i = 0; i < jobList.Count; i++)
            {
                UIJob job = (UIJob)jobList[i];
                if (job.Status == JobStatus.Paused ||
                    job.Status == JobStatus.Printing ||
                    job.Status == JobStatus.Waiting ||
                    job.Status == JobStatus.Unknown ||
                    job.Status == JobStatus.Aborting)
                {
                    job.Status = JobStatus.Idle;
                }
            }
        }

        public JobItemOperate GetJobItemOperate()
        {
            JobItemOperate jobOperate = new JobItemOperate(m_JobListView.SelectedItems.Count > 0);

            JetStatusEnum printerStatus = m_LastPrinterStatus;

            for (int i = 0; i < m_JobListView.SelectedItems.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
                UIJob jobInfo = (UIJob)jobItem.Tag;

                if (jobInfo != null)
                {
                    if (m_PrintJobTask.IsWorkingJob(jobInfo))
                    {
                        jobOperate.UpdateByStatus(jobInfo.Status, printerStatus);

                        if (printerStatus == JetStatusEnum.Error)
                        {
                            jobOperate.CanAbortJob = m_LastJobOperate.CanAbortJob;
                            jobOperate.CanPausePrint = m_LastJobOperate.CanPausePrint;
                            jobOperate.CanResumePrint = m_LastJobOperate.CanResumePrint;
                        }
                    }
                    else
                    {
                        jobOperate.UpdateByStatus(jobInfo.Status, printerStatus);
                    }
                }
            }
            return jobOperate;
        }

        public void OpenJob()
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //AddJobForm fileDialog = new AddJobForm();

            ////fileDialog.Multiselect = true;
            ////fileDialog.CheckFileExists = true;
            ////fileDialog.DefaultExt = ".prn";
            ////fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Prn);
            ////fileDialog.InitialDirectory = m_Preference.WorkingFolder;

            //if(fileDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    Cursor.Current = Cursors.WaitCursor;

            //    m_Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);

            //    m_JobListView.SelectedItems.Clear();

            //    string[] fileNames = fileDialog.FileNames;

            //    this.AddJobs(fileNames);

            //    Cursor.Current = Cursors.Default;
            //}
        }
        public void AddJobs(string[] fileNames)
        {
			try
			{
				for (int i = 0; i < fileNames.Length; i++)
				{
					AddJob(fileNames[i], false);
				}
			}
			catch
			{
				Debug.Assert(false);
            }
			ListViewHelper.SetItemBackColor(this.m_JobListView);
        }
        private string GeneratePreviewName(string jobName)
        {
            string previewName = Path.GetFileNameWithoutExtension(jobName);
            string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
            if (!Directory.Exists(mPreviewFolder))
            {
                Directory.CreateDirectory(mPreviewFolder);
            }
            string mPreviewFile = previewName;
            for (int i = 0; i < 1000; i++)
            {
                mPreviewFile = previewName + "_" + i.ToString("D3") + ".bmp";
                string cur = mPreviewFolder + Path.DirectorySeparatorChar + mPreviewFile;
                if (!File.Exists(cur))
                    return mPreviewFile;
            }
            return "";
        }

        private void AddJob(string fileName, bool bPrint)
        {
            UIJob job = new UIJob();
            job.Name = Path.GetFileName(fileName);
            job.PreViewFile = m_PreviewForlder + Path.GetFileNameWithoutExtension(fileName) + ".bmp";
            job.Status = JobStatus.Idle;

            SPrtFileInfo jobInfo = new SPrtFileInfo();
            Int32 bret = 0;
            bret = CoreInterface.Printer_GetFileInfo(fileName, ref jobInfo, 0);
            if (bret == 1)
            {
                job.PrtFileInfo = jobInfo;
                job.FileLocation = fileName;
#if false
				job.Miniature			= SerialFunction.CreateImageWithImageInfo(jobInfo.sImageInfo);
				job.PreViewFile			= GeneratePreviewName(job.Name);
				string mPreviewFolder	= Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + job.PreViewFile;
				if(job.Miniature != null)
				     job.Miniature.Save(mPreviewFolder);
#endif
                //CoreInterface.FreeJobPreview(jobInfo.sImageInfo.nImageData);
                //m_JobList.Add(job);
            }
            else
            {
				string info = SErrorCode.GetEnumDisplayName(typeof(Software),Software.Parser);
                info += ":" + fileName;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor.Current = Cursors.WaitCursor;
                return;
            }

            JobListItem jobItem = new JobListItem(job, m_Preference);
            m_JobListView.Items.Add(jobItem);

            jobItem.Selected = true;

            if (bPrint)
            {
                if (JobItemOperate.IsPrinterCanPrint())
                {
                    PrintJob((UIJob)(jobItem.Tag));
                }
            }


            ///////////////////////////////////
            //Preview
            m_PreviewTask.AddJob(job);
            if (!m_PreviewTask.IsWorking())
            {
                m_PreviewTask.SetWorking(true);
                ThreadStart threadStart = new ThreadStart(m_PreviewTask.WorkingThreadProc);
                Thread mPrintThread = new Thread(threadStart);
                mPrintThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                mPrintThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                mPrintThread.IsBackground = true;
                m_PreviewTask.SetWorker(mPrintThread);
                mPrintThread.Start();
            }

            //this.SetImageIndex(job,ref jobItem);
        }

        private void SetImageIndex(UIJob job, ref JobListItem jobItem)
        {
            string strImageKey = job.PreViewFile;
            if (!this.imageList.Images.Keys.Contains(strImageKey))
            {
                if (job.ThumbnailImage != null)
                {
                    this.imageList.Images.Add(strImageKey, job.ThumbnailImage);
                    //jobItem.ImageIndex = this.imageList.Images.IndexOfKey(strImageKey);
                    jobItem.ImageKey = strImageKey;
                    //jobItem.ImageIndex = this.imageList.Images.Count - 1;
                }
                else
                {
                    //jobItem.ImageIndex = 0;
                    jobItem.ImageKey = this.imageList.Images.Keys[0];
                }
            }
            else
            {
                if (job.ThumbnailImage != null)
                {
                    int index = this.imageList.Images.IndexOfKey(strImageKey);//jobItem.ImageIndex;
                    this.imageList.Images[index] = job.ThumbnailImage;
                }
                //jobItem.ImageIndex = this.imageList.Images.IndexOfKey(strImageKey);
                jobItem.ImageKey = strImageKey;
                //jobItem.ImageIndex = this.imageList.Images.Count - 1;
            }
        }

        public void DeleteJob()
        {
            if (m_JobListView.SelectedItems.Count == 0)
            {
                return;
            }

            string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Delete);

            if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int index = 0;
                int count = m_JobListView.SelectedItems.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
                    if (((UIJob)jobItem.Tag).Status != JobStatus.Printing)
                    {
                        m_PrintJobTask.AbortJob((UIJob)jobItem.Tag);
                        m_PreviewTask.AbortJob((UIJob)jobItem.Tag);
                        m_JobListView.Items.Remove(jobItem);

                        if (!string.IsNullOrEmpty(jobItem.ImageKey))// > 0)
                            this.imageList.Images.RemoveByKey(jobItem.ImageKey);
                    }
                }

                if (m_JobListView.Items.Count > 0 && m_JobListView.SelectedItems.Count <= 0)
                {
                    if (index > m_JobListView.Items.Count - 1)
                    {
                        index = m_JobListView.Items.Count - 1;
                    }
                    m_JobListView.Items[index].Selected = true;
                }
                else if (m_JobListView.Items.Count == 0)
                {
                    this.m_PreviewAndInfo.UpdateUIJobPreview(null);
                }

				ListViewHelper.SetItemBackColor(this.m_JobListView);
            }
        }

        public void EditJob()
        {
            if (m_JobListView.SelectedItems.Count == 0)
            {
                return;
            }

            int copies = 1;

            if (m_JobListView.SelectedItems.Count == 1)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];

                copies = ((UIJob)(jobItem.Tag)).Copies;
            }
            JobEditForm editJob = new JobEditForm();
            //EditJobForm editJob = new EditJobForm();

            editJob.Copies = copies;
            //editJob.SetPrinterChange(this.m_iPrinterChange);
            //editJob.OnPrinterPropertyChange(this.m_iPrinterChange.GetAllParam().PrinterProperty);
            //editJob.EditJob = ((UIJob)(jobItem.Tag)).Clone();

            if (editJob.ShowDialog(this) == DialogResult.OK)
            {
                copies = editJob.Copies;

                for (int i = 0; i < m_JobListView.SelectedItems.Count; i++)
                {
                    JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
                    //jobItem1.Tag = editJob.EditJob;
                    ((UIJob)(jobItem.Tag)).Copies = copies;
                    int copyIndex = m_Preference.IndexOf(JobListColumnHeader.Copies);
                    if (copyIndex > 0)
                        jobItem.SubItems[copyIndex].Text = copies.ToString();

                    //if (editJob.PreviewChanged)
                    //    this.SetImageIndex((UIJob)(jobItem1.Tag), ref jobItem1);
                }
            }
        }
        private float GetInkStripeWidth(SColorBarSetting iss)
        {
            float inkSW = iss.fStripeWidth +
                iss.fStripeOffset;
            switch (iss.eStripePosition)
            {
                case InkStrPosEnum.Both:
                    inkSW *= 2;
                    break;
                case InkStrPosEnum.Left:
                    break;
                case InkStrPosEnum.Right:
                    break;
                case InkStrPosEnum.None:
                    inkSW = 0;
                    break;
                default:
                    inkSW = 0;
                    break;
            }
            return inkSW;
        }
        public void GetPrintableArea(out float Area, out float height)
        {
            float mediaHeight = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fPaperHeight;
            float mediaWidth = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fPaperWidth;
            float origin = this.m_iPrinterChange.GetAllParam().PrinterSetting.sFrequencySetting.fXOrigin;
            float originY = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fYOrigin;
            float xPaperLeft = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fLeftMargin;
            float xPaperTop = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fTopMargin;
            float inkstripeWidth = GetInkStripeWidth(this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.sStripeSetting);
            float usableWidth = xPaperLeft + mediaWidth - origin - inkstripeWidth;
            float usableHeight = xPaperTop + mediaWidth - originY;

            Area = usableWidth;
            height = usableHeight;
        }
        public void PrintJob(PrintMode printMode)
        {
            if (!JobItemOperate.IsPrinterCanPrint())
            {
                return;
            }
            this.m_iPrinterChange.NotifyUIParamChanged();
            string lostFiles = null;

            float usableWidth, usableHeight;
            GetPrintableArea(out usableWidth, out usableHeight);

            JobListItem[] printableItems = new JobListItem[m_JobListView.SelectedItems.Count];
            JobListItem[] notPrintableItems = new JobListItem[m_JobListView.SelectedItems.Count];
            JobListItem[] langIdErrorItems = new JobListItem[m_JobListView.SelectedItems.Count];

            int printableCount = 0;
            int notPrintableCount = 0;

            for (int i = 0; i < m_JobListView.SelectedItems.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];

                UIJob jobInfo = (UIJob)jobItem.Tag;
                jobInfo.sJobSetting.pPrintMode = printMode;
                if (!File.Exists(jobInfo.FileLocation))
                {
                    if (lostFiles == null)
                    {
                        lostFiles = "\n    " + jobItem.Text;
                    }
                    else
                    {
                        lostFiles += ",\n    " + jobItem.Text;
                    }
                    continue;
                }
#if CHECK_IN_PRINT
				if(jobInfo.JobSize.Width > usableWidth)
#else
                if (false)
#endif
                {
                    notPrintableItems[notPrintableCount] = jobItem;

                    notPrintableCount++;
                }
                else
                {
                    printableItems[printableCount] = jobItem;

                    printableCount++;
                }
            }

            if (printableCount > 0)
            {
                for (int i = 0; i < printableCount; i++)
                {
                    PrintJob((UIJob)printableItems[i].Tag);
                }
            }

            if (notPrintableCount > 0)
            {
                string itemList = "";

                itemList += "\n    " + notPrintableItems[0].Text;

                for (int i = 1; i < notPrintableCount; i++)
                {
                    itemList += ",\n    " + notPrintableItems[i].Text;
                }

				string notPrintableMsg = SErrorCode.GetEnumDisplayName(typeof(Software),Software.MediaTooSmall) + "{0}"
                    + "\n" + ResString.GetPrintJobAnyway();

                notPrintableMsg = string.Format(notPrintableMsg, itemList);

                if (MessageBox.Show(notPrintableMsg, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    for (int i = 0; i < notPrintableCount; i++)
                    {
                        PrintJob((UIJob)notPrintableItems[i].Tag);
                    }
                }
            }
            if (lostFiles != null)
            {
                lostFiles += ".\n";

                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.FileNotExist);

                info += "\n" + lostFiles;

                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
		}
		public void PrintJob()
		{
			this.PrintJob(PrintMode.Normal);
		}
        private void PrintJob(UIJob job)
        {
            m_PrintJobTask.AddJob(job);
            job.Status = JobStatus.Waiting;
			OnJobStatusChanged(job);
            if (!m_PrintJobTask.IsWorking())
            {
                m_PrintJobTask.SetWorking(true);
                ThreadStart threadStart = new ThreadStart(m_PrintJobTask.WorkingThreadProc);
                Thread mPrintThread = new Thread(threadStart);
                mPrintThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                mPrintThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                mPrintThread.IsBackground = true;
                m_PrintJobTask.SetWorker(mPrintThread);
                mPrintThread.Start();
            }
        }
        public void AbortJob()
        {
            for (int i = 0; i < m_JobListView.SelectedItems.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
                UIJob job = (UIJob)jobItem.Tag;
				if(job.Status == JobStatus.Printing)
					CoreInterface.Printer_Abort();
                m_PrintJobTask.AbortJob(job);
            }
        }

		public void AbortPrintingJob()
		{
			CoreInterface.Printer_Abort();
			m_PrintJobTask.Abort();
		}

        private void ResetJob()
        {
            for (int i = 0; i < m_JobListView.SelectedItems.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];

                ((UIJob)(jobItem.Tag)).Status = JobStatus.Idle;
            }
        }

        public void TerminatePrintingJob(bool bTerm)
        {
            if (m_PrintJobTask.IsWorking())
                m_PrintJobTask.Terminate(bTerm);
            if (bTerm)
            {
                if (m_PrintJobTask.m_Worker != null)
                    m_PrintJobTask.m_Worker.Join();
            }
            if (m_PreviewTask.IsWorking())
                m_PreviewTask.Terminate(bTerm);
            if (bTerm)
            {
                if (m_PreviewTask.m_Worker != null)
                    m_PreviewTask.m_Worker.Join();
            }
        }


        public void PrintDemoPage()
        {
#if false
			string fileName = Application.StartupPath + Path.DirectorySeparatorChar + "1.prn";
			
			if(!File.Exists(fileName))
			{
				string info = ResString.GetResString("UIError_FileNotExist");

				info += "\n" + lostFiles;
				
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			try
			{
				AddJob(fileName,true);
			}
			catch
			{
				Debug.Assert(false);
			}
			
			Cursor.Current = Cursors.Default;
#endif
        }

        private JobListItem GetJobItemByJob(UIJob job)
        {
			for(int i = 0;i < m_JobListView.Items.Count; i++)
			{
				JobListItem jobItem = (JobListItem)m_JobListView.Items[i];

                if (jobItem.Tag == job)
                {
                    return jobItem;
                }
            }
            return null;
        }

        public bool Confirm_Exit()
        {
            bool bBusy = false;

            for (int i = 0; i < m_JobListView.Items.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.Items[i];

                UIJob jobInfo = (UIJob)jobItem.Tag;

                switch (jobInfo.Status)
                {
                    case JobStatus.Printing:
                    case JobStatus.Waiting:
                    case JobStatus.Paused:
                        {
                            bBusy = true;
                            break;
                        }
                }
            }

            if (bBusy)
            {
                string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.AbortPrinter);

                if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }




        private void UpdateButtonsStates()
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            m_LastJobOperate = jobOperate;

            m_MenuItemAdd.Enabled = jobOperate.CanOpenJob;
            m_MenuItemBPCP.Enabled = m_MenuItemPrint.Enabled = jobOperate.CanPrintJob;
            m_MenuItemDelete.Enabled = jobOperate.CanDeleteJob;

        }

        private void OnSwitchToJobList()
        {
            m_PreviewAndInfo.SetPrintingPreview(false);
            //No Item in job List
            if (m_JobListView.Items.Count == 0)
            {
                m_PreviewAndInfo.UpdateUIJobPreview(null);
            }
            else if (m_JobListView.SelectedItems.Count == 0)
            {
                //Set Select
                m_JobListView.Items[0].Selected = true;
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
                UIJob jobInfo = (UIJob)jobItem.Tag; ;
                Debug.Assert(jobInfo != null);

                m_PreviewAndInfo.UpdateUIJobPreview(jobInfo);
            }
            else
            {
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
                UIJob jobInfo = (UIJob)jobItem.Tag; ;
                Debug.Assert(jobInfo != null);
                m_PreviewAndInfo.UpdateUIJobPreview(jobInfo);
            }
        }

        public void m_ToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
        }

        private void m_JobListView_Enter(object sender, System.EventArgs e)
        {
            OnSwitchToJobList();
            UpdateButtonsStates();
        }

        private void m_JobListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateButtonsStates();
            if (m_JobListView.SelectedItems.Count == 0 || !isMouseLeftButton)
            {
                return;
            }
            m_PreviewAndInfo.SetPrintingPreview(false);
            JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
            UIJob jobInfo = (UIJob)jobItem.Tag; ;
            Debug.Assert(jobInfo != null);
            m_PreviewAndInfo.UpdateUIJobPreview(jobInfo);
			
			if (this.SelectedIndexChanged != null)
				this.SelectedIndexChanged(sender,e);
        }

        private void m_JobListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
                e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyData != Keys.Delete)
            {
                return;
            }

            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanDeleteJob)
            {
                DeleteJob();
            }
        }

        private void m_JobListView_DoubleClick(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanEditJob)
            {
                EditJob();
            }

        }

        private void m_JobListView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] fileItems = null;
            if (e.Data.GetDataPresent("FileDrop"))
            {
                fileItems = (string[])e.Data.GetData("FileDrop");
            }
            else if (e.Data.GetDataPresent(typeof(System.String[])))
            {
                fileItems = (string[])e.Data.GetData(typeof(System.String[]));
            }

            if (fileItems == null || fileItems.Length == 0)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            for (int i = 0; i < fileItems.Length; i++)
            {
                try
                {
                    AddJob(fileItems[i], false);
                }
                catch
                {
                    Debug.Assert(false);
                }
            }
			ListViewHelper.SetItemBackColor(this.m_JobListView);
            Cursor.Current = Cursors.Default;
        }

        private void m_JobListView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private ArrayList GetAllFiles(string[] fileItems)
        {
            ArrayList fileList = new ArrayList();

            for (int i = 0; i < fileItems.Length; i++)
            {
                string extName = Path.GetExtension(fileItems[i]).ToLower();

                if (extName == ".prn" || extName == ".prt")
                {
                    if (File.Exists(fileItems[i]))
                    {
                        fileList.Add(fileItems[i]);
                    }
                }
            }
            return fileList;
        }

        private void m_ContextMenuJobList_Popup(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            m_MenuItemDelete.Enabled = jobOperate.CanDeleteJob;
            m_MenuItemEdit.Enabled = jobOperate.CanEditJob;

            m_MenuItemPrint.Enabled = jobOperate.CanPrintJob;
            m_MenuItemAbort.Enabled = jobOperate.CanAbortJob | jobOperate.CanAbortPrint;

        }
        private void m_MenuItemAdd_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanOpenJob)
            {
                OpenJob();
            }

        }
        private void m_ButtonDelete_Click()
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanDeleteJob)
            {
                DeleteJob();
            }
        }

        private void m_ButtonEdit_Click()
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanEditJob)
            {
                EditJob();
            }
        }

        public void m_ButtonPrint_Click()
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanPrintJob)
            {
                PrintJob();
            }
        }

        private void m_ButtonReset_Click()
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanResetJob)
            {
                ResetJob();
            }
        }


        private void m_MenuItemDelete_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanDeleteJob)
            {
                DeleteJob();
            }
        }

        private void m_MenuItemEdit_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanEditJob)
            {
                EditJob();
            }
        }

        private void m_MenuItemPrint_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanPrintJob)
            {
                PrintJob();
            }
        }

        private void m_MenuItemAbort_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

//			if(jobOperate.CanAbortPrint)
//			{
//				CoreInterface.Printer_Abort();
//			}

            if (jobOperate.CanAbortJob)
            {
                AbortJob();
            }
        }

        private void m_MenuItemReset_Click(object sender, System.EventArgs e)
        {
            JobItemOperate jobOperate = GetJobItemOperate();

            if (jobOperate.CanResetJob)
            {
                ResetJob();
            }
        }

        private void ThumbnailStripButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ThumbnailStripButton.Checked)
            {
                this.m_JobListView.View = View.LargeIcon;
                this.m_JobListView.Alignment = ListViewAlignment.Left;
            }
            else
            {
                this.m_JobListView.View = View.Details;
                this.m_JobListView.Alignment = ListViewAlignment.Top;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in this.m_JobListView.Items)
            {
                lvi.Selected = true;
            }
        }
        private bool isMouseLeftButton = true;
        private void m_JobListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_LastPrinterStatus != JetStatusEnum.Busy)
                return;
            if (e.Button == MouseButtons.Left)
                isMouseLeftButton = true;
            else
                isMouseLeftButton = false;
        }

        private void JobListForm_Load(object sender, EventArgs e)
        {
            this.m_JobListView.ListViewItemSorter = new ListViewColumnSorter();
            this.m_JobListView.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
        }

        public void ScrollSeletedControlIntoView()
        {
			if(this.m_JobListView.SelectedItems.Count > 0)
			{
				int lastSelectedIndex = this.m_JobListView.SelectedItems[this.m_JobListView.SelectedItems.Count - 1].Index;
				this.m_JobListView.EnsureVisible(lastSelectedIndex);
			}
        }

        private void m_ContextMenuJobList_Opening(object sender, CancelEventArgs e)
        {
            UpdateButtonsStates();
        }

        public UIJob PrintingJob
        {
            get { return this.m_PrintJobTask.GetWorkingJob(); }
        }

        private void m_MenuItemBPCP_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            JobItemOperate jobOperate = this.GetJobItemOperate();

            if (jobOperate.CanPrintJob)
            {
                if (e.ClickedItem == this.m_MenuItemNextBand)
                {
                    this.PrintJob(PrintMode.BreakPoint_NextBand);
                }
                if (e.ClickedItem == this.m_MenuItemCurBand)
                {
                    this.PrintJob(PrintMode.BreakPoint_CurBand);
                }
            }
        }
				public bool IsFristCopiesOrNoJobPrint()
		{
			UIJob job = this.m_PrintJobTask.GetWorkingJob();
			if(job==null  // 有喷嘴检查/校准打印等情况
				|| (job!=null && this.m_PrintJobTask.PrintingPage < 2))
				return true;
			else
				return false;
		}
    }
}
