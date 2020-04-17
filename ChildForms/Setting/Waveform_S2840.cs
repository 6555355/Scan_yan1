using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
    public partial class Waveform_S2840 : Form
    {
        private AutoResetEvent _downloadWaveformAutoResetEvent = new AutoResetEvent(false);
        private bool _downloadSuccess = false;
        public AutoResetEvent DownloadWaveformAutoResetEvent
        {
            get { return _downloadWaveformAutoResetEvent; }
            set { _downloadWaveformAutoResetEvent = value; }
        }
        public bool DownloadSuccess
        {
            get { return _downloadSuccess; }
            set { _downloadSuccess = value; }
        }
        readonly HEAD_BOARD_TYPE _headBoardType = 0;
        public Waveform_S2840()
        {
            InitializeComponent();
            _headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            int headNum = 0;
            switch (_headBoardType)
            {
                case HEAD_BOARD_TYPE.EPSON_S1600_8H:
                    {
                        if (CoreInterface.IsS_system())
                        {
                            USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                            CoreInterface.GetUserSetInfo(ref info);
                            headNum = 8 * info.HeadBoardNum;
                        }
                        else
                        {
                            headNum = 8;
                        }
                    }
                    break;
                case HEAD_BOARD_TYPE.EPSON_S2840_4H:
                    {
                        if (CoreInterface.IsS_system())
                        {
                            USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                            CoreInterface.GetUserSetInfo(ref info);
                            headNum = 4 * info.HeadBoardNum;
                        }
                        else
                        {
                            headNum = 4;
                        }
                    }
                    break;
                case HEAD_BOARD_TYPE.EPSON_5113_2H:
                    {
                        if (CoreInterface.IsS_system())
                        {
                            USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                            CoreInterface.GetUserSetInfo(ref info);
                            headNum = 2 * info.HeadBoardNum;
                        }
                        else
                        {
                            headNum = 2;
                        }
                    }
                    break;
                case HEAD_BOARD_TYPE.EPSON_5113_8H:
                {
                    if (CoreInterface.IsS_system())
                    {
                        USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                        CoreInterface.GetUserSetInfo(ref info);
                        headNum = 8 * info.HeadBoardNum;
                    }
                    else
                    {
                        headNum = 8;
                    }
                }
                    break;
                case HEAD_BOARD_TYPE.EPSON_5113_6H:
                    {
                        if (CoreInterface.IsS_system())
                        {
                            USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                            CoreInterface.GetUserSetInfo(ref info);
                            headNum = 6 * info.HeadBoardNum;
                        }
                        else
                        {
                            headNum = 6;
                        }
                    }
                    break;
                case HEAD_BOARD_TYPE.Ricoh_Gen6_16H:
                {
                    if (CoreInterface.IsS_system())
                    {
                        USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                        CoreInterface.GetUserSetInfo(ref info);
                        headNum = 16 * info.HeadBoardNum;
                    }
                    else
                    {
                        headNum = 16;
                    }
                }
                    break;
                default:
                    headNum = 0;
                    //MessageBox.Show(@"Unsupported headboard type!");
                    break;
            }
            for (int i = 0; i < headNum; i++)
            {
                LabelButtonTextBox lbt = new LabelButtonTextBox { LabelHead = "H" + (i + 1) + ":", HeadNo = i };
                flowLayoutPanel_Content.Controls.Add(lbt);
            }
        }

        private byte[] GetBytesFromWaveFormInfo(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] buffer=new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private bool DownloadWaveForm(byte[] info,int headno)
        {
            bool ret = false;
            switch (_headBoardType)
            {
                case HEAD_BOARD_TYPE.EPSON_S2840_4H:
                case HEAD_BOARD_TYPE.EPSON_S1600_8H:
                    {
                        _downloadSuccess = false;
                        _downloadWaveformAutoResetEvent.Reset();
                        int nLen = 21 + info.Length;
                        byte[] val = new byte[nLen];
                        val[0] = 0x3B;
                        val[1] = 0x00;
                        val[2] = (byte)headno;
                        val[19] = 1;
                        Array.Copy(info, 0, val, 21, info.Length);
                        ret = CoreInterface.Down382WaveForm(val, nLen, 0) != 0 && _downloadWaveformAutoResetEvent.WaitOne(5000) && _downloadSuccess;
                    }
                    break;
                case HEAD_BOARD_TYPE.EPSON_5113_2H:
                case HEAD_BOARD_TYPE.EPSON_5113_8H:
                case HEAD_BOARD_TYPE.EPSON_5113_6H:
                case HEAD_BOARD_TYPE.EPSON_I3200_4H_8DRV:
                    {
                        _downloadSuccess = false;
                        _downloadWaveformAutoResetEvent.Reset();
                        int nLen = 21 + info.Length;
                        byte[] val = new byte[nLen];
                        val[0] = 0x3B;
                        val[1] = 0x00;
                        val[2] = (byte)headno;
                        val[19] = 1;
                        Array.Copy(info, 0, val, 21, info.Length);
                        ret = CoreInterface.Down382WaveForm(val, nLen, 0) != 0;
                    }
                    break;
                case HEAD_BOARD_TYPE.Ricoh_Gen6_16H:
                    {
                        _downloadSuccess = false;
                        _downloadWaveformAutoResetEvent.Reset();
                        byte reqCode = 0x80;
                        byte value = 0;
                        byte index = 0;
                        List<byte> buffer = new List<byte>()
                        {
                            0xC5,
                            0xDB,
                            0x00,
                        };
                        uint bufferSize = (uint)buffer.Count;
                        if (CoreInterface.SetEpsonEP0Cmd(reqCode, buffer.ToArray(), ref bufferSize, value, index) != 0)
                        {
                            int nLen = 21 + info.Length;
                            byte[] val = new byte[nLen];
                            val[0] = 0x3B;
                            val[1] = 0x00;
                            val[2] = (byte)headno;
                            val[19] = 1;
                            Array.Copy(info, 0, val, 21, info.Length);
                            ret = CoreInterface.Down382WaveForm(val, nLen, 0) != 0 && _downloadWaveformAutoResetEvent.WaitOne(5000) && _downloadSuccess;
                        }
                    }
                    break;
                default:
                    break;
            }
            return ret;
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = @"waveform file|*.wav"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Control control in flowLayoutPanel_Content.Controls)
                {
                    LabelButtonTextBox lbt = control as LabelButtonTextBox;
                    if (lbt != null)
                    {
                        lbt.FileName = openFileDialog.FileName;
                    }
                }
            }
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            Thread thread=new Thread(DownloadWaveFormThread);
            thread.Start();
        }

        private void SetEnabled(bool enable)
        {
            foreach (Control control in Controls)
            {
                control.Enabled = enable;
            }
        }

        private void DownloadWaveFormThread()
        {
            SetEnabled(false);
            try
            {
                int emptyNum = 0;
                string info = string.Empty;
                foreach (Control control in flowLayoutPanel_Content.Controls)
                {
                    LabelButtonTextBox lbt = control as LabelButtonTextBox;
                    if (lbt != null)
                    {
                        if (!string.IsNullOrEmpty(lbt.FileName))
                        {
                            if (File.Exists(lbt.FileName))
                            {
                                if (!DownloadWaveForm(GetBytesFromWaveFormInfo(lbt.FileName), lbt.HeadNo))
                                {
                                    info += "“" + lbt.LabelHead + "”";
                                }
                            }
                            else
                            {
                                info += "“" + lbt.LabelHead + "”";
                            }
                        }
                        else
                        {
                            emptyNum++;
                        }
                    }
                }
                if (emptyNum == flowLayoutPanel_Content.Controls.Count)
                {
                    //如果都为空就不提示下载信息了；
                    return;
                }
                if (info == string.Empty)
                {
                    MessageBox.Show(@"Download Success!");
                }
                else
                {
                    MessageBox.Show(@"Download Fail!" + info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetEnabled(true);
            }
        }
    }
}
