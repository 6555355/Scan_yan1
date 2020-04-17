/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics ;
using System.Xml;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Text;

using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Setting;
using BYHXPrinterManager.Calibration;
using BYHXPrinterManager.Port;
using BYHXPrinterManager.GradientControls;


namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form,IPrinterChange,IMessageFilter
	{
		#region 常量
		const int   WM_KEYDOWN  = 0x0100;
		const int   WM_KEYUP    = 0x0101;
		const int   WM_SYSKEYDOWN =0x0104;
		const int	WM_SYSKEYUP   = 0x0105;
		#endregion

		#region 变量

		//ToolBarButton m_PushedToolBarButton = null;
		private bool m_bExitAtStart = false;
		private uint m_KernelMessage	= SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
		private PrinterOperate m_LastOperate = new PrinterOperate(); 
		DateTime m_StartTime = DateTime.Now;
		private AllParam m_allParam;

		private PortManager m_PortManager;
		private bool m_bSendMoveCmd = false;
		private SettingForm m_FuncSettingForm = null;
		private CaliWizard m_wizard = null;
		private UVForm m_hwForm = null;
		private bool m_bFirstReady = false;
		private string m_sFormTile = "";
		private MyMessageBox m_MyMessageBox = null;
		private Grouper m_GroupboxStyle = null;
		private MeasureQuestionForm m_MeasureQuestionForm = null;
		private bool m_bShowUI = true;

		private int ProcessID = 0;
		private System.Timers.Timer time = new System.Timers.Timer(10);//实例化Timer类，设置间隔时间为10毫秒；
		private System.Windows.Forms.Timer mPumpInkTimer; 
		private int m_CurErrorCode = -1;
		private bool m_bDuringPrinting = false;
		// 用于计算打印面积
		private int m_PrintPercent = -1;
		private UIJob m_PrintingJob = null;

		#endregion

		#region 构造函数
		private System.Windows.Forms.MainMenu m_MainMenu;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonAdd;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonDelete;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonEdit;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonPrint;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonPauseResume;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonAbort;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSep1;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonCheckNozzle;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonAutoClean;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSpray;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSingleClean;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSep2;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonLeft;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonRight;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonForward;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonBackward;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonGoHome;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSetOrigin;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonMeasurePaper;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonStop;
		private System.Windows.Forms.ImageList m_ToolbarImageList;
		private System.Windows.Forms.ToolBar m_ToolBarCommand;
		private System.Windows.Forms.Panel m_PanelToolBarSetting;
		private System.Windows.Forms.Panel m_PanelJobList;
		private BYHXPrinterManager.JobListView.PrintingInfo m_PreviewAndInfo;
		private System.Windows.Forms.Splitter m_SplitterStatus;
		private System.Windows.Forms.Splitter m_SplitterJobList;
		private System.Windows.Forms.MenuItem m_MenuItemJob;
		private System.Windows.Forms.MenuItem m_MenuItemAdd;
		private System.Windows.Forms.MenuItem m_MenuItemDelete;
		private System.Windows.Forms.MenuItem m_MenuItemPrint;
		private System.Windows.Forms.MenuItem m_MenuItemExit;
		private System.Windows.Forms.MenuItem m_MenuItemSetting;
		private System.Windows.Forms.MenuItem m_MenuItemSave;
		private System.Windows.Forms.MenuItem m_MenuItemLoad;
		private System.Windows.Forms.MenuItem m_MenuItemSaveToPrinter;
		private System.Windows.Forms.MenuItem m_MenuItemLoadFromPrinter;
		private System.Windows.Forms.MenuItem m_MenuItemEdit;
		private System.Windows.Forms.MenuItem m_MenuItemTools;
		private System.Windows.Forms.MenuItem m_MenuItemUpdate;
		private System.Windows.Forms.MenuItem m_MenuItemPassword;
		private System.Windows.Forms.MenuItem m_MenuItemDemoPage;
		private System.Windows.Forms.MenuItem m_MenuItemCalibraion;
		private System.Windows.Forms.MenuItem m_MenuItemView;
		private System.Windows.Forms.MenuItem m_MenuItemTopDown;
		private System.Windows.Forms.MenuItem m_MenuItemLeftRight;
		private System.Windows.Forms.MenuItem m_MenuItemHelp;
		private System.Windows.Forms.MenuItem m_MenuItemAbout;
		private System.Windows.Forms.Splitter m_SplitterToolbar;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonDownZ;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonUpZ;
		private System.Windows.Forms.MenuItem m_MenuItemHWSetting;
		private System.Windows.Forms.MenuItem m_MenuItemFactoryDebug;
		private System.Windows.Forms.MenuItem m_MenuItemDebug;
		private System.Windows.Forms.MenuItem m_MenuItemRealTime;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonGoHomeY;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonGoHomeZ;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSep3;
		private System.Windows.Forms.MenuItem m_MenuItemUVSetting;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSetOriginY;
		private System.Windows.Forms.Panel m_WorkForlderpanel;
		private BYHXPrinterManager.JobListView.JThumbnailView m_FolderPreview;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
		private System.Windows.Forms.StatusBar m_StatusBarApp;
		private System.Windows.Forms.ImageList UIIconImageList;
		private BYHXPrinterManager.JobListView.JobListForm m_JobListForm;
		private System.Windows.Forms.ImageList SubToolBarimageList;
		private System.Windows.Forms.ContextMenu MenuFolderPreview;
		private System.Windows.Forms.MenuItem menuItemAddToList;
		private System.Windows.Forms.MenuItem menuItemReLoad;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemSelectAll;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Panel panelSubBar;
		private System.Windows.Forms.Splitter m_WorkForldersplitter;
		private BYHXPrinterManager.Main.PrintInformation m_PrintInformation;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButtonWorkForlder;
		private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
		private System.Windows.Forms.ToolBarButton toolBarButtonAddToList;
		private System.Windows.Forms.ToolBarButton toolBarButtonSelectAll;
		private System.Windows.Forms.ImageList imageListMenu;
		private System.Windows.Forms.MenuItem m_MenuItemOldView;
		private Dev4Arabs.OfficeMenus officeMenus1;
		private System.Windows.Forms.ContextMenu m_StartMenu;
		private System.Windows.Forms.MenuItem menuItemDongle;
		private System.Windows.Forms.MenuItem menuItemDongleKey;
		private System.Windows.Forms.NotifyIcon notifyIconBYHX;
		private System.Windows.Forms.ImageList notifyIconImageList;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemSaveCaliPara;
		private System.Windows.Forms.MenuItem menuItemLoadCaliPara;
		private BYHXPrinterManager.Setting.ToolbarSetting m_ToolbarSetting;
		private System.ComponentModel.IContainer components;

		public MainForm(bool bshowUI)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			if(MyWndProc.LoadRenderResource(""))
			{
#if USETHEME
				this.ControlBox = false;
				MyWndProc.SetWindowTheme(this.Handle,"","");
#endif
			}
#if USETHEME
			MyWndProc.DrawRoundForm(this);
#endif
			StartWithInitComponent();
			//this.WindowState = System.Windows.Forms.FormWindowState.Maximized; 
			string iconpath= Application.StartupPath;
			iconpath += "\\setup\\app.ico";
			if(File.Exists(iconpath))
			{
				Icon icon = new Icon(iconpath);
				this.Icon = icon;
			}
			//Application.Icon = icon; 

			string NamePath = Application.StartupPath;
			NamePath += "\\setup\\Vender.xml";
			if(File.Exists(NamePath))
			{
				XmlDocument xmldoc = new XmlDocument();
				xmldoc.Load(NamePath);
				XmlElement node = xmldoc.DocumentElement;

				XmlNodeList list = node.GetElementsByTagName("Name");
				if(list != null || list.Count>=1)
				{
					string txt = list[0].InnerXml;
					//this.Text = txt + "  " +this.Text;
					this.Text = txt + "  " +ResString.GetProductName();
					m_sFormTile = this.Text;
				}
			}
			///////////////////////////////////////////
			///
			UIViewMode uimode = (UIViewMode)m_allParam.Preference.ViewModeIndex;
			if(	uimode == UIViewMode.OldView ||	uimode ==  UIViewMode.NotifyIcon)
				PubFunc.IconReload(m_ToolbarImageList);

			m_GroupboxStyle = new Grouper();
			m_GroupboxStyle.BackgroundGradientMode = GroupBoxGradientMode.Vertical;
			m_GroupboxStyle.PaintGroupBox = true;
			m_GroupboxStyle.RoundCorners = 5;
			m_GroupboxStyle.ShadowColor = System.Drawing.Color.DarkGray;
			m_GroupboxStyle.ShadowControl = false;
			m_GroupboxStyle.ShadowThickness = 3;
			//			m_GroupboxStyle.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.LightBlue);
			m_GroupboxStyle.TitileGradientColors = new BYHXPrinterManager.Style(Color.FromArgb(0x65,0x93,0xb7),Color.FromArgb(0x48,0x74,0x97));
			m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
			m_GroupboxStyle.TitleTextColor = Color.White;			
			//			m_GroupboxStyle.GradientColors = new Style(Color.Lavender,SystemColors.Highlight);
			//			m_GroupboxStyle.GradientColors = new BYHXPrinterManager.Style(Color.FromArgb(0x65,0x93,0xb7),Color.FromArgb(0x48,0x74,0x97));
			m_GroupboxStyle.GradientColors = new BYHXPrinterManager.Style(SystemColors.Control,SystemColors.Control);
			m_GroupboxStyle.GroupImage = null;
			this.m_JobListForm.m_SampleGroup = m_GroupboxStyle;
			//必须先初始化BYHXSoftLock.m_DongleKeyAlarm
			BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm();
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired+=new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H+=new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
			BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished+=new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);

#if EpsonLcd
			this.m_bShowUI = bshowUI;
			this.HandleCreated +=new EventHandler(mainWin_HandleCreated);

			if(!this.m_bShowUI)
			{
				this.ShowInTaskbar = false;
				this.WindowState = FormWindowState.Minimized;
			}		

			ProcessID = Process.GetCurrentProcess().Id;
			time.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；  
			time.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；  
			time.Enabled = false;//是否执行System.Timers.Timer.Elapsed事件；
#else
			menuItem1.Visible = menuItemSaveCaliPara.Visible = menuItemLoadCaliPara.Visible = false;
#endif
			mPumpInkTimer = new System.Windows.Forms.Timer();
			mPumpInkTimer.Interval = 10000; // 10秒
			mPumpInkTimer.Tick += new EventHandler(mPumpInkTimer_Tick);//到达时间的时候执行事件；  
#if INKWIN_UI
			this.m_PanelToolBarSetting.Visible = false;
