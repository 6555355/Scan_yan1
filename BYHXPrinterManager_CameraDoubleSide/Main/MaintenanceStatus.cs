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
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Setting;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for PrintingInfo.
	/// </summary>
	public class MaintenanceStatus : BYHXUserControl
	{
		private IPrinterChange m_iPrinterChange =null;
		private bool m_bPrintingPreview = true;
		private UILengthUnit m_curUnit;
        private Panel panel2;
        private Main.InkTankStatusControl inkTankStatusControl1;
        private Main.PurgeControl purgeControl1;
        private MaintenanceSystemStatus maintenanceSystemStatus1;
        private GzPurgeControl gzPurgeControl1;

        /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public MaintenanceStatus()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		    // TODO: Add any initialization after the InitializeComponent call

#if SHIDAO
            this.inkTankStatusControl1.Visible = this.purgeControl1.Visible = true;
#else
            this.inkTankStatusControl1.Visible = this.purgeControl1.Visible = false;
#endif
#if ALLWIN
		    maintenanceSystemStatus1.Visible = true;
#else
		    maintenanceSystemStatus1.Visible = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingInfo));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style4 = new BYHXPrinterManager.Style();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gzPurgeControl1 = new BYHXPrinterManager.Main.GzPurgeControl();
            this.inkTankStatusControl1 = new BYHXPrinterManager.Main.InkTankStatusControl();
            this.purgeControl1 = new BYHXPrinterManager.Main.PurgeControl();
            this.maintenanceSystemStatus1 = new BYHXPrinterManager.Setting.MaintenanceSystemStatus();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.gzPurgeControl1);
            this.panel2.Controls.Add(this.inkTankStatusControl1);
            this.panel2.Controls.Add(this.purgeControl1);
            this.panel2.Controls.Add(this.maintenanceSystemStatus1);
            this.panel2.Name = "panel2";
            // 
            // gzPurgeControl1
            // 
            this.gzPurgeControl1.BackColor = System.Drawing.SystemColors.Window;
            this.gzPurgeControl1.Divider = false;
            resources.ApplyResources(this.gzPurgeControl1, "gzPurgeControl1");
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.gzPurgeControl1.GradientColors = style1;
            this.gzPurgeControl1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gzPurgeControl1.GrouperTitleStyle = null;
            this.gzPurgeControl1.Name = "gzPurgeControl1";
            // 
            // inkTankStatusControl1
            // 
            this.inkTankStatusControl1.Divider = false;
            resources.ApplyResources(this.inkTankStatusControl1, "inkTankStatusControl1");
            style2.Color1 = System.Drawing.SystemColors.Control;
            style2.Color2 = System.Drawing.SystemColors.Control;
            this.inkTankStatusControl1.GradientColors = style2;
            this.inkTankStatusControl1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.inkTankStatusControl1.GrouperTitleStyle = null;
            this.inkTankStatusControl1.Name = "inkTankStatusControl1";
            // 
            // purgeControl1
            // 
            this.purgeControl1.Divider = false;
            resources.ApplyResources(this.purgeControl1, "purgeControl1");
            style3.Color1 = System.Drawing.SystemColors.Control;
            style3.Color2 = System.Drawing.SystemColors.Control;
            this.purgeControl1.GradientColors = style3;
            this.purgeControl1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.purgeControl1.GrouperTitleStyle = null;
            this.purgeControl1.Name = "purgeControl1";
            // 
            // maintenanceSystemStatus1
            // 
            resources.ApplyResources(this.maintenanceSystemStatus1, "maintenanceSystemStatus1");
            this.maintenanceSystemStatus1.Divider = false;
            style4.Color1 = System.Drawing.SystemColors.Control;
            style4.Color2 = System.Drawing.SystemColors.Control;
            this.maintenanceSystemStatus1.GradientColors = style4;
            this.maintenanceSystemStatus1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.maintenanceSystemStatus1.GrouperTitleStyle = null;
            this.maintenanceSystemStatus1.Name = "maintenanceSystemStatus1";
            // 
            // PrintingInfo
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel2);
            this.Name = "PrintingInfo";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public void SetGroupBoxStyle(Grouper ts)
        {
            this.inkTankStatusControl1.GrouperTitleStyle = ts;
            purgeControl1.GrouperTitleStyle = ts;
            maintenanceSystemStatus1.GrouperTitleStyle = ts;
        }

	    public void OnPrinterPropertyChange(SPrinterProperty sp)
	    {
#if SHIDAO
            inkTankStatusControl1.OnPrinterPropertyChanged(sp);
            purgeControl1.OnPrinterPropertyChanged(sp);
#endif
	        maintenanceSystemStatus1.OnPrinterPropertyChanged(sp);

            gzPurgeControl1.Visible = SPrinterProperty.IsGongZengUv();
	        gzPurgeControl1.OnPrinterPropertyChanged(sp);
	    }

	    public void SetStatusData(byte[] buf)
	    {
            maintenanceSystemStatus1.OnStatusDataChanged(buf);
        }

		public void OnPrinterSettingChange( SPrinterSetting ss)
		{

		}

        public void SetPrinterStatusChanged(JetStatusEnum status, bool waitingPauseBetweenLayers=false)
		{
#if SHIDAO
            inkTankStatusControl1.OnPrinterStatusChanged(status);
            purgeControl1.OnPrinterStatusChanged(status);
#endif
            gzPurgeControl1.OnPrinterStatusChanged(status);
        }
		public void SetPrinterChange(IPrinterChange ic)
		{
			m_iPrinterChange = ic;
		}
	}

}
