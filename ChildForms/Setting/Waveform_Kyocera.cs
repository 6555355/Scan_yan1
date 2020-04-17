using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BYHXPrinterManager.Setting
{
    public partial class Waveform_Kyocera : Form
    {
        private AutoResetEvent downloadEvent = new AutoResetEvent(false);

        public AutoResetEvent DownloadEvent
        {
            get { return downloadEvent; }
            set { downloadEvent = value; }
        }
        //private int m_nHeadId = 0;
        private List<CheckBox> headList;
        private List<CheckBox> abList;
        public SPrinterProperty m_printerProperty { get;set; }
        public Waveform_Kyocera()
        {
            InitializeComponent(); 
            readWfWorker = new BackgroundWorker();
            readWfWorker.WorkerReportsProgress = true;
            readWfWorker.ProgressChanged += new ProgressChangedEventHandler(readWfWorker_ProgressChanged);
            readWfWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(readWfWorker_RunWorkerCompleted);
            readWfWorker.DoWork += new DoWorkEventHandler(readWfWorker_DoWork);
        }
        private int GetDrivingWaveformBytes()
        {
            int bytes;
            if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 ||
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl)
            {
                bytes = PubFunc.KJ4A_RH06_WaveformLength;
            }
            else
            {
                bytes = PubFunc.Kyocera_WaveformLength;
            }
            return bytes;
        }

        private string path = "";
        private bool ExportSuccess = true;
        void readWfWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage >= 1)
            //{
            //    int bytes = GetDrivingWaveformBytes();
            //    lock (readWaveformDatas)
            //    {

            //        for (int i = 0; i < readWaveformDatas.Count; i++)
            //        {
            //            if (abList[i].Checked)
            //            {

            //                List<byte> temp = new List<byte>();
            //                string filename;
            //                if (panel4.Visible)
            //                {
            //                    filename = string.Format("{0}_HeadBoard{1}_Head{2}_{3}.txt",
            //                        Path.GetFileNameWithoutExtension(path),
            //                        _hbindexReadall, e.ProgressPercentage,
            //                        i == 0 ? "A" : "B");
            //                }
            //                else
            //                {
            //                    filename = string.Format("{0}_HeadBoard{1}_Head{2}.txt",
            //                        Path.GetFileNameWithoutExtension(path),
            //                        _hbindexReadall, e.ProgressPercentage);
            //                }
            //                string savepath = Path.Combine(Path.GetDirectoryName(path), filename);
            //                //StreamWriter sw = new StreamWriter(savepath, false);
            //                int j = 0;
            //                for (int h = 7; h < readWaveformDatas[i].Length; h++)
            //                {
            //                    temp.Add(readWaveformDatas[i][h]);
            //                    //sw.WriteLine(string.Format("{0}", readWaveformDatas[i][h]));
            //                    j++;
            //                    if (j == bytes)
            //                        break;
            //                }
            //                //sw.Flush();
            //                //sw.Close();
            //                //加密
            //                PubFunc.EncryptSave(savepath, temp.ToArray());
            //            }
            //        }
            //    }
            //}
        }
        private void readWfWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(@"Export Complete");
            groupBox4.Enabled = true;
        }
        private void Waveform_Kyocera_Load(object sender, EventArgs e)
        {
            USER_SET_INFORMATION factoryData = new USER_SET_INFORMATION();
            int ret = CoreInterface.GetUserSetInfo(ref factoryData);
            if (ret != 0)
            {
                numHBIndexW.Items.Clear();
                for (int i = 0; i < factoryData.HeadBoardNum; i++)
                {
                    numHBIndexW.Items.Add(string.Format("#{0}", i + 1));
                }
                numHBIndexW.SelectedIndex = 0;
            }
            headList = new List<CheckBox>() { checkBox1, checkBox2, checkBox3, checkBox4 };
            abList = new List<CheckBox>() { checkBoxA, checkBoxB };
            checkBoxB.Checked=panel4.Visible = m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c
                || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c;
        }

        private void m_buttonFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    CheckFileExists = true,
                    DefaultExt = ".txt",
                    Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Txt)
                };
                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string path = fileDialog.FileName;

                    if (!File.Exists(path) || 
                        numHBIndexW.SelectedIndex < 0||
                        headList.All(head => { return !head.Checked; })|| 
                        abList.All(cbk => { return !cbk.Checked; }))
                    {
                        MessageBox.Show(@"Input is wrong.");
                        return;
                    }
                    groupBox4.Enabled = false;
                    int hbindex = numHBIndexW.SelectedIndex;
                    string info = "";
                    HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
                    byte[] wfdata;
                    if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl)
                    {
                        wfdata = PubFunc.GetAllDataFromFileKyocera_KJ4A_RH06_Des(path, ref info);
                        //wfdata = PubFunc.GetAllDataFromFileKyocera_KJ4A_RH06(m_UpdaterFileName);
                    }
                    else
                    {
                        wfdata = PubFunc.GetAllDataFromFileKyocera_Des(path, ref info);
                        //wfdata = PubFunc.GetAllDataFromFileKyocera(m_UpdaterFileName);
                    }
                    if (null == wfdata)
                    {
                        if (info == "")
                        {
                            MessageBox.Show("wavefrom file error!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(info);
                            return;
                        }
                    }
                    Task.Factory.StartNew(new Action(() =>
                    {
                        downloadEvent.Reset();
                        for (int i = 0; i < headList.Count; i++)
                        {
                            for (int j = 0; j < abList.Count; j++)
                            {
                                if (headList[i].Checked && abList[j].Checked)
                                {
                                    ColorEnum type = ColorEnum.Cyan;//(ColorEnum)(Enum.Parse(typeof(ColorEnum), m_Ctrls.InkGroupers[i].Text));
                                    Decimal saveID = hbindex * 8 + i * 2 + j;
                                    if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl ||
                                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 ||
                                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl ||
                                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl ||
                                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl)
                                        saveID = hbindex * 4 + i;
                                    //m_nHeadId = (int)saveID;
                                    {

                                        UpdateCoreBoard(wfdata, type, (byte)saveID);
                                        //progressBarStatu.Maximum = ++nCount;
                                    }
                                    downloadEvent.WaitOne();
                                    downloadEvent.Reset();
                                }
                            }
                        }
                        MessageBox.Show(@"Download complete");
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] downloadedData;
        private void UpdateCoreBoard(byte[] wfdata, ColorEnum color, byte saveId)
        {
            HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            int nLen = 3 + wfdata.Length;
            byte[] val = new byte[nLen];
            val[0] = (byte)headBoardType;
            val[1] = (byte)color;
            val[2] = saveId;
            Array.Copy(wfdata, 0, val, 3, wfdata.Length);
            int ret = CoreInterface.Down382WaveForm(val, nLen, 0x01);
            downloadedData = wfdata;
        }

        private BackgroundWorker readWfWorker;
        private int _hbindexReadall = 0;
        private void m_buttonUpdata_Click(object sender, EventArgs e)
        {
            try
            {
                if (numHBIndexW.SelectedIndex < 0 ||
                        headList.All(head => { return !head.Checked; })|| 
                        abList.All(cbk => { return !cbk.Checked; }))
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    
                    _hbindexReadall = (int)numHBIndexW.SelectedIndex;
                    if (!readWfWorker.IsBusy)
                    {
                        SaveFileDialog saveas = new SaveFileDialog();
                        saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
                        saveas.Filter = "文本文件|*.txt";
                        saveas.DefaultExt = "txt";
                        DialogResult dr = saveas.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {
                            path = saveas.FileName;
                            groupBox4.Enabled = false;
                            readWfWorker.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Background Worker Is Busy!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void readWfWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                try
                {
                    for (int i = 0; i < headList.Count; i++)
                    {
                        if (headList[i].Checked)
                        {
                            switch (m_printerProperty.ePrinterHead)
                            {
                                case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                                case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                                    {
                                        ReadAllForKyocera_KJ4B(i);
                                        break;
                                    }
                                case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                                case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                                case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                                    {
                                        ReadAllForKJ4A_TA06(i);
                                        break;
                                    }
                                case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                                case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                                    {
                                        ReadAllForKJ4A_RH06(i);
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportWf(int headid)
        {
            int bytes = GetDrivingWaveformBytes();
            for (int i = 0; i < readWaveformDatas.Count; i++)
            {
                if (abList[i].Checked)
                {

                    List<byte> temp = new List<byte>();
                    string filename;
                    if (panel4.Visible)
                    {
                        filename = string.Format("{0}_HeadBoard{1}_Head{2}_{3}.txt",
                            Path.GetFileNameWithoutExtension(path),
                            _hbindexReadall, headid + 1,
                            i == 0 ? "A" : "B");
                    }
                    else
                    {
                        filename = string.Format("{0}_HeadBoard{1}_Head{2}.txt",
                            Path.GetFileNameWithoutExtension(path),
                            _hbindexReadall, headid + 1);
                    }
                    string savepath = Path.Combine(Path.GetDirectoryName(path), filename);
                    //StreamWriter sw = new StreamWriter(savepath, false);
                    int j = 0;
                    for (int h = 7; h < readWaveformDatas[i].Length; h++)
                    {
                        temp.Add(readWaveformDatas[i][h]);
                        //sw.WriteLine(string.Format("{0}", readWaveformDatas[i][h]));
                        j++;
                        if (j == bytes)
                            break;
                    }
                    //sw.Flush();
                    //sw.Close();
                    //加密
                    PubFunc.EncryptSave(savepath, temp.ToArray());
                }
            }
        }
        List<byte[]> readWaveformDatas = new List<byte[]>();
        private void ReadAllForKyocera_KJ4B(int headindex)
        {
            readWaveformDatas = new List<byte[]>();
            ushort value = (ushort)_hbindexReadall;
            byte headid = (byte)headindex;
            string msg = "";
            msg = string.Empty;
            bool retA = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmd.ReadGroupAWaveform, ref msg);
            if (retA)
            {
                readWaveformDatas.Add(ep6Data);
            }
            msg = string.Empty;
            bool retB = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmd.ReadGroupBWaveform, ref msg);
            if (retB)
            {
                readWaveformDatas.Add(ep6Data);
            }
            if (retB && retA)
            {
                //readWfWorker.ReportProgress(headid+1, msg);
                ExportWf(headid);
            }
        }

        private void ReadAllForKJ4A_TA06(int headindex)
        {
            readWaveformDatas = new List<byte[]>();
            ushort value = (ushort)_hbindexReadall;
            byte headid = (byte)headindex;
            string msg = string.Empty;
            bool ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadWaveform, ref msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
                //readWfWorker.ReportProgress(headindex+1, msg);
                ExportWf(headid);
            }
        }

        private void ReadAllForKJ4A_RH06(int headindex)
        {
            readWaveformDatas = new List<byte[]>();
            ushort value = (ushort)_hbindexReadall;
            byte headid = (byte)headindex;
            string msg = "";
            bool ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingWaveform, ref msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
                //readWfWorker.ReportProgress(headindex+1, msg);
                ExportWf(headid);
            }
        }
        private byte[] ep6Data;
        private volatile bool bReadAllEvent = false;
        
        public void OnEp6DataChanged(int ep6Cmd, int index, byte[] buf)
        {
            ep6Data = new byte[buf.Length];
            Buffer.BlockCopy(buf, 0, ep6Data, 0, buf.Length);
            //if (buf[4] == m_WaitingEp6PipeCmd) //FE FE FE LEN CMD
            bReadAllEvent = true;
        }
        private bool SendAndReciveEp6Data(ushort hbid, byte wfId, byte subCmd, ref string msg)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[10];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = wfId;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x04;
            buf[7] = subCmd;

            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", msg), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            bool bOk = false;
            for (int i = 0; i < 10; i++)
            {
                if (bReadAllEvent)
                {
                    bOk = true;
                    break;
                }
                Thread.Sleep(300);
            }
            if (bOk)
            {
                if (ep6Data[2] == 0xfe || ep6Data[2] == 0)
                {
                    msg = (BitConverter.ToString(ep6Data.Skip(7).Take(ep6Data.Length - 9).ToArray()));
                }
                else
                {
                    //string errInfo = ASCIIEncoding.ASCII.GetString(ep6Data.Skip(3).Take(ep6Data.Length - 3).ToArray());
                    switch (ep6Data[3])
                    {
                        case 1:
                            msg = ("Does not support this command!");
                            break;
                        case 2:
                            msg = ("HeadNO. error! ");
                            break;
                        case 0x41:
                            msg = ("Send data to the nozzle failure! ");
                            break;
                        case 0x42:
                            msg = ("Head no response!");
                            break;
                        case 0x43:
                            msg = ("Head not connected!");
                            break;
                        default:
                            msg = string.Format("Unknown error={0}!", ep6Data[2]);
                            break;
                    }
                    return false;
                    //msg = string.Format("ep6 read fail!! cmd={0};ret={1};{2}", ep6Data[1], ep6Data[2], errInfo);
                    //return false;
                }
            }
            else
            {
                msg = ("ep6 timeout！");
                return false;
            }
            return true;
        }
    }
    public enum KyoceraCmd : byte
    {
        ReadSerialNumber = 0x10,  //Len = 8
        ReadNumberOfDriveTimes = 0x11,
        ReadGroupAWaveform = 0x1a,
        ReadGroupBWaveform = 0x1B,
        ReadAdjustmentVoltage = 0x16,
        ReadVoltageSetting = 0x12,
        ReadStopFunctionStatus = 0x32,
        ReadTempLimit = 0x17,
        ReadHeaterTemp = 0x82,

        WriteGroupAWaveform = 0x5a,
        WriteGroupBWaveform = 0x5B,
        WriteAdjustmentVoltage = 0x56,
        WriteVoltageSetting = 0x52,
        WriteStopFunctionStatus = 0x72,
        WriteTempLimit = 0x57,
    };

    public enum KyoceraCmdForKJ4A_TA06 : byte
    {
        ReadSerialNumber = 0x10,  //Len = 8
        ReadNumberOfDriveTimes = 0x11,
        ReadWaveform = 0x20, //ForKJ4A_TA06
        ReadAdjustmentVoltage = 0x17,
        ReadDLYSEL = 0x12,
        ReadStopFunctionStatus = 0x4F,


        WriteWaveform = 0x30,//ForKJ4A_TA06
        WriteAdjustmentVoltage = 0x47,
        WriteDLYSEL = 0x43,
        WriteStopFunctionStatus = 0x4E,
    };

    public enum KyoceraCmdForKJ4A_RH06 : byte
    {
        ReadSerialNumber = 0x10,
        ReadNumberOfDriveTimes = 0x11,
        ReadTotalAdjustmentVoltage = 0x16,
        ReadVoltageSettingByUnit = 0x12,
        ReadTempLimitsForHeaterSetting = 0x17,
        ReadHeaterTemp = 0x85,
        ReadDrivingWaveform = 0x20,
        ReadDelaySettingForU13 = 0x28,
        ReadDelaySettingForU24 = 0x30,
        ReadDrivingStopFunctionStatus = 0x38,

        WriteTotalAdjustmentVoltage = 0x46,
        WriteVoltageSettingByUnit = 0x42,
        WriteTempLimitsForHeaterSetting = 0x47,
        WriteDrivingWaveform = 0x50,
        WriteDelaySettingForU13 = 0x58,
        WriteDelaySettingForU24 = 0x60,
        WriteDrivingStopFunctionStatus = 0x68,

        FixingWrittenDrivingDate = 0x43,
    };
}
