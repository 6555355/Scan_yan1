using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for UVForm.
	/// </summary>
	public class UVForm : System.Windows.Forms.Form
	{
		private DividerPanel.DividerPanel dividerPanel1;
		private System.Windows.Forms.Button m_ButtonCancel;
		private System.Windows.Forms.Button m_ButtonOK;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
        private BYHXPrinterManager.Setting.ZAixsSetting zAixsSetting1;

		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private bool bZMeasurSensorSupport = false;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.ComboBox cmb_UvLightType;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox m_GroupBoxUVSet;
		private System.Windows.Forms.ComboBox m_ComboBoxLeftSet;
		private System.Windows.Forms.Label m_LabelLeftSet;
		private System.Windows.Forms.CheckBox m_CheckBoxShutterLeft;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox checkBox1Leftprinton;
		private System.Windows.Forms.NumericUpDown numericUpDownLeftDis;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.CheckBox checkBox2Rightprinton;
		private System.Windows.Forms.NumericUpDown numericUpDownShutterDis;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox checkBox1Rightprinton;
		private System.Windows.Forms.GroupBox m_GroupBoxUVSetR;
		private System.Windows.Forms.ComboBox m_ComboBoxRightSet;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox m_CheckBoxShutterRight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDownRightDis;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox checkBox2Leftprinton;
        private GroupBox groupBoxLPrint;
        private Panel panel1;
        private ComboBox m_ComboBoxPLeftSet;
        private Label label6;
        private GroupBox groupBoxRPrint;
        private Panel panel2;
        private ComboBox m_ComboBoxPRightSet;
        private Label label7;
        private CheckBox m_checkbox_autoSwitchMode;
        private SPrinterProperty m_sPrinterProperty;
        private GroupBox groupBox7;
        private PictureBox pictureBox1;
        private UvPowerLevelMap _UvPowerLevelMap;
        private Panel panelUVX2Power;
        private Label label9;
        private NumericUpDown numUVX1Power;
        private Label label8;
        private Label label10;
        private NumericUpDown numUVX2Power;
        private Label label11;
        private GZUVX2Param GZUVX2Param;
        public UVForm(UvPowerLevelMap UvPowerLevelMap)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
            _UvPowerLevelMap = UvPowerLevelMap;
            this.Size=new Size(578,590);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UVForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelUVX2Power = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.numUVX2Power = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numUVX1Power = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_checkbox_autoSwitchMode = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.numericUpDownLeftDis = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_UvLightType = new System.Windows.Forms.ComboBox();
            this.numericUpDownRightDis = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxLPrint = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1Rightprinton = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1Leftprinton = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_ComboBoxPLeftSet = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_GroupBoxUVSet = new System.Windows.Forms.GroupBox();
            this.m_ComboBoxLeftSet = new System.Windows.Forms.ComboBox();
            this.m_LabelLeftSet = new System.Windows.Forms.Label();
            this.m_CheckBoxShutterLeft = new System.Windows.Forms.CheckBox();
            this.numericUpDownShutterDis = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxRPrint = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBox2Rightprinton = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox2Leftprinton = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_ComboBoxPRightSet = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_GroupBoxUVSetR = new System.Windows.Forms.GroupBox();
            this.m_ComboBoxRightSet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_CheckBoxShutterRight = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.zAixsSetting1 = new BYHXPrinterManager.Setting.ZAixsSetting();
            this.m_ButtonCancel = new System.Windows.Forms.Button();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelUVX2Power.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVX2Power)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUVX1Power)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightDis)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxLPrint.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_GroupBoxUVSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShutterDis)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxRPrint.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_GroupBoxUVSetR.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.dividerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelUVX2Power);
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.m_checkbox_autoSwitchMode);
            this.tabPage1.Controls.Add(this.groupBox9);
            this.tabPage1.Controls.Add(this.numericUpDownLeftDis);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cmb_UvLightType);
            this.tabPage1.Controls.Add(this.numericUpDownRightDis);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.numericUpDownShutterDis);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // panelUVX2Power
            // 
            this.panelUVX2Power.Controls.Add(this.label10);
            this.panelUVX2Power.Controls.Add(this.numUVX2Power);
            this.panelUVX2Power.Controls.Add(this.label11);
            this.panelUVX2Power.Controls.Add(this.label9);
            this.panelUVX2Power.Controls.Add(this.numUVX1Power);
            this.panelUVX2Power.Controls.Add(this.label8);
            resources.ApplyResources(this.panelUVX2Power, "panelUVX2Power");
            this.panelUVX2Power.Name = "panelUVX2Power";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // numUVX2Power
            // 
            resources.ApplyResources(this.numUVX2Power, "numUVX2Power");
            this.numUVX2Power.Name = "numUVX2Power";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // numUVX1Power
            // 
            resources.ApplyResources(this.numUVX1Power, "numUVX1Power");
            this.numUVX1Power.Name = "numUVX1Power";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // m_checkbox_autoSwitchMode
            // 
            resources.ApplyResources(this.m_checkbox_autoSwitchMode, "m_checkbox_autoSwitchMode");
            this.m_checkbox_autoSwitchMode.Checked = true;
            this.m_checkbox_autoSwitchMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_checkbox_autoSwitchMode.Name = "m_checkbox_autoSwitchMode";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.pictureBox5);
            this.groupBox9.Controls.Add(this.pictureBox4);
            this.groupBox9.Controls.Add(this.pictureBox3);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // numericUpDownLeftDis
            // 
            resources.ApplyResources(this.numericUpDownLeftDis, "numericUpDownLeftDis");
            this.numericUpDownLeftDis.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownLeftDis.Name = "numericUpDownLeftDis";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Name = "label3";
            // 
            // cmb_UvLightType
            // 
            resources.ApplyResources(this.cmb_UvLightType, "cmb_UvLightType");
            this.cmb_UvLightType.Name = "cmb_UvLightType";
            // 
            // numericUpDownRightDis
            // 
            resources.ApplyResources(this.numericUpDownRightDis, "numericUpDownRightDis");
            this.numericUpDownRightDis.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownRightDis.Name = "numericUpDownRightDis";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxLPrint);
            this.groupBox1.Controls.Add(this.m_GroupBoxUVSet);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBoxLPrint
            // 
            this.groupBoxLPrint.Controls.Add(this.groupBox5);
            this.groupBoxLPrint.Controls.Add(this.groupBox3);
            this.groupBoxLPrint.Controls.Add(this.panel1);
            resources.ApplyResources(this.groupBoxLPrint, "groupBoxLPrint");
            this.groupBoxLPrint.Name = "groupBoxLPrint";
            this.groupBoxLPrint.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox1Rightprinton);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // checkBox1Rightprinton
            // 
            this.checkBox1Rightprinton.Checked = true;
            this.checkBox1Rightprinton.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox1Rightprinton, "checkBox1Rightprinton");
            this.checkBox1Rightprinton.Name = "checkBox1Rightprinton";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1Leftprinton);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // checkBox1Leftprinton
            // 
            this.checkBox1Leftprinton.Checked = true;
            this.checkBox1Leftprinton.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox1Leftprinton, "checkBox1Leftprinton");
            this.checkBox1Leftprinton.Name = "checkBox1Leftprinton";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_ComboBoxPLeftSet);
            this.panel1.Controls.Add(this.label6);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // m_ComboBoxPLeftSet
            // 
            this.m_ComboBoxPLeftSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPLeftSet, "m_ComboBoxPLeftSet");
            this.m_ComboBoxPLeftSet.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxPLeftSet.Items"),
            resources.GetString("m_ComboBoxPLeftSet.Items1"),
            resources.GetString("m_ComboBoxPLeftSet.Items2"),
            resources.GetString("m_ComboBoxPLeftSet.Items3")});
            this.m_ComboBoxPLeftSet.Name = "m_ComboBoxPLeftSet";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Name = "label6";
            // 
            // m_GroupBoxUVSet
            // 
            this.m_GroupBoxUVSet.Controls.Add(this.m_ComboBoxLeftSet);
            this.m_GroupBoxUVSet.Controls.Add(this.m_LabelLeftSet);
            this.m_GroupBoxUVSet.Controls.Add(this.m_CheckBoxShutterLeft);
            this.m_GroupBoxUVSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_GroupBoxUVSet, "m_GroupBoxUVSet");
            this.m_GroupBoxUVSet.Name = "m_GroupBoxUVSet";
            this.m_GroupBoxUVSet.TabStop = false;
            // 
            // m_ComboBoxLeftSet
            // 
            this.m_ComboBoxLeftSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxLeftSet, "m_ComboBoxLeftSet");
            this.m_ComboBoxLeftSet.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxLeftSet.Items"),
            resources.GetString("m_ComboBoxLeftSet.Items1"),
            resources.GetString("m_ComboBoxLeftSet.Items2"),
            resources.GetString("m_ComboBoxLeftSet.Items3")});
            this.m_ComboBoxLeftSet.Name = "m_ComboBoxLeftSet";
            // 
            // m_LabelLeftSet
            // 
            resources.ApplyResources(this.m_LabelLeftSet, "m_LabelLeftSet");
            this.m_LabelLeftSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelLeftSet.Name = "m_LabelLeftSet";
            // 
            // m_CheckBoxShutterLeft
            // 
            this.m_CheckBoxShutterLeft.Checked = true;
            this.m_CheckBoxShutterLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.m_CheckBoxShutterLeft, "m_CheckBoxShutterLeft");
            this.m_CheckBoxShutterLeft.Name = "m_CheckBoxShutterLeft";
            // 
            // numericUpDownShutterDis
            // 
            resources.ApplyResources(this.numericUpDownShutterDis, "numericUpDownShutterDis");
            this.numericUpDownShutterDis.Name = "numericUpDownShutterDis";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBoxRPrint);
            this.groupBox2.Controls.Add(this.m_GroupBoxUVSetR);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBoxRPrint
            // 
            this.groupBoxRPrint.Controls.Add(this.groupBox6);
            this.groupBoxRPrint.Controls.Add(this.groupBox4);
            this.groupBoxRPrint.Controls.Add(this.panel2);
            resources.ApplyResources(this.groupBoxRPrint, "groupBoxRPrint");
            this.groupBoxRPrint.Name = "groupBoxRPrint";
            this.groupBoxRPrint.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBox2Rightprinton);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // checkBox2Rightprinton
            // 
            this.checkBox2Rightprinton.Checked = true;
            this.checkBox2Rightprinton.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox2Rightprinton, "checkBox2Rightprinton");
            this.checkBox2Rightprinton.Name = "checkBox2Rightprinton";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox2Leftprinton);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // checkBox2Leftprinton
            // 
            this.checkBox2Leftprinton.Checked = true;
            this.checkBox2Leftprinton.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox2Leftprinton, "checkBox2Leftprinton");
            this.checkBox2Leftprinton.Name = "checkBox2Leftprinton";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_ComboBoxPRightSet);
            this.panel2.Controls.Add(this.label7);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // m_ComboBoxPRightSet
            // 
            this.m_ComboBoxPRightSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxPRightSet, "m_ComboBoxPRightSet");
            this.m_ComboBoxPRightSet.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxPRightSet.Items"),
            resources.GetString("m_ComboBoxPRightSet.Items1"),
            resources.GetString("m_ComboBoxPRightSet.Items2"),
            resources.GetString("m_ComboBoxPRightSet.Items3")});
            this.m_ComboBoxPRightSet.Name = "m_ComboBoxPRightSet";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Name = "label7";
            // 
            // m_GroupBoxUVSetR
            // 
            this.m_GroupBoxUVSetR.Controls.Add(this.m_ComboBoxRightSet);
            this.m_GroupBoxUVSetR.Controls.Add(this.label1);
            this.m_GroupBoxUVSetR.Controls.Add(this.m_CheckBoxShutterRight);
            this.m_GroupBoxUVSetR.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_GroupBoxUVSetR, "m_GroupBoxUVSetR");
            this.m_GroupBoxUVSetR.Name = "m_GroupBoxUVSetR";
            this.m_GroupBoxUVSetR.TabStop = false;
            // 
            // m_ComboBoxRightSet
            // 
            this.m_ComboBoxRightSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxRightSet, "m_ComboBoxRightSet");
            this.m_ComboBoxRightSet.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxRightSet.Items"),
            resources.GetString("m_ComboBoxRightSet.Items1"),
            resources.GetString("m_ComboBoxRightSet.Items2"),
            resources.GetString("m_ComboBoxRightSet.Items3")});
            this.m_ComboBoxRightSet.Name = "m_ComboBoxRightSet";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Name = "label1";
            // 
            // m_CheckBoxShutterRight
            // 
            this.m_CheckBoxShutterRight.Checked = true;
            this.m_CheckBoxShutterRight.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.m_CheckBoxShutterRight, "m_CheckBoxShutterRight");
            this.m_CheckBoxShutterRight.Name = "m_CheckBoxShutterRight";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.zAixsSetting1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // zAixsSetting1
            // 
            this.zAixsSetting1.Divider = false;
            resources.ApplyResources(this.zAixsSetting1, "zAixsSetting1");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.zAixsSetting1.GradientColors = style1;
            this.zAixsSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.zAixsSetting1.GrouperTitleStyle = null;
            this.zAixsSetting1.IsMeasureBeforePrint = false;
            this.zAixsSetting1.Name = "zAixsSetting1";
            // 
            // m_ButtonCancel
            // 
            this.m_ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonCancel, "m_ButtonCancel");
            this.m_ButtonCancel.Name = "m_ButtonCancel";
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonCancel);
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            this.m_ButtonOK.Click += new System.EventHandler(this.m_ButtonToBoard_Click);
            // 
            // UVForm
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.m_ButtonCancel;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dividerPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UVForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.UVForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panelUVX2Power.ResumeLayout(false);
            this.panelUVX2Power.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUVX2Power)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUVX1Power)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeftDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRightDis)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBoxLPrint.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.m_GroupBoxUVSet.ResumeLayout(false);
            this.m_GroupBoxUVSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShutterDis)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxRPrint.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_GroupBoxUVSetR.ResumeLayout(false);
            this.m_GroupBoxUVSetR.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.dividerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_sPrinterProperty = sp;
			SBoardInfo sBoardInfo = new SBoardInfo();
			CoreInterface.GetBoardInfo(0,ref sBoardInfo);

            bZMeasurSensorSupport = sp.IsZMeasurSupport && !sp.IsALLWIN_FLAT();
			if(bZMeasurSensorSupport &&!this.tabControl1.TabPages.Contains(this.tabPage2))
			{
				this.tabControl1.TabPages.Add(this.tabPage2);
			}
			else if(!bZMeasurSensorSupport&&this.tabControl1.TabPages.Contains(this.tabPage2))
			{
				this.tabControl1.TabPages.Remove(this.tabPage2);
			}

            bool isSimpleUv = SPrinterProperty.IsSimpleUV();
            bool bshowReadyUvSet = !isSimpleUv && PubFunc.GetUserPermission() == (int)UserPermission.SupperUser;
            m_checkbox_autoSwitchMode.Visible =
                label5.Visible =cmb_UvLightType.Visible=
            m_GroupBoxUVSet.Visible = m_GroupBoxUVSetR.Visible = bshowReadyUvSet;
            if (!bshowReadyUvSet)
		    {
		        groupBoxLPrint.Location = m_GroupBoxUVSet.Location;
		        groupBoxLPrint.Height += m_GroupBoxUVSet.Height;

		        groupBoxRPrint.Location = m_GroupBoxUVSetR.Location;
                groupBoxRPrint.Height += m_GroupBoxUVSetR.Height;
            }
            //this.m_ComboBoxLeftSet.Items.Clear();
            //this.m_ComboBoxRightSet.Items.Clear();
            //this.m_ComboBoxPLeftSet.Items.Clear();
            //this.m_ComboBoxPRightSet.Items.Clear();
			Type enumtype = typeof(UvLightType);
			UvLightType[] valse = (UvLightType[])Enum.GetValues(enumtype);
			for(int i =0; i < valse.Length; i++)
			{
				string item = ResString.GetEnumDisplayName(enumtype,valse[i]);
				this.cmb_UvLightType.Items.Add(item);
            }
            panelUVX2Power.Visible = SPrinterProperty.IsGongZengUv();
		}
		
		private int OnGetRealTimeFromUI()
		{
			int  status = 0;
			int left  = MapUVStatusToJetStatus(m_ComboBoxLeftSet.SelectedIndex,true,false);
			int right = MapUVStatusToJetStatus(m_ComboBoxRightSet.SelectedIndex,false,false);
			if(m_CheckBoxShutterLeft.Checked)
                left |= (int) INTBIT.Bit_2;
			if(m_CheckBoxShutterRight.Checked)
                left |= (int) INTBIT.Bit_6;
			status = (left|right);
			int onelight = 0;
#if false
			if(m_CheckBoxOneLight.Checked)
				onelight |= 1;
			else
				onelight &= ~1;
			if(m_CheckBoxUVHighPower.Checked)
				onelight |= 2;
			else
				onelight &= ~2;
#endif
#if DOUBLE_SIDE_PRINT_HAPOND
            int leftP = MapUVStatusToJetStatus(m_ComboBoxPLeftSet.SelectedIndex, false, true);
            int rightP = MapUVStatusToJetStatus(m_ComboBoxPRightSet.SelectedIndex, true, true);
#else
            int leftP = MapUVStatusToJetStatus(m_ComboBoxPLeftSet.SelectedIndex, true, true);
            int rightP = MapUVStatusToJetStatus(m_ComboBoxPRightSet.SelectedIndex, false, true);
#endif
            onelight = (leftP | rightP);
            // 
            onelight |= (int) INTBIT.Bit_3;
            onelight |= (int) (m_checkbox_autoSwitchMode.Checked ? INTBIT.Bit_2 : 0);
            status |= (onelight << 16);
			return status;
		}
		public void OnPreferenceChange( UIPreference up)
		{
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
//				this.isDirty = false;
			}
			this.zAixsSetting1.OnPreferenceChange(up);
		}

		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownLeftDis);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownRightDis);
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownShutterDis);
			string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownLeftDis, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownRightDis, this.m_ToolTip);
			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownShutterDis, this.m_ToolTip);
