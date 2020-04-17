namespace BYHXPrinterManager.Setting
{
    partial class DOCANManualCleanSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DOCANManualCleanSetting));
            this.groupBox_CleanParameter = new System.Windows.Forms.GroupBox();
            this.button_trigger = new System.Windows.Forms.Button();
            this.button_set = new System.Windows.Forms.Button();
            this.numericUpDown_ZSensorCurVal = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_PressInkTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ZSensorErrRange = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_zCLeanPos = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_xStartPos = new System.Windows.Forms.NumericUpDown();
            this.label_PressInkTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_zCLeanPos = new System.Windows.Forms.Label();
            this.label_ZSensorErrRange = new System.Windows.Forms.Label();
            this.label_xStartPos = new System.Windows.Forms.Label();
            this.groupBox_CleanParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ZSensorCurVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PressInkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ZSensorErrRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_zCLeanPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xStartPos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_CleanParameter
            // 
            resources.ApplyResources(this.groupBox_CleanParameter, "groupBox_CleanParameter");
            this.groupBox_CleanParameter.Controls.Add(this.button_trigger);
            this.groupBox_CleanParameter.Controls.Add(this.button_set);
            this.groupBox_CleanParameter.Controls.Add(this.numericUpDown_ZSensorCurVal);
            this.groupBox_CleanParameter.Controls.Add(this.numericUpDown_PressInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.numericUpDown_ZSensorErrRange);
            this.groupBox_CleanParameter.Controls.Add(this.numericUpDown_zCLeanPos);
            this.groupBox_CleanParameter.Controls.Add(this.numericUpDown_xStartPos);
            this.groupBox_CleanParameter.Controls.Add(this.label_PressInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.label4);
            this.groupBox_CleanParameter.Controls.Add(this.label_zCLeanPos);
            this.groupBox_CleanParameter.Controls.Add(this.label_ZSensorErrRange);
            this.groupBox_CleanParameter.Controls.Add(this.label_xStartPos);
            this.groupBox_CleanParameter.Name = "groupBox_CleanParameter";
            this.groupBox_CleanParameter.TabStop = false;
            // 
            // button_trigger
            // 
            resources.ApplyResources(this.button_trigger, "button_trigger");
            this.button_trigger.Name = "button_trigger";
            this.button_trigger.UseVisualStyleBackColor = true;
            this.button_trigger.Click += new System.EventHandler(this.button_trigger_Click);
            // 
            // button_set
            // 
            resources.ApplyResources(this.button_set, "button_set");
            this.button_set.Name = "button_set";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // numericUpDown_ZSensorCurVal
            // 
            resources.ApplyResources(this.numericUpDown_ZSensorCurVal, "numericUpDown_ZSensorCurVal");
            this.numericUpDown_ZSensorCurVal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_ZSensorCurVal.Name = "numericUpDown_ZSensorCurVal";
            this.numericUpDown_ZSensorCurVal.ReadOnly = true;
            // 
            // numericUpDown_PressInkTime
            // 
            resources.ApplyResources(this.numericUpDown_PressInkTime, "numericUpDown_PressInkTime");
            this.numericUpDown_PressInkTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_PressInkTime.Name = "numericUpDown_PressInkTime";
            // 
            // numericUpDown_ZSensorErrRange
            // 
            resources.ApplyResources(this.numericUpDown_ZSensorErrRange, "numericUpDown_ZSensorErrRange");
            this.numericUpDown_ZSensorErrRange.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_ZSensorErrRange.Name = "numericUpDown_ZSensorErrRange";
            // 
            // numericUpDown_zCLeanPos
            // 
            resources.ApplyResources(this.numericUpDown_zCLeanPos, "numericUpDown_zCLeanPos");
            this.numericUpDown_zCLeanPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_zCLeanPos.Name = "numericUpDown_zCLeanPos";
            // 
            // numericUpDown_xStartPos
            // 
            resources.ApplyResources(this.numericUpDown_xStartPos, "numericUpDown_xStartPos");
            this.numericUpDown_xStartPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_xStartPos.Name = "numericUpDown_xStartPos";
            // 
            // label_PressInkTime
            // 
            resources.ApplyResources(this.label_PressInkTime, "label_PressInkTime");
            this.label_PressInkTime.Name = "label_PressInkTime";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label_zCLeanPos
            // 
            resources.ApplyResources(this.label_zCLeanPos, "label_zCLeanPos");
            this.label_zCLeanPos.Name = "label_zCLeanPos";
            // 
            // label_ZSensorErrRange
            // 
            resources.ApplyResources(this.label_ZSensorErrRange, "label_ZSensorErrRange");
            this.label_ZSensorErrRange.Name = "label_ZSensorErrRange";
            // 
            // label_xStartPos
            // 
            resources.ApplyResources(this.label_xStartPos, "label_xStartPos");
            this.label_xStartPos.Name = "label_xStartPos";
            // 
            // DOCANManualCleanSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_CleanParameter);
            this.Name = "DOCANManualCleanSetting";
            this.groupBox_CleanParameter.ResumeLayout(false);
            this.groupBox_CleanParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ZSensorCurVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PressInkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ZSensorErrRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_zCLeanPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_xStartPos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_CleanParameter;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.NumericUpDown numericUpDown_ZSensorCurVal;
        private System.Windows.Forms.NumericUpDown numericUpDown_PressInkTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_ZSensorErrRange;
        private System.Windows.Forms.NumericUpDown numericUpDown_zCLeanPos;
        private System.Windows.Forms.NumericUpDown numericUpDown_xStartPos;
        private System.Windows.Forms.Label label_PressInkTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_zCLeanPos;
        private System.Windows.Forms.Label label_ZSensorErrRange;
        private System.Windows.Forms.Label label_xStartPos;
        private System.Windows.Forms.Button button_trigger;

    }
}
