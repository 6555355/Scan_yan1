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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;

using System.Xml;
using System.Runtime.InteropServices;
using  System.Diagnostics;

using  BYHXPrinterManager.Setting;
using BYHXPrinterManager.Main;
using FactoryWrite;
using BYHXPrinterManager;

namespace FactoryWriter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PrinterWrite : System.Windows.Forms.Form
	{
		private AllParam m_allParam;
        QRCode QRCodeForm = null;
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
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.StatusBar m_StatusBarApp;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelJetStaus;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelComment;
		private System.Windows.Forms.Button m_ButtonUpdater;
		private System.Windows.Forms.Button m_ButtonFactoryData;
		private System.Windows.Forms.Button m_ButtonRealTime;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelError;
		private System.Windows.Forms.StatusBarPanel m_StatusBarPanelPercent;
        private System.Windows.Forms.Button buttonCleanParameter;
        private Button btnSetMbId;
        private Button button_QACode;
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
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
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
            this.m_ButtonUpdater = new System.Windows.Forms.Button();
            this.m_ButtonFactoryData = new System.Windows.Forms.Button();
            this.m_ButtonRealTime = new System.Windows.Forms.Button();
            this.buttonCleanParameter = new System.Windows.Forms.Button();
            this.btnSetMbId = new System.Windows.Forms.Button();
            this.button_QACode = new System.Windows.Forms.Button();
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
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem2,
            this.menuItem8,
            this.menuItem9,
            this.menuItem4});
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.menuItem3.Click += new System.EventHandler(this.m_MenuItemHWSetting_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            resources.ApplyResources(this.menuItem2, "menuItem2");
            this.menuItem2.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            resources.ApplyResources(this.menuItem8, "menuItem8");
            this.menuItem8.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
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
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
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
            // m_ButtonUpdater
            // 
            resources.ApplyResources(this.m_ButtonUpdater, "m_ButtonUpdater");
            this.m_ButtonUpdater.Name = "m_ButtonUpdater";
            this.m_ButtonUpdater.Click += new System.EventHandler(this.m_MenuItemUpdate_Click);
            // 
            // m_ButtonFactoryData
            // 
            resources.ApplyResources(this.m_ButtonFactoryData, "m_ButtonFactoryData");
            this.m_ButtonFactoryData.Name = "m_ButtonFactoryData";
            this.m_ButtonFactoryData.Click += new System.EventHandler(this.m_MenuItemHWSetting_Click);
            // 
            // m_ButtonRealTime
            // 
            resources.ApplyResources(this.m_ButtonRealTime, "m_ButtonRealTime");
            this.m_ButtonRealTime.Name = "m_ButtonRealTime";
            this.m_ButtonRealTime.Click += new System.EventHandler(this.m_MenuItemRealTime_Click);
            // 
            // buttonCleanParameter
            // 
            resources.ApplyResources(this.buttonCleanParameter, "buttonCleanParameter");
            this.buttonCleanParameter.Name = "buttonCleanParameter";
            this.buttonCleanParameter.Click += new System.EventHandler(this.buttonCleanParameter_Click);
            // 
            // btnSetMbId
            // 
            resources.ApplyResources(this.btnSetMbId, "btnSetMbId");
            this.btnSetMbId.Name = "btnSetMbId";
            this.btnSetMbId.Click += new System.EventHandler(this.btnSetMbId_Click);
            // 
            // button_QACode
            // 
            resources.ApplyResources(this.button_QACode, "button_QACode");
            this.button_QACode.Name = "button_QACode";
            this.button_QACode.UseVisualStyleBackColor = true;
            this.button_QACode.Click += new System.EventHandler(this.button_QACode_Click);
            // 
            // PrinterWrite
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.button_QACode);
            this.Controls.Add(this.btnSetMbId);
            this.Controls.Add(this.buttonCleanParameter);
            this.Controls.Add(this.m_ButtonFactoryData);
            this.Controls.Add(this.m_ButtonRealTime);
            this.Controls.Add(this.m_StatusBarApp);
            this.Controls.Add(this.m_ButtonUpdater);
            this.KeyPreview = true;
            this.Menu = this.m_MainMenu;
            this.Name = "PrinterWrite";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.PrinterWrite_Closing);
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
		    CoreInterface.AllParams = m_allParam;
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
			//SaveWriteConfig(m_WriteConfigList);

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
            button_QACode.Visible = m_allParam.PrinterProperty.ePrinterHead == PrinterHeadEnum.Epson_S2840 || m_allParam.PrinterProperty.ePrinterHead == PrinterHeadEnum.Epson_S2840_WaterInk || m_allParam.PrinterProperty.ePrinterHead == PrinterHeadEnum.EPSON_I3200 || m_allParam.PrinterProperty.ePrinterHead == PrinterHeadEnum.EPSON_S1600_RC_UV;

