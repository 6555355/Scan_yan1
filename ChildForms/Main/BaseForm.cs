using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics ;
using System.Xml;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Text;

using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Setting;
using BYHXPrinterManager.Port;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.Calibration;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// BaseForm 的摘要说明。
	/// </summary>
	public class BaseForm : System.Windows.Forms.Form,IPrinterChange
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BaseForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BaseForm));
			// 
			// BaseForm
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.KeyPreview = true;
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "BaseForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");

		}
		#endregion

		#region 常量

		#endregion

		#region 变量

		//ToolBarButton m_PushedToolBarButton = null;
		private bool m_bExitAtStart = false;
		private uint m_KernelMessage	= SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
		private PrinterOperate m_LastOperate = new PrinterOperate(); 
		DateTime m_StartTime = DateTime.Now;
		private AllParam m_allParam;
		private PortManager m_PortManager;
		private bool m_bSendMoveCmd = false;
		private SettingForm m_FuncSettingForm = null;
		private CaliWizard m_wizard = null;
		private bool m_bFirstReady = false;
		private string m_sFormTile = "";
		private MyMessageBox m_MyMessageBox = null;
		private Grouper m_GroupboxStyle = null;
		private UIJob mPrintingjob = null;
		#endregion

		#region 接口实现

		# region IPrinterChange 接口成员实现

		public virtual void OnPrinterStatusChanged(JetStatusEnum status)
		{
//			UpdateButtonStates(status);
//			SetPrinterStatusChanged(status);
			if(status == JetStatusEnum.Error)
			{
				OnErrorCodeChanged(CoreInterface.GetBoardError());
#if false
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
#endif
			}
			else
				OnErrorCodeChanged(0);
//			m_ToolbarSetting.OnPrinterStatusChanged(status);                 
//			m_JobListForm.OnPrinterStatusChanged(status);
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

				///Check version
				SBoardInfo sBoardInfo = new SBoardInfo();
				if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
				{
					const int MINI_MAINBOARD_VERSION = 0x00020200;
					SFWVersion fwv= new SFWVersion(sBoardInfo.m_nBoradVersion);
					SFWVersion min_fwv= new SFWVersion(MINI_MAINBOARD_VERSION);
					if( (((fwv.m_nMainVersion <<8)+ fwv.m_nSubVersion)<<8) < MINI_MAINBOARD_VERSION)
					{
						string info = "";
						string mPrintingFormat = SErrorCode.GetEnumDisplayName(typeof(Software),Software.VersionNoMatch);
						string curVersion = fwv.m_nMainVersion + "." + fwv.m_nSubVersion;
						string minVersion = min_fwv.m_nMainVersion + "." + min_fwv.m_nSubVersion;
						info += "\n" + string.Format(mPrintingFormat,curVersion,minVersion);
						MessageBox.Show(this, info,"",MessageBoxButtons.OK);
						//m_bExitAtStart = true;
					}
				}
#else
				CoreInterface.VerifyHeadType();
				//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
				BYHXSoftLock.m_DongleKeyAlarm.FirstReadyShakeHand();
//				ShowMeasureQuestionForm(this.Visible);
			}
		}
		public virtual void OnPrinterPropertyChange( SPrinterProperty sp)
		{

			JetStatusEnum status = CoreInterface.GetBoardStatus();
			if(status != JetStatusEnum.PowerOff)
			{
				byte headNum =  sp.nHeadNum;
				if(SPrinterProperty.IsKonica512 (sp.ePrinterHead))
					headNum /= 2;
				else if(SPrinterProperty.IsPolaris (sp.ePrinterHead))
				{
					headNum /= 4;
				}

				this.Text = m_sFormTile 
					+ " "   + sp.ePrinterHead.ToString() 
					+ " "	+ headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Head)
					+ " "	+ sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName),DispName.Color);
			}
		}
		public virtual void OnPrinterSettingChange( SPrinterSetting ss)
		{
		}
		public virtual void OnPreferenceChange( UIPreference up)
		{
		}

		public virtual bool LoadJobList()
		{
			return true;
		}
		public virtual bool SaveJobList()
		{
			return true;
		}
		public virtual AllParam GetAllParam()
		{
			return m_allParam;
		}

		public virtual void NotifyUIParamChanged()
		{

		}

		public virtual void NotifyUIKeyDownAndUp(Keys keyData,bool bKeydown)
		{

		}

		public virtual void OnSwitchPreview()
		{

		}

		public virtual void NotifyUICalibrationExit(bool bSave)
		{

		}
		#endregion

		#endregion

		#region 方法

		protected virtual void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
		{
			CoreMsgEnum	kParam	= (CoreMsgEnum)wParam.ToInt32();

			switch(kParam)
			{
				case CoreMsgEnum.UpdaterPercentage:
				{
					int	percentage	= lParam.ToInt32();
					//OnPrintingProgressChanged(percentage);
					string info = "";
					string mPrintingFormat = ResString.GetUpdatingProgress();
					info += string.Format(mPrintingFormat,percentage);
//					this.m_StatusBarPanelPercent.Text = info;
//					this.m_PrintInformation.PrintString(info,UserLevel.user,ErrorAction.Updating);
					break;
				}

				case CoreMsgEnum.Percentage:
				{
					int	percentage	= lParam.ToInt32();
					OnPrintingProgressChanged(percentage);
					Console.WriteLine("Printing: {0}%",percentage);
					if(percentage > 0)
					{
//						m_PreviewAndInfo.UpdatePercentage(percentage);
					}

					break;
				}
				case CoreMsgEnum.Job_Begin:
				{	
					int startType = lParam.ToInt32();

					if(startType == 0)
					{
					}
					else if(startType == 1)
					{
						OnPrintingStart();
					}
					
					break;
				}
				case CoreMsgEnum.Job_End:
				{

					int endType	= lParam.ToInt32();

					if(endType == 0)
					{
					}
					else if(endType == 1)
					{
						OnPrintingEnd();
					}
					break;
				}
				case CoreMsgEnum.Power_On:
				{
					int bPowerOn = lParam.ToInt32();
					if(bPowerOn != 0)
					{
						int  bPropertyChanged, bSettingChanged;
						SPrinterProperty  sPrinterProperty;
						SPrinterSetting sPrinterSetting;
						
						m_allParam.PowerOnEvent(out bPropertyChanged, out bSettingChanged,out sPrinterProperty,out sPrinterSetting);
						if(bPropertyChanged != 0)
						{
							OnPrinterPropertyChange(sPrinterProperty);
							if(m_FuncSettingForm != null)
								m_FuncSettingForm.OnPrinterPropertyChange(sPrinterProperty);
						}
						if(bSettingChanged != 0)
						{
							OnPrinterSettingChange(sPrinterSetting);
						}
					}
					else
					{
						if(m_MyMessageBox != null)
							SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );

//						this.m_JobListForm.TerminatePrintingJob(false);
						m_allParam.PowerOffEvent();
						//m_bFirstReady = false;
					}
					break;
				}
				case CoreMsgEnum.Status_Change:
				{
					int status = lParam.ToInt32();
					OnPrinterStatusChanged((JetStatusEnum)status);
					if(m_bExitAtStart)
					{
						End();
						Application.Exit();
					}
					break;
				}
				case CoreMsgEnum.ErrorCode:
				{
					int errorCode = lParam.ToInt32();
					SErrorCode sErrorCode= new SErrorCode(errorCode);
					if(SErrorCode.IsOnlyPauseError(errorCode))
					{
						if(m_MyMessageBox != null)
						{
							SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );
						}
						m_MyMessageBox = new MyMessageBox();

						string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
						//DialogResult result = m_MyMessageBox.ShowDialog();
						//DialogResult result = MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
						if(sErrorCode.nErrorCause == (byte)ErrorCause.COM &&
							(sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyGoHome 
							||sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyMeasure
							))
						{
							DialogResult result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Information);
							CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
							if(sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
								sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,sErrorCode.nErrorCode);
#endif
							if(result != DialogResult.No)
							{
								if(sErrorCode.nErrorCode ==(byte) COMCommand_Abort.NotifyGoHome)
								{
									CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint,(int)AxisDir.X);
								}
								else if(sErrorCode.nErrorCode ==(byte) COMCommand_Abort.NotifyMeasure)
								{
									CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper,1);
								}
							}
						}
						else
						{
							DialogResult result = DialogResult.Retry;
							if(sErrorCode.nErrorCause == (byte)ErrorCause.Software &&
								sErrorCode.nErrorCode == (byte)Software.MediaTooSmall
								)
							{
//								UIJob printingjob = m_JobListForm.PrintingJob;
//								if( printingjob!=null && !printingjob.Equals(mPrintingjob))
//								{
//									result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
//									if(result == DialogResult.Cancel)
//									{
//										//CoreInterface.Printer_Abort();
//										if(CoreInterface.Printer_IsOpen() != 0)
//											m_JobListForm.AbortPrintingJob();						
//									}
//									else
//									{
//										this.mPrintingjob = printingjob;
//									}
//								}
							}
							else
							{
								result = m_MyMessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
								if(result == DialogResult.Cancel)
								{
									//CoreInterface.Printer_Abort();
//									if(CoreInterface.Printer_IsOpen() != 0)
//										m_JobListForm.AbortPrintingJob();						
								}
							}
							if(result != DialogResult.Cancel)
							{
								CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
								if(sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
									sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
									CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,sErrorCode.nErrorCode);
#endif
							}
						}
						m_MyMessageBox = null;

					}
					OnErrorCodeChanged(lParam.ToInt32());
					//For Updateing
					ErrorCause cause = (ErrorCause)sErrorCode.nErrorCause;
					if(cause == ErrorCause.CoreBoard && (ErrorAction)sErrorCode.nErrorAction == ErrorAction.Updating)
					{
						if(0 != sErrorCode.nErrorCode)
						{
							if(sErrorCode.nErrorCode == 1)
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
						if(m_FuncSettingForm != null)
							m_FuncSettingForm.OnPrinterSettingChange(sPrinterSetting);
					}
					//m_LockUpdate = false;
					break;
				}
				case CoreMsgEnum.AbortPrintCmd:
					//MessageBox.Show(this,"Accept! ");