//			this.isDirty = false;
		}

		private UvLightType m_uvLightType = UvLightType.UvLightType1;
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_uvLightType = (UvLightType)ss.UVSetting.eUvLightType;

			this.m_ComboBoxLeftSet.Items.Clear();
			this.m_ComboBoxRightSet.Items.Clear();
            this.m_ComboBoxPLeftSet.Items.Clear();
            this.m_ComboBoxPRightSet.Items.Clear();
			Type enumtype = typeof(UVStatus);
			if(m_uvLightType == UvLightType.UvLightType2||m_uvLightType==UvLightType.UvLightType3)//(sp.IsAllWinZMeasurSensorSupport())
			{
				enumtype = typeof(UVStatus_ALLWIN);
				UVStatus_ALLWIN[] vals = (UVStatus_ALLWIN[])Enum.GetValues(enumtype);
				for(int i =0; i < vals.Length; i++)
				{
					string item = ResString.GetEnumDisplayName(enumtype,vals[i]);
					this.m_ComboBoxLeftSet.Items.Add(item);
					this.m_ComboBoxRightSet.Items.Add(item);
                    this.m_ComboBoxPLeftSet.Items.Add(item);
                    this.m_ComboBoxPRightSet.Items.Add(item);
				}
			}
			else
			{
				UVStatus[] vals = (UVStatus[])Enum.GetValues(enumtype);
				for(int i =0; i < vals.Length; i++)
				{
					string item = ResString.GetEnumDisplayName(enumtype,vals[i]);
					this.m_ComboBoxLeftSet.Items.Add(item);
					this.m_ComboBoxRightSet.Items.Add(item);
                    this.m_ComboBoxPLeftSet.Items.Add(item);
                    this.m_ComboBoxPRightSet.Items.Add(item);
				}
			}

			SUVSetting uvseting = ss.UVSetting;
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownShutterDis, uvseting.fShutterOpenDistance);
#if !DOUBLE_SIDE_PRINT_HAPOND
			UIPreference.SetValueAndClampWithMinMax(this.numericUpDownLeftDis,uvseting.fLeftDisFromNozzel);
			UIPreference.SetValueAndClampWithMinMax(this.numericUpDownRightDis,uvseting.fRightDisFromNozzel);
			this.checkBox1Leftprinton.Checked = (uvseting.iLeftRightMask &0x02) !=0;
			this.checkBox1Rightprinton.Checked = (uvseting.iLeftRightMask &0x01) !=0;
			this.checkBox2Leftprinton.Checked = (uvseting.iLeftRightMask &0x08) !=0;
			this.checkBox2Rightprinton.Checked = (uvseting.iLeftRightMask &0x04) !=0;	
