using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BYHXPrinterManager.Setting;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// MeasureQuestionForm 的摘要说明。
	/// </summary>
    public class MeasureQuestionForm : ByhxBaseChildForm
	{
		private System.Windows.Forms.Label frmMessage;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.Button buttonNo;
		private System.ComponentModel.IContainer components;
		private BYHXPrinterManager.Setting.ZAixsSetting zAixsSetting1;
        private CheckBox checkBoxShowAttention;

		private IPrinterChange mIPrinterChange;
	    private bool _isMeasureBeforePrint;
        public MeasureQuestionForm(IPrinterChange ipc, bool isMeasureBeforePrint = false)
	    {
	        //
	        // Windows 窗体设计器支持所必需的
	        //
	        InitializeComponent();

	        //
	        // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
	        //
	        this.Text = ResString.GetProductName();
	        this.frmMessage.Image = SystemIcons.Question.ToBitmap();
	        this.frmMessage.Text = SErrorCode.GetResString("COMCommand_Abort_NotifyMeasure");
	        mIPrinterChange = ipc;
	        AllParam allp = mIPrinterChange.GetAllParam();
            checkBoxShowAttention.Visible = SPrinterProperty.IsFloraUv() && allp.PrinterProperty.IsZMeasurSupport;

            _isMeasureBeforePrint = zAixsSetting1.IsMeasureBeforePrint = isMeasureBeforePrint;

            zAixsSetting1.CustomButtonClicked += new EventHandler<Setting.csEventArgs>(zAixsSetting1_CostomButtonClicked);
	    }

        void zAixsSetting1_CostomButtonClicked(object sender, Setting.csEventArgs e)
        {
            this.DialogResult = e.Dr;
            //switch (e.Case)
            //{
            //    case CsButtonCase.MeasureBeforePrint:
            //    {
            //        break;
            //    }
            //    case CsButtonCase.PrintDirectly:
            //    {
            //        this.DialogResult = DialogResult.OK;
            //        break;
            //    }
            //    case CsButtonCase.CancelJob:
            //    {
            //        this.DialogResult = DialogResult.OK;
            //        break;
            //    }
            //    case CsButtonCase.None:
            //    {
            //        break;
            //    }
            //    //default:
            //        //throw new ArgumentOutOfRangeException();
            //}
        }

	    /// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureQuestionForm));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.buttonNo = new System.Windows.Forms.Button();
            this.frmMessage = new System.Windows.Forms.Label();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.zAixsSetting1 = new BYHXPrinterManager.Setting.ZAixsSetting();
            this.checkBoxShowAttention = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonNo
            // 
            resources.ApplyResources(this.buttonNo, "buttonNo");
            this.buttonNo.Name = "buttonNo";
            this.m_ToolTip.SetToolTip(this.buttonNo, resources.GetString("buttonNo.ToolTip"));
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // frmMessage
            // 
            resources.ApplyResources(this.frmMessage, "frmMessage");
            this.frmMessage.Name = "frmMessage";
            this.m_ToolTip.SetToolTip(this.frmMessage, resources.GetString("frmMessage.ToolTip"));
            // 
            // zAixsSetting1
            // 
            resources.ApplyResources(this.zAixsSetting1, "zAixsSetting1");
            this.zAixsSetting1.Divider = false;
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.zAixsSetting1.GradientColors = style1;
            this.zAixsSetting1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.zAixsSetting1.GrouperTitleStyle = null;
            this.zAixsSetting1.Name = "zAixsSetting1";
            this.m_ToolTip.SetToolTip(this.zAixsSetting1, resources.GetString("zAixsSetting1.ToolTip"));
            // 
            // checkBoxShowAttention
            // 
            resources.ApplyResources(this.checkBoxShowAttention, "checkBoxShowAttention");
            this.checkBoxShowAttention.Name = "checkBoxShowAttention";
            this.m_ToolTip.SetToolTip(this.checkBoxShowAttention, resources.GetString("checkBoxShowAttention.ToolTip"));
            // 
            // MeasureQuestionForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.checkBoxShowAttention);
            this.Controls.Add(this.zAixsSetting1);
            this.Controls.Add(this.frmMessage);
            this.Controls.Add(this.buttonNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeasureQuestionForm";
            this.m_ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	    /// <summary>
	    /// 是否进行过测高
	    /// </summary>
	    public bool HasMeasured
	    {
	        get { return zAixsSetting1.HasMeasured; }
	    } // 

		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
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
        public void OnGetPreference(ref UIPreference up)
        {
            if(checkBoxShowAttention.Visible)
                up.bShowMeasureFormBeforPrint =!this.checkBoxShowAttention.Checked;
        }
		private void  OnUnitChange(UILengthUnit newUnit)
		{
//			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDownMesureXCoor);
//			UIPreference.OnFloatNumericUpDownUnitChanged(newUnit,m_CurrentUnit,this.numericUpDown7);
//
//			string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
//			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownMesureXCoor, this.m_ToolTip);
//			UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown7, this.m_ToolTip);
		}


		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			SZSetting zseting = ss.ZSetting;
//			this.numericUpDownMesureXCoor.Value = new decimal( UIPreference.ToDisplayLength(m_CurrentUnit,zseting.fMesureXCoor));
//			this.numericUpDown7.Value = new decimal( UIPreference.ToDisplayLength(m_CurrentUnit,zseting.fMesureYCoor));
			this.zAixsSetting1.OnPrinterSettingChange(ss);
		}

		public void OnGetPrinterSetting(ref SPrinterSetting ss,ref bool bzsettingchang)
		{
//			float temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDownMesureXCoor.Value));
//			if(temp != ss.ZSetting.fMesureXCoor)
//			{
//				ss.ZSetting.fMesureXCoor		=	temp;
//				bzsettingchang = true;
//			}
//			temp = UIPreference.ToInchLength(m_CurrentUnit,Decimal.ToSingle(this.numericUpDown7.Value));
//			if(temp != ss.ZSetting.fMesureYCoor)
//			{
//				ss.ZSetting.fMesureYCoor		=	temp;
//				bzsettingchang = true;
//			}		
            this.zAixsSetting1.OnGetPrinterSetting(ref ss, ref bzsettingchang);
		}

		private void buttonNo_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private bool bMeasuring = false;
		private bool bSelfRaised =false;
		public void SetPrinterStatusChanged(JetStatusEnum status)
		{
			PrinterOperate po = PrinterOperate.UpdateByPrinterStatus(status);
			if(bSelfRaised && status == JetStatusEnum.Measuring)
			{
				bMeasuring = true;
				bSelfRaised = false;
			}
			if(bMeasuring && status == JetStatusEnum.Ready)
			{
				bSelfRaised = bMeasuring = false;
			}
//			this.buttonCancel.Enabled = po.CanMoveStop;
//			this.buttonYes.Enabled = po.CanMoveLeft && po.CanMoveRight; 
			this.zAixsSetting1.SetPrinterStatusChanged(status);
		}
	}
}
