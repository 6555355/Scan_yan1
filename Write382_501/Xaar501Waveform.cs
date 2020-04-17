using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager;
using BYHXPrinterManager.GradientControls;

namespace Write382
{
    public partial class Xaar501Waveform : Form
    {
        public SHeadInfoType_501[] m_winfroms;
        private int nCount = 0;
        private int nNoRecv = 0;
        private uint m_KernelMessage = SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
        private bool bFristPowerOnAfterPowerOff = true;
        public class MyButton : Button
        {
            public int id = -1;
        }
        public class MyGrouper : GroupBox
        {
            public int id = -1;
        }
        public struct WAVEFORMWITHINK
        {
            public MyGrouper[] InkGroupers;
            public Label[] labels;
            public NumericUpDown[] IdNumericUpDowns;
            public TextBox[] TextBoxs;
            public MyButton[] FileButtons;
            public MyButton[] DownButtons;
            public MyButton[] ReadButtons;
            public String[] StrHeadType;
            public String[] StrSampleClock;
            public String[] StrWaveformID;
            //winform


            public WAVEFORMWITHINK(int nColorNum)
            {
                InkGroupers = new MyGrouper[nColorNum];
                TextBoxs = new TextBox[nColorNum];
                labels = new Label[nColorNum];
                IdNumericUpDowns = new NumericUpDown[nColorNum];
                FileButtons = new MyButton[nColorNum];
                DownButtons = new MyButton[nColorNum];
                ReadButtons = new MyButton[nColorNum];
                StrHeadType = new String[nColorNum];
                StrSampleClock = new String[nColorNum];
                StrWaveformID = new String[nColorNum];
            }
        }
        private SPrinterProperty m_printerProperty;
        private WAVEFORMWITHINK m_Ctrls;

        public Xaar501Waveform()
        {
            InitializeComponent();
            this.Controls.Add(this.progressBarStatu);
            m_winfroms = new SHeadInfoType_501[8];
            this.FormClosing += OnClose;
        }

        private void Xaar501Waveform_Load(object sender, EventArgs e)
        {
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            OnPrinterStatusChanged(status);
            if (status != JetStatusEnum.PowerOff)
            {
                if (CoreInterface.GetSPrinterProperty(ref m_printerProperty) == 0)
                {
                    Debug.Assert(false);
                }
                else
                {
                    CreatUI();
                }
            }
        }

        private void SetExampleDisable(bool bEnable)
        {
            m_grouperColor.Visible = bEnable;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            CoreInterface.SystemClose();
        }

        private void CreatUI()
        {
            //if (1 == CoreInterface.GetSPrinterProperty(ref m_printerProperty))//非Ready状态无法读到正确的值
            {
                ClearAndCreatCtrls();
                LayoutCtrls();
                SetExampleDisable(false);
                progressBarStatu.Minimum = 0;
                progressBarStatu.Visible = false;
            }

            //Must after printer property because status depend on property sensor measurepaper
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            SetCtrlIsEnable(status != JetStatusEnum.PowerOff);

        }

        public bool Start()
        {
            CoreInterface.SystemInit();
            if (CoreInterface.SetMessageWindow(this.Handle, m_KernelMessage) == 0)
            {
                return false;
            }
            AllParam m_allParam = new AllParam();
            m_allParam.LoadFromXml(null, true);
            m_printerProperty = m_allParam.PrinterProperty;

            return true;
        }

        private void ClearAndCreatCtrls()
        {
            if (m_Ctrls.InkGroupers != null)
            {
                int n = m_Ctrls.InkGroupers.Count();
                //if (n != (int)(m_printerProperty.nColorNum))
                {
                    for (int i = 0; i < n; i++)
                    {
                        this.m_Ctrls.InkGroupers[i].Controls.Remove(m_Ctrls.DownButtons[i]);
                        this.m_Ctrls.InkGroupers[i].Controls.Remove(m_Ctrls.FileButtons[i]);
                        this.m_Ctrls.InkGroupers[i].Controls.Remove(m_Ctrls.IdNumericUpDowns[i]);
                        this.m_Ctrls.InkGroupers[i].Controls.Remove(m_Ctrls.TextBoxs[i]);
                        this.grouper1.Controls.Remove(m_Ctrls.InkGroupers[i]);
                    }
                    m_Ctrls = new WAVEFORMWITHINK((int)(m_printerProperty.nColorNum));
                }
            }
            else
            {
                m_Ctrls = new WAVEFORMWITHINK((int)(m_printerProperty.nColorNum));
            }
        }

