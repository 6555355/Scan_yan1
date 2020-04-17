namespace BYHXPrinterManager.Setting
{
    partial class MediaConfigs
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobConfigModes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_listBoxLayerSettings = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_PropertyGridStep = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CopyAs = new System.Windows.Forms.Button();
            this.ExportMode = new System.Windows.Forms.Button();
            this.ImportMode = new System.Windows.Forms.Button();
            this.RemoveMode = new System.Windows.Forms.Button();
            this.AddMode = new System.Windows.Forms.Button();
            this.UpMode = new System.Windows.Forms.Button();
            this.DownMode = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_listBoxLayerSettings);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // m_listBoxLayerSettings
            // 
            resources.ApplyResources(this.m_listBoxLayerSettings, "m_listBoxLayerSettings");
            this.m_listBoxLayerSettings.FormattingEnabled = true;
            this.m_listBoxLayerSettings.Name = "m_listBoxLayerSettings";
            this.m_listBoxLayerSettings.SelectedIndexChanged += new System.EventHandler(this.m_listBoxLayerSettings_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_PropertyGridStep);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // m_PropertyGridStep
            // 
            resources.ApplyResources(this.m_PropertyGridStep, "m_PropertyGridStep");
            this.m_PropertyGridStep.Name = "m_PropertyGridStep";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CopyAs);
            this.groupBox3.Controls.Add(this.ExportMode);
            this.groupBox3.Controls.Add(this.ImportMode);
            this.groupBox3.Controls.Add(this.RemoveMode);
            this.groupBox3.Controls.Add(this.AddMode);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // CopyAs
            // 
            resources.ApplyResources(this.CopyAs, "CopyAs");
            this.CopyAs.Name = "CopyAs";
            this.CopyAs.UseVisualStyleBackColor = true;
            this.CopyAs.Click += new System.EventHandler(this.CopyAs_Click);
            // 
            // ExportMode
            // 
            resources.ApplyResources(this.ExportMode, "ExportMode");
            this.ExportMode.Name = "ExportMode";
            this.ExportMode.UseVisualStyleBackColor = true;
            this.ExportMode.Click += new System.EventHandler(this.ExportMode_Click);
            // 
            // ImportMode
            // 
            resources.ApplyResources(this.ImportMode, "ImportMode");
            this.ImportMode.Name = "ImportMode";
            this.ImportMode.UseVisualStyleBackColor = true;
            this.ImportMode.Click += new System.EventHandler(this.ImportMode_Click);
            // 
            // RemoveMode
            // 
            resources.ApplyResources(this.RemoveMode, "RemoveMode");
            this.RemoveMode.Name = "RemoveMode";
            this.RemoveMode.UseVisualStyleBackColor = true;
            this.RemoveMode.Click += new System.EventHandler(this.RemoveMode_Click);
            // 
            // AddMode
            // 
            resources.ApplyResources(this.AddMode, "AddMode");
            this.AddMode.Name = "AddMode";
            this.AddMode.UseVisualStyleBackColor = true;
            this.AddMode.Click += new System.EventHandler(this.AddMode_Click);
            // 
            // UpMode
            // 
            resources.ApplyResources(this.UpMode, "UpMode");
            this.UpMode.Name = "UpMode";
            this.UpMode.UseVisualStyleBackColor = true;
            this.UpMode.Click += new System.EventHandler(this.UpMode_Click);
            // 
            // DownMode
            // 
            resources.ApplyResources(this.DownMode, "DownMode");
            this.DownMode.Name = "DownMode";
            this.DownMode.UseVisualStyleBackColor = true;
            this.DownMode.Click += new System.EventHandler(this.DownMode_Click);
            // 
            // JobConfigModes
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DownMode);
            this.Controls.Add(this.UpMode);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "JobConfigModes";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox m_listBoxLayerSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button CopyAs;
        private System.Windows.Forms.Button ExportMode;
        private System.Windows.Forms.Button ImportMode;
        private System.Windows.Forms.Button RemoveMode;
        private System.Windows.Forms.Button AddMode;
        private System.Windows.Forms.Button UpMode;
        private System.Windows.Forms.Button DownMode;
        private System.Windows.Forms.PropertyGrid m_PropertyGridStep;
    }
}