#else
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownLeftDis, uvseting.fRightDisFromNozzel);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownRightDis, uvseting.fLeftDisFromNozzel);
            this.checkBox1Leftprinton.Checked = (uvseting.iLeftRightMask & 0x04) != 0;
            this.checkBox1Rightprinton.Checked = (uvseting.iLeftRightMask & 0x08) != 0;
            this.checkBox2Leftprinton.Checked = (uvseting.iLeftRightMask & 0x01) != 0;
            this.checkBox2Rightprinton.Checked = (uvseting.iLeftRightMask & 0x02) != 0;
#endif
			this.zAixsSetting1.OnPrinterSettingChange(ss);
            this.cmb_UvLightType.SelectedIndex = ss.UVSetting.eUvLightType;
		    if (EpsonLCD.GetGZUVX2Param(ref GZUVX2Param))
		    {
		        numUVX1Power.Value = GZUVX2Param.UVX1Power <= 100 ? GZUVX2Param.UVX1Power : 0;
                numUVX2Power.Value = GZUVX2Param.UVX2Power <= 100 ? GZUVX2Param.UVX2Power : 0;
		    }
		}

		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
			ss.UVSetting.fShutterOpenDistance		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownShutterDis.Value));
			int mask = 0x00;
#if !DOUBLE_SIDE_PRINT_HAPOND
            ss.UVSetting.fLeftDisFromNozzel = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownLeftDis.Value));
            ss.UVSetting.fRightDisFromNozzel = UIPreference.ToInchLength(m_CurrentUnit, Decimal.ToSingle(this.numericUpDownRightDis.Value));
            mask |= (this.checkBox1Leftprinton.Checked ? 0x2 : 0);
			mask |=	(this.checkBox1Rightprinton.Checked?0x1:0);
			mask |=	(this.checkBox2Leftprinton.Checked? 0x8:0);
			mask |=	(this.checkBox2Rightprinton.Checked?0x4:0);
