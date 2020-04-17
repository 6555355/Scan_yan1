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
using System.Diagnostics;
using System.Drawing.Drawing2D;

using BYHXPrinterManager.JobListView;
namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// Summary description for ToolbarSetting.
	/// </summary>
	public class ToolbarSetting_1 : GradientControls.CrystalPanel //System.Windows.Forms.UserControl
	{
		private JetStatusEnum  m_curStatus = JetStatusEnum.Ready;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Inch;
		private IPrinterChange m_iPrinterChange;
		private bool m_bInitControl = false;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownStep;
		private System.Windows.Forms.Label m_LabelOrigin;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownOrigin;
		private System.Windows.Forms.CheckBox m_CheckBoxUsePrinterSetting;
		private System.Windows.Forms.CheckBox m_CheckBoxBidirection;
		private System.Windows.Forms.ComboBox m_ComboBoxPass;
		private System.Windows.Forms.ComboBox m_ComboBoxSpeed;
		private System.Windows.Forms.Label m_LabelStep;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownOriginY;
		private System.Windows.Forms.Label m_LabelOriginY;
        private TableLayoutPanel tableLayoutPanel1;
        private Label m_LabelPass;
        private Label m_LabelSpeed;
		private System.ComponentModel.IContainer components;

        public ToolbarSetting_1()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			InitComboBoxPass();
#if LIYUUSB
			m_CheckBoxUsePrinterSetting.Visible = false;
            //m_LabelStep.Visible = m_NumericUpDownStep.Visible = false;
#endif
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolbarSetting_1));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_NumericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.m_LabelOrigin = new System.Windows.Forms.Label();
            this.m_NumericUpDownOrigin = new System.Windows.Forms.NumericUpDown();
            this.m_CheckBoxUsePrinterSetting = new System.Windows.Forms.CheckBox();
            this.m_CheckBoxBidirection = new System.Windows.Forms.CheckBox();
            this.m_ComboBoxPass = new System.Windows.Forms.ComboBox();
            this.m_ComboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.m_LabelStep = new System.Windows.Forms.Label();
            this.m_NumericUpDownOriginY = new System.Windows.Forms.NumericUpDown();
            this.m_LabelOriginY = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_LabelSpeed = new System.Windows.Forms.Label();
            this.m_LabelPass = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOriginY)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_NumericUpDownStep
            // 
            resources.ApplyResources(this.m_NumericUpDownStep, "m_NumericUpDownStep");
            this.m_NumericUpDownStep.InterceptArrowKeys = false;
            this.m_NumericUpDownStep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDownStep.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.m_NumericUpDownStep.Name = "m_NumericUpDownStep";
            this.m_NumericUpDownStep.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownStep.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownStep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            // 
            // m_LabelOrigin
            // 
            resources.ApplyResources(this.m_LabelOrigin, "m_LabelOrigin");
            this.m_LabelOrigin.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelOrigin.Name = "m_LabelOrigin";
            // 
            // m_NumericUpDownOrigin
            // 
            resources.ApplyResources(this.m_NumericUpDownOrigin, "m_NumericUpDownOrigin");
            this.m_NumericUpDownOrigin.InterceptArrowKeys = false;
            this.m_NumericUpDownOrigin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownOrigin.Name = "m_NumericUpDownOrigin";
            this.m_NumericUpDownOrigin.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOrigin.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOrigin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            // 
            // m_CheckBoxUsePrinterSetting
            // 
            resources.ApplyResources(this.m_CheckBoxUsePrinterSetting, "m_CheckBoxUsePrinterSetting");
            this.m_CheckBoxUsePrinterSetting.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxUsePrinterSetting.Checked = true;
            this.m_CheckBoxUsePrinterSetting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_CheckBoxUsePrinterSetting.Name = "m_CheckBoxUsePrinterSetting";
            this.m_CheckBoxUsePrinterSetting.UseVisualStyleBackColor = false;
            this.m_CheckBoxUsePrinterSetting.Click += new System.EventHandler(this.m_CheckBoxUsePrinterSetting_Click);
            this.m_CheckBoxUsePrinterSetting.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // m_CheckBoxBidirection
            // 
            resources.ApplyResources(this.m_CheckBoxBidirection, "m_CheckBoxBidirection");
            this.m_CheckBoxBidirection.BackColor = System.Drawing.Color.Transparent;
            this.m_CheckBoxBidirection.Checked = true;
            this.m_CheckBoxBidirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_CheckBoxBidirection.Name = "m_CheckBoxBidirection";
            this.m_CheckBoxBidirection.UseVisualStyleBackColor = false;
            this.m_CheckBoxBidirection.CheckedChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            // 
            // m_ComboBoxPass
            // 
            resources.ApplyResources(this.m_ComboBoxPass, "m_ComboBoxPass");
            this.m_ComboBoxPass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxPass.Items.AddRange(new object[] {
            resources.GetString("m_ComboBoxPass.Items"),
            resources.GetString("m_ComboBoxPass.Items1"),
            resources.GetString("m_ComboBoxPass.Items2"),
            resources.GetString("m_ComboBoxPass.Items3"),
            resources.GetString("m_ComboBoxPass.Items4"),
            resources.GetString("m_ComboBoxPass.Items5"),
            resources.GetString("m_ComboBoxPass.Items6"),
            resources.GetString("m_ComboBoxPass.Items7"),
            resources.GetString("m_ComboBoxPass.Items8"),
            resources.GetString("m_ComboBoxPass.Items9"),
            resources.GetString("m_ComboBoxPass.Items10")});
            this.m_ComboBoxPass.Name = "m_ComboBoxPass";
            this.m_ComboBoxPass.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxPass_SelectedIndexChanged);
            this.m_ComboBoxPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxPass_KeyDown);
            // 
            // m_ComboBoxSpeed
            // 
            resources.ApplyResources(this.m_ComboBoxSpeed, "m_ComboBoxSpeed");
            this.m_ComboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ComboBoxSpeed.Name = "m_ComboBoxSpeed";
            this.m_ComboBoxSpeed.SelectedIndexChanged += new System.EventHandler(this.m_ComboBoxSpeed_SelectedIndexChanged);
            this.m_ComboBoxSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ComboBoxSpeed_KeyDown);
            // 
            // m_LabelStep
            // 
            resources.ApplyResources(this.m_LabelStep, "m_LabelStep");
            this.m_LabelStep.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelStep.Name = "m_LabelStep";
            // 
            // m_NumericUpDownOriginY
            // 
            resources.ApplyResources(this.m_NumericUpDownOriginY, "m_NumericUpDownOriginY");
            this.m_NumericUpDownOriginY.InterceptArrowKeys = false;
            this.m_NumericUpDownOriginY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownOriginY.Name = "m_NumericUpDownOriginY";
            this.m_NumericUpDownOriginY.ValueChanged += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOriginY.Leave += new System.EventHandler(this.m_AnyControl_ValueChanged);
            this.m_NumericUpDownOriginY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_NumericUpDownOrigin_KeyDown);
            // 
            // m_LabelOriginY
            // 
            resources.ApplyResources(this.m_LabelOriginY, "m_LabelOriginY");
            this.m_LabelOriginY.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelOriginY.Name = "m_LabelOriginY";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.Controls.Add(this.m_LabelSpeed, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_LabelPass, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_LabelOrigin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_NumericUpDownStep, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_CheckBoxBidirection, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_NumericUpDownOrigin, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_ComboBoxSpeed, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_ComboBoxPass, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_LabelOriginY, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_NumericUpDownOriginY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_LabelStep, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_CheckBoxUsePrinterSetting, 1, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // m_LabelSpeed
            // 
            resources.ApplyResources(this.m_LabelSpeed, "m_LabelSpeed");
            this.m_LabelSpeed.Name = "m_LabelSpeed";
            // 
            // m_LabelPass
            // 
            resources.ApplyResources(this.m_LabelPass, "m_LabelPass");
            this.m_LabelPass.Name = "m_LabelPass";
            // 
            // ToolbarSetting_1
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this, "$this");
            this.Name = "ToolbarSetting_1";
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownOriginY)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			m_curStatus = status;
			bool bEnabled = (m_curStatus == JetStatusEnum.Ready);


			m_CheckBoxUsePrinterSetting.Enabled = bEnabled;
			m_NumericUpDownOrigin.Enabled = bEnabled;
			m_NumericUpDownOriginY.Enabled = bEnabled;
			m_NumericUpDownStep.Enabled = true;

			bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
			m_CheckBoxBidirection.Enabled = bEnabled;

		}
		public void OnPrinterPropertyChange( SPrinterProperty sp)
		{
			m_bInitControl = true;
			m_NumericUpDownOrigin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
			m_NumericUpDownOrigin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperWidth));
			m_NumericUpDownOriginY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
			m_NumericUpDownOriginY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,sp.fMaxPaperHeight));


			//m_ComboBoxUnit
			m_ComboBoxSpeed.Items.Clear();
			foreach(SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
			{
				if(mode == SpeedEnum.CustomSpeed) 
				{
					if(!PubFunc.IsCustomSpeedDisp(sp.ePrinterHead))
						continue;
				}
				string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum),mode);
				m_ComboBoxSpeed.Items.Add(cmode);
			}

			if(sp.nMediaType == 0)
			{
				m_LabelOriginY.Visible = false;
				m_NumericUpDownOriginY.Visible = false;
			}
			else
			{
				m_LabelOriginY.Visible = true;
				m_NumericUpDownOriginY.Visible = true;
			}

			m_bInitControl = false;
		}
		private void InitComboBoxPass()
		{
			string passStr = (string)m_ComboBoxPass.SelectedItem;
			m_ComboBoxPass.Items.Clear();
#if false
			int PassListNum;
			int [] PassList;
			sp.GetPassListNumber(out PassListNum,out PassList);
			string spass = ResString.GetDisplayPass();
			for(int i = 0;i <PassListNum; i++)
			{
				int passNum = PassList[i];
				string dispPass = PassList[i].ToString() + " " + spass;
				m_ComboBoxPass.Items.Add(dispPass);
			}
			m_ComboBoxPass.SelectedIndex = FoundMatchPass(passStr);
#else
			string sPass = ResString.GetDisplayPass();
			for (int i=0; i< CoreConst.MAX_PASS_NUM;i++)
			{
				//int passNum = PassList[i];
				string dispPass = (i+1).ToString() + " " + sPass;
				m_ComboBoxPass.Items.Add(dispPass);
			}
#endif
		}
		private int FoundMatchPass(string dispPass)
		{
			for (int i = 0; i< m_ComboBoxPass.Items.Count;i++)
			{
				if(string.Compare((string)m_ComboBoxPass.Items[i] , dispPass)==0)
					return i;
			}
			return -1;
		}
		public void ClampWithMinMax(Decimal min,Decimal max,ref Decimal cur)
		{
			if(cur<min)
				cur = min;
			if(cur>max)
				cur = max;
		}

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_bInitControl = true;

			m_NumericUpDownOrigin.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fLeftMargin));
			m_NumericUpDownOrigin.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,(ss.sBaseSetting.fLeftMargin + ss.sBaseSetting.fPaperWidth)));

			m_CheckBoxUsePrinterSetting.Checked		=	ss.sFrequencySetting.bUsePrinterSetting == 0;
			m_CheckBoxBidirection.Checked			=	ss.sFrequencySetting.nBidirection != 0;

			if(m_ComboBoxSpeed.Items.Count <= (int)ss.sFrequencySetting.nSpeed)
				m_ComboBoxSpeed.SelectedIndex		=	m_ComboBoxSpeed.Items.Count-1;
			else
				m_ComboBoxSpeed.SelectedIndex		=	(int)ss.sFrequencySetting.nSpeed;
			m_ComboBoxPass.SelectedIndex		=	(int)FoundMatchPass(ss.sFrequencySetting.nPass.ToString()+" "+ResString.GetDisplayPass());

			Decimal curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sFrequencySetting.fXOrigin)); 
			ClampWithMinMax(m_NumericUpDownOrigin.Minimum,m_NumericUpDownOrigin.Maximum,ref curvalue);
			m_NumericUpDownOrigin.Value		=	curvalue;

			curvalue = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,ss.sBaseSetting.fYOrigin)); 
			ClampWithMinMax(m_NumericUpDownOriginY.Minimum,m_NumericUpDownOriginY.Maximum,ref curvalue);
			m_NumericUpDownOriginY.Value		=	curvalue;

			m_NumericUpDownStep.Value		=	ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1];

			bool bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
			m_CheckBoxBidirection.Enabled = bEnabled;
			m_bInitControl = false;
		}
		public void OnGetPrinterSetting(ref SPrinterSetting ss)
		{
			ss.sFrequencySetting.bUsePrinterSetting =	m_CheckBoxUsePrinterSetting.Checked?0:1	;
			ss.sFrequencySetting.nBidirection		= (byte)( m_CheckBoxBidirection.Checked?1:0);

			ss.sFrequencySetting.nSpeed				=	(SpeedEnum)m_ComboBoxSpeed.SelectedIndex;		

			string PassString = m_ComboBoxPass.Text;
			string[] split = PassString.Split(new char[] {' '});
			Debug.Assert(split != null && split.Length == 2);
			ss.sFrequencySetting.nPass = Convert.ToByte(split[0]);
																											

			ss.sFrequencySetting.fXOrigin			=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownOrigin.Value));
			ss.sBaseSetting.fYOrigin			=	UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(m_NumericUpDownOriginY.Value));
			ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1]				=	Decimal.ToInt32(m_NumericUpDownStep.Value);	
		}

		public void OnPreferenceChange( UIPreference up)
		{
			m_bInitControl = true;
			if(m_CurrentUnit != up.Unit)
			{
				OnUnitChange(up.Unit);
				m_CurrentUnit = up.Unit;
			}
			m_bInitControl = false;
		}
		private void  OnUnitChange(UILengthUnit newUnit)
		{
			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownOrigin);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownOrigin,this.m_ToolTip);

			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,m_NumericUpDownOriginY);
			UIPreference.NumericUpDownToolTip(newUnit.ToString(),this.m_NumericUpDownOriginY,this.m_ToolTip);

		}
		public void OnPrinterStatusChanged(JetStatusEnum status)
		{
		}
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}

		private void m_AnyControl_ValueChanged(object sender, System.EventArgs e)
		{
			if(m_bInitControl == false)
				m_iPrinterChange.NotifyUIParamChanged();
		}

		private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_ComboBoxPass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int PassListNum;
			int [] PassList;
			m_iPrinterChange.GetAllParam().PrinterProperty.GetPassListNumber(out PassListNum,out PassList);
			int [] PassStepArray = m_iPrinterChange.GetAllParam().PrinterSetting.sCalibrationSetting.nPassStepArray;
			//this.m_NumericUpDownStep.Value = PassStepArray[PassList[m_ComboBoxPass.SelectedIndex]-1];
			this.m_NumericUpDownStep.Value = PassStepArray[m_ComboBoxPass.SelectedIndex];

			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_CheckBoxUsePrinterSetting_Click(object sender, System.EventArgs e)
		{
			bool bEnabled = !m_CheckBoxUsePrinterSetting.Checked && (m_curStatus == JetStatusEnum.Ready);
			m_ComboBoxSpeed.Enabled = bEnabled;
			m_ComboBoxPass.Enabled = bEnabled;
			m_CheckBoxBidirection.Enabled = bEnabled;
			m_AnyControl_ValueChanged(sender,e);
		}

		private void m_ComboBoxPass_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
		}

		private void m_ComboBoxSpeed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
		
		}

		private void m_NumericUpDownOrigin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Up || e.KeyData == Keys.Down ||
				e.KeyData == Keys.Left ||e.KeyData == Keys.Right)
			{
				e.Handled = true;
				return;
			}
		
		}

		///
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///
		/////////////////////////////////////////////////////////////////////////////////////////////////////////////


	}
}
