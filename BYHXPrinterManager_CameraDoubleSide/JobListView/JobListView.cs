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
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;

using System.Xml;
using System.Timers;
using System.Resources;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using BYHXPrinterManager.Main;
using BYHXPrinterManager.TcpIp;
using Application = System.Windows.Forms.Application;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using FontStyle = System.Drawing.FontStyle;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;
using MessageBox = System.Windows.Forms.MessageBox;
using Size = System.Drawing.Size;
using System.Collections.Generic;
using System.Windows.Media;
using BYHXPrinterManager.Setting;
using MultimediaLayout.Models;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for JobListView.
	/// </summary>
	public class JobListForm : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// 选中文件夹对应得预览文件夹
		/// </summary>
		public string PreviewFolderPath = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar;//string.Empty;
		private bool m_bDuringPrinting = false;
	    private int m_nJobIndex = 0;

		const string m_PreviewFolder = "Preview";
		const string m_JobListFile = "Joblist.xml";
		private PrintingInfo m_PreviewAndInfo;
        private PrintJobTask m_PrintJobTask; 
		private PreviewJobTask m_PreviewTask;
        private GenDoublePrintPrtWorker m_DoublePrintTask;
        private IPrinterChange m_iPrinterChange;
		private JetStatusEnum m_LastPrinterStatus = JetStatusEnum.PowerOff;
		private JobItemOperate m_LastJobOperate;
		private UIPreference m_Preference;

		private Color m_CustomRowBackColor = Color.PaleTurquoise;
#if ENABLECLIPTIP		
		private EditJobForm m_editJobForm =null;
