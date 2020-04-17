using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Main;
namespace BYHXPrinterManager.Preview
{
	/// <summary>
	/// PrintingJobInfo 的摘要说明。
	/// </summary>
	public class PrintingJobInfo : System.Windows.Forms.UserControl
	{
		private BYHXPrinterManager.GradientControls.CrystalPanel dividerPanel1;
		private BYHXPrinterManager.GradientControls.CrystalLabel crystalLabel1;
		private BYHXPrinterManager.GradientControls.CrystalLabel crystalLabel2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private DropDownPanel DropDownPanelStatus;
		private DropDownPanel dropDownPanelJobInfo;
		private DropDownPanel dropDownPanelErrorList;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintingJobInfo()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrintingJobInfo));
			this.DropDownPanelStatus = new BYHXPrinterManager.GradientControls.DropDownPanel();
			this.crystalLabel1 = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.dropDownPanelJobInfo = new BYHXPrinterManager.GradientControls.DropDownPanel();
			this.crystalLabel2 = new BYHXPrinterManager.GradientControls.CrystalLabel();
			this.dropDownPanelErrorList = new BYHXPrinterManager.GradientControls.DropDownPanel();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.dividerPanel1 = new BYHXPrinterManager.GradientControls.CrystalPanel();
			this.DropDownPanelStatus.SuspendLayout();
			this.dropDownPanelJobInfo.SuspendLayout();
			this.dropDownPanelErrorList.SuspendLayout();
			this.dividerPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// DropDownPanelStatus
			// 
			this.DropDownPanelStatus.AccessibleDescription = resources.GetString("DropDownPanelStatus.AccessibleDescription");
			this.DropDownPanelStatus.AccessibleName = resources.GetString("DropDownPanelStatus.AccessibleName");
			this.DropDownPanelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("DropDownPanelStatus.Anchor")));
			this.DropDownPanelStatus.AutoCollapseDelay = -1;
			this.DropDownPanelStatus.AutoScroll = ((bool)(resources.GetObject("DropDownPanelStatus.AutoScroll")));
			this.DropDownPanelStatus.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("DropDownPanelStatus.AutoScrollMargin")));
			this.DropDownPanelStatus.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("DropDownPanelStatus.AutoScrollMinSize")));
			this.DropDownPanelStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DropDownPanelStatus.BackgroundImage")));
			this.DropDownPanelStatus.Controls.Add(this.crystalLabel1);
			this.DropDownPanelStatus.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("DropDownPanelStatus.Dock")));
			this.DropDownPanelStatus.Enabled = ((bool)(resources.GetObject("DropDownPanelStatus.Enabled")));
			this.DropDownPanelStatus.EnableHeaderMenu = true;
			this.DropDownPanelStatus.ExpandAnimationSpeed = BYHXPrinterManager.GradientControls.AnimationSpeed.Medium;
			this.DropDownPanelStatus.Expanded = true;
			this.DropDownPanelStatus.Font = ((System.Drawing.Font)(resources.GetObject("DropDownPanelStatus.Font")));
			this.DropDownPanelStatus.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.DropDownPanelStatus.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.DropDownPanelStatus.HeaderGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.DropDownPanelStatus.HeaderHeight = 20;
			this.DropDownPanelStatus.HeaderIconNormal = null;
			this.DropDownPanelStatus.HeaderIconOver = null;
			this.DropDownPanelStatus.HeaderText = resources.GetString("DropDownPanelStatus.HeaderText");
			this.DropDownPanelStatus.HomeLocation = new System.Drawing.Point(0, 0);
			this.DropDownPanelStatus.HotTrackStyle = BYHXPrinterManager.GradientControls.HotTrackStyle.Both;
			this.DropDownPanelStatus.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("DropDownPanelStatus.ImeMode")));
			this.DropDownPanelStatus.Location = ((System.Drawing.Point)(resources.GetObject("DropDownPanelStatus.Location")));
			this.DropDownPanelStatus.ManageControls = false;
			this.DropDownPanelStatus.Moveable = false;
			this.DropDownPanelStatus.Name = "DropDownPanelStatus";
			this.DropDownPanelStatus.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("DropDownPanelStatus.RightToLeft")));
			this.DropDownPanelStatus.RoundedCorners = true;
			this.DropDownPanelStatus.Size = ((System.Drawing.Size)(resources.GetObject("DropDownPanelStatus.Size")));
			this.DropDownPanelStatus.TabIndex = ((int)(resources.GetObject("DropDownPanelStatus.TabIndex")));
			this.DropDownPanelStatus.Text = resources.GetString("DropDownPanelStatus.Text");
			this.DropDownPanelStatus.TransparentMode = true;
			this.DropDownPanelStatus.Visible = ((bool)(resources.GetObject("DropDownPanelStatus.Visible")));
			// 
			// crystalLabel1
			// 
			this.crystalLabel1.AccessibleDescription = resources.GetString("crystalLabel1.AccessibleDescription");
			this.crystalLabel1.AccessibleName = resources.GetString("crystalLabel1.AccessibleName");
			this.crystalLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("crystalLabel1.Anchor")));
			this.crystalLabel1.AutoScroll = ((bool)(resources.GetObject("crystalLabel1.AutoScroll")));
			this.crystalLabel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.AutoScrollMargin")));
			this.crystalLabel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.AutoScrollMinSize")));
			this.crystalLabel1.BackColor = System.Drawing.Color.White;
			this.crystalLabel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("crystalLabel1.BackgroundImage")));
			this.crystalLabel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("crystalLabel1.Dock")));
			this.crystalLabel1.Enabled = ((bool)(resources.GetObject("crystalLabel1.Enabled")));
			this.crystalLabel1.Font = ((System.Drawing.Font)(resources.GetObject("crystalLabel1.Font")));
			this.crystalLabel1.ForeColor = System.Drawing.Color.Black;
			this.crystalLabel1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.DarkOrange);
			this.crystalLabel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.crystalLabel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("crystalLabel1.ImeMode")));
			this.crystalLabel1.Location = ((System.Drawing.Point)(resources.GetObject("crystalLabel1.Location")));
			this.crystalLabel1.Name = "crystalLabel1";
			this.crystalLabel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("crystalLabel1.RightToLeft")));
			this.crystalLabel1.Size = ((System.Drawing.Size)(resources.GetObject("crystalLabel1.Size")));
			this.crystalLabel1.TabIndex = ((int)(resources.GetObject("crystalLabel1.TabIndex")));
			this.crystalLabel1.TextAlignment = System.Drawing.StringAlignment.Center;
			this.crystalLabel1.TreeColorGradient = true;
			this.crystalLabel1.Visible = ((bool)(resources.GetObject("crystalLabel1.Visible")));
			// 
			// dropDownPanelJobInfo
			// 
			this.dropDownPanelJobInfo.AccessibleDescription = resources.GetString("dropDownPanelJobInfo.AccessibleDescription");
			this.dropDownPanelJobInfo.AccessibleName = resources.GetString("dropDownPanelJobInfo.AccessibleName");
			this.dropDownPanelJobInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dropDownPanelJobInfo.Anchor")));
			this.dropDownPanelJobInfo.AutoCollapseDelay = -1;
			this.dropDownPanelJobInfo.AutoScroll = ((bool)(resources.GetObject("dropDownPanelJobInfo.AutoScroll")));
			this.dropDownPanelJobInfo.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("dropDownPanelJobInfo.AutoScrollMargin")));
			this.dropDownPanelJobInfo.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("dropDownPanelJobInfo.AutoScrollMinSize")));
			this.dropDownPanelJobInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dropDownPanelJobInfo.BackgroundImage")));
			this.dropDownPanelJobInfo.Controls.Add(this.crystalLabel2);
			this.dropDownPanelJobInfo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dropDownPanelJobInfo.Dock")));
			this.dropDownPanelJobInfo.Enabled = ((bool)(resources.GetObject("dropDownPanelJobInfo.Enabled")));
			this.dropDownPanelJobInfo.EnableHeaderMenu = true;
			this.dropDownPanelJobInfo.ExpandAnimationSpeed = BYHXPrinterManager.GradientControls.AnimationSpeed.Medium;
			this.dropDownPanelJobInfo.Expanded = true;
			this.dropDownPanelJobInfo.Font = ((System.Drawing.Font)(resources.GetObject("dropDownPanelJobInfo.Font")));
			this.dropDownPanelJobInfo.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dropDownPanelJobInfo.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dropDownPanelJobInfo.HeaderGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.dropDownPanelJobInfo.HeaderHeight = 20;
			this.dropDownPanelJobInfo.HeaderIconNormal = null;
			this.dropDownPanelJobInfo.HeaderIconOver = null;
			this.dropDownPanelJobInfo.HeaderText = resources.GetString("dropDownPanelJobInfo.HeaderText");
			this.dropDownPanelJobInfo.HomeLocation = new System.Drawing.Point(0, 96);
			this.dropDownPanelJobInfo.HotTrackStyle = BYHXPrinterManager.GradientControls.HotTrackStyle.Both;
			this.dropDownPanelJobInfo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dropDownPanelJobInfo.ImeMode")));
			this.dropDownPanelJobInfo.Location = ((System.Drawing.Point)(resources.GetObject("dropDownPanelJobInfo.Location")));
			this.dropDownPanelJobInfo.ManageControls = false;
			this.dropDownPanelJobInfo.Moveable = false;
			this.dropDownPanelJobInfo.Name = "dropDownPanelJobInfo";
			this.dropDownPanelJobInfo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dropDownPanelJobInfo.RightToLeft")));
			this.dropDownPanelJobInfo.RoundedCorners = true;
			this.dropDownPanelJobInfo.Size = ((System.Drawing.Size)(resources.GetObject("dropDownPanelJobInfo.Size")));
			this.dropDownPanelJobInfo.TabIndex = ((int)(resources.GetObject("dropDownPanelJobInfo.TabIndex")));
			this.dropDownPanelJobInfo.Text = resources.GetString("dropDownPanelJobInfo.Text");
			this.dropDownPanelJobInfo.TransparentMode = true;
			this.dropDownPanelJobInfo.Visible = ((bool)(resources.GetObject("dropDownPanelJobInfo.Visible")));
			// 
			// crystalLabel2
			// 
			this.crystalLabel2.AccessibleDescription = resources.GetString("crystalLabel2.AccessibleDescription");
			this.crystalLabel2.AccessibleName = resources.GetString("crystalLabel2.AccessibleName");
			this.crystalLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("crystalLabel2.Anchor")));
			this.crystalLabel2.AutoScroll = ((bool)(resources.GetObject("crystalLabel2.AutoScroll")));
			this.crystalLabel2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("crystalLabel2.AutoScrollMargin")));
			this.crystalLabel2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("crystalLabel2.AutoScrollMinSize")));
			this.crystalLabel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("crystalLabel2.BackgroundImage")));
			this.crystalLabel2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("crystalLabel2.Dock")));
			this.crystalLabel2.Enabled = ((bool)(resources.GetObject("crystalLabel2.Enabled")));
			this.crystalLabel2.Font = ((System.Drawing.Font)(resources.GetObject("crystalLabel2.Font")));
			this.crystalLabel2.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.crystalLabel2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.crystalLabel2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("crystalLabel2.ImeMode")));
			this.crystalLabel2.Location = ((System.Drawing.Point)(resources.GetObject("crystalLabel2.Location")));
			this.crystalLabel2.Name = "crystalLabel2";
			this.crystalLabel2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("crystalLabel2.RightToLeft")));
			this.crystalLabel2.Size = ((System.Drawing.Size)(resources.GetObject("crystalLabel2.Size")));
			this.crystalLabel2.TabIndex = ((int)(resources.GetObject("crystalLabel2.TabIndex")));
			this.crystalLabel2.TextAlignment = System.Drawing.StringAlignment.Center;
			this.crystalLabel2.Visible = ((bool)(resources.GetObject("crystalLabel2.Visible")));
			// 
			// dropDownPanelErrorList
			// 
			this.dropDownPanelErrorList.AccessibleDescription = resources.GetString("dropDownPanelErrorList.AccessibleDescription");
			this.dropDownPanelErrorList.AccessibleName = resources.GetString("dropDownPanelErrorList.AccessibleName");
			this.dropDownPanelErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dropDownPanelErrorList.Anchor")));
			this.dropDownPanelErrorList.AutoCollapseDelay = -1;
			this.dropDownPanelErrorList.AutoScroll = ((bool)(resources.GetObject("dropDownPanelErrorList.AutoScroll")));
			this.dropDownPanelErrorList.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("dropDownPanelErrorList.AutoScrollMargin")));
			this.dropDownPanelErrorList.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("dropDownPanelErrorList.AutoScrollMinSize")));
			this.dropDownPanelErrorList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dropDownPanelErrorList.BackgroundImage")));
			this.dropDownPanelErrorList.Controls.Add(this.richTextBox1);
			this.dropDownPanelErrorList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dropDownPanelErrorList.Dock")));
			this.dropDownPanelErrorList.Enabled = ((bool)(resources.GetObject("dropDownPanelErrorList.Enabled")));
			this.dropDownPanelErrorList.EnableHeaderMenu = true;
			this.dropDownPanelErrorList.ExpandAnimationSpeed = BYHXPrinterManager.GradientControls.AnimationSpeed.Medium;
			this.dropDownPanelErrorList.Expanded = true;
			this.dropDownPanelErrorList.Font = ((System.Drawing.Font)(resources.GetObject("dropDownPanelErrorList.Font")));
			this.dropDownPanelErrorList.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dropDownPanelErrorList.HeaderFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dropDownPanelErrorList.HeaderGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.dropDownPanelErrorList.HeaderHeight = 20;
			this.dropDownPanelErrorList.HeaderIconNormal = null;
			this.dropDownPanelErrorList.HeaderIconOver = null;
			this.dropDownPanelErrorList.HeaderText = resources.GetString("dropDownPanelErrorList.HeaderText");
			this.dropDownPanelErrorList.HomeLocation = new System.Drawing.Point(0, 420);
			this.dropDownPanelErrorList.HotTrackStyle = BYHXPrinterManager.GradientControls.HotTrackStyle.Both;
			this.dropDownPanelErrorList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dropDownPanelErrorList.ImeMode")));
			this.dropDownPanelErrorList.Location = ((System.Drawing.Point)(resources.GetObject("dropDownPanelErrorList.Location")));
			this.dropDownPanelErrorList.ManageControls = false;
			this.dropDownPanelErrorList.Moveable = false;
			this.dropDownPanelErrorList.Name = "dropDownPanelErrorList";
			this.dropDownPanelErrorList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dropDownPanelErrorList.RightToLeft")));
			this.dropDownPanelErrorList.RoundedCorners = true;
			this.dropDownPanelErrorList.Size = ((System.Drawing.Size)(resources.GetObject("dropDownPanelErrorList.Size")));
			this.dropDownPanelErrorList.TabIndex = ((int)(resources.GetObject("dropDownPanelErrorList.TabIndex")));
			this.dropDownPanelErrorList.Text = resources.GetString("dropDownPanelErrorList.Text");
			this.dropDownPanelErrorList.TransparentMode = true;
			this.dropDownPanelErrorList.Visible = ((bool)(resources.GetObject("dropDownPanelErrorList.Visible")));
			// 
			// richTextBox1
			// 
			this.richTextBox1.AccessibleDescription = resources.GetString("richTextBox1.AccessibleDescription");
			this.richTextBox1.AccessibleName = resources.GetString("richTextBox1.AccessibleName");
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("richTextBox1.Anchor")));
			this.richTextBox1.AutoSize = ((bool)(resources.GetObject("richTextBox1.AutoSize")));
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
			this.richTextBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("richTextBox1.BackgroundImage")));
			this.richTextBox1.BulletIndent = ((int)(resources.GetObject("richTextBox1.BulletIndent")));
			this.richTextBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("richTextBox1.Dock")));
			this.richTextBox1.Enabled = ((bool)(resources.GetObject("richTextBox1.Enabled")));
			this.richTextBox1.Font = ((System.Drawing.Font)(resources.GetObject("richTextBox1.Font")));
			this.richTextBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("richTextBox1.ImeMode")));
			this.richTextBox1.Location = ((System.Drawing.Point)(resources.GetObject("richTextBox1.Location")));
			this.richTextBox1.MaxLength = ((int)(resources.GetObject("richTextBox1.MaxLength")));
			this.richTextBox1.Multiline = ((bool)(resources.GetObject("richTextBox1.Multiline")));
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.RightMargin = ((int)(resources.GetObject("richTextBox1.RightMargin")));
			this.richTextBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("richTextBox1.RightToLeft")));
			this.richTextBox1.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("richTextBox1.ScrollBars")));
			this.richTextBox1.Size = ((System.Drawing.Size)(resources.GetObject("richTextBox1.Size")));
			this.richTextBox1.TabIndex = ((int)(resources.GetObject("richTextBox1.TabIndex")));
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			this.richTextBox1.Visible = ((bool)(resources.GetObject("richTextBox1.Visible")));
			this.richTextBox1.WordWrap = ((bool)(resources.GetObject("richTextBox1.WordWrap")));
			this.richTextBox1.ZoomFactor = ((System.Single)(resources.GetObject("richTextBox1.ZoomFactor")));
			// 
			// dividerPanel1
			// 
			this.dividerPanel1.AccessibleDescription = resources.GetString("dividerPanel1.AccessibleDescription");
			this.dividerPanel1.AccessibleName = resources.GetString("dividerPanel1.AccessibleName");
			this.dividerPanel1.AllowDrop = true;
			this.dividerPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("dividerPanel1.Anchor")));
			this.dividerPanel1.AutoScroll = ((bool)(resources.GetObject("dividerPanel1.AutoScroll")));
			this.dividerPanel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.AutoScrollMargin")));
			this.dividerPanel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.AutoScrollMinSize")));
			this.dividerPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dividerPanel1.BackgroundImage")));
			this.dividerPanel1.Controls.Add(this.dropDownPanelErrorList);
			this.dividerPanel1.Controls.Add(this.dropDownPanelJobInfo);
			this.dividerPanel1.Controls.Add(this.DropDownPanelStatus);
			this.dividerPanel1.Divider = false;
			this.dividerPanel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("dividerPanel1.Dock")));
			this.dividerPanel1.Enabled = ((bool)(resources.GetObject("dividerPanel1.Enabled")));
			this.dividerPanel1.Font = ((System.Drawing.Font)(resources.GetObject("dividerPanel1.Font")));
			this.dividerPanel1.ForeColor = System.Drawing.SystemColors.WindowText;
			this.dividerPanel1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.dividerPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.dividerPanel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("dividerPanel1.ImeMode")));
			this.dividerPanel1.Location = ((System.Drawing.Point)(resources.GetObject("dividerPanel1.Location")));
			this.dividerPanel1.Name = "dividerPanel1";
			this.dividerPanel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("dividerPanel1.RightToLeft")));
			this.dividerPanel1.Size = ((System.Drawing.Size)(resources.GetObject("dividerPanel1.Size")));
			this.dividerPanel1.TabIndex = ((int)(resources.GetObject("dividerPanel1.TabIndex")));
			this.dividerPanel1.TreeColorGradient = true;
			this.dividerPanel1.Visible = ((bool)(resources.GetObject("dividerPanel1.Visible")));
			// 
			// PrintingJobInfo
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.dividerPanel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "PrintingJobInfo";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Load += new System.EventHandler(this.PrintingJobInfo_Load);
			this.DropDownPanelStatus.ResumeLayout(false);
			this.dropDownPanelJobInfo.ResumeLayout(false);
			this.dropDownPanelErrorList.ResumeLayout(false);
			this.dividerPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		# region 属性
		private string m_status = String.Empty;
		public string StatusString
		{
			get{return m_status;}
			set
			{
				m_status = value;
				this.crystalLabel1.Text = m_status;}
		}

		private string m_JobInfo = String.Empty;
		public string JobInfoString
		{
			get{return m_JobInfo;}
			set
			{
				m_JobInfo = value;
				this.crystalLabel2.Text = m_JobInfo;}
		}
		#endregion

		#region 方法
