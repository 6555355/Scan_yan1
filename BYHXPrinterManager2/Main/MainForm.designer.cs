using BYHXPrinterManager.Setting;
namespace BYHXPrinterManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageWorkForder = new System.Windows.Forms.TabPage();
            this.prtFileBrowser1 = new BYHXPrinterManager.Browser.ExplorerTree();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.m_PreviewAndInfo = new BYHXPrinterManager.JobListView.PrintingInfo();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.m_TabControlSetting = new System.Windows.Forms.TabControl();
            this.m_TabPagePrinterSetting = new System.Windows.Forms.TabPage();
            this.combinedSettings1 = new BYHXPrinterManager.Setting.CombinedSettings();
            this.m_TabPageService = new System.Windows.Forms.TabPage();
            this.bSeviceSettingApply = new System.Windows.Forms.Button();
            this.m_SeviceSetting = new BYHXPrinterManager.Setting.SeviceSetting();
            this.tabStrip1 = new BYHXPrinterManager.TabStrip();
            this.tabStripButtonPrinter = new BYHXPrinterManager.TabStripButton();
            this.tabStripButtonService = new BYHXPrinterManager.TabStripButton();
            this.tabStrip2 = new BYHXPrinterManager.TabStrip();
            this.tabStripButtonWorkForder = new BYHXPrinterManager.TabStripButton();
            this.tabStripButtonPreview = new BYHXPrinterManager.TabStripButton();
            this.tabStripButtonSetting = new BYHXPrinterManager.TabStripButton();
            this.m_JobListForm = new BYHXPrinterManager.JobListView.JobListForm();
            this.m_ToolBarCommand = new System.Windows.Forms.ToolStrip();
            this.m_ToolBarButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonPauseResume = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonAbort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolBarButtonCheckNozzle = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonAutoClean = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonSpray = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonStop = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonSingleClean = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolBarButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonRight = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonSetOrigin = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonForward = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonBackward = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonSetOriginY = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonDownZ = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonUpZ = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolBarButtonGoHome = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonGoHomeY = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonGoHomeZ = new System.Windows.Forms.ToolStripButton();
            this.m_ToolBarButtonMeasurePaper = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonSetting = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveToPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadFromPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFacWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPassWord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDongle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDongleKey = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFactryDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.TabimageList = new System.Windows.Forms.ImageList(this.components);
            this.m_MainMenu = new System.Windows.Forms.MenuStrip();
            this.m_MenuItemJob = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemSaveToPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemLoadFromPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemDemoPage = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemCalibraion = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemRealTime = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemUVSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemTopDown = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemLeftRight = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MenuItemFactoryDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_StatusBarPanelJetStaus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusBarPanelError = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusBarPanelPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_StatusBarPanelComment = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_BLACK = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_CYAN = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_MAGENTA = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_YELLOW = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_LIGHTMAGENTA = new System.Windows.Forms.ToolStripStatusLabel();
            this.LabelPUMP_LIGHTCYAN = new System.Windows.Forms.ToolStripStatusLabel();
            this.PumpInktimer = new System.Windows.Forms.Timer(this.components);
            this.Beeptimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageWorkForder.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.m_TabControlSetting.SuspendLayout();
            this.m_TabPagePrinterSetting.SuspendLayout();
            this.m_TabPageService.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.tabStrip2.SuspendLayout();
            this.m_ToolBarCommand.SuspendLayout();
            this.m_MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.AccessibleDescription = null;
            this.toolStripContainer1.AccessibleName = null;
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.AccessibleDescription = null;
            this.toolStripContainer1.BottomToolStripPanel.AccessibleName = null;
            this.toolStripContainer1.BottomToolStripPanel.BackgroundImage = null;
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            this.toolStripContainer1.BottomToolStripPanel.Font = null;
            this.toolTip1.SetToolTip(this.toolStripContainer1.BottomToolStripPanel, resources.GetString("toolStripContainer1.BottomToolStripPanel.ToolTip"));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AccessibleDescription = null;
            this.toolStripContainer1.ContentPanel.AccessibleName = null;
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.BackgroundImage = null;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Font = null;
            this.toolTip1.SetToolTip(this.toolStripContainer1.ContentPanel, resources.GetString("toolStripContainer1.ContentPanel.ToolTip"));
            this.toolStripContainer1.Font = null;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.AccessibleDescription = null;
            this.toolStripContainer1.LeftToolStripPanel.AccessibleName = null;
            this.toolStripContainer1.LeftToolStripPanel.BackgroundImage = null;
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.LeftToolStripPanel.Font = null;
            this.toolTip1.SetToolTip(this.toolStripContainer1.LeftToolStripPanel, resources.GetString("toolStripContainer1.LeftToolStripPanel.ToolTip"));
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.AccessibleDescription = null;
            this.toolStripContainer1.RightToolStripPanel.AccessibleName = null;
            this.toolStripContainer1.RightToolStripPanel.BackgroundImage = null;
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            this.toolStripContainer1.RightToolStripPanel.Font = null;
            this.toolTip1.SetToolTip(this.toolStripContainer1.RightToolStripPanel, resources.GetString("toolStripContainer1.RightToolStripPanel.ToolTip"));
            this.toolTip1.SetToolTip(this.toolStripContainer1, resources.GetString("toolStripContainer1.ToolTip"));
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.AccessibleDescription = null;
            this.toolStripContainer1.TopToolStripPanel.AccessibleName = null;
            this.toolStripContainer1.TopToolStripPanel.BackgroundImage = null;
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.m_ToolBarCommand);
            this.toolStripContainer1.TopToolStripPanel.Font = null;
            this.toolTip1.SetToolTip(this.toolStripContainer1.TopToolStripPanel, resources.GetString("toolStripContainer1.TopToolStripPanel.ToolTip"));
            // 
            // splitContainer1
            // 
            this.splitContainer1.AccessibleDescription = null;
            this.splitContainer1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackgroundImage = null;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Font = null;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleDescription = null;
            this.splitContainer1.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.BackgroundImage = null;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.tabStrip2);
            this.splitContainer1.Panel1.Font = null;
            this.toolTip1.SetToolTip(this.splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleDescription = null;
            this.splitContainer1.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.BackgroundImage = null;
            this.splitContainer1.Panel2.Controls.Add(this.m_JobListForm);
            this.splitContainer1.Panel2.Font = null;
            this.toolTip1.SetToolTip(this.splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            this.splitContainer1.TabStop = false;
            this.toolTip1.SetToolTip(this.splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleDescription = null;
            this.tabControl1.AccessibleName = null;
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.BackgroundImage = null;
            this.tabControl1.Controls.Add(this.tabPageWorkForder);
            this.tabControl1.Controls.Add(this.tabPagePreview);
            this.tabControl1.Controls.Add(this.tabPageSetting);
            this.tabControl1.Font = null;
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabStop = false;
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tabControl1_PreviewKeyDown);
            // 
            // tabPageWorkForder
            // 
            this.tabPageWorkForder.AccessibleDescription = null;
            this.tabPageWorkForder.AccessibleName = null;
            resources.ApplyResources(this.tabPageWorkForder, "tabPageWorkForder");
            this.tabPageWorkForder.BackColor = System.Drawing.Color.Transparent;
            this.tabPageWorkForder.BackgroundImage = null;
            this.tabPageWorkForder.Controls.Add(this.prtFileBrowser1);
            this.tabPageWorkForder.Font = null;
            this.tabPageWorkForder.Name = "tabPageWorkForder";
            this.toolTip1.SetToolTip(this.tabPageWorkForder, resources.GetString("tabPageWorkForder.ToolTip"));
            this.tabPageWorkForder.UseVisualStyleBackColor = true;
            // 
            // prtFileBrowser1
            // 
            this.prtFileBrowser1.AccessibleDescription = null;
            this.prtFileBrowser1.AccessibleName = null;
            this.prtFileBrowser1.AllowDrop = true;
            resources.ApplyResources(this.prtFileBrowser1, "prtFileBrowser1");
            this.prtFileBrowser1.BackColor = System.Drawing.Color.White;
            this.prtFileBrowser1.BackgroundImage = null;
            this.prtFileBrowser1.Font = null;
            this.prtFileBrowser1.Name = "prtFileBrowser1";
            this.prtFileBrowser1.SelectedPath = "C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE";
            this.prtFileBrowser1.ShowAddressbar = true;
            this.prtFileBrowser1.ShowMyDocuments = true;
            this.prtFileBrowser1.ShowMyFavorites = true;
            this.prtFileBrowser1.ShowMyNetwork = true;
            this.prtFileBrowser1.ShowToolbar = true;
            this.toolTip1.SetToolTip(this.prtFileBrowser1, resources.GetString("prtFileBrowser1.ToolTip"));
            this.prtFileBrowser1.UserContextMenuClick += new BYHXPrinterManager.Browser.ExplorerTree.UserContextMenuClickEventHandler(this.prtFileBrowser1_UserContextMenuClick);
            this.prtFileBrowser1.SelectedFilesChanged += new BYHXPrinterManager.Browser.ExplorerTree.SelectedFileChangedEventHandler(this.prtFileBrowser1_SelectedFilesChanged);
            this.prtFileBrowser1.PathChanged += new BYHXPrinterManager.Browser.ExplorerTree.PathChangedEventHandler(this.prtFileBrowser1_PathChanged);
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.AccessibleDescription = null;
            this.tabPagePreview.AccessibleName = null;
            resources.ApplyResources(this.tabPagePreview, "tabPagePreview");
            this.tabPagePreview.BackColor = System.Drawing.Color.Transparent;
            this.tabPagePreview.BackgroundImage = null;
            this.tabPagePreview.Controls.Add(this.m_PreviewAndInfo);
            this.tabPagePreview.Font = null;
            this.tabPagePreview.Name = "tabPagePreview";
            this.toolTip1.SetToolTip(this.tabPagePreview, resources.GetString("tabPagePreview.ToolTip"));
            this.tabPagePreview.UseVisualStyleBackColor = true;
            // 
            // m_PreviewAndInfo
            // 
            this.m_PreviewAndInfo.AccessibleDescription = null;
            this.m_PreviewAndInfo.AccessibleName = null;
            resources.ApplyResources(this.m_PreviewAndInfo, "m_PreviewAndInfo");
            this.m_PreviewAndInfo.BackColor = System.Drawing.SystemColors.Control;
            this.m_PreviewAndInfo.BackgroundImage = null;
            this.m_PreviewAndInfo.IsSpliterFixed = false;
            this.m_PreviewAndInfo.Name = "m_PreviewAndInfo";
            this.m_PreviewAndInfo.SpliterOrientathion = true;
            this.toolTip1.SetToolTip(this.m_PreviewAndInfo, resources.GetString("m_PreviewAndInfo.ToolTip"));
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.AccessibleDescription = null;
            this.tabPageSetting.AccessibleName = null;
            resources.ApplyResources(this.tabPageSetting, "tabPageSetting");
            this.tabPageSetting.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSetting.BackgroundImage = null;
            this.tabPageSetting.Controls.Add(this.m_TabControlSetting);
            this.tabPageSetting.Controls.Add(this.tabStrip1);
            this.tabPageSetting.Font = null;
            this.tabPageSetting.Name = "tabPageSetting";
            this.toolTip1.SetToolTip(this.tabPageSetting, resources.GetString("tabPageSetting.ToolTip"));
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // m_TabControlSetting
            // 
            this.m_TabControlSetting.AccessibleDescription = null;
            this.m_TabControlSetting.AccessibleName = null;
            resources.ApplyResources(this.m_TabControlSetting, "m_TabControlSetting");
            this.m_TabControlSetting.BackgroundImage = null;
            this.m_TabControlSetting.Controls.Add(this.m_TabPagePrinterSetting);
            this.m_TabControlSetting.Controls.Add(this.m_TabPageService);
            this.m_TabControlSetting.Font = null;
            this.m_TabControlSetting.Multiline = true;
            this.m_TabControlSetting.Name = "m_TabControlSetting";
            this.m_TabControlSetting.SelectedIndex = 0;
            this.m_TabControlSetting.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.m_TabControlSetting.TabStop = false;
            this.toolTip1.SetToolTip(this.m_TabControlSetting, resources.GetString("m_TabControlSetting.ToolTip"));
            // 
            // m_TabPagePrinterSetting
            // 
            this.m_TabPagePrinterSetting.AccessibleDescription = null;
            this.m_TabPagePrinterSetting.AccessibleName = null;
            resources.ApplyResources(this.m_TabPagePrinterSetting, "m_TabPagePrinterSetting");
            this.m_TabPagePrinterSetting.BackColor = System.Drawing.SystemColors.Control;
            this.m_TabPagePrinterSetting.BackgroundImage = null;
            this.m_TabPagePrinterSetting.Controls.Add(this.combinedSettings1);
            this.m_TabPagePrinterSetting.Font = null;
            this.m_TabPagePrinterSetting.Name = "m_TabPagePrinterSetting";
            this.toolTip1.SetToolTip(this.m_TabPagePrinterSetting, resources.GetString("m_TabPagePrinterSetting.ToolTip"));
            this.m_TabPagePrinterSetting.UseVisualStyleBackColor = true;
            // 
            // combinedSettings1
            // 
            this.combinedSettings1.AccessibleDescription = null;
            this.combinedSettings1.AccessibleName = null;
            resources.ApplyResources(this.combinedSettings1, "combinedSettings1");
            this.combinedSettings1.BackColor = System.Drawing.SystemColors.Control;
            this.combinedSettings1.BackgroundImage = null;
            this.combinedSettings1.GrouperTitleStyle = null;
            this.combinedSettings1.IsDirty = false;
            this.combinedSettings1.IsT_VDirty = true;
            this.combinedSettings1.Name = "combinedSettings1";
            this.toolTip1.SetToolTip(this.combinedSettings1, resources.GetString("combinedSettings1.ToolTip"));
            this.combinedSettings1.ApplyButtonClicked += new System.EventHandler(this.buttonBaseSettingApply_Click);
            // 
            // m_TabPageService
            // 
            this.m_TabPageService.AccessibleDescription = null;
            this.m_TabPageService.AccessibleName = null;
            resources.ApplyResources(this.m_TabPageService, "m_TabPageService");
            this.m_TabPageService.BackColor = System.Drawing.SystemColors.Control;
            this.m_TabPageService.BackgroundImage = null;
            this.m_TabPageService.Controls.Add(this.bSeviceSettingApply);
            this.m_TabPageService.Controls.Add(this.m_SeviceSetting);
            this.m_TabPageService.Font = null;
            this.m_TabPageService.Name = "m_TabPageService";
            this.toolTip1.SetToolTip(this.m_TabPageService, resources.GetString("m_TabPageService.ToolTip"));
            this.m_TabPageService.UseVisualStyleBackColor = true;
            // 
            // bSeviceSettingApply
            // 
            this.bSeviceSettingApply.AccessibleDescription = null;
            this.bSeviceSettingApply.AccessibleName = null;
            resources.ApplyResources(this.bSeviceSettingApply, "bSeviceSettingApply");
            this.bSeviceSettingApply.BackgroundImage = null;
            this.bSeviceSettingApply.Font = null;
            this.bSeviceSettingApply.Name = "bSeviceSettingApply";
            this.toolTip1.SetToolTip(this.bSeviceSettingApply, resources.GetString("bSeviceSettingApply.ToolTip"));
            this.bSeviceSettingApply.UseVisualStyleBackColor = true;
            this.bSeviceSettingApply.Click += new System.EventHandler(this.bSeviceSettingApply_Click);
            // 
            // m_SeviceSetting
            // 
            this.m_SeviceSetting.AccessibleDescription = null;
            this.m_SeviceSetting.AccessibleName = null;
            resources.ApplyResources(this.m_SeviceSetting, "m_SeviceSetting");
            this.m_SeviceSetting.BackColor = System.Drawing.SystemColors.Control;
            this.m_SeviceSetting.BackgroundImage = null;
            this.m_SeviceSetting.GrouperTitleStyle = null;
            this.m_SeviceSetting.IsDirty = false;
            this.m_SeviceSetting.Name = "m_SeviceSetting";
            this.toolTip1.SetToolTip(this.m_SeviceSetting, resources.GetString("m_SeviceSetting.ToolTip"));
            // 
            // tabStrip1
            // 
            this.tabStrip1.AccessibleDescription = null;
            this.tabStrip1.AccessibleName = null;
            resources.ApplyResources(this.tabStrip1, "tabStrip1");
            this.tabStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.tabStrip1.BackgroundImage = null;
            this.tabStrip1.FlipButtons = false;
            this.tabStrip1.Font = null;
            this.tabStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabStripButtonPrinter,
            this.tabStripButtonService});
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabStrip1.SelectedTab = this.tabStripButtonPrinter;
            this.tabStrip1.Stretch = true;
            this.toolTip1.SetToolTip(this.tabStrip1, resources.GetString("tabStrip1.ToolTip"));
            this.tabStrip1.UseVisualStyles = false;
            this.tabStrip1.SelectedTabChanged += new System.EventHandler<BYHXPrinterManager.SelectedTabChangedEventArgs>(this.tabStrip1_SelectedTabChanged);
            // 
            // tabStripButtonPrinter
            // 
            this.tabStripButtonPrinter.AccessibleDescription = null;
            this.tabStripButtonPrinter.AccessibleName = null;
            resources.ApplyResources(this.tabStripButtonPrinter, "tabStripButtonPrinter");
            this.tabStripButtonPrinter.BackgroundImage = null;
            this.tabStripButtonPrinter.Checked = true;
            this.tabStripButtonPrinter.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabStripButtonPrinter.IsSelected = true;
            this.tabStripButtonPrinter.Margin = new System.Windows.Forms.Padding(0);
            this.tabStripButtonPrinter.Name = "tabStripButtonPrinter";
            this.tabStripButtonPrinter.Padding = new System.Windows.Forms.Padding(0);
            this.tabStripButtonPrinter.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStripButtonPrinter.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabStripButtonService
            // 
            this.tabStripButtonService.AccessibleDescription = null;
            this.tabStripButtonService.AccessibleName = null;
            resources.ApplyResources(this.tabStripButtonService, "tabStripButtonService");
            this.tabStripButtonService.BackgroundImage = null;
            this.tabStripButtonService.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabStripButtonService.IsSelected = false;
            this.tabStripButtonService.Margin = new System.Windows.Forms.Padding(0);
            this.tabStripButtonService.Name = "tabStripButtonService";
            this.tabStripButtonService.Padding = new System.Windows.Forms.Padding(0);
            this.tabStripButtonService.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStripButtonService.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabStrip2
            // 
            this.tabStrip2.AccessibleDescription = null;
            this.tabStrip2.AccessibleName = null;
            resources.ApplyResources(this.tabStrip2, "tabStrip2");
            this.tabStrip2.BackgroundImage = null;
            this.tabStrip2.FlipButtons = true;
            this.tabStrip2.Font = null;
            this.tabStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabStripButtonWorkForder,
            this.tabStripButtonPreview,
            this.tabStripButtonSetting});
            this.tabStrip2.Name = "tabStrip2";
            this.tabStrip2.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabStrip2.SelectedTab = this.tabStripButtonSetting;
            this.tabStrip2.Stretch = true;
            this.toolTip1.SetToolTip(this.tabStrip2, resources.GetString("tabStrip2.ToolTip"));
            this.tabStrip2.UseVisualStyles = false;
            this.tabStrip2.SelectedTabChanged += new System.EventHandler<BYHXPrinterManager.SelectedTabChangedEventArgs>(this.tabStrip2_SelectedTabChanged);
            // 
            // tabStripButtonWorkForder
            // 
            this.tabStripButtonWorkForder.AccessibleDescription = null;
            this.tabStripButtonWorkForder.AccessibleName = null;
            resources.ApplyResources(this.tabStripButtonWorkForder, "tabStripButtonWorkForder");
            this.tabStripButtonWorkForder.BackgroundImage = null;
            this.tabStripButtonWorkForder.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabStripButtonWorkForder.IsSelected = false;
            this.tabStripButtonWorkForder.Margin = new System.Windows.Forms.Padding(0);
            this.tabStripButtonWorkForder.Name = "tabStripButtonWorkForder";
            this.tabStripButtonWorkForder.Padding = new System.Windows.Forms.Padding(0);
            this.tabStripButtonWorkForder.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStripButtonWorkForder.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabStripButtonPreview
            // 
            this.tabStripButtonPreview.AccessibleDescription = null;
            this.tabStripButtonPreview.AccessibleName = null;
            resources.ApplyResources(this.tabStripButtonPreview, "tabStripButtonPreview");
            this.tabStripButtonPreview.BackgroundImage = null;
            this.tabStripButtonPreview.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabStripButtonPreview.IsSelected = false;
            this.tabStripButtonPreview.Margin = new System.Windows.Forms.Padding(0);
            this.tabStripButtonPreview.Name = "tabStripButtonPreview";
            this.tabStripButtonPreview.Padding = new System.Windows.Forms.Padding(0);
            this.tabStripButtonPreview.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStripButtonPreview.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabStripButtonSetting
            // 
            this.tabStripButtonSetting.AccessibleDescription = null;
            this.tabStripButtonSetting.AccessibleName = null;
            resources.ApplyResources(this.tabStripButtonSetting, "tabStripButtonSetting");
            this.tabStripButtonSetting.BackgroundImage = null;
            this.tabStripButtonSetting.Checked = true;
            this.tabStripButtonSetting.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabStripButtonSetting.IsSelected = true;
            this.tabStripButtonSetting.Margin = new System.Windows.Forms.Padding(0);
            this.tabStripButtonSetting.Name = "tabStripButtonSetting";
            this.tabStripButtonSetting.Padding = new System.Windows.Forms.Padding(0);
            this.tabStripButtonSetting.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStripButtonSetting.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            // 
            // m_JobListForm
            // 
            this.m_JobListForm.AccessibleDescription = null;
            this.m_JobListForm.AccessibleName = null;
            this.m_JobListForm.AllowDrop = true;
            resources.ApplyResources(this.m_JobListForm, "m_JobListForm");
            this.m_JobListForm.BackgroundImage = null;
            this.m_JobListForm.mAlignment = System.Windows.Forms.ListViewAlignment.Left;
            this.m_JobListForm.Name = "m_JobListForm";
            this.m_JobListForm.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.m_JobListForm.ThumbNailSize = new System.Drawing.Size(95, 95);
            this.toolTip1.SetToolTip(this.m_JobListForm, resources.GetString("m_JobListForm.ToolTip"));
            this.m_JobListForm.SelectedIndexChanged += new System.EventHandler(this.m_JobListForm_SelectedIndexChanged);
            // 
            // m_ToolBarCommand
            // 
            this.m_ToolBarCommand.AccessibleDescription = null;
            this.m_ToolBarCommand.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarCommand, "m_ToolBarCommand");
            this.m_ToolBarCommand.BackColor = System.Drawing.SystemColors.Control;
            this.m_ToolBarCommand.Font = null;
            this.m_ToolBarCommand.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_ToolBarCommand.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.m_ToolBarCommand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolBarButtonAdd,
            this.m_ToolBarButtonDelete,
            this.m_ToolBarButtonPrint,
            this.m_ToolBarButtonPauseResume,
            this.m_ToolBarButtonAbort,
            this.toolStripSeparator1,
            this.m_ToolBarButtonCheckNozzle,
            this.m_ToolBarButtonAutoClean,
            this.m_ToolBarButtonSpray,
            this.m_ToolBarButtonStop,
            this.m_ToolBarButtonSingleClean,
            this.toolStripSeparator2,
            this.m_ToolBarButtonLeft,
            this.m_ToolBarButtonRight,
            this.m_ToolBarButtonSetOrigin,
            this.m_ToolBarButtonForward,
            this.m_ToolBarButtonBackward,
            this.m_ToolBarButtonSetOriginY,
            this.m_ToolBarButtonDownZ,
            this.m_ToolBarButtonUpZ,
            this.toolStripSeparator3,
            this.m_ToolBarButtonGoHome,
            this.m_ToolBarButtonGoHomeY,
            this.m_ToolBarButtonGoHomeZ,
            this.m_ToolBarButtonMeasurePaper,
            this.toolStripDropDownButtonSetting});
            this.m_ToolBarCommand.Name = "m_ToolBarCommand";
            this.m_ToolBarCommand.Stretch = true;
            this.toolTip1.SetToolTip(this.m_ToolBarCommand, resources.GetString("m_ToolBarCommand.ToolTip"));
            this.m_ToolBarCommand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarCommand.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.m_ToolBarCommand_ItemClicked);
            // 
            // m_ToolBarButtonAdd
            // 
            this.m_ToolBarButtonAdd.AccessibleDescription = null;
            this.m_ToolBarButtonAdd.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonAdd, "m_ToolBarButtonAdd");
            this.m_ToolBarButtonAdd.BackgroundImage = null;
            this.m_ToolBarButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonAdd.Name = "m_ToolBarButtonAdd";
            // 
            // m_ToolBarButtonDelete
            // 
            this.m_ToolBarButtonDelete.AccessibleDescription = null;
            this.m_ToolBarButtonDelete.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonDelete, "m_ToolBarButtonDelete");
            this.m_ToolBarButtonDelete.BackgroundImage = null;
            this.m_ToolBarButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonDelete.Name = "m_ToolBarButtonDelete";
            // 
            // m_ToolBarButtonPrint
            // 
            this.m_ToolBarButtonPrint.AccessibleDescription = null;
            this.m_ToolBarButtonPrint.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonPrint, "m_ToolBarButtonPrint");
            this.m_ToolBarButtonPrint.BackgroundImage = null;
            this.m_ToolBarButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonPrint.Name = "m_ToolBarButtonPrint";
            // 
            // m_ToolBarButtonPauseResume
            // 
            this.m_ToolBarButtonPauseResume.AccessibleDescription = null;
            this.m_ToolBarButtonPauseResume.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonPauseResume, "m_ToolBarButtonPauseResume");
            this.m_ToolBarButtonPauseResume.BackgroundImage = null;
            this.m_ToolBarButtonPauseResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonPauseResume.Name = "m_ToolBarButtonPauseResume";
            // 
            // m_ToolBarButtonAbort
            // 
            this.m_ToolBarButtonAbort.AccessibleDescription = null;
            this.m_ToolBarButtonAbort.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonAbort, "m_ToolBarButtonAbort");
            this.m_ToolBarButtonAbort.BackgroundImage = null;
            this.m_ToolBarButtonAbort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonAbort.Name = "m_ToolBarButtonAbort";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AccessibleDescription = null;
            this.toolStripSeparator1.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // m_ToolBarButtonCheckNozzle
            // 
            this.m_ToolBarButtonCheckNozzle.AccessibleDescription = null;
            this.m_ToolBarButtonCheckNozzle.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonCheckNozzle, "m_ToolBarButtonCheckNozzle");
            this.m_ToolBarButtonCheckNozzle.BackgroundImage = null;
            this.m_ToolBarButtonCheckNozzle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonCheckNozzle.Name = "m_ToolBarButtonCheckNozzle";
            // 
            // m_ToolBarButtonAutoClean
            // 
            this.m_ToolBarButtonAutoClean.AccessibleDescription = null;
            this.m_ToolBarButtonAutoClean.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonAutoClean, "m_ToolBarButtonAutoClean");
            this.m_ToolBarButtonAutoClean.BackgroundImage = null;
            this.m_ToolBarButtonAutoClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonAutoClean.Name = "m_ToolBarButtonAutoClean";
            // 
            // m_ToolBarButtonSpray
            // 
            this.m_ToolBarButtonSpray.AccessibleDescription = null;
            this.m_ToolBarButtonSpray.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonSpray, "m_ToolBarButtonSpray");
            this.m_ToolBarButtonSpray.BackgroundImage = null;
            this.m_ToolBarButtonSpray.CheckOnClick = true;
            this.m_ToolBarButtonSpray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonSpray.Name = "m_ToolBarButtonSpray";
            // 
            // m_ToolBarButtonStop
            // 
            this.m_ToolBarButtonStop.AccessibleDescription = null;
            this.m_ToolBarButtonStop.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonStop, "m_ToolBarButtonStop");
            this.m_ToolBarButtonStop.BackgroundImage = null;
            this.m_ToolBarButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonStop.Name = "m_ToolBarButtonStop";
            // 
            // m_ToolBarButtonSingleClean
            // 
            this.m_ToolBarButtonSingleClean.AccessibleDescription = null;
            this.m_ToolBarButtonSingleClean.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonSingleClean, "m_ToolBarButtonSingleClean");
            this.m_ToolBarButtonSingleClean.BackgroundImage = null;
            this.m_ToolBarButtonSingleClean.CheckOnClick = true;
            this.m_ToolBarButtonSingleClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonSingleClean.Name = "m_ToolBarButtonSingleClean";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AccessibleDescription = null;
            this.toolStripSeparator2.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // m_ToolBarButtonLeft
            // 
            this.m_ToolBarButtonLeft.AccessibleDescription = null;
            this.m_ToolBarButtonLeft.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonLeft, "m_ToolBarButtonLeft");
            this.m_ToolBarButtonLeft.BackgroundImage = null;
            this.m_ToolBarButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonLeft.Name = "m_ToolBarButtonLeft";
            this.m_ToolBarButtonLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonLeft_MouseDown);
            // 
            // m_ToolBarButtonRight
            // 
            this.m_ToolBarButtonRight.AccessibleDescription = null;
            this.m_ToolBarButtonRight.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonRight, "m_ToolBarButtonRight");
            this.m_ToolBarButtonRight.BackgroundImage = null;
            this.m_ToolBarButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonRight.Name = "m_ToolBarButtonRight";
            this.m_ToolBarButtonRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonRight_MouseDown);
            // 
            // m_ToolBarButtonSetOrigin
            // 
            this.m_ToolBarButtonSetOrigin.AccessibleDescription = null;
            this.m_ToolBarButtonSetOrigin.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonSetOrigin, "m_ToolBarButtonSetOrigin");
            this.m_ToolBarButtonSetOrigin.BackgroundImage = null;
            this.m_ToolBarButtonSetOrigin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonSetOrigin.Name = "m_ToolBarButtonSetOrigin";
            // 
            // m_ToolBarButtonForward
            // 
            this.m_ToolBarButtonForward.AccessibleDescription = null;
            this.m_ToolBarButtonForward.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonForward, "m_ToolBarButtonForward");
            this.m_ToolBarButtonForward.BackgroundImage = null;
            this.m_ToolBarButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonForward.Name = "m_ToolBarButtonForward";
            this.m_ToolBarButtonForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonForward_MouseDown);
            // 
            // m_ToolBarButtonBackward
            // 
            this.m_ToolBarButtonBackward.AccessibleDescription = null;
            this.m_ToolBarButtonBackward.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonBackward, "m_ToolBarButtonBackward");
            this.m_ToolBarButtonBackward.BackgroundImage = null;
            this.m_ToolBarButtonBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonBackward.Name = "m_ToolBarButtonBackward";
            this.m_ToolBarButtonBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonBackward_MouseDown);
            // 
            // m_ToolBarButtonSetOriginY
            // 
            this.m_ToolBarButtonSetOriginY.AccessibleDescription = null;
            this.m_ToolBarButtonSetOriginY.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonSetOriginY, "m_ToolBarButtonSetOriginY");
            this.m_ToolBarButtonSetOriginY.BackgroundImage = null;
            this.m_ToolBarButtonSetOriginY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonSetOriginY.Name = "m_ToolBarButtonSetOriginY";
            // 
            // m_ToolBarButtonDownZ
            // 
            this.m_ToolBarButtonDownZ.AccessibleDescription = null;
            this.m_ToolBarButtonDownZ.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonDownZ, "m_ToolBarButtonDownZ");
            this.m_ToolBarButtonDownZ.BackgroundImage = null;
            this.m_ToolBarButtonDownZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonDownZ.Name = "m_ToolBarButtonDownZ";
            this.m_ToolBarButtonDownZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonDownZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonDownZ_MouseDown);
            // 
            // m_ToolBarButtonUpZ
            // 
            this.m_ToolBarButtonUpZ.AccessibleDescription = null;
            this.m_ToolBarButtonUpZ.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonUpZ, "m_ToolBarButtonUpZ");
            this.m_ToolBarButtonUpZ.BackgroundImage = null;
            this.m_ToolBarButtonUpZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonUpZ.Name = "m_ToolBarButtonUpZ";
            this.m_ToolBarButtonUpZ.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarCommand_MouseUp);
            this.m_ToolBarButtonUpZ.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_ToolBarButtonUpZ_MouseDown);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AccessibleDescription = null;
            this.toolStripSeparator3.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // m_ToolBarButtonGoHome
            // 
            this.m_ToolBarButtonGoHome.AccessibleDescription = null;
            this.m_ToolBarButtonGoHome.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonGoHome, "m_ToolBarButtonGoHome");
            this.m_ToolBarButtonGoHome.BackgroundImage = null;
            this.m_ToolBarButtonGoHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonGoHome.Name = "m_ToolBarButtonGoHome";
            // 
            // m_ToolBarButtonGoHomeY
            // 
            this.m_ToolBarButtonGoHomeY.AccessibleDescription = null;
            this.m_ToolBarButtonGoHomeY.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonGoHomeY, "m_ToolBarButtonGoHomeY");
            this.m_ToolBarButtonGoHomeY.BackgroundImage = null;
            this.m_ToolBarButtonGoHomeY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonGoHomeY.Name = "m_ToolBarButtonGoHomeY";
            // 
            // m_ToolBarButtonGoHomeZ
            // 
            this.m_ToolBarButtonGoHomeZ.AccessibleDescription = null;
            this.m_ToolBarButtonGoHomeZ.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonGoHomeZ, "m_ToolBarButtonGoHomeZ");
            this.m_ToolBarButtonGoHomeZ.BackgroundImage = null;
            this.m_ToolBarButtonGoHomeZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonGoHomeZ.Name = "m_ToolBarButtonGoHomeZ";
            // 
            // m_ToolBarButtonMeasurePaper
            // 
            this.m_ToolBarButtonMeasurePaper.AccessibleDescription = null;
            this.m_ToolBarButtonMeasurePaper.AccessibleName = null;
            resources.ApplyResources(this.m_ToolBarButtonMeasurePaper, "m_ToolBarButtonMeasurePaper");
            this.m_ToolBarButtonMeasurePaper.BackgroundImage = null;
            this.m_ToolBarButtonMeasurePaper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolBarButtonMeasurePaper.Name = "m_ToolBarButtonMeasurePaper";
            // 
            // toolStripDropDownButtonSetting
            // 
            this.toolStripDropDownButtonSetting.AccessibleDescription = null;
            this.toolStripDropDownButtonSetting.AccessibleName = null;
            this.toolStripDropDownButtonSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripDropDownButtonSetting, "toolStripDropDownButtonSetting");
            this.toolStripDropDownButtonSetting.BackgroundImage = null;
            this.toolStripDropDownButtonSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSave,
            this.toolStripMenuItemLoad,
            this.toolStripMenuItemSaveToPrinter,
            this.toolStripMenuItemLoadFromPrinter,
            this.toolStripSeparator5,
            this.toolStripMenuItemCalibration,
            this.toolStripMenuItemFacWrite,
            this.toolStripSeparator4,
            this.toolStripMenuItemUpdate,
            this.toolStripMenuItemPassWord,
            this.menuItemDongle,
            this.toolStripSeparator6,
            this.toolStripMenuItemFactryDebug,
            this.toolStripMenuItemAbout});
            this.toolStripDropDownButtonSetting.Name = "toolStripDropDownButtonSetting";
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.AccessibleDescription = null;
            this.toolStripMenuItemSave.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemSave, "toolStripMenuItemSave");
            this.toolStripMenuItemSave.BackgroundImage = null;
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemSave.Click += new System.EventHandler(this.m_MenuItemSave_Click);
            // 
            // toolStripMenuItemLoad
            // 
            this.toolStripMenuItemLoad.AccessibleDescription = null;
            this.toolStripMenuItemLoad.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemLoad, "toolStripMenuItemLoad");
            this.toolStripMenuItemLoad.BackgroundImage = null;
            this.toolStripMenuItemLoad.Name = "toolStripMenuItemLoad";
            this.toolStripMenuItemLoad.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemLoad.Click += new System.EventHandler(this.m_MenuItemLoad_Click);
            // 
            // toolStripMenuItemSaveToPrinter
            // 
            this.toolStripMenuItemSaveToPrinter.AccessibleDescription = null;
            this.toolStripMenuItemSaveToPrinter.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemSaveToPrinter, "toolStripMenuItemSaveToPrinter");
            this.toolStripMenuItemSaveToPrinter.BackgroundImage = null;
            this.toolStripMenuItemSaveToPrinter.Name = "toolStripMenuItemSaveToPrinter";
            this.toolStripMenuItemSaveToPrinter.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemSaveToPrinter.Click += new System.EventHandler(this.m_MenuItemSaveToPrinter_Click);
            // 
            // toolStripMenuItemLoadFromPrinter
            // 
            this.toolStripMenuItemLoadFromPrinter.AccessibleDescription = null;
            this.toolStripMenuItemLoadFromPrinter.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemLoadFromPrinter, "toolStripMenuItemLoadFromPrinter");
            this.toolStripMenuItemLoadFromPrinter.BackgroundImage = null;
            this.toolStripMenuItemLoadFromPrinter.Name = "toolStripMenuItemLoadFromPrinter";
            this.toolStripMenuItemLoadFromPrinter.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemLoadFromPrinter.Click += new System.EventHandler(this.m_MenuItemLoadFromPrinter_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AccessibleDescription = null;
            this.toolStripSeparator5.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // toolStripMenuItemCalibration
            // 
            this.toolStripMenuItemCalibration.AccessibleDescription = null;
            this.toolStripMenuItemCalibration.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemCalibration, "toolStripMenuItemCalibration");
            this.toolStripMenuItemCalibration.BackgroundImage = null;
            this.toolStripMenuItemCalibration.Name = "toolStripMenuItemCalibration";
            this.toolStripMenuItemCalibration.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemCalibration.Click += new System.EventHandler(this.m_MenuItemCalibraion_Click);
            // 
            // toolStripMenuItemFacWrite
            // 
            this.toolStripMenuItemFacWrite.AccessibleDescription = null;
            this.toolStripMenuItemFacWrite.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemFacWrite, "toolStripMenuItemFacWrite");
            this.toolStripMenuItemFacWrite.BackgroundImage = null;
            this.toolStripMenuItemFacWrite.Name = "toolStripMenuItemFacWrite";
            this.toolStripMenuItemFacWrite.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemFacWrite.Click += new System.EventHandler(this.toolStripMenuItemFacWrite_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AccessibleDescription = null;
            this.toolStripSeparator4.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // toolStripMenuItemUpdate
            // 
            this.toolStripMenuItemUpdate.AccessibleDescription = null;
            this.toolStripMenuItemUpdate.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemUpdate, "toolStripMenuItemUpdate");
            this.toolStripMenuItemUpdate.BackgroundImage = null;
            this.toolStripMenuItemUpdate.Name = "toolStripMenuItemUpdate";
            this.toolStripMenuItemUpdate.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemUpdate.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
            // 
            // toolStripMenuItemPassWord
            // 
            this.toolStripMenuItemPassWord.AccessibleDescription = null;
            this.toolStripMenuItemPassWord.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemPassWord, "toolStripMenuItemPassWord");
            this.toolStripMenuItemPassWord.BackgroundImage = null;
            this.toolStripMenuItemPassWord.Name = "toolStripMenuItemPassWord";
            this.toolStripMenuItemPassWord.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemPassWord.Click += new System.EventHandler(this.m_MenuItemPassword_Click);
            // 
            // menuItemDongle
            // 
            this.menuItemDongle.AccessibleDescription = null;
            this.menuItemDongle.AccessibleName = null;
            resources.ApplyResources(this.menuItemDongle, "menuItemDongle");
            this.menuItemDongle.BackgroundImage = null;
            this.menuItemDongle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDongleKey});
            this.menuItemDongle.Name = "menuItemDongle";
            this.menuItemDongle.ShortcutKeyDisplayString = null;
            // 
            // menuItemDongleKey
            // 
            this.menuItemDongleKey.AccessibleDescription = null;
            this.menuItemDongleKey.AccessibleName = null;
            resources.ApplyResources(this.menuItemDongleKey, "menuItemDongleKey");
            this.menuItemDongleKey.BackgroundImage = null;
            this.menuItemDongleKey.Name = "menuItemDongleKey";
            this.menuItemDongleKey.ShortcutKeyDisplayString = null;
            this.menuItemDongleKey.Click += new System.EventHandler(this.menuItemDongleKey_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AccessibleDescription = null;
            this.toolStripSeparator6.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // toolStripMenuItemFactryDebug
            // 
            this.toolStripMenuItemFactryDebug.AccessibleDescription = null;
            this.toolStripMenuItemFactryDebug.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemFactryDebug, "toolStripMenuItemFactryDebug");
            this.toolStripMenuItemFactryDebug.BackgroundImage = null;
            this.toolStripMenuItemFactryDebug.Name = "toolStripMenuItemFactryDebug";
            this.toolStripMenuItemFactryDebug.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemFactryDebug.Click += new System.EventHandler(this.m_MenuItemFactoryDebug_Click);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.AccessibleDescription = null;
            this.toolStripMenuItemAbout.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItemAbout, "toolStripMenuItemAbout");
            this.toolStripMenuItemAbout.BackgroundImage = null;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.ShortcutKeyDisplayString = null;
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.m_MenuItemAbout_Click);
            // 
            // TabimageList
            // 
            this.TabimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabimageList.ImageStream")));
            this.TabimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TabimageList.Images.SetKeyName(0, "Images Folder.png");
            this.TabimageList.Images.SetKeyName(1, "Gear.ico");
            this.TabimageList.Images.SetKeyName(2, "about.ico");
            //this.TabimageList.Images.SetKeyName(3, "");
            // 
            // m_MainMenu
            // 
            this.m_MainMenu.AccessibleDescription = null;
            this.m_MainMenu.AccessibleName = null;
            resources.ApplyResources(this.m_MainMenu, "m_MainMenu");
            this.m_MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.m_MainMenu.BackgroundImage = null;
            this.m_MainMenu.Font = null;
            this.m_MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemJob,
            this.m_MenuItemSetting,
            this.m_MenuItemTools,
            this.m_MenuItemView,
            this.m_MenuItemHelp,
            this.m_MenuItemDebug});
            this.m_MainMenu.Name = "m_MainMenu";
            this.toolTip1.SetToolTip(this.m_MainMenu, resources.GetString("m_MainMenu.ToolTip"));
            // 
            // m_MenuItemJob
            // 
            this.m_MenuItemJob.AccessibleDescription = null;
            this.m_MenuItemJob.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemJob, "m_MenuItemJob");
            this.m_MenuItemJob.BackgroundImage = null;
            this.m_MenuItemJob.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemAdd,
            this.m_MenuItemDelete,
            this.m_MenuItemPrint,
            this.m_MenuItemExit});
            this.m_MenuItemJob.Name = "m_MenuItemJob";
            this.m_MenuItemJob.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemAdd
            // 
            this.m_MenuItemAdd.AccessibleDescription = null;
            this.m_MenuItemAdd.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemAdd, "m_MenuItemAdd");
            this.m_MenuItemAdd.BackgroundImage = null;
            this.m_MenuItemAdd.Name = "m_MenuItemAdd";
            this.m_MenuItemAdd.ShortcutKeyDisplayString = null;
            this.m_MenuItemAdd.Click += new System.EventHandler(this.m_MenuItemAdd_Click);
            // 
            // m_MenuItemDelete
            // 
            this.m_MenuItemDelete.AccessibleDescription = null;
            this.m_MenuItemDelete.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemDelete, "m_MenuItemDelete");
            this.m_MenuItemDelete.BackgroundImage = null;
            this.m_MenuItemDelete.Name = "m_MenuItemDelete";
            this.m_MenuItemDelete.ShortcutKeyDisplayString = null;
            this.m_MenuItemDelete.Click += new System.EventHandler(this.m_MenuItemDelete_Click);
            // 
            // m_MenuItemPrint
            // 
            this.m_MenuItemPrint.AccessibleDescription = null;
            this.m_MenuItemPrint.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemPrint, "m_MenuItemPrint");
            this.m_MenuItemPrint.BackgroundImage = null;
            this.m_MenuItemPrint.Name = "m_MenuItemPrint";
            this.m_MenuItemPrint.ShortcutKeyDisplayString = null;
            this.m_MenuItemPrint.Click += new System.EventHandler(this.m_MenuItemPrint_Click);
            // 
            // m_MenuItemExit
            // 
            this.m_MenuItemExit.AccessibleDescription = null;
            this.m_MenuItemExit.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemExit, "m_MenuItemExit");
            this.m_MenuItemExit.BackgroundImage = null;
            this.m_MenuItemExit.Name = "m_MenuItemExit";
            this.m_MenuItemExit.ShortcutKeyDisplayString = null;
            this.m_MenuItemExit.Click += new System.EventHandler(this.m_MenuItemExit_Click);
            // 
            // m_MenuItemSetting
            // 
            this.m_MenuItemSetting.AccessibleDescription = null;
            this.m_MenuItemSetting.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemSetting, "m_MenuItemSetting");
            this.m_MenuItemSetting.BackgroundImage = null;
            this.m_MenuItemSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemSave,
            this.m_MenuItemLoad,
            this.m_MenuItemSaveToPrinter,
            this.m_MenuItemLoadFromPrinter});
            this.m_MenuItemSetting.Name = "m_MenuItemSetting";
            this.m_MenuItemSetting.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemSave
            // 
            this.m_MenuItemSave.AccessibleDescription = null;
            this.m_MenuItemSave.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemSave, "m_MenuItemSave");
            this.m_MenuItemSave.BackgroundImage = null;
            this.m_MenuItemSave.Name = "m_MenuItemSave";
            this.m_MenuItemSave.ShortcutKeyDisplayString = null;
            this.m_MenuItemSave.Click += new System.EventHandler(this.m_MenuItemSave_Click);
            // 
            // m_MenuItemLoad
            // 
            this.m_MenuItemLoad.AccessibleDescription = null;
            this.m_MenuItemLoad.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemLoad, "m_MenuItemLoad");
            this.m_MenuItemLoad.BackgroundImage = null;
            this.m_MenuItemLoad.Name = "m_MenuItemLoad";
            this.m_MenuItemLoad.ShortcutKeyDisplayString = null;
            this.m_MenuItemLoad.Click += new System.EventHandler(this.m_MenuItemLoad_Click);
            // 
            // m_MenuItemSaveToPrinter
            // 
            this.m_MenuItemSaveToPrinter.AccessibleDescription = null;
            this.m_MenuItemSaveToPrinter.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemSaveToPrinter, "m_MenuItemSaveToPrinter");
            this.m_MenuItemSaveToPrinter.BackgroundImage = null;
            this.m_MenuItemSaveToPrinter.Name = "m_MenuItemSaveToPrinter";
            this.m_MenuItemSaveToPrinter.ShortcutKeyDisplayString = null;
            this.m_MenuItemSaveToPrinter.Click += new System.EventHandler(this.m_MenuItemSaveToPrinter_Click);
            // 
            // m_MenuItemLoadFromPrinter
            // 
            this.m_MenuItemLoadFromPrinter.AccessibleDescription = null;
            this.m_MenuItemLoadFromPrinter.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemLoadFromPrinter, "m_MenuItemLoadFromPrinter");
            this.m_MenuItemLoadFromPrinter.BackgroundImage = null;
            this.m_MenuItemLoadFromPrinter.Name = "m_MenuItemLoadFromPrinter";
            this.m_MenuItemLoadFromPrinter.ShortcutKeyDisplayString = null;
            this.m_MenuItemLoadFromPrinter.Click += new System.EventHandler(this.m_MenuItemLoadFromPrinter_Click);
            // 
            // m_MenuItemTools
            // 
            this.m_MenuItemTools.AccessibleDescription = null;
            this.m_MenuItemTools.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemTools, "m_MenuItemTools");
            this.m_MenuItemTools.BackgroundImage = null;
            this.m_MenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemUpdate,
            this.m_MenuItemDemoPage,
            this.m_MenuItemCalibraion,
            this.m_MenuItemRealTime,
            this.m_MenuItemUVSetting});
            this.m_MenuItemTools.Name = "m_MenuItemTools";
            this.m_MenuItemTools.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemUpdate
            // 
            this.m_MenuItemUpdate.AccessibleDescription = null;
            this.m_MenuItemUpdate.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemUpdate, "m_MenuItemUpdate");
            this.m_MenuItemUpdate.BackgroundImage = null;
            this.m_MenuItemUpdate.Name = "m_MenuItemUpdate";
            this.m_MenuItemUpdate.ShortcutKeyDisplayString = null;
            this.m_MenuItemUpdate.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
            // 
            // m_MenuItemDemoPage
            // 
            this.m_MenuItemDemoPage.AccessibleDescription = null;
            this.m_MenuItemDemoPage.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemDemoPage, "m_MenuItemDemoPage");
            this.m_MenuItemDemoPage.BackgroundImage = null;
            this.m_MenuItemDemoPage.Name = "m_MenuItemDemoPage";
            this.m_MenuItemDemoPage.ShortcutKeyDisplayString = null;
            this.m_MenuItemDemoPage.Click += new System.EventHandler(this.m_MenuItemDemoPage_Click);
            // 
            // m_MenuItemCalibraion
            // 
            this.m_MenuItemCalibraion.AccessibleDescription = null;
            this.m_MenuItemCalibraion.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemCalibraion, "m_MenuItemCalibraion");
            this.m_MenuItemCalibraion.BackgroundImage = null;
            this.m_MenuItemCalibraion.Name = "m_MenuItemCalibraion";
            this.m_MenuItemCalibraion.ShortcutKeyDisplayString = null;
            this.m_MenuItemCalibraion.Click += new System.EventHandler(this.m_MenuItemCalibraion_Click);
            // 
            // m_MenuItemRealTime
            // 
            this.m_MenuItemRealTime.AccessibleDescription = null;
            this.m_MenuItemRealTime.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemRealTime, "m_MenuItemRealTime");
            this.m_MenuItemRealTime.BackgroundImage = null;
            this.m_MenuItemRealTime.Name = "m_MenuItemRealTime";
            this.m_MenuItemRealTime.ShortcutKeyDisplayString = null;
            this.m_MenuItemRealTime.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
            // 
            // m_MenuItemUVSetting
            // 
            this.m_MenuItemUVSetting.AccessibleDescription = null;
            this.m_MenuItemUVSetting.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemUVSetting, "m_MenuItemUVSetting");
            this.m_MenuItemUVSetting.BackgroundImage = null;
            this.m_MenuItemUVSetting.Name = "m_MenuItemUVSetting";
            this.m_MenuItemUVSetting.ShortcutKeyDisplayString = null;
            this.m_MenuItemUVSetting.Click += new System.EventHandler(this.m_MenuItemUVSetting_Click);
            // 
            // m_MenuItemView
            // 
            this.m_MenuItemView.AccessibleDescription = null;
            this.m_MenuItemView.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemView, "m_MenuItemView");
            this.m_MenuItemView.BackgroundImage = null;
            this.m_MenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemTopDown,
            this.m_MenuItemLeftRight});
            this.m_MenuItemView.Name = "m_MenuItemView";
            this.m_MenuItemView.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemTopDown
            // 
            this.m_MenuItemTopDown.AccessibleDescription = null;
            this.m_MenuItemTopDown.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemTopDown, "m_MenuItemTopDown");
            this.m_MenuItemTopDown.BackgroundImage = null;
            this.m_MenuItemTopDown.Checked = true;
            this.m_MenuItemTopDown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_MenuItemTopDown.Name = "m_MenuItemTopDown";
            this.m_MenuItemTopDown.ShortcutKeyDisplayString = null;
            this.m_MenuItemTopDown.Click += new System.EventHandler(this.m_MenuItemTopDown_Click);
            // 
            // m_MenuItemLeftRight
            // 
            this.m_MenuItemLeftRight.AccessibleDescription = null;
            this.m_MenuItemLeftRight.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemLeftRight, "m_MenuItemLeftRight");
            this.m_MenuItemLeftRight.BackgroundImage = null;
            this.m_MenuItemLeftRight.CheckOnClick = true;
            this.m_MenuItemLeftRight.Name = "m_MenuItemLeftRight";
            this.m_MenuItemLeftRight.ShortcutKeyDisplayString = null;
            this.m_MenuItemLeftRight.Click += new System.EventHandler(this.m_MenuItemLeftRight_Click);
            // 
            // m_MenuItemHelp
            // 
            this.m_MenuItemHelp.AccessibleDescription = null;
            this.m_MenuItemHelp.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemHelp, "m_MenuItemHelp");
            this.m_MenuItemHelp.BackgroundImage = null;
            this.m_MenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemAbout});
            this.m_MenuItemHelp.Name = "m_MenuItemHelp";
            this.m_MenuItemHelp.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemAbout
            // 
            this.m_MenuItemAbout.AccessibleDescription = null;
            this.m_MenuItemAbout.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemAbout, "m_MenuItemAbout");
            this.m_MenuItemAbout.BackgroundImage = null;
            this.m_MenuItemAbout.Name = "m_MenuItemAbout";
            this.m_MenuItemAbout.ShortcutKeyDisplayString = null;
            this.m_MenuItemAbout.Click += new System.EventHandler(this.m_MenuItemAbout_Click);
            // 
            // m_MenuItemDebug
            // 
            this.m_MenuItemDebug.AccessibleDescription = null;
            this.m_MenuItemDebug.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemDebug, "m_MenuItemDebug");
            this.m_MenuItemDebug.BackgroundImage = null;
            this.m_MenuItemDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_MenuItemFactoryDebug,
            this.toolStripMenuItem1});
            this.m_MenuItemDebug.Name = "m_MenuItemDebug";
            this.m_MenuItemDebug.ShortcutKeyDisplayString = null;
            // 
            // m_MenuItemFactoryDebug
            // 
            this.m_MenuItemFactoryDebug.AccessibleDescription = null;
            this.m_MenuItemFactoryDebug.AccessibleName = null;
            resources.ApplyResources(this.m_MenuItemFactoryDebug, "m_MenuItemFactoryDebug");
            this.m_MenuItemFactoryDebug.BackgroundImage = null;
            this.m_MenuItemFactoryDebug.Name = "m_MenuItemFactoryDebug";
            this.m_MenuItemFactoryDebug.ShortcutKeyDisplayString = null;
            this.m_MenuItemFactoryDebug.Click += new System.EventHandler(this.m_MenuItemFactoryDebug_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.BackgroundImage = null;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeyDisplayString = null;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AccessibleDescription = null;
            this.statusStrip1.AccessibleName = null;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.BackgroundImage = null;
            this.statusStrip1.Font = null;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_StatusBarPanelJetStaus,
            this.m_StatusBarPanelError,
            this.m_StatusBarPanelPercent,
            this.m_StatusBarPanelComment,
            this.LabelPUMP_BLACK,
            this.LabelPUMP_CYAN,
            this.LabelPUMP_MAGENTA,
            this.LabelPUMP_YELLOW,
            this.LabelPUMP_LIGHTMAGENTA,
            this.LabelPUMP_LIGHTCYAN});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // m_StatusBarPanelJetStaus
            // 
            this.m_StatusBarPanelJetStaus.AccessibleDescription = null;
            this.m_StatusBarPanelJetStaus.AccessibleName = null;
            resources.ApplyResources(this.m_StatusBarPanelJetStaus, "m_StatusBarPanelJetStaus");
            this.m_StatusBarPanelJetStaus.BackgroundImage = null;
            this.m_StatusBarPanelJetStaus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusBarPanelJetStaus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.m_StatusBarPanelJetStaus.Name = "m_StatusBarPanelJetStaus";
            // 
            // m_StatusBarPanelError
            // 
            this.m_StatusBarPanelError.AccessibleDescription = null;
            this.m_StatusBarPanelError.AccessibleName = null;
            resources.ApplyResources(this.m_StatusBarPanelError, "m_StatusBarPanelError");
            this.m_StatusBarPanelError.BackgroundImage = null;
            this.m_StatusBarPanelError.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusBarPanelError.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.m_StatusBarPanelError.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.m_StatusBarPanelError.Name = "m_StatusBarPanelError";
            // 
            // m_StatusBarPanelPercent
            // 
            this.m_StatusBarPanelPercent.AccessibleDescription = null;
            this.m_StatusBarPanelPercent.AccessibleName = null;
            resources.ApplyResources(this.m_StatusBarPanelPercent, "m_StatusBarPanelPercent");
            this.m_StatusBarPanelPercent.BackgroundImage = null;
            this.m_StatusBarPanelPercent.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusBarPanelPercent.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.m_StatusBarPanelPercent.Name = "m_StatusBarPanelPercent";
            // 
            // m_StatusBarPanelComment
            // 
            this.m_StatusBarPanelComment.AccessibleDescription = null;
            this.m_StatusBarPanelComment.AccessibleName = null;
            resources.ApplyResources(this.m_StatusBarPanelComment, "m_StatusBarPanelComment");
            this.m_StatusBarPanelComment.BackgroundImage = null;
            this.m_StatusBarPanelComment.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_StatusBarPanelComment.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.m_StatusBarPanelComment.Name = "m_StatusBarPanelComment";
            this.m_StatusBarPanelComment.Spring = true;
            // 
            // LabelPUMP_BLACK
            // 
            this.LabelPUMP_BLACK.AccessibleDescription = null;
            this.LabelPUMP_BLACK.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_BLACK, "LabelPUMP_BLACK");
            this.LabelPUMP_BLACK.BackColor = System.Drawing.Color.Black;
            this.LabelPUMP_BLACK.BackgroundImage = null;
            this.LabelPUMP_BLACK.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_BLACK.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_BLACK.Name = "LabelPUMP_BLACK";
            // 
            // LabelPUMP_CYAN
            // 
            this.LabelPUMP_CYAN.AccessibleDescription = null;
            this.LabelPUMP_CYAN.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_CYAN, "LabelPUMP_CYAN");
            this.LabelPUMP_CYAN.BackColor = System.Drawing.Color.Cyan;
            this.LabelPUMP_CYAN.BackgroundImage = null;
            this.LabelPUMP_CYAN.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_CYAN.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_CYAN.Name = "LabelPUMP_CYAN";
            // 
            // LabelPUMP_MAGENTA
            // 
            this.LabelPUMP_MAGENTA.AccessibleDescription = null;
            this.LabelPUMP_MAGENTA.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_MAGENTA, "LabelPUMP_MAGENTA");
            this.LabelPUMP_MAGENTA.BackColor = System.Drawing.Color.Magenta;
            this.LabelPUMP_MAGENTA.BackgroundImage = null;
            this.LabelPUMP_MAGENTA.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_MAGENTA.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_MAGENTA.Name = "LabelPUMP_MAGENTA";
            // 
            // LabelPUMP_YELLOW
            // 
            this.LabelPUMP_YELLOW.AccessibleDescription = null;
            this.LabelPUMP_YELLOW.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_YELLOW, "LabelPUMP_YELLOW");
            this.LabelPUMP_YELLOW.BackColor = System.Drawing.Color.Yellow;
            this.LabelPUMP_YELLOW.BackgroundImage = null;
            this.LabelPUMP_YELLOW.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_YELLOW.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_YELLOW.Name = "LabelPUMP_YELLOW";
            // 
            // LabelPUMP_LIGHTMAGENTA
            // 
            this.LabelPUMP_LIGHTMAGENTA.AccessibleDescription = null;
            this.LabelPUMP_LIGHTMAGENTA.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_LIGHTMAGENTA, "LabelPUMP_LIGHTMAGENTA");
            this.LabelPUMP_LIGHTMAGENTA.BackColor = System.Drawing.Color.Violet;
            this.LabelPUMP_LIGHTMAGENTA.BackgroundImage = null;
            this.LabelPUMP_LIGHTMAGENTA.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_LIGHTMAGENTA.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_LIGHTMAGENTA.Name = "LabelPUMP_LIGHTMAGENTA";
            // 
            // LabelPUMP_LIGHTCYAN
            // 
            this.LabelPUMP_LIGHTCYAN.AccessibleDescription = null;
            this.LabelPUMP_LIGHTCYAN.AccessibleName = null;
            resources.ApplyResources(this.LabelPUMP_LIGHTCYAN, "LabelPUMP_LIGHTCYAN");
            this.LabelPUMP_LIGHTCYAN.BackColor = System.Drawing.Color.LightCyan;
            this.LabelPUMP_LIGHTCYAN.BackgroundImage = null;
            this.LabelPUMP_LIGHTCYAN.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.LabelPUMP_LIGHTCYAN.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.LabelPUMP_LIGHTCYAN.Name = "LabelPUMP_LIGHTCYAN";
            // 
            // PumpInktimer
            // 
            this.PumpInktimer.Interval = 500;
            this.PumpInktimer.Tick += new System.EventHandler(this.PumpInktimer_Tick);
            // 
            // Beeptimer
            // 
            this.Beeptimer.Interval = 500;
            this.Beeptimer.Tick += new System.EventHandler(this.Beeptimer_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.m_MainMenu);
            this.DoubleBuffered = true;
            this.Font = null;
            this.Icon = null;
            this.KeyPreview = true;
            this.MainMenuStrip = this.m_MainMenu;
            this.Name = "MainForm";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageWorkForder.ResumeLayout(false);
            this.tabPagePreview.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabPageSetting.PerformLayout();
            this.m_TabControlSetting.ResumeLayout(false);
            this.m_TabPagePrinterSetting.ResumeLayout(false);
            this.m_TabPagePrinterSetting.PerformLayout();
            this.m_TabPageService.ResumeLayout(false);
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.tabStrip2.ResumeLayout(false);
            this.tabStrip2.PerformLayout();
            this.m_ToolBarCommand.ResumeLayout(false);
            this.m_ToolBarCommand.PerformLayout();
            this.m_MainMenu.ResumeLayout(false);
            this.m_MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_MainMenu;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemJob;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusBarPanelError;
        private System.Windows.Forms.ToolStrip m_ToolBarCommand;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ImageList TabimageList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemSetting;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageWorkForder;
        private System.Windows.Forms.TabPage tabPagePreview;
        private System.Windows.Forms.TabPage tabPageSetting;
        private BYHXPrinterManager.JobListView.JobListForm m_JobListForm;
        private BYHXPrinterManager.JobListView.PrintingInfo m_PreviewAndInfo;
        private BYHXPrinterManager.Browser.ExplorerTree prtFileBrowser1;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonAdd;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonDelete;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonPrint;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonPauseResume;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonAbort;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonCheckNozzle;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonSpray;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonSingleClean;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonLeft;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonRight;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonGoHome;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonBackward;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonForward;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonGoHomeY;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonUpZ;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonDownZ;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonGoHomeZ;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonSetOrigin;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonMeasurePaper;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonSetOriginY;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemLoad;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemSaveToPrinter;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemLoadFromPrinter;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemTools;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDemoPage;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemCalibraion;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemUVSetting;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemDebug;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemFactoryDebug;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonAutoClean;
        private System.Windows.Forms.ToolStripButton m_ToolBarButtonStop;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusBarPanelJetStaus;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusBarPanelPercent;
        private System.Windows.Forms.ToolStripStatusLabel m_StatusBarPanelComment;
        private TabStrip tabStrip1;
        private TabStripButton tabStripButtonPrinter;
        private TabStripButton tabStripButtonService;
        private System.Windows.Forms.TabControl m_TabControlSetting;
        private System.Windows.Forms.TabPage m_TabPagePrinterSetting;
        private System.Windows.Forms.TabPage m_TabPageService;
        private BYHXPrinterManager.Setting.SeviceSetting m_SeviceSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private TabStrip tabStrip2;
        private TabStripButton tabStripButtonWorkForder;
        private TabStripButton tabStripButtonPreview;
        private TabStripButton tabStripButtonSetting;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemView;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemTopDown;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemLeftRight;
        private System.Windows.Forms.ToolStripMenuItem m_MenuItemRealTime;
        private System.Windows.Forms.Button bSeviceSettingApply;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_BLACK;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_CYAN;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_LIGHTCYAN;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_LIGHTMAGENTA;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_MAGENTA;
        private System.Windows.Forms.ToolStripStatusLabel LabelPUMP_YELLOW;
        private System.Windows.Forms.Timer PumpInktimer;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoad;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveToPrinter;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadFromPrinter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUpdate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPassWord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFactryDebug;
        private CombinedSettings combinedSettings1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCalibration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFacWrite;
        private System.Windows.Forms.Timer Beeptimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemDongle;
        private System.Windows.Forms.ToolStripMenuItem menuItemDongleKey;

    }
}