#endif

		public System.Windows.Forms.ListView m_JobListView;
		private System.Windows.Forms.ColumnHeader m_ColumnHeaderName;
		private System.Windows.Forms.ColumnHeader m_ColumnHeaderSize;
		private ImageList imageList;
		private PrivewFileManager m_PrivewFileManager=null;
		public GradientControls.Grouper m_SampleGroup=null;

		private System.Windows.Forms.ContextMenu m_ContextMenuJobList;
		private System.Windows.Forms.MenuItem m_MenuItemAdd;
		private System.Windows.Forms.MenuItem m_MenuItemDelete;
		private System.Windows.Forms.MenuItem m_MenuItemEdit;
		private System.Windows.Forms.MenuItem m_MenuItemPrint;
		private System.Windows.Forms.MenuItem m_MenuItemAbort;
        private System.Windows.Forms.MenuItem menuItemLLPrint;
		private System.ComponentModel.IContainer components;
		/// <summary> 
		/// Required designer variable.
		public event EventHandler SelectedIndexChanged;
        private MenuItem m_Doubleprintfile;
		private BackgroundWorker tcpipListener;
        private MenuItem menuItem1;
        private MenuItem menuItemInkCounter;
        private MenuItem menuItemMultiLayout;
	    private WaitingCreatDoubleSideFile waitingCreatDoubleSideFileForm;
		public JobListForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            m_PrintJobTask = new PrintJobTask(new CallbackWorkingJobFinished(OnJobStatusChanged), this.imageList.ImageSize, m_JobListView);
			m_PreviewTask = new PreviewJobTask(new CallbackWorkingJobFinished(OnPreviewChanged), this.imageList.ImageSize, m_JobListView);
            m_DoublePrintTask = new GenDoublePrintPrtWorker(new CallbackWorkingJobFinished(OnDoublePrintFileCreated), this.imageList.ImageSize, this.m_JobListView);
            m_DoublePrintTask.allWorkFinished += new EventHandler(m_DoublePrintTask_allWorkFinished);
            if (PubFunc.IsInDesignMode())
				return;	
			m_PrivewFileManager = new PrivewFileManager();
			this.m_folderName = this.m_PrivewFileManager.GetTheLastForderName();

			tcpipListener = new BackgroundWorker();
			tcpipListener.DoWork +=new DoWorkEventHandler(tcpipListener_DoWork);
			tcpipListener.WorkerReportsProgress = true;
			tcpipListener.WorkerSupportsCancellation = true;

		    this.m_Doubleprintfile.Visible = PubFunc.SupportDoubleSidePrint;
		}

        public List<UIJob> UIJobList
        {
            get
            {
                List<UIJob> jobList = new List<UIJob>();
                foreach (ListViewItem oneItem in m_JobListView.Items)
                {
                    if (oneItem.Tag == null)
                        continue;
                    UIJob job = (UIJob)oneItem.Tag;
                    jobList.Add(job);
                }
                return jobList;
            }
        }

	    /// <summary>
	    /// 是否处于层间暂停期间
	    /// </summary>
	    public bool WaitingPauseBetweenLayers
	    {
	        get
	        {
	            return m_PrintJobTask.IsWorking() && m_PrintJobTask.WaitingPauseBetweenLayers;
	        }
	    }

        void m_DoublePrintTask_allWorkFinished(object sender, EventArgs e)
        {
            if (waitingCreatDoubleSideFileForm != null)
            {
                waitingCreatDoubleSideFileForm.Close();
                waitingCreatDoubleSideFileForm = null;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobListForm));
            this.m_JobListView = new System.Windows.Forms.ListView();
            this.m_ColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_ColumnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.m_ContextMenuJobList = new System.Windows.Forms.ContextMenu();
            this.m_MenuItemAdd = new System.Windows.Forms.MenuItem();
            this.m_MenuItemDelete = new System.Windows.Forms.MenuItem();
            this.m_MenuItemEdit = new System.Windows.Forms.MenuItem();
            this.m_MenuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuItemLLPrint = new System.Windows.Forms.MenuItem();
            this.m_MenuItemAbort = new System.Windows.Forms.MenuItem();
            this.m_Doubleprintfile = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemMultiLayout = new System.Windows.Forms.MenuItem();
            this.menuItemInkCounter = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // m_JobListView
            // 
            this.m_JobListView.AllowDrop = true;
            this.m_JobListView.BackColor = System.Drawing.Color.White;
            this.m_JobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColumnHeaderName,
            this.m_ColumnHeaderSize});
            resources.ApplyResources(this.m_JobListView, "m_JobListView");
            this.m_JobListView.ForeColor = System.Drawing.Color.Black;
            this.m_JobListView.FullRowSelect = true;
            this.m_JobListView.GridLines = true;
            this.m_JobListView.HideSelection = false;
            this.m_JobListView.LargeImageList = this.imageList;
            this.m_JobListView.Name = "m_JobListView";
            this.m_JobListView.OwnerDraw = true;
            this.m_JobListView.ShowItemToolTips = true;
            this.m_JobListView.UseCompatibleStateImageBehavior = false;
            this.m_JobListView.View = System.Windows.Forms.View.Details;
            this.m_JobListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.m_JobListView_DrawColumnHeader);
            this.m_JobListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.m_JobListView_DrawItem);
            this.m_JobListView.SelectedIndexChanged += new System.EventHandler(this.m_JobListView_SelectedIndexChanged);
            this.m_JobListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragDrop);
            this.m_JobListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragEnter);
            this.m_JobListView.DoubleClick += new System.EventHandler(this.m_JobListView_DoubleClick);
            this.m_JobListView.Enter += new System.EventHandler(this.m_JobListView_Enter);
            this.m_JobListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_JobListView_KeyDown);
            this.m_JobListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_JobListView_MouseDown);
            // 
            // m_ColumnHeaderName
            // 
            resources.ApplyResources(this.m_ColumnHeaderName, "m_ColumnHeaderName");
            // 
            // m_ColumnHeaderSize
            // 
            resources.ApplyResources(this.m_ColumnHeaderSize, "m_ColumnHeaderSize");
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageList, "imageList");
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // m_ContextMenuJobList
            // 
            this.m_ContextMenuJobList.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemAdd,
            this.m_MenuItemDelete,
            this.m_MenuItemEdit,
            this.m_MenuItemPrint,
            this.menuItemLLPrint,
            this.m_MenuItemAbort,
            this.m_Doubleprintfile,
            this.menuItem1,
            this.menuItemMultiLayout,
            this.menuItemInkCounter});
            this.m_ContextMenuJobList.Popup += new System.EventHandler(this.m_ContextMenuJobList_Popup);
            // 
            // m_MenuItemAdd
            // 
            this.m_MenuItemAdd.Index = 0;
            resources.ApplyResources(this.m_MenuItemAdd, "m_MenuItemAdd");
            this.m_MenuItemAdd.Click += new System.EventHandler(this.m_MenuItemAdd_Click);
            // 
            // m_MenuItemDelete
            // 
            this.m_MenuItemDelete.Index = 1;
            resources.ApplyResources(this.m_MenuItemDelete, "m_MenuItemDelete");
            this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
            // 
            // m_MenuItemEdit
            // 
            this.m_MenuItemEdit.Index = 2;
            resources.ApplyResources(this.m_MenuItemEdit, "m_MenuItemEdit");
            this.m_MenuItemEdit.Click += new System.EventHandler(this.m_MenuItemEdit_Click);
            // 
            // m_MenuItemPrint
            // 
            this.m_MenuItemPrint.Index = 3;
            resources.ApplyResources(this.m_MenuItemPrint, "m_MenuItemPrint");
            this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
            // 
            // menuItemLLPrint
            // 
            this.menuItemLLPrint.Index = 4;
            resources.ApplyResources(this.menuItemLLPrint, "menuItemLLPrint");
            this.menuItemLLPrint.Click += new System.EventHandler(this.menuItemLLPrint_Click);
            // 
            // m_MenuItemAbort
            // 
            this.m_MenuItemAbort.Index = 5;
            resources.ApplyResources(this.m_MenuItemAbort, "m_MenuItemAbort");
            this.m_MenuItemAbort.Click += new System.EventHandler(this.m_MenuItemAbort_Click);
            // 
            // m_Doubleprintfile
            // 
            this.m_Doubleprintfile.Index = 6;
            resources.ApplyResources(this.m_Doubleprintfile, "m_Doubleprintfile");
            this.m_Doubleprintfile.Click += new System.EventHandler(this.m_Doubleprintfile_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 7;
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemMultiLayout
            // 
            this.menuItemMultiLayout.Index = 8;
            resources.ApplyResources(this.menuItemMultiLayout, "menuItemMultiLayout");
            this.menuItemMultiLayout.Click += new System.EventHandler(this.menuItemMultiLayout_Click);
            // 
            // menuItemInkCounter
            // 
            this.menuItemInkCounter.Index = 9;
            resources.ApplyResources(this.menuItemInkCounter, "menuItemInkCounter");
            this.menuItemInkCounter.Click += new System.EventHandler(this.menuItemInkCounter_Click);
            // 
            // JobListForm
            // 
            this.ContextMenu = this.m_ContextMenuJobList;
            this.Controls.Add(this.m_JobListView);
            this.DoubleBuffered = true;
            resources.ApplyResources(this, "$this");
            this.Name = "JobListForm";
            this.Load += new System.EventHandler(this.JobListForm_Load);
            this.ResumeLayout(false);

		}

        private void m_JobListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs drawListViewColumnHeaderEventArgs)
	    {
            drawListViewColumnHeaderEventArgs.DrawDefault = true;
	    }

	    #endregion
		
		public CLockQueue LockQueue
		{
			get
			{
				return m_QueStich;
			}
			set
			{
				m_QueStich = value;
			}
		}
		private CLockQueue m_QueStich = new CLockQueue(); 
		
		public void  SelectAll()
		{
			foreach (ListViewItem lvi in this.m_JobListView.Items)
			{
				lvi.Selected = true;
			}
		}
		public void ReLoadItems()
		{
			if(m_folderName == null || m_folderName == "" || !Directory.Exists(m_folderName))
				return;

			string strFilter = "*.prt;*prn";
			ArrayList fileList =  new ArrayList();
			string[] arExtensions = strFilter.Split(';');

			foreach (string filter in arExtensions)
			{
				string[] strFiles = Directory.GetFiles(m_folderName, filter);
				if(strFiles.Length > 0)
					fileList.AddRange(strFiles);
			}
			fileList.Sort();

			if(fileList.Count > 0)
			{
				int count = m_JobListView.Items.Count;
				for(int i = count -1; i >=0 ; i--)
				{
					JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
					if(((UIJob)jobItem.Tag).Status != JobStatus.Printing)
					{
						m_PrintJobTask.AbortJob((UIJob)jobItem.Tag);
						m_PreviewTask.AbortJob((UIJob)jobItem.Tag);
						m_JobListView.Items.Remove(jobItem);
					}
				}
				this.imageList.Images.Clear();

				if(this.m_iPrinterChange.GetAllParam().Preference.ViewModeIndex != (int)UIViewMode.LeftRight)
					this.PreviewFolderPath = this.m_PrivewFileManager.Add(this.m_folderName);
				foreach (string filter in arExtensions)
				{
					string[] strFiles = Directory.GetFiles(m_folderName, filter);
					if(strFiles.Length > 0)
						this.AddJobs(strFiles);
				}
			}
		}
		private string m_folderName = string.Empty;//Path.GetDirectoryName(Application.ExecutablePath);
		public string FolderName
		{
			get { return m_folderName; }
			set
			{
				if (!Directory.Exists(value))
					return;
				m_folderName = value;
				ReLoadItems();
			}
		}

		public bool GridLines
		{
			get{return this.m_JobListView.GridLines;}
			set{this.m_JobListView.GridLines = value;}
		}

		public Color CustomRowBackColor
		{
			get{return m_CustomRowBackColor;}
			set
			{
				m_CustomRowBackColor = value;
				ListViewHelper.m_CustomRowBackColor = value;
			}
		}
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

	    public int JobIndex
	    {
            get { return m_nJobIndex; }
            set { m_nJobIndex = value; }
	    }

	    public void OnPrinterStatusChanged(JetStatusEnum status)
		{
			if( (status != JetStatusEnum.Error || SErrorCode.IsWarningError(CoreInterface.GetBoardError()) || SErrorCode.IsOnlyPauseError(CoreInterface.GetBoardError())) &&
				status != JetStatusEnum.Cleaning)
			{
				m_LastPrinterStatus = status;
			}
			if(!this.m_PrintJobTask.IsWorking())
			{
				return;
			}

			JobListItem jobItem = GetJobItemByJob(m_PrintJobTask.GetWorkingJob());

			if(jobItem == null)
			{
				return;
			}

			switch(m_LastPrinterStatus)
			{
				case JetStatusEnum.Busy:
				case JetStatusEnum.Pause:
				case JetStatusEnum.Aborting:
				case JetStatusEnum.Error:
				{
					int IndexOfStatus = m_Preference.IndexOf(JobListColumnHeader.Status);
					if(IndexOfStatus>0)
					{
						if(m_LastPrinterStatus == JetStatusEnum.Busy)
							jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JetStatusEnum),m_LastPrinterStatus) + m_PrintJobTask.GetCopiesString();
						else
							jobItem.SubItems[(int)IndexOfStatus].Text = ResString.GetEnumDisplayName(typeof(JetStatusEnum),m_LastPrinterStatus);
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

	    private SPrinterProperty _printerProperty;
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
		    _printerProperty = sp;
			this.menuItemLLPrint.Visible = sp.nWhiteInkNum > 0;
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
		    _printerSetting = ss;
		}
		public void OnPreferenceChange( UIPreference up)
		{
			bool bUnitChange = (m_Preference == null || m_Preference.Unit != up.Unit);
			bool bHeaderChange = CheckHeadIsChange(up);
			m_Preference = up.DeepCopy();
			
			// 
			// HotForlderWatcher
			// 
			HotForlderWatcherPrt = new FileSystemWatcher();
			if(m_Preference.EnableHotForlder && Directory.Exists(m_Preference.HotForlderPath))
			{
				this.HotForlderWatcherPrt.BeginInit();
				this.HotForlderWatcherPrt.Path = m_Preference.HotForlderPath;
				this.HotForlderWatcherPrt.NotifyFilter = NotifyFilters.FileName;
				this.HotForlderWatcherPrt.Filter = "*.prt";
				this.HotForlderWatcherPrt.SynchronizingObject = this;
				this.HotForlderWatcherPrt.Deleted += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Deleted);
				this.HotForlderWatcherPrt.Renamed += new System.IO.RenamedEventHandler(this.HotForlderWatcher_Renamed);
				this.HotForlderWatcherPrt.Created += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Created);
				this.HotForlderWatcherPrt.EndInit();
				this.HotForlderWatcherPrt.EnableRaisingEvents = true;
			}

            // 
            HotForlderWatcherPrn = new FileSystemWatcher();
            if (m_Preference.EnableHotForlder && Directory.Exists(m_Preference.HotForlderPath))
            {
                this.HotForlderWatcherPrn.BeginInit();
                this.HotForlderWatcherPrn.Path = m_Preference.HotForlderPath;
                this.HotForlderWatcherPrn.NotifyFilter = NotifyFilters.FileName;
                this.HotForlderWatcherPrn.Filter = "*.prn";
                this.HotForlderWatcherPrn.SynchronizingObject = this;
                this.HotForlderWatcherPrn.Deleted += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Deleted);
                this.HotForlderWatcherPrn.Renamed += new System.IO.RenamedEventHandler(this.HotForlderWatcher_Renamed);
                this.HotForlderWatcherPrn.Created += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Created);
                this.HotForlderWatcherPrn.EndInit();
                this.HotForlderWatcherPrn.EnableRaisingEvents = true;
            }

			if(bHeaderChange)
			{
				OnChangeHeader(m_Preference);
			}
			if(bUnitChange)
			{
				int IndexOfJobSize = up.IndexOf(JobListColumnHeader.Size);
				for(int i = 0;i < m_JobListView.Items.Count; i++)
				{
					JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
					UIJob jobInfo = (UIJob)jobItem.Tag;
					string sizeInfo = jobItem.GetJobSize(jobInfo.ResolutionX,jobInfo.ResolutionY,jobInfo.Dimension,m_Preference);
					if(IndexOfJobSize > 0)
						jobItem.SubItems[(int)IndexOfJobSize].Text = sizeInfo;
				}
#if ENABLECLIPTIP
				if(m_editJobForm!=null)
				{
					m_editJobForm.OnPreferenceChange(up);
				}
#endif
			}
		}

        public void SetPrinterChange(IPrinterChange ic, IntPtr handle = default(IntPtr), uint kernelMessage = 0)
		{
			m_iPrinterChange = ic;
			if(m_PrintJobTask != null)
                m_PrintJobTask.SetPrinterChange(ic, handle,kernelMessage);
			if(m_PreviewTask != null)
                m_PreviewTask.SetPrinterChange(ic, handle, kernelMessage);
            if (m_DoublePrintTask != null)
                m_DoublePrintTask.SetPrinterChange(this.m_iPrinterChange, handle, kernelMessage);

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
			for (int i=0; i<colLen;i++)
			{
				columnHeader[i] = new ColumnHeader();
				JobListColumnHeader cur = (JobListColumnHeader)options.JobListHeaderList[i];
				string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader),cur);
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
			for(int i = 0;i < m_JobListView.Items.Count; i++)
			{
				JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
				UIJob job = (UIJob)jobItem.Tag;
				if(!File.Exists(job.FileLocation))
				{
					jobItem.ForeColor = Color.LightGray;

					jobItem.Font = new Font(jobItem.Font.FontFamily,jobItem.Font.Size,FontStyle.Italic);
				}
			}
		}

		private void OnChangeHeader(UIPreference up)
		{
            List<UIJob> list = GetJobList();
			m_JobListView.Clear();
			InitListHeader(up);
			InitJobList(list);

		}
		private bool CheckHeadIsChange(UIPreference up)
		{
			if( m_Preference == null ||m_Preference.JobListHeaderList==null||
				m_Preference.JobListHeaderList.Length == 0 || up.JobListHeaderList.Length != m_Preference.JobListHeaderList.Length)
				return true;
			for  (int i=0; i< up.JobListHeaderList.Length;i++)
			{
				if(up.JobListHeaderList[i] != m_Preference.JobListHeaderList[i])
				{
					return true;
				}
			}
			return false;
		}

        private void OnDoublePrintFileCreated(UIJob job)
        {
            //job.Status = JobStatus.Idle;
            job.IsCreatingDoubleSideFile = job.Status == JobStatus.GenDoudleFile;
            if (job.Status == JobStatus.Idle)
            {
                string prePath = PubFunc.GetDoublePrintFileName(job.FileLocation, false);
                string posPath = PubFunc.GetDoublePrintFileName(job.FileLocation, true);
                if (File.Exists(prePath) && File.Exists(posPath))
                {
                    this.AddJob(prePath, false);
                    this.AddJob(posPath, false);
                }
                else
                {
                    MessageBox.Show(ResString.GetResString("FaildGenDoubleSideFile"));
                }
            }
        }
		
        public void OnJobStatusChanged(UIJob job)
		{
			JobStatus jobStatus = job.Status;
			int IndexOfStatus = m_Preference.IndexOf(JobListColumnHeader.Status);
			JobListItem jobItem = GetJobItemByJob(job);

			if(jobItem == null)
			{
				//////This si for history job reprint design
				//				Debug.Assert(false);
				//				if(job == null)
				//				{
				return;
				//				}
				//				m_JobListView.SelectedItems.Clear();
				//				jobItem = new JobListItem(job,m_Preference,false);
				//				jobItem.Selected = true;
				//				m_JobListView.Items.Add(jobItem);
				ListViewHelper.SetItemBackColor(this.m_JobListView);
			}
			jobItem.RefreshDis(job,m_Preference,m_PrintJobTask.GetCopiesString());

            if(jobStatus == JobStatus.Printed)
			{
				if(this.m_Preference.DelJobAfterPrint)
				{
					m_JobListView.Items.Remove(jobItem);
                    if (this.m_Preference.DelFileAfterPrint)
                    {
                        if (File.Exists(job.FileLocation))
                        {
                            File.Delete(job.FileLocation);
                        }
                    }
					ListViewHelper.SetItemBackColor(this.m_JobListView);
				}
                
				UpdateButtonsStates();
				return;
			}
			else
			{
				if(jobItem.Selected)
				{
					UpdateButtonsStates();
				}
			}
		}

		public void OnPreviewChanged(UIJob job)
		{
            //job.PreViewFile			= GeneratePreviewName(job.Name);
            //string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + job.PreViewFile;

            //if(job.IsClipOrTile)
            //{
            //    if(job.Clips.SrcMiniature != null)
            //        job.Clips.SrcMiniature.Save(mPreviewFolder);
            //    string clipf = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(mPreviewFolder) + EditJobForm.CLIPPREVIEWFILEENDDING;
            //    if(job.Miniature != null)
            //        job.Miniature.Save(clipf);
            //}
            //else
            //{
            //    if(job.Miniature != null)
            //        job.Miniature.Save(mPreviewFolder);
            //}

			JobListItem jobItem = GetJobItemByJob(job);

			if(jobItem == null)
			{
				return;
				//////This si for history job reprint design
			}

#if ENABLECLIPTIP
		    Image miniature = null;
            if (File.Exists(job.Miniature))
                miniature = new Bitmap(job.Miniature);
		    Image srcMiniature = null;
            if (File.Exists(job.Clips.SrcMiniature))
                srcMiniature = new Bitmap(job.Clips.SrcMiniature);
            if (this.m_editJobForm != null && jobItem.Tag == m_editJobForm.EditJob && miniature != null && srcMiniature != null)
                m_editJobForm.UpdateClipBox(miniature, srcMiniature);
#endif
			this.SetImageIndex(job,ref jobItem);
            //if (m_JobListView.SelectedItems != null)
            //    m_JobListView.SelectedItems.Clear();
            //jobItem.Selected = true;
		    int itemindex = m_JobListView.Items.IndexOf(jobItem);
            m_JobListView.RedrawItems(itemindex, itemindex,false);
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
//			m_StartTime = DateTime.Now;
			OnSwitchToPrintingPreview();
			m_PreviewAndInfo.OnPrintingStart();
			//This Only Update Printed Pass

			JobListItem jobItem = GetJobItemByJob(m_PrintJobTask.GetWorkingJob());
			if(jobItem != null )
			{
				//UpdatePrintingJobInfo(m_curUnit,jobInfo);
				//SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth,jobInfo.sImageInfo.nImageHeight,jobInfo.sFreSetting.nResolutionX,jobInfo.sFreSetting.nResolutionY);
				//UpdateJobSizeInfo(previewsize);
				
				jobItem.OnPrintStart(m_Preference);
			}
		}
		public void OnPrintingEnd()
		{
			m_bDuringPrinting = false;
			OnSwitchToJobList();
		}

        public void OnPrintingProgressChanged(int percent)
        {
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
	            try
	            {
	                string path = PubFunc.GetFullPreviewPath(job.PreViewFile);
	                if (job.IsClipOrTile)
	                    path = PubFunc.GetFullPreviewPath(job.TilePreViewFile);
	                if (File.Exists(path))
	                {
	                    Image miniature = new Bitmap(path);
	                    m_PreviewAndInfo.OnUpdatePrintingInfo(miniature);
	                }
	            }
	            catch
	            {
	            }
	        }
	    }

	    public List<UIJob> GetJobList()
		{
            List<UIJob> list = new List<UIJob>();
            for (int i = 0; i < m_JobListView.Items.Count; i++)
            {
                ListViewItem oneItem = m_JobListView.Items[i];
				if (oneItem.Tag == null)
					continue;
				list.Add((UIJob)oneItem.Tag);
			}
			return list;
		}
        private void InitJobList(List<UIJob> jobInfoList)
		{
			m_JobListView.Items.Clear();
			
			if(jobInfoList == null)
			{
				return;
			}
			for(int i = 0;i < jobInfoList.Count; i++)
			{
				UIJob jobInfo =jobInfoList[i];
				string file = PubFunc.GetFullPreviewPath(jobInfo.PreViewFile);
				try
				{
					if(File.Exists(file))
					{
                        //FileStream stream = new FileStream(file,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
						if(jobInfo.IsClipOrTile)
						{
                            jobInfo.Clips.SrcMiniature = file;
							string clipf = PubFunc.GetFullPreviewPath(jobInfo.TilePreViewFile);
							if(File.Exists(clipf))
							{
                                //FileStream streamclip = new FileStream(clipf,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                                //jobInfo.Miniature = clipf;
                                //streamclip.Close();
							}
							else
                                jobInfo.Clips.CreateClipsMiniature().Save(jobInfo.Miniature);
                            //Image miniature = new Bitmap(jobInfo.Miniature);
                            //int imageW = Convert.ToInt32(((float)this.imageList.ImageSize.Height / (float)miniature.Height) * miniature.Width);
                            //jobInfo.ThumbnailImage = PubFunc.CreateThumbnailImage(miniature, this.imageList.ImageSize, thumbBorderColor);
                            //miniature.Dispose();
						}
                        //else
						{
                            jobInfo.Miniature = file;
                            //Image miniature = new Bitmap(jobInfo.Miniature);
                            //int imageW = Convert.ToInt32(((float)this.imageList.ImageSize.Height / (float)miniature.Height) * miniature.Width);
                            //jobInfo.ThumbnailImage = PubFunc.CreateThumbnailImage(miniature, this.imageList.ImageSize, thumbBorderColor);
                            //jobInfo.ThumbnailImage = jobInfo.Miniature.GetThumbnailImage(this.imageList.ImageSize.Width, this.imageList.ImageSize.Height, null, IntPtr.Zero);
                            //miniature.Dispose();
                        }
                        //stream.Close();
					}
				}
				catch(Exception){}
				JobListItem jobItem = new JobListItem(jobInfo,m_Preference);

                //if((!jobInfo.IsClipOrTile && jobInfo.PreViewFile != null)||(jobInfo.IsClipOrTile && jobInfo.Clips.SrcMiniature != null))
				{
					this.SetImageIndex(jobInfo, ref jobItem);
					m_JobListView.Items.Add(jobItem);
				}
                //else
                //    AddJob( jobInfo.FileLocation,false);

			}

			if(m_JobListView.Items.Count > 0)
			{
				m_JobListView.Items[0].Selected = true;
			}
			ListViewHelper.SetItemBackColor(this.m_JobListView);
		}

        private void FilterPreviewJobList(List<UIJob> jobList)
		{
			string sPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar ;
			if(!Directory.Exists(sPreviewFolder))
			{
				Directory.CreateDirectory(sPreviewFolder);
			}
			DirectoryInfo di = new DirectoryInfo(sPreviewFolder);
			FileInfo[] fiArr = di.GetFiles();
			foreach(FileInfo f in fiArr)
			{
				try
				{
					bool bFound = false;
					for (int i=0;i< jobList.Count;i++)
					{
						UIJob job = jobList[i];
						if(job.PreViewFile != null && (f.Name.ToUpper() == job.PreViewFile.ToUpper()
							|| f.Name.ToUpper() == job.TilePreViewFile.ToUpper())
							|| (job.IsClipOrTile && f.Name.ToUpper() == Path.GetFileName(job.Clips.NoteImageFileName).ToUpper())
							)
						{
							bFound = true;
							break;
						}
					}
					if(!bFound)
					{
						File.Delete(f.FullName);
					}
				}
				catch(Exception)
				{
					continue;
				}
			}
		}
		public bool SaveJobList()
		{
			string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;
            List<UIJob> jobList = GetJobList();
			//if(jobList.Count == 0) return true;
			FilterPreviewJobList(jobList);
			XmlDocument doc = new XmlDocument();
			FilterJobStatus(jobList);
			bool	success	= true;
			try
			{
				XmlElement root = doc.CreateElement("JobList");
				doc.AppendChild(root);
				string xml = "";
				for (int i=0; i< jobList.Count;i++)
				{
					UIJob job = ((UIJob)jobList[i]).Clone();
					job.Miniature = null;
					job.PrtFileInfo.sImageInfo.nImageData = IntPtr.Zero;
					//xml += PubFunc.SystemConvertToXml((UIJob)jobList[i],jobList[i].GetType());
					xml += job.SystemConvertToXml();
				}
				string hottest = "<HotFolder><FullPath>" + m_Preference.HotForlderPath + "</FullPath>";
				if (Directory.Exists(m_Preference.HotForlderPath))
				{
					string[] files = Directory.GetFiles(m_Preference.HotForlderPath);
					for (int i = 0; i < files.Length; i++)
					{
						if (Path.GetExtension(files[i]).ToLower() == ".prt" || Path.GetExtension(files[i]).ToLower() == ".prn")
							hottest += PubFunc.SystemConvertToXml(files[i], typeof(string));
					}
				}
				hottest += "</HotFolder>";
				root.InnerXml = xml + hottest;
			    string curFile = fileName + ".tmp";
                doc.Save(curFile);
                if (File.Exists(fileName))
                    File.Copy(curFile, fileName, true);
                else
                    File.Move(curFile, fileName);
            }
			catch(Exception e)
			{
				success	= false;

				Debug.Assert(false,e.Message + e.StackTrace);
			}

			return success;
		}

		public bool LoadJobList()
		{
			string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;
            List<UIJob> jobList = new List<UIJob>();
			ArrayList newFileList = new ArrayList();
			if(!File.Exists(fileName))
				return false;

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(fileName);
				XmlElement	root	= (XmlElement)doc.DocumentElement;
				try
				{
					XmlNode XmlJob = root.FirstChild;
					while(XmlJob != null)
					{
						if( XmlJob.Name == typeof(UIJob).Name)
						{
							//UIJob job = (UIJob)PubFunc.SystemConvertFromXml(XmlJob.OuterXml,typeof(UIJob));
							UIJob job = (UIJob)UIJob.SystemConvertFromXml((XmlElement) XmlJob,typeof(UIJob));
							jobList.Add(job);
						}
						else if( XmlJob.Name == "HotFolder")
						{
							if(XmlJob.ChildNodes[0].InnerText== this.m_Preference.HotForlderPath && Directory.Exists(m_Preference.HotForlderPath))
							{
								string[] files =Directory.GetFiles(this.m_Preference.HotForlderPath); 
								for(int i =0;i<files.Length;i++)
								{
									if(Path.GetExtension(files[i]).ToLower() != ".prt" && Path.GetExtension(files[i]).ToLower() != ".prn"  )
										continue;
									bool isnew = true;
									for(int j =1;j<XmlJob.ChildNodes.Count;j++)
									{
										if(files[i] == XmlJob.ChildNodes[j].InnerText)
										{
											isnew = false;
											break;
										}
									}
									if(isnew)
										newFileList.Add(files[i]);
								}
							}
						}
						XmlJob = XmlJob.NextSibling;
					}
				}
				catch(Exception e)
				{
					Debug.Assert(false,e.Message + e.StackTrace);
				}
				FilterJobStatus(jobList);
				InitJobList(jobList);
				for(int i =0;i<newFileList.Count;i++)
					this.AddJob(newFileList[i].ToString(),true);
				return true;
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}
		}

        private void FilterJobStatus(List<UIJob> jobList)
		{
			for (int i=0; i< jobList.Count;i++)
			{
				UIJob job		= jobList[i];
				if(job.Status == JobStatus.Paused ||
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
            if (m_JobListView.SelectedItems.Count > 0)
            {
                int i = 0;
                JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
				UIJob jobInfo = (UIJob) jobItem.Tag;
	
				if(jobInfo != null)
				{
					if( m_PrintJobTask.IsWorkingJob(jobInfo))
					{
						jobOperate.UpdateByStatus(jobInfo.Status,printerStatus);

						if(printerStatus == JetStatusEnum.Error)
						{
							jobOperate.CanAbortJob = m_LastJobOperate.CanAbortJob;
							jobOperate.CanPausePrint = m_LastJobOperate.CanPausePrint;
							jobOperate.CanResumePrint = m_LastJobOperate.CanResumePrint;
						}
					}
					else
					{
						jobOperate.UpdateByStatus(jobInfo.Status,printerStatus);
					}
				}
			}
			return jobOperate;
		}

		public void OpenJob()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.Multiselect = true;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".prn";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Prn);
			fileDialog.InitialDirectory = m_Preference.WorkingFolder;

			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				Cursor.Current = Cursors.WaitCursor;

				m_Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
				m_iPrinterChange.OnPreferenceChange(m_Preference);
				m_JobListView.SelectedItems.Clear();
				
				string[] fileNames = fileDialog.FileNames;

				this.AddJobs(fileNames);

				Cursor.Current = Cursors.Default;
			}
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
			catch(Exception ex)
			{
			    MessageBox.Show(ex.Message);
			    //Debug.Assert(false);
			}
			ListViewHelper.SetItemBackColor(this.m_JobListView);
		}
		private string GeneratePreviewName(string jobName)
		{
			string previewName = Path.GetFileNameWithoutExtension(jobName);
			string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
			if(!Directory.Exists(mPreviewFolder))
			{
				Directory.CreateDirectory(mPreviewFolder);
			}
			string mPreviewFile =  previewName;
			for (int i=0;i<1000;i++)
			{
				mPreviewFile = previewName+"_" + i.ToString("D3") + ".bmp";
				string cur = mPreviewFolder + Path.DirectorySeparatorChar +  mPreviewFile;
				if(!File.Exists(cur))
					return mPreviewFile;
			}
			return "";
		}
		private void AddJob(string fileName,bool bPrint,bool bReversePrint)
		{
			UIJob	job	= new UIJob();
			job.sJobSetting.bReversePrint = bReversePrint;

			job.Name	= Path.GetFileName(fileName);
			job.Status	= JobStatus.Idle;
		    job.JobID = ++m_nJobIndex;
			SPrtFileInfo	jobInfo = new SPrtFileInfo();
			Int32 bret = 0;
			bret = CoreInterface.Printer_GetFileInfo(fileName,ref jobInfo,0);
			if(bret == 1)
			{
				job.PrtFileInfo			= jobInfo;
				job.FileLocation		= fileName;
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
				info += ":"+ fileName;
				Cursor.Current = Cursors.Default;
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
				Cursor.Current = Cursors.WaitCursor;
				return;
			}

			JobListItem jobItem = new JobListItem(job,m_Preference);
			this.m_JobListView.BeginUpdate();// ① 2009-11-11 add
			m_JobListView.Items.Add(jobItem);//②
			this.m_JobListView.EndUpdate();// ③ 2009-11-11 add
			/* ① .netframework1.1设置Application.EnableVisualStyles()后会导致②处抛出ArgumentOutOfRange异常。
			需要在②前后加入①、③处代码*/
			//			jobItem.Selected = true;
			if(bPrint)
			{
#if false
                //Tony : PrintJob(5)=>Moving(Cleaning) 导致无法添加作业打印
                //如果警告客户也是应该自动弹出， 故障恢复自动消失的错误， 不需要客户干预。

			    bool bCanPrint = JobItemOperate.IsPrinterCanPrint();
			    while (!bCanPrint)
			    {
                    string title = ResString.GetProductName();
                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.CannotPrintStaus);
                    string statusName = ResString.GetEnumDisplayName(typeof(JetStatusEnum), m_LastPrinterStatus);
                    info = string.Format(info, statusName);
                    DialogResult ret = MessageBox.Show(info, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                    LogWriter.WriteLog(new string[] { string.Format("CreatFinished adding job={0}", info) }, true);
                    if (ret == DialogResult.Retry)
                    {
                        Thread.Sleep(500);
                        bCanPrint = JobItemOperate.IsPrinterCanPrint();
                    }
                    else
                    {
                        break;
                    }
			    }
                if (bCanPrint)
#endif
                {
                    LogWriter.WriteLog(new string[] { string.Format("CreatFinished enter Printing job={0}", fileName) }, true);
                    PrintJob(job);
				}
			}

			/////////////////////////////////////
			///Preview
			m_PreviewTask.AddJob(job);
			if(!m_PreviewTask.IsWorking())
			{
				m_PreviewTask.SetWorking(true);
				ThreadStart	threadStart	= new ThreadStart(m_PreviewTask.WorkingThreadProc);
				Thread mPrintThread = new Thread(threadStart);
				mPrintThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
				mPrintThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
				mPrintThread.IsBackground = true;
				m_PreviewTask.SetWorker(mPrintThread);
				mPrintThread.Start();
			}
			//this.SetImageIndex(job,ref jobItem);
		}

		private void AddJob(string fileName,bool bPrint)
		{
			this.AddJob(fileName,bPrint,this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.bReversePrint);
		}

		private void SetImageIndex(UIJob job, ref JobListItem jobItem)
		{
            //if (job.ThumbnailImage != null)
            //{
            //    if(jobItem.ImageIndex != -1)
            //    {
            //        this.imageList.Images[jobItem.ImageIndex] = job.ThumbnailImage;
            //    }
            //    else
            //    {
            //        this.imageList.Images.Add(job.ThumbnailImage);
            //        jobItem.ImageIndex = this.imageList.Images.Count - 1;
            //    }
            //}
		}

		public void DeleteJob()
		{
			if(m_JobListView.SelectedItems.Count == 0)
			{
				return;
			}

			string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.Delete);

			if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
			{
				int index = 0;
				int count = m_JobListView.SelectedItems.Count;
				for(int i = count -1; i >=0 ; i--)
				{
					JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
					if(((UIJob)jobItem.Tag).Status != JobStatus.Printing)
					{
						int delIndex = jobItem.Index;
						m_PrintJobTask.AbortJob((UIJob)jobItem.Tag);
						m_PreviewTask.AbortJob((UIJob)jobItem.Tag);
						m_JobListView.Items.Remove(jobItem);

						if(jobItem.ImageIndex != -1)
						{
							this.imageList.Images.RemoveAt(jobItem.ImageIndex);
							for(int j = delIndex;j<this.m_JobListView.Items.Count;j++)
							{
								this.m_JobListView.Items[j].ImageIndex = this.m_JobListView.Items[j].Index;
							}
						}
					}
				}

				if(m_JobListView.Items.Count > 0 && m_JobListView.SelectedItems.Count <= 0)
				{
					if(index > m_JobListView.Items.Count - 1)
					{
						index = m_JobListView.Items.Count - 1;
					}
					m_JobListView.Items[index].Selected = true;
				}
				ListViewHelper.SetItemBackColor(this.m_JobListView);
			}
            DeleteJob_After();
		}

        /// <summary>
        /// 作业删除后处理
        /// </summary>
        private void DeleteJob_After()
        {
            m_JobListView.Refresh();
            OnSwitchToJobList();
            UpdateButtonsStates();
        }

		public void EditJob()
		{
			if(m_JobListView.SelectedItems.Count == 0)
			{
				return;
			}

			int copies = 1;
			
			if ((UIViewMode)m_iPrinterChange.GetAllParam().Preference.ViewModeIndex != UIViewMode.NotifyIcon)
			{
#if ENABLECLIPTIP
				JobListItem jobItem1 = null; 
				if(m_JobListView.SelectedItems.Count >= 1)
				{
					jobItem1 = (JobListItem)m_JobListView.SelectedItems[0];
					copies = ((UIJob)(jobItem1.Tag)).Copies;
				}
			    AllParam allParam = this.m_iPrinterChange.GetAllParam();
				m_editJobForm = new EditJobForm();
                m_editJobForm.SetPrinterChange(this.m_iPrinterChange);
                // OnPrinterSettingChange需在设置m_editJobForm.EditJob前被调用
                m_editJobForm.OnPrinterSettingChange(allParam.PrinterSetting);
                m_editJobForm.OnPreferenceChange(allParam.Preference);
				m_editJobForm.Copies = copies;
				m_editJobForm.EditJob = ((UIJob)(jobItem1.Tag)).Clone();
				m_editJobForm.SetGroupBoxStyle(this.m_SampleGroup);

				if(m_editJobForm.ShowDialog(this) == DialogResult.OK)
				{
					copies = m_editJobForm.Copies;

					for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
					{
						JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
					    UIJob editedJob = m_editJobForm.EditJob;
                        jobItem.Tag = editedJob;
                        editedJob.Copies = copies;
                        jobItem.RefreshSize(allParam.Preference);
                        //Image miniature = null;

                        //string path = "";
                        //if (editedJob.IsClipOrTile)
                        //    path = PubFunc.GetFullPreviewPath(editedJob.TilePreViewFile);
                        //else
                        //    path = PubFunc.GetFullPreviewPath(editedJob.PreViewFile);
                        //miniature = new Bitmap(path);

                        //editedJob.ThumbnailImage = PubFunc.CreateThumbnailImage(miniature, this.m_JobListView.LargeImageList.ImageSize, this.thumbBorderColor);
                        //miniature.Dispose();

                        jobItem.RefreshDis(editedJob, m_Preference, editedJob.Copies.ToString());
                        m_PreviewAndInfo.UpdateUIJobPreview(editedJob);
						if(jobItem != null)
                            this.SetImageIndex(editedJob, ref jobItem);
					}
				}
				m_editJobForm = null;
#else
				SJobSetting_UI sJobSetting = new SJobSetting_UI();
				if(m_JobListView.SelectedItems.Count == 1)
				{
					JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];

					copies = ((UIJob)(jobItem.Tag)).Copies;
					sJobSetting = ((UIJob)(jobItem.Tag)).sJobSetting;
				}
				JobEditForm editJob = new JobEditForm();
				editJob.SetPrinterChange(this.m_iPrinterChange);
				editJob.Copies = copies;
				editJob.JobSetting = sJobSetting;
				if(editJob.ShowDialog(this) == DialogResult.OK)
				{
					copies = editJob.Copies;
					sJobSetting = editJob.JobSetting;
					for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
					{
						JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
						((UIJob)(jobItem.Tag)).Copies = copies;
						((UIJob)(jobItem.Tag)).sJobSetting = sJobSetting;
						int copyIndex = m_Preference.IndexOf(JobListColumnHeader.Copies);
						if(copyIndex >0)
							jobItem.SubItems[ copyIndex].Text = copies.ToString();
					}
				}
#endif
			}
			else
			{
			    if (m_JobListView.SelectedItems.Count >= 1)
			    {
			        JobListItem jobItem = (JobListItem) m_JobListView.SelectedItems[0];

					copies = ((UIJob)(jobItem.Tag)).Copies;
				}

				JobEditForm editJob = new JobEditForm();

				editJob.Copies = copies;
			
				if(editJob.ShowDialog(this) == DialogResult.OK)
				{
					copies = editJob.Copies;

					for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
					{
						JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];

						((UIJob)(jobItem.Tag)).Copies = copies;
						int copyIndex = m_Preference.IndexOf(JobListColumnHeader.Copies);
						if(copyIndex >0)
							jobItem.SubItems[ copyIndex].Text = copies.ToString();
					}
				}
			}
            //GC.Collect();
        }
		private float GetInkStripeWidth(SColorBarSetting iss)
		{
			float inkSW = iss.fStripeWidth +
				iss.fStripeOffset;
			switch(iss.eStripePosition)
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

        private void PrintJob(bool blevellingPrint)
		{
            //if(!JobItemOperate.IsPrinterCanPrint())
            //{
            //    return;
            //}
			if(!m_bDuringPrinting)
			{
				/* 手动输入数值后直接点击打印时,输入的参数不能再本次打印立即应用.所以打印操作前应用下设置
			 * (原因:焦点迁移事件在按钮点击事件后触发).
			 * */
				m_iPrinterChange.NotifyUIParamChanged();
				// Synchronous PrinterSetting before print job.
			    AllParam allParam = this.m_iPrinterChange.GetAllParam();
				allParam.PrinterProperty.SynchronousCalibrationSettings(ref this.m_iPrinterChange.GetAllParam().PrinterSetting);
                LogWriter.LogSetPrinterSetting(allParam.PrinterSetting, m_bDuringPrinting, "SynchronousCalibrationSettings");
			}
			string lostFiles = null;

			JobListItem[] printableItems = new JobListItem[m_JobListView.SelectedItems.Count];
			JobListItem[] notPrintableItems = new JobListItem[m_JobListView.SelectedItems.Count];
			JobListItem[] langIdErrorItems = new JobListItem[m_JobListView.SelectedItems.Count];

			int printableCount = 0;
			int notPrintableCount = 0;

			for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
			{
				JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];

				UIJob jobInfo = (UIJob)jobItem.Tag;
				jobInfo.sJobSetting.bLevelLingPrint = blevellingPrint;
				if(!File.Exists(jobInfo.FileLocation))
				{
					if(lostFiles == null)
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
				if(false)
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

			if(printableCount  >0)
			{
				for(int i = 0;i < printableCount; i++)
				{
					PrintJob((UIJob)printableItems[i].Tag);
				}
			}

			if(notPrintableCount > 0)
			{
				string itemList = "";

				itemList += "\n    " + notPrintableItems[0].Text;

				for(int i = 1; i < notPrintableCount; i ++)
				{
					itemList += ",\n    " + notPrintableItems[i].Text;
				}
				
				string notPrintableMsg = SErrorCode.GetEnumDisplayName(typeof(Software),Software.MediaTooSmall) + "{0}"
					+ "\n" + ResString.GetPrintJobAnyway();

				notPrintableMsg = string.Format(notPrintableMsg,itemList);
				
				if(MessageBox.Show(notPrintableMsg,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					for(int i = 0;i < notPrintableCount; i++)
					{
						PrintJob((UIJob)notPrintableItems[i].Tag);
					}
				}
			}
			if(lostFiles != null)
			{
				lostFiles += ".\n";

				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.FileNotExist);

				info += "\n" + lostFiles;

				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}
		}

        /// <summary>
        /// 彩神直喷机,打印前提前开启印后加热装置,并人工确认温度ok后开始打印
        /// </summary>
        /// <returns></returns>
	    public bool CheckMediaHeaterTemp()
	    {
            if (SPrinterProperty.IsFloraBeltTEXTILE())
            {
                //提前开启加热,关闭仍由fw控制
                byte[] buf = new byte[]{1};
                uint buflen = (uint) buf.Length;
                CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref buflen, 0, 0xA2);
                // 提示客户确认温度是否加热到位
                string msg = ResString.GetResString("PrintTemperatureWarning");//Whether print temperature reaches the set value. 
                DialogResult dr = MessageBox.Show(msg, ResString.GetProductName(), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    buf = new byte[] { 0 };
                    buflen = (uint)buf.Length; 
                    CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref buflen, 0, 0xA2);
                    return false;
                }
            }
	        return true;
	    }

		public void PrintJob()
		{
            if (AutoInkTestHelper.Para.Enable)
            {
                DialogResult dr =MessageBox.Show("自动墨水测试功能开启状态,确认开始打印嘛?","",MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                    return;
            }
		    AllParam allParam = m_iPrinterChange.GetAllParam();
            if (allParam.PrinterProperty.IsZMeasurSupport
    && allParam.Preference.bShowMeasureFormBeforPrint)
            {
              DialogResult dr =  this.ShowMeasureQuestionForm(true, true);
                if (dr != DialogResult.OK)
                {
                    return;
                }
            }
		    if (!CheckMediaHeaterTemp())
		    {
		        return;
		    }
            this.PrintJob(false);
		}
		private void PrintJob(UIJob job)
		{
			if(job.Status != JobStatus.Idle && job.Status != JobStatus.Printed)
				return;
		    UpdateJobPlateSetting(job);
            LogWriter.WriteLog(new string[] { string.Format("CreatFinished start Printing job={0}", job.FileLocation) }, true);
            m_PrintJobTask.AddJob(job);
			job.Status = JobStatus.Waiting;
			OnJobStatusChanged(job);
			if(!m_PrintJobTask.IsWorking())
			{
				m_PrintJobTask.SetWorking(true);
				ThreadStart	threadStart	= new ThreadStart(m_PrintJobTask.WorkingThreadProc);
				Thread mPrintThread = new Thread(threadStart);
				mPrintThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
				mPrintThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
				mPrintThread.IsBackground = true;
				m_PrintJobTask.SetWorker(mPrintThread);
				mPrintThread.Start();
			}
		}

        private byte _curScanningAxis;
        /// <summary>
        /// 根据当前设置,配置job平台选择属性
        /// </summary>
        /// <param name="job"></param>
        private void UpdateJobPlateSetting(UIJob job)
	    {
            AllParam allParam = this.m_iPrinterChange.GetAllParam();
            SJobSetting_UI sjobseting = job.sJobSetting;
            // 非A非B则认为是自动模式
            if (allParam.Preference.ScanningAxis != CoreConst.AXIS_X && allParam.Preference.ScanningAxis != CoreConst.AXIS_4)
            {
                if (_curScanningAxis == 0)
                {
                    sjobseting.scanningAxis = CoreConst.AXIS_X;
                }
                else
                {
                    if (_curScanningAxis == CoreConst.AXIS_X)
                        sjobseting.scanningAxis = CoreConst.AXIS_4;
                    else
                        sjobseting.scanningAxis = CoreConst.AXIS_X;
                }
                _curScanningAxis = sjobseting.scanningAxis;
            }
            else
            {
                sjobseting.scanningAxis = allParam.Preference.ScanningAxis;
                _curScanningAxis = sjobseting.scanningAxis;
            }
            job.sJobSetting = sjobseting;
	    }

	    public void AbortJob()
		{
			for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
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
			m_PrintJobTask.Abort();
            CoreInterface.Printer_Abort();
        }

		private void ResetJob()
		{
			for(int i = 0; i < m_JobListView.SelectedItems.Count; i++)
			{
				JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];

				((UIJob)(jobItem.Tag)).Status = JobStatus.Idle;
			}
		}

		public void TerminatePrintingJob(bool bTerm)
		{
            if (m_PrintJobTask.IsWorking())
				m_PrintJobTask.Terminate(bTerm);
			if(m_PreviewTask.IsWorking())
				m_PreviewTask.Terminate(bTerm);
            CoreInterface.Printer_Abort();
        }

        public void AbortAllPrintingJob(bool bTerm)
        {
            if (m_PrintJobTask.IsWorking())
                m_PrintJobTask.Terminate(bTerm);
            CoreInterface.Printer_Abort();
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

				if(jobItem.Tag == job)
				{
					return jobItem;
				}
			}

			return null;
		}

	    public UIJob GetJobByPath(string jobPath)
        {
            for (int i = 0; i < m_JobListView.Items.Count; i++)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
                UIJob job = (UIJob)jobItem.Tag;
                if (jobPath.ToLower().Trim() == job.FileLocation.ToLower().Trim())
                {
                    return job;
                }
            }

            return null;
        }
        /// <summary>
        /// 弹出取消确认提示框
        /// </summary>
        /// <param name="mLastOperate"></param>
        /// <returns>1:取消当前打印作业;2:取消所有作业</returns>
		public CanleType Confirm_Exit(PrinterOperate mLastOperate)
		{
			bool bBusy = false;
            AllParam allParam =  m_iPrinterChange.GetAllParam();
            UIPreference preference = allParam.Preference;
            if (preference.DefaultCanleType != CanleType.AlwaysQuestion)
            {
                return preference.DefaultCanleType;
            }
			for(int i = 0;i < m_JobListView.Items.Count; i++)
			{
				JobListItem jobItem = (JobListItem)m_JobListView.Items[i];

				UIJob jobInfo = (UIJob)jobItem.Tag;
				
				switch(jobInfo.Status)
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

            if (bBusy || mLastOperate.CanAbort)
			{
				string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.AbortPrinter);
                MyMessageBox msg = new MyMessageBox(null,true);
                DialogResult dr = msg.ShowAbortPrinting(info, ResString.GetProductName());
                if (dr == DialogResult.OK)
                {
                    if (msg.RememberMyChoose)
                    {
                        preference.DefaultCanleType = CanleType.PrintingJob;
                    }
                    //取消当前打印作业
                    return CanleType.PrintingJob;
                }
                else if (dr == DialogResult.Yes) 
                {
                    //取消所有作业
                    if (msg.RememberMyChoose)
                    {
                        preference.DefaultCanleType = CanleType.All;
                    }
                    return CanleType.All;
                }
                else if (dr == DialogResult.Cancel) 
                {
                    return CanleType.None;
                }
			}
            return CanleType.None;
		}




		private void UpdateButtonsStates()
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			m_LastJobOperate = jobOperate; 

			m_MenuItemAdd.Enabled = jobOperate.CanOpenJob;
			menuItemLLPrint.Enabled = m_MenuItemPrint.Enabled = jobOperate.CanPrintJob;
			m_MenuItemDelete.Enabled = jobOperate.CanDeleteJob;

		}

		private void OnSwitchToJobList()
		{
			m_PreviewAndInfo.SetPrintingPreview(false);
			//No Item in job List
			if(m_JobListView.Items.Count == 0)
			{
				m_PreviewAndInfo.UpdateUIJobPreview(null);
			}
			else if(m_JobListView.SelectedItems.Count == 0)
			{
				//Set Select
				m_JobListView.Items[0].Selected = true;
				JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
				UIJob jobInfo	= (UIJob)jobItem.Tag;;
				Debug.Assert(jobInfo != null);

				m_PreviewAndInfo.UpdateUIJobPreview(jobInfo);
			}
			else
			{
				JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
				UIJob jobInfo	= (UIJob)jobItem.Tag;;
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
			if(m_JobListView.SelectedItems.Count == 0 || !isMouseLeftButton) return;
			m_PreviewAndInfo.SetPrintingPreview(false);
			JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
			UIJob jobInfo	= (UIJob)jobItem.Tag;;
			Debug.Assert(jobInfo != null);
			if(!this.m_PreviewTask.IsWorkingJob(jobInfo))
				m_PreviewAndInfo.UpdateUIJobPreview(jobInfo);
			
			if (this.SelectedIndexChanged != null)
				this.SelectedIndexChanged(sender,e);
		}

		private void m_JobListView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
			if(e.KeyData != Keys.Delete)
			{
				return;
			}

			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanDeleteJob)
			{
				DeleteJob();
			}		
		}

		private void m_JobListView_DoubleClick(object sender, System.EventArgs e)
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanEditJob)
			{
				EditJob();			
			}
	
		}

		private void m_JobListView_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			string [] fileItems = null;
			if(e.Data.GetDataPresent("FileDrop"))
			{
				fileItems = (string [])e.Data.GetData("FileDrop");
			}	
			else if(e.Data.GetDataPresent(typeof(System.String[])))
			{
				fileItems = (string [])e.Data.GetData(typeof(System.String[]));
			}

			if(fileItems == null || fileItems.Length == 0)
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
		private ArrayList GetAllFiles(string [] fileItems)
		{
			ArrayList fileList = new ArrayList();

			for(int i = 0;i < fileItems.Length; i++)
			{
				string extName = Path.GetExtension(fileItems[i]).ToLower();

				if(extName == ".prn" || extName == ".prt")
				{
					if(File.Exists(fileItems[i]))
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

			m_MenuItemPrint.Enabled = jobOperate.CanPrintJob && !BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG;
			m_MenuItemAbort.Enabled = jobOperate.CanAbortJob|jobOperate.CanAbortPrint;
	
		}
		private void m_MenuItemAdd_Click(object sender, System.EventArgs e)
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanOpenJob)
			{
				OpenJob();
			}
	
		}
		private void m_ButtonDelete_Click()
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanDeleteJob)
			{
				DeleteJob();
			}
		}

		private void m_ButtonEdit_Click()
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanEditJob)
			{
				EditJob();
			}
		}

		public void m_ButtonPrint_Click()
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanPrintJob)
			{
				PrintJob();
			}
		}
	
		private void m_ButtonReset_Click()
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanResetJob)
			{
				ResetJob();
			}
		}


		private void m_MenuItemDelete_Click(object sender, System.EventArgs e)
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanDeleteJob)
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

			if(jobOperate.CanPrintJob)
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

			if(jobOperate.CanAbortJob)
			{
				AbortJob();
			}
		}

		private void m_MenuItemReset_Click(object sender, System.EventArgs e)
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanResetJob)
			{
				ResetJob();
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
			if(!PubFunc.IsInDesignMode())
                this.tcpipListener.RunWorkerAsync();
		}

		public void ScrollSeletedControlIntoView()
		{
			if(this.m_JobListView.SelectedItems.Count > 0)
			{
				int lastSelectedIndex = this.m_JobListView.SelectedItems[this.m_JobListView.SelectedItems.Count - 1].Index;
				this.m_JobListView.EnsureVisible(lastSelectedIndex);
			}
		}
		
		#region HotForlderWatcher
        private System.IO.FileSystemWatcher HotForlderWatcherPrt;
        private System.IO.FileSystemWatcher HotForlderWatcherPrn;
		private ArrayList EdittingJobList = new ArrayList();
		private Thread HotForlderThread = null;
	    private SPrinterSetting _printerSetting;

	    private void HotForlderWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
		{	
			if(!this.m_Preference.EnableHotForlder)
				return;

			Debug.WriteLine("File: " +  e.FullPath + " " + e.ChangeType);
			if(EdittingJobList.Contains(e.FullPath))
			{
				EdittingJobList.Remove(e.FullPath);
				this.AddJob(e.FullPath,true);
			}
		}

		private void HotForlderWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
		{	
			if(!this.m_Preference.EnableHotForlder)
				return;
            if (e.ChangeType != WatcherChangeTypes.Created)
                return;

			Debug.WriteLine("File: " +  e.FullPath + " Created");

			if(!EdittingJobList.Contains(e.FullPath))
			{
                LogWriter.WriteLog(new string[]{string.Format("HotForlderWatcher_Created FullPath={0}",e.FullPath)},true );
				EdittingJobList.Add(e.FullPath);
                string loginfo = "HotForlderWatcher_Created [";
                for (int i = 0; i < EdittingJobList.Count; i++)
                {
                    loginfo += EdittingJobList[i].ToString()+";";
                }
			    loginfo += "]";
                Debug.WriteLine(loginfo);
                LogWriter.WriteLog(new string[]{loginfo},true );

				if(HotForlderThread == null || !HotForlderThread.IsAlive)
				{
					ThreadStart	threadStart	= new ThreadStart(this.WaitForCreatFinished);
					HotForlderThread = new Thread(threadStart);
					HotForlderThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
					HotForlderThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
					HotForlderThread.IsBackground = true;
					HotForlderThread.Start();
				}
			}	
		}
        public delegate void AddJobHandler(string path,bool bprint);
		private void WaitForCreatFinished()
		{
			while(EdittingJobList.Count > 0)
			{
                int i = 0;
#if !GZ_CARTON  // 纸箱机严格按照文件创建的先后顺序打印
				for(; i< EdittingJobList.Count;)
#endif
				{
					try
					{
                        // 如果文件正在写入中,独占方式打开会引发异常
					    string jobpath = EdittingJobList[i].ToString();
                        FileStream fs = File.Open(jobpath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
					    fs.Close();

                        LogWriter.WriteLog(new string[] { string.Format("CreatFinished enter addjob={0}", jobpath) }, true);
					    if (this.InvokeRequired)
                            this.Invoke(new AddJobHandler(this.AddJob), new object[] { jobpath, true });
                        else
                            this.AddJob(jobpath, true);
					    EdittingJobList.RemoveAt(i);
					}
					catch (IOException ioex)
					{
#if !GZ_CARTON
                        i++;// 没有复制完成的prt跳过,检测下一个
#endif
                    }
					catch (InvalidOperationException ex)
					{
                        MessageBox.Show(ex.Message);
                    }
				}
				Thread.Sleep(1000);
			}
		}

		private void HotForlderWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{
			if(!this.m_Preference.EnableHotForlder)
				return;

			Debug.WriteLine("File: " +  e.FullPath + " Deleted");

			if(EdittingJobList.Contains(e.FullPath))
				EdittingJobList.Remove(e.FullPath);

			int count = m_JobListView.Items.Count;
			for(int i = count -1; i >=0 ; i--)
			{
				UIJob tag = this.m_JobListView.Items[i].Tag as UIJob;
				if(tag.FileLocation == e.FullPath)
				{
					m_PrintJobTask.AbortJob(tag);
					m_PreviewTask.AbortJob(tag);
					m_JobListView.Items.Remove(this.m_JobListView.Items[i]);
				}
			}
		}

		private void HotForlderWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
		{
			if(!this.m_Preference.EnableHotForlder)
				return;

			Debug.WriteLine("File: " +  e.FullPath + " Renamed");

			int count = m_JobListView.Items.Count;

			for(int i = count -1; i >=0 ; i--)
			{
				UIJob tag = this.m_JobListView.Items[i].Tag as UIJob;
				if(tag.FileLocation == e.OldFullPath)
				{
					JobListItem jobItem = this.m_JobListView.Items[i] as JobListItem;
					int IndexOfName = m_Preference.IndexOf(JobListColumnHeader.Name);
					int IndexOfLocation = m_Preference.IndexOf(JobListColumnHeader.Location);
					if(IndexOfName>=0)
					{
						jobItem.SubItems[IndexOfName].Text = e.Name;
						tag.Name = e.Name;
					}
					if(IndexOfLocation>=0)
					{
						jobItem.SubItems[IndexOfLocation].Text = e.FullPath;
						tag.FileLocation = e.FullPath;
					}
				}
			}
		}
		#endregion

		public void StopLoad()
		{
			if(this.m_PreviewTask.IsAlive())
				m_PreviewTask.Abort();
		}

		private void menuItemLLPrint_Click(object sender, System.EventArgs e)
		{
			JobItemOperate jobOperate = GetJobItemOperate();

			if(jobOperate.CanPrintJob)
			{
				PrintJob(true);
			}
		}

		public UIJob PrintingJob
		{
			get{return this.m_PrintJobTask.GetWorkingJob();}
		}

		public bool IsFristCopiesOrNoJobPrint()
		{
			UIJob job = this.m_PrintJobTask.GetWorkingJob();
			if(
                //job == null ||  // 有喷嘴检查/校准打印等情况
				(job!=null && this.m_PrintJobTask.PrintingPage < 2))
				return true;
			else
				return false;
		}

		private void tcpipListener_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				while(true)
				{
					object obj = m_QueStich.GetFromQueue();
					if(obj == null )
					{
						break;
					}
					TcpipCmdPara array = (TcpipCmdPara)obj;
					if(array.CmdType == -1)
					{
						break;
					}
					this.AddJob(array.PrtPath,true,array.ReversePrint);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void m_Doubleprintfile_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_JobListView.SelectedItems.Count; i++)
            {
                UIJob job = (UIJob)m_JobListView.SelectedItems[i].Tag;
                m_DoublePrintTask.AddJob(job);
                job.Status = JobStatus.GenDoudleFile;
                if (!m_DoublePrintTask.IsWorking())
                {
                    m_DoublePrintTask.SetWorking(true);
                    ThreadStart threadStart = new ThreadStart(m_DoublePrintTask.WorkingThreadProc);
                    Thread mPrintThread = new Thread(threadStart);
                    mPrintThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
                    mPrintThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                    mPrintThread.IsBackground = true;
                    m_DoublePrintTask.SetWorker(mPrintThread);
                    mPrintThread.Start();
                }
            }
            waitingCreatDoubleSideFileForm = new WaitingCreatDoubleSideFile(m_DoublePrintTask);
            waitingCreatDoubleSideFileForm.StartPosition = FormStartPosition.CenterScreen;
            waitingCreatDoubleSideFileForm.ShowDialog();

        }

        private void menuItemInkCounter_Click(object sender, EventArgs e)
        {
            if (m_JobListView.SelectedItems.Count <= 0) return;

            JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];
            UIJob jobInfo = (UIJob)jobItem.Tag;
            if (jobInfo != null)
            {
#if true
                InkCounterForm inkCounterForm = new InkCounterForm(jobInfo);
                inkCounterForm.OnPrinterPropertyChange(m_iPrinterChange.GetAllParam().PrinterProperty);
                inkCounterForm.ShowDialog();
#else
                InkCounterWindow inkCounterWin = new InkCounterWindow(new BindingList<UIJob>() { jobInfo }, 0, _printerProperty);
                //inkCounterWin.Owner = (System.Windows.Window)this.ViewCore;
                inkCounterWin.ShowDialog();
#endif
            }
        }

        private void menuItemMultiLayout_Click(object sender, EventArgs e)
        {
            var jobList = this.UIJobList;
             List<MultimediaLayout.Models.Paper> papers = new List<Paper>();
             papers.Add(new MultimediaLayout.Models.Paper(_printerSetting.sBaseSetting.fPaperWidth, _printerSetting.sBaseSetting.fLeftMargin));
            if(_printerSetting.sExtensionSetting.fPaper2Width!=0)
                papers.Add(new MultimediaLayout.Models.Paper(_printerSetting.sExtensionSetting.fPaper2Width, _printerSetting.sExtensionSetting.fPaper2Left));
            if (_printerSetting.sExtensionSetting.fPaper3Width != 0)
                papers.Add(new MultimediaLayout.Models.Paper(_printerSetting.sExtensionSetting.fPaper3Width, _printerSetting.sExtensionSetting.fPaper3Left));
            MultimediaLayout.MainWindow mainWindow = new MultimediaLayout.MainWindow(jobList, papers);
            CultureInfo cultrueInfo = new CultureInfo(this.m_iPrinterChange.GetAllParam().Preference.LangIndex);
            MultimediaLayout.Helper.LocalizationHelper.SetCultures(mainWindow, cultrueInfo.Name);
            if (mainWindow.ShowDialog()==true)
            {
                if (!string.IsNullOrEmpty(mainWindow.PrtFile))
                {
                    AddJob(mainWindow.PrtFile, false);
                }
            }
        }

        Brush brushFore = new SolidBrush(Color.Black);
        Brush brushBack = new SolidBrush(Color.White);
        Brush brushForeSelected = new SolidBrush(Color.White);
        Brush brushBackSelected = new SolidBrush(Color.DodgerBlue);
        private void m_JobListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Debug.WriteLine(string.Format("==========ItemIndex=={0}", e.ItemIndex));
            if (m_JobListView.View == View.Details)
            {
                e.DrawDefault = true;
                return;
            }
            else
            {
                UIJob job = (UIJob) e.Item.Tag;
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(brushBackSelected, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(brushBack, e.Bounds);
                }
                string path = "";
                if (job.IsClipOrTile)
                    path = PubFunc.GetFullPreviewPath(job.TilePreViewFile);
                else
                    path = PubFunc.GetFullPreviewPath(job.PreViewFile);
                Size imageSize = this.m_JobListView.LargeImageList.ImageSize;
                int margin = 4;
                if (File.Exists(PubFunc.GetFullPreviewPath(path)))
                {
                    Image miniature = null;
                    try
                    {
                        miniature = new Bitmap(path);
                        Image image = PubFunc.CreateThumbnailImage(miniature, imageSize, Color.Wheat);
                        miniature.Dispose();
                        int x = e.Bounds.X + (e.Bounds.Width - image.Width) / 2;
                        int y = e.Bounds.Y + margin;
                        e.Graphics.DrawImage(image, new System.Drawing.Point(x, y));
                        image.Dispose();
                    }
                    catch
                    {
                    }
                }

                //e.DrawText(TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.WordBreak);
                int ytxt = e.Bounds.Y + margin + imageSize.Height + margin;
                Rectangle layoutRectangle = new Rectangle(e.Bounds.X, ytxt, e.Bounds.Width, e.Bounds.Height + e.Bounds.Y - ytxt);
                SizeF msize = e.Graphics.MeasureString(e.Item.Text, e.Item.Font, layoutRectangle.Width);
                msize.Width += 5;
                if (msize.Width < layoutRectangle.Width)
                {
                    layoutRectangle = new Rectangle(
                        (int) Math.Round(e.Bounds.X + (layoutRectangle.Width-msize.Width)/2)
                        , ytxt
                        , (int)Math.Round(msize.Width)
                        , layoutRectangle.Height
                        );
                }
                if (e.Item.Selected)
                {
                    e.Graphics.DrawString(e.Item.Text, e.Item.Font, brushForeSelected, layoutRectangle);
                }
                else
                {
                    e.Graphics.DrawString(e.Item.Text, e.Item.Font, brushFore, layoutRectangle);
                }
                e.DrawFocusRectangle();
            }
        }

	    public void DeleteJob(string parameter)
	    {
	        int index = 0;
	        int count = m_JobListView.Items.Count;
	        for (int i = count - 1; i >= 0; i--)
	        {
	            JobListItem jobItem = (JobListItem) m_JobListView.Items[i];
	            if (((UIJob) jobItem.Tag).FileLocation == parameter)
	            {
	                int delIndex = jobItem.Index;
	                m_PrintJobTask.AbortJob((UIJob) jobItem.Tag);
	                m_PreviewTask.AbortJob((UIJob) jobItem.Tag);
	                m_JobListView.Items.Remove(jobItem);

	                if (jobItem.ImageIndex != -1)
	                {
	                    this.imageList.Images.RemoveAt(jobItem.ImageIndex);
	                    for (int j = delIndex; j < this.m_JobListView.Items.Count; j++)
	                    {
	                        this.m_JobListView.Items[j].ImageIndex = this.m_JobListView.Items[j].Index;
	                    }
	                }
	            }
	        }
	        DeleteJob_After();
	    }

	    public void PrintJob(string parameter)
	    {
            int count = m_JobListView.Items.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                JobListItem jobItem = (JobListItem)m_JobListView.Items[i];
                if (((UIJob)jobItem.Tag).FileLocation == parameter)
                {
                    PrintJob((UIJob) jobItem.Tag);
                }
            }
        }

        private MeasureQuestionForm m_MeasureQuestionForm = null;
        public DialogResult ShowMeasureQuestionForm(bool mainformloaded, bool isMeasureBeforePrint = false)
        {
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            AllParam m_allParam = m_iPrinterChange.GetAllParam();
            if (mainformloaded
                && !SPrinterProperty.IsFloraT50OrT180()
                && status != JetStatusEnum.PowerOff
                && m_allParam.PrinterProperty.IsZMeasurSupport
                )// PubFunc.IsVender92())
            {
                if (m_MeasureQuestionForm == null)
                {
                    m_MeasureQuestionForm = new MeasureQuestionForm(this.m_iPrinterChange, isMeasureBeforePrint);
                    m_MeasureQuestionForm.OnPrinterSettingChange(m_allParam.PrinterSetting);
                    m_MeasureQuestionForm.OnPreferenceChange(m_allParam.Preference);
                    m_MeasureQuestionForm.SetPrinterStatusChanged(status);
                    bool bchanged = false;
                   DialogResult dr = m_MeasureQuestionForm.ShowDialog();
                    m_MeasureQuestionForm.OnGetPreference(ref m_allParam.Preference);
                    m_MeasureQuestionForm.OnGetPrinterSetting(ref m_allParam.PrinterSetting, ref bchanged);
                    m_MeasureQuestionForm = null;
                    return dr;
                }
            }
            return DialogResult.None;
        }

	}


}