#if !EpsonLcd
			this.buttonCleanParameter.Visible = false;
#endif
		}
		public void OnPrinterSettingChange( SPrinterSetting ss)
		{
			m_allParam.PrinterSetting = ss;
		}
		public void OnPreferenceChange( UIPreference up)
		{
			m_allParam.Preference = up;
		}

//		public void OnPrinterStatusChanged(JetStatusEnum status)
//		{
//
//			UpdateButtonStates(status);
//			SetPrinterStatusChanged(status);
//		}
        private bool m_IsFATAL = false;

	    public void OnPrinterStatusChanged(JetStatusEnum status)
	    {

	        UpdateButtonStates(status);
	        SetPrinterStatusChanged(status);
	        if (status == JetStatusEnum.Error)
	        {
	            OnErrorCodeChanged(CoreInterface.GetBoardError());

	            int errorCode = CoreInterface.GetBoardError();
	            SErrorCode sErrorCode = new SErrorCode(errorCode);
	            if (SErrorCode.IsOnlyPauseError(errorCode))
	            {
	                string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);

	                if (
	                    MessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.RetryCancel,
	                        MessageBoxIcon.Exclamation) == DialogResult.Retry)
	                {
	                    CoreInterface.Printer_Resume();
	                }
	            }
	        }
	        else
	            OnErrorCodeChanged(0);
	        if (status != JetStatusEnum.PowerOff && status != JetStatusEnum.Initializing && m_IsFATAL == false)
	        {
                if (SPrinterProperty.IsGongZeng() || SPrinterProperty.IsDocanPrintMode() || SPrinterProperty.IsRuiZhi()) // 此功能按 厂商限定
	            {
	                byte[] infos = new byte[19];
	                BYHX_SL_RetValue ret = BYHXSoftLock.GetDongleInfo(ref infos);
	                if (ret == BYHX_SL_RetValue.SUCSESS)
	                {
	                    byte[] dtV = new byte[4];
	                    Buffer.BlockCopy(infos, 4, dtV, 0, dtV.Length);
	                    uint boardId = BitConverter.ToUInt32(dtV, 0);
	                    if (CoreInterface.IsFatal(boardId))
	                    {
	                        m_IsFATAL = true;
	                    }
	                }
	            }
	            else
	            {
	                m_IsFATAL = true;
	            }
	        }
	    }

	    private void UpdateButtonStates(JetStatusEnum status)
		{
			if(status == JetStatusEnum.PowerOff)
			{
				m_ButtonFactoryData.Enabled = false;
				menuItem3.Enabled= false;
                btnSetMbId.Enabled = false;
			}
			else
			{
				m_ButtonFactoryData.Enabled = true;
                menuItem3.Enabled = true;
                btnSetMbId.Enabled = true;
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

			const string MUTEX = CoreConst.c_MUTEX_App;
			bool createdNew  = false;
			Mutex mutex = new Mutex(true,MUTEX,out createdNew);
			if(!createdNew)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.OnlyOneProgram));
				mutex.Close();
				return;
			}

