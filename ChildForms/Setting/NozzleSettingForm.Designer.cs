namespace BYHXPrinterManager.Setting
{
    partial class NozzleSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NozzleSettingForm));
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.nozzleXYoffset1 = new BYHXPrinterManager.Setting.NozzleXYoffset();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonCancel);
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            // 
            // nozzleXYoffset1
            // 
            resources.ApplyResources(this.nozzleXYoffset1, "nozzleXYoffset1");
            this.nozzleXYoffset1.Name = "nozzleXYoffset1";
            // 
            // NozzleSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nozzleXYoffset1);
            this.Controls.Add(this.dividerPanel1);
            this.Name = "NozzleSettingForm";
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BYHXPrinterManager.Setting.NozzleXYoffset nozzleXYoffset1;
        private DividerPanel.DividerPanel dividerPanel1;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonOK;
    }
}