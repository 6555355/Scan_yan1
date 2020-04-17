namespace BYHXPrinterManager.Setting
{
    partial class Waveform_KM1024A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waveform_KM1024A));
            this.comboBox_HeadBoardIndex = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_HeadSelected = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_DownloadAll = new System.Windows.Forms.Button();
            this.button_Download = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.button_Open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_HeadBoardIndex
            // 
            this.comboBox_HeadBoardIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_HeadBoardIndex.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_HeadBoardIndex, "comboBox_HeadBoardIndex");
            this.comboBox_HeadBoardIndex.Name = "comboBox_HeadBoardIndex";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox_HeadSelected
            // 
            this.comboBox_HeadSelected.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_HeadSelected.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_HeadSelected, "comboBox_HeadSelected");
            this.comboBox_HeadSelected.Name = "comboBox_HeadSelected";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button_DownloadAll
            // 
            resources.ApplyResources(this.button_DownloadAll, "button_DownloadAll");
            this.button_DownloadAll.Name = "button_DownloadAll";
            this.button_DownloadAll.UseVisualStyleBackColor = true;
            this.button_DownloadAll.Click += new System.EventHandler(this.button_DownloadAll_Click);
            // 
            // button_Download
            // 
            resources.ApplyResources(this.button_Download, "button_Download");
            this.button_Download.Name = "button_Download";
            this.button_Download.UseVisualStyleBackColor = true;
            this.button_Download.Click += new System.EventHandler(this.button_Download_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox_FileName
            // 
            resources.ApplyResources(this.textBox_FileName, "textBox_FileName");
            this.textBox_FileName.Name = "textBox_FileName";
            // 
            // button_Open
            // 
            resources.ApplyResources(this.button_Open, "button_Open");
            this.button_Open.Name = "button_Open";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // Waveform_KM1024A
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_Open);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_DownloadAll);
            this.Controls.Add(this.button_Download);
            this.Controls.Add(this.comboBox_HeadBoardIndex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_HeadSelected);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Waveform_KM1024A";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_HeadBoardIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_HeadSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_DownloadAll;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_FileName;
        private System.Windows.Forms.Button button_Open;
    }
}