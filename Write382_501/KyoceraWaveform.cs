using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BYHXPrinterManager;
using BYHXPrinterManager.GradientControls;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace Write382
{
    public partial class KyoceraWaveform : Form
    {

        private byte m_WaitingEp6PipeCmd = 0;
        private int m_nHeadId = 0;
        private int nCount = 0;
        private int nNoRecv = 0;
        private uint m_KernelMessage = SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");
        private bool bFristPowerOnAfterPowerOff = true;
        private string writeDelaySettingPath = string.Empty;

        private SPrinterProperty m_printerProperty;
        private BackgroundWorker readVolWorker;
        private BackgroundWorker writeVolWorker;
        private List<CheckBox> headList;
        private List<CheckBox> abList;
        private AllParam m_allParam;
        public KyoceraWaveform()
        {
            InitializeComponent();
            this.Controls.Add(this.progressBarStatu);
            this.FormClosing += OnClose;
            readallWorker = new BackgroundWorker();
            readallWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(readallWorker_RunWorkerCompleted);
            readallWorker.DoWork += new DoWorkEventHandler(readallWorker_DoWork);

            readWfWorker = new BackgroundWorker();
            readWfWorker.WorkerReportsProgress = true;
            readWfWorker.ProgressChanged += new ProgressChangedEventHandler(readWfWorker_ProgressChanged);
            readWfWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(readWfWorker_RunWorkerCompleted);
            readWfWorker.DoWork += new DoWorkEventHandler(readWfWorker_DoWork);

            readVolWorker= new BackgroundWorker();
            readVolWorker.WorkerReportsProgress = true;
            readVolWorker.ProgressChanged += new ProgressChangedEventHandler(readVolWorker_ProgressChanged);
            readVolWorker.DoWork += new DoWorkEventHandler(readwriteWorker_DoWork);

            writeVolWorker= new BackgroundWorker();
            writeVolWorker.WorkerReportsProgress = true;
            writeVolWorker.DoWork += new DoWorkEventHandler(writeVolWorker_DoWork);
            writeVolWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(writeVolWorker_RunWorkerCompleted);
            writeVolWorker.ProgressChanged += new ProgressChangedEventHandler(writeVolWorker_ProgressChanged);

            headList = new List<CheckBox>() { checkBox1, checkBox2, checkBox3, checkBox4};
            abList = new List<CheckBox>(){checkBoxA,checkBoxB};
    }
        int ConvertTempToData( int temp)
        {
            int limitdata = 0;
            limitdata = (int)(4095.0f * 10000.0f / (10000.0f + 10000.0f *Math.Exp(3450 * (1.0 / (273.5f + temp) - 1.0f / 298.15f))));
            return limitdata;
        }
        int ConvertDataToTemp(int limitdata)
        {
            int temp = 0;
            double R = Math.Log((4095.0f*10000.0f/limitdata -10000.0f)/10000.0f);
            R =     1.0f/(R/3450.0f + 1.0f / 298.15f) -273.5f;
            temp = (int)Math.Round(R);
            return temp;
        }
        void PaserEp6Buf(byte[] buf, int bufIndex, out string Asii, out string parserInfo )
        {
            Asii = "";
            parserInfo = "";
            int curIndex = bufIndex + 2;

            switch (buf[bufIndex + 1])
            {
                case (byte)KyoceraCmd.ReadSerialNumber:
                    //Len = 8;
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 8);
                    parserInfo = string.Format("year={0};month={1}; number= {2}",
                        buf[curIndex] * 10 + buf[curIndex+1],
                        buf[curIndex+2] * 10 + buf[curIndex+3],
                        buf[curIndex+5] * 100 + buf[curIndex+6] *10 + buf[curIndex+7]);
                      break;
                case (byte)KyoceraCmd.ReadNumberOfDriveTimes:
                      Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 7);
                    parserInfo = string.Format("times ={0}",
                         ((buf[curIndex]&0x7f) <<48)+(buf[curIndex+1] <<40) + (buf[curIndex+2] <<32)
                         + (buf[curIndex+3]<<24)+(buf[curIndex+4] <<16) + (buf[curIndex+5] <<8) + buf[curIndex+6] );
                    break;
                case (byte)KyoceraCmd.ReadGroupAWaveform:
                case (byte)KyoceraCmd.ReadGroupBWaveform:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, PubFunc.Kyocera_WaveformLength);
                    parserInfo = string.Join("-", buf.Skip(bufIndex + 2).Take(PubFunc.Kyocera_WaveformLength));// ASCIIEncoding.ASCII.GetString(buf, bufIndex + 2, 224);
                    break;
                case (byte)KyoceraCmd.ReadAdjustmentVoltage:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 4);
                    parserInfo = string.Format("B AdjustmentVoltage={0};A AdjustmentVoltage ={1}",
                    (decimal)((sbyte)buf[curIndex] / 10.0f),
                        (decimal)((sbyte)buf[curIndex + 1] / 10.0f)
                        );

                    break;
                case (byte)KyoceraCmd.ReadVoltageSetting:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 2);
                    parserInfo = string.Format("B VoltageSetting={0};A VoltageSetting ={1}",
                        (float)buf[curIndex]/10.0f +24.0f ,
                        (float)buf[curIndex+1] / 10.0f +24.0f
                        );
                   break;

                case (byte)KyoceraCmd.ReadStopFunctionStatus:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 2);
                    parserInfo = string.Format(" Stop BIT ={0} ",
                        buf[curIndex] == 0x1 ? true:false);
                   break;

                case (byte)KyoceraCmd.ReadTempLimit:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 4);
                    int upper = ConvertDataToTemp(buf[curIndex] + (buf[curIndex+1]<<8));
                    int low = ConvertDataToTemp(buf[curIndex+2] + (buf[curIndex+3]<<8));
                    parserInfo = string.Format(" Upper temp  ={0},Low temp  ={1} ",
                        upper, low);
                   break;
                case (byte)KyoceraCmd.ReadHeaterTemp:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 6);
                    int curtemp = ConvertDataToTemp((buf[curIndex]<<8) + (buf[curIndex+1]));
                    parserInfo = string.Format(" temp  ={0}", curtemp);
                    break;


                //case (byte)KyoceraCmd.WriteGroupAWaveform:
                //    break;
                //case (byte)KyoceraCmd.WriteGroupBWaveform:
                //    break;
                //case (byte)KyoceraCmd.WriteAdjustmentVoltage:
                //    break;
                //case (byte)KyoceraCmd.WriteVoltageSetting:
                //    break;
                //case (byte)KyoceraCmd.WriteTempLimit:
                //    break;
                //case (byte)KyoceraCmd.WriteStopFunctionStatus:
                //    break;

            }
        }
        void PaserEp6BufForKJ4A_TA06(byte[] buf, int bufIndex, out string Asii, out string parserInfo)
        {
            Asii = "";
            parserInfo = "";
            int curIndex = bufIndex + 2;

            switch (buf[bufIndex + 1])
            {
                case (byte)KyoceraCmdForKJ4A_TA06.ReadSerialNumber:
                    //Len = 8;
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 8);
                    parserInfo = string.Format("year={0};month={1}; number= {2}",
                        buf[curIndex] * 10 + buf[curIndex + 1],
                        buf[curIndex + 2] * 10 + buf[curIndex + 3],
                        buf[curIndex + 5] * 100 + buf[curIndex + 6] * 10 + buf[curIndex + 7]);
                    break;
                case (byte)KyoceraCmdForKJ4A_TA06.ReadNumberOfDriveTimes:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 7);
                    parserInfo = string.Format("times ={0}",
                         ((buf[curIndex] & 0x7f) << 48) + (buf[curIndex + 1] << 40) + (buf[curIndex + 2] << 32)
                         + (buf[curIndex + 3] << 24) + (buf[curIndex + 4] << 16) + (buf[curIndex + 5] << 8) + buf[curIndex + 6]);
                    break;
                case (byte)KyoceraCmdForKJ4A_TA06.ReadWaveform:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, PubFunc.Kyocera_WaveformLength);
                    parserInfo = string.Join("-", buf.Skip(bufIndex + 2).Take(PubFunc.Kyocera_WaveformLength));// ASCIIEncoding.ASCII.GetString(buf, bufIndex + 2, 224);
                    break;
                case (byte)KyoceraCmdForKJ4A_TA06.ReadAdjustmentVoltage:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 4);
                    if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl||
                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl||
                        m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl)
                    {
                        parserInfo = string.Format("AdjustmentVoltage={0};",
                            (float)((sbyte)buf[curIndex]) / 10.0f                            );
                    }
                    else
                    {
                        parserInfo = string.Format("B AdjustmentVoltage={0};A AdjustmentVoltage ={1}",
                            (float)((sbyte)buf[curIndex]) / 10.0f,
                            (float)((sbyte)buf[curIndex + 1]) / 10.0f
                            );
                    }
                    break;
                case (byte)KyoceraCmdForKJ4A_TA06.ReadDLYSEL:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 2);
                    parserInfo = string.Format("DLYSEL1={0};DLYSEL2={1};DLYSEL3={2};DLYSEL4={3};",
                        (float)buf[curIndex],
                        (float)buf[curIndex + 1],
                        (float)buf[curIndex + 2],
                        (float)buf[curIndex + 3]
                        );
                    break;
                case (byte)KyoceraCmdForKJ4A_TA06.ReadStopFunctionStatus:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 2);
                    parserInfo = string.Format(" Stop BIT ={0} ",
                        buf[curIndex] == 0x1 ? true : false);
                    break;
            }
        }
        void PaserEp6BufForKJ4A_RH06(byte[] buf, int bufIndex, out string Asii, out string parserInfo)
        {
            Asii = "";
            parserInfo = "";
            int curIndex = bufIndex + 2;

            switch (buf[bufIndex + 1])
            {
                case (byte)KyoceraCmdForKJ4A_RH06.ReadSerialNumber:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 8);
                    parserInfo = string.Format("year={0};month={1}; number= {2}",
                        buf[curIndex] * 10 + buf[curIndex + 1],
                        buf[curIndex + 2] * 10 + buf[curIndex + 3],
                        buf[curIndex + 5] * 100 + buf[curIndex + 6] * 10 + buf[curIndex + 7]);
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadNumberOfDriveTimes:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 7);
                    parserInfo = string.Format("times ={0}",
                         ((buf[curIndex] & 0x7f) << 48) + (buf[curIndex + 1] << 40) + (buf[curIndex + 2] << 32)
                         + (buf[curIndex + 3] << 24) + (buf[curIndex + 4] << 16) + (buf[curIndex + 5] << 8) + buf[curIndex + 6]);
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadTotalAdjustmentVoltage:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 4);
                    parserInfo = string.Format("Total Adjustment Voltage={0};",
                            (float)((sbyte)buf[curIndex]) / 10.0f);
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadVoltageSettingByUnit:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 6);
                    parserInfo = string.Format("Voltage for Unit1={0};Voltage for Unit2={1};Voltage for Unit3={2};Voltage for Unit4={3};",
                        (float)((sbyte)buf[curIndex]) / 10.0f, 
                        (float)((sbyte)buf[curIndex + 1]) / 10.0f, 
                        (float)((sbyte)buf[curIndex + 2]) / 10.0f,
                        (float)((sbyte)buf[curIndex + 3]) / 10.0f);
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadTempLimitsForHeaterSetting:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 4);
                    int upper = ConvertDataToTemp(buf[curIndex] + (buf[curIndex + 1] << 8));
                    int low = ConvertDataToTemp(buf[curIndex + 2] + (buf[curIndex + 3] << 8));
                    parserInfo = string.Format(" Upper temp  ={0},Low temp  ={1} ",
                        upper, low);
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadHeaterTemp:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 6);
                    parserInfo = string.Format(" Heater temp  ={0} ",
                        ConvertDataToTemp((buf[curIndex] << 8) + (buf[curIndex + 1])));
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingWaveform:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, PubFunc.KJ4A_RH06_WaveformLength);
                    parserInfo = string.Join("-", buf.Skip(bufIndex + 2).Take(PubFunc.KJ4A_RH06_WaveformLength));
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadDelaySettingForU13:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 128);
                    parserInfo = string.Join("-", buf.Skip(bufIndex + 2).Take(128));
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadDelaySettingForU24:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 128);
                    parserInfo = string.Join("-", buf.Skip(bufIndex + 2).Take(128));
                    break;
                case (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingStopFunctionStatus:
                    Asii = ASCIIEncoding.ASCII.GetString(buf, curIndex, 2);
                    parserInfo = string.Format(" Stop BIT ={0} ",
                        buf[curIndex] == 0x1 ? true : false);
                    break;
            }
        }
        public bool WriteAdjustmentVoltage(ushort hbid, byte HeadIndex, float BVol, float AVol)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmd.WriteAdjustmentVoltage;
            buf[8] = (byte)(sbyte)(BVol * 10);
            buf[9] = (byte)(sbyte)(AVol * 10);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;

            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff||ep6Data[7] != 0x80)
                {
                    return false;
                }
             }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteAdjustmentVoltageForKJ4A_TA06(ushort hbid, byte HeadIndex, float AVol)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmdForKJ4A_TA06.WriteAdjustmentVoltage;
            buf[8] = (byte)(sbyte)(AVol * 10);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;

            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteVoltageSetting(ushort hbid, byte HeadIndex, float BVol, float AVol)
        {
            if (BVol > 28 || BVol < 24)
                return false;
            if (AVol > 28 || AVol < 24)
                return false;

            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmd.WriteVoltageSetting;
            buf[8] = (byte)((BVol - 24)*10) ;
            buf[9] = (byte)((AVol - 24)*10);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteTempLimit(ushort hbid, byte HeadIndex, float upperTemp, float lowTemp)
        {
            int upper = ConvertTempToData((int)upperTemp);
            int low = ConvertTempToData((int)lowTemp);
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmd.WriteTempLimit;
            buf[8] = (byte)(upper&0xff);
            buf[9] = (byte)((upper>>8)&0xff);
            buf[10] = (byte)(low&0xff);
            buf[11] = (byte)((low>>8)&0xff);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteStopFunctionStatus(ushort hbid, byte HeadIndex, byte Stop)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x04;
            buf[7] = (byte)KyoceraCmd.WriteStopFunctionStatus;
            buf[8] = Stop; //BIT 0: 1 for STOP

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteDLYSELForKJ4A_TA06(ushort hbid, byte HeadIndex, byte[] dlysels)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmdForKJ4A_TA06.WriteDLYSEL;
            buf[8] = dlysels[0]; 
            buf[9] = dlysels[1]; 
            buf[10] = dlysels[2];
            buf[11] = dlysels[3];

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteAdjustmentVoltageForKJ4A_RH06(ushort hbid, byte HeadIndex, float AVol)
        {
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmdForKJ4A_RH06.WriteTotalAdjustmentVoltage;
            buf[8] = (byte)(sbyte)(AVol * 10);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;

            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteVoltageSettingForKJ4A_RH06(ushort hbid, byte HeadIndex, float Vol1, float Vol2, float Vol3, float Vol4)
        {
            if (Vol1 > 28 || Vol1 < 24)
                return false;
            if (Vol2 > 28 || Vol2 < 24)
                return false;
            if (Vol3 > 28 || Vol3 < 24)
                return false;
            if (Vol4 > 28 || Vol4 < 24)
                return false;

            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[14];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x08;
            buf[7] = (byte)KyoceraCmdForKJ4A_RH06.WriteVoltageSettingByUnit;
            buf[8] = (byte)((Vol1 - 24) * 10);
            buf[9] = (byte)((Vol2 - 24) * 10);
            buf[10] = (byte)((Vol3 - 24) * 10);
            buf[11] = (byte)((Vol4 - 24) * 10);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteTempLimitForKJ4A_RH06(ushort hbid, byte HeadIndex, float upperTemp, float lowTemp)
        {
            int upper = ConvertTempToData((int)upperTemp);
            int low = ConvertTempToData((int)lowTemp);
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[12];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x06;
            buf[7] = (byte)KyoceraCmdForKJ4A_RH06.WriteTempLimitsForHeaterSetting;
            buf[8] = (byte)(upper & 0xff);
            buf[9] = (byte)((upper >> 8) & 0xff);
            buf[10] = (byte)(low & 0xff);
            buf[11] = (byte)((low >> 8) & 0xff);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteDelaySettingForU13ForKJ4A_RH06(ushort hbid, byte HeadIndex)
        {
            byte [] msg = PubFunc.GetDelaySettingKyocera_KJ4A_RH06(writeDelaySettingPath);
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[136];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x82;
            buf[7] = (byte)KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU13;
            Array.Copy(msg, 0, buf, 8, msg.Length);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public bool WriteDelaySettingForU24ForKJ4A_RH06(ushort hbid, byte HeadIndex)
        {
            byte[] msg = PubFunc.GetDelaySettingKyocera_KJ4A_RH06(writeDelaySettingPath);
            byte cmd = 0x80;
            ushort value = (ushort)hbid;
            ushort index = 0;
            // heater temp
            byte[] buf = new byte[136];
            buf[0] = 0xc4;
            buf[1] = 0xb8;
            buf[2] = HeadIndex;
            buf[3] = 0xfe;
            buf[4] = 0xfe;
            buf[5] = 0xfe;
            buf[6] = 0x82;
            buf[7] = (byte) KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU24;
            Array.Copy(msg, 0, buf, 8, msg.Length);

            m_WaitingEp6PipeCmd = buf[7];
            bReadAllEvent = false;
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                MessageBox.Show(string.Format("Send to read {0} command failed! ", buf[7]), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (ep6Data[1] == 0xff || ep6Data[7] != 0x80)
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ep6 data timeout!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        void writeVolWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void writeVolWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool) e.Result)
            {
                MessageBox.Show("Write parameters successfully.");
            }
            else
            {
                MessageBox.Show("Write parameters fail.");
            }
        }

        private void writeVolWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ushort hbid = (ushort) _hbIndexVt;
            byte hdindex = (byte) numHdIndexVT.Value;
            bool ret = false;
            switch (m_printerProperty.ePrinterHead)
            {
                case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                    {
                        switch ((byte)e.Argument)
                        {
                            case (byte)KyoceraCmdForKJ4A_RH06.WriteTotalAdjustmentVoltage:
                                {
                                    float volA = (float)numericUpDownAdjustVolForKJ4A_RH06.Value;
                                    ret = WriteAdjustmentVoltageForKJ4A_RH06(hbid, hdindex, volA);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_RH06.WriteVoltageSettingByUnit:
                                {
                                    float vol1 = (float)numericUpDownVolSet1ForKJ4A_RH06.Value;
                                    float vol2 = (float)numericUpDownVolSet2ForKJ4A_RH06.Value;
                                    float vol3 = (float)numericUpDownVolSet3ForKJ4A_RH06.Value;
                                    float vol4 = (float)numericUpDownVolSet4ForKJ4A_RH06.Value;
                                    ret = WriteVoltageSettingForKJ4A_RH06(hbid, hdindex, vol1, vol2, vol3, vol4);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_RH06.WriteTempLimitsForHeaterSetting:
                                {
                                    float limitU = (float)numericUpDownTempLimitUForKJ4A_RH06.Value;
                                    float limitL = (float)numericUpDownTempLimitLForKJ4A_RH06.Value;
                                    ret = WriteTempLimitForKJ4A_RH06(hbid, hdindex, limitU, limitL);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU13:
                                {
                                    ret = WriteDelaySettingForU13ForKJ4A_RH06(hbid, hdindex);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU24:
                                {
                                    ret = WriteDelaySettingForU24ForKJ4A_RH06(hbid, hdindex);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        switch ((byte)e.Argument)
                        {
                            case (byte)KyoceraCmd.WriteAdjustmentVoltage:
                                {
                                    float volA = (float)numAdjustVolA.Value;
                                    float volB = (float)numAdjustVolB.Value;
                                    ret = WriteAdjustmentVoltage(hbid, hdindex, volB, volA);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_TA06.WriteAdjustmentVoltage:
                                {
                                    float volA = (float)numericUpDown2.Value;
                                    ret = WriteAdjustmentVoltageForKJ4A_TA06(hbid, hdindex, volA);
                                    break;
                                }
                            case (byte)KyoceraCmd.WriteVoltageSetting:
                                {
                                    float volA = (float)numBaseVolA.Value;
                                    float volB = (float)numBaseVolB.Value;
                                    ret = WriteVoltageSetting(hbid, hdindex, volB, volA);
                                    break;
                                }
                            case (byte)KyoceraCmd.WriteTempLimit:
                                {
                                    float limitA = (float)numTempLimitA.Value;
                                    float limitB = (float)numTempLimitB.Value;
                                    ret = WriteTempLimit(hbid, hdindex, limitB, limitA);
                                    break;
                                }
                            case (byte)KyoceraCmdForKJ4A_TA06.WriteDLYSEL:
                                {
                                    ret = WriteDLYSELForKJ4A_TA06(hbid, hdindex, dlysels);
                                    break;
                                }
                        }
                        break;
                    }
            }
            e.Result = ret;
        }

        private void readVolWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage != 0)
                {
                    switch (m_printerProperty.ePrinterHead)
                    {
                        case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                        case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                            {
                                switch ((KyoceraCmd)m_WaitingEp6PipeCmd)
                                {
                                    case KyoceraCmd.ReadAdjustmentVoltage:
                                        {
                                            numAdjustVolB.Value = (decimal)((sbyte)ep6Data[7] / 10f);
                                            numAdjustVolA.Value = (decimal)((sbyte)ep6Data[8] / 10f);
                                            break;
                                        }
                                    case KyoceraCmd.ReadVoltageSetting:
                                        {
                                            numBaseVolB.Value = (decimal)(ep6Data[7] / 10f + 24);
                                            numBaseVolA.Value = (decimal)(ep6Data[8] / 10f + 24);
                                            break;
                                        }
                                    case KyoceraCmd.ReadTempLimit:
                                        {
                                            int upper = ConvertDataToTemp(ep6Data[7] + (ep6Data[8] << 8));
                                            int low = ConvertDataToTemp(ep6Data[9] + (ep6Data[10] << 8));
                                            numTempLimitB.Value = (decimal)(upper);
                                            numTempLimitA.Value = (decimal)(low);
                                            break;
                                        }
                                }
                                break;
                            }
                        case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                        case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                            {
                                switch ((KyoceraCmdForKJ4A_TA06)m_WaitingEp6PipeCmd)
                                {
                                    case KyoceraCmdForKJ4A_TA06.ReadAdjustmentVoltage:
                                        {
                                            numericUpDown2.Value = (decimal)((sbyte)ep6Data[7] / 10f);
                                            break;
                                        }
                                    case KyoceraCmdForKJ4A_TA06.ReadDLYSEL:
                                        {
                                            for (int j = 0; j < comboBoxDlysel1.Items.Count; j++)
                                            {
                                                if (ep6Data[7].ToString() == comboBoxDlysel1.Items[j].ToString())
                                                {
                                                    comboBoxDlysel1.SelectedIndex = j;
                                                }
                                            }
                                            for (int j = 0; j < comboBoxDlysel2.Items.Count; j++)
                                            {
                                                if (ep6Data[8].ToString() == comboBoxDlysel2.Items[j].ToString())
                                                {
                                                    comboBoxDlysel2.SelectedIndex = j;
                                                }
                                            }
                                            for (int j = 0; j < comboBoxDlysel3.Items.Count; j++)
                                            {
                                                if (ep6Data[9].ToString() == comboBoxDlysel3.Items[j].ToString())
                                                {
                                                    comboBoxDlysel3.SelectedIndex = j;
                                                }
                                            }
                                            for (int j = 0; j < comboBoxDlysel4.Items.Count; j++)
                                            {
                                                if (ep6Data[10].ToString() == comboBoxDlysel4.Items[j].ToString())
                                                {
                                                    comboBoxDlysel4.SelectedIndex = j;
                                                }
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                            {
                                switch ((KyoceraCmdForKJ4A_RH06)m_WaitingEp6PipeCmd)
                                {
                                    case KyoceraCmdForKJ4A_RH06.ReadTotalAdjustmentVoltage:
                                        {
                                            numericUpDownAdjustVolForKJ4A_RH06.Value = (decimal)((sbyte)ep6Data[7] / 10f);
                                            break;
                                        }
                                    case KyoceraCmdForKJ4A_RH06.ReadVoltageSettingByUnit:
                                        {
                                            numericUpDownVolSet1ForKJ4A_RH06.Value = (decimal)((sbyte)ep6Data[7] / 10f + 24);
                                            numericUpDownVolSet2ForKJ4A_RH06.Value = (decimal)((sbyte)ep6Data[8] / 10f + 24);
                                            numericUpDownVolSet3ForKJ4A_RH06.Value = (decimal)((sbyte)ep6Data[9] / 10f + 24);
                                            numericUpDownVolSet4ForKJ4A_RH06.Value = (decimal)((sbyte)ep6Data[10] / 10f + 24);
                                            break;
                                        }
                                    case KyoceraCmdForKJ4A_RH06.ReadTempLimitsForHeaterSetting:
                                        {
                                            int upper = ConvertDataToTemp(ep6Data[7] + (ep6Data[8] << 8));
                                            int low = ConvertDataToTemp(ep6Data[9] + (ep6Data[10] << 8));
                                            numericUpDownTempLimitUForKJ4A_RH06.Value = (decimal)(upper);
                                            numericUpDownTempLimitLForKJ4A_RH06.Value = (decimal)(low);
                                            break;
                                        }
                                }
                            break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show(e.UserState.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void readwriteWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ushort value = (ushort) _hbIndexVt;
            byte headid = (byte) numHdIndexVT.Value;
            string msg = string.Empty;
            bool ret = SendAndReciveEp6Data(value, (byte) headid, (byte) e.Argument, ref msg);
            readVolWorker.ReportProgress(ret ? 1 : 0, msg);
        }

        void readWfWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage ==1)
            {
                string stracii, strParseinfo;
                switch (m_printerProperty.ePrinterHead)
                {
                    case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                    case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                    case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                        {
                            PaserEp6BufForKJ4A_TA06(ep6Data, 5, out stracii, out strParseinfo);
                            break;
                        }
                    case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                    case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                        {
                            PaserEp6BufForKJ4A_RH06(ep6Data, 5, out stracii, out strParseinfo);
                            break;
                        }
                    case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                    case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                    default:
                    {
                        PaserEp6Buf(ep6Data, 5, out stracii, out strParseinfo);
                        break;
                    }
                }

                richTextBoxText.AppendText(strParseinfo + Environment.NewLine);
                richTextBoxAscii.AppendText(e.UserState.ToString() + Environment.NewLine);
            }
            else
            {
                richTextBoxText.AppendText(e.UserState.ToString() + Environment.NewLine);
                richTextBoxAscii.AppendText(e.UserState.ToString() + Environment.NewLine);
            }
        }
        List<byte[]> readWaveformDatas = new List<byte[]>();
        List<byte[]> readDelaySettingForU13Datas = new List<byte[]>();
        List<byte[]> readDelaySettingForU24Datas = new List<byte[]>();
        void readWfWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                try
                {
                    switch (m_printerProperty.ePrinterHead)
                    {
                        case PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c:
                        case PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c:
                            {
                                ReadAllForKyocera_KJ4B();
                                break;
                            }
                        case PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl:
                        case PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl:
                            {
                                ReadAllForKJ4A_TA06();
                                break;
                            }
                        case PrinterHeadEnum.Kyocera_KJ4A_RH06:
                        case PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl:
                            {
                                ReadAllForKJ4A_RH06();
                                break;
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

        private void ReadAllForKyocera_KJ4B()
        {
            readWaveformDatas = new List<byte[]>();
            ushort value = (ushort) _hbindexReadall;
            byte headid = (byte) numHeadIndex.Value;
            string msg = string.Format("*************************{0}******************************",
                "serial number");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            bool ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadSerialNumber, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "number of drive times");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadNumberOfDriveTimes, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "CH group A Reading Driving waveform");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadGroupAWaveform, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
            }
            msg = string.Format("*************************{0}******************************",
                "CH group B Reading Driving waveform");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadGroupBWaveform, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
            }

            msg = string.Format("*************************{0}******************************",
                "adjustment voltage");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadAdjustmentVoltage, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "drive voltage setting");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadVoltageSetting, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "driving stop function status");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadStopFunctionStatus, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "temp. limits for heater setting");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadTempLimit, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "heater temp");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte) headid, (byte) KyoceraCmd.ReadHeaterTemp, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
        }

        private void ReadAllForKJ4A_TA06()
        {
            readWaveformDatas = new List<byte[]>();
            ushort value = (ushort)_hbindexReadall;
            byte headid = (byte)numHeadIndex.Value;
            string msg = string.Format("*************************{0}******************************",
                "serial number");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            bool ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadSerialNumber, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "number of drive times");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadNumberOfDriveTimes, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "Reading Driving waveform");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadWaveform, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
            }

            msg = string.Format("*************************{0}******************************",
                "adjustment voltage");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadAdjustmentVoltage, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "DLYSEL");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadDLYSEL, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "driving stop function status");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_TA06.ReadStopFunctionStatus, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
        }

        private void ReadAllForKJ4A_RH06()
        {
            readWaveformDatas = new List<byte[]>();
            readDelaySettingForU13Datas = new List<byte[]>();
            readDelaySettingForU24Datas = new List<byte[]>();
            ushort value = (ushort)_hbindexReadall;
            byte headid = (byte)numHeadIndex.Value;
            string msg = string.Format("*************************{0}******************************",
                "serial number");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            bool ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadSerialNumber, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "number of drive times");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadNumberOfDriveTimes, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "total adjustment voltage");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadTotalAdjustmentVoltage, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "voltage setting by unit");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadVoltageSettingByUnit, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "temp limits for heater setting");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadTempLimitsForHeaterSetting, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "heater temp");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadHeaterTemp, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);

            msg = string.Format("*************************{0}******************************",
                "driving waveform");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingWaveform, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readWaveformDatas.Add(ep6Data);
            }

            msg = string.Format("*************************{0}******************************",
                "delay setting for U1/3");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadDelaySettingForU13, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readDelaySettingForU13Datas.Add(ep6Data);
            }


            msg = string.Format("*************************{0}******************************",
                "delay setting for U2/4");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadDelaySettingForU24, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
            if (ret)
            {
                readDelaySettingForU24Datas.Add(ep6Data);
            }


            msg = string.Format("*************************{0}******************************",
                "driving stop function status");
            readWfWorker.ReportProgress(0, msg);
            msg = string.Empty;
            ret = SendAndReciveEp6Data(value, (byte)headid, (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingStopFunctionStatus, ref msg);
            readWfWorker.ReportProgress(ret ? 1 : 0, msg);
        }

        private void readWfWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonSaveAs.Enabled = button1.Enabled = readWaveformDatas.Count > 0;
            if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06)
            {
                buttonDelayU13.Enabled = buttonDelayU24.Enabled = readWaveformDatas.Count > 0;
            }
        }

        void readallWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                bool bOk = CheckDownloadedData();
                e.Result = bOk;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void readallWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                MessageBox.Show("Download and check Success.");
            }
            else
            {
                MessageBox.Show("Check failure.");
                //MessageBox.Show("校验失败.");
            }
            m_buttonUpdata.Enabled = true;
            downloadEvent.Set();
        }

        private bool binit = false;
        private HEAD_BOARD_TYPE headBoardType;
        private void Xaar501Waveform_Load(object sender, EventArgs e)
        {
            headBoardType = (HEAD_BOARD_TYPE) CoreInterface.get_HeadBoardType(true);
            this.Text +=string.Format(" [{0}] [{1}]" ,headBoardType,m_printerProperty.ePrinterHead);
            JetStatusEnum status = CoreInterface.GetBoardStatus();
            OnPrinterStatusChanged(status);
            if (status != JetStatusEnum.PowerOff)
            {
                if (CoreInterface.GetSPrinterProperty(ref m_printerProperty) == 0)
                {
                    Debug.Assert(false);
                }
            }

            USER_SET_INFORMATION factoryData = new USER_SET_INFORMATION();
           int ret = CoreInterface.GetUserSetInfo(ref factoryData);
            if (ret != 0)
            {
                numHBIndexW.Items.Clear();
                numHeadBoardindex.Items.Clear();
                comboBoxHbIndexVT.Items.Clear();
                for (int i = 0; i < factoryData.HeadBoardNum; i++)
                {
                    numHBIndexW.Items.Add(string.Format("#{0}", i + 1));
                    numHeadBoardindex.Items.Add(string.Format("#{0}", i + 1));
                    comboBoxHbIndexVT.Items.Add(string.Format("#{0}", i + 1));
                }
                numHBIndexW.SelectedIndex = numHeadBoardindex.SelectedIndex = comboBoxHbIndexVT.SelectedIndex = 0;
            }
            CreatUI();

            /*
（使用默认波形选项，放到下载京瓷的waveform界面里）
* request=0x6F, index 3, value = 0, 不使用， value = 1使用  支持set get, 
* 设置时提示用户重启后生效，下载波形前需要判断value是否为0， 
* 非0提示用户取消 “使用默认波形”，确定取消后才能下载波形
*/
            byte cmd = 0x6f;
            ushort value = 0;
            ushort index = (ushort)3;
            byte[] buf = new byte[3];
            uint len = (uint)buf.Length;
            if (CoreInterface.GetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {

                //MessageBox.Show("获取是否使用默认波形设置失败!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("Failed to get whether to use default waveform Settings!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                binit = true;
                checkBoxUseDefaultWF.Checked = buf[2] == 1;
                binit = false;
            }
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            CoreInterface.SystemClose();
        }

        private void CreatUI()
        {
            //if (1 == CoreInterface.GetSPrinterProperty(ref m_printerProperty))//非Ready状态无法读到正确的值
            {
                progressBarStatu.Minimum = 0;
                progressBarStatu.Visible = false;
            }
            groupBoxWriteAdjustmentVoltageForKJ4A_TA06.Visible =
            groupBoxWriteDlysel.Visible = 
            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl||
            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl||
            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl;

            groupBoxWriteAdjustmentVoltage.Visible = panel4.Visible =
             groupBoxWriteTempLimit.Visible = groupBoxWriteVoltageSetting.Visible =
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_0300_5pl_1h2c
                || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_0300_5pl_1h2c;


            buttonDelayU13.Visible =
            buttonDelayU24.Visible =
            groupBoxWriteAdjustmentVoltageForKJ4A_RH06.Visible = 
            groupBoxWriteDelaySettingForKJ4A_RH06.Visible =
            groupBoxWriteVoltageSettingForKJ4A_RH06.Visible =
            groupBoxWriteTempLimitsForKJ4A_RH06.Visible =
            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 ||
            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl;

            
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
            m_allParam = new AllParam();
            m_allParam.LoadFromXml(null, true);
            m_printerProperty = m_allParam.PrinterProperty;

            return true;
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
        AutoResetEvent downloadEvent = new AutoResetEvent(false);
        private void m_buttonUpdata_Click(object sender, EventArgs e)
        {
            string path = m_textBoxPath.Text;
            if (!File.Exists(path)
                || numHBIndexW.SelectedIndex < 0
                || headList.All(head => { return !head.Checked; })
                || abList.All(cbk => { return !cbk.Checked; })
                )
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }

            /*
 （使用默认波形选项，放到下载京瓷的waveform界面里）
 * request=0x6F, index 3, value = 0, 不使用， value = 1使用  支持set get, 
 * 设置时提示用户重启后生效，下载波形前需要判断value是否为0， 
 * 非0提示用户取消 “使用默认波形”，确定取消后才能下载波形
 */
            //byte cmd = 0x6f;
            //ushort value = 0;
            //ushort index = (ushort)3;
            //byte[] buf = new byte[3];
            //uint len = (uint)buf.Length;
            //if (CoreInterface.GetEpsonEP0Cmd(cmd, buf, ref len, value, index) != 0)
            //{
            //    if (buf[2] == 1)
            //    {
            //        MessageBox.Show("当前为使用默认波形模式,请去除此设置后重试!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            StartCount();
            try
            {
                {
                    if (File.Exists(path))
                    {
                        m_buttonUpdata.Enabled = false;
                        int hbindex = numHBIndexW.SelectedIndex;
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
                                        if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl||
                                            m_printerProperty.ePrinterHead==PrinterHeadEnum.Kyocera_KJ4A_RH06||
                                            m_printerProperty.ePrinterHead==PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl||
                                            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl||
                                            m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl)
                                            saveID = hbindex * 4 + i;
                                        m_nHeadId = (int)saveID;
                                        {
                                            UpdateCoreBoard(path, type, (byte)saveID);
                                            //progressBarStatu.Maximum = ++nCount;
                                        }
                                        downloadEvent.WaitOne();
                                        downloadEvent.Reset();
                                    }
                                }
                            }
                        }));
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
                    m_textBoxPath.Text = fileDialog.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private BackgroundWorker readallWorker;
        private BackgroundWorker readWfWorker;
        private byte[] downloadedData;
        private void UpdateCoreBoard(string m_UpdaterFileName, ColorEnum color, byte saveId)
        {
            string info = "";
            //添加喷头类型,颜色,saveId,waveformID
            HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            byte[] wfdata;
            if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl)
            {
                wfdata = PubFunc.GetAllDataFromFileKyocera_KJ4A_RH06_Des(m_UpdaterFileName, ref info);
                //wfdata = PubFunc.GetAllDataFromFileKyocera_KJ4A_RH06(m_UpdaterFileName);
            }
            else
            {
                wfdata = PubFunc.GetAllDataFromFileKyocera_Des(m_UpdaterFileName, ref info);
                //wfdata = PubFunc.GetAllDataFromFileKyocera(m_UpdaterFileName);
            }
            if (null == wfdata)
            {
                Invoke(new Action(() => m_buttonUpdata.Enabled = true));
                downloadEvent.Set();
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
            int nLen = 3 + wfdata.Length;
            byte[] val = new byte[nLen];
            val[0] = (byte)headBoardType;
            val[1] = (byte)color;
            val[2] = saveId;
            Array.Copy(wfdata, 0, val, 3, wfdata.Length);
            int ret = CoreInterface.Down382WaveForm(val, nLen, 0x01);
            downloadedData = wfdata;
        }

        private bool SendAndReciveEp6Data(ushort hbid, byte wfId, byte subCmd,ref string msg)
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

            m_WaitingEp6PipeCmd = buf[7];
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
                    bOk =true;
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
        private byte[] ep6Data;
        private volatile bool bReadAllEvent = false;
        public void OnEp6DataChanged(int ep6Cmd, int index, byte[] buf)
        {
            ep6Data = new byte[buf.Length];
            Buffer.BlockCopy(buf, 0, ep6Data, 0, buf.Length);
            //if (buf[4] == m_WaitingEp6PipeCmd) //FE FE FE LEN CMD
                  bReadAllEvent = true;
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
            LogWriter.WriteLog(new string[] { string.Format("ProceedKernelMessage kParam={0};lParam={1}", kParam, lParam.ToInt64()) }, true);

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
                                    //string info = ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.UpdateSuccess);
                                    //MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //CheckDownloadedData();
                                    if (!readallWorker.IsBusy)
                                    {
                                        readallWorker.RunWorkerAsync();
                                    }
                                    else
                                    {
                                        m_buttonUpdata.Enabled = true;
                                        downloadEvent.Set();
                                        MessageBox.Show("Background Worker Is Busy!");
                                    }
                                }
                                else
                                {
                                    string info = ResString.GetEnumDisplayName(typeof(UIError), UIError.UpdateFail);
                                    MessageBox.Show(info, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    m_buttonUpdata.Enabled = true;
                                    downloadEvent.Set();
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
                            LogWriter.WriteLog(new string[] { string.Format("GetEp6PipeData buflen={0};", buflen) }, true);
                            if (CoreInterface.GetEp6PipeData(ep6Cmd, index, bufep6, ref buflen) != 0)
                            {
                                OnEp6DataChanged(ep6Cmd, index, bufep6);
                            }
                            else
                            {
                                MessageBox.Show("GetEp6PipeData fail!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("GetEp6PipeData  buflen fail!");
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 读回下载的数据做校验
        /// </summary>
        private bool CheckDownloadedData()
        {
            // 读回下载的数据做校验
            bReadAllEvent = false;
            string path = Path.Combine(Application.StartupPath, "waveformlog.txt");
            StreamWriter sw = new StreamWriter(path, false);
            ushort value = (ushort) 0;
            sw.WriteLine(string.Format("*************************{0}******************************", "write waveform"));
            sw.WriteLine(BitConverter.ToString(downloadedData));

            byte headid = (byte)((m_nHeadId % 8)/2);
            ushort hbid = (ushort)(m_nHeadId / 8);
            byte subcmd = (byte) (m_nHeadId%2 == 0 ? 0x1a : 0x1b);
            if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl||
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl ||
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl ||
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 || 
                m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl)
            {
                headid = (byte)((m_nHeadId % 4));
                hbid = (ushort)(m_nHeadId / 4);
                if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_TA06_6pl||
                    m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_QA06_5pl||
                    m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_AA06_3pl)
                    subcmd = (byte)KyoceraCmdForKJ4A_TA06.ReadWaveform;
                if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06 || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl)
                    subcmd = (byte)KyoceraCmdForKJ4A_RH06.ReadDrivingWaveform;
            }
            string msg = "read waveform*****CH group A Reading Driving waveform";
            bool bRet = SendAndReciveEp6Data(hbid, headid, subcmd,ref msg);
            sw.Flush();
            sw.Close();
            if (bRet)
            {
                // 验证写入的和读出的数据是否一致
                int bytes = GetDrivingWaveformBytes();
                bool bNoMathch = false;
                for (int i = 0; i < bytes; i++)
                {
                    if (ep6Data.Length <= 7 + i)
                    {
                        //MessageBox.Show("读到的数据长度不足!");
                        bNoMathch = true;
                        break;
                    }
                    if (downloadedData.Length <= 18 + i)
                    {
                        bNoMathch = true;
                        //MessageBox.Show("写入的数据长度不足!");
                        break;
                    }
                    if (ep6Data[7 + i] != downloadedData[18 + i])
                    {
                        bNoMathch = true;
                        //MessageBox.Show("写入与读出的数据不一致!");
                        break;
                    }
                }
                return (!bNoMathch);
                //MessageBox.Show("下载并校验成功.");
            }
            return false;
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
            bEnable = true;
            //for (int i = 0; i < m_printerProperty.nColorNum; i++)
            //{
            //    if (null != m_Ctrls.FileButtons && null != m_Ctrls.DownButtons)
            //    {
            //        m_Ctrls.FileButtons[i].Enabled = bEnable;
            //        m_Ctrls.DownButtons[i].Enabled = bEnable;
            //        m_Ctrls.IdNumericUpDowns[i].Enabled = bEnable;
            //        m_Ctrls.TextBoxs[i].Enabled = bEnable;
            //        m_buttonSyn.Enabled = bEnable;
            //        m_buttonApply.Enabled = bEnable;
            //    }
            //}
        }

        private void UpdateButtonStates(JetStatusEnum status)
        {
            if (status == JetStatusEnum.PowerOff)
            {
                bFristPowerOnAfterPowerOff = true;
                SetCtrlIsEnable(false);
            }
            buttonPrintPrt.Enabled = status == JetStatusEnum.Ready;
        }

        private void Xaar501Waveform_SizeChanged(object sender, EventArgs e)
        {
            this.progressBarStatu.Location = new Point(0, m_StatusBarApp.Top - progressBarStatu.Height);
            this.progressBarStatu.Width = this.Width;
        }

        private void checkBoxUseDefaultWF_CheckedChanged(object sender, EventArgs e)
        {
            //grouper1.Enabled = !checkBoxUseDefaultWF.Checked;
            if (binit)
                return;

            /*
             （使用默认波形选项，放到下载京瓷的waveform界面里）
             * request=0x6F, index 3, value = 0, 不使用， value = 1使用  支持set get, 
             * 设置时提示用户重启后生效，下载波形前需要判断value是否为0， 
             * 非0提示用户取消 “使用默认波形”，确定取消后才能下载波形
             */
            byte cmd = 0x6f;
            ushort value = 0;
            ushort index = (ushort)3;
            byte[] buf = new byte[1];
            buf[0] = (byte)(checkBoxUseDefaultWF.Checked ? 1 : 0);
            uint len = (uint)buf.Length;
            if (CoreInterface.SetEpsonEP0Cmd(cmd, buf, ref len, value, index) == 0)
            {
                //MessageBox.Show("设置使用默认波形选项失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("Set using the default waveform option failed!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //MessageBox.Show("设置使用默认波形选项成功,板卡重启后生效！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Set using the default waveform option is successful, the board after the restart to take effect!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int _hbindexReadall = 0;
        private void buttonRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (numHeadBoardindex.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbindexReadall = (int)numHeadBoardindex.SelectedIndex;
                    if (!readWfWorker.IsBusy)
                    {
                        buttonSaveAs.Enabled = button1.Enabled = false;
                        if (m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4B_YH06_5pl || m_printerProperty.ePrinterHead == PrinterHeadEnum.Kyocera_KJ4A_RH06)
                        {
                            buttonDelayU13.Enabled = buttonDelayU24.Enabled = false;
                        }
                        richTextBoxText.Clear();
                        richTextBoxAscii.Clear();
                        readWfWorker.RunWorkerAsync();
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

        private void buttonSetAdjustVol_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmd.WriteAdjustmentVoltage;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private void buttonSetBaseVol_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                writeVolWorker.RunWorkerAsync((byte)KyoceraCmd.WriteVoltageSetting);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private void buttonSetTempLimit_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                writeVolWorker.RunWorkerAsync((byte)KyoceraCmd.WriteTempLimit);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private int _hbIndexVt = 0;
        private void buttonReadAdjustVol_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        byte cmd = (byte) KyoceraCmd.ReadAdjustmentVoltage;
                        readVolWorker.RunWorkerAsync(cmd);
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

        private void buttonReadBaseVol_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        readVolWorker.RunWorkerAsync((byte)KyoceraCmd.ReadVoltageSetting);
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

        private void buttonReadTempLimit_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        readVolWorker.RunWorkerAsync((byte)KyoceraCmd.ReadTempLimit);
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

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
            saveas.Filter = "文本文件|*.txt";
            saveas.DefaultExt = "txt";
            DialogResult dr = saveas.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveas.FileName,false);
                for (int i = 0; i < richTextBoxAscii.Lines.Length; i++)
                {
                    sw.WriteLine(richTextBoxAscii.Lines[i]);
                }
                sw.Flush();
                sw.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
            saveas.Filter = "文本文件|*.txt";
            saveas.DefaultExt = "txt";
            DialogResult dr = saveas.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                int bytes = GetDrivingWaveformBytes();
                for (int i = 0; i < readWaveformDatas.Count; i++)
                {
                    List<byte> temp = new List<byte>();
                    string filename = string.Format("{0}_{1}.txt", Path.GetFileNameWithoutExtension(saveas.FileName),
                        i == 0 ? "A" : "B");
                    string savepath = Path.Combine(Path.GetDirectoryName(saveas.FileName), filename);
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

        
        private void buttonPrintPrt_Click(object sender, EventArgs e)
        {
            string prtPath = Path.Combine(Application.StartupPath, "byhx.prt");
            if (File.Exists(prtPath))
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    SPrinterSetting ss = m_allParam.PrinterSetting;
                    ss.sFrequencySetting.nPass = 1;
                    ss.sBaseSetting.nFeatherPercent = 0;
                    m_allParam.PrinterSetting = ss;
                    CoreInterface.SetPrinterSetting(ref ss);

                    CoreInterface.Printer_PrintFile(prtPath);
                })); 
            }
            else
            {
                MessageBox.Show(string.Format(@"File ({0}) does not exist. ",prtPath));
            }
        }

        public bool End()
        {

            if (m_allParam != null)
            {
                m_allParam.SaveToXml(null, true);
            }

            CoreInterface.SystemClose();
            return true;
        }
        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_allParam.PrinterProperty = sp;
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            m_allParam.PrinterSetting = ss;
        }
        public void OnPreferenceChange(UIPreference up)
        {
            m_allParam.Preference = up;
        }

        private void buttonReadDlysel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        byte cmd = (byte)KyoceraCmdForKJ4A_TA06.ReadDLYSEL;
                        readVolWorker.RunWorkerAsync(cmd);
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

        private byte[] dlysels;
        private void buttonSetDlysel_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            dlysels = new byte[4] { 6, 2, 6, 2 }; //默认值
            byte.TryParse(comboBoxDlysel1.Text, out dlysels[0]);
            byte.TryParse(comboBoxDlysel2.Text, out dlysels[1]);
            byte.TryParse(comboBoxDlysel3.Text, out dlysels[2]);
            byte.TryParse(comboBoxDlysel4.Text, out dlysels[3]);
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmdForKJ4A_TA06.WriteDLYSEL;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }

        }

        private void buttonReadAdjustVol2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        byte cmd = (byte)KyoceraCmdForKJ4A_TA06.ReadAdjustmentVoltage;
                        readVolWorker.RunWorkerAsync(cmd);
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

        private void buttonSetAdjustVol2_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmdForKJ4A_TA06.WriteAdjustmentVoltage;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }

        }

        private void buttonReadAdjustVolForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        readVolWorker.RunWorkerAsync((byte)KyoceraCmdForKJ4A_RH06.ReadTotalAdjustmentVoltage);
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

        private void buttonReadVolSetForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        readVolWorker.RunWorkerAsync((byte)KyoceraCmdForKJ4A_RH06.ReadVoltageSettingByUnit);
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

        private void buttonReadTempLimitForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                {
                    _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                    if (!readVolWorker.IsBusy)
                    {
                        readVolWorker.RunWorkerAsync((byte)KyoceraCmdForKJ4A_RH06.ReadTempLimitsForHeaterSetting);
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

        private void buttonSetAdjustVolForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmdForKJ4A_RH06.WriteTotalAdjustmentVoltage;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private void buttonSetVolSetForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmdForKJ4A_RH06.WriteVoltageSettingByUnit;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private void buttonSetTempLimitForKJ4A_RH06_Click(object sender, EventArgs e)
        {
            if (comboBoxHbIndexVT.SelectedIndex < 0)
            {
                MessageBox.Show(@"Input is wrong.");
                return;
            }
            _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
            if (!writeVolWorker.IsBusy)
            {
                byte cmd = (byte)KyoceraCmdForKJ4A_RH06.WriteTempLimitsForHeaterSetting;
                writeVolWorker.RunWorkerAsync(cmd);
            }
            else
            {
                MessageBox.Show("Background Worker Is Busy!");
            }
        }

        private void buttonDelayU13_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
            saveas.Filter = "文本文件|*.txt";
            saveas.DefaultExt = "txt";
            DialogResult dr = saveas.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                for (int i = 0; i < readDelaySettingForU13Datas.Count; i++)
                {
                    StreamWriter sw = new StreamWriter(saveas.FileName, false);
                    int j = 0;
                    for (int h = 7; h < readDelaySettingForU13Datas[i].Length; h++)
                    {
                        sw.WriteLine(string.Format("{0}", readDelaySettingForU13Datas[i][h]));
                        j++;
                        if (j == 128)
                            break;
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private void buttonDelayU24_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveas = new SaveFileDialog();
            saveas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //桌面路径
            saveas.Filter = "文本文件|*.txt";
            saveas.DefaultExt = "txt";
            DialogResult dr = saveas.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                for (int i = 0; i < readDelaySettingForU24Datas.Count; i++)
                {
                    StreamWriter sw = new StreamWriter(saveas.FileName, false);
                    int j = 0;
                    for (int h = 7; h < readDelaySettingForU24Datas[i].Length; h++)
                    {
                        sw.WriteLine(string.Format("{0}", readDelaySettingForU24Datas[i][h]));
                        j++;
                        if (j == 128)
                            break;
                    }
                    sw.Flush();
                    sw.Close();
                }
            }
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

        private void buttonWriteDelaySettingForKJ4A_RH0613_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Txt);
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                writeDelaySettingPath = fileDialog.FileName;
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                if (!writeVolWorker.IsBusy)
                {
                    byte cmd = (byte)KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU13;
                    writeVolWorker.RunWorkerAsync(cmd);
                }
                else
                {
                    MessageBox.Show("Background Worker Is Busy!");
                }
            }
        }

        private void buttonWriteDelaySettingForKJ4A_RH0624_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Txt);
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                writeDelaySettingPath = fileDialog.FileName;
                if (comboBoxHbIndexVT.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Input is wrong.");
                    return;
                }
                _hbIndexVt = comboBoxHbIndexVT.SelectedIndex;
                if (!writeVolWorker.IsBusy)
                {
                    byte cmd = (byte)KyoceraCmdForKJ4A_RH06.WriteDelaySettingForU24;
                    writeVolWorker.RunWorkerAsync(cmd);
                }
                else
                {
                    MessageBox.Show("Background Worker Is Busy!");
                }
            }
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
