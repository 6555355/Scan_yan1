namespace BYHXPrinterManager.Main
{
    partial class CameraViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraViewer));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.panelCaribrationLeft = new System.Windows.Forms.Panel();
            this.numLeftHigh = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numLeftLow = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.panelCaribrationRight = new System.Windows.Forms.Panel();
            this.numRightHigh = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numRightLow = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCameraStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCameraClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCariMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonEndCari = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripDistinguishCnt = new System.Windows.Forms.ToolStripLabel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            this.panelCaribrationLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftLow)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            this.panelCaribrationRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRightLow)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.pictureBoxLeft);
            this.groupBox2.Controls.Add(this.panelCaribrationLeft);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // pictureBoxLeft
            // 
            resources.ApplyResources(this.pictureBoxLeft, "pictureBoxLeft");
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.TabStop = false;
            // 
            // panelCaribrationLeft
            // 
            resources.ApplyResources(this.panelCaribrationLeft, "panelCaribrationLeft");
            this.panelCaribrationLeft.Controls.Add(this.numLeftHigh);
            this.panelCaribrationLeft.Controls.Add(this.label2);
            this.panelCaribrationLeft.Controls.Add(this.numLeftLow);
            this.panelCaribrationLeft.Controls.Add(this.label1);
            this.panelCaribrationLeft.Name = "panelCaribrationLeft";
            // 
            // numLeftHigh
            // 
            resources.ApplyResources(this.numLeftHigh, "numLeftHigh");
            this.numLeftHigh.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLeftHigh.Name = "numLeftHigh";
            this.numLeftHigh.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numLeftHigh.ValueChanged += new System.EventHandler(this.numLeftLow_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numLeftLow
            // 
            resources.ApplyResources(this.numLeftLow, "numLeftLow");
            this.numLeftLow.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLeftLow.Name = "numLeftLow";
            this.numLeftLow.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numLeftLow.ValueChanged += new System.EventHandler(this.numLeftLow_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.pictureBoxRight);
            this.groupBox3.Controls.Add(this.panelCaribrationRight);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // pictureBoxRight
            // 
            resources.ApplyResources(this.pictureBoxRight, "pictureBoxRight");
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.TabStop = false;
            // 
            // panelCaribrationRight
            // 
            resources.ApplyResources(this.panelCaribrationRight, "panelCaribrationRight");
            this.panelCaribrationRight.Controls.Add(this.numRightHigh);
            this.panelCaribrationRight.Controls.Add(this.label3);
            this.panelCaribrationRight.Controls.Add(this.numRightLow);
            this.panelCaribrationRight.Controls.Add(this.label4);
            this.panelCaribrationRight.Name = "panelCaribrationRight";
            // 
            // numRightHigh
            // 
            resources.ApplyResources(this.numRightHigh, "numRightHigh");
            this.numRightHigh.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRightHigh.Name = "numRightHigh";
            this.numRightHigh.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numRightHigh.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numRightLow
            // 
            resources.ApplyResources(this.numRightLow, "numRightLow");
            this.numRightLow.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRightLow.Name = "numRightLow";
            this.numRightLow.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numRightLow.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCameraStart,
            this.toolStripButtonCameraClose,
            this.toolStripSeparator1,
            this.toolStripButtonCariMode,
            this.toolStripComboBox1,
            this.toolStripButtonEndCari,
            this.toolStripSeparator2,
            this.toolStripButtonSetting,
            this.toolStripDistinguishCnt});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButtonCameraStart
            // 
            resources.ApplyResources(this.toolStripButtonCameraStart, "toolStripButtonCameraStart");
            this.toolStripButtonCameraStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCameraStart.Name = "toolStripButtonCameraStart";
            this.toolStripButtonCameraStart.Click += new System.EventHandler(this.buttonCameraStart_Click);
            // 
            // toolStripButtonCameraClose
            // 
            resources.ApplyResources(this.toolStripButtonCameraClose, "toolStripButtonCameraClose");
            this.toolStripButtonCameraClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCameraClose.Name = "toolStripButtonCameraClose";
            this.toolStripButtonCameraClose.Click += new System.EventHandler(this.buttonCameraClose_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButtonCariMode
            // 
            resources.ApplyResources(this.toolStripButtonCariMode, "toolStripButtonCariMode");
            this.toolStripButtonCariMode.CheckOnClick = true;
            this.toolStripButtonCariMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCariMode.Name = "toolStripButtonCariMode";
            this.toolStripButtonCariMode.CheckedChanged += new System.EventHandler(this.toolStripButtonCariMode_CheckedChanged);
            // 
            // toolStripComboBox1
            // 
            resources.ApplyResources(this.toolStripComboBox1, "toolStripComboBox1");
            this.toolStripComboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("toolStripComboBox1.AutoCompleteCustomSource"),
            resources.GetString("toolStripComboBox1.AutoCompleteCustomSource1"),
            resources.GetString("toolStripComboBox1.AutoCompleteCustomSource2"),
            resources.GetString("toolStripComboBox1.AutoCompleteCustomSource3")});
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            resources.GetString("toolStripComboBox1.Items"),
            resources.GetString("toolStripComboBox1.Items1"),
            resources.GetString("toolStripComboBox1.Items2"),
            resources.GetString("toolStripComboBox1.Items3")});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            // 
            // toolStripButtonEndCari
            // 
            resources.ApplyResources(this.toolStripButtonEndCari, "toolStripButtonEndCari");
            this.toolStripButtonEndCari.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonEndCari.Name = "toolStripButtonEndCari";
            this.toolStripButtonEndCari.Click += new System.EventHandler(this.toolStripButtonEndCari_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripButtonSetting
            // 
            resources.ApplyResources(this.toolStripButtonSetting, "toolStripButtonSetting");
            this.toolStripButtonSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSetting.Name = "toolStripButtonSetting";
            this.toolStripButtonSetting.Click += new System.EventHandler(this.toolStripButtonSetting_Click);
            // 
            // toolStripDistinguishCnt
            // 
            resources.ApplyResources(this.toolStripDistinguishCnt, "toolStripDistinguishCnt");
            this.toolStripDistinguishCnt.Name = "toolStripDistinguishCnt";
            // 
            // CameraViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CameraViewer";
            this.Load += new System.EventHandler(this.CameraSettings_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            this.panelCaribrationLeft.ResumeLayout(false);
            this.panelCaribrationLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftLow)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            this.panelCaribrationRight.ResumeLayout(false);
            this.panelCaribrationRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRightLow)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBoxRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCameraStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonCameraClose;
        private System.Windows.Forms.ToolStripButton toolStripButtonSetting;
        private System.Windows.Forms.Panel panelCaribrationLeft;
        private System.Windows.Forms.NumericUpDown numLeftLow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLeftHigh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelCaribrationRight;
        private System.Windows.Forms.NumericUpDown numRightHigh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numRightLow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCariMode;
        private System.Windows.Forms.ToolStripButton toolStripButtonEndCari;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripDistinguishCnt;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}