//			mask |=	(this.m_checkBoxVisableLeft.Checked? 0x1:0);
//			mask |=	(this.m_checkBoxVisableRight.Checked?0x4:0);
#else
            ss.UVSetting.fLeftDisFromNozzel		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownRightDis.Value));
			ss.UVSetting.fRightDisFromNozzel		=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownLeftDis.Value));
            mask |= (this.checkBox2Leftprinton.Checked ? 0x1 : 0);
            mask |= (this.checkBox2Rightprinton.Checked ? 0x2 : 0);
            mask |= (this.checkBox1Leftprinton.Checked ? 0x4 : 0);
            mask |= (this.checkBox1Rightprinton.Checked ? 0x8 : 0);
            //			mask |=	(this.m_checkBoxVisableLeft.Checked? 0x1:0);
            //			mask |=	(this.m_checkBoxVisableRight.Checked?0x4:0);
#endif
            ss.UVSetting.iLeftRightMask =	(uint)mask;
			bool bMoveSettingChang = false;
			this.zAixsSetting1.OnGetPrinterSetting(ref ss ,ref bMoveSettingChang);
            ss.UVSetting.eUvLightType = (byte)this.cmb_UvLightType.SelectedIndex;
		}


		public void OnRealTimeChange()
		{
			int status = 0;
			if(CoreInterface.GetUVStatus(ref status)!=0)
			{
				int UV_status =  status &0x33;
#if DOUBLE_SIDE_PRINT_HAPOND
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLeftSet,MapJetStatusToUVStatus((UV_status),false,false));
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxRightSet, MapJetStatusToUVStatus((UV_status), true, false));
#else
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLeftSet,MapJetStatusToUVStatus((UV_status),true,false));
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxRightSet,MapJetStatusToUVStatus((UV_status),false,false));
#endif
                m_CheckBoxShutterLeft.Checked = ((status & 0x4)!= 0);
				m_CheckBoxShutterRight.Checked = ((status & 0x40) != 0);