#endif
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.m_MainMenu = new System.Windows.Forms.MainMenu();
			this.m_MenuItemJob = new System.Windows.Forms.MenuItem();
			this.m_MenuItemAdd = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDelete = new System.Windows.Forms.MenuItem();
			this.m_MenuItemPrint = new System.Windows.Forms.MenuItem();
			this.m_MenuItemExit = new System.Windows.Forms.MenuItem();
			this.m_MenuItemSetting = new System.Windows.Forms.MenuItem();
			this.m_MenuItemSave = new System.Windows.Forms.MenuItem();
			this.m_MenuItemLoad = new System.Windows.Forms.MenuItem();
			this.m_MenuItemSaveToPrinter = new System.Windows.Forms.MenuItem();
			this.m_MenuItemLoadFromPrinter = new System.Windows.Forms.MenuItem();
			this.m_MenuItemEdit = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemSaveCaliPara = new System.Windows.Forms.MenuItem();
			this.menuItemLoadCaliPara = new System.Windows.Forms.MenuItem();
			this.m_MenuItemTools = new System.Windows.Forms.MenuItem();
			this.m_MenuItemUpdate = new System.Windows.Forms.MenuItem();
			this.m_MenuItemPassword = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDemoPage = new System.Windows.Forms.MenuItem();
			this.m_MenuItemCalibraion = new System.Windows.Forms.MenuItem();
			this.m_MenuItemHWSetting = new System.Windows.Forms.MenuItem();
			this.m_MenuItemRealTime = new System.Windows.Forms.MenuItem();
			this.m_MenuItemUVSetting = new System.Windows.Forms.MenuItem();
			this.m_MenuItemView = new System.Windows.Forms.MenuItem();
			this.m_MenuItemTopDown = new System.Windows.Forms.MenuItem();
			this.m_MenuItemLeftRight = new System.Windows.Forms.MenuItem();
			this.m_MenuItemOldView = new System.Windows.Forms.MenuItem();
			this.m_MenuItemHelp = new System.Windows.Forms.MenuItem();
			this.m_MenuItemAbout = new System.Windows.Forms.MenuItem();
			this.menuItemDongle = new System.Windows.Forms.MenuItem();
			this.menuItemDongleKey = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDebug = new System.Windows.Forms.MenuItem();
			this.m_MenuItemFactoryDebug = new System.Windows.Forms.MenuItem();
			this.m_ToolBarCommand = new System.Windows.Forms.ToolBar();
			this.m_ToolBarButtonAdd = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonPrint = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonPauseResume = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonAbort = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSep1 = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonCheckNozzle = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonAutoClean = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSpray = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonStop = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSingleClean = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSep2 = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonLeft = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonRight = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSetOrigin = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonForward = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonBackward = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSetOriginY = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonDownZ = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonUpZ = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonSep3 = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonGoHome = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonGoHomeY = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonGoHomeZ = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonMeasurePaper = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
			this.m_ToolbarImageList = new System.Windows.Forms.ImageList(this.components);
			this.m_PanelToolBarSetting = new System.Windows.Forms.Panel();
			this.m_ToolbarSetting = new BYHXPrinterManager.Setting.ToolbarSetting();
			this.m_PanelJobList = new System.Windows.Forms.Panel();
			this.m_SplitterJobList = new System.Windows.Forms.Splitter();
			this.m_WorkForlderpanel = new System.Windows.Forms.Panel();
			this.m_JobListForm = new BYHXPrinterManager.JobListView.JobListForm();
			this.m_WorkForldersplitter = new System.Windows.Forms.Splitter();
			this.m_FolderPreview = new BYHXPrinterManager.JobListView.JThumbnailView();
			this.panelSubBar = new System.Windows.Forms.Panel();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButtonWorkForlder = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonAddToList = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonSelectAll = new System.Windows.Forms.ToolBarButton();
			this.SubToolBarimageList = new System.Windows.Forms.ImageList(this.components);
			this.m_PreviewAndInfo = new BYHXPrinterManager.JobListView.PrintingInfo();
			this.MenuFolderPreview = new System.Windows.Forms.ContextMenu();
			this.menuItemAddToList = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemReLoad = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItemSelectAll = new System.Windows.Forms.MenuItem();
			this.m_SplitterStatus = new System.Windows.Forms.Splitter();
			this.m_SplitterToolbar = new System.Windows.Forms.Splitter();
			this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
			this.m_PrintInformation = new BYHXPrinterManager.Main.PrintInformation();
			this.UIIconImageList = new System.Windows.Forms.ImageList(this.components);
			this.m_StartMenu = new System.Windows.Forms.ContextMenu();
			this.imageListMenu = new System.Windows.Forms.ImageList(this.components);
			this.officeMenus1 = new Dev4Arabs.OfficeMenus(this.components);
			this.notifyIconBYHX = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyIconImageList = new System.Windows.Forms.ImageList(this.components);
			this.m_PanelToolBarSetting.SuspendLayout();
			this.m_PanelJobList.SuspendLayout();
			this.m_WorkForlderpanel.SuspendLayout();
			this.panelSubBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).BeginInit();
			this.SuspendLayout();
			// 
			// m_MainMenu
			// 
			this.m_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.m_MenuItemJob,
																					   this.m_MenuItemSetting,
																					   this.m_MenuItemTools,
																					   this.m_MenuItemView,
																					   this.m_MenuItemHelp,
																					   this.menuItemDongle,
																					   this.m_MenuItemDebug});
			this.m_MainMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_MainMenu.RightToLeft")));
			// 
			// m_MenuItemJob
			// 
			this.m_MenuItemJob.Enabled = ((bool)(resources.GetObject("m_MenuItemJob.Enabled")));
			this.m_MenuItemJob.Index = 0;
			this.m_MenuItemJob.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.m_MenuItemAdd,
																						  this.m_MenuItemDelete,
																						  this.m_MenuItemPrint,
																						  this.m_MenuItemExit});
			this.m_MenuItemJob.OwnerDraw = true;
			this.m_MenuItemJob.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemJob.Shortcut")));
			this.m_MenuItemJob.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemJob.ShowShortcut")));
			this.m_MenuItemJob.Text = resources.GetString("m_MenuItemJob.Text");
			this.m_MenuItemJob.Visible = ((bool)(resources.GetObject("m_MenuItemJob.Visible")));
			// 
			// m_MenuItemAdd
			// 
			this.m_MenuItemAdd.Enabled = ((bool)(resources.GetObject("m_MenuItemAdd.Enabled")));
			this.m_MenuItemAdd.Index = 0;
			this.m_MenuItemAdd.OwnerDraw = true;
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
			this.m_MenuItemDelete.OwnerDraw = true;
			this.m_MenuItemDelete.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemDelete.Shortcut")));
			this.m_MenuItemDelete.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemDelete.ShowShortcut")));
			this.m_MenuItemDelete.Text = resources.GetString("m_MenuItemDelete.Text");
			this.m_MenuItemDelete.Visible = ((bool)(resources.GetObject("m_MenuItemDelete.Visible")));
			this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
			// 
			// m_MenuItemPrint
			// 
			this.m_MenuItemPrint.Enabled = ((bool)(resources.GetObject("m_MenuItemPrint.Enabled")));
			this.m_MenuItemPrint.Index = 2;
			this.m_MenuItemPrint.OwnerDraw = true;
			this.m_MenuItemPrint.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemPrint.Shortcut")));
			this.m_MenuItemPrint.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemPrint.ShowShortcut")));
			this.m_MenuItemPrint.Text = resources.GetString("m_MenuItemPrint.Text");
			this.m_MenuItemPrint.Visible = ((bool)(resources.GetObject("m_MenuItemPrint.Visible")));
			this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
			// 
			// m_MenuItemExit
			// 
			this.m_MenuItemExit.Enabled = ((bool)(resources.GetObject("m_MenuItemExit.Enabled")));
			this.m_MenuItemExit.Index = 3;
			this.m_MenuItemExit.OwnerDraw = true;
			this.m_MenuItemExit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemExit.Shortcut")));
			this.m_MenuItemExit.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemExit.ShowShortcut")));
			this.m_MenuItemExit.Text = resources.GetString("m_MenuItemExit.Text");
			this.m_MenuItemExit.Visible = ((bool)(resources.GetObject("m_MenuItemExit.Visible")));
			this.m_MenuItemExit.Click += new System.EventHandler(this.m_MenuItemExit_Click);
			// 
			// m_MenuItemSetting
			// 
			this.m_MenuItemSetting.Enabled = ((bool)(resources.GetObject("m_MenuItemSetting.Enabled")));
			this.m_MenuItemSetting.Index = 1;
			this.m_MenuItemSetting.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.m_MenuItemSave,
																							  this.m_MenuItemLoad,
																							  this.m_MenuItemSaveToPrinter,
																							  this.m_MenuItemLoadFromPrinter,
																							  this.m_MenuItemEdit,
																							  this.menuItem1,
																							  this.menuItemSaveCaliPara,
																							  this.menuItemLoadCaliPara});
			this.m_MenuItemSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemSetting.Shortcut")));
			this.m_MenuItemSetting.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemSetting.ShowShortcut")));
			this.m_MenuItemSetting.Text = resources.GetString("m_MenuItemSetting.Text");
			this.m_MenuItemSetting.Visible = ((bool)(resources.GetObject("m_MenuItemSetting.Visible")));
			// 
			// m_MenuItemSave
			// 
			this.m_MenuItemSave.Enabled = ((bool)(resources.GetObject("m_MenuItemSave.Enabled")));
			this.m_MenuItemSave.Index = 0;
			this.m_MenuItemSave.OwnerDraw = true;
			this.m_MenuItemSave.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemSave.Shortcut")));
			this.m_MenuItemSave.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemSave.ShowShortcut")));
			this.m_MenuItemSave.Text = resources.GetString("m_MenuItemSave.Text");
			this.m_MenuItemSave.Visible = ((bool)(resources.GetObject("m_MenuItemSave.Visible")));
			this.m_MenuItemSave.Click += new System.EventHandler(this.m_MenuItemSave_Click);
			// 
			// m_MenuItemLoad
			// 
			this.m_MenuItemLoad.Enabled = ((bool)(resources.GetObject("m_MenuItemLoad.Enabled")));
			this.m_MenuItemLoad.Index = 1;
			this.m_MenuItemLoad.OwnerDraw = true;
			this.m_MenuItemLoad.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemLoad.Shortcut")));
			this.m_MenuItemLoad.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemLoad.ShowShortcut")));
			this.m_MenuItemLoad.Text = resources.GetString("m_MenuItemLoad.Text");
			this.m_MenuItemLoad.Visible = ((bool)(resources.GetObject("m_MenuItemLoad.Visible")));
			this.m_MenuItemLoad.Click += new System.EventHandler(this.m_MenuItemLoad_Click);
			// 
			// m_MenuItemSaveToPrinter
			// 
			this.m_MenuItemSaveToPrinter.Enabled = ((bool)(resources.GetObject("m_MenuItemSaveToPrinter.Enabled")));
			this.m_MenuItemSaveToPrinter.Index = 2;
			this.m_MenuItemSaveToPrinter.OwnerDraw = true;
			this.m_MenuItemSaveToPrinter.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemSaveToPrinter.Shortcut")));
			this.m_MenuItemSaveToPrinter.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemSaveToPrinter.ShowShortcut")));
			this.m_MenuItemSaveToPrinter.Text = resources.GetString("m_MenuItemSaveToPrinter.Text");
			this.m_MenuItemSaveToPrinter.Visible = ((bool)(resources.GetObject("m_MenuItemSaveToPrinter.Visible")));
			this.m_MenuItemSaveToPrinter.Click += new System.EventHandler(this.m_MenuItemSaveToPrinter_Click);
			// 
			// m_MenuItemLoadFromPrinter
			// 
			this.m_MenuItemLoadFromPrinter.Enabled = ((bool)(resources.GetObject("m_MenuItemLoadFromPrinter.Enabled")));
			this.m_MenuItemLoadFromPrinter.Index = 3;
			this.m_MenuItemLoadFromPrinter.OwnerDraw = true;
			this.m_MenuItemLoadFromPrinter.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemLoadFromPrinter.Shortcut")));
			this.m_MenuItemLoadFromPrinter.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemLoadFromPrinter.ShowShortcut")));
			this.m_MenuItemLoadFromPrinter.Text = resources.GetString("m_MenuItemLoadFromPrinter.Text");
			this.m_MenuItemLoadFromPrinter.Visible = ((bool)(resources.GetObject("m_MenuItemLoadFromPrinter.Visible")));
			this.m_MenuItemLoadFromPrinter.Click += new System.EventHandler(this.m_MenuItemLoadFromPrinter_Click);
			// 
			// m_MenuItemEdit
			// 
			this.m_MenuItemEdit.Enabled = ((bool)(resources.GetObject("m_MenuItemEdit.Enabled")));
			this.m_MenuItemEdit.Index = 4;
			this.m_MenuItemEdit.OwnerDraw = true;
			this.m_MenuItemEdit.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemEdit.Shortcut")));
			this.m_MenuItemEdit.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemEdit.ShowShortcut")));
			this.m_MenuItemEdit.Text = resources.GetString("m_MenuItemEdit.Text");
			this.m_MenuItemEdit.Visible = ((bool)(resources.GetObject("m_MenuItemEdit.Visible")));
			this.m_MenuItemEdit.Click += new System.EventHandler(this.m_MenuItemEdit_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 5;
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// menuItemSaveCaliPara
			// 
			this.menuItemSaveCaliPara.Enabled = ((bool)(resources.GetObject("menuItemSaveCaliPara.Enabled")));
			this.menuItemSaveCaliPara.Index = 6;
			this.menuItemSaveCaliPara.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemSaveCaliPara.Shortcut")));
			this.menuItemSaveCaliPara.ShowShortcut = ((bool)(resources.GetObject("menuItemSaveCaliPara.ShowShortcut")));
			this.menuItemSaveCaliPara.Text = resources.GetString("menuItemSaveCaliPara.Text");
			this.menuItemSaveCaliPara.Visible = ((bool)(resources.GetObject("menuItemSaveCaliPara.Visible")));
			this.menuItemSaveCaliPara.Click += new System.EventHandler(this.menuItemSaveCaliPara_Click);
			// 
			// menuItemLoadCaliPara
			// 
			this.menuItemLoadCaliPara.Enabled = ((bool)(resources.GetObject("menuItemLoadCaliPara.Enabled")));
			this.menuItemLoadCaliPara.Index = 7;
			this.menuItemLoadCaliPara.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemLoadCaliPara.Shortcut")));
			this.menuItemLoadCaliPara.ShowShortcut = ((bool)(resources.GetObject("menuItemLoadCaliPara.ShowShortcut")));
			this.menuItemLoadCaliPara.Text = resources.GetString("menuItemLoadCaliPara.Text");
			this.menuItemLoadCaliPara.Visible = ((bool)(resources.GetObject("menuItemLoadCaliPara.Visible")));
			this.menuItemLoadCaliPara.Click += new System.EventHandler(this.menuItemLoadCaliPara_Click);
			// 
			// m_MenuItemTools
			// 
			this.m_MenuItemTools.Enabled = ((bool)(resources.GetObject("m_MenuItemTools.Enabled")));
			this.m_MenuItemTools.Index = 2;
			this.m_MenuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.m_MenuItemUpdate,
																							this.m_MenuItemPassword,
																							this.m_MenuItemDemoPage,
																							this.m_MenuItemCalibraion,
																							this.m_MenuItemHWSetting,
																							this.m_MenuItemRealTime,
																							this.m_MenuItemUVSetting});
			this.m_MenuItemTools.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemTools.Shortcut")));
			this.m_MenuItemTools.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemTools.ShowShortcut")));
			this.m_MenuItemTools.Text = resources.GetString("m_MenuItemTools.Text");
			this.m_MenuItemTools.Visible = ((bool)(resources.GetObject("m_MenuItemTools.Visible")));
			// 
			// m_MenuItemUpdate
			// 
			this.m_MenuItemUpdate.Enabled = ((bool)(resources.GetObject("m_MenuItemUpdate.Enabled")));
			this.m_MenuItemUpdate.Index = 0;
			this.m_MenuItemUpdate.OwnerDraw = true;
			this.m_MenuItemUpdate.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemUpdate.Shortcut")));
			this.m_MenuItemUpdate.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemUpdate.ShowShortcut")));
			this.m_MenuItemUpdate.Text = resources.GetString("m_MenuItemUpdate.Text");
			this.m_MenuItemUpdate.Visible = ((bool)(resources.GetObject("m_MenuItemUpdate.Visible")));
			this.m_MenuItemUpdate.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
			// 
			// m_MenuItemPassword
			// 
			this.m_MenuItemPassword.Enabled = ((bool)(resources.GetObject("m_MenuItemPassword.Enabled")));
			this.m_MenuItemPassword.Index = 1;
			this.m_MenuItemPassword.OwnerDraw = true;
			this.m_MenuItemPassword.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemPassword.Shortcut")));
			this.m_MenuItemPassword.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemPassword.ShowShortcut")));
			this.m_MenuItemPassword.Text = resources.GetString("m_MenuItemPassword.Text");
			this.m_MenuItemPassword.Visible = ((bool)(resources.GetObject("m_MenuItemPassword.Visible")));
			this.m_MenuItemPassword.Click += new System.EventHandler(this.m_MenuItemPassword_Click);
			// 
			// m_MenuItemDemoPage
			// 
			this.m_MenuItemDemoPage.Enabled = ((bool)(resources.GetObject("m_MenuItemDemoPage.Enabled")));
			this.m_MenuItemDemoPage.Index = 2;
			this.m_MenuItemDemoPage.OwnerDraw = true;
			this.m_MenuItemDemoPage.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemDemoPage.Shortcut")));
			this.m_MenuItemDemoPage.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemDemoPage.ShowShortcut")));
			this.m_MenuItemDemoPage.Text = resources.GetString("m_MenuItemDemoPage.Text");
			this.m_MenuItemDemoPage.Visible = ((bool)(resources.GetObject("m_MenuItemDemoPage.Visible")));
			this.m_MenuItemDemoPage.Click += new System.EventHandler(this.m_MenuItemDemoPage_Click);
			// 
			// m_MenuItemCalibraion
			// 
			this.m_MenuItemCalibraion.Enabled = ((bool)(resources.GetObject("m_MenuItemCalibraion.Enabled")));
			this.m_MenuItemCalibraion.Index = 3;
			this.m_MenuItemCalibraion.OwnerDraw = true;
			this.m_MenuItemCalibraion.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemCalibraion.Shortcut")));
			this.m_MenuItemCalibraion.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemCalibraion.ShowShortcut")));
			this.m_MenuItemCalibraion.Text = resources.GetString("m_MenuItemCalibraion.Text");
			this.m_MenuItemCalibraion.Visible = ((bool)(resources.GetObject("m_MenuItemCalibraion.Visible")));
			this.m_MenuItemCalibraion.Click += new System.EventHandler(this.m_MenuItemCalibraion_Click);
			// 
			// m_MenuItemHWSetting
			// 
			this.m_MenuItemHWSetting.Enabled = ((bool)(resources.GetObject("m_MenuItemHWSetting.Enabled")));
			this.m_MenuItemHWSetting.Index = 4;
			this.m_MenuItemHWSetting.OwnerDraw = true;
			this.m_MenuItemHWSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemHWSetting.Shortcut")));
			this.m_MenuItemHWSetting.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemHWSetting.ShowShortcut")));
			this.m_MenuItemHWSetting.Text = resources.GetString("m_MenuItemHWSetting.Text");
			this.m_MenuItemHWSetting.Visible = ((bool)(resources.GetObject("m_MenuItemHWSetting.Visible")));
			this.m_MenuItemHWSetting.Click += new System.EventHandler(this.m_MenuItemHWSetting_Click);
			// 
			// m_MenuItemRealTime
			// 
			this.m_MenuItemRealTime.Enabled = ((bool)(resources.GetObject("m_MenuItemRealTime.Enabled")));
			this.m_MenuItemRealTime.Index = 5;
			this.m_MenuItemRealTime.OwnerDraw = true;
			this.m_MenuItemRealTime.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemRealTime.Shortcut")));
			this.m_MenuItemRealTime.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemRealTime.ShowShortcut")));
			this.m_MenuItemRealTime.Text = resources.GetString("m_MenuItemRealTime.Text");
			this.m_MenuItemRealTime.Visible = ((bool)(resources.GetObject("m_MenuItemRealTime.Visible")));
			this.m_MenuItemRealTime.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
			// 
			// m_MenuItemUVSetting
			// 
			this.m_MenuItemUVSetting.Enabled = ((bool)(resources.GetObject("m_MenuItemUVSetting.Enabled")));
			this.m_MenuItemUVSetting.Index = 6;
			this.m_MenuItemUVSetting.OwnerDraw = true;
			this.m_MenuItemUVSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemUVSetting.Shortcut")));
			this.m_MenuItemUVSetting.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemUVSetting.ShowShortcut")));
			this.m_MenuItemUVSetting.Text = resources.GetString("m_MenuItemUVSetting.Text");
			this.m_MenuItemUVSetting.Visible = ((bool)(resources.GetObject("m_MenuItemUVSetting.Visible")));
			this.m_MenuItemUVSetting.Click += new System.EventHandler(this.m_MenuItemUVSetting_Click);
			// 
			// m_MenuItemView
			// 
			this.m_MenuItemView.Enabled = ((bool)(resources.GetObject("m_MenuItemView.Enabled")));
			this.m_MenuItemView.Index = 3;
			this.m_MenuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.m_MenuItemTopDown,
																						   this.m_MenuItemLeftRight,
																						   this.m_MenuItemOldView});
			this.m_MenuItemView.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemView.Shortcut")));
			this.m_MenuItemView.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemView.ShowShortcut")));
			this.m_MenuItemView.Text = resources.GetString("m_MenuItemView.Text");
			this.m_MenuItemView.Visible = ((bool)(resources.GetObject("m_MenuItemView.Visible")));
			// 
			// m_MenuItemTopDown
			// 
			this.m_MenuItemTopDown.Enabled = ((bool)(resources.GetObject("m_MenuItemTopDown.Enabled")));
			this.m_MenuItemTopDown.Index = 0;
			this.m_MenuItemTopDown.OwnerDraw = true;
			this.m_MenuItemTopDown.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemTopDown.Shortcut")));
			this.m_MenuItemTopDown.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemTopDown.ShowShortcut")));
			this.m_MenuItemTopDown.Text = resources.GetString("m_MenuItemTopDown.Text");
			this.m_MenuItemTopDown.Visible = ((bool)(resources.GetObject("m_MenuItemTopDown.Visible")));
			this.m_MenuItemTopDown.Click += new System.EventHandler(this.m_MenuItemTopDown_Click);
			// 
			// m_MenuItemLeftRight
			// 
			this.m_MenuItemLeftRight.Enabled = ((bool)(resources.GetObject("m_MenuItemLeftRight.Enabled")));
			this.m_MenuItemLeftRight.Index = 1;
			this.m_MenuItemLeftRight.OwnerDraw = true;
			this.m_MenuItemLeftRight.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemLeftRight.Shortcut")));
			this.m_MenuItemLeftRight.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemLeftRight.ShowShortcut")));
			this.m_MenuItemLeftRight.Text = resources.GetString("m_MenuItemLeftRight.Text");
			this.m_MenuItemLeftRight.Visible = ((bool)(resources.GetObject("m_MenuItemLeftRight.Visible")));
			this.m_MenuItemLeftRight.Click += new System.EventHandler(this.m_MenuItemLeftRight_Click);
			// 
			// m_MenuItemOldView
			// 
			this.m_MenuItemOldView.Enabled = ((bool)(resources.GetObject("m_MenuItemOldView.Enabled")));
			this.m_MenuItemOldView.Index = 2;
			this.m_MenuItemOldView.OwnerDraw = true;
			this.m_MenuItemOldView.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemOldView.Shortcut")));
			this.m_MenuItemOldView.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemOldView.ShowShortcut")));
			this.m_MenuItemOldView.Text = resources.GetString("m_MenuItemOldView.Text");
			this.m_MenuItemOldView.Visible = ((bool)(resources.GetObject("m_MenuItemOldView.Visible")));
			this.m_MenuItemOldView.Click += new System.EventHandler(this.m_MenuItemOldView_Click);
			// 
			// m_MenuItemHelp
			// 
			this.m_MenuItemHelp.Enabled = ((bool)(resources.GetObject("m_MenuItemHelp.Enabled")));
			this.m_MenuItemHelp.Index = 4;
			this.m_MenuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.m_MenuItemAbout});
			this.m_MenuItemHelp.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemHelp.Shortcut")));
			this.m_MenuItemHelp.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemHelp.ShowShortcut")));
			this.m_MenuItemHelp.Text = resources.GetString("m_MenuItemHelp.Text");
			this.m_MenuItemHelp.Visible = ((bool)(resources.GetObject("m_MenuItemHelp.Visible")));
			// 
			// m_MenuItemAbout
			// 
			this.m_MenuItemAbout.Enabled = ((bool)(resources.GetObject("m_MenuItemAbout.Enabled")));
			this.m_MenuItemAbout.Index = 0;
			this.m_MenuItemAbout.OwnerDraw = true;
			this.m_MenuItemAbout.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemAbout.Shortcut")));
			this.m_MenuItemAbout.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemAbout.ShowShortcut")));
			this.m_MenuItemAbout.Text = resources.GetString("m_MenuItemAbout.Text");
			this.m_MenuItemAbout.Visible = ((bool)(resources.GetObject("m_MenuItemAbout.Visible")));
			this.m_MenuItemAbout.Click += new System.EventHandler(this.m_MenuItemAbout_Click);
			// 
			// menuItemDongle
			// 
			this.menuItemDongle.Enabled = ((bool)(resources.GetObject("menuItemDongle.Enabled")));
			this.menuItemDongle.Index = 5;
			this.menuItemDongle.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuItemDongleKey});
			this.menuItemDongle.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemDongle.Shortcut")));
			this.menuItemDongle.ShowShortcut = ((bool)(resources.GetObject("menuItemDongle.ShowShortcut")));
			this.menuItemDongle.Text = resources.GetString("menuItemDongle.Text");
			this.menuItemDongle.Visible = ((bool)(resources.GetObject("menuItemDongle.Visible")));
			// 
			// menuItemDongleKey
			// 
			this.menuItemDongleKey.Enabled = ((bool)(resources.GetObject("menuItemDongleKey.Enabled")));
			this.menuItemDongleKey.Index = 0;
			this.menuItemDongleKey.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemDongleKey.Shortcut")));
			this.menuItemDongleKey.ShowShortcut = ((bool)(resources.GetObject("menuItemDongleKey.ShowShortcut")));
			this.menuItemDongleKey.Text = resources.GetString("menuItemDongleKey.Text");
			this.menuItemDongleKey.Visible = ((bool)(resources.GetObject("menuItemDongleKey.Visible")));
			this.menuItemDongleKey.Click += new System.EventHandler(this.menuItemDongleKey_Click);
			// 
			// m_MenuItemDebug
			// 
			this.m_MenuItemDebug.Enabled = ((bool)(resources.GetObject("m_MenuItemDebug.Enabled")));
			this.m_MenuItemDebug.Index = 6;
			this.m_MenuItemDebug.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.m_MenuItemFactoryDebug});
			this.m_MenuItemDebug.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemDebug.Shortcut")));
			this.m_MenuItemDebug.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemDebug.ShowShortcut")));
			this.m_MenuItemDebug.Text = resources.GetString("m_MenuItemDebug.Text");
			this.m_MenuItemDebug.Visible = ((bool)(resources.GetObject("m_MenuItemDebug.Visible")));
			// 
			// m_MenuItemFactoryDebug
			// 
			this.m_MenuItemFactoryDebug.Enabled = ((bool)(resources.GetObject("m_MenuItemFactoryDebug.Enabled")));
			this.m_MenuItemFactoryDebug.Index = 0;
			this.m_MenuItemFactoryDebug.OwnerDraw = true;
			this.m_MenuItemFactoryDebug.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemFactoryDebug.Shortcut")));
			this.m_MenuItemFactoryDebug.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemFactoryDebug.ShowShortcut")));
			this.m_MenuItemFactoryDebug.Text = resources.GetString("m_MenuItemFactoryDebug.Text");
			this.m_MenuItemFactoryDebug.Visible = ((bool)(resources.GetObject("m_MenuItemFactoryDebug.Visible")));
			this.m_MenuItemFactoryDebug.Click += new System.EventHandler(this.m_MenuItemFactoryDebug_Click);
			// 
			// m_ToolBarCommand
			// 
			this.m_ToolBarCommand.AccessibleDescription = resources.GetString("m_ToolBarCommand.AccessibleDescription");
			this.m_ToolBarCommand.AccessibleName = resources.GetString("m_ToolBarCommand.AccessibleName");
			this.m_ToolBarCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ToolBarCommand.Anchor")));
			this.m_ToolBarCommand.Appearance = ((System.Windows.Forms.ToolBarAppearance)(resources.GetObject("m_ToolBarCommand.Appearance")));
			this.m_ToolBarCommand.AutoSize = ((bool)(resources.GetObject("m_ToolBarCommand.AutoSize")));
			this.m_ToolBarCommand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ToolBarCommand.BackgroundImage")));
			this.m_ToolBarCommand.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																								this.m_ToolBarButtonAdd,
																								this.m_ToolBarButtonDelete,
																								this.m_ToolBarButtonPrint,
																								this.m_ToolBarButtonPauseResume,
																								this.m_ToolBarButtonAbort,
																								this.m_ToolBarButtonSep1,
																								this.m_ToolBarButtonCheckNozzle,
																								this.m_ToolBarButtonAutoClean,
																								this.m_ToolBarButtonSpray,
																								this.m_ToolBarButtonStop,
																								this.m_ToolBarButtonSingleClean,
																								this.m_ToolBarButtonSep2,
																								this.m_ToolBarButtonLeft,
																								this.m_ToolBarButtonRight,
																								this.m_ToolBarButtonSetOrigin,
																								this.m_ToolBarButtonForward,
																								this.m_ToolBarButtonBackward,
																								this.m_ToolBarButtonSetOriginY,
																								this.m_ToolBarButtonDownZ,
																								this.m_ToolBarButtonUpZ,
																								this.m_ToolBarButtonSep3,
																								this.m_ToolBarButtonGoHome,
																								this.m_ToolBarButtonGoHomeY,
																								this.m_ToolBarButtonGoHomeZ,
																								this.m_ToolBarButtonMeasurePaper,
																								this.m_ToolBarButtonEdit});
			this.m_ToolBarCommand.ButtonSize = ((System.Drawing.Size)(resources.GetObject("m_ToolBarCommand.ButtonSize")));
			this.m_ToolBarCommand.Divider = false;
			this.m_ToolBarCommand.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ToolBarCommand.Dock")));
			this.m_ToolBarCommand.DropDownArrows = ((bool)(resources.GetObject("m_ToolBarCommand.DropDownArrows")));
			this.m_ToolBarCommand.Enabled = ((bool)(resources.GetObject("m_ToolBarCommand.Enabled")));
			this.m_ToolBarCommand.Font = ((System.Drawing.Font)(resources.GetObject("m_ToolBarCommand.Font")));
			this.m_ToolBarCommand.ImageList = this.m_ToolbarImageList;
			this.m_ToolBarCommand.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ToolBarCommand.ImeMode")));
			this.m_ToolBarCommand.Location = ((System.Drawing.Point)(resources.GetObject("m_ToolBarCommand.Location")));
			this.m_ToolBarCommand.Name = "m_ToolBarCommand";
			this.m_ToolBarCommand.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ToolBarCommand.RightToLeft")));
			this.m_ToolBarCommand.ShowToolTips = ((bool)(resources.GetObject("m_ToolBarCommand.ShowToolTips")));
			this.m_ToolBarCommand.Size = ((System.Drawing.Size)(resources.GetObject("m_ToolBarCommand.Size")));
			this.m_ToolBarCommand.TabIndex = ((int)(resources.GetObject("m_ToolBarCommand.TabIndex")));
			this.m_ToolBarCommand.TextAlign = ((System.Windows.Forms.ToolBarTextAlign)(resources.GetObject("m_ToolBarCommand.TextAlign")));
			this.m_ToolBarCommand.Visible = ((bool)(resources.GetObject("m_ToolBarCommand.Visible")));
			this.m_ToolBarCommand.Wrappable = ((bool)(resources.GetObject("m_ToolBarCommand.Wrappable")));
			this.m_ToolBarCommand.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_ToolBarCommand_ButtonClick);
			this.m_ToolBarCommand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
			this.m_ToolBarCommand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseDown);
			// 
			// m_ToolBarButtonAdd
			// 
			this.m_ToolBarButtonAdd.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonAdd.Enabled")));
			this.m_ToolBarButtonAdd.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonAdd.ImageIndex")));
			this.m_ToolBarButtonAdd.Text = resources.GetString("m_ToolBarButtonAdd.Text");
			this.m_ToolBarButtonAdd.ToolTipText = resources.GetString("m_ToolBarButtonAdd.ToolTipText");
			this.m_ToolBarButtonAdd.Visible = ((bool)(resources.GetObject("m_ToolBarButtonAdd.Visible")));
			// 
			// m_ToolBarButtonDelete
			// 
			this.m_ToolBarButtonDelete.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonDelete.Enabled")));
			this.m_ToolBarButtonDelete.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonDelete.ImageIndex")));
			this.m_ToolBarButtonDelete.Text = resources.GetString("m_ToolBarButtonDelete.Text");
			this.m_ToolBarButtonDelete.ToolTipText = resources.GetString("m_ToolBarButtonDelete.ToolTipText");
			this.m_ToolBarButtonDelete.Visible = ((bool)(resources.GetObject("m_ToolBarButtonDelete.Visible")));
			// 
			// m_ToolBarButtonPrint
			// 
			this.m_ToolBarButtonPrint.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonPrint.Enabled")));
			this.m_ToolBarButtonPrint.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonPrint.ImageIndex")));
			this.m_ToolBarButtonPrint.Text = resources.GetString("m_ToolBarButtonPrint.Text");
			this.m_ToolBarButtonPrint.ToolTipText = resources.GetString("m_ToolBarButtonPrint.ToolTipText");
			this.m_ToolBarButtonPrint.Visible = ((bool)(resources.GetObject("m_ToolBarButtonPrint.Visible")));
			// 
			// m_ToolBarButtonPauseResume
			// 
			this.m_ToolBarButtonPauseResume.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonPauseResume.Enabled")));
			this.m_ToolBarButtonPauseResume.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonPauseResume.ImageIndex")));
			this.m_ToolBarButtonPauseResume.Text = resources.GetString("m_ToolBarButtonPauseResume.Text");
			this.m_ToolBarButtonPauseResume.ToolTipText = resources.GetString("m_ToolBarButtonPauseResume.ToolTipText");
			this.m_ToolBarButtonPauseResume.Visible = ((bool)(resources.GetObject("m_ToolBarButtonPauseResume.Visible")));
			// 
			// m_ToolBarButtonAbort
			// 
			this.m_ToolBarButtonAbort.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonAbort.Enabled")));
			this.m_ToolBarButtonAbort.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonAbort.ImageIndex")));
			this.m_ToolBarButtonAbort.Text = resources.GetString("m_ToolBarButtonAbort.Text");
			this.m_ToolBarButtonAbort.ToolTipText = resources.GetString("m_ToolBarButtonAbort.ToolTipText");
			this.m_ToolBarButtonAbort.Visible = ((bool)(resources.GetObject("m_ToolBarButtonAbort.Visible")));
			// 
			// m_ToolBarButtonSep1
			// 
			this.m_ToolBarButtonSep1.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSep1.Enabled")));
			this.m_ToolBarButtonSep1.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSep1.ImageIndex")));
			this.m_ToolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.m_ToolBarButtonSep1.Text = resources.GetString("m_ToolBarButtonSep1.Text");
			this.m_ToolBarButtonSep1.ToolTipText = resources.GetString("m_ToolBarButtonSep1.ToolTipText");
			this.m_ToolBarButtonSep1.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSep1.Visible")));
			// 
			// m_ToolBarButtonCheckNozzle
			// 
			this.m_ToolBarButtonCheckNozzle.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonCheckNozzle.Enabled")));
			this.m_ToolBarButtonCheckNozzle.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonCheckNozzle.ImageIndex")));
			this.m_ToolBarButtonCheckNozzle.Text = resources.GetString("m_ToolBarButtonCheckNozzle.Text");
			this.m_ToolBarButtonCheckNozzle.ToolTipText = resources.GetString("m_ToolBarButtonCheckNozzle.ToolTipText");
			this.m_ToolBarButtonCheckNozzle.Visible = ((bool)(resources.GetObject("m_ToolBarButtonCheckNozzle.Visible")));
			// 
			// m_ToolBarButtonAutoClean
			// 
			this.m_ToolBarButtonAutoClean.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonAutoClean.Enabled")));
			this.m_ToolBarButtonAutoClean.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonAutoClean.ImageIndex")));
			this.m_ToolBarButtonAutoClean.Text = resources.GetString("m_ToolBarButtonAutoClean.Text");
			this.m_ToolBarButtonAutoClean.ToolTipText = resources.GetString("m_ToolBarButtonAutoClean.ToolTipText");
			this.m_ToolBarButtonAutoClean.Visible = ((bool)(resources.GetObject("m_ToolBarButtonAutoClean.Visible")));
			// 
			// m_ToolBarButtonSpray
			// 
			this.m_ToolBarButtonSpray.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSpray.Enabled")));
			this.m_ToolBarButtonSpray.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSpray.ImageIndex")));
			this.m_ToolBarButtonSpray.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.m_ToolBarButtonSpray.Text = resources.GetString("m_ToolBarButtonSpray.Text");
			this.m_ToolBarButtonSpray.ToolTipText = resources.GetString("m_ToolBarButtonSpray.ToolTipText");
			this.m_ToolBarButtonSpray.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSpray.Visible")));
			// 
			// m_ToolBarButtonStop
			// 
			this.m_ToolBarButtonStop.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonStop.Enabled")));
			this.m_ToolBarButtonStop.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonStop.ImageIndex")));
			this.m_ToolBarButtonStop.Text = resources.GetString("m_ToolBarButtonStop.Text");
			this.m_ToolBarButtonStop.ToolTipText = resources.GetString("m_ToolBarButtonStop.ToolTipText");
			this.m_ToolBarButtonStop.Visible = ((bool)(resources.GetObject("m_ToolBarButtonStop.Visible")));
			// 
			// m_ToolBarButtonSingleClean
			// 
			this.m_ToolBarButtonSingleClean.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSingleClean.Enabled")));
			this.m_ToolBarButtonSingleClean.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSingleClean.ImageIndex")));
			this.m_ToolBarButtonSingleClean.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.m_ToolBarButtonSingleClean.Text = resources.GetString("m_ToolBarButtonSingleClean.Text");
			this.m_ToolBarButtonSingleClean.ToolTipText = resources.GetString("m_ToolBarButtonSingleClean.ToolTipText");
			this.m_ToolBarButtonSingleClean.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSingleClean.Visible")));
			// 
			// m_ToolBarButtonSep2
			// 
			this.m_ToolBarButtonSep2.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSep2.Enabled")));
			this.m_ToolBarButtonSep2.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSep2.ImageIndex")));
			this.m_ToolBarButtonSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.m_ToolBarButtonSep2.Text = resources.GetString("m_ToolBarButtonSep2.Text");
			this.m_ToolBarButtonSep2.ToolTipText = resources.GetString("m_ToolBarButtonSep2.ToolTipText");
			this.m_ToolBarButtonSep2.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSep2.Visible")));
			// 
			// m_ToolBarButtonLeft
			// 
			this.m_ToolBarButtonLeft.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonLeft.Enabled")));
			this.m_ToolBarButtonLeft.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonLeft.ImageIndex")));
			this.m_ToolBarButtonLeft.Text = resources.GetString("m_ToolBarButtonLeft.Text");
			this.m_ToolBarButtonLeft.ToolTipText = resources.GetString("m_ToolBarButtonLeft.ToolTipText");
			this.m_ToolBarButtonLeft.Visible = ((bool)(resources.GetObject("m_ToolBarButtonLeft.Visible")));
			// 
			// m_ToolBarButtonRight
			// 
			this.m_ToolBarButtonRight.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonRight.Enabled")));
			this.m_ToolBarButtonRight.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonRight.ImageIndex")));
			this.m_ToolBarButtonRight.Text = resources.GetString("m_ToolBarButtonRight.Text");
			this.m_ToolBarButtonRight.ToolTipText = resources.GetString("m_ToolBarButtonRight.ToolTipText");
			this.m_ToolBarButtonRight.Visible = ((bool)(resources.GetObject("m_ToolBarButtonRight.Visible")));
			// 
			// m_ToolBarButtonSetOrigin
			// 
			this.m_ToolBarButtonSetOrigin.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSetOrigin.Enabled")));
			this.m_ToolBarButtonSetOrigin.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSetOrigin.ImageIndex")));
			this.m_ToolBarButtonSetOrigin.Text = resources.GetString("m_ToolBarButtonSetOrigin.Text");
			this.m_ToolBarButtonSetOrigin.ToolTipText = resources.GetString("m_ToolBarButtonSetOrigin.ToolTipText");
			this.m_ToolBarButtonSetOrigin.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSetOrigin.Visible")));
			// 
			// m_ToolBarButtonForward
			// 
			this.m_ToolBarButtonForward.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonForward.Enabled")));
			this.m_ToolBarButtonForward.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonForward.ImageIndex")));
			this.m_ToolBarButtonForward.Text = resources.GetString("m_ToolBarButtonForward.Text");
			this.m_ToolBarButtonForward.ToolTipText = resources.GetString("m_ToolBarButtonForward.ToolTipText");
			this.m_ToolBarButtonForward.Visible = ((bool)(resources.GetObject("m_ToolBarButtonForward.Visible")));
			// 
			// m_ToolBarButtonBackward
			// 
			this.m_ToolBarButtonBackward.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonBackward.Enabled")));
			this.m_ToolBarButtonBackward.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonBackward.ImageIndex")));
			this.m_ToolBarButtonBackward.Text = resources.GetString("m_ToolBarButtonBackward.Text");
			this.m_ToolBarButtonBackward.ToolTipText = resources.GetString("m_ToolBarButtonBackward.ToolTipText");
			this.m_ToolBarButtonBackward.Visible = ((bool)(resources.GetObject("m_ToolBarButtonBackward.Visible")));
			// 
			// m_ToolBarButtonSetOriginY
			// 
			this.m_ToolBarButtonSetOriginY.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSetOriginY.Enabled")));
			this.m_ToolBarButtonSetOriginY.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSetOriginY.ImageIndex")));
			this.m_ToolBarButtonSetOriginY.Text = resources.GetString("m_ToolBarButtonSetOriginY.Text");
			this.m_ToolBarButtonSetOriginY.ToolTipText = resources.GetString("m_ToolBarButtonSetOriginY.ToolTipText");
			this.m_ToolBarButtonSetOriginY.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSetOriginY.Visible")));
			// 
			// m_ToolBarButtonDownZ
			// 
			this.m_ToolBarButtonDownZ.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonDownZ.Enabled")));
			this.m_ToolBarButtonDownZ.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonDownZ.ImageIndex")));
			this.m_ToolBarButtonDownZ.Text = resources.GetString("m_ToolBarButtonDownZ.Text");
			this.m_ToolBarButtonDownZ.ToolTipText = resources.GetString("m_ToolBarButtonDownZ.ToolTipText");
			this.m_ToolBarButtonDownZ.Visible = ((bool)(resources.GetObject("m_ToolBarButtonDownZ.Visible")));
			// 
			// m_ToolBarButtonUpZ
			// 
			this.m_ToolBarButtonUpZ.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonUpZ.Enabled")));
			this.m_ToolBarButtonUpZ.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonUpZ.ImageIndex")));
			this.m_ToolBarButtonUpZ.Text = resources.GetString("m_ToolBarButtonUpZ.Text");
			this.m_ToolBarButtonUpZ.ToolTipText = resources.GetString("m_ToolBarButtonUpZ.ToolTipText");
			this.m_ToolBarButtonUpZ.Visible = ((bool)(resources.GetObject("m_ToolBarButtonUpZ.Visible")));
			// 
			// m_ToolBarButtonSep3
			// 
			this.m_ToolBarButtonSep3.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonSep3.Enabled")));
			this.m_ToolBarButtonSep3.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonSep3.ImageIndex")));
			this.m_ToolBarButtonSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.m_ToolBarButtonSep3.Text = resources.GetString("m_ToolBarButtonSep3.Text");
			this.m_ToolBarButtonSep3.ToolTipText = resources.GetString("m_ToolBarButtonSep3.ToolTipText");
			this.m_ToolBarButtonSep3.Visible = ((bool)(resources.GetObject("m_ToolBarButtonSep3.Visible")));
			// 
			// m_ToolBarButtonGoHome
			// 
			this.m_ToolBarButtonGoHome.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonGoHome.Enabled")));
			this.m_ToolBarButtonGoHome.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonGoHome.ImageIndex")));
			this.m_ToolBarButtonGoHome.Text = resources.GetString("m_ToolBarButtonGoHome.Text");
			this.m_ToolBarButtonGoHome.ToolTipText = resources.GetString("m_ToolBarButtonGoHome.ToolTipText");
			this.m_ToolBarButtonGoHome.Visible = ((bool)(resources.GetObject("m_ToolBarButtonGoHome.Visible")));
			// 
			// m_ToolBarButtonGoHomeY
			// 
			this.m_ToolBarButtonGoHomeY.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonGoHomeY.Enabled")));
			this.m_ToolBarButtonGoHomeY.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonGoHomeY.ImageIndex")));
			this.m_ToolBarButtonGoHomeY.Text = resources.GetString("m_ToolBarButtonGoHomeY.Text");
			this.m_ToolBarButtonGoHomeY.ToolTipText = resources.GetString("m_ToolBarButtonGoHomeY.ToolTipText");
			this.m_ToolBarButtonGoHomeY.Visible = ((bool)(resources.GetObject("m_ToolBarButtonGoHomeY.Visible")));
			// 
			// m_ToolBarButtonGoHomeZ
			// 
			this.m_ToolBarButtonGoHomeZ.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonGoHomeZ.Enabled")));
			this.m_ToolBarButtonGoHomeZ.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonGoHomeZ.ImageIndex")));
			this.m_ToolBarButtonGoHomeZ.Text = resources.GetString("m_ToolBarButtonGoHomeZ.Text");
			this.m_ToolBarButtonGoHomeZ.ToolTipText = resources.GetString("m_ToolBarButtonGoHomeZ.ToolTipText");
			this.m_ToolBarButtonGoHomeZ.Visible = ((bool)(resources.GetObject("m_ToolBarButtonGoHomeZ.Visible")));
			// 
			// m_ToolBarButtonMeasurePaper
			// 
			this.m_ToolBarButtonMeasurePaper.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonMeasurePaper.Enabled")));
			this.m_ToolBarButtonMeasurePaper.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonMeasurePaper.ImageIndex")));
			this.m_ToolBarButtonMeasurePaper.Text = resources.GetString("m_ToolBarButtonMeasurePaper.Text");
			this.m_ToolBarButtonMeasurePaper.ToolTipText = resources.GetString("m_ToolBarButtonMeasurePaper.ToolTipText");
			this.m_ToolBarButtonMeasurePaper.Visible = ((bool)(resources.GetObject("m_ToolBarButtonMeasurePaper.Visible")));
			// 
			// m_ToolBarButtonEdit
			// 
			this.m_ToolBarButtonEdit.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonEdit.Enabled")));
			this.m_ToolBarButtonEdit.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonEdit.ImageIndex")));
			this.m_ToolBarButtonEdit.Text = resources.GetString("m_ToolBarButtonEdit.Text");
			this.m_ToolBarButtonEdit.ToolTipText = resources.GetString("m_ToolBarButtonEdit.ToolTipText");
			this.m_ToolBarButtonEdit.Visible = ((bool)(resources.GetObject("m_ToolBarButtonEdit.Visible")));
			// 
			// m_ToolbarImageList
			// 
			this.m_ToolbarImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.m_ToolbarImageList.ImageSize = ((System.Drawing.Size)(resources.GetObject("m_ToolbarImageList.ImageSize")));
			this.m_ToolbarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ToolbarImageList.ImageStream")));
			this.m_ToolbarImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// m_PanelToolBarSetting
			// 
			this.m_PanelToolBarSetting.AccessibleDescription = resources.GetString("m_PanelToolBarSetting.AccessibleDescription");
			this.m_PanelToolBarSetting.AccessibleName = resources.GetString("m_PanelToolBarSetting.AccessibleName");
			this.m_PanelToolBarSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PanelToolBarSetting.Anchor")));
			this.m_PanelToolBarSetting.AutoScroll = ((bool)(resources.GetObject("m_PanelToolBarSetting.AutoScroll")));
			this.m_PanelToolBarSetting.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PanelToolBarSetting.AutoScrollMargin")));
			this.m_PanelToolBarSetting.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PanelToolBarSetting.AutoScrollMinSize")));
			this.m_PanelToolBarSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PanelToolBarSetting.BackgroundImage")));
			this.m_PanelToolBarSetting.Controls.Add(this.m_ToolbarSetting);
			this.m_PanelToolBarSetting.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PanelToolBarSetting.Dock")));
			this.m_PanelToolBarSetting.Enabled = ((bool)(resources.GetObject("m_PanelToolBarSetting.Enabled")));
			this.m_PanelToolBarSetting.Font = ((System.Drawing.Font)(resources.GetObject("m_PanelToolBarSetting.Font")));
			this.m_PanelToolBarSetting.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PanelToolBarSetting.ImeMode")));
			this.m_PanelToolBarSetting.Location = ((System.Drawing.Point)(resources.GetObject("m_PanelToolBarSetting.Location")));
			this.m_PanelToolBarSetting.Name = "m_PanelToolBarSetting";
			this.m_PanelToolBarSetting.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PanelToolBarSetting.RightToLeft")));
			this.m_PanelToolBarSetting.Size = ((System.Drawing.Size)(resources.GetObject("m_PanelToolBarSetting.Size")));
			this.m_PanelToolBarSetting.TabIndex = ((int)(resources.GetObject("m_PanelToolBarSetting.TabIndex")));
			this.m_PanelToolBarSetting.Text = resources.GetString("m_PanelToolBarSetting.Text");
			this.m_PanelToolBarSetting.Visible = ((bool)(resources.GetObject("m_PanelToolBarSetting.Visible")));
			// 
			// m_ToolbarSetting
			// 
			this.m_ToolbarSetting.AccessibleDescription = resources.GetString("m_ToolbarSetting.AccessibleDescription");
			this.m_ToolbarSetting.AccessibleName = resources.GetString("m_ToolbarSetting.AccessibleName");
			this.m_ToolbarSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ToolbarSetting.Anchor")));
			this.m_ToolbarSetting.AutoScroll = ((bool)(resources.GetObject("m_ToolbarSetting.AutoScroll")));
			this.m_ToolbarSetting.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.AutoScrollMargin")));
			this.m_ToolbarSetting.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.AutoScrollMinSize")));
			this.m_ToolbarSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ToolbarSetting.BackgroundImage")));
			this.m_ToolbarSetting.Divider = false;
			this.m_ToolbarSetting.DividerSide = System.Windows.Forms.Border3DSide.Top;
			this.m_ToolbarSetting.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ToolbarSetting.Dock")));
			this.m_ToolbarSetting.Enabled = ((bool)(resources.GetObject("m_ToolbarSetting.Enabled")));
			this.m_ToolbarSetting.Font = ((System.Drawing.Font)(resources.GetObject("m_ToolbarSetting.Font")));
			this.m_ToolbarSetting.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.m_ToolbarSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.m_ToolbarSetting.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ToolbarSetting.ImeMode")));
			this.m_ToolbarSetting.Location = ((System.Drawing.Point)(resources.GetObject("m_ToolbarSetting.Location")));
			this.m_ToolbarSetting.Name = "m_ToolbarSetting";
			this.m_ToolbarSetting.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ToolbarSetting.RightToLeft")));
			this.m_ToolbarSetting.Size = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.Size")));
			this.m_ToolbarSetting.TabIndex = ((int)(resources.GetObject("m_ToolbarSetting.TabIndex")));
			this.m_ToolbarSetting.VerticalDirection = false;
			this.m_ToolbarSetting.Visible = ((bool)(resources.GetObject("m_ToolbarSetting.Visible")));
			// 
			// m_PanelJobList
			// 
			this.m_PanelJobList.AccessibleDescription = resources.GetString("m_PanelJobList.AccessibleDescription");
			this.m_PanelJobList.AccessibleName = resources.GetString("m_PanelJobList.AccessibleName");
			this.m_PanelJobList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PanelJobList.Anchor")));
			this.m_PanelJobList.AutoScroll = ((bool)(resources.GetObject("m_PanelJobList.AutoScroll")));
			this.m_PanelJobList.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PanelJobList.AutoScrollMargin")));
			this.m_PanelJobList.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PanelJobList.AutoScrollMinSize")));
			this.m_PanelJobList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PanelJobList.BackgroundImage")));
			this.m_PanelJobList.Controls.Add(this.m_SplitterJobList);
			this.m_PanelJobList.Controls.Add(this.m_WorkForlderpanel);
			this.m_PanelJobList.Controls.Add(this.m_PreviewAndInfo);
			this.m_PanelJobList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PanelJobList.Dock")));
			this.m_PanelJobList.Enabled = ((bool)(resources.GetObject("m_PanelJobList.Enabled")));
			this.m_PanelJobList.Font = ((System.Drawing.Font)(resources.GetObject("m_PanelJobList.Font")));
			this.m_PanelJobList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PanelJobList.ImeMode")));
			this.m_PanelJobList.Location = ((System.Drawing.Point)(resources.GetObject("m_PanelJobList.Location")));
			this.m_PanelJobList.Name = "m_PanelJobList";
			this.m_PanelJobList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PanelJobList.RightToLeft")));
			this.m_PanelJobList.Size = ((System.Drawing.Size)(resources.GetObject("m_PanelJobList.Size")));
			this.m_PanelJobList.TabIndex = ((int)(resources.GetObject("m_PanelJobList.TabIndex")));
			this.m_PanelJobList.Text = resources.GetString("m_PanelJobList.Text");
			this.m_PanelJobList.Visible = ((bool)(resources.GetObject("m_PanelJobList.Visible")));
			// 
			// m_SplitterJobList
			// 
			this.m_SplitterJobList.AccessibleDescription = resources.GetString("m_SplitterJobList.AccessibleDescription");
			this.m_SplitterJobList.AccessibleName = resources.GetString("m_SplitterJobList.AccessibleName");
			this.m_SplitterJobList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SplitterJobList.Anchor")));
			this.m_SplitterJobList.BackColor = System.Drawing.SystemColors.Control;
			this.m_SplitterJobList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SplitterJobList.BackgroundImage")));
			this.m_SplitterJobList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SplitterJobList.Dock")));
			this.m_SplitterJobList.Enabled = ((bool)(resources.GetObject("m_SplitterJobList.Enabled")));
			this.m_SplitterJobList.Font = ((System.Drawing.Font)(resources.GetObject("m_SplitterJobList.Font")));
			this.m_SplitterJobList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SplitterJobList.ImeMode")));
			this.m_SplitterJobList.Location = ((System.Drawing.Point)(resources.GetObject("m_SplitterJobList.Location")));
			this.m_SplitterJobList.MinExtra = ((int)(resources.GetObject("m_SplitterJobList.MinExtra")));
			this.m_SplitterJobList.MinSize = ((int)(resources.GetObject("m_SplitterJobList.MinSize")));
			this.m_SplitterJobList.Name = "m_SplitterJobList";
			this.m_SplitterJobList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SplitterJobList.RightToLeft")));
			this.m_SplitterJobList.Size = ((System.Drawing.Size)(resources.GetObject("m_SplitterJobList.Size")));
			this.m_SplitterJobList.TabIndex = ((int)(resources.GetObject("m_SplitterJobList.TabIndex")));
			this.m_SplitterJobList.TabStop = false;
			this.m_SplitterJobList.Visible = ((bool)(resources.GetObject("m_SplitterJobList.Visible")));
			// 
			// m_WorkForlderpanel
			// 
			this.m_WorkForlderpanel.AccessibleDescription = resources.GetString("m_WorkForlderpanel.AccessibleDescription");
			this.m_WorkForlderpanel.AccessibleName = resources.GetString("m_WorkForlderpanel.AccessibleName");
			this.m_WorkForlderpanel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WorkForlderpanel.Anchor")));
			this.m_WorkForlderpanel.AutoScroll = ((bool)(resources.GetObject("m_WorkForlderpanel.AutoScroll")));
			this.m_WorkForlderpanel.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_WorkForlderpanel.AutoScrollMargin")));
			this.m_WorkForlderpanel.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_WorkForlderpanel.AutoScrollMinSize")));
			this.m_WorkForlderpanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WorkForlderpanel.BackgroundImage")));
			this.m_WorkForlderpanel.Controls.Add(this.m_JobListForm);
			this.m_WorkForlderpanel.Controls.Add(this.m_WorkForldersplitter);
			this.m_WorkForlderpanel.Controls.Add(this.m_FolderPreview);
			this.m_WorkForlderpanel.Controls.Add(this.panelSubBar);
			this.m_WorkForlderpanel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WorkForlderpanel.Dock")));
			this.m_WorkForlderpanel.Enabled = ((bool)(resources.GetObject("m_WorkForlderpanel.Enabled")));
			this.m_WorkForlderpanel.Font = ((System.Drawing.Font)(resources.GetObject("m_WorkForlderpanel.Font")));
			this.m_WorkForlderpanel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WorkForlderpanel.ImeMode")));
			this.m_WorkForlderpanel.Location = ((System.Drawing.Point)(resources.GetObject("m_WorkForlderpanel.Location")));
			this.m_WorkForlderpanel.Name = "m_WorkForlderpanel";
			this.m_WorkForlderpanel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WorkForlderpanel.RightToLeft")));
			this.m_WorkForlderpanel.Size = ((System.Drawing.Size)(resources.GetObject("m_WorkForlderpanel.Size")));
			this.m_WorkForlderpanel.TabIndex = ((int)(resources.GetObject("m_WorkForlderpanel.TabIndex")));
			this.m_WorkForlderpanel.Text = resources.GetString("m_WorkForlderpanel.Text");
			this.m_WorkForlderpanel.Visible = ((bool)(resources.GetObject("m_WorkForlderpanel.Visible")));
			// 
			// m_JobListForm
			// 
			this.m_JobListForm.AccessibleDescription = resources.GetString("m_JobListForm.AccessibleDescription");
			this.m_JobListForm.AccessibleName = resources.GetString("m_JobListForm.AccessibleName");
			this.m_JobListForm.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_JobListForm.Anchor")));
			this.m_JobListForm.AutoScroll = ((bool)(resources.GetObject("m_JobListForm.AutoScroll")));
			this.m_JobListForm.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMargin")));
			this.m_JobListForm.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMinSize")));
			this.m_JobListForm.BackColor = System.Drawing.SystemColors.Control;
			this.m_JobListForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_JobListForm.BackgroundImage")));
			this.m_JobListForm.CustomRowBackColor = System.Drawing.Color.White;
			this.m_JobListForm.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_JobListForm.Dock")));
			this.m_JobListForm.Enabled = ((bool)(resources.GetObject("m_JobListForm.Enabled")));
			this.m_JobListForm.FolderName = "";
			this.m_JobListForm.Font = ((System.Drawing.Font)(resources.GetObject("m_JobListForm.Font")));
			this.m_JobListForm.ForeColor = System.Drawing.SystemColors.WindowText;
			this.m_JobListForm.GridLines = true;
			this.m_JobListForm.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_JobListForm.ImeMode")));
			this.m_JobListForm.Location = ((System.Drawing.Point)(resources.GetObject("m_JobListForm.Location")));
			this.m_JobListForm.mAlignment = System.Windows.Forms.ListViewAlignment.Top;
			this.m_JobListForm.Name = "m_JobListForm";
			this.m_JobListForm.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_JobListForm.RightToLeft")));
			this.m_JobListForm.Size = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.Size")));
			this.m_JobListForm.TabIndex = ((int)(resources.GetObject("m_JobListForm.TabIndex")));
			this.m_JobListForm.ThumbBorderColor = System.Drawing.Color.Wheat;
			this.m_JobListForm.ThumbNailSize = new System.Drawing.Size(95, 95);
			this.m_JobListForm.Visible = ((bool)(resources.GetObject("m_JobListForm.Visible")));
			// 
			// m_WorkForldersplitter
			// 
			this.m_WorkForldersplitter.AccessibleDescription = resources.GetString("m_WorkForldersplitter.AccessibleDescription");
			this.m_WorkForldersplitter.AccessibleName = resources.GetString("m_WorkForldersplitter.AccessibleName");
			this.m_WorkForldersplitter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_WorkForldersplitter.Anchor")));
			this.m_WorkForldersplitter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_WorkForldersplitter.BackgroundImage")));
			this.m_WorkForldersplitter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_WorkForldersplitter.Dock")));
			this.m_WorkForldersplitter.Enabled = ((bool)(resources.GetObject("m_WorkForldersplitter.Enabled")));
			this.m_WorkForldersplitter.Font = ((System.Drawing.Font)(resources.GetObject("m_WorkForldersplitter.Font")));
			this.m_WorkForldersplitter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_WorkForldersplitter.ImeMode")));
			this.m_WorkForldersplitter.Location = ((System.Drawing.Point)(resources.GetObject("m_WorkForldersplitter.Location")));
			this.m_WorkForldersplitter.MinExtra = ((int)(resources.GetObject("m_WorkForldersplitter.MinExtra")));
			this.m_WorkForldersplitter.MinSize = ((int)(resources.GetObject("m_WorkForldersplitter.MinSize")));
			this.m_WorkForldersplitter.Name = "m_WorkForldersplitter";
			this.m_WorkForldersplitter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_WorkForldersplitter.RightToLeft")));
			this.m_WorkForldersplitter.Size = ((System.Drawing.Size)(resources.GetObject("m_WorkForldersplitter.Size")));
			this.m_WorkForldersplitter.TabIndex = ((int)(resources.GetObject("m_WorkForldersplitter.TabIndex")));
			this.m_WorkForldersplitter.TabStop = false;
			this.m_WorkForldersplitter.Visible = ((bool)(resources.GetObject("m_WorkForldersplitter.Visible")));
			// 
			// m_FolderPreview
			// 
			this.m_FolderPreview.AccessibleDescription = resources.GetString("m_FolderPreview.AccessibleDescription");
			this.m_FolderPreview.AccessibleName = resources.GetString("m_FolderPreview.AccessibleName");
			this.m_FolderPreview.Alignment = ((System.Windows.Forms.ListViewAlignment)(resources.GetObject("m_FolderPreview.Alignment")));
			this.m_FolderPreview.AllowDrop = true;
			this.m_FolderPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_FolderPreview.Anchor")));
			this.m_FolderPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_FolderPreview.BackgroundImage")));
			this.m_FolderPreview.CanReLoad = true;
			this.m_FolderPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_FolderPreview.Dock")));
			this.m_FolderPreview.Enabled = ((bool)(resources.GetObject("m_FolderPreview.Enabled")));
			this.m_FolderPreview.FolderName = "";
			this.m_FolderPreview.Font = ((System.Drawing.Font)(resources.GetObject("m_FolderPreview.Font")));
			this.m_FolderPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_FolderPreview.ImeMode")));
			this.m_FolderPreview.LabelWrap = ((bool)(resources.GetObject("m_FolderPreview.LabelWrap")));
			this.m_FolderPreview.Location = ((System.Drawing.Point)(resources.GetObject("m_FolderPreview.Location")));
			this.m_FolderPreview.Name = "m_FolderPreview";
			this.m_FolderPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_FolderPreview.RightToLeft")));
			this.m_FolderPreview.Size = ((System.Drawing.Size)(resources.GetObject("m_FolderPreview.Size")));
			this.m_FolderPreview.TabIndex = ((int)(resources.GetObject("m_FolderPreview.TabIndex")));
			this.m_FolderPreview.Text = resources.GetString("m_FolderPreview.Text");
			this.m_FolderPreview.ThumbBorderColor = System.Drawing.Color.Wheat;
			this.m_FolderPreview.ThumbNailSize = 95;
			this.m_FolderPreview.Visible = ((bool)(resources.GetObject("m_FolderPreview.Visible")));
			this.m_FolderPreview.ItemActivate += new System.EventHandler(this.m_FolderPreview_ItemActivate);
			// 
			// panelSubBar
			// 
			this.panelSubBar.AccessibleDescription = resources.GetString("panelSubBar.AccessibleDescription");
			this.panelSubBar.AccessibleName = resources.GetString("panelSubBar.AccessibleName");
			this.panelSubBar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panelSubBar.Anchor")));
			this.panelSubBar.AutoScroll = ((bool)(resources.GetObject("panelSubBar.AutoScroll")));
			this.panelSubBar.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panelSubBar.AutoScrollMargin")));
			this.panelSubBar.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panelSubBar.AutoScrollMinSize")));
			this.panelSubBar.BackColor = System.Drawing.SystemColors.Control;
			this.panelSubBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelSubBar.BackgroundImage")));
			this.panelSubBar.Controls.Add(this.toolBar1);
			this.panelSubBar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panelSubBar.Dock")));
			this.panelSubBar.Enabled = ((bool)(resources.GetObject("panelSubBar.Enabled")));
			this.panelSubBar.Font = ((System.Drawing.Font)(resources.GetObject("panelSubBar.Font")));
			this.panelSubBar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panelSubBar.ImeMode")));
			this.panelSubBar.Location = ((System.Drawing.Point)(resources.GetObject("panelSubBar.Location")));
			this.panelSubBar.Name = "panelSubBar";
			this.panelSubBar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panelSubBar.RightToLeft")));
			this.panelSubBar.Size = ((System.Drawing.Size)(resources.GetObject("panelSubBar.Size")));
			this.panelSubBar.TabIndex = ((int)(resources.GetObject("panelSubBar.TabIndex")));
			this.panelSubBar.Text = resources.GetString("panelSubBar.Text");
			this.panelSubBar.Visible = ((bool)(resources.GetObject("panelSubBar.Visible")));
			// 
			// toolBar1
			// 
			this.toolBar1.AccessibleDescription = resources.GetString("toolBar1.AccessibleDescription");
			this.toolBar1.AccessibleName = resources.GetString("toolBar1.AccessibleName");
			this.toolBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("toolBar1.Anchor")));
			this.toolBar1.Appearance = ((System.Windows.Forms.ToolBarAppearance)(resources.GetObject("toolBar1.Appearance")));
			this.toolBar1.AutoSize = ((bool)(resources.GetObject("toolBar1.AutoSize")));
			this.toolBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolBar1.BackgroundImage")));
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolBarButtonWorkForlder,
																						this.toolBarButtonRefresh,
																						this.toolBarButtonAddToList,
																						this.toolBarButtonSelectAll});
			this.toolBar1.ButtonSize = ((System.Drawing.Size)(resources.GetObject("toolBar1.ButtonSize")));
			this.toolBar1.Divider = false;
			this.toolBar1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("toolBar1.Dock")));
			this.toolBar1.DropDownArrows = ((bool)(resources.GetObject("toolBar1.DropDownArrows")));
			this.toolBar1.Enabled = ((bool)(resources.GetObject("toolBar1.Enabled")));
			this.toolBar1.Font = ((System.Drawing.Font)(resources.GetObject("toolBar1.Font")));
			this.toolBar1.ImageList = this.SubToolBarimageList;
			this.toolBar1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("toolBar1.ImeMode")));
			this.toolBar1.Location = ((System.Drawing.Point)(resources.GetObject("toolBar1.Location")));
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("toolBar1.RightToLeft")));
			this.toolBar1.ShowToolTips = ((bool)(resources.GetObject("toolBar1.ShowToolTips")));
			this.toolBar1.Size = ((System.Drawing.Size)(resources.GetObject("toolBar1.Size")));
			this.toolBar1.TabIndex = ((int)(resources.GetObject("toolBar1.TabIndex")));
			this.toolBar1.TextAlign = ((System.Windows.Forms.ToolBarTextAlign)(resources.GetObject("toolBar1.TextAlign")));
			this.toolBar1.Visible = ((bool)(resources.GetObject("toolBar1.Visible")));
			this.toolBar1.Wrappable = ((bool)(resources.GetObject("toolBar1.Wrappable")));
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarButtonWorkForlder
			// 
			this.toolBarButtonWorkForlder.Enabled = ((bool)(resources.GetObject("toolBarButtonWorkForlder.Enabled")));
			this.toolBarButtonWorkForlder.ImageIndex = ((int)(resources.GetObject("toolBarButtonWorkForlder.ImageIndex")));
			this.toolBarButtonWorkForlder.Text = resources.GetString("toolBarButtonWorkForlder.Text");
			this.toolBarButtonWorkForlder.ToolTipText = resources.GetString("toolBarButtonWorkForlder.ToolTipText");
			this.toolBarButtonWorkForlder.Visible = ((bool)(resources.GetObject("toolBarButtonWorkForlder.Visible")));
			// 
			// toolBarButtonRefresh
			// 
			this.toolBarButtonRefresh.Enabled = ((bool)(resources.GetObject("toolBarButtonRefresh.Enabled")));
			this.toolBarButtonRefresh.ImageIndex = ((int)(resources.GetObject("toolBarButtonRefresh.ImageIndex")));
			this.toolBarButtonRefresh.Text = resources.GetString("toolBarButtonRefresh.Text");
			this.toolBarButtonRefresh.ToolTipText = resources.GetString("toolBarButtonRefresh.ToolTipText");
			this.toolBarButtonRefresh.Visible = ((bool)(resources.GetObject("toolBarButtonRefresh.Visible")));
			// 
			// toolBarButtonAddToList
			// 
			this.toolBarButtonAddToList.Enabled = ((bool)(resources.GetObject("toolBarButtonAddToList.Enabled")));
			this.toolBarButtonAddToList.ImageIndex = ((int)(resources.GetObject("toolBarButtonAddToList.ImageIndex")));
			this.toolBarButtonAddToList.Text = resources.GetString("toolBarButtonAddToList.Text");
			this.toolBarButtonAddToList.ToolTipText = resources.GetString("toolBarButtonAddToList.ToolTipText");
			this.toolBarButtonAddToList.Visible = ((bool)(resources.GetObject("toolBarButtonAddToList.Visible")));
			// 
			// toolBarButtonSelectAll
			// 
			this.toolBarButtonSelectAll.Enabled = ((bool)(resources.GetObject("toolBarButtonSelectAll.Enabled")));
			this.toolBarButtonSelectAll.ImageIndex = ((int)(resources.GetObject("toolBarButtonSelectAll.ImageIndex")));
			this.toolBarButtonSelectAll.Text = resources.GetString("toolBarButtonSelectAll.Text");
			this.toolBarButtonSelectAll.ToolTipText = resources.GetString("toolBarButtonSelectAll.ToolTipText");
			this.toolBarButtonSelectAll.Visible = ((bool)(resources.GetObject("toolBarButtonSelectAll.Visible")));
			// 
			// SubToolBarimageList
			// 
			this.SubToolBarimageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.SubToolBarimageList.ImageSize = ((System.Drawing.Size)(resources.GetObject("SubToolBarimageList.ImageSize")));
			this.SubToolBarimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SubToolBarimageList.ImageStream")));
			this.SubToolBarimageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// m_PreviewAndInfo
			// 
			this.m_PreviewAndInfo.AccessibleDescription = resources.GetString("m_PreviewAndInfo.AccessibleDescription");
			this.m_PreviewAndInfo.AccessibleName = resources.GetString("m_PreviewAndInfo.AccessibleName");
			this.m_PreviewAndInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PreviewAndInfo.Anchor")));
			this.m_PreviewAndInfo.AutoScroll = ((bool)(resources.GetObject("m_PreviewAndInfo.AutoScroll")));
			this.m_PreviewAndInfo.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.AutoScrollMargin")));
			this.m_PreviewAndInfo.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.AutoScrollMinSize")));
			this.m_PreviewAndInfo.BackColor = System.Drawing.SystemColors.Control;
			this.m_PreviewAndInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PreviewAndInfo.BackgroundImage")));
			this.m_PreviewAndInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PreviewAndInfo.Dock")));
			this.m_PreviewAndInfo.Enabled = ((bool)(resources.GetObject("m_PreviewAndInfo.Enabled")));
			this.m_PreviewAndInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_PreviewAndInfo.Font")));
			this.m_PreviewAndInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PreviewAndInfo.ImeMode")));
			this.m_PreviewAndInfo.LableVisble = true;
			this.m_PreviewAndInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_PreviewAndInfo.Location")));
			this.m_PreviewAndInfo.Name = "m_PreviewAndInfo";
			this.m_PreviewAndInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PreviewAndInfo.RightToLeft")));
			this.m_PreviewAndInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.Size")));
			this.m_PreviewAndInfo.TabIndex = ((int)(resources.GetObject("m_PreviewAndInfo.TabIndex")));
			this.m_PreviewAndInfo.Visible = ((bool)(resources.GetObject("m_PreviewAndInfo.Visible")));
			// 
			// MenuFolderPreview
			// 
			this.MenuFolderPreview.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.menuItemAddToList,
																							  this.menuItem2,
																							  this.menuItemReLoad,
																							  this.menuItem4,
																							  this.menuItemSelectAll});
			this.MenuFolderPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("MenuFolderPreview.RightToLeft")));
			this.MenuFolderPreview.Popup += new System.EventHandler(this.MenuFolderPreview_Popup);
			// 
			// menuItemAddToList
			// 
			this.menuItemAddToList.Enabled = ((bool)(resources.GetObject("menuItemAddToList.Enabled")));
			this.menuItemAddToList.Index = 0;
			this.menuItemAddToList.OwnerDraw = true;
			this.menuItemAddToList.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemAddToList.Shortcut")));
			this.menuItemAddToList.ShowShortcut = ((bool)(resources.GetObject("menuItemAddToList.ShowShortcut")));
			this.menuItemAddToList.Text = resources.GetString("menuItemAddToList.Text");
			this.menuItemAddToList.Visible = ((bool)(resources.GetObject("menuItemAddToList.Visible")));
			this.menuItemAddToList.Click += new System.EventHandler(this.menuItemAddToList_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 1;
			this.menuItem2.OwnerDraw = true;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			// 
			// menuItemReLoad
			// 
			this.menuItemReLoad.Enabled = ((bool)(resources.GetObject("menuItemReLoad.Enabled")));
			this.menuItemReLoad.Index = 2;
			this.menuItemReLoad.OwnerDraw = true;
			this.menuItemReLoad.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemReLoad.Shortcut")));
			this.menuItemReLoad.ShowShortcut = ((bool)(resources.GetObject("menuItemReLoad.ShowShortcut")));
			this.menuItemReLoad.Text = resources.GetString("menuItemReLoad.Text");
			this.menuItemReLoad.Visible = ((bool)(resources.GetObject("menuItemReLoad.Visible")));
			this.menuItemReLoad.Click += new System.EventHandler(this.menuItemReLoad_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = ((bool)(resources.GetObject("menuItem4.Enabled")));
			this.menuItem4.Index = 3;
			this.menuItem4.OwnerDraw = true;
			this.menuItem4.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem4.Shortcut")));
			this.menuItem4.ShowShortcut = ((bool)(resources.GetObject("menuItem4.ShowShortcut")));
			this.menuItem4.Text = resources.GetString("menuItem4.Text");
			this.menuItem4.Visible = ((bool)(resources.GetObject("menuItem4.Visible")));
			// 
			// menuItemSelectAll
			// 
			this.menuItemSelectAll.Enabled = ((bool)(resources.GetObject("menuItemSelectAll.Enabled")));
			this.menuItemSelectAll.Index = 4;
			this.menuItemSelectAll.OwnerDraw = true;
			this.menuItemSelectAll.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemSelectAll.Shortcut")));
			this.menuItemSelectAll.ShowShortcut = ((bool)(resources.GetObject("menuItemSelectAll.ShowShortcut")));
			this.menuItemSelectAll.Text = resources.GetString("menuItemSelectAll.Text");
			this.menuItemSelectAll.Visible = ((bool)(resources.GetObject("menuItemSelectAll.Visible")));
			this.menuItemSelectAll.Click += new System.EventHandler(this.menuItemSelectAll_Click);
			// 
			// m_SplitterStatus
			// 
			this.m_SplitterStatus.AccessibleDescription = resources.GetString("m_SplitterStatus.AccessibleDescription");
			this.m_SplitterStatus.AccessibleName = resources.GetString("m_SplitterStatus.AccessibleName");
			this.m_SplitterStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SplitterStatus.Anchor")));
			this.m_SplitterStatus.BackColor = System.Drawing.SystemColors.Control;
			this.m_SplitterStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SplitterStatus.BackgroundImage")));
			this.m_SplitterStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_SplitterStatus.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SplitterStatus.Dock")));
			this.m_SplitterStatus.Enabled = ((bool)(resources.GetObject("m_SplitterStatus.Enabled")));
			this.m_SplitterStatus.Font = ((System.Drawing.Font)(resources.GetObject("m_SplitterStatus.Font")));
			this.m_SplitterStatus.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SplitterStatus.ImeMode")));
			this.m_SplitterStatus.Location = ((System.Drawing.Point)(resources.GetObject("m_SplitterStatus.Location")));
			this.m_SplitterStatus.MinExtra = ((int)(resources.GetObject("m_SplitterStatus.MinExtra")));
			this.m_SplitterStatus.MinSize = ((int)(resources.GetObject("m_SplitterStatus.MinSize")));
			this.m_SplitterStatus.Name = "m_SplitterStatus";
			this.m_SplitterStatus.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SplitterStatus.RightToLeft")));
			this.m_SplitterStatus.Size = ((System.Drawing.Size)(resources.GetObject("m_SplitterStatus.Size")));
			this.m_SplitterStatus.TabIndex = ((int)(resources.GetObject("m_SplitterStatus.TabIndex")));
			this.m_SplitterStatus.TabStop = false;
			this.m_SplitterStatus.Visible = ((bool)(resources.GetObject("m_SplitterStatus.Visible")));
			// 
			// m_SplitterToolbar
			// 
			this.m_SplitterToolbar.AccessibleDescription = resources.GetString("m_SplitterToolbar.AccessibleDescription");
			this.m_SplitterToolbar.AccessibleName = resources.GetString("m_SplitterToolbar.AccessibleName");
			this.m_SplitterToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SplitterToolbar.Anchor")));
			this.m_SplitterToolbar.BackColor = System.Drawing.SystemColors.Control;
			this.m_SplitterToolbar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SplitterToolbar.BackgroundImage")));
			this.m_SplitterToolbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_SplitterToolbar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SplitterToolbar.Dock")));
			this.m_SplitterToolbar.Enabled = ((bool)(resources.GetObject("m_SplitterToolbar.Enabled")));
			this.m_SplitterToolbar.Font = ((System.Drawing.Font)(resources.GetObject("m_SplitterToolbar.Font")));
			this.m_SplitterToolbar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SplitterToolbar.ImeMode")));
			this.m_SplitterToolbar.Location = ((System.Drawing.Point)(resources.GetObject("m_SplitterToolbar.Location")));
			this.m_SplitterToolbar.MinExtra = ((int)(resources.GetObject("m_SplitterToolbar.MinExtra")));
			this.m_SplitterToolbar.MinSize = ((int)(resources.GetObject("m_SplitterToolbar.MinSize")));
			this.m_SplitterToolbar.Name = "m_SplitterToolbar";
			this.m_SplitterToolbar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SplitterToolbar.RightToLeft")));
			this.m_SplitterToolbar.Size = ((System.Drawing.Size)(resources.GetObject("m_SplitterToolbar.Size")));
			this.m_SplitterToolbar.TabIndex = ((int)(resources.GetObject("m_SplitterToolbar.TabIndex")));
			this.m_SplitterToolbar.TabStop = false;
			this.m_SplitterToolbar.Visible = ((bool)(resources.GetObject("m_SplitterToolbar.Visible")));
			// 
			// m_StatusBarPanelJetStaus
			// 
			this.m_StatusBarPanelJetStaus.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelJetStaus.Alignment")));
			this.m_StatusBarPanelJetStaus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelJetStaus.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelJetStaus.Icon")));
			this.m_StatusBarPanelJetStaus.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelJetStaus.MinWidth")));
			this.m_StatusBarPanelJetStaus.Text = resources.GetString("m_StatusBarPanelJetStaus.Text");
			this.m_StatusBarPanelJetStaus.ToolTipText = resources.GetString("m_StatusBarPanelJetStaus.ToolTipText");
			this.m_StatusBarPanelJetStaus.Width = ((int)(resources.GetObject("m_StatusBarPanelJetStaus.Width")));
			// 
			// m_StatusBarPanelError
			// 
			this.m_StatusBarPanelError.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelError.Alignment")));
			this.m_StatusBarPanelError.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelError.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelError.Icon")));
			this.m_StatusBarPanelError.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelError.MinWidth")));
			this.m_StatusBarPanelError.Text = resources.GetString("m_StatusBarPanelError.Text");
			this.m_StatusBarPanelError.ToolTipText = resources.GetString("m_StatusBarPanelError.ToolTipText");
			this.m_StatusBarPanelError.Width = ((int)(resources.GetObject("m_StatusBarPanelError.Width")));
			// 
			// m_StatusBarPanelPercent
			// 
			this.m_StatusBarPanelPercent.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelPercent.Alignment")));
			this.m_StatusBarPanelPercent.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelPercent.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelPercent.Icon")));
			this.m_StatusBarPanelPercent.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelPercent.MinWidth")));
			this.m_StatusBarPanelPercent.Text = resources.GetString("m_StatusBarPanelPercent.Text");
			this.m_StatusBarPanelPercent.ToolTipText = resources.GetString("m_StatusBarPanelPercent.ToolTipText");
			this.m_StatusBarPanelPercent.Width = ((int)(resources.GetObject("m_StatusBarPanelPercent.Width")));
			// 
			// m_StatusBarPanelComment
			// 
			this.m_StatusBarPanelComment.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelComment.Alignment")));
			this.m_StatusBarPanelComment.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.m_StatusBarPanelComment.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelComment.Icon")));
			this.m_StatusBarPanelComment.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelComment.MinWidth")));
			this.m_StatusBarPanelComment.Text = resources.GetString("m_StatusBarPanelComment.Text");
			this.m_StatusBarPanelComment.ToolTipText = resources.GetString("m_StatusBarPanelComment.ToolTipText");
			this.m_StatusBarPanelComment.Width = ((int)(resources.GetObject("m_StatusBarPanelComment.Width")));
			// 
			// m_StatusBarApp
			// 
			this.m_StatusBarApp.AccessibleDescription = resources.GetString("m_StatusBarApp.AccessibleDescription");
			this.m_StatusBarApp.AccessibleName = resources.GetString("m_StatusBarApp.AccessibleName");
			this.m_StatusBarApp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_StatusBarApp.Anchor")));
			this.m_StatusBarApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_StatusBarApp.BackgroundImage")));
			this.m_StatusBarApp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_StatusBarApp.Dock")));
			this.m_StatusBarApp.Enabled = ((bool)(resources.GetObject("m_StatusBarApp.Enabled")));
			this.m_StatusBarApp.Font = ((System.Drawing.Font)(resources.GetObject("m_StatusBarApp.Font")));
			this.m_StatusBarApp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_StatusBarApp.ImeMode")));
			this.m_StatusBarApp.Location = ((System.Drawing.Point)(resources.GetObject("m_StatusBarApp.Location")));
			this.m_StatusBarApp.Name = "m_StatusBarApp";
			this.m_StatusBarApp.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																							  this.m_StatusBarPanelJetStaus,
																							  this.m_StatusBarPanelError,
																							  this.m_StatusBarPanelPercent,
																							  this.m_StatusBarPanelComment});
			this.m_StatusBarApp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_StatusBarApp.RightToLeft")));
			this.m_StatusBarApp.ShowPanels = true;
			this.m_StatusBarApp.Size = ((System.Drawing.Size)(resources.GetObject("m_StatusBarApp.Size")));
			this.m_StatusBarApp.TabIndex = ((int)(resources.GetObject("m_StatusBarApp.TabIndex")));
			this.m_StatusBarApp.Text = resources.GetString("m_StatusBarApp.Text");
			this.m_StatusBarApp.Visible = ((bool)(resources.GetObject("m_StatusBarApp.Visible")));
			// 
			// m_PrintInformation
			// 
			this.m_PrintInformation.AccessibleDescription = resources.GetString("m_PrintInformation.AccessibleDescription");
			this.m_PrintInformation.AccessibleName = resources.GetString("m_PrintInformation.AccessibleName");
			this.m_PrintInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PrintInformation.Anchor")));
			this.m_PrintInformation.AutoScroll = ((bool)(resources.GetObject("m_PrintInformation.AutoScroll")));
			this.m_PrintInformation.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PrintInformation.AutoScrollMargin")));
			this.m_PrintInformation.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PrintInformation.AutoScrollMinSize")));
			this.m_PrintInformation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PrintInformation.BackgroundImage")));
			this.m_PrintInformation.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PrintInformation.Dock")));
			this.m_PrintInformation.Enabled = ((bool)(resources.GetObject("m_PrintInformation.Enabled")));
			this.m_PrintInformation.Font = ((System.Drawing.Font)(resources.GetObject("m_PrintInformation.Font")));
			this.m_PrintInformation.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PrintInformation.ImeMode")));
			this.m_PrintInformation.Location = ((System.Drawing.Point)(resources.GetObject("m_PrintInformation.Location")));
			this.m_PrintInformation.Name = "m_PrintInformation";
			this.m_PrintInformation.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PrintInformation.RightToLeft")));
			this.m_PrintInformation.Size = ((System.Drawing.Size)(resources.GetObject("m_PrintInformation.Size")));
			this.m_PrintInformation.TabIndex = ((int)(resources.GetObject("m_PrintInformation.TabIndex")));
			this.m_PrintInformation.Visible = ((bool)(resources.GetObject("m_PrintInformation.Visible")));
			this.m_PrintInformation.StartButtonClicked += new System.EventHandler(this.m_PrintInformation_StartButtonClicked);
			// 
			// UIIconImageList
			// 
			this.UIIconImageList.ImageSize = ((System.Drawing.Size)(resources.GetObject("UIIconImageList.ImageSize")));
			this.UIIconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UIIconImageList.ImageStream")));
			this.UIIconImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// m_StartMenu
			// 
			this.m_StartMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_StartMenu.RightToLeft")));
			// 
			// imageListMenu
			// 
			this.imageListMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageListMenu.ImageSize = ((System.Drawing.Size)(resources.GetObject("imageListMenu.ImageSize")));
			this.imageListMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMenu.ImageStream")));
			this.imageListMenu.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// officeMenus1
			// 
			this.officeMenus1.ImageList = this.imageListMenu;
			// 
			// notifyIconBYHX
			// 
			this.notifyIconBYHX.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconBYHX.Icon")));
			this.notifyIconBYHX.Text = resources.GetString("notifyIconBYHX.Text");
			this.notifyIconBYHX.Visible = ((bool)(resources.GetObject("notifyIconBYHX.Visible")));
			this.notifyIconBYHX.DoubleClick += new System.EventHandler(this.notifyIconBYHX_DoubleClick);
			// 
			// notifyIconImageList
			// 
			this.notifyIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.notifyIconImageList.ImageSize = ((System.Drawing.Size)(resources.GetObject("notifyIconImageList.ImageSize")));
			this.notifyIconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("notifyIconImageList.ImageStream")));
			this.notifyIconImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.m_SplitterStatus);
			this.Controls.Add(this.m_PanelJobList);
			this.Controls.Add(this.m_PanelToolBarSetting);
			this.Controls.Add(this.m_SplitterToolbar);
			this.Controls.Add(this.m_StatusBarApp);
			this.Controls.Add(this.m_ToolBarCommand);
			this.Controls.Add(this.m_PrintInformation);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.Menu = this.m_MainMenu;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "MainForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainApp_Closing);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.m_PanelToolBarSetting.ResumeLayout(false);
			this.m_PanelJobList.ResumeLayout(false);
			this.m_WorkForlderpanel.ResumeLayout(false);
			this.panelSubBar.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region 接口实现

		# region IPrinterChange 接口成员实现

		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
			SetPrinterStatusChanged(status);
			if(status == JetStatusEnum.Error)
			{
				int errorcode = CoreInterface.GetBoardError();
				OnErrorCodeChanged(errorcode);
#if false
				int errorCode = CoreInterface.GetBoardError();
				SErrorCode sErrorCode= new SErrorCode(errorCode);
				if(SErrorCode.IsOnlyPauseError(errorCode))
				{
					string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);

					if(MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation) == DialogResult.Retry)
					{
						CoreInterface.Printer_Resume();
					}
				}
