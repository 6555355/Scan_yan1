/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Text;
using System.Net.Sockets;
using System.Globalization;
using System.Linq;
using System.Net;
using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Setting;
using BYHXPrinterManager.Calibration;
using BYHXPrinterManager.Port;
using BYHXPrinterManager.GradientControls;
using BYHXPrinterManager.TcpIp;
using MainWindow;
using PrinterStubC.CInterface;
using PrinterStubC.Utility;
using TcpIpBase;
using WAF_OnePass.Domain.Utility;
using PrinterStubC.Common;
using TcpIpBase.Byhx;

namespace BYHXPrinterManager.Main
{
    /// <summary>
    /// Summary description for MainForm.
    /// </summary>
    public partial class MainForm : System.Windows.Forms.Form, IPrinterChange, IMessageFilter
    {
        #region 常量
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_SYSKEYDOWN = 0x0104;
        const int WM_SYSKEYUP = 0x0105;
        #endregion

        #region 变量

        //ToolBarButton m_PushedToolBarButton = null;
        private bool m_bExitAtStart = false;
        private uint m_KernelMessage = SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
        private PrinterOperate m_LastOperate = new PrinterOperate();
        DateTime m_StartTime = DateTime.Now;
        private AllParam m_allParam;

        private PortManager m_PortManager;
        private bool m_bSendMoveCmd = false;
        private SettingForm m_FuncSettingForm = null;
        private CaliWizard m_wizard = null;
        private UVSettingForm m_hwForm = null;
        private bool m_bFirstReady = false;
        private bool m_IsFATAL = false;
        private string m_sFormTile = "";
        private MyMessageBox m_MyMessageBox = null;
        private Grouper m_GroupboxStyle = null;
        private bool m_bShowUI = true;

        private int ProcessID = 0;
        private System.Timers.Timer time = new System.Timers.Timer(10);//实例化Timer类，设置间隔时间为10毫秒；
        private System.Windows.Forms.Timer mPumpInkTimer;
        private int m_CurErrorCode = -1;
        private bool m_bDuringPrinting = false;
        //private JetStatusEnum curStatus = JetStatusEnum.Unknown;
        private bool isPrinting = false;
        private RemoteControlServer remoteControlServer;
        private RemoteClient server;
        private BackgroundWorker serverTask = new BackgroundWorker();
        private MyBackgroundWorker remoteControlServerTask = null;
        // 用于计算打印面积
        private int m_PrintPercent = -1;
        private UIJob m_PrintingJob = null;
        private DoubleSidePrintForm _normalPrintWindow = null;
        private List<string> pumpInkColorList;
        private bool pumpInkColorListWorked = false;
        GZClothMotionParam GZClothMotionParam = new GZClothMotionParam();
        private string m_VendorProductID = string.Empty;
        private System.Windows.Forms.Timer timerTempLog;
    
        private bool m_bFirstSetErrorFormat;
    #endregion

        #region 构造函数
        private bool m_bFrobidPrinter = false;
        private bool m_bOffline = false;
        private Size bigIconSize = new System.Drawing.Size(48, 48);
        private bool m_bGongzheng = false;
        public MainForm(bool bshowUI, string skinName = "")
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            PubFunc.GetUserPermission(); // 触发权限更新,必须保证在isfactoryuser函数之前
            StartWithInitComponent();
            UIViewMode uimode = (UIViewMode)m_allParam.Preference.ViewModeIndex;
            string skinpath = Path.Combine(Path.Combine(Application.StartupPath, CoreConst.SkinForlderName), skinName);
            if (MyWndProc.LoadRenderResource(skinpath))
            {
                this.ControlBox = (uimode == UIViewMode.OldView || uimode == UIViewMode.NotifyIcon) || !MyWndProc.CanDrawFormHeader;
                MyWndProc.SetWindowTheme(this.Handle, "", "");
            }
            MyWndProc.DrawRoundForm(this);

            if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
            {
                ScorpionCoreInterface.SetFwSupportErrorCode(PrinterStubC.Interface.ErrorFormat.New);
            }

            string iconpath = Application.StartupPath;
            iconpath += "\\setup\\app.ico";
            if (File.Exists(iconpath))
            {
                Icon icon = new Icon(iconpath);
                this.Icon = icon;
            }
            //Application.Icon = icon; 

            string NamePath = Application.StartupPath;
            NamePath += "\\setup\\Vender.xml";
            if (File.Exists(NamePath))
            {
                SelfcheckXmlDocument xmldoc = new SelfcheckXmlDocument();
                xmldoc.Load(NamePath);
                XmlElement node = xmldoc.DocumentElement;

                XmlNodeList list = node.GetElementsByTagName("Name");
                if (list != null && list.Count >= 1)
                {
                    string txt = list[0].InnerXml;
                    //this.Text = txt + "  " +this.Text;
                    this.Text = txt + "  " + ResString.GetProductName();
                    m_sFormTile = this.Text;
                }
                // 初始化泵墨管脚映射
                pumpInkColorList = new List<string>(32);
                XmlNodeList pumpInkColormap = node.GetElementsByTagName("PumpInkColorMap");
                if (pumpInkColormap != null && pumpInkColormap.Count >= 1)
                {
                    pumpInkColorListWorked = true;
                    string txt = pumpInkColormap[0].InnerXml;
                    string[] colors = txt.Split(',');
                    for (int i = 0; i < colors.Length; i++)
                    {
                        pumpInkColorList.Add(colors[i]);
                    }
                    if (pumpInkColorList.Count < 32)
                    {
                        for (int i = pumpInkColorList.Count; i < 32; i++)
                        {
                            pumpInkColorList.Add((i + 1).ToString());
                        }
                    }
                }
            }
            ///////////////////////////////////////////
            ///
            if (uimode == UIViewMode.OldView || uimode == UIViewMode.NotifyIcon)
            {
                PubFunc.IconReload(m_ToolbarImageList, m_allParam.Preference.SkinName);
            }

            m_GroupboxStyle = new Grouper();
            m_GroupboxStyle.BackgroundGradientMode = GroupBoxGradientMode.Vertical;
            m_GroupboxStyle.PaintGroupBox = true;
            m_GroupboxStyle.RoundCorners = 5;
            m_GroupboxStyle.ShadowColor = System.Drawing.Color.DarkGray;
            m_GroupboxStyle.ShadowControl = false;
            m_GroupboxStyle.ShadowThickness = 3;
            //			m_GroupboxStyle.TitileGradientColors = new BYHXPrinterManager.Style(System.Drawing.Color.LightBlue, System.Drawing.Color.LightBlue);
            m_GroupboxStyle.TitileGradientColors = new BYHXPrinterManager.Style(Color.FromArgb(0x65, 0x93, 0xb7), Color.FromArgb(0x48, 0x74, 0x97));
            //m_GroupboxStyle.TitileGradientColors = new BYHXPrinterManager.Style(Color.FromArgb(153, 187, 225), Color.FromArgb(234, 243, 252));
            m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            m_GroupboxStyle.TitleTextColor = Color.White;
            //			m_GroupboxStyle.GradientColors = new Style(Color.Lavender,SystemColors.Highlight);
            //			m_GroupboxStyle.GradientColors = new BYHXPrinterManager.Style(Color.FromArgb(0x65,0x93,0xb7),Color.FromArgb(0x48,0x74,0x97));
            m_GroupboxStyle.GradientColors = new BYHXPrinterManager.Style(SystemColors.Control, SystemColors.Control);
            m_GroupboxStyle.GroupImage = null;
            this.m_JobListForm.m_SampleGroup = m_GroupboxStyle;
            m_PreviewAndInfo.SetGroupBoxStyle(m_GroupboxStyle);
            //必须先初始化BYHXSoftLock.m_DongleKeyAlarm
            BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm();
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired += new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H += new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
            BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished += new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);

#if EpsonLcd
			this.m_bShowUI = bshowUI;
			this.HandleCreated +=new EventHandler(mainWin_HandleCreated);

			if(!this.m_bShowUI)
			{
				this.ShowInTaskbar = false;
				this.WindowState = FormWindowState.Minimized;
			}		

			ProcessID = Process.GetCurrentProcess().Id;
			time.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；  
			time.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；  
			time.Enabled = false;//是否执行System.Timers.Timer.Elapsed事件；
#else
            menuItem1.Visible = menuItemSaveCaliPara.Visible = menuItemLoadCaliPara.Visible = false;
#endif

            mPumpInkTimer = new System.Windows.Forms.Timer();
            mPumpInkTimer.Interval = 10000; // 10秒
            mPumpInkTimer.Tick += new EventHandler(mPumpInkTimer_Tick);//到达时间的时候执行事件；  


#if !INKTYPE
            toolBarButtonSwitchInk.Visible =
            m_MenuItemSelectInkType.Visible = false;
#endif
            this.serverTask.WorkerSupportsCancellation = false;
            this.serverTask.WorkerReportsProgress = true;
            this.serverTask.DoWork += new DoWorkEventHandler(backworker_DoWork);
            this.serverTask.ProgressChanged += new ProgressChangedEventHandler(backworker_ProgressChanged);
            remoteControlServerTask = new MyBackgroundWorker(this);
            //this.remoteControlServerTask.WorkerSupportsCancellation = false;
            //this.remoteControlServerTask.WorkerReportsProgress = true;
            this.remoteControlServerTask.DoWork += new DoWorkEventHandler(remoteControlServerTask_DoWork);
            this.remoteControlServerTask.ProgressChanged += new ProgressChangedEventHandler(remoteControlServerTask_ProgressChanged);
            menuItem3.Visible = this.menuItemAdvancedMode.Visible = PubFunc.SupportKingColorSimpleMode;
            this.toolBarButtonDoublePrintCari.Visible = PubFunc.SupportDoubleSidePrint;
            //this.toolBarButtonDoublePrintCari.Enabled = m_bDuringPrinting;
            AutoInkTestHelper.Load();

            toolBarButtonAuxiliaryControl.Visible = false;
#if !ALLWIN
            m_ToolBarColorSeperationPurge.Visible = toolBarButtonOnlineState.Visible = false;
            //toolBarButtonCamera.Visible = false;
#endif

            timerTempLog = new System.Windows.Forms.Timer();
            timerTempLog.Tick += new EventHandler(timerTempLog_Tick);
            timerTempLog.Interval = 3000;
        }

        private int _logTempTimes = 0; //温度log写入次数,每次重新上电重置为0
        private void timerTempLog_Tick(object sender, EventArgs e)
        {
            if (m_bFirstReady)
            {
                SRealTimeCurrentInfo realTimeCurrent = new SRealTimeCurrentInfo();
                uint Rmask = 0;
                Rmask |= 1 << (int) EnumVoltageTemp.TemperatureSet;
                Rmask |= 1 << (int) EnumVoltageTemp.TemperatureCur;
                if (CoreInterface.GetRealTimeInfo(ref realTimeCurrent, Rmask) != 0)
                {
                    string temp = string.Empty;
                    for (int k = 0; k < realTimeCurrent.cTemperatureCur.Length; k++)
                    {
                        temp += string.Format("({0},{1})", realTimeCurrent.cTemperatureSet[k],
                            realTimeCurrent.cTemperatureCur[k]);
                    }
                    LogWriter.WriteTemperatureLog(new string[] { temp }, _logTempTimes > 0);
                    _logTempTimes++;
                }
            }
        }

        private void SetBigImg()
        {
            ushort vid = 0;
            ushort pid = 0;
            CoreInterface.GetProductID(ref vid, ref pid);
            if (vid == (decimal)VenderID.GONGZENG && !m_bGongzheng)
            {
                ToolBarButton btn = this.m_ToolBarCommand.Buttons[3];
                this.m_ToolBarCommand.Buttons[3] = this.m_ToolBarCommand.Buttons[2];
                this.m_ToolBarCommand.Buttons[2] = btn;
                this.m_bGongzheng = true;
                UpdateViewMode((int)m_allParam.Preference.ViewModeIndex);//refresh icon
            }
            this.m_ToolBarCommand.Refresh();
        }

        #endregion

        #region 接口实现

        # region IPrinterChange 接口成员实现

        private bool _isFirstErrorAfterLoad = true;

        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            if (status == JetStatusEnum.Busy)
                isPrinting = true;
            else
            {
                if (status == JetStatusEnum.PowerOff
                    || status == JetStatusEnum.Offline
                    || status == JetStatusEnum.Ready
                    || status == JetStatusEnum.Initializing
                    )
                    isPrinting = false;
            }
            if (m_bOffline)
                status = JetStatusEnum.Offline;

            if (CoreInterface.CurBoardStatus == status)//
                return;

            CoreInterface.CurBoardStatus = status;
            if (remoteControlServer != null)
            {
                // 状态变化通知tcpip客户端
#if FHZL3D
                ProtocolFhzl returnFhzl = new ProtocolFhzl(TcpIpCmdFhzl.ResponsePackage,
                    (byte) GetInfoSubCmd.Status, ((int) curStatus).ToString());
#else
                ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                    (byte)GetInfoSubCmd.Status, ((int)CoreInterface.CurBoardStatus).ToString());
#endif
                remoteControlServer.SendCmdReturnValue(returnFhzl);
            }
            SetPrinterStatusChanged(status);
            if (status == JetStatusEnum.Error)
            {
                int errorcode = CoreInterface.GetBoardError();
                if (_isFirstErrorAfterLoad && SErrorCode.IsOnlyPauseError(errorcode) && m_MyMessageBox == null)
                {
                    //post 消息 统一处理流程,避免主板报错后才启动pm,导致需要用户处理的弹框式消息无法弹出.
                    SystemCall.PostMessage(this.Handle, m_KernelMessage, (int) CoreMsgEnum.ErrorCode, errorcode);
                    _isFirstErrorAfterLoad = false;
                }
                else
                {
                    OnErrorCodeChanged(errorcode);
                }
            }
            else
                OnErrorCodeChanged(0);
            m_ToolbarSetting.OnPrinterStatusChanged(status);
            if (_normalPrintWindow != null)
                _normalPrintWindow.OnPrinterStatusChanged(status);
            if (hapondMotorSetting != null)
                hapondMotorSetting.OnPrinterStatusChanged(status);
            if (status == JetStatusEnum.PowerOff)
            {
                m_IsFATAL = false;
                m_bFirstReady = false;
                timerTempLog.Stop();
                m_bFirstSetErrorFormat = true;
            }
            if (status != JetStatusEnum.PowerOff && m_bFirstSetErrorFormat)
            {
                m_bFirstSetErrorFormat = false;
                if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
                {
                    ScorpionCoreInterface.SetFwSupportErrorCode(PrinterStubC.Interface.ErrorFormat.New);
                }
            }
            if (status != JetStatusEnum.PowerOff && status != JetStatusEnum.Initializing && m_IsFATAL == false)
            {
#if ADD_HARDKEY
                if (SPrinterProperty.IsGongZeng() || SPrinterProperty.IsRuiZhi()) // 此功能按 厂商限定
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
                            //同步墨量
                            if (InkAreaStaticsHelper.CanSynchronizeInkAndArea()) // 板卡是否支持同步功能
                            {
                                if (InkAreaStaticsHelper.BoardHasChanged()) // 判断当前数据文件名是否和板卡id一致
                                {
                                    string[] files = InkAreaStaticsHelper.GetAllInkStaticsFiles();
                                    string curFilePath = files[0];
                                    //询问是否同步密码
                                    DialogResult dr =
                                        MessageBox.Show(ResString.GetResString("QuestionSynchronizeInkAndArea"),
                                            ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dr == DialogResult.Yes)
                                    {
                                        if (File.Exists(curFilePath))
                                        {
                                            //同步文件内容到板卡里
                                            FileStream fs = new FileStream(curFilePath, FileMode.Open);
                                            BinaryReader br = new BinaryReader(fs);
                                            byte[] ink = new byte[Marshal.SizeOf(typeof (InkOfMonths))];
                                            byte[] area = new byte[Marshal.SizeOf(typeof (AreaOfMonths))];
                                            ink = br.ReadBytes(ink.Length);
                                            area = br.ReadBytes(area.Length);
                                            br.Close();
                                            fs.Close();
                                            if (InkAreaStaticsHelper.SetInkQuantity(ink) &&
                                                InkAreaStaticsHelper.SetPrintArea(area))
                                            {
                                                MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaSuccess"),
                                                    ResString.GetProductName());
                                            }
                                            else
                                            {
                                                MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaFail"),
                                                    ResString.GetProductName());
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(ResString.GetResString("SynchronizeInkAndAreaFail"),
                                                ResString.GetProductName());
                                        }
                                    }
                                    //同步墨量,生成新的数据文件
                                    InkAreaStaticsHelper.SynchronizeInkAndArea(BYHXSoftLock.GetDateTime());
                                    if (File.Exists(curFilePath))
                                        File.Delete(curFilePath); //删除此数据文件,防止同一目录下出现多个文件的情况
                                    byte[] val = new byte[4];
                                    if (EpsonLCD.GetFlagOfBoard(val))
                                    {
                                        string flag = System.Text.Encoding.Default.GetString(val);
                                        if (flag != "BYHX") // 
                                        {
                                            EpsonLCD.SetFlagOfBoard();
                                        }
                                    }
                                }
                                else
                                {
                                    //同步墨量
                                    InkAreaStaticsHelper.SynchronizeInkAndArea(BYHXSoftLock.GetDateTime());
                                    string[] files = InkAreaStaticsHelper.GetAllInkStaticsFiles();
                                    if (files.Length > 1)
                                    {
                                        InkAreaStaticsHelper.ClearInkStaticsFiles();
                                    }
                                }
                                InkAreaStaticsHelper.SetCurrentDate(BYHXSoftLock.GetDateTime());
                            }
                        }
                    }
                }
                else
                {
                    m_IsFATAL = true;
                }
#elif INWEAR_REVO
                if (ScorpionCoreInterface.GetSupportInwearHeadLimite())
                {
                    uint boardId = 0;
                    SBoardInfo sBoardInfo = new SBoardInfo();
                    if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                    {
                        boardId = sBoardInfo.m_nBoardSerialNum;
                        if (CoreInterface.IsFatal(boardId, 1))
                        {
                            m_IsFATAL = true;
                        }
                    }
                }
#else
                {
                    m_IsFATAL = true;
                }