#if EpsonLcd
			int lcid  = PubFunc.GetLanguage();
			if(lcid == -1)
			{
				// 未连接usb线,无法获取软件语言,启动终止.
				MessageBox.Show(ResString.GetResString("NoFoundUSBReturn"),ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
#else
			AllParam cur = new AllParam();
			int lcid = cur.GetLanguage();			
#endif
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);

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



		private void PrinterWrite_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			End();
		}

		private void m_MenuItemUpdate_Click(object sender, System.EventArgs e)
		{
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			if(printerStatus == JetStatusEnum.Busy)
			{
				if(MessageBox.Show(this, 
					"Are you sure want to update?", 
					"update", 
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
			fileDialog.DefaultExt = ".dat";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Dat);
			fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
				UpdateCoreBoard(fileDialog.FileName);
			}
		}
		private void UpdateCoreBoard(string m_UpdaterFileName)
		{
#if !LIYUUSB
			bool bRead = false;
			byte[]			buffer = null;
			int				fileLen = 0;
			try
			{
				const int  USB_EP2_MIN_PACKAGESIZE =  1024;

				FileStream		fileStream		= new FileStream(m_UpdaterFileName, FileMode.Open,FileAccess.Read,FileShare.Read);
				BinaryReader	binaryReader	= new BinaryReader(fileStream);
				fileLen = (int)fileStream.Length;
				int				buffersize = (fileLen + USB_EP2_MIN_PACKAGESIZE -1)/USB_EP2_MIN_PACKAGESIZE * USB_EP2_MIN_PACKAGESIZE;
				buffer			= new byte[buffersize];
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
				CoreInterface.BeginUpdating(buffer,fileLen);
			}
#else
			CoreInterface.BeginUpdateMotion(m_UpdaterFileName);
#endif

		}

		private void m_MenuItemRealTime_Click(object sender, System.EventArgs e)
		{
			KonicTemperature form = new KonicTemperature();
			form.OnPreferenceChange(m_allParam.Preference);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.OnRealTimeChange();
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				form.ApplyToBoard();
			}
		}

		private void m_MenuItemHWSetting_Click(object sender, System.EventArgs e)
		{
            //if(SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead))
			{
				FWValidationForm fwvf = new FWValidationForm();
				if (fwvf.ShowDialog() != DialogResult.OK)
					return;
			}
            PrinterHWSettingForm form = new PrinterHWSettingForm();
			form.OnPreferenceChange(m_allParam.Preference);
            form.OnPrinterSettingChange(m_allParam.PrinterSetting);
			if(!form.OnPrinterPropertyChange(m_allParam.PrinterProperty))
                return;
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bChangeProperty = false;
				form.OnGetProperty(ref m_allParam.PrinterProperty,ref bChangeProperty);
                form.OnGetPrinterSetting(ref m_allParam.PrinterSetting);
				m_allParam.PrinterSetting.sFrequencySetting.nResolutionX = m_allParam.PrinterProperty.nResX;
                CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
				OnPreferenceChange(m_allParam.Preference);
				if(bChangeProperty)
					CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
				//m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting);
			}
	
		}
		protected override void WndProc(ref Message m)
		{
		    try
		    {
			    if(((int)m.WParam.ToInt64()) ==   0xF060)   //   关闭消息   
                {
                    string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Exit);
                    if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                base.WndProc(ref m);

                if (m.Msg == this.m_KernelMessage)
                {
                    ProceedKernelMessage(m.WParam, m.LParam);
                }
            }
		    catch (Exception ex)
		    {
		        MessageBox.Show(ex.Message);
		    }
		}
		private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
		{
            CoreMsgEnum kParam = (CoreMsgEnum)wParam.ToInt64();

            switch (kParam)
            {
                case CoreMsgEnum.UpdaterPercentage:
                    {
                        int percentage = (int)lParam.ToInt64();
                        //OnPrintingProgressChanged(percentage);
                        string info = "";
                        string mPrintingFormat = ResString.GetUpdatingProgress();
                        info += "\n" + string.Format(mPrintingFormat, percentage);
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

                        if (startType == 0)
                        {
                        }
                        else if (startType == 1)
                        {
                            //OnPrintingStart();
                        }

                        break;
                    }
                case CoreMsgEnum.Job_End:
                    {
                        int endType = (int)lParam.ToInt64();

                        if (endType == 0)
                        {
                        }
                        else if (endType == 1)
                        {
                            //OnPrintingEnd();
                        }

                        break;
                    }
                case CoreMsgEnum.Power_On:
                    {
                        int bPowerOn = (int)lParam.ToInt64();
                        if (bPowerOn != 0)
                        {
                            int bPropertyChanged, bSettingChanged;
                            SPrinterProperty sPrinterProperty;
                            SPrinterSetting sPrinterSetting;

                            m_allParam.PowerOnEvent(out bPropertyChanged, out bSettingChanged, out sPrinterProperty, out sPrinterSetting);
                            if (bPropertyChanged != 0)
                            {
                                OnPrinterPropertyChange(sPrinterProperty);
                            }
                            if (bSettingChanged != 0)
                            {
                                OnPrinterSettingChange(sPrinterSetting);
                            }
                        }
                        else
                        {
                            m_allParam.PowerOffEvent();
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
                        if (cause == ErrorCause.CoreBoard && (ErrorAction)serrorcode.nErrorAction == ErrorAction.Updating)
                        {
                            if (0 != serrorcode.n16ErrorCode)
                            {
                                if (serrorcode.n16ErrorCode == 1)
                                {
                                    string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.UpdateSuccess);
                                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.UpdateFail);
                                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
#if !LIYUUSB
                                CoreInterface.SendJetCommand((int)JetCmdEnum.ClearUpdatingStatus, 0);
#endif
                            }
                        }

                        break;
                    }
                case CoreMsgEnum.Parameter_Change:
                    {
                        //m_LockUpdate = true;
                        SPrinterSetting sPrinterSetting = m_allParam.PrinterSetting;
                        if (CoreInterface.GetPrinterSetting(ref sPrinterSetting) == 0)
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
                case CoreMsgEnum.Ep6Pipe:
                    {
                        int ep6Cmd = (int)lParam.ToInt64() & 0x0000ffff;
                        int index = (((int)lParam.ToInt64()) >> 16);
                        byte[] bufep6 = null;
                        int buflen = 0;
                        int ret = (CoreInterface.GetEp6PipeData(ep6Cmd, index, bufep6, ref buflen));
                        if (ret != 0 && buflen > 0)
                        {
                            bufep6 = new byte[buflen];
                            if (CoreInterface.GetEp6PipeData(ep6Cmd, index, bufep6, ref buflen) != 0)
                            {
                                switch (bufep6[0])
                                {
                                    case 0xD4:
                                        if (QRCodeForm != null)
                                        {
                                            QRCodeForm.SetQrCode(bufep6);
                                        }
                                        break;
                                    case 0xD3:
                                        if (QRCodeForm != null)
                                        {
                                            QRCodeForm.FeedBack(bufep6);
                                        }
                                        break;
                                    case 0xE6:
                                        if (QRCodeForm != null)
                                        {
                                            QRCodeForm.SetQrCodeFromHead(bufep6);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show(ResString.GetResString("GetEp6PipeDatafail"));
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show("GetEp6PipeData  buflen fail!");
                        //}
                    }
                    break;
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

		private void buttonCleanParameter_Click(object sender, System.EventArgs e)
		{
			EpsonCleanForm cleanform = new EpsonCleanForm();
		    cleanform.Owner = this;
            cleanform.StartPosition = FormStartPosition.CenterParent;
            cleanform.ShowDialog();
		}

        private void menuItem6_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);

        }

        private void btnSetMbId_Click(object sender, EventArgs e)
        {
            SetMbIdForm mbIdForm = new SetMbIdForm();
            mbIdForm.StartPosition = FormStartPosition.CenterParent;
            mbIdForm.Owner = this;
            mbIdForm.ShowDialog();
        }

        private void button_QACode_Click(object sender, EventArgs e)
        {
            QRCodeForm = new QRCode();
            QRCodeForm.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            QRCodeForm.ShowDialog();
            QRCodeForm = null;
        }
	}

}
