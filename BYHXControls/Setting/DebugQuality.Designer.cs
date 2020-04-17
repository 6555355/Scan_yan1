namespace BYHXPrinterManager.Setting
{
    partial class DebugQuality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugQuality));
            this.m_GroupBoxVer = new System.Windows.Forms.GroupBox();
            this.m_LabelVerValue = new System.Windows.Forms.Label();
            this.m_LabelVerHead = new System.Windows.Forms.Label();
            this.m_LabelVerSample = new System.Windows.Forms.Label();
            this.m_TextBoxVerSample = new System.Windows.Forms.NumericUpDown();
            this.m_GroupBoxVer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxVerSample)).BeginInit();
            this.SuspendLayout();
            // 
            // m_GroupBoxVer
            // 
            this.m_GroupBoxVer.Controls.Add(this.m_TextBoxVerSample);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerValue);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerHead);
            this.m_GroupBoxVer.Controls.Add(this.m_LabelVerSample);
            resources.ApplyResources(this.m_GroupBoxVer, "m_GroupBoxVer");
            this.m_GroupBoxVer.Name = "m_GroupBoxVer";
            this.m_GroupBoxVer.TabStop = false;
            // 
            // m_LabelVerValue
            // 
            resources.ApplyResources(this.m_LabelVerValue, "m_LabelVerValue");
            this.m_LabelVerValue.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerValue.Name = "m_LabelVerValue";
            // 
            // m_LabelVerHead
            // 
            resources.ApplyResources(this.m_LabelVerHead, "m_LabelVerHead");
            this.m_LabelVerHead.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerHead.Name = "m_LabelVerHead";
            // 
            // m_LabelVerSample
            // 
            resources.ApplyResources(this.m_LabelVerSample, "m_LabelVerSample");
            this.m_LabelVerSample.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelVerSample.Name = "m_LabelVerSample";
            // 
            // m_TextBoxVerSample
            // 
            this.m_TextBoxVerSample.DecimalPlaces = 2;
            resources.ApplyResources(this.m_TextBoxVerSample, "m_TextBoxVerSample");
            this.m_TextBoxVerSample.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_TextBoxVerSample.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.m_TextBoxVerSample.Name = "m_TextBoxVerSample";
            // 
            // DebugQuality
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_GroupBoxVer);
            this.Name = "DebugQuality";
            this.m_GroupBoxVer.ResumeLayout(false);
            this.m_GroupBoxVer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_TextBoxVerSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_GroupBoxVer;
        private System.Windows.Forms.Label m_LabelVerValue;
        private System.Windows.Forms.Label m_LabelVerHead;
        private System.Windows.Forms.Label m_LabelVerSample;
        private System.Windows.Forms.NumericUpDown m_TextBoxVerSample;
    }
}