#endif
            }
            m_JobListForm.OnPrinterStatusChanged(status, m_IsFATAL);
            if (m_bFirstReady == false && status == JetStatusEnum.Ready)
            {
                SetBigImg();

                m_bFirstReady = true;
                _logTempTimes = 0;
                if (PubFunc.GetUserPermission() == (int) UserPermission.SupperUser)
                    timerTempLog.Start();

                PubFunc.RegisteredToMainboard();
#if !LIYUUSB
                ////检测是否是奥威的主板以及喷头类型是否匹配

                //if (m_allParam.PrinterProperty.IsDisplayForm())
                //{
                //    if (m_allParam.Preference.bShowAttentionOnLoad)
                //    {
                //        TipsWizard tipWizard = new TipsWizard(m_allParam);
                //        tipWizard.ShowDialog();
                //    }
                //}

                if (!m_JobListForm.PrintJobTaskIsWorking)
                {
                    string vol = ResString.GetResString("FW_Voltage");
                    byte[] buffer = System.Text.Encoding.Unicode.GetBytes(vol);
                    int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
                    CoreInterface.SetFWVoltage(buffer, buffer.Length, lcid);
                }

                ///Check version
                ///

                //				SBoardInfo sBoardInfo = new SBoardInfo();
                //				if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
                //				{
                //					const int MINI_MAINBOARD_VERSION = 0x00020200;
                //					SFWVersion fwv= new SFWVersion(sBoardInfo.m_nBoradVersion);
                //					SFWVersion min_fwv= new SFWVersion(MINI_MAINBOARD_VERSION);
                //					if( (((fwv.m_nMainVersion <<8)+ fwv.m_nSubVersion)<<8) < MINI_MAINBOARD_VERSION)
                //					{
                //						string info = "";
                //						string mPrintingFormat = SErrorCode.GetEnumDisplayName(typeof(Software),Software.VersionNoMatch);
                //						string curVersion = fwv.m_nMainVersion + "." + fwv.m_nSubVersion;
                //						string minVersion = min_fwv.m_nMainVersion + "." + min_fwv.m_nSubVersion;
                //						info += "\n" + string.Format(mPrintingFormat,curVersion,minVersion);
                //						MessageBox.Show(this, info,"",MessageBoxButtons.OK);
                //						//m_bExitAtStart = true;
                //					}
                //				}
#else
				CoreInterface.VerifyHeadType();
				//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
                BYHXSoftLock.m_DongleKeyAlarm.FirstReadyShakeHand();
                //				if(SPrinterProperty.EPSONLCD_DEFINED)
                //				{
                //					this.OnPrinterSettingChange(m_allParam.PrinterSetting);
                //				}
                if (!SPrinterProperty.NoShowMeasureMsgWhenFirstReady())
                    this.ShowMeasureQuestionForm(this.Visible);

                bool m_bNewXaar382 = SPrinterProperty.IsNewXaar382(m_allParam.PrinterProperty.ePrinterHead);
                if (m_bNewXaar382)
                {
                    SRealTimeCurrentInfo_382 m_SRT_382 = new SRealTimeCurrentInfo_382();
                    float minnum = 0, maxnum = 100;
                    // this.numericUpDownTargetTemp.Minimum;this.numericUpDownTargetTemp.Maximum
                    if (CoreInterface.Get382RealTimeInfo(ref m_SRT_382) != 0)
                    {
                        int m_HeadNum = m_allParam.PrinterProperty.nHeadNum;

                        bool bTargetTempError = false;
                        for (int i = 0; i < m_HeadNum; i++)
                        {
                            float targetTemp = m_SRT_382.cTargetTemp[i];
                            if (targetTemp < minnum || targetTemp > maxnum)
                            {
                                m_SRT_382.cTargetTemp[i] = 40;
                                bTargetTempError = true;
                            }
                        }
                        if (bTargetTempError)
                        {
                            CoreInterface.Set382RealTimeInfo(ref m_SRT_382);
                        }
                    }
                }

                USER_SET_INFORMATION userInfo = new USER_SET_INFORMATION(true);
                bool isGetOk = CoreInterface.GetUserSetInfo(ref userInfo) == 1;
                if (isGetOk)
                {
                    menuItemAutoStopPumpInk.Checked = userInfo.PumpType == CoreConst.ENABLE_CONTINU_PUMP_INK;
                }
                EpsonLCD.SetPeripheralExtendedSettings(m_allParam);
#if INKTYPE
                byte selectedInkType = 0;
                if (PubFunc.GetCurInkType(ref selectedInkType))
                {
                    toolBarButtonSwitchInk.ImageIndex = selectedInkType == 1 ? 25 : 26;
                    int ret = PubFunc.SwitchInkType(m_allParam, selectedInkType, selectedInkType);
                    if (ret == 1)
                    {
                        LogWriter.WriteLog(new string[] { "SwitchInkType ok!" }, true);
                    }
                    else if (ret == -1)
                    {
                        MessageBox.Show(string.Format("SwitchInkType Error!cmd {0},index {1}", 0x68, 0x02));
                        return;
                    }
                    else if (ret == -2)
                    {
                        MessageBox.Show(string.Format("UpdatePrinterSetting  SwitchInkType Error!cmd {0}", 0x04));
                    }
                }
                else
                {
                    LogWriter.WriteLog(new string[] { "GetCurInkType fail!" }, true);
                }
#endif
                // 软件保存基准色等参数,一上电需要同步给dll
                SSeviceSetting sSeviceSet = m_allParam.SeviceSetting;
                CoreInterface.SetSeviceSetting(ref sSeviceSet);

                if ((m_allParam.PrinterProperty.IsDocan() || m_allParam.PrinterProperty.IsColorJetRoolUv())
                    && m_allParam.PrinterProperty.bSupportZendPointSensor && !PubFunc.IsDocan_Belt_Textile())
                {
                    float fZSpace = m_allParam.PrinterSetting.sBaseSetting.fZSpace;
                    float fPaperThick = m_allParam.PrinterSetting.sBaseSetting.fPaperThick;
                    float fMeshHight = 0;
                    CoreInterface.MoveZ(01, fZSpace, fPaperThick + fMeshHight);
                    LogWriter.SaveOptionLog(string.Format("move z to work pos after ready; fZSpace={0};fPaperThick ={1};fPulsePerInchZ={2}", fZSpace, fPaperThick, m_allParam.PrinterProperty.fPulsePerInchZ));
                }
                if(m_toolBarButtonRetractableCloth.Visible)
                    EpsonLCD.GetGZClothMotionParam(ref GZClothMotionParam);
                if (m_allParam.PrinterProperty.bSupportDoubleMachine)
                {
                    //东川、Colorjet 板卷一体机
                    byte selectedType = 0;
                    if (EpsonLCD.GetRollOrFlat(ref selectedType))
                    {
                        switch (selectedType)
                        {
                            case 1:
                                m_toolBarButtonRollFlat.ImageIndex = 29;
                                break;
                            case 2:
                                m_toolBarButtonRollFlat.ImageIndex = 30;
                                break;
                            default:
                                MessageBox.Show(string.Format("Roll Or Flat Error!"));
                                break;
                        }
                    }
                }
                //更新jobprint.dll的boardid,用于传给fw做验证,验证与否fw决定
                {
                    uint boardId = 0;
#if INWEAR_REVO
                    SBoardInfo sBoardInfo = new SBoardInfo();
                    if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                    {
                        boardId = sBoardInfo.m_nBoardSerialNum;
                    }
#else
                    byte[] infos = new byte[19];
                    BYHX_SL_RetValue ret = BYHXSoftLock.GetDongleInfo(ref infos);
                    if (ret == BYHX_SL_RetValue.SUCSESS)
                    {
                        byte[] dtV = new byte[4];
                        Buffer.BlockCopy(infos, 4, dtV, 0, dtV.Length);
                        boardId = BitConverter.ToUInt32(dtV, 0);
                    }
#endif
                    CoreInterface.BoardId = boardId;
                    CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                }
                // 如果支持z终点传感器,则从fw获取z最大行程
                if (m_allParam.PrinterProperty.bSupportZendPointSensor)
                {
                    int xMax = 0;
                    int yMax = 0;
                    int zMax = 0;
                    int ret = CoreInterface.QueryPrintMaxLen(ref xMax, ref yMax, ref zMax);
                    if (ret == 1 && m_allParam.PrinterProperty.fPulsePerInchZ > 0)
                    {
                        m_allParam.PrinterSetting.sExtensionSetting.zMaxLength = zMax/
                                                                                 m_allParam.PrinterProperty
                                                                                     .fPulsePerInchZ;
                        LogWriter.SaveOptionLog(string.Format("Mainform first ready xMaxPos={0};yMaxPos={1};zMaxPos={2};fPulsePerInchZ={3}", 
                            xMax, yMax, zMax, m_allParam.PrinterProperty.fPulsePerInchZ));
                    }
                }
#if INWEAR_REVO
                if (!ScorpionCoreInterface.GetSupportInwearHeadLimite())
                {
                    MessageBox.Show(ResString.GetResString("strFwVersionError"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#endif
            }
            
            UpdateButtonStates(status);
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_allParam.PrinterProperty = sp;
            m_ToolbarSetting.OnPrinterPropertyChange(sp);
            m_PreviewAndInfo.OnPrinterPropertyChange(sp);
            m_JobListForm.OnPrinterPropertyChange(sp);
            OnPrinterPropertyChange_Toolbar(sp);
            if (m_FuncSettingForm != null)
                m_FuncSettingForm.OnPrinterPropertyChange(sp);

            bool bFacUser = (PubFunc.GetUserPermission() != (int)UserPermission.Operator);
            if (bFacUser)
                m_MenuItemDebug.Visible = true;
            else
                m_MenuItemDebug.Visible = false;
            bool bNoRealPage =
                (
                (sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_80W)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_40W)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_126)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_500)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_35W)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_70W)
                //				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl)
                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_1001_GS6)
                || (sp.ePrinterHead == PrinterHeadEnum.EGen5)
                );
            if (bNoRealPage)
            {
                m_MenuItemRealTimeChart.Visible = this.m_MenuItemRealTime.Visible = false;
            }
            else
                m_MenuItemRealTimeChart.Visible = this.m_MenuItemRealTime.Visible = true;

            this.m_MenuItemDemoPage.Visible = false;
            this.m_MenuItemHWSetting.Visible = true;// !SPrinterProperty.IsEpson(sp.ePrinterHead);
            if (UIFunctionOnOff.OnlyFacUserShowRealTime && !bFacUser) //171212 只有工厂调试权限才显示
            {
                m_MenuItemRealTimeChart.Visible = this.m_MenuItemRealTime.Visible = false;
            }
            if (!bFacUser)
            {
                this.m_MenuItemHWSetting.Visible = true;
            }
            this.m_MenuItemSave.Visible = this.m_MenuItemSaveToPrinter.Visible =
                this.m_MenuItemLoad.Visible = this.m_MenuItemLoadFromPrinter.Visible = !sp.EPSONLCD_DEFINED;
            this.m_MenuItemCalibraion.Visible = !sp.EPSONLCD_DEFINED || (PubFunc.GetUserPermission() == (int)UserPermission.SupperUser);
            m_MenuItemInkStatistics.Visible = (SPrinterProperty.IsGongZeng() || SPrinterProperty.IsRuiZhi()) && InkAreaStaticsHelper.CanSynchronizeInkAndArea();

            if (!sp.bSupportUV)
            {
                this.m_MenuItemUVSetting.Visible = false;
            }
            else
                this.m_MenuItemUVSetting.Visible = true;
            // fw已不支持此功能,界面彻底隐藏
            this.m_MenuItemWaveFormSetting.Visible = false;//sp.IsKonicHead()&&(!PubFunc.IsFhzl3D());
            this.UpdateFormHeaderText();

            TransformMeasurePaperIcon();
            if (PubFunc.SupportKingColorSimpleMode)
                SwitchToAdvancedMode(PubFunc.IsKingColorAdvancedMode);
            this.m_ToolBarButtonSand.Visible = PubFunc.Is3DPrintMachine();// && PubFunc.IsFhzl3D();
            //this.m_ToolBarButtonSingleClean.Visible = PubFunc.IsColorJet_Belt_Textile(); //是否显示靠配置文件决定
            menuItemMotorParameters.Visible = PubFunc.IsHapond();
            toolBarButtonPretreatment.Visible = SPrinterProperty.IsFloraT50OrT180() && !SPrinterProperty.IsFlora_BELT_TEXTILE1526();
            m_MenuItemPassword.Visible = true;// !SPrinterProperty.IsInwearSimpleUi() || PubFunc.GetUserPermission() == (int)UserPermission.SupperUser;
            menuItemDoubleYAxisSetting.Visible = UIFunctionOnOff.SupportDoubleYAxis;
        }
        private bool m_bMeasurePaperIconTransformed = false;
        private bool _bStopPumpInkWhenTimeout = true;

        private void TransformMeasurePaperIcon()
        {
            // 如果支持Z轴测量，则主界面介质测量按钮为测介质高度。旋转图标以匹配功能语义
            if (!SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead)
                && m_allParam.PrinterProperty.IsZMeasurSupport && !m_bMeasurePaperIconTransformed)
            {
                Image img = this.m_ToolbarImageList.Images[16];
                Image temp = new Bitmap(img.Width, img.Height);
                Graphics gra = Graphics.FromImage(temp);
                gra.Transform = new Matrix(0, 1, -1, 0, temp.Width, 0); //旋转90度
                gra.DrawImage(img, 0, 0);
                gra.Dispose();
                this.m_ToolbarImageList.Images[16] = temp;

                this.m_ToolBarButtonMeasurePaper.ToolTipText = ResString.GetResString("UiToolBar_MeasurePaperHeight");
                m_bMeasurePaperIconTransformed = true;
                this.m_ToolBarCommand.Refresh();
            }
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_allParam.PrinterSetting = ss;
            m_ToolbarSetting.OnPrinterSettingChange(m_allParam, this.m_allParam.EpsonAllParam);
            m_PreviewAndInfo.OnPrinterSettingChange(ss);
            m_JobListForm.OnPrinterSettingChange(ss);
            if (m_FuncSettingForm != null)
                m_FuncSettingForm.OnPrinterSettingChange(m_allParam);
        }
        public void OnPowerONGetSettingsForEpson()
        {
            if (m_allParam.PrinterProperty.EPSONLCD_DEFINED)
            {
                //从FW读取epson的各项参数,初始化m_epsinAllParam
                EpsonLCD.GetCalibrationSetting(ref this.m_allParam.PrinterSetting.sCalibrationSetting, false);
                EpsonLCD.GetCaliConfig(ref m_allParam.EpsonAllParam.sCaliConfig);
                EpsonLCD.GetCleaningOption(ref m_allParam.EpsonAllParam.sCLEANPARA);
                EpsonLCD.GetEPR_FactoryData_Ex(ref m_allParam.EpsonAllParam.sEPR_FactoryData_Ex);
                EpsonLCD.GetHeadparameter(ref m_allParam.EpsonAllParam.headParameterPercent);
                EpsonLCD.GetMainUI_Param(ref m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
                EpsonLCD.GetMedia_Info(ref m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
                EpsonLCD.GetPrint_Quality(ref m_allParam.EpsonAllParam.sUSB_Print_Quality);
                switch (this.m_allParam.EpsonAllParam.sUSB_Print_Quality.PrintQuality)
                {
                    case (byte)EpsonFeatherType.None:
                        this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 0;
                        break;
                    case (byte)EpsonFeatherType.Small:
                        this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 20;
                        break;
                    case (byte)EpsonFeatherType.Medium:
                        this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 40;
                        break;
                    case (byte)EpsonFeatherType.Large:
                        this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 100;
                        break;
                }
                this.m_allParam.PrinterSetting.sBaseSetting.fPaperWidth = m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaWidth;
                this.m_allParam.PrinterSetting.sFrequencySetting.fXOrigin = m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin;

                CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
                LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "OnPowerONGetSettingsForEpson");
            }

        }
        public void OnPreferenceChange(UIPreference up)
        {
            m_allParam.Preference = up;
            m_ToolbarSetting.OnPreferenceChange(up);
            m_PreviewAndInfo.OnPreferenceChange(up);
            m_JobListForm.OnPreferenceChange(up);
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);
        }

        public bool LoadJobList()
        {
            return m_JobListForm.LoadJobList();
        }
        public bool SaveJobList()
        {
            return m_JobListForm.SaveJobList();
        }
        public AllParam GetAllParam()
        {
            return m_allParam;
        }

        public void NotifyUIParamChanged()
        {
            m_JobListForm.IsUsedJobMode = false;
            m_JobListForm.IsUsedJobMediaMode = false;
            m_JobListForm.JobMode = new JobMode();
            if (m_ToolbarSetting.comboBoxPringConfigList.Text.ToLower() != "default")
            {
                JobMode curJobMode = m_ToolbarSetting.GetJobCofigByName();
                if (curJobMode != null)
                {
                    m_JobListForm.IsUsedJobMode = true;
                    m_JobListForm.JobMode = curJobMode;
                }
            }
            m_JobListForm.JobMediaMode = new JobMediaMode() { Item = new MediaConfig() { BaseStep = m_allParam.PrinterSetting.sCalibrationSetting.nStepPerHead} };
            if (m_ToolbarSetting.comboBoxMediaMode.Text.ToLower() != "default")
            {
                JobMediaMode curJobMode = m_ToolbarSetting.GetJobMediaCofigByName();
                if (curJobMode != null)
                {
                    m_JobListForm.IsUsedJobMediaMode = true;
                    m_JobListForm.JobMediaMode = curJobMode;
                }
            }

            m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam, ref this.m_allParam.EpsonAllParam);
            m_ToolbarSetting.OnGetPreference(ref m_allParam.Preference);
            CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
            if (SPrinterProperty.SurpportDoublePlatform())
            {
                //设置彩神t-shirt打印平台选择
                SSeviceSetting sSeviceSet = m_allParam.SeviceSetting;
                if (m_allParam.Preference.ScanningAxis == CoreConst.AXIS_X
                    || m_allParam.Preference.ScanningAxis == CoreConst.AXIS_4)
                {
                    sSeviceSet.scanningAxis = m_allParam.Preference.ScanningAxis;
                }
                else
                {
                    sSeviceSet.scanningAxis = CoreConst.AXIS_X;
                }
                CoreInterface.SetSeviceSetting(ref sSeviceSet);
            }
            LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "NotifyUIParamChanged");
            if (m_allParam.PrinterProperty.EPSONLCD_DEFINED)
                EpsonLCD.SetMainUI_Param(this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
        }

        public void NotifyUIKeyDownAndUp(Keys keyData, bool bKeydown)
        {
            if (bKeydown)
            {
                MainForm_KeyDownEvent(keyData);
            }
            else
            {
                MainForm_KeyUpEvent(keyData);
            }
        }

        public void OnSwitchPreview()
        {
            m_JobListForm.OnSwitchPreview();
        }

        public void NotifyUICalibrationExit(bool bSave, bool bClose)
        {
            m_MenuItemCalibraion.Enabled = true;
            if (bSave)
            {
                //OnPrinterSettingChange(m_allParam.PrinterSetting);
                m_ToolbarSetting.OnPrinterSettingChange(m_allParam, this.m_allParam.EpsonAllParam);
            }
            if (bClose)
            {
                m_wizard = null;
            }
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            OnPrinterStatusChanged(printerStatus);
        }

        public CLockQueue GetLockQueue()
        {
            return this.m_JobListForm.LockQueue;
        }
        #endregion

        #region IMessageFilter 接口成员实现

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x020A)    //MouseWheel
                return true;
            //   TODO:     添加   comboNoWheel.PreFilterMessage   实现   
            return false;

        }
        #endregion

        #endregion

        #region 方法

        #region private
        private void StartWithInitComponent()
        {
            m_JobListForm.SetPreviewInfo(m_PreviewAndInfo);
            m_allParam = new AllParam();



            m_JobListForm.SetPrinterChange(this, this.Handle, this.m_KernelMessage);
            m_ToolbarSetting.SetPrinterChange(this);
            m_PreviewAndInfo.SetPrinterChange(this);

        }

        private void UpdateButtonStates(JetStatusEnum status)
        {
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
                status = JetStatusEnum.PowerOff;
            LogWriter.SaveOptionLog(string.Format("(status = {0}", status));

            if (status == JetStatusEnum.Error && SErrorCode.IsWarningError(CoreInterface.GetBoardError()))
            {
                LogWriter.SaveOptionLog("(status == JetStatusEnum.Error && SErrorCode.IsWarningError(CoreInterface.GetBoardError())");
                return;
            }

            PrinterOperate printerOperate = PrinterOperate.UpdateByPrinterStatus(status);
            switch (status)
            {
                case JetStatusEnum.Error:
                    {
                        printerOperate.CanPause = m_LastOperate.CanPause;
                        printerOperate.CanResume = m_LastOperate.CanResume;
                        break;
                    }
                case JetStatusEnum.Cleaning:
                case JetStatusEnum.Moving:
                    {
                        printerOperate.CanPause = m_LastOperate.CanPause;
                        printerOperate.CanResume = m_LastOperate.CanResume;
                        break;
                    }
            }

            m_toolBarButtonRetractableCloth.Enabled = status == JetStatusEnum.Ready && GZClothMotionParam.enable != 0;

            m_LastOperate = printerOperate;

            m_MenuItemRealTimeChart.Enabled = m_MenuItemRealTime.Enabled = printerOperate.CanSaveLoadSettings;
            m_MenuItemUVSetting.Enabled = printerOperate.CanSaveLoadSettings;
            m_MenuItemTools.Enabled = true;//printerOperate.CanUpdate;
            m_MenuItemSaveToPrinter.Enabled = printerOperate.CanSaveLoadSettings;
            m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;
            m_MenuItemCalibraion.Enabled = printerOperate.CanUpdate;
            //m_MenuItemInkStatistics.Enabled = printerOperate.CanSaveLoadSettings;

            button_Print.Enabled=m_ToolBarButtonPrint.Enabled = m_MenuItemPrint.Enabled = printerOperate.CanPrint && m_MenuItemCalibraion.Enabled && !m_bFrobidPrinter && m_IsFATAL;
            LogWriter.SaveOptionLog(string.Format("printerOperate.CanPrint={0},m_MenuItemCalibraion.Enabled ={1};m_bFrobidPrinter={2};m_IsFATAL={3}",
                printerOperate.CanPrint , m_MenuItemCalibraion.Enabled , m_bFrobidPrinter , m_IsFATAL));
            bool bColorJet = PubFunc.IsColorJet_Belt_Textile();
            m_ToolBarButtonSingleClean.Enabled = m_ToolBarButtonSingleClean.Pushed || printerOperate.CanClean || bColorJet;
            button_Spray.Enabled=m_ToolBarButtonSpray.Enabled = m_ToolBarButtonSpray.Pushed || printerOperate.CanClean;
            m_ToolBarButtonAutoClean.Enabled = m_ToolBarButtonAutoClean.Pushed || printerOperate.CanClean;

            m_ToolBarButtonLeft.Enabled = printerOperate.CanMoveLeft;
            if (!button_Left_MouseDownFlag)
            {
                button_Left.Enabled = printerOperate.CanMoveLeft;
            }
            m_ToolBarButtonRight.Enabled = printerOperate.CanMoveRight;
            if (!button_Right_MouseDownFlag)
            {
                button_Right.Enabled = printerOperate.CanMoveRight;
            }
            m_ToolBarButtonForward.Enabled = printerOperate.CanMoveForward;
            if (!button_Forward_MouseDownFlag)
            {
                button_Forward.Enabled = printerOperate.CanMoveForward;
            }
            m_ToolBarButtonBackward.Enabled = printerOperate.CanMoveBackward;
            if (!button_Backward_MouseDownFlag)
            {
                button_Backward.Enabled = printerOperate.CanMoveBackward;
            }
            m_ToolBarButtonDownZ.Enabled = printerOperate.CanMoveDown;
            m_ToolBarButtonUpZ.Enabled = printerOperate.CanMoveUp;
            m_ToolBarButtonGoHomeZ.Enabled = printerOperate.CanMoveBackward && printerOperate.CanMoveForward;
            m_ToolBarButtonGoHomeY.Enabled = printerOperate.CanMoveForward && printerOperate.CanMoveBackward;
            button_MoveToHome.Enabled=m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveOriginal;
            button_MoveToHome.Enabled=m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveLeft && printerOperate.CanMoveRight;

            button_NozzleCheck.Enabled=m_ToolBarButtonCheckNozzle.Enabled = printerOperate.CanPrint;
            m_ToolBarButtonMeasurePaper.Enabled = (printerOperate.CanPrint && m_allParam.PrinterProperty.bSupportPaperSensor);
            m_ToolBarButtonSetOrigin.Enabled = printerOperate.CanPrint;
            m_ToolBarButtonSetOriginY.Enabled = printerOperate.CanPrint;

            button_Cancel.Enabled=m_ToolBarButtonAbort.Enabled = printerOperate.CanAbort || m_JobListForm.WaitingPauseBetweenLayers || (m_allParam.PrinterProperty.IsZMeasurSupport ? printerOperate.CanMoveStop : false);
            button_Pause.Enabled=m_ToolBarButtonPauseResume.Enabled = (printerOperate.CanPause || printerOperate.CanResume);
            //m_ToolBarButtonPause.Enabled = printerOperate.CanPause; //???????
            //m_ToolBarButtonResume.Enabled = printerOperate.CanResume; //??????
            button_Setting.Enabled=this.m_MenuItemEdit.Enabled = this.m_ToolBarButtonEdit.Enabled = m_allParam.PrinterProperty.EPSONLCD_DEFINED ? m_bFirstReady : true;
            button_MoveToOrigin.Enabled=m_ToolBarButtonZero.Enabled = printerOperate.CanOffline;
            m_ToolBarButtonSand.Enabled = printerOperate.CanPrint;
            toolBarButtonSwitchInk.Enabled = printerOperate.CanPrint;
            m_ToolBarButtonStop.Enabled = printerOperate.CanMoveStop;
            m_toolBarButtonRollFlat.Enabled = printerOperate.CanPrint;
            toolBarButtonPretreatment.Enabled = printerOperate.CanPrint;

        }

        private void SetPrinterStatusChanged(JetStatusEnum status)
        {
            string info = ResString.GetEnumDisplayName(typeof(JetStatusEnum), status);
            this.m_StatusBarPanelJetStaus.Text = info;
            if (CoreInterface.Printer_IsOpen() == 0)
            {
                this.m_StatusBarPanelPercent.Text = "";
            }
            m_ToolbarSetting.SetPrinterStatusChanged(status);
            this.m_PreviewAndInfo.SetPrinterStatusChanged(status, m_JobListForm.WaitingPauseBetweenLayers);
            this.m_ToolbarSetting.SetPrinterStatusChanged(status, m_JobListForm.WaitingPauseBetweenLayers);
            this.UpdateNotifyIconInfo(status);
            if (m_wizard != null)
            {
                m_wizard.SetPrinterStatusChanged(status);
            }
            if (m_FuncSettingForm != null)
            {
                m_FuncSettingForm.SetPrinterStatusChanged(status);
            }
            if (m_hwForm != null)
            {
                m_hwForm.SetPrinterStatusChanged(status);
            }

        }


        private int GetSpeedWithDir(MoveDirectionEnum dir)
        {
            if (dir == MoveDirectionEnum.Left || dir == MoveDirectionEnum.Right)
            {
                return m_allParam.PrinterSetting.sMoveSetting.nXMoveSpeed;
            }
            else if (dir == MoveDirectionEnum.Up || dir == MoveDirectionEnum.Down)
            {
                return m_allParam.PrinterSetting.sMoveSetting.nYMoveSpeed;
            }
            else if (dir == MoveDirectionEnum.Up_Z || dir == MoveDirectionEnum.Down_Z)
            {
                return m_allParam.PrinterSetting.sMoveSetting.nZMoveSpeed;
            }
            else
                return m_allParam.PrinterSetting.sMoveSetting.n4MoveSpeed;

        }
        private bool MainForm_KeyDownEvent(Keys keyData)
        {
            bool blnProcess = false;
            if (keyData == Keys.Left)
            {
                if (m_ToolBarButtonLeft.Enabled)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Left;
                    dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonLeft.Pushed = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Right)
            {
                if (m_ToolBarButtonRight.Enabled)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Right;
                    dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonRight.Pushed = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Up)
            {
                if (m_ToolBarButtonBackward.Enabled)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Up;
                    dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonBackward.Pushed = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Down)
            {
                if (m_ToolBarButtonForward.Enabled)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Down;
                    dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonForward.Pushed = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            return blnProcess;
        }

        private bool MainForm_KeyUpEvent(Keys keyData)
        {
            bool blnProcess = false;
            if ((keyData == Keys.Left)
                || (keyData == Keys.Right)
                || (keyData == Keys.Up)
                || (keyData == Keys.Down))
            {
                AutoStopMoveAndUpdateUI();
                blnProcess = true;
            }
            return blnProcess;
        }

        private void AutoStopMoveAndUpdateUI()
        {
            if (m_bSendMoveCmd)
            {
                CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
                m_ToolBarButtonLeft.Pushed = false;
                m_ToolBarButtonRight.Pushed = false;
                m_ToolBarButtonBackward.Pushed = false;
                m_ToolBarButtonForward.Pushed = false;
                m_ToolBarButtonDownZ.Pushed = false;
                m_ToolBarButtonUpZ.Pushed = false;
                m_bSendMoveCmd = false;
            }
        }


        private void UpdateCoreBoard(string m_UpdaterFileName)
        {
#if !LIYUUSB
            bool bRead = false;
            byte[] buffer = null;
            int fileLen = 0;
            try
            {
                const int USB_EP2_MIN_PACKAGESIZE = 1024;

                FileStream fileStream = new FileStream(m_UpdaterFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                fileLen = (int)fileStream.Length;
                int buffersize = (fileLen + USB_EP2_MIN_PACKAGESIZE - 1) / USB_EP2_MIN_PACKAGESIZE * USB_EP2_MIN_PACKAGESIZE;
                buffer = new byte[buffersize];
                int readBytes = 0;

                fileStream.Seek(0, SeekOrigin.Begin);
                readBytes = binaryReader.Read(buffer, 0, fileLen);
                Debug.Assert(fileLen == readBytes);

                binaryReader.Close();
                fileStream.Close();
                bRead = true;
            }
            catch { }
            if (bRead)
            {
                //CoreInterface.SetMessageWindow(this.Handle, m_MessageUpdater);
                CoreInterface.BeginUpdating(buffer, fileLen);
            }
#else
			CoreInterface.BeginUpdateMotion(m_UpdaterFileName);
#endif
        }


        private void OnSingleClean()
        {
            bool bColorJet = PubFunc.IsColorJet_Belt_Textile() ;
            if (bColorJet || PubFunc.IsGzSurrportManualClean(m_allParam.PrinterProperty) || PubFunc.IsALLWIN_ROLL_TEXTILE() || PubFunc.IsDocan_Belt_Textile())
            {
                ShowManualCleaningForm(bColorJet);
            }
            else
            {
                CleanForm form = new CleanForm();
                form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                if (CoreInterface.SendJetCommand((int)JetCmdEnum.EnterSingleCleanMode, 0) == 1)
                    form.ShowDialog(this);
            }
        }

        /// <summary>
        /// 手动清洗对话框显示
        /// </summary>
        /// <param name="bColorJet"></param>
        private void ShowManualCleaningForm(bool bColorJet)
        {
            ManualCleanForm form = new ManualCleanForm(m_allParam.PrinterProperty);
            form.OnPreferenceChange(m_allParam.Preference);
            form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            form.OnPrinterSettingChange(m_allParam.PrinterSetting, m_allParam.PrinterProperty);
           DialogResult dr = form.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                form.OnGetPrinterSetting(ref m_allParam.PrinterSetting);
            }
        }

        private void OnPrinterPropertyChange_Toolbar(SPrinterProperty sp)
        {
            ushort pid, vid;
            pid = vid = 0;
            bool isSkyShip = false;
            bool isSimpleUV = SPrinterProperty.IsSimpleUV();
            bool isInwearSimpleUi = SPrinterProperty.IsInwearSimpleUi();
            int ret = CoreInterface.GetProductID(ref vid, ref pid);
            if (ret != 0)
            {
                isSkyShip = vid == (ushort)VenderID.SKYSHIP || vid == (ushort)VenderID.SKYSHIP_FLAT_UV; //Docan
            }
            if (sp.bSupportZMotion)
            {
                m_ToolBarButtonDownZ.Visible = true;
                m_ToolBarButtonUpZ.Visible = true;
                m_ToolBarButtonGoHomeZ.Visible = true;
            }
            else
            {
                m_ToolBarButtonDownZ.Visible = false;
                m_ToolBarButtonUpZ.Visible = false;
                m_ToolBarButtonGoHomeZ.Visible = false;
            }
            if (sp.bSupportHandFlash)
            {
                m_ToolBarButtonSpray.Style = ToolBarButtonStyle.ToggleButton;
                m_ToolBarButtonSpray.Visible = !SPrinterProperty.IsEpson(sp.ePrinterHead) && !isSkyShip && !isSimpleUV;
            }
            else
            //if(sp.bSupportAutoClean == false)
            {
                m_ToolBarButtonSpray.Style = ToolBarButtonStyle.PushButton;
                m_ToolBarButtonSpray.Visible = !SPrinterProperty.IsEpson(sp.ePrinterHead) && !isSkyShip && (!isSimpleUV || sp.IsDocan());
            }
            //else
            //	m_ToolBarButtonSpray.Visible = false;

            m_ToolBarButtonAutoClean.Visible = sp.bSupportAutoClean && !isSkyShip && !isSimpleUV;

            if (sp.eSingleClean == SingleCleanEnum.None || SPrinterProperty.IsEpson(sp.ePrinterHead) || isSkyShip || sp.IsALLWIN_512i_HighSpeed())
            {
                m_ToolBarButtonSingleClean.Visible = false;
            }
            else if (sp.eSingleClean == SingleCleanEnum.PureManual)
            {
                m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.ToggleButton;
                m_ToolBarButtonSingleClean.Visible = true;
            }
            else
            {
                m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.PushButton;
                m_ToolBarButtonSingleClean.Visible = true;
            }

            if (sp.bSupportPaperSensor == true && !sp.IsGongZengEpson())
            //if(true)
            {
                m_ToolBarButtonMeasurePaper.Visible = true;
            }
            else
                m_ToolBarButtonMeasurePaper.Visible = false;
            if (sp.nMediaType == 0)
            {
                m_ToolBarButtonGoHomeY.Visible = false;
                m_ToolBarButtonSetOriginY.Visible = false;
            }
            else
            {
                m_ToolBarButtonGoHomeY.Visible = true;
                m_ToolBarButtonSetOriginY.Visible = (sp.nMediaType != 0) && !isSimpleUV;
            }
            m_ToolBarButtonSetOrigin.Visible = !isSimpleUV;
            m_ToolBarButtonZero.Visible = SPrinterProperty.IsSurportCapping(); // !isSkyShip;//印可丽已经将此功能移到panel上了
            m_toolBarButtonRollFlat.Visible = sp.bSupportDoubleMachine;
            m_ToolBarButtonStop.Visible = !isInwearSimpleUi;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //Emulate a slow loader
            //System.Threading.Thread.Sleep(2000);

            bool splashEnable = PubFunc.GetEnableSplash();
            if (splashEnable || (PubFunc.GetUserPermission() == (int)UserPermission.Operator))
                Splasher.Fadeout();
        }

        private int _errorCode = 0;
        private int _printPercent = 0; // 仅用于tcpip获取,打印完成不清0
        private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
        {
            CoreMsgEnum kParam = (CoreMsgEnum)wParam.ToInt64();
            LogWriter.WriteLog(new string[] { string.Format("ProceedKernelMessage kParam={0};lParam={1}", kParam, lParam.ToInt64()) }, true);

            switch (kParam)
            {
                case CoreMsgEnum.UpdaterPercentage:
                    {
                        int percentage = (int)lParam.ToInt64();
                        //OnPrintingProgressChanged(percentage);
                        string info = "";
                        string mPrintingFormat = ResString.GetUpdatingProgress();
                        info += string.Format(mPrintingFormat, percentage);
                        this.m_StatusBarPanelPercent.Text = info;
                        this.m_PrintInformation.PrintString(info, UserLevel.user, ErrorAction.Updating);
                        break;
                    }

                case CoreMsgEnum.Percentage:
                    {
                        int percentage = (int)lParam.ToInt64();
                        OnPrintingProgressChanged(percentage);
                        Console.WriteLine("Printing: {0}%", percentage);
                        if (percentage > 0)
                        {
                            m_PreviewAndInfo.UpdatePercentage(percentage);
                        }
                        _printPercent = percentage;
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
                            OnPrintingStart();
                        }

                        break;
                    }
                case CoreMsgEnum.Job_End:
                    {
                        if (m_JobListForm.PrintingJob!=null && !m_JobListForm.PrintingJob.IsAbort)
                        {
                            _printPercent = 100;
                        }
                        int endType = (int)lParam.ToInt64();

                        if (endType == 0)
                        {
                        }
                        else if (endType == 1)
                        {
                            OnPrintingEnd();
                        }
                        break;
                    }
                case CoreMsgEnum.Power_On:
                    {
                        int bPowerOn = (int)lParam.ToInt64();
                        if (bPowerOn != 0)
                        {
                            int bPropertyChanged, bSettingChanged;
                            //						SPrinterProperty  sPrinterProperty;
                            //						SPrinterSetting sPrinterSetting;

                            m_allParam.PowerOnEvent(out bPropertyChanged, out bSettingChanged, out this.m_allParam.PrinterProperty, out this.m_allParam.PrinterSetting);
                            this.OnPowerONGetSettingsForEpson();
                            if (bPropertyChanged != 0)
                            {
                                OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
                            }
                            if (bSettingChanged != 0)
                            {
                                OnPrinterSettingChange(this.m_allParam.PrinterSetting);
                            }
                        }
                        else
                        {
                            if (m_MyMessageBox != null)
                            {
                                // post消息,通知MyMessageBox自动关闭
                                SystemCall.PostMessage(m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                            }

                            //this.m_JobListForm.TerminatePrintingJob(false);
                            m_allParam.PowerOffEvent();
                            //m_bFirstReady = false;
                            PubFunc.ResetCurY(); // 断电时重置当前位置
                        }
                        break;
                    }
                case CoreMsgEnum.Status_Change:
                    {
                        int status = (int)lParam.ToInt64();
                        OnPrinterStatusChanged((JetStatusEnum)status);
                        LogWriter.WriteLog(new string[] { string.Format("==========Status_Change = {0}", (JetStatusEnum)status) }, true);
                        if ((JetStatusEnum) status == JetStatusEnum.Ready)
                        {
                           PubFunc.RefreshRollLength();
                        }
                        if (m_bExitAtStart)
                        {
                            End();
                            Application.Exit();
                        }
                        break;
                    }
                case CoreMsgEnum.ErrorCode:
                    {
                        int errorCode = (int)lParam.ToInt64();
                        if (m_CurErrorCode != errorCode)
                            m_CurErrorCode = -1; // 泵墨超时错误消失后清空标志
                        SErrorCode sErrorCode = new SErrorCode(errorCode);
                        if (SErrorCode.IsOnlyPauseError(errorCode))
                        {
                            if (m_MyMessageBox != null)
                            {
                                // post消息,通知MyMessageBox自动关闭
                                SystemCall.PostMessage(m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                            }
                            m_MyMessageBox = new MyMessageBox(AutoStopMoveAndUpdateUI);

                            string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
                            //DialogResult result = m_MyMessageBox.ShowDialog();
                            //DialogResult result = MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
                            if (sErrorCode.nErrorCause == (byte)ErrorCause.COM &&
                                (sErrorCode.n16ErrorCode == (short)COMCommand_Abort.NotifyGoHome
                                || sErrorCode.n16ErrorCode == (short)COMCommand_Abort.NotifyMeasure
                                ))
                            {
                                DialogResult result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
                                if (sErrorCode.nErrorCause == (byte) ErrorCause.COM ||
                                    sErrorCode.nErrorCause == (byte) ErrorCause.CoreBoard)
                                {
                                    if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
                                    {
                                        CoreInterface.SendJetCommand((int) JetCmdEnum.ClearNewErrorCode, errorCode);
                                    }
                                    else
                                    {
                                        CoreInterface.SendJetCommand((int) JetCmdEnum.ClearErrorCode,
                                            sErrorCode.n16ErrorCode);
                                    }
                                }
#endif
                                if (result != DialogResult.No)
                                {
                                    if (sErrorCode.n16ErrorCode == (short)COMCommand_Abort.NotifyGoHome)
                                    {
                                        CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
                                    }
                                    else if (sErrorCode.n16ErrorCode == (short)COMCommand_Abort.NotifyMeasure)
                                    {
                                        CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 1);
                                    }
                                }
                            }
                            else if (sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard &&
                                     sErrorCode.n16ErrorCode == (short)CoreBoard_Err.SAND_LAYING)
                            {
                                DialogResult result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // 重复铺砂指令后，提示后自动清除报错
                                CoreInterface.ClearErrorCode(errorCode);
                                if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
                                {
                                    CoreInterface.SendJetCommand((int)JetCmdEnum.ClearNewErrorCode, errorCode);
                                }
                                else
                                {
                                    CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode, sErrorCode.n16ErrorCode);
                                }
                            }
                            else
                            {
                                bool bNeedClearFwError = false;
                                DialogResult result = DialogResult.Retry;
                                //if (sErrorCode.nErrorCause == (byte)ErrorCause.Software &&sErrorCode.nErrorCode == (byte)Software.MediaTooSmall)
                                if (sErrorCode.nErrorCause == (byte)ErrorCause.Software)
                                    {
                                    if (m_JobListForm.IsFristCopiesOrNoJobPrint())
                                    {
                                        if (sErrorCode.nErrorCode == (byte) Software.MediaTooSmall)
                                        {
                                            errorInfo = SErrorCode.GetResString("Software_MediaWidthTooSmall");
                                            float realW = UIPreference.ToDisplayLength(m_allParam.Preference.Unit, PubFunc.CalcRealJobWidth(this.m_JobListForm.PrintingJob.JobSize.Width, m_allParam));
                                            errorInfo = string.Format(errorInfo, realW.ToString() + m_allParam.Preference.GetUnitDisplayName());
                                        }
                                        if (sErrorCode.nErrorCode == (byte) Software.MediaHeightTooSmall)
                                        {
                                            errorInfo = SErrorCode.GetResString("Software_MediaHeightTooSmall");
                                        }

                                    }
                                    result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(),MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                    if (result == DialogResult.Cancel)
                                    {
                                        //CoreInterface.Printer_Abort();
                                        if (isPrinting || CoreInterface.Printer_IsOpen() != 0)
                                            m_JobListForm.AbortPrintingJob();
                                    }
                                    bNeedClearFwError = result != DialogResult.Cancel|| sErrorCode.nErrorCause == (byte)ErrorCause.Software;
                                }
                                else if (sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard &&
                                         sErrorCode.n16ErrorCode == (short)CoreBoard_Err.PUMPINKTIMEOUT
                                    )
                                {
#if false
								if(m_CurErrorCode==-1)
								{
									m_CurErrorCode = errorCode;
									if(mPumpInkTimer.Enabled)
									{
										mPumpInkTimer.Stop();
									}
									DialogResult dr = m_MyMessageBox.ShowPumpInkTimeOut(errorInfo,ResString.GetProductName());
									if(dr == DialogResult.Abort)
									{
										CoreInterface.ClearErrorCode(errorCode);
										if(mPumpInkTimer.Enabled)
											mPumpInkTimer.Stop();
										m_CurErrorCode = -1;
									}
									else
									{
										if(!mPumpInkTimer.Enabled)
											mPumpInkTimer.Start();
									}
								}
#else
                                    if (_bStopPumpInkWhenTimeout)
                                    {
                                        result = m_MyMessageBox.ShowPumpInkTimeOut(errorInfo, ResString.GetProductName());
                                        bNeedClearFwError = true; //result == DialogResult.Ignore;
                                        //if (result != DialogResult.Ignore)
                                        //{
                                        //    CoreInterface.Printer_Pause();
                                        //}
                                    }
#endif
                                }
                                else if ((sErrorCode.nErrorCause == (byte)ErrorCause.COM &&
                 sErrorCode.n16ErrorCode == (short)COMCommand_Abort.TouchSensor))
                                {
                                    result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(),
                                     MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                    if (result == DialogResult.Cancel)
                                    {
                                        if (isPrinting || CoreInterface.Printer_IsOpen() != 0)
                                            m_JobListForm.AbortPrintingJob();
                                    }
                                    bNeedClearFwError = result != DialogResult.Cancel;
                                }
                                else if ((sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard &&
                                             sErrorCode.n16ErrorCode == (short)CoreBoard_Err.InkSpilledAlarm))
                                {
                                    result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(),
                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    bNeedClearFwError = true;
                                }
                                else
                                {
                                    result = m_MyMessageBox.Show(errorInfo, ResString.GetProductName(),
                                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                    if (result == DialogResult.Cancel)
                                    {
                                        //CoreInterface.Printer_Abort();
                                        if (isPrinting || CoreInterface.Printer_IsOpen() != 0)
                                            m_JobListForm.AbortPrintingJob();
                                    }
                                    bNeedClearFwError = result != DialogResult.Cancel
                                        || sErrorCode.nErrorCause == (byte)ErrorCause.Software;
                                }
                                if (bNeedClearFwError)
                                {
                                    CoreInterface.ClearErrorCode(_errorCode);
#if LIYUUSB
							if(sErrorCode.nErrorCause != (byte)ErrorCause.Software)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,errorCode);
#else
                                    if (sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
                                        sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
                                    {
                                        if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
                                        {
                                            CoreInterface.SendJetCommand((int)JetCmdEnum.ClearNewErrorCode, errorCode);
                                        }
                                        else
                                        {
                                            CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode, sErrorCode.n16ErrorCode);
                                        }
                                    }
#endif
                                }
                            }
                            m_MyMessageBox = null;
                        }
                        OnErrorCodeChanged((int)lParam.ToInt64());
                        //For Updateing
                        ErrorCause cause = (ErrorCause)sErrorCode.nErrorCause;
                        if (cause == ErrorCause.CoreBoard &&
                            (ErrorAction) sErrorCode.nErrorAction == ErrorAction.Updating)
                        {
                            if (0 != sErrorCode.nErrorCode)
                            {
                                if (sErrorCode.n16ErrorCode == 1)
                                {
                                    if (konicTemperatureForm == null) //屏蔽gama下载波形重启提示
                                    {
                                        string info = ResString.GetEnumDisplayName(typeof (UISuccess),
                                            UISuccess.UpdateSuccess);
                                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    }
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

#if GZ_CARTON
                        if (SErrorCode.IsGzCartonShowError(errorCode))
                        {
                            if (m_MyMessageBox != null)
                            {
                                SystemCall.PostMessage(m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                            }
                            m_MyMessageBox = new MyMessageBox();
                            string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
                            m_MyMessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            m_MyMessageBox = null;
                        }
#endif
                        // 工正专有缺纸自动取消打印错误 20140827 gzw
                        if (cause == ErrorCause.COM && (ErrorAction)sErrorCode.nErrorAction == ErrorAction.Pause
                            && sErrorCode.n16ErrorCode == (short)COMCommand_Abort.NoPaperCancelPrint)
                        {
                            //m_JobListForm.TerminatePrintingJob(false);
                            //if (curStatus == JetStatusEnum.Busy)
                                CoreInterface.Printer_Pause();
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
                            if (m_allParam.PrinterProperty.EPSONLCD_DEFINED)
                            {
                                this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin =
                                    this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin = sPrinterSetting.sFrequencySetting.fXOrigin;
                                EpsonLCD.SetMainUI_Param(this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
                            }
                            OnPrinterSettingChange(sPrinterSetting);
                        }
                        //m_LockUpdate = false;
                        break;
                    }
                case CoreMsgEnum.AbortPrintCmd:
                    //MessageBox.Show(this,"Accept! ");
                    {
                        m_JobListForm.TerminatePrintingJob(false);
                    }
                    break;
                //				case CoreMsgEnum.BlockNotifyUI:
                //					int msg1 = lParam.ToInt32();
                //				{
                //					if(m_MyMessageBox != null)
                //					{
                //						SystemCall.PostMessage( m_MyMessageBox.Handle, m_MyMessageBox. m_KernelMessage, (int)CoreMsgEnum.Power_On, 0 );
                //					}
                //					m_MyMessageBox = new MyMessageBox();
                //				{
                //					string m1 = ResString.GetEnumDisplayName(typeof(Confirm),Confirm.GoHome);
                //					DialogResult result = m_MyMessageBox.Show(m1,ResString.GetProductName(),MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                //					if(result != DialogResult.No)
                //					{
                //						CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint,(int)AxisDir.X);
                //					}
                //				}
                //					m_MyMessageBox = null;
                //				}
                //					break;
                case CoreMsgEnum.HardPanelDirty:
                    {
                        Ep6Cmd cmd = (Ep6Cmd)lParam.ToInt64();
                        bool bIsDirty = false;
                        switch (cmd)
                        {
                            case Ep6Cmd.Epson_MainUI_Param: //see struct USB_RPT_MainUI_Param
                                {
                                    EpsonLCD.GetMainUI_Param(ref this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param);
                                    this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin =
                                        this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin;
                                    m_ToolbarSetting.OnPrinterSettingChange(m_allParam,
                                        this.m_allParam.EpsonAllParam);
                                    m_ToolbarSetting.OnGetPrinterSetting(ref this.m_allParam,
                                        ref this.m_allParam.EpsonAllParam);
                                    bIsDirty = true;
                                }
                                break;
                            case Ep6Cmd.Epson_Quality: //羽化
                                {
                                    EpsonLCD.GetPrint_Quality(ref this.m_allParam.EpsonAllParam.sUSB_Print_Quality);
                                    switch (this.m_allParam.EpsonAllParam.sUSB_Print_Quality.PrintQuality)
                                    {
                                        case (byte)EpsonFeatherType.None:
                                            this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 0;
                                            break;
                                        case (byte)EpsonFeatherType.Small:
                                            this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 20;
                                            break;
                                        case (byte)EpsonFeatherType.Medium:
                                            this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 40;
                                            break;
                                        case (byte)EpsonFeatherType.Large:
                                            this.m_allParam.PrinterSetting.sBaseSetting.nFeatherPercent = 100;
                                            break;
                                    }
                                    if (m_FuncSettingForm != null)
                                        m_FuncSettingForm.OnPrinterSettingChange(m_allParam);
                                    bIsDirty = true;
                                }
                                break;
                            case Ep6Cmd.Epson_Media_Info: //Media_Info
                                EpsonLCD.GetMedia_Info(ref this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
                                this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin =
                                    this.m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaOrigin;
                                m_ToolbarSetting.OnPrinterSettingChange(m_allParam,
                                    this.m_allParam.EpsonAllParam);
                                m_ToolbarSetting.OnGetPrinterSetting(ref this.m_allParam,
                                    ref this.m_allParam.EpsonAllParam);
                                this.m_allParam.PrinterSetting.sBaseSetting.fPaperWidth =
                                    m_allParam.EpsonAllParam.sUSB_RPT_Media_Info.MediaWidth;
                                if (m_FuncSettingForm != null)
                                    m_FuncSettingForm.OnPrinterSettingChange(m_allParam);
                                                              bIsDirty = true;
                                break;
                            case Ep6Cmd.Epson_Calibration: // 校准参数
                                {
                                    EpsonLCD.GetCalibrationSetting(ref this.m_allParam.PrinterSetting.sCalibrationSetting,
                                        false);
                                    if (m_wizard != null)
                                    {
                                        // 只通知m_wizard重新抓校准参数,内部并不直接用m_allParam.PrinterSetting.sCalibrationSetting.
                                        m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
                                    }
                                    bIsDirty = true;
                                }
                                break;
                            case Ep6Cmd.Epson_STEP: // EP6_CMD_T_STEP_DIRTY  0x9
                                {
                                    int step = 0;
                                    int passnum = 0;
                                    EpsonLCD.GetSTEP(ref step, ref passnum);

                                    if (this.m_bDuringPrinting)
                                    {
                                        SPrtFileInfo jobInfo = new SPrtFileInfo();
                                        if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
                                        {
                                            int realpass = jobInfo.sFreSetting.nPass;
                                            if (realpass != passnum)
                                                LogWriter.WriteLog(
                                                    new string[]
                                                    {string.Format("realpass[{0}] != passnum[{1}]", realpass, passnum)},
                                                    true);
                                            this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PassNum = realpass;
                                            this.m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.StepModify = step;

                                            if (this.m_allParam.PrinterSetting.nKillBiDirBanding != 0)
                                                this.m_allParam.PrinterSetting.sCalibrationSetting.nPassStepArray[
                                                    passnum / 2 - 1] = step;
                                            else
                                                this.m_allParam.PrinterSetting.sCalibrationSetting.nPassStepArray[
                                                    passnum - 1] = step;
                                        }
                                    }

                                    m_ToolbarSetting.OnPrinterSettingChange(m_allParam,
                                        this.m_allParam.EpsonAllParam);
                                    if (m_wizard != null)
                                    {
                                        // 只通知m_wizard重新抓校准参数,内部并不直接用m_allParam.PrinterSetting.sCalibrationSetting.
                                        m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
                                    }
                                    bIsDirty = true;
                                }
                                break;
                            case Ep6Cmd.Panel_PIPE:
                                {
                                    byte ep6Cmd = (byte)lParam.ToInt64();
                                    byte[] buf = new byte[512];
                                    int buflen = buf.Length;
                                    if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                    {
                                        byte[] temp = new byte[buflen];
                                        Buffer.BlockCopy(buf, 0, temp, 0, buflen);
                                        this.m_PreviewAndInfo.SetStatusData(temp);
                                        if (temp[12] != 0)
                                        {
                                            string msg = string.Empty;
                                            msg += (temp[12] & (1)) != 0 ? "彩色负压出错|" : "";
                                            msg += (temp[12] & (1 << 1)) != 0 ? "白色负压出错|" : "";
                                            msg += (temp[12] & (1 << 2)) != 0 ? "安全气瓶1进墨水|" : "";
                                            msg += (temp[12] & (1 << 3)) != 0 ? "安全气瓶2进墨水|" : "";
                                            msg += (temp[12] & (1 << 4)) != 0 ? "外部气压过低|" : "";
                                            msg += (temp[12] & (1 << 5)) != 0 ? "外部气压过高|" : "";
                                            if (!string.IsNullOrEmpty(msg))
                                            {
                                                this.m_StatusBarPanelError.Text = msg;
                                                this.m_PrintInformation.printJobInfomation(0x10030000 + temp[12], UserLevel.user, msg);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format("GetDirtyCmd fialed! cmd={0}", ep6Cmd));
                                    }
                                    break;
                                }
                            case Ep6Cmd.REPORT_SINGNAL:
                                {
                                    break;
                                }
                            case Ep6Cmd.REPORT_SCORPION:
                                {
                                    break;
                                }
                            case Ep6Cmd.NOZZLE_CER_FAIL:
                                {
                                    byte ep6Cmd = (byte)lParam.ToInt64();
                                    byte[] buf = new byte[512];
                                    int buflen = buf.Length;
                                    if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                    {
                                        if (m_MyMessageBox != null)
                                        {
                                            // post消息,通知MyMessageBox自动关闭
                                            SystemCall.PostMessage(m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                                        }
                                        m_MyMessageBox = new MyMessageBox(AutoStopMoveAndUpdateUI); 
                                        byte[] temp = new byte[buflen];
                                        Buffer.BlockCopy(buf, 0, temp, 0, buflen);
                                        var reqNum = temp[0];
                                        var reqStr = ResString.GetResString("TotalReqNumStr");
                                        var actualConNum = temp[1];
                                        var actualConStr = ResString.GetResString("ActualNumStr");
                                        var passNum = temp[2];
                                        var passStr = ResString.GetResString("PassNumStr");
                                        var sb = new StringBuilder();
                                        int noPassNum = actualConNum - passNum;
                                        var noPassStr = ResString.GetResString("NoPassNumStr");
                                        
                                        if (noPassNum > 0) 
                                        {
                                            if (SPrinterProperty.IsSG1024(m_allParam.PrinterProperty.ePrinterHead))
                                            {
                                                for (int i = 0; i < noPassNum; i++)
                                                {
                                                    int startNum = i * 8 + 3;
                                                    byte[] pAdress = new byte[8];
                                                    Array.Copy(temp, startNum, pAdress, 0, pAdress.Length);
                                                    string noPassCode = Encoding.ASCII.GetString(pAdress.Skip(1).Take(3).ToArray()) + 
                                                        string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", pAdress[4], pAdress[5], pAdress[6], pAdress[7]);
                                               
                                                    sb.Append("SN:" + noPassCode + "\n");
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < noPassNum; i++)
                                                {
                                                    int startNum = i*8 + 3;
                                                    var pre = Encoding.ASCII.GetString(temp, startNum, 4);
                                                        //System.Text.Encoding.UTF8.GetString(Encoding.UTF8.ASCII.GetString(temp, startNum, 4));
                                                    byte[] revTemp = new byte[4];
                                                    Array.Copy(temp, startNum + 4, revTemp, 0, 4);
                                                    var end =
                                                        BitConverter.ToUInt32(revTemp.Reverse().ToArray(), 0)
                                                            .ToString("000000");
                                                    //var noPassCode = BitConverter.ToString(temp, startNum, 8);                                                
                                                    var noPassCode = pre + "-" + end.Substring(end.Length - 6);
                                                    sb.Append("SN:" + noPassCode + "\n");
                                                }
                                            }
                                            m_MyMessageBox.Show(reqStr + reqNum.ToString() + "\n" + actualConStr + actualConNum.ToString() +
                                            "\n" + passStr + passNum.ToString() + "\n" + noPassStr + "\n" + sb.ToString(), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }
                                        else
                                            m_MyMessageBox.Show(reqStr + reqNum.ToString() + "\n" + actualConStr + actualConNum.ToString() +
                                            "\n" + passStr + passNum.ToString() + "\n", ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        m_MyMessageBox = null;
                                    }
                                    break;
                                }
                            case Ep6Cmd.PumpInkBit:
                                {
                                    //如果vender.xml中不存在此配置项,则此功能无效
                                    if (pumpInkColorList.Count != 32)
                                        break;
                                    byte ep6Cmd = (byte)lParam.ToInt64();
                                    byte[] buf = new byte[512];
                                    int buflen = buf.Length;
                                    if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                    {
                                        if (!pumpInkColorListWorked)
                                        {
                                            pumpInkColorListWorked = true;
                                            //break; //第一次不显示信息,避免和传统错误号重复.
                                        }

                                        byte[] temp = new byte[buflen];
                                        Buffer.BlockCopy(buf, 0, temp, 0, buflen);
                                        int code = BitConverter.ToInt32(temp, 0);
                                        string colorName = string.Empty;
                                        for (int i = 0; i < pumpInkColorList.Count; i++)
                                        {
                                            if ((code & (1 << i)) != 0)
                                            {
                                                if (string.IsNullOrEmpty(colorName))
                                                    colorName += pumpInkColorList[i];
                                                else
                                                    colorName += "," + pumpInkColorList[i];
                                            }
                                        }

                                        string errStr = string.Format(ResString.GetResString("Pumping_Ink"), colorName);
                                        this.m_StatusBarPanelError.Text = errStr;
                                        this.m_PrintInformation.printJobInfomation(code, UserLevel.user, errStr);
                                        LogWriter.WriteLog(new string[] { string.Format("PumpInkBit={0}", errStr) }, true);
                                        
                                    }
                                    else
                                    {
                                        pumpInkColorListWorked = false;
                                        LogWriter.WriteLog(
                                            new string[] { string.Format("GetDirtyCmd fialed! cmd={0}", ep6Cmd) }, true);
                                    }
                                    break;
                                }
                                case Ep6Cmd.LowInkBit:
                            {
                                byte ep6Cmd = (byte)lParam.ToInt64();
                                byte[] buf = new byte[512];
                                int buflen = buf.Length;
                                if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                {
                                    byte[] temp = new byte[buflen];
                                    Buffer.BlockCopy(buf, 0, temp, 0, buflen);
                                    int code = BitConverter.ToInt32(temp, 0);
                                    string colorName = string.Empty;
                                    for (int i = 0; i < m_allParam.PrinterProperty.nColorNum; i++)
                                    {
                                        if ((code & (1 << i)) != 0)
                                        {
                                            if (string.IsNullOrEmpty(colorName))
                                                colorName += m_allParam.PrinterProperty.Get_ColorIndex(i);
                                            else
                                                colorName += "," + m_allParam.PrinterProperty.Get_ColorIndex(i);
                                        }
                                    }
                                    string errStr = string.Empty;
                                    //if (!string.IsNullOrEmpty(colorName))
                                    //    errStr = string.Format("{0}{1}", colorName, ErrRes.GetResString("CoreBoard_Fatal_INK_SUPPLY_LOW"));
                                    //this.m_StatusBarPanelError.Text = errStr;
                                    this.m_PrintInformation.printJobInfomation(code, UserLevel.user, errStr);
                                }
                                else
                                {
                                    LogWriter.WriteLog(
                                        new string[] { string.Format("GetDirtyCmd fialed! cmd={0}", ep6Cmd) }, true);
                                }
                                break;
                            }
                            case Ep6Cmd.Fw_Msg:
                                {
                                    byte ep6Cmd = (byte)lParam.ToInt64();
                                    byte[] buf = new byte[512];
                                    int buflen = buf.Length;
                                    if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                    {
                                        MyMessageBox messageBox = new MyMessageBox(AutoStopMoveAndUpdateUI,false,false);
                                        byte[] temp = new byte[4];
                                        Buffer.BlockCopy(buf, 0, temp, 0, 4);
                                        temp[3] = (byte) ErrorAction.FwMsg; //
                                        temp[2] = (byte) ErrorCause.CoreBoard;
                                        int errorCode = BitConverter.ToInt32(temp, 0);//
                                        // 先从错误信息库查找对应文本信息
                                        string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
                                        if (!string.IsNullOrEmpty(errorInfo))
                                        {
                                            this.m_PrintInformation.printJobInfomation(errorCode, UserLevel.user, errorInfo);
                                            messageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            // 错误信息库没有对应语言信息的话,直接显示板卡附带信息
                                            byte[] lentemp = new byte[2];
                                            Buffer.BlockCopy(buf, 14, lentemp, 0, 2);
                                            short len = BitConverter.ToInt16(lentemp, 0);
                                            byte[] msg = new byte[len];
                                            Buffer.BlockCopy(buf, 16, msg, 0, len);
                                            string strmsg = Encoding.Default.GetString(msg);
                                            string[] msgs = strmsg.Split(new char[] {(char) 0});
                                            if (msgs.Length >1 
                                                &&!string.IsNullOrEmpty(msgs[1])
                                                &&Thread.CurrentThread.CurrentUICulture.LCID ==
                                                new CultureInfo("zh-CHS").LCID)
                                            {
                                                this.m_PrintInformation.printJobInfomation(errorCode, UserLevel.user, msgs[1]);
                                                messageBox.Show(msgs[1], ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else if (msgs.Length > 2
                                                     && !string.IsNullOrEmpty(msgs[2])
                                                     && Thread.CurrentThread.CurrentUICulture.LCID ==
                                                     new CultureInfo("zh-CHT").LCID)
                                            {
                                                this.m_PrintInformation.printJobInfomation(errorCode, UserLevel.user, msgs[2]);
                                                messageBox.Show(msgs[2], ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                this.m_PrintInformation.printJobInfomation(errorCode, UserLevel.user, msgs[0]);
                                                messageBox.Show(msgs[0], ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    break;
                                }
                            case Ep6Cmd.Origin_Change:
                                {
                                    byte ep6Cmd = (byte)lParam.ToInt64();
                                    byte[] buf = new byte[512];
                                    int buflen = buf.Length;
                                    if (CoreInterface.GetDirtyCmd(ep6Cmd, buf, ref buflen) != 0)
                                    {
                                        byte[] temp = new byte[buflen];
                                        Buffer.BlockCopy(buf, 0, temp, 0, buflen);
                                        float OriginX = BitConverter.ToSingle(temp, 0);
                                        float OriginY = BitConverter.ToSingle(temp, 4);
                                        float OriginZ = BitConverter.ToSingle(temp, 8);

//#if DEBUG
//                                        MessageBox.Show("X:" + OriginX.ToString() + ",Y:" + OriginY.ToString() + ",Z:" + OriginZ.ToString());
//#endif

                                        m_allParam.PrinterSetting.sFrequencySetting.fXOrigin = OriginX;
                                        m_allParam.PrinterSetting.sBaseSetting.fYOrigin = OriginY;

                                        m_ToolbarSetting.OnPrinterSettingChange(m_allParam,
                                        this.m_allParam.EpsonAllParam);
                                        
                                    }
                                    break;
                                }
                            default:
                                bIsDirty = false;
                                break;
                        }
                        if (bIsDirty)
                        {
                            CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
                            LogWriter.LogSetPrinterSetting(this.m_allParam.PrinterSetting, m_bDuringPrinting,
                                                           string.Format("{0}-{1}", CoreMsgEnum.HardPanelDirty, cmd));
                        }
                    }
                    break;
                case CoreMsgEnum.HardPanelCmd:
                    {
                        uint uLen = 3;
                        byte[] val = new byte[uLen];
                        byte nSendCmd = 0x20;
                        switch ((HardPanelCmd)lParam.ToInt64())
                        {
                            case HardPanelCmd.PA_CANCEL: // #define EP6_CMD_T_PA_CANCEL     0x1
                                m_JobListForm.TerminatePrintingJob(false);
                                break;
                            case HardPanelCmd.PA_PAUSE: // #define EP6_CMD_T_PA_PAUSE      0x2
                            case HardPanelCmd.PA_RESUME: // #define EP6_CMD_T_PA_RESUME     0x3
                                CoreInterface.Printer_PauseOrResume();
                                break;
                            case HardPanelCmd.PRINT_NOZZLE_CHECK:
                                CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckNozzleCmd, 0,
                                    ref this.m_allParam.PrinterSetting,true);
                                break;
                            case HardPanelCmd.SET_ORIGINAL:
                                CoreInterface.SendJetCommand((int)JetCmdEnum.SetOrigin, (int)AxisDir.X);
                                break;
                            case HardPanelCmd.FROBIDPRINT:
                                this.m_bFrobidPrinter = true;
                                if (!m_ToolBarButtonAbort.Enabled)
                                {
                                    m_JobListForm.TerminatePrintingJob(false);
                                    this.m_bFrobidPrinter = true;
                                    MessageBox.Show("打印开关已经被关闭!");
                                }
                                OnPrinterStatusChanged(CoreInterface.GetBoardStatus());
                                break;
                            case HardPanelCmd.CANCELFROBID:
                                this.m_bFrobidPrinter = false;
                                OnPrinterStatusChanged(CoreInterface.GetBoardStatus());
                                break;
                            case HardPanelCmd.PRINT_START:
                                if (m_ToolBarButtonPrint.Enabled)
                                    m_JobListForm.PrintJob();
                                break;
                            case HardPanelCmd.PM_DISABLE_UI:
                                m_bOffline = true;
                                OnPrinterStatusChanged(JetStatusEnum.Offline);
                                break;
                            case HardPanelCmd.PM_ENABLE_UI:
                                m_bOffline = false;
                                OnPrinterStatusChanged(CoreInterface.GetBoardStatus());
                                break;
                            case HardPanelCmd.FIRESPRAY:
                                CoreInterface.SendJetCommand((int)JetCmdEnum.FireSprayHead, 0);
                                break;
                            case HardPanelCmd.SET_ORIGINAL_Y:
                                CoreInterface.SendJetCommand((int)JetCmdEnum.SetOrigin, (int)AxisDir.Y);
                                break;
                            case HardPanelCmd.PRINT_START_CS:
                                byte printAXIS = (byte)(lParam.ToInt64() >> 8);
                                //LogWriter.WriteLog(new string[]{string.Format("PRINT_START_CS printAXIS={0}",printAXIS)},true);
                                if (printAXIS == CoreConst.AXIS_4)
                                    m_allParam.Preference.ScanningAxis = CoreConst.AXIS_4;
                                if (printAXIS == CoreConst.AXIS_X)
                                    m_allParam.Preference.ScanningAxis = CoreConst.AXIS_X;
                                if (m_ToolbarSetting != null)
                                    m_ToolbarSetting.OnPreferenceChange(m_allParam.Preference);
                                if (m_ToolBarButtonPrint.Enabled)
                                    m_JobListForm.PrintJob();
                                break;
                        }
                    }
                    break;
                case CoreMsgEnum.PrintInfo:
                    {
                        int n_JobID = (int)lParam.ToInt64();
                        m_PreviewAndInfo.UpdateJobID(n_JobID);
                    }
                    break;
                case CoreMsgEnum.Ep6Pipe:
                    {

                        int ep6Cmd = (int)lParam.ToInt64() & 0x0000ffff;
                        int index = (((int)lParam.ToInt64()) >> 16);
                        byte[] bufep6 = null;
                        int buflen = 0;
                        int ret = (CoreInterface.GetEp6PipeData(ep6Cmd, index, bufep6, ref buflen));
                        if (ret != 0)
                        {
                            bufep6 = new byte[buflen];
                            if (CoreInterface.GetEp6PipeData(ep6Cmd, index, bufep6, ref buflen) != 0)
                            {
                                if (hwForm != null)
                                {
                                    /// 工厂调试串口读取喷头参数
                                    hwForm.OnEp6DataChanged(ep6Cmd, index, bufep6);
                                }
                                if (konicTemperatureForm != null)
                                {
                                    konicTemperatureForm.OnEp6DataChanged(ep6Cmd, index, bufep6);
                                }
                            }
                        }
                        break;
                    }
            }
        }


        private void UpdateViewMode(int mode)
        {
            UIViewMode uimode = (UIViewMode)mode;
            switch (uimode)
            {
                case UIViewMode.LeftRight:
                    //this.panelSubBar.Visible = true;
                    //this.m_FolderPreview.Visible = true;
                    //this.m_WorkForldersplitter.Enabled = true;
                    //this.m_WorkForlderpanel.Dock = DockStyle.Right;
                    //this.m_WorkForlderpanel.Width = this.Width / 3;
                    //m_SplitterJobList.Dock = DockStyle.Right;
                    //this.toolBarButtonAddToList.Visible = true;
                    //this.m_JobListForm.m_JobListView.View = View.Details;
                    //this.panelSubBar.SendToBack();
                    //this.m_JobListForm.StopLoad();
                    //this.m_FolderPreview.ReLoadItems();
                    //break;
                case UIViewMode.NotifyIcon:
                case UIViewMode.OldView:
                    //break;
                case UIViewMode.TopDown:
                default:
                    this.panelSubBar.Visible = true;
                    this.m_FolderPreview.Visible = false;
                    this.m_FolderPreview.StopLoad();
                    this.m_WorkForldersplitter.Enabled = false;
                    //this.m_WorkForlderpanel.Dock = DockStyle.Bottom;
                    //this.m_WorkForlderpanel.Height = this.Height / 3;
                    //m_SplitterJobList.Dock = DockStyle.Bottom;
                    this.toolBarButtonAddToList.Visible = false;
                    this.m_JobListForm.m_JobListView.View = View.LargeIcon;
                    //this.m_JobListForm.mAlignment = ListViewAlignment.Left;
                    break;
            }
            this.m_MenuItemLeftRight.RadioCheck = this.m_MenuItemLeftRight.Checked = uimode == UIViewMode.LeftRight;
            this.m_MenuItemOldView.RadioCheck = this.m_MenuItemOldView.Checked = (uimode == UIViewMode.NotifyIcon || uimode == UIViewMode.OldView);
            this.m_MenuItemTopDown.RadioCheck = this.m_MenuItemTopDown.Checked = uimode == UIViewMode.TopDown;
            switch (uimode)
            {
                case UIViewMode.OldView:
                case UIViewMode.NotifyIcon:
                    //this.m_FolderPreview.Visible = false;
                    //this.m_WorkForldersplitter.Enabled = false;
                    //this.m_WorkForlderpanel.Dock = DockStyle.Bottom;
                    //this.m_WorkForlderpanel.Height = this.Height / 3;
                    //m_SplitterJobList.Dock = DockStyle.Bottom;
                    //m_PreviewAndInfo.Dock = DockStyle.Fill;
                    //this.panelSubBar.Visible = false;
                    //this.m_JobListForm.m_JobListView.View = View.Details;
                    //m_StatusBarApp.Visible = true;
                    //m_PrintInformation.Visible = false;
                    //this.m_PreviewAndInfo.SetBackgroundimage(null, null);
                    //m_PreviewAndInfo.SetDisStyle(false, Color.White, Color.DarkGray);
                    //m_ToolbarSetting.GradientColors = new Style(SystemColors.Control, SystemColors.Control);
                    //this.m_ToolBarCommand.Divider = m_ToolbarSetting.Divider = true;
                    //Dev4Arabs.Globals.menuFont = SystemInformation.MenuFont;
                    //this.m_MainMenu.MenuItems.Clear();
                    //this.m_MenuItemView.Visible = this.m_MenuItemJob.Visible = true;
                    //this.m_MainMenu.MenuItems.AddRange(new MenuItem[]{this.m_MenuItemJob,
                    //                                                     this.m_MenuItemSetting,
                    //                                                     this.m_MenuItemTools,
                    //                                                     this.m_MenuItemView,
                    //                                                     this.m_MenuItemHelp,
                    //                                                     this.menuItemDongle,
                    //                                                     this.m_MenuItemDebug});
                    //m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.Standard;
                    //if (PubFunc.IconReload(m_ToolbarImageList, m_allParam.Preference.SkinName))
                    //{
                    //    // 重新加载图标(PubFunc.IconReload())后,清除标记
                    //    this.m_bMeasurePaperIconTransformed = false;
                    //}
                    //officeMenus1.End(this);
                    //break;
                case UIViewMode.LeftRight:
                case UIViewMode.TopDown:
                default:
                    //m_PreviewAndInfo.Dock = DockStyle.Fill;
                    m_StatusBarApp.Visible = false;
                    m_PrintInformation.Visible = true;
                    m_PreviewAndInfo.SetDisStyle(true, Color.LightBlue, Color.SteelBlue);
                    m_ToolbarSetting.GradientColors = new BYHXPrinterManager.Style(MyWndProc.HeaderColor2, MyWndProc.HeaderColor3);
                    this.m_ToolBarCommand.Divider = m_ToolbarSetting.Divider = false;
                    this.m_PreviewAndInfo.SetBackgroundimage(MyWndProc.imgbackground, MyWndProc.imgbackground_main);
                    this.m_ToolbarSetting.SetBackgroundimage(MyWndProc.imgToolSetting);
                    Dev4Arabs.Globals.PIC_AREA_SIZE = 32;
                    Dev4Arabs.Globals.MenuDarkColor2 = Color.FromArgb(68, 112, 243);
                    Dev4Arabs.Globals.MenuLightColor2 = Color.FromArgb(68, 112, 243);
                    Dev4Arabs.Globals.SelectionColor = Color.FromArgb(68, 112, 243);
                    Dev4Arabs.Globals.menuFont = new Font(Dev4Arabs.Globals.menuFont.FontFamily, SystemInformation.MenuFont.Size + 2);
                    this.m_StartMenu.MenuItems.Clear();
                    this.m_MenuItemView.Visible = this.m_MenuItemJob.Visible = false;
                    this.m_StartMenu.MenuItems.AddRange(new MenuItem[] { this.m_MenuItemSetting, this.m_MenuItemTools, this.m_MenuItemHelp, this.menuItemDongle, this.m_MenuItemDebug });
                    m_GroupboxStyle.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;

                    System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
                    ImageList imgl = new ImageList();
                    imgl.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
                    if (m_bGongzheng)
                    {
                        m_ToolbarImageList.ImageSize = bigIconSize;
                    }
                    imgl.ImageSize = m_ToolbarImageList.ImageSize;
                    //Thread.CurrentThread.CurrentCulture保证在任何语言时都使用英文资源内的图标
                    imgl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ToolbarImageList.ImageStream", Thread.CurrentThread.CurrentCulture)));
                    imgl.TransparentColor = System.Drawing.Color.Transparent;
                    for (int i = 0; i < imgl.Images.Count; i++)
                    {
                        if (m_bGongzheng)
                        {
                            if (i < this.m_ToolbarImageList.Images.Count)
                            {
                                this.m_ToolbarImageList.Images[i] = new Bitmap(imgl.Images[i], bigIconSize.Width, bigIconSize.Height);
                            }
                            else
                            {
                                this.m_ToolbarImageList.Images.Add(new Bitmap(imgl.Images[i], bigIconSize.Width, bigIconSize.Height), System.Drawing.Color.Transparent);
                            }
                        }
                        else
                        {
                            if (i < this.m_ToolbarImageList.Images.Count)
                            {
                                this.m_ToolbarImageList.Images[i] = imgl.Images[i];
                            }
                            else
                            {
                                this.m_ToolbarImageList.Images.Add(imgl.Images[i], System.Drawing.Color.Transparent);

                            }
                        }
                    }
                    imgl.ImageSize = m_ToolbarImageList.ImageSize;
                    InitMenus();
                    // 重新加载图标后,清除标记
                    this.m_bMeasurePaperIconTransformed = false;
                    break;
            }
            this.ControlBox = (uimode == UIViewMode.OldView || uimode == UIViewMode.NotifyIcon) || !MyWndProc.CanDrawFormHeader;
            //this.m_SplitterJobList.SendToBack();
            this.m_WorkForlderpanel.SendToBack();
            //this.m_SplitterStatus.SendToBack();
            this.m_PrintInformation.SendToBack();

            this.TransformMeasurePaperIcon();
        }


        private void AddSelectedToList()
        {
            string[] m_FileNames = new string[this.m_FolderPreview.SelectedItems.Count];
            for (int i = 0; i < this.m_FolderPreview.SelectedItems.Count; i++)
            {
                m_FileNames[i] = this.m_FolderPreview.SelectedItems[i].Tag.ToString();
            }

            this.m_JobListForm.AddJobs(m_FileNames);
        }


        private void InitMenus()
        {
            //			this.officeMenus1.AddPicture(this.m_MenuItemJob, null);
            this.officeMenus1.AddPicture(this.m_MenuItemAdd, 0);
            //			this.officeMenus1.AddPicture(this.m_MenuItemDelete, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemPrint, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemExit, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemSetting, null);
            this.officeMenus1.AddPicture(this.m_MenuItemSave, 0);
            this.officeMenus1.AddPicture(this.m_MenuItemLoad, 1);
            this.officeMenus1.AddPicture(this.m_MenuItemSaveToPrinter, 2);
            this.officeMenus1.AddPicture(this.m_MenuItemLoadFromPrinter, 3);
            this.officeMenus1.AddPicture(this.m_MenuItemEdit, 4);
            //			this.officeMenus1.AddPicture(this.m_MenuItemTools, null);

            this.officeMenus1.AddPicture(this.m_MenuItemUpdate, 5);
            this.officeMenus1.AddPicture(this.m_MenuItemPassword, 6);
            this.officeMenus1.AddPicture(this.m_MenuItemDemoPage, 1);
            this.officeMenus1.AddPicture(this.m_MenuItemCalibraion, 7);
            this.officeMenus1.AddPicture(this.m_MenuItemHWSetting, 8);
            this.officeMenus1.AddPicture(this.m_MenuItemRealTime, 9);
            this.officeMenus1.AddPicture(this.m_MenuItemRealTimeChart, 9);
            this.officeMenus1.AddPicture(this.m_MenuItemUVSetting, 10);
            //this.officeMenus1.AddPicture(this.menuItemAutoStopPumpInk, 0);
            this.officeMenus1.AddPicture(this.menuItemPrintRecord, 4);


            //			this.officeMenus1.AddPicture(this.m_MenuItemView, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemTopDown, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemLeftRight, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemOldView, null);
            //			this.officeMenus1.AddPicture(this.m_MenuItemHelp, null);
            this.officeMenus1.AddPicture(this.m_MenuItemAbout, 0);
            //this.officeMenus1.AddPicture(this.m_MenuItemSelectInkType,17);
            //			this.officeMenus1.AddPicture(this.m_MenuItemDebug, null);
            this.officeMenus1.AddPicture(this.m_MenuItemFactoryDebug, 0);
            //			this.officeMenus1.AddPicture(this.menuItemAddToList, null);
            //			this.officeMenus1.AddPicture(this.menuItem2, null);
            //			this.officeMenus1.AddPicture(this.menuItemReLoad, null);
            //			this.officeMenus1.AddPicture(this.menuItem4, null);
            //			this.officeMenus1.AddPicture(this.menuItemSelectAll, null);
            //			this.officeMenus1.AddPicture(this.menuItemSave, null);
            //			this.officeMenus1.AddPicture(this.menuItemLoad, null);
            //			this.officeMenus1.AddPicture(this.menuItemSaveToPrinter, null);
            //			this.officeMenus1.AddPicture(this.menuItemLoadFromPrinter, null);
            //			this.officeMenus1.AddPicture(this.menuItemUpdate, null);
            //			this.officeMenus1.AddPicture(this.menuItemPassWord, null);
            //			this.officeMenus1.AddPicture(this.menuItemDemoPage, null);
            //			this.officeMenus1.AddPicture(this.menuItemCalibrationWizard, null);
            //			this.officeMenus1.AddPicture(this.menuItemHWSetting, null);
            //			this.officeMenus1.AddPicture(this.menuItemRealSetting, null);
            //			this.officeMenus1.AddPicture(this.menuItemUVSetting, null);
            //			this.officeMenus1.AddPicture(this.menuItem1, null);
            //			this.officeMenus1.AddPicture(this.menuItemAbout, null);
            //			this.officeMenus1.AddPicture(this.menuItem3, null);
            //			this.officeMenus1.AddPicture(this.menuItemFactoryDebug, null);
        }

        #endregion

        #region public

        public bool Start()
        {
            if (!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle, false))
                return m_bExitAtStart;
            SystemCall.PreventSystemPowerdown();
            m_allParam = new AllParam();
            CoreInterface.AllParams = m_allParam;
            SystemInit init = new SystemInit(this, this.Handle, m_KernelMessage);
            init.SystemStart();

            m_PortManager = new PortManager(this);
            m_PortManager.OpenPort();
            m_PortManager.TaskStart();
            return !m_bExitAtStart;
        }

        public bool End()
        {
            try
            {
                this.m_JobListForm.TerminatePrintingJob(true);
                if ((SPrinterProperty.IsGongZeng() || SPrinterProperty.IsRuiZhi()) && InkAreaStaticsHelper.CanSynchronizeInkAndArea() && !InkAreaStaticsHelper.BoardHasChanged())
                {
                    InkAreaStaticsHelper.SynchronizeInkAndArea(BYHXSoftLock.GetDateTime());//墨量同步
                }
                PubFunc.UnregisteredToMainboard();
                SystemInit init = new SystemInit(this, this.Handle, m_KernelMessage);
                init.SystemEnd();

                if (m_PortManager != null)
                    m_PortManager.ClosePort();
                SystemCall.AllowSystemPowerdown();
                this.notifyIconBYHX.Dispose();
                if (this.server != null)
                    this.server.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        public void OnErrorCodeChanged(int code)
        {
            _errorCode = code;
            if (remoteControlServer != null && _errorCode != 0)
            {
                // 错误号发生变化主动通知tcpip客户端
#if FHZL3D
                ProtocolFhzl returnFhzl = new ProtocolFhzl(TcpIpCmdFhzl.ResponsePackage,
                    (byte) GetInfoSubCmd.ErrorCode, _errorCode.ToString("X8"));
#else
                ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                    (byte)GetInfoSubCmd.ErrorCode, _errorCode.ToString("X8"));
#endif
                remoteControlServer.SendCmdReturnValue(returnFhzl);
            }
            bool bColorChange = false;
            SErrorCode errcode = new SErrorCode(code);
            if (pumpInkColorListWorked
                &&
                (
                    errcode.nErrorAction == (byte)ErrorAction.Warning &&
                    errcode.nErrorCause == (byte)ErrorCause.CoreBoard
                    &&
                    (
                        errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTCYAN
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_CYAN
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTMAGENTA
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_MAGENTA
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_SPOTCOLOR1
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_YELLOW
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_SPOTCOLOR2
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_BLACK
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C9
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C10
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C11
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C12
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C13
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C14
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C15
                        || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_C16
                        )))
            {
                return; // 如果ep6上报泵墨信息已经正常工作,则屏蔽原有错误号报泵墨
            }

#if SHIDAO
            // 世导不显示泵墨错误信息，与其他接口不一致
            if (errcode.nErrorAction == (byte)ErrorAction.Warning && errcode.nErrorCause == (byte)ErrorCause.CoreBoard
                &&
            (errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_BLACK
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_CYAN
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTCYAN
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTMAGENTA
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_MAGENTA
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR1
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR2
            || errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_YELLOW
            ))
            {
                return;
            }
#endif
            if (this.m_allParam.PrinterProperty.nColorNum == 4)
            {
                if (errcode.nErrorAction == (byte)ErrorAction.Warning && errcode.nErrorCause == (byte)ErrorCause.CoreBoard)
                {
                    if (errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTCYAN)
                    {
                        bColorChange = true;
                        errcode.n16ErrorCode = (short)CoreBoard_Warning.PUMP_CYAN;
                    }
                    else if (errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTMAGENTA)
                    {
                        bColorChange = true;
                        errcode.n16ErrorCode = (short)CoreBoard_Warning.PUMP_BLACK;
                    }
                    else if (errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_SPOTCOLOR1)
                    {
                        bColorChange = true;
                        errcode.n16ErrorCode = (short)CoreBoard_Warning.PUMP_MAGENTA;
                    }
                    else if (errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_SPOTCOLOR2)
                    {
                        bColorChange = true;
                        errcode.n16ErrorCode = (short)CoreBoard_Warning.PUMP_YELLOW;
                    }
                    code = errcode.nErrorCode + (errcode.nErrorSub << 8) + (errcode.nErrorCause << 16) + (errcode.nErrorAction << 24);
                }
            }
            string errStr = SErrorCode.GetInfoFromErrCode(code);
#if true// 支持自定义色序,兼容老版本俩个专色仍固定为专色,不做映射
            errcode = new SErrorCode(code);
            if (errcode.nErrorAction == (byte)ErrorAction.Warning && errcode.nErrorCause == (byte)ErrorCause.CoreBoard
                &&
                (
                errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTCYAN
                || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_CYAN
                || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_LIGHTMAGENTA
                || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_MAGENTA
                //|| errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR1
                || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_YELLOW
                //|| errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR2
                || errcode.n16ErrorCode == (short)CoreBoard_Warning.PUMP_BLACK
                ))
            {
                int colorindex = 0;
                switch ((CoreBoard_Warning)errcode.n16ErrorCode)
                {
                    case CoreBoard_Warning.PUMP_CYAN:
                        colorindex = 1;
                        break;
                    case CoreBoard_Warning.PUMP_MAGENTA:
                        colorindex = 2;
                        break;
                    case CoreBoard_Warning.PUMP_YELLOW:
                        colorindex = 3;
                        break;
                    case CoreBoard_Warning.PUMP_BLACK:
                        colorindex = 0;
                        break;
                    case CoreBoard_Warning.PUMP_LIGHTCYAN:
                        colorindex = 5;
                        break;
                    case CoreBoard_Warning.PUMP_LIGHTMAGENTA:
                        colorindex = 4;
                        break;
                        //case CoreBoard_Warning.PUMP_SPOTCOLOR1:
                        //    colorindex = 6;
                        //    break;
                        //case CoreBoard_Warning.PUMP_SPOTCOLOR2:
                        //    colorindex = 7;
                        //    break;
                }
                ColorEnum colorEnum = m_allParam.PrinterProperty.Get_ColorEnum(colorindex % m_allParam.PrinterProperty.nColorNum);
                string colorName = ResString.GetEnumDisplayName(typeof(ColorEnum), colorEnum);
                errStr = string.Format(ResString.GetResString("Pumping_Ink"), colorName);
                //errStr = string.Format("{0}正在泵墨", colorName);
            }
#endif
            //if (errcode.nErrorCause == (byte)ErrorCause.Software && errcode.nErrorCode == (byte)Software.MediaTooSmall)
            if (errcode.nErrorCause == (byte)ErrorCause.Software)
            {
                if (m_JobListForm.IsFristCopiesOrNoJobPrint())
                {
                    if (errcode.nErrorCode == (byte) Software.MediaTooSmall)
                    {
                        errStr = SErrorCode.GetResString("Software_MediaWidthTooSmall");
                        float realW = UIPreference.ToDisplayLength(m_allParam.Preference.Unit, PubFunc.CalcRealJobWidth(this.m_JobListForm.PrintingJob.JobSize.Width, m_allParam));
                        errStr = string.Format(errStr, realW.ToString() + m_allParam.Preference.GetUnitDisplayName());
                    }
                    if (errcode.nErrorCode == (byte) Software.MediaHeightTooSmall)
                    {
                        errStr = SErrorCode.GetResString("Software_MediaHeightTooSmall");
                    }
                }
            }
            if (bColorChange)
                errStr += "< ... 2 >";
            this.m_StatusBarPanelError.Text = errStr;

            this.m_PrintInformation.printJobInfomation(code, UserLevel.user, errStr);
            //			this.m_PreviewAndInfo.printJobInfomation(code,UserLevel.user);
            //this.m_PreviewAndInfo.SetPrinterStatusChanged(CoreInterface.GetBoardStatus());
        }

        public void OnEditPrinterSetting()
        {
            SettingForm form = new SettingForm();
            form.SetGroupBoxStyle(m_GroupboxStyle);
            m_FuncSettingForm = form;
            //OnPrinterStatusChanged(printerStatus);
            form.OnPreferenceChange(m_allParam.Preference);
            form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            //Change the IconButton status for calibration
            // SetPrinterStatusChanged应在OnPrinterPropertyChange后调用,SetPrinterStatusChanged中用的了PrinterProperty
            m_FuncSettingForm.SetPrinterStatusChanged(printerStatus);
            //获得DoubleYAxis
            bool bDoubleYAxis = PubFunc.IsDoubleYAxis();
            DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
            if (bDoubleYAxis)
            {
                if (EpsonLCD.GetDoubleYAxis_Info(ref doubleYaxis))
                {
                    m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis = doubleYaxis;
                }
                else
                {
                    MessageBox.Show(ResString.GetResString("StrFwNoSurportDoubleY"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //获得3D Machine info
            bool bSupport3DPrint = false;
            S_3DPrint s_3DPrint = new S_3DPrint();
            if (PubFunc.Is3DPrintMachine() && EpsonLCD.Get3DPrint_Info(ref s_3DPrint))
            {
                byte[] byteflag = BitConverter.GetBytes(s_3DPrint.Flag);
                if (Encoding.ASCII.GetString(byteflag) == "FeH\0")
                {
                    bSupport3DPrint = true;
                    m_allParam.s3DPrint = s_3DPrint;
                }
            }

            //获得用户扩展设置情报
            bool bUserExtensionSupport = false;
            PositionSetting_LeCai sPS_lecai = new PositionSetting_LeCai();
            if (EpsonLCD.GetPosition_Info(ref sPS_lecai) &&
                Encoding.ASCII.GetString(BitConverter.GetBytes(sPS_lecai.nFlag)) == "LEC\0")
            {
                bUserExtensionSupport = true;
                m_allParam.sPS_lecai = sPS_lecai;
            }

            //设置Form更新
            bool isFlora = SPrinterProperty.IsFloraT50OrT180();
            if (isFlora)
            {
                EpsonLCD.GetFloraParam(ref m_allParam);
            }
            form.OnPrinterSettingChange(m_allParam);
            form.OnRealTimeChange();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                bool bChangeProperty = false;
                form.OnGetPrinterSetting(ref m_allParam, ref bChangeProperty);

                //				if(SPrinterProperty.EPSONLCD_DEFINED)
                //					EpsonLCD.SetCalibrationSetting(m_allParam.PrinterSetting.sCalibrationSetting);
                if (m_allParam.PrinterProperty.EPSONLCD_DEFINED)
                {
                    EpsonLCD.SetPrint_Quality(m_allParam.EpsonAllParam.sUSB_Print_Quality);
                    EpsonLCD.SetMedia_Info(m_allParam.EpsonAllParam.sUSB_RPT_Media_Info);
                    EpsonLCD.SetHeadparameter(m_allParam.EpsonAllParam.headParameterPercent);
                    EpsonLCD.SetCleaningOption(m_allParam.EpsonAllParam.sCLEANPARA);
                }
                OnPreferenceChange(m_allParam.Preference);
                if (bChangeProperty)
                    CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
                m_ToolbarSetting.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                m_ToolbarSetting.OnPrinterSettingChange(m_allParam, this.m_allParam.EpsonAllParam);
                m_ToolbarSetting.OnGetPrinterSetting(ref m_allParam, ref this.m_allParam.EpsonAllParam);
                m_allParam.PrinterProperty.SynchronousCalibrationSettings(ref m_allParam.PrinterSetting);
                //双轴偏移设定
                if (bDoubleYAxis)
                {
                    doubleYaxis.DrvEncRatio1 =  m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis.DrvEncRatio1;
                    doubleYaxis.DrvEncRatio2 = m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis.DrvEncRatio2;
                    doubleYaxis.DoubeYRatio = m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis.DoubeYRatio;
                    doubleYaxis.fMaxTolerancepos = m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis.fMaxTolerancepos;
                    doubleYaxis.bCorrectoffset = m_allParam.PrinterSetting.sExtensionSetting.sDouble_Yaxis.bCorrectoffset;
                    EpsonLCD.SetDoubleYAxis_Info(doubleYaxis);
                }

                //set 3D Machine info
                if (bSupport3DPrint)
                {
                    EpsonLCD.Set3DPrint_Info(m_allParam.s3DPrint);
                }

                //设定用户扩展设置
                if (bUserExtensionSupport)
                {
                    EpsonLCD.SetPosition_Info(m_allParam.sPS_lecai);
                }
                EpsonLCD.SetPeripheralExtendedSettings(m_allParam);
                if (isFlora)
                {
                    EpsonLCD.SetFloraParam(m_allParam);
                }
                m_ToolbarSetting.JobConfigList_Bind(true);
                m_ToolbarSetting.JobMediaConfigList_Bind(true);
                LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "OnEditPrinterSetting");
            }
            form = null;
            m_FuncSettingForm = null;
        }


        public void OnPrintingProgressChanged(int percent)
        {
            string info = "";
            string mPrintingFormat = ResString.GetPrintingProgress();
            info += string.Format(mPrintingFormat, percent);
            this.m_StatusBarPanelPercent.Text = info;
            this.m_PrintInformation.PrintString(info, UserLevel.user, ErrorAction.Updating);
            this.m_JobListForm.OnPrintingProgressChanged(percent);
            m_PrintPercent = percent;
        }
       
        /// <summary>
        /// 当前已打印份数
        /// </summary>
        public int PrintedCopies = 0;
        /// <summary>
        /// 当前正在打印份数
        /// </summary>
        public int PrintingPage = 0;
        public void OnPrintingStart()
        {
            m_bDuringPrinting = true;
            m_StartTime = DateTime.Now;
            m_JobListForm.OnPrintingStart();
            m_PrintingJob = m_JobListForm.PrintingJob;
            //this.toolBarButtonDoublePrintCari.Enabled = m_bDuringPrinting;
            if (PubFunc.IsFhzl3D())
            {
                LogLastPrintJob(m_PrintingJob);
            }
        }

        private void LogLastPrintJob(UIJob mPrintingJob)
        {
            try
            {
                if (mPrintingJob != null)
                {
                    string strStartTime = string.Format("{0}-{1}", m_StartTime.ToShortDateString(),
                        m_StartTime.ToShortTimeString());
                    string loginfo = string.Format("{0}; {1}; layer={2};",
                        new object[] { m_PrintingJob.FileLocation, strStartTime, mPrintingJob.JobID });
                    string path = Path.Combine(Application.StartupPath, "LastPrintJob.txt");
                    StreamWriter sw;
                    if (!File.Exists(path))
                        sw = File.CreateText(path);
                    else
                        sw = new StreamWriter(path, false);
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + " : " + loginfo);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                LogWriter.WriteLog(new string[] { ex.Message }, true);
            }

        }

        public void OnPrintingEnd()
        {
            try
            {
                m_JobListForm.OnChangedCalcuTime();
                m_bDuringPrinting = false;
                PrintedCopies++;
                CalcuJobPrint();
                m_JobListForm.OnPrintingEnd();
                m_PreviewAndInfo.UpdatePercentage(0);
                LogPrintedArea(m_PrintPercent);
                //m_PrintPercent = 0;
                m_PrintingJob = null;
                //this.toolBarButtonDoublePrintCari.Enabled = m_bDuringPrinting;
                if ((SPrinterProperty.IsGongZeng() || SPrinterProperty.IsRuiZhi()) && InkAreaStaticsHelper.CanSynchronizeInkAndArea() && !InkAreaStaticsHelper.BoardHasChanged())
                {
                    InkAreaStaticsHelper.SynchronizeInkAndArea(BYHXSoftLock.GetDateTime());//墨量同步
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 统计作业打印参数
        /// </summary>
        private void CalcuJobPrint()
        {
            UIJob job = m_JobListForm.PrintingJob;
            if (job == null)
            {
                PrintedCopies = 0;
                return;
            }
            SumPrintTime(job);
            if (PrintedCopies != job.Copies
              && !job.IsAbort)
            {
                //当前作业未打印完成
                return;
            }
            job.PrintedDate = DateTime.Now;
            float printedWidth = getPrintedWidth();
            if (job.AllCopiesTime != TimeSpan.Zero)
                PrintRecordService.GetInstance().CalculateInk(job, printedWidth, PrintedCopies, m_JobListForm.PrintingPage(), _printPercent, this.m_allParam.PrinterSetting);

            PrintingPage = 0;
            PrintedCopies = 0;
            PrintRecordService.GetInstance().Save();

        }
        /// <summary>
        /// 获取打印宽度 m
        /// </summary>
        /// <returns></returns>
        private float getPrintedWidth()
        {
            float width = 0;
            if (m_JobListForm.PrintingJob.PrtFileInfo.sFreSetting.nResolutionX != 0)
            {
                width = m_JobListForm.PrintingJob.JobSize.Width;
                width = PubFunc.CalcRealJobWidth(width, this.m_allParam);
            }
            float height = 0;
            if (m_JobListForm.PrintingJob.PrtFileInfo.sFreSetting.nResolutionY != 0)
            {
                height = m_JobListForm.PrintingJob.JobSize.Height;
            }
            return UIPreference.ToDisplayLength(UILengthUnit.Meter, width);
        }
        /// <summary>
        /// 累计当前作业已打印时间,
        /// </summary>
        private void SumPrintTime(UIJob printingJob)
        {
            if (printingJob != null)
            {
                TimeSpan time = m_JobListForm.m_PreviewAndInfo.CalcuPrintTime.Elapsed;//DateTime.Now - printerService.StartTime;
                printingJob.AllCopiesTime = printingJob.UsedTime
                                                          +
                                                          new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
                    //精确到秒，去掉毫秒
                printingJob.UsedTime = printingJob.AllCopiesTime;
            }
        }
        #endregion

        #region protected

        #endregion

        #endregion

        #region override
        protected override void WndProc(ref Message m)
        {
            if (m.WParam.ToInt64() == 0xF060)   //   关闭消息   
            {
                string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Exit);
                if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            if (MyWndProc.NCButtonClick(m, this, true))
                base.WndProc(ref m);

            if (m.Msg == 0x0219) //WM_DEVICECHANGE
            {
                switch ((uint)m.WParam)
                {
                    case Common.DBT_DEVICEARRIVAL:
                        break;
                    case Common.DBT_DEVICEREMOVECOMPLETE:
                    {
                        DEV_BROADCAST_DEVICEINTERFACE pDbt =
                            (DEV_BROADCAST_DEVICEINTERFACE)
                                Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_DEVICEINTERFACE));
                        // Check GUID to see whether it's a SenseLock EL device. Notice:the GUIDs of HID devices are 
                        // of the same,so we have to make further check on PID and VID
                        if (pDbt.dbcc_classguid == Common.GUID_CLASS_ITOKEN2)
                        {
                            // 机密狗被拔掉,软件关闭前,如果正在使用主界面按钮移动,则先停止移动,否则会一直移动无法停止
                            AutoStopMoveAndUpdateUI();
                        }
                        break;
                    }
                }
                BYHXSoftLock.OnDeviceChange(m.WParam, m.LParam);
            }

            if (m.Msg == this.m_KernelMessage)
            {
                ProceedKernelMessage(m.WParam, m.LParam);
            }
            else if (m_allParam != null && m_allParam.Preference.ViewModeIndex != 2)
                MyWndProc.PaintFormCaption(ref m, this, true);
        }

        //		protected override void OnResize(EventArgs e) 
        //		{ 
        //			if (this.WindowState == FormWindowState.Maximized && this.FormBorderStyle != FormBorderStyle.FixedDialog) 
        //			{ 
        //				this.FormBorderStyle = FormBorderStyle.FixedDialog;
        //			} 
        //			else if(this.FormBorderStyle != FormBorderStyle.Sizable) 
        //			{
        //				this.FormBorderStyle = FormBorderStyle.Sizable;
        //			}
        //			base.OnResize(e);
        //		}

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            switch ((UIViewMode)m_allParam.Preference.ViewModeIndex)
            {
                case UIViewMode.NotifyIcon:
                case UIViewMode.OldView:
                    break;
                case UIViewMode.LeftRight:
                case UIViewMode.TopDown:
                default:
                    MyWndProc.DrawToolbarBackGroundImage(pevent.Graphics, this.m_ToolBarCommand.Bounds, null);
                    break;
            }
        }

        #endregion

        #region 事件

        #region MainForm 事件
        private void MainApp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ScorpionCoreInterface.GetIsSupportNewErrorCode())
            {
                ScorpionCoreInterface.SetFwSupportErrorCode(PrinterStubC.Interface.ErrorFormat.old);
            }
            End();
        }
        private void menuItemDongleKey_Click(object sender, System.EventArgs e)
        {
            DongleKeyForm dkf = new DongleKeyForm();
            dkf.ShowDialog();
            if (!BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKey())
                this.Close();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
#if ADD_HARDKEY
			this.menuItemDongle.Visible = true;
#else
            this.menuItemDongle.Visible = false;
#endif
            this.m_JobListForm.PreviewFolderPath = this.m_FolderPreview.m_LastPreviewForlder;
            if (!SPrinterProperty.NoShowMeasureMsgWhenFirstReady())
                this.ShowMeasureQuestionForm(true);

#if EpsonLcd
			this.Visible = this.m_bShowUI;
			this.time.Start();
#endif
            /*****************************
			 * ip:127.0.0.1
			 * 发送端口:23
			 * 监听端口:9001
			 * **********************************/
            server = new RemoteClient(new string[] { "127.0.0.1-23-9001" }, this);

            this.serverTask.RunWorkerAsync();
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            IPAddress ipAddress = IPAddress.Loopback;
            int port = 6668;
            if (cfa.AppSettings.Settings.AllKeys.Contains("IP"))
            {
                IPAddress.TryParse(cfa.AppSettings.Settings["IP"].Value, out ipAddress);
            }
            if (cfa.AppSettings.Settings.AllKeys.Contains("Port"))
            {
                int.TryParse(cfa.AppSettings.Settings["Port"].Value,out port);
            }
#if FHZL3D
            ProtocolHandlerFhzl handlerFhzl = new ProtocolHandlerFhzl(TcpIpConst.FhzlPattern);
#else
            ProtocolHandlerByhx handlerFhzl = new ProtocolHandlerByhx(TcpIpConst.ByhxPattern);
#endif
            remoteControlServer = new RemoteControlServer(ipAddress, port, handlerFhzl);
            remoteControlServerTask.RunWorkerAsync();

            USER_SET_INFORMATION userInfo = new USER_SET_INFORMATION(true);
            bool isGetProperty = CoreInterface.GetUserSetInfo(ref userInfo) == 1;
            if (isGetProperty)
            {
                menuItemAutoStopPumpInk.Checked = userInfo.PumpType == CoreConst.ENABLE_CONTINU_PUMP_INK;
            }
                        string namePath = Application.StartupPath + "\\setup\\Vender.xml";
            if(!Authentication.HasAuthenticationed(namePath, PubFunc.GetCompileTime()))
            {
                string errStr = SErrorCode.GetInfoFromErrCode(0x40030018);
                MessageBox.Show(errStr, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            _isFirstErrorAfterLoad = false;
        }

        private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            MainForm_KeyDownEvent(e.KeyData);
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.P)
                {
                    m_JobListForm.PrintJob();
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    CoreInterface.Printer_PauseOrResume();
                }
                else if (e.KeyCode == Keys.X)
                {
                    CanleType nRet = m_JobListForm.Confirm_Exit(m_LastOperate);
                    if (nRet == CanleType.PrintingJob)
                        m_JobListForm.AbortPrintingJob();
                    else if (nRet == CanleType.All)
                        m_JobListForm.AbortAllPrintingJob(true);
                }
                else if (e.KeyCode == Keys.A)
                {
                    m_JobListForm.OpenJob();
                }
                else if (e.KeyCode == Keys.D)
                {
                    m_JobListForm.DeleteJob();
                }
                else if (e.KeyCode == Keys.E)
                {
                    OnEditPrinterSetting();
                }
            }
        }

        private void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            MainForm_KeyUpEvent(e.KeyData);
        }

        private void MainForm_SizeChanged(object sender, System.EventArgs e)
        {
            MyWndProc.DrawRoundForm(this);

            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal)
            {
                this.m_WorkForlderpanel.Width = this.Width / 3;
                this.m_FolderPreview.Height = this.Height / 3;
            }
        }

        #endregion

        #region MainMenu 事件
        private void m_MenuItemSelectInkType_Click(object sender, EventArgs e)
        {
            SelectInkTypeForm win = new SelectInkTypeForm(m_allParam);
            win.Owner = this;
            win.ShowDialog();
        }

        private void m_MenuItemAdd_Click(object sender, System.EventArgs e)
        {
            m_JobListForm.OpenJob();

        }

        private void m_MenuItemDelete_Click(object sender, System.EventArgs e)
        {
            m_JobListForm.DeleteJob();

        }

        private void m_MenuItemPrint_Click(object sender, System.EventArgs e)
        {
            m_JobListForm.PrintJob();
        }

        private void m_MenuItemExit_Click(object sender, System.EventArgs e)
        {
            string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Exit);
            if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            End();
            Application.Exit();
        }

        private void m_MenuItemSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.OverwritePrompt = true;
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Env);
            fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);

                string ext = Path.GetExtension(fileDialog.FileName);
                m_allParam.SaveToXml(fileDialog.FileName, false);
            }
        }

        private void m_MenuItemLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Env);
            fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);

                string ext = Path.GetExtension(fileDialog.FileName);
                m_allParam.LoadFromXml(fileDialog.FileName, false);
                this.OnPrinterSettingChange(m_allParam.PrinterSetting);
                if (CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting) == 0)
                {
                    Debug.Assert(false);
                }
                LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "m_MenuItemLoad_Click");
                string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.RstoreSetting);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void m_MenuItemSaveToPrinter_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(this,
                ResString.GetEnumDisplayName(typeof(Confirm), Confirm.SaveToBoard),
                ResString.GetProductName(),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {

                int iRet;
                //iRet = CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                Cursor.Current = Cursors.WaitCursor;
                iRet = m_allParam.SetFWSetting();
                Cursor.Current = Cursors.Default;
                if (iRet != 0)
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SaveSetToBoardSuccess), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveSetToBoardFail), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void m_MenuItemLoadFromPrinter_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this,
                    ResString.GetEnumDisplayName(typeof(Confirm), Confirm.LoadFromBoard),
                    ResString.GetProductName(),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    int iRet = 0;
                    Cursor.Current = Cursors.WaitCursor;
                    iRet = m_allParam.GetFWSetting();
                    Cursor.Current = Cursors.Default;
                    if (iRet != 0)
                    {
                        this.OnPrinterSettingChange(m_allParam.PrinterSetting);
                        string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.LoadSetFromBoardSuccess);
                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.LoadSetFromBoardFail);
                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.LoadSetFromBoardFail);
                MessageBox.Show(ex.Message + Environment.NewLine + info);
            }
        }

        private void m_MenuItemEdit_Click(object sender, System.EventArgs e)
        {
            OnEditPrinterSetting();
        }

        private void m_MenuItemUpdate_Click(object sender, System.EventArgs e)
        {
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            if (printerStatus == JetStatusEnum.Busy)
            {
                if (MessageBox.Show(this,
                        ResString.GetResString("Confirm_Update"),
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
                    m_JobListForm.TerminatePrintingJob(false);
                }
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".dat";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Dat);
            fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
                UpdateCoreBoard(fileDialog.FileName);
            }
        }
        private void m_MenuItemPassword_Click(object sender, System.EventArgs e)
        {
            PasswordForm pwdform = new PasswordForm();
            pwdform.ShowDialog(this);

        }

        private void m_MenuItemDemoPage_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.BiDirectionCmd, 0, ref m_allParam.PrinterSetting,true);
            Cursor.Current = Cursors.Default;
        }

        private void m_MenuItemCalibraion_Click(object sender, System.EventArgs e)
        {
            m_wizard = new CaliWizard();
            m_wizard.SetGroupBoxStyle(m_GroupboxStyle);
            m_MenuItemCalibraion.Enabled = false;
            m_wizard.SetPrinterChange(this);
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            //Change the IconButton status for calibration
            m_wizard.SetPrinterStatusChanged(printerStatus);
            OnPrinterStatusChanged(printerStatus);

            m_wizard.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            m_wizard.OnPrinterSettingChange(m_allParam.PrinterSetting);
            m_wizard.Owner = this;
            //wizard.Parent = this;
            //DialogResult ret  = wizard.ShowDialog(this);
            m_wizard.Show();
            //if(ret == DialogResult.OK)
            //{
            //wizard.OnGetPrinterSetting(ref m_allParam.PrinterSetting);
            //}
        }

        private void m_MenuItemAbout_Click(object sender, System.EventArgs e)
        {
            string printHeadInfo = this.Text.Replace(m_sFormTile, "");
            AboutForm_AllWin_JetRix aboutForm = new AboutForm_AllWin_JetRix(printHeadInfo);
            aboutForm.ShowDialog(this);
        }

        FactoryDebug hwForm = new FactoryDebug();
        private KonicTemperature konicTemperatureForm;
        private void m_MenuItemFactoryDebug_Click(object sender, System.EventArgs e)
        {
            hwForm = new FactoryDebug();
            hwForm.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
            hwForm.OnPrinterSettingChange(this.m_allParam.PrinterSetting);
            hwForm.OnPreferenceChange(this.m_allParam.Preference);
            DialogResult dr = hwForm.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                hwForm.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting);
                CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
                LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "m_MenuItemFactoryDebug_Click");
            }
            EpsonLCD.GetGZClothMotionParam(ref GZClothMotionParam);
            UpdateButtonStates(CoreInterface.GetBoardStatus());
            hwForm = null;
        }

        private Waveform_Kyocera Waveform_KyoceraForm;
        private void menuItem_KYWaveform_Click(object sender, EventArgs e)
        {
            Waveform_KyoceraForm = new Waveform_Kyocera
            {
                m_printerProperty = m_allParam.PrinterProperty
            };
            Waveform_KyoceraForm.ShowDialog(this);
            Waveform_KyoceraForm = null;
        }

        private void m_MenuItemRealTime_Click(object sender, System.EventArgs e)
        {
            try
            {
                konicTemperatureForm = new KonicTemperature();
                konicTemperatureForm.SetGroupBoxStyle(m_GroupboxStyle);
                konicTemperatureForm.OnPreferenceChange(m_allParam.Preference);
                konicTemperatureForm.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                konicTemperatureForm.OnPrinterSettingChange(m_allParam.PrinterSetting);
                konicTemperatureForm.OnRealTimeChange();
                if (konicTemperatureForm.ShowDialog(this) == DialogResult.OK)
                {
                    bool bChangeProperty = false;
                    konicTemperatureForm.OnGetPrinterSetting(ref m_allParam, ref bChangeProperty);
                    CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                    LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "m_MenuItemRealTime_Click");
                    konicTemperatureForm.ApplyToBoard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                konicTemperatureForm = null;
            }
        }

        private void m_MenuItemUVSetting_Click(object sender, System.EventArgs e)
        {
            m_hwForm = new UVSettingForm(m_allParam.UvPowerLevelMap);
            m_hwForm.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
            m_hwForm.OnPrinterSettingChange(this.m_allParam);
            m_hwForm.OnPreferenceChange(this.m_allParam.Preference);
            DialogResult dr = m_hwForm.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                PeripheralExtendedSettings p = new PeripheralExtendedSettings();
                m_hwForm.OnGetPrinterSetting(ref this.m_allParam.PrinterSetting,ref p);
                CoreInterface.SetPrinterSetting(ref this.m_allParam.PrinterSetting);
            }
            m_hwForm = null;
        }
        private void m_MenuItemTopDown_Click(object sender, System.EventArgs e)
        {
            m_allParam.Preference.ViewModeIndex = (int)UIViewMode.TopDown;
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);
        }

        private void m_MenuItemLeftRight_Click(object sender, System.EventArgs e)
        {
            m_allParam.Preference.ViewModeIndex = (int)UIViewMode.LeftRight;
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);
        }

        private void m_MenuItemOldView_Click(object sender, System.EventArgs e)
        {
            m_allParam.Preference.ViewModeIndex = (int)UIViewMode.OldView;
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog(this);
        }

        private void m_MenuItemHWSetting_Click(object sender, System.EventArgs e)
        {
            SFWFactoryData fwData = new SFWFactoryData();
            bool bGet = (CoreInterface.GetFWFactoryData(ref fwData) > 0);
            if (!bGet)
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool bDocan = m_allParam.PrinterProperty.IsDocan();
            bool bEncoder = ((fwData.m_nEncoder & (byte)INTBIT.Bit_0) == 0) ? false : true;
            bool bFlat = ((fwData.m_nEncoder & (byte)INTBIT.Bit_5) == 0) ? true : false;
            if (bDocan && !m_allParam.PrinterProperty.bSupportDoubleMachine)
                bFlat = ((fwData.m_nEncoder & (byte)INTBIT.Bit_4) == 0) ? true : false;
            bool bUseYEncoder = ((fwData.m_nEncoder & (byte)INTBIT.Bit_6) == 0) ? false : true;
            PrinterHWSettingSimple form = new PrinterHWSettingSimple();
            form.SetGroupBoxStyle(m_GroupboxStyle);
            form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            form.SetLineEncoder(bEncoder);
            form.SetFlat(bFlat);
            form.SetUseYEncoder(bUseYEncoder);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                bool bEncoder1 = form.GetLineEncoder();
                bool bFlat1 = form.GetFlat();
                bool bUseYEncoder1 = form.GetUseYEncoder();
                if ((bEncoder1 != bEncoder) || (bFlat1 != bFlat) || bUseYEncoder1 != bUseYEncoder)
                {
                    byte value1 = 0;
                    if (bEncoder1)
                        value1 |= (byte)INTBIT.Bit_0;

                    if (bDocan && !m_allParam.PrinterProperty.bSupportDoubleMachine)
                    {
                        if (!bFlat1)
                            value1 |= (byte)INTBIT.Bit_4;
                    }
                    else
                    {
                        if (!bFlat1)
                            value1 |= (byte)INTBIT.Bit_5;
                    }

                    if (bUseYEncoder1)
                        value1 |= (byte)INTBIT.Bit_6;

                    SFWFactoryData fwData1 = new SFWFactoryData();
                    fwData1 = fwData;
#if !LIYUUSB
                    fwData1.m_nValidSize = 62;
                    fwData1.m_nReserve = new byte[CONSTANT.nReserveSizeConst];
#endif
                    fwData1.m_nEncoder = (byte)value1;
                    if (CoreInterface.SetFWFactoryData(ref fwData1) != 0)
                    {
                        string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SetHWSetting);
                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetHWSettingFail);
                        MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //////////////////////////////////////
                    ///
                    if ((bFlat1 != bFlat))
                    {
#if false
						if(m_allParam.PrinterProperty.bSupportDoubleMachine)
						{
							///////////////////////////////////////////////////////////////
							/// this is only for kincolor
							////////////////////////////////////////////////////////////////
							if(bFlat1)
							{
								m_allParam.PrinterSetting.sCalibrationSetting.nStepPerHead =
									m_allParam.PrinterProperty.nStepPerHead;
							}
							else
							{
								m_allParam.PrinterSetting.sCalibrationSetting.nStepPerHead = 
									m_allParam.PrinterProperty.nStepPerHead*8;
							}
						}
#endif
                        if (!bFlat1)
                        {
                            m_allParam.PrinterSetting.sBaseSetting.fYOrigin = 0.0f;
                            m_allParam.PrinterSetting.sBaseSetting.bYPrintContinue = true;
                        }
                        CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                        LogWriter.LogSetPrinterSetting(m_allParam.PrinterSetting, m_bDuringPrinting, "m_MenuItemHWSetting_Click");
                        m_ToolbarSetting.OnPrinterSettingChange(m_allParam, this.m_allParam.EpsonAllParam);
                    }

                }
            }
        }

        #endregion

        #region Tool Strip 事件

        private void m_ToolBarCommand_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == m_ToolBarButtonPauseResume)
            {
                if (PubFunc.Is3DPrintMachine() && m_JobListForm.WaitingPauseBetweenLayers && m_allParam.PrinterSetting.sExtensionSetting.bAutoPausePerPage)
                {
                    m_allParam.PrinterSetting.sExtensionSetting.bAutoPausePerPage = false;
                    m_ToolbarSetting.OnPrinterSettingChange(m_allParam, m_allParam.EpsonAllParam);
                    return;
                }
                if (CoreInterface.CurBoardStatus == JetStatusEnum.Pause)
                {
                    if (!m_JobListForm.CheckMediaHeaterTemp())
                        return;
                }
                CoreInterface.Printer_PauseOrResume();
            }
            else if (e.Button == m_ToolBarButtonAbort)
            {
                //CoreInterface.Printer_Abort();
                if (m_LastOperate.CanMoveStop && m_allParam.PrinterProperty.IsZMeasurSupport)
                {
                    int len = 0;
                    const int port = 1;
                    const byte PRINTER_PIPECMDSIZE = 26;
                    byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                    m_pData[0] = 4 + 2;
                    m_pData[1] = 0x57; ////SciCmd_CMD_AbortMeasure  = 0x57
                    m_pData[2] = (byte)(len & 0xff);
                    m_pData[3] = (byte)((len >> 8) & 0xff);
                    m_pData[4] = (byte)((len >> 16) & 0xff);
                    m_pData[5] = (byte)((len >> 24) & 0xff);

                    CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
                }
                else
                {
                    CanleType nRet = m_JobListForm.Confirm_Exit(m_LastOperate);
                    if (nRet == CanleType.PrintingJob)
                        m_JobListForm.AbortPrintingJob();
                    else if (nRet == CanleType.All)
                        m_JobListForm.AbortAllPrintingJob(true);
                }
                //m_JobListForm.TerminatePrintingJob(true);
            }
            else if (e.Button == m_ToolBarButtonAdd)
            {
                if (PubFunc.Is3DPrintMachine())
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Please select the working directory";
                    fbd.SelectedPath = this.m_JobListForm.FolderName;
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        this.m_JobListForm.FolderName = fbd.SelectedPath;
                    }
                }
                else
                {
                    m_JobListForm.OpenJob();
                }
            }
            else if (e.Button == m_ToolBarButtonDelete)
            {
                m_JobListForm.DeleteJob();
            }
            else if (e.Button == m_ToolBarButtonEdit)
            {
                OnEditPrinterSetting();
                //m_JobListForm.EditJob();
            }
            else if (e.Button == m_ToolBarButtonPrint)
            {
                m_JobListForm.PrintJob();
            }
            else if (e.Button == m_toolBarButtonRetractableCloth)
            {
                if (m_toolBarButtonRetractableCloth.Pushed)
                {
                    EpsonLCD.RetractableClothBegin();
                }
                else
                {
                    EpsonLCD.RetractableClothEnd();
                }
            }
            else if (e.Button == m_toolBarButtonRollFlat)
            {
                byte selectedType = 0;
                if (!EpsonLCD.GetRollOrFlat(ref selectedType)) return;
                switch (selectedType)
                {
                    case 1:
                        if (EpsonLCD.SetRoll())
                        {
                            m_toolBarButtonRollFlat.ImageIndex = 30;
                        }
                        break;
                    case 2:
                        if (EpsonLCD.SetFlat())
                        {
                            m_toolBarButtonRollFlat.ImageIndex = 29;
                        }
                        break;
                    default:
                        MessageBox.Show(string.Format("Error!"));
                        break;
                }
            }
            else if (e.Button == toolBarButtonDoublePrintCari)
            {
                //if (m_PrintingJob != null)
                {
                    _normalPrintWindow = new DoubleSidePrintForm(m_PrintingJob, this, !m_bDuringPrinting);
                    _normalPrintWindow.Closed += new EventHandler(_normalPrintWindow_Closed);
                    _normalPrintWindow.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                    _normalPrintWindow.OnPrinterSettingChange(m_allParam.PrinterSetting);
                    _normalPrintWindow.OnPreferenceChange(m_allParam.Preference);
                    _normalPrintWindow.StartPosition = FormStartPosition.CenterScreen;
                    _normalPrintWindow.Show();
                }
            }
            else if (e.Button == m_ToolBarButtonSand)
            {
                EpsonLCD.LayingSand();
            }
            else if (e.Button == toolBarButtonSwitchInk)
            {
                byte selectedInkType = 0;
                if (PubFunc.GetCurInkType(ref selectedInkType))
                {
                    byte newInkType = (byte)(selectedInkType == 1 ? 2 : 1);
                    int ret = PubFunc.SwitchInkType(m_allParam, selectedInkType, newInkType);
                    if (ret == 1)
                    {
                        toolBarButtonSwitchInk.ImageIndex = newInkType == 1 ? 25 : 26;
                        LogWriter.WriteLog(new string[] { "SwitchInkType ok!" }, true);
                    }
                    else if (ret == -1)
                    {
                        MessageBox.Show(string.Format("SwitchInkType Error!cmd {0},index {1}", 0x68, 0x02));
                        return;
                    }
                    else if (ret == -2)
                    {
                        MessageBox.Show(string.Format("UpdatePrinterSetting  SwitchInkType Error!cmd {0}", 0x04));
                    }
                }
                else
                {
                    LogWriter.WriteLog(new string[] { "GetCurInkType fail!" }, true);
                }
            }
            else if (e.Button == toolBarButtonAuxiliaryControl)
            {
                AuxiliaryControlForm auxiliaryControlForm = new AuxiliaryControlForm();
                auxiliaryControlForm.Owner = this;
                auxiliaryControlForm.StartPosition = FormStartPosition.CenterScreen;

                auxiliaryControlForm.OnPrinterPropertyChanged(this.m_allParam.PrinterProperty);
                auxiliaryControlForm.ShowDialog();
            }
            else if (e.Button == m_ToolBarColorSeperationPurge)
            {
                ColorSeparationPurgeForm purgeForm = new ColorSeparationPurgeForm();
                purgeForm.Owner = this;
                purgeForm.StartPosition = FormStartPosition.CenterScreen;
                purgeForm.OnPrinterPropertyChanged(this.m_allParam.PrinterProperty);
                purgeForm.ShowDialog();
            }
            else if (e.Button == toolBarButtonOnlineState)
            {
                var result = MessageBox.Show("是否关闭机器电源", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                byte[] buf = new byte[1];
                uint bufsize = (uint)buf.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x05);
                if (ret == 0)
                {
                    MessageBox.Show("Send shutdown commond error!");
                }
            }
            else if (e.Button == toolBarButtonPretreatment)//彩神喷淋预处理液
            {
                byte[] buf = new byte[1];
                uint bufsize = (uint)buf.Length;
                int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xA3);
                if (ret == 0)
                {
                    MessageBox.Show("Send Prepare commond error!");
                }
            }
            else
            {
                JetCmdEnum cmd;
                int cmdvalue = 0;
                if (e.Button == m_ToolBarButtonCheckNozzle)
                {
                    if ((SPrinterProperty.IsSimpleUV()||SPrinterProperty.IsGongZeng())
                        && m_allParam.PrinterProperty.IsZMeasurSupport
                        && m_allParam.Preference.bShowMeasureFormBeforPrint)
                    {
                        DialogResult? result= this.ShowMeasureQuestionForm(true,true);
                        if (!result.HasValue || result.Value != DialogResult.OK)
                            return;
                    }
                    CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckNozzleCmd, 0, ref this.m_allParam.PrinterSetting, true);
                    return;
                }
                else if (e.Button == m_ToolBarButtonAutoClean)
                    cmd = JetCmdEnum.AutoSuckHead;
                else if (e.Button == m_ToolBarButtonSpray)
                {
                    if (m_allParam.PrinterProperty.bSupportHandFlash)
                    {
                        if (e.Button.Pushed == true)
                            cmd = JetCmdEnum.StartSpray;
                        else
                            cmd = JetCmdEnum.StopSpray;
                    }
                    else
                        cmd = JetCmdEnum.FireSprayHead;
                }
                else if (e.Button == m_ToolBarButtonSingleClean)
                {
                    if (m_allParam.PrinterProperty.eSingleClean == SingleCleanEnum.PureManual)
                    {
                        if (e.Button.Pushed == true)
                            cmd = JetCmdEnum.EnterSingleCleanMode;
                        else
                            cmd = JetCmdEnum.ExitSingleCleanMode;
                    }
                    else
                    {
                        OnSingleClean(); 
                        return;
                    }
                }
                else if (e.Button == m_ToolBarButtonGoHome)
                    cmd = JetCmdEnum.BackToHomePoint;
                else if (e.Button == m_ToolBarButtonGoHomeY)
                {
                    cmd = JetCmdEnum.BackToHomePointY;
                    cmdvalue = (int)AxisDir.Y;
                }
                else if (e.Button == m_ToolBarButtonGoHomeZ)
                {
                    cmd = JetCmdEnum.BackToHomePointY;
                    cmdvalue = (int)AxisDir.Z;
                }
                else if (e.Button == m_ToolBarButtonSetOrigin)
                {
                    // epson 时,触发epson机器内置的设原点
                    if (SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead))
                    {
                        uint bufsize = 64;
                        byte[] buf = new byte[bufsize];
                        CoreInterface.SetEpsonEP0Cmd(0x7f, buf, ref bufsize, 17, 0);
                        return;
                    }
                    cmd = JetCmdEnum.SetOrigin;
                    cmdvalue = (int)AxisDir.X;
                }
                else if (e.Button == m_ToolBarButtonSetOriginY)
                {
                    cmd = JetCmdEnum.SetOrigin;
                    cmdvalue = (int)AxisDir.Y;
                }
                else if (e.Button == m_ToolBarButtonMeasurePaper)
                {
                    cmd = JetCmdEnum.MeasurePaper;
                    if (m_allParam.PrinterProperty.IsZMeasurSupport)
                    {
                        this.ShowMeasureQuestionForm(true);
                        return;
                    }
                }
                else if (e.Button == m_ToolBarButtonStop)
                {
                    //if(CoreInterface.GetBoardStatus() == JetStatusEnum.Moving)
                    cmd = JetCmdEnum.StopMove;
                }
                else if (e.Button == m_ToolBarButtonZero)
                {
                    CoreInterface.SendJetCommand((int)JetCmdEnum.BackToServiceStation, 0);
                    return;
                }
                else
                    return;
                CoreInterface.SendJetCommand((int)cmd, cmdvalue);
            }
        }

        void _normalPrintWindow_Closed(object sender, EventArgs e)
        {
            _normalPrintWindow = null;
        }

        private void m_ToolBarCommand_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_ToolBarButtonLeft.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonLeft.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Left;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonLeft.Pushed = true;
                m_bSendMoveCmd = true;
            }
            else if (m_ToolBarButtonRight.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonRight.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Right;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonRight.Pushed = true;
                m_bSendMoveCmd = true;
            }
            else if (m_ToolBarButtonForward.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonForward.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Down;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonForward.Pushed = true;
                m_bSendMoveCmd = true;
            }
            else if (m_ToolBarButtonBackward.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonBackward.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Up;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonBackward.Pushed = true;
                m_bSendMoveCmd = true;
            }
            else if (m_ToolBarButtonDownZ.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonDownZ.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Down_Z;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonDownZ.Pushed = true;
                m_bSendMoveCmd = true;
            }
            else if (m_ToolBarButtonUpZ.Rectangle.Contains(e.X, e.Y) && m_ToolBarButtonUpZ.Enabled)
            {
                MoveDirectionEnum dir = MoveDirectionEnum.Up_Z;
                dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
                int speed = GetSpeedWithDir(dir);
                CoreInterface.MoveCmd((int)dir, 0, speed);

                m_ToolBarButtonUpZ.Pushed = true;
                m_bSendMoveCmd = true;
            }
        }

        private void m_ToolBarCommand_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AutoStopMoveAndUpdateUI();
        }

        #endregion

        #region 主工作区内控件事件
        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.toolBarButtonWorkForlder)
            {
                if (PubFunc.Is3DPrintMachine())
                {
                    m_JobListForm.OpenJob();
                }
                else
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Please select the working directory";

                    if (m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
                        fbd.SelectedPath = this.m_FolderPreview.FolderName;
                    else
                        fbd.SelectedPath = this.m_JobListForm.FolderName;
                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        if (m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
                        {
                            this.m_FolderPreview.FolderName = fbd.SelectedPath;
                            this.m_JobListForm.PreviewFolderPath = this.m_FolderPreview.m_LastPreviewForlder;
                        }
                        else
                        {
                            this.m_JobListForm.FolderName = fbd.SelectedPath;
                        }
                    }
                }
            }
            else if (e.Button == this.toolBarButtonRefresh)
            {
                if (m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
                {
                    //					if(Directory.Exists(this.m_FolderPreview.m_LastPreviewForlder))
                    //						Directory.Delete(this.m_FolderPreview.m_LastPreviewForlder,true);
                    this.m_FolderPreview.ReLoadItems();
                }
                else
                {
                    this.m_JobListForm.ReLoadItems();
                }
            }
            else if (e.Button == this.toolBarButtonAddToList)
            {
                this.AddSelectedToList();
            }
            else if (e.Button == this.toolBarButtonSelectAll)
            {
                if (m_allParam.Preference.ViewModeIndex == (int)UIViewMode.LeftRight)
                {
                    this.m_FolderPreview.SelectAll();
                }
                else
                {
                    this.m_JobListForm.SelectAll();
                }
            }
        }


        private void m_FolderPreview_ItemActivate(object sender, System.EventArgs e)
        {
            this.AddSelectedToList();
        }

        private void menuItemAddToList_Click(object sender, System.EventArgs e)
        {
            this.AddSelectedToList();
        }

        private void menuItemReLoad_Click(object sender, System.EventArgs e)
        {
            this.m_FolderPreview.ReLoadItems();
        }

        private void menuItemSelectAll_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem lvi in this.m_FolderPreview.Items)
            {
                lvi.Selected = true;
            }
        }

        private void MenuFolderPreview_Popup(object sender, System.EventArgs e)
        {
            this.menuItemAddToList.Enabled = this.m_FolderPreview.SelectedItems.Count > 0;
        }


        private void m_PrintInformation_StartButtonClicked(object sender, System.EventArgs e)
        {
            this.ContextMenu = this.m_StartMenu;
            Graphics g = this.CreateGraphics();
            // calculate the menu height
            int ItemHeight = this.officeMenus1.MeasureItemHeight(this.m_MainMenu.MenuItems[0], new MeasureItemEventArgs(g, 0));// SystemInformation.MenuHeight;
            g.Dispose();
            int itemscount = 0;//this.m_MenuItemDebug.Visible?m_StartMenu.MenuItems.Count:m_StartMenu.MenuItems.Count-1;
            for (int i = 0; i < m_StartMenu.MenuItems.Count; i++)
                if (m_StartMenu.MenuItems[i].Visible)
                    itemscount++;
            Point cml = new Point(0, -ItemHeight * itemscount);
            officeMenus1.Start(this);
            this.m_StartMenu.Show(this.m_PrintInformation, cml);
            officeMenus1.End(this);
            this.ContextMenu = null;
        }
        #endregion

        #endregion

        private void m_DongleKeyAlarm_EncryptDogExpired(object sender, EventArgs e)
        {
            this.UpdateFormHeaderText();
            this.m_JobListForm.TerminatePrintingJob(false);
            UpdateButtonStates(CoreInterface.GetBoardStatus());
        }

        private void m_DongleKeyAlarm_EncryptDogLast100H(object sender, EventArgs e)
        {
            MessageBox.Show(ResString.GetResString("EncryptDog_Warning"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished(object sender, EventArgs e)
        {
            UpdateButtonStates(CoreInterface.GetBoardStatus());
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
            {
                this.m_JobListForm.TerminatePrintingJob(false);
            }
            this.UpdateFormHeaderText();
        }

        private void UpdateFormHeaderText()
        {
            bool isInwearSimpleUi = SPrinterProperty.IsInwearSimpleUi();
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            if (status != JetStatusEnum.PowerOff && !isInwearSimpleUi) // inwear精简ui不显示几色几组什么喷头20170523gzw
            {
                SPrinterProperty sp = m_allParam.PrinterProperty;
                ushort headNum = sp.nHeadNum;
                int rowNumPerHead = sp.nHeadNumPerColor * sp.nOneHeadDivider;
                if (m_allParam.PrinterProperty.IsMirrorArrangement())
                    rowNumPerHead /= 2;
                if (SPrinterProperty.IsKonica512(sp.ePrinterHead)
                    || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4_7pl
                    || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4P_7pl
                    || sp.ePrinterHead == PrinterHeadEnum.RICOH_GEN4L_15pl
                    || (SPrinterProperty.IsKonica1024i(sp.ePrinterHead) && !CoreInterface.IsKm1024I_AS_4HEAD()))
                    headNum /= 2;
                else if (SPrinterProperty.IsKonica1024i(sp.ePrinterHead) && CoreInterface.IsKm1024I_AS_4HEAD())
                    headNum /= 4;
                else if (SPrinterProperty.IsPolarisOneHead4Color(sp.ePrinterHead))
                {
                    headNum /= 8;
                }
                else if (SPrinterProperty.IsPolaris(sp.ePrinterHead) 
                        || sp.ePrinterHead == PrinterHeadEnum.XAAR_1201_Y1)
                {
                    headNum /= 4;
                }
                else if (sp.ePrinterHead == PrinterHeadEnum.EGen5)
                {
                    headNum /= 8;
                }
                else if (SPrinterProperty.IsSG1024(sp.ePrinterHead))
                {
                    if (CoreInterface.IsSG1024_AS_8_HEAD())
                        headNum /= 8;
                    else
                    {
                        if (sp.nOneHeadDivider == 2)
                            headNum = (byte)(sp.nHeadNum / 2);
                    }
                }
                //else if (SPrinterProperty.IsKyocera(sp.ePrinterHead))
                //{
                //    headNum = (byte) (sp.nHeadNum/16);
                //}
                else
                {
                    if (rowNumPerHead == 0) return;
                    headNum = (byte)(sp.nHeadNum / rowNumPerHead);
                }
                if (PubFunc.IsFhzl3D())
                {
                    this.Text = m_sFormTile
                        + " " + headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head)
                        + " " + sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color);
                }
                else
                {
                    string strPrinterHead = sp.ePrinterHead.ToString();
#if INWEAR_REVO
                    if (sp.ePrinterHead == PrinterHeadEnum.Konica_KM1024i_MAE_13pl)
                    {
                        strPrinterHead = "REVO_H";
                    }
                    if (sp.ePrinterHead == PrinterHeadEnum.Konica_KM3688_6pl)
                    {
                        strPrinterHead = "REVO_E";
                    }
#endif
                    if (PubFunc.IsColorJet_0x8() && sp.ePrinterHead  == PrinterHeadEnum.Konica_KM1024i_MHE_13pl)
                    {
                        strPrinterHead = "KM6988H";
                    }

                    this.Text = m_sFormTile
                        + " " + strPrinterHead
                        + " " + headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head)
                        + " " + sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color);
                }
            }
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
            {
                this.Text = " " + ResString.GetResString("EncryptDog_Expired");
            }
        }

        private DialogResult? ShowMeasureQuestionForm(bool mainformloaded, bool isMeasureBeforePrint = false)
        {
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            if (mainformloaded
                && !SPrinterProperty.IsFloraT50OrT180()
                //&& !m_allParam.PrinterProperty.IsDocan()  //Tony: DOCAN IsZMeasurSupport &&  !bSupportZendPointSensor 
                && status != JetStatusEnum.PowerOff
                && m_allParam.PrinterProperty.IsZMeasurSupport
                && m_bFirstReady
                && m_bShowUI)// PubFunc.IsVender92())
            {
                return m_JobListForm.ShowMeasureQuestionForm(mainformloaded, isMeasureBeforePrint);
            }
            return null;
        }

        #region 主UI显/隐控制
