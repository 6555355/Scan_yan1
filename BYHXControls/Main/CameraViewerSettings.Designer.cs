namespace BYHXPrinterManager.Main
{
    partial class CameraViewerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraViewerSettings));
            this.groupBoxImageProcessPara = new System.Windows.Forms.GroupBox();
            this.numRightCameraPosY = new System.Windows.Forms.NumericUpDown();
            this.numLeftCameraPosY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numMaxError = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numMinDotCnt = new System.Windows.Forms.NumericUpDown();
            this.numRightCameraPosX = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numLeftCameraPosX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numDelayCycleNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonApplyCameraInstallSettings = new System.Windows.Forms.Button();
            this.groupBoxRightCameraPara = new System.Windows.Forms.GroupBox();
            this.numrightExposureTime = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonApplyRightCameraConfig = new System.Windows.Forms.Button();
            this.groupBoxLeftCameraPara = new System.Windows.Forms.GroupBox();
            this.numleftExposureTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonApplyLeftCameraConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonApplyCameraBind = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxRightIp = new System.Windows.Forms.ComboBox();
            this.lblRightCameraIP = new System.Windows.Forms.Label();
            this.comboBoxLeftIp = new System.Windows.Forms.ComboBox();
            this.lblLeftCameraIP = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxImageProcessPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightCameraPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftCameraPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDotCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRightCameraPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftCameraPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayCycleNum)).BeginInit();
            this.groupBoxRightCameraPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numrightExposureTime)).BeginInit();
            this.groupBoxLeftCameraPara.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numleftExposureTime)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxImageProcessPara
            // 
            resources.ApplyResources(this.groupBoxImageProcessPara, "groupBoxImageProcessPara");
            this.groupBoxImageProcessPara.Controls.Add(this.numRightCameraPosY);
            this.groupBoxImageProcessPara.Controls.Add(this.numLeftCameraPosY);
            this.groupBoxImageProcessPara.Controls.Add(this.label9);
            this.groupBoxImageProcessPara.Controls.Add(this.numMaxError);
            this.groupBoxImageProcessPara.Controls.Add(this.label8);
            this.groupBoxImageProcessPara.Controls.Add(this.numMinDotCnt);
            this.groupBoxImageProcessPara.Controls.Add(this.numRightCameraPosX);
            this.groupBoxImageProcessPara.Controls.Add(this.label7);
            this.groupBoxImageProcessPara.Controls.Add(this.numLeftCameraPosX);
            this.groupBoxImageProcessPara.Controls.Add(this.label6);
            this.groupBoxImageProcessPara.Controls.Add(this.numDelayCycleNum);
            this.groupBoxImageProcessPara.Controls.Add(this.label5);
            this.groupBoxImageProcessPara.Controls.Add(this.buttonApplyCameraInstallSettings);
            this.groupBoxImageProcessPara.Name = "groupBoxImageProcessPara";
            this.groupBoxImageProcessPara.TabStop = false;
            // 
            // numRightCameraPosY
            // 
            resources.ApplyResources(this.numRightCameraPosY, "numRightCameraPosY");
            this.numRightCameraPosY.DecimalPlaces = 3;
            this.numRightCameraPosY.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numRightCameraPosY.Name = "numRightCameraPosY";
            // 
            // numLeftCameraPosY
            // 
            resources.ApplyResources(this.numLeftCameraPosY, "numLeftCameraPosY");
            this.numLeftCameraPosY.DecimalPlaces = 3;
            this.numLeftCameraPosY.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numLeftCameraPosY.Name = "numLeftCameraPosY";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // numMaxError
            // 
            resources.ApplyResources(this.numMaxError, "numMaxError");
            this.numMaxError.DecimalPlaces = 1;
            this.numMaxError.Name = "numMaxError";
            this.numMaxError.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numMinDotCnt
            // 
            resources.ApplyResources(this.numMinDotCnt, "numMinDotCnt");
            this.numMinDotCnt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinDotCnt.Name = "numMinDotCnt";
            this.numMinDotCnt.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numRightCameraPosX
            // 
            resources.ApplyResources(this.numRightCameraPosX, "numRightCameraPosX");
            this.numRightCameraPosX.DecimalPlaces = 3;
            this.numRightCameraPosX.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numRightCameraPosX.Name = "numRightCameraPosX";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numLeftCameraPosX
            // 
            resources.ApplyResources(this.numLeftCameraPosX, "numLeftCameraPosX");
            this.numLeftCameraPosX.DecimalPlaces = 3;
            this.numLeftCameraPosX.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numLeftCameraPosX.Name = "numLeftCameraPosX";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // numDelayCycleNum
            // 
            resources.ApplyResources(this.numDelayCycleNum, "numDelayCycleNum");
            this.numDelayCycleNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDelayCycleNum.Name = "numDelayCycleNum";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // buttonApplyCameraInstallSettings
            // 
            resources.ApplyResources(this.buttonApplyCameraInstallSettings, "buttonApplyCameraInstallSettings");
            this.buttonApplyCameraInstallSettings.Name = "buttonApplyCameraInstallSettings";
            this.buttonApplyCameraInstallSettings.UseVisualStyleBackColor = true;
            this.buttonApplyCameraInstallSettings.Click += new System.EventHandler(this.buttonApplyCameraInstallSettings_Click);
            // 
            // groupBoxRightCameraPara
            // 
            resources.ApplyResources(this.groupBoxRightCameraPara, "groupBoxRightCameraPara");
            this.groupBoxRightCameraPara.Controls.Add(this.numrightExposureTime);
            this.groupBoxRightCameraPara.Controls.Add(this.label4);
            this.groupBoxRightCameraPara.Controls.Add(this.buttonApplyRightCameraConfig);
            this.groupBoxRightCameraPara.Name = "groupBoxRightCameraPara";
            this.groupBoxRightCameraPara.TabStop = false;
            // 
            // numrightExposureTime
            // 
            resources.ApplyResources(this.numrightExposureTime, "numrightExposureTime");
            this.numrightExposureTime.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numrightExposureTime.Name = "numrightExposureTime";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // buttonApplyRightCameraConfig
            // 
            resources.ApplyResources(this.buttonApplyRightCameraConfig, "buttonApplyRightCameraConfig");
            this.buttonApplyRightCameraConfig.Name = "buttonApplyRightCameraConfig";
            this.buttonApplyRightCameraConfig.UseVisualStyleBackColor = true;
            this.buttonApplyRightCameraConfig.Click += new System.EventHandler(this.buttonApplyRightCameraConfig_Click);
            // 
            // groupBoxLeftCameraPara
            // 
            resources.ApplyResources(this.groupBoxLeftCameraPara, "groupBoxLeftCameraPara");
            this.groupBoxLeftCameraPara.Controls.Add(this.numleftExposureTime);
            this.groupBoxLeftCameraPara.Controls.Add(this.label3);
            this.groupBoxLeftCameraPara.Controls.Add(this.buttonApplyLeftCameraConfig);
            this.groupBoxLeftCameraPara.Name = "groupBoxLeftCameraPara";
            this.groupBoxLeftCameraPara.TabStop = false;
            // 
            // numleftExposureTime
            // 
            resources.ApplyResources(this.numleftExposureTime, "numleftExposureTime");
            this.numleftExposureTime.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numleftExposureTime.Name = "numleftExposureTime";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonApplyLeftCameraConfig
            // 
            resources.ApplyResources(this.buttonApplyLeftCameraConfig, "buttonApplyLeftCameraConfig");
            this.buttonApplyLeftCameraConfig.Name = "buttonApplyLeftCameraConfig";
            this.buttonApplyLeftCameraConfig.UseVisualStyleBackColor = true;
            this.buttonApplyLeftCameraConfig.Click += new System.EventHandler(this.buttonApplyLeftCameraConfig_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.buttonApplyCameraBind);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBoxRightIp);
            this.groupBox1.Controls.Add(this.lblRightCameraIP);
            this.groupBox1.Controls.Add(this.comboBoxLeftIp);
            this.groupBox1.Controls.Add(this.lblLeftCameraIP);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonApplyCameraBind
            // 
            resources.ApplyResources(this.buttonApplyCameraBind, "buttonApplyCameraBind");
            this.buttonApplyCameraBind.Name = "buttonApplyCameraBind";
            this.buttonApplyCameraBind.UseVisualStyleBackColor = true;
            this.buttonApplyCameraBind.Click += new System.EventHandler(this.buttonApplyCameraBind_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxRightIp
            // 
            resources.ApplyResources(this.comboBoxRightIp, "comboBoxRightIp");
            this.comboBoxRightIp.FormattingEnabled = true;
            this.comboBoxRightIp.Items.AddRange(new object[] {
            resources.GetString("comboBoxRightIp.Items"),
            resources.GetString("comboBoxRightIp.Items1")});
            this.comboBoxRightIp.Name = "comboBoxRightIp";
            // 
            // lblRightCameraIP
            // 
            resources.ApplyResources(this.lblRightCameraIP, "lblRightCameraIP");
            this.lblRightCameraIP.Name = "lblRightCameraIP";
            // 
            // comboBoxLeftIp
            // 
            resources.ApplyResources(this.comboBoxLeftIp, "comboBoxLeftIp");
            this.comboBoxLeftIp.FormattingEnabled = true;
            this.comboBoxLeftIp.Items.AddRange(new object[] {
            resources.GetString("comboBoxLeftIp.Items"),
            resources.GetString("comboBoxLeftIp.Items1")});
            this.comboBoxLeftIp.Name = "comboBoxLeftIp";
            // 
            // lblLeftCameraIP
            // 
            resources.ApplyResources(this.lblLeftCameraIP, "lblLeftCameraIP");
            this.lblLeftCameraIP.Name = "lblLeftCameraIP";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CameraViewerSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBoxImageProcessPara);
            this.Controls.Add(this.groupBoxRightCameraPara);
            this.Controls.Add(this.groupBoxLeftCameraPara);
            this.Controls.Add(this.groupBox1);
            this.Name = "CameraViewerSettings";
            this.Load += new System.EventHandler(this.CameraViewerSettings_Load);
            this.groupBoxImageProcessPara.ResumeLayout(false);
            this.groupBoxImageProcessPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightCameraPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftCameraPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinDotCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRightCameraPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftCameraPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayCycleNum)).EndInit();
            this.groupBoxRightCameraPara.ResumeLayout(false);
            this.groupBoxRightCameraPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numrightExposureTime)).EndInit();
            this.groupBoxLeftCameraPara.ResumeLayout(false);
            this.groupBoxLeftCameraPara.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numleftExposureTime)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxImageProcessPara;
        private System.Windows.Forms.Button buttonApplyCameraInstallSettings;
        private System.Windows.Forms.GroupBox groupBoxRightCameraPara;
        private System.Windows.Forms.NumericUpDown numrightExposureTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonApplyRightCameraConfig;
        private System.Windows.Forms.GroupBox groupBoxLeftCameraPara;
        private System.Windows.Forms.NumericUpDown numleftExposureTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonApplyLeftCameraConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonApplyCameraBind;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxRightIp;
        private System.Windows.Forms.Label lblRightCameraIP;
        private System.Windows.Forms.ComboBox comboBoxLeftIp;
        private System.Windows.Forms.Label lblLeftCameraIP;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numRightCameraPosX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numLeftCameraPosX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDelayCycleNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numMaxError;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numMinDotCnt;
        private System.Windows.Forms.NumericUpDown numRightCameraPosY;
        private System.Windows.Forms.NumericUpDown numLeftCameraPosY;
    }
}