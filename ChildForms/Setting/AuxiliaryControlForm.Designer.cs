namespace BYHXPrinterManager.Setting
{
    partial class AuxiliaryControlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuxiliaryControlForm));
            this.maintenanceSystemSetting1 = new BYHXPrinterManager.Setting.MaintenanceSystemSetting();
            this.SuspendLayout();
            // 
            // maintenanceSystemSetting1
            // 
            resources.ApplyResources(this.maintenanceSystemSetting1, "maintenanceSystemSetting1");
            this.maintenanceSystemSetting1.GrouperTitleStyle = null;
            this.maintenanceSystemSetting1.Name = "maintenanceSystemSetting1";
            // 
            // AuxiliaryControlForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.maintenanceSystemSetting1);
            this.Name = "AuxiliaryControlForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MaintenanceSystemSetting maintenanceSystemSetting1;
    }
}