#if EpsonLcd
		public void theout(object source, System.Timers.ElapsedEventArgs e)
		{
			ProcessMessageHandler pmh = new ProcessMessageHandler(ProcessMessage);
			this.Invoke(pmh);
		}//end fun

		public delegate void ProcessMessageHandler();

		public void ProcessMessage()
		{
			MyData data = ProcessMessaging.GetShareMem();
			if (data.ProcessID == ProcessID)
			{
				switch (data.InfoCode)
				{
					// ...处理
					case 0:
						m_bShowUI = false;
						this.ShowInTaskbar = false;
						this.WindowState = FormWindowState.Minimized;
						this.Hide();
						break;
					case 1:
						m_bShowUI = true;
						this.ShowInTaskbar = true;
						this.WindowState = FormWindowState.Maximized;
						this.Show();
						UpdateViewMode(m_allParam.Preference.ViewModeIndex);
						this.BringToFront();
						break;
					default:
						break;
				}
			}
			//	...清空data
			if(data.InfoCode != -1)
			{
				data.InfoCode = -1;
				ProcessMessaging.SetShareMem(data);
			}	
		}

#endif
        #endregion

        private void notifyIconBYHX_DoubleClick(object sender, System.EventArgs e)
        {
#if true
            m_bShowUI = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Maximized;
            this.Show();
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);
            this.BringToFront();
