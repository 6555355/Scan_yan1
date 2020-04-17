namespace BYHXPrinterManager.Setting
{
    partial class UserExtensionSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserExtensionSetting));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grouperPosSet = new BYHXPrinterManager.GradientControls.Grouper();
            this.labelZDefaultPos = new System.Windows.Forms.Label();
            this.numericUpDownZDefaultPos = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownZSensorOffset = new System.Windows.Forms.NumericUpDown();
            this.labelZSensorOffset = new System.Windows.Forms.Label();
            this.numericUpDownFeedDistance = new System.Windows.Forms.NumericUpDown();
            this.labelFeedDistance = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_ToolTip = new System.Windows.Forms.ToolTip();
            this.tableLayoutPanel.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.grouperPosSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZDefaultPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZSensorOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFeedDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.grouperPosSet);
            this.panelLeft.Controls.Add(this.panel1);
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // grouperPosSet
            // 
            this.grouperPosSet.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.grouperPosSet.BorderThickness = 1F;
            this.grouperPosSet.Controls.Add(this.labelZDefaultPos);
            this.grouperPosSet.Controls.Add(this.numericUpDownZDefaultPos);
            this.grouperPosSet.Controls.Add(this.numericUpDownZSensorOffset);
            this.grouperPosSet.Controls.Add(this.labelZSensorOffset);
            this.grouperPosSet.Controls.Add(this.numericUpDownFeedDistance);
            this.grouperPosSet.Controls.Add(this.labelFeedDistance);
            this.grouperPosSet.GroupImage = null;
            resources.ApplyResources(this.grouperPosSet, "grouperPosSet");
            this.grouperPosSet.Name = "grouperPosSet";
            this.grouperPosSet.PaintGroupBox = false;
            this.grouperPosSet.RoundCorners = 5;
            this.grouperPosSet.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPosSet.ShadowControl = false;
            this.grouperPosSet.ShadowThickness = 3;
            this.grouperPosSet.TabStop = false;
            this.grouperPosSet.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.grouperPosSet.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // labelZDefaultPos
            // 
            resources.ApplyResources(this.labelZDefaultPos, "labelZDefaultPos");
            this.labelZDefaultPos.BackColor = System.Drawing.Color.Transparent;
            this.labelZDefaultPos.Name = "labelZDefaultPos";
            // 
            // numericUpDownZDefaultPos
            // 
            resources.ApplyResources(this.numericUpDownZDefaultPos, "numericUpDownZDefaultPos");
            this.numericUpDownZDefaultPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownZDefaultPos.Name = "numericUpDownZDefaultPos";
            this.m_ToolTip.SetToolTip(this.numericUpDownZDefaultPos, resources.GetString("numericUpDownZDefaultPos.ToolTip"));
            // 
            // numericUpDownZSensorOffset
            // 
            resources.ApplyResources(this.numericUpDownZSensorOffset, "numericUpDownZSensorOffset");
            this.numericUpDownZSensorOffset.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownZSensorOffset.Name = "numericUpDownZSensorOffset";
            this.m_ToolTip.SetToolTip(this.numericUpDownZSensorOffset, resources.GetString("numericUpDownZSensorOffset.ToolTip"));
            // 
            // labelZSensorOffset
            // 
            resources.ApplyResources(this.labelZSensorOffset, "labelZSensorOffset");
            this.labelZSensorOffset.BackColor = System.Drawing.Color.Transparent;
            this.labelZSensorOffset.Name = "labelZSensorOffset";
            // 
            // numericUpDownFeedDistance
            // 
            resources.ApplyResources(this.numericUpDownFeedDistance, "numericUpDownFeedDistance");
            this.numericUpDownFeedDistance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownFeedDistance.Name = "numericUpDownFeedDistance";
            this.m_ToolTip.SetToolTip(this.numericUpDownFeedDistance, resources.GetString("numericUpDownFeedDistance.ToolTip"));
            // 
            // labelFeedDistance
            // 
            resources.ApplyResources(this.labelFeedDistance, "labelFeedDistance");
            this.labelFeedDistance.BackColor = System.Drawing.Color.Transparent;
            this.labelFeedDistance.Name = "labelFeedDistance";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // UserExtensionSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UserExtensionSetting";
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.grouperPosSet.ResumeLayout(false);
            this.grouperPosSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZDefaultPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZSensorOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFeedDistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelLeft;
        private GradientControls.Grouper grouperPosSet;
        private System.Windows.Forms.NumericUpDown numericUpDownZDefaultPos;
        private System.Windows.Forms.NumericUpDown numericUpDownZSensorOffset;
        private System.Windows.Forms.Label labelZSensorOffset;
        private System.Windows.Forms.NumericUpDown numericUpDownFeedDistance;
        private System.Windows.Forms.Label labelFeedDistance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelZDefaultPos;
        private System.Windows.Forms.ToolTip m_ToolTip;
    }
}
