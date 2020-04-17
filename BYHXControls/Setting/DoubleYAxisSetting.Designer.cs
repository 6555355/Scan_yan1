namespace BYHXPrinterManager.Setting
{
    partial class DoubleYAxisSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoubleYAxisSetting));
            this.groupBoxMotorParam = new System.Windows.Forms.GroupBox();
            this.groupBoxRetractableClothRoller = new System.Windows.Forms.GroupBox();
            this.buttonRetractableUnroll = new System.Windows.Forms.Button();
            this.buttonRetractableRoll = new System.Windows.Forms.Button();
            this.numericUpDownDoubeYRatio = new System.Windows.Forms.NumericUpDown();
            this.labelDoubeYRatio = new System.Windows.Forms.Label();
            this.buttonMotorSet = new System.Windows.Forms.Button();
            this.buttonMotorGet = new System.Windows.Forms.Button();
            this.numericUpDownMotorSpeed = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.comboBoxMotorMode = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.rollingMotorCheckBox = new System.Windows.Forms.CheckBox();
            this.comboBoxDoubleYMoveDir = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.comboBoxDoubleYAxis = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.numDoubleYMoveLen = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.buttonMoveY = new System.Windows.Forms.Button();
            this.groupBoxMotorParam.SuspendLayout();
            this.groupBoxRetractableClothRoller.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoubeYRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMotorSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoubleYMoveLen)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMotorParam
            // 
            resources.ApplyResources(this.groupBoxMotorParam, "groupBoxMotorParam");
            this.groupBoxMotorParam.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxMotorParam.Controls.Add(this.groupBoxRetractableClothRoller);
            this.groupBoxMotorParam.Controls.Add(this.numericUpDownDoubeYRatio);
            this.groupBoxMotorParam.Controls.Add(this.labelDoubeYRatio);
            this.groupBoxMotorParam.Controls.Add(this.buttonMotorSet);
            this.groupBoxMotorParam.Controls.Add(this.buttonMotorGet);
            this.groupBoxMotorParam.Controls.Add(this.numericUpDownMotorSpeed);
            this.groupBoxMotorParam.Controls.Add(this.label39);
            this.groupBoxMotorParam.Controls.Add(this.comboBoxMotorMode);
            this.groupBoxMotorParam.Controls.Add(this.label38);
            this.groupBoxMotorParam.Controls.Add(this.rollingMotorCheckBox);
            this.groupBoxMotorParam.Name = "groupBoxMotorParam";
            this.groupBoxMotorParam.TabStop = false;
            // 
            // groupBoxRetractableClothRoller
            // 
            resources.ApplyResources(this.groupBoxRetractableClothRoller, "groupBoxRetractableClothRoller");
            this.groupBoxRetractableClothRoller.Controls.Add(this.buttonRetractableUnroll);
            this.groupBoxRetractableClothRoller.Controls.Add(this.buttonRetractableRoll);
            this.groupBoxRetractableClothRoller.Name = "groupBoxRetractableClothRoller";
            this.groupBoxRetractableClothRoller.TabStop = false;
            // 
            // buttonRetractableUnroll
            // 
            resources.ApplyResources(this.buttonRetractableUnroll, "buttonRetractableUnroll");
            this.buttonRetractableUnroll.Name = "buttonRetractableUnroll";
            this.buttonRetractableUnroll.UseVisualStyleBackColor = true;
            this.buttonRetractableUnroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRetractableUnroll_MouseDown);
            this.buttonRetractableUnroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRetractableUnroll_MouseUp);
            // 
            // buttonRetractableRoll
            // 
            resources.ApplyResources(this.buttonRetractableRoll, "buttonRetractableRoll");
            this.buttonRetractableRoll.Name = "buttonRetractableRoll";
            this.buttonRetractableRoll.UseVisualStyleBackColor = true;
            this.buttonRetractableRoll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRetractableRoll_MouseDown);
            this.buttonRetractableRoll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRetractableRoll_MouseUp);
            // 
            // numericUpDownDoubeYRatio
            // 
            resources.ApplyResources(this.numericUpDownDoubeYRatio, "numericUpDownDoubeYRatio");
            this.numericUpDownDoubeYRatio.DecimalPlaces = 4;
            this.numericUpDownDoubeYRatio.Name = "numericUpDownDoubeYRatio";
            // 
            // labelDoubeYRatio
            // 
            resources.ApplyResources(this.labelDoubeYRatio, "labelDoubeYRatio");
            this.labelDoubeYRatio.Name = "labelDoubeYRatio";
            // 
            // buttonMotorSet
            // 
            resources.ApplyResources(this.buttonMotorSet, "buttonMotorSet");
            this.buttonMotorSet.Name = "buttonMotorSet";
            this.buttonMotorSet.UseVisualStyleBackColor = true;
            this.buttonMotorSet.Click += new System.EventHandler(this.buttonMotorSet_Click);
            // 
            // buttonMotorGet
            // 
            resources.ApplyResources(this.buttonMotorGet, "buttonMotorGet");
            this.buttonMotorGet.Name = "buttonMotorGet";
            this.buttonMotorGet.UseVisualStyleBackColor = true;
            this.buttonMotorGet.Click += new System.EventHandler(this.buttonMotorGet_Click);
            // 
            // numericUpDownMotorSpeed
            // 
            resources.ApplyResources(this.numericUpDownMotorSpeed, "numericUpDownMotorSpeed");
            this.numericUpDownMotorSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMotorSpeed.Name = "numericUpDownMotorSpeed";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // comboBoxMotorMode
            // 
            resources.ApplyResources(this.comboBoxMotorMode, "comboBoxMotorMode");
            this.comboBoxMotorMode.FormattingEnabled = true;
            this.comboBoxMotorMode.Items.AddRange(new object[] {
            resources.GetString("comboBoxMotorMode.Items"),
            resources.GetString("comboBoxMotorMode.Items1")});
            this.comboBoxMotorMode.Name = "comboBoxMotorMode";
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            // 
            // rollingMotorCheckBox
            // 
            resources.ApplyResources(this.rollingMotorCheckBox, "rollingMotorCheckBox");
            this.rollingMotorCheckBox.Name = "rollingMotorCheckBox";
            this.rollingMotorCheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBoxDoubleYMoveDir
            // 
            resources.ApplyResources(this.comboBoxDoubleYMoveDir, "comboBoxDoubleYMoveDir");
            this.comboBoxDoubleYMoveDir.FormattingEnabled = true;
            this.comboBoxDoubleYMoveDir.Items.AddRange(new object[] {
            resources.GetString("comboBoxDoubleYMoveDir.Items"),
            resources.GetString("comboBoxDoubleYMoveDir.Items1")});
            this.comboBoxDoubleYMoveDir.Name = "comboBoxDoubleYMoveDir";
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Name = "label37";
            // 
            // comboBoxDoubleYAxis
            // 
            resources.ApplyResources(this.comboBoxDoubleYAxis, "comboBoxDoubleYAxis");
            this.comboBoxDoubleYAxis.FormattingEnabled = true;
            this.comboBoxDoubleYAxis.Items.AddRange(new object[] {
            resources.GetString("comboBoxDoubleYAxis.Items"),
            resources.GetString("comboBoxDoubleYAxis.Items1")});
            this.comboBoxDoubleYAxis.Name = "comboBoxDoubleYAxis";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Name = "label36";
            // 
            // numDoubleYMoveLen
            // 
            resources.ApplyResources(this.numDoubleYMoveLen, "numDoubleYMoveLen");
            this.numDoubleYMoveLen.Name = "numDoubleYMoveLen";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Name = "label35";
            // 
            // buttonMoveY
            // 
            resources.ApplyResources(this.buttonMoveY, "buttonMoveY");
            this.buttonMoveY.Name = "buttonMoveY";
            this.buttonMoveY.UseVisualStyleBackColor = true;
            this.buttonMoveY.Click += new System.EventHandler(this.buttonMoveY_Click);
            // 
            // DoubleYAxisSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMotorParam);
            this.Controls.Add(this.comboBoxDoubleYMoveDir);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.comboBoxDoubleYAxis);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.numDoubleYMoveLen);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.buttonMoveY);
            this.Name = "DoubleYAxisSetting";
            this.groupBoxMotorParam.ResumeLayout(false);
            this.groupBoxMotorParam.PerformLayout();
            this.groupBoxRetractableClothRoller.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoubeYRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMotorSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDoubleYMoveLen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMotorParam;
        private System.Windows.Forms.GroupBox groupBoxRetractableClothRoller;
        private System.Windows.Forms.Button buttonRetractableUnroll;
        private System.Windows.Forms.Button buttonRetractableRoll;
        private System.Windows.Forms.NumericUpDown numericUpDownDoubeYRatio;
        private System.Windows.Forms.Label labelDoubeYRatio;
        private System.Windows.Forms.Button buttonMotorSet;
        private System.Windows.Forms.Button buttonMotorGet;
        private System.Windows.Forms.NumericUpDown numericUpDownMotorSpeed;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ComboBox comboBoxMotorMode;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox rollingMotorCheckBox;
        private System.Windows.Forms.ComboBox comboBoxDoubleYMoveDir;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox comboBoxDoubleYAxis;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.NumericUpDown numDoubleYMoveLen;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button buttonMoveY;
    }
}
