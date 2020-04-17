namespace BYHXPrinterManager.Setting
{
    partial class AutoBackHomeSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoBackHomeSetting));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grouperAutoCapping = new BYHXPrinterManager.GradientControls.Grouper();
            this.checkBoxsalverPos = new System.Windows.Forms.CheckBox();
            this.numSalverPos = new System.Windows.Forms.NumericUpDown();
            this.numCappingDelayTime = new System.Windows.Forms.NumericUpDown();
            this.numCappingPosZ = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableCappingPosZ = new System.Windows.Forms.CheckBox();
            this.numCappingPosY = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableCappingPosY = new System.Windows.Forms.CheckBox();
            this.numCappingPosX = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableCappingPosX = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableAutoCapping = new System.Windows.Forms.CheckBox();
            this.grouperAutoHoming = new BYHXPrinterManager.GradientControls.Grouper();
            this.numAutoHomeDelay = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnableHomeDelay = new System.Windows.Forms.CheckBox();
            this.numHomePosY = new System.Windows.Forms.NumericUpDown();
            this.checkBoxYHomePos = new System.Windows.Forms.CheckBox();
            this.numHomePosX = new System.Windows.Forms.NumericUpDown();
            this.checkBoxXHomePos = new System.Windows.Forms.CheckBox();
            this.grouperAutoCapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalverPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingDelayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosX)).BeginInit();
            this.grouperAutoHoming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoHomeDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHomePosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHomePosX)).BeginInit();
            this.SuspendLayout();
            // 
            // grouperAutoCapping
            // 
            resources.ApplyResources(this.grouperAutoCapping, "grouperAutoCapping");
            this.grouperAutoCapping.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperAutoCapping.BorderThickness = 1F;
            this.grouperAutoCapping.Controls.Add(this.checkBoxsalverPos);
            this.grouperAutoCapping.Controls.Add(this.numSalverPos);
            this.grouperAutoCapping.Controls.Add(this.numCappingDelayTime);
            this.grouperAutoCapping.Controls.Add(this.numCappingPosZ);
            this.grouperAutoCapping.Controls.Add(this.checkBoxEnableCappingPosZ);
            this.grouperAutoCapping.Controls.Add(this.numCappingPosY);
            this.grouperAutoCapping.Controls.Add(this.checkBoxEnableCappingPosY);
            this.grouperAutoCapping.Controls.Add(this.numCappingPosX);
            this.grouperAutoCapping.Controls.Add(this.checkBoxEnableCappingPosX);
            this.grouperAutoCapping.Controls.Add(this.checkBoxEnableAutoCapping);
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperAutoCapping.GradientColors = style1;
            this.grouperAutoCapping.GroupImage = null;
            this.grouperAutoCapping.Name = "grouperAutoCapping";
            this.grouperAutoCapping.PaintGroupBox = false;
            this.grouperAutoCapping.RoundCorners = 10;
            this.grouperAutoCapping.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAutoCapping.ShadowControl = false;
            this.grouperAutoCapping.ShadowThickness = 3;
            this.grouperAutoCapping.TabStop = false;
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperAutoCapping.TitileGradientColors = style2;
            this.grouperAutoCapping.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperAutoCapping.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // checkBoxsalverPos
            // 
            resources.ApplyResources(this.checkBoxsalverPos, "checkBoxsalverPos");
            this.checkBoxsalverPos.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxsalverPos.Name = "checkBoxsalverPos";
            this.checkBoxsalverPos.UseVisualStyleBackColor = false;
            // 
            // numSalverPos
            // 
            resources.ApplyResources(this.numSalverPos, "numSalverPos");
            this.numSalverPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numSalverPos.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numSalverPos.Name = "numSalverPos";
            // 
            // numCappingDelayTime
            // 
            this.numCappingDelayTime.DecimalPlaces = 1;
            resources.ApplyResources(this.numCappingDelayTime, "numCappingDelayTime");
            this.numCappingDelayTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCappingDelayTime.Name = "numCappingDelayTime";
            // 
            // numCappingPosZ
            // 
            this.numCappingPosZ.DecimalPlaces = 1;
            resources.ApplyResources(this.numCappingPosZ, "numCappingPosZ");
            this.numCappingPosZ.Name = "numCappingPosZ";
            // 
            // checkBoxEnableCappingPosZ
            // 
            resources.ApplyResources(this.checkBoxEnableCappingPosZ, "checkBoxEnableCappingPosZ");
            this.checkBoxEnableCappingPosZ.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableCappingPosZ.Name = "checkBoxEnableCappingPosZ";
            this.checkBoxEnableCappingPosZ.UseVisualStyleBackColor = false;
            // 
            // numCappingPosY
            // 
            this.numCappingPosY.DecimalPlaces = 1;
            resources.ApplyResources(this.numCappingPosY, "numCappingPosY");
            this.numCappingPosY.Name = "numCappingPosY";
            // 
            // checkBoxEnableCappingPosY
            // 
            resources.ApplyResources(this.checkBoxEnableCappingPosY, "checkBoxEnableCappingPosY");
            this.checkBoxEnableCappingPosY.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableCappingPosY.Name = "checkBoxEnableCappingPosY";
            this.checkBoxEnableCappingPosY.UseVisualStyleBackColor = false;
            // 
            // numCappingPosX
            // 
            this.numCappingPosX.DecimalPlaces = 1;
            resources.ApplyResources(this.numCappingPosX, "numCappingPosX");
            this.numCappingPosX.Name = "numCappingPosX";
            // 
            // checkBoxEnableCappingPosX
            // 
            resources.ApplyResources(this.checkBoxEnableCappingPosX, "checkBoxEnableCappingPosX");
            this.checkBoxEnableCappingPosX.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableCappingPosX.Name = "checkBoxEnableCappingPosX";
            this.checkBoxEnableCappingPosX.UseVisualStyleBackColor = false;
            // 
            // checkBoxEnableAutoCapping
            // 
            resources.ApplyResources(this.checkBoxEnableAutoCapping, "checkBoxEnableAutoCapping");
            this.checkBoxEnableAutoCapping.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableAutoCapping.Name = "checkBoxEnableAutoCapping";
            this.checkBoxEnableAutoCapping.UseVisualStyleBackColor = false;
            // 
            // grouperAutoHoming
            // 
            resources.ApplyResources(this.grouperAutoHoming, "grouperAutoHoming");
            this.grouperAutoHoming.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperAutoHoming.BorderThickness = 1F;
            this.grouperAutoHoming.Controls.Add(this.numAutoHomeDelay);
            this.grouperAutoHoming.Controls.Add(this.checkBoxEnableHomeDelay);
            this.grouperAutoHoming.Controls.Add(this.numHomePosY);
            this.grouperAutoHoming.Controls.Add(this.checkBoxYHomePos);
            this.grouperAutoHoming.Controls.Add(this.numHomePosX);
            this.grouperAutoHoming.Controls.Add(this.checkBoxXHomePos);
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperAutoHoming.GradientColors = style3;
            this.grouperAutoHoming.GroupImage = null;
            this.grouperAutoHoming.Name = "grouperAutoHoming";
            this.grouperAutoHoming.PaintGroupBox = false;
            this.grouperAutoHoming.RoundCorners = 10;
            this.grouperAutoHoming.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAutoHoming.ShadowControl = false;
            this.grouperAutoHoming.ShadowThickness = 3;
            this.grouperAutoHoming.TabStop = false;
            style4.Color1 = System.Drawing.Color.LightBlue;
            style4.Color2 = System.Drawing.Color.SteelBlue;
            this.grouperAutoHoming.TitileGradientColors = style4;
            this.grouperAutoHoming.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Default;
            this.grouperAutoHoming.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // numAutoHomeDelay
            // 
            this.numAutoHomeDelay.DecimalPlaces = 1;
            resources.ApplyResources(this.numAutoHomeDelay, "numAutoHomeDelay");
            this.numAutoHomeDelay.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numAutoHomeDelay.Name = "numAutoHomeDelay";
            // 
            // checkBoxEnableHomeDelay
            // 
            resources.ApplyResources(this.checkBoxEnableHomeDelay, "checkBoxEnableHomeDelay");
            this.checkBoxEnableHomeDelay.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEnableHomeDelay.Name = "checkBoxEnableHomeDelay";
            this.checkBoxEnableHomeDelay.UseVisualStyleBackColor = false;
            // 
            // numHomePosY
            // 
            this.numHomePosY.DecimalPlaces = 1;
            resources.ApplyResources(this.numHomePosY, "numHomePosY");
            this.numHomePosY.Name = "numHomePosY";
            // 
            // checkBoxYHomePos
            // 
            resources.ApplyResources(this.checkBoxYHomePos, "checkBoxYHomePos");
            this.checkBoxYHomePos.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxYHomePos.Name = "checkBoxYHomePos";
            this.checkBoxYHomePos.UseVisualStyleBackColor = false;
            // 
            // numHomePosX
            // 
            this.numHomePosX.DecimalPlaces = 1;
            resources.ApplyResources(this.numHomePosX, "numHomePosX");
            this.numHomePosX.Name = "numHomePosX";
            // 
            // checkBoxXHomePos
            // 
            resources.ApplyResources(this.checkBoxXHomePos, "checkBoxXHomePos");
            this.checkBoxXHomePos.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxXHomePos.Name = "checkBoxXHomePos";
            this.checkBoxXHomePos.UseVisualStyleBackColor = false;
            // 
            // AutoBackHomeSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grouperAutoCapping);
            this.Controls.Add(this.grouperAutoHoming);
            this.Name = "AutoBackHomeSetting";
            this.grouperAutoCapping.ResumeLayout(false);
            this.grouperAutoCapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalverPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCappingPosX)).EndInit();
            this.grouperAutoHoming.ResumeLayout(false);
            this.grouperAutoHoming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoHomeDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHomePosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHomePosX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GradientControls.Grouper grouperAutoCapping;
        private System.Windows.Forms.NumericUpDown numCappingDelayTime;
        private System.Windows.Forms.NumericUpDown numCappingPosZ;
        private System.Windows.Forms.CheckBox checkBoxEnableCappingPosZ;
        private System.Windows.Forms.NumericUpDown numCappingPosY;
        private System.Windows.Forms.CheckBox checkBoxEnableCappingPosY;
        private System.Windows.Forms.NumericUpDown numCappingPosX;
        private System.Windows.Forms.CheckBox checkBoxEnableCappingPosX;
        private System.Windows.Forms.CheckBox checkBoxEnableAutoCapping;
        public GradientControls.Grouper grouperAutoHoming;
        private System.Windows.Forms.NumericUpDown numAutoHomeDelay;
        private System.Windows.Forms.CheckBox checkBoxEnableHomeDelay;
        private System.Windows.Forms.NumericUpDown numHomePosY;
        private System.Windows.Forms.CheckBox checkBoxYHomePos;
        private System.Windows.Forms.NumericUpDown numHomePosX;
        private System.Windows.Forms.CheckBox checkBoxXHomePos;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.NumericUpDown numSalverPos;
        private System.Windows.Forms.CheckBox checkBoxsalverPos;
    }
}
