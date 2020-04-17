namespace BYHXPrinterManager.Main
{
    partial class AboutWaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWaveForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmb_WaveformIndex = new System.Windows.Forms.ComboBox();
            this.label_WaveIndex = new System.Windows.Forms.Label();
            this.comb_Index = new System.Windows.Forms.ComboBox();
            this.grouper1 = new BYHXPrinterManager.GradientControls.Grouper();
            this.lstbx_HeadType = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Speed = new System.Windows.Forms.ComboBox();
            this.label_InkType = new System.Windows.Forms.Label();
            this.cmb_Ink = new System.Windows.Forms.ComboBox();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmb_WaveformIndex
            // 
            resources.ApplyResources(this.cmb_WaveformIndex, "cmb_WaveformIndex");
            this.cmb_WaveformIndex.AllowDrop = true;
            this.cmb_WaveformIndex.FormattingEnabled = true;
            this.cmb_WaveformIndex.Name = "cmb_WaveformIndex";
            this.cmb_WaveformIndex.SelectedIndexChanged += new System.EventHandler(this.comb_SelectedIndexChanged);
            // 
            // label_WaveIndex
            // 
            resources.ApplyResources(this.label_WaveIndex, "label_WaveIndex");
            this.label_WaveIndex.Name = "label_WaveIndex";
            // 
            // comb_Index
            // 
            resources.ApplyResources(this.comb_Index, "comb_Index");
            this.comb_Index.FormattingEnabled = true;
            this.comb_Index.Name = "comb_Index";
            // 
            // grouper1
            // 
            resources.ApplyResources(this.grouper1, "grouper1");
            this.grouper1.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.None;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lstbx_HeadType);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.cmb_Speed);
            this.grouper1.Controls.Add(this.label_InkType);
            this.grouper1.Controls.Add(this.cmb_Ink);
            this.grouper1.GradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.grouper1.GroupImage = null;
            this.grouper1.Name = "grouper1";
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 5;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.TabStop = false;
            this.grouper1.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.SteelBlue);
            this.grouper1.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouper1.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // lstbx_HeadType
            // 
            resources.ApplyResources(this.lstbx_HeadType, "lstbx_HeadType");
            this.lstbx_HeadType.FormattingEnabled = true;
            this.lstbx_HeadType.Name = "lstbx_HeadType";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmb_Speed
            // 
            resources.ApplyResources(this.cmb_Speed, "cmb_Speed");
            this.cmb_Speed.FormattingEnabled = true;
            this.cmb_Speed.Name = "cmb_Speed";
            // 
            // label_InkType
            // 
            resources.ApplyResources(this.label_InkType, "label_InkType");
            this.label_InkType.Name = "label_InkType";
            // 
            // cmb_Ink
            // 
            resources.ApplyResources(this.cmb_Ink, "cmb_Ink");
            this.cmb_Ink.FormattingEnabled = true;
            this.cmb_Ink.Name = "cmb_Ink";
            // 
            // AboutWaveForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.label_WaveIndex);
            this.Controls.Add(this.cmb_WaveformIndex);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "AboutWaveForm";
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cmb_WaveformIndex;
        private System.Windows.Forms.Label label_WaveIndex;
        private GradientControls.Grouper grouper1;
        private System.Windows.Forms.ComboBox comb_Index;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Speed;
        private System.Windows.Forms.Label label_InkType;
        private System.Windows.Forms.ComboBox cmb_Ink;
        private System.Windows.Forms.ListBox lstbx_HeadType;
    }
}