namespace BYHXPrinterManager.Setting
{
    partial class UVSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UVSettingForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.uvSetting1 = new BYHXPrinterManager.Setting.UVSetting();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.zAixsSetting1 = new BYHXPrinterManager.Setting.ZAixsSetting();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_ButtonCancel);
            this.panel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // m_ButtonCancel
            // 
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // m_ButtonOK
            // 
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uvSetting1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // uvSetting1
            // 
            this.uvSetting1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.uvSetting1, "uvSetting1");
            this.uvSetting1.Name = "uvSetting1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.zAixsSetting1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // zAixsSetting1
            // 
            this.zAixsSetting1.Divider = false;
            resources.ApplyResources(this.zAixsSetting1, "zAixsSetting1");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.zAixsSetting1.GradientColors = style1;
            this.zAixsSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.zAixsSetting1.GrouperTitleStyle = null;
            this.zAixsSetting1.HasMeasured = false;
            this.zAixsSetting1.IsMeasureBeforePrint = false;
            this.zAixsSetting1.Name = "zAixsSetting1";
            // 
            // UVSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UVSettingForm";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.UVSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private UVSetting uvSetting1;
        private ZAixsSetting zAixsSetting1;
    }
}