/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/


#region References
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using BYHXPrinterManager;
#endregion

namespace BYHXControls
{
	/// <summary>
	/// Represents an extentable wizard control with basic page navigation functionality.
	/// </summary>
	[Designer(typeof(Wizard.WizardDesigner))]
	public class Wizard : System.Windows.Forms.UserControl
	{

		#region Consts
		private const int MarginGap = 8;
		private const int FOOTER_AREA_HEIGHT = 48;
		#endregion

		#region Fields
		private WizardPage selectedPage = null;
		private WizardPagesCollection pages = null;
		private Image headerImage = null;
		private Image welcomeImage = null;
		private Font headerFont = null;
		private Font headerTitleFont = null;
		private Font welcomeFont = null;
		private Font welcomeTitleFont = null;
		#endregion

		#region Designer generated code
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonBack;
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.Button buttonSave;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructor&Dispose
		/// <summary>
		/// Creates a new instance of the <see cref="Wizard"/> class.
		/// </summary>
		public Wizard()
		{
			// call required by designer
			this.InitializeComponent();

			// reset control style to improove rendering (reduce flicker)
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.UserPaint, true);

			// reset dock style
			base.Dock = DockStyle.Fill;

			// init pages collection
			this.pages = new WizardPagesCollection(this);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Wizard));
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonBack = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleDescription = resources.GetString("buttonCancel.AccessibleDescription");
			this.buttonCancel.AccessibleName = resources.GetString("buttonCancel.AccessibleName");
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonCancel.Anchor")));
			this.buttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancel.BackgroundImage")));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonCancel.Dock")));
			this.buttonCancel.Enabled = ((bool)(resources.GetObject("buttonCancel.Enabled")));
			this.buttonCancel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonCancel.FlatStyle")));
			this.buttonCancel.Font = ((System.Drawing.Font)(resources.GetObject("buttonCancel.Font")));
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonCancel.ImageAlign")));
			this.buttonCancel.ImageIndex = ((int)(resources.GetObject("buttonCancel.ImageIndex")));
			this.buttonCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonCancel.ImeMode")));
			this.buttonCancel.Location = ((System.Drawing.Point)(resources.GetObject("buttonCancel.Location")));
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonCancel.RightToLeft")));
			this.buttonCancel.Size = ((System.Drawing.Size)(resources.GetObject("buttonCancel.Size")));
			this.buttonCancel.TabIndex = ((int)(resources.GetObject("buttonCancel.TabIndex")));
			this.buttonCancel.Text = resources.GetString("buttonCancel.Text");
			this.buttonCancel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonCancel.TextAlign")));
			this.buttonCancel.Visible = ((bool)(resources.GetObject("buttonCancel.Visible")));
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
			this.buttonNext.Text = resources.GetString("buttonNext.Text");
			this.buttonNext.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonNext.TextAlign")));
			this.buttonNext.Visible = ((bool)(resources.GetObject("buttonNext.Visible")));
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonBack
			// 
			this.buttonBack.AccessibleDescription = resources.GetString("buttonBack.AccessibleDescription");
			this.buttonBack.AccessibleName = resources.GetString("buttonBack.AccessibleName");
			this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonBack.Anchor")));
			this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
			this.buttonBack.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonBack.Dock")));
			this.buttonBack.Enabled = ((bool)(resources.GetObject("buttonBack.Enabled")));
			this.buttonBack.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonBack.FlatStyle")));
			this.buttonBack.Font = ((System.Drawing.Font)(resources.GetObject("buttonBack.Font")));
			this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
			this.buttonBack.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonBack.ImageAlign")));
			this.buttonBack.ImageIndex = ((int)(resources.GetObject("buttonBack.ImageIndex")));
			this.buttonBack.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonBack.ImeMode")));
			this.buttonBack.Location = ((System.Drawing.Point)(resources.GetObject("buttonBack.Location")));
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonBack.RightToLeft")));
			this.buttonBack.Size = ((System.Drawing.Size)(resources.GetObject("buttonBack.Size")));
			this.buttonBack.TabIndex = ((int)(resources.GetObject("buttonBack.TabIndex")));
			this.buttonBack.Text = resources.GetString("buttonBack.Text");
			this.buttonBack.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonBack.TextAlign")));
			this.buttonBack.Visible = ((bool)(resources.GetObject("buttonBack.Visible")));
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// buttonHelp
			// 
			this.buttonHelp.AccessibleDescription = resources.GetString("buttonHelp.AccessibleDescription");
			this.buttonHelp.AccessibleName = resources.GetString("buttonHelp.AccessibleName");
			this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonHelp.Anchor")));
			this.buttonHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonHelp.BackgroundImage")));
			this.buttonHelp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonHelp.Dock")));
			this.buttonHelp.Enabled = ((bool)(resources.GetObject("buttonHelp.Enabled")));
			this.buttonHelp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonHelp.FlatStyle")));
			this.buttonHelp.Font = ((System.Drawing.Font)(resources.GetObject("buttonHelp.Font")));
			this.buttonHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonHelp.Image")));
			this.buttonHelp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonHelp.ImageAlign")));
			this.buttonHelp.ImageIndex = ((int)(resources.GetObject("buttonHelp.ImageIndex")));
			this.buttonHelp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonHelp.ImeMode")));
			this.buttonHelp.Location = ((System.Drawing.Point)(resources.GetObject("buttonHelp.Location")));
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonHelp.RightToLeft")));
			this.buttonHelp.Size = ((System.Drawing.Size)(resources.GetObject("buttonHelp.Size")));
			this.buttonHelp.TabIndex = ((int)(resources.GetObject("buttonHelp.TabIndex")));
			this.buttonHelp.Text = resources.GetString("buttonHelp.Text");
			this.buttonHelp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonHelp.TextAlign")));
			this.buttonHelp.Visible = ((bool)(resources.GetObject("buttonHelp.Visible")));
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.AccessibleDescription = resources.GetString("buttonSave.AccessibleDescription");
			this.buttonSave.AccessibleName = resources.GetString("buttonSave.AccessibleName");
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonSave.Anchor")));
			this.buttonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSave.BackgroundImage")));
			this.buttonSave.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonSave.Dock")));
			this.buttonSave.Enabled = ((bool)(resources.GetObject("buttonSave.Enabled")));
			this.buttonSave.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonSave.FlatStyle")));
			this.buttonSave.Font = ((System.Drawing.Font)(resources.GetObject("buttonSave.Font")));
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonSave.ImageAlign")));
			this.buttonSave.ImageIndex = ((int)(resources.GetObject("buttonSave.ImageIndex")));
			this.buttonSave.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonSave.ImeMode")));
			this.buttonSave.Location = ((System.Drawing.Point)(resources.GetObject("buttonSave.Location")));
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonSave.RightToLeft")));
			this.buttonSave.Size = ((System.Drawing.Size)(resources.GetObject("buttonSave.Size")));
			this.buttonSave.TabIndex = ((int)(resources.GetObject("buttonSave.TabIndex")));
			this.buttonSave.Text = resources.GetString("buttonSave.Text");
			this.buttonSave.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonSave.TextAlign")));
			this.buttonSave.Visible = ((bool)(resources.GetObject("buttonSave.Visible")));
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// Wizard
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.buttonSave);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "Wizard";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets which edge of the parent container a control is docked to.
		/// </summary>
		[DefaultValue(DockStyle.Fill)]
		[Category("Layout")]
		[Description("Gets or sets which edge of the parent container a control is docked to.")]
		public new DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>
		/// Gets the collection of wizard pages in this tab control.
		/// </summary>
		[Category("Wizard")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the collection of wizard pages in this tab control.")]
		public WizardPagesCollection Pages
		{
			get
			{
				return this.pages;
			}
		}

		/// <summary>
		/// Gets or sets the currently-selected wizard page.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WizardPage SelectedPage
		{
			get
			{
				return this.selectedPage;
			}
			set
			{
				// select new page
				this.ActivatePage(value);
			}
		}

		/// <summary>
		/// Gets or sets the currently-selected wizard page by index.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get
			{
				return this.pages.IndexOf(this.selectedPage);
			}
			set
			{
				// check if there are any pages
				if(this.pages.Count == 0)
				{
					// reset invalid index
					this.ActivatePage(-1);
					return;
				}

				// validate page index
				if (value < -1 || value >= this.pages.Count)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex",
														value,
														"The page index must be between 0 and " + Convert.ToString(this.pages.Count - 1));
				}

				// select new page
				this.ActivatePage(value);
			}
		}

		/// <summary>
		/// Gets or sets the image displayed on the header of the standard pages.
		/// </summary>
		[DefaultValue(null)]
		[Category("Wizard")]
		[Description("Gets or sets the image displayed on the header of the standard pages.")]
		public Image HeaderImage
		{
			get
			{
				return this.headerImage;
			}
			set
			{
				if (this.headerImage != value)
				{
					this.headerImage = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the image displayed on the welcome and finish pages.
		/// </summary>
		[DefaultValue(null)]
		[Category("Wizard")]
		[Description("Gets or sets the image displayed on the welcome and finish pages.")]
		public Image WelcomeImage
		{
			get
			{
				return this.welcomeImage;
			}
			set
			{
				if (this.welcomeImage != value)
				{
					this.welcomeImage = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the font used to display the description of a standard page.
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the font used to display the description of a standard page.")]
		public Font HeaderFont
		{
			get
			{
				if (this.headerFont == null)
				{
					return this.Font;
				}
				else
				{
					return this.headerFont;
				}
			}
			set
			{
				if (this.headerFont != value)
				{
					this.headerFont = value;
					this.Invalidate();
				}
			}
		}
		protected bool ShouldSerializeHeaderFont()
		{
			return this.headerFont != null;
		}

		/// <summary>
		/// Gets or sets the font used to display the title of a standard page.
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the font used to display the title of a standard page.")]
		public Font HeaderTitleFont
		{
			get
			{
				if (this.headerTitleFont == null)
				{
					return new Font(this.Font.FontFamily, this.Font.Size + 2, FontStyle.Bold);
				}
				else
				{
					return this.headerTitleFont;
				}
			}
			set
			{
				if (this.headerTitleFont != value)
				{
					this.headerTitleFont = value;
					this.Invalidate();
				}
			}
		}
		protected bool ShouldSerializeHeaderTitleFont()
		{
			return this.headerTitleFont != null;
		}

		/// <summary>
		/// Gets or sets the font used to display the description of a welcome of finish page.
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the font used to display the description of a welcome of finish page.")]
		public Font WelcomeFont
		{
			get
			{
				if (this.welcomeFont == null)
				{
					return this.Font;
				}
				else
				{
					return this.welcomeFont;
				}
			}
			set
			{
				if (this.welcomeFont != value)
				{
					this.welcomeFont = value;
					this.Invalidate();
				}
			}
		}
		protected bool ShouldSerializeWelcomeFont()
		{
			return this.welcomeFont != null;
		}

		/// <summary>
		/// Gets or sets the font used to display the title of a welcome of finish page.
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the font used to display the title of a welcome of finish page.")]
		public Font WelcomeTitleFont
		{
			get
			{
				if (this.welcomeTitleFont == null)
				{
					return new Font(this.Font.FontFamily, this.Font.Size + 10, FontStyle.Bold);
				}
				else
				{
					return this.welcomeTitleFont;
				}
			}
			set
			{
				if (this.welcomeTitleFont != value)
				{
					this.welcomeTitleFont = value;
					this.Invalidate();
				}
			}
		}
		protected bool ShouldSerializeWelcomeTitleFont()
		{
			return this.welcomeTitleFont != null;
		}

		/// <summary>
		/// Gets or sets the enabled state of the Next button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool NextEnabled
		{
			get
			{
				return this.buttonNext.Enabled;
			}
			set
			{
				this.buttonNext.Enabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the enabled state of the back button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool BackEnabled
		{
			get
			{
				return this.buttonBack.Enabled;
			}
			set
			{
				this.buttonBack.Enabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the enabled state of the cancel button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CancelEnabled
		{
			get
			{
				return this.buttonCancel.Enabled;
			}
			set
			{
		
				this.buttonCancel.Enabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the visible state of the help button. 
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets the visible state of the help button. ")]
		public bool HelpVisible
		{
			get
			{
				return this.buttonHelp.Visible;
			}
			set
			{
		
				this.buttonHelp.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets the visible state of the help button. 
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets the visible state of the help button. ")]
		public bool HelpEnabled
		{
			get
			{
				return this.buttonHelp.Enabled;
			}
			set
			{
		
				this.buttonHelp.Enabled = value;
			}
		}
		/// <summary>
		/// Gets or sets the visible state of the help button. 
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets the visible state of the save button. ")]
		public bool SaveVisible
		{
			get
			{
				return this.buttonSave.Visible;
			}
			set
			{
		
				this.buttonSave.Visible = value;
			}
		}


		/// <summary>
		/// Gets or sets the text displayed by the Next button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string NextText
		{
			get
			{
				return this.buttonNext.Text;
			}
			set
			{
				this.buttonNext.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the text displayed by the back button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string BackText
		{
			get
			{
				return this.buttonBack.Text;
			}
			set
			{
				this.buttonBack.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the text displayed by the cancel button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CancelText
		{
			get
			{
				return this.buttonCancel.Text;
			}
			set
			{
		
				this.buttonCancel.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the text displayed by the cancel button. 
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string HelpText
		{
			get
			{
				return this.buttonHelp.Text;
			}
			set
			{
		
				this.buttonHelp.Text = value;
			}
		}

		#endregion

		#region Methods
		/// <summary>
		/// Swithes forward to next wizard page.
		/// </summary>
		public void Next()
		{
			// check if we're on the last page (finish)
			if (this.SelectedIndex == this.pages.Count - 1)
			{
				this.buttonNext.Enabled = false;
			}
			else
			{
				// handle page switch
				this.OnBeforeSwitchPages(new BeforeSwitchPagesEventArgs(this.SelectedIndex, this.SelectedIndex + 1));
			}
		}

		/// <summary>
		/// Swithes backward to previous wizard page.
		/// </summary>
		public void Back()
		{
			if (this.SelectedIndex == 0)
			{
				this.buttonBack.Enabled = false;
			}
			else
			{
				// handle page switch
				this.OnBeforeSwitchPages(new BeforeSwitchPagesEventArgs(this.SelectedIndex, this.SelectedIndex - 1));
			}
		}

        /// <summary>
        /// 直接跳转到序列号的页面
        /// </summary>
        public void ToPage(int index) {
            if (index >= this.pages.Count || index < 0) { return; }
            else if (this.SelectedIndex == this.pages.Count - 1)
            {
                this.buttonNext.Enabled = false;
            }
            else if (this.SelectedIndex == 0)
            {
                this.buttonBack.Enabled = false;
            }
            {
                this.OnBeforeSwitchPages(new BeforeSwitchPagesEventArgs(this.SelectedIndex, index));
            }
        }

		/// <summary>
		/// Activates the specified wizard bage.
		/// </summary>
		/// <param name="index">An Integer value representing the zero-based index of the page to be activated.</param>
		private void ActivatePage(int index)
		{
			// check if new page is invalid
			if (index < 0 || index >= this.pages.Count)
			{
				// filter out
				return;
			}
		
			// get new page
			WizardPage page = (WizardPage)this.pages[index];

			// activate page
			this.ActivatePage(page);
		}
		static private string GetResString(string name)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Wizard));
			return resources.GetString(name);
		}

		/// <summary>
		/// Activates the specified wizard bage.
		/// </summary>
		/// <param name="page">A WizardPage object representing the page to be activated.</param>
		private void ActivatePage(WizardPage page)
		{
			// validate given page
			if (this.pages.Contains(page) == false)
			{
				// filter out
				return;
			}

			// deactivate current page
			if (this.selectedPage != null)
			{
				this.selectedPage.Visible = false;
			}

			// activate new page
			this.selectedPage = page;

			if (this.selectedPage != null)
			{
				//Ensure that this panel displays inside the wizard
				this.selectedPage.Parent = this;
				if (this.Contains(this.selectedPage) == false)
				{	
					this.Container.Add(this.selectedPage);
				}
//				if (this.selectedPage.Style == WizardPageStyle.Finish)
				{
					this.buttonCancel.Text = ResString.GetResString("buttonFinish_Text");//"OK";
					this.buttonCancel.DialogResult = DialogResult.OK;
				}
//				else
//				{
//					this.buttonCancel.Text = ResString.GetResString("buttonCancel_Text");//"Cancel";
//					this.buttonCancel.DialogResult = DialogResult.Cancel;
//				}

				//Make it fill the space
				this.selectedPage.SetBounds(0, 0, this.Width, this.Height - FOOTER_AREA_HEIGHT);
				this.selectedPage.Visible = true;
				this.selectedPage.BringToFront();
				this.FocusFirstTabIndex(this.selectedPage);
			}
			
			//What should the back button say
			if (this.SelectedIndex > 0)
			{
				buttonBack.Enabled = true;
			}
			else
			{
				buttonBack.Enabled = false;
			}

			//What should the Next button say
			if (this.SelectedIndex < this.pages.Count - 1)
			{
				this.buttonNext.Enabled = true;
			}
			else
			{
				if (this.DesignMode == false)
				{
					// at runtime disable back button (we finished; there's no point going back)
					//buttonBack.Enabled = false;
				}
				this.buttonNext.Enabled = false;
			}
			
			// refresh
			if (this.selectedPage != null)
			{
				this.selectedPage.Invalidate();
			}
			else
			{
				this.Invalidate();
			}
		}

		/// <summary>
		/// Focus the control with a lowest tab index in the given container.
		/// </summary>
		/// <param name="container">A Control object to pe processed.</param>
		private void FocusFirstTabIndex(Control container)
		{
			// init search result varialble
			Control searchResult = null;

			// find the control with the lowest tab index
			foreach (Control control in container.Controls)
			{
				if (control.CanFocus && (searchResult == null || control.TabIndex < searchResult.TabIndex))
				{
					searchResult = control;
				}
			}

			// check if anything searchResult
			if (searchResult != null)
			{
				// focus found control
				searchResult.Focus();
			}
			else
			{
				// focus the container
				container.Focus();
			}
		}

		/// <summary>
		/// Raises the SwitchPages event.
		/// </summary>
		/// <param name="e">A WizardPageEventArgs object that holds event data.</param>
		protected virtual void OnBeforeSwitchPages(BeforeSwitchPagesEventArgs e)
		{
			// check if there are subscribers
			if (this.BeforeSwitchPages != null)
			{
				// raise BeforeSwitchPages event
				this.BeforeSwitchPages(this, e);
			}

			// check if user canceled
			if (e.Cancel)
			{
				// filter
				return;
			}

			// activate new page
			this.ActivatePage(e.NewIndex);

			// raise the after event
			this.OnAfterSwitchPages(e as AfterSwitchPagesEventArgs);
		}

		/// <summary>
		/// Raises the SwitchPages event.
		/// </summary>
		/// <param name="e">A WizardPageEventArgs object that holds event data.</param>
		protected virtual void OnAfterSwitchPages(AfterSwitchPagesEventArgs e)
		{
			// check if there are subscribers
			if (this.AfterSwitchPages != null)
			{
				// raise AfterSwitchPages event
				this.AfterSwitchPages(this, e);
			}
		}

		/// <summary>
		/// Raises the Cancel event.
		/// </summary>
		/// <param name="e">A CancelEventArgs object that holds event data.</param>
		protected virtual void OnCancel(CancelEventArgs e)
		{
			// check if there are subscribers
			if (this.Cancel != null)
			{
				// raise Cancel event
				this.Cancel(this, e);
			}

			// check if user canceled
			if (e.Cancel)
			{
				// cancel closing (when ShowDialog is used)
				this.ParentForm.DialogResult = DialogResult.None;
			}
			else
			{
				// ensure parent form is closed (even when ShowDialog is not used)
				this.ParentForm.Close();
			}
		}

		/// <summary>
		/// Raises the Finish event.
		/// </summary>
		/// <param name="e">A EventArgs object that holds event data.</param>
		protected virtual void OnFinish(EventArgs e)
		{
			// check if there are subscribers
			if (this.Finish != null)
			{
				// raise Finish event
				this.Finish(this, e);
			}

			// ensure parent form is closed (even when ShowDialog is not used)
			this.ParentForm.Close();
		}

		/// <summary>
		/// Raises the Help event.
		/// </summary>
		/// <param name="e">A EventArgs object that holds event data.</param>
		protected virtual void OnHelp(EventArgs e)
		{
			// check if there are subscribers
			if (this.Help != null)
			{
				// raise Help event
				this.Help(this, e);
			}
		}
		protected virtual void OnSave(EventArgs e)
		{
			// check if there are subscribers
			if (this.Save != null)
			{
				// raise Help event
				this.Save(this, e);
			}
		}

		/// <summary>
		/// Raises the Load event.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			// raise the Load event
			base.OnLoad(e);
			
			// activate first page, if exists
			if (this.pages.Count > 0)
			{
				this.ActivatePage(0);
			}
		}

		/// <summary>
		/// Raises the Resize event.
		/// </summary>
		protected override void OnResize(EventArgs e)
		{
			// raise the Resize event
			base.OnResize(e);

			// resize the selected page to fit the wizard
			if (this.selectedPage != null)
			{
				this.selectedPage.SetBounds(0, 0, this.Width, this.Height - FOOTER_AREA_HEIGHT);
			}

			// position navigation buttons
			this.buttonCancel.Location = new Point(this.Width - MarginGap - this.buttonCancel.Width,
													this.Height - MarginGap - this.buttonCancel.Height);
			this.buttonNext.Location = new Point(this.buttonCancel.Location.X - MarginGap - this.buttonNext.Width,
												this.buttonCancel.Location.Y);
			this.buttonBack.Location = new Point(this.buttonNext.Location.X - MarginGap - this.buttonBack.Width,
												this.buttonCancel.Location.Y);
			this.buttonHelp.Location = new Point(this.buttonHelp.Location.X,
												this.buttonCancel.Location.Y);
			this.buttonSave.Location = new Point(this.buttonHelp.Location.X + MarginGap + this.buttonHelp.Width,
				this.buttonCancel.Location.Y);

		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{
			// raise the Paint event
			base.OnPaint(e);
			Rectangle bottomRect = this.ClientRectangle;
			bottomRect.Y = this.Height - FOOTER_AREA_HEIGHT;
			bottomRect.Height = FOOTER_AREA_HEIGHT;
			ControlPaint.DrawBorder3D(e.Graphics, bottomRect, Border3DStyle.Etched, Border3DSide.Top);
		}

		/// <summary>
		/// Raises the ControlAdded event.
		/// </summary>
		protected override void OnControlAdded(ControlEventArgs e) 
		{
			// prevent other controls from being added directly to the wizard
			if (e.Control is WizardPage == false &&
				e.Control != this.buttonCancel &&
				e.Control != this.buttonNext &&
				e.Control != this.buttonBack)
			{
				// add the control to the selected page
				if (this.selectedPage != null)
				{
					this.selectedPage.Controls.Add(e.Control);
				}
			}
			else
			{
				// raise the ControlAdded event
				base.OnControlAdded(e);
			}
		}

		#endregion

		#region Events
		/// <summary>
		/// Occurs before the wizard pages are switched, giving the user a chance to validate.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs before the wizard pages are switched, giving the user a chance to validate.")]
		public event BeforeSwitchPagesEventHandler BeforeSwitchPages;
		/// <summary>
		/// Occurs after the wizard pages are switched, giving the user a chance to setup the new page.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs after the wizard pages are switched, giving the user a chance to setup the new page.")]
		public event AfterSwitchPagesEventHandler AfterSwitchPages;
		/// <summary>
		/// Occurs when wizard is canceled, giving the user a chance to validate.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs when wizard is canceled, giving the user a chance to validate.")]
		public event CancelEventHandler Cancel;
		/// <summary>
		/// Occurs when wizard is finished, giving the user a chance to do extra stuff.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs when wizard is finished, giving the user a chance to do extra stuff.")]
		public event EventHandler Finish;
		/// <summary>
		/// Occurs when the user clicks the help button.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs when the user clicks the help button.")]
		public event EventHandler Help;
		/// <summary>
		/// Occurs when the user clicks the help button.
		/// </summary>
		[Category("Wizard")]
		[Description("Occurs when the user clicks the save button.")]
		public event EventHandler Save;

		/// <summary>
		/// Represents the method that will handle the BeforeSwitchPages event of the Wizard control.
		/// </summary>
		public delegate void BeforeSwitchPagesEventHandler(object sender, BeforeSwitchPagesEventArgs e);
		/// <summary>
		/// Represents the method that will handle the AfterSwitchPages event of the Wizard control.
		/// </summary>
		public delegate void AfterSwitchPagesEventHandler(object sender, AfterSwitchPagesEventArgs e);
		#endregion

		#region Events handlers
		/// <summary>
		/// Handles the Click event of buttonNext.
		/// </summary>
		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			this.Next();
		}

		/// <summary>
		/// Handles the Click event of buttonBack.
		/// </summary>
		private void buttonBack_Click(object sender, System.EventArgs e)
		{
			this.Back();
		}
		
		/// <summary>
		/// Handles the Click event of buttonCancel.
		/// </summary>
		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			// check if button is cancel mode
			if (this.buttonCancel.DialogResult == DialogResult.Cancel)
			{
				this.OnCancel(new CancelEventArgs());
			}
			// check if button is finish mode
			else if (this.buttonCancel.DialogResult == DialogResult.OK)
			{
				this.OnFinish(EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Handles the Click event of buttonHelp.
		/// </summary>
        private void buttonHelp_Click(object sender, System.EventArgs e)
        {
			this.OnHelp(EventArgs.Empty);
        }
		#endregion

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			this.OnSave(EventArgs.Empty);
		}

		#region Inner classes
		/// <summary>
		/// Represents a designer for the wizard control.
		/// </summary>
		internal class WizardDesigner : ParentControlDesigner
		{

			#region Methods
			/// <summary>
			/// Overrides the handling of Mouse clicks to allow back-next to work in the designer.
			/// </summary>
			/// <param name="msg">A Message value.</param>
			protected override void WndProc(ref Message msg)
			{
				// declare PInvoke constants
				const int WM_LBUTTONDOWN = 0x0201;
				const int WM_LBUTTONDBLCLK = 0x0203;

				// check message
				if (msg.Msg == WM_LBUTTONDOWN || msg.Msg == WM_LBUTTONDBLCLK)
				{
					// get the control under the mouse
					ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
					
					if (ss.PrimarySelection is Wizard)
					{
						Wizard wizard = (Wizard)ss.PrimarySelection;

						// extract the mouse position
						int xPos = (short)((uint)msg.LParam & 0x0000FFFF);
						int yPos = (short)(((uint)msg.LParam & 0xFFFF0000) >> 16);
						Point mousePos =  new Point(xPos, yPos);
						
						if (msg.HWnd == wizard.buttonNext.Handle)
						{
							if (wizard.buttonNext.Enabled && 
								wizard.buttonNext.ClientRectangle.Contains(mousePos))
							{
								//Press the button
								wizard.Next();
							}
						}
						else if (msg.HWnd == wizard.buttonBack.Handle)
						{
							if (wizard.buttonBack.Enabled && 
								wizard.buttonBack.ClientRectangle.Contains(mousePos))
							{
								//Press the button
								wizard.Back();
							}
						}
						
						// filter message
						return;
					}
				}

				// forward message
				base.WndProc(ref msg);
			}

			/// <summary>
			/// Prevents the grid from being drawn on the Wizard.
			/// </summary>
			protected override bool DrawGrid
			{
				get
				{
					return false;
				}
			}
			#endregion

		}

		/// <summary>
		/// Provides data for the AfterSwitchPages event of the Wizard control.
		/// </summary>
		public class AfterSwitchPagesEventArgs : EventArgs
		{

			#region Fields
			private int oldIndex;
			protected int newIndex;
			#endregion

			#region Constructor
			/// <summary>
			/// Creates a new instance of the <see cref="WizardPageEventArgs"/> class.
			/// </summary>
			/// <param name="oldIndex">An integer value representing the index of the old page.</param>
			/// <param name="newIndex">An integer value representing the index of the new page.</param>
			internal AfterSwitchPagesEventArgs(int oldIndex, int newIndex)
			{
				this.oldIndex = oldIndex;
				this.newIndex = newIndex;
			}

			#endregion

			#region Properties
			/// <summary>
			/// Gets the index of the old page.
			/// </summary>
			public int OldIndex
			{
				get
				{
					return this.oldIndex;
				}
			}

			/// <summary>
			/// Gets or sets the index of the new page.
			/// </summary>
			public int NewIndex
			{
				get
				{
					return this.newIndex;
				}
			}
			#endregion

		}

		/// <summary>
		/// Provides data for the BeforeSwitchPages event of the Wizard control.
		/// </summary>
		public class BeforeSwitchPagesEventArgs : AfterSwitchPagesEventArgs
		{

			#region Fields
			private bool cancel = false;
			#endregion

			#region Constructor
			/// <summary>
			/// Creates a new instance of the <see cref="WizardPageEventArgs"/> class.
			/// </summary>
			/// <param name="oldIndex">An integer value representing the index of the old page.</param>
			/// <param name="newIndex">An integer value representing the index of the new page.</param>
			internal BeforeSwitchPagesEventArgs(int oldIndex, int newIndex) : base(oldIndex, newIndex)
			{
				// nothing
			}

			#endregion

			#region Properties
			/// <summary>
			/// Indicates whether the page switch should be canceled.
			/// </summary>
			public bool Cancel
			{
				get
				{
					return this.cancel;
				}
				set
				{
					this.cancel = value;
				}
			}

			/// <summary>
			/// Gets or sets the index of the new page.
			/// </summary>
			public new int NewIndex
			{
				get
				{
					return base.newIndex;
				}
				set
				{
					base.newIndex = value;
				}
			}
			#endregion


		}
		#endregion

	}
}
