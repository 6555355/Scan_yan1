namespace FileBrowser
{
    partial class Browser
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
            FileBrowser.ListViewColumnSorter listViewColumnSorter1 = new FileBrowser.ListViewColumnSorter();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.browseSplitter = new System.Windows.Forms.SplitContainer();
            this.folderView = new FileBrowser.BrowserTreeView();
            this.viewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.fileView = new FileBrowser.JThumbnailView();
            this.navigationBar = new System.Windows.Forms.ToolStrip();
            this.navBackButton = new System.Windows.Forms.ToolStripSplitButton();
            this.navForwardButton = new System.Windows.Forms.ToolStripSplitButton();
            this.navUpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.navFoldersButton = new System.Windows.Forms.ToolStripButton();
            this.navAddressLabel = new System.Windows.Forms.ToolStripLabel();
            this.navAddressBox = new FileBrowser.BrowserComboBox();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.FileViewSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.browseSplitter.Panel1.SuspendLayout();
            this.browseSplitter.Panel2.SuspendLayout();
            this.browseSplitter.SuspendLayout();
            this.viewSplitContainer.Panel2.SuspendLayout();
            this.viewSplitContainer.SuspendLayout();
            this.navigationBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.browseSplitter);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.navigationBar);
            // 
            // browseSplitter
            // 
            resources.ApplyResources(this.browseSplitter, "browseSplitter");
            this.browseSplitter.Name = "browseSplitter";
            // 
            // browseSplitter.Panel1
            // 
            this.browseSplitter.Panel1.Controls.Add(this.folderView);
            // 
            // browseSplitter.Panel2
            // 
            this.browseSplitter.Panel2.Controls.Add(this.viewSplitContainer);
            this.browseSplitter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseMove);
            this.browseSplitter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseDown);
            this.browseSplitter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitter_MouseUp);
            // 
            // folderView
            // 
            resources.ApplyResources(this.folderView, "folderView");
            this.folderView.Name = "folderView";
            this.folderView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.folderView_PreviewKeyDown);
            // 
            // viewSplitContainer
            // 
            resources.ApplyResources(this.viewSplitContainer, "viewSplitContainer");
            this.viewSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.viewSplitContainer.Name = "viewSplitContainer";
            this.viewSplitContainer.Panel1Collapsed = true;
            // 
            // viewSplitContainer.Panel2
            // 
            this.viewSplitContainer.Panel2.Controls.Add(this.fileView);
            // 
            // fileView
            // 
            this.fileView.CanLoad = true;
            this.fileView.ColumnHeaderContextMenu = null;
            this.fileView.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.fileView, "fileView");
            this.fileView.FolderName = null;
            listViewColumnSorter1.Order = System.Windows.Forms.SortOrder.None;
            listViewColumnSorter1.SortColumn = 0;
            this.fileView.ListViewColumnSorter = listViewColumnSorter1;
            this.fileView.Name = "fileView";
            this.fileView.OwnerDraw = true;
            this.fileView.SmallImageList = this.FileViewSmallImageList;
            this.fileView.SuspendHeaderContextMenu = false;
            this.fileView.TabStop = false;
            this.fileView.ThumbBorderColor = System.Drawing.Color.Wheat;
            this.fileView.ThumbNailSize = 95;
            this.fileView.UseCompatibleStateImageBehavior = false;
            this.fileView.SelectedIndexChanged += new System.EventHandler(this.fileView_SelectedIndexChanged);
            this.fileView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.fileView_PreviewKeyDown);
            // 
            // navigationBar
            // 
            this.navigationBar.CanOverflow = false;
            resources.ApplyResources(this.navigationBar, "navigationBar");
            this.navigationBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.navigationBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.navigationBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navBackButton,
            this.navForwardButton,
            this.navUpButton,
            this.toolStripSeparator1,
            this.navFoldersButton,
            this.navAddressLabel,
            this.navAddressBox,
            this.toolStripButtonRefresh});
            this.navigationBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.navigationBar.Name = "navigationBar";
            this.navigationBar.Stretch = true;
            this.navigationBar.Resize += new System.EventHandler(this.navigationBar_Resize);
            // 
            // navBackButton
            // 
            this.navBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.navBackButton, "navBackButton");
            this.navBackButton.Image = global::FileBrowser.Properties.Resources.BrowserBack;
            this.navBackButton.Name = "navBackButton";
            this.navBackButton.ButtonClick += new System.EventHandler(this.navBackForwardButton_ButtonClick);
            this.navBackButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.navBackForwardButton_DropDownItemClicked);
            // 
            // navForwardButton
            // 
            this.navForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.navForwardButton, "navForwardButton");
            this.navForwardButton.Image = global::FileBrowser.Properties.Resources.BrowserForward;
            this.navForwardButton.Name = "navForwardButton";
            this.navForwardButton.Click += new System.EventHandler(this.navBackForwardButton_ButtonClick);
            this.navForwardButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.navBackForwardButton_DropDownItemClicked);
            // 
            // navUpButton
            // 
            this.navUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.navUpButton, "navUpButton");
            this.navUpButton.Name = "navUpButton";
            this.navUpButton.Click += new System.EventHandler(this.navUpButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // navFoldersButton
            // 
            this.navFoldersButton.Checked = true;
            this.navFoldersButton.CheckOnClick = true;
            this.navFoldersButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.navFoldersButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.navFoldersButton, "navFoldersButton");
            this.navFoldersButton.Name = "navFoldersButton";
            this.navFoldersButton.CheckedChanged += new System.EventHandler(this.navFoldersButton_CheckedChanged);
            // 
            // navAddressLabel
            // 
            this.navAddressLabel.MergeIndex = 0;
            this.navAddressLabel.Name = "navAddressLabel";
            this.navAddressLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            resources.ApplyResources(this.navAddressLabel, "navAddressLabel");
            // 
            // navAddressBox
            // 
            this.navAddressBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.navAddressBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.navAddressBox.CurrentItem = null;
            resources.ApplyResources(this.navAddressBox, "navAddressBox");
            this.navAddressBox.MergeIndex = 0;
            this.navAddressBox.Name = "navAddressBox";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = global::FileBrowser.Properties.Resources.arrow_circle_double_135;
            resources.ApplyResources(this.toolStripButtonRefresh, "toolStripButtonRefresh");
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // FileViewSmallImageList
            // 
            this.FileViewSmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.FileViewSmallImageList, "FileViewSmallImageList");
            this.FileViewSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Browser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Browser";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.browseSplitter.Panel1.ResumeLayout(false);
            this.browseSplitter.Panel2.ResumeLayout(false);
            this.browseSplitter.ResumeLayout(false);
            this.viewSplitContainer.Panel2.ResumeLayout(false);
            this.viewSplitContainer.ResumeLayout(false);
            this.navigationBar.ResumeLayout(false);
            this.navigationBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip navigationBar;
        private System.Windows.Forms.ToolStripSplitButton navBackButton;
        private System.Windows.Forms.ToolStripSplitButton navForwardButton;
        private System.Windows.Forms.ToolStripButton navUpButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton navFoldersButton;
        private System.Windows.Forms.ToolStripLabel navAddressLabel;
        private BrowserComboBox navAddressBox;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer browseSplitter;
        private System.Windows.Forms.SplitContainer viewSplitContainer;
        private BrowserTreeView folderView;
        private JThumbnailView fileView;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ImageList FileViewSmallImageList;

    }
}
