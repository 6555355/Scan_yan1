namespace BYHXPrinterManager.Setting
{
    partial class HLCCleanSetting
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HLCCleanSetting));
            this.groupBox_CleanParameter = new System.Windows.Forms.GroupBox();
            this.numYCleanSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelYCleanSpeed = new System.Windows.Forms.Label();
            this.numPressInkTime = new System.Windows.Forms.NumericUpDown();
            this.labelPressInkTime = new System.Windows.Forms.Label();
            this.button_set = new System.Windows.Forms.Button();
            this.numZCleanPos = new System.Windows.Forms.NumericUpDown();
            this.numYCleanPos = new System.Windows.Forms.NumericUpDown();
            this.numXPressInkPos = new System.Windows.Forms.NumericUpDown();
            this.labelZCleanPos = new System.Windows.Forms.Label();
            this.labelYCleanPos = new System.Windows.Forms.Label();
            this.labelXPressInkPos = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numRecoveryInkTime = new System.Windows.Forms.NumericUpDown();
            this.labelRecoveryInkTime = new System.Windows.Forms.Label();
            this.groupBox_CleanParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYCleanSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPressInkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZCleanPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYCleanPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXPressInkPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRecoveryInkTime)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_CleanParameter
            // 
            this.groupBox_CleanParameter.Controls.Add(this.numRecoveryInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.labelRecoveryInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.numYCleanSpeed);
            this.groupBox_CleanParameter.Controls.Add(this.labelYCleanSpeed);
            this.groupBox_CleanParameter.Controls.Add(this.numPressInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.labelPressInkTime);
            this.groupBox_CleanParameter.Controls.Add(this.button_set);
            this.groupBox_CleanParameter.Controls.Add(this.numZCleanPos);
            this.groupBox_CleanParameter.Controls.Add(this.numYCleanPos);
            this.groupBox_CleanParameter.Controls.Add(this.numXPressInkPos);
            this.groupBox_CleanParameter.Controls.Add(this.labelZCleanPos);
            this.groupBox_CleanParameter.Controls.Add(this.labelYCleanPos);
            this.groupBox_CleanParameter.Controls.Add(this.labelXPressInkPos);
            resources.ApplyResources(this.groupBox_CleanParameter, "groupBox_CleanParameter");
            this.groupBox_CleanParameter.Name = "groupBox_CleanParameter";
            this.groupBox_CleanParameter.TabStop = false;
            // 
            // numYCleanSpeed
            // 
            resources.ApplyResources(this.numYCleanSpeed, "numYCleanSpeed");
            this.numYCleanSpeed.Name = "numYCleanSpeed";
            // 
            // labelYCleanSpeed
            // 
            resources.ApplyResources(this.labelYCleanSpeed, "labelYCleanSpeed");
            this.labelYCleanSpeed.Name = "labelYCleanSpeed";
            // 
            // numPressInkTime
            // 
            resources.ApplyResources(this.numPressInkTime, "numPressInkTime");
            this.numPressInkTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPressInkTime.Name = "numPressInkTime";
            // 
            // labelPressInkTime
            // 
            resources.ApplyResources(this.labelPressInkTime, "labelPressInkTime");
            this.labelPressInkTime.Name = "labelPressInkTime";
            // 
            // button_set
            // 
            resources.ApplyResources(this.button_set, "button_set");
            this.button_set.Name = "button_set";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.button_set_Click);
            // 
            // numZCleanPos
            // 
            resources.ApplyResources(this.numZCleanPos, "numZCleanPos");
            this.numZCleanPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numZCleanPos.Name = "numZCleanPos";
            // 
            // numYCleanPos
            // 
            resources.ApplyResources(this.numYCleanPos, "numYCleanPos");
            this.numYCleanPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numYCleanPos.Name = "numYCleanPos";
            // 
            // numXPressInkPos
            // 
            resources.ApplyResources(this.numXPressInkPos, "numXPressInkPos");
            this.numXPressInkPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numXPressInkPos.Name = "numXPressInkPos";
            // 
            // labelZCleanPos
            // 
            resources.ApplyResources(this.labelZCleanPos, "labelZCleanPos");
            this.labelZCleanPos.Name = "labelZCleanPos";
            // 
            // labelYCleanPos
            // 
            resources.ApplyResources(this.labelYCleanPos, "labelYCleanPos");
            this.labelYCleanPos.Name = "labelYCleanPos";
            // 
            // labelXPressInkPos
            // 
            resources.ApplyResources(this.labelXPressInkPos, "labelXPressInkPos");
            this.labelXPressInkPos.Name = "labelXPressInkPos";
            // 
            // numRecoveryInkTime
            // 
            resources.ApplyResources(this.numRecoveryInkTime, "numRecoveryInkTime");
            this.numRecoveryInkTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numRecoveryInkTime.Name = "numRecoveryInkTime";
            // 
            // labelRecoveryInkTime
            // 
            resources.ApplyResources(this.labelRecoveryInkTime, "labelRecoveryInkTime");
            this.labelRecoveryInkTime.Name = "labelRecoveryInkTime";
            // 
            // HLCCleanSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_CleanParameter);
            this.Name = "HLCCleanSetting";
            this.groupBox_CleanParameter.ResumeLayout(false);
            this.groupBox_CleanParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYCleanSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPressInkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZCleanPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYCleanPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXPressInkPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRecoveryInkTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_CleanParameter;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.NumericUpDown numZCleanPos;
        private System.Windows.Forms.NumericUpDown numYCleanPos;
        private System.Windows.Forms.NumericUpDown numXPressInkPos;
        private System.Windows.Forms.Label labelZCleanPos;
        private System.Windows.Forms.Label labelYCleanPos;
        private System.Windows.Forms.Label labelXPressInkPos;
        private System.Windows.Forms.NumericUpDown numPressInkTime;
        private System.Windows.Forms.Label labelPressInkTime;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numYCleanSpeed;
        private System.Windows.Forms.Label labelYCleanSpeed;
        private System.Windows.Forms.NumericUpDown numRecoveryInkTime;
        private System.Windows.Forms.Label labelRecoveryInkTime;

    }
}