//		public void printJobInfomation(int mSErrorCode, UserLevel Level)
//		{
//			if (Level != PrintInformation.getUserLevel())
//				return;
//			// Indent bulleted text 30 pixels away from the bullet.
//			richTextBox1.BulletIndent = 30;
//			// Specify that the following items are to be added to a bulleted list.
//			richTextBox1.SelectionBullet = true;
//			// Set the color of the item text.
//			if(SErrorCode.IsOnlyPauseError(mSErrorCode))
//				richTextBox1.SelectionColor = Color.Green;
//			else if(SErrorCode.IsWarningError(mSErrorCode))
//				richTextBox1.SelectionColor = Color.Purple;
//			else
//				richTextBox1.SelectionColor = Color.Red;
//			// Assign the text to the bulleted item.
//			this.richTextBox1.AppendText(SErrorCode.GetInfoFromErrCode(mSErrorCode) + "\n");
//			// End the bulleted list.
//			richTextBox1.SelectionBullet = false;
//			this.richTextBox1.Update();
//			this.richTextBox1.ScrollToCaret();
//		}
		#endregion

		private void PrintingJobInfo_Load(object sender, System.EventArgs e)
		{
//			this.DropDownPanelStatus.AddManagedControl(this.dropDownPanelJobInfo);
//			this.DropDownPanelStatus.AddManagedControl(this.dropDownPanelErrorList);
//			this.dropDownPanelJobInfo.AddManagedControl(this.dropDownPanelErrorList);
		}
	}
}
