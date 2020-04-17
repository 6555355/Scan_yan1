namespace BYHXPrinterManager.Setting
{
    partial class ColorjetManualCleanSettingA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorjetManualCleanSettingA));
            this.m_GroupBoxCleanParam = new System.Windows.Forms.GroupBox();
            this.buttonSet = new System.Windows.Forms.Button();
            this.numericSpeed = new System.Windows.Forms.NumericUpDown();
            this.m_ComboBoxHeaderNumPerTime = new System.Windows.Forms.ComboBox();
            this.numericOriginOffset = new System.Windows.Forms.NumericUpDown();
            this.m_LabelAxisXStartPos = new System.Windows.Forms.Label();
            this.checkBoxFunctionOn = new System.Windows.Forms.CheckBox();
            this.labelDelayTime = new System.Windows.Forms.Label();
            this.numericDelayTime = new System.Windows.Forms.NumericUpDown();
            this.labelOriginOffset = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.m_NumericUpDownXStartPos = new System.Windows.Forms.NumericUpDown();
            this.m_LabelHeaderNum = new System.Windows.Forms.Label();
            this.m_GroupBoxClean = new System.Windows.Forms.GroupBox();
            this.buttonZBottom = new System.Windows.Forms.Button();
            this.buttonZCenter = new System.Windows.Forms.Button();
            this.panelColorMask = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.m_LabelCleanColor = new System.Windows.Forms.Label();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonStartClean = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_GroupBoxCleanParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOriginOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownXStartPos)).BeginInit();
            this.m_GroupBoxClean.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_GroupBoxCleanParam
            // 
            resources.ApplyResources(this.m_GroupBoxCleanParam, "m_GroupBoxCleanParam");
            this.m_GroupBoxCleanParam.Controls.Add(this.buttonSet);
            this.m_GroupBoxCleanParam.Controls.Add(this.numericSpeed);
            this.m_GroupBoxCleanParam.Controls.Add(this.m_ComboBoxHeaderNumPerTime);
            this.m_GroupBoxCleanParam.Controls.Add(this.numericOriginOffset);
            this.m_GroupBoxCleanParam.Controls.Add(this.m_LabelAxisXStartPos);
            this.m_GroupBoxCleanParam.Controls.Add(this.checkBoxFunctionOn);
            this.m_GroupBoxCleanParam.Controls.Add(this.labelDelayTime);
            this.m_GroupBoxCleanParam.Controls.Add(this.numericDelayTime);
            this.m_GroupBoxCleanParam.Controls.Add(this.labelOriginOffset);
            this.m_GroupBoxCleanParam.Controls.Add(this.labelSpeed);
            this.m_GroupBoxCleanParam.Controls.Add(this.m_NumericUpDownXStartPos);
            this.m_GroupBoxCleanParam.Controls.Add(this.m_LabelHeaderNum);
            this.m_GroupBoxCleanParam.Name = "m_GroupBoxCleanParam";
            this.m_GroupBoxCleanParam.TabStop = false;
            // 
            // buttonSet
            // 
            resources.ApplyResources(this.buttonSet, "buttonSet");
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.m_ButtonSet_Click);
            // 
            // numericSpeed
            // 
            resources.ApplyResources(this.numericSpeed, "numericSpeed");
            this.numericSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericSpeed.Name = "numericSpeed";
            // 
            // m_ComboBoxHeaderNumPerTime
            // 
            this.m_ComboBoxHeaderNumPerTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxHeaderNumPerTime.FormattingEnabled = true;
            resources.ApplyResources(this.m_ComboBoxHeaderNumPerTime, "m_ComboBoxHeaderNumPerTime");
            this.m_ComboBoxHeaderNumPerTime.Name = "m_ComboBoxHeaderNumPerTime";
            // 
            // numericOriginOffset
            // 
            resources.ApplyResources(this.numericOriginOffset, "numericOriginOffset");
            this.numericOriginOffset.Name = "numericOriginOffset";
            // 
            // m_LabelAxisXStartPos
            // 
            resources.ApplyResources(this.m_LabelAxisXStartPos, "m_LabelAxisXStartPos");
            this.m_LabelAxisXStartPos.Name = "m_LabelAxisXStartPos";
            // 
            // checkBoxFunctionOn
            // 
            resources.ApplyResources(this.checkBoxFunctionOn, "checkBoxFunctionOn");
            this.checkBoxFunctionOn.Name = "checkBoxFunctionOn";
            this.checkBoxFunctionOn.UseVisualStyleBackColor = true;
            // 
            // labelDelayTime
            // 
            resources.ApplyResources(this.labelDelayTime, "labelDelayTime");
            this.labelDelayTime.Name = "labelDelayTime";
            // 
            // numericDelayTime
            // 
            resources.ApplyResources(this.numericDelayTime, "numericDelayTime");
            this.numericDelayTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericDelayTime.Name = "numericDelayTime";
            // 
            // labelOriginOffset
            // 
            resources.ApplyResources(this.labelOriginOffset, "labelOriginOffset");
            this.labelOriginOffset.Name = "labelOriginOffset";
            // 
            // labelSpeed
            // 
            resources.ApplyResources(this.labelSpeed, "labelSpeed");
            this.labelSpeed.Name = "labelSpeed";
            // 
            // m_NumericUpDownXStartPos
            // 
            this.m_NumericUpDownXStartPos.DecimalPlaces = 2;
            resources.ApplyResources(this.m_NumericUpDownXStartPos, "m_NumericUpDownXStartPos");
            this.m_NumericUpDownXStartPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownXStartPos.Name = "m_NumericUpDownXStartPos";
            // 
            // m_LabelHeaderNum
            // 
            resources.ApplyResources(this.m_LabelHeaderNum, "m_LabelHeaderNum");
            this.m_LabelHeaderNum.Name = "m_LabelHeaderNum";
            // 
            // m_GroupBoxClean
            // 
            this.m_GroupBoxClean.Controls.Add(this.buttonZBottom);
            this.m_GroupBoxClean.Controls.Add(this.buttonZCenter);
            this.m_GroupBoxClean.Controls.Add(this.panelColorMask);
            this.m_GroupBoxClean.Controls.Add(this.checkBoxAll);
            this.m_GroupBoxClean.Controls.Add(this.m_LabelCleanColor);
            this.m_GroupBoxClean.Controls.Add(this.m_ButtonCancel);
            this.m_GroupBoxClean.Controls.Add(this.m_ButtonStartClean);
            resources.ApplyResources(this.m_GroupBoxClean, "m_GroupBoxClean");
            this.m_GroupBoxClean.Name = "m_GroupBoxClean";
            this.m_GroupBoxClean.TabStop = false;
            // 
            // buttonZBottom
            // 
            resources.ApplyResources(this.buttonZBottom, "buttonZBottom");
            this.buttonZBottom.Name = "buttonZBottom";
            this.buttonZBottom.UseVisualStyleBackColor = true;
            this.buttonZBottom.Click += new System.EventHandler(this.buttonZBottom_Click);
            // 
            // buttonZCenter
            // 
            resources.ApplyResources(this.buttonZCenter, "buttonZCenter");
            this.buttonZCenter.Name = "buttonZCenter";
            this.buttonZCenter.UseVisualStyleBackColor = true;
            this.buttonZCenter.Click += new System.EventHandler(this.buttonZCenter_Click);
            // 
            // panelColorMask
            // 
            resources.ApplyResources(this.panelColorMask, "panelColorMask");
            this.panelColorMask.Name = "panelColorMask";
            // 
            // checkBoxAll
            // 
            resources.ApplyResources(this.checkBoxAll, "checkBoxAll");
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // m_LabelCleanColor
            // 
            resources.ApplyResources(this.m_LabelCleanColor, "m_LabelCleanColor");
            this.m_LabelCleanColor.Name = "m_LabelCleanColor";
            // 
            // m_ButtonCancel
            // 
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            this.m_ButtonCancel.UseVisualStyleBackColor = true;
            this.m_ButtonCancel.Click += new System.EventHandler(this.m_ButtonCancel_Click);
            // 
            // m_ButtonStartClean
            // 
            resources.ApplyResources(this.m_ButtonStartClean, "m_ButtonStartClean");
            this.m_ButtonStartClean.Name = "m_ButtonStartClean";
            this.m_ButtonStartClean.UseVisualStyleBackColor = true;
            this.m_ButtonStartClean.Click += new System.EventHandler(this.m_ButtonStartClean_Click);
            // 
            // ColorjetManualCleanSettingA
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_GroupBoxCleanParam);
            this.Controls.Add(this.m_GroupBoxClean);
            this.Name = "ColorjetManualCleanSettingA";
            this.m_GroupBoxCleanParam.ResumeLayout(false);
            this.m_GroupBoxCleanParam.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOriginOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownXStartPos)).EndInit();
            this.m_GroupBoxClean.ResumeLayout(false);
            this.m_GroupBoxClean.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox m_GroupBoxCleanParam;
        private System.Windows.Forms.NumericUpDown m_NumericUpDownXStartPos;
        private System.Windows.Forms.Label m_LabelAxisXStartPos;
        private System.Windows.Forms.ComboBox m_ComboBoxHeaderNumPerTime;
        private System.Windows.Forms.Label m_LabelHeaderNum;
        private System.Windows.Forms.GroupBox m_GroupBoxClean;
        private System.Windows.Forms.FlowLayoutPanel panelColorMask;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Label m_LabelCleanColor;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonStartClean;
        private System.Windows.Forms.CheckBox checkBoxFunctionOn;
        private System.Windows.Forms.NumericUpDown numericOriginOffset;
        private System.Windows.Forms.Label labelOriginOffset;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.NumericUpDown numericDelayTime;
        private System.Windows.Forms.Label labelDelayTime;
        private System.Windows.Forms.NumericUpDown numericSpeed;
        private System.Windows.Forms.Button buttonZBottom;
        private System.Windows.Forms.Button buttonZCenter;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.Button buttonSet;
    }
}