#endif
			}
			else
				OnErrorCodeChanged(0);
			m_ToolbarSetting.OnPrinterStatusChanged(status);    
            m_PreviewAndInfo.OnPrinterStatusChanged(status);  
			m_JobListForm.OnPrinterStatusChanged(status);
			if(status == JetStatusEnum.PowerOff)
				m_bFirstReady = false;
			if(m_bFirstReady == false && status == JetStatusEnum.Ready)
			{
				m_bFirstReady = true;
#if !LIYUUSB
				string vol = ResString.GetResString("FW_Voltage");
				byte [] buffer = System.Text.Encoding.Unicode.GetBytes(vol);
				int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
				CoreInterface.SetFWVoltage(buffer,buffer.Length,lcid);

				///Check version
				///

				//				SBoardInfo sBoardInfo = new SBoardInfo();
				//				if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
				//				{
				//					const int MINI_MAINBOARD_VERSION = 0x00020200;
				//					SFWVersion fwv= new SFWVersion(sBoardInfo.m_nBoradVersion);
				//					SFWVersion min_fwv= new SFWVersion(MINI_MAINBOARD_VERSION);
				//					if( (((fwv.m_nMainVersion <<8)+ fwv.m_nSubVersion)<<8) < MINI_MAINBOARD_VERSION)
				//					{
				//						string info = "";
				//						string mPrintingFormat = SErrorCode.GetEnumDisplayName(typeof(Software),Software.VersionNoMatch);
				//						string curVersion = fwv.m_nMainVersion + "." + fwv.m_nSubVersion;
				//						string minVersion = min_fwv.m_nMainVersion + "." + min_fwv.m_nSubVersion;
				//						info += "\n" + string.Format(mPrintingFormat,curVersion,minVersion);
				//						MessageBox.Show(this, info,"",MessageBoxButtons.OK);
				//						//m_bExitAtStart = true;
				//					}
				//				}
#else
				CoreInterface.VerifyHeadType();
				//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
				BYHXSoftLock.m_DongleKeyAlarm.FirstReadyShakeHand();
				//				if(SPrinterProperty.EPSONLCD_DEFINED)
				//				{
				//					this.OnPrinterSettingChange(m_allParam.PrinterSetting);
				//				}
				this.ShowMeasureQuestionForm(this.Visible);

				bool m_bNewXaar382 = SPrinterProperty.IsNewXaar382(m_allParam.PrinterProperty.ePrinterHead);
				if(m_bNewXaar382)
				{
					SRealTimeCurrentInfo_382 m_SRT_382 = new SRealTimeCurrentInfo_382();
					float minnum = 0,maxnum = 100;// this.numericUpDownTargetTemp.Minimum;this.numericUpDownTargetTemp.Maximum
					if(CoreInterface.Get382RealTimeInfo(ref m_SRT_382)!=0)
					{
						int	m_HeadNum = m_allParam.PrinterProperty.nHeadNum;

						bool bTargetTempError = false;
						for(int i = 0; i < m_HeadNum; i ++)
						{									
							float targetTemp = m_SRT_382.cTargetTemp[i];
							if(targetTemp < minnum || targetTemp > maxnum)
							{
								m_SRT_382.cTargetTemp[i] = 40;
								bTargetTempError = true;
							}
						}
						if(bTargetTempError)
						{
							CoreInterface.Set382RealTimeInfo(ref m_SRT_382);
						}
					}
				}
			}
			UpdateButtonStates(status);
		}
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_allParam.PrinterProperty = sp;
			m_ToolbarSetting.OnPrinterPropertyChange(sp);
			m_PreviewAndInfo.OnPrinterPropertyChange(sp);
			m_JobListForm.OnPrinterPropertyChange(sp);
			OnPrinterPropertyChange_Toolbar(sp);
			if(m_FuncSettingForm != null)
				m_FuncSettingForm.OnPrinterPropertyChange(sp);

			bool bfac = PubFunc.IsFactoryUser();
			if(bfac)
				m_MenuItemDebug.Visible = true;
			else
				m_MenuItemDebug.Visible = false;
			bool bNoRealPage = 
				(
				(sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_80W)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_40W)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_126)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_500)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_35W)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_70W)
				//				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl)
				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_1001_GS6)
				|| (sp.ePrinterHead == PrinterHeadEnum.EGen5)
				);
			if(bNoRealPage)
			{
				this.m_MenuItemRealTime.Visible = false;
			}
			else
				this.m_MenuItemRealTime.Visible = true;

			this.m_MenuItemDemoPage.Visible = this.m_MenuItemHWSetting.Visible = !SPrinterProperty.IsEpson(sp.ePrinterHead);

			this.m_MenuItemSave.Visible = this.m_MenuItemSaveToPrinter.Visible =
				this.m_MenuItemLoad.Visible = this.m_MenuItemLoadFromPrinter.Visible = !SPrinterProperty.EPSONLCD_DEFINED;
			this.m_MenuItemCalibraion.Visible = !SPrinterProperty.EPSONLCD_DEFINED || PubFunc.IsFactoryUser();

			if(!sp.bSupportUV)
			{
				this.m_MenuItemUVSetting.Visible = false;
			}
			else
				this.m_MenuItemUVSetting.Visible = true;

			this.UpdateFormHeaderText();

			TransformMeasurePaperIcon();
		}
		private bool m_bMeasurePaperIconTransformed = false;
		private void TransformMeasurePaperIcon()
		{
			// 如果支持Z轴测量，则主界面介质测量按钮为测介质高度。旋转图标以匹配功能语义
			if(!SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead) 
				&& m_allParam.PrinterProperty.IsAllWinZMeasurSensorSupport() && !m_bMeasurePaperIconTransformed)
			{
				Image img = this.m_ToolbarImageList.Images[16];
				Image temp = new Bitmap(img.Width,img.Height);
				Graphics gra = Graphics.FromImage(temp);
				gra.Transform = new Matrix(0, 1,-1, 0,temp.Width, 0); //旋转90度
				gra.DrawImage(img,0,0);
				gra.Dispose();
				this.m_ToolbarImageList.Images[16] = temp;

				this.m_ToolBarButtonMeasurePaper.ToolTipText = ResString.GetResString("UiToolBar_MeasurePaperHeight");
				m_bMeasurePaperIconTransformed = true;
				this.m_ToolBarCommand.Refresh();
			}
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_allParam.PrinterSetting = ss;
			m_ToolbarSetting.OnPrinterSettingChange(ss,this.m_allParam.EpsonAllParam);
			m_PreviewAndInfo.OnPrinterSettingChange(ss,this.m_allParam.EpsonAllParam);
			m_JobListForm.OnPrinterSettingChange(ss);
			if(m_FuncSettingForm != null)
				m_FuncSettingForm.OnPrinterSettingChange(ss,m_allParam.EpsonAllParam);
		}
		public void OnPowerONGetSettingsForEpson()
		{
			if(SPrinterProperty.EPSONLCD_DEFINED)
			{
				//从FW读取epson的各项参数,初始化m_epsinAllParam
				EpsonLCD.GetCalibrationSetting(ref this.m_allParam.PrinterSetting.sCalibrationSetting,false);
				EpsonLCD.GetCaliConfig(ref m_allParam.EpsonAllParam.sCaliConfig);
				EpsonLCD.GetCleaningOption(ref m_allParam.EpsonAllParam.sCLEANPARA);
				EpsonLCD.GetEPR_FactoryData_Ex(ref m_allParam.EpsonAllParam.sEPR_FactoryData_Ex);
				EpsonLCD.GetHeadparameter(ref m_allParam.EpsonAllParam.headParameterPercent);
				EpsonLCD.GetMainUI_Param(ref m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
				EpsonLCD.GetMedia_Info(ref m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
				EpsonLCD.GetPrint_Quality(ref m_allParam.EpsonAllParam.sUSB_Print_Quality);
				switch(this.m_allParam.EpsonAllParam.sUSB_Print_Quality.PrintQuality)
				{
					case (byte)EpsonFeatherType.None:
						this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 0;
						break;
					case (byte)EpsonFeatherType.Small:
						this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 20;
						break;
					case (byte)EpsonFeatherType.Medium:
						this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 40;
						break;
					case (byte)EpsonFeatherType.Large:
						this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 100;
						break;
				}
				this.m_allParam.PrinterSetting.sBaseSetting.fPaperWidth = m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaWidth;
				this.m_allParam.PrinterSetting.sFrequencySetting.fXOrigin = m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin;

				CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
				LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"OnPowerONGetSettingsForEpson");
			}

		}
		public void OnPreferenceChange( UIPreference up)
		{
			m_allParam.Preference = up;
			m_ToolbarSetting.OnPreferenceChange(up);
			m_PreviewAndInfo.OnPreferenceChange(up);
			m_JobListForm.OnPreferenceChange(up);
			UpdateViewMode(m_allParam.Preference.ViewModeIndex);
		}

		public bool LoadJobList()
		{
			return m_JobListForm.LoadJobList();
		}
		public bool SaveJobList()
		{
			return m_JobListForm.SaveJobList();
		}
		public AllParam GetAllParam()
		{
			return m_allParam;
		}

		public void NotifyUIParamChanged()
		{
#if INKWIN_UI
			m_PreviewAndInfo.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
			m_PreviewAndInfo.OnGetPreference(ref m_allParam.Preference);
#else
			m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
			m_ToolbarSetting.OnGetPreference(ref m_allParam.Preference);
#endif
			CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
			LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"NotifyUIParamChanged");
			if(SPrinterProperty.EPSONLCD_DEFINED)
				EpsonLCD.SetMainUI_Param(this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
		}

		public void NotifyUIKeyDownAndUp(Keys keyData,bool bKeydown)
		{
			if(bKeydown)
			{
				MainForm_KeyDownEvent(keyData);
			}
			else
			{
				MainForm_KeyUpEvent(keyData);
			}
		}

		public void OnSwitchPreview()
		{
			m_JobListForm.OnSwitchPreview();
		}

		public void NotifyUICalibrationExit(bool bSave)
		{
			m_MenuItemCalibraion.Enabled = true;
			if(bSave)
			{
				//OnPrinterSettingChange(m_allParam.PrinterSetting);
#if INKWIN_UI
				m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting,this.m_allParam.EpsonAllParam);
#else
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting,this.m_allParam.EpsonAllParam);
#endif
			}
			m_wizard = null;
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			OnPrinterStatusChanged(printerStatus);
		}
		#endregion

		#region IMessageFilter 接口成员实现

		public   bool   PreFilterMessage(ref   Message   m)　     
		{
			//			if(m.Msg   ==   0x020A)    //MouseWheel
			//				return   true;   
			//   TODO:     添加   comboNoWheel.PreFilterMessage   实现   
			return   false;   

		}
		#endregion

		#endregion

		#region 方法

		#region private
		private void StartWithInitComponent()
		{
			m_JobListForm.SetPreviewInfo(m_PreviewAndInfo);
			m_allParam = new AllParam();



			m_JobListForm.SetPrinterChange(this);
			m_ToolbarSetting.SetPrinterChange(this);
			m_PreviewAndInfo.SetPrinterChange(this);

		}

		private void UpdateButtonStates(JetStatusEnum status)
		{
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
                status = JetStatusEnum.PowerOff;

			if( status == JetStatusEnum.Error && SErrorCode.IsWarningError(CoreInterface.GetBoardError()))
			{
				return;
			}
		
			PrinterOperate printerOperate =  PrinterOperate.UpdateByPrinterStatus(status);
			switch(status)
			{
				case JetStatusEnum.Error:
				{
					printerOperate.CanPause = m_LastOperate.CanPause;
					printerOperate.CanResume = m_LastOperate.CanResume;
					break;
				}
				case JetStatusEnum.Cleaning:
				case JetStatusEnum.Moving:
				{
					printerOperate.CanPause = m_LastOperate.CanPause;
					printerOperate.CanResume = m_LastOperate.CanResume;
					break;
				}
			}
		
			m_LastOperate = printerOperate;

			m_MenuItemRealTime.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemUVSetting.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemTools.Enabled = printerOperate.CanUpdate;
			m_MenuItemSaveToPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemCalibraion.Enabled = printerOperate.CanUpdate;

			m_ToolBarButtonPrint.Enabled = m_MenuItemPrint.Enabled = printerOperate.CanPrint && m_MenuItemCalibraion.Enabled;
			m_ToolBarButtonSingleClean.Enabled =m_ToolBarButtonSingleClean.Pushed || printerOperate.CanClean;
			m_ToolBarButtonSpray.Enabled = m_ToolBarButtonSpray.Pushed ||printerOperate.CanClean;
			m_ToolBarButtonAutoClean.Enabled = m_ToolBarButtonAutoClean.Pushed||printerOperate.CanClean;

			m_ToolBarButtonLeft.Enabled = printerOperate.CanMoveLeft;
			m_ToolBarButtonRight.Enabled = printerOperate.CanMoveRight;
			m_ToolBarButtonForward.Enabled =printerOperate.CanMoveForward;
			m_ToolBarButtonBackward.Enabled =printerOperate.CanMoveBackward;
			m_ToolBarButtonDownZ.Enabled =printerOperate.CanMoveDown;
			m_ToolBarButtonUpZ.Enabled =printerOperate.CanMoveUp;
			m_ToolBarButtonGoHomeZ.Enabled = printerOperate.CanMoveBackward&&printerOperate.CanMoveForward;
			m_ToolBarButtonGoHomeY.Enabled = printerOperate.CanMoveForward&&printerOperate.CanMoveBackward;
			//m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveOriginal;
			m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveLeft && printerOperate.CanMoveRight;

			m_ToolBarButtonCheckNozzle.Enabled = printerOperate.CanPrint;
			m_ToolBarButtonMeasurePaper.Enabled = (printerOperate.CanPrint && m_allParam.PrinterProperty.bSupportPaperSensor);
			m_ToolBarButtonSetOrigin.Enabled = printerOperate.CanPrint;
			m_ToolBarButtonSetOriginY.Enabled = printerOperate.CanPrint;
					
			m_ToolBarButtonAbort.Enabled = printerOperate.CanAbort | (this.m_allParam.PrinterProperty.ZMeasurSensorSupport()?printerOperate.CanMoveStop:false);
			m_ToolBarButtonPauseResume.Enabled = (printerOperate.CanPause || printerOperate.CanResume);
			//m_ToolBarButtonPause.Enabled = printerOperate.CanPause; //???????
			//m_ToolBarButtonResume.Enabled = printerOperate.CanResume; //??????
			this.m_MenuItemEdit.Enabled = this.m_ToolBarButtonEdit.Enabled = SPrinterProperty.EPSONLCD_DEFINED?m_bFirstReady:true;
		}

		private void SetPrinterStatusChanged(JetStatusEnum status)
		{
			string info = ResString.GetEnumDisplayName(typeof(JetStatusEnum),status);
			this.m_StatusBarPanelJetStaus.Text = info;
			if(CoreInterface.Printer_IsOpen() == 0)
			{
				this.m_StatusBarPanelPercent.Text = "";
			}
			if(m_wizard != null)
			{
				m_wizard.SetPrinterStatusChanged( status);
			}
			if(m_FuncSettingForm != null)
			{
				m_FuncSettingForm.SetPrinterStatusChanged(status);
			}
			if(m_hwForm!=null)
			{
				m_hwForm.SetPrinterStatusChanged(status);
			}
			if(m_MeasureQuestionForm!= null)
			{
				m_MeasureQuestionForm.SetPrinterStatusChanged(status);
			}
			m_ToolbarSetting.SetPrinterStatusChanged(status);
			this.m_PreviewAndInfo.SetPrinterStatusChanged(status);
			this.UpdateNotifyIconInfo(status);

		}


		private int GetSpeedWithDir(MoveDirectionEnum dir)
		{
			if(dir == MoveDirectionEnum.Left || dir == MoveDirectionEnum.Right)
			{
				return m_allParam.PrinterSetting.sMoveSetting.nXMoveSpeed;
			}
			else if(dir == MoveDirectionEnum.Up ||dir ==  MoveDirectionEnum.Down)
			{
				return m_allParam.PrinterSetting.sMoveSetting.nYMoveSpeed;
			}
			else if(dir == MoveDirectionEnum.Up_Z || dir == MoveDirectionEnum.Down_Z)
			{
				return m_allParam.PrinterSetting.sMoveSetting.nZMoveSpeed;
			}
			else 
				return m_allParam.PrinterSetting.sMoveSetting.n4MoveSpeed;

		}
		private bool MainForm_KeyDownEvent(Keys keyData)
		{
			bool blnProcess = false;
			if(keyData == Keys.Left )
			{
				if(m_ToolBarButtonLeft.Enabled)
				{
					MoveDirectionEnum dir = MoveDirectionEnum.Left;
					int speed = GetSpeedWithDir(dir);
					CoreInterface.MoveCmd((int)dir,0,speed);
					m_ToolBarButtonLeft.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData == Keys.Right )
			{
				if(m_ToolBarButtonRight.Enabled)
				{
					MoveDirectionEnum dir = MoveDirectionEnum.Right;
					int speed = GetSpeedWithDir(dir);
					CoreInterface.MoveCmd((int)dir,0,speed);
					m_ToolBarButtonRight.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData == Keys.Up )
			{
				if(m_ToolBarButtonBackward.Enabled)
				{
					MoveDirectionEnum dir = MoveDirectionEnum.Up;
					if(m_allParam.PrinterProperty.nMediaType == 2)
						dir = MoveDirectionEnum.Down;

					int speed = GetSpeedWithDir(dir);
					CoreInterface.MoveCmd((int)dir,0,speed);
				
					m_ToolBarButtonBackward.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData ==Keys.Down)
			{
				if(m_ToolBarButtonForward.Enabled)
				{
					MoveDirectionEnum dir = MoveDirectionEnum.Down;
					if(m_allParam.PrinterProperty.nMediaType == 2)
						dir = MoveDirectionEnum.Up;

					int speed = GetSpeedWithDir(dir);
					CoreInterface.MoveCmd((int)dir,0,speed);

					m_ToolBarButtonForward.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			return blnProcess;
		}

		private bool MainForm_KeyUpEvent(Keys keyData)
		{
			bool blnProcess = false;
			if((keyData == Keys.Left)
				||(keyData == Keys.Right)
				||(keyData == Keys.Up)
				||(keyData == Keys.Down))
			{
				if(m_bSendMoveCmd)
				{
					CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove,0);
					m_ToolBarButtonLeft.Pushed = false;
					m_ToolBarButtonRight.Pushed = false;
					m_ToolBarButtonBackward.Pushed = false;
					m_ToolBarButtonForward.Pushed = false;
					m_ToolBarButtonDownZ.Pushed = false;
					m_ToolBarButtonUpZ.Pushed = false;
					m_bSendMoveCmd = false;
				}
				blnProcess = true;
			}
			return blnProcess; 
		}


		private void UpdateCoreBoard(string m_UpdaterFileName)
		{
#if !LIYUUSB
			bool bRead = false;
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				const int  USB_EP2_MIN_PACKAGESIZE =  1024;

				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				int				buffersize = (fileLen + USB_EP2_MIN_PACKAGESIZE -1)/USB_EP2_MIN_PACKAGESIZE * USB_EP2_MIN_PACKAGESIZE;
				buffer			= new byte[buffersize];
				int				readBytes		= 0;

				fileStream.Seek(0,SeekOrigin.Begin);
				readBytes	= binaryReader.Read(buffer,0,fileLen);
				Debug.Assert(fileLen == readBytes);

				binaryReader.Close();
				fileStream.Close();
				bRead = true;
			}
			catch{}
			if(bRead)
			{
				//CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
				CoreInterface.BeginUpdating(buffer,fileLen);
			}
#else
			CoreInterface.BeginUpdateMotion(m_UpdaterFileName);
#endif
		}


		private void OnSingleClean()
		{
			CleanForm form = new CleanForm();
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			if(CoreInterface.SendJetCommand((int)JetCmdEnum.EnterSingleCleanMode,0) == 1)
				form.ShowDialog(this);
		}


		private void OnPrinterPropertyChange_Toolbar( SPrinterProperty sp)
		{
			if(sp.bSupportZMotion)
			{
				m_ToolBarButtonDownZ.Visible = true;
				m_ToolBarButtonUpZ.Visible = true;
				m_ToolBarButtonGoHomeZ.Visible = true;
			}
			else
			{
				m_ToolBarButtonDownZ.Visible = false;
				m_ToolBarButtonUpZ.Visible = false;
				m_ToolBarButtonGoHomeZ.Visible = false;
			}
			if(sp.bSupportHandFlash)
			{
				m_ToolBarButtonSpray.Style = ToolBarButtonStyle.ToggleButton;
				m_ToolBarButtonSpray.Visible = !SPrinterProperty.IsEpson(sp.ePrinterHead);
			}
			else 
				//if(sp.bSupportAutoClean == false)
			{
				m_ToolBarButtonSpray.Style = ToolBarButtonStyle.PushButton;
				m_ToolBarButtonSpray.Visible = !SPrinterProperty.IsEpson(sp.ePrinterHead);
			}
			//else
			//	m_ToolBarButtonSpray.Visible = false;

			m_ToolBarButtonAutoClean.Visible = sp.bSupportAutoClean;

			if(sp.eSingleClean == SingleCleanEnum.None  || SPrinterProperty.IsEpson(sp.ePrinterHead))
			{
				m_ToolBarButtonSingleClean.Visible = false;
			}
			else if(sp.eSingleClean == SingleCleanEnum.PureManual)
			{
				m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.ToggleButton;
				m_ToolBarButtonSingleClean.Visible = true;
			}
			else
			{
				m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.PushButton;
				m_ToolBarButtonSingleClean.Visible = true;
			}

			if(sp.bSupportPaperSensor == true && !sp.IsGongZengEpson())
			{
				m_ToolBarButtonMeasurePaper.Visible = true;
			}
			else
				m_ToolBarButtonMeasurePaper.Visible = false;
			if(sp.nMediaType == 0)
			{
				m_ToolBarButtonGoHomeY.Visible = false;
				m_ToolBarButtonSetOriginY.Visible = false;
			}
			else
			{
				m_ToolBarButtonGoHomeY.Visible = true;
				m_ToolBarButtonSetOriginY.Visible = true;
			}
		}
		
		private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
		{
			CoreMsgEnum	kParam	= (CoreMsgEnum)wParam.ToInt32();

			switch(kParam)
			{
				case CoreMsgEnum.UpdaterPercentage:
				{
					int	percentage	= lParam.ToInt32();
					//OnPrintingProgressChanged(percentage);
					string info = "";
					string mPrintingFormat = ResString.GetUpdatingProgress();
					info += string.Format(mPrintingFormat,percentage);
					this.m_StatusBarPanelPercent.Text = info;
					this.m_PrintInformation.PrintString(info,UserLevel.user,ErrorAction.Updating);
					break;
				}

				case CoreMsgEnum.Percentage:
				{
					int	percentage	= lParam.ToInt32();
					OnPrintingProgressChanged(percentage);
					Console.WriteLine("Printing: {0}%",percentage);
					if(percentage > 0)
					{
						m_PreviewAndInfo.UpdatePercentage(percentage);
					}

					break;
				}
				case CoreMsgEnum.Job_Begin:
				{	
					int startType = lParam.ToInt32();

					if(startType == 0)
					{
					}
					else if(startType == 1)
					{
						OnPrintingStart();
					}
					
					break;
				}
				case CoreMsgEnum.Job_End:
				{

					int endType	= lParam.ToInt32();

					if(endType == 0)
					{
					}
					else if(endType == 1)
					{
						OnPrintingEnd();
					}
					break;
				}
				case CoreMsgEnum.Power_On:
				{
					int bPowerOn = lParam.ToInt32();
					if(bPowerOn != 0)
					{
						int  bPropertyChanged, bSettingChanged;
//						SPrinterProperty  sPrinterProperty;
//						SPrinterSetting sPrinterSetting;

						m_allParam.PowerOnEvent(out bPropertyChanged, out bSettingChanged,out this.m_allParam.PrinterProperty,out this.m_allParam.PrinterSetting);
						SPrinterProperty.UpdateEPSONLCDSupport(this.m_allParam.PrinterProperty.ePrinterHead);
						this.OnPowerONGetSettingsForEpson();
						if(bPropertyChanged != 0)
						{
							OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
						}
						if(bSettingChanged != 0)
						{
							OnPrinterSettingChange(this.m_allParam.PrinterSetting);
						}
					}
					else
					{
						if(m_MyMessageBox != null)
							SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );

						this.m_JobListForm.TerminatePrintingJob(false);
						m_allParam.PowerOffEvent();
						//m_bFirstReady = false;
					}
					break;
				}
				case CoreMsgEnum.Status_Change:
				{
					int status = lParam.ToInt32();
					OnPrinterStatusChanged((JetStatusEnum)status);
					if(m_bExitAtStart)
					{
						End();
						Application.Exit();
					}
					break;
				}
				case CoreMsgEnum.ErrorCode:
				{
					int errorCode = lParam.ToInt32();
					if(m_CurErrorCode != errorCode)
						m_CurErrorCode = -1; // 泵墨超时错误消失后清空标志
					SErrorCode sErrorCode= new SErrorCode(errorCode);
					if(SErrorCode.IsOnlyPauseError(errorCode))
					{
						if(m_MyMessageBox != null)
						{
							SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );
						}
						m_MyMessageBox = new MyMessageBox();

						string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
						//DialogResult result = m_MyMessageBox.ShowDialog();
						//DialogResult result = MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
						if(sErrorCode.nErrorCause == (byte)ErrorCause.COM &&
							(sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyGoHome 
							||sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyMeasure
							))
						{
							DialogResult result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Information);
							CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
							if(sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
								sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,sErrorCode.nErrorCode);
#endif
							if(result != DialogResult.No)
							{
								if(sErrorCode.nErrorCode ==(byte) COMCommand_Abort.NotifyGoHome)
								{
									CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint,(int)AxisDir.X);
								}
								else if(sErrorCode.nErrorCode ==(byte) COMCommand_Abort.NotifyMeasure)
								{
									CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,1);
								}
							}
						}
						else
						{
							DialogResult result = DialogResult.Retry;
							if(sErrorCode.nErrorCause == (byte)ErrorCause.Software &&
								sErrorCode.nErrorCode == (byte)Software.MediaTooSmall
								)
							{
								if(m_JobListForm.IsFristCopiesOrNoJobPrint())
								{
								    errorInfo = SErrorCode.GetResString("Software_MediaTooSmall_New");
									float realW = UIPreference.ToDisplayLength(m_allParam.Preference.Unit,PubFunc.CalcRealJobWidth(this.m_JobListForm.PrintingJob.JobSize.Width,m_allParam));
									errorInfo = string.Format(errorInfo,realW.ToString() + m_allParam.Preference.GetUnitDisplayName());
								}
                                result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                if (result == DialogResult.Cancel)
                                {
                                    //CoreInterface.Printer_Abort();
                                    if (CoreInterface.Printer_IsOpen() != 0)
                                        m_JobListForm.AbortPrintingJob();
                                }
							}
							else if(sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard &&
								sErrorCode.nErrorCode == (byte)CoreBoard_Err.PUMPINKTIMEOUT
								)
							{
#if false
								if(m_CurErrorCode==-1)
								{
									m_CurErrorCode = errorCode;
									if(mPumpInkTimer.Enabled)
									{
										mPumpInkTimer.Stop();
									}
									DialogResult dr = m_MyMessageBox.ShowPumpInkTimeOut(errorInfo,ResString.GetProductName());
									if(dr == DialogResult.Abort)
									{
										CoreInterface.ClearErrorCode(errorCode);
										if(mPumpInkTimer.Enabled)
											mPumpInkTimer.Stop();
										m_CurErrorCode = -1;
									}
									else
									{
										if(!mPumpInkTimer.Enabled)
											mPumpInkTimer.Start();
									}
								}
#else
								DialogResult dr = m_MyMessageBox.ShowPumpInkTimeOut(errorInfo,ResString.GetProductName());
								if(dr == DialogResult.Abort)
								{
									CoreInterface.ClearErrorCode(errorCode);
								}
#endif
							}
							else
							{
								result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
								if(result == DialogResult.Cancel)
								{
									//CoreInterface.Printer_Abort();
									if(CoreInterface.Printer_IsOpen() != 0)
										m_JobListForm.AbortPrintingJob();						
								}
							}
							if(result != DialogResult.Cancel)
							{
								CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
								if(sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
									sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
									CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,sErrorCode.nErrorCode);
#endif
							}
						}
						m_MyMessageBox = null;

					}
					OnErrorCodeChanged(lParam.ToInt32());
					//For Updateing
					ErrorCause cause = (ErrorCause)sErrorCode.nErrorCause;
					if(cause == ErrorCause.CoreBoard && (ErrorAction)sErrorCode.nErrorAction == ErrorAction.Updating)
					{
						if(0 != sErrorCode.nErrorCode)
						{
							if(sErrorCode.nErrorCode == 1)
							{
								string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.UpdateSuccess);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
							else
							{
								string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.UpdateFail);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
#if !LIYUUSB
							CoreInterface.SendJetCommand((int)JetCmdEnum.ClearUpdatingStatus,0);
#endif
						}
					}

					break;
				}
				case CoreMsgEnum.Parameter_Change:
				{
					//m_LockUpdate = true;
					SPrinterSetting sPrinterSetting = m_allParam.PrinterSetting;
					if(CoreInterface.GetPrinterSetting(ref sPrinterSetting) == 0)
					{
						Debug.Assert(false);
					}
					else
					{
						if(SPrinterProperty.EPSONLCD_DEFINED)
						{
							this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin = 
								this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin = sPrinterSetting.sFrequencySetting.fXOrigin;
							EpsonLCD.SetMainUI_Param(this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
						}
						OnPrinterSettingChange(sPrinterSetting);
					}
					//m_LockUpdate = false;
					break;
				}
				case CoreMsgEnum.AbortPrintCmd:
					//MessageBox.Show(this,"Accept! ");
                    m_JobListForm.AbortPrintingJob();
					break;
//				case CoreMsgEnum.BlockNotifyUI:
//					int msg1 = lParam.ToInt32();
//				{
//					if(m_MyMessageBox != null)
//					{
//						SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );
//					}
//					m_MyMessageBox = new MyMessageBox();
//				{
//					string m1 = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.GoHome);
//					DialogResult result = m_MyMessageBox.Show(m1,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Information);
//					if(result != DialogResult.No)
//					{
//						CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint,(int)AxisDir.X);
//					}
//				}
//					m_MyMessageBox = null;
//				}
//					break;
				case CoreMsgEnum.HardPanelDirty:
				{
					Ep6Cmd cmd = (Ep6Cmd)lParam.ToInt32();
					switch( cmd)
					{
						case Ep6Cmd.Epson_MainUI_Param: //see struct USB_RPT_MainUI_Param
						{
							EpsonLCD.GetMainUI_Param(ref this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
							this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin = this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin;
#if INKWIN_UI
							m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							m_PreviewAndInfo.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#else
							m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							m_ToolbarSetting.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#endif
						}
							break;
						case Ep6Cmd.Epson_Quality://羽化
						{
							EpsonLCD.GetPrint_Quality(ref this.m_allParam.EpsonAllParam.sUSB_Print_Quality);
							switch(this.m_allParam.EpsonAllParam.sUSB_Print_Quality.PrintQuality)
							{
								case (byte)EpsonFeatherType.None:
									this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 0;
									break;
								case (byte)EpsonFeatherType.Small:
									this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 20;
									break;
								case (byte)EpsonFeatherType.Medium:
									this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 40;
									break;
								case (byte)EpsonFeatherType.Large:
									this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 100;
									break;
							}
							if(m_FuncSettingForm != null)
								m_FuncSettingForm.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
						}
							break;
						case Ep6Cmd.Epson_Media_Info://Media_Info
							EpsonLCD.GetMedia_Info(ref this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
							this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin = this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin;
#if INKWIN_UI
							m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							m_PreviewAndInfo.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#else
							m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							m_ToolbarSetting.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#endif
							this.m_allParam.PrinterSetting.sBaseSetting.fPaperWidth = m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaWidth;
							if(m_FuncSettingForm != null)
								m_FuncSettingForm.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							break;
						case Ep6Cmd.Epson_Calibration:// 校准参数
						{
							EpsonLCD.GetCalibrationSetting(ref this.m_allParam.PrinterSetting.sCalibrationSetting,false);
							if(m_wizard != null)
							{
								// 只通知m_wizard重新抓校准参数,内部并不直接用m_allParam.PrinterSetting.sCalibrationSetting.
								m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
							}
						}
							break;
						case Ep6Cmd.Epson_STEP:// EP6_CMD_T_STEP_DIRTY  0x9
						{
							int step = 0;
							int passnum = 0;
							EpsonLCD.GetSTEP(ref step,ref passnum);
							
							if(this.m_bDuringPrinting)
							{
								SPrtFileInfo jobInfo = new SPrtFileInfo();
								if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
								{
									int realpass = jobInfo.sFreSetting.nPass;
									if(realpass != passnum)
										LogWriter.WriteLog(new string[]{string.Format("realpass[{0}] != passnum[{1}]",realpass,passnum)},true);
									this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PassNum = realpass;
									this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.StepModify = step;

									if(this.m_allParam.PrinterSetting.nKillBiDirBanding != 0)
										this.m_allParam.PrinterSetting.sCalibrationSetting.nPassStepArray[passnum/2 -1] =	step;	
									else
										this.m_allParam.PrinterSetting.sCalibrationSetting.nPassStepArray[passnum -1] = step;	
								}
							}
#if INKWIN_UI
							m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
#else
							m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
#endif
							if(m_wizard != null)
							{
								// 只通知m_wizard重新抓校准参数,内部并不直接用m_allParam.PrinterSetting.sCalibrationSetting.
								m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
							}						
						}
							break;
					}
					CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
					LogWriter.LogSetPrinterSetting(this.m_allParam.PrinterSetting,m_bDuringPrinting,string.Format("{0}-{1}",CoreMsgEnum.HardPanelDirty,cmd));
				}
					break;
				case CoreMsgEnum.HardPanelCmd:
				{
					switch( lParam.ToInt32())
					{
						case 0x1: // #define EP6_CMD_T_PA_CANCEL     0x1
							m_JobListForm.AbortPrintingJob();
							break;
						case 0x2: // #define EP6_CMD_T_PA_PAUSE      0x2
						case 0x3: // #define EP6_CMD_T_PA_RESUME     0x3
							CoreInterface.Printer_PauseOrResume();
							break;
					}
				}
					break;
			}
		}


		private void UpdateViewMode(int mode)
		{
			UIViewMode uimode  = (UIViewMode)mode;
			switch(uimode)
			{
				case UIViewMode.LeftRight:
					this.panelSubBar.Visible = true;
					this.m_FolderPreview.Visible = true;
					this.m_WorkForldersplitter.Enabled = true;
					this.m_WorkForlderpanel.Dock = DockStyle.Right;
					this.m_WorkForlderpanel.Width = this.Width/3;
					m_SplitterJobList.Dock = DockStyle.Right;
					this.toolBarButtonAddToList.Visible = true;
					this.m_JobListForm.m_JobListView.View = View.Details;
					this.panelSubBar.SendToBack();
					this.m_JobListForm.StopLoad();
					this.m_FolderPreview.ReLoadItems();
					break;
				case UIViewMode.NotifyIcon:
				case UIViewMode.OldView:
					break;
				case UIViewMode.TopDown:
				default:
					this.panelSubBar.Visible = true;
					this.m_FolderPreview.Visible = false;
					this.m_FolderPreview.StopLoad();
					this.m_WorkForldersplitter.Enabled = false;
					this.m_WorkForlderpanel.Dock = DockStyle.Bottom;
					this.m_WorkForlderpanel.Height = this.Height/3;
					m_SplitterJobList.Dock = DockStyle.Bottom;
					this.toolBarButtonAddToList.Visible = false;
					this.m_JobListForm.m_JobListView.View = View.LargeIcon;
					this.m_JobListForm.mAlignment = ListViewAlignment.Left;
					break;
			}
			this.m_MenuItemLeftRight.RadioCheck = this.m_MenuItemLeftRight.Checked = uimode == UIViewMode.LeftRight;
			this.m_MenuItemOldView.RadioCheck = this.m_MenuItemOldView.Checked = (uimode == UIViewMode.NotifyIcon || uimode == UIViewMode.OldView);
			this.m_MenuItemTopDown.RadioCheck = this.m_MenuItemTopDown.Checked = uimode == UIViewMode.TopDown;
			switch(uimode)
			{
				case UIViewMode.OldView:
				case UIViewMode.NotifyIcon:
					this.m_FolderPreview.Visible = false;
					this.m_WorkForldersplitter.Enabled = false;
					this.m_WorkForlderpanel.Dock = DockStyle.Bottom;
					this.m_WorkForlderpanel.Height = this.Height/3;
					m_SplitterJobList.Dock = DockStyle.Bottom;
					m_PreviewAndInfo.Dock = DockStyle.Fill;
					this.panelSubBar.Visible = false;
					this.m_JobListForm.m_JobListView.View = View.Details;
					m_StatusBarApp.Visible = true;
					m_PrintInformation.Visible = false;
					this.m_PreviewAndInfo.SetBackgroundimage(null,null);
#if INKWIN_UI
					m_PreviewAndInfo.SetDisStyle(false,Color.White,Color.DarkGray,true);
#else
					m_PreviewAndInfo.SetDisStyle(false,Color.White,Color.DarkGray,false);
#endif
					m_ToolbarSetting.GradientColors = new Style(SystemColors.Control,SystemColors.Control);
					this.m_ToolBarCommand.Divider = m_ToolbarSetting.Divider = true;
					Dev4Arabs.Globals.menuFont = SystemInformation.MenuFont;
					this.m_MainMenu.MenuItems.Clear();
					this.m_MenuItemView.Visible = this.m_MenuItemJob.Visible = true;
					this.m_MainMenu.MenuItems.AddRange(new MenuItem[]{this.m_MenuItemJob,
																		 this.m_MenuItemSetting,
																		 this.m_MenuItemTools,
																		 this.m_MenuItemView,
																		 this.m_MenuItemHelp,
																		 this.menuItemDongle,
																		 this.m_MenuItemDebug});
					m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Standard;
					if(PubFunc.IconReload(m_ToolbarImageList))
					{
						// 重新加载图标(PubFunc.IconReload())后,清除标记
						this.m_bMeasurePaperIconTransformed = false;
					}
					officeMenus1.End(this);
					break;
				case UIViewMode.LeftRight:
				case UIViewMode.TopDown:
				default:
					m_PreviewAndInfo.Dock = DockStyle.Fill;
					m_StatusBarApp.Visible = false;
					m_PrintInformation.Visible = true;
#if INKWIN_UI
					m_PreviewAndInfo.SetDisStyle(true,Color.LightBlue,Color.SteelBlue,true);
#else
					m_PreviewAndInfo.SetDisStyle(true,Color.LightBlue,Color.SteelBlue,false);
#endif
					m_ToolbarSetting.GradientColors =new BYHXPrinterManager.Style(MyWndProc.HeaderColor2,MyWndProc.HeaderColor3);
					this.m_ToolBarCommand.Divider = m_ToolbarSetting.Divider = false;
					this.m_PreviewAndInfo.SetBackgroundimage(MyWndProc.imgbackground,MyWndProc.imgbackground_main);
					Dev4Arabs.Globals.menuFont = new Font(Dev4Arabs.Globals.menuFont.FontFamily, SystemInformation.MenuFont.Size + 2);
					this.m_StartMenu.MenuItems.Clear();
					this.m_MenuItemView.Visible = this.m_MenuItemJob.Visible = false;
					this.m_StartMenu.MenuItems.AddRange(new MenuItem[]{this.m_MenuItemSetting,this.m_MenuItemTools,this.m_MenuItemHelp,this.menuItemDongle,this.m_MenuItemDebug});
					m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;

					System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
					ImageList imgl = new ImageList();
					imgl.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
					// Thread.CurrentThread.CurrentCulture保证在任何语言时都使用英文资源内的图标
					imgl.ImageSize = ((System.Drawing.Size)(resources.GetObject("m_ToolbarImageList.ImageSize",Thread.CurrentThread.CurrentCulture)));
					imgl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ToolbarImageList.ImageStream",Thread.CurrentThread.CurrentCulture)));
					imgl.TransparentColor = System.Drawing.Color.Transparent;
					for(int i =0;i<imgl.Images.Count;i++)
					{
						if(i < this.m_ToolbarImageList.Images.Count)
							this.m_ToolbarImageList.Images[i]=imgl.Images[i];
						else
							this.m_ToolbarImageList.Images.Add(imgl.Images[i],System.Drawing.Color.Transparent);
					}
					InitMenus();
					// 重新加载图标后,清除标记
					this.m_bMeasurePaperIconTransformed = false;
					break;
			}
			this.m_SplitterJobList.SendToBack();
			this.m_WorkForlderpanel.SendToBack();
			this.m_SplitterStatus.SendToBack();
			this.m_PrintInformation.SendToBack();
			
			this.TransformMeasurePaperIcon();
		}


		private void AddSelectedToList()
		{
			string[] m_FileNames = new string[this.m_FolderPreview.SelectedItems.Count];
			for (int i = 0; i < this.m_FolderPreview.SelectedItems.Count; i++)
			{
				m_FileNames[i] = this.m_FolderPreview.SelectedItems[i].Tag.ToString();
			}

			this.m_JobListForm.AddJobs(m_FileNames);
		}


		private void InitMenus()
		{
			//			this.officeMenus1.AddPicture(this.m_MenuItemJob, null);
			this.officeMenus1.AddPicture(this.m_MenuItemAdd, 0);
			//			this.officeMenus1.AddPicture(this.m_MenuItemDelete, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemPrint, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemExit, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemSetting, null);
			this.officeMenus1.AddPicture(this.m_MenuItemSave, 4);
			this.officeMenus1.AddPicture(this.m_MenuItemLoad, 5);
			this.officeMenus1.AddPicture(this.m_MenuItemSaveToPrinter, 6);
			this.officeMenus1.AddPicture(this.m_MenuItemLoadFromPrinter, 7);
			this.officeMenus1.AddPicture(this.m_MenuItemEdit, 8);
			//			this.officeMenus1.AddPicture(this.m_MenuItemTools, null);
			this.officeMenus1.AddPicture(this.m_MenuItemUpdate, 9);
			this.officeMenus1.AddPicture(this.m_MenuItemPassword, 10);
			this.officeMenus1.AddPicture(this.m_MenuItemDemoPage, 11);
			this.officeMenus1.AddPicture(this.m_MenuItemCalibraion, 12);
			this.officeMenus1.AddPicture(this.m_MenuItemHWSetting, 13);
			this.officeMenus1.AddPicture(this.m_MenuItemRealTime, 14);
			this.officeMenus1.AddPicture(this.m_MenuItemUVSetting, 15);
			//			this.officeMenus1.AddPicture(this.m_MenuItemView, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemTopDown, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemLeftRight, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemOldView, null);
			//			this.officeMenus1.AddPicture(this.m_MenuItemHelp, null);
			this.officeMenus1.AddPicture(this.m_MenuItemAbout, 16);
			//			this.officeMenus1.AddPicture(this.m_MenuItemDebug, null);
			this.officeMenus1.AddPicture(this.m_MenuItemFactoryDebug, 3);
			//			this.officeMenus1.AddPicture(this.menuItemAddToList, null);
			//			this.officeMenus1.AddPicture(this.menuItem2, null);
			//			this.officeMenus1.AddPicture(this.menuItemReLoad, null);
			//			this.officeMenus1.AddPicture(this.menuItem4, null);
			//			this.officeMenus1.AddPicture(this.menuItemSelectAll, null);
			//			this.officeMenus1.AddPicture(this.menuItemSave, null);
			//			this.officeMenus1.AddPicture(this.menuItemLoad, null);
			//			this.officeMenus1.AddPicture(this.menuItemSaveToPrinter, null);
			//			this.officeMenus1.AddPicture(this.menuItemLoadFromPrinter, null);
			//			this.officeMenus1.AddPicture(this.menuItemUpdate, null);
			//			this.officeMenus1.AddPicture(this.menuItemPassWord, null);
			//			this.officeMenus1.AddPicture(this.menuItemDemoPage, null);
			//			this.officeMenus1.AddPicture(this.menuItemCalibrationWizard, null);
			//			this.officeMenus1.AddPicture(this.menuItemHWSetting, null);
			//			this.officeMenus1.AddPicture(this.menuItemRealSetting, null);
			//			this.officeMenus1.AddPicture(this.menuItemUVSetting, null);
			//			this.officeMenus1.AddPicture(this.menuItem1, null);
			//			this.officeMenus1.AddPicture(this.menuItemAbout, null);
			//			this.officeMenus1.AddPicture(this.menuItem3, null);
			//			this.officeMenus1.AddPicture(this.menuItemFactoryDebug, null);
		}

		#endregion

		#region public

		public bool Start()
		{
			if(!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle,false))
				return m_bExitAtStart;
			SystemCall.PreventSystemPowerdown();
			m_allParam = new AllParam();
			SystemInit init = new SystemInit(this,this.Handle,m_KernelMessage);
			init.SystemStart();

			m_PortManager = new PortManager(this);
			m_PortManager.OpenPort();
			m_PortManager.TaskStart();
			return !m_bExitAtStart;
		}

		public bool End()
		{
			this.m_JobListForm.TerminatePrintingJob(true);
			SystemInit init = new SystemInit(this,this.Handle,m_KernelMessage);
			init.SystemEnd();

			if(m_PortManager != null)
				m_PortManager.ClosePort();
			SystemCall.AllowSystemPowerdown();
			this.notifyIconBYHX.Dispose();
			return true;
		}


		public void OnErrorCodeChanged(int code)
		{
			bool bColorChange = false;
            SErrorCode errcode = new SErrorCode(code);
            if (this.m_allParam.PrinterProperty.nColorNum == 4)
			{
				if(errcode.nErrorAction== (byte)ErrorAction.Warning && errcode.nErrorCause==(byte)ErrorCause.CoreBoard)
				{
					if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTCYAN)
					{
						bColorChange = true;
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_CYAN;
					}
					else if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTMAGENTA)
					{
						bColorChange = true;
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_MAGENTA;
					}
					else if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR1 )
					{
						bColorChange = true;
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_YELLOW;
					}
					else if(errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR2)
					{
						bColorChange = true;
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_BLACK;
					}
					code = errcode.nErrorCode + (errcode.nErrorSub<<8 )+(errcode.nErrorCause<<16 )+(errcode.nErrorAction<<24 ); 
				}
			}
			string  errStr = SErrorCode.GetInfoFromErrCode(code);
            if (errcode.nErrorCause == (byte)ErrorCause.Software && errcode.nErrorCode == (byte)Software.MediaTooSmall)
            {
                if (m_JobListForm.IsFristCopiesOrNoJobPrint())
                {
                    errStr = SErrorCode.GetResString("Software_MediaTooSmall_New");
                    float realW = UIPreference.ToDisplayLength(m_allParam.Preference.Unit, PubFunc.CalcRealJobWidth(this.m_JobListForm.PrintingJob.JobSize.Width, m_allParam));
                    errStr = string.Format(errStr, realW.ToString() + m_allParam.Preference.GetUnitDisplayName());
                }
            }
		    if(bColorChange)
				errStr += "< ... 2 >";
			this.m_StatusBarPanelError.Text = errStr;
			
			this.m_PrintInformation.printJobInfomation(code,UserLevel.user,errStr);
			//			this.m_PreviewAndInfo.printJobInfomation(code,UserLevel.user);
			//this.m_PreviewAndInfo.SetPrinterStatusChanged(CoreInterface.GetBoardStatus());
		}

		public void OnEditPrinterSetting()
		{
			SettingForm form = new SettingForm();
			form.SetGroupBoxStyle(m_GroupboxStyle);
			m_FuncSettingForm = form;
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			//Change the IconButton status for calibration
			m_FuncSettingForm.SetPrinterStatusChanged(printerStatus);
			//OnPrinterStatusChanged(printerStatus);
			form.OnPreferenceChange(m_allParam.Preference);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.OnPrinterSettingChange(m_allParam.PrinterSetting,m_allParam.EpsonAllParam);
			form.OnRealTimeChange();
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bChangeProperty = false;
				form.OnGetPrinterSetting(ref m_allParam,ref bChangeProperty);
//				if(SPrinterProperty.EPSONLCD_DEFINED)
//					EpsonLCD.SetCalibrationSetting(m_allParam.PrinterSetting.sCalibrationSetting);
				if(SPrinterProperty.EPSONLCD_DEFINED)
				{
					EpsonLCD.SetPrint_Quality(m_allParam.EpsonAllParam.sUSB_Print_Quality);
					EpsonLCD.SetMedia_Info(m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
					EpsonLCD.SetHeadparameter(m_allParam.EpsonAllParam.headParameterPercent);
					EpsonLCD.SetCleaningOption(m_allParam.EpsonAllParam.sCLEANPARA);
				}
				OnPreferenceChange(m_allParam.Preference);
				if(bChangeProperty)
					CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
#if INKWIN_UI
				m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
				m_PreviewAndInfo.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#else
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
				m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
#endif			
				SPrinterProperty.SynchronousCalibrationSettings(ref m_allParam.PrinterSetting);
				LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"OnEditPrinterSetting");
			}
			form = null;
			m_FuncSettingForm = null;
		}

		
		public void OnPrintingProgressChanged(int percent)
		{
			string info = "";
			string mPrintingFormat = ResString.GetPrintingProgress();
			info += string.Format(mPrintingFormat,percent);
			this.m_StatusBarPanelPercent.Text = info;
			this.m_PrintInformation.PrintString(info,UserLevel.user,ErrorAction.Updating);
			m_PrintPercent = percent;
		}


		public void OnPrintingStart()
		{
			m_bDuringPrinting = true;
			m_StartTime = DateTime.Now;
			m_JobListForm.OnPrintingStart();
			m_PrintingJob = m_JobListForm.PrintingJob;
		}
		public void OnPrintingEnd()
		{
			m_bDuringPrinting = false;
			m_JobListForm.OnPrintingEnd();
			m_PreviewAndInfo.UpdatePercentage(0);
			LogPrintedArea(m_PrintPercent);
			m_PrintPercent = 0;
			m_PrintingJob = null;
		}
		#endregion

		#region protected

		#endregion

		#endregion

		#region override
		protected override void WndProc(ref Message m)
		{
			if(m.WParam.ToInt32()==   0xF060)   //   关闭消息   
			{   
				string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.Exit);
				if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
			}
