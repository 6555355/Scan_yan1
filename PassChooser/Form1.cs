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
namespace WriteBoardConfig
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PrinterWrite : System.Windows.Forms.Form
	{
		private bool m_bFirstReady = false;
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
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_ButtonPrint;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonStop;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private IContainer components;

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
			button1_Click(null,null);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterWrite));
            this.m_MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.m_StatusBarApp = new System.Windows.Forms.StatusBar();
            this.m_StatusBarPanelJetStaus = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelError = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelPercent = new System.Windows.Forms.StatusBarPanel();
            this.m_StatusBarPanelComment = new System.Windows.Forms.StatusBarPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_ButtonPrint = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MainMenu
            // 
            this.m_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem2,
            this.menuItem3,
            this.menuItem8,
            this.menuItem9,
            this.menuItem4});
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            resources.ApplyResources(this.menuItem7, "menuItem7");
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            resources.ApplyResources(this.menuItem8, "menuItem8");
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 4;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 5;
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6});
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            resources.ApplyResources(this.menuItem6, "menuItem6");
            // 
            // m_StatusBarApp
            // 
            resources.ApplyResources(this.m_StatusBarApp, "m_StatusBarApp");
            this.m_StatusBarApp.Name = "m_StatusBarApp";
            this.m_StatusBarApp.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.m_StatusBarPanelJetStaus,
            this.m_StatusBarPanelError,
            this.m_StatusBarPanelPercent,
            this.m_StatusBarPanelComment});
            this.m_StatusBarApp.ShowPanels = true;
            // 
            // m_StatusBarPanelJetStaus
            // 
            this.m_StatusBarPanelJetStaus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelJetStaus, "m_StatusBarPanelJetStaus");
            // 
            // m_StatusBarPanelError
            // 
            this.m_StatusBarPanelError.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelError, "m_StatusBarPanelError");
            // 
            // m_StatusBarPanelPercent
            // 
            this.m_StatusBarPanelPercent.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.m_StatusBarPanelPercent, "m_StatusBarPanelPercent");
            // 
            // m_StatusBarPanelComment
            // 
            this.m_StatusBarPanelComment.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            resources.ApplyResources(this.m_StatusBarPanelComment, "m_StatusBarPanelComment");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel5);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // m_ButtonPrint
            // 
            resources.ApplyResources(this.m_ButtonPrint, "m_ButtonPrint");
            this.m_ButtonPrint.Name = "m_ButtonPrint";
            this.m_ButtonPrint.Click += new System.EventHandler(this.m_ButtonPrint_Click);
            // 
            // buttonStop
            // 
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.m_ButtonPrint);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.panel4);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // PrinterWrite
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.m_StatusBarApp);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Menu = this.m_MainMenu;
            this.Name = "PrinterWrite";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PrinterWrite_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelJetStaus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_StatusBarPanelComment)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
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
			}
			else
				OnErrorCodeChanged(0);
			if(status == JetStatusEnum.PowerOff)
				m_bFirstReady = false;
			if(m_bFirstReady == false && status == JetStatusEnum.Ready)
			{
				m_bFirstReady = true;
#if !LIYUUSB
				string vol = ResString.GetResString("FW_Voltage");
				byte [] buffer = System.Text.Encoding.Unicode.GetBytes(vol);
				int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
				CoreInterface.SetFWVoltage(buffer,buffer.Length,lcid);

#else
				CoreInterface.VerifyHeadType();
				//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
			}
		}

		private void UpdateButtonStates(JetStatusEnum status)
		{
			if(status == JetStatusEnum.Ready)
			{
				m_ButtonPrint.Enabled = true;
			}
			else
			{
				m_ButtonPrint.Enabled = false;
			}
		}

		private void SetPrinterStatusChanged(JetStatusEnum status)
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
			bool createdNew  = false;
			Mutex mutex = new Mutex(true,MUTEX,out createdNew);
			if(!createdNew)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.OnlyOneProgram));
				mutex.Close();
				return;
			}
#if true
			PrinterWrite mainWin = new PrinterWrite();
			if(mainWin.Start())
				Application.Run(mainWin);
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
			if(m.WParam.ToInt64()==   0xF060)   //   πÿ±’œ˚œ¢   
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
            CoreMsgEnum kParam = (CoreMsgEnum)wParam.ToInt64();

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

		private byte[] realRet;
		public byte[] Passes
		{
			get{return realRet;}
		}

		private bool creatCheckBoxs(int count)
		{
			int margin = 8;
			if (count <= 0)
				return false;
            this.panel5.Controls.Clear();
			int ColCount = 0;
			string Maxtext = count.ToString();
			CheckBox CBsampale = new CheckBox();
			CBsampale.Width = 50;
			CBsampale.Text = Maxtext;

            ColCount = (int)((this.panel5.Width - margin * 2) / (CBsampale.Width));
			int i = 0;
			int rowindex = 0;
			while (i < count)
			{
				for (int j = 0; j < ColCount && i < count; j++)
				{
					CheckBox CB1 = new CheckBox();
					CB1.Text = (i + 1).ToString();
					CB1.Width = 50;
					CB1.CheckedChanged += new EventHandler(CB1_CheckedChanged);
					CB1.Location = new Point(j * (CB1.Width)+ 20 ,rowindex * (CB1.Width/2));
                    this.panel5.Controls.Add(CB1);
					i++;
				}
				rowindex++;
			}
			return true;
		}

		void CB1_CheckedChanged(object sender, EventArgs e)
		{
			string text = string.Empty;
			int count = int.Parse(this.textBox1.Text);
//			if(count < 8) count = 8;
			byte[] ret = new byte[count];
			for (int j = 0; j < count; j++)
			{
//				if(j < this.groupBox2.Controls.Count)
                ret[j] = ((CheckBox)this.panel5.Controls[j]).Checked ? (byte)1 : (byte)0;
			}

			byte[] hex = new byte[8];
			realRet = new byte[Math.Max( count /8,1)];
			int i = 0;
			foreach(byte bt in ret)
			{
				i++;
				text += bt.ToString("X2") + " ";
				hex[(i-1)%8] = bt;
				if(i%8 == 0 || i == ret.Length)
				{
					byte ihex = 0;
					string hexStr = string.Empty;
					for(int m = 0;m < 8; m++)
					{
						ihex += (byte)(hex[m]* Math.Pow(2,m%8));
					}
					realRet[(i -1)/8]= ihex;
					text +=  "  " + ihex.ToString("X2").ToUpper() + "\n";
				}
			}

			this.label2.Text = text;
			this.label2.Invalidate();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			int passNum = 0;
			try
			{
				passNum = int.Parse(this.textBox1.Text);
				if(passNum <= 0)
					return;
				this.creatCheckBoxs(passNum);
				this.CB1_CheckedChanged(null,new EventArgs());
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void m_ButtonPrint_Click(object sender, System.EventArgs e)
		{
			CoreInterface.UpdateHeadMask(realRet,realRet.Length);
			int ret = CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckNozzleCmd,0,ref this.m_allParam.PrinterSetting);
		    if (ret == 0)
		    {
                MessageBox.Show("SendCalibrateCmd  ß∞‹!!");
		    }
            return;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			int count = int.Parse(this.textBox1.Text);
            bool bChecked = ((CheckBox)this.panel5.Controls[0]).Checked;
			for (int j = 0; j < count; j++)
			{
                ((CheckBox)this.panel5.Controls[j]).Checked = !bChecked;
			}
	
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			CoreInterface.Printer_Abort();	
		}


	}


}
