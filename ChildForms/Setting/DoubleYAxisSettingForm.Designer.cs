namespace BYHXPrinterManager.Setting
{
    partial class DoubleYAxisSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoubleYAxisSettingForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.buttonClose = new System.Windows.Forms.Button();
            this.doubleYAxisSetting1 = new BYHXPrinterManager.Setting.DoubleYAxisSetting();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // doubleYAxisSetting1
            // 
            resources.ApplyResources(this.doubleYAxisSetting1, "doubleYAxisSetting1");
            this.doubleYAxisSetting1.Divider = false;
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.doubleYAxisSetting1.GradientColors = style1;
            this.doubleYAxisSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.doubleYAxisSetting1.GrouperTitleStyle = null;
            this.doubleYAxisSetting1.Name = "doubleYAxisSetting1";
            // 
            // DoubleYAxisSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.doubleYAxisSetting1);
            this.Name = "DoubleYAxisSettingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleYAxisSetting doubleYAxisSetting1;
        private System.Windows.Forms.Button buttonClose;
    }
}