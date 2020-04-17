namespace WaveFormTool
{
	partial class WaveMappingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveMappingControl));
            this.m_GroupBox_Func = new System.Windows.Forms.GroupBox();
            this.m_GroupBox_Set = new System.Windows.Forms.GroupBox();
            this.m_TableLayoutPanel_Right = new System.Windows.Forms.TableLayoutPanel();
            this.panel_voltage = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_voltage = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_ComboBox_Channel = new System.Windows.Forms.ComboBox();
            this.panel_Color = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_ComboBox_Wave = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel_vsd = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.m_ComboBox_VSD = new System.Windows.Forms.ComboBox();
            this.m_tableLayoutPanel_DPI = new System.Windows.Forms.TableLayoutPanel();
            this.m_ComboBox_DPI = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_Button_WriteWaveMapping = new System.Windows.Forms.Button();
            this.m_Button_ReadWaveMapping = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_GroupBox_Func.SuspendLayout();
            this.m_GroupBox_Set.SuspendLayout();
            this.m_TableLayoutPanel_Right.SuspendLayout();
            this.panel_voltage.SuspendLayout();
            this.tableLayoutPanel_voltage.SuspendLayout();
            this.panel_Color.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel_vsd.SuspendLayout();
            this.m_tableLayoutPanel_DPI.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_GroupBox_Func
            // 
            this.m_GroupBox_Func.Controls.Add(this.m_GroupBox_Set);
            this.m_GroupBox_Func.Controls.Add(this.m_Button_WriteWaveMapping);
            this.m_GroupBox_Func.Controls.Add(this.m_Button_ReadWaveMapping);
            resources.ApplyResources(this.m_GroupBox_Func, "m_GroupBox_Func");
            this.m_GroupBox_Func.Name = "m_GroupBox_Func";
            this.m_GroupBox_Func.TabStop = false;
            // 
            // m_GroupBox_Set
            // 
            this.m_GroupBox_Set.Controls.Add(this.m_TableLayoutPanel_Right);
            resources.ApplyResources(this.m_GroupBox_Set, "m_GroupBox_Set");
            this.m_GroupBox_Set.Name = "m_GroupBox_Set";
            this.m_GroupBox_Set.TabStop = false;
            // 
            // m_TableLayoutPanel_Right
            // 
            resources.ApplyResources(this.m_TableLayoutPanel_Right, "m_TableLayoutPanel_Right");
            this.m_TableLayoutPanel_Right.Controls.Add(this.panel_voltage, 0, 0);
            this.m_TableLayoutPanel_Right.Controls.Add(this.panel_Color, 0, 3);
            this.m_TableLayoutPanel_Right.Controls.Add(this.tableLayoutPanel_vsd, 0, 1);
            this.m_TableLayoutPanel_Right.Controls.Add(this.m_tableLayoutPanel_DPI, 0, 2);
            this.m_TableLayoutPanel_Right.Name = "m_TableLayoutPanel_Right";
            // 
            // panel_voltage
            // 
            this.panel_voltage.Controls.Add(this.tableLayoutPanel_voltage);
            resources.ApplyResources(this.panel_voltage, "panel_voltage");
            this.panel_voltage.Name = "panel_voltage";
            // 
            // tableLayoutPanel_voltage
            // 
            resources.ApplyResources(this.tableLayoutPanel_voltage, "tableLayoutPanel_voltage");
            this.tableLayoutPanel_voltage.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel_voltage.Controls.Add(this.m_ComboBox_Channel, 1, 0);
            this.tableLayoutPanel_voltage.Name = "tableLayoutPanel_voltage";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // m_ComboBox_Channel
            // 
            resources.ApplyResources(this.m_ComboBox_Channel, "m_ComboBox_Channel");
            this.m_ComboBox_Channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBox_Channel.FormattingEnabled = true;
            this.m_ComboBox_Channel.Name = "m_ComboBox_Channel";
            this.m_ComboBox_Channel.SelectedIndexChanged += new System.EventHandler(this.m_ComboBox_Channel_SelectedIndexChanged);
            // 
            // panel_Color
            // 
            this.panel_Color.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panel_Color, "panel_Color");
            this.panel_Color.Name = "panel_Color";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_ComboBox_Wave, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // m_ComboBox_Wave
            // 
            resources.ApplyResources(this.m_ComboBox_Wave, "m_ComboBox_Wave");
            this.m_ComboBox_Wave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBox_Wave.FormattingEnabled = true;
            this.m_ComboBox_Wave.Name = "m_ComboBox_Wave";
            this.m_ComboBox_Wave.SelectedIndexChanged += new System.EventHandler(this.m_ComboBox_Wave_SelectedIndexChanged);
            // 
            // tableLayoutPanel_vsd
            // 
            resources.ApplyResources(this.tableLayoutPanel_vsd, "tableLayoutPanel_vsd");
            this.tableLayoutPanel_vsd.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel_vsd.Controls.Add(this.m_ComboBox_VSD, 1, 0);
            this.tableLayoutPanel_vsd.Name = "tableLayoutPanel_vsd";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // m_ComboBox_VSD
            // 
            resources.ApplyResources(this.m_ComboBox_VSD, "m_ComboBox_VSD");
            this.m_ComboBox_VSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBox_VSD.FormattingEnabled = true;
            this.m_ComboBox_VSD.Name = "m_ComboBox_VSD";
            this.m_ComboBox_VSD.SelectedIndexChanged += new System.EventHandler(this.m_ComboBox_VSD_SelectedIndexChanged);
            // 
            // m_tableLayoutPanel_DPI
            // 
            resources.ApplyResources(this.m_tableLayoutPanel_DPI, "m_tableLayoutPanel_DPI");
            this.m_tableLayoutPanel_DPI.Controls.Add(this.m_ComboBox_DPI, 0, 0);
            this.m_tableLayoutPanel_DPI.Controls.Add(this.label11, 0, 0);
            this.m_tableLayoutPanel_DPI.Name = "m_tableLayoutPanel_DPI";
            // 
            // m_ComboBox_DPI
            // 
            resources.ApplyResources(this.m_ComboBox_DPI, "m_ComboBox_DPI");
            this.m_ComboBox_DPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBox_DPI.FormattingEnabled = true;
            this.m_ComboBox_DPI.Name = "m_ComboBox_DPI";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // m_Button_WriteWaveMapping
            // 
            resources.ApplyResources(this.m_Button_WriteWaveMapping, "m_Button_WriteWaveMapping");
            this.m_Button_WriteWaveMapping.Name = "m_Button_WriteWaveMapping";
            this.m_Button_WriteWaveMapping.UseVisualStyleBackColor = true;
            this.m_Button_WriteWaveMapping.Click += new System.EventHandler(this.m_Button_WriteWaveMapping_Click);
            // 
            // m_Button_ReadWaveMapping
            // 
            resources.ApplyResources(this.m_Button_ReadWaveMapping, "m_Button_ReadWaveMapping");
            this.m_Button_ReadWaveMapping.Name = "m_Button_ReadWaveMapping";
            this.m_Button_ReadWaveMapping.UseVisualStyleBackColor = true;
            this.m_Button_ReadWaveMapping.Click += new System.EventHandler(this.m_Button_ReadWaveMapping_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // WaveMappingControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.m_GroupBox_Func);
            this.Name = "WaveMappingControl";
            this.m_GroupBox_Func.ResumeLayout(false);
            this.m_GroupBox_Set.ResumeLayout(false);
            this.m_TableLayoutPanel_Right.ResumeLayout(false);
            this.panel_voltage.ResumeLayout(false);
            this.tableLayoutPanel_voltage.ResumeLayout(false);
            this.tableLayoutPanel_voltage.PerformLayout();
            this.panel_Color.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel_vsd.ResumeLayout(false);
            this.tableLayoutPanel_vsd.PerformLayout();
            this.m_tableLayoutPanel_DPI.ResumeLayout(false);
            this.m_tableLayoutPanel_DPI.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.GroupBox m_GroupBox_Func;
		private System.Windows.Forms.GroupBox m_GroupBox_Set;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanel_Right;
        private System.Windows.Forms.Panel panel_voltage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_voltage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_ComboBox_Channel;
        private System.Windows.Forms.Panel panel_Color;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_ComboBox_Wave;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_vsd;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_ComboBox_VSD;
		private System.Windows.Forms.Button m_Button_WriteWaveMapping;
        private System.Windows.Forms.Button m_Button_ReadWaveMapping;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel m_tableLayoutPanel_DPI;
        private System.Windows.Forms.ComboBox m_ComboBox_DPI;
        private System.Windows.Forms.Label label11;
	}
}
