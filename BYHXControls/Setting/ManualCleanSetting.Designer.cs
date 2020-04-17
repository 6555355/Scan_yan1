namespace BYHXPrinterManager.Setting
{
    partial class ManualCleanSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualCleanSetting));
            this.m_GroupBoxClean = new System.Windows.Forms.GroupBox();
            this.buttonZBottom = new System.Windows.Forms.Button();
            this.buttonZCenter = new System.Windows.Forms.Button();
            this.labelCleanTime = new System.Windows.Forms.Label();
            this.panelColorMask = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.m_LabelCleanColor = new System.Windows.Forms.Label();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.m_ButtonStartClean = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelxAxisPos = new System.Windows.Forms.Label();
            this.numericXAxisPos = new System.Windows.Forms.NumericUpDown();
            this.labelScrapPlatformzPos = new System.Windows.Forms.Label();
            this.numericScrapPlatformZPos = new System.Windows.Forms.NumericUpDown();
            this.labelBladeYMobileDistance = new System.Windows.Forms.Label();
            this.numericBladeYMobileDistance = new System.Windows.Forms.NumericUpDown();
            this.labelBladeCleanDuration = new System.Windows.Forms.Label();
            this.numericBladeCleanDuration = new System.Windows.Forms.NumericUpDown();
            this.buttonSet = new System.Windows.Forms.Button();
            this.m_GroupBoxCleanParamAoKang = new System.Windows.Forms.GroupBox();
            this.m_GroupBoxClean.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericXAxisPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScrapPlatformZPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBladeYMobileDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBladeCleanDuration)).BeginInit();
            this.m_GroupBoxCleanParamAoKang.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_GroupBoxClean
            // 
            this.m_GroupBoxClean.Controls.Add(this.buttonZBottom);
            this.m_GroupBoxClean.Controls.Add(this.buttonZCenter);
            this.m_GroupBoxClean.Controls.Add(this.labelCleanTime);
            this.m_GroupBoxClean.Controls.Add(this.panelColorMask);
            this.m_GroupBoxClean.Controls.Add(this.comboBoxLevel);
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
            // labelCleanTime
            // 
            resources.ApplyResources(this.labelCleanTime, "labelCleanTime");
            this.labelCleanTime.Name = "labelCleanTime";
            // 
            // panelColorMask
            // 
            resources.ApplyResources(this.panelColorMask, "panelColorMask");
            this.panelColorMask.Name = "panelColorMask";
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Items.AddRange(new object[] {
            resources.GetString("comboBoxLevel.Items"),
            resources.GetString("comboBoxLevel.Items1"),
            resources.GetString("comboBoxLevel.Items2"),
            resources.GetString("comboBoxLevel.Items3"),
            resources.GetString("comboBoxLevel.Items4"),
            resources.GetString("comboBoxLevel.Items5"),
            resources.GetString("comboBoxLevel.Items6"),
            resources.GetString("comboBoxLevel.Items7"),
            resources.GetString("comboBoxLevel.Items8"),
            resources.GetString("comboBoxLevel.Items9")});
            resources.ApplyResources(this.comboBoxLevel, "comboBoxLevel");
            this.comboBoxLevel.Name = "comboBoxLevel";
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
            // labelxAxisPos
            // 
            resources.ApplyResources(this.labelxAxisPos, "labelxAxisPos");
            this.labelxAxisPos.Name = "labelxAxisPos";
            // 
            // numericXAxisPos
            // 
            resources.ApplyResources(this.numericXAxisPos, "numericXAxisPos");
            this.numericXAxisPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericXAxisPos.Name = "numericXAxisPos";
            // 
            // labelScrapPlatformzPos
            // 
            resources.ApplyResources(this.labelScrapPlatformzPos, "labelScrapPlatformzPos");
            this.labelScrapPlatformzPos.Name = "labelScrapPlatformzPos";
            // 
            // numericScrapPlatformZPos
            // 
            resources.ApplyResources(this.numericScrapPlatformZPos, "numericScrapPlatformZPos");
            this.numericScrapPlatformZPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericScrapPlatformZPos.Name = "numericScrapPlatformZPos";
            // 
            // labelBladeYMobileDistance
            // 
            resources.ApplyResources(this.labelBladeYMobileDistance, "labelBladeYMobileDistance");
            this.labelBladeYMobileDistance.Name = "labelBladeYMobileDistance";
            // 
            // numericBladeYMobileDistance
            // 
            resources.ApplyResources(this.numericBladeYMobileDistance, "numericBladeYMobileDistance");
            this.numericBladeYMobileDistance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericBladeYMobileDistance.Name = "numericBladeYMobileDistance";
            // 
            // labelBladeCleanDuration
            // 
            resources.ApplyResources(this.labelBladeCleanDuration, "labelBladeCleanDuration");
            this.labelBladeCleanDuration.Name = "labelBladeCleanDuration";
            // 
            // numericBladeCleanDuration
            // 
            resources.ApplyResources(this.numericBladeCleanDuration, "numericBladeCleanDuration");
            this.numericBladeCleanDuration.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericBladeCleanDuration.Name = "numericBladeCleanDuration";
            // 
            // buttonSet
            // 
            resources.ApplyResources(this.buttonSet, "buttonSet");
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // m_GroupBoxCleanParamAoKang
            // 
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.buttonSet);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.numericBladeCleanDuration);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.labelBladeCleanDuration);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.numericBladeYMobileDistance);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.labelBladeYMobileDistance);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.numericScrapPlatformZPos);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.labelScrapPlatformzPos);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.numericXAxisPos);
            this.m_GroupBoxCleanParamAoKang.Controls.Add(this.labelxAxisPos);
            resources.ApplyResources(this.m_GroupBoxCleanParamAoKang, "m_GroupBoxCleanParamAoKang");
            this.m_GroupBoxCleanParamAoKang.Name = "m_GroupBoxCleanParamAoKang";
            this.m_GroupBoxCleanParamAoKang.TabStop = false;
            // 
            // ManualCleanSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_GroupBoxClean);
            this.Controls.Add(this.m_GroupBoxCleanParamAoKang);
            this.Name = "ManualCleanSetting";
            this.m_GroupBoxClean.ResumeLayout(false);
            this.m_GroupBoxClean.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericXAxisPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScrapPlatformZPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBladeYMobileDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBladeCleanDuration)).EndInit();
            this.m_GroupBoxCleanParamAoKang.ResumeLayout(false);
            this.m_GroupBoxCleanParamAoKang.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_GroupBoxClean;
        private System.Windows.Forms.Label labelCleanTime;
        private System.Windows.Forms.FlowLayoutPanel panelColorMask;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Label m_LabelCleanColor;
        private System.Windows.Forms.Button m_ButtonCancel;
        private System.Windows.Forms.Button m_ButtonStartClean;
        private System.Windows.Forms.Button buttonZBottom;
        private System.Windows.Forms.Button buttonZCenter;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.Label labelxAxisPos;
        private System.Windows.Forms.NumericUpDown numericXAxisPos;
        private System.Windows.Forms.Label labelScrapPlatformzPos;
        private System.Windows.Forms.NumericUpDown numericScrapPlatformZPos;
        private System.Windows.Forms.Label labelBladeYMobileDistance;
        private System.Windows.Forms.NumericUpDown numericBladeYMobileDistance;
        private System.Windows.Forms.Label labelBladeCleanDuration;
        private System.Windows.Forms.NumericUpDown numericBladeCleanDuration;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.GroupBox m_GroupBoxCleanParamAoKang;
    }
}
