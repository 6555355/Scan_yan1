using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FactoryWrite
{
	/// <summary>
	/// EpsonCleanForm 的摘要说明。
	/// </summary>
	public class EpsonCleanForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageCleanParameter1;
		private System.Windows.Forms.TabPage tabPageCleanParameter2;
		private BYHXPrinterManager.Setting.AllwinCleanControl allwinCleanControl1;
		private EpsonControlLibrary.Micolor_CleanParameterControl micolor_CleanParameterControl1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EpsonCleanForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EpsonCleanForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageCleanParameter1 = new System.Windows.Forms.TabPage();
			this.allwinCleanControl1 = new BYHXPrinterManager.Setting.AllwinCleanControl();
			this.tabPageCleanParameter2 = new System.Windows.Forms.TabPage();
			this.micolor_CleanParameterControl1 = new EpsonControlLibrary.Micolor_CleanParameterControl();
			this.tabControl1.SuspendLayout();
			this.tabPageCleanParameter1.SuspendLayout();
			this.tabPageCleanParameter2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.AccessibleDescription = resources.GetString("tabControl1.AccessibleDescription");
			this.tabControl1.AccessibleName = resources.GetString("tabControl1.AccessibleName");
			this.tabControl1.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("tabControl1.Alignment")));
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabControl1.Anchor")));
			this.tabControl1.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("tabControl1.Appearance")));
			this.tabControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabControl1.BackgroundImage")));
			this.tabControl1.Controls.Add(this.tabPageCleanParameter1);
			this.tabControl1.Controls.Add(this.tabPageCleanParameter2);
			this.tabControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabControl1.Dock")));
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
			// tabPageCleanParameter1
			// 
			this.tabPageCleanParameter1.AccessibleDescription = resources.GetString("tabPageCleanParameter1.AccessibleDescription");
			this.tabPageCleanParameter1.AccessibleName = resources.GetString("tabPageCleanParameter1.AccessibleName");
			this.tabPageCleanParameter1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPageCleanParameter1.Anchor")));
			this.tabPageCleanParameter1.AutoScroll = ((bool)(resources.GetObject("tabPageCleanParameter1.AutoScroll")));
			this.tabPageCleanParameter1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter1.AutoScrollMargin")));
			this.tabPageCleanParameter1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter1.AutoScrollMinSize")));
			this.tabPageCleanParameter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageCleanParameter1.BackgroundImage")));
			this.tabPageCleanParameter1.Controls.Add(this.allwinCleanControl1);
			this.tabPageCleanParameter1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPageCleanParameter1.Dock")));
			this.tabPageCleanParameter1.Enabled = ((bool)(resources.GetObject("tabPageCleanParameter1.Enabled")));
			this.tabPageCleanParameter1.Font = ((System.Drawing.Font)(resources.GetObject("tabPageCleanParameter1.Font")));
			this.tabPageCleanParameter1.ImageIndex = ((int)(resources.GetObject("tabPageCleanParameter1.ImageIndex")));
			this.tabPageCleanParameter1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPageCleanParameter1.ImeMode")));
			this.tabPageCleanParameter1.Location = ((System.Drawing.Point)(resources.GetObject("tabPageCleanParameter1.Location")));
			this.tabPageCleanParameter1.Name = "tabPageCleanParameter1";
			this.tabPageCleanParameter1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPageCleanParameter1.RightToLeft")));
			this.tabPageCleanParameter1.Size = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter1.Size")));
			this.tabPageCleanParameter1.TabIndex = ((int)(resources.GetObject("tabPageCleanParameter1.TabIndex")));
			this.tabPageCleanParameter1.Text = resources.GetString("tabPageCleanParameter1.Text");
			this.tabPageCleanParameter1.ToolTipText = resources.GetString("tabPageCleanParameter1.ToolTipText");
			this.tabPageCleanParameter1.Visible = ((bool)(resources.GetObject("tabPageCleanParameter1.Visible")));
			// 
			// allwinCleanControl1
			// 
			this.allwinCleanControl1.AccessibleDescription = resources.GetString("allwinCleanControl1.AccessibleDescription");
			this.allwinCleanControl1.AccessibleName = resources.GetString("allwinCleanControl1.AccessibleName");
			this.allwinCleanControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("allwinCleanControl1.Anchor")));
			this.allwinCleanControl1.AutoScroll = ((bool)(resources.GetObject("allwinCleanControl1.AutoScroll")));
			this.allwinCleanControl1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("allwinCleanControl1.AutoScrollMargin")));
			this.allwinCleanControl1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("allwinCleanControl1.AutoScrollMinSize")));
			this.allwinCleanControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("allwinCleanControl1.BackgroundImage")));
			this.allwinCleanControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("allwinCleanControl1.Dock")));
			this.allwinCleanControl1.Enabled = ((bool)(resources.GetObject("allwinCleanControl1.Enabled")));
			this.allwinCleanControl1.Font = ((System.Drawing.Font)(resources.GetObject("allwinCleanControl1.Font")));
			this.allwinCleanControl1.GrouperTitleStyle = null;
			this.allwinCleanControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("allwinCleanControl1.ImeMode")));
			this.allwinCleanControl1.Location = ((System.Drawing.Point)(resources.GetObject("allwinCleanControl1.Location")));
			this.allwinCleanControl1.Name = "allwinCleanControl1";
			this.allwinCleanControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("allwinCleanControl1.RightToLeft")));
			this.allwinCleanControl1.Size = ((System.Drawing.Size)(resources.GetObject("allwinCleanControl1.Size")));
			this.allwinCleanControl1.TabIndex = ((int)(resources.GetObject("allwinCleanControl1.TabIndex")));
			this.allwinCleanControl1.Visible = ((bool)(resources.GetObject("allwinCleanControl1.Visible")));
			// 
			// tabPageCleanParameter2
			// 
			this.tabPageCleanParameter2.AccessibleDescription = resources.GetString("tabPageCleanParameter2.AccessibleDescription");
			this.tabPageCleanParameter2.AccessibleName = resources.GetString("tabPageCleanParameter2.AccessibleName");
			this.tabPageCleanParameter2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tabPageCleanParameter2.Anchor")));
			this.tabPageCleanParameter2.AutoScroll = ((bool)(resources.GetObject("tabPageCleanParameter2.AutoScroll")));
			this.tabPageCleanParameter2.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter2.AutoScrollMargin")));
			this.tabPageCleanParameter2.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter2.AutoScrollMinSize")));
			this.tabPageCleanParameter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPageCleanParameter2.BackgroundImage")));
			this.tabPageCleanParameter2.Controls.Add(this.micolor_CleanParameterControl1);
			this.tabPageCleanParameter2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tabPageCleanParameter2.Dock")));
			this.tabPageCleanParameter2.Enabled = ((bool)(resources.GetObject("tabPageCleanParameter2.Enabled")));
			this.tabPageCleanParameter2.Font = ((System.Drawing.Font)(resources.GetObject("tabPageCleanParameter2.Font")));
			this.tabPageCleanParameter2.ImageIndex = ((int)(resources.GetObject("tabPageCleanParameter2.ImageIndex")));
			this.tabPageCleanParameter2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tabPageCleanParameter2.ImeMode")));
			this.tabPageCleanParameter2.Location = ((System.Drawing.Point)(resources.GetObject("tabPageCleanParameter2.Location")));
			this.tabPageCleanParameter2.Name = "tabPageCleanParameter2";
			this.tabPageCleanParameter2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tabPageCleanParameter2.RightToLeft")));
			this.tabPageCleanParameter2.Size = ((System.Drawing.Size)(resources.GetObject("tabPageCleanParameter2.Size")));
			this.tabPageCleanParameter2.TabIndex = ((int)(resources.GetObject("tabPageCleanParameter2.TabIndex")));
			this.tabPageCleanParameter2.Text = resources.GetString("tabPageCleanParameter2.Text");
			this.tabPageCleanParameter2.ToolTipText = resources.GetString("tabPageCleanParameter2.ToolTipText");
			this.tabPageCleanParameter2.Visible = ((bool)(resources.GetObject("tabPageCleanParameter2.Visible")));
			// 
			// micolor_CleanParameterControl1
			// 
			this.micolor_CleanParameterControl1.AccessibleDescription = resources.GetString("micolor_CleanParameterControl1.AccessibleDescription");
			this.micolor_CleanParameterControl1.AccessibleName = resources.GetString("micolor_CleanParameterControl1.AccessibleName");
			this.micolor_CleanParameterControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("micolor_CleanParameterControl1.Anchor")));
			this.micolor_CleanParameterControl1.AutoScroll = ((bool)(resources.GetObject("micolor_CleanParameterControl1.AutoScroll")));
			this.micolor_CleanParameterControl1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("micolor_CleanParameterControl1.AutoScrollMargin")));
			this.micolor_CleanParameterControl1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("micolor_CleanParameterControl1.AutoScrollMinSize")));
			this.micolor_CleanParameterControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("micolor_CleanParameterControl1.BackgroundImage")));
			this.micolor_CleanParameterControl1.CleanWay = BYHXPrinterManager.EpsonAutoCleanWay.Strong;
			this.micolor_CleanParameterControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("micolor_CleanParameterControl1.Dock")));
			this.micolor_CleanParameterControl1.Enabled = ((bool)(resources.GetObject("micolor_CleanParameterControl1.Enabled")));
			this.micolor_CleanParameterControl1.Font = ((System.Drawing.Font)(resources.GetObject("micolor_CleanParameterControl1.Font")));
			this.micolor_CleanParameterControl1.GrouperTitleStyle = null;
			this.micolor_CleanParameterControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("micolor_CleanParameterControl1.ImeMode")));
			this.micolor_CleanParameterControl1.Location = ((System.Drawing.Point)(resources.GetObject("micolor_CleanParameterControl1.Location")));
			this.micolor_CleanParameterControl1.Name = "micolor_CleanParameterControl1";
			this.micolor_CleanParameterControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("micolor_CleanParameterControl1.RightToLeft")));
			this.micolor_CleanParameterControl1.Size = ((System.Drawing.Size)(resources.GetObject("micolor_CleanParameterControl1.Size")));
			this.micolor_CleanParameterControl1.TabIndex = ((int)(resources.GetObject("micolor_CleanParameterControl1.TabIndex")));
			this.micolor_CleanParameterControl1.Visible = ((bool)(resources.GetObject("micolor_CleanParameterControl1.Visible")));
			// 
			// EpsonCleanForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.tabControl1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "EpsonCleanForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.tabControl1.ResumeLayout(false);
			this.tabPageCleanParameter1.ResumeLayout(false);
			this.tabPageCleanParameter2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
