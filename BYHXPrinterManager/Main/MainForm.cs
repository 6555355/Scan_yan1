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
using System.Reflection;

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
		const int   WM_KEYDOWN  = 0x0100;
		const int   WM_KEYUP    = 0x0101;
		const int   WM_SYSKEYDOWN =0x0104;
		const int	WM_SYSKEYUP   = 0x0105;
		const int WM_POWERBROADCAST = 0x0218;

		const int PBT_APMQUERYSUSPEND         =    0x0000;
		const int PBT_APMSUSPEND              =    0x0004;
		IntPtr BROADCAST_QUERY_DENY		  =	 new IntPtr(0x424D5144);

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
		private BYHXPrinterManager.Setting.ToolbarSetting m_ToolbarSetting;
		private System.Windows.Forms.ToolBar m_ToolBarCommand;
		private System.Windows.Forms.Panel m_PanelToolBarSetting;
		private System.Windows.Forms.Panel m_PanelJobList;
		private BYHXPrinterManager.Preview.PrintingInfo m_PreviewAndInfo;
		private BYHXPrinterManager.JobListView.JobListForm m_JobListForm;
		private System.Windows.Forms.StatusBar m_StatusBarApp;
		private System.Windows.Forms.Splitter m_SplitterStatus;
		private System.Windows.Forms.Splitter m_SplitterJobList;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
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
			PubFunc.IconReload(m_ToolbarImageList);
			// Define Sample Grouper
			m_GroupboxStyle= new Grouper();
			m_GroupboxStyle.TitleStyle = TitleStyles.Standard;
			//必须先初始化BYHXSoftLock.m_DongleKeyAlarm
			BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm();
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired += new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
			BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H += new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
			BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished += new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);
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
			this.m_MenuItemView = new System.Windows.Forms.MenuItem();
			this.m_MenuItemTopDown = new System.Windows.Forms.MenuItem();
			this.m_MenuItemLeftRight = new System.Windows.Forms.MenuItem();
			this.m_MenuItemHelp = new System.Windows.Forms.MenuItem();
			this.m_MenuItemAbout = new System.Windows.Forms.MenuItem();
			this.menuItemDongle = new System.Windows.Forms.MenuItem();
			this.menuItemDongleKey = new System.Windows.Forms.MenuItem();
			this.m_MenuItemDebug = new System.Windows.Forms.MenuItem();
			this.m_MenuItemFactoryDebug = new System.Windows.Forms.MenuItem();
			this.m_ToolBarCommand = new System.Windows.Forms.ToolBar();
			this.m_ToolBarButtonAdd = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonAbort = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonPrint = new System.Windows.Forms.ToolBarButton();
			this.m_ToolBarButtonPauseResume = new System.Windows.Forms.ToolBarButton();
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
			this.m_ToolbarSetting = new BYHXPrinterManager.Setting.ToolbarSetting();
			this.m_PanelToolBarSetting = new System.Windows.Forms.Panel();
			this.m_PanelJobList = new System.Windows.Forms.Panel();
			this.m_PreviewAndInfo = new BYHXPrinterManager.Preview.PrintingInfo();
			this.m_SplitterJobList = new System.Windows.Forms.Splitter();
			this.m_JobListForm = new BYHXPrinterManager.JobListView.JobListForm();
			this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
			this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
			this.m_SplitterStatus = new System.Windows.Forms.Splitter();
			this.m_SplitterToolbar = new System.Windows.Forms.Splitter();
			this.m_PanelToolBarSetting.SuspendLayout();
			this.m_PanelJobList.SuspendLayout();
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
			this.m_MenuItemJob.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemJob.Shortcut")));
			this.m_MenuItemJob.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemJob.ShowShortcut")));
			this.m_MenuItemJob.Text = resources.GetString("m_MenuItemJob.Text");
			this.m_MenuItemJob.Visible = ((bool)(resources.GetObject("m_MenuItemJob.Visible")));
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
			// m_MenuItemPrint
			// 
			this.m_MenuItemPrint.Enabled = ((bool)(resources.GetObject("m_MenuItemPrint.Enabled")));
			this.m_MenuItemPrint.Index = 2;
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
																						   this.m_MenuItemLeftRight});
			this.m_MenuItemView.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemView.Shortcut")));
			this.m_MenuItemView.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemView.ShowShortcut")));
			this.m_MenuItemView.Text = resources.GetString("m_MenuItemView.Text");
			this.m_MenuItemView.Visible = ((bool)(resources.GetObject("m_MenuItemView.Visible")));
			// 
			// m_MenuItemTopDown
			// 
			this.m_MenuItemTopDown.Checked = true;
			this.m_MenuItemTopDown.Enabled = ((bool)(resources.GetObject("m_MenuItemTopDown.Enabled")));
			this.m_MenuItemTopDown.Index = 0;
			this.m_MenuItemTopDown.RadioCheck = true;
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
			this.m_MenuItemLeftRight.RadioCheck = true;
			this.m_MenuItemLeftRight.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("m_MenuItemLeftRight.Shortcut")));
			this.m_MenuItemLeftRight.ShowShortcut = ((bool)(resources.GetObject("m_MenuItemLeftRight.ShowShortcut")));
			this.m_MenuItemLeftRight.Text = resources.GetString("m_MenuItemLeftRight.Text");
			this.m_MenuItemLeftRight.Visible = ((bool)(resources.GetObject("m_MenuItemLeftRight.Visible")));
			this.m_MenuItemLeftRight.Click += new System.EventHandler(this.m_MenuItemLeftRight_Click);
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
																								this.m_ToolBarButtonAbort,
																								this.m_ToolBarButtonPrint,
																								this.m_ToolBarButtonPauseResume,
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
			// m_ToolBarButtonAbort
			// 
			this.m_ToolBarButtonAbort.Enabled = ((bool)(resources.GetObject("m_ToolBarButtonAbort.Enabled")));
			this.m_ToolBarButtonAbort.ImageIndex = ((int)(resources.GetObject("m_ToolBarButtonAbort.ImageIndex")));
			this.m_ToolBarButtonAbort.Text = resources.GetString("m_ToolBarButtonAbort.Text");
			this.m_ToolBarButtonAbort.ToolTipText = resources.GetString("m_ToolBarButtonAbort.ToolTipText");
			this.m_ToolBarButtonAbort.Visible = ((bool)(resources.GetObject("m_ToolBarButtonAbort.Visible")));
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
			// m_ToolbarSetting
			// 
			this.m_ToolbarSetting.AccessibleDescription = resources.GetString("m_ToolbarSetting.AccessibleDescription");
			this.m_ToolbarSetting.AccessibleName = resources.GetString("m_ToolbarSetting.AccessibleName");
			this.m_ToolbarSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ToolbarSetting.Anchor")));
			this.m_ToolbarSetting.AutoScroll = ((bool)(resources.GetObject("m_ToolbarSetting.AutoScroll")));
			this.m_ToolbarSetting.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.AutoScrollMargin")));
			this.m_ToolbarSetting.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_ToolbarSetting.AutoScrollMinSize")));
			this.m_ToolbarSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ToolbarSetting.BackgroundImage")));
			this.m_ToolbarSetting.Divider = true;
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
			this.m_ToolbarSetting.TransparentMode = true;
			this.m_ToolbarSetting.Visible = ((bool)(resources.GetObject("m_ToolbarSetting.Visible")));
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
			// m_PanelJobList
			// 
			this.m_PanelJobList.AccessibleDescription = resources.GetString("m_PanelJobList.AccessibleDescription");
			this.m_PanelJobList.AccessibleName = resources.GetString("m_PanelJobList.AccessibleName");
			this.m_PanelJobList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PanelJobList.Anchor")));
			this.m_PanelJobList.AutoScroll = ((bool)(resources.GetObject("m_PanelJobList.AutoScroll")));
			this.m_PanelJobList.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PanelJobList.AutoScrollMargin")));
			this.m_PanelJobList.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PanelJobList.AutoScrollMinSize")));
			this.m_PanelJobList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PanelJobList.BackgroundImage")));
			this.m_PanelJobList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_PanelJobList.Controls.Add(this.m_PreviewAndInfo);
			this.m_PanelJobList.Controls.Add(this.m_SplitterJobList);
			this.m_PanelJobList.Controls.Add(this.m_JobListForm);
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
			// m_PreviewAndInfo
			// 
			this.m_PreviewAndInfo.AccessibleDescription = resources.GetString("m_PreviewAndInfo.AccessibleDescription");
			this.m_PreviewAndInfo.AccessibleName = resources.GetString("m_PreviewAndInfo.AccessibleName");
			this.m_PreviewAndInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_PreviewAndInfo.Anchor")));
			this.m_PreviewAndInfo.AutoScroll = ((bool)(resources.GetObject("m_PreviewAndInfo.AutoScroll")));
			this.m_PreviewAndInfo.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.AutoScrollMargin")));
			this.m_PreviewAndInfo.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.AutoScrollMinSize")));
			this.m_PreviewAndInfo.BackColor = System.Drawing.Color.White;
			this.m_PreviewAndInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_PreviewAndInfo.BackgroundImage")));
			this.m_PreviewAndInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_PreviewAndInfo.Dock")));
			this.m_PreviewAndInfo.Enabled = ((bool)(resources.GetObject("m_PreviewAndInfo.Enabled")));
			this.m_PreviewAndInfo.Font = ((System.Drawing.Font)(resources.GetObject("m_PreviewAndInfo.Font")));
			this.m_PreviewAndInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_PreviewAndInfo.ImeMode")));
			this.m_PreviewAndInfo.Location = ((System.Drawing.Point)(resources.GetObject("m_PreviewAndInfo.Location")));
			this.m_PreviewAndInfo.Name = "m_PreviewAndInfo";
			this.m_PreviewAndInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_PreviewAndInfo.RightToLeft")));
			this.m_PreviewAndInfo.Size = ((System.Drawing.Size)(resources.GetObject("m_PreviewAndInfo.Size")));
			this.m_PreviewAndInfo.TabIndex = ((int)(resources.GetObject("m_PreviewAndInfo.TabIndex")));
			this.m_PreviewAndInfo.Visible = ((bool)(resources.GetObject("m_PreviewAndInfo.Visible")));
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
			// m_JobListForm
			// 
			this.m_JobListForm.AccessibleDescription = resources.GetString("m_JobListForm.AccessibleDescription");
			this.m_JobListForm.AccessibleName = resources.GetString("m_JobListForm.AccessibleName");
			this.m_JobListForm.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_JobListForm.Anchor")));
			this.m_JobListForm.AutoScroll = ((bool)(resources.GetObject("m_JobListForm.AutoScroll")));
			this.m_JobListForm.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMargin")));
			this.m_JobListForm.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.AutoScrollMinSize")));
			this.m_JobListForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_JobListForm.BackgroundImage")));
			this.m_JobListForm.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_JobListForm.Dock")));
			this.m_JobListForm.Enabled = ((bool)(resources.GetObject("m_JobListForm.Enabled")));
			this.m_JobListForm.Font = ((System.Drawing.Font)(resources.GetObject("m_JobListForm.Font")));
			this.m_JobListForm.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_JobListForm.ImeMode")));
			this.m_JobListForm.Location = ((System.Drawing.Point)(resources.GetObject("m_JobListForm.Location")));
			this.m_JobListForm.Name = "m_JobListForm";
			this.m_JobListForm.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_JobListForm.RightToLeft")));
			this.m_JobListForm.Size = ((System.Drawing.Size)(resources.GetObject("m_JobListForm.Size")));
			this.m_JobListForm.TabIndex = ((int)(resources.GetObject("m_JobListForm.TabIndex")));
			this.m_JobListForm.Visible = ((bool)(resources.GetObject("m_JobListForm.Visible")));
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
			this.Controls.Add(this.m_StatusBarApp);
			this.Controls.Add(this.m_PanelToolBarSetting);
			this.Controls.Add(this.m_SplitterToolbar);
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
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
			this.m_PanelToolBarSetting.ResumeLayout(false);
			this.m_PanelJobList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


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
				if(m_JobListForm.Confirm_Exit())
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
					if(PubFunc.IsVender92())
					{
						this.ShowMeasureQuestionForm(true);
						return;
					}
				}
				else if(e.Button==m_ToolBarButtonStop)
				{
					cmd = JetCmdEnum.StopMove;
				}
				else 
					return;
				CoreInterface.SendJetCommand((int)cmd,cmdvalue);
			}
		}
		}
		private void OnSingleClean()
		{
			CleanForm form = new CleanForm();
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			if(CoreInterface.SendJetCommand((int)JetCmdEnum.EnterSingleCleanMode,0) == 1)
				form.ShowDialog(this);
		}

		private void StartWithInitComponent()
		{
			m_JobListForm.SetPreviewInfo(m_PreviewAndInfo);
			m_allParam = new AllParam();  


			m_JobListForm.SetPrinterChange(this);
			m_ToolbarSetting.SetPrinterChange(this);
			m_PreviewAndInfo.SetPrinterChange(this);

		}

		public bool Start()
		{
			if (!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle))
				return m_bExitAtStart;

		//			SystemCall.PreventSystemPowerdown();
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
			//			SystemCall.AllowSystemPowerdown();
			return true;
		}
		private void FirstReadyEvent()
		{
#if !LIYUUSB
				string vol = ResString.GetResString("FW_Voltage");
				byte [] buffer = System.Text.Encoding.Unicode.GetBytes(vol);
				int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
				CoreInterface.SetFWVoltage(buffer,buffer.Length,lcid);

				///Check version
				///
#if false
				SBoardInfo sBoardInfo = new SBoardInfo();
				if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
				{
					const int MINI_MAINBOARD_VERSION = 0x00020200;
					SFWVersion fwv= new SFWVersion(sBoardInfo.m_nBoradVersion);
					SFWVersion min_fwv= new SFWVersion(MINI_MAINBOARD_VERSION);
					if( (((fwv.m_nMainVersion <<8)+ fwv.m_nSubVersion)<<8) < MINI_MAINBOARD_VERSION)
					{
						string info = "";
						string mPrintingFormat = SErrorCode.GetEnumDisplayName(typeof(Software),Software.VersionNoMatch);
						string curVersion = fwv.m_nMainVersion + "." + fwv.m_nSubVersion;
						string minVersion = min_fwv.m_nMainVersion + "." + min_fwv.m_nSubVersion;
						info += "\n" + string.Format(mPrintingFormat,curVersion,minVersion);
						MessageBox.Show(this, info,"",MessageBoxButtons.OK);
						//m_bExitAtStart = true;
					}
				}
#endif
#else
				CoreInterface.VerifyHeadType();
				//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
				BYHXSoftLock.m_DongleKeyAlarm.FirstReadyShakeHand();
				//MeasureQuestionForm.ShowMeasureQuestionForm(this.Visible,this);
		}
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
				FirstReadyEvent();
			}
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

			JetStatusEnum status = CoreInterface.GetBoardStatus();
			if(status != JetStatusEnum.PowerOff)
			{
				byte headNum =  sp.nHeadNum;
				if(SPrinterProperty.IsKonica512 (sp.ePrinterHead))
					headNum /= 2;
				else if(SPrinterProperty.IsPolaris(sp.ePrinterHead))
				{
					headNum /= 4;
				}

				this.UpdateFormHeaderText();
			}
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
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_allParam.PrinterSetting = ss;
			m_ToolbarSetting.OnPrinterSettingChange(ss,m_allParam.EpsonAllParam);
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
		public void NotifyUIParamChanged()
		{
			m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam.PrinterSetting,ref m_allParam.EpsonAllParam);
			CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
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

		private void MainApp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			End();
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

		public void OnEditPrinterSetting()
		{
			SettingForm form = new SettingForm();
			form.SetGroupBoxStyle(m_GroupboxStyle);
			m_FuncSettingForm = form;
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			//Change the IconButton status for calibration
			form.SetPrinterStatusChanged(printerStatus);
			form.OnPreferenceChange(m_allParam.Preference);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.OnPrinterSettingChange(m_allParam.PrinterSetting,m_allParam.EpsonAllParam);
			form.OnRealTimeChange();
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bChangeProperty = false;
				form.OnGetPrinterSetting(ref m_allParam,ref bChangeProperty);
				CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
				OnPreferenceChange(m_allParam.Preference);
				if(bChangeProperty)
					CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting,m_allParam.EpsonAllParam);
			}
			form = null;
			m_FuncSettingForm = null;
		}

		public void OnSwitchPreview()
		{
			m_JobListForm.OnSwitchPreview();
		}

		public void OnPrintingStart()
		{
			m_StartTime = DateTime.Now;
			m_JobListForm.OnPrintingStart();
			m_ToolbarSetting.OnPrintingStart();
		}
		public void OnPrintingEnd()
		{
			m_JobListForm.OnPrintingEnd();
			m_ToolbarSetting.OnPrintingEnd();
			m_PreviewAndInfo.UpdatePercentage(0);
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
			if(m_hwForm != null)
			{
				m_hwForm.SetPrinterStatusChanged(status);
			}
			if(m_MeasureQuestionForm != null)
			{
				m_MeasureQuestionForm.SetPrinterStatusChanged(status);
			}
			m_ToolbarSetting.SetPrinterStatusChanged(status);
		}

		public void OnErrorCodeChanged(int code)
		{
			if(this.m_allParam.PrinterProperty.nColorNum==4)
			{
				SErrorCode errcode = new SErrorCode(code);
				if(errcode.nErrorAction== (byte)ErrorAction.Warning && errcode.nErrorCause==(byte)ErrorCause.CoreBoard)
				{
					if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTCYAN)
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_CYAN;
					if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTMAGENTA)
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_MAGENTA;
					if( errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR1 )
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_YELLOW;
					if(errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR2)
						errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_BLACK;
					code = errcode.nErrorCode + (errcode.nErrorSub<<8 )+(errcode.nErrorCause<<16 )+(errcode.nErrorAction<<24 ); 
				}
			}
			this.m_StatusBarPanelError.Text = SErrorCode.GetInfoFromErrCode(code);
		}

		public void OnPrintingProgressChanged(int percent)
		{
			string info = "";
			string mPrintingFormat = ResString.GetPrintingProgress();
			info += "\n" + string.Format(mPrintingFormat,percent);
			this.m_StatusBarPanelPercent.Text = info;
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
			m_MenuItemHWSetting.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemTools.Enabled = printerOperate.CanUpdate;
			m_MenuItemSaveToPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;
			m_MenuItemCalibraion.Enabled = m_MenuItemDemoPage.Enabled = printerOperate.CanUpdate;

			m_ToolBarButtonPrint.Enabled = m_MenuItemPrint.Enabled = printerOperate.CanPrint;
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

			m_ToolBarButtonAbort.Enabled = printerOperate.CanAbort;
			m_ToolBarButtonPauseResume.Enabled = (printerOperate.CanPause || printerOperate.CanResume);
			//m_ToolBarButtonPause.Enabled = printerOperate.CanPause; //???????
			//m_ToolBarButtonResume.Enabled = printerOperate.CanResume; //??????
			
		}



		//[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
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
			base.WndProc(ref m);

			if(m.Msg == this.m_KernelMessage)
			{
				ProceedKernelMessage(m.WParam,m.LParam);
			}
			if (m.Msg == 0x0219)//WM_DEVICECHANGE
				BYHXSoftLock.OnDeviceChange(m.WParam, m.LParam);
			if (m.Msg == WM_POWERBROADCAST)//WM_POWERBROADCAST 
			{
				// Check the status and act accordingly.
				int reason = m.WParam.ToInt32();
				switch (reason)
				{
					case PBT_APMQUERYSUSPEND:
						m.Result = BROADCAST_QUERY_DENY;
						break;
					case PBT_APMSUSPEND:
						//						if(m_JobListForm.Confirm_Exit())
						CoreInterface.Printer_Pause();
						break;
				}
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
					info += "\n" + string.Format(mPrintingFormat,percentage);
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
						//DialogResult result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
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
									result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
									if(result == DialogResult.Cancel)
									{
										//CoreInterface.Printer_Abort();
										if(CoreInterface.Printer_IsOpen() != 0)
											m_JobListForm.AbortPrintingJob();						
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
				case CoreMsgEnum.PrinterReady:
					break;

			}
		}

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
					"Are you sure want to update?", 
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
		public void NotifyUICalibrationExit(bool bSave)
		{
			m_MenuItemCalibraion.Enabled = true;
			if(bSave)
			{
				//OnPrinterSettingChange(m_allParam.PrinterSetting);
				m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting,m_allParam.EpsonAllParam);
			}
			m_wizard = null;
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			OnPrinterStatusChanged(printerStatus);
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
		private void UpdateViewMode(int mode)
		{
			UIViewMode uimode  = (UIViewMode)mode;
			switch(uimode)
			{
				case UIViewMode.LeftRight:
					m_JobListForm.Width = m_PanelJobList.Width/2;
					m_JobListForm.Height = m_PanelJobList.Height/2;
					m_JobListForm.Dock = DockStyle.Right; 
					m_SplitterJobList.Dock = DockStyle.Right;
					m_PreviewAndInfo.Dock = DockStyle.Fill;
					m_MenuItemLeftRight.Checked = true;
					m_MenuItemTopDown.Checked = !m_MenuItemLeftRight.Checked;
					break;
				case UIViewMode.TopDown:
				case UIViewMode.NotifyIcon:
				default:
					m_JobListForm.Width = m_PanelJobList.Width/2;
					m_JobListForm.Height = m_PanelJobList.Height/2;
					m_JobListForm.Dock = DockStyle.Bottom; 
					m_SplitterJobList.Dock = DockStyle.Bottom;
					m_PreviewAndInfo.Dock = DockStyle.Fill;
					m_MenuItemTopDown.Checked = true;
					m_MenuItemLeftRight.Checked = !m_MenuItemTopDown.Checked;

					break;
			}
		}

		private void m_MenuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog(this);
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
						m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting,m_allParam.EpsonAllParam);
					}

				}
			}
	
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
			form.OnPreferenceChange(m_allParam.Preference);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.OnPrinterSettingChange(m_allParam.PrinterSetting);
			form.OnRealTimeChange();
			form.SetGroupBoxStyle(m_GroupboxStyle);
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bChangeProperty = false;
				form.OnGetPrinterSetting(ref m_allParam,ref bChangeProperty);
				CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
				form.ApplyToBoard();
			}
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

		public bool PreFilterMessage(ref  Message m)
		{
			if(m.Msg   ==   0x020A)    //MouseWheel
				return   true;   
			//   TODO:     添加   comboNoWheel.PreFilterMessage   实现   
			return   false;   

		}
		protected override void OnResize(EventArgs e) 
		{ 
			if (this.WindowState ==  FormWindowState.Maximized) 
			{ 
				this.FormBorderStyle = FormBorderStyle.FixedDialog;
			} 
			else 
			{
				this.FormBorderStyle = FormBorderStyle.Sizable;
			}
			base.OnResize(e);
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
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Form2 form2 = new Form2();
			form2.ShowDialog(this);
		}

		private void menuItemDongleKey_Click(object sender, System.EventArgs e)
		{
			DongleKeyForm dkf = new DongleKeyForm();
			dkf.ShowDialog();
			if(!BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKey())
				this.Close();
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
#if ADD_HARDKEY
			this.menuItemDongle.Visible = true;
#else
			this.menuItemDongle.Visible = false;
#endif
			//MeasureQuestionForm.ShowMeasureQuestionForm(true,this);
		}

		private void MainForm_Deactivate(object sender, System.EventArgs e)
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