#if false
				m_NumericUpDownDebugCur.Value = (byte)((status>>8)&0xff);
				m_CheckBoxOneLight.Checked = ((status>>16)&0x1) != 0;
				m_CheckBoxUVHighPower.Checked = ((status>>16)&0x2) != 0;
#endif
				int UV_statusP =  (status >>16) &0xf0;
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPLeftSet, MapJetStatusToUVStatus((UV_statusP), true, true));
                UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxPRightSet, MapJetStatusToUVStatus((UV_statusP), false, true));
                m_checkbox_autoSwitchMode.Checked = ((status >> 16) & 0x04) != 0;
            }
			else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.LoadSetFromBoardFail));

		}


		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			this.zAixsSetting1.SetPrinterStatusChanged(status);
		}

		private void m_ButtonToBoard_Click(object sender, System.EventArgs e)
		{
			int status = 0;
			status = OnGetRealTimeFromUI();
#if false
			////////////////////////////////
			byte onelight = 1;
			if(m_CheckBoxOneLight.Checked)
			{
				onelight = 1;
			}
			else
			{
				onelight = 0;
			}
			const int port = 1;
			const byte PRINTER_PIPECMDSIZE = 26;
			byte [] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
			//First Send Begin Updater
			m_pData[0] = 2 + 1;
			m_pData[1] = 0x45; //One Light mode
			m_pData[2] = onelight; 

			if(CoreInterface.SetPipeCmdPackage(m_pData,m_pData[0],port)==0)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
				return;
			}