#if USETHEME
			if(MyWndProc.NCButtonClick(m,this,true))
#endif
				base.WndProc(ref m);

                if (m.Msg == 0x0219)//WM_DEVICECHANGE
                    BYHXSoftLock.OnDeviceChange(m.WParam, m.LParam);

			if(m.Msg == this.m_KernelMessage)
			{
				ProceedKernelMessage(m.WParam,m.LParam);
			}
#if USETHEME
			else 
				MyWndProc.PaintFormCaption(ref m,this,true);
#endif
		}

//		protected override void OnResize(EventArgs e) 
//		{ 
//			if (this.WindowState == FormWindowState.Maximized && this.FormBorderStyle != FormBorderStyle.FixedDialog) 
//			{ 
//				this.FormBorderStyle = FormBorderStyle.FixedDialog;
//			} 
//			else if(this.FormBorderStyle != FormBorderStyle.Sizable) 
//			{
//				this.FormBorderStyle = FormBorderStyle.Sizable;
//			}
//			base.OnResize(e);
//		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground (pevent);
			switch((UIViewMode)m_allParam.Preference.ViewModeIndex)
			{
				case UIViewMode.NotifyIcon:
				case UIViewMode.OldView:
					break;
				case UIViewMode.LeftRight:
				case UIViewMode.TopDown:
				default:
					MyWndProc.DrawToolbarBackGroundImage(pevent.Graphics,this.m_ToolBarCommand.Bounds,null);
					break;
			}
		}

		#endregion

		#region 事件

		#region MainForm 事件
		private void MainApp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			End();
		}
        private void menuItemDongleKey_Click(object sender, System.EventArgs e)
        {
            DongleKeyForm dkf = new DongleKeyForm();
            dkf.ShowDialog();
            if (!BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKey())
                this.Close();
        }

		private void MainForm_Load(object sender, System.EventArgs e)
		{
#if ADD_HARDKEY
			this.menuItemDongle.Visible = true;
#else
			this.menuItemDongle.Visible = false;
#endif
			this.m_JobListForm.m_PreviewForlder = this.m_FolderPreview.m_LastPreviewForlder;
			this.ShowMeasureQuestionForm(true);

#if EpsonLcd
			this.Visible = this.m_bShowUI;
			this.time.Start();
#endif
		}

		private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			MainForm_KeyDownEvent(e.KeyData);
			if (e.Modifiers == Keys.Control)
			{
				if(e.KeyCode == Keys.P) 
				{
					m_JobListForm.PrintJob();
				}
				else if	(e.KeyCode == Keys.Tab)
				{
					CoreInterface.Printer_PauseOrResume();
				}
				else if	(e.KeyCode == Keys.X)
				{
					if(m_JobListForm.Confirm_Exit())
						m_JobListForm.AbortPrintingJob();
				}
				else if(e.KeyCode == Keys.A )
				{
					m_JobListForm.OpenJob();
				}
				else if(e.KeyCode == Keys.D)
				{
					m_JobListForm.DeleteJob();
				}
				else if(e.KeyCode == Keys.E)
				{
					OnEditPrinterSetting();
				}
			}
		}

		private void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			MainForm_KeyUpEvent	(e.KeyData);
		}

		private void MainForm_SizeChanged(object sender, System.EventArgs e)
		{
			MyWndProc.DrawRoundForm(this);

			if(this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal)
			{
				this.m_WorkForlderpanel.Width = this.Width/3;
				this.m_FolderPreview.Height = this.Height/3;
			}
		}

		#endregion

		#region MainMenu 事件
		private void m_MenuItemAdd_Click(object sender, System.EventArgs e)
		{
			m_JobListForm.OpenJob();
		}

		private void m_MenuItemDelete_Click(object sender, System.EventArgs e)
		{
			m_JobListForm.DeleteJob();
	
		}

		private void m_MenuItemPrint_Click(object sender, System.EventArgs e)
		{
			m_JobListForm.PrintJob();
		}

		private void m_MenuItemExit_Click(object sender, System.EventArgs e)
		{
			string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.Exit);
			if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
			{
				return;
			}
			End();
			Application.Exit();
		}

		private void m_MenuItemSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			
			fileDialog.OverwritePrompt = true;
			fileDialog.DefaultExt = ".xml";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Env);
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				m_allParam.Preference.WorkingFolder =  Path.GetDirectoryName(fileDialog.FileName);

				string ext = Path.GetExtension(fileDialog.FileName);
				m_allParam.SaveToXml(fileDialog.FileName,false);
			}		
		}

		private void m_MenuItemLoad_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".xml";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Env);
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				m_allParam.Preference.WorkingFolder =  Path.GetDirectoryName(fileDialog.FileName);

				string ext = Path.GetExtension(fileDialog.FileName);
				m_allParam.LoadFromXml(fileDialog.FileName,false);

				this.OnPrinterSettingChange(m_allParam.PrinterSetting);
				if(CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting) == 0)
				{
					Debug.Assert(false);
				}
				LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"m_MenuItemLoad_Click");
				string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.RstoreSetting);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}		
		}

		private void m_MenuItemSaveToPrinter_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show(this,
				ResString.GetEnumDisplayName(typeof(Confirm),Confirm.SaveToBoard),
				ResString.GetProductName(),
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question)
				== DialogResult.Yes)
			{

				int iRet;
				//iRet = CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
				Cursor.Current = Cursors.WaitCursor;
				iRet = m_allParam.SetFWSetting();
				Cursor.Current = Cursors.Default;
				if(iRet != 0)
				{
					MessageBox.Show(ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.SaveSetToBoardSuccess),this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveSetToBoardFail),this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				}

			}
		
		}

		private void m_MenuItemLoadFromPrinter_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show(this,
				ResString.GetEnumDisplayName(typeof(Confirm),Confirm.LoadFromBoard),
				ResString.GetProductName(),
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question)
				== DialogResult.Yes)
			{
				int iRet = 0;
				Cursor.Current = Cursors.WaitCursor;
				iRet = m_allParam.GetFWSetting();
				Cursor.Current = Cursors.Default;
				if(iRet != 0)
				{
					this.OnPrinterSettingChange(m_allParam.PrinterSetting);
					string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.LoadSetFromBoardSuccess);
					MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
				else
				{
					string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.LoadSetFromBoardFail);
					MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				}
			}		
		}

		private void m_MenuItemEdit_Click(object sender, System.EventArgs e)
		{
			OnEditPrinterSetting();
		}

		private void m_MenuItemUpdate_Click(object sender, System.EventArgs e)
		{
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			if(printerStatus == JetStatusEnum.Busy)
			{
				if(MessageBox.Show(this, 
                        ResString.GetResString("Confirm_Update"),
					"update", 
					System.Windows.Forms.MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2)
					== DialogResult.No)
				{
					return;
				}
				else
				{
					CoreInterface.Printer_Abort();
				}
			}
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".dat";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Dat);
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
				UpdateCoreBoard(fileDialog.FileName);
			}
		}
		private void m_MenuItemPassword_Click(object sender, System.EventArgs e)
		{
			PasswordForm pwdform = new PasswordForm();
			pwdform.ShowDialog(this);
		
		}

		private void m_MenuItemDemoPage_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.BiDirectionCmd,0,ref m_allParam.PrinterSetting);
			Cursor.Current = Cursors.Default;
		}

		private void m_MenuItemCalibraion_Click(object sender, System.EventArgs e)
		{
			m_wizard = new CaliWizard();
			m_wizard.SetGroupBoxStyle(m_GroupboxStyle);
			m_MenuItemCalibraion.Enabled = false;
			m_wizard.SetPrinterChange(this);
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			//Change the IconButton status for calibration
			m_wizard.SetPrinterStatusChanged(printerStatus);
			OnPrinterStatusChanged(printerStatus);

			m_wizard.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
			m_wizard.Owner = this;
			//wizard.Parent = this;
			//DialogResult ret  = wizard.ShowDialog(this);
			m_wizard.Show();
			//if(ret == DialogResult.OK)
			//{
			//wizard.OnGetPrinterSetting(ref m_allParam.PrinterSetting);
			//}
		}

		private void m_MenuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog(this);
		}

		private void m_MenuItemFactoryDebug_Click(object sender, System.EventArgs e)
		{
			FactoryDebug hwForm = new FactoryDebug();
			hwForm.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
			hwForm.OnPrinterSettingChange(this.m_allParam.PrinterSetting);
			hwForm.OnPreferenceChange(this.m_allParam.Preference);
			DialogResult dr = hwForm.ShowDialog(this);
			if(dr == DialogResult.OK)
			{
				hwForm.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting);
				CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
				LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"m_MenuItemFactoryDebug_Click");
			}
		}

		private void m_MenuItemRealTime_Click(object sender, System.EventArgs e)
		{
			try
			{
				KonicTemperature form = new KonicTemperature();
				form.SetGroupBoxStyle(m_GroupboxStyle);
				form.OnPreferenceChange(m_allParam.Preference);
				form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
				form.OnPrinterSettingChange(m_allParam.PrinterSetting);
				form.OnRealTimeChange();
				if(form.ShowDialog(this) == DialogResult.OK)
				{
					bool bChangeProperty = false;
					form.OnGetPrinterSetting(ref m_allParam,ref bChangeProperty);
					CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
					LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"m_MenuItemRealTime_Click");
					form.ApplyToBoard();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void m_MenuItemUVSetting_Click(object sender, System.EventArgs e)
		{
			m_hwForm = new UVForm();
			m_hwForm.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
			m_hwForm.OnPrinterSettingChange(this.m_allParam.PrinterSetting);
			m_hwForm.OnPreferenceChange(this.m_allParam.Preference);
			DialogResult dr = m_hwForm.ShowDialog(this);
			if(dr == DialogResult.OK)
			{
				m_hwForm.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting);
				CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
			}
			m_hwForm = null;
		}
		private void m_MenuItemTopDown_Click(object sender, System.EventArgs e)
		{
			m_allParam.Preference.ViewModeIndex = (int)UIViewMode.TopDown;
			UpdateViewMode(m_allParam.Preference.ViewModeIndex);
		}

		private void m_MenuItemLeftRight_Click(object sender, System.EventArgs e)
		{
			m_allParam.Preference.ViewModeIndex = (int)UIViewMode.LeftRight;
			UpdateViewMode(m_allParam.Preference.ViewModeIndex);
		}

		private void m_MenuItemOldView_Click(object sender, System.EventArgs e)
		{
			m_allParam.Preference.ViewModeIndex = (int)UIViewMode.OldView;
			UpdateViewMode(m_allParam.Preference.ViewModeIndex);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Form2 form2 = new Form2();
			form2.ShowDialog(this);
		}

		private void m_MenuItemHWSetting_Click(object sender, System.EventArgs e)
		{
			SFWFactoryData fwData = new SFWFactoryData();
			bool bGet = (CoreInterface.GetFWFactoryData(ref fwData) > 0);
			if(!bGet)
			{
				string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.GetHWSettingFail);
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}

			bool bEncoder = ((fwData.m_nEncoder&(byte)INTBIT.Bit_0) == 0)?false:true;
			bool bFlat = ((fwData.m_nEncoder&(byte)INTBIT.Bit_5) == 0)? true:false;
			bool bUseYEncoder =((fwData.m_nEncoder&(byte)INTBIT.Bit_6) == 0)? false:true; 
			PrinterHWSettingSimple form = new PrinterHWSettingSimple();
			form.SetGroupBoxStyle(m_GroupboxStyle);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.SetLineEncoder (bEncoder);
			form.SetFlat (bFlat);
			form.SetUseYEncoder(bUseYEncoder);
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bEncoder1 = form.GetLineEncoder ();
				bool bFlat1 = form.GetFlat ();
				bool bUseYEncoder1 = form.GetUseYEncoder ();
				if((bEncoder1 != bEncoder)|| (bFlat1 != bFlat) || bUseYEncoder1 != bUseYEncoder)
				{
					byte value1 = 0;
					if(bEncoder1)
						value1 |= (byte)INTBIT.Bit_0;
					if(!bFlat1)
						value1 |= (byte)INTBIT.Bit_5;

					if(bUseYEncoder1)
						value1 |= (byte)INTBIT.Bit_6;

					SFWFactoryData fwData1 = new SFWFactoryData();
					fwData1 = fwData;
#if !LIYUUSB
					fwData1.m_nValidSize = 62;
					fwData1.m_nReserve = new byte[CONSTANT.nReserveSizeConst];
#endif
					fwData1.m_nEncoder = (byte)value1;
					if(CoreInterface.SetFWFactoryData(ref fwData1)!= 0)
					{
						string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.SetHWSetting);
						MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
					}
					else
					{
						string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.SetHWSettingFail);
						MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
					}
					//////////////////////////////////////
					///
					if((bFlat1 != bFlat))
					{
						if(m_allParam.PrinterProperty.bSupportDoubleMachine)
						{
							///////////////////////////////////////////////////////////////
							/// this is only for kincolor
							////////////////////////////////////////////////////////////////
							if(bFlat1)
							{
								m_allParam.PrinterSetting.sCalibrationSetting.nStepPerHead =
									m_allParam.PrinterProperty.nStepPerHead;
							}
							else
							{
								m_allParam.PrinterSetting.sCalibrationSetting.nStepPerHead = 
									m_allParam.PrinterProperty.nStepPerHead*8;
							}
						}
						if(!bFlat1)
						{
							m_allParam.PrinterSetting.sBaseSetting.fYOrigin = 0.0f;
							m_allParam.PrinterSetting.sBaseSetting.bYPrintContinue = true;
						}
						CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
						LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting,m_bDuringPrinting,"m_MenuItemHWSetting_Click");
