/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for PreferenceSetting.
	/// </summary>
	public class PreferenceSetting : BYHXUserControl//System.Windows.Forms.UserControl
	{
		private ArrayList m_LangArrayList;
		private bool m_bInitFinished;
        private bool isDirty = false;

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

		private System.Windows.Forms.ComboBox m_ComboBoxViewMode;
		private System.Windows.Forms.ComboBox m_ComboBoxLang;
		private System.Windows.Forms.ComboBox m_ComboBoxUnit;
		private System.Windows.Forms.CheckBox m_CheckBoxBeepBeforePrint;
		private System.Windows.Forms.CheckBox m_CheckBoxDelJobAfterPrint;
		private System.Windows.Forms.TextBox m_TextBoxWorkFolder;
		private System.Windows.Forms.Button m_ButtonWorkFolder;
		private System.Windows.Forms.CheckedListBox m_CheckedListBoxJobListHeader;
		private System.Windows.Forms.Label m_LabelWorkfolder;
		private System.Windows.Forms.Label m_labelColumn;
		private System.Windows.Forms.Label m_labelViewMode;
		private System.Windows.Forms.Label m_labelLang;
		private System.Windows.Forms.Label m_labelUnit;
		private System.Windows.Forms.Button buttonHotFolder;
		private System.Windows.Forms.TextBox textBoxHotFolder;
		private System.Windows.Forms.CheckBox checkBoxHotFolder;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBoxShowAttention;
        private CheckBox m_CheckBoxDelFileAfterPrint;
        private ComboBox comCancelButtonAction;
        private Label label1;
        private CheckBox checkBoxHoriMovDirReverse;
        private ComboBox comboBoxSkins;
        private Label label2;
        private CheckBox checkBoxShowMeasurBeforPrint;
        private CheckBox checkBoxVertMovDirReverse;
        private CheckBox checkBoxZMovDirReverse;
        private CheckBox checkBox_HotFolderPrintImmediately;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PreferenceSetting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            if (PubFunc.IsInDesignMode())
                return;
            string skinpath = Path.Combine(Application.StartupPath, CoreConst.SkinForlderName);
            DirectoryInfo skinDir = new DirectoryInfo(skinpath);
            DirectoryInfo[] skins = skinDir.GetDirectories();
            comboBoxSkins.Items.Clear();
            for (int i = 0; i < skins.Length; i++)
		    {
                comboBoxSkins.Items.Add(skins[i].Name);
            }
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferenceSetting));
            this.m_ComboBoxViewMode = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxLang = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxUnit = new System.Windows.Forms.ComboBox();
            this.m_CheckBoxBeepBeforePrint = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxDelJobAfterPrint = new System.Windows.Forms.CheckBox();
            this.m_TextBoxWorkFolder = new System.Windows.Forms.TextBox();
            this.m_ButtonWorkFolder = new System.Windows.Forms.Button();
            this.m_CheckedListBoxJobListHeader = new System.Windows.Forms.CheckedListBox();
            this.m_LabelWorkfolder = new System.Windows.Forms.Label();
            this.m_labelColumn = new System.Windows.Forms.Label();
            this.m_labelViewMode = new System.Windows.Forms.Label();
            this.m_labelLang = new System.Windows.Forms.Label();
            this.m_labelUnit = new System.Windows.Forms.Label();
            this.buttonHotFolder = new System.Windows.Forms.Button();
            this.textBoxHotFolder = new System.Windows.Forms.TextBox();
            this.checkBoxHotFolder = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxShowAttention = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxDelFileAfterPrint = new System.Windows.Forms.CheckBox();
            this.comCancelButtonAction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxHoriMovDirReverse = new System.Windows.Forms.CheckBox();
            this.comboBoxSkins = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxShowMeasurBeforPrint = new System.Windows.Forms.CheckBox();
            this.checkBoxVertMovDirReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxZMovDirReverse = new System.Windows.Forms.CheckBox();
            this.checkBox_HotFolderPrintImmediately = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ComboBoxViewMode
            // 
            this.m_ComboBoxViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxViewMode, "m_ComboBoxViewMode");
            this.m_ComboBoxViewMode.Name = "m_ComboBoxViewMode";
            this.m_ComboBoxViewMode.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ComboBoxLang
            // 
            this.m_ComboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxLang, "m_ComboBoxLang");
            this.m_ComboBoxLang.Name = "m_ComboBoxLang";
            this.m_ComboBoxLang.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxLang_SelectedIndexChanged);
            // 
            // m_ComboBoxUnit
            // 
            this.m_ComboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxUnit, "m_ComboBoxUnit");
            this.m_ComboBoxUnit.Name = "m_ComboBoxUnit";
            this.m_ComboBoxUnit.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxBeepBeforePrint
            // 
            resources.ApplyResources(this.m_CheckBoxBeepBeforePrint, "m_CheckBoxBeepBeforePrint");
            this.m_CheckBoxBeepBeforePrint.Name = "m_CheckBoxBeepBeforePrint";
            this.m_CheckBoxBeepBeforePrint.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_CheckBoxDelJobAfterPrint
            // 
            resources.ApplyResources(this.m_CheckBoxDelJobAfterPrint, "m_CheckBoxDelJobAfterPrint");
            this.m_CheckBoxDelJobAfterPrint.Name = "m_CheckBoxDelJobAfterPrint";
            this.m_CheckBoxDelJobAfterPrint.CheckedChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_TextBoxWorkFolder
            // 
            resources.ApplyResources(this.m_TextBoxWorkFolder, "m_TextBoxWorkFolder");
            this.m_TextBoxWorkFolder.Name = "m_TextBoxWorkFolder";
            this.m_TextBoxWorkFolder.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_ButtonWorkFolder
            // 
            resources.ApplyResources(this.m_ButtonWorkFolder, "m_ButtonWorkFolder");
            this.m_ButtonWorkFolder.Name = "m_ButtonWorkFolder";
            this.m_ButtonWorkFolder.Click += new System.EventHandler(this.m_ButtonWorkFolder_Click);
            // 
            // m_CheckedListBoxJobListHeader
            // 
            resources.ApplyResources(this.m_CheckedListBoxJobListHeader, "m_CheckedListBoxJobListHeader");
            this.m_CheckedListBoxJobListHeader.Name = "m_CheckedListBoxJobListHeader";
            this.m_CheckedListBoxJobListHeader.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_CheckedListBoxJobListHeader_ItemCheck);
            this.m_CheckedListBoxJobListHeader.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelWorkfolder
            // 
            resources.ApplyResources(this.m_LabelWorkfolder, "m_LabelWorkfolder");
            this.m_LabelWorkfolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_LabelWorkfolder.Name = "m_LabelWorkfolder";
            // 
            // m_labelColumn
            // 
            this.m_labelColumn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_labelColumn, "m_labelColumn");
            this.m_labelColumn.Name = "m_labelColumn";
            // 
            // m_labelViewMode
            // 
            resources.ApplyResources(this.m_labelViewMode, "m_labelViewMode");
            this.m_labelViewMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_labelViewMode.Name = "m_labelViewMode";
            // 
            // m_labelLang
            // 
            resources.ApplyResources(this.m_labelLang, "m_labelLang");
            this.m_labelLang.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_labelLang.Name = "m_labelLang";
            // 
            // m_labelUnit
            // 
            resources.ApplyResources(this.m_labelUnit, "m_labelUnit");
            this.m_labelUnit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_labelUnit.Name = "m_labelUnit";
            // 
            // buttonHotFolder
            // 
            resources.ApplyResources(this.buttonHotFolder, "buttonHotFolder");
            this.buttonHotFolder.Name = "buttonHotFolder";
            this.buttonHotFolder.Click += new System.EventHandler(this.buttonHotFolder_Click);
            // 
            // textBoxHotFolder
            // 
            resources.ApplyResources(this.textBoxHotFolder, "textBoxHotFolder");
            this.textBoxHotFolder.Name = "textBoxHotFolder";
            this.textBoxHotFolder.TextChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // checkBoxHotFolder
            // 
            this.checkBoxHotFolder.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.checkBoxHotFolder, "checkBoxHotFolder");
            this.checkBoxHotFolder.Name = "checkBoxHotFolder";
            this.checkBoxHotFolder.UseVisualStyleBackColor = false;
            this.checkBoxHotFolder.CheckedChanged += new System.EventHandler(this.checkBoxHotFolder_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxShowAttention
            // 
            resources.ApplyResources(this.checkBoxShowAttention, "checkBoxShowAttention");
            this.checkBoxShowAttention.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxShowAttention.Checked = true;
            this.checkBoxShowAttention.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowAttention.Name = "checkBoxShowAttention";
            this.checkBoxShowAttention.UseVisualStyleBackColor = false;
            // 
            // m_CheckBoxDelFileAfterPrint
            // 
            resources.ApplyResources(this.m_CheckBoxDelFileAfterPrint, "m_CheckBoxDelFileAfterPrint");
            this.m_CheckBoxDelFileAfterPrint.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxDelFileAfterPrint.Name = "m_CheckBoxDelFileAfterPrint";
            this.m_CheckBoxDelFileAfterPrint.UseVisualStyleBackColor = false;
            this.m_CheckBoxDelFileAfterPrint.CheckedChanged += new System.EventHandler(this.m_CheckBoxDelFileAfterPrint_CheckedChanged);
            // 
            // comCancelButtonAction
            // 
            this.comCancelButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comCancelButtonAction, "comCancelButtonAction");
            this.comCancelButtonAction.Name = "comCancelButtonAction";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Name = "label1";
            // 
            // checkBoxHoriMovDirReverse
            // 
            resources.ApplyResources(this.checkBoxHoriMovDirReverse, "checkBoxHoriMovDirReverse");
            this.checkBoxHoriMovDirReverse.Name = "checkBoxHoriMovDirReverse";
            // 
            // comboBoxSkins
            // 
            this.comboBoxSkins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxSkins, "comboBoxSkins");
            this.comboBoxSkins.Name = "comboBoxSkins";
            this.comboBoxSkins.SelectedIndexChanged += new System.EventHandler(this.comboBoxSkins_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Name = "label2";
            // 
            // checkBoxShowMeasurBeforPrint
            // 
            resources.ApplyResources(this.checkBoxShowMeasurBeforPrint, "checkBoxShowMeasurBeforPrint");
            this.checkBoxShowMeasurBeforPrint.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxShowMeasurBeforPrint.Checked = true;
            this.checkBoxShowMeasurBeforPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowMeasurBeforPrint.Name = "checkBoxShowMeasurBeforPrint";
            this.checkBoxShowMeasurBeforPrint.UseVisualStyleBackColor = false;
            // 
            // checkBoxVertMovDirReverse
            // 
            resources.ApplyResources(this.checkBoxVertMovDirReverse, "checkBoxVertMovDirReverse");
            this.checkBoxVertMovDirReverse.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxVertMovDirReverse.Name = "checkBoxVertMovDirReverse";
            this.checkBoxVertMovDirReverse.UseVisualStyleBackColor = false;
            // 
            // checkBoxZMovDirReverse
            // 
            resources.ApplyResources(this.checkBoxZMovDirReverse, "checkBoxZMovDirReverse");
            this.checkBoxZMovDirReverse.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxZMovDirReverse.Name = "checkBoxZMovDirReverse";
            this.checkBoxZMovDirReverse.UseVisualStyleBackColor = false;
            // 
            // checkBox_HotFolderPrintImmediately
            // 
            resources.ApplyResources(this.checkBox_HotFolderPrintImmediately, "checkBox_HotFolderPrintImmediately");
            this.checkBox_HotFolderPrintImmediately.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_HotFolderPrintImmediately.Name = "checkBox_HotFolderPrintImmediately";
            this.checkBox_HotFolderPrintImmediately.UseVisualStyleBackColor = false;
            // 
            // PreferenceSetting
            // 
            this.Controls.Add(this.checkBox_HotFolderPrintImmediately);
            this.Controls.Add(this.checkBoxZMovDirReverse);
            this.Controls.Add(this.checkBoxVertMovDirReverse);
            this.Controls.Add(this.checkBoxShowMeasurBeforPrint);
            this.Controls.Add(this.comboBoxSkins);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxHoriMovDirReverse);
            this.Controls.Add(this.comCancelButtonAction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_CheckBoxDelFileAfterPrint);
            this.Controls.Add(this.checkBoxShowAttention);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxHotFolder);
            this.Controls.Add(this.buttonHotFolder);
            this.Controls.Add(this.textBoxHotFolder);
            this.Controls.Add(this.m_LabelWorkfolder);
            this.Controls.Add(this.m_CheckedListBoxJobListHeader);
            this.Controls.Add(this.m_ButtonWorkFolder);
            this.Controls.Add(this.m_TextBoxWorkFolder);
            this.Controls.Add(this.m_CheckBoxDelJobAfterPrint);
            this.Controls.Add(this.m_CheckBoxBeepBeforePrint);
            this.Controls.Add(this.m_ComboBoxUnit);
            this.Controls.Add(this.m_ComboBoxLang);
            this.Controls.Add(this.m_ComboBoxViewMode);
            this.Controls.Add(this.m_labelColumn);
            this.Controls.Add(this.m_labelViewMode);
            this.Controls.Add(this.m_labelLang);
            this.Controls.Add(this.m_labelUnit);
            resources.ApplyResources(this, "$this");
            this.Name = "PreferenceSetting";
            this.Load += new System.EventHandler(this.PreferenceSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			if(SPrinterProperty.IsEpson(sp.ePrinterHead))
				this.m_CheckBoxBeepBeforePrint.Visible = false;
			if(sp.EPSONLCD_DEFINED)
			{
				m_labelLang.Visible = m_ComboBoxLang.Visible = false;
				m_CheckBoxDelJobAfterPrint.Location = m_labelUnit.Location;
				m_labelUnit.Location = m_labelLang.Location;
				m_ComboBoxUnit.Location = m_ComboBoxLang.Location;
			}
            this.checkBoxShowAttention.Visible = sp.IsDisplayForm();
            this.checkBoxShowMeasurBeforPrint.Visible = sp.IsZMeasurSupport;
			this.isDirty = false;
		}
		public void OnPreferenceChange( UIPreference up)
		{
            m_bInitFinished = false;
			Initialize();
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxViewMode,(int)up.ViewModeIndex);
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxUnit,(int)up.Unit);
			m_CheckBoxBeepBeforePrint.Checked = up.BeepBeforePrint;
			m_CheckBoxDelJobAfterPrint.Checked = up.DelJobAfterPrint;
			m_TextBoxWorkFolder.Text = up.WorkingFolder;
            checkBoxHoriMovDirReverse.Checked = up.ReverseHoriMoveDirection;
            checkBoxVertMovDirReverse.Checked = up.ReverseVertMoveDirection;
            checkBoxZMovDirReverse.Checked = up.ReverseZMoveDirection;
            checkBox_HotFolderPrintImmediately.Checked = up.HotFolderPrintImmediately;
			JobListColumnHeader[] myHeaderList = (JobListColumnHeader[])up.JobListHeaderList.Clone();
			
			JobListColumnHeader [] array = (JobListColumnHeader[]) Enum.GetValues(typeof(JobListColumnHeader));
			int count = array.Length;
			//int count = m_CheckedListBoxJobListHeader.Items.Count;
			for (int i=0;i< count;i++)
			{
				//string item = (string)m_CheckedListBoxJobListHeader.Items[i];
				bool found = false;
				for (int j=0; j< myHeaderList.Length;j++)
				{
					if(myHeaderList[j] == array[i])
					{
						found = true;
						break;
					}
				}
				if(!found)
				{
					m_CheckedListBoxJobListHeader.SetItemChecked(i,false);
				}
				else
				{
					m_CheckedListBoxJobListHeader.SetItemChecked(i,true);
				}
			}
			bool found1 = false;
			for (int i=0; i< m_LangArrayList.Count;i++)
			{
				if(up.LangIndex == (int)m_LangArrayList[i])
				{
					found1 = true;
					UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLang,i);
					break;
				}
			}
			if(!found1)
			{
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLang,0);
			}
			this.checkBoxHotFolder.Checked = up.EnableHotForlder;
			this.textBoxHotFolder.Text = up.HotForlderPath;
		    this.m_CheckBoxDelFileAfterPrint.Checked = up.DelFileAfterPrint;
			if(up.PrintedAreaLogConfig!= null && up.PrintedAreaLogConfig!= string.Empty)
			{
				string[] filters = up.PrintedAreaLogConfig.Split(new char[]{'|'});
				this.checkBox1.Checked = false;
				this.richTextBox1.Text = string.Empty;
				for(int i = 0; i < filters.Length; i++)
				{
					if(i==0)
					{
						this.checkBox1.Checked = (filters[i]!= string.Empty&&filters[i] == "*");			
					}
					if(filters[i]!= string.Empty &&filters[i] != "*")
					{
						this.richTextBox1.AppendText(filters[i].Trim()+Environment.NewLine);
					}
				}
			}
			else
			{
				this.checkBox1.Checked = false;
				this.richTextBox1.Text = string.Empty;
			}
			this.checkBoxShowAttention.Checked = up.bShowAttentionOnLoad;
		    checkBoxShowMeasurBeforPrint.Checked = up.bShowMeasureFormBeforPrint;
   			UIPreference.SetSelectIndexAndClampWithMax(comCancelButtonAction,(int) up.DefaultCanleType-1);

		    bool bfind = false;
		    for (int i = 0; i < comboBoxSkins.Items.Count; i++)
		    {
		        if (up.SkinName == comboBoxSkins.Items[i].ToString())
		        {
		            comboBoxSkins.SelectedIndex = i;
		            bfind = true;
		            break;
		        }
		    }
            if (!bfind)
                comboBoxSkins.SelectedIndex = 0;
            m_bInitFinished = true;
            this.isDirty = false;
		}
		public void OnGetPreference(ref UIPreference up)
		{
		    up.DefaultCanleType = (CanleType) (comCancelButtonAction.SelectedIndex + 1);
			up.ViewModeIndex = m_ComboBoxViewMode.SelectedIndex;
			up.Unit = (UILengthUnit)m_ComboBoxUnit.SelectedIndex;
			up.BeepBeforePrint = m_CheckBoxBeepBeforePrint.Checked; 
			up.DelJobAfterPrint = m_CheckBoxDelJobAfterPrint.Checked;
		    up.DelFileAfterPrint = m_CheckBoxDelFileAfterPrint.Checked;
            up.ReverseHoriMoveDirection = checkBoxHoriMovDirReverse.Checked;
		    up.ReverseVertMoveDirection = checkBoxVertMovDirReverse.Checked;
            up.ReverseZMoveDirection = checkBoxZMovDirReverse.Checked;
			up.WorkingFolder = m_TextBoxWorkFolder.Text;
		    up.HotFolderPrintImmediately = checkBox_HotFolderPrintImmediately.Checked;
			ArrayList list = new ArrayList();
			JobListColumnHeader [] array = (JobListColumnHeader[]) Enum.GetValues(typeof(JobListColumnHeader));
			int count = array.Length;
			//int count = m_CheckedListBoxJobListHeader.Items.Count;
			for (int i=0;i< count;i++)
			{
				//string item = (string)m_CheckedListBoxJobListHeader.Items[i];
				if(m_CheckedListBoxJobListHeader.GetItemChecked(i))
				{
					//JobListColumnHeader head = (JobListColumnHeader)Enum.Parse(typeof(JobListColumnHeader),(string)m_CheckedListBoxJobListHeader.Items[i]);
					list.Add(array[i]);
				}
			}
			//up.JobListHeaderList = new JobListColumnHeader[list.Count];
			up.JobListHeaderList = (JobListColumnHeader[])list.ToArray(typeof(JobListColumnHeader));
			up.LangIndex =(int) m_LangArrayList[m_ComboBoxLang.SelectedIndex];
			up.EnableHotForlder = this.checkBoxHotFolder.Checked;
			up.HotForlderPath = this.textBoxHotFolder.Text;

			string config = string.Empty;
			if(this.checkBox1.Checked)
				config += "*|";
			for(int i = 0; i < this.richTextBox1.Lines.Length; i++)
			{
				if(this.richTextBox1.Lines[i].Trim() != string.Empty)
					config += this.richTextBox1.Lines[i].Trim()+"|";
			}
			up.PrintedAreaLogConfig = config;
            up.bShowAttentionOnLoad = this.checkBoxShowAttention.Checked;
            up.bShowMeasureFormBeforPrint = this.checkBoxShowMeasurBeforPrint.Checked;
		    up.SkinName = comboBoxSkins.SelectedItem.ToString();
		}

		public void Initialize()
		{
			ArrayList langlist;
			UIPreference.InitializeLanguage(out langlist);
			m_LangArrayList = langlist;
			//m_ComboBoxLang
			m_ComboBoxLang.Items.Clear();
			for(int i = 0;i <langlist.Count; i++)
			{
				CultureInfo cInfo = new CultureInfo((int)langlist[i]);
				//m_ComboBoxLang.Items.Add(cInfo.DisplayName);
				if(Thread.CurrentThread.CurrentUICulture.LCID == 0x0409)
					m_ComboBoxLang.Items.Add(cInfo.EnglishName);
				else
					m_ComboBoxLang.Items.Add(cInfo.NativeName);
			}
			//m_ComboBoxPass.SelectedIndex = FoundMatchPass(passStr);

			//m_ComboBoxViewMode
			m_ComboBoxViewMode.Items.Clear();
			foreach(UIViewMode mode in Enum.GetValues(typeof(UIViewMode)))
			{
#if !XINLAN
				if(mode >= UIViewMode.NotifyIcon) continue;
#else
				if(mode == UIViewMode.NotifyIcon) continue;
#endif
				string cmode = ResString.GetEnumDisplayName(typeof(UIViewMode),mode);
				m_ComboBoxViewMode.Items.Add(cmode);
			}

			//m_ComboBoxUnit
			m_ComboBoxUnit.Items.Clear();
			foreach(UILengthUnit mode in Enum.GetValues(typeof(UILengthUnit)))
			{
			    if (mode == UILengthUnit.Null)
			        continue;
				string cmode = ResString.GetEnumDisplayName(typeof(UILengthUnit),mode);
				m_ComboBoxUnit.Items.Add(cmode);
			}
			//m_CheckedListBoxJobListHeader
			m_CheckedListBoxJobListHeader.Items.Clear();
			foreach(JobListColumnHeader mode in Enum.GetValues(typeof(JobListColumnHeader)))
			{
				string cmode = ResString.GetEnumDisplayName(typeof(JobListColumnHeader),mode);
				m_CheckedListBoxJobListHeader.Items.Add(cmode);
			}

            comCancelButtonAction.Items.Clear();
            foreach (CanleType mode in Enum.GetValues(typeof(CanleType)))
            {
                if (mode == CanleType.None)
                    continue;
                string cmode = ResString.GetEnumDisplayName(typeof(CanleType), mode);
                comCancelButtonAction.Items.Add(cmode);
            }
        }
		private void m_ComboBoxLang_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_bInitFinished)
			{
				string info = ResString.GetResString("Restart_Lang");
				MessageBox.Show(info,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.isDirty = true;
			}
		}

		private void m_CheckedListBoxJobListHeader_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if(e.Index ==0 && e.NewValue == CheckState.Unchecked)
			{
				e.NewValue = CheckState.Checked;
			}
            this.isDirty = true;
		}

		private void m_ButtonWorkFolder_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = m_TextBoxWorkFolder.Text;
			
			if(dlg.ShowDialog(this) == DialogResult.OK)
			{
				m_TextBoxWorkFolder.Text =  dlg.SelectedPath;
			}		

		}
        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
            this.m_CheckBoxDelFileAfterPrint.Enabled = this.m_CheckBoxDelJobAfterPrint.Checked;
        }

        private void PreferenceSetting_Load(object sender, EventArgs e)
        {
#if LIYUUSB
            this.m_labelColumn.Visible = false;
            this.m_CheckedListBoxJobListHeader.Visible = false;
            m_labelViewMode.Visible = false;
            m_ComboBoxViewMode.Visible = false;
            m_CheckBoxDelJobAfterPrint.Visible = false;
            m_labelLang.Top = m_ComboBoxLang.Top = m_labelColumn.Top;
            m_labelLang.Left = m_labelColumn.Left;
            m_ComboBoxLang.Left = m_labelLang.Right;
            m_labelUnit.Top = m_ComboBoxUnit.Top = m_CheckedListBoxJobListHeader.Top;
            m_labelUnit.Left = m_CheckedListBoxJobListHeader.Left;
            m_ComboBoxUnit.Left = m_labelUnit.Right;
#endif
#if !XINLAN
			this.textBoxHotFolder.Visible = this.checkBoxHotFolder.Visible = this.buttonHotFolder.Visible = false;		
#endif
        }

		private void buttonHotFolder_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.SelectedPath = this.textBoxHotFolder.Text;
			
			if(dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxHotFolder.Text =  dlg.SelectedPath;
			}		
		}

		private void checkBoxHotFolder_CheckedChanged(object sender, System.EventArgs e)
		{
			this.textBoxHotFolder.Enabled = this.buttonHotFolder.Enabled = this.checkBoxHotFolder.Checked;
			isDirty = true;
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.richTextBox1.Enabled = !this.checkBox1.Checked;
			isDirty = true;
		}

		private void richTextBox1_TextChanged(object sender, System.EventArgs e)
		{
//			string config = string.Empty;
//			for(int i = 0; i < this.richTextBox1.Lines.Length; i++)
//			{
//				if(this.richTextBox1.Lines[i].Trim() != string.Empty)
//					config += this.richTextBox1.Lines[i].Trim();
//			}
//			this.richTextBox1.Text = config;
		}

        private void m_CheckBoxDelFileAfterPrint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bInitFinished)
            {
                string info = ResString.GetResString("Restart_Skin");
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isDirty = true;
            } 

        }


	}
}
