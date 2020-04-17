using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;

using System.Xml;
using System.Runtime.InteropServices;
using  System.Diagnostics;

//using  BYHXPrinterManager.Setting;
//using BYHXPrinterManager.Main;

using BYHXPrinterManager;
using Write382;
using Write501;

namespace WriteBoardConfig
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PrinterWrite : System.Windows.Forms.Form
	{
		ArrayList m_WriteConfigList = new ArrayList();	
		private const string sWriteConfig = "WriteConfigList_";
		private AllParam m_allParam;

		/// <summary>
		/// //////////////////////////////////////////////////////////////////
		/// </summary>
		
		
		private uint m_KernelMessage	= SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
		private System.Windows.Forms.MainMenu m_MainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.StatusBar m_StatusBarApp;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownHardVersion;
		private System.Windows.Forms.Button m_ButtonMBRead;
		private System.Windows.Forms.GroupBox m_GroupBoxMainBoard;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
		private System.Windows.Forms.Label m_LabelBoardID;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownSerNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownFirmVer;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownType;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWfmID1;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWfmID2;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWfmID3;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownWfmID4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button m_ButtonDualBandWrite;
		private System.Windows.Forms.Button m_ButtonWVFMWrite;
		private System.Windows.Forms.ComboBox m_ComboBoxVenderID;
		private System.Windows.Forms.Button buttonDownloadWV;
		private System.Windows.Forms.ComboBox m_ComboBoxDualBank;
		private System.Windows.Forms.ComboBox m_ComboBoxWVFMSelect;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtSerialCmd;
		private System.Windows.Forms.Button btnSendSerialCmd;
		private System.Windows.Forms.TextBox txtSerialCmdLen;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrinterWrite()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = ResString.GetProductName();
			this.m_ComboBoxDualBank.SelectedIndex = 1;
			this.m_ComboBoxWVFMSelect.SelectedIndex = 0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PrinterWrite));
			this.m_MainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.m_GroupBoxMainBoard = new System.Windows.Forms.GroupBox();
			this.m_ComboBoxDualBank = new System.Windows.Forms.ComboBox();
			this.m_ComboBoxVenderID = new System.Windows.Forms.ComboBox();
			this.m_NumericUpDownSerNo = new System.Windows.Forms.NumericUpDown();
			this.m_LabelBoardID = new System.Windows.Forms.Label();
			this.m_NumericUpDownHardVersion = new System.Windows.Forms.NumericUpDown();
			this.m_NumericUpDownFirmVer = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_NumericUpDownType = new System.Windows.Forms.NumericUpDown();
			this.m_NumericUpDownWfmID1 = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.m_NumericUpDownWfmID2 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.m_NumericUpDownWfmID3 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.m_NumericUpDownWfmID4 = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.m_ButtonDualBandWrite = new System.Windows.Forms.Button();
			this.m_ButtonMBRead = new System.Windows.Forms.Button();
			this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
			this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
			this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
			this.m_ButtonWVFMWrite = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonDownloadWV = new System.Windows.Forms.Button();
			this.m_ComboBoxWVFMSelect = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtSerialCmd = new System.Windows.Forms.TextBox();
			this.btnSendSerialCmd = new System.Windows.Forms.Button();
			this.txtSerialCmdLen = new System.Windows.Forms.TextBox();
			this.m_GroupBoxMainBoard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSerNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHardVersion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFirmVer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).BeginInit();
			this.SuspendLayout();
			// 
			// m_MainMenu
			// 
			this.m_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem1,
																					   this.menuItem5});
			this.m_MainMenu.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_MainMenu.RightToLeft")));
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = ((bool)(resources.GetObject("menuItem1.Enabled")));
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7,
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem4});
			this.menuItem1.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem1.Shortcut")));
			this.menuItem1.ShowShortcut = ((bool)(resources.GetObject("menuItem1.ShowShortcut")));
			this.menuItem1.Text = resources.GetString("menuItem1.Text");
			this.menuItem1.Visible = ((bool)(resources.GetObject("menuItem1.Visible")));
			// 
			// menuItem7
			// 
			this.menuItem7.Enabled = ((bool)(resources.GetObject("menuItem7.Enabled")));
			this.menuItem7.Index = 0;
			this.menuItem7.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem7.Shortcut")));
			this.menuItem7.ShowShortcut = ((bool)(resources.GetObject("menuItem7.ShowShortcut")));
			this.menuItem7.Text = resources.GetString("menuItem7.Text");
			this.menuItem7.Visible = ((bool)(resources.GetObject("menuItem7.Visible")));
			// 
			// menuItem2
			// 
			this.menuItem2.Enabled = ((bool)(resources.GetObject("menuItem2.Enabled")));
			this.menuItem2.Index = 1;
			this.menuItem2.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem2.Shortcut")));
			this.menuItem2.ShowShortcut = ((bool)(resources.GetObject("menuItem2.ShowShortcut")));
			this.menuItem2.Text = resources.GetString("menuItem2.Text");
			this.menuItem2.Visible = ((bool)(resources.GetObject("menuItem2.Visible")));
			// 
			// menuItem3
			// 
			this.menuItem3.Enabled = ((bool)(resources.GetObject("menuItem3.Enabled")));
			this.menuItem3.Index = 2;
			this.menuItem3.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem3.Shortcut")));
			this.menuItem3.ShowShortcut = ((bool)(resources.GetObject("menuItem3.ShowShortcut")));
			this.menuItem3.Text = resources.GetString("menuItem3.Text");
			this.menuItem3.Visible = ((bool)(resources.GetObject("menuItem3.Visible")));
			// 
			// menuItem8
			// 
			this.menuItem8.Enabled = ((bool)(resources.GetObject("menuItem8.Enabled")));
			this.menuItem8.Index = 3;
			this.menuItem8.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem8.Shortcut")));
			this.menuItem8.ShowShortcut = ((bool)(resources.GetObject("menuItem8.ShowShortcut")));
			this.menuItem8.Text = resources.GetString("menuItem8.Text");
			this.menuItem8.Visible = ((bool)(resources.GetObject("menuItem8.Visible")));
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = ((bool)(resources.GetObject("menuItem9.Enabled")));
			this.menuItem9.Index = 4;
			this.menuItem9.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem9.Shortcut")));
			this.menuItem9.ShowShortcut = ((bool)(resources.GetObject("menuItem9.ShowShortcut")));
			this.menuItem9.Text = resources.GetString("menuItem9.Text");
			this.menuItem9.Visible = ((bool)(resources.GetObject("menuItem9.Visible")));
			// 
			// menuItem4
			// 
			this.menuItem4.Enabled = ((bool)(resources.GetObject("menuItem4.Enabled")));
			this.menuItem4.Index = 5;
			this.menuItem4.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem4.Shortcut")));
			this.menuItem4.ShowShortcut = ((bool)(resources.GetObject("menuItem4.ShowShortcut")));
			this.menuItem4.Text = resources.GetString("menuItem4.Text");
			this.menuItem4.Visible = ((bool)(resources.GetObject("menuItem4.Visible")));
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = ((bool)(resources.GetObject("menuItem5.Enabled")));
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem6});
			this.menuItem5.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem5.Shortcut")));
			this.menuItem5.ShowShortcut = ((bool)(resources.GetObject("menuItem5.ShowShortcut")));
			this.menuItem5.Text = resources.GetString("menuItem5.Text");
			this.menuItem5.Visible = ((bool)(resources.GetObject("menuItem5.Visible")));
			// 
			// menuItem6
			// 
			this.menuItem6.Enabled = ((bool)(resources.GetObject("menuItem6.Enabled")));
			this.menuItem6.Index = 0;
			this.menuItem6.Shortcut = ((System.Windows.Forms.Shortcut)(resources.GetObject("menuItem6.Shortcut")));
			this.menuItem6.ShowShortcut = ((bool)(resources.GetObject("menuItem6.ShowShortcut")));
			this.menuItem6.Text = resources.GetString("menuItem6.Text");
			this.menuItem6.Visible = ((bool)(resources.GetObject("menuItem6.Visible")));
			// 
			// m_GroupBoxMainBoard
			// 
			this.m_GroupBoxMainBoard.AccessibleDescription = resources.GetString("m_GroupBoxMainBoard.AccessibleDescription");
			this.m_GroupBoxMainBoard.AccessibleName = resources.GetString("m_GroupBoxMainBoard.AccessibleName");
			this.m_GroupBoxMainBoard.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_GroupBoxMainBoard.Anchor")));
			this.m_GroupBoxMainBoard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_GroupBoxMainBoard.BackgroundImage")));
			this.m_GroupBoxMainBoard.Controls.Add(this.m_ComboBoxDualBank);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_ComboBoxVenderID);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownSerNo);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_LabelBoardID);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownHardVersion);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownFirmVer);
			this.m_GroupBoxMainBoard.Controls.Add(this.label1);
			this.m_GroupBoxMainBoard.Controls.Add(this.label2);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownType);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownWfmID1);
			this.m_GroupBoxMainBoard.Controls.Add(this.label3);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownWfmID2);
			this.m_GroupBoxMainBoard.Controls.Add(this.label4);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownWfmID3);
			this.m_GroupBoxMainBoard.Controls.Add(this.label5);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_NumericUpDownWfmID4);
			this.m_GroupBoxMainBoard.Controls.Add(this.label6);
			this.m_GroupBoxMainBoard.Controls.Add(this.label7);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_ButtonDualBandWrite);
			this.m_GroupBoxMainBoard.Controls.Add(this.m_ButtonMBRead);
			this.m_GroupBoxMainBoard.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_GroupBoxMainBoard.Dock")));
			this.m_GroupBoxMainBoard.Enabled = ((bool)(resources.GetObject("m_GroupBoxMainBoard.Enabled")));
			this.m_GroupBoxMainBoard.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_GroupBoxMainBoard.Font = ((System.Drawing.Font)(resources.GetObject("m_GroupBoxMainBoard.Font")));
			this.m_GroupBoxMainBoard.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_GroupBoxMainBoard.ImeMode")));
			this.m_GroupBoxMainBoard.Location = ((System.Drawing.Point)(resources.GetObject("m_GroupBoxMainBoard.Location")));
			this.m_GroupBoxMainBoard.Name = "m_GroupBoxMainBoard";
			this.m_GroupBoxMainBoard.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_GroupBoxMainBoard.RightToLeft")));
			this.m_GroupBoxMainBoard.Size = ((System.Drawing.Size)(resources.GetObject("m_GroupBoxMainBoard.Size")));
			this.m_GroupBoxMainBoard.TabIndex = ((int)(resources.GetObject("m_GroupBoxMainBoard.TabIndex")));
			this.m_GroupBoxMainBoard.TabStop = false;
			this.m_GroupBoxMainBoard.Text = resources.GetString("m_GroupBoxMainBoard.Text");
			this.m_GroupBoxMainBoard.Visible = ((bool)(resources.GetObject("m_GroupBoxMainBoard.Visible")));
			// 
			// m_ComboBoxDualBank
			// 
			this.m_ComboBoxDualBank.AccessibleDescription = resources.GetString("m_ComboBoxDualBank.AccessibleDescription");
			this.m_ComboBoxDualBank.AccessibleName = resources.GetString("m_ComboBoxDualBank.AccessibleName");
			this.m_ComboBoxDualBank.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ComboBoxDualBank.Anchor")));
			this.m_ComboBoxDualBank.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ComboBoxDualBank.BackgroundImage")));
			this.m_ComboBoxDualBank.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ComboBoxDualBank.Dock")));
			this.m_ComboBoxDualBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_ComboBoxDualBank.Enabled = ((bool)(resources.GetObject("m_ComboBoxDualBank.Enabled")));
			this.m_ComboBoxDualBank.Font = ((System.Drawing.Font)(resources.GetObject("m_ComboBoxDualBank.Font")));
			this.m_ComboBoxDualBank.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ComboBoxDualBank.ImeMode")));
			this.m_ComboBoxDualBank.IntegralHeight = ((bool)(resources.GetObject("m_ComboBoxDualBank.IntegralHeight")));
			this.m_ComboBoxDualBank.ItemHeight = ((int)(resources.GetObject("m_ComboBoxDualBank.ItemHeight")));
			this.m_ComboBoxDualBank.Items.AddRange(new object[] {
																	resources.GetString("m_ComboBoxDualBank.Items"),
																	resources.GetString("m_ComboBoxDualBank.Items1")});
			this.m_ComboBoxDualBank.Location = ((System.Drawing.Point)(resources.GetObject("m_ComboBoxDualBank.Location")));
			this.m_ComboBoxDualBank.MaxDropDownItems = ((int)(resources.GetObject("m_ComboBoxDualBank.MaxDropDownItems")));
			this.m_ComboBoxDualBank.MaxLength = ((int)(resources.GetObject("m_ComboBoxDualBank.MaxLength")));
			this.m_ComboBoxDualBank.Name = "m_ComboBoxDualBank";
			this.m_ComboBoxDualBank.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ComboBoxDualBank.RightToLeft")));
			this.m_ComboBoxDualBank.Size = ((System.Drawing.Size)(resources.GetObject("m_ComboBoxDualBank.Size")));
			this.m_ComboBoxDualBank.TabIndex = ((int)(resources.GetObject("m_ComboBoxDualBank.TabIndex")));
			this.m_ComboBoxDualBank.Text = resources.GetString("m_ComboBoxDualBank.Text");
			this.m_ComboBoxDualBank.Visible = ((bool)(resources.GetObject("m_ComboBoxDualBank.Visible")));
			// 
			// m_ComboBoxVenderID
			// 
			this.m_ComboBoxVenderID.AccessibleDescription = resources.GetString("m_ComboBoxVenderID.AccessibleDescription");
			this.m_ComboBoxVenderID.AccessibleName = resources.GetString("m_ComboBoxVenderID.AccessibleName");
			this.m_ComboBoxVenderID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ComboBoxVenderID.Anchor")));
			this.m_ComboBoxVenderID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ComboBoxVenderID.BackgroundImage")));
			this.m_ComboBoxVenderID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ComboBoxVenderID.Dock")));
			this.m_ComboBoxVenderID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_ComboBoxVenderID.Enabled = ((bool)(resources.GetObject("m_ComboBoxVenderID.Enabled")));
			this.m_ComboBoxVenderID.Font = ((System.Drawing.Font)(resources.GetObject("m_ComboBoxVenderID.Font")));
			this.m_ComboBoxVenderID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ComboBoxVenderID.ImeMode")));
			this.m_ComboBoxVenderID.IntegralHeight = ((bool)(resources.GetObject("m_ComboBoxVenderID.IntegralHeight")));
			this.m_ComboBoxVenderID.ItemHeight = ((int)(resources.GetObject("m_ComboBoxVenderID.ItemHeight")));
			this.m_ComboBoxVenderID.Location = ((System.Drawing.Point)(resources.GetObject("m_ComboBoxVenderID.Location")));
			this.m_ComboBoxVenderID.MaxDropDownItems = ((int)(resources.GetObject("m_ComboBoxVenderID.MaxDropDownItems")));
			this.m_ComboBoxVenderID.MaxLength = ((int)(resources.GetObject("m_ComboBoxVenderID.MaxLength")));
			this.m_ComboBoxVenderID.Name = "m_ComboBoxVenderID";
			this.m_ComboBoxVenderID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ComboBoxVenderID.RightToLeft")));
			this.m_ComboBoxVenderID.Size = ((System.Drawing.Size)(resources.GetObject("m_ComboBoxVenderID.Size")));
			this.m_ComboBoxVenderID.TabIndex = ((int)(resources.GetObject("m_ComboBoxVenderID.TabIndex")));
			this.m_ComboBoxVenderID.Text = resources.GetString("m_ComboBoxVenderID.Text");
			this.m_ComboBoxVenderID.Visible = ((bool)(resources.GetObject("m_ComboBoxVenderID.Visible")));
			// 
			// m_NumericUpDownSerNo
			// 
			this.m_NumericUpDownSerNo.AccessibleDescription = resources.GetString("m_NumericUpDownSerNo.AccessibleDescription");
			this.m_NumericUpDownSerNo.AccessibleName = resources.GetString("m_NumericUpDownSerNo.AccessibleName");
			this.m_NumericUpDownSerNo.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownSerNo.Anchor")));
			this.m_NumericUpDownSerNo.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownSerNo.Dock")));
			this.m_NumericUpDownSerNo.Enabled = ((bool)(resources.GetObject("m_NumericUpDownSerNo.Enabled")));
			this.m_NumericUpDownSerNo.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownSerNo.Font")));
			this.m_NumericUpDownSerNo.Hexadecimal = true;
			this.m_NumericUpDownSerNo.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownSerNo.ImeMode")));
			this.m_NumericUpDownSerNo.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownSerNo.Location")));
			this.m_NumericUpDownSerNo.Maximum = new System.Decimal(new int[] {
																				 -1,
																				 0,
																				 0,
																				 0});
			this.m_NumericUpDownSerNo.Name = "m_NumericUpDownSerNo";
			this.m_NumericUpDownSerNo.ReadOnly = true;
			this.m_NumericUpDownSerNo.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownSerNo.RightToLeft")));
			this.m_NumericUpDownSerNo.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownSerNo.Size")));
			this.m_NumericUpDownSerNo.TabIndex = ((int)(resources.GetObject("m_NumericUpDownSerNo.TabIndex")));
			this.m_NumericUpDownSerNo.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownSerNo.TextAlign")));
			this.m_NumericUpDownSerNo.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownSerNo.ThousandsSeparator")));
			this.m_NumericUpDownSerNo.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownSerNo.UpDownAlign")));
			this.m_NumericUpDownSerNo.Visible = ((bool)(resources.GetObject("m_NumericUpDownSerNo.Visible")));
			// 
			// m_LabelBoardID
			// 
			this.m_LabelBoardID.AccessibleDescription = resources.GetString("m_LabelBoardID.AccessibleDescription");
			this.m_LabelBoardID.AccessibleName = resources.GetString("m_LabelBoardID.AccessibleName");
			this.m_LabelBoardID.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_LabelBoardID.Anchor")));
			this.m_LabelBoardID.AutoSize = ((bool)(resources.GetObject("m_LabelBoardID.AutoSize")));
			this.m_LabelBoardID.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_LabelBoardID.Dock")));
			this.m_LabelBoardID.Enabled = ((bool)(resources.GetObject("m_LabelBoardID.Enabled")));
			this.m_LabelBoardID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_LabelBoardID.Font = ((System.Drawing.Font)(resources.GetObject("m_LabelBoardID.Font")));
			this.m_LabelBoardID.Image = ((System.Drawing.Image)(resources.GetObject("m_LabelBoardID.Image")));
			this.m_LabelBoardID.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelBoardID.ImageAlign")));
			this.m_LabelBoardID.ImageIndex = ((int)(resources.GetObject("m_LabelBoardID.ImageIndex")));
			this.m_LabelBoardID.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_LabelBoardID.ImeMode")));
			this.m_LabelBoardID.Location = ((System.Drawing.Point)(resources.GetObject("m_LabelBoardID.Location")));
			this.m_LabelBoardID.Name = "m_LabelBoardID";
			this.m_LabelBoardID.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_LabelBoardID.RightToLeft")));
			this.m_LabelBoardID.Size = ((System.Drawing.Size)(resources.GetObject("m_LabelBoardID.Size")));
			this.m_LabelBoardID.TabIndex = ((int)(resources.GetObject("m_LabelBoardID.TabIndex")));
			this.m_LabelBoardID.Text = resources.GetString("m_LabelBoardID.Text");
			this.m_LabelBoardID.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_LabelBoardID.TextAlign")));
			this.m_LabelBoardID.Visible = ((bool)(resources.GetObject("m_LabelBoardID.Visible")));
			// 
			// m_NumericUpDownHardVersion
			// 
			this.m_NumericUpDownHardVersion.AccessibleDescription = resources.GetString("m_NumericUpDownHardVersion.AccessibleDescription");
			this.m_NumericUpDownHardVersion.AccessibleName = resources.GetString("m_NumericUpDownHardVersion.AccessibleName");
			this.m_NumericUpDownHardVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownHardVersion.Anchor")));
			this.m_NumericUpDownHardVersion.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownHardVersion.Dock")));
			this.m_NumericUpDownHardVersion.Enabled = ((bool)(resources.GetObject("m_NumericUpDownHardVersion.Enabled")));
			this.m_NumericUpDownHardVersion.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownHardVersion.Font")));
			this.m_NumericUpDownHardVersion.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownHardVersion.ImeMode")));
			this.m_NumericUpDownHardVersion.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownHardVersion.Location")));
			this.m_NumericUpDownHardVersion.Maximum = new System.Decimal(new int[] {
																					   255,
																					   0,
																					   0,
																					   0});
			this.m_NumericUpDownHardVersion.Name = "m_NumericUpDownHardVersion";
			this.m_NumericUpDownHardVersion.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownHardVersion.RightToLeft")));
			this.m_NumericUpDownHardVersion.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownHardVersion.Size")));
			this.m_NumericUpDownHardVersion.TabIndex = ((int)(resources.GetObject("m_NumericUpDownHardVersion.TabIndex")));
			this.m_NumericUpDownHardVersion.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownHardVersion.TextAlign")));
			this.m_NumericUpDownHardVersion.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownHardVersion.ThousandsSeparator")));
			this.m_NumericUpDownHardVersion.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownHardVersion.UpDownAlign")));
			this.m_NumericUpDownHardVersion.Visible = ((bool)(resources.GetObject("m_NumericUpDownHardVersion.Visible")));
			this.m_NumericUpDownHardVersion.ValueChanged += new System.EventHandler(this.m_NumericUpDownVolBaseSample_ValueChanged);
			this.m_NumericUpDownHardVersion.Leave += new System.EventHandler(this.m_NumericUpDownControl_Leave);
			// 
			// m_NumericUpDownFirmVer
			// 
			this.m_NumericUpDownFirmVer.AccessibleDescription = resources.GetString("m_NumericUpDownFirmVer.AccessibleDescription");
			this.m_NumericUpDownFirmVer.AccessibleName = resources.GetString("m_NumericUpDownFirmVer.AccessibleName");
			this.m_NumericUpDownFirmVer.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownFirmVer.Anchor")));
			this.m_NumericUpDownFirmVer.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownFirmVer.Dock")));
			this.m_NumericUpDownFirmVer.Enabled = ((bool)(resources.GetObject("m_NumericUpDownFirmVer.Enabled")));
			this.m_NumericUpDownFirmVer.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownFirmVer.Font")));
			this.m_NumericUpDownFirmVer.Hexadecimal = true;
			this.m_NumericUpDownFirmVer.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownFirmVer.ImeMode")));
			this.m_NumericUpDownFirmVer.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownFirmVer.Location")));
			this.m_NumericUpDownFirmVer.Maximum = new System.Decimal(new int[] {
																				   -1,
																				   0,
																				   0,
																				   0});
			this.m_NumericUpDownFirmVer.Name = "m_NumericUpDownFirmVer";
			this.m_NumericUpDownFirmVer.ReadOnly = true;
			this.m_NumericUpDownFirmVer.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownFirmVer.RightToLeft")));
			this.m_NumericUpDownFirmVer.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownFirmVer.Size")));
			this.m_NumericUpDownFirmVer.TabIndex = ((int)(resources.GetObject("m_NumericUpDownFirmVer.TabIndex")));
			this.m_NumericUpDownFirmVer.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownFirmVer.TextAlign")));
			this.m_NumericUpDownFirmVer.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownFirmVer.ThousandsSeparator")));
			this.m_NumericUpDownFirmVer.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownFirmVer.UpDownAlign")));
			this.m_NumericUpDownFirmVer.Visible = ((bool)(resources.GetObject("m_NumericUpDownFirmVer.Visible")));
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// m_NumericUpDownType
			// 
			this.m_NumericUpDownType.AccessibleDescription = resources.GetString("m_NumericUpDownType.AccessibleDescription");
			this.m_NumericUpDownType.AccessibleName = resources.GetString("m_NumericUpDownType.AccessibleName");
			this.m_NumericUpDownType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownType.Anchor")));
			this.m_NumericUpDownType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownType.Dock")));
			this.m_NumericUpDownType.Enabled = ((bool)(resources.GetObject("m_NumericUpDownType.Enabled")));
			this.m_NumericUpDownType.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownType.Font")));
			this.m_NumericUpDownType.Hexadecimal = true;
			this.m_NumericUpDownType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownType.ImeMode")));
			this.m_NumericUpDownType.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownType.Location")));
			this.m_NumericUpDownType.Maximum = new System.Decimal(new int[] {
																				-1,
																				0,
																				0,
																				0});
			this.m_NumericUpDownType.Name = "m_NumericUpDownType";
			this.m_NumericUpDownType.ReadOnly = true;
			this.m_NumericUpDownType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownType.RightToLeft")));
			this.m_NumericUpDownType.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownType.Size")));
			this.m_NumericUpDownType.TabIndex = ((int)(resources.GetObject("m_NumericUpDownType.TabIndex")));
			this.m_NumericUpDownType.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownType.TextAlign")));
			this.m_NumericUpDownType.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownType.ThousandsSeparator")));
			this.m_NumericUpDownType.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownType.UpDownAlign")));
			this.m_NumericUpDownType.Visible = ((bool)(resources.GetObject("m_NumericUpDownType.Visible")));
			// 
			// m_NumericUpDownWfmID1
			// 
			this.m_NumericUpDownWfmID1.AccessibleDescription = resources.GetString("m_NumericUpDownWfmID1.AccessibleDescription");
			this.m_NumericUpDownWfmID1.AccessibleName = resources.GetString("m_NumericUpDownWfmID1.AccessibleName");
			this.m_NumericUpDownWfmID1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownWfmID1.Anchor")));
			this.m_NumericUpDownWfmID1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownWfmID1.Dock")));
			this.m_NumericUpDownWfmID1.Enabled = ((bool)(resources.GetObject("m_NumericUpDownWfmID1.Enabled")));
			this.m_NumericUpDownWfmID1.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownWfmID1.Font")));
			this.m_NumericUpDownWfmID1.Hexadecimal = true;
			this.m_NumericUpDownWfmID1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownWfmID1.ImeMode")));
			this.m_NumericUpDownWfmID1.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownWfmID1.Location")));
			this.m_NumericUpDownWfmID1.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownWfmID1.Name = "m_NumericUpDownWfmID1";
			this.m_NumericUpDownWfmID1.ReadOnly = true;
			this.m_NumericUpDownWfmID1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownWfmID1.RightToLeft")));
			this.m_NumericUpDownWfmID1.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownWfmID1.Size")));
			this.m_NumericUpDownWfmID1.TabIndex = ((int)(resources.GetObject("m_NumericUpDownWfmID1.TabIndex")));
			this.m_NumericUpDownWfmID1.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownWfmID1.TextAlign")));
			this.m_NumericUpDownWfmID1.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownWfmID1.ThousandsSeparator")));
			this.m_NumericUpDownWfmID1.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownWfmID1.UpDownAlign")));
			this.m_NumericUpDownWfmID1.Visible = ((bool)(resources.GetObject("m_NumericUpDownWfmID1.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// m_NumericUpDownWfmID2
			// 
			this.m_NumericUpDownWfmID2.AccessibleDescription = resources.GetString("m_NumericUpDownWfmID2.AccessibleDescription");
			this.m_NumericUpDownWfmID2.AccessibleName = resources.GetString("m_NumericUpDownWfmID2.AccessibleName");
			this.m_NumericUpDownWfmID2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownWfmID2.Anchor")));
			this.m_NumericUpDownWfmID2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownWfmID2.Dock")));
			this.m_NumericUpDownWfmID2.Enabled = ((bool)(resources.GetObject("m_NumericUpDownWfmID2.Enabled")));
			this.m_NumericUpDownWfmID2.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownWfmID2.Font")));
			this.m_NumericUpDownWfmID2.Hexadecimal = true;
			this.m_NumericUpDownWfmID2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownWfmID2.ImeMode")));
			this.m_NumericUpDownWfmID2.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownWfmID2.Location")));
			this.m_NumericUpDownWfmID2.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownWfmID2.Name = "m_NumericUpDownWfmID2";
			this.m_NumericUpDownWfmID2.ReadOnly = true;
			this.m_NumericUpDownWfmID2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownWfmID2.RightToLeft")));
			this.m_NumericUpDownWfmID2.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownWfmID2.Size")));
			this.m_NumericUpDownWfmID2.TabIndex = ((int)(resources.GetObject("m_NumericUpDownWfmID2.TabIndex")));
			this.m_NumericUpDownWfmID2.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownWfmID2.TextAlign")));
			this.m_NumericUpDownWfmID2.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownWfmID2.ThousandsSeparator")));
			this.m_NumericUpDownWfmID2.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownWfmID2.UpDownAlign")));
			this.m_NumericUpDownWfmID2.Visible = ((bool)(resources.GetObject("m_NumericUpDownWfmID2.Visible")));
			// 
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
			// 
			// m_NumericUpDownWfmID3
			// 
			this.m_NumericUpDownWfmID3.AccessibleDescription = resources.GetString("m_NumericUpDownWfmID3.AccessibleDescription");
			this.m_NumericUpDownWfmID3.AccessibleName = resources.GetString("m_NumericUpDownWfmID3.AccessibleName");
			this.m_NumericUpDownWfmID3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownWfmID3.Anchor")));
			this.m_NumericUpDownWfmID3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownWfmID3.Dock")));
			this.m_NumericUpDownWfmID3.Enabled = ((bool)(resources.GetObject("m_NumericUpDownWfmID3.Enabled")));
			this.m_NumericUpDownWfmID3.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownWfmID3.Font")));
			this.m_NumericUpDownWfmID3.Hexadecimal = true;
			this.m_NumericUpDownWfmID3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownWfmID3.ImeMode")));
			this.m_NumericUpDownWfmID3.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownWfmID3.Location")));
			this.m_NumericUpDownWfmID3.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownWfmID3.Name = "m_NumericUpDownWfmID3";
			this.m_NumericUpDownWfmID3.ReadOnly = true;
			this.m_NumericUpDownWfmID3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownWfmID3.RightToLeft")));
			this.m_NumericUpDownWfmID3.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownWfmID3.Size")));
			this.m_NumericUpDownWfmID3.TabIndex = ((int)(resources.GetObject("m_NumericUpDownWfmID3.TabIndex")));
			this.m_NumericUpDownWfmID3.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownWfmID3.TextAlign")));
			this.m_NumericUpDownWfmID3.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownWfmID3.ThousandsSeparator")));
			this.m_NumericUpDownWfmID3.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownWfmID3.UpDownAlign")));
			this.m_NumericUpDownWfmID3.Visible = ((bool)(resources.GetObject("m_NumericUpDownWfmID3.Visible")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// m_NumericUpDownWfmID4
			// 
			this.m_NumericUpDownWfmID4.AccessibleDescription = resources.GetString("m_NumericUpDownWfmID4.AccessibleDescription");
			this.m_NumericUpDownWfmID4.AccessibleName = resources.GetString("m_NumericUpDownWfmID4.AccessibleName");
			this.m_NumericUpDownWfmID4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_NumericUpDownWfmID4.Anchor")));
			this.m_NumericUpDownWfmID4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_NumericUpDownWfmID4.Dock")));
			this.m_NumericUpDownWfmID4.Enabled = ((bool)(resources.GetObject("m_NumericUpDownWfmID4.Enabled")));
			this.m_NumericUpDownWfmID4.Font = ((System.Drawing.Font)(resources.GetObject("m_NumericUpDownWfmID4.Font")));
			this.m_NumericUpDownWfmID4.Hexadecimal = true;
			this.m_NumericUpDownWfmID4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_NumericUpDownWfmID4.ImeMode")));
			this.m_NumericUpDownWfmID4.Location = ((System.Drawing.Point)(resources.GetObject("m_NumericUpDownWfmID4.Location")));
			this.m_NumericUpDownWfmID4.Maximum = new System.Decimal(new int[] {
																				  -1,
																				  0,
																				  0,
																				  0});
			this.m_NumericUpDownWfmID4.Name = "m_NumericUpDownWfmID4";
			this.m_NumericUpDownWfmID4.ReadOnly = true;
			this.m_NumericUpDownWfmID4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_NumericUpDownWfmID4.RightToLeft")));
			this.m_NumericUpDownWfmID4.Size = ((System.Drawing.Size)(resources.GetObject("m_NumericUpDownWfmID4.Size")));
			this.m_NumericUpDownWfmID4.TabIndex = ((int)(resources.GetObject("m_NumericUpDownWfmID4.TabIndex")));
			this.m_NumericUpDownWfmID4.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_NumericUpDownWfmID4.TextAlign")));
			this.m_NumericUpDownWfmID4.ThousandsSeparator = ((bool)(resources.GetObject("m_NumericUpDownWfmID4.ThousandsSeparator")));
			this.m_NumericUpDownWfmID4.UpDownAlign = ((System.Windows.Forms.LeftRightAlignment)(resources.GetObject("m_NumericUpDownWfmID4.UpDownAlign")));
			this.m_NumericUpDownWfmID4.Visible = ((bool)(resources.GetObject("m_NumericUpDownWfmID4.Visible")));
			// 
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// m_ButtonDualBandWrite
			// 
			this.m_ButtonDualBandWrite.AccessibleDescription = resources.GetString("m_ButtonDualBandWrite.AccessibleDescription");
			this.m_ButtonDualBandWrite.AccessibleName = resources.GetString("m_ButtonDualBandWrite.AccessibleName");
			this.m_ButtonDualBandWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonDualBandWrite.Anchor")));
			this.m_ButtonDualBandWrite.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonDualBandWrite.BackgroundImage")));
			this.m_ButtonDualBandWrite.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonDualBandWrite.Dock")));
			this.m_ButtonDualBandWrite.Enabled = ((bool)(resources.GetObject("m_ButtonDualBandWrite.Enabled")));
			this.m_ButtonDualBandWrite.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonDualBandWrite.FlatStyle")));
			this.m_ButtonDualBandWrite.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonDualBandWrite.Font")));
			this.m_ButtonDualBandWrite.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonDualBandWrite.Image")));
			this.m_ButtonDualBandWrite.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonDualBandWrite.ImageAlign")));
			this.m_ButtonDualBandWrite.ImageIndex = ((int)(resources.GetObject("m_ButtonDualBandWrite.ImageIndex")));
			this.m_ButtonDualBandWrite.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonDualBandWrite.ImeMode")));
			this.m_ButtonDualBandWrite.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonDualBandWrite.Location")));
			this.m_ButtonDualBandWrite.Name = "m_ButtonDualBandWrite";
			this.m_ButtonDualBandWrite.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonDualBandWrite.RightToLeft")));
			this.m_ButtonDualBandWrite.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonDualBandWrite.Size")));
			this.m_ButtonDualBandWrite.TabIndex = ((int)(resources.GetObject("m_ButtonDualBandWrite.TabIndex")));
			this.m_ButtonDualBandWrite.Text = resources.GetString("m_ButtonDualBandWrite.Text");
			this.m_ButtonDualBandWrite.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonDualBandWrite.TextAlign")));
			this.m_ButtonDualBandWrite.Visible = ((bool)(resources.GetObject("m_ButtonDualBandWrite.Visible")));
			this.m_ButtonDualBandWrite.Click += new System.EventHandler(this.m_ButtonDualBandWrite_Click);
			// 
			// m_ButtonMBRead
			// 
			this.m_ButtonMBRead.AccessibleDescription = resources.GetString("m_ButtonMBRead.AccessibleDescription");
			this.m_ButtonMBRead.AccessibleName = resources.GetString("m_ButtonMBRead.AccessibleName");
			this.m_ButtonMBRead.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonMBRead.Anchor")));
			this.m_ButtonMBRead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonMBRead.BackgroundImage")));
			this.m_ButtonMBRead.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonMBRead.Dock")));
			this.m_ButtonMBRead.Enabled = ((bool)(resources.GetObject("m_ButtonMBRead.Enabled")));
			this.m_ButtonMBRead.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonMBRead.FlatStyle")));
			this.m_ButtonMBRead.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonMBRead.Font")));
			this.m_ButtonMBRead.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonMBRead.Image")));
			this.m_ButtonMBRead.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonMBRead.ImageAlign")));
			this.m_ButtonMBRead.ImageIndex = ((int)(resources.GetObject("m_ButtonMBRead.ImageIndex")));
			this.m_ButtonMBRead.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonMBRead.ImeMode")));
			this.m_ButtonMBRead.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonMBRead.Location")));
			this.m_ButtonMBRead.Name = "m_ButtonMBRead";
			this.m_ButtonMBRead.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonMBRead.RightToLeft")));
			this.m_ButtonMBRead.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonMBRead.Size")));
			this.m_ButtonMBRead.TabIndex = ((int)(resources.GetObject("m_ButtonMBRead.TabIndex")));
			this.m_ButtonMBRead.Text = resources.GetString("m_ButtonMBRead.Text");
			this.m_ButtonMBRead.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonMBRead.TextAlign")));
			this.m_ButtonMBRead.Visible = ((bool)(resources.GetObject("m_ButtonMBRead.Visible")));
			this.m_ButtonMBRead.Click += new System.EventHandler(this.m_ButtonMBRead_Click);
			// 
			// m_StatusBarApp
			// 
			this.m_StatusBarApp.AccessibleDescription = resources.GetString("m_StatusBarApp.AccessibleDescription");
			this.m_StatusBarApp.AccessibleName = resources.GetString("m_StatusBarApp.AccessibleName");
			this.m_StatusBarApp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_StatusBarApp.Anchor")));
			this.m_StatusBarApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_StatusBarApp.BackgroundImage")));
			this.m_StatusBarApp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_StatusBarApp.Dock")));
			this.m_StatusBarApp.Enabled = ((bool)(resources.GetObject("m_StatusBarApp.Enabled")));
			this.m_StatusBarApp.Font = ((System.Drawing.Font)(resources.GetObject("m_StatusBarApp.Font")));
			this.m_StatusBarApp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_StatusBarApp.ImeMode")));
			this.m_StatusBarApp.Location = ((System.Drawing.Point)(resources.GetObject("m_StatusBarApp.Location")));
			this.m_StatusBarApp.Name = "m_StatusBarApp";
			this.m_StatusBarApp.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																							  this.m_StatusBarPanelJetStaus,
																							  this.m_StatusBarPanelError,
																							  this.m_StatusBarPanelPercent,
																							  this.m_StatusBarPanelComment});
			this.m_StatusBarApp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_StatusBarApp.RightToLeft")));
			this.m_StatusBarApp.ShowPanels = true;
			this.m_StatusBarApp.Size = ((System.Drawing.Size)(resources.GetObject("m_StatusBarApp.Size")));
			this.m_StatusBarApp.TabIndex = ((int)(resources.GetObject("m_StatusBarApp.TabIndex")));
			this.m_StatusBarApp.Text = resources.GetString("m_StatusBarApp.Text");
			this.m_StatusBarApp.Visible = ((bool)(resources.GetObject("m_StatusBarApp.Visible")));
			// 
			// m_StatusBarPanelJetStaus
			// 
			this.m_StatusBarPanelJetStaus.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelJetStaus.Alignment")));
			this.m_StatusBarPanelJetStaus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelJetStaus.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelJetStaus.Icon")));
			this.m_StatusBarPanelJetStaus.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelJetStaus.MinWidth")));
			this.m_StatusBarPanelJetStaus.Text = resources.GetString("m_StatusBarPanelJetStaus.Text");
			this.m_StatusBarPanelJetStaus.ToolTipText = resources.GetString("m_StatusBarPanelJetStaus.ToolTipText");
			this.m_StatusBarPanelJetStaus.Width = ((int)(resources.GetObject("m_StatusBarPanelJetStaus.Width")));
			// 
			// m_StatusBarPanelError
			// 
			this.m_StatusBarPanelError.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelError.Alignment")));
			this.m_StatusBarPanelError.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelError.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelError.Icon")));
			this.m_StatusBarPanelError.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelError.MinWidth")));
			this.m_StatusBarPanelError.Text = resources.GetString("m_StatusBarPanelError.Text");
			this.m_StatusBarPanelError.ToolTipText = resources.GetString("m_StatusBarPanelError.ToolTipText");
			this.m_StatusBarPanelError.Width = ((int)(resources.GetObject("m_StatusBarPanelError.Width")));
			// 
			// m_StatusBarPanelPercent
			// 
			this.m_StatusBarPanelPercent.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelPercent.Alignment")));
			this.m_StatusBarPanelPercent.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.m_StatusBarPanelPercent.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelPercent.Icon")));
			this.m_StatusBarPanelPercent.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelPercent.MinWidth")));
			this.m_StatusBarPanelPercent.Text = resources.GetString("m_StatusBarPanelPercent.Text");
			this.m_StatusBarPanelPercent.ToolTipText = resources.GetString("m_StatusBarPanelPercent.ToolTipText");
			this.m_StatusBarPanelPercent.Width = ((int)(resources.GetObject("m_StatusBarPanelPercent.Width")));
			// 
			// m_StatusBarPanelComment
			// 
			this.m_StatusBarPanelComment.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_StatusBarPanelComment.Alignment")));
			this.m_StatusBarPanelComment.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.m_StatusBarPanelComment.Icon = ((System.Drawing.Icon)(resources.GetObject("m_StatusBarPanelComment.Icon")));
			this.m_StatusBarPanelComment.MinWidth = ((int)(resources.GetObject("m_StatusBarPanelComment.MinWidth")));
			this.m_StatusBarPanelComment.Text = resources.GetString("m_StatusBarPanelComment.Text");
			this.m_StatusBarPanelComment.ToolTipText = resources.GetString("m_StatusBarPanelComment.ToolTipText");
			this.m_StatusBarPanelComment.Width = ((int)(resources.GetObject("m_StatusBarPanelComment.Width")));
			// 
			// m_ButtonWVFMWrite
			// 
			this.m_ButtonWVFMWrite.AccessibleDescription = resources.GetString("m_ButtonWVFMWrite.AccessibleDescription");
			this.m_ButtonWVFMWrite.AccessibleName = resources.GetString("m_ButtonWVFMWrite.AccessibleName");
			this.m_ButtonWVFMWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ButtonWVFMWrite.Anchor")));
			this.m_ButtonWVFMWrite.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ButtonWVFMWrite.BackgroundImage")));
			this.m_ButtonWVFMWrite.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ButtonWVFMWrite.Dock")));
			this.m_ButtonWVFMWrite.Enabled = ((bool)(resources.GetObject("m_ButtonWVFMWrite.Enabled")));
			this.m_ButtonWVFMWrite.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("m_ButtonWVFMWrite.FlatStyle")));
			this.m_ButtonWVFMWrite.Font = ((System.Drawing.Font)(resources.GetObject("m_ButtonWVFMWrite.Font")));
			this.m_ButtonWVFMWrite.Image = ((System.Drawing.Image)(resources.GetObject("m_ButtonWVFMWrite.Image")));
			this.m_ButtonWVFMWrite.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonWVFMWrite.ImageAlign")));
			this.m_ButtonWVFMWrite.ImageIndex = ((int)(resources.GetObject("m_ButtonWVFMWrite.ImageIndex")));
			this.m_ButtonWVFMWrite.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ButtonWVFMWrite.ImeMode")));
			this.m_ButtonWVFMWrite.Location = ((System.Drawing.Point)(resources.GetObject("m_ButtonWVFMWrite.Location")));
			this.m_ButtonWVFMWrite.Name = "m_ButtonWVFMWrite";
			this.m_ButtonWVFMWrite.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ButtonWVFMWrite.RightToLeft")));
			this.m_ButtonWVFMWrite.Size = ((System.Drawing.Size)(resources.GetObject("m_ButtonWVFMWrite.Size")));
			this.m_ButtonWVFMWrite.TabIndex = ((int)(resources.GetObject("m_ButtonWVFMWrite.TabIndex")));
			this.m_ButtonWVFMWrite.Text = resources.GetString("m_ButtonWVFMWrite.Text");
			this.m_ButtonWVFMWrite.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("m_ButtonWVFMWrite.TextAlign")));
			this.m_ButtonWVFMWrite.Visible = ((bool)(resources.GetObject("m_ButtonWVFMWrite.Visible")));
			this.m_ButtonWVFMWrite.Click += new System.EventHandler(this.m_ButtonWVFMWrite_Click);
			// 
			// label8
			// 
			this.label8.AccessibleDescription = resources.GetString("label8.AccessibleDescription");
			this.label8.AccessibleName = resources.GetString("label8.AccessibleName");
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label8.Anchor")));
			this.label8.AutoSize = ((bool)(resources.GetObject("label8.AutoSize")));
			this.label8.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label8.Dock")));
			this.label8.Enabled = ((bool)(resources.GetObject("label8.Enabled")));
			this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label8.Font = ((System.Drawing.Font)(resources.GetObject("label8.Font")));
			this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
			this.label8.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.ImageAlign")));
			this.label8.ImageIndex = ((int)(resources.GetObject("label8.ImageIndex")));
			this.label8.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label8.ImeMode")));
			this.label8.Location = ((System.Drawing.Point)(resources.GetObject("label8.Location")));
			this.label8.Name = "label8";
			this.label8.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label8.RightToLeft")));
			this.label8.Size = ((System.Drawing.Size)(resources.GetObject("label8.Size")));
			this.label8.TabIndex = ((int)(resources.GetObject("label8.TabIndex")));
			this.label8.Text = resources.GetString("label8.Text");
			this.label8.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label8.TextAlign")));
			this.label8.Visible = ((bool)(resources.GetObject("label8.Visible")));
			// 
			// buttonDownloadWV
			// 
			this.buttonDownloadWV.AccessibleDescription = resources.GetString("buttonDownloadWV.AccessibleDescription");
			this.buttonDownloadWV.AccessibleName = resources.GetString("buttonDownloadWV.AccessibleName");
			this.buttonDownloadWV.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("buttonDownloadWV.Anchor")));
			this.buttonDownloadWV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDownloadWV.BackgroundImage")));
			this.buttonDownloadWV.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("buttonDownloadWV.Dock")));
			this.buttonDownloadWV.Enabled = ((bool)(resources.GetObject("buttonDownloadWV.Enabled")));
			this.buttonDownloadWV.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("buttonDownloadWV.FlatStyle")));
			this.buttonDownloadWV.Font = ((System.Drawing.Font)(resources.GetObject("buttonDownloadWV.Font")));
			this.buttonDownloadWV.Image = ((System.Drawing.Image)(resources.GetObject("buttonDownloadWV.Image")));
			this.buttonDownloadWV.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonDownloadWV.ImageAlign")));
			this.buttonDownloadWV.ImageIndex = ((int)(resources.GetObject("buttonDownloadWV.ImageIndex")));
			this.buttonDownloadWV.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("buttonDownloadWV.ImeMode")));
			this.buttonDownloadWV.Location = ((System.Drawing.Point)(resources.GetObject("buttonDownloadWV.Location")));
			this.buttonDownloadWV.Name = "buttonDownloadWV";
			this.buttonDownloadWV.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("buttonDownloadWV.RightToLeft")));
			this.buttonDownloadWV.Size = ((System.Drawing.Size)(resources.GetObject("buttonDownloadWV.Size")));
			this.buttonDownloadWV.TabIndex = ((int)(resources.GetObject("buttonDownloadWV.TabIndex")));
			this.buttonDownloadWV.Text = resources.GetString("buttonDownloadWV.Text");
			this.buttonDownloadWV.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("buttonDownloadWV.TextAlign")));
			this.buttonDownloadWV.Visible = ((bool)(resources.GetObject("buttonDownloadWV.Visible")));
			this.buttonDownloadWV.Click += new System.EventHandler(this.buttonDownloadWV_Click);
			// 
			// m_ComboBoxWVFMSelect
			// 
			this.m_ComboBoxWVFMSelect.AccessibleDescription = resources.GetString("m_ComboBoxWVFMSelect.AccessibleDescription");
			this.m_ComboBoxWVFMSelect.AccessibleName = resources.GetString("m_ComboBoxWVFMSelect.AccessibleName");
			this.m_ComboBoxWVFMSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_ComboBoxWVFMSelect.Anchor")));
			this.m_ComboBoxWVFMSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_ComboBoxWVFMSelect.BackgroundImage")));
			this.m_ComboBoxWVFMSelect.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_ComboBoxWVFMSelect.Dock")));
			this.m_ComboBoxWVFMSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_ComboBoxWVFMSelect.Enabled = ((bool)(resources.GetObject("m_ComboBoxWVFMSelect.Enabled")));
			this.m_ComboBoxWVFMSelect.Font = ((System.Drawing.Font)(resources.GetObject("m_ComboBoxWVFMSelect.Font")));
			this.m_ComboBoxWVFMSelect.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_ComboBoxWVFMSelect.ImeMode")));
			this.m_ComboBoxWVFMSelect.IntegralHeight = ((bool)(resources.GetObject("m_ComboBoxWVFMSelect.IntegralHeight")));
			this.m_ComboBoxWVFMSelect.ItemHeight = ((int)(resources.GetObject("m_ComboBoxWVFMSelect.ItemHeight")));
			this.m_ComboBoxWVFMSelect.Items.AddRange(new object[] {
																	  resources.GetString("m_ComboBoxWVFMSelect.Items"),
																	  resources.GetString("m_ComboBoxWVFMSelect.Items1"),
																	  resources.GetString("m_ComboBoxWVFMSelect.Items2"),
																	  resources.GetString("m_ComboBoxWVFMSelect.Items3")});
			this.m_ComboBoxWVFMSelect.Location = ((System.Drawing.Point)(resources.GetObject("m_ComboBoxWVFMSelect.Location")));
			this.m_ComboBoxWVFMSelect.MaxDropDownItems = ((int)(resources.GetObject("m_ComboBoxWVFMSelect.MaxDropDownItems")));
			this.m_ComboBoxWVFMSelect.MaxLength = ((int)(resources.GetObject("m_ComboBoxWVFMSelect.MaxLength")));
			this.m_ComboBoxWVFMSelect.Name = "m_ComboBoxWVFMSelect";
			this.m_ComboBoxWVFMSelect.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_ComboBoxWVFMSelect.RightToLeft")));
			this.m_ComboBoxWVFMSelect.Size = ((System.Drawing.Size)(resources.GetObject("m_ComboBoxWVFMSelect.Size")));
			this.m_ComboBoxWVFMSelect.TabIndex = ((int)(resources.GetObject("m_ComboBoxWVFMSelect.TabIndex")));
			this.m_ComboBoxWVFMSelect.Text = resources.GetString("m_ComboBoxWVFMSelect.Text");
			this.m_ComboBoxWVFMSelect.Visible = ((bool)(resources.GetObject("m_ComboBoxWVFMSelect.Visible")));
			// 
			// label9
			// 
			this.label9.AccessibleDescription = resources.GetString("label9.AccessibleDescription");
			this.label9.AccessibleName = resources.GetString("label9.AccessibleName");
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label9.Anchor")));
			this.label9.AutoSize = ((bool)(resources.GetObject("label9.AutoSize")));
			this.label9.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label9.Dock")));
			this.label9.Enabled = ((bool)(resources.GetObject("label9.Enabled")));
			this.label9.Font = ((System.Drawing.Font)(resources.GetObject("label9.Font")));
			this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
			this.label9.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.ImageAlign")));
			this.label9.ImageIndex = ((int)(resources.GetObject("label9.ImageIndex")));
			this.label9.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label9.ImeMode")));
			this.label9.Location = ((System.Drawing.Point)(resources.GetObject("label9.Location")));
			this.label9.Name = "label9";
			this.label9.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label9.RightToLeft")));
			this.label9.Size = ((System.Drawing.Size)(resources.GetObject("label9.Size")));
			this.label9.TabIndex = ((int)(resources.GetObject("label9.TabIndex")));
			this.label9.Text = resources.GetString("label9.Text");
			this.label9.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label9.TextAlign")));
			this.label9.Visible = ((bool)(resources.GetObject("label9.Visible")));
			// 
			// txtSerialCmd
			// 
			this.txtSerialCmd.AccessibleDescription = resources.GetString("txtSerialCmd.AccessibleDescription");
			this.txtSerialCmd.AccessibleName = resources.GetString("txtSerialCmd.AccessibleName");
			this.txtSerialCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtSerialCmd.Anchor")));
			this.txtSerialCmd.AutoSize = ((bool)(resources.GetObject("txtSerialCmd.AutoSize")));
			this.txtSerialCmd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtSerialCmd.BackgroundImage")));
			this.txtSerialCmd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtSerialCmd.Dock")));
			this.txtSerialCmd.Enabled = ((bool)(resources.GetObject("txtSerialCmd.Enabled")));
			this.txtSerialCmd.Font = ((System.Drawing.Font)(resources.GetObject("txtSerialCmd.Font")));
			this.txtSerialCmd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtSerialCmd.ImeMode")));
			this.txtSerialCmd.Location = ((System.Drawing.Point)(resources.GetObject("txtSerialCmd.Location")));
			this.txtSerialCmd.MaxLength = ((int)(resources.GetObject("txtSerialCmd.MaxLength")));
			this.txtSerialCmd.Multiline = ((bool)(resources.GetObject("txtSerialCmd.Multiline")));
			this.txtSerialCmd.Name = "txtSerialCmd";
			this.txtSerialCmd.PasswordChar = ((char)(resources.GetObject("txtSerialCmd.PasswordChar")));
			this.txtSerialCmd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtSerialCmd.RightToLeft")));
			this.txtSerialCmd.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtSerialCmd.ScrollBars")));
			this.txtSerialCmd.Size = ((System.Drawing.Size)(resources.GetObject("txtSerialCmd.Size")));
			this.txtSerialCmd.TabIndex = ((int)(resources.GetObject("txtSerialCmd.TabIndex")));
			this.txtSerialCmd.Text = resources.GetString("txtSerialCmd.Text");
			this.txtSerialCmd.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtSerialCmd.TextAlign")));
			this.txtSerialCmd.Visible = ((bool)(resources.GetObject("txtSerialCmd.Visible")));
			this.txtSerialCmd.WordWrap = ((bool)(resources.GetObject("txtSerialCmd.WordWrap")));
			this.txtSerialCmd.TextChanged += new System.EventHandler(this.txtSerialCmd_TextChanged);
			// 
			// btnSendSerialCmd
			// 
			this.btnSendSerialCmd.AccessibleDescription = resources.GetString("btnSendSerialCmd.AccessibleDescription");
			this.btnSendSerialCmd.AccessibleName = resources.GetString("btnSendSerialCmd.AccessibleName");
			this.btnSendSerialCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSendSerialCmd.Anchor")));
			this.btnSendSerialCmd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendSerialCmd.BackgroundImage")));
			this.btnSendSerialCmd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSendSerialCmd.Dock")));
			this.btnSendSerialCmd.Enabled = ((bool)(resources.GetObject("btnSendSerialCmd.Enabled")));
			this.btnSendSerialCmd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("btnSendSerialCmd.FlatStyle")));
			this.btnSendSerialCmd.Font = ((System.Drawing.Font)(resources.GetObject("btnSendSerialCmd.Font")));
			this.btnSendSerialCmd.Image = ((System.Drawing.Image)(resources.GetObject("btnSendSerialCmd.Image")));
			this.btnSendSerialCmd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSendSerialCmd.ImageAlign")));
			this.btnSendSerialCmd.ImageIndex = ((int)(resources.GetObject("btnSendSerialCmd.ImageIndex")));
			this.btnSendSerialCmd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSendSerialCmd.ImeMode")));
			this.btnSendSerialCmd.Location = ((System.Drawing.Point)(resources.GetObject("btnSendSerialCmd.Location")));
			this.btnSendSerialCmd.Name = "btnSendSerialCmd";
			this.btnSendSerialCmd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSendSerialCmd.RightToLeft")));
			this.btnSendSerialCmd.Size = ((System.Drawing.Size)(resources.GetObject("btnSendSerialCmd.Size")));
			this.btnSendSerialCmd.TabIndex = ((int)(resources.GetObject("btnSendSerialCmd.TabIndex")));
			this.btnSendSerialCmd.Text = resources.GetString("btnSendSerialCmd.Text");
			this.btnSendSerialCmd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("btnSendSerialCmd.TextAlign")));
			this.btnSendSerialCmd.Visible = ((bool)(resources.GetObject("btnSendSerialCmd.Visible")));
			this.btnSendSerialCmd.Click += new System.EventHandler(this.btnSendSerialCmd_Click);
			// 
			// txtSerialCmdLen
			// 
			this.txtSerialCmdLen.AccessibleDescription = resources.GetString("txtSerialCmdLen.AccessibleDescription");
			this.txtSerialCmdLen.AccessibleName = resources.GetString("txtSerialCmdLen.AccessibleName");
			this.txtSerialCmdLen.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtSerialCmdLen.Anchor")));
			this.txtSerialCmdLen.AutoSize = ((bool)(resources.GetObject("txtSerialCmdLen.AutoSize")));
			this.txtSerialCmdLen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtSerialCmdLen.BackgroundImage")));
			this.txtSerialCmdLen.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtSerialCmdLen.Dock")));
			this.txtSerialCmdLen.Enabled = ((bool)(resources.GetObject("txtSerialCmdLen.Enabled")));
			this.txtSerialCmdLen.Font = ((System.Drawing.Font)(resources.GetObject("txtSerialCmdLen.Font")));
			this.txtSerialCmdLen.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtSerialCmdLen.ImeMode")));
			this.txtSerialCmdLen.Location = ((System.Drawing.Point)(resources.GetObject("txtSerialCmdLen.Location")));
			this.txtSerialCmdLen.MaxLength = ((int)(resources.GetObject("txtSerialCmdLen.MaxLength")));
			this.txtSerialCmdLen.Multiline = ((bool)(resources.GetObject("txtSerialCmdLen.Multiline")));
			this.txtSerialCmdLen.Name = "txtSerialCmdLen";
			this.txtSerialCmdLen.PasswordChar = ((char)(resources.GetObject("txtSerialCmdLen.PasswordChar")));
			this.txtSerialCmdLen.ReadOnly = true;
			this.txtSerialCmdLen.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtSerialCmdLen.RightToLeft")));
			this.txtSerialCmdLen.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtSerialCmdLen.ScrollBars")));
			this.txtSerialCmdLen.Size = ((System.Drawing.Size)(resources.GetObject("txtSerialCmdLen.Size")));
			this.txtSerialCmdLen.TabIndex = ((int)(resources.GetObject("txtSerialCmdLen.TabIndex")));
			this.txtSerialCmdLen.Text = resources.GetString("txtSerialCmdLen.Text");
			this.txtSerialCmdLen.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtSerialCmdLen.TextAlign")));
			this.txtSerialCmdLen.Visible = ((bool)(resources.GetObject("txtSerialCmdLen.Visible")));
			this.txtSerialCmdLen.WordWrap = ((bool)(resources.GetObject("txtSerialCmdLen.WordWrap")));
			// 
			// PrinterWrite
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.Add(this.txtSerialCmdLen);
			this.Controls.Add(this.btnSendSerialCmd);
			this.Controls.Add(this.txtSerialCmd);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.buttonDownloadWV);
			this.Controls.Add(this.m_StatusBarApp);
			this.Controls.Add(this.m_GroupBoxMainBoard);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.m_ButtonWVFMWrite);
			this.Controls.Add(this.m_ComboBoxWVFMSelect);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "PrinterWrite";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PrinterWrite_Closing);
			this.m_GroupBoxMainBoard.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownSerNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownHardVersion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownFirmVer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownWfmID4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		public bool Start()
		{
			CoreInterface.SystemInit();
			if( CoreInterface.SetMessageWindow(this.Handle,m_KernelMessage)== 0)
			{
				return false;
			}
			m_allParam = new AllParam();
			m_allParam.LoadFromXml(null,true);
			OnPreferenceChange(m_allParam.Preference);
			OnPrinterPropertyChange(m_allParam.PrinterProperty);
			OnPrinterSettingChange(m_allParam.PrinterSetting);

			//Must after printer property because status depend on property sensor measurepaper
			JetStatusEnum status = CoreInterface.GetBoardStatus();
			OnPrinterStatusChanged(status);

			m_ComboBoxVenderID.Items.Clear();
			for (int i=0; i<16;i++)
				m_ComboBoxVenderID.Items.Add(i.ToString());
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxVenderID,0);
		
			return true;
		}

		public bool End()
		{

			if(m_allParam != null)
			{
				m_allParam.SaveToXml(null,true);
			}

			CoreInterface.SystemClose();
			return true;
		}
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_allParam.PrinterProperty = sp;
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_allParam.PrinterSetting = ss;
		}
		public void OnPreferenceChange( UIPreference up)
		{
			m_allParam.Preference = up;
		}

		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
			UpdateButtonStates(status);
			SetPrinterStatusChanged(status);
			if(status == JetStatusEnum.Error)
			{
				OnErrorCodeChanged(CoreInterface.GetBoardError());

				int errorCode = CoreInterface.GetBoardError();
				SErrorCode sErrorCode= new SErrorCode(errorCode);
				if(SErrorCode.IsOnlyPauseError(errorCode))
				{
					string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);

					if(MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation) == DialogResult.Retry)
					{
						CoreInterface.Printer_Resume();
					}
				}
			}
			else
				OnErrorCodeChanged(0);
		}
		private void UpdateButtonStates(JetStatusEnum status)
		{
			bool benable = status!= JetStatusEnum.PowerOff;
			this.m_ButtonDualBandWrite.Enabled =benable;
			this.m_ButtonMBRead.Enabled = benable;
			this.m_ButtonWVFMWrite.Enabled = benable;
			this.buttonDownloadWV.Enabled = benable;
		}

		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			string info = ResString.GetEnumDisplayName(typeof(JetStatusEnum),status);
			this.m_StatusBarPanelJetStaus.Text = info;
		}


		////////////////////////////////////////////////////////////////////////////////////////
		///
		///	default parameter
		///
		///
		////////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		///
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			// enable XP theme support
			Application.EnableVisualStyles();
			Application.DoEvents();
			AllParam cur = new AllParam();
			int lcid = cur.GetLanguage();
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);

            const string MUTEX = CoreConst.c_MUTEX_App;
            bool createdNew = false;
            Mutex mutex = new Mutex(true, MUTEX, out createdNew);
            if (!createdNew)
            {
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.OnlyOneProgram));
                mutex.Close();
                return;
            }