#else
			m_bShowUI = !m_bShowUI;
			if(m_bShowUI)
			{
				this.ShowInTaskbar = m_bShowUI;
				this.WindowState = FormWindowState.Maximized;
				this.Show();
				UpdateViewMode(m_allParam.Preference.ViewModeIndex);
				this.BringToFront();
			}
			else
			{
				this.ShowInTaskbar = false;
				this.WindowState = FormWindowState.Minimized;
				this.Hide();
			}
#endif
        }

        private void mainWin_HandleCreated(object sender, EventArgs e)
        {
            if (CoreInterface.SetMessageWindow(this.Handle, m_KernelMessage) == 0)
            {
                Debug.Assert(false);
            }
        }

        private void UpdateNotifyIconInfo(JetStatusEnum status)
        {
            string notifytext = this.Text
                + Environment.NewLine + ResString.GetEnumDisplayName(typeof(JetStatusEnum), status);
            System.Reflection.Assembly assembly = typeof(MainForm).Assembly;
            string iconpath = Application.StartupPath;
            iconpath += "\\setup\\app.ico";

            if (File.Exists(iconpath))
            {
                this.notifyIconBYHX.Icon = new Icon(iconpath);
            }
            switch (status)
            {
              
                case JetStatusEnum.Error:
                    {
                        int errorcode = CoreInterface.GetBoardError();
                        SErrorCode sec = new SErrorCode();
                      
                        notifytext = ResString.GetEnumDisplayName(typeof(JetStatusEnum), status)
                            + Environment.NewLine + SErrorCode.GetInfoFromErrCode(errorcode);
                    }
                    break;
            }
            // notifyIconBYHX.Text长度最大不能超过64个字符
            if (notifytext.Length >= 64)
                this.notifyIconBYHX.Text = notifytext.Substring(0, 60) + "...";
            else
                this.notifyIconBYHX.Text = notifytext;
            Debug.WriteLine("notifytext.Length = " + notifytext.Length.ToString());
        }


        private void mPumpInkTimer_Tick(object sender, EventArgs e)
        {
            if (m_CurErrorCode == -1)
            {
                // 其他原因导致错误消失后关闭定时器
                if (mPumpInkTimer.Enabled)
                    mPumpInkTimer.Stop();
                return;
            }
            string errorInfo = SErrorCode.GetInfoFromErrCode(m_CurErrorCode);
            MyMessageBox msg = new MyMessageBox(AutoStopMoveAndUpdateUI);
            DialogResult dr = msg.ShowPumpInkTimeOut(errorInfo, ResString.GetProductName());
            if (dr == DialogResult.Abort)
            {
                CoreInterface.ClearErrorCode(m_CurErrorCode);
                if (mPumpInkTimer.Enabled)
                    mPumpInkTimer.Stop();
                m_CurErrorCode = -1;
            }
        }

        private void menuItemSaveCaliPara_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.OverwritePrompt = true;
            fileDialog.DefaultExt = ".bin";
            fileDialog.Filter = "Binary File(*.bin)|*.bin";
            fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                EpsonLCD.SaveEpsonCaliParaToFile(fileDialog.FileName);
            }

        }

        private void menuItemLoadCaliPara_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".bin";
            fileDialog.Filter = "Binary File(*.bin)|*.bin";
            fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;

            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                EpsonLCD.LoadEpsonCaliParaFromFile(fileDialog.FileName);
            }
        }

        /// <summary>
        /// 按照设置将打印面积记入log文件
        /// </summary>
        /// <param name="percent">打印结束时的打印百分比</param>
        public void LogPrintedArea(int percent)
        {
            UIPreference up = m_allParam.Preference;
            bool bNeedLog = false;
            if (m_PrintingJob != null && up.PrintedAreaLogConfig != null && up.PrintedAreaLogConfig != string.Empty)
            {
                string[] filters = up.PrintedAreaLogConfig.Split(new char[] { '|' });
                for (int i = 0; i < filters.Length; i++)
                {
                    if (i == 0 && (filters[i] != string.Empty && filters[i] == "*"))
                    {
                        bNeedLog = true;
                        break;
                    }
                    if (filters[i] != string.Empty && filters[i] != "*" && m_PrintingJob.FileLocation.EndsWith(filters[i]))
                    {
                        bNeedLog = true;
                        break;
                    }
                }
            }

            if (bNeedLog)
            {
                string loginfo = this.GetPrintedAreaLogString();
                LogWriter.WritePrintedAreaLog(new string[] { loginfo }, true);
            }
            //m_PrintPercent = 0;
        }

        private string GetPrintedAreaLogString()
        {
            TimeSpan time = DateTime.Now - m_StartTime;
            float width = 0;
            if (m_PrintingJob.PrtFileInfo.sFreSetting.nResolutionX != 0)
            {
                width = m_PrintingJob.JobSize.Width;
                width = PubFunc.CalcRealJobWidth(width, this.m_allParam);
            }
            float height = 0;
            if (m_PrintingJob.PrtFileInfo.sFreSetting.nResolutionY != 0)
            {
                height = m_PrintingJob.JobSize.Height;
            }
            float m_fArea = UIPreference.ToDisplayLength(UILengthUnit.Meter, width) * UIPreference.ToDisplayLength(UILengthUnit.Meter, height);
            string strStartTime = string.Format("{0}-{1}", m_StartTime.ToShortDateString(), m_StartTime.ToShortTimeString());
            string strTime = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
            string strPercentage = m_PrintPercent.ToString() + "%";
            string unitStr = ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
            string strArea = (m_fArea * m_PrintPercent / 100.0f).ToString() + " " + unitStr + "2";
            return string.Format("{0}; {1}; {2}; {3}; {4}", new object[] { m_PrintingJob.FileLocation, strStartTime, strTime, strPercentage, strArea });
        }
        void backworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case -1:
                        {
                            break;
                        }
                    case 0:
                        {
                            this.m_PrintInformation.PrintString((string)e.UserState, UserLevel.manager, ErrorAction.UserResume);
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            server.Worker = (sender as BackgroundWorker);
            server.StartListening(true);
        }

        private void menuItemAdvancedMode_Click(object sender, EventArgs e)
        {
            FWValidationForm fwvf = new FWValidationForm();
            if (fwvf.ShowDialog() == DialogResult.OK)
            {
                PubFunc.IsKingColorAdvancedMode = true;
                // 先按原先逻辑设置显示或隐藏
                this.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
                SwitchToAdvancedMode(PubFunc.IsKingColorAdvancedMode);
            }
        }

        private void SwitchToAdvancedMode(bool bAdvancedMode)
        {
            // 然后再原先逻辑结果上做与运算
            this.m_ToolBarButtonAutoClean.Visible &= bAdvancedMode;
            this.m_ToolBarButtonSpray.Visible &= bAdvancedMode;
            this.m_ToolBarButtonSingleClean.Visible &= bAdvancedMode;
            this.m_ToolBarButtonSetOrigin.Visible &= bAdvancedMode;
            this.m_ToolBarButtonSetOriginY.Visible &= bAdvancedMode;
            if (PubFunc.IsFhzl3D() && (PubFunc.GetUserPermission() == (int)UserPermission.Operator))
            {
                this.m_MenuItemHWSetting.Visible = true;
            }
            else
            {
               // this.m_MenuItemHWSetting.Visible &= bAdvancedMode;
                this.m_MenuItemHWSetting.Visible = true;
            }

            this.m_ToolbarSetting.OnPrinterPropertyChange(this.m_allParam.PrinterProperty);
            this.m_ToolbarSetting.OnPrinterSettingChange(this.m_allParam, m_allParam.EpsonAllParam);
        }


        private void menuItemAutoStopPumpInk_Click(object sender, EventArgs e)
        {
            USER_SET_INFORMATION userInfo = new USER_SET_INFORMATION(true);
            bool IsGetProperty = CoreInterface.GetUserSetInfo(ref userInfo) == 1;
            if (IsGetProperty)
            {
                userInfo.PumpType = (ushort)(this.menuItemAutoStopPumpInk.Checked ? 0 : CoreConst.ENABLE_CONTINU_PUMP_INK);
                if (CoreInterface.SetUserSetInfo(ref userInfo) != 1)
                {
                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.SetHWSettingFail);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.menuItemAutoStopPumpInk.Checked = !this.menuItemAutoStopPumpInk.Checked;
                }
            }
            else
            {
                string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.GetHWSettingFail);
                MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ItemWaveSetting_Click(object sender, EventArgs e)
        {
            AboutWaveForm form = new AboutWaveForm(this.m_allParam);
            form.ShowDialog();
        }

        HapondMotorSettingForm hapondMotorSetting;
        private void menuItem7_Click(object sender, EventArgs e)
        {
            hapondMotorSetting = new HapondMotorSettingForm();
            hapondMotorSetting.Owner = this;
            hapondMotorSetting.StartPosition = FormStartPosition.CenterParent;
            hapondMotorSetting.OnPrinterStatusChanged(CoreInterface.CurBoardStatus);
            hapondMotorSetting.ShowDialog();
            hapondMotorSetting = null;
        }

        private void m_MenuItemInkStatistics_Click(object sender, EventArgs e)
        {
            InkStatistics fm=new InkStatistics();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();
        }

        private void menuItemDoubleYAxisSetting_Click(object sender, EventArgs e)
        {
            DoubleYAxisSettingForm doubleYAxisSettingForm = new DoubleYAxisSettingForm();
            doubleYAxisSettingForm.StartPosition = FormStartPosition.CenterParent;
            doubleYAxisSettingForm.Owner = this;
            doubleYAxisSettingForm.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            doubleYAxisSettingForm.OnPrinterSettingChange(m_allParam.PrinterSetting);
            doubleYAxisSettingForm.ShowDialog();
        }

        private void menuItemPrintRecord_Click(object sender, EventArgs e)
        {
            PrintRecordPreview.PrintHistoryExplorer explorer = new PrintRecordPreview.PrintHistoryExplorer();
            explorer.ShowDialog();
        }

        private void m_MenuItemRealTimeChart_Click(object sender, EventArgs e)
        {
            try
            {
                RealTimeChart form = new RealTimeChart();
                form.OnPreferenceChange(m_allParam.Preference);
                form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                form.OnPrinterSettingChange(m_allParam.PrinterSetting);
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolBar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (toolBarButtonWorkForlder.Rectangle.Contains(e.Location))
                {
                    toolBarButtonWorkForlder.ImageIndex = 3;
                }
                if (toolBarButtonRefresh.Rectangle.Contains(e.Location))
                {
                    toolBarButtonRefresh.ImageIndex = 4;
                }
                if (toolBarButtonSelectAll.Rectangle.Contains(e.Location))
                {
                    toolBarButtonSelectAll.ImageIndex = 5;
                }
            }
        }

        private void toolBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (toolBarButtonWorkForlder.Rectangle.Contains(e.Location))
                {
                    toolBarButtonWorkForlder.ImageIndex = 0;
                }
                if (toolBarButtonRefresh.Rectangle.Contains(e.Location))
                {
                    toolBarButtonRefresh.ImageIndex = 1;
                }
                if (toolBarButtonSelectAll.Rectangle.Contains(e.Location))
                {
                    toolBarButtonSelectAll.ImageIndex = 2;
                }
            }
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            if (PubFunc.Is3DPrintMachine())
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Please select the working directory";
                fbd.SelectedPath = this.m_JobListForm.FolderName;
                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    this.m_JobListForm.FolderName = fbd.SelectedPath;
                }
            }
            else
            {
                m_JobListForm.OpenJob();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {

            m_JobListForm.DeleteJob();
        }

        private void button_NozzleCheck_Click(object sender, EventArgs e)
        {
            if ((SPrinterProperty.IsSimpleUV() || SPrinterProperty.IsGongZeng())
                        && m_allParam.PrinterProperty.IsZMeasurSupport
                        && m_allParam.Preference.bShowMeasureFormBeforPrint)
            {
                DialogResult? result = this.ShowMeasureQuestionForm(true, true);
                if (!result.HasValue || result.Value != DialogResult.OK)
                    return;
            }
            CoreInterface.SendCalibrateCmd((int)CalibrationCmdEnum.CheckNozzleCmd, 0, ref this.m_allParam.PrinterSetting, true);
        }

        private bool button_SprayStart = true;
        private void button_Spray_Click(object sender, EventArgs e)
        {
            JetCmdEnum cmd;
            int cmdvalue = 0;
            if (m_allParam.PrinterProperty.bSupportHandFlash)
            {
                if (button_SprayStart == true)
                {
                    cmd = JetCmdEnum.StartSpray;
                    button_SprayStart = false;
                }
                else
                {
                    cmd = JetCmdEnum.StopSpray;
                    button_SprayStart = true;
                }
            }
            else
                cmd = JetCmdEnum.FireSprayHead;
            CoreInterface.SendJetCommand((int)cmd, cmdvalue);
        }

        private void button_MoveToHome_Click(object sender, EventArgs e)
        {

            JetCmdEnum cmd;
            int cmdvalue = 0;
            cmd = JetCmdEnum.BackToHomePoint;
            CoreInterface.SendJetCommand((int)cmd, cmdvalue);
        }
        private bool button_Left_MouseDownFlag = false;
        private void button_Left_MouseDown(object sender, MouseEventArgs e)
        {
            button_Left_MouseDownFlag = true;
            MoveDirectionEnum dir = MoveDirectionEnum.Left;
            dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
            int speed = GetSpeedWithDir(dir);
            CoreInterface.MoveCmd((int)dir, 0, speed);

            m_ToolBarButtonLeft.Pushed = true;
            m_bSendMoveCmd = true;
        }

        private void button_Left_MouseUp(object sender, MouseEventArgs e)
        {
            button_Left_MouseDownFlag = false;
            AutoStopMoveAndUpdateUI();
        }
        private bool button_Right_MouseDownFlag = false;
        private void button_Right_MouseDown(object sender, MouseEventArgs e)
        {
            button_Right_MouseDownFlag = true;
            MoveDirectionEnum dir = MoveDirectionEnum.Right;
            dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);
            int speed = GetSpeedWithDir(dir);
            CoreInterface.MoveCmd((int)dir, 0, speed);

            m_ToolBarButtonRight.Pushed = true;
            m_bSendMoveCmd = true;
        }

        private void button_Right_MouseUp(object sender, MouseEventArgs e)
        {
            button_Right_MouseDownFlag = false;
            AutoStopMoveAndUpdateUI();
        }
        //将回到原点改成设置原点2018-10-31
        private void button_MoveToOrigin_Click(object sender, EventArgs e)
        {
            JetCmdEnum cmd;
            int cmdvalue = 0;
            if (SPrinterProperty.IsEpson(m_allParam.PrinterProperty.ePrinterHead))
            {
                uint bufsize = 64;
                byte[] buf = new byte[bufsize];
                CoreInterface.SetEpsonEP0Cmd(0x7f, buf, ref bufsize, 17, 0);
                return;
            }
            cmd = JetCmdEnum.SetOrigin;
            cmdvalue = (int)AxisDir.X;
            CoreInterface.SendJetCommand((int)cmd, cmdvalue);
        }
        private bool button_Forward_MouseDownFlag = false;
        private void button_Forward_MouseDown(object sender, MouseEventArgs e)
        {
            button_Forward_MouseDownFlag = true;
            MoveDirectionEnum dir = MoveDirectionEnum.Down;
            dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

            int speed = GetSpeedWithDir(dir);
            CoreInterface.MoveCmd((int)dir, 0, speed);

            m_ToolBarButtonForward.Pushed = true;
            m_bSendMoveCmd = true;
        }

        private void button_Forward_MouseUp(object sender, MouseEventArgs e)
        {
            button_Forward_MouseDownFlag = false;
            AutoStopMoveAndUpdateUI();
        }

        private bool button_Backward_MouseDownFlag = false;
        private void button_Backward_MouseDown(object sender, MouseEventArgs e)
        {
            button_Backward_MouseDownFlag = true;
            MoveDirectionEnum dir = MoveDirectionEnum.Up;
            dir = PubFunc.GetRealMoveDir(dir, m_allParam.PrinterProperty, m_allParam.Preference);

            int speed = GetSpeedWithDir(dir);
            CoreInterface.MoveCmd((int)dir, 0, speed);

            m_ToolBarButtonBackward.Pushed = true;
            m_bSendMoveCmd = true;
        }

        private void button_Backward_MouseUp(object sender, MouseEventArgs e)
        {
            button_Backward_MouseDownFlag = false;
            AutoStopMoveAndUpdateUI();
        }

        private void button_Setting_Click(object sender, EventArgs e)
        {

            OnEditPrinterSetting();
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            if (PubFunc.Is3DPrintMachine() && m_JobListForm.WaitingPauseBetweenLayers && m_allParam.PrinterSetting.sExtensionSetting.bAutoPausePerPage)
            {
                m_allParam.PrinterSetting.sExtensionSetting.bAutoPausePerPage = false;
                m_ToolbarSetting.OnPrinterSettingChange(m_allParam, m_allParam.EpsonAllParam);
                return;
            }
            if (CoreInterface.CurBoardStatus == JetStatusEnum.Pause)
            {
                if (!m_JobListForm.CheckMediaHeaterTemp())
                    return;
            }
            CoreInterface.Printer_PauseOrResume();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            //CoreInterface.Printer_Abort();
            if (m_LastOperate.CanMoveStop && m_allParam.PrinterProperty.IsZMeasurSupport)
            {
                int len = 0;
                const int port = 1;
                const byte PRINTER_PIPECMDSIZE = 26;
                byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                m_pData[0] = 4 + 2;
                m_pData[1] = 0x57; ////SciCmd_CMD_AbortMeasure  = 0x57
                m_pData[2] = (byte)(len & 0xff);
                m_pData[3] = (byte)((len >> 8) & 0xff);
                m_pData[4] = (byte)((len >> 16) & 0xff);
                m_pData[5] = (byte)((len >> 24) & 0xff);

                CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
            }
            else
            {
                CanleType nRet = m_JobListForm.Confirm_Exit(m_LastOperate);
                if (nRet == CanleType.PrintingJob)
                    m_JobListForm.AbortPrintingJob();
                else if (nRet == CanleType.All)
                    m_JobListForm.AbortAllPrintingJob(true);
            }
            //m_JobListForm.TerminatePrintingJob(true);
        }
        private bool isPrintEvent = false;
        private void button_Print_Click(object sender, EventArgs e)
        {
            isPrintEvent = true;
            button_Print.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(54)))), ((int)(((byte)(84)))));
            Thread printButtonThread = new Thread(listenprint);
            printButtonThread.IsBackground = true;
            printButtonThread.Start();
            m_JobListForm.PrintJob();
        }
        private void listenprint() {
            Thread.Sleep(2000);
            while (true) {
                if (!isPrinting && isPrintEvent) {
                    isPrintEvent = false;
                    button_Print.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(113)))), ((int)(((byte)(244)))));
                    break;
                }
            }
            Thread.CurrentThread.Abort();
        }
    }
}
