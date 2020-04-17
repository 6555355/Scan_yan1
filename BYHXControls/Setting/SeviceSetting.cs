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
using System.Runtime.InteropServices;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for SeviceSetting.
	/// </summary>
	public class SeviceSetting : BYHXUserControl//System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label m_LabelHighSpeed;
		private System.Windows.Forms.CheckedListBox m_CheckedListBoxPass;
		private System.Windows.Forms.ComboBox m_ComboBoxHighSpeed;
		private System.Windows.Forms.Label m_LabelPass;
		private System.Windows.Forms.Label m_LabelColor;
		private System.Windows.Forms.CheckedListBox m_CheckedListBoxColor;
		private System.Windows.Forms.ComboBox m_ComboBoxMiddleSpeed;
		private System.Windows.Forms.Label m_LabelMiddleSpeed;
		private System.Windows.Forms.ComboBox m_ComboBoxLowSpeed;
		private System.Windows.Forms.Label m_LabelLowSpeed;
		private System.Windows.Forms.ComboBox m_ComboBoxBaseColor;
		private System.Windows.Forms.Label m_LabelBaseColor;
		private System.Windows.Forms.Label m_LabelCleanPoint;
		private System.Windows.Forms.Label m_LabelBaseStep;
		private System.Windows.Forms.TextBox m_TextBoxCleanPoint;
		private System.Windows.Forms.TextBox m_TextBoxBaseStep;
		private System.Windows.Forms.TreeView m_TreeViewPrinterProperty;
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox m_ComboBoxBit2Mode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBoxYStepColor1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxYStepColor2;
        private bool isDirty = false;
		private System.Windows.Forms.ComboBox m_ComboBoxSpeed;
        private System.Windows.Forms.Label labelSpeed;
		private SPrinterProperty m_sPrinterProperty;

        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }
		public SeviceSetting()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeviceSetting));
            this.m_LabelHighSpeed = new System.Windows.Forms.Label();
            this.m_CheckedListBoxPass = new System.Windows.Forms.CheckedListBox();
            this.m_TextBoxCleanPoint = new System.Windows.Forms.TextBox();
            this.m_ComboBoxHighSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelPass = new System.Windows.Forms.Label();
            this.m_LabelColor = new System.Windows.Forms.Label();
            this.m_CheckedListBoxColor = new System.Windows.Forms.CheckedListBox();
            this.m_ComboBoxMiddleSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelMiddleSpeed = new System.Windows.Forms.Label();
            this.m_ComboBoxBaseColor = new System.Windows.Forms.ComboBox();
            this.m_LabelBaseColor = new System.Windows.Forms.Label();
            this.m_ComboBoxLowSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelLowSpeed = new System.Windows.Forms.Label();
            this.m_LabelCleanPoint = new System.Windows.Forms.Label();
            this.m_LabelBaseStep = new System.Windows.Forms.Label();
            this.m_TextBoxBaseStep = new System.Windows.Forms.TextBox();
            this.m_TreeViewPrinterProperty = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_ComboBoxBit2Mode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxYStepColor1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxYStepColor2 = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_LabelHighSpeed
            // 
            this.m_LabelHighSpeed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelHighSpeed, "m_LabelHighSpeed");
            this.m_LabelHighSpeed.Name = "m_LabelHighSpeed";
            // 
            // m_CheckedListBoxPass
            // 
            resources.ApplyResources(this.m_CheckedListBoxPass, "m_CheckedListBoxPass");
            this.m_CheckedListBoxPass.Name = "m_CheckedListBoxPass";
            this.m_CheckedListBoxPass.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_CheckedListBoxColor_ItemCheck);
            // 
            // m_TextBoxCleanPoint
            // 
            resources.ApplyResources(this.m_TextBoxCleanPoint, "m_TextBoxCleanPoint");
            this.m_TextBoxCleanPoint.Name = "m_TextBoxCleanPoint";
            // 
            // m_ComboBoxHighSpeed
            // 
            this.m_ComboBoxHighSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxHighSpeed, "m_ComboBoxHighSpeed");
            this.m_ComboBoxHighSpeed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxHighSpeed.Items"),
            resources.GetString("m_ComboBoxHighSpeed.Items1"),
            resources.GetString("m_ComboBoxHighSpeed.Items2"),
            resources.GetString("m_ComboBoxHighSpeed.Items3"),
            resources.GetString("m_ComboBoxHighSpeed.Items4"),
            resources.GetString("m_ComboBoxHighSpeed.Items5"),
            resources.GetString("m_ComboBoxHighSpeed.Items6"),
            resources.GetString("m_ComboBoxHighSpeed.Items7")});
            this.m_ComboBoxHighSpeed.Name = "m_ComboBoxHighSpeed";
            this.m_ComboBoxHighSpeed.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelPass
            // 
            this.m_LabelPass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelPass, "m_LabelPass");
            this.m_LabelPass.Name = "m_LabelPass";
            // 
            // m_LabelColor
            // 
            this.m_LabelColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelColor, "m_LabelColor");
            this.m_LabelColor.Name = "m_LabelColor";
            // 
            // m_CheckedListBoxColor
            // 
            resources.ApplyResources(this.m_CheckedListBoxColor, "m_CheckedListBoxColor");
            this.m_CheckedListBoxColor.Name = "m_CheckedListBoxColor";
            this.m_CheckedListBoxColor.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_CheckedListBoxColor_ItemCheck);
            // 
            // m_ComboBoxMiddleSpeed
            // 
            this.m_ComboBoxMiddleSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxMiddleSpeed, "m_ComboBoxMiddleSpeed");
            this.m_ComboBoxMiddleSpeed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxMiddleSpeed.Items"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items1"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items2"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items3"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items4"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items5"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items6"),
            resources.GetString("m_ComboBoxMiddleSpeed.Items7")});
            this.m_ComboBoxMiddleSpeed.Name = "m_ComboBoxMiddleSpeed";
            this.m_ComboBoxMiddleSpeed.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelMiddleSpeed
            // 
            this.m_LabelMiddleSpeed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelMiddleSpeed, "m_LabelMiddleSpeed");
            this.m_LabelMiddleSpeed.Name = "m_LabelMiddleSpeed";
            // 
            // m_ComboBoxBaseColor
            // 
            this.m_ComboBoxBaseColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxBaseColor, "m_ComboBoxBaseColor");
            this.m_ComboBoxBaseColor.Name = "m_ComboBoxBaseColor";
            this.m_ComboBoxBaseColor.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelBaseColor
            // 
            this.m_LabelBaseColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelBaseColor, "m_LabelBaseColor");
            this.m_LabelBaseColor.Name = "m_LabelBaseColor";
            // 
            // m_ComboBoxLowSpeed
            // 
            this.m_ComboBoxLowSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxLowSpeed, "m_ComboBoxLowSpeed");
            this.m_ComboBoxLowSpeed.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxLowSpeed.Items"),
            resources.GetString("m_ComboBoxLowSpeed.Items1"),
            resources.GetString("m_ComboBoxLowSpeed.Items2"),
            resources.GetString("m_ComboBoxLowSpeed.Items3"),
            resources.GetString("m_ComboBoxLowSpeed.Items4"),
            resources.GetString("m_ComboBoxLowSpeed.Items5"),
            resources.GetString("m_ComboBoxLowSpeed.Items6"),
            resources.GetString("m_ComboBoxLowSpeed.Items7")});
            this.m_ComboBoxLowSpeed.Name = "m_ComboBoxLowSpeed";
            this.m_ComboBoxLowSpeed.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // m_LabelLowSpeed
            // 
            this.m_LabelLowSpeed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelLowSpeed, "m_LabelLowSpeed");
            this.m_LabelLowSpeed.Name = "m_LabelLowSpeed";
            // 
            // m_LabelCleanPoint
            // 
            this.m_LabelCleanPoint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelCleanPoint, "m_LabelCleanPoint");
            this.m_LabelCleanPoint.Name = "m_LabelCleanPoint";
            // 
            // m_LabelBaseStep
            // 
            this.m_LabelBaseStep.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelBaseStep, "m_LabelBaseStep");
            this.m_LabelBaseStep.Name = "m_LabelBaseStep";
            // 
            // m_TextBoxBaseStep
            // 
            resources.ApplyResources(this.m_TextBoxBaseStep, "m_TextBoxBaseStep");
            this.m_TextBoxBaseStep.Name = "m_TextBoxBaseStep";
            // 
            // m_TreeViewPrinterProperty
            // 
            resources.ApplyResources(this.m_TreeViewPrinterProperty, "m_TreeViewPrinterProperty");
            this.m_TreeViewPrinterProperty.ItemHeight = 18;
            this.m_TreeViewPrinterProperty.Name = "m_TreeViewPrinterProperty";
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // m_ComboBoxBit2Mode
            // 
            this.m_ComboBoxBit2Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxBit2Mode, "m_ComboBoxBit2Mode");
            this.m_ComboBoxBit2Mode.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxBit2Mode.Items"),
            resources.GetString("m_ComboBoxBit2Mode.Items1"),
            resources.GetString("m_ComboBoxBit2Mode.Items2")});
            this.m_ComboBoxBit2Mode.Name = "m_ComboBoxBit2Mode";
            this.m_ComboBoxBit2Mode.SelectedIndexChanged += new System.EventHandler(this.m_CheckBox_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Name = "label4";
            // 
            // comboBoxYStepColor1
            // 
            this.comboBoxYStepColor1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxYStepColor1, "comboBoxYStepColor1");
            this.comboBoxYStepColor1.Name = "comboBoxYStepColor1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Name = "label5";
            // 
            // comboBoxYStepColor2
            // 
            this.comboBoxYStepColor2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxYStepColor2, "comboBoxYStepColor2");
            this.comboBoxYStepColor2.Name = "comboBoxYStepColor2";
            // 
            // m_ComboBoxSpeed
            // 
            this.m_ComboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.m_ComboBoxSpeed, "m_ComboBoxSpeed");
            this.m_ComboBoxSpeed.Name = "m_ComboBoxSpeed";
            // 
            // labelSpeed
            // 
            resources.ApplyResources(this.labelSpeed, "labelSpeed");
            this.labelSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelSpeed.Name = "labelSpeed";
            // 
            // SeviceSetting
            // 
            this.Controls.Add(this.m_ComboBoxSpeed);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxYStepColor1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxYStepColor2);
            this.Controls.Add(this.m_TreeViewPrinterProperty);
            this.Controls.Add(this.m_LabelHighSpeed);
            this.Controls.Add(this.m_CheckedListBoxPass);
            this.Controls.Add(this.m_TextBoxCleanPoint);
            this.Controls.Add(this.m_ComboBoxHighSpeed);
            this.Controls.Add(this.m_LabelPass);
            this.Controls.Add(this.m_LabelColor);
            this.Controls.Add(this.m_CheckedListBoxColor);
            this.Controls.Add(this.m_ComboBoxMiddleSpeed);
            this.Controls.Add(this.m_LabelMiddleSpeed);
            this.Controls.Add(this.m_ComboBoxBaseColor);
            this.Controls.Add(this.m_LabelBaseColor);
            this.Controls.Add(this.m_ComboBoxLowSpeed);
            this.Controls.Add(this.m_LabelLowSpeed);
            this.Controls.Add(this.m_LabelCleanPoint);
            this.Controls.Add(this.m_LabelBaseStep);
            this.Controls.Add(this.m_TextBoxBaseStep);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_ComboBoxBit2Mode);
            resources.ApplyResources(this, "$this");
            this.Name = "SeviceSetting";
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
            bool bSupperUser = (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser);
			m_sPrinterProperty = sp;
			m_CheckedListBoxColor.Items.Clear();
			int colornum = sp.GetRealColorNum();
			for(int i = 0;i <colornum; i++)
			{
				ColorEnum color = (ColorEnum)sp.eColorOrder[i];
				string cmode = ResString.GetEnumDisplayName(typeof(ColorEnum),color);
				m_CheckedListBoxColor.Items.Add(cmode);
				m_CheckedListBoxColor.SetItemChecked(i,true);
			}
			m_ComboBoxBaseColor.Items.Clear();
			for(int i = 0;i <colornum; i++)
			{
				ColorEnum color = (ColorEnum)sp.eColorOrder[i];
				string cmode = ResString.GetEnumDisplayName(typeof(ColorEnum),color);
				m_ComboBoxBaseColor.Items.Add(cmode);
				comboBoxYStepColor1.Items.Add(cmode);
				comboBoxYStepColor2.Items.Add(cmode);
			}
            m_CheckedListBoxPass.Visible =
            m_LabelPass.Visible = bSupperUser;

			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxBaseColor,0);
			UIPreference.SetSelectIndexAndClampWithMax(comboBoxYStepColor1,0);
			UIPreference.SetSelectIndexAndClampWithMax(comboBoxYStepColor2,0);

			m_TextBoxCleanPoint.Text = "0";
			m_TextBoxBaseStep.Text = sp.nStepPerHead.ToString();

            m_TextBoxCleanPoint.Visible =
            m_LabelBaseStep.Visible =m_TextBoxBaseStep.Visible = bSupperUser;

			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxHighSpeed,sp.eSpeedMap[0]);
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxMiddleSpeed,sp.eSpeedMap[1]);
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxLowSpeed,sp.eSpeedMap[2]);
            m_ComboBoxHighSpeed.Visible =
            m_ComboBoxMiddleSpeed.Visible =
            m_ComboBoxLowSpeed.Visible = bSupperUser; 

			ObjectContainer contain = new ObjectContainer();
			contain.Info = null;
			contain.Index = -1;
			contain.Object = sp;
			contain.ObjType = sp.GetType();
			m_TreeViewPrinterProperty.Tag = contain;

            m_TreeViewPrinterProperty.Nodes.Clear();
			PubFunc.AddNode(sp.GetType().Name, sp, m_TreeViewPrinterProperty.Nodes, null, null, -1);
            label1.Visible = m_TreeViewPrinterProperty.Visible = bSupperUser;

            this.comboBoxYStepColor1.Visible = this.label4.Visible = sp.EPSONLCD_DEFINED;
            this.comboBoxYStepColor2.Visible = this.label5.Visible = sp.EPSONLCD_DEFINED;

			m_ComboBoxSpeed.Items.Clear();
			foreach(SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
			{
				if(mode == SpeedEnum.CustomSpeed) 
				{
					if(!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
						continue;
				}
				string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum),mode);
				if(SPrinterProperty.IsEpson(sp.ePrinterHead))
					cmode = "VSD_"+ ((int)mode + 1).ToString();
				m_ComboBoxSpeed.Items.Add(cmode);
			}
            
            m_LabelHighSpeed.Visible =
            m_LabelMiddleSpeed.Visible =
            m_LabelLowSpeed.Visible =

            m_LabelCleanPoint.Visible=
            labelSpeed.Visible=m_ComboBoxSpeed.Visible = bSupperUser;
            //this.isDirty = false;
		}
        public void OnPrinterSettingChange(SPrinterSetting ss, CaliConfig cc)
		{
			int PassListNum = ss.sFrequencySetting.nPass;
			m_CheckedListBoxPass.Items.Clear();
			string spass =ResString.GetDisplayPass();
			for(int i = 0;i <PassListNum; i++)
			{
				string dispPass = (i+1).ToString() + " " + spass;
				m_CheckedListBoxPass.Items.Add(dispPass);
				m_CheckedListBoxPass.SetItemChecked(i,true);
			}
            //CoreInterface.GetSeviceSetting(ref sSeviceSet);
            //OnServiceSettingChange(sSeviceSet,cc);
            //this.isDirty = false;

		}
        public void OnServiceSettingChange(SSeviceSetting sSeviceSet, CaliConfig cc)
		{
            if (m_sPrinterProperty.EPSONLCD_DEFINED)
			{
				int i = 0;
				foreach(byte ce in m_sPrinterProperty.eColorOrder)
				{
					if(ce == cc.BaseColor)
						UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxBaseColor,i);

					if(ce == cc.YStepColor[0])
					{
						UIPreference.SetSelectIndexAndClampWithMax(comboBoxYStepColor1,i);
					}
					if(ce == cc.YStepColor[1])
					{
						UIPreference.SetSelectIndexAndClampWithMax(comboBoxYStepColor2,i);
					}
					i++;
				}
			}
			else
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxBaseColor,sSeviceSet.nCalibrationHeadIndex);
			if(sSeviceSet.Vsd2ToVsd3_ColorDeep>=1&&sSeviceSet.Vsd2ToVsd3_ColorDeep<=3)
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxBit2Mode,sSeviceSet.Vsd2ToVsd3_ColorDeep - 1);
			else
				UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxBit2Mode,0);

			uint mask = 0x1;
			for (int j=0; j< m_CheckedListBoxColor.Items.Count;j++)
			{
				if((sSeviceSet.unColorMask&mask) == 0)
					m_CheckedListBoxColor.SetItemChecked(j,true);
				else
					m_CheckedListBoxColor.SetItemChecked(j,false);
				mask<<=1;
			}
			mask = 0x1;
			for (int j=0; j< m_CheckedListBoxPass.Items.Count;j++)
			{
				if((sSeviceSet.unPassMask&mask) == 0)
					m_CheckedListBoxPass.SetItemChecked(j,true);
				else
					m_CheckedListBoxPass.SetItemChecked(j,false);
				mask<<=1;
			}
			UIPreference.SetSelectIndexAndClampWithMax(m_ComboBoxSpeed,(int)sSeviceSet.Vsd2ToVsd3);

		}
		public void OnGetServiceSetting(ref SSeviceSetting sSeviceSet)
		{
            if (!m_sPrinterProperty.EPSONLCD_DEFINED)
			sSeviceSet.nCalibrationHeadIndex = m_ComboBoxBaseColor.SelectedIndex;
			sSeviceSet.Vsd2ToVsd3_ColorDeep = (byte)(m_ComboBoxBit2Mode.SelectedIndex + 1);

			uint mask = 0x1;
			uint cur = 0;
			for (int j=0; j< m_CheckedListBoxColor.Items.Count;j++)
			{
				if(!m_CheckedListBoxColor.GetItemChecked(j))
					cur |= mask;
				mask<<=1;
			}
			sSeviceSet.unColorMask = cur;

			mask = 0x1;
			cur = 0;
			for (int j=0; j< m_CheckedListBoxPass.Items.Count;j++)
			{
				if(!m_CheckedListBoxPass.GetItemChecked(j))
					cur |= mask;
				mask<<=1;
			}
			sSeviceSet.unPassMask = cur;
			sSeviceSet.Vsd2ToVsd3 = (byte)m_ComboBoxSpeed.SelectedIndex;		

			sSeviceSet.nDirty = (this.isDirty)?(uint)1:(uint)0;
		}
		
		public void OnGetServiceSetting(ref CaliConfig cc)
		{
			cc.len = (byte)Marshal.SizeOf(typeof(CaliConfig));
			cc.version = 0x1;
			cc.BaseColor = (byte)(this.m_sPrinterProperty.eColorOrder[this.m_ComboBoxBaseColor.SelectedIndex]);			
			cc.YStepColor[0] = (byte)(this.m_sPrinterProperty.eColorOrder[this.comboBoxYStepColor1.SelectedIndex]);
			cc.YStepColor[1] = (byte)(this.m_sPrinterProperty.eColorOrder[this.comboBoxYStepColor2.SelectedIndex]);
		}
		
		public void OnGetProperty(ref SPrinterProperty sp, ref bool bChangeProperty)
		{
			bChangeProperty = false;
			if(sp.eSpeedMap[0] != (byte)m_ComboBoxHighSpeed.SelectedIndex)
			{
				bChangeProperty = true;
				sp.eSpeedMap[0] = (byte)m_ComboBoxHighSpeed.SelectedIndex;
			}
			if(sp.eSpeedMap[1] != (byte)m_ComboBoxMiddleSpeed.SelectedIndex)
			{
				bChangeProperty = true;
				sp.eSpeedMap[1] = (byte)m_ComboBoxMiddleSpeed.SelectedIndex;
			}
			if(sp.eSpeedMap[2] != (byte)m_ComboBoxLowSpeed.SelectedIndex)
			{
				bChangeProperty = true;
				sp.eSpeedMap[2] = (byte)m_ComboBoxLowSpeed.SelectedIndex;
			}
		}

        private void m_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isDirty = true;
        }

		private void m_CheckedListBoxColor_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			isDirty = true;
		}

		///
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}
}