#endif
            byte[] uvPowerLevelMap = SerializationUnit.StructToBytes(_UvPowerLevelMap);
            uint length = (uint)uvPowerLevelMap.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x68, uvPowerLevelMap, ref length, 0, 0x01);
            if (ret == 0)
            {
                MessageBox.Show("Send UVPowerLevelMap parameters error! cmd=0x68");
            }
            GZUVX2Param.UVX1Power = (ushort)numUVX1Power.Value;
            GZUVX2Param.UVX2Power = (ushort)numUVX2Power.Value;
		    if (!EpsonLCD.SetGZUVX2Param(GZUVX2Param))
            {
                MessageBox.Show("Set UV X2 parameters error!");
		    }
			if(CoreInterface.SetUVStatus(status)!=0)
			{
				if((status&0xf)==0 ||(status&0xf0)==0)
					MessageBox.Show("Please Open the UV light after 10 min.");
			}
			else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.SaveRealTimeFail));
		}
		private void UVForm_Load(object sender, System.EventArgs e)
		{
			OnRealTimeChange();
		}
		//Bit0: ON/OFF  BIT1:HIGH/LOW  BIT2:SHUTERON/OFF
		private int MapUVStatusToJetStatus(int status,bool bLeft,bool bPrinting)
		{
			int jetstatus = 0;
			switch(m_uvLightType)
			{
				case UvLightType.UvLightType1:
				{
					if(status == (int)UVStatus.OFF)
						jetstatus = 0;
					else
					{
						jetstatus |= 1;
						if(status == (int)UVStatus.ON100)
						{
							jetstatus|=2;
						}
					}
					break;
				}
                case UvLightType.UvLightType2:
                case UvLightType.UvLightType3:
				{
					jetstatus = status&0x3; 
					break;
				}
			}
			if(bPrinting)
			{
				if(bLeft)
					jetstatus = (jetstatus<<4);
				else
					jetstatus = (jetstatus<<6);
			}
			else
			{
				if(!bLeft)
					jetstatus = (jetstatus<<4);
			}
			return jetstatus;
		}
		private int MapJetStatusToUVStatus(int jetstatus,bool bLeft,bool bPrinting)
		{
			int iRet = 0;
			if(bPrinting)
			{
				if(bLeft)
					jetstatus = (jetstatus>>4);
				else
					jetstatus = (jetstatus>>6);
			}
			else
			{
				if(!bLeft)
					jetstatus = (jetstatus>>4);
			}
			switch(m_uvLightType)
			{
				case UvLightType.UvLightType1:
				{
					UVStatus status = UVStatus.OFF;
					if((jetstatus & 1)==0 )
					{
						status = UVStatus.OFF;
					}
					else
					{
						if((jetstatus & 2)==0 )
						{
							status = UVStatus.ON60;
						}
						else
						{
							status = UVStatus.ON100;
						}
					}
					iRet = (int)status;
					break;
				}
                case UvLightType.UvLightType2:
                case UvLightType.UvLightType3:
				{
					iRet = jetstatus&0x3;
					break;
				}
			}
			return iRet;
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		///
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public enum UVStatus
        //{
        //    OFF   = 0,
        //    ON60  = 1,
        //    ON100 = 2
        //}
        //public enum UVStatus_ALLWIN
        //{
        //    OFF   = 0,
        //    LOW  = 1,
        //    MID = 2,
        //    HIGH = 3
        //}
        //public enum UvLightType
        //{
        //    /// <summary>
        //    /// SubZero_055_085S
        //    /// </summary>
        //    UvLightType1 = 0,//
        //    /// <summary>
        //    /// new type
        //    /// </summary>
        //    UvLightType2 = 1,

        //    /// <summary>
        //    /// non-polar ÎÞ¼«UVµÆ
        //    /// </summary>
        //    UvLightType3=2
        //}
	}
}
