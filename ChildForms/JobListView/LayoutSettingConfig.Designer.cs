namespace BYHXPrinterManager.JobListView
{
    partial class LayoutSettingConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutSettingConfig));
            this.m_listBoxLayout = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_buttonCopyAs = new System.Windows.Forms.Button();
            this.m_buttonRemoveMode = new System.Windows.Forms.Button();
            this.m_buttonAddMode = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupHoriz = new System.Windows.Forms.GroupBox();
            this.listHorGroupNum = new System.Windows.Forms.ComboBox();
            this.panelHG = new System.Windows.Forms.Panel();
            this.cbxHG4 = new System.Windows.Forms.CheckBox();
            this.cbxHG3 = new System.Windows.Forms.CheckBox();
            this.cbxHG2 = new System.Windows.Forms.CheckBox();
            this.cbxHG1 = new System.Windows.Forms.CheckBox();
            this.labelHorGroupNum = new System.Windows.Forms.Label();
            this.subLayerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.listSubLayerNum = new System.Windows.Forms.ComboBox();
            this.lblSubLayerNum = new System.Windows.Forms.Label();
            this.numYOffset = new System.Windows.Forms.NumericUpDown();
            this.lblYOffset = new System.Windows.Forms.Label();
            this.numYContinue = new System.Windows.Forms.NumericUpDown();
            this.lblYContinue = new System.Windows.Forms.Label();
            this.numYinterleaveNum = new System.Windows.Forms.NumericUpDown();
            this.lblYinterleaveNum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numLayerSpaceY = new System.Windows.Forms.NumericUpDown();
            this.lblLayerSpaceY = new System.Windows.Forms.Label();
            this.cbxSpecialLayout = new System.Windows.Forms.CheckBox();
            this.listBaseLayer = new System.Windows.Forms.ComboBox();
            this.lblBaseLayer = new System.Windows.Forms.Label();
            this.listLayerNum = new System.Windows.Forms.ComboBox();
            this.cbxListLayer = new System.Windows.Forms.CheckedListBox();
            this.lblLayerNum = new System.Windows.Forms.Label();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupHoriz.SuspendLayout();
            this.panelHG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYContinue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYinterleaveNum)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLayerSpaceY)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listBoxLayout
            // 
            resources.ApplyResources(this.m_listBoxLayout, "m_listBoxLayout");
            this.m_listBoxLayout.FormattingEnabled = true;
            this.m_listBoxLayout.Name = "m_listBoxLayout";
            this.m_listBoxLayout.SelectedIndexChanged += new System.EventHandler(this.m_listBoxLayout_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_listBoxLayout);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // m_buttonCopyAs
            // 
            resources.ApplyResources(this.m_buttonCopyAs, "m_buttonCopyAs");
            this.m_buttonCopyAs.Name = "m_buttonCopyAs";
            this.m_buttonCopyAs.UseVisualStyleBackColor = true;
            this.m_buttonCopyAs.Click += new System.EventHandler(this.m_buttonCopyAs_Click);
            // 
            // m_buttonRemoveMode
            // 
            resources.ApplyResources(this.m_buttonRemoveMode, "m_buttonRemoveMode");
            this.m_buttonRemoveMode.Name = "m_buttonRemoveMode";
            this.m_buttonRemoveMode.UseVisualStyleBackColor = true;
            this.m_buttonRemoveMode.Click += new System.EventHandler(this.m_buttonRemoveMode_Click);
            // 
            // m_buttonAddMode
            // 
            resources.ApplyResources(this.m_buttonAddMode, "m_buttonAddMode");
            this.m_buttonAddMode.Name = "m_buttonAddMode";
            this.m_buttonAddMode.UseVisualStyleBackColor = true;
            this.m_buttonAddMode.Click += new System.EventHandler(this.m_buttonAddMode_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.groupHoriz);
            this.groupBox3.Controls.Add(this.subLayerPanel);
            this.groupBox3.Controls.Add(this.listSubLayerNum);
            this.groupBox3.Controls.Add(this.lblSubLayerNum);
            this.groupBox3.Controls.Add(this.numYOffset);
            this.groupBox3.Controls.Add(this.lblYOffset);
            this.groupBox3.Controls.Add(this.numYContinue);
            this.groupBox3.Controls.Add(this.lblYContinue);
            this.groupBox3.Controls.Add(this.numYinterleaveNum);
            this.groupBox3.Controls.Add(this.lblYinterleaveNum);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupHoriz
            // 
            this.groupHoriz.Controls.Add(this.listHorGroupNum);
            this.groupHoriz.Controls.Add(this.panelHG);
            this.groupHoriz.Controls.Add(this.labelHorGroupNum);
            resources.ApplyResources(this.groupHoriz, "groupHoriz");
            this.groupHoriz.Name = "groupHoriz";
            this.groupHoriz.TabStop = false;
            // 
            // listHorGroupNum
            // 
            this.listHorGroupNum.FormattingEnabled = true;
            resources.ApplyResources(this.listHorGroupNum, "listHorGroupNum");
            this.listHorGroupNum.Name = "listHorGroupNum";
            this.listHorGroupNum.SelectedIndexChanged += new System.EventHandler(this.listHorGroupNum_SelectedIndexChanged);
            // 
            // panelHG
            // 
            this.panelHG.Controls.Add(this.cbxHG4);
            this.panelHG.Controls.Add(this.cbxHG3);
            this.panelHG.Controls.Add(this.cbxHG2);
            this.panelHG.Controls.Add(this.cbxHG1);
            resources.ApplyResources(this.panelHG, "panelHG");
            this.panelHG.Name = "panelHG";
            // 
            // cbxHG4
            // 
            resources.ApplyResources(this.cbxHG4, "cbxHG4");
            this.cbxHG4.Checked = true;
            this.cbxHG4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxHG4.Name = "cbxHG4";
            this.cbxHG4.UseVisualStyleBackColor = true;
            // 
            // cbxHG3
            // 
            resources.ApplyResources(this.cbxHG3, "cbxHG3");
            this.cbxHG3.Checked = true;
            this.cbxHG3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxHG3.Name = "cbxHG3";
            this.cbxHG3.UseVisualStyleBackColor = true;
            // 
            // cbxHG2
            // 
            resources.ApplyResources(this.cbxHG2, "cbxHG2");
            this.cbxHG2.Checked = true;
            this.cbxHG2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxHG2.Name = "cbxHG2";
            this.cbxHG2.UseVisualStyleBackColor = true;
            // 
            // cbxHG1
            // 
            resources.ApplyResources(this.cbxHG1, "cbxHG1");
            this.cbxHG1.Checked = true;
            this.cbxHG1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxHG1.Name = "cbxHG1";
            this.cbxHG1.UseVisualStyleBackColor = true;
            // 
            // labelHorGroupNum
            // 
            resources.ApplyResources(this.labelHorGroupNum, "labelHorGroupNum");
            this.labelHorGroupNum.Name = "labelHorGroupNum";
            // 
            // subLayerPanel
            // 
            resources.ApplyResources(this.subLayerPanel, "subLayerPanel");
            this.subLayerPanel.Name = "subLayerPanel";
            // 
            // listSubLayerNum
            // 
            this.listSubLayerNum.FormattingEnabled = true;
            this.listSubLayerNum.Items.AddRange(new object[] {
            resources.GetString("listSubLayerNum.Items"),
            resources.GetString("listSubLayerNum.Items1"),
            resources.GetString("listSubLayerNum.Items2"),
            resources.GetString("listSubLayerNum.Items3"),
            resources.GetString("listSubLayerNum.Items4"),
            resources.GetString("listSubLayerNum.Items5"),
            resources.GetString("listSubLayerNum.Items6"),
            resources.GetString("listSubLayerNum.Items7")});
            resources.ApplyResources(this.listSubLayerNum, "listSubLayerNum");
            this.listSubLayerNum.Name = "listSubLayerNum";
            this.listSubLayerNum.SelectedIndexChanged += new System.EventHandler(this.listSubLayerNum_SelectedIndexChanged);
            // 
            // lblSubLayerNum
            // 
            resources.ApplyResources(this.lblSubLayerNum, "lblSubLayerNum");
            this.lblSubLayerNum.Name = "lblSubLayerNum";
            // 
            // numYOffset
            // 
            this.numYOffset.DecimalPlaces = 2;
            resources.ApplyResources(this.numYOffset, "numYOffset");
            this.numYOffset.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numYOffset.Name = "numYOffset";
            // 
            // lblYOffset
            // 
            resources.ApplyResources(this.lblYOffset, "lblYOffset");
            this.lblYOffset.Name = "lblYOffset";
            // 
            // numYContinue
            // 
            resources.ApplyResources(this.numYContinue, "numYContinue");
            this.numYContinue.Name = "numYContinue";
            // 
            // lblYContinue
            // 
            resources.ApplyResources(this.lblYContinue, "lblYContinue");
            this.lblYContinue.Name = "lblYContinue";
            // 
            // numYinterleaveNum
            // 
            resources.ApplyResources(this.numYinterleaveNum, "numYinterleaveNum");
            this.numYinterleaveNum.Name = "numYinterleaveNum";
            // 
            // lblYinterleaveNum
            // 
            resources.ApplyResources(this.lblYinterleaveNum, "lblYinterleaveNum");
            this.lblYinterleaveNum.Name = "lblYinterleaveNum";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numLayerSpaceY);
            this.groupBox1.Controls.Add(this.lblLayerSpaceY);
            this.groupBox1.Controls.Add(this.cbxSpecialLayout);
            this.groupBox1.Controls.Add(this.listBaseLayer);
            this.groupBox1.Controls.Add(this.lblBaseLayer);
            this.groupBox1.Controls.Add(this.listLayerNum);
            this.groupBox1.Controls.Add(this.cbxListLayer);
            this.groupBox1.Controls.Add(this.lblLayerNum);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numLayerSpaceY
            // 
            resources.ApplyResources(this.numLayerSpaceY, "numLayerSpaceY");
            this.numLayerSpaceY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numLayerSpaceY.Name = "numLayerSpaceY";
            // 
            // lblLayerSpaceY
            // 
            resources.ApplyResources(this.lblLayerSpaceY, "lblLayerSpaceY");
            this.lblLayerSpaceY.Name = "lblLayerSpaceY";
            // 
            // cbxSpecialLayout
            // 
            resources.ApplyResources(this.cbxSpecialLayout, "cbxSpecialLayout");
            this.cbxSpecialLayout.Name = "cbxSpecialLayout";
            this.cbxSpecialLayout.UseVisualStyleBackColor = true;
            // 
            // listBaseLayer
            // 
            resources.ApplyResources(this.listBaseLayer, "listBaseLayer");
            this.listBaseLayer.FormattingEnabled = true;
            this.listBaseLayer.Name = "listBaseLayer";
            // 
            // lblBaseLayer
            // 
            resources.ApplyResources(this.lblBaseLayer, "lblBaseLayer");
            this.lblBaseLayer.Name = "lblBaseLayer";
            // 
            // listLayerNum
            // 
            this.listLayerNum.FormattingEnabled = true;
            this.listLayerNum.Items.AddRange(new object[] {
            resources.GetString("listLayerNum.Items"),
            resources.GetString("listLayerNum.Items1"),
            resources.GetString("listLayerNum.Items2"),
            resources.GetString("listLayerNum.Items3"),
            resources.GetString("listLayerNum.Items4"),
            resources.GetString("listLayerNum.Items5"),
            resources.GetString("listLayerNum.Items6"),
            resources.GetString("listLayerNum.Items7")});
            resources.ApplyResources(this.listLayerNum, "listLayerNum");
            this.listLayerNum.Name = "listLayerNum";
            this.listLayerNum.SelectedIndexChanged += new System.EventHandler(this.listLayerNum_SelectedIndexChanged);
            // 
            // cbxListLayer
            // 
            this.cbxListLayer.FormattingEnabled = true;
            resources.ApplyResources(this.cbxListLayer, "cbxListLayer");
            this.cbxListLayer.Name = "cbxListLayer";
            this.cbxListLayer.SelectedIndexChanged += new System.EventHandler(this.cbxListLayer_SelectedIndexChanged);
            // 
            // lblLayerNum
            // 
            resources.ApplyResources(this.lblLayerNum, "lblLayerNum");
            this.lblLayerNum.Name = "lblLayerNum";
            // 
            // m_buttonSave
            // 
            resources.ApplyResources(this.m_buttonSave, "m_buttonSave");
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.btnSaveLayout_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnImport);
            this.groupBox4.Controls.Add(this.btnExport);
            this.groupBox4.Controls.Add(this.m_buttonAddMode);
            this.groupBox4.Controls.Add(this.m_buttonSave);
            this.groupBox4.Controls.Add(this.m_buttonCopyAs);
            this.groupBox4.Controls.Add(this.m_buttonRemoveMode);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // btnImport
            // 
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.Name = "btnImport";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // LayoutSettingConfig
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayoutSettingConfig";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LayoutSettingConfig_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupHoriz.ResumeLayout(false);
            this.groupHoriz.PerformLayout();
            this.panelHG.ResumeLayout(false);
            this.panelHG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYContinue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYinterleaveNum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLayerSpaceY)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox m_listBoxLayout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button m_buttonRemoveMode;
        private System.Windows.Forms.Button m_buttonAddMode;
        private System.Windows.Forms.Button m_buttonCopyAs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox cbxListLayer;
        private System.Windows.Forms.Label lblLayerNum;
        private System.Windows.Forms.ComboBox listLayerNum;
        private System.Windows.Forms.ComboBox listBaseLayer;
        private System.Windows.Forms.Label lblBaseLayer;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.NumericUpDown numYinterleaveNum;
        private System.Windows.Forms.Label lblYinterleaveNum;
        private System.Windows.Forms.NumericUpDown numYContinue;
        private System.Windows.Forms.Label lblYContinue;
        private System.Windows.Forms.NumericUpDown numYOffset;
        private System.Windows.Forms.Label lblYOffset;
        private System.Windows.Forms.ComboBox listSubLayerNum;
        private System.Windows.Forms.Label lblSubLayerNum;
        private System.Windows.Forms.FlowLayoutPanel subLayerPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbxSpecialLayout;
        private System.Windows.Forms.NumericUpDown numLayerSpaceY;
        private System.Windows.Forms.Label lblLayerSpaceY;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label labelHorGroupNum;
        private System.Windows.Forms.GroupBox groupHoriz;
        private System.Windows.Forms.CheckBox cbxHG1;
        private System.Windows.Forms.Panel panelHG;
        private System.Windows.Forms.CheckBox cbxHG4;
        private System.Windows.Forms.CheckBox cbxHG3;
        private System.Windows.Forms.CheckBox cbxHG2;
        private System.Windows.Forms.ComboBox listHorGroupNum;
    }
}