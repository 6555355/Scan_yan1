using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BYHXPrinterManager.Setting
{
	public class AllwinCleanControl : BYHXPrinterManager.Setting.BYHXUserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBoxCleanMode;
		private System.Windows.Forms.NumericUpDown numericUpDownSuckTimes;
		private System.Windows.Forms.NumericUpDown numericUpDownCarriage_X_SuckPos;
		private System.Windows.Forms.NumericUpDown numericUpDownHeadBox_Z_SuckPos;
		private System.Windows.Forms.NumericUpDown numericUpDownSuckInkTime;
		private System.Windows.Forms.NumericUpDown numericUpDownInputAirTime;
		private System.Windows.Forms.NumericUpDown numericUpDownSuckWasteInkTime;
		private System.Windows.Forms.NumericUpDown numericUpDownWipeTimes;
		private System.Windows.Forms.NumericUpDown numericUpDownHeadBox_Z_WipePos;
		private System.Windows.Forms.NumericUpDown numericUpDownCarriage_X_Wipe_Speed;
		private System.Windows.Forms.NumericUpDown numWiperPos_Y0;
		private System.Windows.Forms.NumericUpDown numWiperPos_Y1;
		private System.Windows.Forms.NumericUpDown numWiperPos_Y2;
		private System.Windows.Forms.NumericUpDown numWiperPos_Y3;
		private System.Windows.Forms.NumericUpDown numXWipePosStart3;
		private System.Windows.Forms.NumericUpDown numXWipePosStart2;
		private System.Windows.Forms.NumericUpDown numXWipePosStart1;
		private System.Windows.Forms.NumericUpDown numXWipePosStart0;
		private System.Windows.Forms.NumericUpDown numXWipePosEnd3;
		private System.Windows.Forms.NumericUpDown numXWipePosEnd2;
		private System.Windows.Forms.NumericUpDown numXWipePosEnd1;
		private System.Windows.Forms.NumericUpDown numXWipePosEnd0;
		private System.ComponentModel.IContainer components = null;

		private AllwinCleanParameter mCleanParameter;
		private NumericUpDown[] WiperPos_Y_UpDowns;
		private NumericUpDown[] X_WipePos_Start_UpDowns;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.Button buttonRead;
		private NumericUpDown[] X_WipePos_End_UpDowns;

		public AllwinCleanControl()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			PubFunc.SetNumricMaxAndMin(this, false);
			this.comboBoxCleanMode.Items.Clear();
			foreach (EpsonAutoCleanWay val in Enum.GetValues(typeof(EpsonAutoCleanWay)))
			{
				string txt = ResString.GetEnumDisplayName(val.GetType(),val);
				this.comboBoxCleanMode.Items.Add(txt);
			}
			this.comboBoxCleanMode.SelectedIndex = 0;

			WiperPos_Y_UpDowns = new NumericUpDown[4]{numWiperPos_Y0,numWiperPos_Y1,numWiperPos_Y2,numWiperPos_Y3};
			X_WipePos_Start_UpDowns = new NumericUpDown[4]{numXWipePosStart0,numXWipePosStart1,numXWipePosStart2,numXWipePosStart3};
			X_WipePos_End_UpDowns = new NumericUpDown[4]{numXWipePosEnd0,numXWipePosEnd1,numXWipePosEnd2,numXWipePosEnd3};
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AllwinCleanControl));
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxCleanMode = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDownSuckWasteInkTime = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownInputAirTime = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownSuckInkTime = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDownHeadBox_Z_SuckPos = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDownCarriage_X_SuckPos = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownSuckTimes = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWipeTimes = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.numXWipePosEnd3 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosEnd2 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosEnd1 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosEnd0 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosStart3 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosStart2 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosStart1 = new System.Windows.Forms.NumericUpDown();
			this.numXWipePosStart0 = new System.Windows.Forms.NumericUpDown();
			this.numWiperPos_Y3 = new System.Windows.Forms.NumericUpDown();
			this.numWiperPos_Y2 = new System.Windows.Forms.NumericUpDown();
			this.numWiperPos_Y1 = new System.Windows.Forms.NumericUpDown();
			this.numWiperPos_Y0 = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.numericUpDownCarriage_X_Wipe_Speed = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.numericUpDownHeadBox_Z_WipePos = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonRead = new System.Windows.Forms.Button();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckWasteInkTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInputAirTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckInkTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBox_Z_SuckPos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCarriage_X_SuckPos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckTimes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWipeTimes)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCarriage_X_Wipe_Speed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBox_Z_WipePos)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// comboBoxCleanMode
			// 
			this.comboBoxCleanMode.AccessibleDescription = resources.GetString("comboBoxCleanMode.AccessibleDescription");
			this.comboBoxCleanMode.AccessibleName = resources.GetString("comboBoxCleanMode.AccessibleName");
			this.comboBoxCleanMode.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("comboBoxCleanMode.Anchor")));
			this.comboBoxCleanMode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("comboBoxCleanMode.BackgroundImage")));
			this.comboBoxCleanMode.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("comboBoxCleanMode.Dock")));
			this.comboBoxCleanMode.Enabled = ((bool)(resources.GetObject("comboBoxCleanMode.Enabled")));
			this.comboBoxCleanMode.Font = ((System.Drawing.Font)(resources.GetObject("comboBoxCleanMode.Font")));
			this.comboBoxCleanMode.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("comboBoxCleanMode.ImeMode")));
			this.comboBoxCleanMode.IntegralHeight = ((bool)(resources.GetObject("comboBoxCleanMode.IntegralHeight")));
			this.comboBoxCleanMode.ItemHeight = ((int)(resources.GetObject("comboBoxCleanMode.ItemHeight")));
			this.comboBoxCleanMode.Location = ((System.Drawing.Point)(resources.GetObject("comboBoxCleanMode.Location")));
			this.comboBoxCleanMode.MaxDropDownItems = ((int)(resources.GetObject("comboBoxCleanMode.MaxDropDownItems")));
			this.comboBoxCleanMode.MaxLength = ((int)(resources.GetObject("comboBoxCleanMode.MaxLength")));
			this.comboBoxCleanMode.Name = "comboBoxCleanMode";
			this.comboBoxCleanMode.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("comboBoxCleanMode.RightToLeft")));
			this.comboBoxCleanMode.Size = ((System.Drawing.Size)(resources.GetObject("comboBoxCleanMode.Size")));
			this.comboBoxCleanMode.TabIndex = ((int)(resources.GetObject("comboBoxCleanMode.TabIndex")));
			this.comboBoxCleanMode.Text = resources.GetString("comboBoxCleanMode.Text");
			this.comboBoxCleanMode.Visible = ((bool)(resources.GetObject("comboBoxCleanMode.Visible")));
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.numericUpDownSuckWasteInkTime);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.numericUpDownInputAirTime);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.numericUpDownSuckInkTime);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.numericUpDownHeadBox_Z_SuckPos);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.numericUpDownCarriage_X_SuckPos);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.numericUpDownSuckTimes);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// numericUpDownSuckWasteInkTime
			// 
			this.numericUpDownSuckWasteInkTime.AccessibleDescription = resources.GetString("numericUpDownSuckWasteInkTime.AccessibleDescription");
			this.numericUpDownSuckWasteInkTime.AccessibleName = resources.GetString("numericUpDownSuckWasteInkTime.AccessibleName");
			this.numericUpDownSuckWasteInkTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownSuckWasteInkTime.Anchor")));
			this.numericUpDownSuckWasteInkTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownSuckWasteInkTime.Dock")));
			this.numericUpDownSuckWasteInkTime.Enabled = ((bool)(resources.GetObject("numericUpDownSuckWasteInkTime.Enabled")));
			this.numericUpDownSuckWasteInkTime.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownSuckWasteInkTime.Font")));
			this.numericUpDownSuckWasteInkTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownSuckWasteInkTime.ImeMode")));
			this.numericUpDownSuckWasteInkTime.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownSuckWasteInkTime.Location")));
			this.numericUpDownSuckWasteInkTime.Name = "numericUpDownSuckWasteInkTime";
			this.numericUpDownSuckWasteInkTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownSuckWasteInkTime.RightToLeft")));
			this.numericUpDownSuckWasteInkTime.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownSuckWasteInkTime.Size")));
			this.numericUpDownSuckWasteInkTime.TabIndex = ((int)(resources.GetObject("numericUpDownSuckWasteInkTime.TabIndex")));
			this.numericUpDownSuckWasteInkTime.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownSuckWasteInkTime.TextAlign")));
			this.numericUpDownSuckWasteInkTime.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownSuckWasteInkTime.ThousandsSeparator")));
			this.numericUpDownSuckWasteInkTime.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownSuckWasteInkTime.UpDownAlign")));
			this.numericUpDownSuckWasteInkTime.Visible = ((bool)(resources.GetObject("numericUpDownSuckWasteInkTime.Visible")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// numericUpDownInputAirTime
			// 
			this.numericUpDownInputAirTime.AccessibleDescription = resources.GetString("numericUpDownInputAirTime.AccessibleDescription");
			this.numericUpDownInputAirTime.AccessibleName = resources.GetString("numericUpDownInputAirTime.AccessibleName");
			this.numericUpDownInputAirTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownInputAirTime.Anchor")));
			this.numericUpDownInputAirTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownInputAirTime.Dock")));
			this.numericUpDownInputAirTime.Enabled = ((bool)(resources.GetObject("numericUpDownInputAirTime.Enabled")));
			this.numericUpDownInputAirTime.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownInputAirTime.Font")));
			this.numericUpDownInputAirTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownInputAirTime.ImeMode")));
			this.numericUpDownInputAirTime.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownInputAirTime.Location")));
			this.numericUpDownInputAirTime.Name = "numericUpDownInputAirTime";
			this.numericUpDownInputAirTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownInputAirTime.RightToLeft")));
			this.numericUpDownInputAirTime.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownInputAirTime.Size")));
			this.numericUpDownInputAirTime.TabIndex = ((int)(resources.GetObject("numericUpDownInputAirTime.TabIndex")));
			this.numericUpDownInputAirTime.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownInputAirTime.TextAlign")));
			this.numericUpDownInputAirTime.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownInputAirTime.ThousandsSeparator")));
			this.numericUpDownInputAirTime.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownInputAirTime.UpDownAlign")));
			this.numericUpDownInputAirTime.Visible = ((bool)(resources.GetObject("numericUpDownInputAirTime.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// numericUpDownSuckInkTime
			// 
			this.numericUpDownSuckInkTime.AccessibleDescription = resources.GetString("numericUpDownSuckInkTime.AccessibleDescription");
			this.numericUpDownSuckInkTime.AccessibleName = resources.GetString("numericUpDownSuckInkTime.AccessibleName");
			this.numericUpDownSuckInkTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownSuckInkTime.Anchor")));
			this.numericUpDownSuckInkTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownSuckInkTime.Dock")));
			this.numericUpDownSuckInkTime.Enabled = ((bool)(resources.GetObject("numericUpDownSuckInkTime.Enabled")));
			this.numericUpDownSuckInkTime.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownSuckInkTime.Font")));
			this.numericUpDownSuckInkTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownSuckInkTime.ImeMode")));
			this.numericUpDownSuckInkTime.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownSuckInkTime.Location")));
			this.numericUpDownSuckInkTime.Name = "numericUpDownSuckInkTime";
			this.numericUpDownSuckInkTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownSuckInkTime.RightToLeft")));
			this.numericUpDownSuckInkTime.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownSuckInkTime.Size")));
			this.numericUpDownSuckInkTime.TabIndex = ((int)(resources.GetObject("numericUpDownSuckInkTime.TabIndex")));
			this.numericUpDownSuckInkTime.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownSuckInkTime.TextAlign")));
			this.numericUpDownSuckInkTime.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownSuckInkTime.ThousandsSeparator")));
			this.numericUpDownSuckInkTime.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownSuckInkTime.UpDownAlign")));
			this.numericUpDownSuckInkTime.Visible = ((bool)(resources.GetObject("numericUpDownSuckInkTime.Visible")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// numericUpDownHeadBox_Z_SuckPos
			// 
			this.numericUpDownHeadBox_Z_SuckPos.AccessibleDescription = resources.GetString("numericUpDownHeadBox_Z_SuckPos.AccessibleDescription");
			this.numericUpDownHeadBox_Z_SuckPos.AccessibleName = resources.GetString("numericUpDownHeadBox_Z_SuckPos.AccessibleName");
			this.numericUpDownHeadBox_Z_SuckPos.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Anchor")));
			this.numericUpDownHeadBox_Z_SuckPos.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Dock")));
			this.numericUpDownHeadBox_Z_SuckPos.Enabled = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Enabled")));
			this.numericUpDownHeadBox_Z_SuckPos.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Font")));
			this.numericUpDownHeadBox_Z_SuckPos.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.ImeMode")));
			this.numericUpDownHeadBox_Z_SuckPos.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Location")));
			this.numericUpDownHeadBox_Z_SuckPos.Name = "numericUpDownHeadBox_Z_SuckPos";
			this.numericUpDownHeadBox_Z_SuckPos.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.RightToLeft")));
			this.numericUpDownHeadBox_Z_SuckPos.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Size")));
			this.numericUpDownHeadBox_Z_SuckPos.TabIndex = ((int)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.TabIndex")));
			this.numericUpDownHeadBox_Z_SuckPos.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.TextAlign")));
			this.numericUpDownHeadBox_Z_SuckPos.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.ThousandsSeparator")));
			this.numericUpDownHeadBox_Z_SuckPos.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.UpDownAlign")));
			this.numericUpDownHeadBox_Z_SuckPos.Visible = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_SuckPos.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// numericUpDownCarriage_X_SuckPos
			// 
			this.numericUpDownCarriage_X_SuckPos.AccessibleDescription = resources.GetString("numericUpDownCarriage_X_SuckPos.AccessibleDescription");
			this.numericUpDownCarriage_X_SuckPos.AccessibleName = resources.GetString("numericUpDownCarriage_X_SuckPos.AccessibleName");
			this.numericUpDownCarriage_X_SuckPos.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Anchor")));
			this.numericUpDownCarriage_X_SuckPos.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Dock")));
			this.numericUpDownCarriage_X_SuckPos.Enabled = ((bool)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Enabled")));
			this.numericUpDownCarriage_X_SuckPos.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Font")));
			this.numericUpDownCarriage_X_SuckPos.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownCarriage_X_SuckPos.ImeMode")));
			this.numericUpDownCarriage_X_SuckPos.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Location")));
			this.numericUpDownCarriage_X_SuckPos.Name = "numericUpDownCarriage_X_SuckPos";
			this.numericUpDownCarriage_X_SuckPos.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownCarriage_X_SuckPos.RightToLeft")));
			this.numericUpDownCarriage_X_SuckPos.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Size")));
			this.numericUpDownCarriage_X_SuckPos.TabIndex = ((int)(resources.GetObject("numericUpDownCarriage_X_SuckPos.TabIndex")));
			this.numericUpDownCarriage_X_SuckPos.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownCarriage_X_SuckPos.TextAlign")));
			this.numericUpDownCarriage_X_SuckPos.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownCarriage_X_SuckPos.ThousandsSeparator")));
			this.numericUpDownCarriage_X_SuckPos.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownCarriage_X_SuckPos.UpDownAlign")));
			this.numericUpDownCarriage_X_SuckPos.Visible = ((bool)(resources.GetObject("numericUpDownCarriage_X_SuckPos.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// numericUpDownSuckTimes
			// 
			this.numericUpDownSuckTimes.AccessibleDescription = resources.GetString("numericUpDownSuckTimes.AccessibleDescription");
			this.numericUpDownSuckTimes.AccessibleName = resources.GetString("numericUpDownSuckTimes.AccessibleName");
			this.numericUpDownSuckTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownSuckTimes.Anchor")));
			this.numericUpDownSuckTimes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownSuckTimes.Dock")));
			this.numericUpDownSuckTimes.Enabled = ((bool)(resources.GetObject("numericUpDownSuckTimes.Enabled")));
			this.numericUpDownSuckTimes.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownSuckTimes.Font")));
			this.numericUpDownSuckTimes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownSuckTimes.ImeMode")));
			this.numericUpDownSuckTimes.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownSuckTimes.Location")));
			this.numericUpDownSuckTimes.Name = "numericUpDownSuckTimes";
			this.numericUpDownSuckTimes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownSuckTimes.RightToLeft")));
			this.numericUpDownSuckTimes.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownSuckTimes.Size")));
			this.numericUpDownSuckTimes.TabIndex = ((int)(resources.GetObject("numericUpDownSuckTimes.TabIndex")));
			this.numericUpDownSuckTimes.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownSuckTimes.TextAlign")));
			this.numericUpDownSuckTimes.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownSuckTimes.ThousandsSeparator")));
			this.numericUpDownSuckTimes.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownSuckTimes.UpDownAlign")));
			this.numericUpDownSuckTimes.Visible = ((bool)(resources.GetObject("numericUpDownSuckTimes.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// numericUpDownWipeTimes
			// 
			this.numericUpDownWipeTimes.AccessibleDescription = resources.GetString("numericUpDownWipeTimes.AccessibleDescription");
			this.numericUpDownWipeTimes.AccessibleName = resources.GetString("numericUpDownWipeTimes.AccessibleName");
			this.numericUpDownWipeTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownWipeTimes.Anchor")));
			this.numericUpDownWipeTimes.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownWipeTimes.Dock")));
			this.numericUpDownWipeTimes.Enabled = ((bool)(resources.GetObject("numericUpDownWipeTimes.Enabled")));
			this.numericUpDownWipeTimes.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownWipeTimes.Font")));
			this.numericUpDownWipeTimes.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownWipeTimes.ImeMode")));
			this.numericUpDownWipeTimes.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownWipeTimes.Location")));
			this.numericUpDownWipeTimes.Name = "numericUpDownWipeTimes";
			this.numericUpDownWipeTimes.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownWipeTimes.RightToLeft")));
			this.numericUpDownWipeTimes.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownWipeTimes.Size")));
			this.numericUpDownWipeTimes.TabIndex = ((int)(resources.GetObject("numericUpDownWipeTimes.TabIndex")));
			this.numericUpDownWipeTimes.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownWipeTimes.TextAlign")));
			this.numericUpDownWipeTimes.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownWipeTimes.ThousandsSeparator")));
			this.numericUpDownWipeTimes.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownWipeTimes.UpDownAlign")));
			this.numericUpDownWipeTimes.Visible = ((bool)(resources.GetObject("numericUpDownWipeTimes.Visible")));
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
			// 
			// groupBox2
			// 
			this.groupBox2.AccessibleDescription = resources.GetString("groupBox2.AccessibleDescription");
			this.groupBox2.AccessibleName = resources.GetString("groupBox2.AccessibleName");
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox2.Anchor")));
			this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
			this.groupBox2.Controls.Add(this.numXWipePosEnd3);
			this.groupBox2.Controls.Add(this.numXWipePosEnd2);
			this.groupBox2.Controls.Add(this.numXWipePosEnd1);
			this.groupBox2.Controls.Add(this.numXWipePosEnd0);
			this.groupBox2.Controls.Add(this.numXWipePosStart3);
			this.groupBox2.Controls.Add(this.numXWipePosStart2);
			this.groupBox2.Controls.Add(this.numXWipePosStart1);
			this.groupBox2.Controls.Add(this.numXWipePosStart0);
			this.groupBox2.Controls.Add(this.numWiperPos_Y3);
			this.groupBox2.Controls.Add(this.numWiperPos_Y2);
			this.groupBox2.Controls.Add(this.numWiperPos_Y1);
			this.groupBox2.Controls.Add(this.numWiperPos_Y0);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.numericUpDownCarriage_X_Wipe_Speed);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.numericUpDownHeadBox_Z_WipePos);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.numericUpDownWipeTimes);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox2.Dock")));
			this.groupBox2.Enabled = ((bool)(resources.GetObject("groupBox2.Enabled")));
			this.groupBox2.Font = ((System.Drawing.Font)(resources.GetObject("groupBox2.Font")));
			this.groupBox2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox2.ImeMode")));
			this.groupBox2.Location = ((System.Drawing.Point)(resources.GetObject("groupBox2.Location")));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox2.RightToLeft")));
			this.groupBox2.Size = ((System.Drawing.Size)(resources.GetObject("groupBox2.Size")));
			this.groupBox2.TabIndex = ((int)(resources.GetObject("groupBox2.TabIndex")));
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = resources.GetString("groupBox2.Text");
			this.groupBox2.Visible = ((bool)(resources.GetObject("groupBox2.Visible")));
			// 
			// numXWipePosEnd3
			// 
			this.numXWipePosEnd3.AccessibleDescription = resources.GetString("numXWipePosEnd3.AccessibleDescription");
			this.numXWipePosEnd3.AccessibleName = resources.GetString("numXWipePosEnd3.AccessibleName");
			this.numXWipePosEnd3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosEnd3.Anchor")));
			this.numXWipePosEnd3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosEnd3.Dock")));
			this.numXWipePosEnd3.Enabled = ((bool)(resources.GetObject("numXWipePosEnd3.Enabled")));
			this.numXWipePosEnd3.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosEnd3.Font")));
			this.numXWipePosEnd3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosEnd3.ImeMode")));
			this.numXWipePosEnd3.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosEnd3.Location")));
			this.numXWipePosEnd3.Name = "numXWipePosEnd3";
			this.numXWipePosEnd3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosEnd3.RightToLeft")));
			this.numXWipePosEnd3.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosEnd3.Size")));
			this.numXWipePosEnd3.TabIndex = ((int)(resources.GetObject("numXWipePosEnd3.TabIndex")));
			this.numXWipePosEnd3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosEnd3.TextAlign")));
			this.numXWipePosEnd3.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosEnd3.ThousandsSeparator")));
			this.numXWipePosEnd3.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosEnd3.UpDownAlign")));
			this.numXWipePosEnd3.Visible = ((bool)(resources.GetObject("numXWipePosEnd3.Visible")));
			// 
			// numXWipePosEnd2
			// 
			this.numXWipePosEnd2.AccessibleDescription = resources.GetString("numXWipePosEnd2.AccessibleDescription");
			this.numXWipePosEnd2.AccessibleName = resources.GetString("numXWipePosEnd2.AccessibleName");
			this.numXWipePosEnd2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosEnd2.Anchor")));
			this.numXWipePosEnd2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosEnd2.Dock")));
			this.numXWipePosEnd2.Enabled = ((bool)(resources.GetObject("numXWipePosEnd2.Enabled")));
			this.numXWipePosEnd2.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosEnd2.Font")));
			this.numXWipePosEnd2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosEnd2.ImeMode")));
			this.numXWipePosEnd2.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosEnd2.Location")));
			this.numXWipePosEnd2.Name = "numXWipePosEnd2";
			this.numXWipePosEnd2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosEnd2.RightToLeft")));
			this.numXWipePosEnd2.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosEnd2.Size")));
			this.numXWipePosEnd2.TabIndex = ((int)(resources.GetObject("numXWipePosEnd2.TabIndex")));
			this.numXWipePosEnd2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosEnd2.TextAlign")));
			this.numXWipePosEnd2.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosEnd2.ThousandsSeparator")));
			this.numXWipePosEnd2.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosEnd2.UpDownAlign")));
			this.numXWipePosEnd2.Visible = ((bool)(resources.GetObject("numXWipePosEnd2.Visible")));
			// 
			// numXWipePosEnd1
			// 
			this.numXWipePosEnd1.AccessibleDescription = resources.GetString("numXWipePosEnd1.AccessibleDescription");
			this.numXWipePosEnd1.AccessibleName = resources.GetString("numXWipePosEnd1.AccessibleName");
			this.numXWipePosEnd1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosEnd1.Anchor")));
			this.numXWipePosEnd1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosEnd1.Dock")));
			this.numXWipePosEnd1.Enabled = ((bool)(resources.GetObject("numXWipePosEnd1.Enabled")));
			this.numXWipePosEnd1.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosEnd1.Font")));
			this.numXWipePosEnd1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosEnd1.ImeMode")));
			this.numXWipePosEnd1.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosEnd1.Location")));
			this.numXWipePosEnd1.Name = "numXWipePosEnd1";
			this.numXWipePosEnd1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosEnd1.RightToLeft")));
			this.numXWipePosEnd1.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosEnd1.Size")));
			this.numXWipePosEnd1.TabIndex = ((int)(resources.GetObject("numXWipePosEnd1.TabIndex")));
			this.numXWipePosEnd1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosEnd1.TextAlign")));
			this.numXWipePosEnd1.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosEnd1.ThousandsSeparator")));
			this.numXWipePosEnd1.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosEnd1.UpDownAlign")));
			this.numXWipePosEnd1.Visible = ((bool)(resources.GetObject("numXWipePosEnd1.Visible")));
			// 
			// numXWipePosEnd0
			// 
			this.numXWipePosEnd0.AccessibleDescription = resources.GetString("numXWipePosEnd0.AccessibleDescription");
			this.numXWipePosEnd0.AccessibleName = resources.GetString("numXWipePosEnd0.AccessibleName");
			this.numXWipePosEnd0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosEnd0.Anchor")));
			this.numXWipePosEnd0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosEnd0.Dock")));
			this.numXWipePosEnd0.Enabled = ((bool)(resources.GetObject("numXWipePosEnd0.Enabled")));
			this.numXWipePosEnd0.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosEnd0.Font")));
			this.numXWipePosEnd0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosEnd0.ImeMode")));
			this.numXWipePosEnd0.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosEnd0.Location")));
			this.numXWipePosEnd0.Name = "numXWipePosEnd0";
			this.numXWipePosEnd0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosEnd0.RightToLeft")));
			this.numXWipePosEnd0.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosEnd0.Size")));
			this.numXWipePosEnd0.TabIndex = ((int)(resources.GetObject("numXWipePosEnd0.TabIndex")));
			this.numXWipePosEnd0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosEnd0.TextAlign")));
			this.numXWipePosEnd0.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosEnd0.ThousandsSeparator")));
			this.numXWipePosEnd0.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosEnd0.UpDownAlign")));
			this.numXWipePosEnd0.Visible = ((bool)(resources.GetObject("numXWipePosEnd0.Visible")));
			// 
			// numXWipePosStart3
			// 
			this.numXWipePosStart3.AccessibleDescription = resources.GetString("numXWipePosStart3.AccessibleDescription");
			this.numXWipePosStart3.AccessibleName = resources.GetString("numXWipePosStart3.AccessibleName");
			this.numXWipePosStart3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosStart3.Anchor")));
			this.numXWipePosStart3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosStart3.Dock")));
			this.numXWipePosStart3.Enabled = ((bool)(resources.GetObject("numXWipePosStart3.Enabled")));
			this.numXWipePosStart3.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosStart3.Font")));
			this.numXWipePosStart3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosStart3.ImeMode")));
			this.numXWipePosStart3.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosStart3.Location")));
			this.numXWipePosStart3.Name = "numXWipePosStart3";
			this.numXWipePosStart3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosStart3.RightToLeft")));
			this.numXWipePosStart3.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosStart3.Size")));
			this.numXWipePosStart3.TabIndex = ((int)(resources.GetObject("numXWipePosStart3.TabIndex")));
			this.numXWipePosStart3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosStart3.TextAlign")));
			this.numXWipePosStart3.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosStart3.ThousandsSeparator")));
			this.numXWipePosStart3.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosStart3.UpDownAlign")));
			this.numXWipePosStart3.Visible = ((bool)(resources.GetObject("numXWipePosStart3.Visible")));
			// 
			// numXWipePosStart2
			// 
			this.numXWipePosStart2.AccessibleDescription = resources.GetString("numXWipePosStart2.AccessibleDescription");
			this.numXWipePosStart2.AccessibleName = resources.GetString("numXWipePosStart2.AccessibleName");
			this.numXWipePosStart2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosStart2.Anchor")));
			this.numXWipePosStart2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosStart2.Dock")));
			this.numXWipePosStart2.Enabled = ((bool)(resources.GetObject("numXWipePosStart2.Enabled")));
			this.numXWipePosStart2.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosStart2.Font")));
			this.numXWipePosStart2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosStart2.ImeMode")));
			this.numXWipePosStart2.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosStart2.Location")));
			this.numXWipePosStart2.Name = "numXWipePosStart2";
			this.numXWipePosStart2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosStart2.RightToLeft")));
			this.numXWipePosStart2.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosStart2.Size")));
			this.numXWipePosStart2.TabIndex = ((int)(resources.GetObject("numXWipePosStart2.TabIndex")));
			this.numXWipePosStart2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosStart2.TextAlign")));
			this.numXWipePosStart2.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosStart2.ThousandsSeparator")));
			this.numXWipePosStart2.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosStart2.UpDownAlign")));
			this.numXWipePosStart2.Visible = ((bool)(resources.GetObject("numXWipePosStart2.Visible")));
			// 
			// numXWipePosStart1
			// 
			this.numXWipePosStart1.AccessibleDescription = resources.GetString("numXWipePosStart1.AccessibleDescription");
			this.numXWipePosStart1.AccessibleName = resources.GetString("numXWipePosStart1.AccessibleName");
			this.numXWipePosStart1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosStart1.Anchor")));
			this.numXWipePosStart1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosStart1.Dock")));
			this.numXWipePosStart1.Enabled = ((bool)(resources.GetObject("numXWipePosStart1.Enabled")));
			this.numXWipePosStart1.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosStart1.Font")));
			this.numXWipePosStart1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosStart1.ImeMode")));
			this.numXWipePosStart1.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosStart1.Location")));
			this.numXWipePosStart1.Name = "numXWipePosStart1";
			this.numXWipePosStart1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosStart1.RightToLeft")));
			this.numXWipePosStart1.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosStart1.Size")));
			this.numXWipePosStart1.TabIndex = ((int)(resources.GetObject("numXWipePosStart1.TabIndex")));
			this.numXWipePosStart1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosStart1.TextAlign")));
			this.numXWipePosStart1.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosStart1.ThousandsSeparator")));
			this.numXWipePosStart1.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosStart1.UpDownAlign")));
			this.numXWipePosStart1.Visible = ((bool)(resources.GetObject("numXWipePosStart1.Visible")));
			// 
			// numXWipePosStart0
			// 
			this.numXWipePosStart0.AccessibleDescription = resources.GetString("numXWipePosStart0.AccessibleDescription");
			this.numXWipePosStart0.AccessibleName = resources.GetString("numXWipePosStart0.AccessibleName");
			this.numXWipePosStart0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numXWipePosStart0.Anchor")));
			this.numXWipePosStart0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numXWipePosStart0.Dock")));
			this.numXWipePosStart0.Enabled = ((bool)(resources.GetObject("numXWipePosStart0.Enabled")));
			this.numXWipePosStart0.Font = ((System.Drawing.Font)(resources.GetObject("numXWipePosStart0.Font")));
			this.numXWipePosStart0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numXWipePosStart0.ImeMode")));
			this.numXWipePosStart0.Location = ((System.Drawing.Point)(resources.GetObject("numXWipePosStart0.Location")));
			this.numXWipePosStart0.Name = "numXWipePosStart0";
			this.numXWipePosStart0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numXWipePosStart0.RightToLeft")));
			this.numXWipePosStart0.Size = ((System.Drawing.Size)(resources.GetObject("numXWipePosStart0.Size")));
			this.numXWipePosStart0.TabIndex = ((int)(resources.GetObject("numXWipePosStart0.TabIndex")));
			this.numXWipePosStart0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numXWipePosStart0.TextAlign")));
			this.numXWipePosStart0.ThousandsSeparator = ((bool)(resources.GetObject("numXWipePosStart0.ThousandsSeparator")));
			this.numXWipePosStart0.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numXWipePosStart0.UpDownAlign")));
			this.numXWipePosStart0.Visible = ((bool)(resources.GetObject("numXWipePosStart0.Visible")));
			// 
			// numWiperPos_Y3
			// 
			this.numWiperPos_Y3.AccessibleDescription = resources.GetString("numWiperPos_Y3.AccessibleDescription");
			this.numWiperPos_Y3.AccessibleName = resources.GetString("numWiperPos_Y3.AccessibleName");
			this.numWiperPos_Y3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numWiperPos_Y3.Anchor")));
			this.numWiperPos_Y3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numWiperPos_Y3.Dock")));
			this.numWiperPos_Y3.Enabled = ((bool)(resources.GetObject("numWiperPos_Y3.Enabled")));
			this.numWiperPos_Y3.Font = ((System.Drawing.Font)(resources.GetObject("numWiperPos_Y3.Font")));
			this.numWiperPos_Y3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numWiperPos_Y3.ImeMode")));
			this.numWiperPos_Y3.Location = ((System.Drawing.Point)(resources.GetObject("numWiperPos_Y3.Location")));
			this.numWiperPos_Y3.Name = "numWiperPos_Y3";
			this.numWiperPos_Y3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numWiperPos_Y3.RightToLeft")));
			this.numWiperPos_Y3.Size = ((System.Drawing.Size)(resources.GetObject("numWiperPos_Y3.Size")));
			this.numWiperPos_Y3.TabIndex = ((int)(resources.GetObject("numWiperPos_Y3.TabIndex")));
			this.numWiperPos_Y3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numWiperPos_Y3.TextAlign")));
			this.numWiperPos_Y3.ThousandsSeparator = ((bool)(resources.GetObject("numWiperPos_Y3.ThousandsSeparator")));
			this.numWiperPos_Y3.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numWiperPos_Y3.UpDownAlign")));
			this.numWiperPos_Y3.Visible = ((bool)(resources.GetObject("numWiperPos_Y3.Visible")));
			// 
			// numWiperPos_Y2
			// 
			this.numWiperPos_Y2.AccessibleDescription = resources.GetString("numWiperPos_Y2.AccessibleDescription");
			this.numWiperPos_Y2.AccessibleName = resources.GetString("numWiperPos_Y2.AccessibleName");
			this.numWiperPos_Y2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numWiperPos_Y2.Anchor")));
			this.numWiperPos_Y2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numWiperPos_Y2.Dock")));
			this.numWiperPos_Y2.Enabled = ((bool)(resources.GetObject("numWiperPos_Y2.Enabled")));
			this.numWiperPos_Y2.Font = ((System.Drawing.Font)(resources.GetObject("numWiperPos_Y2.Font")));
			this.numWiperPos_Y2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numWiperPos_Y2.ImeMode")));
			this.numWiperPos_Y2.Location = ((System.Drawing.Point)(resources.GetObject("numWiperPos_Y2.Location")));
			this.numWiperPos_Y2.Name = "numWiperPos_Y2";
			this.numWiperPos_Y2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numWiperPos_Y2.RightToLeft")));
			this.numWiperPos_Y2.Size = ((System.Drawing.Size)(resources.GetObject("numWiperPos_Y2.Size")));
			this.numWiperPos_Y2.TabIndex = ((int)(resources.GetObject("numWiperPos_Y2.TabIndex")));
			this.numWiperPos_Y2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numWiperPos_Y2.TextAlign")));
			this.numWiperPos_Y2.ThousandsSeparator = ((bool)(resources.GetObject("numWiperPos_Y2.ThousandsSeparator")));
			this.numWiperPos_Y2.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numWiperPos_Y2.UpDownAlign")));
			this.numWiperPos_Y2.Visible = ((bool)(resources.GetObject("numWiperPos_Y2.Visible")));
			// 
			// numWiperPos_Y1
			// 
			this.numWiperPos_Y1.AccessibleDescription = resources.GetString("numWiperPos_Y1.AccessibleDescription");
			this.numWiperPos_Y1.AccessibleName = resources.GetString("numWiperPos_Y1.AccessibleName");
			this.numWiperPos_Y1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numWiperPos_Y1.Anchor")));
			this.numWiperPos_Y1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numWiperPos_Y1.Dock")));
			this.numWiperPos_Y1.Enabled = ((bool)(resources.GetObject("numWiperPos_Y1.Enabled")));
			this.numWiperPos_Y1.Font = ((System.Drawing.Font)(resources.GetObject("numWiperPos_Y1.Font")));
			this.numWiperPos_Y1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numWiperPos_Y1.ImeMode")));
			this.numWiperPos_Y1.Location = ((System.Drawing.Point)(resources.GetObject("numWiperPos_Y1.Location")));
			this.numWiperPos_Y1.Name = "numWiperPos_Y1";
			this.numWiperPos_Y1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numWiperPos_Y1.RightToLeft")));
			this.numWiperPos_Y1.Size = ((System.Drawing.Size)(resources.GetObject("numWiperPos_Y1.Size")));
			this.numWiperPos_Y1.TabIndex = ((int)(resources.GetObject("numWiperPos_Y1.TabIndex")));
			this.numWiperPos_Y1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numWiperPos_Y1.TextAlign")));
			this.numWiperPos_Y1.ThousandsSeparator = ((bool)(resources.GetObject("numWiperPos_Y1.ThousandsSeparator")));
			this.numWiperPos_Y1.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numWiperPos_Y1.UpDownAlign")));
			this.numWiperPos_Y1.Visible = ((bool)(resources.GetObject("numWiperPos_Y1.Visible")));
			// 
			// numWiperPos_Y0
			// 
			this.numWiperPos_Y0.AccessibleDescription = resources.GetString("numWiperPos_Y0.AccessibleDescription");
			this.numWiperPos_Y0.AccessibleName = resources.GetString("numWiperPos_Y0.AccessibleName");
			this.numWiperPos_Y0.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numWiperPos_Y0.Anchor")));
			this.numWiperPos_Y0.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numWiperPos_Y0.Dock")));
			this.numWiperPos_Y0.Enabled = ((bool)(resources.GetObject("numWiperPos_Y0.Enabled")));
			this.numWiperPos_Y0.Font = ((System.Drawing.Font)(resources.GetObject("numWiperPos_Y0.Font")));
			this.numWiperPos_Y0.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numWiperPos_Y0.ImeMode")));
			this.numWiperPos_Y0.Location = ((System.Drawing.Point)(resources.GetObject("numWiperPos_Y0.Location")));
			this.numWiperPos_Y0.Name = "numWiperPos_Y0";
			this.numWiperPos_Y0.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numWiperPos_Y0.RightToLeft")));
			this.numWiperPos_Y0.Size = ((System.Drawing.Size)(resources.GetObject("numWiperPos_Y0.Size")));
			this.numWiperPos_Y0.TabIndex = ((int)(resources.GetObject("numWiperPos_Y0.TabIndex")));
			this.numWiperPos_Y0.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numWiperPos_Y0.TextAlign")));
			this.numWiperPos_Y0.ThousandsSeparator = ((bool)(resources.GetObject("numWiperPos_Y0.ThousandsSeparator")));
			this.numWiperPos_Y0.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numWiperPos_Y0.UpDownAlign")));
			this.numWiperPos_Y0.Visible = ((bool)(resources.GetObject("numWiperPos_Y0.Visible")));
			// 
			// label13
			// 
			this.label13.AccessibleDescription = resources.GetString("label13.AccessibleDescription");
			this.label13.AccessibleName = resources.GetString("label13.AccessibleName");
			this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label13.Anchor")));
			this.label13.AutoSize = ((bool)(resources.GetObject("label13.AutoSize")));
			this.label13.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label13.Dock")));
			this.label13.Enabled = ((bool)(resources.GetObject("label13.Enabled")));
			this.label13.Font = ((System.Drawing.Font)(resources.GetObject("label13.Font")));
			this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
			this.label13.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.ImageAlign")));
			this.label13.ImageIndex = ((int)(resources.GetObject("label13.ImageIndex")));
			this.label13.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label13.ImeMode")));
			this.label13.Location = ((System.Drawing.Point)(resources.GetObject("label13.Location")));
			this.label13.Name = "label13";
			this.label13.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label13.RightToLeft")));
			this.label13.Size = ((System.Drawing.Size)(resources.GetObject("label13.Size")));
			this.label13.TabIndex = ((int)(resources.GetObject("label13.TabIndex")));
			this.label13.Text = resources.GetString("label13.Text");
			this.label13.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label13.TextAlign")));
			this.label13.Visible = ((bool)(resources.GetObject("label13.Visible")));
			// 
			// label12
			// 
			this.label12.AccessibleDescription = resources.GetString("label12.AccessibleDescription");
			this.label12.AccessibleName = resources.GetString("label12.AccessibleName");
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label12.Anchor")));
			this.label12.AutoSize = ((bool)(resources.GetObject("label12.AutoSize")));
			this.label12.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label12.Dock")));
			this.label12.Enabled = ((bool)(resources.GetObject("label12.Enabled")));
			this.label12.Font = ((System.Drawing.Font)(resources.GetObject("label12.Font")));
			this.label12.Image = ((System.Drawing.Image)(resources.GetObject("label12.Image")));
			this.label12.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.ImageAlign")));
			this.label12.ImageIndex = ((int)(resources.GetObject("label12.ImageIndex")));
			this.label12.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label12.ImeMode")));
			this.label12.Location = ((System.Drawing.Point)(resources.GetObject("label12.Location")));
			this.label12.Name = "label12";
			this.label12.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label12.RightToLeft")));
			this.label12.Size = ((System.Drawing.Size)(resources.GetObject("label12.Size")));
			this.label12.TabIndex = ((int)(resources.GetObject("label12.TabIndex")));
			this.label12.Text = resources.GetString("label12.Text");
			this.label12.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label12.TextAlign")));
			this.label12.Visible = ((bool)(resources.GetObject("label12.Visible")));
			// 
			// label11
			// 
			this.label11.AccessibleDescription = resources.GetString("label11.AccessibleDescription");
			this.label11.AccessibleName = resources.GetString("label11.AccessibleName");
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label11.Anchor")));
			this.label11.AutoSize = ((bool)(resources.GetObject("label11.AutoSize")));
			this.label11.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label11.Dock")));
			this.label11.Enabled = ((bool)(resources.GetObject("label11.Enabled")));
			this.label11.Font = ((System.Drawing.Font)(resources.GetObject("label11.Font")));
			this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
			this.label11.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.ImageAlign")));
			this.label11.ImageIndex = ((int)(resources.GetObject("label11.ImageIndex")));
			this.label11.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label11.ImeMode")));
			this.label11.Location = ((System.Drawing.Point)(resources.GetObject("label11.Location")));
			this.label11.Name = "label11";
			this.label11.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label11.RightToLeft")));
			this.label11.Size = ((System.Drawing.Size)(resources.GetObject("label11.Size")));
			this.label11.TabIndex = ((int)(resources.GetObject("label11.TabIndex")));
			this.label11.Text = resources.GetString("label11.Text");
			this.label11.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label11.TextAlign")));
			this.label11.Visible = ((bool)(resources.GetObject("label11.Visible")));
			// 
			// numericUpDownCarriage_X_Wipe_Speed
			// 
			this.numericUpDownCarriage_X_Wipe_Speed.AccessibleDescription = resources.GetString("numericUpDownCarriage_X_Wipe_Speed.AccessibleDescription");
			this.numericUpDownCarriage_X_Wipe_Speed.AccessibleName = resources.GetString("numericUpDownCarriage_X_Wipe_Speed.AccessibleName");
			this.numericUpDownCarriage_X_Wipe_Speed.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Anchor")));
			this.numericUpDownCarriage_X_Wipe_Speed.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Dock")));
			this.numericUpDownCarriage_X_Wipe_Speed.Enabled = ((bool)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Enabled")));
			this.numericUpDownCarriage_X_Wipe_Speed.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Font")));
			this.numericUpDownCarriage_X_Wipe_Speed.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.ImeMode")));
			this.numericUpDownCarriage_X_Wipe_Speed.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Location")));
			this.numericUpDownCarriage_X_Wipe_Speed.Name = "numericUpDownCarriage_X_Wipe_Speed";
			this.numericUpDownCarriage_X_Wipe_Speed.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.RightToLeft")));
			this.numericUpDownCarriage_X_Wipe_Speed.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Size")));
			this.numericUpDownCarriage_X_Wipe_Speed.TabIndex = ((int)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.TabIndex")));
			this.numericUpDownCarriage_X_Wipe_Speed.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.TextAlign")));
			this.numericUpDownCarriage_X_Wipe_Speed.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.ThousandsSeparator")));
			this.numericUpDownCarriage_X_Wipe_Speed.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.UpDownAlign")));
			this.numericUpDownCarriage_X_Wipe_Speed.Visible = ((bool)(resources.GetObject("numericUpDownCarriage_X_Wipe_Speed.Visible")));
			// 
			// label10
			// 
			this.label10.AccessibleDescription = resources.GetString("label10.AccessibleDescription");
			this.label10.AccessibleName = resources.GetString("label10.AccessibleName");
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label10.Anchor")));
			this.label10.AutoSize = ((bool)(resources.GetObject("label10.AutoSize")));
			this.label10.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label10.Dock")));
			this.label10.Enabled = ((bool)(resources.GetObject("label10.Enabled")));
			this.label10.Font = ((System.Drawing.Font)(resources.GetObject("label10.Font")));
			this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
			this.label10.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.ImageAlign")));
			this.label10.ImageIndex = ((int)(resources.GetObject("label10.ImageIndex")));
			this.label10.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label10.ImeMode")));
			this.label10.Location = ((System.Drawing.Point)(resources.GetObject("label10.Location")));
			this.label10.Name = "label10";
			this.label10.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label10.RightToLeft")));
			this.label10.Size = ((System.Drawing.Size)(resources.GetObject("label10.Size")));
			this.label10.TabIndex = ((int)(resources.GetObject("label10.TabIndex")));
			this.label10.Text = resources.GetString("label10.Text");
			this.label10.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label10.TextAlign")));
			this.label10.Visible = ((bool)(resources.GetObject("label10.Visible")));
			// 
			// numericUpDownHeadBox_Z_WipePos
			// 
			this.numericUpDownHeadBox_Z_WipePos.AccessibleDescription = resources.GetString("numericUpDownHeadBox_Z_WipePos.AccessibleDescription");
			this.numericUpDownHeadBox_Z_WipePos.AccessibleName = resources.GetString("numericUpDownHeadBox_Z_WipePos.AccessibleName");
			this.numericUpDownHeadBox_Z_WipePos.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Anchor")));
			this.numericUpDownHeadBox_Z_WipePos.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Dock")));
			this.numericUpDownHeadBox_Z_WipePos.Enabled = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Enabled")));
			this.numericUpDownHeadBox_Z_WipePos.Font = ((System.Drawing.Font)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Font")));
			this.numericUpDownHeadBox_Z_WipePos.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.ImeMode")));
			this.numericUpDownHeadBox_Z_WipePos.Location = ((System.Drawing.Point)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Location")));
			this.numericUpDownHeadBox_Z_WipePos.Name = "numericUpDownHeadBox_Z_WipePos";
			this.numericUpDownHeadBox_Z_WipePos.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.RightToLeft")));
			this.numericUpDownHeadBox_Z_WipePos.Size = ((System.Drawing.Size)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Size")));
			this.numericUpDownHeadBox_Z_WipePos.TabIndex = ((int)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.TabIndex")));
			this.numericUpDownHeadBox_Z_WipePos.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.TextAlign")));
			this.numericUpDownHeadBox_Z_WipePos.ThousandsSeparator = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.ThousandsSeparator")));
			this.numericUpDownHeadBox_Z_WipePos.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.UpDownAlign")));
			this.numericUpDownHeadBox_Z_WipePos.Visible = ((bool)(resources.GetObject("numericUpDownHeadBox_Z_WipePos.Visible")));
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
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
			this.panel1.Controls.Add(this.buttonRead);
			this.panel1.Controls.Add(this.buttonWrite);
			this.panel1.Controls.Add(this.comboBoxCleanMode);
			this.panel1.Controls.Add(this.label1);
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
			// buttonRead
			// 
			this.buttonRead.AccessibleDescription = resources.GetString("buttonRead.AccessibleDescription");
			this.buttonRead.AccessibleName = resources.GetString("buttonRead.AccessibleName");
			this.buttonRead.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonRead.Anchor")));
			this.buttonRead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRead.BackgroundImage")));
			this.buttonRead.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonRead.Dock")));
			this.buttonRead.Enabled = ((bool)(resources.GetObject("buttonRead.Enabled")));
			this.buttonRead.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonRead.FlatStyle")));
			this.buttonRead.Font = ((System.Drawing.Font)(resources.GetObject("buttonRead.Font")));
			this.buttonRead.Image = ((System.Drawing.Image)(resources.GetObject("buttonRead.Image")));
			this.buttonRead.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonRead.ImageAlign")));
			this.buttonRead.ImageIndex = ((int)(resources.GetObject("buttonRead.ImageIndex")));
			this.buttonRead.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonRead.ImeMode")));
			this.buttonRead.Location = ((System.Drawing.Point)(resources.GetObject("buttonRead.Location")));
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonRead.RightToLeft")));
			this.buttonRead.Size = ((System.Drawing.Size)(resources.GetObject("buttonRead.Size")));
			this.buttonRead.TabIndex = ((int)(resources.GetObject("buttonRead.TabIndex")));
			this.buttonRead.Text = resources.GetString("buttonRead.Text");
			this.buttonRead.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonRead.TextAlign")));
			this.buttonRead.Visible = ((bool)(resources.GetObject("buttonRead.Visible")));
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// buttonWrite
			// 
			this.buttonWrite.AccessibleDescription = resources.GetString("buttonWrite.AccessibleDescription");
			this.buttonWrite.AccessibleName = resources.GetString("buttonWrite.AccessibleName");
			this.buttonWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonWrite.Anchor")));
			this.buttonWrite.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonWrite.BackgroundImage")));
			this.buttonWrite.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonWrite.Dock")));
			this.buttonWrite.Enabled = ((bool)(resources.GetObject("buttonWrite.Enabled")));
			this.buttonWrite.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonWrite.FlatStyle")));
			this.buttonWrite.Font = ((System.Drawing.Font)(resources.GetObject("buttonWrite.Font")));
			this.buttonWrite.Image = ((System.Drawing.Image)(resources.GetObject("buttonWrite.Image")));
			this.buttonWrite.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonWrite.ImageAlign")));
			this.buttonWrite.ImageIndex = ((int)(resources.GetObject("buttonWrite.ImageIndex")));
			this.buttonWrite.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonWrite.ImeMode")));
			this.buttonWrite.Location = ((System.Drawing.Point)(resources.GetObject("buttonWrite.Location")));
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonWrite.RightToLeft")));
			this.buttonWrite.Size = ((System.Drawing.Size)(resources.GetObject("buttonWrite.Size")));
			this.buttonWrite.TabIndex = ((int)(resources.GetObject("buttonWrite.TabIndex")));
			this.buttonWrite.Text = resources.GetString("buttonWrite.Text");
			this.buttonWrite.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonWrite.TextAlign")));
			this.buttonWrite.Visible = ((bool)(resources.GetObject("buttonWrite.Visible")));
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// AllwinCleanControl
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "AllwinCleanControl";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckWasteInkTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInputAirTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckInkTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBox_Z_SuckPos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCarriage_X_SuckPos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckTimes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWipeTimes)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosEnd0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numXWipePosStart0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWiperPos_Y0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCarriage_X_Wipe_Speed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeadBox_Z_WipePos)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region Public Methods

		/// <summary>
		/// The get clean parameter.
		/// </summary>
		/// <returns>
		/// </returns>
		public AllwinCleanParameter GetCleanParameter()
		{
			if (PubFunc.IsInDesignMode())
			{
				return this.mCleanParameter;
			}
			this.mCleanParameter = new AllwinCleanParameter();
			this.mCleanParameter.SuckTimes = (byte)this.numericUpDownSuckTimes.Value;
			this.mCleanParameter.Carriage_X_SuckPos = (short)this.numericUpDownCarriage_X_SuckPos.Value;
			this.mCleanParameter.HeadBox_Z_SuckPos = (short)this.numericUpDownHeadBox_Z_SuckPos.Value;
			this.mCleanParameter.SuckInkTime = (short)this.numericUpDownSuckInkTime.Value;
			this.mCleanParameter.InputAirTime = (short)this.numericUpDownInputAirTime.Value;
			this.mCleanParameter.SuckWasteInkTime = (short)this.numericUpDownSuckWasteInkTime.Value;

			this.mCleanParameter.WipeTimes = (byte)this.numericUpDownWipeTimes.Value;
			if (this.mCleanParameter.WiperPos_Y == null)
				this.mCleanParameter.WiperPos_Y = new short[4];
			for (int i = 0; i < this.WiperPos_Y_UpDowns.Length; i++)
			{
				this.mCleanParameter.WiperPos_Y[i] = (short)this.WiperPos_Y_UpDowns[i].Value;
			}

			this.mCleanParameter.HeadBox_Z_WipePos = (short)this.numericUpDownHeadBox_Z_WipePos.Value;
			if (this.mCleanParameter.Carriage_X_WipePos_Start == null)
				this.mCleanParameter.Carriage_X_WipePos_Start = new short[4];
			for (int i = 0; i < this.X_WipePos_Start_UpDowns.Length; i++)
			{
				this.mCleanParameter.Carriage_X_WipePos_Start[i] = (short)this.X_WipePos_Start_UpDowns[i].Value;
			}
			if (this.mCleanParameter.Carriage_X_WipePos_End == null)
				this.mCleanParameter.Carriage_X_WipePos_End = new short[4];
			for (int i = 0; i < this.X_WipePos_End_UpDowns.Length; i++)
			{
				this.mCleanParameter.Carriage_X_WipePos_End[i] = (short)this.X_WipePos_End_UpDowns[i].Value;
			}

			this.mCleanParameter.Carriage_X_Wipe_Speed = (byte)this.numericUpDownCarriage_X_Wipe_Speed.Value;

			return this.mCleanParameter;
		}

		/// <summary>
		/// The set clean parameter.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		public void SetCleanParameter(AllwinCleanParameter value)
		{
			this.mCleanParameter = value;
			if (PubFunc.IsInDesignMode())
			{
				return;
			}

			this.numericUpDownSuckTimes.Value = new decimal(this.mCleanParameter.SuckTimes);
			this.numericUpDownCarriage_X_SuckPos.Value = new decimal(this.mCleanParameter.Carriage_X_SuckPos);
			this.numericUpDownHeadBox_Z_SuckPos.Value = new decimal(this.mCleanParameter.HeadBox_Z_SuckPos);
			this.numericUpDownSuckInkTime.Value = new decimal(this.mCleanParameter.SuckInkTime);
			this.numericUpDownInputAirTime.Value = new decimal(this.mCleanParameter.InputAirTime);
			this.numericUpDownSuckWasteInkTime.Value = new decimal(this.mCleanParameter.SuckWasteInkTime);

			this.numericUpDownWipeTimes.Value = new decimal(this.mCleanParameter.WipeTimes);
			for (int i = 0; i < this.WiperPos_Y_UpDowns.Length; i++)
			{
				this.WiperPos_Y_UpDowns[i].Value = this.mCleanParameter.WiperPos_Y[i];
			}

			this.numericUpDownHeadBox_Z_WipePos.Value = new decimal(this.mCleanParameter.HeadBox_Z_WipePos);
			for (int i = 0; i < this.X_WipePos_Start_UpDowns.Length; i++)
			{
				this.X_WipePos_Start_UpDowns[i].Value = this.mCleanParameter.Carriage_X_WipePos_Start[i];
			}

			for (int i = 0; i < this.X_WipePos_End_UpDowns.Length; i++)
			{
				this.X_WipePos_End_UpDowns[i].Value = this.mCleanParameter.Carriage_X_WipePos_End[i];
			}

			this.numericUpDownCarriage_X_Wipe_Speed.Value = new decimal(this.mCleanParameter.Carriage_X_Wipe_Speed);
		}

		#endregion

		/// <summary>
		/// 2。读相应的Parameter.
		/// 通过USB EP0。方向是IN。
		/// reqCode是0x7F。value是3。EP0是要读的参数。
		/// </summary>
		private byte[] ReadAllWinCleanParameter()
		{
			byte[] subval = new byte[Marshal.SizeOf(typeof(AllwinCleanParameter))];
			ushort index = (ushort)(this.comboBoxCleanMode.SelectedIndex << 8);
			uint bufsize = (uint)subval.Length;
			if (CoreInterface.GetEpsonEP0Cmd( 0x7F, subval, ref bufsize, 3, index) == 0)
			{
				MessageBox.Show("读AllWin清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			/* StructToBytes 方法传入的字节流需要按照结构内最大原子结构对齐，此处要添加填充位*/
			// for (int i = 1; i < subval.Length - 1; i++)
			// {
			// if (i == 1 || i == 13)
			// {
			// Buffer.BlockCopy(subval, i, subval, i + 1, subval.Length - i - 1);
			// subval[i] = 0;
			// }
			// }
			return subval;
		}
		/// <summary>
		/// 1。设置Parameter
		/// 通过USB EP0。方向是OUT。
		/// reqCode是0x7F。value是2，setuplength是长度，一次操作的长度不能超过64Byte。
		/// 写失败会报STATUS_FTA_EEPROM_WRITE.
		/// </summary>
		/// <returns>
		/// The set all win clean parameter.
		/// </returns>
		private bool SetAllWinCleanParameter()
		{
			// write之前read一次,读出HeadMask待用
			byte[] vals = this.ReadAllWinCleanParameter();
			AllwinCleanParameter paratemp = (AllwinCleanParameter)PubFunc.BytesToStruct(vals, typeof(AllwinCleanParameter));

			AllwinCleanParameter para =this.GetCleanParameter();
			para.HeadMask = paratemp.HeadMask;
			byte[] subval = PubFunc.StructToBytes(para);

			// byte[] rel = new byte[39];
			/* BytesToStruct 方法放回的字节流，是按照结构内最大原子结构对齐过的，此处要去除填充位*/
			// Buffer.BlockCopy(subval, 0, rel, 0, 1);
			// Buffer.BlockCopy(subval, 2, rel, 1, 11);
			// Buffer.BlockCopy(subval, 14, rel, 12, 27);
			uint bufsize = (uint)subval.Length;
			ushort index = (ushort)(this.comboBoxCleanMode.SelectedIndex << 8);
			if (CoreInterface.SetEpsonEP0Cmd(0x7F,subval,ref bufsize,  2,index) == 0)
			{
				MessageBox.Show("设置AllWin清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private void buttonWrite_Click(object sender, System.EventArgs e)
		{
			this.SetAllWinCleanParameter();
		}

		private void buttonRead_Click(object sender, System.EventArgs e)
		{
			byte[] vals = this.ReadAllWinCleanParameter();
			this.mCleanParameter = (AllwinCleanParameter)PubFunc.BytesToStruct(vals, typeof(AllwinCleanParameter));
			this.SetCleanParameter(this.mCleanParameter);
		}

	}
}

