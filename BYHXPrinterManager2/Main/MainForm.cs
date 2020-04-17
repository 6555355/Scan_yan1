using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using BYHXPrinterManager.Calibration;
using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Main;
using BYHXPrinterManager.Port;
using BYHXPrinterManager.Setting;

namespace BYHXPrinterManager
{
    public partial class MainForm : Form, IPrinterChange //, IMessageFilter
    {
        #region 常量

        private const int WM_KEYDOWN = 0x0100;

        private const int WM_KEYUP = 0x0101;

        private const int WM_SYSKEYDOWN = 0x0104;

        private const int WM_SYSKEYUP = 0x0105;

        #endregion

        #region 变量

        private AllParam m_allParam;

        private PortManager m_PortManager;

        private uint m_KernelMessage = SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");

        private CaliWizard m_wizard = null;

        private bool m_bSendMoveCmd = false;

        private PrinterOperate m_LastOperate = new PrinterOperate();

        private string m_sFormTile = "";

        private MyMessageBox m_MyMessageBox = null;

        private DateTime m_StartTime = DateTime.Now;

        private JetStatusEnum m_LastPrinterStatus = JetStatusEnum.PowerOff;

        private bool m_bFirstReady = false;

        private bool m_bExitAtStart = false;

        private bool isDirty = false;

        private bool m_setted = false;

        private int m_nCurNum = 0;

        private bool m_bPaused = false;

        private string mVersionText = string.Empty;


        #endregion

        #region 构造函数

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            if (PubFunc.IsInDesignMode()) return;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            if (MyWndProc.LoadRenderResource(""))
            {
                this.ControlBox = false;
                MyWndProc.SetWindowTheme(this.Handle, "", "");
            }

            MyWndProc.DrawRoundForm(this);
            //this.Text = "BYHX Printer Manager";//ResString.GetProductName();
            StartWithInitComponent();
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized; 
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
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(NamePath);
                XmlElement node = xmldoc.DocumentElement;

                XmlNodeList list = node.GetElementsByTagName("Name");
                if (list != null || list.Count >= 1)
                {
                    string txt = list[0].InnerXml;
                    //this.Text = txt + "  " +this.Text;
                    this.Text = txt + "  " + ResString.GetProductName();
                    m_sFormTile = this.Text;
                }
            }

            ///////////////////////////////////////////
            //必须先初始化BYHXSoftLock.m_DongleKeyAlarm
            BYHXSoftLock.m_DongleKeyAlarm = new DongleKeyAlarm();
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogExpired += new EventHandler(m_DongleKeyAlarm_EncryptDogExpired);
            BYHXSoftLock.m_DongleKeyAlarm.EncryptDogLast100H += new EventHandler(m_DongleKeyAlarm_EncryptDogLast100H);
            BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKeyFinished +=
                new EventHandler(m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished);
            ///
            //PubFunc.IconReload(m_ToolbarImageList);
        }

        #endregion

        #region 接口实现

        # region IPrinterChange 接口成员实现

        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            m_LastPrinterStatus = status;
            UpdateButtonStates(status);
            SetPrinterStatusChanged(status);
            if (status == JetStatusEnum.Error)
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
            else OnErrorCodeChanged(0);
            m_PreviewAndInfo.OnPrinterStatusChanged(status);
            m_JobListForm.OnPrinterStatusChanged(status);
            if (status == JetStatusEnum.PowerOff) m_bFirstReady = false;
            if (m_bFirstReady == false && status == JetStatusEnum.Ready)
            {
                m_bFirstReady = true;
                //Setting_LoadChangedSettings();
                string vol = ResString.GetResString("FW_Voltage");
                byte[] buffer = System.Text.Encoding.Unicode.GetBytes(vol);
                int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
                CoreInterface.SetFWVoltage(buffer, buffer.Length, lcid);
#if !LIYUUSB
    ///Check version
                SBoardInfo sBoardInfo = new SBoardInfo();
                if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
                {
                    const int MINI_MAINBOARD_VERSION = 0x00020200;
                    SFWVersion fwv = new SFWVersion(sBoardInfo.m_nBoradVersion);
                    SFWVersion min_fwv = new SFWVersion(MINI_MAINBOARD_VERSION);
                    if ((((fwv.m_nMainVersion << 8) + fwv.m_nSubVersion) << 8) < MINI_MAINBOARD_VERSION)
                    {
                        string info = "";
						string mPrintingFormat = SErrorCode.GetEnumDisplayName(typeof(Software),Software.VersionNoMatch);
                        string curVersion = fwv.m_nMainVersion + "." + fwv.m_nSubVersion;
                        string minVersion = min_fwv.m_nMainVersion + "." + min_fwv.m_nSubVersion;
                        info += "\n" + string.Format(mPrintingFormat, curVersion, minVersion);
                        MessageBox.Show(this, info, "", MessageBoxButtons.OK);
                        //m_bExitAtStart = true;
                    }
                }
#else
                CoreInterface.VerifyHeadType();
                //CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
#endif
                BYHXSoftLock.m_DongleKeyAlarm.FirstReadyShakeHand();
                mVersionText = (new AboutForm()).CacheVersionText();
            }
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_allParam.PrinterProperty = sp;
            //m_ToolbarSetting.OnPrinterPropertyChange(sp);
            m_PreviewAndInfo.OnPrinterPropertyChange(sp);
            m_JobListForm.OnPrinterPropertyChange(sp);
            OnPrinterPropertyChange_Toolbar(sp);
            bool bfac = PubFunc.IsFactoryUser();
            if (bfac) this.toolStripMenuItemFactryDebug.Visible = m_MenuItemDebug.Visible = true;
            else this.toolStripMenuItemFactryDebug.Visible = m_MenuItemDebug.Visible = false;
            bool bNoRealPage = ((sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_80W)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_XJ128_40W)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_126)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_500)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_35W)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_Electron_70W)
                                //				|| (sp.ePrinterHead == PrinterHeadEnum.Xaar_Proton382_35pl)
                                || (sp.ePrinterHead == PrinterHeadEnum.Xaar_1001_GS6));
            if (bNoRealPage)
            {
                //this.m_MenuItemRealTime.Visible = false;
                //this.tabStripButtonTemperature.Visible = false;
            }
            else
            {
                //this.m_MenuItemRealTime.Visible = true;
                //this.tabStripButtonTemperature.Visible = true;
            }

            if (!sp.bSupportUV)
            {
                this.m_MenuItemUVSetting.Visible = false;
            }
            else this.m_MenuItemUVSetting.Visible = true;

            JetStatusEnum status = CoreInterface.GetBoardStatus();
            if (status != JetStatusEnum.PowerOff)
            {
                byte headNum = sp.nHeadNum;
                if (SPrinterProperty.IsKonica512(sp.ePrinterHead)) headNum /= 2;
                else if (SPrinterProperty.IsPolaris(sp.ePrinterHead))
                {
                    headNum /= 4;
                }
#if !LIYUUSB
                this.Text = m_sFormTile
                    + " " + sp.ePrinterHead.ToString()
                    + " " + headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head)
                    + " " + sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color);