//					m_JobListForm.AbortPrintingJob();
					break;
				case CoreMsgEnum.BlockNotifyUI:
					int msg1 = lParam.ToInt32();
				{
					if(m_MyMessageBox != null)
					{
						SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );
					}
					m_MyMessageBox = new MyMessageBox();
				{
					string m1 = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.GoHome);
					DialogResult result = m_MyMessageBox.Show(m1,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Information);
					if(result != DialogResult.No)
					{
						CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint,(int)AxisDir.X);
					}
				}
					m_MyMessageBox = null;
				}
					break;
			}
		}


		public virtual bool Start()
		{
			if(!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle))
				return m_bExitAtStart;
			SystemCall.PreventSystemPowerdown();
			m_allParam = new AllParam();
			SystemInit init = new SystemInit(this,this.Handle,m_KernelMessage);
			init.SystemStart();

			m_PortManager = new PortManager();
			m_PortManager.OpenPort();
			m_PortManager.TaskStart();
			return !m_bExitAtStart;
		}


		public virtual bool End()
		{
			SystemInit init = new SystemInit(this,this.Handle,m_KernelMessage);
			init.SystemEnd();

			if(m_PortManager != null)
				m_PortManager.ClosePort();
			SystemCall.AllowSystemPowerdown();
			return true;
		}


		public virtual void OnErrorCodeChanged(int code)
		{

		}

		public virtual void OnEditPrinterSetting()
		{
			SettingForm form = new SettingForm();
			form.SetGroupBoxStyle(m_GroupboxStyle);
			m_FuncSettingForm = form;
			JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
			//Change the IconButton status for calibration
			m_FuncSettingForm.SetPrinterStatusChanged(printerStatus);
			//OnPrinterStatusChanged(printerStatus);
			form.OnPreferenceChange(m_allParam.Preference);
			form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
			form.OnPrinterSettingChange(m_allParam.PrinterSetting);
			form.OnRealTimeChange();
			if(form.ShowDialog(this) == DialogResult.OK)
			{
				bool bChangeProperty = false;
				form.OnGetPrinterSetting(ref m_allParam,ref bChangeProperty);
				CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
				OnPreferenceChange(m_allParam.Preference);
				if(bChangeProperty)
					CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
			}
			form = null;
			m_FuncSettingForm = null;
		}

		
		public virtual void OnPrintingProgressChanged(int percent)
		{

		}


		public virtual void OnPrintingStart()
		{
		}

		public virtual void OnPrintingEnd()
		{

		}

		#endregion

		#region override
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

			if (m.Msg == 0x0219)//WM_DEVICECHANGE
				BYHXSoftLock.OnDeviceChange(m.WParam, m.LParam);

			if(m.Msg == this.m_KernelMessage)
			{
				ProceedKernelMessage(m.WParam,m.LParam);
			}

		}

		#endregion
	}
}
