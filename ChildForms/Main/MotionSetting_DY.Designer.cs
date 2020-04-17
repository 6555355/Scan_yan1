namespace BYHXPrinterManager.Main
{
    partial class MotionSetting_DY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MotionSetting_DY));
            this.groupBoxMp = new System.Windows.Forms.GroupBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnReFresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelIv = new System.Windows.Forms.Label();
            this.labeltorque = new System.Windows.Forms.Label();
            this.numericUpDownLT = new System.Windows.Forms.NumericUpDown();
            this.comboBoxControlMode = new System.Windows.Forms.ComboBox();
            this.numericUpDownPrintHeadHeight = new System.Windows.Forms.NumericUpDown();
            this.labellt = new System.Windows.Forms.Label();
            this.numericUpDownTorque = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLV = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.comboBoxMotorType = new System.Windows.Forms.ComboBox();
            this.labelMotorType = new System.Windows.Forms.Label();
            this.comboBoxSwitchType = new System.Windows.Forms.ComboBox();
            this.labelSwitchType = new System.Windows.Forms.Label();
            this.groupBoxMp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrintHeadHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTorque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMp
            // 
            this.groupBoxMp.Controls.Add(this.comboBoxSwitchType);
            this.groupBoxMp.Controls.Add(this.labelSwitchType);
            this.groupBoxMp.Controls.Add(this.comboBoxMotorType);
            this.groupBoxMp.Controls.Add(this.labelMotorType);
            this.groupBoxMp.Controls.Add(this.btnSet);
            this.groupBoxMp.Controls.Add(this.btnReFresh);
            this.groupBoxMp.Controls.Add(this.label3);
            this.groupBoxMp.Controls.Add(this.label2);
            this.groupBoxMp.Controls.Add(this.numericUpDownTimeout);
            this.groupBoxMp.Controls.Add(this.label1);
            this.groupBoxMp.Controls.Add(this.labelIv);
            this.groupBoxMp.Controls.Add(this.labeltorque);
            this.groupBoxMp.Controls.Add(this.numericUpDownLT);
            this.groupBoxMp.Controls.Add(this.comboBoxControlMode);
            this.groupBoxMp.Controls.Add(this.numericUpDownPrintHeadHeight);
            this.groupBoxMp.Controls.Add(this.labellt);
            this.groupBoxMp.Controls.Add(this.numericUpDownTorque);
            this.groupBoxMp.Controls.Add(this.numericUpDownLV);
            this.groupBoxMp.Controls.Add(this.label40);
            this.groupBoxMp.Controls.Add(this.label41);
            resources.ApplyResources(this.groupBoxMp, "groupBoxMp");
            this.groupBoxMp.Name = "groupBoxMp";
            this.groupBoxMp.TabStop = false;
            // 
            // btnSet
            // 
            resources.ApplyResources(this.btnSet, "btnSet");
            this.btnSet.Name = "btnSet";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnReFresh
            // 
            resources.ApplyResources(this.btnReFresh, "btnReFresh");
            this.btnReFresh.Name = "btnReFresh";
            this.btnReFresh.UseVisualStyleBackColor = true;
            this.btnReFresh.Click += new System.EventHandler(this.btnReFresh_Click);
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
            // numericUpDownTimeout
            // 
            resources.ApplyResources(this.numericUpDownTimeout, "numericUpDownTimeout");
            this.numericUpDownTimeout.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownTimeout.Name = "numericUpDownTimeout";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelIv
            // 
            resources.ApplyResources(this.labelIv, "labelIv");
            this.labelIv.Name = "labelIv";
            // 
            // labeltorque
            // 
            resources.ApplyResources(this.labeltorque, "labeltorque");
            this.labeltorque.Name = "labeltorque";
            // 
            // numericUpDownLT
            // 
            resources.ApplyResources(this.numericUpDownLT, "numericUpDownLT");
            this.numericUpDownLT.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownLT.Name = "numericUpDownLT";
            // 
            // comboBoxControlMode
            // 
            this.comboBoxControlMode.FormattingEnabled = true;
            this.comboBoxControlMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxControlMode.Items"),
            resources.GetString("comboBoxControlMode.Items1")});
            resources.ApplyResources(this.comboBoxControlMode, "comboBoxControlMode");
            this.comboBoxControlMode.Name = "comboBoxControlMode";
            // 
            // numericUpDownPrintHeadHeight
            // 
            resources.ApplyResources(this.numericUpDownPrintHeadHeight, "numericUpDownPrintHeadHeight");
            this.numericUpDownPrintHeadHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownPrintHeadHeight.Name = "numericUpDownPrintHeadHeight";
            // 
            // labellt
            // 
            resources.ApplyResources(this.labellt, "labellt");
            this.labellt.Name = "labellt";
            // 
            // numericUpDownTorque
            // 
            resources.ApplyResources(this.numericUpDownTorque, "numericUpDownTorque");
            this.numericUpDownTorque.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownTorque.Name = "numericUpDownTorque";
            // 
            // numericUpDownLV
            // 
            resources.ApplyResources(this.numericUpDownLV, "numericUpDownLV");
            this.numericUpDownLV.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numericUpDownLV.Name = "numericUpDownLV";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // comboBoxMotorType
            // 
            this.comboBoxMotorType.FormattingEnabled = true;
            this.comboBoxMotorType.Items.AddRange(new object[] {
            resources.GetString("comboBoxMotorType.Items"),
            resources.GetString("comboBoxMotorType.Items1"),
            resources.GetString("comboBoxMotorType.Items2"),
            resources.GetString("comboBoxMotorType.Items3")});
            resources.ApplyResources(this.comboBoxMotorType, "comboBoxMotorType");
            this.comboBoxMotorType.Name = "comboBoxMotorType";
            // 
            // labelMotorType
            // 
            resources.ApplyResources(this.labelMotorType, "labelMotorType");
            this.labelMotorType.Name = "labelMotorType";
            // 
            // comboBoxSwitchType
            // 
            this.comboBoxSwitchType.FormattingEnabled = true;
            this.comboBoxSwitchType.Items.AddRange(new object[] {
            resources.GetString("comboBoxSwitchType.Items"),
            resources.GetString("comboBoxSwitchType.Items1"),
            resources.GetString("comboBoxSwitchType.Items2")});
            resources.ApplyResources(this.comboBoxSwitchType, "comboBoxSwitchType");
            this.comboBoxSwitchType.Name = "comboBoxSwitchType";
            // 
            // labelSwitchType
            // 
            resources.ApplyResources(this.labelSwitchType, "labelSwitchType");
            this.labelSwitchType.Name = "labelSwitchType";
            // 
            // MotionSetting_DY
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MotionSetting_DY";
            this.groupBoxMp.ResumeLayout(false);
            this.groupBoxMp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrintHeadHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTorque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMp;
        private System.Windows.Forms.Label labelIv;
        private System.Windows.Forms.NumericUpDown numericUpDownLT;
        private System.Windows.Forms.NumericUpDown numericUpDownPrintHeadHeight;
        private System.Windows.Forms.Label labellt;
        private System.Windows.Forms.NumericUpDown numericUpDownLV;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ComboBox comboBoxControlMode;
        private System.Windows.Forms.Label labeltorque;
        private System.Windows.Forms.NumericUpDown numericUpDownTorque;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnReFresh;
        private System.Windows.Forms.ComboBox comboBoxMotorType;
        private System.Windows.Forms.Label labelMotorType;
        private System.Windows.Forms.ComboBox comboBoxSwitchType;
        private System.Windows.Forms.Label labelSwitchType;
    }
}