        private void ReadAllWaveform()
        {
            this.StartCount();
            byte[] MaxMem = new byte[19 * 8];
            ushort num = 0;
            CoreInterface.Get501HeadInfo(MaxMem, ref num);
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SHeadInfoType_501)));
            if (num <= 8)
                for (int i = 0; i < num; i++)
                {
                    Marshal.Copy(MaxMem, 19 * i, ptr, 19);
                    m_winfroms[i] = (SHeadInfoType_501)Marshal.PtrToStructure(ptr, typeof(SHeadInfoType_501));
                    for (int j = 0; j < m_Ctrls.InkGroupers.Length; j++)
                    {
                        if (Enum.IsDefined(typeof(ColorEnum), m_Ctrls.InkGroupers[j].Text) && (byte)Enum.Parse(typeof(ColorEnum), m_Ctrls.InkGroupers[j].Text) == m_winfroms[i].color)
                        {
                            m_Ctrls.IdNumericUpDowns[j].Value = new Decimal(m_winfroms[i].saveID);
                            //m_Ctrls.StrWaveformID[j] = GetHexString(m_winfroms[i].waveformID);
                            m_Ctrls.StrWaveformID[j] = Encoding.ASCII.GetString(m_winfroms[i].waveformID);
                            if (!Enum.IsDefined(typeof(HEAD_BOARD_TYPE), (int)(m_winfroms[i].head)))
                            {
                                m_Ctrls.StrHeadType[j] = "No define color!";
                            }
                            else
                            {
                                m_Ctrls.StrHeadType[j] = Enum.GetName(typeof(HEAD_BOARD_TYPE), (HEAD_BOARD_TYPE)(m_winfroms[i].head));
                            }
                        }
                    }
                }
            this.EndCount();
        }

        private void OnUiApplyWinfrom()
        {
            for (int i = 0; i < m_winfroms.Length; i++)
            {
                for (int j = 0; j < m_Ctrls.InkGroupers.Length; j++)
                {
                    if (m_winfroms[i].color == (byte)Enum.Parse(typeof(ColorEnum), m_Ctrls.InkGroupers[j].Text, true))
                    {
                        m_Ctrls.IdNumericUpDowns[i].Value = new Decimal(m_winfroms[i].saveID);
                        m_Ctrls.StrHeadType[i] = "Xaar501";
                        m_Ctrls.StrSampleClock[i] = "";
                        m_Ctrls.StrWaveformID[i] = GetHexString(m_winfroms[i].waveformID);
                        m_Ctrls.TextBoxs[i].Text = "";
                    }
                    else
                    {
                        m_Ctrls.IdNumericUpDowns[i].Value = new Decimal(m_winfroms[i].saveID);
                        m_Ctrls.StrHeadType[i] = "No defined color";
                        m_Ctrls.StrSampleClock[i] = "No clock";
                        m_Ctrls.StrWaveformID[i] = GetHexString(m_winfroms[i].waveformID);
                        m_Ctrls.TextBoxs[i].Text = "";
                    }
                }

            }
        }

        private string GetHexString(byte[] pData)
        {
            string temp = "";
            for (int i = 0; i < pData.Length; i++)
            {
                temp += pData[i].ToString("X2") + " ";
            }
            return temp;
        }

        private void LayoutCtrls()
        {
            int space = m_grouperColor.Height;
            int bottom = this.Bottom;
            for (int i = 0; i < m_printerProperty.nColorNum; i++)
            {
                if (m_Ctrls.InkGroupers[i] == null)
                {
                    m_Ctrls.InkGroupers[i] = new MyGrouper();
                }
                m_Ctrls.InkGroupers[i].Size = m_grouperColor.Size;
                string color = Enum.GetName(typeof(ColorEnum), m_printerProperty.eColorOrder[i]);
                if (!string.IsNullOrEmpty(color))
                {
                    m_Ctrls.InkGroupers[i].Text = color;
                }
                else
                {
                    m_Ctrls.InkGroupers[i].Text = "No define color";
                }
                m_Ctrls.InkGroupers[i].Location = i == 0 ? new Point(m_grouperColor.Location.X, m_grouperColor.Location.Y) : new Point(m_grouperColor.Location.X, m_Ctrls.InkGroupers[i - 1].Location.Y + space);
                m_Ctrls.InkGroupers[i].MouseHover += m_grouperColor_MouseHover;
                m_Ctrls.InkGroupers[i].id = i;
                m_Ctrls.InkGroupers[i].Visible = true;
                this.grouper1.Controls.Add(m_Ctrls.InkGroupers[i]);
                //
                if (m_Ctrls.labels[i] ==null)
                {
                    m_Ctrls.labels[i] = new Label();
                }
                ControlClone.LabelClone(m_Ctrls.labels[i], m_labelSaveId);
                m_Ctrls.labels[i].Location = i == 0 ? new Point(m_labelSaveId.Location.X, m_labelSaveId.Location.Y) : new Point(m_labelSaveId.Location.X, m_Ctrls.labels[i - 1].Location.Y);
                m_Ctrls.labels[i].Visible = true;
                this.m_Ctrls.InkGroupers[i].Controls.Add(m_Ctrls.labels[i]);

                if (m_Ctrls.IdNumericUpDowns[i] == null)
                {
                    m_Ctrls.IdNumericUpDowns[i] = new NumericUpDown();
                }
                ControlClone.NumericUpDownClone(m_Ctrls.IdNumericUpDowns[i], m_numericUpDownSaveID);
                m_Ctrls.IdNumericUpDowns[i].Location = i == 0 ? new Point(m_numericUpDownSaveID.Location.X, m_numericUpDownSaveID.Location.Y) : new Point(m_numericUpDownSaveID.Location.X, m_Ctrls.IdNumericUpDowns[i - 1].Location.Y);
                m_Ctrls.IdNumericUpDowns[i].Value = new Decimal(i);
                m_Ctrls.IdNumericUpDowns[i].Visible = true;
                this.m_Ctrls.InkGroupers[i].Controls.Add(m_Ctrls.IdNumericUpDowns[i]);

                if (m_Ctrls.TextBoxs[i] == null)
                {
                    m_Ctrls.TextBoxs[i] = new TextBox();                    
                }
                ControlClone.TextBoxClone(m_Ctrls.TextBoxs[i], m_textBoxPath);
                m_Ctrls.TextBoxs[i].Location = i == 0 ? new Point(m_textBoxPath.Location.X, m_textBoxPath.Location.Y) : new Point(m_textBoxPath.Location.X, m_Ctrls.TextBoxs[i - 1].Location.Y);
                m_Ctrls.TextBoxs[i].Visible = true;
                this.m_Ctrls.InkGroupers[i].Controls.Add(m_Ctrls.TextBoxs[i]);

                if (m_Ctrls.FileButtons[i] == null)
                {
                    m_Ctrls.FileButtons[i] = new MyButton();
                }
                ControlClone.ButtonClone(m_Ctrls.FileButtons[i], m_buttonFile);
                m_Ctrls.FileButtons[i].id = i;
                m_Ctrls.FileButtons[i].Location = i == 0 ? new Point(m_buttonFile.Location.X, m_buttonFile.Location.Y) : new Point(m_buttonFile.Location.X, m_Ctrls.FileButtons[i - 1].Location.Y);
                m_Ctrls.FileButtons[i].Click += m_buttonFile_Click;
                m_Ctrls.FileButtons[i].Visible = true;
                this.m_Ctrls.InkGroupers[i].Controls.Add(m_Ctrls.FileButtons[i]);

                if (m_Ctrls.DownButtons[i]==null)
                {
                    m_Ctrls.DownButtons[i] = new MyButton();
                }
                ControlClone.ButtonClone(m_Ctrls.DownButtons[i], m_buttonUpdata);
                m_Ctrls.DownButtons[i].id = i;
                m_Ctrls.DownButtons[i].Location = i == 0 ? new Point(m_buttonUpdata.Location.X, m_buttonUpdata.Location.Y) : new Point(m_buttonUpdata.Location.X, m_Ctrls.DownButtons[i - 1].Location.Y);
                m_Ctrls.DownButtons[i].Click += m_buttonUpdata_Click;
                m_Ctrls.DownButtons[i].Visible = true;
                this.m_Ctrls.InkGroupers[i].Controls.Add(m_Ctrls.DownButtons[i]);
                bottom = m_Ctrls.InkGroupers[i].Bottom + 10;


            }
            this.grouper1.Height = bottom;
            this.Height = bottom + 100;
            this.grouper3.Height = this.grouper1.Height - this.grouper2.Height;
            this.grouper2.Location = new Point(this.grouper2.Left, this.grouper3.Bottom);

        }

        private void StartCount()
        {
            this.SetCtrlIsEnable(false);
            this.nCount = 0;
            this.nNoRecv = 0;
        }

        private void EndCount()
        {
            this.SetCtrlIsEnable(true);
            this.nCount = 0;
        }

        private void m_buttonUpdata_Click(object sender, EventArgs e)
        {
            StartCount();
            try
            {
                //for (int i = 0; i < m_printerProperty.nColorNum; i++)
                int i = (sender as MyButton).id;
                {
                    string path = m_Ctrls.TextBoxs[i].Text;
                    if (File.Exists(path) && Enum.IsDefined(typeof(ColorEnum), m_Ctrls.InkGroupers[i].Text))
                    {
                        ColorEnum type = (ColorEnum)(Enum.Parse(typeof(ColorEnum), m_Ctrls.InkGroupers[i].Text));
                        Decimal saveID = m_Ctrls.IdNumericUpDowns[i].Value;
                        if (saveID < 0 || saveID > 8)
                        {
                            if (DialogResult.OK == MessageBox.Show("No saveID,continue?", "Warning", MessageBoxButtons.OKCancel))
                            {
                                UpdateCoreBoard(path, type, 0);
                                progressBarStatu.Maximum = ++nCount;
                            }
                            else
                            {
                                return;
                            }

                        }
                        else
                        {
                            UpdateCoreBoard(path, type, (byte)saveID);
                            progressBarStatu.Maximum = ++nCount;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (nCount==0)
            {
                EndCount();                
            }
        }

        private void m_buttonFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.CheckFileExists = true;
                fileDialog.DefaultExt = ".txt";
                fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Txt);
                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    MyButton own = (MyButton)sender;
                    if (null != own && own.id >= 0 && own.id <= 8)
                    {
                        m_Ctrls.TextBoxs[own.id].Text = fileDialog.FileName;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void UpdateCoreBoard(string m_UpdaterFileName, ColorEnum color, byte saveId)
        {
            //添加喷头类型,颜色,saveId,waveformID
            HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            byte[] wfdata = PubFunc.GetAllDataFromFile(m_UpdaterFileName);
            if (null == wfdata)
            {
                MessageBox.Show("wavefrom file erro!");
                return;
            }
            int nLen = 3 + wfdata.Length;
            byte[] val = new byte[nLen];
            val[0] = (byte)headBoardType;
            val[1] = (byte)color;
            val[2] = saveId;
            Array.Copy(wfdata, 0, val, 3, wfdata.Length);
            int ret = CoreInterface.Down382WaveForm(val, nLen, 0x01);
        }

        private byte[] GetBufferFromString(string str)
        {
            char[] pStr = str.ToCharArray();
            byte[] pdata = new byte[str.Length];
            for (int i = 0; i < pdata.Length; i++)
            {
                pdata[i] = (byte)pStr[i];
            }
            return pdata;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.WParam.ToInt32() == 0xF060)   //   关闭消息   
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
                        //this.progressBarStatu.Value = nRecv/nCount + percentage/100;
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
                            if (CoreInterface.GetSPrinterProperty(ref m_printerProperty) == 0)
                            {
                                Debug.Assert(false);
                            }
                            else
                            {
                                if (bFristPowerOnAfterPowerOff)
                                {
                                    bFristPowerOnAfterPowerOff = false;
                                    CreatUI();
                                }
                                this.SetCtrlIsEnable(true);
                            }
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
                            if (0 != serrorcode.nErrorCode)
                            {
                                if (serrorcode.nErrorCode == 1)
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
                                nNoRecv = --nCount;
                                //this.progressBarStatu.Value =nNoRecv;
                                if (nNoRecv <= 0)
                                {
                                    EndCount();
                                }

                            }
                        }

                        break;
                    }
                case CoreMsgEnum.Parameter_Change:
                    {
                        break;
                    }
            }
        }

        public void OnPrintingProgressChanged(int percent)
        {
            string info = "";
            string mPrintingFormat = ResString.GetPrintingProgress();
            info += "\n" + string.Format(mPrintingFormat, percent);
            this.m_StatusBarPanelPercent.Text = info;
        }
        public void OnErrorCodeChanged(int code)
        {
            this.m_StatusBarPanelError.Text = SErrorCode.GetInfoFromErrCode(code);
        }
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

                    if (MessageBox.Show(errorInfo, ResString.GetProductName(), MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
                    {
                        CoreInterface.Printer_Resume();
                    }
                }
            }
            else
                OnErrorCodeChanged(0);
        }
        public void SetPrinterStatusChanged(JetStatusEnum status)
        {
            string info = ResString.GetEnumDisplayName(typeof(JetStatusEnum), status);
            this.m_StatusBarPanelJetStaus.Text = info;
        }

        private void SetCtrlIsEnable(bool bEnable)
        {
            for (int i = 0; i < m_printerProperty.nColorNum; i++)
            {
                if (null != m_Ctrls.FileButtons && null != m_Ctrls.DownButtons)
                {
                    m_Ctrls.FileButtons[i].Enabled = bEnable;
                    m_Ctrls.DownButtons[i].Enabled = bEnable;
                    m_Ctrls.IdNumericUpDowns[i].Enabled = bEnable;
                    m_Ctrls.TextBoxs[i].Enabled = bEnable;
                    m_buttonSyn.Enabled = bEnable;
                    m_buttonApply.Enabled = bEnable;
                }
            }
        }

        private void UpdateButtonStates(JetStatusEnum status)
        {
            if (status == JetStatusEnum.PowerOff)
            {
                bFristPowerOnAfterPowerOff = true;
                SetCtrlIsEnable(false);
            }
        }

        private void m_grouperColor_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (null != sender)
                {
                    MyGrouper myGrouper = (MyGrouper)sender;
                    m_textBoxHead.Text = m_Ctrls.StrHeadType[myGrouper.id];
                    //textBoxSampleclock.Text = m_Ctrls.StrSampleClock[myGrouper.id];
                    m_textBoxWaveformId.Text = m_Ctrls.StrWaveformID[myGrouper.id];

                }
            }
            catch (System.Exception ex)
            {

            }

        }

        private void m_grouperColor_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void m_buttonSyn_Click(object sender, EventArgs e)
        {
            ReadAllWaveform();
        }

        private void m_buttonApply_Click(object sender, EventArgs e)
        {
            m_buttonUpdata_Click(null, null);
        }

        private void Xaar501Waveform_SizeChanged(object sender, EventArgs e)
        {
            this.progressBarStatu.Location = new Point(0, m_StatusBarApp.Top - progressBarStatu.Height);
            this.progressBarStatu.Width = this.Width;
        }

    }
}
