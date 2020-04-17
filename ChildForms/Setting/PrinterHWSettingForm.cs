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
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using  System.Diagnostics;

using BYHXPrinterManager.GradientControls;
namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for PrinterHWSetting.
	/// </summary>
	public class PrinterHWSettingForm : System.Windows.Forms.Form
	{
        private BYHXPrinterManager.Setting.PrinterHWSetting printerHWSetting1;
		private System.ComponentModel.IContainer components;

        public PrinterHWSettingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if LIYUUSB
            //this.AutoSize = this.printerHWSetting1.AutoSize = true;
            //this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //this.printerHWSetting1.AutoSizeMode = AutoSizeMode.GrowOnly;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.printerHWSetting1 = new BYHXPrinterManager.Setting.PrinterHWSetting();
            this.SuspendLayout();
            // 
            // printerHWSetting1
            // 
            this.printerHWSetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printerHWSetting1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.printerHWSetting1.GrouperTitleStyle = null;
            this.printerHWSetting1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.printerHWSetting1.Location = new System.Drawing.Point(0, 0);
            this.printerHWSetting1.Name = "printerHWSetting1";
            this.printerHWSetting1.Size = new System.Drawing.Size(721, 460);
            this.printerHWSetting1.TabIndex = 0;
            this.printerHWSetting1.OKButtonClicked += new System.EventHandler(this.printerHWSetting1_OKButtonClicked);
            // 
            // PrinterHWSettingForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(721, 460);
            this.Controls.Add(this.printerHWSetting1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrinterHWSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PrinterHWSetting";
            this.Load += new System.EventHandler(this.PrinterHWSettingForm_Load);
            this.ResumeLayout(false);

		}
		#endregion

		public bool OnPrinterPropertyChange( SPrinterProperty sp)
		{
           return this.printerHWSetting1.OnPrinterPropertyChange(sp);
		}

		public void OnPreferenceChange( UIPreference up)
		{
            this.printerHWSetting1.OnPreferenceChange(up);
		}
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            this.printerHWSetting1.OnPrinterSettingChange(ss);
        }

        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            this.printerHWSetting1.OnGetPrinterSetting(ref ss);
        }

        public void OnGetProperty(ref SPrinterProperty sp, ref bool bChangeProperty)
        {
            this.printerHWSetting1.OnGetProperty(ref sp, ref bChangeProperty);
        }

        private void printerHWSetting1_OKButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

		private void PrinterHWSettingForm_Load(object sender, System.EventArgs e)
		{
			Grouper sample = new Grouper();
#if LIYUUSB
            sample.BackgroundGradientMode = GroupBoxGradientMode.Vertical;
            sample.TitleStyle = TitleStyles.XPStyle;
#else
			sample.TitleStyle = TitleStyles.Standard;
#endif
            SetGroupBoxStyle(sample);
		}
		public void SetGroupBoxStyle(Grouper ts)
		{
			this.printerHWSetting1.GrouperTitleStyle = ts;
		}
	}

}