#if true

            MainForm mainForm = new MainForm();
		    mainForm.StartPosition = FormStartPosition.CenterScreen;
		    if (mainForm.Start())
		    {
		        mainForm.ShowDialog();
		    }
		    else
		    {
                mutex.Close();
                return;
		    }

            if (mainForm.SelcetedCase == 3)
            {
                KyoceraWaveform mainWin = new KyoceraWaveform();
                if (mainWin.Start())
                    Application.Run(mainWin);
            }

		    if (mainForm.SelcetedCase == 2)
		    {
		        Xaar501Waveform mainWin = new Xaar501Waveform();
		        if (mainWin.Start())
		            Application.Run(mainWin);
		    }
		    if (mainForm.SelcetedCase == 1)
		    {
		        PrinterWrite mainWin = new PrinterWrite();
		        if (mainWin.Start())
		            Application.Run(mainWin);
		    }
#else
			FormHeadBoard hb = new FormHeadBoard();
			Application.Run(hb);
#endif

            mutex.Close();
		}

		private void m_NumericUpDownVolBaseSample_ValueChanged(object sender, System.EventArgs e)
		{
			NumericUpDown cur = (NumericUpDown)sender;
			cur.Text = cur.Value.ToString();
		}
		private void m_NumericUpDownControl_Leave(object sender, System.EventArgs e)
		{
			NumericUpDown textBox = (NumericUpDown)sender;
			bool isValidNumber = true;
			try
			{
				int val = int.Parse(textBox.Text);
				textBox.Value = new Decimal(val);
			}
			catch(Exception )
			{
				//Console.WriteLine(ex.Message);
				isValidNumber = false;
			}

			if(!isValidNumber)
			{
				SystemCall.Beep(200,50);
				textBox.Focus();
				textBox.Select(0,textBox.Text.Length);
			}
		}

		private void PrinterWrite_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			End();
		}

		protected override void WndProc(ref Message m)
		{
			if(m.WParam.ToInt32()==   0xF060)   //   关闭消息   
			{   
				string info = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.Exit);
				if(MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}
			}   
			base.WndProc(ref m);

			if(m.Msg == this.m_KernelMessage)
			{
				ProceedKernelMessage(m.WParam,m.LParam);
			}
		}
		private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
		{
			CoreMsgEnum	kParam	= (CoreMsgEnum)wParam.ToInt64();

			switch(kParam)
			{
				case CoreMsgEnum.UpdaterPercentage:
				{
                    int percentage = (int)lParam.ToInt64();
					//OnPrintingProgressChanged(percentage);
					string info = "";
					string mPrintingFormat = ResString.GetUpdatingProgress();
					info += "\n" + string.Format(mPrintingFormat,percentage);
					this.m_StatusBarPanelPercent.Text = info;
					break;
				}

				case CoreMsgEnum.Percentage:
				{
                    int percentage = (int)lParam.ToInt64();
					OnPrintingProgressChanged(percentage);

					break;
				}
				case CoreMsgEnum.Job_Begin:
				{

                    int startType = (int)lParam.ToInt64();

					if(startType == 0)
					{
					}
					else if(startType == 1)
					{
						//OnPrintingStart();
					}

					break;
				}
				case CoreMsgEnum.Job_End:
				{

                    int endType = (int)lParam.ToInt64();

					if(endType == 0)
					{
					}
					else if(endType == 1)
					{
						//OnPrintingEnd();
					}

					break;
				}
				case CoreMsgEnum.Power_On:
				{
                    int bPowerOn = (int)lParam.ToInt64();
					if(bPowerOn != 0)
					{
						SPrinterProperty sPrinterProperty = m_allParam.PrinterProperty;
						if(CoreInterface.GetSPrinterProperty(ref sPrinterProperty) == 0)
						{
							Debug.Assert(false);
						}
						else
						{
							OnPrinterPropertyChange(sPrinterProperty);
						}

						SPrinterSetting sPrinterSetting = m_allParam.PrinterSetting;
						if(CoreInterface.GetPrinterSetting(ref sPrinterSetting) == 0)
						{
							Debug.Assert(false);
						}
						else
						{
							OnPrinterSettingChange(sPrinterSetting);
						}
					}
					else
					{
						//this.m_JobListForm.TerminatePrintingJob(false);
						CoreInterface.SavePrinterSetting();
					}
					break;
				}
				case CoreMsgEnum.Status_Change:
				{
                    int status = (int)lParam.ToInt64();
					OnPrinterStatusChanged((JetStatusEnum)status);
					break;
				}
				case CoreMsgEnum.ErrorCode:
				{
                    OnErrorCodeChanged((int)lParam.ToInt64());
					//For Updateing
                    int errorcode = (int)lParam.ToInt64();
					SErrorCode serrorcode = new SErrorCode(errorcode);
					ErrorCause cause = (ErrorCause)serrorcode.nErrorCause;
					if(cause == ErrorCause.CoreBoard && (ErrorAction)serrorcode.nErrorAction == ErrorAction.Updating)
					{
						if(0 != serrorcode.nErrorCode)
						{
							if(serrorcode.nErrorCode == 1)
							{
								string info = ResString.GetEnumDisplayName(typeof(UISuccess),UISuccess.UpdateSuccess);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
							else
							{
								string info = ResString.GetEnumDisplayName(typeof(UIError),UIError.UpdateFail);
								MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
							}
#if !LIYUUSB
							CoreInterface.SendJetCommand((int)JetCmdEnum.ClearUpdatingStatus,0);
#endif
						}
					}

					break;
				}
				case CoreMsgEnum.Parameter_Change:
				{
					//m_LockUpdate = true;
					SPrinterSetting sPrinterSetting = m_allParam.PrinterSetting;
					if(CoreInterface.GetPrinterSetting(ref sPrinterSetting) == 0)
					{
						Debug.Assert(false);
					}
					else
					{
						OnPrinterSettingChange(sPrinterSetting);
					}
					//m_LockUpdate = false;
					break;
				}
			}
		}

		public void OnPrintingProgressChanged(int percent)
		{
			string info = "";
			string mPrintingFormat = ResString.GetPrintingProgress();
			info += "\n" + string.Format(mPrintingFormat,percent);
			this.m_StatusBarPanelPercent.Text = info;
		}
		public void OnErrorCodeChanged(int code)
		{
			this.m_StatusBarPanelError.Text = SErrorCode.GetInfoFromErrCode(code);
		}
		private void On382InfoChange(SHeadInfoType_382 sPrinterSetting)
		{
			m_NumericUpDownSerNo.Value = sPrinterSetting.SerNo;
			m_NumericUpDownFirmVer.Value = sPrinterSetting.FirmVer;
			m_NumericUpDownType.Value = sPrinterSetting.Type;
			m_NumericUpDownWfmID1.Value = sPrinterSetting.WfmID1;
			m_NumericUpDownWfmID2.Value = sPrinterSetting.WfmID2;
			m_NumericUpDownWfmID3.Value = sPrinterSetting.WfmID3;
			m_NumericUpDownWfmID4.Value = sPrinterSetting.WfmID4;
			this.m_ComboBoxDualBank.SelectedIndex = sPrinterSetting.DualBank;
		}

		private void Reset382Info()
		{
			m_NumericUpDownSerNo.Value = m_NumericUpDownSerNo.Minimum;
			m_NumericUpDownFirmVer.Value = m_NumericUpDownFirmVer.Minimum;
			m_NumericUpDownType.Value = m_NumericUpDownType.Minimum;
			m_NumericUpDownWfmID1.Value = m_NumericUpDownWfmID1.Minimum;
			m_NumericUpDownWfmID2.Value = m_NumericUpDownWfmID2.Minimum;
			m_NumericUpDownWfmID3.Value = m_NumericUpDownWfmID3.Minimum;
			m_NumericUpDownWfmID4.Value = m_NumericUpDownWfmID4.Minimum;
			this.m_ComboBoxDualBank.SelectedIndex = -1;
		}
		private void m_ButtonMBRead_Click(object sender, System.EventArgs e)
		{
			Reset382Info();
			SHeadInfoType_382 sPrinterSetting = new SHeadInfoType_382();
			int nheadIndex = m_ComboBoxVenderID.SelectedIndex;
			if(CoreInterface.Get382HeadInfo(ref sPrinterSetting,nheadIndex) == 0)
			{
				Debug.Assert(false);
				MessageBox.Show("读取喷头信息失败.");
			}
			else
			{
				On382InfoChange(sPrinterSetting);
				MessageBox.Show("读取喷头信息成功.");
			}
		}

		private void m_ButtonDualBandWrite_Click(object sender, System.EventArgs e)
		{
			int wv = Decimal.ToInt32(m_ComboBoxDualBank.SelectedIndex);
			int nheadIndex = m_ComboBoxVenderID.SelectedIndex;
			if(CoreInterface.Set382DualBand((ushort)wv,nheadIndex) == 0)
			{
				Debug.Assert(false);
				MessageBox.Show("设置DualBand失败.");
			}
		}

		private void m_ButtonWVFMWrite_Click(object sender, System.EventArgs e)
		{
			int wv =  Decimal.ToInt32(m_ComboBoxWVFMSelect.SelectedIndex);
			int nheadIndex = m_ComboBoxVenderID.SelectedIndex;
			if(CoreInterface.Set382WVFMSelect(wv,nheadIndex) == 0)
			{
				Debug.Assert(false);
				MessageBox.Show("设置382WVFMSelect 失败.");
			}
		}

		private void buttonDownloadWV_Click(object sender, System.EventArgs e)
		{
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			if(printerStatus == JetStatusEnum.Busy)
			{
				if(MessageBox.Show(this, 
					"您确认要升级吗?", 
					"升级", 
					System.Windows.Forms.MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2)
					== DialogResult.No)
				{
					return;
				}
				else
				{
					CoreInterface.Printer_Abort();
				}
			}
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".txt";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Txt);
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
				UpdateCoreBoard(fileDialog.FileName);
			}
		}

		private void UpdateCoreBoard(string m_UpdaterFileName)
		{
			bool bRead = false;
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				buffer			= new byte[fileLen];
				int				readBytes		= 0;
				fileStream.Seek(0,SeekOrigin.Begin);
				readBytes	= binaryReader.Read(buffer,0,fileLen);
				Debug.Assert(fileLen == readBytes);

				binaryReader.Close();
				fileStream.Close();
				bRead = true;
			}
			catch{}
			if(bRead)
			{
				//CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
				int nheadIndex = m_ComboBoxVenderID.SelectedIndex;
				if(CoreInterface.Down382WaveForm(buffer,fileLen,nheadIndex)==0)
					MessageBox.Show("失败!");
			}
		}

		private void btnSendSerialCmd_Click(object sender, System.EventArgs e)
		{
			try
			{
				int len = this.txtSerialCmd.Text.Length/2;
				byte[] cmds= new byte[len];
				for(int i = 0; i< len; i++)
				{
					byte cmd = byte.Parse(this.txtSerialCmd.Text.Substring(i*2,2),NumberStyles.HexNumber);
					cmds[i] = cmd;
				}
				if(CoreInterface.Set382ComCmd(cmds,len,this.m_ComboBoxVenderID.SelectedIndex,0)==0)
					MessageBox.Show("失败!");
				else
					MessageBox.Show("成功!");
			}
			catch(Exception ex)
			{
				MessageBox.Show(this,ex.Message);
			}
		}

		private void txtSerialCmd_TextChanged(object sender, System.EventArgs e)
		{
			this.txtSerialCmdLen.Text = (this.txtSerialCmd.Text.Length/2).ToString();
		}
	}


}
