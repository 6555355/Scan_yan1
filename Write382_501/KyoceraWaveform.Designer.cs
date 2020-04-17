using System.Windows.Forms;

namespace Write382
{
    partial class KyoceraWaveform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KyoceraWaveform));
            this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
            this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
            this.progressBarStatu = new System.Windows.Forms.ProgressBar();
            this.buttonRead = new System.Windows.Forms.Button();
            this.m_buttonUpdata = new System.Windows.Forms.Button();
            this.m_buttonFile = new System.Windows.Forms.Button();
            this.m_textBoxPath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxText = new System.Windows.Forms.RichTextBox();
            this.richTextBoxAscii = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonDelayU24 = new System.Windows.Forms.Button();
            this.buttonDelayU13 = new System.Windows.Forms.Button();
            this.numHeadBoardindex = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numHeadIndex = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBoxWriteDelaySettingForKJ4A_RH06 = new System.Windows.Forms.GroupBox();
            this.buttonWriteDelaySettingForKJ4A_RH0624 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.buttonWriteDelaySettingForKJ4A_RH0613 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBoxWriteTempLimitsForKJ4A_RH06 = new System.Windows.Forms.GroupBox();
            this.buttonReadTempLimitForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.buttonSetTempLimitForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.numericUpDownTempLimitLForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownTempLimitUForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBoxWriteVoltageSettingForKJ4A_RH06 = new System.Windows.Forms.GroupBox();
            this.buttonSetVolSetForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.buttonReadVolSetForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.numericUpDownVolSet4ForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVolSet3ForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVolSet2ForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVolSet1ForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06 = new System.Windows.Forms.GroupBox();
            this.buttonSetAdjustVolForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.buttonReadAdjustVolForKJ4A_RH06 = new System.Windows.Forms.Button();
            this.numericUpDownAdjustVolForKJ4A_RH06 = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06 = new System.Windows.Forms.GroupBox();
            this.buttonReadAdjustVol2 = new System.Windows.Forms.Button();
            this.buttonSetAdjustVol2 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBoxWriteDlysel = new System.Windows.Forms.GroupBox();
            this.comboBoxDlysel4 = new System.Windows.Forms.ComboBox();
            this.comboBoxDlysel3 = new System.Windows.Forms.ComboBox();
            this.comboBoxDlysel2 = new System.Windows.Forms.ComboBox();
            this.comboBoxDlysel1 = new System.Windows.Forms.ComboBox();
            this.buttonReadDlysel = new System.Windows.Forms.Button();
            this.buttonSetDlysel = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBoxWriteAdjustmentVoltage = new System.Windows.Forms.GroupBox();
            this.buttonReadAdjustVol = new System.Windows.Forms.Button();
            this.buttonSetAdjustVol = new System.Windows.Forms.Button();
            this.numAdjustVolB = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numAdjustVolA = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxWriteVoltageSetting = new System.Windows.Forms.GroupBox();
            this.buttonReadBaseVol = new System.Windows.Forms.Button();
            this.buttonSetBaseVol = new System.Windows.Forms.Button();
            this.numBaseVolB = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numBaseVolA = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxWriteTempLimit = new System.Windows.Forms.GroupBox();
            this.buttonReadTempLimit = new System.Windows.Forms.Button();
            this.buttonSetTempLimit = new System.Windows.Forms.Button();
            this.numTempLimitB = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numTempLimitA = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxHbIndexVT = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numHdIndexVT = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numHBIndexW = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBoxB = new System.Windows.Forms.CheckBox();
            this.checkBoxA = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonPrintPrt = new System.Windows.Forms.Button();
            this.checkBoxUseDefaultWF = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeadIndex)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBoxWriteDelaySettingForKJ4A_RH06.SuspendLayout();
            this.groupBoxWriteTempLimitsForKJ4A_RH06.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempLimitLForKJ4A_RH06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempLimitUForKJ4A_RH06)).BeginInit();
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet4ForKJ4A_RH06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet3ForKJ4A_RH06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet2ForKJ4A_RH06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet1ForKJ4A_RH06)).BeginInit();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdjustVolForKJ4A_RH06)).BeginInit();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBoxWriteDlysel.SuspendLayout();
            this.groupBoxWriteAdjustmentVoltage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustVolB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustVolA)).BeginInit();
            this.groupBoxWriteVoltageSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseVolB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseVolA)).BeginInit();
            this.groupBoxWriteTempLimit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLimitB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLimitA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdIndexVT)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_StatusBarApp
            // 
            resources.ApplyResources(this.m_StatusBarApp, "m_StatusBarApp");
            this.m_StatusBarApp.Name = "m_StatusBarApp";
            this.m_StatusBarApp.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.m_StatusBarPanelJetStaus,
            this.m_StatusBarPanelError,
            this.m_StatusBarPanelPercent,
            this.m_StatusBarPanelComment});
            this.m_StatusBarApp.ShowPanels = true;
            // 
            // m_StatusBarPanelJetStaus
            // 
            this.m_StatusBarPanelJetStaus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelJetStaus, "m_StatusBarPanelJetStaus");
            // 
            // m_StatusBarPanelError
            // 
            this.m_StatusBarPanelError.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelError, "m_StatusBarPanelError");
            // 
            // m_StatusBarPanelPercent
            // 
            this.m_StatusBarPanelPercent.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelPercent, "m_StatusBarPanelPercent");
            // 
            // m_StatusBarPanelComment
            // 
            this.m_StatusBarPanelComment.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            resources.ApplyResources(this.m_StatusBarPanelComment, "m_StatusBarPanelComment");
            // 
            // progressBarStatu
            // 
            resources.ApplyResources(this.progressBarStatu, "progressBarStatu");
            this.progressBarStatu.Name = "progressBarStatu";
            // 
            // buttonRead
            // 
            resources.ApplyResources(this.buttonRead, "buttonRead");
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // m_buttonUpdata
            // 
            resources.ApplyResources(this.m_buttonUpdata, "m_buttonUpdata");
            this.m_buttonUpdata.Name = "m_buttonUpdata";
            this.m_buttonUpdata.UseVisualStyleBackColor = true;
            this.m_buttonUpdata.Click += new System.EventHandler(this.m_buttonUpdata_Click);
            // 
            // m_buttonFile
            // 
            resources.ApplyResources(this.m_buttonFile, "m_buttonFile");
            this.m_buttonFile.Name = "m_buttonFile";
            this.m_buttonFile.UseVisualStyleBackColor = true;
            this.m_buttonFile.Click += new System.EventHandler(this.m_buttonFile_Click);
            // 
            // m_textBoxPath
            // 
            resources.ApplyResources(this.m_textBoxPath, "m_textBoxPath");
            this.m_textBoxPath.Name = "m_textBoxPath";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.checkBoxUseDefaultWF);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Controls.Add(this.panel3);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxAscii, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // richTextBoxText
            // 
            resources.ApplyResources(this.richTextBoxText, "richTextBoxText");
            this.richTextBoxText.Name = "richTextBoxText";
            // 
            // richTextBoxAscii
            // 
            resources.ApplyResources(this.richTextBoxAscii, "richTextBoxAscii");
            this.richTextBoxAscii.Name = "richTextBoxAscii";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonDelayU24);
            this.panel3.Controls.Add(this.buttonDelayU13);
            this.panel3.Controls.Add(this.numHeadBoardindex);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.buttonSaveAs);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.buttonRead);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.numHeadIndex);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // buttonDelayU24
            // 
            resources.ApplyResources(this.buttonDelayU24, "buttonDelayU24");
            this.buttonDelayU24.Name = "buttonDelayU24";
            this.buttonDelayU24.UseVisualStyleBackColor = true;
            this.buttonDelayU24.Click += new System.EventHandler(this.buttonDelayU24_Click);
            // 
            // buttonDelayU13
            // 
            resources.ApplyResources(this.buttonDelayU13, "buttonDelayU13");
            this.buttonDelayU13.Name = "buttonDelayU13";
            this.buttonDelayU13.UseVisualStyleBackColor = true;
            this.buttonDelayU13.Click += new System.EventHandler(this.buttonDelayU13_Click);
            // 
            // numHeadBoardindex
            // 
            this.numHeadBoardindex.FormattingEnabled = true;
            resources.ApplyResources(this.numHeadBoardindex, "numHeadBoardindex");
            this.numHeadBoardindex.Name = "numHeadBoardindex";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSaveAs
            // 
            resources.ApplyResources(this.buttonSaveAs, "buttonSaveAs");
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numHeadIndex
            // 
            resources.ApplyResources(this.numHeadIndex, "numHeadIndex");
            this.numHeadIndex.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numHeadIndex.Name = "numHeadIndex";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.buttonPrintPrt);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel5);
            this.groupBox5.Controls.Add(this.comboBoxHbIndexVT);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.numHdIndexVT);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBoxWriteDelaySettingForKJ4A_RH06);
            this.panel5.Controls.Add(this.groupBoxWriteTempLimitsForKJ4A_RH06);
            this.panel5.Controls.Add(this.groupBoxWriteVoltageSettingForKJ4A_RH06);
            this.panel5.Controls.Add(this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06);
            this.panel5.Controls.Add(this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06);
            this.panel5.Controls.Add(this.groupBoxWriteDlysel);
            this.panel5.Controls.Add(this.groupBoxWriteAdjustmentVoltage);
            this.panel5.Controls.Add(this.groupBoxWriteVoltageSetting);
            this.panel5.Controls.Add(this.groupBoxWriteTempLimit);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // groupBoxWriteDelaySettingForKJ4A_RH06
            // 
            this.groupBoxWriteDelaySettingForKJ4A_RH06.Controls.Add(this.buttonWriteDelaySettingForKJ4A_RH0624);
            this.groupBoxWriteDelaySettingForKJ4A_RH06.Controls.Add(this.label21);
            this.groupBoxWriteDelaySettingForKJ4A_RH06.Controls.Add(this.buttonWriteDelaySettingForKJ4A_RH0613);
            this.groupBoxWriteDelaySettingForKJ4A_RH06.Controls.Add(this.label20);
            resources.ApplyResources(this.groupBoxWriteDelaySettingForKJ4A_RH06, "groupBoxWriteDelaySettingForKJ4A_RH06");
            this.groupBoxWriteDelaySettingForKJ4A_RH06.Name = "groupBoxWriteDelaySettingForKJ4A_RH06";
            this.groupBoxWriteDelaySettingForKJ4A_RH06.TabStop = false;
            // 
            // buttonWriteDelaySettingForKJ4A_RH0624
            // 
            resources.ApplyResources(this.buttonWriteDelaySettingForKJ4A_RH0624, "buttonWriteDelaySettingForKJ4A_RH0624");
            this.buttonWriteDelaySettingForKJ4A_RH0624.Name = "buttonWriteDelaySettingForKJ4A_RH0624";
            this.buttonWriteDelaySettingForKJ4A_RH0624.UseVisualStyleBackColor = true;
            this.buttonWriteDelaySettingForKJ4A_RH0624.Click += new System.EventHandler(this.buttonWriteDelaySettingForKJ4A_RH0624_Click);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // buttonWriteDelaySettingForKJ4A_RH0613
            // 
            resources.ApplyResources(this.buttonWriteDelaySettingForKJ4A_RH0613, "buttonWriteDelaySettingForKJ4A_RH0613");
            this.buttonWriteDelaySettingForKJ4A_RH0613.Name = "buttonWriteDelaySettingForKJ4A_RH0613";
            this.buttonWriteDelaySettingForKJ4A_RH0613.UseVisualStyleBackColor = true;
            this.buttonWriteDelaySettingForKJ4A_RH0613.Click += new System.EventHandler(this.buttonWriteDelaySettingForKJ4A_RH0613_Click);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // groupBoxWriteTempLimitsForKJ4A_RH06
            // 
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.buttonReadTempLimitForKJ4A_RH06);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.buttonSetTempLimitForKJ4A_RH06);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.numericUpDownTempLimitLForKJ4A_RH06);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.label18);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.numericUpDownTempLimitUForKJ4A_RH06);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Controls.Add(this.label19);
            resources.ApplyResources(this.groupBoxWriteTempLimitsForKJ4A_RH06, "groupBoxWriteTempLimitsForKJ4A_RH06");
            this.groupBoxWriteTempLimitsForKJ4A_RH06.Name = "groupBoxWriteTempLimitsForKJ4A_RH06";
            this.groupBoxWriteTempLimitsForKJ4A_RH06.TabStop = false;
            // 
            // buttonReadTempLimitForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonReadTempLimitForKJ4A_RH06, "buttonReadTempLimitForKJ4A_RH06");
            this.buttonReadTempLimitForKJ4A_RH06.Name = "buttonReadTempLimitForKJ4A_RH06";
            this.buttonReadTempLimitForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonReadTempLimitForKJ4A_RH06.Click += new System.EventHandler(this.buttonReadTempLimitForKJ4A_RH06_Click);
            // 
            // buttonSetTempLimitForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonSetTempLimitForKJ4A_RH06, "buttonSetTempLimitForKJ4A_RH06");
            this.buttonSetTempLimitForKJ4A_RH06.Name = "buttonSetTempLimitForKJ4A_RH06";
            this.buttonSetTempLimitForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonSetTempLimitForKJ4A_RH06.Click += new System.EventHandler(this.buttonSetTempLimitForKJ4A_RH06_Click);
            // 
            // numericUpDownTempLimitLForKJ4A_RH06
            // 
            this.numericUpDownTempLimitLForKJ4A_RH06.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownTempLimitLForKJ4A_RH06, "numericUpDownTempLimitLForKJ4A_RH06");
            this.numericUpDownTempLimitLForKJ4A_RH06.Name = "numericUpDownTempLimitLForKJ4A_RH06";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // numericUpDownTempLimitUForKJ4A_RH06
            // 
            this.numericUpDownTempLimitUForKJ4A_RH06.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownTempLimitUForKJ4A_RH06, "numericUpDownTempLimitUForKJ4A_RH06");
            this.numericUpDownTempLimitUForKJ4A_RH06.Name = "numericUpDownTempLimitUForKJ4A_RH06";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // groupBoxWriteVoltageSettingForKJ4A_RH06
            // 
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.buttonSetVolSetForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.buttonReadVolSetForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.numericUpDownVolSet4ForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.numericUpDownVolSet3ForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.numericUpDownVolSet2ForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.numericUpDownVolSet1ForKJ4A_RH06);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Controls.Add(this.label17);
            resources.ApplyResources(this.groupBoxWriteVoltageSettingForKJ4A_RH06, "groupBoxWriteVoltageSettingForKJ4A_RH06");
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.Name = "groupBoxWriteVoltageSettingForKJ4A_RH06";
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.TabStop = false;
            // 
            // buttonSetVolSetForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonSetVolSetForKJ4A_RH06, "buttonSetVolSetForKJ4A_RH06");
            this.buttonSetVolSetForKJ4A_RH06.Name = "buttonSetVolSetForKJ4A_RH06";
            this.buttonSetVolSetForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonSetVolSetForKJ4A_RH06.Click += new System.EventHandler(this.buttonSetVolSetForKJ4A_RH06_Click);
            // 
            // buttonReadVolSetForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonReadVolSetForKJ4A_RH06, "buttonReadVolSetForKJ4A_RH06");
            this.buttonReadVolSetForKJ4A_RH06.Name = "buttonReadVolSetForKJ4A_RH06";
            this.buttonReadVolSetForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonReadVolSetForKJ4A_RH06.Click += new System.EventHandler(this.buttonReadVolSetForKJ4A_RH06_Click);
            // 
            // numericUpDownVolSet4ForKJ4A_RH06
            // 
            this.numericUpDownVolSet4ForKJ4A_RH06.DecimalPlaces = 1;
            this.numericUpDownVolSet4ForKJ4A_RH06.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDownVolSet4ForKJ4A_RH06, "numericUpDownVolSet4ForKJ4A_RH06");
            this.numericUpDownVolSet4ForKJ4A_RH06.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numericUpDownVolSet4ForKJ4A_RH06.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownVolSet4ForKJ4A_RH06.Name = "numericUpDownVolSet4ForKJ4A_RH06";
            this.numericUpDownVolSet4ForKJ4A_RH06.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // numericUpDownVolSet3ForKJ4A_RH06
            // 
            this.numericUpDownVolSet3ForKJ4A_RH06.DecimalPlaces = 1;
            this.numericUpDownVolSet3ForKJ4A_RH06.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDownVolSet3ForKJ4A_RH06, "numericUpDownVolSet3ForKJ4A_RH06");
            this.numericUpDownVolSet3ForKJ4A_RH06.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numericUpDownVolSet3ForKJ4A_RH06.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownVolSet3ForKJ4A_RH06.Name = "numericUpDownVolSet3ForKJ4A_RH06";
            this.numericUpDownVolSet3ForKJ4A_RH06.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // numericUpDownVolSet2ForKJ4A_RH06
            // 
            this.numericUpDownVolSet2ForKJ4A_RH06.DecimalPlaces = 1;
            this.numericUpDownVolSet2ForKJ4A_RH06.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDownVolSet2ForKJ4A_RH06, "numericUpDownVolSet2ForKJ4A_RH06");
            this.numericUpDownVolSet2ForKJ4A_RH06.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numericUpDownVolSet2ForKJ4A_RH06.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownVolSet2ForKJ4A_RH06.Name = "numericUpDownVolSet2ForKJ4A_RH06";
            this.numericUpDownVolSet2ForKJ4A_RH06.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // numericUpDownVolSet1ForKJ4A_RH06
            // 
            this.numericUpDownVolSet1ForKJ4A_RH06.DecimalPlaces = 1;
            this.numericUpDownVolSet1ForKJ4A_RH06.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDownVolSet1ForKJ4A_RH06, "numericUpDownVolSet1ForKJ4A_RH06");
            this.numericUpDownVolSet1ForKJ4A_RH06.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numericUpDownVolSet1ForKJ4A_RH06.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownVolSet1ForKJ4A_RH06.Name = "numericUpDownVolSet1ForKJ4A_RH06";
            this.numericUpDownVolSet1ForKJ4A_RH06.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // groupBoxWriteAdjustmentVoltageForKJ4A_RH06
            // 
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Controls.Add(this.buttonSetAdjustVolForKJ4A_RH06);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Controls.Add(this.buttonReadAdjustVolForKJ4A_RH06);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Controls.Add(this.numericUpDownAdjustVolForKJ4A_RH06);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Controls.Add(this.label14);
            resources.ApplyResources(this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06, "groupBoxWriteAdjustmentVoltageForKJ4A_RH06");
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Name = "groupBoxWriteAdjustmentVoltageForKJ4A_RH06";
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.TabStop = false;
            // 
            // buttonSetAdjustVolForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonSetAdjustVolForKJ4A_RH06, "buttonSetAdjustVolForKJ4A_RH06");
            this.buttonSetAdjustVolForKJ4A_RH06.Name = "buttonSetAdjustVolForKJ4A_RH06";
            this.buttonSetAdjustVolForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonSetAdjustVolForKJ4A_RH06.Click += new System.EventHandler(this.buttonSetAdjustVolForKJ4A_RH06_Click);
            // 
            // buttonReadAdjustVolForKJ4A_RH06
            // 
            resources.ApplyResources(this.buttonReadAdjustVolForKJ4A_RH06, "buttonReadAdjustVolForKJ4A_RH06");
            this.buttonReadAdjustVolForKJ4A_RH06.Name = "buttonReadAdjustVolForKJ4A_RH06";
            this.buttonReadAdjustVolForKJ4A_RH06.UseVisualStyleBackColor = true;
            this.buttonReadAdjustVolForKJ4A_RH06.Click += new System.EventHandler(this.buttonReadAdjustVolForKJ4A_RH06_Click);
            // 
            // numericUpDownAdjustVolForKJ4A_RH06
            // 
            this.numericUpDownAdjustVolForKJ4A_RH06.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDownAdjustVolForKJ4A_RH06, "numericUpDownAdjustVolForKJ4A_RH06");
            this.numericUpDownAdjustVolForKJ4A_RH06.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            65536});
            this.numericUpDownAdjustVolForKJ4A_RH06.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147418112});
            this.numericUpDownAdjustVolForKJ4A_RH06.Name = "numericUpDownAdjustVolForKJ4A_RH06";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // groupBoxWriteAdjustmentVoltageForKJ4A_TA06
            // 
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Controls.Add(this.buttonReadAdjustVol2);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Controls.Add(this.buttonSetAdjustVol2);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Controls.Add(this.numericUpDown2);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Controls.Add(this.label16);
            resources.ApplyResources(this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06, "groupBoxWriteAdjustmentVoltageForKJ4A_TA06");
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Name = "groupBoxWriteAdjustmentVoltageForKJ4A_TA06";
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.TabStop = false;
            // 
            // buttonReadAdjustVol2
            // 
            resources.ApplyResources(this.buttonReadAdjustVol2, "buttonReadAdjustVol2");
            this.buttonReadAdjustVol2.Name = "buttonReadAdjustVol2";
            this.buttonReadAdjustVol2.UseVisualStyleBackColor = true;
            this.buttonReadAdjustVol2.Click += new System.EventHandler(this.buttonReadAdjustVol2_Click);
            // 
            // buttonSetAdjustVol2
            // 
            resources.ApplyResources(this.buttonSetAdjustVol2, "buttonSetAdjustVol2");
            this.buttonSetAdjustVol2.Name = "buttonSetAdjustVol2";
            this.buttonSetAdjustVol2.UseVisualStyleBackColor = true;
            this.buttonSetAdjustVol2.Click += new System.EventHandler(this.buttonSetAdjustVol2_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            65536});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147418112});
            this.numericUpDown2.Name = "numericUpDown2";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // groupBoxWriteDlysel
            // 
            this.groupBoxWriteDlysel.Controls.Add(this.comboBoxDlysel4);
            this.groupBoxWriteDlysel.Controls.Add(this.comboBoxDlysel3);
            this.groupBoxWriteDlysel.Controls.Add(this.comboBoxDlysel2);
            this.groupBoxWriteDlysel.Controls.Add(this.comboBoxDlysel1);
            this.groupBoxWriteDlysel.Controls.Add(this.buttonReadDlysel);
            this.groupBoxWriteDlysel.Controls.Add(this.buttonSetDlysel);
            this.groupBoxWriteDlysel.Controls.Add(this.label15);
            resources.ApplyResources(this.groupBoxWriteDlysel, "groupBoxWriteDlysel");
            this.groupBoxWriteDlysel.Name = "groupBoxWriteDlysel";
            this.groupBoxWriteDlysel.TabStop = false;
            // 
            // comboBoxDlysel4
            // 
            this.comboBoxDlysel4.FormattingEnabled = true;
            this.comboBoxDlysel4.Items.AddRange(new object[] {
            resources.GetString("comboBoxDlysel4.Items"),
            resources.GetString("comboBoxDlysel4.Items1"),
            resources.GetString("comboBoxDlysel4.Items2")});
            resources.ApplyResources(this.comboBoxDlysel4, "comboBoxDlysel4");
            this.comboBoxDlysel4.Name = "comboBoxDlysel4";
            // 
            // comboBoxDlysel3
            // 
            this.comboBoxDlysel3.FormattingEnabled = true;
            this.comboBoxDlysel3.Items.AddRange(new object[] {
            resources.GetString("comboBoxDlysel3.Items"),
            resources.GetString("comboBoxDlysel3.Items1"),
            resources.GetString("comboBoxDlysel3.Items2")});
            resources.ApplyResources(this.comboBoxDlysel3, "comboBoxDlysel3");
            this.comboBoxDlysel3.Name = "comboBoxDlysel3";
            // 
            // comboBoxDlysel2
            // 
            this.comboBoxDlysel2.FormattingEnabled = true;
            this.comboBoxDlysel2.Items.AddRange(new object[] {
            resources.GetString("comboBoxDlysel2.Items"),
            resources.GetString("comboBoxDlysel2.Items1"),
            resources.GetString("comboBoxDlysel2.Items2")});
            resources.ApplyResources(this.comboBoxDlysel2, "comboBoxDlysel2");
            this.comboBoxDlysel2.Name = "comboBoxDlysel2";
            // 
            // comboBoxDlysel1
            // 
            this.comboBoxDlysel1.FormattingEnabled = true;
            this.comboBoxDlysel1.Items.AddRange(new object[] {
            resources.GetString("comboBoxDlysel1.Items"),
            resources.GetString("comboBoxDlysel1.Items1"),
            resources.GetString("comboBoxDlysel1.Items2")});
            resources.ApplyResources(this.comboBoxDlysel1, "comboBoxDlysel1");
            this.comboBoxDlysel1.Name = "comboBoxDlysel1";
            // 
            // buttonReadDlysel
            // 
            resources.ApplyResources(this.buttonReadDlysel, "buttonReadDlysel");
            this.buttonReadDlysel.Name = "buttonReadDlysel";
            this.buttonReadDlysel.UseVisualStyleBackColor = true;
            this.buttonReadDlysel.Click += new System.EventHandler(this.buttonReadDlysel_Click);
            // 
            // buttonSetDlysel
            // 
            resources.ApplyResources(this.buttonSetDlysel, "buttonSetDlysel");
            this.buttonSetDlysel.Name = "buttonSetDlysel";
            this.buttonSetDlysel.UseVisualStyleBackColor = true;
            this.buttonSetDlysel.Click += new System.EventHandler(this.buttonSetDlysel_Click);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // groupBoxWriteAdjustmentVoltage
            // 
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.buttonReadAdjustVol);
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.buttonSetAdjustVol);
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.numAdjustVolB);
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.label7);
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.numAdjustVolA);
            this.groupBoxWriteAdjustmentVoltage.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBoxWriteAdjustmentVoltage, "groupBoxWriteAdjustmentVoltage");
            this.groupBoxWriteAdjustmentVoltage.Name = "groupBoxWriteAdjustmentVoltage";
            this.groupBoxWriteAdjustmentVoltage.TabStop = false;
            // 
            // buttonReadAdjustVol
            // 
            resources.ApplyResources(this.buttonReadAdjustVol, "buttonReadAdjustVol");
            this.buttonReadAdjustVol.Name = "buttonReadAdjustVol";
            this.buttonReadAdjustVol.UseVisualStyleBackColor = true;
            this.buttonReadAdjustVol.Click += new System.EventHandler(this.buttonReadAdjustVol_Click);
            // 
            // buttonSetAdjustVol
            // 
            resources.ApplyResources(this.buttonSetAdjustVol, "buttonSetAdjustVol");
            this.buttonSetAdjustVol.Name = "buttonSetAdjustVol";
            this.buttonSetAdjustVol.UseVisualStyleBackColor = true;
            this.buttonSetAdjustVol.Click += new System.EventHandler(this.buttonSetAdjustVol_Click);
            // 
            // numAdjustVolB
            // 
            this.numAdjustVolB.DecimalPlaces = 2;
            resources.ApplyResources(this.numAdjustVolB, "numAdjustVolB");
            this.numAdjustVolB.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            65536});
            this.numAdjustVolB.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147418112});
            this.numAdjustVolB.Name = "numAdjustVolB";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numAdjustVolA
            // 
            this.numAdjustVolA.DecimalPlaces = 2;
            resources.ApplyResources(this.numAdjustVolA, "numAdjustVolA");
            this.numAdjustVolA.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            65536});
            this.numAdjustVolA.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147418112});
            this.numAdjustVolA.Name = "numAdjustVolA";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBoxWriteVoltageSetting
            // 
            this.groupBoxWriteVoltageSetting.Controls.Add(this.buttonReadBaseVol);
            this.groupBoxWriteVoltageSetting.Controls.Add(this.buttonSetBaseVol);
            this.groupBoxWriteVoltageSetting.Controls.Add(this.numBaseVolB);
            this.groupBoxWriteVoltageSetting.Controls.Add(this.label8);
            this.groupBoxWriteVoltageSetting.Controls.Add(this.numBaseVolA);
            this.groupBoxWriteVoltageSetting.Controls.Add(this.label9);
            resources.ApplyResources(this.groupBoxWriteVoltageSetting, "groupBoxWriteVoltageSetting");
            this.groupBoxWriteVoltageSetting.Name = "groupBoxWriteVoltageSetting";
            this.groupBoxWriteVoltageSetting.TabStop = false;
            // 
            // buttonReadBaseVol
            // 
            resources.ApplyResources(this.buttonReadBaseVol, "buttonReadBaseVol");
            this.buttonReadBaseVol.Name = "buttonReadBaseVol";
            this.buttonReadBaseVol.UseVisualStyleBackColor = true;
            this.buttonReadBaseVol.Click += new System.EventHandler(this.buttonReadBaseVol_Click);
            // 
            // buttonSetBaseVol
            // 
            resources.ApplyResources(this.buttonSetBaseVol, "buttonSetBaseVol");
            this.buttonSetBaseVol.Name = "buttonSetBaseVol";
            this.buttonSetBaseVol.UseVisualStyleBackColor = true;
            this.buttonSetBaseVol.Click += new System.EventHandler(this.buttonSetBaseVol_Click);
            // 
            // numBaseVolB
            // 
            this.numBaseVolB.DecimalPlaces = 2;
            resources.ApplyResources(this.numBaseVolB, "numBaseVolB");
            this.numBaseVolB.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numBaseVolB.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numBaseVolB.Name = "numBaseVolB";
            this.numBaseVolB.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numBaseVolA
            // 
            this.numBaseVolA.DecimalPlaces = 2;
            resources.ApplyResources(this.numBaseVolA, "numBaseVolA");
            this.numBaseVolA.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.numBaseVolA.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numBaseVolA.Name = "numBaseVolA";
            this.numBaseVolA.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // groupBoxWriteTempLimit
            // 
            this.groupBoxWriteTempLimit.Controls.Add(this.buttonReadTempLimit);
            this.groupBoxWriteTempLimit.Controls.Add(this.buttonSetTempLimit);
            this.groupBoxWriteTempLimit.Controls.Add(this.numTempLimitB);
            this.groupBoxWriteTempLimit.Controls.Add(this.label10);
            this.groupBoxWriteTempLimit.Controls.Add(this.numTempLimitA);
            this.groupBoxWriteTempLimit.Controls.Add(this.label11);
            resources.ApplyResources(this.groupBoxWriteTempLimit, "groupBoxWriteTempLimit");
            this.groupBoxWriteTempLimit.Name = "groupBoxWriteTempLimit";
            this.groupBoxWriteTempLimit.TabStop = false;
            // 
            // buttonReadTempLimit
            // 
            resources.ApplyResources(this.buttonReadTempLimit, "buttonReadTempLimit");
            this.buttonReadTempLimit.Name = "buttonReadTempLimit";
            this.buttonReadTempLimit.UseVisualStyleBackColor = true;
            this.buttonReadTempLimit.Click += new System.EventHandler(this.buttonReadTempLimit_Click);
            // 
            // buttonSetTempLimit
            // 
            resources.ApplyResources(this.buttonSetTempLimit, "buttonSetTempLimit");
            this.buttonSetTempLimit.Name = "buttonSetTempLimit";
            this.buttonSetTempLimit.UseVisualStyleBackColor = true;
            this.buttonSetTempLimit.Click += new System.EventHandler(this.buttonSetTempLimit_Click);
            // 
            // numTempLimitB
            // 
            this.numTempLimitB.DecimalPlaces = 2;
            resources.ApplyResources(this.numTempLimitB, "numTempLimitB");
            this.numTempLimitB.Name = "numTempLimitB";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // numTempLimitA
            // 
            this.numTempLimitA.DecimalPlaces = 2;
            resources.ApplyResources(this.numTempLimitA, "numTempLimitA");
            this.numTempLimitA.Name = "numTempLimitA";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // comboBoxHbIndexVT
            // 
            this.comboBoxHbIndexVT.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxHbIndexVT, "comboBoxHbIndexVT");
            this.comboBoxHbIndexVT.Name = "comboBoxHbIndexVT";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // numHdIndexVT
            // 
            resources.ApplyResources(this.numHdIndexVT, "numHdIndexVT");
            this.numHdIndexVT.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numHdIndexVT.Name = "numHdIndexVT";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numHBIndexW);
            this.groupBox4.Controls.Add(this.m_buttonUpdata);
            this.groupBox4.Controls.Add(this.m_buttonFile);
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Controls.Add(this.m_textBoxPath);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // numHBIndexW
            // 
            this.numHBIndexW.FormattingEnabled = true;
            resources.ApplyResources(this.numHBIndexW, "numHBIndexW");
            this.numHBIndexW.Name = "numHBIndexW";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.checkBoxB);
            this.panel4.Controls.Add(this.checkBoxA);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // checkBoxB
            // 
            resources.ApplyResources(this.checkBoxB, "checkBoxB");
            this.checkBoxB.Name = "checkBoxB";
            this.checkBoxB.UseVisualStyleBackColor = true;
            // 
            // checkBoxA
            // 
            resources.ApplyResources(this.checkBoxA, "checkBoxA");
            this.checkBoxA.Checked = true;
            this.checkBoxA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxA.Name = "checkBoxA";
            this.checkBoxA.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox4);
            this.panel2.Controls.Add(this.checkBox3);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // buttonPrintPrt
            // 
            resources.ApplyResources(this.buttonPrintPrt, "buttonPrintPrt");
            this.buttonPrintPrt.Name = "buttonPrintPrt";
            this.buttonPrintPrt.UseVisualStyleBackColor = true;
            this.buttonPrintPrt.Click += new System.EventHandler(this.buttonPrintPrt_Click);
            // 
            // checkBoxUseDefaultWF
            // 
            resources.ApplyResources(this.checkBoxUseDefaultWF, "checkBoxUseDefaultWF");
            this.checkBoxUseDefaultWF.Name = "checkBoxUseDefaultWF";
            this.checkBoxUseDefaultWF.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultWF.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultWF_CheckedChanged);
            // 
            // KyoceraWaveform
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_StatusBarApp);
            this.Name = "KyoceraWaveform";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Xaar501Waveform_Load);
            this.SizeChanged += new System.EventHandler(this.Xaar501Waveform_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeadIndex)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBoxWriteDelaySettingForKJ4A_RH06.ResumeLayout(false);
            this.groupBoxWriteDelaySettingForKJ4A_RH06.PerformLayout();
            this.groupBoxWriteTempLimitsForKJ4A_RH06.ResumeLayout(false);
            this.groupBoxWriteTempLimitsForKJ4A_RH06.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempLimitLForKJ4A_RH06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempLimitUForKJ4A_RH06)).EndInit();
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.ResumeLayout(false);
            this.groupBoxWriteVoltageSettingForKJ4A_RH06.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet4ForKJ4A_RH06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet3ForKJ4A_RH06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet2ForKJ4A_RH06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolSet1ForKJ4A_RH06)).EndInit();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.ResumeLayout(false);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_RH06.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdjustVolForKJ4A_RH06)).EndInit();
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.ResumeLayout(false);
            this.groupBoxWriteAdjustmentVoltageForKJ4A_TA06.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBoxWriteDlysel.ResumeLayout(false);
            this.groupBoxWriteDlysel.PerformLayout();
            this.groupBoxWriteAdjustmentVoltage.ResumeLayout(false);
            this.groupBoxWriteAdjustmentVoltage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustVolB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustVolA)).EndInit();
            this.groupBoxWriteVoltageSetting.ResumeLayout(false);
            this.groupBoxWriteVoltageSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseVolB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseVolA)).EndInit();
            this.groupBoxWriteTempLimit.ResumeLayout(false);
            this.groupBoxWriteTempLimit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLimitB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLimitA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHdIndexVT)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusBar m_StatusBarApp;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
        private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
        private System.Windows.Forms.ProgressBar progressBarStatu;
        private System.Windows.Forms.Button m_buttonUpdata;
        private System.Windows.Forms.Button m_buttonFile;
        private System.Windows.Forms.TextBox m_textBoxPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultWF;
        private Button buttonRead;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label2;
        private NumericUpDown numHeadIndex;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private RichTextBox richTextBoxText;
        private RichTextBox richTextBoxAscii;
        private Panel panel3;
        private CheckBox checkBoxB;
        private CheckBox checkBoxA;
        private Label label4;
        private Label label5;
        private Label label3;
        private GroupBox groupBoxWriteAdjustmentVoltage;
        private Button buttonSetAdjustVol;
        private NumericUpDown numAdjustVolB;
        private Label label7;
        private NumericUpDown numAdjustVolA;
        private Label label6;
        private GroupBox groupBoxWriteTempLimit;
        private Button buttonSetTempLimit;
        private NumericUpDown numTempLimitB;
        private Label label10;
        private NumericUpDown numTempLimitA;
        private Label label11;
        private GroupBox groupBoxWriteVoltageSetting;
        private Button buttonSetBaseVol;
        private NumericUpDown numBaseVolB;
        private Label label8;
        private NumericUpDown numBaseVolA;
        private Label label9;
        private Button buttonReadTempLimit;
        private Button buttonReadBaseVol;
        private Button buttonReadAdjustVol;
        private Button buttonSaveAs;
        private Button button1;
        private Panel panel4;
        private Panel panel2;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button buttonPrintPrt;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private ComboBox numHBIndexW;
        private ComboBox numHeadBoardindex;
        private ComboBox comboBoxHbIndexVT;
        private Label label12;
        private Label label13;
        private NumericUpDown numHdIndexVT;
        private Panel panel5;
        private GroupBox groupBoxWriteDlysel;
        private Button buttonReadDlysel;
        private Button buttonSetDlysel;
        private Label label15;
        private ComboBox comboBoxDlysel4;
        private ComboBox comboBoxDlysel3;
        private ComboBox comboBoxDlysel2;
        private ComboBox comboBoxDlysel1;
        private GroupBox groupBoxWriteAdjustmentVoltageForKJ4A_TA06;
        private Button buttonReadAdjustVol2;
        private Button buttonSetAdjustVol2;
        private NumericUpDown numericUpDown2;
        private Label label16;
        private GroupBox groupBoxWriteAdjustmentVoltageForKJ4A_RH06;
        private Button buttonSetAdjustVolForKJ4A_RH06;
        private Button buttonReadAdjustVolForKJ4A_RH06;
        private NumericUpDown numericUpDownAdjustVolForKJ4A_RH06;
        private Label label14;
        private GroupBox groupBoxWriteDelaySettingForKJ4A_RH06;
        private Label label21;
        private Button buttonWriteDelaySettingForKJ4A_RH0613;
        private Label label20;
        private GroupBox groupBoxWriteTempLimitsForKJ4A_RH06;
        private Button buttonReadTempLimitForKJ4A_RH06;
        private Button buttonSetTempLimitForKJ4A_RH06;
        private NumericUpDown numericUpDownTempLimitLForKJ4A_RH06;
        private Label label18;
        private NumericUpDown numericUpDownTempLimitUForKJ4A_RH06;
        private Label label19;
        private GroupBox groupBoxWriteVoltageSettingForKJ4A_RH06;
        private Button buttonSetVolSetForKJ4A_RH06;
        private Button buttonReadVolSetForKJ4A_RH06;
        private NumericUpDown numericUpDownVolSet4ForKJ4A_RH06;
        private NumericUpDown numericUpDownVolSet3ForKJ4A_RH06;
        private NumericUpDown numericUpDownVolSet2ForKJ4A_RH06;
        private NumericUpDown numericUpDownVolSet1ForKJ4A_RH06;
        private Label label17;
        private Button buttonWriteDelaySettingForKJ4A_RH0624;
        private Button buttonDelayU24;
        private Button buttonDelayU13;
    }
}