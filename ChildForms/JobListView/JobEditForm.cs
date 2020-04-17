/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BYHXPrinterManager.JobListView
{
	/// <summary>
	/// Summary description for JobEditForm.
	/// </summary>
	public class JobEditForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label m_LabelCopy;
		private System.Windows.Forms.NumericUpDown m_NumericUpDownCopy;
		private System.Windows.Forms.Button m_ButtonOK;
		private System.Windows.Forms.CheckBox CheckBoxMirrorPrint;
		private System.Windows.Forms.CheckBox CheckBoxAlternatingprint;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private IPrinterChange m_iPrinterChange;

		public JobEditForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobEditForm));
            this.m_LabelCopy = new System.Windows.Forms.Label();
            this.m_NumericUpDownCopy = new System.Windows.Forms.NumericUpDown();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this.CheckBoxMirrorPrint = new System.Windows.Forms.CheckBox();
            this.CheckBoxAlternatingprint = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCopy)).BeginInit();
            this.SuspendLayout();
            // 
            // m_LabelCopy
            // 
            resources.ApplyResources(this.m_LabelCopy, "m_LabelCopy");
            this.m_LabelCopy.Name = "m_LabelCopy";
            // 
            // m_NumericUpDownCopy
            // 
            resources.ApplyResources(this.m_NumericUpDownCopy, "m_NumericUpDownCopy");
            this.m_NumericUpDownCopy.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDownCopy.Name = "m_NumericUpDownCopy";
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            // 
            // CheckBoxMirrorPrint
            // 
            resources.ApplyResources(this.CheckBoxMirrorPrint, "CheckBoxMirrorPrint");
            this.CheckBoxMirrorPrint.Name = "CheckBoxMirrorPrint";
            // 
            // CheckBoxAlternatingprint
            // 
            resources.ApplyResources(this.CheckBoxAlternatingprint, "CheckBoxAlternatingprint");
            this.CheckBoxAlternatingprint.Name = "CheckBoxAlternatingprint";
            // 
            // JobEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.CheckBoxAlternatingprint);
            this.Controls.Add(this.CheckBoxMirrorPrint);
            this.Controls.Add(this.m_ButtonOK);
            this.Controls.Add(this.m_NumericUpDownCopy);
            this.Controls.Add(this.m_LabelCopy);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JobEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDownCopy)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}

		public int Copies
		{
			get
			{
				return Decimal.ToInt32(m_NumericUpDownCopy.Value);
			}
			set
			{
				 UIPreference.SetValueAndClampWithMinMax(m_NumericUpDownCopy,value);
			}
		}
		SJobSetting_UI sJobSetting = new SJobSetting_UI();
		public SJobSetting_UI JobSetting
		{
			get
			{
				sJobSetting.bReversePrint = CheckBoxMirrorPrint.Checked;
				sJobSetting.bAlternatingPrint = CheckBoxAlternatingprint.Checked;
				return sJobSetting;
			}
			set
			{
				CheckBoxMirrorPrint.Checked = sJobSetting.bReversePrint;
				CheckBoxAlternatingprint.Checked = sJobSetting.bAlternatingPrint;
			}
		}
	}
}
