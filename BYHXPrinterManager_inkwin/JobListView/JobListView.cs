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

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for JobListView.
	/// </summary>
	public class JobListForm : System.Windows.Forms.UserControl
	{
		// 选中文件夹对应得预览文件夹
		public string m_PreviewForlder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar;//string.Empty;
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
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		/// <summary> 
		/// Required designer variable.
		public event EventHandler SelectedIndexChanged;

		public JobListForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_PrintJobTask = new PrintJobTask(new CallbackWorkingJobFinished(OnJobStatusChanged), this.imageList.ImageSize, m_JobListView);
			m_PreviewTask = new PreviewJobTask(new CallbackWorkingJobFinished(OnPreviewChanged), this.imageList.ImageSize, m_JobListView);
			if(PubFunc.IsInDesignMode())
				return;	
			m_PrivewFileManager = new PrivewFileManager();
			this.m_folderName = this.m_PrivewFileManager.GetTheLastForderName();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(JobListForm));
			this.m_JobListView = new System.Windows.Forms.ListView();
			this.m_ColumnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.m_ColumnHeaderSize = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.m_ContextMenuJobList = new System.Windows.Forms.ContextMenu();
			this.m_MenuItemAdd = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDelete = new System.Windows.Forms.MenuItem();
			this.m_MenuItemEdit = new System.Windows.Forms.MenuItem();
			this.m_MenuItemPrint = new System.Windows.Forms.MenuItem();
			this.menuItemLLPrint = new System.Windows.Forms.MenuItem();
			this.m_MenuItemAbort = new System.Windows.Forms.MenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// m_JobListView
			// 
			this.m_JobListView.AccessibleDescription = resources.GetString("m_JobListView.AccessibleDescription");
			this.m_JobListView.AccessibleName = resources.GetString("m_JobListView.AccessibleName");
			this.m_JobListView.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("m_JobListView.Alignment")));
			this.m_JobListView.AllowDrop = true;
			this.m_JobListView.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_JobListView.Anchor")));
			this.m_JobListView.BackColor = System.Drawing.Color.White;
			this.m_JobListView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_JobListView.BackgroundImage")));
			this.m_JobListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.m_ColumnHeaderName,
																							this.m_ColumnHeaderSize});
			this.m_JobListView.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_JobListView.Dock")));
			this.m_JobListView.Enabled = ((bool)(resources.GetObject("m_JobListView.Enabled")));
			this.m_JobListView.Font = ((System.Drawing.Font)(resources.GetObject("m_JobListView.Font")));
			this.m_JobListView.ForeColor = System.Drawing.Color.Black;
			this.m_JobListView.FullRowSelect = true;
			this.m_JobListView.GridLines = true;
			this.m_JobListView.HideSelection = false;
			this.m_JobListView.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_JobListView.ImeMode")));
			this.m_JobListView.LabelWrap = ((bool)(resources.GetObject("m_JobListView.LabelWrap")));
			this.m_JobListView.LargeImageList = this.imageList;
			this.m_JobListView.Location = ((System.Drawing.Point)(resources.GetObject("m_JobListView.Location")));
			this.m_JobListView.Name = "m_JobListView";
			this.m_JobListView.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_JobListView.RightToLeft")));
			this.m_JobListView.Size = ((System.Drawing.Size)(resources.GetObject("m_JobListView.Size")));
			this.m_JobListView.TabIndex = ((int)(resources.GetObject("m_JobListView.TabIndex")));
			this.m_JobListView.Text = resources.GetString("m_JobListView.Text");
			this.toolTip1.SetToolTip(this.m_JobListView, resources.GetString("m_JobListView.ToolTip"));
			this.m_JobListView.View = System.Windows.Forms.View.Details;
			this.m_JobListView.Visible = ((bool)(resources.GetObject("m_JobListView.Visible")));
			this.m_JobListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_JobListView_KeyDown);
			this.m_JobListView.DoubleClick += new System.EventHandler(this.m_JobListView_DoubleClick);
			this.m_JobListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragDrop);
			this.m_JobListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_JobListView_DragEnter);
			this.m_JobListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_JobListView_MouseMove);
			this.m_JobListView.Enter += new System.EventHandler(this.m_JobListView_Enter);
			this.m_JobListView.SelectedIndexChanged += new System.EventHandler(this.m_JobListView_SelectedIndexChanged);
			// 
			// m_ColumnHeaderName
			// 
			this.m_ColumnHeaderName.Text = resources.GetString("m_ColumnHeaderName.Text");
			this.m_ColumnHeaderName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_ColumnHeaderName.TextAlign")));
			this.m_ColumnHeaderName.Width = ((int)(resources.GetObject("m_ColumnHeaderName.Width")));
			// 
			// m_ColumnHeaderSize
			// 
			this.m_ColumnHeaderSize.Text = resources.GetString("m_ColumnHeaderSize.Text");
			this.m_ColumnHeaderSize.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_ColumnHeaderSize.TextAlign")));
			this.m_ColumnHeaderSize.Width = ((int)(resources.GetObject("m_ColumnHeaderSize.Width")));
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = ((System.Drawing.Size)(resources.GetObject("imageList.ImageSize")));
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
																								 this.m_MenuItemAbort});
			this.m_ContextMenuJobList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ContextMenuJobList.RightToLeft")));
			this.m_ContextMenuJobList.Popup += new System.EventHandler(this.m_ContextMenuJobList_Popup);
			// 
			// m_MenuItemAdd
			// 
			this.m_MenuItemAdd.Enabled = ((bool)(resources.GetObject("m_MenuItemAdd.Enabled")));
			this.m_MenuItemAdd.Index = 0;
			this.m_MenuItemAdd.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemAdd.Shortcut")));
			this.m_MenuItemAdd.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemAdd.ShowShortcut")));
			this.m_MenuItemAdd.Text = resources.GetString("m_MenuItemAdd.Text");
			this.m_MenuItemAdd.Visible = ((bool)(resources.GetObject("m_MenuItemAdd.Visible")));
			this.m_MenuItemAdd.Click += new System.EventHandler(this.m_MenuItemAdd_Click);
			// 
			// m_MenuItemDelete
			// 
			this.m_MenuItemDelete.Enabled = ((bool)(resources.GetObject("m_MenuItemDelete.Enabled")));
			this.m_MenuItemDelete.Index = 1;
			this.m_MenuItemDelete.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemDelete.Shortcut")));
			this.m_MenuItemDelete.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemDelete.ShowShortcut")));
			this.m_MenuItemDelete.Text = resources.GetString("m_MenuItemDelete.Text");
			this.m_MenuItemDelete.Visible = ((bool)(resources.GetObject("m_MenuItemDelete.Visible")));
			this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
			// 
			// m_MenuItemEdit
			// 
			this.m_MenuItemEdit.Enabled = ((bool)(resources.GetObject("m_MenuItemEdit.Enabled")));
			this.m_MenuItemEdit.Index = 2;
			this.m_MenuItemEdit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemEdit.Shortcut")));
			this.m_MenuItemEdit.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemEdit.ShowShortcut")));
			this.m_MenuItemEdit.Text = resources.GetString("m_MenuItemEdit.Text");
			this.m_MenuItemEdit.Visible = ((bool)(resources.GetObject("m_MenuItemEdit.Visible")));
			this.m_MenuItemEdit.Click += new System.EventHandler(this.m_MenuItemEdit_Click);
			// 
			// m_MenuItemPrint
			// 
			this.m_MenuItemPrint.Enabled = ((bool)(resources.GetObject("m_MenuItemPrint.Enabled")));
			this.m_MenuItemPrint.Index = 3;
			this.m_MenuItemPrint.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemPrint.Shortcut")));
			this.m_MenuItemPrint.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemPrint.ShowShortcut")));
			this.m_MenuItemPrint.Text = resources.GetString("m_MenuItemPrint.Text");
			this.m_MenuItemPrint.Visible = ((bool)(resources.GetObject("m_MenuItemPrint.Visible")));
			this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
			// 
			// menuItemLLPrint
			// 
			this.menuItemLLPrint.Enabled = ((bool)(resources.GetObject("menuItemLLPrint.Enabled")));
			this.menuItemLLPrint.Index = 4;
			this.menuItemLLPrint.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemLLPrint.Shortcut")));
			this.menuItemLLPrint.ShowShortcut = ((bool)(resources.GetObject("menuItemLLPrint.ShowShortcut")));
			this.menuItemLLPrint.Text = resources.GetString("menuItemLLPrint.Text");
			this.menuItemLLPrint.Visible = ((bool)(resources.GetObject("menuItemLLPrint.Visible")));
			this.menuItemLLPrint.Click += new System.EventHandler(this.menuItemLLPrint_Click);
			// 
			// m_MenuItemAbort
			// 
			this.m_MenuItemAbort.Enabled = ((bool)(resources.GetObject("m_MenuItemAbort.Enabled")));
			this.m_MenuItemAbort.Index = 5;
			this.m_MenuItemAbort.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemAbort.Shortcut")));
			this.m_MenuItemAbort.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemAbort.ShowShortcut")));
			this.m_MenuItemAbort.Text = resources.GetString("m_MenuItemAbort.Text");
			this.m_MenuItemAbort.Visible = ((bool)(resources.GetObject("m_MenuItemAbort.Visible")));
			this.m_MenuItemAbort.Click += new System.EventHandler(this.m_MenuItemAbort_Click);
			// 
			// JobListForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ContextMenu = this.m_ContextMenuJobList;
			this.Controls.Add(this.m_JobListView);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "JobListForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.Load += new System.EventHandler(this.JobListForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

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
					this.m_PreviewForlder = this.m_PrivewFileManager.Add(this.m_folderName);
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

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			this.menuItemLLPrint.Visible = sp.nWhiteInkNum > 0;
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
		}
		public void OnPreferenceChange( UIPreference up)
		{
			bool bUnitChange = (m_Preference == null || m_Preference.Unit != up.Unit);
			bool bHeaderChange = CheckHeadIsChange(up);
			m_Preference = up.DeepCopy();
			
			// 
			// HotForlderWatcher
			// 
			HotForlderWatcher = new FileSystemWatcher();
			if(m_Preference.EnableHotForlder && Directory.Exists(m_Preference.HotForlderPath))
			{
				this.HotForlderWatcher.BeginInit();
				this.HotForlderWatcher.Path = m_Preference.HotForlderPath;
				this.HotForlderWatcher.NotifyFilter = NotifyFilters.FileName;
				this.HotForlderWatcher.Filter = "*.prt";
				this.HotForlderWatcher.SynchronizingObject = this;
				this.HotForlderWatcher.Deleted += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Deleted);
				this.HotForlderWatcher.Renamed += new System.IO.RenamedEventHandler(this.HotForlderWatcher_Renamed);
				this.HotForlderWatcher.Created += new System.IO.FileSystemEventHandler(this.HotForlderWatcher_Created);
				this.HotForlderWatcher.EndInit();
				this.HotForlderWatcher.EnableRaisingEvents = true;
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
			ArrayList list = GetJobList();
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
			job.PreViewFile			= GeneratePreviewName(job.Name);
			string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + job.PreViewFile;

			if(job.IsClipOrTile)
			{
				if(job.Clips.SrcMiniature != null)
					job.Clips.SrcMiniature.Save(mPreviewFolder);
				string clipf = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(mPreviewFolder) + EditJobForm.CLIPPREVIEWFILEENDDING;
				if(job.Miniature != null)
					job.Miniature.Save(clipf);
			}
			else
			{
				if(job.Miniature != null)
					job.Miniature.Save(mPreviewFolder);
			}

			JobListItem jobItem = GetJobItemByJob(job);

			if(jobItem == null)
			{
				return;
				//////This si for history job reprint design
			}

#if ENABLECLIPTIP
			if(this.m_editJobForm!=null&& jobItem.Tag == m_editJobForm.EditJob)
				m_editJobForm.UpdateClipBox(job.Miniature,job.Clips.SrcMiniature);
#endif
			this.SetImageIndex(job,ref jobItem);
			jobItem.Selected = true;
			//Notify UI changed 
			if(m_JobListView.SelectedItems!= null && m_JobListView.SelectedItems.IndexOf(jobItem) == 0)
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

		private void OnSwitchToPrintingPreview()
		{
			m_PreviewAndInfo.SetPrintingPreview(true);
			UIJob job = m_PrintJobTask.GetWorkingJob();
			if(job == null)
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
			
			if(jobInfoList == null)
			{
				return;
			}
			for(int i = 0;i < jobInfoList.Count; i++)
			{
				UIJob jobInfo = (UIJob)jobInfoList[i];
				string file = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + jobInfo.PreViewFile;
				try
				{
					if(File.Exists(file))
					{
						FileStream stream = new FileStream(file,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
						if(jobInfo.IsClipOrTile)
						{
							jobInfo.Clips.SrcMiniature = new Bitmap(stream);
							string clipf = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file)+EditJobForm.CLIPPREVIEWFILEENDDING;
							if(File.Exists(clipf))
							{
								FileStream streamclip = new FileStream(clipf,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
								jobInfo.Miniature = new Bitmap(streamclip);
								streamclip.Close();
							}
							else
								jobInfo.Miniature = jobInfo.Clips.CreateClipsMiniature();
							int imageW = Convert.ToInt32(((float)this.imageList.ImageSize.Height / (float)jobInfo.Miniature.Height) * jobInfo.Miniature.Width);
							jobInfo.ThumbnailImage = PubFunc.CreateThumbnailImage(jobInfo.Miniature, this.imageList.ImageSize, thumbBorderColor);
						}
						else
						{
							jobInfo.Miniature = new Bitmap(stream);
							int imageW = Convert.ToInt32(((float)this.imageList.ImageSize.Height / (float)jobInfo.Miniature.Height) * jobInfo.Miniature.Width);
							jobInfo.ThumbnailImage = PubFunc.CreateThumbnailImage(jobInfo.Miniature, this.imageList.ImageSize, thumbBorderColor);
							//jobInfo.ThumbnailImage = jobInfo.Miniature.GetThumbnailImage(this.imageList.ImageSize.Width, this.imageList.ImageSize.Height, null, IntPtr.Zero);
						}
						stream.Close();
					}
				}
				catch(Exception){}
				JobListItem jobItem = new JobListItem(jobInfo,m_Preference);

				if((!jobInfo.IsClipOrTile && jobInfo.Miniature != null)||(jobInfo.IsClipOrTile && jobInfo.Clips.SrcMiniature != null))
				{
					this.SetImageIndex(jobInfo, ref jobItem);
					m_JobListView.Items.Add(jobItem);
				}
				else
					AddJob( jobInfo.FileLocation,false);

			}

			if(m_JobListView.Items.Count > 0)
			{
				m_JobListView.Items[0].Selected = true;
			}
			ListViewHelper.SetItemBackColor(this.m_JobListView);
		}

		private void FilterPreviewJobList(ArrayList jobList)
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
						UIJob job = (UIJob)jobList[i];
						if(job.PreViewFile != null && (f.Name.ToUpper() == job.PreViewFile.ToUpper()
							|| f.Name.ToUpper() == (Path.GetFileNameWithoutExtension(job.PreViewFile) + EditJobForm.CLIPPREVIEWFILEENDDING).ToUpper())
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
			ArrayList jobList  = GetJobList();
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
					job.PrtFileInfo.sImageInfo.nImageData = 0;
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
				doc.Save(fileName);

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
			ArrayList jobList = new ArrayList();
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

		private void FilterJobStatus(ArrayList jobList)
		{
			for (int i=0; i< jobList.Count;i++)
			{
				UIJob job		= (UIJob)jobList[i];
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
			
			for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
			{
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
		private void AddJob(string fileName,bool bPrint)
		{
			UIJob	job	= new UIJob();
			job.sJobSetting.bReversePrint = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.bReversePrint;

			job.Name	= Path.GetFileName(fileName);
			job.PreViewFile = m_PreviewForlder + Path.GetFileNameWithoutExtension(fileName) + ".bmp";
			job.Status	= JobStatus.Idle;
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
				if(JobItemOperate.IsPrinterCanPrint())
				{
					PrintJob((UIJob)(jobItem.Tag));
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

		private void SetImageIndex(UIJob job, ref JobListItem jobItem)
		{
			if (job.ThumbnailImage != null)
			{
				this.imageList.Images.Add(job.ThumbnailImage);
				jobItem.ImageIndex = this.imageList.Images.Count - 1;
			}
			//            else
			//            {
			//                jobItem.ImageIndex = 0;
			//            }
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
				if(m_JobListView.SelectedItems.Count == 1)
				{
					jobItem1 = (JobListItem)m_JobListView.SelectedItems[0];
					copies = ((UIJob)(jobItem1.Tag)).Copies;
				}

				m_editJobForm = new EditJobForm();
				m_editJobForm.SetPrinterChange(this.m_iPrinterChange);
				m_editJobForm.OnPreferenceChange(this.m_iPrinterChange.GetAllParam().Preference);
				m_editJobForm.Copies = copies;
				m_editJobForm.EditJob = ((UIJob)(jobItem1.Tag)).Clone();
				m_editJobForm.SetGroupBoxStyle(this.m_SampleGroup);

				if(m_editJobForm.ShowDialog(this) == DialogResult.OK)
				{
					copies = m_editJobForm.Copies;

					for(int i = 0;i < m_JobListView.SelectedItems.Count; i++)
					{
						JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[i];
						jobItem.Tag = m_editJobForm.EditJob;
						m_editJobForm.EditJob.Copies = copies;
						m_editJobForm.EditJob.ThumbnailImage = PubFunc.CreateThumbnailImage(m_editJobForm.EditJob.Miniature, this.m_JobListView.LargeImageList.ImageSize,this.thumbBorderColor);

						int copyIndex = m_Preference.IndexOf(JobListColumnHeader.Copies);
						if(copyIndex >0)
							jobItem.SubItems[ copyIndex].Text = copies.ToString();
						m_PreviewAndInfo.UpdateUIJobPreview(m_editJobForm.EditJob);
						if(jobItem != null)
							this.SetImageIndex(m_editJobForm.EditJob,ref jobItem);
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
				if(m_JobListView.SelectedItems.Count == 1)
				{
					JobListItem jobItem = (JobListItem)m_JobListView.SelectedItems[0];

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
		public void GetPrintableArea(out float Area, out float height)
		{
			float mediaHeight = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fPaperHeight;
			float mediaWidth = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fPaperWidth;
			float origin = this.m_iPrinterChange.GetAllParam().PrinterSetting.sFrequencySetting.fXOrigin;
			float originY = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fYOrigin;
			float xPaperLeft = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fLeftMargin;
			float xPaperTop = this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.fTopMargin;
			float inkstripeWidth =  GetInkStripeWidth(this.m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.sStripeSetting);
			float usableWidth = xPaperLeft + mediaWidth - origin - inkstripeWidth;
			float usableHeight = xPaperTop + mediaWidth - originY;

			Area = usableWidth;
			height = usableHeight;
		}
		private void PrintJob(bool blevellingPrint)
		{
			if(!JobItemOperate.IsPrinterCanPrint())
			{
				return;
			}
			if(!m_bDuringPrinting)
			{
				/* 手动输入数值后直接点击打印时,输入的参数不能再本次打印立即应用.所以打印操作前应用下设置
			 * (原因:焦点迁移事件在按钮点击事件后触发).
			 * */
				m_iPrinterChange.NotifyUIParamChanged();
				// Synchronous PrinterSetting before print job.
				SPrinterProperty.SynchronousCalibrationSettings(ref this.m_iPrinterChange.GetAllParam().PrinterSetting);
				LogWriter.LogSetPrinterSetting(this.m_iPrinterChange.GetAllParam().PrinterSetting,m_bDuringPrinting,"SynchronousCalibrationSettings");
			}
			string lostFiles = null;

			float usableWidth, usableHeight;
			GetPrintableArea(out usableWidth,out usableHeight);

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
		public void PrintJob()
		{
			this.PrintJob(false);
		}
		private void PrintJob(UIJob job)
		{
			if(job.Status != JobStatus.Idle && job.Status != JobStatus.Printed)
				return;
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
			CoreInterface.Printer_Abort();
			m_PrintJobTask.Abort();
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
			if(m_PrintJobTask.IsWorking())
				m_PrintJobTask.Terminate(bTerm);
			if(bTerm)
			{
				if(m_PrintJobTask.m_Worker != null)
					m_PrintJobTask.m_Worker.Join();
			}
			if(m_PreviewTask.IsWorking())
				m_PreviewTask.Terminate(bTerm);
			if(bTerm)
			{
				if(m_PreviewTask.m_Worker != null)
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

				if(jobItem.Tag == job)
				{
					return jobItem;
				}
			}

			return null;
		}

		public bool Confirm_Exit()
		{
			bool bBusy = false;

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

			if(bBusy)
			{
				string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.AbortPrinter);

				if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
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
		private System.IO.FileSystemWatcher HotForlderWatcher;
		private ArrayList EdittingJobList = new ArrayList();
		private Thread HotForlderThread = null;

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

			Debug.WriteLine("File: " +  e.FullPath + " Created");

			if(!EdittingJobList.Contains(e.FullPath))
			{
				EdittingJobList.Add(e.FullPath);

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

		private void WaitForCreatFinished()
		{
			while(EdittingJobList.Count > 0)
			{
				for(int i = 0; i< EdittingJobList.Count;i++)
				{
					try
					{
						FileStream fs = File.Open(EdittingJobList[i].ToString(),FileMode.Open,FileAccess.ReadWrite,FileShare.None);
						fs.Close();
						this.AddJob(EdittingJobList[i].ToString(),true);
						EdittingJobList.RemoveAt(i);
					}
					catch(Exception ex)
					{
						Debug.WriteLine("File: " +  EdittingJobList[i].ToString() + " " +ex.Message);
						continue;
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

		private void m_JobListView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem lvi = this.m_JobListView.GetItemAt( e.X,e.Y);
			if(lvi != null)
			{	
				string oldtipstring = this.toolTip1.GetToolTip(this.m_JobListView);
				UIPreference options = this.m_iPrinterChange.GetAllParam().Preference;
				JobListItem jli = (lvi as JobListItem);
//				jli.RefreshDis((jli.Tag as UIJob),options,m_PrintJobTask.GetCopiesString());
				string newtip = jli.ToolTipString;
				if(!newtip.Equals(oldtipstring))
					this.toolTip1.SetToolTip(this.m_JobListView,newtip);
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
	}


}
