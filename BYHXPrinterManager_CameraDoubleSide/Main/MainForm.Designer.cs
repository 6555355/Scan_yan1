using System;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
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
        private System.Windows.Forms.TableLayoutPanel m_PanelJobList;
        private BYHXPrinterManager.JobListView.PrintingInfo m_PreviewAndInfo;
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
        private System.Windows.Forms.MenuItem m_MenuItemSelectInkType;
        private System.Windows.Forms.ToolBarButton m_ToolBarButtonSetOriginY;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
        private System.Windows.Forms.StatusBar m_StatusBarApp;
        private BYHXPrinterManager.Setting.ToolbarSetting m_ToolbarSetting;
        private System.Windows.Forms.ImageList UIIconImageList;
        private BYHXPrinterManager.JobListView.JobListForm m_JobListForm;
        private System.Windows.Forms.ImageList SubToolBarimageList;
        private System.Windows.Forms.ContextMenu MenuFolderPreview;
        private System.Windows.Forms.MenuItem menuItemAddToList;
        private System.Windows.Forms.MenuItem menuItemReLoad;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemSelectAll;
        private System.Windows.Forms.MenuItem menuItem4;
        private BYHXPrinterManager.Main.PrintInformation m_PrintInformation;
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
        private MenuItem menuItem3;
        private MenuItem menuItemAdvancedMode;
        private ToolBarButton toolBarButtonDoublePrintCari;
        private MenuItem menuItem5;
        private MenuItem menuItemAutoStopPumpInk;
        private ToolBarButton m_ToolBarButtonZero;
        private MenuItem m_MenuItemWaveFormSetting;
        private MenuItem menuItem6;
        private ToolBarButton toolBarButtonAuxiliaryControl;
        private ToolBarButton m_ToolBarColorSeperationPurge;
        private ToolBarButton toolBarButtonOnlineState;//const big icon size
        private ToolBarButton m_ToolBarButtonSand;
        private MenuItem menuItemMotorParameters;
        private ToolBarButton toolBarButtonSwitchInk;//const big icon size

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.CLockQueue cLockQueue1 = new BYHXPrinterManager.CLockQueue();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            this.m_MainMenu = new System.Windows.Forms.MainMenu(this.components);
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
            this.m_MenuItemSelectInkType = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemMotorParameters = new System.Windows.Forms.MenuItem();
            this.m_MenuItemWaveFormSetting = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemAutoStopPumpInk = new System.Windows.Forms.MenuItem();
            this.m_MenuItemView = new System.Windows.Forms.MenuItem();
            this.m_MenuItemTopDown = new System.Windows.Forms.MenuItem();
            this.m_MenuItemLeftRight = new System.Windows.Forms.MenuItem();
            this.m_MenuItemOldView = new System.Windows.Forms.MenuItem();
            this.m_MenuItemHelp = new System.Windows.Forms.MenuItem();
            this.m_MenuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemAdvancedMode = new System.Windows.Forms.MenuItem();
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
            this.toolBarButtonDoublePrintCari = new System.Windows.Forms.ToolBarButton();
            this.m_ToolBarButtonZero = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonAuxiliaryControl = new System.Windows.Forms.ToolBarButton();
            this.m_ToolBarColorSeperationPurge = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOnlineState = new System.Windows.Forms.ToolBarButton();
            this.m_ToolBarButtonSand = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSwitchInk = new System.Windows.Forms.ToolBarButton();
            this.m_toolBarButtonRetractableCloth = new System.Windows.Forms.ToolBarButton();
            this.m_ToolbarImageList = new System.Windows.Forms.ImageList(this.components);
            this.m_PanelToolBarSetting = new System.Windows.Forms.Panel();
            this.m_ToolbarSetting = new BYHXPrinterManager.Setting.ToolbarSetting();
            this.m_PanelJobList = new System.Windows.Forms.TableLayoutPanel();
            this.m_JobListForm = new BYHXPrinterManager.JobListView.JobListForm();
            this.m_PreviewAndInfo = new BYHXPrinterManager.JobListView.PrintingInfo();
            this.cameraSettings1 = new BYHXPrinterManager.Main.CameraViewer();
            this.maintenanceStatus1 = new BYHXPrinterManager.Main.MaintenanceStatus();
            this.SubToolBarimageList = new System.Windows.Forms.ImageList(this.components);
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
            this.UIIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.m_StartMenu = new System.Windows.Forms.ContextMenu();
            this.imageListMenu = new System.Windows.Forms.ImageList(this.components);
            this.notifyIconBYHX = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.officeMenus1 = new Dev4Arabs.OfficeMenus(this.components);
            this.m_PrintInformation = new BYHXPrinterManager.Main.PrintInformation();
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
            // 
            // m_MenuItemJob
            // 
            this.m_MenuItemJob.Index = 0;
            this.m_MenuItemJob.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemAdd,
            this.m_MenuItemDelete,
            this.m_MenuItemPrint,
            this.m_MenuItemExit});
            this.m_MenuItemJob.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemJob, "m_MenuItemJob");
            // 
            // m_MenuItemAdd
            // 
            this.m_MenuItemAdd.Index = 0;
            this.m_MenuItemAdd.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemAdd, "m_MenuItemAdd");
            this.m_MenuItemAdd.Click += new System.EventHandler(this.m_MenuItemAdd_Click);
            // 
            // m_MenuItemDelete
            // 
            this.m_MenuItemDelete.Index = 1;
            this.m_MenuItemDelete.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemDelete, "m_MenuItemDelete");
            this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
            // 
            // m_MenuItemPrint
            // 
            this.m_MenuItemPrint.Index = 2;
            this.m_MenuItemPrint.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemPrint, "m_MenuItemPrint");
            this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
            // 
            // m_MenuItemExit
            // 
            this.m_MenuItemExit.Index = 3;
            this.m_MenuItemExit.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemExit, "m_MenuItemExit");
            this.m_MenuItemExit.Click += new System.EventHandler(this.m_MenuItemExit_Click);
            // 
            // m_MenuItemSetting
            // 
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
            resources.ApplyResources(this.m_MenuItemSetting, "m_MenuItemSetting");
            // 
            // m_MenuItemSave
            // 
            this.m_MenuItemSave.Index = 0;
            this.m_MenuItemSave.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemSave, "m_MenuItemSave");
            this.m_MenuItemSave.Click += new System.EventHandler(this.m_MenuItemSave_Click);
            // 
            // m_MenuItemLoad
            // 
            this.m_MenuItemLoad.Index = 1;
            this.m_MenuItemLoad.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemLoad, "m_MenuItemLoad");
            this.m_MenuItemLoad.Click += new System.EventHandler(this.m_MenuItemLoad_Click);
            // 
            // m_MenuItemSaveToPrinter
            // 
            this.m_MenuItemSaveToPrinter.Index = 2;
            this.m_MenuItemSaveToPrinter.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemSaveToPrinter, "m_MenuItemSaveToPrinter");
            this.m_MenuItemSaveToPrinter.Click += new System.EventHandler(this.m_MenuItemSaveToPrinter_Click);
            // 
            // m_MenuItemLoadFromPrinter
            // 
            this.m_MenuItemLoadFromPrinter.Index = 3;
            this.m_MenuItemLoadFromPrinter.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemLoadFromPrinter, "m_MenuItemLoadFromPrinter");
            this.m_MenuItemLoadFromPrinter.Click += new System.EventHandler(this.m_MenuItemLoadFromPrinter_Click);
            // 
            // m_MenuItemEdit
            // 
            this.m_MenuItemEdit.Index = 4;
            this.m_MenuItemEdit.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemEdit, "m_MenuItemEdit");
            this.m_MenuItemEdit.Click += new System.EventHandler(this.m_MenuItemEdit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItemSaveCaliPara
            // 
            this.menuItemSaveCaliPara.Index = 6;
            resources.ApplyResources(this.menuItemSaveCaliPara, "menuItemSaveCaliPara");
            this.menuItemSaveCaliPara.Click += new System.EventHandler(this.menuItemSaveCaliPara_Click);
            // 
            // menuItemLoadCaliPara
            // 
            this.menuItemLoadCaliPara.Index = 7;
            resources.ApplyResources(this.menuItemLoadCaliPara, "menuItemLoadCaliPara");
            this.menuItemLoadCaliPara.Click += new System.EventHandler(this.menuItemLoadCaliPara_Click);
            // 
            // m_MenuItemTools
            // 
            this.m_MenuItemTools.Index = 2;
            this.m_MenuItemTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemUpdate,
            this.m_MenuItemPassword,
            this.m_MenuItemDemoPage,
            this.m_MenuItemCalibraion,
            this.m_MenuItemHWSetting,
            this.m_MenuItemRealTime,
            this.m_MenuItemUVSetting,
            this.m_MenuItemSelectInkType,
            this.menuItem5,
            this.menuItemMotorParameters,
            this.m_MenuItemWaveFormSetting,
            this.menuItem6,
            this.menuItemAutoStopPumpInk});
            resources.ApplyResources(this.m_MenuItemTools, "m_MenuItemTools");
            // 
            // m_MenuItemUpdate
            // 
            this.m_MenuItemUpdate.Index = 0;
            this.m_MenuItemUpdate.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemUpdate, "m_MenuItemUpdate");
            this.m_MenuItemUpdate.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
            // 
            // m_MenuItemPassword
            // 
            this.m_MenuItemPassword.Index = 1;
            this.m_MenuItemPassword.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemPassword, "m_MenuItemPassword");
            this.m_MenuItemPassword.Click += new System.EventHandler(this.m_MenuItemPassword_Click);
            // 
            // m_MenuItemDemoPage
            // 
            this.m_MenuItemDemoPage.Index = 2;
            this.m_MenuItemDemoPage.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemDemoPage, "m_MenuItemDemoPage");
            this.m_MenuItemDemoPage.Click += new System.EventHandler(this.m_MenuItemDemoPage_Click);
            // 
            // m_MenuItemCalibraion
            // 
            this.m_MenuItemCalibraion.Index = 3;
            this.m_MenuItemCalibraion.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemCalibraion, "m_MenuItemCalibraion");
            this.m_MenuItemCalibraion.Click += new System.EventHandler(this.m_MenuItemCalibraion_Click);
            // 
            // m_MenuItemHWSetting
            // 
            this.m_MenuItemHWSetting.Index = 4;
            this.m_MenuItemHWSetting.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemHWSetting, "m_MenuItemHWSetting");
            this.m_MenuItemHWSetting.Click += new System.EventHandler(this.m_MenuItemHWSetting_Click);
            // 
            // m_MenuItemRealTime
            // 
            this.m_MenuItemRealTime.Index = 5;
            this.m_MenuItemRealTime.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemRealTime, "m_MenuItemRealTime");
            this.m_MenuItemRealTime.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
            // 
            // m_MenuItemUVSetting
            // 
            this.m_MenuItemUVSetting.Index = 6;
            this.m_MenuItemUVSetting.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemUVSetting, "m_MenuItemUVSetting");
            this.m_MenuItemUVSetting.Click += new System.EventHandler(this.m_MenuItemUVSetting_Click);
            // 
            // m_MenuItemSelectInkType
            // 
            this.m_MenuItemSelectInkType.Index = 7;
            this.m_MenuItemSelectInkType.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemSelectInkType, "m_MenuItemSelectInkType");
            this.m_MenuItemSelectInkType.Click += new System.EventHandler(this.m_MenuItemSelectInkType_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 8;
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // menuItemMotorParameters
            // 
            this.menuItemMotorParameters.Index = 9;
            resources.ApplyResources(this.menuItemMotorParameters, "menuItemMotorParameters");
            this.menuItemMotorParameters.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // m_MenuItemWaveFormSetting
            // 
            this.m_MenuItemWaveFormSetting.Index = 10;
            resources.ApplyResources(this.m_MenuItemWaveFormSetting, "m_MenuItemWaveFormSetting");
            this.m_MenuItemWaveFormSetting.Click += new System.EventHandler(this.ItemWaveSetting_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 11;
            resources.ApplyResources(this.menuItem6, "menuItem6");
            // 
            // menuItemAutoStopPumpInk
            // 
            this.menuItemAutoStopPumpInk.Index = 12;
            resources.ApplyResources(this.menuItemAutoStopPumpInk, "menuItemAutoStopPumpInk");
            this.menuItemAutoStopPumpInk.Click += new System.EventHandler(this.menuItemAutoStopPumpInk_Click);
            // 
            // m_MenuItemView
            // 
            this.m_MenuItemView.Index = 3;
            this.m_MenuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemTopDown,
            this.m_MenuItemLeftRight,
            this.m_MenuItemOldView});
            resources.ApplyResources(this.m_MenuItemView, "m_MenuItemView");
            // 
            // m_MenuItemTopDown
            // 
            this.m_MenuItemTopDown.Index = 0;
            this.m_MenuItemTopDown.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemTopDown, "m_MenuItemTopDown");
            this.m_MenuItemTopDown.Click += new System.EventHandler(this.m_MenuItemTopDown_Click);
            // 
            // m_MenuItemLeftRight
            // 
            this.m_MenuItemLeftRight.Index = 1;
            this.m_MenuItemLeftRight.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemLeftRight, "m_MenuItemLeftRight");
            this.m_MenuItemLeftRight.Click += new System.EventHandler(this.m_MenuItemLeftRight_Click);
            // 
            // m_MenuItemOldView
            // 
            this.m_MenuItemOldView.Index = 2;
            this.m_MenuItemOldView.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemOldView, "m_MenuItemOldView");
            this.m_MenuItemOldView.Click += new System.EventHandler(this.m_MenuItemOldView_Click);
            // 
            // m_MenuItemHelp
            // 
            this.m_MenuItemHelp.Index = 4;
            this.m_MenuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemAbout,
            this.menuItem3,
            this.menuItemAdvancedMode});
            resources.ApplyResources(this.m_MenuItemHelp, "m_MenuItemHelp");
            // 
            // m_MenuItemAbout
            // 
            this.m_MenuItemAbout.Index = 0;
            this.m_MenuItemAbout.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemAbout, "m_MenuItemAbout");
            this.m_MenuItemAbout.Click += new System.EventHandler(this.m_MenuItemAbout_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItemAdvancedMode
            // 
            this.menuItemAdvancedMode.Index = 2;
            resources.ApplyResources(this.menuItemAdvancedMode, "menuItemAdvancedMode");
            this.menuItemAdvancedMode.Click += new System.EventHandler(this.menuItemAdvancedMode_Click);
            // 
            // menuItemDongle
            // 
            this.menuItemDongle.Index = 5;
            this.menuItemDongle.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDongleKey});
            resources.ApplyResources(this.menuItemDongle, "menuItemDongle");
            // 
            // menuItemDongleKey
            // 
            this.menuItemDongleKey.Index = 0;
            resources.ApplyResources(this.menuItemDongleKey, "menuItemDongleKey");
            this.menuItemDongleKey.Click += new System.EventHandler(this.menuItemDongleKey_Click);
            // 
            // m_MenuItemDebug
            // 
            this.m_MenuItemDebug.Index = 6;
            this.m_MenuItemDebug.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_MenuItemFactoryDebug});
            resources.ApplyResources(this.m_MenuItemDebug, "m_MenuItemDebug");
            // 
            // m_MenuItemFactoryDebug
            // 
            this.m_MenuItemFactoryDebug.Index = 0;
            this.m_MenuItemFactoryDebug.OwnerDraw = true;
            resources.ApplyResources(this.m_MenuItemFactoryDebug, "m_MenuItemFactoryDebug");
            this.m_MenuItemFactoryDebug.Click += new System.EventHandler(this.m_MenuItemFactoryDebug_Click);
            // 
            // m_ToolBarCommand
            // 
            resources.ApplyResources(this.m_ToolBarCommand, "m_ToolBarCommand");
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
            this.m_ToolBarButtonEdit,
            this.toolBarButtonDoublePrintCari,
            this.m_ToolBarButtonZero,
            this.toolBarButtonAuxiliaryControl,
            this.m_ToolBarColorSeperationPurge,
            this.toolBarButtonOnlineState,
            this.m_ToolBarButtonSand,
            this.toolBarButtonSwitchInk,
            this.m_toolBarButtonRetractableCloth});
            this.m_ToolBarCommand.Divider = false;
            this.m_ToolBarCommand.ImageList = this.m_ToolbarImageList;
            this.m_ToolBarCommand.Name = "m_ToolBarCommand";
            this.m_ToolBarCommand.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.m_ToolBarCommand_ButtonClick);
            this.m_ToolBarCommand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseDown);
            this.m_ToolBarCommand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            // 
            // m_ToolBarButtonAdd
            // 
            resources.ApplyResources(this.m_ToolBarButtonAdd, "m_ToolBarButtonAdd");
            this.m_ToolBarButtonAdd.Name = "m_ToolBarButtonAdd";
            // 
            // m_ToolBarButtonDelete
            // 
            resources.ApplyResources(this.m_ToolBarButtonDelete, "m_ToolBarButtonDelete");
            this.m_ToolBarButtonDelete.Name = "m_ToolBarButtonDelete";
            // 
            // m_ToolBarButtonPrint
            // 
            resources.ApplyResources(this.m_ToolBarButtonPrint, "m_ToolBarButtonPrint");
            this.m_ToolBarButtonPrint.Name = "m_ToolBarButtonPrint";
            // 
            // m_ToolBarButtonPauseResume
            // 
            resources.ApplyResources(this.m_ToolBarButtonPauseResume, "m_ToolBarButtonPauseResume");
            this.m_ToolBarButtonPauseResume.Name = "m_ToolBarButtonPauseResume";
            // 
            // m_ToolBarButtonAbort
            // 
            resources.ApplyResources(this.m_ToolBarButtonAbort, "m_ToolBarButtonAbort");
            this.m_ToolBarButtonAbort.Name = "m_ToolBarButtonAbort";
            // 
            // m_ToolBarButtonSep1
            // 
            this.m_ToolBarButtonSep1.Name = "m_ToolBarButtonSep1";
            this.m_ToolBarButtonSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // m_ToolBarButtonCheckNozzle
            // 
            resources.ApplyResources(this.m_ToolBarButtonCheckNozzle, "m_ToolBarButtonCheckNozzle");
            this.m_ToolBarButtonCheckNozzle.Name = "m_ToolBarButtonCheckNozzle";
            // 
            // m_ToolBarButtonAutoClean
            // 
            resources.ApplyResources(this.m_ToolBarButtonAutoClean, "m_ToolBarButtonAutoClean");
            this.m_ToolBarButtonAutoClean.Name = "m_ToolBarButtonAutoClean";
            // 
            // m_ToolBarButtonSpray
            // 
            resources.ApplyResources(this.m_ToolBarButtonSpray, "m_ToolBarButtonSpray");
            this.m_ToolBarButtonSpray.Name = "m_ToolBarButtonSpray";
            this.m_ToolBarButtonSpray.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // m_ToolBarButtonStop
            // 
            resources.ApplyResources(this.m_ToolBarButtonStop, "m_ToolBarButtonStop");
            this.m_ToolBarButtonStop.Name = "m_ToolBarButtonStop";
            // 
            // m_ToolBarButtonSingleClean
            // 
            resources.ApplyResources(this.m_ToolBarButtonSingleClean, "m_ToolBarButtonSingleClean");
            this.m_ToolBarButtonSingleClean.Name = "m_ToolBarButtonSingleClean";
            this.m_ToolBarButtonSingleClean.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // m_ToolBarButtonSep2
            // 
            this.m_ToolBarButtonSep2.Name = "m_ToolBarButtonSep2";
            this.m_ToolBarButtonSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // m_ToolBarButtonLeft
            // 
            resources.ApplyResources(this.m_ToolBarButtonLeft, "m_ToolBarButtonLeft");
            this.m_ToolBarButtonLeft.Name = "m_ToolBarButtonLeft";
            // 
            // m_ToolBarButtonRight
            // 
            resources.ApplyResources(this.m_ToolBarButtonRight, "m_ToolBarButtonRight");
            this.m_ToolBarButtonRight.Name = "m_ToolBarButtonRight";
            // 
            // m_ToolBarButtonSetOrigin
            // 
            resources.ApplyResources(this.m_ToolBarButtonSetOrigin, "m_ToolBarButtonSetOrigin");
            this.m_ToolBarButtonSetOrigin.Name = "m_ToolBarButtonSetOrigin";
            // 
            // m_ToolBarButtonForward
            // 
            resources.ApplyResources(this.m_ToolBarButtonForward, "m_ToolBarButtonForward");
            this.m_ToolBarButtonForward.Name = "m_ToolBarButtonForward";
            // 
            // m_ToolBarButtonBackward
            // 
            resources.ApplyResources(this.m_ToolBarButtonBackward, "m_ToolBarButtonBackward");
            this.m_ToolBarButtonBackward.Name = "m_ToolBarButtonBackward";
            // 
            // m_ToolBarButtonSetOriginY
            // 
            resources.ApplyResources(this.m_ToolBarButtonSetOriginY, "m_ToolBarButtonSetOriginY");
            this.m_ToolBarButtonSetOriginY.Name = "m_ToolBarButtonSetOriginY";
            // 
            // m_ToolBarButtonDownZ
            // 
            resources.ApplyResources(this.m_ToolBarButtonDownZ, "m_ToolBarButtonDownZ");
            this.m_ToolBarButtonDownZ.Name = "m_ToolBarButtonDownZ";
            // 
            // m_ToolBarButtonUpZ
            // 
            resources.ApplyResources(this.m_ToolBarButtonUpZ, "m_ToolBarButtonUpZ");
            this.m_ToolBarButtonUpZ.Name = "m_ToolBarButtonUpZ";
            // 
            // m_ToolBarButtonSep3
            // 
            this.m_ToolBarButtonSep3.Name = "m_ToolBarButtonSep3";
            this.m_ToolBarButtonSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // m_ToolBarButtonGoHome
            // 
            resources.ApplyResources(this.m_ToolBarButtonGoHome, "m_ToolBarButtonGoHome");
            this.m_ToolBarButtonGoHome.Name = "m_ToolBarButtonGoHome";
            // 
            // m_ToolBarButtonGoHomeY
            // 
            resources.ApplyResources(this.m_ToolBarButtonGoHomeY, "m_ToolBarButtonGoHomeY");
            this.m_ToolBarButtonGoHomeY.Name = "m_ToolBarButtonGoHomeY";
            // 
            // m_ToolBarButtonGoHomeZ
            // 
            resources.ApplyResources(this.m_ToolBarButtonGoHomeZ, "m_ToolBarButtonGoHomeZ");
            this.m_ToolBarButtonGoHomeZ.Name = "m_ToolBarButtonGoHomeZ";
            // 
            // m_ToolBarButtonMeasurePaper
            // 
            resources.ApplyResources(this.m_ToolBarButtonMeasurePaper, "m_ToolBarButtonMeasurePaper");
            this.m_ToolBarButtonMeasurePaper.Name = "m_ToolBarButtonMeasurePaper";
            // 
            // m_ToolBarButtonEdit
            // 
            resources.ApplyResources(this.m_ToolBarButtonEdit, "m_ToolBarButtonEdit");
            this.m_ToolBarButtonEdit.Name = "m_ToolBarButtonEdit";
            // 
            // toolBarButtonDoublePrintCari
            // 
            resources.ApplyResources(this.toolBarButtonDoublePrintCari, "toolBarButtonDoublePrintCari");
            this.toolBarButtonDoublePrintCari.Name = "toolBarButtonDoublePrintCari";
            // 
            // m_ToolBarButtonZero
            // 
            resources.ApplyResources(this.m_ToolBarButtonZero, "m_ToolBarButtonZero");
            this.m_ToolBarButtonZero.Name = "m_ToolBarButtonZero";
            // 
            // toolBarButtonAuxiliaryControl
            // 
            resources.ApplyResources(this.toolBarButtonAuxiliaryControl, "toolBarButtonAuxiliaryControl");
            this.toolBarButtonAuxiliaryControl.Name = "toolBarButtonAuxiliaryControl";
            // 
            // m_ToolBarColorSeperationPurge
            // 
            resources.ApplyResources(this.m_ToolBarColorSeperationPurge, "m_ToolBarColorSeperationPurge");
            this.m_ToolBarColorSeperationPurge.Name = "m_ToolBarColorSeperationPurge";
            // 
            // toolBarButtonOnlineState
            // 
            resources.ApplyResources(this.toolBarButtonOnlineState, "toolBarButtonOnlineState");
            this.toolBarButtonOnlineState.Name = "toolBarButtonOnlineState";
            // 
            // m_ToolBarButtonSand
            // 
            resources.ApplyResources(this.m_ToolBarButtonSand, "m_ToolBarButtonSand");
            this.m_ToolBarButtonSand.Name = "m_ToolBarButtonSand";
            // 
            // toolBarButtonSwitchInk
            // 
            resources.ApplyResources(this.toolBarButtonSwitchInk, "toolBarButtonSwitchInk");
            this.toolBarButtonSwitchInk.Name = "toolBarButtonSwitchInk";
            // 
            // m_toolBarButtonRetractableCloth
            // 
            resources.ApplyResources(this.m_toolBarButtonRetractableCloth, "m_toolBarButtonRetractableCloth");
            this.m_toolBarButtonRetractableCloth.Name = "m_toolBarButtonRetractableCloth";
            this.m_toolBarButtonRetractableCloth.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // m_ToolbarImageList
            // 
            this.m_ToolbarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ToolbarImageList.ImageStream")));
            this.m_ToolbarImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_ToolbarImageList.Images.SetKeyName(0, "");
            this.m_ToolbarImageList.Images.SetKeyName(1, "");
            this.m_ToolbarImageList.Images.SetKeyName(2, "");
            this.m_ToolbarImageList.Images.SetKeyName(3, "");
            this.m_ToolbarImageList.Images.SetKeyName(4, "");
            this.m_ToolbarImageList.Images.SetKeyName(5, "");
            this.m_ToolbarImageList.Images.SetKeyName(6, "");
            this.m_ToolbarImageList.Images.SetKeyName(7, "");
            this.m_ToolbarImageList.Images.SetKeyName(8, "");
            this.m_ToolbarImageList.Images.SetKeyName(9, "");
            this.m_ToolbarImageList.Images.SetKeyName(10, "");
            this.m_ToolbarImageList.Images.SetKeyName(11, "");
            this.m_ToolbarImageList.Images.SetKeyName(12, "");
            this.m_ToolbarImageList.Images.SetKeyName(13, "");
            this.m_ToolbarImageList.Images.SetKeyName(14, "");
            this.m_ToolbarImageList.Images.SetKeyName(15, "");
            this.m_ToolbarImageList.Images.SetKeyName(16, "");
            this.m_ToolbarImageList.Images.SetKeyName(17, "");
            this.m_ToolbarImageList.Images.SetKeyName(18, "");
            this.m_ToolbarImageList.Images.SetKeyName(19, "");
            this.m_ToolbarImageList.Images.SetKeyName(20, "");
            this.m_ToolbarImageList.Images.SetKeyName(21, "");
            this.m_ToolbarImageList.Images.SetKeyName(22, "");
            //this.m_ToolbarImageList.Images.SetKeyName(23, "");
            //this.m_ToolbarImageList.Images.SetKeyName(24, "");
            //this.m_ToolbarImageList.Images.SetKeyName(25, "");
            //this.m_ToolbarImageList.Images.SetKeyName(26, "");
            //this.m_ToolbarImageList.Images.SetKeyName(27, "");
            //this.m_ToolbarImageList.Images.SetKeyName(28, "");
            // 
            // m_PanelToolBarSetting
            // 
            this.m_PanelToolBarSetting.Controls.Add(this.m_ToolbarSetting);
            resources.ApplyResources(this.m_PanelToolBarSetting, "m_PanelToolBarSetting");
            this.m_PanelToolBarSetting.Name = "m_PanelToolBarSetting";
            // 
            // m_ToolbarSetting
            // 
            this.m_ToolbarSetting.BackColor = System.Drawing.SystemColors.Window;
            this.m_ToolbarSetting.Divider = false;
            this.m_ToolbarSetting.DividerSide = System.Windows.Forms.Border3DSide.Top;
            resources.ApplyResources(this.m_ToolbarSetting, "m_ToolbarSetting");
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.m_ToolbarSetting.GradientColors = style1;
            this.m_ToolbarSetting.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_ToolbarSetting.GrouperTitleStyle = null;
            this.m_ToolbarSetting.Name = "m_ToolbarSetting";
            this.m_ToolbarSetting.VerticalDirection = false;
            // 
            // m_PanelJobList
            // 
            resources.ApplyResources(this.m_PanelJobList, "m_PanelJobList");
            this.m_PanelJobList.Controls.Add(this.m_JobListForm, 1, 1);
            this.m_PanelJobList.Controls.Add(this.m_PreviewAndInfo, 0, 0);
            this.m_PanelJobList.Controls.Add(this.cameraSettings1, 2, 0);
            this.m_PanelJobList.Controls.Add(this.maintenanceStatus1, 0, 1);
            this.m_PanelJobList.Name = "m_PanelJobList";
            // 
            // m_JobListForm
            // 
            this.m_JobListForm.BackColor = System.Drawing.SystemColors.Control;
            this.m_JobListForm.CustomRowBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.m_JobListForm, "m_JobListForm");
            this.m_JobListForm.FolderName = null;
            this.m_JobListForm.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_JobListForm.GridLines = true;
            this.m_JobListForm.JobIndex = 0;
            this.m_JobListForm.LockQueue = cLockQueue1;
            this.m_JobListForm.mAlignment = System.Windows.Forms.ListViewAlignment.Top;
            this.m_JobListForm.Name = "m_JobListForm";
            this.m_JobListForm.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.m_JobListForm.ThumbNailSize = new System.Drawing.Size(95, 95);
            // 
            // m_PreviewAndInfo
            // 
            this.m_PreviewAndInfo.BackColor = System.Drawing.SystemColors.Window;
            this.m_PanelJobList.SetColumnSpan(this.m_PreviewAndInfo, 2);
            this.m_PreviewAndInfo.Divider = false;
            resources.ApplyResources(this.m_PreviewAndInfo, "m_PreviewAndInfo");
            style2.Color1 = System.Drawing.SystemColors.Control;
            style2.Color2 = System.Drawing.SystemColors.Control;
            this.m_PreviewAndInfo.GradientColors = style2;
            this.m_PreviewAndInfo.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_PreviewAndInfo.GrouperTitleStyle = null;
            this.m_PreviewAndInfo.LableVisble = true;
            this.m_PreviewAndInfo.Name = "m_PreviewAndInfo";
            // 
            // cameraSettings1
            // 
            resources.ApplyResources(this.cameraSettings1, "cameraSettings1");
            this.cameraSettings1.Name = "cameraSettings1";
            this.m_PanelJobList.SetRowSpan(this.cameraSettings1, 2);
            // 
            // maintenanceStatus1
            // 
            this.maintenanceStatus1.BackColor = System.Drawing.SystemColors.Window;
            this.maintenanceStatus1.Divider = false;
            resources.ApplyResources(this.maintenanceStatus1, "maintenanceStatus1");
            style3.Color1 = System.Drawing.SystemColors.Control;
            style3.Color2 = System.Drawing.SystemColors.Control;
            this.maintenanceStatus1.GradientColors = style3;
            this.maintenanceStatus1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.maintenanceStatus1.GrouperTitleStyle = null;
            this.maintenanceStatus1.Name = "maintenanceStatus1";
            // 
            // SubToolBarimageList
            // 
            this.SubToolBarimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SubToolBarimageList.ImageStream")));
            this.SubToolBarimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SubToolBarimageList.Images.SetKeyName(0, "");
            this.SubToolBarimageList.Images.SetKeyName(1, "");
            this.SubToolBarimageList.Images.SetKeyName(2, "");
            this.SubToolBarimageList.Images.SetKeyName(3, "");
            // 
            // MenuFolderPreview
            // 
            this.MenuFolderPreview.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAddToList,
            this.menuItem2,
            this.menuItemReLoad,
            this.menuItem4,
            this.menuItemSelectAll});
            this.MenuFolderPreview.Popup += new System.EventHandler(this.MenuFolderPreview_Popup);
            // 
            // menuItemAddToList
            // 
            this.menuItemAddToList.Index = 0;
            this.menuItemAddToList.OwnerDraw = true;
            resources.ApplyResources(this.menuItemAddToList, "menuItemAddToList");
            this.menuItemAddToList.Click += new System.EventHandler(this.menuItemAddToList_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.OwnerDraw = true;
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // menuItemReLoad
            // 
            this.menuItemReLoad.Index = 2;
            this.menuItemReLoad.OwnerDraw = true;
            resources.ApplyResources(this.menuItemReLoad, "menuItemReLoad");
            this.menuItemReLoad.Click += new System.EventHandler(this.menuItemReLoad_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.OwnerDraw = true;
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItemSelectAll
            // 
            this.menuItemSelectAll.Index = 4;
            this.menuItemSelectAll.OwnerDraw = true;
            resources.ApplyResources(this.menuItemSelectAll, "menuItemSelectAll");
            this.menuItemSelectAll.Click += new System.EventHandler(this.menuItemSelectAll_Click);
            // 
            // m_SplitterStatus
            // 
            this.m_SplitterStatus.BackColor = System.Drawing.SystemColors.Control;
            this.m_SplitterStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.m_SplitterStatus, "m_SplitterStatus");
            this.m_SplitterStatus.Name = "m_SplitterStatus";
            this.m_SplitterStatus.TabStop = false;
            // 
            // m_SplitterToolbar
            // 
            this.m_SplitterToolbar.BackColor = System.Drawing.SystemColors.Control;
            this.m_SplitterToolbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.m_SplitterToolbar, "m_SplitterToolbar");
            this.m_SplitterToolbar.Name = "m_SplitterToolbar";
            this.m_SplitterToolbar.TabStop = false;
            // 
            // m_StatusBarPanelJetStaus
            // 
            this.m_StatusBarPanelJetStaus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelJetStaus, "m_StatusBarPanelJetStaus");
            // 
            // m_StatusBarPanelError
            // 
            this.m_StatusBarPanelError.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelError, "m_StatusBarPanelError");
            // 
            // m_StatusBarPanelPercent
            // 
            this.m_StatusBarPanelPercent.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelPercent, "m_StatusBarPanelPercent");
            // 
            // m_StatusBarPanelComment
            // 
            this.m_StatusBarPanelComment.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            resources.ApplyResources(this.m_StatusBarPanelComment, "m_StatusBarPanelComment");
            // 
            // m_StatusBarApp
            // 
            resources.ApplyResources(this.m_StatusBarApp, "m_StatusBarApp");
            this.m_StatusBarApp.Name = "m_StatusBarApp";
            this.m_StatusBarApp.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.m_StatusBarPanelJetStaus,
            this.m_StatusBarPanelError,
            this.m_StatusBarPanelPercent,
            this.m_StatusBarPanelComment});
            this.m_StatusBarApp.ShowPanels = true;
            // 
            // UIIconImageList
            // 
            this.UIIconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UIIconImageList.ImageStream")));
            this.UIIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.UIIconImageList.Images.SetKeyName(0, "");
            // 
            // imageListMenu
            // 
            this.imageListMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMenu.ImageStream")));
            this.imageListMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMenu.Images.SetKeyName(0, "");
            this.imageListMenu.Images.SetKeyName(1, "");
            this.imageListMenu.Images.SetKeyName(2, "");
            this.imageListMenu.Images.SetKeyName(3, "");
            this.imageListMenu.Images.SetKeyName(4, "");
            this.imageListMenu.Images.SetKeyName(5, "");
            this.imageListMenu.Images.SetKeyName(6, "");
            this.imageListMenu.Images.SetKeyName(7, "");
            this.imageListMenu.Images.SetKeyName(8, "");
            this.imageListMenu.Images.SetKeyName(9, "");
            this.imageListMenu.Images.SetKeyName(10, "");
            this.imageListMenu.Images.SetKeyName(11, "");
            this.imageListMenu.Images.SetKeyName(12, "");
            this.imageListMenu.Images.SetKeyName(13, "");
            this.imageListMenu.Images.SetKeyName(14, "");
            this.imageListMenu.Images.SetKeyName(15, "");
            this.imageListMenu.Images.SetKeyName(16, "");
            // 
            // notifyIconBYHX
            // 
            resources.ApplyResources(this.notifyIconBYHX, "notifyIconBYHX");
            this.notifyIconBYHX.DoubleClick += new System.EventHandler(this.notifyIconBYHX_DoubleClick);
            // 
            // notifyIconImageList
            // 
            this.notifyIconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("notifyIconImageList.ImageStream")));
            this.notifyIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.notifyIconImageList.Images.SetKeyName(0, "");
            this.notifyIconImageList.Images.SetKeyName(1, "");
            this.notifyIconImageList.Images.SetKeyName(2, "");
            this.notifyIconImageList.Images.SetKeyName(3, "");
            this.notifyIconImageList.Images.SetKeyName(4, "");
            this.notifyIconImageList.Images.SetKeyName(5, "");
            this.notifyIconImageList.Images.SetKeyName(6, "");
            this.notifyIconImageList.Images.SetKeyName(7, "");
            this.notifyIconImageList.Images.SetKeyName(8, "");
            this.notifyIconImageList.Images.SetKeyName(9, "");
            // 
            // officeMenus1
            // 
            this.officeMenus1.ImageList = this.imageListMenu;
            // 
            // m_PrintInformation
            // 
            resources.ApplyResources(this.m_PrintInformation, "m_PrintInformation");
            this.m_PrintInformation.Name = "m_PrintInformation";
            this.m_PrintInformation.StartButtonClicked += new System.EventHandler(this.m_PrintInformation_StartButtonClicked);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.m_SplitterStatus);
            this.Controls.Add(this.m_PanelJobList);
            this.Controls.Add(this.m_PanelToolBarSetting);
            this.Controls.Add(this.m_SplitterToolbar);
            this.Controls.Add(this.m_StatusBarApp);
            this.Controls.Add(this.m_ToolBarCommand);
            this.Controls.Add(this.m_PrintInformation);
            this.KeyPreview = true;
            this.Menu = this.m_MainMenu;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainApp_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.m_PanelToolBarSetting.ResumeLayout(false);
            this.m_PanelJobList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolBarButton m_toolBarButtonRetractableCloth;
		 private CameraViewer cameraSettings1;
        private MaintenanceStatus maintenanceStatus1;
    }
}