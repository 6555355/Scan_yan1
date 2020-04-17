namespace BYHXPrinterManager.Setting
{
    partial class NktDyjUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NktDyjUserControl));
            this.labelBeltSpeed = new System.Windows.Forms.Label();
            this.numBeltSpeed = new System.Windows.Forms.NumericUpDown();
            this.numFeedSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelFeedSpeed = new System.Windows.Forms.Label();
            this.numStepSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelStepSpeed = new System.Windows.Forms.Label();
            this.numDetectorOffset = new System.Windows.Forms.NumericUpDown();
            this.labelDetectorOffset = new System.Windows.Forms.Label();
            this.buttonRead = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numBeltSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFeedSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDetectorOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBeltSpeed
            // 
            resources.ApplyResources(this.labelBeltSpeed, "labelBeltSpeed");
            this.labelBeltSpeed.Name = "labelBeltSpeed";
            // 
            // numBeltSpeed
            // 
            resources.ApplyResources(this.numBeltSpeed, "numBeltSpeed");
            this.numBeltSpeed.Name = "numBeltSpeed";
            // 
            // numFeedSpeed
            // 
            resources.ApplyResources(this.numFeedSpeed, "numFeedSpeed");
            this.numFeedSpeed.Name = "numFeedSpeed";
            // 
            // labelFeedSpeed
            // 
            resources.ApplyResources(this.labelFeedSpeed, "labelFeedSpeed");
            this.labelFeedSpeed.Name = "labelFeedSpeed";
            // 
            // numStepSpeed
            // 
            resources.ApplyResources(this.numStepSpeed, "numStepSpeed");
            this.numStepSpeed.Name = "numStepSpeed";
            // 
            // labelStepSpeed
            // 
            resources.ApplyResources(this.labelStepSpeed, "labelStepSpeed");
            this.labelStepSpeed.Name = "labelStepSpeed";
            // 
            // numDetectorOffset
            // 
            resources.ApplyResources(this.numDetectorOffset, "numDetectorOffset");
            this.numDetectorOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDetectorOffset.Name = "numDetectorOffset";
            // 
            // labelDetectorOffset
            // 
            resources.ApplyResources(this.labelDetectorOffset, "labelDetectorOffset");
            this.labelDetectorOffset.Name = "labelDetectorOffset";
            // 
            // buttonRead
            // 
            resources.ApplyResources(this.buttonRead, "buttonRead");
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numStepSpeed);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.labelBeltSpeed);
            this.groupBox1.Controls.Add(this.buttonRead);
            this.groupBox1.Controls.Add(this.numBeltSpeed);
            this.groupBox1.Controls.Add(this.numDetectorOffset);
            this.groupBox1.Controls.Add(this.labelFeedSpeed);
            this.groupBox1.Controls.Add(this.labelDetectorOffset);
            this.groupBox1.Controls.Add(this.numFeedSpeed);
            this.groupBox1.Controls.Add(this.labelStepSpeed);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // NktDyjUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "NktDyjUserControl";
            this.Load += new System.EventHandler(this.NktDyjUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numBeltSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFeedSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDetectorOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelBeltSpeed;
        private System.Windows.Forms.NumericUpDown numBeltSpeed;
        private System.Windows.Forms.NumericUpDown numFeedSpeed;
        private System.Windows.Forms.Label labelFeedSpeed;
        private System.Windows.Forms.NumericUpDown numStepSpeed;
        private System.Windows.Forms.Label labelStepSpeed;
        private System.Windows.Forms.NumericUpDown numDetectorOffset;
        private System.Windows.Forms.Label labelDetectorOffset;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
