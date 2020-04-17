/* *************************************************
 * Programmer: Rajesh Lal(connectrajesh@hotmail.com)
 * Date: 06/25/06
 * Company Info: www.irajesh.com
 * See EULA.txt and Copyright.txt for additional information
 * **************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Text;
using Microsoft.Win32;		//For registry access
using System.Runtime.InteropServices;	//Used for .dll import
using System.IO;
using System.Configuration;
using System.DirectoryServices;
using System.Management;
using System.Net;
using System.Threading;

namespace BYHXPrinterManager.Browser
{
    /// <summary>
    /// Summary description for ExplorerTree.
    /// </summary>
    /// 
    [ToolboxBitmapAttribute(typeof(BYHXPrinterManager.Browser.ExplorerTree), "tree.gif"), DefaultEvent("PathChanged")]
    public class ExplorerTree : System.Windows.Forms.UserControl
    {

        # region Const Variable

        private string STR_DESKTOP = "Desktop";
        private string STR_MYDOCUMENTS = "My Documents";
        private string STR_MYCOMPUTER = "My Computer";
        private string STR_MYNETWORKPLACES = "My Network Places";
        private string STR_ENTIRENETWORK = "Entire Network";
        private string STR_MICROSOFTWINDOWSNETWORK = "Microsoft Windows Network";
        private string STR_MYFAVORITES = "My Favorites";

        private const string STR_NETWORKNODE = "Network Node";
        private const string STR_MYNETNODE = "my netNode";
        private const string STR_NETWORK = "NETWORK";
        private const string STR_SHARE = "-share";

        #endregion

        #region Variable

        private bool goflag = false;
        private bool showMyDocuments = true;
        private bool showMyFavorites = true;
        private bool showMyNetwork = true;

        private bool showAddressbar = true;
        private bool showToolbar = true;
        private bool isByUser = true;

        TreeNode TreeNodeMyComputer;
        TreeNode TreeNodeRootNode;
        TreeNode TreeNodeMyNetWork;
        TreeNode TreeNodeEntireNetwork;
        ServerEnum m_Servers_Folders = null;
        ServerEnum m_Servers = null;
        private bool bCompleted = false;
        BackgroundWorker bgw;
        #endregion

        #region Component Designer generated code
        private System.Windows.Forms.TreeView tvwMain;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenu cMShortcut;
        private System.Windows.Forms.MenuItem mnuShortcut;
        private SplitContainer splitContainer1;
        private ToolStrip grptoolbar;
        private ToolStripButton btnAdd;
        private ToolStripButton btnBack;
        private ToolStripButton btnNext;
        private ToolStripButton btnUp;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnHome;
        private ToolStripButton btnInfo;
        private SplitContainer splitContainer2;
        private BYHXPrinterManager.JobListView.JThumbnailView jThumbnailView1;
        private SplitContainer splitContainer3;
        private TextBox txtPath;
        private Button btnGo;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem ViewModetoolStripMenuItem1;
        private ToolStripMenuItem largeIconToolStripMenuItem;
        private ToolStripMenuItem detailsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem refeshToolStripMenuItem;
        private ToolStripLabel tSLNodeText;
        private ToolStripMenuItem SelectAlltoolStripMenuItem;
        private ToolStripMenuItem InversetoolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private string selectedPath = "home";

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
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerTree));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grptoolbar = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnHome = new System.Windows.Forms.ToolStripButton();
            this.btnInfo = new System.Windows.Forms.ToolStripButton();
            this.tSLNodeText = new System.Windows.Forms.ToolStripLabel();
            this.tvwMain = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.jThumbnailView1 = new BYHXPrinterManager.JobListView.JThumbnailView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectAlltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InversetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewModetoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refeshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.Path = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.cMShortcut = new System.Windows.Forms.ContextMenu();
            this.mnuShortcut = new System.Windows.Forms.MenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grptoolbar.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.TabStop = false;
            this.splitContainer1.Click += new System.EventHandler(this.splitContainer1_Click);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer3
            // 
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grptoolbar);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tvwMain);
            this.splitContainer3.TabStop = false;
            // 
            // grptoolbar
            // 
            this.grptoolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.grptoolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnBack,
            this.btnNext,
            this.btnUp,
            this.btnRefresh,
            this.btnHome,
            this.btnInfo,
            this.tSLNodeText});
            resources.ApplyResources(this.grptoolbar, "grptoolbar");
            this.grptoolbar.Name = "grptoolbar";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBack
            // 
            this.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnHome
            // 
            this.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnHome, "btnHome");
            this.btnHome.Name = "btnHome";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnInfo, "btnInfo");
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // tSLNodeText
            // 
            this.tSLNodeText.Name = "tSLNodeText";
            resources.ApplyResources(this.tSLNodeText, "tSLNodeText");
            // 
            // tvwMain
            // 
            this.tvwMain.AllowDrop = true;
            this.tvwMain.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tvwMain, "tvwMain");
            this.tvwMain.HideSelection = false;
            this.tvwMain.ImageList = this.imageList1;
            this.tvwMain.Name = "tvwMain";
            this.tvwMain.ShowLines = false;
            this.tvwMain.ShowRootLines = false;
            this.tvwMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwMain_BeforeExpand);
            this.tvwMain.DoubleClick += new System.EventHandler(this.tvwMain_DoubleClick);
            this.tvwMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwMain_MouseUp);
            this.tvwMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMain_AfterSelect);
            this.tvwMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwMain_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            this.imageList1.Images.SetKeyName(16, "");
            this.imageList1.Images.SetKeyName(17, "");
            this.imageList1.Images.SetKeyName(18, "");
            this.imageList1.Images.SetKeyName(19, "");
            this.imageList1.Images.SetKeyName(20, "");
            this.imageList1.Images.SetKeyName(21, "");
            this.imageList1.Images.SetKeyName(22, "");
            this.imageList1.Images.SetKeyName(23, "");
            this.imageList1.Images.SetKeyName(24, "");
            this.imageList1.Images.SetKeyName(25, "");
            this.imageList1.Images.SetKeyName(26, "");
            this.imageList1.Images.SetKeyName(27, "");
            this.imageList1.Images.SetKeyName(28, "");
            // 
            // splitContainer2
            // 
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtPath);
            this.splitContainer2.Panel1.Controls.Add(this.btnGo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.jThumbnailView1);
            this.splitContainer2.TabStop = false;
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPath_KeyUp);
            // 
            // btnGo
            // 
            resources.ApplyResources(this.btnGo, "btnGo");
            this.btnGo.ImageList = this.imageList1;
            this.btnGo.Name = "btnGo";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // jThumbnailView1
            // 
            this.jThumbnailView1.AllowDrop = true;
            this.jThumbnailView1.AnimatedImage = global::IntermediateControl.Properties.Resources.status_anim;
            this.jThumbnailView1.CanReLoad = true;
            this.jThumbnailView1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.jThumbnailView1, "jThumbnailView1");
            this.jThumbnailView1.FolderName = "";
            this.jThumbnailView1.Name = "jThumbnailView1";
            this.jThumbnailView1.OwnerDraw = true;
            this.jThumbnailView1.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.jThumbnailView1.ThumbNailSize = 95;
            this.jThumbnailView1.UseCompatibleStateImageBehavior = false;
            this.jThumbnailView1.ItemActivate += new System.EventHandler(this.jThumbnailView1_ItemActivate);
            this.jThumbnailView1.SelectedIndexChanged += new System.EventHandler(this.jThumbnailView1_SelectedIndexChanged);
            this.jThumbnailView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwMain_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAlltoolStripMenuItem,
            this.InversetoolStripMenuItem,
            this.toolStripSeparator2,
            this.addToolStripMenuItem,
            this.ViewModetoolStripMenuItem1,
            this.toolStripSeparator1,
            this.refeshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // SelectAlltoolStripMenuItem
            // 
            this.SelectAlltoolStripMenuItem.Name = "SelectAlltoolStripMenuItem";
            resources.ApplyResources(this.SelectAlltoolStripMenuItem, "SelectAlltoolStripMenuItem");
            this.SelectAlltoolStripMenuItem.Click += new System.EventHandler(this.SelectAlltoolStripMenuItem_Click);
            // 
            // InversetoolStripMenuItem
            // 
            this.InversetoolStripMenuItem.Name = "InversetoolStripMenuItem";
            resources.ApplyResources(this.InversetoolStripMenuItem, "InversetoolStripMenuItem");
            this.InversetoolStripMenuItem.Click += new System.EventHandler(this.InversetoolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // ViewModetoolStripMenuItem1
            // 
            this.ViewModetoolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.ViewModetoolStripMenuItem1.Name = "ViewModetoolStripMenuItem1";
            resources.ApplyResources(this.ViewModetoolStripMenuItem1, "ViewModetoolStripMenuItem1");
            // 
            // largeIconToolStripMenuItem
            // 
            this.largeIconToolStripMenuItem.Checked = true;
            this.largeIconToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.largeIconToolStripMenuItem.Name = "largeIconToolStripMenuItem";
            resources.ApplyResources(this.largeIconToolStripMenuItem, "largeIconToolStripMenuItem");
            this.largeIconToolStripMenuItem.Click += new System.EventHandler(this.largeIconToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            resources.ApplyResources(this.detailsToolStripMenuItem, "detailsToolStripMenuItem");
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // refeshToolStripMenuItem
            // 
            this.refeshToolStripMenuItem.Name = "refeshToolStripMenuItem";
            resources.ApplyResources(this.refeshToolStripMenuItem, "refeshToolStripMenuItem");
            this.refeshToolStripMenuItem.Click += new System.EventHandler(this.refeshToolStripMenuItem_Click);
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Path,
            this.Status});
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // cMShortcut
            // 
            this.cMShortcut.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuShortcut});
            // 
            // mnuShortcut
            // 
            this.mnuShortcut.Index = 0;
            resources.ApplyResources(this.mnuShortcut, "mnuShortcut");
            this.mnuShortcut.Click += new System.EventHandler(this.mnuShortcut_Click);
            // 
            // ExplorerTree
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.listView1);
            this.Name = "ExplorerTree";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.ExplorerTree_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.grptoolbar.ResumeLayout(false);
            this.grptoolbar.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Constructor

        public ExplorerTree()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitializeComponent call

            bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

            if (!PubFunc.IsFactoryUser())
                this.btnInfo.Visible = false;
            setCultrueString();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                bCompleted = true;
                this.GetAllShareFiles(this.TreeNodeMyNetWork);
                this.GetEntireNetwork(this.TreeNodeEntireNetwork);
            }
            catch (Exception)
            {
#if DEBUG
                System.Diagnostics.Debug.Assert(false);
#endif
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.GetNetworkList();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false);
            }
        }

        #endregion

        # region propertys

        private bool GoFlag
        {
            get
            {
                return goflag;
            }
            set
            {
                goflag = value;
            }
        }

        public bool ShowAddressbar
        {
            get
            {
                return showAddressbar;
            }
            set
            {
                showAddressbar = value;
            }
        }

        public bool ShowToolbar
        {
            get
            {
                return showToolbar;
            }
            set
            {
                showToolbar = value;
            }
        }

        public bool ShowMyDocuments
        {
            get
            {
                return showMyDocuments;
            }
            set
            {
                showMyDocuments = value;
                this.Refresh();
            }
        }

        public bool ShowMyFavorites
        {
            get
            {
                return showMyFavorites;
            }
            set
            {
                showMyFavorites = value;
                this.Refresh();
            }
        }

        public bool ShowMyNetwork
        {
            get
            {
                return showMyNetwork;
            }
            set
            {
                showMyNetwork = value;
                this.Refresh();
            }
        }

        [Browsable(false)]
        public string PreviewFolder
        {
            get { return this.jThumbnailView1.m_LastPreviewForlder; }
        }

        [Category("Appearance"), Description("Selected Path of Image")]
        public string SelectedPath
        {
            get
            {
                return this.selectedPath;
            }
            set
            {
                this.selectedPath = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return this.jThumbnailView1.SelectedItems;
            }
        }
        #endregion

        # region Event

        #region event define
        public delegate void PathChangedEventHandler(object sender, EventArgs e);
        // This delegate is used for the UserContextMenuClickEvent
        public delegate void UserContextMenuClickEventHandler(object sender, EventArgs e);
        // This delegate is used for the SelectedFileChanged Event
        public delegate void SelectedFileChangedEventHandler(object sender, EventArgs e);

        public event PathChangedEventHandler PathChanged;
        public event UserContextMenuClickEventHandler UserContextMenuClick;
        public event SelectedFileChangedEventHandler SelectedFilesChanged;
        #endregion

        private void ExplorerTree_Load(object sender, System.EventArgs e)
        {
            if (DesignMode)
                return;
            this.grptoolbar.ImageList = this.imageList1;
            this.btnAdd.ImageIndex = 18;
            this.btnBack.ImageIndex = 20;
            this.btnNext.ImageIndex = 19;
            this.btnUp.ImageIndex = 24;
            this.btnRefresh.ImageIndex = 25;
            this.btnHome.ImageIndex = 23;
            this.btnInfo.ImageIndex = 22;
            GetDirectory();
            bgw.RunWorkerAsync();
            selectedPath = this.jThumbnailView1.m_LastPreviewForlder;
            if (Directory.Exists(selectedPath))
            {
                setCurrentPath(selectedPath);
            }
            else
            {
                setCurrentPath("home");
            }
            btnGo_Click(this, e);

            refreshView();
        }

        private void tvwMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                if (e.Action == TreeViewAction.Unknown)
                    return;
                TreeNode n = e.Node;
                if ((String.Compare(n.Text, STR_MYCOMPUTER) == 0)
                    || (String.Compare(n.Text, STR_MYNETWORKPLACES) == 0)
                    || (String.Compare(n.Text, STR_ENTIRENETWORK) == 0))
                {
                }
                else
                {
                    txtPath.Text = n.Tag.ToString();
                }
            }
            catch { }
        }

        private void tvwMain_DoubleClick(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ExploreTreeNode(tvwMain.SelectedNode);
            Cursor.Current = Cursors.Default;
        }

        private void tvwMain_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //if(e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)
            if (isByUser)
                this.ExploreTreeNode(e.Node);

            Cursor.Current = Cursors.Default;
        }

        private void tvwMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            updateList(txtPath.Text);
            if (tvwMain.SelectedNode != null)
            {

                if ((tvwMain.SelectedNode.ImageIndex == 18) && (e.Button == MouseButtons.Right))
                    cMShortcut.Show(tvwMain, new Point(e.X, e.Y));
            }
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            refreshView();

            try
            {
                TreeNode selcetednd = (TreeNode)tvwMain.SelectedNode.Clone();
                refreshFolders();
                TreeNode[] tns = tvwMain.Nodes.Find(selcetednd.Name, true);
                if (Directory.Exists(selcetednd.Tag.ToString()))
                {
                    btnGo_Click(null, null);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("Error: " + e1.Message);
            }
            finally
            {
                //setCurrentPath("home");
                Cursor.Current = Cursors.Default;
                //ExploreMyComputer();
            }

        }

        public void btnGo_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //ExploreMyComputer();
                string myString = "";
                bool h = true;
                myString = txtPath.Text.ToLower();

                TreeNode tn = TreeNodeMyComputer;

            StartAgain:

                do
                {
                    foreach (TreeNode t in tn.Nodes)
                    {
                        string mypath = t.Tag.ToString();
                        //mypath =  mypath.Replace("Desktop\\","") ;
                        //mypath =  mypath.Replace("My Computer\\","") ;
                        //mypath =  mypath.Replace(@"\\",@"\") ;

                        //mypath =  mypath.Replace("My Documents\\",Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\") ;
                        mypath = mypath.ToLower();
                        string mypathf = mypath;
                        if (!mypath.EndsWith(@"\"))
                        {
                            if (myString.Length > mypathf.Length)
                                mypathf = mypath + @"\";
                        }

                        if (myString.StartsWith(mypathf))
                        {
                            t.TreeView.Focus();
                            t.TreeView.SelectedNode = t;
                            t.EnsureVisible();
                            //t.Expand();
                            if (t.Nodes.Count >= 1)
                            {
                                t.Expand();
                                tn = t;
                            }
                            else
                            {
                                if (String.Compare(myString, mypath) == 0)
                                {
                                    h = false;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            if (mypathf.StartsWith(myString))
                            {
                                h = false;
                                break;
                            }
                            else
                            {
                                goto StartAgain;
                            }
                        }
                    }
                    tn = tn.NextNode;

                } while (h && tn != null);

            }
            catch (Exception e1)
            {
                MessageBox.Show("Error: " + e1.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnHome_Click(object sender, System.EventArgs e)
        {
            setCurrentPath("home");
            //ExploreMyComputer();
            btnGo_Click(sender, e);

        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            GoFlag = true;
            string cpath = txtPath.Text;
            UpdateListGoFwd();

            if (String.Compare(cpath, txtPath.Text) == 0)
            { }
            else
            {
                btnGo_Click(sender, e);
            }
            GoFlag = false;
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            GoFlag = true;
            string cpath = txtPath.Text;
            UpdateListGoBack();

            if (String.Compare(cpath, txtPath.Text) == 0)
            { }
            else
            {
                btnGo_Click(sender, e);
            }
            GoFlag = false;
        }

        private void btnUp_Click(object sender, System.EventArgs e)
        {
            try
            {
                DirectoryInfo MYINFO = new DirectoryInfo(txtPath.Text);

                if (MYINFO.Parent.Exists)
                    txtPath.Text = MYINFO.Parent.FullName;
                updateList(txtPath.Text);
                btnGo_Click(sender, e);
            }
            catch (Exception)
            {
                //MessageBox.Show ("Parent directory does not exists","Directory Not Found",MessageBoxButtons.OK,MessageBoxIcon.Information ); 
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string myname = "";
            string mypath = "";


            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Add Folder in Explorer Tree";
            dialog.ShowNewFolderButton = true;
            dialog.SelectedPath = txtPath.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mypath = dialog.SelectedPath;
                myname = mypath.Substring(mypath.LastIndexOf("\\") + 1);

                AddFolderNode(myname, mypath);

            }
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            AboutExplorerTree();
        }

        private void mnuShortcut_Click(object sender, System.EventArgs e)
        {
            if (tvwMain.SelectedNode.ImageIndex == 18)
                tvwMain.SelectedNode.Remove();
        }

        private void txtPath_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (Directory.Exists(txtPath.Text))
                {
                    SelectedPath = txtPath.Text;
                    this.jThumbnailView1.FolderName = SelectedPath;
                    if (PathChanged != null)
                        PathChanged(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPath_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnGo_Click(sender, e);
                txtPath.Focus();
            }
        }

        private void SelectAlltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jThumbnailView1.SelectAll();
        }

        private void InversetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jThumbnailView1.Inverse();
        }

        private void refeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jThumbnailView1.ReLoadItems();
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.largeIconToolStripMenuItem.Checked = true;
            this.detailsToolStripMenuItem.Checked = false;
            this.jThumbnailView1.View = View.LargeIcon;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.largeIconToolStripMenuItem.Checked = false;
            this.detailsToolStripMenuItem.Checked = true;
            this.jThumbnailView1.View = View.Details;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.UserContextMenuClick != null && ((ToolStripMenuItem)sender).Name == this.addToolStripMenuItem.Name)
                this.UserContextMenuClick(0, e);
        }

        private void jThumbnailView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedFilesChanged != null)
                this.SelectedFilesChanged(sender, e);
        }

        private void jThumbnailView1_ItemActivate(object sender, EventArgs e)
        {
            if (this.UserContextMenuClick != null)
                this.UserContextMenuClick(0, e);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.SelectNextControl(this, true, true, true, false);
        }

        private void splitContainer1_Click(object sender, EventArgs e)
        {
            this.SelectNextControl(this, true, true, true, false);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.addToolStripMenuItem.Enabled = this.jThumbnailView1.SelectedItems.Count > 0;
            this.refeshToolStripMenuItem.Enabled = this.jThumbnailView1.SelectedItems.Count == 0;
        }
        #endregion

        # region private Method

        private void GetNetworkList()
        {
            m_Servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
                                        ResourceType.RESOURCETYPE_DISK,
                                        ResourceUsage.RESOURCEUSAGE_ALL,
                                        ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK, "");

            ServerEnum servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
                                                    ResourceType.RESOURCETYPE_DISK,
                                                    ResourceUsage.RESOURCEUSAGE_ALL,
                                                    ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK, "");

            foreach (string s1 in servers)
            {
                string s2 = "";
                s2 = s1.Substring(0, s1.IndexOf("|", 1));

                if (s1.IndexOf(STR_NETWORK, 1) <= 0)
                {
                    m_Servers_Folders = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
                                            ResourceType.RESOURCETYPE_DISK,
                                            ResourceUsage.RESOURCEUSAGE_ALL,
                                            ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER, s2);
                    return;
                }
            }

        }

        private void GetEntireNetwork(TreeNode tn)
        {
            if (!bCompleted)
                return;
            if (tn.FirstNode.Text == STR_NETWORKNODE)
            {
                tn.FirstNode.Remove();
                //NETRESOURCE netRoot = new NETRESOURCE();

                //if (m_Servers == null)
                //    m_Servers = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
                //                                            ResourceType.RESOURCETYPE_DISK,
                //                                            ResourceUsage.RESOURCEUSAGE_ALL,
                //                                            ResourceDisplayType.RESOURCEDISPLAYTYPE_NETWORK, "");

                foreach (string s1 in m_Servers)
                {
                    string s2 = "";
                    s2 = s1.Substring(0, s1.IndexOf("|", 1));

                    if (s1.IndexOf(STR_NETWORK, 1) > 0)
                    {
                        TreeNode nodeNN = new TreeNode();
                        nodeNN.Tag = s2;
                        nodeNN.Text = s2;//dir.Substring(dir.LastIndexOf(@"\") + 1);
                        nodeNN.ImageIndex = 15;
                        nodeNN.SelectedImageIndex = 15;
                        tn.Nodes.Add(nodeNN);
                    }
                    else
                    {
                        //TreeNode nodemNc;
                        TreeNode nodemN = new TreeNode();
                        nodemN.Tag = s2;//"my Node";
                        nodemN.Text = s2;//"my Node";//dir.Substring(dir.LastIndexOf(@"\") + 1);
                        nodemN.ImageIndex = 16;
                        nodemN.SelectedImageIndex = 16;
                        tn.LastNode.Nodes.Add(nodemN);

                        //TreeNode nodemNc = new TreeNode();
                        //nodemNc.Tag = STR_MYNETNODE;
                        //nodemNc.Text = STR_MYNETNODE;//dir.Substring(dir.LastIndexOf(@"\") + 1);
                        //nodemNc.ImageIndex = 12;
                        //nodemNc.SelectedImageIndex = 12;
                        //nodemN.Nodes.Add(nodemNc);

                        this.GetMicrosoftWindowsNetwork(nodemN);
                    }
                }
            }
        }

        /// <summary>
        /// 枚举指定计算机的共享文件夹 
        /// </summary>
        /// <param name="tnCom"></param>
        /// <param name="Computer"></param>
        private void GetMicrosoftWindowsNetwork(TreeNode tnCom)
        {
            if (!bCompleted || m_Servers_Folders == null)
                return;
            //if (tnCom.FirstNode.Text == STR_MYNETNODE)
            //{
            //    tnCom.FirstNode.Remove();

            //string pS = tnCom.Text;
            //if (m_Servers_Folders == null)
            //    m_Servers_Folders = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
            //            ResourceType.RESOURCETYPE_DISK,
            //            ResourceUsage.RESOURCEUSAGE_ALL,
            //            ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER, pS);

            foreach (string s1 in m_Servers_Folders)
            {
                string s2 = "";

                if ((s1.Length < 6) || (String.Compare(s1.Substring(s1.Length - 6, 6), STR_SHARE) != 0))
                {
                    s2 = s1;//.Substring(s1.IndexOf("\\",2));
                    TreeNode nodeNN = new TreeNode();
                    nodeNN.Tag = s2;
                    nodeNN.Text = s2.Substring(2);
                    nodeNN.ImageIndex = 12;
                    nodeNN.SelectedImageIndex = 12;
                    tnCom.Nodes.Add(nodeNN);
                    foreach (string s1node in m_Servers_Folders)
                    {
                        if (s1node.Length > 6)
                        {
                            if (String.Compare(s1node.Substring(s1node.Length - 6, 6), STR_SHARE) == 0)
                            {
                                if (s2.Length <= s1node.Length)
                                {
                                    try
                                    {
                                        if (String.Compare(s1node.Substring(0, s2.Length + 1), s2 + @"\") == 0)
                                        {
                                            TreeNode nodeNNode = new TreeNode();
                                            nodeNNode.Tag = s1node.Substring(0, s1node.Length - 6);
                                            nodeNNode.Text = s1node.Substring(s2.Length + 1, s1node.Length - s2.Length - 7);
                                            nodeNNode.ImageIndex = 28;
                                            nodeNNode.SelectedImageIndex = 28;
                                            nodeNN.Nodes.Add(nodeNNode);
                                        }
                                    }
                                    catch (Exception)
                                    { 
                                    }
                                }
                            }
                        }
                    }
                }

            }
            //}
        }

        private void GetAllShareFiles(TreeNode tnCom)
        {
            if (!bCompleted || m_Servers_Folders == null)
                return;

            //NETRESOURCE netRoot = new NETRESOURCE();
            //if (m_Servers_Folders == null)
            //{
            //    m_Servers_Folders = new ServerEnum(ResourceScope.RESOURCE_GLOBALNET,
            //                                ResourceType.RESOURCETYPE_DISK,
            //                                ResourceUsage.RESOURCEUSAGE_ALL,
            //                                ResourceDisplayType.RESOURCEDISPLAYTYPE_SERVER, pS);
            //}
            foreach (string s1 in m_Servers_Folders)
            {
                string s2 = "";

                if ((s1.Length > 6) && (String.Compare(s1.Substring(s1.Length - 6, 6), STR_SHARE) == 0))
                {
                    s2 = s1.Substring(s1.IndexOf("\\", 2));
                    TreeNode nodeNN = new TreeNode();
                    nodeNN.Tag = s1.Substring(0, s1.Length - 6);
                    nodeNN.Text = s2.Substring(s1.IndexOf("\\", 1), s2.Length - 7);
                    nodeNN.ImageIndex = 28;
                    nodeNN.SelectedImageIndex = 28;
                    tnCom.Nodes.Add(nodeNN);
                    this.FillFilesandDirs(nodeNN);
                }
            }
        }

        private void ExploreTreeNode(TreeNode n)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //get dirs one more level deep in current dir so user can see there is
                //more dirs underneath current dir
                if (String.Compare(n.Text, STR_DESKTOP) == 0)
                {
                    //GetDirectory();
                }
                else if (String.Compare(n.Text, STR_MYCOMPUTER) == 0)
                {
                    this.ExploreMyComputer();
                }
                else if (string.Compare(n.Text, STR_MYNETWORKPLACES) == 0)
                {
                    //GetAllShareFiles(n);
                }
                else if ((String.Compare(n.Text, STR_ENTIRENETWORK) == 0))
                {
                    //this.GetEntireNetwork(n);
                }
                else if ((n.Parent != null) && (String.Compare(n.Parent.Text, STR_MICROSOFTWINDOWSNETWORK) == 0))
                {
                    //this.GetMicrosoftWindowsNetwork(n);
                }
                else
                {
                    FillFilesandDirs(n);
                    foreach (TreeNode subNode in n.Nodes)
                    {
                        FillFilesandDirs(subNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void GetDirectories(TreeNode parentNode)
        {
            // added after suggestion
            string[] dirList;

            dirList = Directory.GetDirectories(parentNode.Tag.ToString(), "*", SearchOption.TopDirectoryOnly);
            Array.Sort(dirList);

            //check if dir already exists in case click same dir twice
            //if (dirList.Length == parentNode.Nodes.Count)
            //return;
            //add each dir in selected dir
            if (String.Compare(parentNode.Text, STR_DESKTOP) != 0)//(parentNode != this.TreeNodeRootNode)
            {
                parentNode.Nodes.Clear();
                for (int i = 0; i < dirList.Length; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = dirList[i]; //store path in tag
                    node.Text = dirList[i].Substring(dirList[i].LastIndexOf(@"\") + 1);
                    node.ImageIndex = 1;
                    parentNode.Nodes.Add(node);
                }
            }
            else
            {
                for (int i = 0; i < dirList.Length; i++)
                {
                    if (!this.Contains(parentNode, dirList[i]))
                    {
                        TreeNode node = new TreeNode();
                        node.Tag = dirList[i]; //store path in tag
                        node.Text = dirList[i].Substring(dirList[i].LastIndexOf(@"\") + 1);
                        node.ImageIndex = 1;
                        parentNode.Nodes.Add(node);
                        GetDirectories(node);
                    }
                }

                int j = parentNode.Nodes.Count;
                for (int i = 0; i < j; i++)
                {
                    bool contains = false;
                    foreach (string str in dirList)
                    {
                        if (parentNode.Nodes[i].Tag.ToString() == str)
                        {
                            contains = true;
                            break;
                        }
                    }
                    if (!contains)
                    {
                        parentNode.Nodes.RemoveAt(i);
                        j = j - 1;
                    }
                }
            }
        }

        private bool Contains(TreeNode parentNode, string path)
        {
            foreach (TreeNode tn in parentNode.Nodes)
                if (tn.Tag.ToString() == path)
                    return true;
            return false;
        }

        private void FillFilesandDirs(TreeNode comunalNode)
        {
            try
            {
                GetDirectories(comunalNode);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void ExploreMyComputer()
        {

            string[] drives = Environment.GetLogicalDrives();
            string dir2 = "";

            Cursor.Current = Cursors.WaitCursor;
            TreeNode nodeDrive;

            if (TreeNodeMyComputer.GetNodeCount(true) < 2 || TreeNodeMyComputer.GetNodeCount(false) != drives.Length)
            {
                try
                {
                    //TreeNodeMyComputer.FirstNode.Remove();
                    TreeNodeMyComputer.Nodes.Clear();

                    foreach (string drive in drives)
                    {
                        nodeDrive = new TreeNode();
                        nodeDrive.Tag = drive;

                        nodeDrive.Text = drive;

                        switch (Win32.GetDriveType(drive))
                        {
                            case 2:
                                nodeDrive.ImageIndex = 17;
                                nodeDrive.SelectedImageIndex = 17;
                                break;
                            case 3:
                                nodeDrive.ImageIndex = 0;
                                nodeDrive.SelectedImageIndex = 0;
                                break;
                            case 4:
                                nodeDrive.ImageIndex = 8;
                                nodeDrive.SelectedImageIndex = 8;
                                break;
                            case 5:
                                nodeDrive.ImageIndex = 7;
                                nodeDrive.SelectedImageIndex = 7;
                                break;
                            default:
                                nodeDrive.ImageIndex = 0;
                                nodeDrive.SelectedImageIndex = 0;
                                break;
                        }

                        TreeNodeMyComputer.Nodes.Add(nodeDrive);

                        //add dirs under drive
                        if (Directory.Exists(drive))
                        {
                            foreach (string dir in Directory.GetDirectories(drive))
                            {
                                dir2 = dir;
                                TreeNode node = new TreeNode();
                                node.Tag = dir;
                                node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
                                node.ImageIndex = 1;
                                nodeDrive.Nodes.Add(node);
                            }
                        }
                    }
                }
                catch (Exception ex)	//error just add blank dir
                {
                    MessageBox.Show("Error while Filling the Explorer:" + ex.Message);
                }
            }

            //TreeNodeMyComputer.Expand();
        }

        private void UpdateListAddCurrent()
        {
            int i = 0;
            int j = 0;

            int icount = 0;
            icount = listView1.Items.Count + 1;

            for (i = 0; i < listView1.Items.Count - 1; i++)
            {
                if (String.Compare(listView1.Items[i].SubItems[1].Text, "Selected") == 0)
                {
                    for (j = listView1.Items.Count - 1; j > i + 1; j--)
                        listView1.Items[j].Remove();
                    break;
                }

            }
        }

        private void UpdateListGoBack()
        {
            if ((listView1.Items.Count > 0) && (String.Compare(listView1.Items[0].SubItems[1].Text, "Selected") == 0))
                return;
            int i = 0;
            for (i = 0; i < listView1.Items.Count; i++)
            {
                if (String.Compare(listView1.Items[i].SubItems[1].Text, "Selected") == 0)
                {
                    if (i != 0)
                    {
                        listView1.Items[i - 1].SubItems[1].Text = "Selected";
                        txtPath.Text = listView1.Items[i - 1].Text;
                    }
                }
                if (i != 0)
                {
                    listView1.Items[i].SubItems[1].Text = " -/- ";
                }
            }
        }

        private void UpdateListGoFwd()
        {
            if ((listView1.Items.Count > 0) && (String.Compare(listView1.Items[listView1.Items.Count - 1].SubItems[1].Text, "Selected") == 0))
                return;
            int i = 0;
            for (i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (String.Compare(listView1.Items[i].SubItems[1].Text, "Selected") == 0)
                {
                    if (i != listView1.Items.Count)
                    {
                        listView1.Items[i + 1].SubItems[1].Text = "Selected";
                        txtPath.Text = listView1.Items[i + 1].Text;
                    }
                }

                if (i != listView1.Items.Count - 1) listView1.Items[i].SubItems[1].Text = " -/- ";
            }
        }

        private void updateList(string f)
        {
            int i = 0;
            ListViewItem listviewitem;		// Used for creating listview items.

            int icount = 0;
            UpdateListAddCurrent();
            icount = listView1.Items.Count + 1;
            try
            {
                if (listView1.Items.Count > 0)
                {
                    if (String.Compare(listView1.Items[listView1.Items.Count - 1].Text, f) == 0)
                    {
                        return;
                    }
                }

                for (i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].SubItems[1].Text = " -/- ";
                }
                listviewitem = new ListViewItem(f);
                listviewitem.SubItems.Add("Selected");
                listviewitem.Tag = f;
                this.listView1.Items.Add(listviewitem);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddFolderNode(string name, string path)
        {

            try
            {
                TreeNode nodemyC = new TreeNode();

                nodemyC.Tag = path;
                nodemyC.Text = name;

                nodemyC.ImageIndex = 18;
                nodemyC.SelectedImageIndex = 18;

                TreeNodeRootNode.Nodes.Add(nodemyC);

                try
                {
                    //add dirs under drive
                    if (Directory.Exists(path))
                    {
                        foreach (string dir in Directory.GetDirectories(path))
                        {
                            TreeNode node = new TreeNode();
                            node.Tag = dir;
                            node.Text = dir.Substring(dir.LastIndexOf(@"\") + 1);
                            node.ImageIndex = 1;
                            nodemyC.Nodes.Add(node);
                        }
                    }
                }
                catch (Exception ex)	//error just add blank dir
                {
                    MessageBox.Show("Error while Filling the Explorer:" + ex.Message);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void setCultrueString()
        {
            char[] spliter = new char[1] { ',' };
            string[] strCuls = this.tSLNodeText.Text.Split(spliter);
            if (strCuls.Length == 6)
            {
                STR_DESKTOP = strCuls[0];
                STR_MYDOCUMENTS = strCuls[1];
                STR_MYCOMPUTER = strCuls[2];
                STR_MYNETWORKPLACES = strCuls[3];
                STR_ENTIRENETWORK = strCuls[4];
                STR_MYFAVORITES = strCuls[5];
            }
            this.tSLNodeText.Visible = false;
        }

        private bool IsResourceShared(string strServer, string strResourceName, out ShareType nType)
        {
            bool bRet = false;
            nType = ShareType.STYPE_DISKTREE;
            long lType = 0;
            int nRet = Win32.NetShareCheck(strServer, strResourceName, out lType);
            bRet = (0 == nRet);
            if (!bRet)
            {
                if (nRet == 2311)
                {
                    Console.WriteLine("Device not shared");
                }
                else if (nRet == 2310)
                {
                    Console.WriteLine("The device does not exist");
                }
                else
                {
                    Console.WriteLine("Unknown win32 error");
                }
            }
            else
            {
                nType = (ShareType)lType;
            }
            return bRet;
        }
        #endregion

        # region Public Method

        public void refreshView()
        {
            if ((!showAddressbar) && (!showToolbar))
            {
                tvwMain.Top = 0;
                txtPath.Visible = false;
                btnGo.Visible = false;
                grptoolbar.Visible = false;
                tvwMain.Height = this.Height;
            }
            else
            {
                if (showToolbar && (!showAddressbar))
                {
                    tvwMain.Top = 20;
                    txtPath.Visible = false;
                    btnGo.Visible = false;
                    tvwMain.Height = this.Height - 20;
                    grptoolbar.Visible = true;
                }
                else if (showAddressbar && (!showToolbar))
                {
                    tvwMain.Top = 20;
                    txtPath.Top = 1;
                    btnGo.Top = -2;
                    txtPath.Visible = true;
                    btnGo.Visible = true;
                    tvwMain.Height = this.Height - 20;
                    grptoolbar.Visible = false;
                }
                else
                {
                    tvwMain.Top = 40;
                    txtPath.Visible = true;
                    btnGo.Visible = true;
                    txtPath.Top = 19;
                    btnGo.Top = 16;
                    grptoolbar.Visible = true;
                    tvwMain.Height = this.Height - 40;
                }
            }
        }

        public void refreshFolders()
        {
            listView1.Items.Clear();
            //tvwMain.Nodes.Clear();
            //setCurrentPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            GetDirectory();
        }

        public void GetDirectory()
        {
            isByUser = false;
            tvwMain.Nodes.Clear();
            m_Servers_Folders = null;

            string[] drives = Environment.GetLogicalDrives();
            TreeNode nodeDesktop;
            //Environment.UserDomainName .GetFolderPath( 
            //Environment.GetFolderPath (Environment.SystemDirectory);

            TreeNode nodeMyDocuments;
            TreeNode nodeMyFavorites;
            TreeNode nodeMyComputer;
            //TreeNode nodemNc;

            TreeNode nodeNN;

            nodeDesktop = new TreeNode();
            nodeDesktop.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            nodeDesktop.Text = STR_DESKTOP;
            nodeDesktop.ImageIndex = 10;
            nodeDesktop.SelectedImageIndex = 10;

            tvwMain.Nodes.Add(nodeDesktop);
            FillFilesandDirs(nodeDesktop);
            TreeNodeRootNode = nodeDesktop;

            if (ShowMyDocuments)
            {
                //Add My Documents and Desktop folder outside
                nodeMyDocuments = new TreeNode();
                nodeMyDocuments.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                nodeMyDocuments.Text = STR_MYDOCUMENTS;
                nodeMyDocuments.ImageIndex = 9;
                nodeMyDocuments.SelectedImageIndex = 9;
                nodeDesktop.Nodes.Insert(0, nodeMyDocuments);
                FillFilesandDirs(nodeMyDocuments);
            }

            nodeMyComputer = new TreeNode();
            nodeMyComputer.Tag = STR_MYCOMPUTER;
            nodeMyComputer.Text = STR_MYCOMPUTER;
            nodeMyComputer.ImageIndex = 12;
            nodeMyComputer.SelectedImageIndex = 12;
            nodeDesktop.Nodes.Insert(1, nodeMyComputer);

            TreeNodeMyComputer = nodeMyComputer;
            ExploreMyComputer();
            //nodeMyComputer.EnsureVisible();

            //nodemNc = new TreeNode();
            //nodemNc.Tag = "my Node";
            //nodemNc.Text = "my Node";//dir.Substring(dir.LastIndexOf(@"\") + 1);
            //nodemNc.ImageIndex = 12;
            //nodemNc.SelectedImageIndex = 12;
            //nodeMyComputer.Nodes.Add(nodemNc);

            if (ShowMyNetwork)
            {
                TreeNodeMyNetWork = new TreeNode();
                TreeNodeMyNetWork.Tag = STR_MYNETWORKPLACES;
                TreeNodeMyNetWork.Text = STR_MYNETWORKPLACES;
                TreeNodeMyNetWork.ImageIndex = 13;
                TreeNodeMyNetWork.SelectedImageIndex = 13;
                nodeDesktop.Nodes.Insert(2, TreeNodeMyNetWork);
                //nodemyN.EnsureVisible();

                TreeNodeEntireNetwork = new TreeNode();
                TreeNodeEntireNetwork.Tag = STR_ENTIRENETWORK;
                TreeNodeEntireNetwork.Text = STR_ENTIRENETWORK;
                TreeNodeEntireNetwork.ImageIndex = 14;
                TreeNodeEntireNetwork.SelectedImageIndex = 14;
                TreeNodeMyNetWork.Nodes.Add(TreeNodeEntireNetwork);

                nodeNN = new TreeNode();
                nodeNN.Tag = STR_NETWORKNODE;
                nodeNN.Text = STR_NETWORKNODE;
                nodeNN.ImageIndex = 15;
                nodeNN.SelectedImageIndex = 15;
                TreeNodeEntireNetwork.Nodes.Add(nodeNN);
                //nodeEN.EnsureVisible();
                if (bCompleted)
                {
                    bCompleted = false;
                    bgw.RunWorkerAsync();
                }
            }

            if (ShowMyFavorites)
            {
                nodeMyFavorites = new TreeNode();
                nodeMyFavorites.Tag = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
                nodeMyFavorites.Text = STR_MYFAVORITES;
                nodeMyFavorites.ImageIndex = 26;
                nodeMyFavorites.SelectedImageIndex = 26;
                nodeDesktop.Nodes.Insert(3, nodeMyFavorites);
                FillFilesandDirs(nodeMyFavorites);
            }

            this.tvwMain.SelectedNode = nodeDesktop;
            nodeDesktop.Expand();
            isByUser = true;
        }

        public void setCurrentPath(string strPath)
        {
            SelectedPath = strPath;

            if (String.Compare(strPath, "home") == 0)
            {
                txtPath.Text = Application.StartupPath;
            }
            else
            {
                DirectoryInfo inf = new DirectoryInfo(strPath);
                if (inf.Exists)
                {
                    txtPath.Text = strPath;

                }
                else
                    txtPath.Text = Application.StartupPath;
            }


        }

        public void AboutExplorerTree()
        {
            frmOptions form = new frmOptions(showMyDocuments, showMyFavorites, showMyNetwork, showAddressbar, showToolbar);
            if (form.ShowDialog() == DialogResult.OK)
            {
                showMyDocuments = form.myDocument;
                showMyNetwork = form.myNetwork;
                ShowMyFavorites = form.myFavorite;
                ShowAddressbar = form.myAddressbar;
                ShowToolbar = form.myToolbar;

                btnRefresh_Click(this, null);
            }
        }
        #endregion

        private void tvwMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }
    }
}
