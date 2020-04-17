namespace BYHXPrinterManager.Setting
{
    partial class WorkposSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkposSetting));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxEnable2 = new System.Windows.Forms.CheckBox();
            this.cbxEnable1 = new System.Windows.Forms.CheckBox();
            this.numPos2 = new System.Windows.Forms.NumericUpDown();
            this.lblPos2 = new System.Windows.Forms.Label();
            this.numPos1 = new System.Windows.Forms.NumericUpDown();
            this.lblPos1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPos2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPos1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxEnable2);
            this.groupBox1.Controls.Add(this.cbxEnable1);
            this.groupBox1.Controls.Add(this.numPos2);
            this.groupBox1.Controls.Add(this.lblPos2);
            this.groupBox1.Controls.Add(this.numPos1);
            this.groupBox1.Controls.Add(this.lblPos1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbxEnable2
            // 
            resources.ApplyResources(this.cbxEnable2, "cbxEnable2");
            this.cbxEnable2.Name = "cbxEnable2";
            this.cbxEnable2.UseVisualStyleBackColor = true;
            // 
            // cbxEnable1
            // 
            resources.ApplyResources(this.cbxEnable1, "cbxEnable1");
            this.cbxEnable1.Name = "cbxEnable1";
            this.cbxEnable1.UseVisualStyleBackColor = true;
            // 
            // numPos2
            // 
            resources.ApplyResources(this.numPos2, "numPos2");
            this.numPos2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPos2.Name = "numPos2";
            // 
            // lblPos2
            // 
            resources.ApplyResources(this.lblPos2, "lblPos2");
            this.lblPos2.Name = "lblPos2";
            // 
            // numPos1
            // 
            resources.ApplyResources(this.numPos1, "numPos1");
            this.numPos1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPos1.Name = "numPos1";
            // 
            // lblPos1
            // 
            resources.ApplyResources(this.lblPos1, "lblPos1");
            this.lblPos1.Name = "lblPos1";
            // 
            // WorkposSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "WorkposSetting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPos2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPos1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown numPos1;
        private System.Windows.Forms.Label lblPos1;
        public System.Windows.Forms.NumericUpDown numPos2;
        private System.Windows.Forms.Label lblPos2;
        private System.Windows.Forms.CheckBox cbxEnable2;
        private System.Windows.Forms.CheckBox cbxEnable1;

    }
}