#if INKWIN_UI
						m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
#else
						m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
#endif
					}

				}
			}
		}

		#endregion

		#region Tool Strip 事件

		private void m_ToolBarCommand_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == m_ToolBarButtonPauseResume)
			{
				CoreInterface.Printer_PauseOrResume();
			}
			else if	(e.Button == m_ToolBarButtonAbort)
			{
				//CoreInterface.Printer_Abort();
				if(m_LastOperate.CanMoveStop && this.m_allParam.PrinterProperty.ZMeasurSensorSupport())
				{
					int len = 0;
					const int port = 1;
					const byte PRINTER_PIPECMDSIZE = 26;
					byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
					m_pData[0] = 4 + 2;
					m_pData[1] = 0x57; ////SciCmd_CMD_AbortMeasure  = 0x57
					m_pData[2] = (byte)(len&0xff);       
					m_pData[3] = (byte)((len>>8)&0xff);  
					m_pData[4] = (byte)((len>>16)&0xff); 
					m_pData[5] = (byte)((len>>24)&0xff); 

					CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port);
				}
				else if(m_JobListForm.Confirm_Exit())
					m_JobListForm.AbortPrintingJob();
			}
			else if(e.Button == m_ToolBarButtonAdd )
			{
				m_JobListForm.OpenJob();
			}
			else if(e.Button == m_ToolBarButtonDelete)
			{
				m_JobListForm.DeleteJob();
			}
			else if(e.Button == m_ToolBarButtonEdit)
			{
				OnEditPrinterSetting();
				//m_JobListForm.EditJob();
			}
			else if(e.Button == m_ToolBarButtonPrint)
			{
				m_JobListForm.PrintJob();
			}
			else 
			{
				JetCmdEnum cmd;
				int cmdvalue = 0;
				if(e.Button== m_ToolBarButtonCheckNozzle)
				{
					CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.CheckNozzleCmd,0,ref this.m_allParam.PrinterSetting);
					return;
				}
				else if(e.Button==m_ToolBarButtonAutoClean)
					cmd = JetCmdEnum.AutoSuckHead;
				else if(e.Button==m_ToolBarButtonSpray)
				{
					if(m_allParam.PrinterProperty.bSupportHandFlash)
					{
						if(e.Button.Pushed == true)
							cmd = JetCmdEnum.StartSpray;
						else
							cmd = JetCmdEnum.StopSpray;
					}
					else
						cmd = JetCmdEnum.FireSprayHead;
				}
				else if(e.Button==m_ToolBarButtonSingleClean)
				{
					if(m_allParam.PrinterProperty.eSingleClean == SingleCleanEnum.PureManual)
					{
						if(e.Button.Pushed == true)
							cmd = JetCmdEnum.EnterSingleCleanMode;
						else
							cmd = JetCmdEnum.ExitSingleCleanMode;
					}
					else
					{
						OnSingleClean() ;return;
					}
				}
				else if(e.Button==m_ToolBarButtonGoHome)
					cmd = JetCmdEnum.BackToHomePoint;
				else if(e.Button==m_ToolBarButtonGoHomeY)
				{
					cmd = JetCmdEnum.BackToHomePointY;
					cmdvalue = (int)AxisDir.Y;
				}
				else if(e.Button==m_ToolBarButtonGoHomeZ)
				{
					cmd = JetCmdEnum.BackToHomePointY;
					cmdvalue = (int)AxisDir.Z;
				}
				else if(e.Button==m_ToolBarButtonSetOrigin)
				{
					// epson 时,触发epson机器内置的设原点
					if(SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead))
					{
						uint bufsize = 64;
						byte[] buf = new byte[bufsize];
						CoreInterface.SetEpsonEP0Cmd(0x7f,buf,ref bufsize,17,0);
						return;
					}
					cmd = JetCmdEnum.SetOrigin;
					cmdvalue = (int)AxisDir.X;
				}
				else if(e.Button==m_ToolBarButtonSetOriginY)
				{
					cmd = JetCmdEnum.SetOrigin;
					cmdvalue = (int)AxisDir.Y;
				}
				else if(e.Button==m_ToolBarButtonMeasurePaper)
				{
					cmd = JetCmdEnum.MeasurePaper;
					cmdvalue = 1;
					if(m_allParam.PrinterProperty.IsALLWIN_FLAT())
					{
						CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,2);
						return;
					}
					else if(m_allParam.PrinterProperty.ZMeasurSensorSupport())
					{
						this.ShowMeasureQuestionForm(true);
						return;
					}
				}
				else if(e.Button==m_ToolBarButtonStop)
				{
					//if(CoreInterface.GetBoardStatus() == JetStatusEnum.Moving)
					cmd = JetCmdEnum.StopMove;
				}
				else 
					return;
				CoreInterface.SendJetCommand((int)cmd,cmdvalue);
			}
		}
		
		private void m_ToolBarCommand_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( m_ToolBarButtonLeft.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonLeft.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Left;
				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonLeft.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonRight.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonRight.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Right;
				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonRight.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonForward.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonForward.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Down;
				if(m_allParam.PrinterProperty.nMediaType == 2)
					dir = MoveDirectionEnum.Up;

				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonForward.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonBackward.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonBackward.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Up;
				if(m_allParam.PrinterProperty.nMediaType == 2)
					dir = MoveDirectionEnum.Down;

				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonBackward.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonDownZ.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonDownZ.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Down_Z;
				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonDownZ.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonUpZ.Rectangle.Contains(e.X,e.Y) &&m_ToolBarButtonUpZ.Enabled)
			{
				MoveDirectionEnum dir = MoveDirectionEnum.Up_Z;
				int speed = GetSpeedWithDir(dir);
				CoreInterface.MoveCmd((int)dir,0,speed);

				m_ToolBarButtonUpZ.Pushed = true;
				m_bSendMoveCmd = true;
			}
		}

		private void m_ToolBarCommand_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(m_bSendMoveCmd)
			{
				CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove,0);
				m_ToolBarButtonLeft.Pushed = false;
				m_ToolBarButtonRight.Pushed = false;
				m_ToolBarButtonBackward.Pushed = false;
				m_ToolBarButtonForward.Pushed = false;
				m_ToolBarButtonDownZ.Pushed = false;
				m_ToolBarButtonUpZ.Pushed = false;

				m_bSendMoveCmd = false;
			}
		}

		#endregion

		#region 主工作区内控件事件
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.toolBarButtonWorkForlder)
			{
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				fbd.Description = "Please select the working directory";

				if(m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
					fbd.SelectedPath = this.m_FolderPreview.FolderName;
				else
					fbd.SelectedPath = this.m_JobListForm.FolderName;
				if(fbd.ShowDialog(this) == DialogResult.OK)
				{
					if(m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
					{
						this.m_FolderPreview.FolderName = fbd.SelectedPath;
						this.m_JobListForm.m_PreviewForlder = this.m_FolderPreview.m_LastPreviewForlder;
					}
					else
					{
						this.m_JobListForm.FolderName = fbd.SelectedPath;
					}
				}
			}
			else if	(e.Button == this.toolBarButtonRefresh)
			{
				if(m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
				{
//					if(Directory.Exists(this.m_FolderPreview.m_LastPreviewForlder))
//						Directory.Delete(this.m_FolderPreview.m_LastPreviewForlder,true);
					this.m_FolderPreview.ReLoadItems();
				}
				else
				{
					this.m_JobListForm.ReLoadItems();
				}
			}
			else if(e.Button == this.toolBarButtonAddToList)
			{
				this.AddSelectedToList();
			}
			else if(e.Button == this.toolBarButtonSelectAll)
			{
				if(m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
				{
					this.m_FolderPreview.SelectAll();
				}
				else
				{
					this.m_JobListForm.SelectAll();
				}
			}
		}


		private void m_FolderPreview_ItemActivate(object sender, System.EventArgs e)
		{
			this.AddSelectedToList();
		}

		private void menuItemAddToList_Click(object sender, System.EventArgs e)
		{
			this.AddSelectedToList();
		}

		private void menuItemReLoad_Click(object sender, System.EventArgs e)
		{
			this.m_FolderPreview.ReLoadItems();
		}

		private void menuItemSelectAll_Click(object sender, System.EventArgs e)
		{
			foreach (ListViewItem lvi in this.m_FolderPreview.Items)
			{
				lvi.Selected = true;
			}
		}

		private void MenuFolderPreview_Popup(object sender, System.EventArgs e)
		{
			this.menuItemAddToList.Enabled = this.m_FolderPreview.SelectedItems.Count > 0;
		}


		private void m_PrintInformation_StartButtonClicked(object sender, System.EventArgs e)
		{
			this.ContextMenu = this.m_StartMenu;
            Graphics g = this.CreateGraphics();
            // calculate the menu height
            int ItemHeight = this.officeMenus1.MeasureItemHeight(this.m_MainMenu.MenuItems[0],new MeasureItemEventArgs(g,0));// SystemInformation.MenuHeight;
            g.Dispose();
			int itemscount = 0;//this.m_MenuItemDebug.Visible?m_StartMenu.MenuItems.Count:m_StartMenu.MenuItems.Count-1;
			for (int i = 0; i<m_StartMenu.MenuItems.Count;i++)
				if(m_StartMenu.MenuItems[i].Visible)
					itemscount++;
            Point cml = new Point(0, -ItemHeight * itemscount);
			officeMenus1.Start(this);
			this.m_StartMenu.Show(this.m_PrintInformation,cml);
			officeMenus1.End(this);
			this.ContextMenu = null;
		}
		#endregion

		#endregion

		private void m_DongleKeyAlarm_EncryptDogExpired(object sender, EventArgs e)
		{
			this.UpdateFormHeaderText();
			this.m_JobListForm.TerminatePrintingJob(false);
			UpdateButtonStates(CoreInterface.GetBoardStatus());
		}

		private void m_DongleKeyAlarm_EncryptDogLast100H(object sender, EventArgs e)
		{
			MessageBox.Show(ResString.GetResString("EncryptDog_Warning"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished(object sender, EventArgs e)
		{
			UpdateButtonStates(CoreInterface.GetBoardStatus());
			if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
			{
				this.m_JobListForm.TerminatePrintingJob(false);
			}			
			this.UpdateFormHeaderText();
		}

		private void UpdateFormHeaderText()
		{
			JetStatusEnum status = CoreInterface.GetBoardStatus();
			if(status != JetStatusEnum.PowerOff)
			{
				SPrinterProperty sp = m_allParam.PrinterProperty;
				byte headNum =  sp.nHeadNum;
				if(SPrinterProperty.IsKonica512 (sp.ePrinterHead) 
					|| sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
					|| sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl
					|| sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
					)
					headNum /= 2;
				else if(SPrinterProperty.IsPolarisOneHead4Color(sp.ePrinterHead))
				{
					headNum /= 8;
				}
				else if(SPrinterProperty.IsPolaris (sp.ePrinterHead))
				{
					headNum /= 4;
				}
				else if(sp.ePrinterHead == PrinterHeadEnum.EGen5)
				{
					headNum /= 8;
				}

				if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
				{
					this.Text = m_sFormTile 
						+ " "   + sp.ePrinterHead.ToString() 
						+ " "	+ headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Head)
						+ " "	+ sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Color)
						+ " " + ResString.GetResString("EncryptDog_Expired");
				}
				else
				{
					this.Text = m_sFormTile 
						+ " "   + sp.ePrinterHead.ToString() 
						+ " "	+ headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Head)
						+ " "	+ sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Color);
				}
			}		
		}

		private void ShowMeasureQuestionForm(bool mainformloaded)
		{
			JetStatusEnum status = CoreInterface.GetBoardStatus();
			if(mainformloaded 
				&& !m_allParam.PrinterProperty.IsALLWIN_FLAT()
				&& status != JetStatusEnum.PowerOff
				&& m_allParam.PrinterProperty.ZMeasurSensorSupport()
				&& m_bFirstReady
				&& m_bShowUI)// PubFunc.IsVender92())
			{
				if(m_MeasureQuestionForm == null)
					m_MeasureQuestionForm = new MeasureQuestionForm(this);
				m_MeasureQuestionForm.OnPrinterSettingChange(m_allParam.PrinterSetting);
				m_MeasureQuestionForm.OnPreferenceChange(m_allParam.Preference);
				m_MeasureQuestionForm.SetPrinterStatusChanged(status);
				if(!m_MeasureQuestionForm.Visible)
				{
					m_MeasureQuestionForm.ShowDialog();
					m_MeasureQuestionForm = null;
				}
			}
		}



		#region 主UI显/隐控制
#if EpsonLcd
		public void theout(object source, System.Timers.ElapsedEventArgs e)
		{
			ProcessMessageHandler pmh = new ProcessMessageHandler(ProcessMessage);
			this.Invoke(pmh);
		}//end fun

		public delegate void ProcessMessageHandler();

		public void ProcessMessage()
		{
			MyData data = ProcessMessaging.GetShareMem();
			if (data.ProcessID == ProcessID)
			{
				switch (data.InfoCode)
				{
					// ...处理
					case 0:
						m_bShowUI = false;
						this.ShowInTaskbar = false;
						this.WindowState = FormWindowState.Minimized;
						this.Hide();
						break;
					case 1:
						m_bShowUI = true;
						this.ShowInTaskbar = true;
						this.WindowState = FormWindowState.Maximized;
						this.Show();
						UpdateViewMode(m_allParam.Preference.ViewModeIndex);
						this.BringToFront();
						break;
					default:
						break;
				}
			}
			//	...清空data
			if(data.InfoCode != -1)
			{
				data.InfoCode = -1;
				ProcessMessaging.SetShareMem(data);
			}	
		}

#endif
		#endregion
		
		private void notifyIconBYHX_DoubleClick(object sender, System.EventArgs e)
		{
#if true
			m_bShowUI = true;
			this.ShowInTaskbar = true;
			this.WindowState = FormWindowState.Maximized;
			this.Show();
			UpdateViewMode(m_allParam.Preference.ViewModeIndex);
			this.BringToFront();
#else
			m_bShowUI = !m_bShowUI;
			if(m_bShowUI)
			{
				this.ShowInTaskbar = m_bShowUI;
				this.WindowState = FormWindowState.Maximized;
				this.Show();
				UpdateViewMode(m_allParam.Preference.ViewModeIndex);
				this.BringToFront();
			}
			else
			{
				this.ShowInTaskbar = false;
				this.WindowState = FormWindowState.Minimized;
				this.Hide();
			}
#endif 
		}

		private void mainWin_HandleCreated(object sender, EventArgs e)
		{
			if(CoreInterface.SetMessageWindow(this.Handle,m_KernelMessage)==0)
			{
				Debug.Assert(false);
			}
		}

		private void UpdateNotifyIconInfo(JetStatusEnum status)
		{
			string notifytext = this.Text 
				+ Environment.NewLine + ResString.GetEnumDisplayName(typeof(JetStatusEnum),status);
			System.Reflection.Assembly assembly = typeof(MainForm).Assembly;
			switch(status)
			{
				case JetStatusEnum.Ready:
					this.notifyIconBYHX.Icon = new Icon(assembly.GetManifestResourceStream("BYHXPrinterManager.Icons.app.ico"));
					break;
				case JetStatusEnum.PowerOff:
					this.notifyIconBYHX.Icon = new Icon(assembly.GetManifestResourceStream("BYHXPrinterManager.Icons.app_off.ico"));
					break;
				case JetStatusEnum.Busy:
					break;
				case JetStatusEnum.Error:
				{
					int errorcode = CoreInterface.GetBoardError();
					SErrorCode sec = new SErrorCode();
					switch((ErrorAction)sec.nErrorAction)
					{
						case ErrorAction.Warning:
							this.notifyIconBYHX.Icon = new Icon(assembly.GetManifestResourceStream("BYHXPrinterManager.Icons.app_worning.ico"));
							break;
						case ErrorAction.Updating:
						case ErrorAction.Init:
						case ErrorAction.UserResume:
							this.notifyIconBYHX.Icon = new Icon(assembly.GetManifestResourceStream("BYHXPrinterManager.Icons.app_info.ico"));
							break;
						default:
							this.notifyIconBYHX.Icon = new Icon(assembly.GetManifestResourceStream("BYHXPrinterManager.Icons.app_error.ico"));
							break;
				
					}
					notifytext = ResString.GetEnumDisplayName(typeof(JetStatusEnum),status) 
						+ Environment.NewLine + SErrorCode.GetInfoFromErrCode(errorcode);
				}
					break;
			}
			// notifyIconBYHX.Text长度最大不能超过64个字符
			if(notifytext.Length >= 64)
				this.notifyIconBYHX.Text = notifytext.Substring(0,60) + "...";
			else
				this.notifyIconBYHX.Text = notifytext;
			Debug.WriteLine("notifytext.Length = " + notifytext.Length.ToString());
		}


		private void mPumpInkTimer_Tick(object sender,EventArgs e)
		{
			if(m_CurErrorCode == -1)
			{
				// 其他原因导致错误消失后关闭定时器
				if(mPumpInkTimer.Enabled)
					mPumpInkTimer.Stop();
				return;
			}
			string errorInfo = SErrorCode.GetInfoFromErrCode(m_CurErrorCode);
			MyMessageBox msg = new MyMessageBox();
			DialogResult dr = msg.ShowPumpInkTimeOut(errorInfo,ResString.GetProductName());
			if(dr == DialogResult.Abort)
			{
				CoreInterface.ClearErrorCode(m_CurErrorCode);
				if(mPumpInkTimer.Enabled)
					mPumpInkTimer.Stop();
				m_CurErrorCode = -1;
			}
		}

		private void menuItemSaveCaliPara_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			
			fileDialog.OverwritePrompt = true;
			fileDialog.DefaultExt = ".bin";
			fileDialog.Filter = "Binary File(*.bin)|*.bin";
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				EpsonLCD.SaveEpsonCaliParaToFile(fileDialog.FileName);
			}

		}

		private void menuItemLoadCaliPara_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".bin";
			fileDialog.Filter = "Binary File(*.bin)|*.bin";
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				EpsonLCD.LoadEpsonCaliParaFromFile(fileDialog.FileName);
			}	
		}

		/// <summary>
		/// 按照设置将打印面积记入log文件
		/// </summary>
		/// <param name="percent">打印结束时的打印百分比</param>
		public void LogPrintedArea(int percent)
		{
			UIPreference up = m_allParam.Preference;
			bool bNeedLog = false;		
			if(m_PrintingJob !=null && up.PrintedAreaLogConfig != null && up.PrintedAreaLogConfig !=string.Empty)
			{
				string[] filters = up.PrintedAreaLogConfig.Split(new char[]{'|'});
				for(int i = 0; i < filters.Length; i++)
				{
					if(i==0 && (filters[i]!= string.Empty&&filters[i] == "*"))
					{
						bNeedLog = true;
						break;
					}
					if(filters[i]!= string.Empty &&filters[i] != "*"&& m_PrintingJob.FileLocation.EndsWith(filters[i]))
					{
						bNeedLog = true;		
						break;
					}
				}
			}

			if(bNeedLog)
			{
				string loginfo = this.GetPrintedAreaLogString();
				LogWriter.WritePrintedAreaLog(new string[]{loginfo},true);
			}
			m_PrintPercent = 0;
		}

		private string GetPrintedAreaLogString()
		{		
			TimeSpan time = DateTime.Now - m_StartTime;
			float width = 0;
			if(m_PrintingJob.PrtFileInfo.sFreSetting.nResolutionX != 0)
			{
				width = m_PrintingJob.JobSize.Width;
				width = PubFunc.CalcRealJobWidth(width,this.m_allParam);
			}
			float height = 0;
			if(m_PrintingJob.PrtFileInfo.sFreSetting.nResolutionY != 0)
			{
				height = m_PrintingJob.JobSize.Height;
			}
			float m_fArea = UIPreference.ToDisplayLength(UILengthUnit.Meter,width) * UIPreference.ToDisplayLength(UILengthUnit.Meter,height);
			string strStartTime = string.Format("{0}-{1}",m_StartTime.ToShortDateString(),m_StartTime.ToShortTimeString());
			string strTime =time.Hours.ToString() +":" +time.Minutes.ToString() +":" + time.Seconds.ToString();
			string strPercentage = m_PrintPercent.ToString() + "%";			
			string unitStr	= ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
			string strArea = (m_fArea * m_PrintPercent/100.0f).ToString() + " " + unitStr+ "2";
			return string.Format("{0}; {1}; {2}; {3}; {4}",new object[]{m_PrintingJob.FileLocation,strStartTime,strTime,strPercentage,strArea});
		}
	}
}
