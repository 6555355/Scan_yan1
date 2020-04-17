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
using System.Reflection;
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
		private ContextMenu m_StartMenu = new ContextMenu();
		private Grouper m_GroupboxStyle = null;
		private UIJob mPrintingjob = null;
		private MeasureQuestionForm m_MeasureQuestionForm = null;
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
		private System.Windows.Forms.Splitter m_SplitterStatus;
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
		private System.Windows.Forms.MenuItem m_MenuItemHelp;
		private System.Windows.Forms.MenuItem m_MenuItemAbout;
		private System.Windows.Forms.Splitter m_SplitterToolbar;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonDownZ;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonUpZ;
		private System.Windows.Forms.MenuItem m_MenuItemHWSetting;
		private System.Windows.Forms.MenuItem m_MenuItemRealTime;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonGoHomeY;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonGoHomeZ;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSep3;
		private System.Windows.Forms.MenuItem m_MenuItemUVSetting;
		private System.Windows.Forms.ToolBarButton m_ToolBarButtonSetOriginY;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
		private System.Windows.Forms.StatusBar m_StatusBarApp;
		private BYHXPrinterManager.Main.PrintInformation m_PrintInformation;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private BYHXPrinterManager.Preview.PrintingJobInfo printingJobInfo1;
		private BYHXPrinterManager.Setting.ToolbarSetting m_ToolbarSetting;
		private BYHXPrinterManager.JobListView.JobListForm m_JobListForm;
		private BYHXPrinterManager.JobListView.PrintingInfo m_PreviewAndInfo;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelSubToolBar;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButtonWorkForlder;
		private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
		private System.Windows.Forms.ToolBarButton toolBarButtonAddToList;
		private System.Windows.Forms.ToolBarButton toolBarButtonSelectAll;
		private BYHXPrinterManager.GradientControls.CrystalPanel panel2;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonPre;
		private System.Windows.Forms.Button buttonJoblist;
		private System.Windows.Forms.Button buttonPreview;
		private System.Windows.Forms.ToolBarButton toolBarButtonShowPreview;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem Setting;
		private System.Windows.Forms.MenuItem m_CMenuItemTools;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.MenuItem menuItemLoad;
		private System.Windows.Forms.MenuItem menuItemSaveToPrinter;
		private System.Windows.Forms.MenuItem menuItemLoadFromPrinter;
		private System.Windows.Forms.MenuItem menuItemUpdate;
		private System.Windows.Forms.MenuItem menuItemPassWord;
		private System.Windows.Forms.MenuItem menuItemDemoPage;
		private System.Windows.Forms.MenuItem menuItemCalibrationWizard;
		private System.Windows.Forms.MenuItem menuItemHWSetting;
		private System.Windows.Forms.MenuItem menuItemRealSetting;
		private System.Windows.Forms.MenuItem menuItemUVSetting;
		private System.Windows.Forms.MenuItem m_MenuItemDebug;
		private System.Windows.Forms.MenuItem m_MenuItemFactoryDebug;
		private System.Windows.Forms.MenuItem menuItemDongle;
		private System.Windows.Forms.MenuItem menuItemDongleKey;
		private System.ComponentModel.IContainer components;

		public MainForm()
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
			PubFunc.IconReload((UIViewMode)m_allParam.Preference.ViewModeIndex, m_ToolbarImageList);

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
			BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm(this);
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired+=new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H+=new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
			BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished+=new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);
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
			this.m_MenuItemTools = new System.Windows.Forms.MenuItem();
			this.m_MenuItemUpdate = new System.Windows.Forms.MenuItem();
			this.m_MenuItemPassword = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDemoPage = new System.Windows.Forms.MenuItem();
			this.m_MenuItemCalibraion = new System.Windows.Forms.MenuItem();
			this.m_MenuItemHWSetting = new System.Windows.Forms.MenuItem();
			this.m_MenuItemRealTime = new System.Windows.Forms.MenuItem();
			this.m_MenuItemUVSetting = new System.Windows.Forms.MenuItem();
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.m_JobListForm = new BYHXPrinterManager.JobListView.JobListForm();
			this.panelSubToolBar = new System.Windows.Forms.Panel();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarButtonWorkForlder = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonAddToList = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonSelectAll = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonShowPreview = new System.Windows.Forms.ToolBarButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.m_PreviewAndInfo = new BYHXPrinterManager.JobListView.PrintingInfo();
			this.panel2 = new BYHXPrinterManager.GradientControls.CrystalPanel();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonPre = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.printingJobInfo1 = new BYHXPrinterManager.Preview.PrintingJobInfo();
			this.buttonJoblist = new System.Windows.Forms.Button();
			this.buttonPreview = new System.Windows.Forms.Button();
			this.m_SplitterStatus = new System.Windows.Forms.Splitter();
			this.m_SplitterToolbar = new System.Windows.Forms.Splitter();
			this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
			this.m_PrintInformation = new BYHXPrinterManager.Main.PrintInformation();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.Setting = new System.Windows.Forms.MenuItem();
			this.menuItemSave = new System.Windows.Forms.MenuItem();
			this.menuItemLoad = new System.Windows.Forms.MenuItem();
			this.menuItemSaveToPrinter = new System.Windows.Forms.MenuItem();
			this.menuItemLoadFromPrinter = new System.Windows.Forms.MenuItem();
			this.m_CMenuItemTools = new System.Windows.Forms.MenuItem();
			this.menuItemUpdate = new System.Windows.Forms.MenuItem();
			this.menuItemPassWord = new System.Windows.Forms.MenuItem();
			this.menuItemDemoPage = new System.Windows.Forms.MenuItem();
			this.menuItemCalibrationWizard = new System.Windows.Forms.MenuItem();
			this.menuItemHWSetting = new System.Windows.Forms.MenuItem();
			this.menuItemRealSetting = new System.Windows.Forms.MenuItem();
			this.menuItemUVSetting = new System.Windows.Forms.MenuItem();
			this.m_PanelToolBarSetting.SuspendLayout();
			this.m_PanelJobList.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panelSubToolBar.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
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
																							  this.m_MenuItemEdit});
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
			// m_MenuItemHelp
			// 
			this.m_MenuItemHelp.Enabled = ((bool)(resources.GetObject("m_MenuItemHelp.Enabled")));
			this.m_MenuItemHelp.Index = 3;
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
			this.menuItemDongle.Index = 4;
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
			this.m_MenuItemDebug.Index = 5;
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
			this.m_ToolbarSetting.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.FromArgb(((System.Byte)(59)), ((System.Byte)(178)), ((System.Byte)(234))), System.Drawing.Color.FromArgb(((System.Byte)(59)), ((System.Byte)(178)), ((System.Byte)(234))));
			this.m_ToolbarSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.m_ToolbarSetting.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ToolbarSetting.ImeMode")));
			this.m_ToolbarSetting.Location = ((System.Drawing.Point)(resources.GetObject("m_ToolbarSetting.Location")));
			this.m_ToolbarSetting.Name = "m_ToolbarSetting";
			this.m_ToolbarSetting.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ToolbarSetting.RightToLeft")));
			this.m_ToolbarSetting.Size = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.Size")));
			this.m_ToolbarSetting.TabIndex = ((int)(resources.GetObject("m_ToolbarSetting.TabIndex")));
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
			this.m_PanelJobList.Controls.Add(this.tabControl1);
			this.m_PanelJobList.Controls.Add(this.panel1);
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
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl1.Dock")));
			this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControl1.Enabled = ((bool)(resources.GetObject("tabControl1.Enabled")));
			this.tabControl1.Font = ((System.Drawing.Font)(resources.GetObject("tabControl1.Font")));
			this.tabControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabControl1.ImeMode")));
			this.tabControl1.ItemSize = ((System.Drawing.Size)(resources.GetObject("tabControl1.ItemSize")));
			this.tabControl1.Location = ((System.Drawing.Point)(resources.GetObject("tabControl1.Location")));
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = ((System.Drawing.Point)(resources.GetObject("tabControl1.Padding")));
			this.tabControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabControl1.RightToLeft")));
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowToolTips = ((bool)(resources.GetObject("tabControl1.ShowToolTips")));
			this.tabControl1.Size = ((System.Drawing.Size)(resources.GetObject("tabControl1.Size")));
			this.tabControl1.TabIndex = ((int)(resources.GetObject("tabControl1.TabIndex")));
			this.tabControl1.Text = resources.GetString("tabControl1.Text");
			this.tabControl1.Visible = ((bool)(resources.GetObject("tabControl1.Visible")));
			// 
			// tabPage1
			// 
			this.tabPage1.AccessibleDescription = resources.GetString("tabPage1.AccessibleDescription");
			this.tabPage1.AccessibleName = resources.GetString("tabPage1.AccessibleName");
			this.tabPage1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage1.Anchor")));
			this.tabPage1.AutoScroll = ((bool)(resources.GetObject("tabPage1.AutoScroll")));
			this.tabPage1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage1.AutoScrollMargin")));
			this.tabPage1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage1.AutoScrollMinSize")));
			this.tabPage1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage1.BackgroundImage")));
			this.tabPage1.Controls.Add(this.m_JobListForm);
			this.tabPage1.Controls.Add(this.panelSubToolBar);
			this.tabPage1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage1.Dock")));
			this.tabPage1.Enabled = ((bool)(resources.GetObject("tabPage1.Enabled")));
			this.tabPage1.Font = ((System.Drawing.Font)(resources.GetObject("tabPage1.Font")));
			this.tabPage1.ImageIndex = ((int)(resources.GetObject("tabPage1.ImageIndex")));
			this.tabPage1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage1.ImeMode")));
			this.tabPage1.Location = ((System.Drawing.Point)(resources.GetObject("tabPage1.Location")));
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage1.RightToLeft")));
			this.tabPage1.Size = ((System.Drawing.Size)(resources.GetObject("tabPage1.Size")));
			this.tabPage1.TabIndex = ((int)(resources.GetObject("tabPage1.TabIndex")));
			this.tabPage1.Text = resources.GetString("tabPage1.Text");
			this.tabPage1.ToolTipText = resources.GetString("tabPage1.ToolTipText");
			this.tabPage1.Visible = ((bool)(resources.GetObject("tabPage1.Visible")));
			// 
			// m_JobListForm
			// 
			this.m_JobListForm.AccessibleDescription = resources.GetString("m_JobListForm.AccessibleDescription");
			this.m_JobListForm.AccessibleName = resources.GetString("m_JobListForm.AccessibleName");
			this.m_JobListForm.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_JobListForm.Anchor")));
			this.m_JobListForm.AutoScroll = ((bool)(resources.GetObject("m_JobListForm.AutoScroll")));
			this.m_JobListForm.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMargin")));
			this.m_JobListForm.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMinSize")));
			this.m_JobListForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_JobListForm.BackgroundImage")));
			this.m_JobListForm.CustomRowBackColor = System.Drawing.Color.PaleTurquoise;
			this.m_JobListForm.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_JobListForm.Dock")));
			this.m_JobListForm.Enabled = ((bool)(resources.GetObject("m_JobListForm.Enabled")));
			this.m_JobListForm.FolderName = "";
			this.m_JobListForm.Font = ((System.Drawing.Font)(resources.GetObject("m_JobListForm.Font")));
			this.m_JobListForm.GridLines = false;
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
			this.m_JobListForm.SelectedIndexChanged += new System.EventHandler(this.m_JobListForm_SelectedIndexChanged);
			// 
			// panelSubToolBar
			// 
			this.panelSubToolBar.AccessibleDescription = resources.GetString("panelSubToolBar.AccessibleDescription");
			this.panelSubToolBar.AccessibleName = resources.GetString("panelSubToolBar.AccessibleName");
			this.panelSubToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panelSubToolBar.Anchor")));
			this.panelSubToolBar.AutoScroll = ((bool)(resources.GetObject("panelSubToolBar.AutoScroll")));
			this.panelSubToolBar.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panelSubToolBar.AutoScrollMargin")));
			this.panelSubToolBar.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panelSubToolBar.AutoScrollMinSize")));
			this.panelSubToolBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelSubToolBar.BackgroundImage")));
			this.panelSubToolBar.Controls.Add(this.toolBar1);
			this.panelSubToolBar.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panelSubToolBar.Dock")));
			this.panelSubToolBar.Enabled = ((bool)(resources.GetObject("panelSubToolBar.Enabled")));
			this.panelSubToolBar.Font = ((System.Drawing.Font)(resources.GetObject("panelSubToolBar.Font")));
			this.panelSubToolBar.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panelSubToolBar.ImeMode")));
			this.panelSubToolBar.Location = ((System.Drawing.Point)(resources.GetObject("panelSubToolBar.Location")));
			this.panelSubToolBar.Name = "panelSubToolBar";
			this.panelSubToolBar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panelSubToolBar.RightToLeft")));
			this.panelSubToolBar.Size = ((System.Drawing.Size)(resources.GetObject("panelSubToolBar.Size")));
			this.panelSubToolBar.TabIndex = ((int)(resources.GetObject("panelSubToolBar.TabIndex")));
			this.panelSubToolBar.Text = resources.GetString("panelSubToolBar.Text");
			this.panelSubToolBar.Visible = ((bool)(resources.GetObject("panelSubToolBar.Visible")));
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
																						this.toolBarButtonSelectAll,
																						this.toolBarButtonShowPreview});
			this.toolBar1.ButtonSize = ((System.Drawing.Size)(resources.GetObject("toolBar1.ButtonSize")));
			this.toolBar1.Divider = false;
			this.toolBar1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("toolBar1.Dock")));
			this.toolBar1.DropDownArrows = ((bool)(resources.GetObject("toolBar1.DropDownArrows")));
			this.toolBar1.Enabled = ((bool)(resources.GetObject("toolBar1.Enabled")));
			this.toolBar1.Font = ((System.Drawing.Font)(resources.GetObject("toolBar1.Font")));
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
			// toolBarButtonShowPreview
			// 
			this.toolBarButtonShowPreview.Enabled = ((bool)(resources.GetObject("toolBarButtonShowPreview.Enabled")));
			this.toolBarButtonShowPreview.ImageIndex = ((int)(resources.GetObject("toolBarButtonShowPreview.ImageIndex")));
			this.toolBarButtonShowPreview.Pushed = true;
			this.toolBarButtonShowPreview.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.toolBarButtonShowPreview.Text = resources.GetString("toolBarButtonShowPreview.Text");
			this.toolBarButtonShowPreview.ToolTipText = resources.GetString("toolBarButtonShowPreview.ToolTipText");
			this.toolBarButtonShowPreview.Visible = ((bool)(resources.GetObject("toolBarButtonShowPreview.Visible")));
			// 
			// tabPage2
			// 
			this.tabPage2.AccessibleDescription = resources.GetString("tabPage2.AccessibleDescription");
			this.tabPage2.AccessibleName = resources.GetString("tabPage2.AccessibleName");
			this.tabPage2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPage2.Anchor")));
			this.tabPage2.AutoScroll = ((bool)(resources.GetObject("tabPage2.AutoScroll")));
			this.tabPage2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPage2.AutoScrollMargin")));
			this.tabPage2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPage2.AutoScrollMinSize")));
			this.tabPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage2.BackgroundImage")));
			this.tabPage2.Controls.Add(this.m_PreviewAndInfo);
			this.tabPage2.Controls.Add(this.panel2);
			this.tabPage2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPage2.Dock")));
			this.tabPage2.Enabled = ((bool)(resources.GetObject("tabPage2.Enabled")));
			this.tabPage2.Font = ((System.Drawing.Font)(resources.GetObject("tabPage2.Font")));
			this.tabPage2.ImageIndex = ((int)(resources.GetObject("tabPage2.ImageIndex")));
			this.tabPage2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPage2.ImeMode")));
			this.tabPage2.Location = ((System.Drawing.Point)(resources.GetObject("tabPage2.Location")));
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPage2.RightToLeft")));
			this.tabPage2.Size = ((System.Drawing.Size)(resources.GetObject("tabPage2.Size")));
			this.tabPage2.TabIndex = ((int)(resources.GetObject("tabPage2.TabIndex")));
			this.tabPage2.Text = resources.GetString("tabPage2.Text");
			this.tabPage2.ToolTipText = resources.GetString("tabPage2.ToolTipText");
			this.tabPage2.Visible = ((bool)(resources.GetObject("tabPage2.Visible")));
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
			this.m_PreviewAndInfo.LableVisble = false;
			this.m_PreviewAndInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_PreviewAndInfo.Location")));
			this.m_PreviewAndInfo.Name = "m_PreviewAndInfo";
			this.m_PreviewAndInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PreviewAndInfo.RightToLeft")));
			this.m_PreviewAndInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.Size")));
			this.m_PreviewAndInfo.TabIndex = ((int)(resources.GetObject("m_PreviewAndInfo.TabIndex")));
			this.m_PreviewAndInfo.Visible = ((bool)(resources.GetObject("m_PreviewAndInfo.Visible")));
			this.m_PreviewAndInfo.JobInfoChanged += new System.EventHandler(this.m_PreviewAndInfo_JobInfoChanged);
			// 
			// panel2
			// 
			this.panel2.AccessibleDescription = resources.GetString("panel2.AccessibleDescription");
			this.panel2.AccessibleName = resources.GetString("panel2.AccessibleName");
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel2.Anchor")));
			this.panel2.AutoScroll = ((bool)(resources.GetObject("panel2.AutoScroll")));
			this.panel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMargin")));
			this.panel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel2.AutoScrollMinSize")));
			this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.Controls.Add(this.buttonNext);
			this.panel2.Controls.Add(this.buttonPre);
			this.panel2.Divider = false;
			this.panel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel2.Dock")));
			this.panel2.Enabled = ((bool)(resources.GetObject("panel2.Enabled")));
			this.panel2.Font = ((System.Drawing.Font)(resources.GetObject("panel2.Font")));
			this.panel2.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.panel2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.panel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel2.ImeMode")));
			this.panel2.Location = ((System.Drawing.Point)(resources.GetObject("panel2.Location")));
			this.panel2.Name = "panel2";
			this.panel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel2.RightToLeft")));
			this.panel2.Size = ((System.Drawing.Size)(resources.GetObject("panel2.Size")));
			this.panel2.TabIndex = ((int)(resources.GetObject("panel2.TabIndex")));
			this.panel2.TabStop = false;
			this.panel2.Visible = ((bool)(resources.GetObject("panel2.Visible")));
			this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
			// 
			// buttonNext
			// 
			this.buttonNext.AccessibleDescription = resources.GetString("buttonNext.AccessibleDescription");
			this.buttonNext.AccessibleName = resources.GetString("buttonNext.AccessibleName");
			this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonNext.Anchor")));
			this.buttonNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNext.BackgroundImage")));
			this.buttonNext.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonNext.Dock")));
			this.buttonNext.Enabled = ((bool)(resources.GetObject("buttonNext.Enabled")));
			this.buttonNext.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonNext.FlatStyle")));
			this.buttonNext.Font = ((System.Drawing.Font)(resources.GetObject("buttonNext.Font")));
			this.buttonNext.Image = ((System.Drawing.Image)(resources.GetObject("buttonNext.Image")));
			this.buttonNext.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonNext.ImageAlign")));
			this.buttonNext.ImageIndex = ((int)(resources.GetObject("buttonNext.ImageIndex")));
			this.buttonNext.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonNext.ImeMode")));
			this.buttonNext.Location = ((System.Drawing.Point)(resources.GetObject("buttonNext.Location")));
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonNext.RightToLeft")));
			this.buttonNext.Size = ((System.Drawing.Size)(resources.GetObject("buttonNext.Size")));
			this.buttonNext.TabIndex = ((int)(resources.GetObject("buttonNext.TabIndex")));
			this.buttonNext.TabStop = false;
			this.buttonNext.Text = resources.GetString("buttonNext.Text");
			this.buttonNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonNext.TextAlign")));
			this.buttonNext.Visible = ((bool)(resources.GetObject("buttonNext.Visible")));
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonPre
			// 
			this.buttonPre.AccessibleDescription = resources.GetString("buttonPre.AccessibleDescription");
			this.buttonPre.AccessibleName = resources.GetString("buttonPre.AccessibleName");
			this.buttonPre.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonPre.Anchor")));
			this.buttonPre.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPre.BackgroundImage")));
			this.buttonPre.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonPre.Dock")));
			this.buttonPre.Enabled = ((bool)(resources.GetObject("buttonPre.Enabled")));
			this.buttonPre.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonPre.FlatStyle")));
			this.buttonPre.Font = ((System.Drawing.Font)(resources.GetObject("buttonPre.Font")));
			this.buttonPre.Image = ((System.Drawing.Image)(resources.GetObject("buttonPre.Image")));
			this.buttonPre.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonPre.ImageAlign")));
			this.buttonPre.ImageIndex = ((int)(resources.GetObject("buttonPre.ImageIndex")));
			this.buttonPre.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonPre.ImeMode")));
			this.buttonPre.Location = ((System.Drawing.Point)(resources.GetObject("buttonPre.Location")));
			this.buttonPre.Name = "buttonPre";
			this.buttonPre.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonPre.RightToLeft")));
			this.buttonPre.Size = ((System.Drawing.Size)(resources.GetObject("buttonPre.Size")));
			this.buttonPre.TabIndex = ((int)(resources.GetObject("buttonPre.TabIndex")));
			this.buttonPre.TabStop = false;
			this.buttonPre.Text = resources.GetString("buttonPre.Text");
			this.buttonPre.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonPre.TextAlign")));
			this.buttonPre.Visible = ((bool)(resources.GetObject("buttonPre.Visible")));
			this.buttonPre.Click += new System.EventHandler(this.buttonPre_Click);
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
			this.panel1.Controls.Add(this.printingJobInfo1);
			this.panel1.Controls.Add(this.buttonJoblist);
			this.panel1.Controls.Add(this.buttonPreview);
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
			// printingJobInfo1
			// 
			this.printingJobInfo1.AccessibleDescription = resources.GetString("printingJobInfo1.AccessibleDescription");
			this.printingJobInfo1.AccessibleName = resources.GetString("printingJobInfo1.AccessibleName");
			this.printingJobInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("printingJobInfo1.Anchor")));
			this.printingJobInfo1.AutoScroll = ((bool)(resources.GetObject("printingJobInfo1.AutoScroll")));
			this.printingJobInfo1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("printingJobInfo1.AutoScrollMargin")));
			this.printingJobInfo1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("printingJobInfo1.AutoScrollMinSize")));
			this.printingJobInfo1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("printingJobInfo1.BackgroundImage")));
			this.printingJobInfo1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("printingJobInfo1.Dock")));
			this.printingJobInfo1.Enabled = ((bool)(resources.GetObject("printingJobInfo1.Enabled")));
			this.printingJobInfo1.Font = ((System.Drawing.Font)(resources.GetObject("printingJobInfo1.Font")));
			this.printingJobInfo1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("printingJobInfo1.ImeMode")));
			this.printingJobInfo1.JobInfoString = "";
			this.printingJobInfo1.Location = ((System.Drawing.Point)(resources.GetObject("printingJobInfo1.Location")));
			this.printingJobInfo1.Name = "printingJobInfo1";
			this.printingJobInfo1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("printingJobInfo1.RightToLeft")));
			this.printingJobInfo1.Size = ((System.Drawing.Size)(resources.GetObject("printingJobInfo1.Size")));
			this.printingJobInfo1.StatusString = "";
			this.printingJobInfo1.TabIndex = ((int)(resources.GetObject("printingJobInfo1.TabIndex")));
			this.printingJobInfo1.TabStop = false;
			this.printingJobInfo1.Visible = ((bool)(resources.GetObject("printingJobInfo1.Visible")));
			// 
			// buttonJoblist
			// 
			this.buttonJoblist.AccessibleDescription = resources.GetString("buttonJoblist.AccessibleDescription");
			this.buttonJoblist.AccessibleName = resources.GetString("buttonJoblist.AccessibleName");
			this.buttonJoblist.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonJoblist.Anchor")));
			this.buttonJoblist.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonJoblist.BackgroundImage")));
			this.buttonJoblist.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonJoblist.Dock")));
			this.buttonJoblist.Enabled = ((bool)(resources.GetObject("buttonJoblist.Enabled")));
			this.buttonJoblist.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonJoblist.FlatStyle")));
			this.buttonJoblist.Font = ((System.Drawing.Font)(resources.GetObject("buttonJoblist.Font")));
			this.buttonJoblist.Image = ((System.Drawing.Image)(resources.GetObject("buttonJoblist.Image")));
			this.buttonJoblist.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonJoblist.ImageAlign")));
			this.buttonJoblist.ImageIndex = ((int)(resources.GetObject("buttonJoblist.ImageIndex")));
			this.buttonJoblist.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonJoblist.ImeMode")));
			this.buttonJoblist.Location = ((System.Drawing.Point)(resources.GetObject("buttonJoblist.Location")));
			this.buttonJoblist.Name = "buttonJoblist";
			this.buttonJoblist.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonJoblist.RightToLeft")));
			this.buttonJoblist.Size = ((System.Drawing.Size)(resources.GetObject("buttonJoblist.Size")));
			this.buttonJoblist.TabIndex = ((int)(resources.GetObject("buttonJoblist.TabIndex")));
			this.buttonJoblist.TabStop = false;
			this.buttonJoblist.Text = resources.GetString("buttonJoblist.Text");
			this.buttonJoblist.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonJoblist.TextAlign")));
			this.buttonJoblist.Visible = ((bool)(resources.GetObject("buttonJoblist.Visible")));
			this.buttonJoblist.Click += new System.EventHandler(this.button2_Click);
			// 
			// buttonPreview
			// 
			this.buttonPreview.AccessibleDescription = resources.GetString("buttonPreview.AccessibleDescription");
			this.buttonPreview.AccessibleName = resources.GetString("buttonPreview.AccessibleName");
			this.buttonPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonPreview.Anchor")));
			this.buttonPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPreview.BackgroundImage")));
			this.buttonPreview.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonPreview.Dock")));
			this.buttonPreview.Enabled = ((bool)(resources.GetObject("buttonPreview.Enabled")));
			this.buttonPreview.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonPreview.FlatStyle")));
			this.buttonPreview.Font = ((System.Drawing.Font)(resources.GetObject("buttonPreview.Font")));
			this.buttonPreview.Image = ((System.Drawing.Image)(resources.GetObject("buttonPreview.Image")));
			this.buttonPreview.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonPreview.ImageAlign")));
			this.buttonPreview.ImageIndex = ((int)(resources.GetObject("buttonPreview.ImageIndex")));
			this.buttonPreview.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonPreview.ImeMode")));
			this.buttonPreview.Location = ((System.Drawing.Point)(resources.GetObject("buttonPreview.Location")));
			this.buttonPreview.Name = "buttonPreview";
			this.buttonPreview.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonPreview.RightToLeft")));
			this.buttonPreview.Size = ((System.Drawing.Size)(resources.GetObject("buttonPreview.Size")));
			this.buttonPreview.TabIndex = ((int)(resources.GetObject("buttonPreview.TabIndex")));
			this.buttonPreview.TabStop = false;
			this.buttonPreview.Text = resources.GetString("buttonPreview.Text");
			this.buttonPreview.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonPreview.TextAlign")));
			this.buttonPreview.Visible = ((bool)(resources.GetObject("buttonPreview.Visible")));
			this.buttonPreview.Click += new System.EventHandler(this.button1_Click);
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
			this.m_PrintInformation.TabStop = false;
			this.m_PrintInformation.Visible = ((bool)(resources.GetObject("m_PrintInformation.Visible")));
			this.m_PrintInformation.StartButtonClicked += new System.EventHandler(this.m_PrintInformation_StartButtonClicked);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = ((System.Drawing.Size)(resources.GetObject("imageList1.ImageSize")));
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.Setting,
																						 this.m_CMenuItemTools});
			this.contextMenu1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("contextMenu1.RightToLeft")));
			// 
			// Setting
			// 
			this.Setting.Enabled = ((bool)(resources.GetObject("Setting.Enabled")));
			this.Setting.Index = 0;
			this.Setting.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.menuItemSave,
																					this.menuItemLoad,
																					this.menuItemSaveToPrinter,
																					this.menuItemLoadFromPrinter});
			this.Setting.OwnerDraw = true;
			this.Setting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("Setting.Shortcut")));
			this.Setting.ShowShortcut = ((bool)(resources.GetObject("Setting.ShowShortcut")));
			this.Setting.Text = resources.GetString("Setting.Text");
			this.Setting.Visible = ((bool)(resources.GetObject("Setting.Visible")));
			// 
			// menuItemSave
			// 
			this.menuItemSave.Enabled = ((bool)(resources.GetObject("menuItemSave.Enabled")));
			this.menuItemSave.Index = 0;
			this.menuItemSave.OwnerDraw = true;
			this.menuItemSave.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemSave.Shortcut")));
			this.menuItemSave.ShowShortcut = ((bool)(resources.GetObject("menuItemSave.ShowShortcut")));
			this.menuItemSave.Text = resources.GetString("menuItemSave.Text");
			this.menuItemSave.Visible = ((bool)(resources.GetObject("menuItemSave.Visible")));
			this.menuItemSave.Click += new System.EventHandler(this.m_MenuItemSave_Click);
			// 
			// menuItemLoad
			// 
			this.menuItemLoad.Enabled = ((bool)(resources.GetObject("menuItemLoad.Enabled")));
			this.menuItemLoad.Index = 1;
			this.menuItemLoad.OwnerDraw = true;
			this.menuItemLoad.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemLoad.Shortcut")));
			this.menuItemLoad.ShowShortcut = ((bool)(resources.GetObject("menuItemLoad.ShowShortcut")));
			this.menuItemLoad.Text = resources.GetString("menuItemLoad.Text");
			this.menuItemLoad.Visible = ((bool)(resources.GetObject("menuItemLoad.Visible")));
			this.menuItemLoad.Click += new System.EventHandler(this.m_MenuItemLoad_Click);
			// 
			// menuItemSaveToPrinter
			// 
			this.menuItemSaveToPrinter.Enabled = ((bool)(resources.GetObject("menuItemSaveToPrinter.Enabled")));
			this.menuItemSaveToPrinter.Index = 2;
			this.menuItemSaveToPrinter.OwnerDraw = true;
			this.menuItemSaveToPrinter.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemSaveToPrinter.Shortcut")));
			this.menuItemSaveToPrinter.ShowShortcut = ((bool)(resources.GetObject("menuItemSaveToPrinter.ShowShortcut")));
			this.menuItemSaveToPrinter.Text = resources.GetString("menuItemSaveToPrinter.Text");
			this.menuItemSaveToPrinter.Visible = ((bool)(resources.GetObject("menuItemSaveToPrinter.Visible")));
			this.menuItemSaveToPrinter.Click += new System.EventHandler(this.m_MenuItemSaveToPrinter_Click);
			// 
			// menuItemLoadFromPrinter
			// 
			this.menuItemLoadFromPrinter.Enabled = ((bool)(resources.GetObject("menuItemLoadFromPrinter.Enabled")));
			this.menuItemLoadFromPrinter.Index = 3;
			this.menuItemLoadFromPrinter.OwnerDraw = true;
			this.menuItemLoadFromPrinter.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemLoadFromPrinter.Shortcut")));
			this.menuItemLoadFromPrinter.ShowShortcut = ((bool)(resources.GetObject("menuItemLoadFromPrinter.ShowShortcut")));
			this.menuItemLoadFromPrinter.Text = resources.GetString("menuItemLoadFromPrinter.Text");
			this.menuItemLoadFromPrinter.Visible = ((bool)(resources.GetObject("menuItemLoadFromPrinter.Visible")));
			this.menuItemLoadFromPrinter.Click += new System.EventHandler(this.m_MenuItemLoadFromPrinter_Click);
			// 
			// m_CMenuItemTools
			// 
			this.m_CMenuItemTools.Enabled = ((bool)(resources.GetObject("m_CMenuItemTools.Enabled")));
			this.m_CMenuItemTools.Index = 1;
			this.m_CMenuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.menuItemUpdate,
																							 this.menuItemPassWord,
																							 this.menuItemDemoPage,
																							 this.menuItemCalibrationWizard,
																							 this.menuItemHWSetting,
																							 this.menuItemRealSetting,
																							 this.menuItemUVSetting});
			this.m_CMenuItemTools.OwnerDraw = true;
			this.m_CMenuItemTools.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_CMenuItemTools.Shortcut")));
			this.m_CMenuItemTools.ShowShortcut = ((bool)(resources.GetObject("m_CMenuItemTools.ShowShortcut")));
			this.m_CMenuItemTools.Text = resources.GetString("m_CMenuItemTools.Text");
			this.m_CMenuItemTools.Visible = ((bool)(resources.GetObject("m_CMenuItemTools.Visible")));
			// 
			// menuItemUpdate
			// 
			this.menuItemUpdate.Enabled = ((bool)(resources.GetObject("menuItemUpdate.Enabled")));
			this.menuItemUpdate.Index = 0;
			this.menuItemUpdate.OwnerDraw = true;
			this.menuItemUpdate.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemUpdate.Shortcut")));
			this.menuItemUpdate.ShowShortcut = ((bool)(resources.GetObject("menuItemUpdate.ShowShortcut")));
			this.menuItemUpdate.Text = resources.GetString("menuItemUpdate.Text");
			this.menuItemUpdate.Visible = ((bool)(resources.GetObject("menuItemUpdate.Visible")));
			this.menuItemUpdate.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
			// 
			// menuItemPassWord
			// 
			this.menuItemPassWord.Enabled = ((bool)(resources.GetObject("menuItemPassWord.Enabled")));
			this.menuItemPassWord.Index = 1;
			this.menuItemPassWord.OwnerDraw = true;
			this.menuItemPassWord.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemPassWord.Shortcut")));
			this.menuItemPassWord.ShowShortcut = ((bool)(resources.GetObject("menuItemPassWord.ShowShortcut")));
			this.menuItemPassWord.Text = resources.GetString("menuItemPassWord.Text");
			this.menuItemPassWord.Visible = ((bool)(resources.GetObject("menuItemPassWord.Visible")));
			this.menuItemPassWord.Click += new System.EventHandler(this.m_MenuItemPassword_Click);
			// 
			// menuItemDemoPage
			// 
			this.menuItemDemoPage.Enabled = ((bool)(resources.GetObject("menuItemDemoPage.Enabled")));
			this.menuItemDemoPage.Index = 2;
			this.menuItemDemoPage.OwnerDraw = true;
			this.menuItemDemoPage.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemDemoPage.Shortcut")));
			this.menuItemDemoPage.ShowShortcut = ((bool)(resources.GetObject("menuItemDemoPage.ShowShortcut")));
			this.menuItemDemoPage.Text = resources.GetString("menuItemDemoPage.Text");
			this.menuItemDemoPage.Visible = ((bool)(resources.GetObject("menuItemDemoPage.Visible")));
			this.menuItemDemoPage.Click += new System.EventHandler(this.m_MenuItemDemoPage_Click);
			// 
			// menuItemCalibrationWizard
			// 
			this.menuItemCalibrationWizard.Enabled = ((bool)(resources.GetObject("menuItemCalibrationWizard.Enabled")));
			this.menuItemCalibrationWizard.Index = 3;
			this.menuItemCalibrationWizard.OwnerDraw = true;
			this.menuItemCalibrationWizard.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemCalibrationWizard.Shortcut")));
			this.menuItemCalibrationWizard.ShowShortcut = ((bool)(resources.GetObject("menuItemCalibrationWizard.ShowShortcut")));
			this.menuItemCalibrationWizard.Text = resources.GetString("menuItemCalibrationWizard.Text");
			this.menuItemCalibrationWizard.Visible = ((bool)(resources.GetObject("menuItemCalibrationWizard.Visible")));
			this.menuItemCalibrationWizard.Click += new System.EventHandler(this.m_MenuItemCalibraion_Click);
			// 
			// menuItemHWSetting
			// 
			this.menuItemHWSetting.Enabled = ((bool)(resources.GetObject("menuItemHWSetting.Enabled")));
			this.menuItemHWSetting.Index = 4;
			this.menuItemHWSetting.OwnerDraw = true;
			this.menuItemHWSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemHWSetting.Shortcut")));
			this.menuItemHWSetting.ShowShortcut = ((bool)(resources.GetObject("menuItemHWSetting.ShowShortcut")));
			this.menuItemHWSetting.Text = resources.GetString("menuItemHWSetting.Text");
			this.menuItemHWSetting.Visible = ((bool)(resources.GetObject("menuItemHWSetting.Visible")));
			this.menuItemHWSetting.Click += new System.EventHandler(this.m_MenuItemHWSetting_Click);
			// 
			// menuItemRealSetting
			// 
			this.menuItemRealSetting.Enabled = ((bool)(resources.GetObject("menuItemRealSetting.Enabled")));
			this.menuItemRealSetting.Index = 5;
			this.menuItemRealSetting.OwnerDraw = true;
			this.menuItemRealSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemRealSetting.Shortcut")));
			this.menuItemRealSetting.ShowShortcut = ((bool)(resources.GetObject("menuItemRealSetting.ShowShortcut")));
			this.menuItemRealSetting.Text = resources.GetString("menuItemRealSetting.Text");
			this.menuItemRealSetting.Visible = ((bool)(resources.GetObject("menuItemRealSetting.Visible")));
			this.menuItemRealSetting.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
			// 
			// menuItemUVSetting
			// 
			this.menuItemUVSetting.Enabled = ((bool)(resources.GetObject("menuItemUVSetting.Enabled")));
			this.menuItemUVSetting.Index = 6;
			this.menuItemUVSetting.OwnerDraw = true;
			this.menuItemUVSetting.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItemUVSetting.Shortcut")));
			this.menuItemUVSetting.ShowShortcut = ((bool)(resources.GetObject("menuItemUVSetting.ShowShortcut")));
			this.menuItemUVSetting.Text = resources.GetString("menuItemUVSetting.Text");
			this.menuItemUVSetting.Visible = ((bool)(resources.GetObject("menuItemUVSetting.Visible")));
			this.menuItemUVSetting.Click += new System.EventHandler(this.m_MenuItemUVSetting_Click);
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
			this.Controls.Add(this.m_PrintInformation);
			this.Controls.Add(this.m_StatusBarApp);
			this.Controls.Add(this.m_ToolBarCommand);
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
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainApp_Closing);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.m_PanelToolBarSetting.ResumeLayout(false);
			this.m_PanelJobList.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panelSubToolBar.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
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
			UpdateButtonStates(status);
			SetPrinterStatusChanged(status);
			if(status == JetStatusEnum.Error)
			{
				OnErrorCodeChanged(CoreInterface.GetBoardError());
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
				if(SPrinterProperty.EPSONLCD_DEFINED)
				{
					this.OnPrinterSettingChange(m_allParam.PrinterSetting);
				}
				this.ShowMeasureQuestionForm(this.Visible);
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
				);
			if(bNoRealPage)
			{
				this.m_MenuItemRealTime.Visible = false;
			}
			else
				this.m_MenuItemRealTime.Visible = true;

			if(!sp.bSupportUV)
			{
				this.m_MenuItemUVSetting.Visible = false;
			}
			else
				this.m_MenuItemUVSetting.Visible = true;

			this.UpdateFormHeaderText();
		}

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_allParam.PrinterSetting = ss;
			if(SPrinterProperty.EPSONLCD_DEFINED)
			{
				//从FW读取epson的各项参数,初始化m_epsinAllParam
				EpsonLCD.GetCaliConfig(ref m_allParam.EpsonAllParam.sCaliConfig);
				EpsonLCD.GetCleaningOption(ref m_allParam.EpsonAllParam.sCLEANPARA);
				EpsonLCD.GetEPR_FactoryData_Ex(ref m_allParam.EpsonAllParam.sEPR_FactoryData_Ex);
				EpsonLCD.GetHeadparameter(ref m_allParam.EpsonAllParam.headParameterPercent);
				EpsonLCD.GetMainUI_Param(ref m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
				EpsonLCD.GetMedia_Info(ref m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
				EpsonLCD.GetPrint_Quality(ref m_allParam.EpsonAllParam.sUSB_Print_Quality);
			}
			m_ToolbarSetting.OnPrinterSettingChange(ss,this.m_allParam.EpsonAllParam);
			m_PreviewAndInfo.OnPrinterSettingChange(ss);
			m_JobListForm.OnPrinterSettingChange(ss);
			if(m_FuncSettingForm != null)
				m_FuncSettingForm.OnPrinterSettingChange(ss,m_allParam.EpsonAllParam);
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
			m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref this.m_allParam.EpsonAllParam);
			CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
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
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting,this.m_allParam.EpsonAllParam);
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

			menuItemRealSetting.Enabled = m_MenuItemRealTime.Enabled = printerOperate.CanSaveLoadSettings;
			menuItemUVSetting.Enabled = m_MenuItemUVSetting.Enabled = printerOperate.CanSaveLoadSettings;
			m_CMenuItemTools.Enabled = m_MenuItemTools.Enabled = printerOperate.CanUpdate;
			menuItemSaveToPrinter.Enabled = m_MenuItemSaveToPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			menuItemLoadFromPrinter.Enabled = m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemCalibraion.Enabled = printerOperate.CanUpdate;

			m_ToolBarButtonPrint.Enabled = m_MenuItemPrint.Enabled = printerOperate.CanPrint && m_MenuItemCalibraion.Enabled;
			m_ToolBarButtonSingleClean.Enabled =m_ToolBarButtonSingleClean.Pushed || printerOperate.CanClean;
			m_ToolBarButtonSpray.Enabled = m_ToolBarButtonSpray.Pushed ||printerOperate.CanClean;
			m_ToolBarButtonAutoClean.Enabled = m_ToolBarButtonAutoClean.Pushed||printerOperate.CanClean;

			m_ToolBarButtonLeft.Enabled = printerOperate.CanMoveLeft;
			m_ToolBarButtonRight.Enabled = printerOperate.CanMoveRight;
			m_ToolBarButtonForward.Enabled =printerOperate.CanMoveForward;
			m_ToolBarButtonBackward.Enabled =printerOperate.CanMoveBackward;
			m_ToolBarButtonDownZ.Enabled =printerOperate.CanMoveForward;
			m_ToolBarButtonUpZ.Enabled =printerOperate.CanMoveBackward;
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
		}


		private bool MainForm_KeyDownEvent(Keys keyData)
		{
			bool blnProcess = false;
			if(keyData == Keys.Left )
			{
				if(m_ToolBarButtonLeft.Enabled)
				{
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Left,0);
					m_ToolBarButtonLeft.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData == Keys.Right )
			{
				if(m_ToolBarButtonRight.Enabled)
				{
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Right,0);
					m_ToolBarButtonRight.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData == Keys.Up )
			{
				if(m_ToolBarButtonBackward.Enabled)
				{
					if(m_allParam.PrinterProperty.nMediaType == 2)
						CoreInterface.MoveCmd((int)MoveDirectionEnum.Down,0);
					else
						CoreInterface.MoveCmd((int)MoveDirectionEnum.Up,0);
					m_ToolBarButtonBackward.Pushed = true;
					m_bSendMoveCmd = true;
					blnProcess = true;
				}
			}
			else if(keyData ==Keys.Down)
			{
				if(m_ToolBarButtonForward.Enabled)
				{
					if(m_allParam.PrinterProperty.nMediaType == 2)
						CoreInterface.MoveCmd((int)MoveDirectionEnum.Up,0);
					else
						CoreInterface.MoveCmd((int)MoveDirectionEnum.Down,0);
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
				m_ToolBarButtonSpray.Visible = true;
			}
			else 
				//if(sp.bSupportAutoClean == false)
			{
				m_ToolBarButtonSpray.Style = ToolBarButtonStyle.PushButton;
				m_ToolBarButtonSpray.Visible = true;
			}
			//else
			//	m_ToolBarButtonSpray.Visible = false;

			if(sp.bSupportAutoClean)
			{
				m_ToolBarButtonAutoClean.Visible = true;
			}
			else
			{
				m_ToolBarButtonAutoClean.Visible = false;
			}
			if(sp.eSingleClean == SingleCleanEnum.None)
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

			if(sp.bSupportPaperSensor == true)
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
		
					int startType	= lParam.ToInt32();

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
						SPrinterProperty  sPrinterProperty;
						SPrinterSetting sPrinterSetting;
						
						m_allParam.PowerOnEvent(out bPropertyChanged, out bSettingChanged,out sPrinterProperty,out sPrinterSetting);
						SPrinterProperty.UpdateEPSONLCDSupport(sPrinterProperty.ePrinterHead);
						if(bPropertyChanged != 0)
						{
							OnPrinterPropertyChange(sPrinterProperty);
						}
						if(bSettingChanged != 0)
						{
							OnPrinterSettingChange(sPrinterSetting);
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
								UIJob printingjob = m_JobListForm.PrintingJob;
								if( printingjob!=null && !printingjob.Equals(mPrintingjob))
								{
									result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
									if(result == DialogResult.Cancel)
									{
										//CoreInterface.Printer_Abort();
										if(CoreInterface.Printer_IsOpen() != 0)
											m_JobListForm.AbortPrintingJob();						
									}
									else
									{
										this.mPrintingjob = printingjob;
									}
								}
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
					switch( lParam.ToInt32())
					{
						case 0x2: //see struct USB_RPT_MainUI_Param
							EpsonLCD.GetMainUI_Param(ref this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
							m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							break;
						case 0x6://羽化
							EpsonLCD.GetPrint_Quality(ref this.m_allParam.EpsonAllParam.sUSB_Print_Quality);
							if(m_FuncSettingForm != null)
								m_FuncSettingForm.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							break;
						case 0x5://Media_Info
							EpsonLCD.GetMedia_Info(ref this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
							if(m_FuncSettingForm != null)
								m_FuncSettingForm.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
							break;
					}
				}
					break;
			}
		}

		private void UpdateViewMode(int mode)
		{
			//			UIViewMode uimode  = (UIViewMode)mode;
			//			switch(uimode)
			//			{
			//				case UIViewMode.LeftRight:
			////					this.m_FolderPreview.Visible = true;
			////					this.m_WorkForldersplitter.Enabled = true;
			////					this.m_WorkForlderpanel.Dock = DockStyle.Right;
			////					this.m_WorkForlderpanel.Width = this.Width/3;
			////					m_SplitterJobList.Dock = DockStyle.Right;
			//					m_PreviewAndInfo.Dock = DockStyle.Fill;
			//					this.toolBarButtonAddToList.Visible = true;
			//					this.m_JobListForm.m_JobListView.View = View.Details;
			//					this.toolBar1.SendToBack();
			//					break;
			//				case UIViewMode.TopDown:
			//				case UIViewMode.NotifyIcon:
			//				default:
			////					this.m_FolderPreview.Visible = false;
			////					this.m_WorkForldersplitter.Enabled = false;
			////					this.m_WorkForlderpanel.Dock = DockStyle.Bottom;
			////					this.m_WorkForlderpanel.Height = this.Height/3;
			////					m_SplitterJobList.Dock = DockStyle.Bottom;
			//					m_PreviewAndInfo.Dock = DockStyle.Fill;
			//					this.toolBarButtonAddToList.Visible = false;
			//					this.m_JobListForm.m_JobListView.View = View.LargeIcon;
			//					this.m_JobListForm.mAlignment = ListViewAlignment.Left;
			//					break;
			//			}
			////			this.m_SplitterJobList.SendToBack();
			////			this.m_WorkForlderpanel.SendToBack();
			//			this.m_SplitterStatus.SendToBack();
			//			this.m_PrintInformation.SendToBack();
		}

		#endregion

		#region public

		public bool Start()
		{
			if(!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle))
				return m_bExitAtStart;
			SystemCall.PreventSystemPowerdown();
			m_allParam = new AllParam();
			SystemInit init = new SystemInit(this,this.Handle,m_KernelMessage);
			init.SystemStart();

			m_PortManager = new PortManager();
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
			return true;
		}
		public void OnErrorCodeChanged(int code)
		{
			this.m_StatusBarPanelError.Text = SErrorCode.GetInfoFromErrCode(code);
			this.m_PrintInformation.printJobInfomation(code,UserLevel.user);
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
				CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
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
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
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
		}

		public void OnPrintingStart()
		{
			m_StartTime = DateTime.Now;
			m_JobListForm.OnPrintingStart();
		}
		public void OnPrintingEnd()
		{
			m_JobListForm.OnPrintingEnd();
			m_PreviewAndInfo.UpdatePercentage(0);
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
			MyWndProc.DrawToolbarBackGroundImage(pevent.Graphics,this.m_ToolBarCommand.Bounds,null);
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
			this.ShowMeasureQuestionForm(true);
			this.toolBar1.ImageList = this.imageList1;
			if(this.m_JobListForm.m_JobListView.SmallImageList == null)
				this.m_JobListForm.m_JobListView.SmallImageList = this.m_JobListForm.m_JobListView.LargeImageList;
			//			if(m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
			//				this.m_FolderPreview.ReLoadItems();
			this.m_StartMenu.MenuItems.AddRange(new MenuItem[]{this.m_MenuItemSetting,this.m_MenuItemTools,this.m_MenuItemHelp,this.m_MenuItemDebug});
//			this.menuImage2.SetMenuImage(m_MenuItemSetting,"0");
//			this.menuImage2.SetMenuImage(m_MenuItemTools,"1");
//			this.menuImage2.SetMenuImage(m_MenuItemHelp,"2");
//			this.menuImage2.SetMenuImage(m_MenuItemDebug,"3");
			// Define Sample Grouper
			m_GroupboxStyle= new Grouper();
			m_GroupboxStyle.BackgroundGradientMode = GroupBoxGradientMode.Vertical;
			m_GroupboxStyle.BackgroundImage = null;
			m_GroupboxStyle.BorderThickness = 1F;
			m_GroupboxStyle.GradientColors = new Style(Color.LightBlue, Color.SteelBlue);
			m_GroupboxStyle.GroupImage = null;
			m_GroupboxStyle.PaintGroupBox = false;
			m_GroupboxStyle.RoundCorners = 10;
			m_GroupboxStyle.ShadowColor = Color.DarkGray;
			m_GroupboxStyle.ShadowControl = false;
			m_GroupboxStyle.ShadowThickness = 3;
			m_GroupboxStyle.TitileGradientColors = new Style(Color.LightBlue, Color.SteelBlue);
			m_GroupboxStyle.TitleStyle = TitleStyles.XPStyle;
		}


		private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			MainForm_KeyDownEvent(e.KeyData);
			if (e.Modifiers == Keys.Control)
			{
				if(e.KeyCode == Keys.P) 
				{
					m_JobListForm.PrintJob();
					this.tabControl1.SelectedIndex = 1;
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
			}
		}

		private void m_MenuItemRealTime_Click(object sender, System.EventArgs e)
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
				form.ApplyToBoard();
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
					fwData1.m_nReserve = new byte[60-22];
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
						m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting, this.m_allParam.EpsonAllParam);
					}

				}
			}
	
		}

		#endregion

		#region Tool Strip 事件
		private void m_ToolBarCommand_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
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
				this.tabControl1.SelectedIndex = 1;
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
					if(m_allParam.PrinterProperty.ZMeasurSensorSupport())
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
		}


		private void m_ToolBarCommand_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( m_ToolBarButtonLeft.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonLeft.Enabled)
			{
				CoreInterface.MoveCmd((int)MoveDirectionEnum.Left,0);
				m_ToolBarButtonLeft.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonRight.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonRight.Enabled)
			{
				CoreInterface.MoveCmd((int)MoveDirectionEnum.Right,0);
				m_ToolBarButtonRight.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonForward.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonForward.Enabled)
			{
				if(m_allParam.PrinterProperty.nMediaType == 2)
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Up,0);
				else 
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Down,0);

				m_ToolBarButtonForward.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonBackward.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonBackward.Enabled)
			{
				if(m_allParam.PrinterProperty.nMediaType == 2)
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Down,0);
				else
					CoreInterface.MoveCmd((int)MoveDirectionEnum.Up,0);
				m_ToolBarButtonBackward.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonDownZ.Rectangle.Contains(e.X,e.Y)&&m_ToolBarButtonDownZ.Enabled)
			{
				CoreInterface.MoveCmd((int)MoveDirectionEnum.Down_Z,0);
				m_ToolBarButtonDownZ.Pushed = true;
				m_bSendMoveCmd = true;
			}
			else if(m_ToolBarButtonUpZ.Rectangle.Contains(e.X,e.Y) &&m_ToolBarButtonUpZ.Enabled)
			{
				CoreInterface.MoveCmd((int)MoveDirectionEnum.Up_Z,0);
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
				fbd.SelectedPath = this.m_JobListForm.FolderName;
				if(fbd.ShowDialog(this) == DialogResult.OK)
				{
					this.Cursor = Cursors.WaitCursor;
					this.m_JobListForm.FolderName = fbd.SelectedPath;
					this.Cursor = Cursors.Default;
				}
			}
			else if	(e.Button == this.toolBarButtonRefresh)
			{
				this.m_JobListForm.ReLoadItems();
			}
			else if(e.Button == this.toolBarButtonAddToList)
			{
				//				this.AddSelectedToList();
			}
			else if(e.Button == this.toolBarButtonSelectAll)
			{
				this.m_JobListForm.SelectAll();
			}
			else if(e.Button == this.toolBarButtonShowPreview)
			{
				this.m_JobListForm.m_JobListView.SuspendLayout();
				if(this.m_JobListForm.m_JobListView.SmallImageList.ImageSize != this.m_JobListForm.m_JobListView.LargeImageList.ImageSize)
					this.m_JobListForm.m_JobListView.SmallImageList = this.m_JobListForm.m_JobListView.LargeImageList;
				else
				{
					ImageList il = new ImageList();
					il.ImageSize = new Size(1,1);
					this.m_JobListForm.m_JobListView.SmallImageList = il;
				}
				this.m_JobListForm.m_JobListView.ResumeLayout(true);
				this.m_JobListForm.ScrollSeletedControlIntoView();
			}
		}


		private void button2_Click(object sender, System.EventArgs e)
		{
			this.tabControl1.SelectedIndex = 0;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.tabControl1.SelectedIndex = 1;
		}

		private void buttonPre_Click(object sender, System.EventArgs e)
		{
			if(this.m_JobListForm.m_JobListView.Items.Count == 0)
				return;

			if(this.m_JobListForm.SelectedCount != 0)
			{
				if(this.m_JobListForm.m_JobListView.SelectedIndices[0] == (this.m_JobListForm.m_JobListView.Items.Count -1)
					&& this.m_JobListForm.m_JobListView.Items.Count != 1)
					this.buttonNext.Enabled = true;

				int i = this.m_JobListForm.m_JobListView.SelectedIndices[0];
				if(this.m_JobListForm.m_JobListView.Items.Count != 1)
					this.m_JobListForm.m_JobListView.SelectedItems.Clear();
				if(i - 1 >= 0)
					this.m_JobListForm.m_JobListView.Items[i - 1].Selected = true;
				if(i - 1 == 0)
					this.buttonPre.Enabled = false;
			}
			else
			{
				this.m_JobListForm.m_JobListView.Items[0].Selected = true;
				this.buttonPre.Enabled = false;
				this.buttonNext.Enabled = true;
			}
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			if(this.m_JobListForm.m_JobListView.Items.Count == 0)
				return;

			if(this.m_JobListForm.SelectedCount != 0)
			{
				if(this.m_JobListForm.m_JobListView.SelectedIndices[0] == 0	&& this.m_JobListForm.m_JobListView.Items.Count != 1)
					this.buttonPre.Enabled = true;

				int i = this.m_JobListForm.m_JobListView.SelectedIndices[0];
				if(this.m_JobListForm.m_JobListView.Items.Count != 1)
					this.m_JobListForm.m_JobListView.SelectedItems.Clear();
				if(i + 1 < this.m_JobListForm.m_JobListView.Items.Count)
					this.m_JobListForm.m_JobListView.Items[i + 1].Selected = true;
				if(i + 2 == this.m_JobListForm.m_JobListView.Items.Count)
					this.buttonNext.Enabled = false;
			}
			else
			{
				this.m_JobListForm.m_JobListView.Items[0].Selected = true;
				this.buttonPre.Enabled = false;
			}
		}


		private void m_JobListForm_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.printingJobInfo1.StatusString = this.m_PreviewAndInfo.crystalLabel1.Text;
			this.printingJobInfo1.JobInfoString = this.m_PreviewAndInfo.m_LabelPrintingJobInfo.Text;

			if(this.m_JobListForm.m_JobListView.Items.Count > 1 && this.m_JobListForm.SelectedCount != 0)
			{
				if(this.m_JobListForm.m_JobListView.SelectedIndices[0] == 0)
				{
					this.buttonPre.Enabled = false;
					this.buttonNext.Enabled = true;
				}
				else if(this.m_JobListForm.m_JobListView.SelectedIndices[0] == this.m_JobListForm.m_JobListView.Items.Count -1)
				{
					this.buttonPre.Enabled = true;
					this.buttonNext.Enabled = false;
				}
			}
		}

		private void panel2_Resize(object sender, System.EventArgs e)
		{
			int margin = 100;
			this.buttonPre.Location = new Point((this.panel2.Width - this.buttonPre.Width * 2 - margin)/2,this.buttonPre.Location.Y);
			this.buttonNext.Location = new Point(this.buttonPre.Location.X + this.buttonPre.Width + margin,this.buttonPre.Location.Y);
		}


		private void m_PrintInformation_StartButtonClicked(object sender, System.EventArgs e)
		{
			Point cml = new Point(0,  -SystemInformation.MenuHeight * m_StartMenu.MenuItems.Count);
			this.m_StartMenu.Show(this.m_PrintInformation,cml);
		}
		#endregion

		private void m_PreviewAndInfo_JobInfoChanged(object sender, System.EventArgs e)
		{
			this.printingJobInfo1.StatusString = this.m_PreviewAndInfo.crystalLabel1.Text;
			this.printingJobInfo1.JobInfoString = this.m_PreviewAndInfo.m_LabelPrintingJobInfo.Text;
		}

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
				if(SPrinterProperty.IsKonica512 (sp.ePrinterHead))
					headNum /= 2;
				else if(SPrinterProperty.IsPolaris (sp.ePrinterHead))
				{
					headNum /= 4;
				}
				else if(SPrinterProperty.IsEpson_Gen5_ALLWIN(sp.ePrinterHead)||SPrinterProperty.IsEpson_Gen5_MICOLOR(sp.ePrinterHead))
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
			if(mainformloaded && status != JetStatusEnum.PowerOff && m_allParam.PrinterProperty.ZMeasurSensorSupport())// PubFunc.IsVender92())
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
	}
}
