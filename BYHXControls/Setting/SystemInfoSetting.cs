using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public class SystemInfoSetting : BYHXUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SystemInfoSetting));
			this.groupBoxPassword = new BYHXPrinterManager.GradientControls.Grouper();
			this.m_ButtonSetLang = new System.Windows.Forms.Button();
			this.m_SerialNoControlTime = new BYHXControls.SerialNoControl();
			this.m_SerialNoControlLang = new BYHXControls.SerialNoControl();
			this.m_LabelPassword = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.m_ButtonSetTime = new System.Windows.Forms.Button();
			this.groupBox2 = new BYHXPrinterManager.GradientControls.Grouper();
			this.bPrinterHWSettingApply = new System.Windows.Forms.Button();
			this.m_CheckBoxUseYEncoder = new System.Windows.Forms.CheckBox();
			this.m_CheckBoxFlatBed = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new BYHXPrinterManager.GradientControls.Grouper();
			this.m_RadioButtonServoEncoder = new System.Windows.Forms.RadioButton();
			this.m_RadioButtonEncoder = new System.Windows.Forms.RadioButton();
			this.groupBoxPassword.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxPassword
			// 
			this.groupBoxPassword.AccessibleDescription = resources.GetString("groupBoxPassword.AccessibleDescription");
			this.groupBoxPassword.AccessibleName = resources.GetString("groupBoxPassword.AccessibleName");
			this.groupBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBoxPassword.Anchor")));
			this.groupBoxPassword.BackColor = System.Drawing.SystemColors.Control;
			this.groupBoxPassword.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
			this.groupBoxPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBoxPassword.BackgroundImage")));
			this.groupBoxPassword.BorderThickness = 1F;
			this.groupBoxPassword.Controls.Add(this.m_ButtonSetLang);
			this.groupBoxPassword.Controls.Add(this.m_SerialNoControlTime);
			this.groupBoxPassword.Controls.Add(this.m_SerialNoControlLang);
			this.groupBoxPassword.Controls.Add(this.m_LabelPassword);
			this.groupBoxPassword.Controls.Add(this.label1);
			this.groupBoxPassword.Controls.Add(this.m_ButtonSetTime);
			this.groupBoxPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBoxPassword.Dock")));
			this.groupBoxPassword.Enabled = ((bool)(resources.GetObject("groupBoxPassword.Enabled")));
			this.groupBoxPassword.Font = ((System.Drawing.Font)(resources.GetObject("groupBoxPassword.Font")));
			this.groupBoxPassword.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.Gold);
			this.groupBoxPassword.GroupImage = null;
			this.groupBoxPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBoxPassword.ImeMode")));
			this.groupBoxPassword.Location = ((System.Drawing.Point)(resources.GetObject("groupBoxPassword.Location")));
			this.groupBoxPassword.Name = "groupBoxPassword";
			this.groupBoxPassword.PaintGroupBox = false;
			this.groupBoxPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBoxPassword.RightToLeft")));
			this.groupBoxPassword.RoundCorners = 10;
			this.groupBoxPassword.ShadowColor = System.Drawing.Color.DarkGray;
			this.groupBoxPassword.ShadowControl = false;
			this.groupBoxPassword.ShadowThickness = 3;
			this.groupBoxPassword.Size = ((System.Drawing.Size)(resources.GetObject("groupBoxPassword.Size")));
			this.groupBoxPassword.TabIndex = ((int)(resources.GetObject("groupBoxPassword.TabIndex")));
			this.groupBoxPassword.TabStop = false;
			this.groupBoxPassword.Text = resources.GetString("groupBoxPassword.Text");
			this.groupBoxPassword.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.groupBoxPassword.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
			this.groupBoxPassword.Visible = ((bool)(resources.GetObject("groupBoxPassword.Visible")));
			// 
			// m_ButtonSetLang
			// 
			this.m_ButtonSetLang.AccessibleDescription = resources.GetString("m_ButtonSetLang.AccessibleDescription");
			this.m_ButtonSetLang.AccessibleName = resources.GetString("m_ButtonSetLang.AccessibleName");
			this.m_ButtonSetLang.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonSetLang.Anchor")));
			this.m_ButtonSetLang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetLang.BackgroundImage")));
			this.m_ButtonSetLang.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonSetLang.Dock")));
			this.m_ButtonSetLang.Enabled = ((bool)(resources.GetObject("m_ButtonSetLang.Enabled")));
			this.m_ButtonSetLang.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonSetLang.FlatStyle")));
			this.m_ButtonSetLang.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonSetLang.Font")));
			this.m_ButtonSetLang.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetLang.Image")));
			this.m_ButtonSetLang.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetLang.ImageAlign")));
			this.m_ButtonSetLang.ImageIndex = ((int)(resources.GetObject("m_ButtonSetLang.ImageIndex")));
			this.m_ButtonSetLang.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonSetLang.ImeMode")));
			this.m_ButtonSetLang.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonSetLang.Location")));
			this.m_ButtonSetLang.Name = "m_ButtonSetLang";
			this.m_ButtonSetLang.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonSetLang.RightToLeft")));
			this.m_ButtonSetLang.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonSetLang.Size")));
			this.m_ButtonSetLang.TabIndex = ((int)(resources.GetObject("m_ButtonSetLang.TabIndex")));
			this.m_ButtonSetLang.Text = resources.GetString("m_ButtonSetLang.Text");
			this.m_ButtonSetLang.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetLang.TextAlign")));
			this.m_ButtonSetLang.Visible = ((bool)(resources.GetObject("m_ButtonSetLang.Visible")));
			this.m_ButtonSetLang.Click += new System.EventHandler(this.m_ButtonSetLang_Click);
			// 
			// m_SerialNoControlTime
			// 
			this.m_SerialNoControlTime.AccessibleDescription = resources.GetString("m_SerialNoControlTime.AccessibleDescription");
			this.m_SerialNoControlTime.AccessibleName = resources.GetString("m_SerialNoControlTime.AccessibleName");
			this.m_SerialNoControlTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SerialNoControlTime.Anchor")));
			this.m_SerialNoControlTime.AutoScroll = ((bool)(resources.GetObject("m_SerialNoControlTime.AutoScroll")));
			this.m_SerialNoControlTime.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.AutoScrollMargin")));
			this.m_SerialNoControlTime.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.AutoScrollMinSize")));
			this.m_SerialNoControlTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SerialNoControlTime.BackgroundImage")));
			this.m_SerialNoControlTime.CharsPerUnit = 4;
			this.m_SerialNoControlTime.Count = 4;
			this.m_SerialNoControlTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SerialNoControlTime.Dock")));
			this.m_SerialNoControlTime.Enabled = ((bool)(resources.GetObject("m_SerialNoControlTime.Enabled")));
			this.m_SerialNoControlTime.Font = ((System.Drawing.Font)(resources.GetObject("m_SerialNoControlTime.Font")));
			this.m_SerialNoControlTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SerialNoControlTime.ImeMode")));
			this.m_SerialNoControlTime.Location = ((System.Drawing.Point)(resources.GetObject("m_SerialNoControlTime.Location")));
			this.m_SerialNoControlTime.Name = "m_SerialNoControlTime";
			this.m_SerialNoControlTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SerialNoControlTime.RightToLeft")));
			this.m_SerialNoControlTime.SeparateString = "-";
			this.m_SerialNoControlTime.Size = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlTime.Size")));
			this.m_SerialNoControlTime.TabIndex = ((int)(resources.GetObject("m_SerialNoControlTime.TabIndex")));
			this.m_SerialNoControlTime.Visible = ((bool)(resources.GetObject("m_SerialNoControlTime.Visible")));
			// 
			// m_SerialNoControlLang
			// 
			this.m_SerialNoControlLang.AccessibleDescription = resources.GetString("m_SerialNoControlLang.AccessibleDescription");
			this.m_SerialNoControlLang.AccessibleName = resources.GetString("m_SerialNoControlLang.AccessibleName");
			this.m_SerialNoControlLang.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_SerialNoControlLang.Anchor")));
			this.m_SerialNoControlLang.AutoScroll = ((bool)(resources.GetObject("m_SerialNoControlLang.AutoScroll")));
			this.m_SerialNoControlLang.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlLang.AutoScrollMargin")));
			this.m_SerialNoControlLang.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlLang.AutoScrollMinSize")));
			this.m_SerialNoControlLang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_SerialNoControlLang.BackgroundImage")));
			this.m_SerialNoControlLang.CharsPerUnit = 4;
			this.m_SerialNoControlLang.Count = 4;
			this.m_SerialNoControlLang.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_SerialNoControlLang.Dock")));
			this.m_SerialNoControlLang.Enabled = ((bool)(resources.GetObject("m_SerialNoControlLang.Enabled")));
			this.m_SerialNoControlLang.Font = ((System.Drawing.Font)(resources.GetObject("m_SerialNoControlLang.Font")));
			this.m_SerialNoControlLang.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_SerialNoControlLang.ImeMode")));
			this.m_SerialNoControlLang.Location = ((System.Drawing.Point)(resources.GetObject("m_SerialNoControlLang.Location")));
			this.m_SerialNoControlLang.Name = "m_SerialNoControlLang";
			this.m_SerialNoControlLang.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_SerialNoControlLang.RightToLeft")));
			this.m_SerialNoControlLang.SeparateString = "-";
			this.m_SerialNoControlLang.Size = ((System.Drawing.Size)(resources.GetObject("m_SerialNoControlLang.Size")));
			this.m_SerialNoControlLang.TabIndex = ((int)(resources.GetObject("m_SerialNoControlLang.TabIndex")));
			this.m_SerialNoControlLang.Visible = ((bool)(resources.GetObject("m_SerialNoControlLang.Visible")));
			// 
			// m_LabelPassword
			// 
			this.m_LabelPassword.AccessibleDescription = resources.GetString("m_LabelPassword.AccessibleDescription");
			this.m_LabelPassword.AccessibleName = resources.GetString("m_LabelPassword.AccessibleName");
			this.m_LabelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelPassword.Anchor")));
			this.m_LabelPassword.AutoSize = ((bool)(resources.GetObject("m_LabelPassword.AutoSize")));
			this.m_LabelPassword.BackColor = System.Drawing.Color.Transparent;
			this.m_LabelPassword.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelPassword.Dock")));
			this.m_LabelPassword.Enabled = ((bool)(resources.GetObject("m_LabelPassword.Enabled")));
			this.m_LabelPassword.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelPassword.Font")));
			this.m_LabelPassword.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelPassword.Image")));
			this.m_LabelPassword.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPassword.ImageAlign")));
			this.m_LabelPassword.ImageIndex = ((int)(resources.GetObject("m_LabelPassword.ImageIndex")));
			this.m_LabelPassword.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelPassword.ImeMode")));
			this.m_LabelPassword.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelPassword.Location")));
			this.m_LabelPassword.Name = "m_LabelPassword";
			this.m_LabelPassword.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelPassword.RightToLeft")));
			this.m_LabelPassword.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelPassword.Size")));
			this.m_LabelPassword.TabIndex = ((int)(resources.GetObject("m_LabelPassword.TabIndex")));
			this.m_LabelPassword.Text = resources.GetString("m_LabelPassword.Text");
			this.m_LabelPassword.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelPassword.TextAlign")));
			this.m_LabelPassword.Visible = ((bool)(resources.GetObject("m_LabelPassword.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.BackColor = System.Drawing.Color.Transparent;
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
			// m_ButtonSetTime
			// 
			this.m_ButtonSetTime.AccessibleDescription = resources.GetString("m_ButtonSetTime.AccessibleDescription");
			this.m_ButtonSetTime.AccessibleName = resources.GetString("m_ButtonSetTime.AccessibleName");
			this.m_ButtonSetTime.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonSetTime.Anchor")));
			this.m_ButtonSetTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetTime.BackgroundImage")));
			this.m_ButtonSetTime.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonSetTime.Dock")));
			this.m_ButtonSetTime.Enabled = ((bool)(resources.GetObject("m_ButtonSetTime.Enabled")));
			this.m_ButtonSetTime.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonSetTime.FlatStyle")));
			this.m_ButtonSetTime.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonSetTime.Font")));
			this.m_ButtonSetTime.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonSetTime.Image")));
			this.m_ButtonSetTime.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetTime.ImageAlign")));
			this.m_ButtonSetTime.ImageIndex = ((int)(resources.GetObject("m_ButtonSetTime.ImageIndex")));
			this.m_ButtonSetTime.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonSetTime.ImeMode")));
			this.m_ButtonSetTime.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonSetTime.Location")));
			this.m_ButtonSetTime.Name = "m_ButtonSetTime";
			this.m_ButtonSetTime.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonSetTime.RightToLeft")));
			this.m_ButtonSetTime.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonSetTime.Size")));
			this.m_ButtonSetTime.TabIndex = ((int)(resources.GetObject("m_ButtonSetTime.TabIndex")));
			this.m_ButtonSetTime.Text = resources.GetString("m_ButtonSetTime.Text");
			this.m_ButtonSetTime.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonSetTime.TextAlign")));
			this.m_ButtonSetTime.Visible = ((bool)(resources.GetObject("m_ButtonSetTime.Visible")));
			this.m_ButtonSetTime.Click += new System.EventHandler(this.m_ButtonSetTime_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.AccessibleDescription = resources.GetString("groupBox2.AccessibleDescription");
			this.groupBox2.AccessibleName = resources.GetString("groupBox2.AccessibleName");
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox2.Anchor")));
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
			this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
			this.groupBox2.BorderThickness = 1F;
			this.groupBox2.Controls.Add(this.bPrinterHWSettingApply);
			this.groupBox2.Controls.Add(this.m_CheckBoxUseYEncoder);
			this.groupBox2.Controls.Add(this.m_CheckBoxFlatBed);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox2.Dock")));
			this.groupBox2.Enabled = ((bool)(resources.GetObject("groupBox2.Enabled")));
			this.groupBox2.Font = ((System.Drawing.Font)(resources.GetObject("groupBox2.Font")));
			this.groupBox2.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.Gold);
			this.groupBox2.GroupImage = null;
			this.groupBox2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox2.ImeMode")));
			this.groupBox2.Location = ((System.Drawing.Point)(resources.GetObject("groupBox2.Location")));
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.PaintGroupBox = false;
			this.groupBox2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox2.RightToLeft")));
			this.groupBox2.RoundCorners = 10;
			this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
			this.groupBox2.ShadowControl = false;
			this.groupBox2.ShadowThickness = 3;
			this.groupBox2.Size = ((System.Drawing.Size)(resources.GetObject("groupBox2.Size")));
			this.groupBox2.TabIndex = ((int)(resources.GetObject("groupBox2.TabIndex")));
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = resources.GetString("groupBox2.Text");
			this.groupBox2.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.groupBox2.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
			this.groupBox2.Visible = ((bool)(resources.GetObject("groupBox2.Visible")));
			// 
			// bPrinterHWSettingApply
			// 
			this.bPrinterHWSettingApply.AccessibleDescription = resources.GetString("bPrinterHWSettingApply.AccessibleDescription");
			this.bPrinterHWSettingApply.AccessibleName = resources.GetString("bPrinterHWSettingApply.AccessibleName");
			this.bPrinterHWSettingApply.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("bPrinterHWSettingApply.Anchor")));
			this.bPrinterHWSettingApply.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bPrinterHWSettingApply.BackgroundImage")));
			this.bPrinterHWSettingApply.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("bPrinterHWSettingApply.Dock")));
			this.bPrinterHWSettingApply.Enabled = ((bool)(resources.GetObject("bPrinterHWSettingApply.Enabled")));
			this.bPrinterHWSettingApply.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("bPrinterHWSettingApply.FlatStyle")));
			this.bPrinterHWSettingApply.Font = ((System.Drawing.Font)(resources.GetObject("bPrinterHWSettingApply.Font")));
			this.bPrinterHWSettingApply.Image = ((System.Drawing.Image)(resources.GetObject("bPrinterHWSettingApply.Image")));
			this.bPrinterHWSettingApply.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bPrinterHWSettingApply.ImageAlign")));
			this.bPrinterHWSettingApply.ImageIndex = ((int)(resources.GetObject("bPrinterHWSettingApply.ImageIndex")));
			this.bPrinterHWSettingApply.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("bPrinterHWSettingApply.ImeMode")));
			this.bPrinterHWSettingApply.Location = ((System.Drawing.Point)(resources.GetObject("bPrinterHWSettingApply.Location")));
			this.bPrinterHWSettingApply.Name = "bPrinterHWSettingApply";
			this.bPrinterHWSettingApply.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("bPrinterHWSettingApply.RightToLeft")));
			this.bPrinterHWSettingApply.Size = ((System.Drawing.Size)(resources.GetObject("bPrinterHWSettingApply.Size")));
			this.bPrinterHWSettingApply.TabIndex = ((int)(resources.GetObject("bPrinterHWSettingApply.TabIndex")));
			this.bPrinterHWSettingApply.Text = resources.GetString("bPrinterHWSettingApply.Text");
			this.bPrinterHWSettingApply.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("bPrinterHWSettingApply.TextAlign")));
			this.bPrinterHWSettingApply.Visible = ((bool)(resources.GetObject("bPrinterHWSettingApply.Visible")));
			this.bPrinterHWSettingApply.Click += new System.EventHandler(this.bPrinterHWSettingApply_Click);
			// 
			// m_CheckBoxUseYEncoder
			// 
			this.m_CheckBoxUseYEncoder.AccessibleDescription = resources.GetString("m_CheckBoxUseYEncoder.AccessibleDescription");
			this.m_CheckBoxUseYEncoder.AccessibleName = resources.GetString("m_CheckBoxUseYEncoder.AccessibleName");
			this.m_CheckBoxUseYEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_CheckBoxUseYEncoder.Anchor")));
			this.m_CheckBoxUseYEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_CheckBoxUseYEncoder.Appearance")));
			this.m_CheckBoxUseYEncoder.BackColor = System.Drawing.Color.Transparent;
			this.m_CheckBoxUseYEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxUseYEncoder.BackgroundImage")));
			this.m_CheckBoxUseYEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.CheckAlign")));
			this.m_CheckBoxUseYEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_CheckBoxUseYEncoder.Dock")));
			this.m_CheckBoxUseYEncoder.Enabled = ((bool)(resources.GetObject("m_CheckBoxUseYEncoder.Enabled")));
			this.m_CheckBoxUseYEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_CheckBoxUseYEncoder.FlatStyle")));
			this.m_CheckBoxUseYEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_CheckBoxUseYEncoder.Font")));
			this.m_CheckBoxUseYEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxUseYEncoder.Image")));
			this.m_CheckBoxUseYEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.ImageAlign")));
			this.m_CheckBoxUseYEncoder.ImageIndex = ((int)(resources.GetObject("m_CheckBoxUseYEncoder.ImageIndex")));
			this.m_CheckBoxUseYEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_CheckBoxUseYEncoder.ImeMode")));
			this.m_CheckBoxUseYEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_CheckBoxUseYEncoder.Location")));
			this.m_CheckBoxUseYEncoder.Name = "m_CheckBoxUseYEncoder";
			this.m_CheckBoxUseYEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_CheckBoxUseYEncoder.RightToLeft")));
			this.m_CheckBoxUseYEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_CheckBoxUseYEncoder.Size")));
			this.m_CheckBoxUseYEncoder.TabIndex = ((int)(resources.GetObject("m_CheckBoxUseYEncoder.TabIndex")));
			this.m_CheckBoxUseYEncoder.Text = resources.GetString("m_CheckBoxUseYEncoder.Text");
			this.m_CheckBoxUseYEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxUseYEncoder.TextAlign")));
			this.m_CheckBoxUseYEncoder.Visible = ((bool)(resources.GetObject("m_CheckBoxUseYEncoder.Visible")));
			// 
			// m_CheckBoxFlatBed
			// 
			this.m_CheckBoxFlatBed.AccessibleDescription = resources.GetString("m_CheckBoxFlatBed.AccessibleDescription");
			this.m_CheckBoxFlatBed.AccessibleName = resources.GetString("m_CheckBoxFlatBed.AccessibleName");
			this.m_CheckBoxFlatBed.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_CheckBoxFlatBed.Anchor")));
			this.m_CheckBoxFlatBed.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_CheckBoxFlatBed.Appearance")));
			this.m_CheckBoxFlatBed.BackColor = System.Drawing.Color.Transparent;
			this.m_CheckBoxFlatBed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxFlatBed.BackgroundImage")));
			this.m_CheckBoxFlatBed.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.CheckAlign")));
			this.m_CheckBoxFlatBed.Checked = true;
			this.m_CheckBoxFlatBed.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_CheckBoxFlatBed.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_CheckBoxFlatBed.Dock")));
			this.m_CheckBoxFlatBed.Enabled = ((bool)(resources.GetObject("m_CheckBoxFlatBed.Enabled")));
			this.m_CheckBoxFlatBed.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_CheckBoxFlatBed.FlatStyle")));
			this.m_CheckBoxFlatBed.Font = ((System.Drawing.Font)(resources.GetObject("m_CheckBoxFlatBed.Font")));
			this.m_CheckBoxFlatBed.Image = ((System.Drawing.Image)(resources.GetObject("m_CheckBoxFlatBed.Image")));
			this.m_CheckBoxFlatBed.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.ImageAlign")));
			this.m_CheckBoxFlatBed.ImageIndex = ((int)(resources.GetObject("m_CheckBoxFlatBed.ImageIndex")));
			this.m_CheckBoxFlatBed.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_CheckBoxFlatBed.ImeMode")));
			this.m_CheckBoxFlatBed.Location = ((System.Drawing.Point)(resources.GetObject("m_CheckBoxFlatBed.Location")));
			this.m_CheckBoxFlatBed.Name = "m_CheckBoxFlatBed";
			this.m_CheckBoxFlatBed.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_CheckBoxFlatBed.RightToLeft")));
			this.m_CheckBoxFlatBed.Size = ((System.Drawing.Size)(resources.GetObject("m_CheckBoxFlatBed.Size")));
			this.m_CheckBoxFlatBed.TabIndex = ((int)(resources.GetObject("m_CheckBoxFlatBed.TabIndex")));
			this.m_CheckBoxFlatBed.Text = resources.GetString("m_CheckBoxFlatBed.Text");
			this.m_CheckBoxFlatBed.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_CheckBoxFlatBed.TextAlign")));
			this.m_CheckBoxFlatBed.Visible = ((bool)(resources.GetObject("m_CheckBoxFlatBed.Visible")));
			// 
			// groupBox3
			// 
			this.groupBox3.AccessibleDescription = resources.GetString("groupBox3.AccessibleDescription");
			this.groupBox3.AccessibleName = resources.GetString("groupBox3.AccessibleName");
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox3.Anchor")));
			this.groupBox3.BackColor = System.Drawing.Color.Transparent;
			this.groupBox3.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
			this.groupBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox3.BackgroundImage")));
			this.groupBox3.BorderThickness = 1F;
			this.groupBox3.Controls.Add(this.m_RadioButtonServoEncoder);
			this.groupBox3.Controls.Add(this.m_RadioButtonEncoder);
			this.groupBox3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox3.Dock")));
			this.groupBox3.Enabled = ((bool)(resources.GetObject("groupBox3.Enabled")));
			this.groupBox3.Font = ((System.Drawing.Font)(resources.GetObject("groupBox3.Font")));
			this.groupBox3.GradientColors = new BYHXPrinterManager.Style(System.Drawing.SystemColors.Control, System.Drawing.Color.Gold);
			this.groupBox3.GroupImage = null;
			this.groupBox3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox3.ImeMode")));
			this.groupBox3.Location = ((System.Drawing.Point)(resources.GetObject("groupBox3.Location")));
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.PaintGroupBox = false;
			this.groupBox3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox3.RightToLeft")));
			this.groupBox3.RoundCorners = 10;
			this.groupBox3.ShadowColor = System.Drawing.Color.DarkGray;
			this.groupBox3.ShadowControl = false;
			this.groupBox3.ShadowThickness = 3;
			this.groupBox3.Size = ((System.Drawing.Size)(resources.GetObject("groupBox3.Size")));
			this.groupBox3.TabIndex = ((int)(resources.GetObject("groupBox3.TabIndex")));
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = resources.GetString("groupBox3.Text");
			this.groupBox3.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
			this.groupBox3.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
			this.groupBox3.Visible = ((bool)(resources.GetObject("groupBox3.Visible")));
			// 
			// m_RadioButtonServoEncoder
			// 
			this.m_RadioButtonServoEncoder.AccessibleDescription = resources.GetString("m_RadioButtonServoEncoder.AccessibleDescription");
			this.m_RadioButtonServoEncoder.AccessibleName = resources.GetString("m_RadioButtonServoEncoder.AccessibleName");
			this.m_RadioButtonServoEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_RadioButtonServoEncoder.Anchor")));
			this.m_RadioButtonServoEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_RadioButtonServoEncoder.Appearance")));
			this.m_RadioButtonServoEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonServoEncoder.BackgroundImage")));
			this.m_RadioButtonServoEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.CheckAlign")));
			this.m_RadioButtonServoEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_RadioButtonServoEncoder.Dock")));
			this.m_RadioButtonServoEncoder.Enabled = ((bool)(resources.GetObject("m_RadioButtonServoEncoder.Enabled")));
			this.m_RadioButtonServoEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_RadioButtonServoEncoder.FlatStyle")));
			this.m_RadioButtonServoEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_RadioButtonServoEncoder.Font")));
			this.m_RadioButtonServoEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonServoEncoder.Image")));
			this.m_RadioButtonServoEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.ImageAlign")));
			this.m_RadioButtonServoEncoder.ImageIndex = ((int)(resources.GetObject("m_RadioButtonServoEncoder.ImageIndex")));
			this.m_RadioButtonServoEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_RadioButtonServoEncoder.ImeMode")));
			this.m_RadioButtonServoEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_RadioButtonServoEncoder.Location")));
			this.m_RadioButtonServoEncoder.Name = "m_RadioButtonServoEncoder";
			this.m_RadioButtonServoEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_RadioButtonServoEncoder.RightToLeft")));
			this.m_RadioButtonServoEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_RadioButtonServoEncoder.Size")));
			this.m_RadioButtonServoEncoder.TabIndex = ((int)(resources.GetObject("m_RadioButtonServoEncoder.TabIndex")));
			this.m_RadioButtonServoEncoder.Text = resources.GetString("m_RadioButtonServoEncoder.Text");
			this.m_RadioButtonServoEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonServoEncoder.TextAlign")));
			this.m_RadioButtonServoEncoder.Visible = ((bool)(resources.GetObject("m_RadioButtonServoEncoder.Visible")));
			// 
			// m_RadioButtonEncoder
			// 
			this.m_RadioButtonEncoder.AccessibleDescription = resources.GetString("m_RadioButtonEncoder.AccessibleDescription");
			this.m_RadioButtonEncoder.AccessibleName = resources.GetString("m_RadioButtonEncoder.AccessibleName");
			this.m_RadioButtonEncoder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_RadioButtonEncoder.Anchor")));
			this.m_RadioButtonEncoder.Appearance = ((System.Windows.Forms.Appearance)(resources.GetObject("m_RadioButtonEncoder.Appearance")));
			this.m_RadioButtonEncoder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonEncoder.BackgroundImage")));
			this.m_RadioButtonEncoder.CheckAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.CheckAlign")));
			this.m_RadioButtonEncoder.Checked = true;
			this.m_RadioButtonEncoder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_RadioButtonEncoder.Dock")));
			this.m_RadioButtonEncoder.Enabled = ((bool)(resources.GetObject("m_RadioButtonEncoder.Enabled")));
			this.m_RadioButtonEncoder.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_RadioButtonEncoder.FlatStyle")));
			this.m_RadioButtonEncoder.Font = ((System.Drawing.Font)(resources.GetObject("m_RadioButtonEncoder.Font")));
			this.m_RadioButtonEncoder.Image = ((System.Drawing.Image)(resources.GetObject("m_RadioButtonEncoder.Image")));
			this.m_RadioButtonEncoder.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.ImageAlign")));
			this.m_RadioButtonEncoder.ImageIndex = ((int)(resources.GetObject("m_RadioButtonEncoder.ImageIndex")));
			this.m_RadioButtonEncoder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_RadioButtonEncoder.ImeMode")));
			this.m_RadioButtonEncoder.Location = ((System.Drawing.Point)(resources.GetObject("m_RadioButtonEncoder.Location")));
			this.m_RadioButtonEncoder.Name = "m_RadioButtonEncoder";
			this.m_RadioButtonEncoder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_RadioButtonEncoder.RightToLeft")));
			this.m_RadioButtonEncoder.Size = ((System.Drawing.Size)(resources.GetObject("m_RadioButtonEncoder.Size")));
			this.m_RadioButtonEncoder.TabIndex = ((int)(resources.GetObject("m_RadioButtonEncoder.TabIndex")));
			this.m_RadioButtonEncoder.TabStop = true;
			this.m_RadioButtonEncoder.Text = resources.GetString("m_RadioButtonEncoder.Text");
			this.m_RadioButtonEncoder.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_RadioButtonEncoder.TextAlign")));
			this.m_RadioButtonEncoder.Visible = ((bool)(resources.GetObject("m_RadioButtonEncoder.Visible")));
			// 
			// SystemInfoSetting
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.groupBoxPassword);
			this.Controls.Add(this.groupBox2);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "SystemInfoSetting";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Load += new System.EventHandler(this.SystemInfoSetting_Load);
			this.groupBoxPassword.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion

        private BYHXPrinterManager.GradientControls.Grouper groupBoxPassword;
        private System.Windows.Forms.Button m_ButtonSetLang;
        private BYHXControls.SerialNoControl m_SerialNoControlTime;
        private BYHXControls.SerialNoControl m_SerialNoControlLang;
        private System.Windows.Forms.Label m_LabelPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_ButtonSetTime;
        private BYHXPrinterManager.GradientControls.Grouper groupBox2;
        private System.Windows.Forms.CheckBox m_CheckBoxUseYEncoder;
        private System.Windows.Forms.CheckBox m_CheckBoxFlatBed;
        private BYHXPrinterManager.GradientControls.Grouper groupBox3;
        private System.Windows.Forms.RadioButton m_RadioButtonServoEncoder;
        private Button bPrinterHWSettingApply;
        private System.Windows.Forms.RadioButton m_RadioButtonEncoder;

        public event EventHandler HWSettingApplyClicked;
        public SystemInfoSetting()
        {
            InitializeComponent();

#if LIYUUSB
            this.groupBox2.Visible = false;
#endif
        }

        #region PrinterHWSetting

        public void PrinterHW_SetLineEncoder(bool bEncoder)
        {
            m_RadioButtonEncoder.Checked = bEncoder;
            m_RadioButtonServoEncoder.Checked = !bEncoder;
        }
        public bool PrinterHW_GetLineEncoder()
        {
            bool LineEncoder = m_RadioButtonEncoder.Checked;
            return LineEncoder;
        }
        public void PrinterHW_SetFlat(bool bEncoder)
        {
            m_CheckBoxFlatBed.Checked = bEncoder;
        }
        public bool PrinterHW_GetFlat()
        {
            bool LineEncoder = m_CheckBoxFlatBed.Checked;
            return LineEncoder;
        }
        public void PrinterHW_SetUseYEncoder(bool bEncoder)
        {
            m_CheckBoxUseYEncoder.Checked = bEncoder;
        }
        public bool PrinterHW_GetUseYEncoder()
        {
            bool LineEncoder = m_CheckBoxUseYEncoder.Checked;
            return LineEncoder;
        }

        public void PrinterHW_OnPrinterPropertyChange(SPrinterProperty sp)
        {
            if (sp.bSupportDoubleMachine)
            {
                m_CheckBoxFlatBed.Visible = true;
            }
            else
            {
                m_CheckBoxFlatBed.Visible = false;
            }
            if (sp.bSupportYEncoder)
            {
                m_CheckBoxUseYEncoder.Visible = true;
            }
            else
            {
                m_CheckBoxUseYEncoder.Visible = false;
            }

        }

        #endregion

        #region PasswordForm

        private void SetPassword(string strMainPwd, int bLang)
        {
            int iRet = 0;
            // 

            //validCheck should add here
            if (strMainPwd != null && strMainPwd.Length != CoreConst.MAX_PASSWORD_LEN)
            {
                string sFilterString = strMainPwd.Replace("-", null);
                iRet = CoreInterface.SetPassword(sFilterString.ToUpper(), sFilterString.Length, (ushort)BoardEnum.CoreBoard, bLang);
            }
            if (iRet == 0)
            {
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SetPasswordFail),
                    ResString.GetProductName(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }
        }

        public void PasswordTabPage_Load()
        {
            string strTemp = "";
            int passLen = CoreConst.MAX_PASSWORD_LEN;
            byte[] info = new byte[passLen];
            int iRet = CoreInterface.GetPassword(info, ref passLen, (ushort)BoardEnum.CoreBoard, 0);
            if (iRet != 0)
            {
                strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
            }
            else
            {
                strTemp = "";
            }
            m_SerialNoControlTime.SetText(strTemp);
            iRet = CoreInterface.GetPassword(info, ref passLen, (ushort)BoardEnum.CoreBoard, 1);
            if (iRet != 0)
            {
                strTemp = System.Text.Encoding.ASCII.GetString(info, 0, passLen);
            }
            else
            {
                strTemp = "";
            }
            m_SerialNoControlLang.SetText(strTemp);

        }

        #endregion

        private void SystemInfoSetting_Load(object sender, EventArgs e)
        {
            if(!PubFunc.IsInDesignMode())
                PasswordTabPage_Load();
        }

        private void m_ButtonSetTime_Click(object sender, EventArgs e)
        {
            try
            {
                string strMainPwd = m_SerialNoControlTime.GetText();
                SetPassword(strMainPwd, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ButtonSetLang_Click(object sender, EventArgs e)
        {
            try
            {
                string strMainPwd = m_SerialNoControlLang.GetText();
                SetPassword(strMainPwd, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bPrinterHWSettingApply_Click(object sender, EventArgs e)
        {
            if (this.HWSettingApplyClicked != null)
                this.HWSettingApplyClicked(sender,e);
        }
    }
}