#endif
            }
            while (this.statusStrip1.Items.Count > m_nCurNum && m_nCurNum > 0) //清除之前添加的spotcolor项
            {
                this.statusStrip1.Items.RemoveAt(this.statusStrip1.Items.Count - 1);
            }
            m_nCurNum = this.statusStrip1.Items.Count;
            for (int i = 0; i < sp.GetSpotColorNum(); i++)
            {
                ToolStripStatusLabel tsl = new ToolStripStatusLabel(
                    (i + 1).ToString(), BYHXPrinterManager.Properties.Resources.SpotColor);
                tsl.AutoSize = false;
                tsl.Size = this.LabelPUMP_BLACK.Size;
                tsl.BackgroundImageLayout = ImageLayout.Stretch;
                tsl.BorderSides = ToolStripStatusLabelBorderSides.All;
                tsl.BorderStyle = Border3DStyle.SunkenInner;
                tsl.Alignment = ToolStripItemAlignment.Right;
                tsl.TextImageRelation = TextImageRelation.Overlay;
                tsl.TextAlign = ContentAlignment.BottomRight;
                this.statusStrip1.Items.Add(tsl);
            }
            Setting_OnPrinterPropertyChange(sp, status);
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_allParam.PrinterSetting = ss;
            //m_ToolbarSetting.OnPrinterSettingChange(ss);
            m_PreviewAndInfo.OnPrinterSettingChange(ss);
            m_JobListForm.OnPrinterSettingChange(ss);

            Setting_OnPrinterSettingChange(ss, CoreInterface.GetBoardStatus());
        }

        public void OnPreferenceChange(UIPreference up)
        {
            m_allParam.Preference = up;
            //m_ToolbarSetting.OnPreferenceChange(up);
            m_PreviewAndInfo.OnPreferenceChange(up);
            m_JobListForm.OnPreferenceChange(up);
            UpdateViewMode(m_allParam.Preference.ViewModeIndex);

            Setting_OnPreferenceChange(up);
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
            m_PreviewAndInfo.OnGetPrinterSetting(ref m_allParam.PrinterSetting);
            CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
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

        public CLockQueue GetLockQueue()
        {
            return null;
        }

        public void OnSwitchPreview()
        {
            m_JobListForm.OnSwitchPreview();
        }

        public void NotifyUICalibrationExit(bool bSave)
        {
            toolStripMenuItemCalibration.Enabled = m_MenuItemCalibraion.Enabled = true;
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            if (bSave)
            {
                OnPrinterSettingChange(m_allParam.PrinterSetting);
                m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting);
                //Setting_LoadChangedSettings();
            }
            m_wizard = null;
            OnPrinterStatusChanged(printerStatus);
        }

        #endregion

        #region IMessageFilter 接口成员实现

        //public bool PreFilterMessage(ref   Message m)
        //{
        //    //if (m.Msg == 0x020A)    //MouseWheel
        //    //    return true;
        //    //   TODO:     添加   comboNoWheel.PreFilterMessage   实现   
        //    return false;

        //}

        #endregion

        #endregion

        #region 方法

        #region private

        private void StartWithInitComponent()
        {
            m_JobListForm.SetPreviewInfo(m_PreviewAndInfo);
            m_allParam = new AllParam();

            m_JobListForm.SetPrinterChange(this);
            //m_ToolbarSetting.SetPrinterChange(this);
            m_PreviewAndInfo.SetPrinterChange(this);

        }

        private void UpdateButtonStates(JetStatusEnum status)
        {
            JetStatusEnum realstatus = status;
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG) status = JetStatusEnum.PowerOff;
            if (status == JetStatusEnum.Error && SErrorCode.IsWarningError(CoreInterface.GetBoardError()))
            {
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

            m_LastOperate = printerOperate;
            m_MenuItemRealTime.Enabled = printerOperate.CanSaveLoadSettings;
            m_MenuItemUVSetting.Enabled = printerOperate.CanSaveLoadSettings;

            m_MenuItemTools.Enabled = printerOperate.CanUpdate;
            this.toolStripMenuItemSaveToPrinter.Enabled =
                m_MenuItemSaveToPrinter.Enabled = printerOperate.CanSaveLoadSettings;
            this.toolStripMenuItemLoadFromPrinter.Enabled =
                m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;
            this.toolStripMenuItemCalibration.Enabled = printerOperate.CanUpdate;
            this.toolStripMenuItemPassWord.Enabled =
                this.toolStripMenuItemUpdate.Enabled = realstatus != JetStatusEnum.PowerOff;

            m_ToolBarButtonPrint.Enabled =
                m_MenuItemPrint.Enabled = printerOperate.CanPrint && this.m_JobListForm.SelectedCount > 0;
            m_ToolBarButtonSingleClean.Enabled = m_ToolBarButtonSingleClean.Checked || printerOperate.CanClean;
            m_ToolBarButtonSpray.Enabled = m_ToolBarButtonSpray.Checked || printerOperate.CanClean;
            m_ToolBarButtonAutoClean.Enabled = m_ToolBarButtonAutoClean.Checked || printerOperate.CanClean;

            m_ToolBarButtonLeft.Enabled = printerOperate.CanMoveLeft || m_ToolBarButtonLeft.Checked;
            m_ToolBarButtonRight.Enabled = printerOperate.CanMoveRight || m_ToolBarButtonRight.Checked;
            m_ToolBarButtonForward.Enabled = printerOperate.CanMoveForward || m_ToolBarButtonForward.Checked;
            m_ToolBarButtonBackward.Enabled = printerOperate.CanMoveBackward || m_ToolBarButtonBackward.Checked;
            m_ToolBarButtonDownZ.Enabled = printerOperate.CanMoveForward || m_ToolBarButtonDownZ.Checked;
            m_ToolBarButtonUpZ.Enabled = printerOperate.CanMoveBackward || m_ToolBarButtonUpZ.Checked;
            m_ToolBarButtonGoHomeZ.Enabled = printerOperate.CanMoveBackward && printerOperate.CanMoveForward;
            m_ToolBarButtonGoHomeY.Enabled = printerOperate.CanMoveForward && printerOperate.CanMoveBackward;
            //m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveOriginal;
            m_ToolBarButtonGoHome.Enabled = printerOperate.CanMoveLeft && printerOperate.CanMoveRight;

            bool enabled = (status == JetStatusEnum.Ready || status == JetStatusEnum.Spraying);
            m_ToolBarButtonCheckNozzle.Enabled = printerOperate.CanPrint;
            m_ToolBarButtonMeasurePaper.Enabled = (printerOperate.CanPrint
                                                   && m_allParam.PrinterProperty.bSupportPaperSensor);
            m_ToolBarButtonSetOrigin.Enabled = printerOperate.CanPrint;
            m_ToolBarButtonSetOriginY.Enabled = printerOperate.CanPrint;
            this.m_MenuItemSaveToPrinter.Enabled =
                m_MenuItemLoadFromPrinter.Enabled = printerOperate.CanSaveLoadSettings;

            //this.tabStripButtonTemperature.Enabled = printerOperate.CanUpdate;
            this.tabStripButtonService.Enabled = printerOperate.CanUpdate;
            //this.tabStripButtonFactryWrite.Enabled = printerOperate.CanUpdate;
            this.toolStripMenuItemFacWrite.Enabled = printerOperate.CanUpdate;

            m_ToolBarButtonAbort.Enabled = printerOperate.CanAbort;
            m_ToolBarButtonPauseResume.Enabled = (printerOperate.CanPause || printerOperate.CanResume);
            if (printerOperate.CanPause) m_ToolBarButtonPauseResume.Image = BYHXPrinterManager.Properties.Resources.Pause;
            if (printerOperate.CanResume) m_ToolBarButtonPauseResume.Image = BYHXPrinterManager.Properties.Resources.Resume1;

            //m_ToolBarButtonPause.Enabled = printerOperate.CanPause; //???????
            //m_ToolBarButtonResume.Enabled = printerOperate.CanResume; //??????

            this.m_ToolBarButtonAdd.Enabled = this.m_MenuItemAdd.Enabled = this.prtFileBrowser1.SelectedItems.Count > 0;
            this.m_ToolBarButtonDelete.Enabled = this.m_MenuItemDelete.Enabled = this.m_JobListForm.SelectedCount > 0;

            this.LabelPUMP_LIGHTCYAN.Visible =
                this.LabelPUMP_LIGHTMAGENTA.Visible = m_allParam.PrinterProperty.nColorNum == 6;
        }

        private void SetPrinterStatusChanged(JetStatusEnum status)
        {
            string info = ResString.GetEnumDisplayName(typeof(JetStatusEnum), status);
            this.m_StatusBarPanelJetStaus.Text = info;
            if (CoreInterface.Printer_IsOpen() == 0)
            {
                this.m_StatusBarPanelPercent.Text = "";
                //toolStripProgressBar1.Value = 0;
            }
            if (m_wizard != null)
            {
                m_wizard.SetPrinterStatusChanged(status);
            }

            this.Setting_SetPrinterStatusChanged(status);

            m_PreviewAndInfo.SetPrinterStatusChanged(status);

            if (status == JetStatusEnum.PowerOff
                //&& this.tabStrip1.SelectedTab != this.tabStripButtonPrinter
                //&& this.tabStrip1.SelectedTab != this.tabStripButtonPreference
                //&& this.tabStrip1.SelectedTab != this.tabStripButtonCalibration
                ) this.tabStrip1.SelectedTab = this.tabStripButtonPrinter; //Setting_LoadChangedSettings();
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
            else return m_allParam.PrinterSetting.sMoveSetting.n4MoveSpeed;

        }

        private bool MainForm_KeyDownEvent(Keys keyData)
        {
            bool blnProcess = false;
            if (keyData == Keys.Left)
            {
                if (m_ToolBarButtonLeft.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Left;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonLeft.Checked = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Right)
            {
                if (m_ToolBarButtonRight.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Right;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonRight.Checked = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Up)
            {
                if (m_ToolBarButtonBackward.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Up;
                    if (m_allParam.PrinterProperty.nMediaType == 2) dir = MoveDirectionEnum.Down;

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonBackward.Checked = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }
            else if (keyData == Keys.Down)
            {
                if (m_ToolBarButtonForward.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Down;
                    if (m_allParam.PrinterProperty.nMediaType == 2) dir = MoveDirectionEnum.Up;

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonForward.Checked = true;
                    m_bSendMoveCmd = true;
                    blnProcess = true;
                }
            }

            return blnProcess;
        }

        private bool MainForm_KeyUpEvent(Keys keyData)
        {
            bool blnProcess = false;
            if ((keyData == Keys.Left) || (keyData == Keys.Right) || (keyData == Keys.Up) || (keyData == Keys.Down))
            {
                if (m_bSendMoveCmd)
                {
                    CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
                    m_ToolBarButtonLeft.Checked = false;
                    m_ToolBarButtonRight.Checked = false;
                    m_ToolBarButtonBackward.Checked = false;
                    m_ToolBarButtonForward.Checked = false;
                    m_ToolBarButtonDownZ.Checked = false;
                    m_ToolBarButtonUpZ.Checked = false;
                    m_bSendMoveCmd = false;
                }
                blnProcess = true;
            }
            return blnProcess;
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
            CleanForm form = new CleanForm();
            form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            if (CoreInterface.SendJetCommand((int)JetCmdEnum.EnterSingleCleanMode, 0) == 1) form.ShowDialog(this);
        }

        private void OnPrinterPropertyChange_Toolbar(SPrinterProperty sp)
        {
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
                m_ToolBarButtonSpray.CheckOnClick = true;
                //m_ToolBarButtonSpray.Style = ToolBarButtonStyle.ToggleButton;
                m_ToolBarButtonSpray.Visible = true;
            }
            else
                //if(sp.bSupportAutoClean == false)
            {
                m_ToolBarButtonSpray.CheckOnClick = false; //m_ToolBarButtonSpray.Style = ToolBarButtonStyle.PushButton;
                m_ToolBarButtonSpray.Visible = true;
            }
            //else
            //	m_ToolBarButtonSpray.Visible = false;

            if (sp.bSupportAutoClean)
            {
                m_ToolBarButtonAutoClean.Visible = true;
            }
            else
            {
                m_ToolBarButtonAutoClean.Visible = false;
            }
            if (sp.eSingleClean == SingleCleanEnum.None)
            {
                m_ToolBarButtonSingleClean.Visible = false;
            }
            else if (sp.eSingleClean == SingleCleanEnum.PureManual)
            {
                m_ToolBarButtonSingleClean.CheckOnClick = true;
                //m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.ToggleButton;
                m_ToolBarButtonSingleClean.Visible = true;
            }
            else
            {
                m_ToolBarButtonSingleClean.CheckOnClick = false;
                //m_ToolBarButtonSingleClean.Style = ToolBarButtonStyle.PushButton;
                m_ToolBarButtonSingleClean.Visible = true;
            }

            if (sp.bSupportPaperSensor == true)
            {
                m_ToolBarButtonMeasurePaper.Visible = true;
            }
            else m_ToolBarButtonMeasurePaper.Visible = false;
            if (sp.nMediaType == 0)
            {
                m_ToolBarButtonGoHomeY.Visible = false;
                m_ToolBarButtonSetOriginY.Visible = false;
            }
            else
            {
                m_ToolBarButtonGoHomeY.Visible = true;
                m_ToolBarButtonSetOriginY.Visible = true;
            }
        }

        private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
        {
            CoreMsgEnum kParam = (CoreMsgEnum)wParam.ToInt32();
            switch (kParam)
            {
                case CoreMsgEnum.UpdaterPercentage:
                    {
                        int percentage = lParam.ToInt32();
                        //OnPrintingProgressChanged(percentage);
                        string info = "";
                        string mPrintingFormat = ResString.GetUpdatingProgress();
                        info += string.Format(mPrintingFormat, percentage);
                        //"\n" + string.Format(mPrintingFormat, percentage);
                        this.m_StatusBarPanelPercent.Text = info;
                        //this.toolStripProgressBar1.Value = percentage;
                        break;
                    }

                case CoreMsgEnum.Percentage:
                    {
                        int percentage = lParam.ToInt32();
                        OnPrintingProgressChanged(percentage);
                        Console.WriteLine("Printing: {0}%", percentage);
                        if (percentage > 0)
                        {
                            m_PreviewAndInfo.UpdatePercentage(percentage);
                        }

                        break;
                    }
                case CoreMsgEnum.Job_Begin:
                    {

                        int startType = lParam.ToInt32();

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

                        int endType = lParam.ToInt32();

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
                        int bPowerOn = lParam.ToInt32();
                        if (bPowerOn != 0)
                        {
                            int bPropertyChanged, bSettingChanged;
                            SPrinterProperty sPrinterProperty;
                            SPrinterSetting sPrinterSetting;

                            m_allParam.PowerOnEvent(
                                out bPropertyChanged, out bSettingChanged, out sPrinterProperty, out sPrinterSetting);
                            if (bPropertyChanged != 0)
                            {
                                OnPrinterPropertyChange(sPrinterProperty);
                                this.combinedSettings1.OnPrinterPropertyChange(sPrinterProperty);
                            }
                            if (bSettingChanged != 0)
                            {
                                OnPrinterSettingChange(sPrinterSetting);
                            }
                        }
                        else
                        {
                            if (m_MyMessageBox != null)
                                SystemCall.PostMessage(
                                    m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);

                            this.m_JobListForm.TerminatePrintingJob(false);
                            m_allParam.PowerOffEvent();
                            StopFlashInkLable();
                            mVersionText = string.Empty;
                            //m_bFirstReady = false;
                        }
                        break;
                    }
                case CoreMsgEnum.Status_Change:
                    {
                        int status = lParam.ToInt32();
                        OnPrinterStatusChanged((JetStatusEnum)status);
                        if (m_bExitAtStart)
                        {
                            End();
                            Application.Exit();
                        }
                        break;
                    }
                case CoreMsgEnum.ErrorCode:
                    {
                        int errorCode = lParam.ToInt32();
                        SErrorCode sErrorCode = new SErrorCode(errorCode);
                        if (SErrorCode.IsOnlyPauseError(errorCode))
                        {
                            if (m_MyMessageBox != null)
                            {
                                SystemCall.PostMessage(
                                    m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                            }
                            m_MyMessageBox = new MyMessageBox();

                            string errorInfo = SErrorCode.GetInfoFromErrCode(errorCode);
                            //DialogResult result = m_MyMessageBox.ShowDialog();
                            //DialogResult result = MessageBox.Show(errorInfo,ResString.GetProductName(),MessageBoxButtons.RetryCancel,MessageBoxIcon.Exclamation);
                            if (sErrorCode.nErrorCause == (byte)ErrorCause.COM
                                &&
                                (sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyGoHome
                                 || sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyMeasure))
                            {
                                DialogResult result = m_MyMessageBox.Show(
                                    errorInfo,
                                    ResString.GetProductName(),
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information);
                                CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
                                if (sErrorCode.nErrorCause != (byte)ErrorCause.Software) CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode, errorCode);
#else
							if(sErrorCode.nErrorCause == (byte)ErrorCause.COM ||
								sErrorCode.nErrorCause == (byte)ErrorCause.CoreBoard)
								CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode,sErrorCode.nErrorCode);
#endif
                                if (result != DialogResult.No)
                                {
                                    if (sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyGoHome)
                                    {
                                        CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
                                    }
                                    else if (sErrorCode.nErrorCode == (byte)COMCommand_Abort.NotifyMeasure)
                                    {
                                        CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 1);
                                    }
                                }
                            }
                            else
                            {
                                DialogResult result = DialogResult.Retry;
                                if (sErrorCode.nErrorCause == (byte)ErrorCause.Software
                                    && sErrorCode.nErrorCode == (byte)Software.MediaTooSmall)
                                {
								if(m_JobListForm.IsFristCopiesOrNoJobPrint())
                                    {
                                        result = m_MyMessageBox.Show(
                                            errorInfo,
                                            ResString.GetProductName(),
                                            MessageBoxButtons.RetryCancel,
                                            MessageBoxIcon.Exclamation);
                                        if (result == DialogResult.Cancel)
                                        {
                                            //CoreInterface.Printer_Abort();
                                            if (CoreInterface.Printer_IsOpen() != 0) m_JobListForm.AbortPrintingJob();
                                        }
                                    }
                                }
                                else
                                {
                                    result = m_MyMessageBox.Show(
                                        errorInfo,
                                        ResString.GetProductName(),
                                        MessageBoxButtons.RetryCancel,
                                        MessageBoxIcon.Exclamation);
                                    if (result == DialogResult.Cancel)
                                    {
                                        //CoreInterface.Printer_Abort();
                                        if (CoreInterface.Printer_IsOpen() != 0) m_JobListForm.AbortPrintingJob();
                                    }
                                }
                                if (result != DialogResult.Cancel)
                                {
                                    CoreInterface.ClearErrorCode(errorCode);
#if LIYUUSB
                                    if (sErrorCode.nErrorCause != (byte)ErrorCause.Software) CoreInterface.SendJetCommand((int)JetCmdEnum.ClearErrorCode, errorCode);
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
                        if (cause == ErrorCause.CoreBoard
                            && (ErrorAction)sErrorCode.nErrorAction == ErrorAction.Updating)
                        {
                            if (0 != sErrorCode.nErrorCode)
                            {
                                if (sErrorCode.nErrorCode == 1)
                                {
                                    string info = ResString.GetEnumDisplayName(
                                        typeof(UISuccess), UISuccess.UpdateSuccess);
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
                            //if (m_hasLoad == true)
                            //    this.Setting_OnPrinterSettingChange(sPrinterSetting);
                            //this.Setting_LoadChangedSettings();
                        }
                        //m_LockUpdate = false;
                        break;
                    }
                case CoreMsgEnum.AbortPrintCmd:
                    //MessageBox.Show(this,"Accept! ");
                    m_JobListForm.AbortPrintingJob();
                    break;
                case CoreMsgEnum.BlockNotifyUI:
                    int msg1 = lParam.ToInt32();
                    {
                        if (m_MyMessageBox != null)
                        {
                            SystemCall.PostMessage(
                                m_MyMessageBox.Handle, m_MyMessageBox.m_KernelMessage, (int)CoreMsgEnum.Power_On, 0);
                        }
                        m_MyMessageBox = new MyMessageBox();
                        {
                            string m1 = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.GoHome);
                            DialogResult result = m_MyMessageBox.Show(
                                m1, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result != DialogResult.No)
                            {
                                CoreInterface.SendJetCommand((int)JetCmdEnum.BackToHomePoint, (int)AxisDir.X);
                            }
                        }
                        m_MyMessageBox = null;
                    }
                    break;
                case CoreMsgEnum.PumpInk:
                    {
                        //EP1BUF[4]为泵墨状态, 1表示泵墨, 0 表示不泵墨. Bit0~Bit7分别表示K, C, M, Y, Lm, Lc, Nop, Nop
                        int pumpFlag = lParam.ToInt32();
                        if (pumpFlag != 0)
                        {
                            FilterPumpInkMsg(ref pumpFlag);
                            StartFlashInkLable(pumpFlag, false);
                        }
                        else
                        {
                            StopFlashInkLable();
                        }
                    }
                    break;
            }
        }

        private void UpdateViewMode(int mode)
        {
            UIViewMode uimode = (UIViewMode)mode;
            switch (uimode)
            {
                case UIViewMode.LeftRight:
                    this.splitContainer1.Orientation = Orientation.Vertical;
                    this.tabStrip2.Dock = DockStyle.Left;
                    this.tabStrip2.FlipButtons = false;
                    this.m_JobListForm.mAlignment = ListViewAlignment.Top;
                    //this.m_PreviewAndInfo.SpliterOrientathion = false;
                    this.splitContainer1.SplitterDistance = this.splitContainer1.Width * 2 / 3;

                    m_MenuItemLeftRight.Checked = true;
                    m_MenuItemTopDown.Checked = !m_MenuItemLeftRight.Checked;
                    break;
                case UIViewMode.TopDown:
                case UIViewMode.NotifyIcon:
                default:
                    this.splitContainer1.Orientation = Orientation.Horizontal;
                    this.tabStrip2.Dock = DockStyle.Right;
                    this.tabStrip2.FlipButtons = true;
                    this.m_JobListForm.mAlignment = ListViewAlignment.Left;
                    //this.m_PreviewAndInfo.SpliterOrientathion = true;
                    this.splitContainer1.SplitterDistance = this.splitContainer1.Height * 2 / 3;

                    m_MenuItemTopDown.Checked = true;
                    m_MenuItemLeftRight.Checked = !m_MenuItemTopDown.Checked;
                    break;
            }
        }

        private void AddSelectedToList()
        {
            string[] m_FileNames = new string[this.prtFileBrowser1.SelectedItems.Count];
            for (int i = 0; i < this.prtFileBrowser1.SelectedItems.Count; i++)
            {
                //UIJob sit = (UIJob)this.prtFileBrowser1.SelectedItems[i].Tag;
                m_FileNames[i] = this.prtFileBrowser1.SelectedItems[i].Tag.ToString();
            }
            this.m_JobListForm.AddJobs(m_FileNames);
        }

        #endregion

        #region public

        public bool Start()
        {
            if (!BYHXSoftLock.m_DongleKeyAlarm.Start(this.Handle,false)) return m_bExitAtStart;

            SystemCall.PreventSystemPowerdown();
            m_allParam = new AllParam();
            SystemInit init = new SystemInit(this, this.Handle, m_KernelMessage);
            init.SystemStart();

            m_PortManager = new PortManager(this);
            m_PortManager.OpenPort();
            m_PortManager.TaskStart();
            return !m_bExitAtStart;
        }

        public bool End()
        {
            this.m_JobListForm.TerminatePrintingJob(true);
            SystemInit init = new SystemInit(this, this.Handle, m_KernelMessage);
            init.SystemEnd();
            if (m_PortManager != null) m_PortManager.ClosePort();
            SystemCall.AllowSystemPowerdown();
            return true;
        }

        public void OnErrorCodeChanged(int code)
        {
            FilterPumpInkMsg(ref code);
            //this.FalshInkLable(code);
            this.m_StatusBarPanelError.Text = SErrorCode.GetInfoFromErrCode(code);
        }

        public void OnEditPrinterSetting()
        {
            this.tabControl1.SelectedTab = this.tabPageSetting;
        }

        public void OnPrintingProgressChanged(int percent)
        {
            string info = "";
            string mPrintingFormat = ResString.GetPrintingProgress();
            info += string.Format(mPrintingFormat, percent); //"\n" + string.Format(mPrintingFormat, percent);
            this.m_StatusBarPanelPercent.Text = info;
            //this.toolStripProgressBar1.Value = percent;
        }

        public void OnPrintingStart()
        {
            m_StartTime = DateTime.Now;
            m_JobListForm.OnPrintingStart();
        }

        public void OnPrintingEnd()
        {
            m_JobListForm.OnPrintingEnd();
            m_PreviewAndInfo.UpdatePercentage(0);
        }

        #endregion

        #region protected

        #endregion

        #endregion

        #region override

        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.WParam.ToInt32() == 0xF060) //   关闭消息   
                {
                    if (m_setted && (this.combinedSettings1.IsDirty || this.combinedSettings1.IsT_VDirty)) this.Setting_ApplyChangedSettings();
                    string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Exit);
                    if (MessageBox.Show(
                        info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No)
                    {
                        return;
                    }
                }

                if (MyWndProc.NCButtonClick(m, this, true)) base.WndProc(ref m);

                if (m.Msg == 0x0219) //WM_DEVICECHANGE
                    BYHXSoftLock.OnDeviceChange(m.WParam, m.LParam);

                if (m.Msg == this.m_KernelMessage)
                {
                    ProceedKernelMessage(m.WParam, m.LParam);
                }
                else MyWndProc.PaintFormCaption(ref m, this, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //Emulate a slow loader
            //System.Threading.Thread.Sleep(2000);
            if (!PubFunc.IsFactoryUser()) Splasher.Fadeout();
        }

        #endregion

        #region 事件

        #region MainForm 事件

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            End();
        }

        private void menuItemDongleKey_Click(object sender, System.EventArgs e)
        {
            DongleKeyForm dkf = new DongleKeyForm();
            dkf.ShowDialog();
            if (!BYHXSoftLock.m_DongleKeyAlarm.LoadAndCheckDongleKey()) this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
#if ADD_HARDKEY
            this.menuItemDongle.Visible = true;
#else
            this.menuItemDongle.Visible = false;
#endif
            try
            {
                tabStripButtonWorkForder.Image = this.TabimageList.Images[0];
                tabStripButtonPreview.Image = this.TabimageList.Images[1];
                tabStripButtonSetting.Image = this.TabimageList.Images[2];
                this.tabStrip2.SelectedTab = this.tabStripButtonPreview;
                // 设置初始选中tab页

                this.tabStrip1.SelectedTab = this.tabStripButtonPrinter;
                this.Setting_LoadChangedSettings();
                //this.prtFileBrowser1.ShowNavigationBar = true;
                //this.prtFileBrowser1.ShowFoldersButton = true;
                //this.prtFileBrowser1.SplitterDistance = 200;

                //this.splitContainer1.SplitterDistance = 290;
                this.LabelPUMP_BLACK.Alignment = ToolStripItemAlignment.Right;
                this.LabelPUMP_CYAN.Alignment = ToolStripItemAlignment.Right;
                this.LabelPUMP_LIGHTCYAN.Alignment = ToolStripItemAlignment.Right;
                this.LabelPUMP_LIGHTMAGENTA.Alignment = ToolStripItemAlignment.Right;
                this.LabelPUMP_MAGENTA.Alignment = ToolStripItemAlignment.Right;
                this.LabelPUMP_YELLOW.Alignment = ToolStripItemAlignment.Right;

                //this.m_CalibrationSetting.SetButtonVisible(true);
                LogWriter.WriteLog(
                    new string[] { "BYHXPrinterManager Show at:" + Environment.TickCount.ToString() }, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
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
                        if (m_JobListForm.Confirm_Exit()) m_JobListForm.AbortPrintingJob();
                    }
                    else if (e.KeyCode == Keys.A)
                    {
                        //m_JobListForm.OpenJob();
                        //this.AddSelectedToList();
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
                //if (e.Modifiers == Keys.Alt)
                //{
                //    this.m_MainMenu.Visible = !this.m_MainMenu.Visible;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                MainForm_KeyUpEvent(e.KeyData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            MyWndProc.DrawRoundForm(this);
            if (this.WindowState != FormWindowState.Minimized) this.splitContainer1.SplitterDistance = this.splitContainer1.Height * 2 / 3;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                if (m_bSendMoveCmd)
                {
                    CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
                    m_ToolBarButtonLeft.Checked = false;
                    m_ToolBarButtonRight.Checked = false;
                    m_ToolBarButtonForward.Checked = false;
                    m_ToolBarButtonBackward.Checked = false;
                    m_ToolBarButtonDownZ.Checked = false;
                    m_ToolBarButtonUpZ.Checked = false;

                    m_bSendMoveCmd = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region MainMenu 事件

        private void m_MenuItemAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                //m_JobListForm.OpenJob();
                this.AddSelectedToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                m_JobListForm.DeleteJob();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                m_JobListForm.PrintJob();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (m_setted && (this.combinedSettings1.IsDirty || this.combinedSettings1.IsT_VDirty)) this.Setting_ApplyChangedSettings();
                string info = ResString.GetEnumDisplayName(typeof(Confirm), Confirm.Exit);
                if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                {
                    return;
                }
                //End();
                this.Close(); //Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemSave_Click(object sender, System.EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemLoad_Click(object sender, System.EventArgs e)
        {
            try
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
                    //this.Setting_LoadChangedSettings();
                    this.Setting_OnPrinterSettingChange(m_allParam.PrinterSetting, CoreInterface.GetBoardStatus());
                    if (CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting) == 0)
                    {
                        Debug.Assert(false);
                    }
                    string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.RstoreSetting);
                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemSaveToPrinter_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show(
                    this,
                    ResString.GetEnumDisplayName(typeof(Confirm), Confirm.SaveToBoard),
                    ResString.GetProductName(),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int iRet;
                    //iRet = CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                    this.Cursor = Cursors.WaitCursor;
                    iRet = CoreInterface.SendJetCommand((int)JetCmdEnum.SaveSetting, 0);
                    this.Cursor = Cursors.Default;
                    if (iRet != 0)
                    {
                        MessageBox.Show(
                            ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SaveSetToBoardSuccess),
                            this.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveSetToBoardFail),
                            this.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemLoadFromPrinter_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (MessageBox.Show(
                    this,
                    ResString.GetEnumDisplayName(typeof(Confirm), Confirm.LoadFromBoard),
                    ResString.GetProductName(),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int iRet = 0;
                    this.Cursor = Cursors.WaitCursor;
                    iRet = CoreInterface.SendJetCommand((int)JetCmdEnum.LoadSetting, 0);
                    if (iRet != 0) iRet = CoreInterface.GetPrinterSetting(ref m_allParam.PrinterSetting);
                    this.Cursor = Cursors.Default;
                    if (iRet != 0)
                    {
                        this.OnPrinterSettingChange(m_allParam.PrinterSetting);
                        //this.Setting_LoadChangedSettings();
                        this.Setting_OnPrinterSettingChange(m_allParam.PrinterSetting, CoreInterface.GetBoardStatus());
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
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemUpdate_Click(object sender, System.EventArgs e)
        {
            try
            {
                JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
                if (printerStatus == JetStatusEnum.Busy)
                {
                    if (MessageBox.Show(
                        this,
                        ResString.GetResString("Confirm_Update"),
                        "update",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
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
                fileDialog.DefaultExt = ".prg";
                fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Prg);
                fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;
                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    m_allParam.Preference.WorkingFolder = Path.GetDirectoryName(fileDialog.FileName);
                    UpdateCoreBoard(fileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemPassword_Click(object sender, System.EventArgs e)
        {
            PasswordForm pwdform = new PasswordForm();
            pwdform.ShowDialog(this);
        }

        private void m_MenuItemDemoPage_Click(object sender, System.EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CoreInterface.SendCalibrateCmd(CalibrationCmdEnum.BiDirectionCmd, 0, ref m_allParam.PrinterSetting);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemCalibraion_Click(object sender, System.EventArgs e)
        {
            try
            {
                m_wizard = new CaliWizard();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void m_MenuItemAbout_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(mVersionText)) 
                    mVersionText = (new AboutForm()).CacheVersionText();
                AboutForm aboutForm = new AboutForm(this.mVersionText);
                aboutForm.Text += this.UpdateFormHeaderText();
                aboutForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemFactoryDebug_Click(object sender, System.EventArgs e)
        {
            try
            {
                FactoryDebug hwForm = new FactoryDebug();
                hwForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemRealTime_Click(object sender, System.EventArgs e)
        {
            try
            {
                KonicTemperature form = new KonicTemperature();
                form.OnPreferenceChange(m_allParam.Preference);
                form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                form.OnPrinterSettingChange(m_allParam.PrinterSetting);
                form.OnRealTimeChange();
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    bool bChangeProperty = false;
                    form.OnGetPrinterSetting(ref m_allParam, ref bChangeProperty);
                    CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                    form.ApplyToBoard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_MenuItemUVSetting_Click(object sender, System.EventArgs e)
        {
            try
            {
                UVForm hwForm = new UVForm();
                hwForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog(this);
        }

        private void toolStripMenuItemFacWrite_Click(object sender, EventArgs e)
        {
            FWValidationForm fwvf = new FWValidationForm();
            if (fwvf.ShowDialog() != DialogResult.OK) return;
            PrinterHWSettingForm form = new PrinterHWSettingForm();
            form.OnPreferenceChange(m_allParam.Preference);
            form.OnPrinterPropertyChange(m_allParam.PrinterProperty);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                bool bChangeProperty = false;
                form.OnGetProperty(ref m_allParam.PrinterProperty, ref bChangeProperty);
                m_allParam.PrinterSetting.sFrequencySetting.nResolutionX = m_allParam.PrinterProperty.nResX;
                CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                OnPreferenceChange(m_allParam.Preference);
                if (bChangeProperty) CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
                //m_ToolbarSetting.OnPrinterSettingChange(m_allParam.PrinterSetting);
            }
        }

        #endregion

        #region Tool Strip 事件

        private void m_ToolBarCommand_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem == m_ToolBarButtonPauseResume)
                {
                    CoreInterface.Printer_PauseOrResume();
                    m_bPaused = !m_bPaused;
                    if (m_bPaused) m_ToolBarButtonPauseResume.Image = BYHXPrinterManager.Properties.Resources.Resume1;
                    else m_ToolBarButtonPauseResume.Image = BYHXPrinterManager.Properties.Resources.Pause;

                }
                else if (e.ClickedItem == m_ToolBarButtonAbort)
                {
                    //CoreInterface.Printer_Abort();
                    if (m_JobListForm.Confirm_Exit()) m_JobListForm.AbortPrintingJob();
                }
                else if (e.ClickedItem == m_ToolBarButtonAdd)
                {
                    //m_JobListForm.OpenJob();
                    this.AddSelectedToList();
                }
                else if (e.ClickedItem == m_ToolBarButtonDelete)
                {
                    m_JobListForm.DeleteJob();
                }
                    //else if (e.ClickedItem == m_ToolBarButtonEdit)
                    //{
                    //    OnEditPrinterSetting();
                    //    //m_JobListForm.EditJob();
                    //}
                else if (e.ClickedItem == m_ToolBarButtonPrint)
                {
                    if (m_setted && (this.combinedSettings1.IsDirty || this.combinedSettings1.IsT_VDirty)) this.Setting_ApplyChangedSettings();
                    if (this.tabStrip2.SelectedTab != this.tabStripButtonPreview) this.tabStrip2.SelectedTab = this.tabStripButtonPreview;
                    m_JobListForm.PrintJob();
                }
                else
                {
                    JetCmdEnum cmd;
                    int cmdvalue = 0;
                    if (e.ClickedItem == m_ToolBarButtonCheckNozzle)
                    {
                        CoreInterface.SendCalibrateCmd(
                            CalibrationCmdEnum.CheckNozzleCmd, 0, ref this.m_allParam.PrinterSetting);
                        return;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonAutoClean) cmd = JetCmdEnum.AutoSuckHead;
                    else if (e.ClickedItem == m_ToolBarButtonSpray)
                    {
                        if ((e.ClickedItem as ToolStripButton).Checked == true) cmd = JetCmdEnum.StartSpray;
                        else cmd = JetCmdEnum.StopSpray;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonSingleClean)
                    {
                        if (m_allParam.PrinterProperty.eSingleClean == SingleCleanEnum.PureManual)
                        {
                            if ((e.ClickedItem as ToolStripButton).Checked == true) cmd = JetCmdEnum.EnterSingleCleanMode;
                            else cmd = JetCmdEnum.ExitSingleCleanMode;
                        }
                        else
                        {
                            OnSingleClean();
                            return;
                        }
                    }
                    else if (e.ClickedItem == m_ToolBarButtonGoHome) cmd = JetCmdEnum.BackToHomePoint;
                    else if (e.ClickedItem == m_ToolBarButtonGoHomeY)
                    {
                        cmd = JetCmdEnum.BackToHomePointY;
                        cmdvalue = (int)AxisDir.Y;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonGoHomeZ)
                    {
                        cmd = JetCmdEnum.BackToHomePointY;
                        cmdvalue = (int)AxisDir.Z;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonSetOrigin)
                    {
                        cmd = JetCmdEnum.SetOrigin;
                        cmdvalue = (int)AxisDir.X;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonSetOriginY)
                    {
                        cmd = JetCmdEnum.SetOrigin;
                        cmdvalue = (int)AxisDir.Y;
                    }
                    else if (e.ClickedItem == m_ToolBarButtonMeasurePaper) cmd = JetCmdEnum.MeasurePaper;
                    else if (e.ClickedItem == m_ToolBarButtonStop)
                    {
                        //if(CoreInterface.GetBoardStatus() == JetStatusEnum.Moving)
                        cmd = JetCmdEnum.StopMove;
                    }
                    else return;
                    CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonLeft_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonLeft.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Left;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonLeft.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonRight_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonRight.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Right;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonRight.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonForward_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonForward.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Down;
                    if (m_allParam.PrinterProperty.nMediaType == 2) dir = MoveDirectionEnum.Up;

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonForward.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonBackward_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonBackward.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Up;
                    if (m_allParam.PrinterProperty.nMediaType == 2) dir = MoveDirectionEnum.Down;

                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);

                    m_ToolBarButtonBackward.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonDownZ_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonDownZ.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Down_Z;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonDownZ.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarButtonUpZ_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_ToolBarButtonDownZ.Enabled && this.m_LastPrinterStatus != JetStatusEnum.Moving
                    && m_bSendMoveCmd == false)
                {
                    MoveDirectionEnum dir = MoveDirectionEnum.Up_Z;
                    int speed = GetSpeedWithDir(dir);
                    CoreInterface.MoveCmd((int)dir, 0, speed);
                    m_ToolBarButtonUpZ.Checked = true;
                    m_bSendMoveCmd = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_ToolBarCommand_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (m_bSendMoveCmd)
                {
                    CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
                    m_ToolBarButtonLeft.Checked = false;
                    m_ToolBarButtonRight.Checked = false;
                    m_ToolBarButtonForward.Checked = false;
                    m_ToolBarButtonBackward.Checked = false;
                    m_ToolBarButtonDownZ.Checked = false;
                    m_ToolBarButtonUpZ.Checked = false;

                    m_bSendMoveCmd = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 主工作区内控件事件

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (e.TabPage == this.tabPageSetting)
                {
                    //this.m_ToolBarCommand.Enabled = false;
                    this.tabStrip1.SelectedTab = this.tabStripButtonPrinter;
                    this.splitContainer1.Panel2Collapsed = true;
                    m_setted = true;
                }
                else
                {
                    //this.m_ToolBarCommand.Enabled = true;
                    this.splitContainer1.Panel2Collapsed = false;

                    if (m_setted == true && (this.combinedSettings1.IsDirty || this.combinedSettings1.IsT_VDirty))
                    {
                        this.Setting_ApplyChangedSettings();
                        m_setted = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                e.IsInputKey = true;
                return;
            }
        }

        private void tabStrip2_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                if (e.SelectedTab == this.tabStripButtonWorkForder)
                {
                    this.tabControl1.SelectedTab = this.tabPageWorkForder;
                }
                else if (e.SelectedTab == this.tabStripButtonPreview)
                {
                    this.tabControl1.SelectedTab = this.tabPagePreview;
                }
                else if (e.SelectedTab == this.tabStripButtonSetting)
                {
                    this.tabControl1.SelectedTab = this.tabPageSetting;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabStrip1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                if (e.SelectedTab == this.tabStripButtonPrinter)
                {
                    this.m_TabControlSetting.SelectedTab = m_TabPagePrinterSetting;
                }
                    //else if (e.SelectedTab == this.tabStripButtonPreference)
                    //{
                    //    this.m_TabControlSetting.SelectedTab = this.m_TabPagePreference;
                    //}
                    //else if (e.SelectedTab == this.tabStripButtonCalibration)
                    //{
                    //    this.m_TabControlSetting.SelectedTab = this.m_TabPageCaliSetting;
                    //}
                    //else if (e.SelectedTab == this.tabStripButtonTemperature)
                    //{
                    //    // refresh
                    //    this.m_KonicTemperatureSetting1.OnRealTimeChange();
                    //    this.m_TabControlSetting.SelectedTab = this.m_TabPageTemperature;
                    //}
                    //else if (e.SelectedTab == this.tabStripButtonFactryWrite)
                    //{
                    //    this.printerHWSetting1.OnPrinterPropertyChange(m_allParam.PrinterProperty);
                    //    this.m_TabControlSetting.SelectedTab = this.m_TabPageFactryWrite;

                    //}
                else if (e.SelectedTab == this.tabStripButtonService)
                {
                    this.m_TabControlSetting.SelectedTab = this.m_TabPageService;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void prtFileBrowser1_UserContextMenuClick(object sender, EventArgs e)
        {
            try
            {
                int icase = (int)sender;
                switch (icase)
                {
                    case 0:
                        this.AddSelectedToList();
                        this.m_JobListForm.ScrollSeletedControlIntoView();
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void prtFileBrowser1_PathChanged(object sender, EventArgs e)
        {
            this.m_JobListForm.m_PreviewForlder = this.prtFileBrowser1.PreviewFolder;
        }

        private void prtFileBrowser1_SelectedFilesChanged(object sender, EventArgs e)
        {
            this.UpdateButtonStates(this.m_LastPrinterStatus);
        }

        private void m_JobListForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateButtonStates(this.m_LastPrinterStatus);
        }

        private void buttonBaseSettingApply_Click(object sender, EventArgs e)
        {
            if (this.combinedSettings1.IsDirty)
            {
                this.Setting_OnGetPrinterSetting(ref m_allParam);

                CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting);

                OnPreferenceChange(m_allParam.Preference);

                CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
                this.combinedSettings1.IsDirty = false;
            }

            if (this.combinedSettings1.IsT_VDirty)
            {
                JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
                if (printerStatus != JetStatusEnum.PowerOff) combinedSettings1.ApplyToBoard();
                this.combinedSettings1.IsT_VDirty = false;
            }
        }

        private void bSeviceSettingApply_Click(object sender, EventArgs e)
        {
            if (m_SeviceSetting.IsDirty)
            {
                bool bChanged = false;
                m_SeviceSetting.OnGetProperty(ref m_allParam.PrinterProperty, ref bChanged);
                CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
                m_SeviceSetting.IsDirty = false;
            }
        }

        private void printerHWSetting1_OKButtonClicked(object sender, EventArgs e)
        {
            bool bChangeProperty = false;
            //printerHWSetting1.OnGetProperty(ref m_allParam.PrinterProperty, ref bChangeProperty);
            m_allParam.PrinterSetting.sFrequencySetting.nResolutionX = m_allParam.PrinterProperty.nResX;
            CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
            OnPreferenceChange(m_allParam.Preference);
            if (bChangeProperty)
            {
                CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);
                this.OnPrinterSettingChange(m_allParam.PrinterSetting);
            }
        }

        #endregion

        #endregion

        #region Setting Tab 页


        private void Setting_OnPrinterPropertyChange(SPrinterProperty sp, JetStatusEnum printerStatus)
        {
            combinedSettings1.OnPrinterPropertyChange(sp);
            //m_BaseSetting.OnPrinterPropertyChange(sp);
            //m_CalibrationSetting.OnPrinterPropertyChange(sp);

            bool bfac = PubFunc.IsFactoryUser();
            if (bfac) m_SeviceSetting.OnPrinterPropertyChange(sp);
            if (!bfac && m_TabControlSetting.TabPages.Contains(m_TabPageService))
            {
                m_TabControlSetting.TabPages.Remove(m_TabPageService);
                this.tabStrip1.Visible = this.tabStripButtonService.Visible = false;
            }
            else if (bfac && !m_TabControlSetting.TabPages.Contains(m_TabPageService))
            {
                m_TabControlSetting.TabPages.Add(m_TabPageService);
                this.tabStrip1.Visible = this.tabStripButtonService.Visible = true;
            }
            this.tabStrip1.SelectedTab = this.tabStripButtonPrinter; //m_TabControlSetting.SelectedIndex = 0;
            ////if (printerStatus == JetStatusEnum.Ready)
            //    m_KonicTemperatureSetting1.OnPrinterPropertyChange(sp);

            //if (printerStatus != JetStatusEnum.PowerOff)
            //    this.printerHWSetting1.OnPrinterPropertyChange(m_allParam.PrinterProperty);
        }

        private void Setting_OnPrinterSettingChange(SPrinterSetting ss, JetStatusEnum printerStatus)
        {
            combinedSettings1.OnPrinterSettingChange(ss);
            //m_BaseSetting.OnPrinterSettingChange(ss);
            //m_CalibrationSetting.OnPrinterSettingChange(ss);
            if (PubFunc.IsFactoryUser()) m_SeviceSetting.OnPrinterSettingChange(ss, this.m_allParam.EpsonAllParam.sCaliConfig);
            ////m_RealTimeSetting.OnPrinterSettingChange(ss);
            ////if (printerStatus == JetStatusEnum.Ready)
            //    m_KonicTemperatureSetting1.OnPrinterSettingChange(ss);
        }

        private void Setting_OnPreferenceChange(UIPreference up)
        {
            combinedSettings1.OnPreferenceChange(up);
            //m_BaseSetting.OnPreferenceChange(up);
            //m_CalibrationSetting.OnPreferenceChange(up);
            //m_PreferenceSetting.OnPreferenceChange(up);
            //m_RealTimeSetting.OnPreferenceChange(up);

            //this.printerHWSetting1.OnPreferenceChange(m_allParam.Preference);
        }

        private void Setting_OnGetPrinterSetting(ref AllParam allParam)
        {
            combinedSettings1.OnGetPrinterSetting(ref allParam.PrinterSetting);
            combinedSettings1.OnGetPreference(ref allParam.Preference);
            //m_BaseSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
            //m_CalibrationSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
            //m_KonicTemperatureSetting1.OnGetPrinterSetting(ref allParam.PrinterSetting);
            //m_PreferenceSetting.OnGetPreference(ref allParam.Preference);

            if (PubFunc.IsFactoryUser())
            {
                bool bChanged = false;
                m_SeviceSetting.OnGetProperty(ref allParam.PrinterProperty, ref bChanged);
                //m_RealTimeSetting.OnGetPrinterSetting(ref allParam.PrinterSetting);
            }
        }

        //private void Setting_OnRealTimeChange()
        //{
        //    combinedSettings1.OnRealTimeChange();
        //    //m_KonicTemperatureSetting1.OnRealTimeChange();
        //}
        private void Setting_SetPrinterStatusChanged(JetStatusEnum status)
        {
            combinedSettings1.SetPrinterStatusChanged(status);
            //m_BaseSetting.SetPrinterStatusChanged(status);
        }

        private void Setting_LoadChangedSettings()
        {
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();
            //Change the IconButton status for calibration
            this.Setting_SetPrinterStatusChanged(printerStatus);

            this.Setting_OnPreferenceChange(m_allParam.Preference);
            this.Setting_OnPrinterPropertyChange(m_allParam.PrinterProperty, printerStatus);
            this.Setting_OnPrinterSettingChange(m_allParam.PrinterSetting, printerStatus);
            //if (printerStatus != JetStatusEnum.PowerOff)
            //    this.Setting_OnRealTimeChange();

            //this.KonicT_OnPrinterPropertyChange(m_allParam.PrinterProperty);
            //this.KonicT_OnPrinterSettingChange(m_allParam.PrinterSetting);
            //this.KonicT_OnRealTimeChange();

            //this.bBaseSettingApply.Location = new Point(this.m_TabPagePrinterSetting.Width - this.bBaseSettingApply.Width - 25,
            //                                                this.m_TabPagePrinterSetting.Height - this.bBaseSettingApply.Height - 25);
            //this.bPreferenceSettingApply.Location = new Point(this.m_TabPagePreference.Width - this.bPreferenceSettingApply.Width - 5,
            //                                                        this.m_TabPagePreference.Height - this.bPreferenceSettingApply.Height - 5);
            this.bSeviceSettingApply.Location =
                new Point(
                    this.m_TabPageService.Width - this.bSeviceSettingApply.Width - 5,
                    this.m_TabPageService.Height - this.bSeviceSettingApply.Height - 5);
            this.tabStrip1.SelectedTab = this.tabStripButtonPrinter;
        }

        private void Setting_ApplyChangedSettings()
        {
            JetStatusEnum printerStatus = CoreInterface.GetBoardStatus();

            bool bChangeProperty = this.m_SeviceSetting.IsDirty;

            this.isDirty = combinedSettings1.IsDirty || combinedSettings1.IsT_VDirty || bChangeProperty;
            // || bChangeTempSetting || bChangePreference;// || bPrinterHWChaged;
            if (this.isDirty == true)
            {
                string info = ResString.GetResString("SettingPage_Question");
                DialogResult dr = MessageBox.Show(
                    info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.Setting_OnGetPrinterSetting(ref m_allParam);

                    if (this.combinedSettings1.IsDirty == true)
                    {
                        CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                        m_PreviewAndInfo.OnPrinterSettingChange(m_allParam.PrinterSetting);
                    }

                    OnPreferenceChange(m_allParam.Preference);

                    if (bChangeProperty) CoreInterface.SetPrinterProperty(ref m_allParam.PrinterProperty);

                    if (this.combinedSettings1.IsT_VDirty && printerStatus != JetStatusEnum.PowerOff) this.combinedSettings1.ApplyToBoard();

                    this.m_SeviceSetting.IsDirty = false;
                    this.combinedSettings1.IsDirty = false;
                    this.combinedSettings1.IsT_VDirty = false;

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Setting_LoadChangedSettings();
                }
            }
        }

        #endregion

        #region 泵墨提示

        private int m_LastErrorCode = 0;

        private bool bisFatalError = false;

        private bool bisBeeping = false;

        private Color cc = SystemColors.Control;

        private int lenOfPumpInktime_K,
                    lenOfPumpInktime_C,
                    lenOfPumpInktime_M,
                    lenOfPumpInktime_Y,
                    lenOfPumpInktime_Lm,
                    lenOfPumpInktime_Lc,
                    lenOfPumpInktime_Nop1,
                    lenOfPumpInktime_Nop2;

        private bool Overtime_K,
                     Overtime_C,
                     Overtime_M,
                     Overtime_Y,
                     Overtime_Lm,
                     Overtime_Lc,
                     Overtime_Nop1,
                     Overtime_Nop2;

        private const int OVERTIME = 12000; //毫秒

        private void PumpInktimer_Tick(object sender, EventArgs e)
        {
#if false
            if (bisFatalError)
            {
                if (this.m_StatusBarPanelError.ForeColor != Color.Transparent)
                {
                    if (cc == Color.Black)
                        this.m_StatusBarPanelError.ForeColor = cc;
                    this.m_StatusBarPanelError.ForeColor = Color.Transparent;
                }
                else
                {
                    if (cc == Color.Black)
                        this.m_StatusBarPanelError.ForeColor = Color.Transparent;
                    this.m_StatusBarPanelError.ForeColor = cc;
                }
                return;
            }
            switch (m_LastErrorCode)
            {
                case CoreBoard_Warning.PUMP_BLACK:
                    if (this.LabelPUMP_BLACK.Visible)
                    {
                        if (this.LabelPUMP_BLACK.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_BLACK.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_BLACK.BackColor = Color.Black;
                    }
                    break;
                case CoreBoard_Warning.PUMP_CYAN:
                    if (this.LabelPUMP_CYAN.Visible)
                    {
                        if (this.LabelPUMP_CYAN.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_CYAN.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_CYAN.BackColor = Color.Cyan;
                    }
                    break;
                case CoreBoard_Warning.PUMP_LIGHTCYAN:
                    if (this.LabelPUMP_LIGHTCYAN.Visible)
                    {
                        if (this.LabelPUMP_LIGHTCYAN.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_LIGHTCYAN.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_LIGHTCYAN.BackColor = Color.LightCyan;
                    }
                    break;
                case CoreBoard_Warning.PUMP_LIGHTMAGENTA:
                    if (this.LabelPUMP_LIGHTMAGENTA.Visible)
                    {
                        if (this.LabelPUMP_LIGHTMAGENTA.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_LIGHTMAGENTA.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_LIGHTMAGENTA.BackColor = Color.Violet;
                    }
                    break;
                case CoreBoard_Warning.PUMP_MAGENTA:
                    if (this.LabelPUMP_MAGENTA.Visible)
                    {
                        if (this.LabelPUMP_MAGENTA.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_MAGENTA.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_MAGENTA.BackColor = Color.Magenta;
                    }
                    break;
                case CoreBoard_Warning.PUMP_YELLOW:
                    if (this.LabelPUMP_YELLOW.Visible)
                    {
                        if (this.LabelPUMP_YELLOW.BackColor != this.statusStrip1.BackColor)
                            this.LabelPUMP_YELLOW.BackColor = this.statusStrip1.BackColor;
                        else
                            this.LabelPUMP_YELLOW.BackColor = Color.Yellow;
                    }
                    break;
                case CoreBoard_Warning.PUMP_SPOTCOLOR1:
                    if (this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout == ImageLayout.Stretch)
                        this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout = ImageLayout.None;
                    else
                        this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout = ImageLayout.Stretch;
                    break;
                case CoreBoard_Warning.PUMP_SPOTCOLOR2:
                    if (this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout == ImageLayout.Stretch)
                        this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout = ImageLayout.None;
                    else
                        this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout = ImageLayout.Stretch;
                    break;
            }
#else
            //EP1BUF[4]为泵墨状态, 1表示泵墨, 0 表示不泵墨. Bit0~Bit7分别表示K, C, M, Y, Lm, Lc, Nop, Nop

            Color old_color = this.statusStrip1.BackColor;
            old_color = Color.Red;
            if ((m_LastErrorCode & 0x1) != 0)
            {
                if (this.LabelPUMP_BLACK.Visible)
                {
                    if (this.LabelPUMP_BLACK.BackColor != old_color) this.LabelPUMP_BLACK.BackColor = old_color;
                    else this.LabelPUMP_BLACK.BackColor = Color.Black;

                    lenOfPumpInktime_K += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_K >= OVERTIME
                        && (lenOfPumpInktime_K - OVERTIME) / this.PumpInktimer.Interval <= 1) //保证提示只显示一次,防止闪烁
                    {
                        Overtime_K = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_K = false;
                lenOfPumpInktime_K = 0;
            }
            if ((m_LastErrorCode & 0x2) != 0)
            {
                if (this.LabelPUMP_CYAN.Visible)
                {
                    if (this.LabelPUMP_CYAN.BackColor != old_color) this.LabelPUMP_CYAN.BackColor = old_color;
                    else this.LabelPUMP_CYAN.BackColor = Color.Cyan;

                    lenOfPumpInktime_C += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_C >= OVERTIME
                        && (lenOfPumpInktime_C - OVERTIME) / this.PumpInktimer.Interval <= 1)
                    {
                        Overtime_C = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_C = false;
                lenOfPumpInktime_C = 0;
            }
            if ((m_LastErrorCode & 0x4) != 0)
            {
                if (this.LabelPUMP_MAGENTA.Visible)
                {
                    if (this.LabelPUMP_MAGENTA.BackColor != old_color) this.LabelPUMP_MAGENTA.BackColor = old_color;
                    else this.LabelPUMP_MAGENTA.BackColor = Color.Magenta;

                    lenOfPumpInktime_M += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_M >= OVERTIME
                        && (lenOfPumpInktime_M - OVERTIME) / this.PumpInktimer.Interval <= 1)
                    {
                        Overtime_M = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_M = false;
                lenOfPumpInktime_M = 0;
            }
            if ((m_LastErrorCode & 0x8) != 0)
            {
                if (this.LabelPUMP_YELLOW.Visible)
                {
                    if (this.LabelPUMP_YELLOW.BackColor != old_color) this.LabelPUMP_YELLOW.BackColor = old_color;
                    else this.LabelPUMP_YELLOW.BackColor = Color.Yellow;

                    lenOfPumpInktime_Y += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_Y >= OVERTIME
                        && (lenOfPumpInktime_Y - OVERTIME) / this.PumpInktimer.Interval <= 1)
                    {
                        Overtime_Y = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_Y = false;
                lenOfPumpInktime_Y = 0;
            }
            if ((m_LastErrorCode & 0x10) != 0)
            {
                if (this.LabelPUMP_LIGHTMAGENTA.Visible)
                {
                    if (this.LabelPUMP_LIGHTMAGENTA.BackColor != old_color) this.LabelPUMP_LIGHTMAGENTA.BackColor = old_color;
                    else this.LabelPUMP_LIGHTMAGENTA.BackColor = Color.Violet;

                    lenOfPumpInktime_Lm += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_Lm >= OVERTIME
                        && (lenOfPumpInktime_Lm - OVERTIME) / this.PumpInktimer.Interval <= 1)
                    {
                        Overtime_Lm = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_Lm = false;
                lenOfPumpInktime_Lm = 0;
            }
            if ((m_LastErrorCode & 0x20) != 0)
            {
                if (this.LabelPUMP_LIGHTCYAN.Visible)
                {
                    if (this.LabelPUMP_LIGHTCYAN.BackColor != old_color) this.LabelPUMP_LIGHTCYAN.BackColor = old_color;
                    else this.LabelPUMP_LIGHTCYAN.BackColor = Color.LightCyan;

                    lenOfPumpInktime_Lc += this.PumpInktimer.Interval;
                    if (lenOfPumpInktime_Lc >= OVERTIME
                        && (lenOfPumpInktime_Lc - OVERTIME) / this.PumpInktimer.Interval <= 1)
                    {
                        Overtime_Lc = true;
                        ToolTipAndBeeptimer_Tick(sender, e);
                    }
                }
            }
            else
            {
                Overtime_Lc = false;
                lenOfPumpInktime_Lc = 0;
            }
            if ((m_LastErrorCode & 0x40) != 0 && this.statusStrip1.Items.Count > m_nCurNum)
            {
                if (this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout == ImageLayout.Stretch) this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout = ImageLayout.None;
                else this.statusStrip1.Items[m_nCurNum].BackgroundImageLayout = ImageLayout.Stretch;
                lenOfPumpInktime_Nop1 += this.PumpInktimer.Interval;
                if (lenOfPumpInktime_Nop1 >= OVERTIME
                    && (lenOfPumpInktime_Nop1 - OVERTIME) / this.PumpInktimer.Interval <= 1)
                {
                    Overtime_Nop1 = true;
                    ToolTipAndBeeptimer_Tick(sender, e);
                }
            }
            else
            {
                Overtime_Nop1 = false;
                lenOfPumpInktime_Nop1 = 0;
            }
            if ((m_LastErrorCode & 0x80) != 0 && this.statusStrip1.Items.Count > m_nCurNum + 1)
            {
                if (this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout == ImageLayout.Stretch) this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout = ImageLayout.None;
                else this.statusStrip1.Items[m_nCurNum + 1].BackgroundImageLayout = ImageLayout.Stretch;
                lenOfPumpInktime_Nop2 += this.PumpInktimer.Interval;
                if (lenOfPumpInktime_Nop2 >= OVERTIME
                    && (lenOfPumpInktime_Nop2 - OVERTIME) / this.PumpInktimer.Interval <= 1)
                {
                    Overtime_Nop2 = true;
                    ToolTipAndBeeptimer_Tick(sender, e);
                }
            }
            else
            {
                Overtime_Nop2 = false;
                lenOfPumpInktime_Nop2 = 0;
            }

            if (
                !(Overtime_K || Overtime_C || Overtime_M || Overtime_Y || Overtime_Lm || Overtime_Lc || Overtime_Nop1
                  || Overtime_Nop2))
            {
                this.toolTip1.Hide((IWin32Window)this);
                if (bisBeeping)
                {
                    SystemCall.Beep(3000, 500);
                    bisBeeping = false;
                }
            }
# endif
        }

        private void FalshInkLable(int code)
        {
            if (code != 0)
            {
                SErrorCode errorcode = new SErrorCode(code);
                ErrorCause cause = (ErrorCause)errorcode.nErrorCause;

                if ((ErrorAction)errorcode.nErrorAction == ErrorAction.Warning)
                {
                    this.m_StatusBarPanelError.Image = SystemIcons.WinLogo.ToBitmap();
                    cc = SystemColors.ControlText;
                    if (cause == ErrorCause.CoreBoard) StartFlashInkLable(errorcode.nErrorCode, false);
                }
                else if ((ErrorAction)errorcode.nErrorAction == ErrorAction.Abort)
                {
                    this.m_StatusBarPanelError.Image = SystemIcons.Error.ToBitmap();
                    cc = Color.Red;
                    StartFlashInkLable(errorcode.nErrorCode, true);
                }
                else
                {
                    this.m_StatusBarPanelError.Image = SystemIcons.Information.ToBitmap();
                    cc = this.m_StatusBarPanelError.ForeColor = SystemColors.ControlText;
                    StopFlashInkLable();
                }
            }
            else
            {
                this.m_StatusBarPanelError.Image = null;
                cc = this.m_StatusBarPanelError.ForeColor = SystemColors.ControlText;
                StopFlashInkLable();
            }
        }

        private void StartFlashInkLable(int code, bool flashErrorLabel)
        {
            bisFatalError = flashErrorLabel;
            m_LastErrorCode = code;
            this.PumpInktimer.Start();
        }

        private void StopFlashInkLable()
        {
            bisFatalError = false;
            lenOfPumpInktime_K =
                lenOfPumpInktime_C =
                lenOfPumpInktime_M =
                lenOfPumpInktime_Y =
                lenOfPumpInktime_Lm = lenOfPumpInktime_Lc = lenOfPumpInktime_Nop1 = lenOfPumpInktime_Nop2 = 0;
            Overtime_K =
                Overtime_C = Overtime_M = Overtime_Y = Overtime_Lm = Overtime_Lc = Overtime_Nop1 = Overtime_Nop2 = false;

            this.m_StatusBarPanelError.Image = null;
            this.m_StatusBarPanelError.BackColor = SystemColors.Control;
#if !false
            this.LabelPUMP_BLACK.BackColor = Color.Black;
            this.LabelPUMP_CYAN.BackColor = Color.Cyan;
            this.LabelPUMP_LIGHTCYAN.BackColor = Color.LightCyan;
            this.LabelPUMP_LIGHTMAGENTA.BackColor = Color.Violet;
            this.LabelPUMP_MAGENTA.BackColor = Color.Magenta;
            this.LabelPUMP_YELLOW.BackColor = Color.Yellow;
#else
            this.m_StatusBarPanelError.BackColor = SystemColors.Control;
            this.m_StatusBarPanelError.ForeColor = Color.Black;
#endif
            this.PumpInktimer.Stop();
            Beeptimer.Stop();
            if (bisBeeping)
            {
                SystemCall.Beep(3000, 500);
                bisBeeping = false;
            }
        }

        private void Beeptimer_Tick(object sender, EventArgs e)
        {
            bisBeeping = SystemCall.Beep(3000, 500);
        }

        private void ToolTipAndBeeptimer_Tick(object sender, EventArgs e)
        {
            Beeptimer.Start();
            toolTip1.ToolTipIcon = ToolTipIcon.Warning;
            toolTip1.ToolTipTitle = ResString.GetResString("PumpInkTimeToLong");
            string info = string.Empty;
            if ((m_LastErrorCode & 0x1) != 0 && this.LabelPUMP_BLACK.Visible && Overtime_K)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.Black) + ",";
            }
            if ((m_LastErrorCode & 0x2) != 0 && this.LabelPUMP_CYAN.Visible && Overtime_C)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.Cyan) + ",";
            }
            if ((m_LastErrorCode & 0x4) != 0 && this.LabelPUMP_MAGENTA.Visible && Overtime_M)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.Magenta) + ",";
            }
            if ((m_LastErrorCode & 0x8) != 0 && this.LabelPUMP_YELLOW.Visible && Overtime_Y)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.Yellow) + ",";
            }
            if ((m_LastErrorCode & 0x10) != 0 && this.LabelPUMP_LIGHTMAGENTA.Visible && Overtime_Lm)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.LightMagenta) + ",";
            }
            if ((m_LastErrorCode & 0x20) != 0 && this.LabelPUMP_LIGHTCYAN.Visible && Overtime_Lc)
            {
                info += ResString.GetEnumDisplayName(typeof(ColorEnum), ColorEnum.LightCyan) + ",";
            }

            info += ResString.GetResString("PumpInkTimeToLong") + ".";

            // create and fill in the tool tip info
            Type type = typeof(ToolTip);
            BindingFlags flags = BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance;
            IntPtr handle = IntPtr.Zero;
#if true
            Object obj = type.InvokeMember("Handle", flags, null, toolTip1, null);
            if ((IntPtr)obj == null) throw new InvalidProgramException();
            else handle = (IntPtr)obj;
#else
            FieldInfo windowField = toolTip1.GetType().GetField("window", flags);
            NativeWindow window = (NativeWindow)windowField.GetValue(toolTip1);
            if (window.Handle == IntPtr.Zero)
                throw new ArgumentNullException("window handle is not crated.");
            else
                handle = window.Handle;
#endif

            SystemCall.RECT rect = new SystemCall.RECT();
            SystemCall.GetClientRect(handle, ref rect);
            Point loc = new Point(
                this.Width - SystemInformation.Border3DSize.Width * 2 - (rect.right - rect.left),
                statusStrip1.Top + SystemInformation.CaptionHeight + SystemInformation.Border3DSize.Height * 2
                - (rect.bottom - rect.top));
            toolTip1.Show(info, (IWin32Window)this, loc, 5000);
            SystemCall.GetClientRect(handle, ref rect);
            loc = new Point(
                this.Width - SystemInformation.Border3DSize.Width * 2 - (rect.right - rect.left),
                statusStrip1.Top + SystemInformation.CaptionHeight + SystemInformation.Border3DSize.Height * 2
                - (rect.bottom - rect.top));
            toolTip1.Show(info, (IWin32Window)this, loc, 5000);
        }

        #endregion

        private void m_DongleKeyAlarm_EncryptDogExpired(object sender, EventArgs e)
        {
            this.Text = m_sFormTile + " " + this.m_allParam.PrinterProperty.ePrinterHead.ToString() + " "
                        + this.m_allParam.PrinterProperty.nHeadNum.ToString()
                        + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head) + " "
                        + this.m_allParam.PrinterProperty.nColorNum.ToString()
                        + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color) + " "
                        + ResString.GetResString("EncryptDog_Expired");
            this.m_JobListForm.TerminatePrintingJob(false);
            UpdateButtonStates(CoreInterface.GetBoardStatus());
        }

        private void m_DongleKeyAlarm_EncryptDogLast100H(object sender, EventArgs e)
        {
            MessageBox.Show(
                ResString.GetResString("EncryptDog_Warning"),
                ResString.GetProductName(),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void m_DongleKeyAlarm_LoadAndCheckDongleKeyFinished(object sender, EventArgs e)
        {
            UpdateButtonStates(CoreInterface.GetBoardStatus());
            if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
            {
                this.m_JobListForm.TerminatePrintingJob(false);
                this.Text = m_sFormTile + " " + this.m_allParam.PrinterProperty.ePrinterHead.ToString() + " "
                            + this.m_allParam.PrinterProperty.nHeadNum.ToString()
                            + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head) + " "
                            + this.m_allParam.PrinterProperty.nColorNum.ToString()
                            + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color) + " "
                            + ResString.GetResString("EncryptDog_Expired");
            }
            else
                this.Text = m_sFormTile + " " + this.m_allParam.PrinterProperty.ePrinterHead.ToString() + " "
                            + this.m_allParam.PrinterProperty.nHeadNum.ToString()
                            + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head) + " "
                            + this.m_allParam.PrinterProperty.nColorNum.ToString()
                            + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color);
        }

        private void FilterPumpInkMsg(ref int code)
        {
            if (this.m_allParam.PrinterProperty.nColorNum == 4 )
            {
                SErrorCode errcode = new SErrorCode(code);

                if (errcode.nErrorAction == (byte)ErrorAction.Warning
                    && errcode.nErrorCause == (byte)ErrorCause.CoreBoard)
                {
                    if (errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTCYAN) errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_CYAN;
                    if (errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_LIGHTMAGENTA) errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_MAGENTA;
                    if (errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR1) errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_YELLOW;
                    if (errcode.nErrorCode == (byte)CoreBoard_Warning.PUMP_SPOTCOLOR2) errcode.nErrorCode = (byte)CoreBoard_Warning.PUMP_BLACK;
                    code = errcode.nErrorCode + (errcode.nErrorSub << 8) + (errcode.nErrorCause << 16)
                           + (errcode.nErrorAction << 24);
                }
            }
        }

        private string UpdateFormHeaderText()
        {
            string text = string.Empty;
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            if (status != JetStatusEnum.PowerOff)
            {
                SPrinterProperty sp = m_allParam.PrinterProperty;
                byte headNum = sp.nHeadNum;
                if (SPrinterProperty.IsKonica512(sp.ePrinterHead))
                    headNum /= 2;
                else if (SPrinterProperty.IsPolarisOneHead4Color(sp.ePrinterHead))
                {
                    headNum /= 8;
                }
                else if (SPrinterProperty.IsPolaris(sp.ePrinterHead))
                {
                    headNum /= 4;
                }
                else if (sp.ePrinterHead == PrinterHeadEnum.EGen5)
                {
                    headNum /= 8;
                }
                if (BYHXSoftLock.m_DongleKeyAlarm.IsILLEGALDOG)
                {
                    text = " " + sp.ePrinterHead.ToString()
                        + " " + headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head)
                        + " " + sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color)
                        + " " + ResString.GetResString("EncryptDog_Expired");
                }
                else
                {
                    text = " " + sp.ePrinterHead.ToString()
                        + " " + headNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Head)
                        + " " + sp.nColorNum.ToString() + ResString.GetEnumDisplayName(typeof(DispName), DispName.Color);
                }
            }
            return text;
        }


    }


}
