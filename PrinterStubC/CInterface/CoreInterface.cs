/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

using System.Reflection;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using System.Security.Cryptography;
using PrinterStubC.CInterface;
using PrinterStubC.Common;

namespace BYHXPrinterManager
{
    /// <summary>
    /// Summary description for CoreInterface.
    /// </summary>
    /// 
    public struct CoreConst
    {
        public const string c_MUTEX_App = "METEX_NAME_BYHX_PRINTERMANAGER";
        public const string c_KernelDllName = "JobPrint.dll";
        public const string LayoutFileName = "LayoutSettingList.xml";
        public const int MAX_PATH = 260;
        public const int MAX_ELECTRIC_NUM = 32;

        public const int MAX_NAME = 32;
        public const int MAX_NOZZLE_NUM = 512;
        public const int MAX_HEAD_NUM = 64;
        public const int MAX_FILE_HEAD_NUM = 32;

        //public const int MAX_VOL_TEMP_NUM = 64;
        public const int MAX_VOL_TEMP_NUM = 256;
        public const int MAX_GROUPY_NUM = 4;
        public const int MAX_GROUPX_NUM = 2;
        public const int MAX_COLOR_NUM = 16;
        public const int OLD_MAX_COLOR_NUM = 8;
        public const int MAX_X_PASS_NUM = 12;
        public const int MAX_Y_PASS_NUM = 9;
        public const int MAX_PASS_NUM = MAX_X_PASS_NUM * MAX_Y_PASS_NUM;

        public const int MAX_RESLIST_NUM = 4;
        public const int MAX_SPEED_NUM = 4;
        public const int MAX_BIDIRECTION_NUM = 6;
        public const int MAX_PASSWORD_LEN = 16;

        public const int MAX_USBINFO_STRINGLEN = 256;
        public const int MAX_PREVIEW_WIDTH = 256;
        public const int MAX_PREVIEW_BUFFER_SIZE = MAX_PREVIEW_WIDTH * MAX_PREVIEW_WIDTH * OLD_MAX_COLOR_NUM;
        public const int MAX_BOARD_NUM = 32;
        public const int MAX_NAME_LEN = 16;
        public const int SIZEOF_CalibrationHorizonSetting = MAX_HEAD_NUM * 2 + 4; //for 64
        //public const int SIZEOF_CalibrationHorizonSetting = 68;//for 32  MAX_HEAD_NUM  //49;for 24 MAX_HEAD_NUM;
        public const int MAX_HORIZONARRAY_LEN = 512;
        public const int MAX_CHANNEL_NUM = 8;

        ///// <summary> will delete laterly
        public const int BOARD_DATE_LEN = 12;

        public const string DoublePrintPrtPreName = "_pre";
        public const string DoublePrintPrtPosName = "_pos";

        public const uint BeEnableConstMark = 0x19ED5500;
        public const string SkinForlderName = "Skins";

        public const int DefaultMbid = 1;
        public const byte AXIS_X = 0x01; //  第一个工作台
        public const byte AXIS_4 = 0x08; //  第二个工作台
        /// <summary>
        /// 浮点精度误差
        /// </summary>
        public const double DOUBLE_DELTA = 1E-06;
        /// <summary>
        /// 使能泵墨超时报警
        /// </summary>
        public const int ENABLE_CONTINU_PUMP_INK = 0x4D42;

    }

    public class CoreInterface
    {
        public static AllParam AllParams;

        public static int PrintType = 0;
        public static DateTime JobBegin;
        public static bool IsAbortJob = false;
        public static int JobLineCount = 0;
        public static int LayoutIndex = -1;

        public static volatile JetStatusEnum CurBoardStatus = JetStatusEnum.Unknown;
        /// <summary>
        /// 当前连接的板卡id,用于工正加密狗绑定
        /// </summary>
        public static uint BoardId { get; set; }

        public static bool GetLiyuRipHEADER(string filename, out LiyuRipHEADER ripHeader, bool isCsPrt = false)
        {
            int readSize = Marshal.SizeOf(typeof(LiyuRipHEADER));
            if (isCsPrt)
            {
                readSize = Marshal.SizeOf(typeof(CAISHEN_HEADER));
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                byte[] buf = reader.ReadBytes(readSize);
                CAISHEN_HEADER csHeader = (CAISHEN_HEADER)SerializationUnit.BytesToStruct(buf, typeof(CAISHEN_HEADER));
                ripHeader = ConverCAISHEN_HEADER2LiyuRipHEADER(csHeader);
            }
            else
            {
                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                byte[] buf = reader.ReadBytes(readSize);
                ripHeader = (LiyuRipHEADER)SerializationUnit.BytesToStruct(buf, typeof(LiyuRipHEADER));
            }
            return true;
        }

        private static LiyuRipHEADER ConverCAISHEN_HEADER2LiyuRipHEADER(CAISHEN_HEADER csHeader)
        {
            LiyuRipHEADER ripHeader = new LiyuRipHEADER();
            ripHeader.nImageWidth = csHeader.uImageWidth;
            ripHeader.nImageHeight = csHeader.uImageHeight;
            ripHeader.nImageResolutionX = csHeader.uXResolution;
            ripHeader.nImageResolutionY = csHeader.uYResolution;
            ripHeader.nImageColorDeep = csHeader.uGrayBits;
            ripHeader.nImageColorNum = csHeader.uColors;
            // 彩神文件头里无此数据定义,只能自己计算
            ripHeader.nBytePerLine = (ripHeader.nImageWidth * ripHeader.nImageColorDeep + 7) / 8;
            return ripHeader;
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="srcBs"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] EnByteToByte(byte[] srcBs, byte[] key)
        {
            byte[] keyBytes = key;
            byte[] keyIv = keyBytes;
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider { Padding = PaddingMode.None };
            //desProvider.Mode = CipherMode.ECB;
            MemoryStream memStream = new MemoryStream();
            CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateEncryptor(keyBytes, keyIv), CryptoStreamMode.Write);
            crypStream.Write(srcBs, 0, srcBs.Length);
            crypStream.FlushFinalBlock();
            return memStream.ToArray();
        }

        /// <summary>
        /// 工正板卡限定 按功能位清空相应数据
        /// bit15	运行时间清零(1:clr)
        /// bit14	总墨量清零(1:clr)
        /// bit13	UV时间清零(1:clr)
        /// bit12	总打印面积清零(1:clr)
        /// bit11	月墨量清零(1:clr)
        /// bit10	月打印面积清零(1:clr)
        /// bit9	升级包公正Key清零(1:clr)
        /// bit8	升级包公正板卡狗key清零(1:clr)
        /// bit7	升级包墨水密码key清零(1:clr)
        /// bit6-bit0	保留
        /// </summary>
        /// <returns></returns>
        public static bool ClearStatisticsData(short funcBit)
        {
            byte[] desbuf = new byte[56];
            uint desbufsize = (uint)desbuf.Length;
            byte[] randbuf = new byte[64];
            uint bufsize = (uint)randbuf.Length;
            byte[] fwManId = new byte[2];
            byte[] boardIds = new byte[4];
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (GetEpsonEP0Cmd(0x86, randbuf, ref bufsize, 1, 0x10) == 0)
                return false;
            Array.Copy(randbuf, 8, desbuf, 0, 16);
            ushort vid;
            var pid = vid = 0;
            if (GetProductID(ref vid, ref pid) == 0)
                return false;
            if (GetBoardInfo(0, ref sBoardInfo) == 0)
                return false;

            fwManId[1] = (byte)((vid >> 8) & 0xFF);
            fwManId[0] = (byte)(vid & 0xFF);
            boardIds[3] = (byte)((sBoardInfo.m_nBoardSerialNum >> 24) & 0xFF);
            boardIds[2] = (byte)((sBoardInfo.m_nBoardSerialNum >> 16) & 0xFF);
            boardIds[1] = (byte)((sBoardInfo.m_nBoardSerialNum >> 8) & 0xFF);
            boardIds[0] = (byte)(sBoardInfo.m_nBoardSerialNum & 0xFF);
            Array.Copy(fwManId, 0, desbuf, 16, 2);
            Array.Copy(boardIds, 0, desbuf, 18, 4);
            Array.Copy(BitConverter.GetBytes(funcBit), 0, desbuf, 22, 2);
            byte[] keys = { 0x51, 0xfb, 0x25, 0x11, 0x1d, 0x93, 0x21, 0xf9 };
            if (SetEpsonEP0Cmd(0x86, EnByteToByte(desbuf, keys), ref desbufsize, 1, 0x10) == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 工正板卡限定
        /// rand[8-23]	FW Man ID	BoardID	Reserved	
        ///  0 … 15	16 … 17	18-21	22… 56
        /// </summary>
        /// <returns></returns>
        public static bool IsFatal(uint boardId, int functionBit = 0)
        {
            byte[] desbuf = new byte[56];
            uint desbufsize = (uint)desbuf.Length;
            byte[] randbuf = new byte[64];
            uint bufsize = (uint)randbuf.Length;
            byte[] fwManId = new byte[2];
            byte[] boardIds = new byte[4];
            SBoardInfo sBoardInfo = new SBoardInfo();
            //从ARM取回的随机数。
            if (GetEpsonEP0Cmd(0x86, randbuf, ref bufsize, 1, 0x10) == 0)
                return false;
            Array.Copy(randbuf, 8, desbuf, 0, 16);
            ushort vid;
            var pid = vid = 0;
            if (GetProductID(ref vid, ref pid) == 0)
                return false;
            if (GetBoardInfo(0, ref sBoardInfo) == 0)
                return false;
            if (sBoardInfo.m_nBoardSerialNum != boardId)
                return false;

            fwManId[1] = (byte)((vid >> 8) & 0xFF);
            fwManId[0] = (byte)(vid & 0xFF);
            boardIds[3] = (byte)((sBoardInfo.m_nBoardSerialNum >> 24) & 0xFF);
            boardIds[2] = (byte)((sBoardInfo.m_nBoardSerialNum >> 16) & 0xFF);
            boardIds[1] = (byte)((sBoardInfo.m_nBoardSerialNum >> 8) & 0xFF);
            boardIds[0] = (byte)(sBoardInfo.m_nBoardSerialNum & 0xFF);
            Array.Copy(fwManId, 0, desbuf, 16, 2);
            Array.Copy(boardIds, 0, desbuf, 18, 4);
            Array.Copy(BitConverter.GetBytes(functionBit), 0, desbuf, 24, 4);//4字节（INT32）做功能BIT(0)置1表示认证通过，其他保留域置0

            byte[] keys = { 0x51, 0xfb, 0x25, 0x11, 0x1d, 0x93, 0x21, 0xf9 };
            //保留域目前均应该设置为0，保留域的未来使用方法是全0表示是旧PM版本，全1表示不校验，PM应该将不认识的保留域清0。
            if (SetEpsonEP0Cmd(0x86, EnByteToByte(desbuf, keys), ref desbufsize, 1, 0x10) == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="axil"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static int MoveToPosCmd(int pos, int axil, int speed)
        {
            SJetMovePosInfo info = new SJetMovePosInfo();
            //info.m_nDstPos = (uint) pos;
            //info.m_nAxil = (byte) axil;
            //info.m_nSpeed = (uint) speed;
            //byte[] buff = SerializationUnit.StructToBytes(info);
            //NOTE : StructToBytes 是 32 位对齐的

            byte[] buff = new byte[9];
            buff[0] = (byte)axil;

            buff[1] = (byte)((pos & 0xff));
            buff[2] = (byte)((pos >> 8) & 0xff);
            buff[3] = (byte)((pos >> 16) & 0xff);
            buff[4] = (byte)((pos >> 24) & 0xff);

            buff[5] = (byte)((speed & 0xff));
            buff[6] = (byte)((speed >> 8) & 0xff);
            buff[7] = (byte)((speed >> 16) & 0xff);
            buff[8] = (byte)((speed >> 24) & 0xff);


            uint size = (uint)buff.Length;
            return SetEpsonEP0Cmd(0x67, buff, ref size, 0, 0);
        }
        /// <summary>
        /// 获取对应pass数的步进高度
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns>喷头分辨率单位</returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetJobAdvanceHeight(int Pass);


        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetBandIndex(int bandIndex, ref int dir, ref int FireStart_X, ref int FireNum,
            ref int Step);

        /// <summary>
        /// 获取ep6返回数据,buf如果是null则为获取数据长度,否则按传入长度返回数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="index"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetEp6PipeData(int cmd, int index, byte[] buf, ref int len);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int UpdatePrinterSetting(int cmd, byte[] data, int len, int index, int value);

        public static bool IsS_system()
        {
            if (AllParams != null
                //&& AllParams.PrinterProperty.Version>0
                )
                return AllParams.PrinterProperty.SSysterm != 0;
            return false;
        }
        /// <summary>
        /// 可动态切花软件布局和映射
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetHeadMask(byte mode, byte mask);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int CalcInkCounter(string Jobname, int filetype, int inkindex, ulong[] counterarray,
            int x_start, int y_start, int clip_width, int clip_height,
    int x_copy, int y_copy, float x_interval, float y_interval, ulong[] bufs);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern bool IsKm1024I_AS_4HEAD();

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern bool IsSG1024_AS_8_HEAD();

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int get_HeadBoardType(bool bPoweron);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int DownInkCurve(byte[] sBuffer, int nBufferSize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetWaveformData(byte[] pSendData, uint len);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern ushort Crc16(byte[] puchMsg, ushort usDataLen);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetGetKonicInkTest(ref ushort pulseWidth, ref ushort delay, ref ushort fireFreq, ref ushort ta, int bSet);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetGetKonicInkTest_struct(byte[] pParam, ref uint size, int bSet);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetGetKonicPulseWidth(ref int pulseWidth, int bSet);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetGetAdjustClock(ushort[] clock, int len, int bSet);

        //Sysem 
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SystemInit();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SystemClose();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetMessageWindow(IntPtr hWnd, uint nMsg);//????

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SendJetCommand(int nCmd, int nValue);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int MoveCmd(int nCmd, int nValue, int speed);

        //2Updater 
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int BeginMilling(string sFilename);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int BeginUpdating(byte[] sBuffer, int nBufferSize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int AbortUpdating();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetPipeCmdPackage(byte[] info, int infosize, int port);


        //3Print 
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_Abort();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_Pause();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_Resume();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_PauseOrResume();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_IsOpen();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_Open();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void Printer_Close(int hHandle);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_Send(int hHandle, byte[] sBuffer, int nBufferSize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_GetFileInfo(string sFilename, ref SPrtFileInfo sInfo, int bGenPrev);
        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int Printer_GetJobInfo(ref SPrtFileInfo sInfo);

        /// <summary>
        /// 获取实际打印使用的作业参数
        /// </summary>
        /// <param name="sInfo"></param>
        /// <param name="useNewInterface">是否启用新接口,计算实际pass数考虑羽化,固定色序等因素</param>
        /// <returns></returns>
        public static int Printer_GetJobInfo(ref SPrtFileInfo sInfo, bool useNewInterface = true)
        {
            int ret = Printer_GetJobInfo(ref sInfo);
            if (useNewInterface && !SPrinterProperty.IsDocanPrintMode())
            {
                sInfo.sFreSetting.nPass = (byte)GetRealPassNum();
            }
            return ret;
        }
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern float GetAdvanceHeight(int Pass);
        /// <summary>
        /// 计算实际pass数考虑羽化,固定色序等因素
        /// </summary>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName, CharSet = CharSet.Auto)]
        public static extern int GetRealPassNum();

        [DllImport(CoreConst.c_KernelDllName, CharSet = CharSet.Auto)]
        public static extern int Printer_GetFilePreview(string sFilename, IntPtr pPreviewData, ref SPrtFileInfo sInfo);


        //Get Parameter
        //PrintCommand
        //1 Calibration
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetStepReviseValue(float fRevise, int Pass, ref SCalibrationSetting sSetting, int bOnePass);
        /// <summary>
        /// 打印校准图案
        /// </summary>
        /// <param name="cmd">指示校准图形的枚举值</param>
        /// <param name="nValue">某些图案特有参数</param>
        /// <param name="sSetting">应用于本次打印的设置参数</param>
        /// <returns>0:失败;1:成功</returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SendCalibrateCmd(int cmd, int nValue, ref SPrinterSetting sSetting);

        [HandleProcessCorruptedStateExceptions]
        public static int SendCalibrateCmd(int cmd, int nValue, ref SPrinterSetting sSetting,
            bool bSetJobSetting = true, byte scanningAxis = CoreConst.AXIS_X, int mediaIndex = 0)
        {
            try
            {
                if (SPrinterProperty.IsJianRui())
                {
                    int firstWorkPosIdx = -1;
                    for (int i = 0; i < 4; i++)
                    {
                        bool isEnable = ((sSetting.sExtensionSetting.WorkPosEnable >> i) & 1) == 1;
                        if (isEnable)
                        {
                            firstWorkPosIdx = i;
                            break;
                        }
                    }

                    if (firstWorkPosIdx >= 0)
                    {
                        int pulse = sSetting.sExtensionSetting.WorkPosList[firstWorkPosIdx];
                        const int port = 1;
                        byte[] m_pData = new byte[28];
                        //First Send Begin Updater
                        m_pData[0] = 6 + 2;
                        m_pData[1] = 0x41; //Move cmd

                        m_pData[2] = (byte)8; //Move cmd
                        m_pData[3] = (byte)6; //Move cmd
                        m_pData[4] = (byte)(pulse & 0xff); //Move cmd
                        m_pData[5] = (byte)((pulse >> 8) & 0xff); //Move cmd
                        m_pData[6] = (byte)((pulse >> 16) & 0xff); //Move cmd
                        m_pData[7] = (byte)((pulse >> 24) & 0xff); //Move cmd
                        CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);

                        LogWriter.WriteLog(new string[] { string.Format("[JianRui]Move WorkPos:{0}", pulse) }, true);

                        Thread.Sleep(2000);

                        while (true)
                        {
                            JetStatusEnum curStatus = CoreInterface.GetBoardStatus();

                            if (JetStatusEnum.Ready == curStatus || JetStatusEnum.PowerOff == curStatus)
                            {
                                break;
                            }

                            Thread.Sleep(1000);
                        }
                    }
                    LogWriter.WriteLog(new string[] { string.Format("[JianRui]Begin CheckNozzle") }, true);
                }

                SPrinterSetting tempSetting = sSetting;

                CoreInterface.JobBegin = DateTime.Now;
                if (cmd == (int)CalibrationCmdEnum.CheckNozzleCmd)
                {
                    CoreInterface.PrintType = 1;
                }
                else
                {
                    CoreInterface.PrintType = 2;
                }

                if (cmd == (int)CalibrationCmdEnum.Mechanical_CheckAngleCmd
                    || cmd == (int)CalibrationCmdEnum.Mechanical_CrossHeadCmd
                    || cmd == (int)CalibrationCmdEnum.NozzleAllCmd
                    || cmd == (int)CalibrationCmdEnum.Mechanical_CheckVerticalCmd)
                {
                    tempSetting.sFrequencySetting.nResolutionX /= tempSetting.sBaseSetting.nXResutionDiv;
                }

                if (bSetJobSetting)
                {
                    SJobSetting sjobseting = new SJobSetting();
                    CoreInterface.GetSJobSetting(ref sjobseting);
                    if (AllParams.PrinterProperty.Y_BackToOrgControlBySw())
                    {
                        sjobseting.Yorg = (byte)(tempSetting.sExtensionSetting.bYBackOrigin ? 1 : 2);// !globalAlternatingPrint;
                    }
                    else
                    {
                        sjobseting.Yorg = 0;
                    }
                    CoreInterface.SetSJobSetting(ref sjobseting);
                    if (PubFunc.IsZhuoZhan())
                    {
                        SSeviceSetting sSeviceSet = new SSeviceSetting();
                        CoreInterface.GetSeviceSetting(ref sSeviceSet);

                        sSeviceSet.scanningAxis = scanningAxis;
                        CoreInterface.SetSeviceSetting(ref sSeviceSet);

                        //AllParams.CurPrintPlatForm = scanningAxis;
                        //CoreInterface.SetPrinterSetting(ref sSetting);
                    }
                }

                if (tempSetting.sBaseSetting.nSpotColor1Mask == 0)
                    tempSetting.sBaseSetting.nSpotColor1Mask = (ushort)0xFF00;

                if (tempSetting.sBaseSetting.nSpotColor2Mask == 0)
                    tempSetting.sBaseSetting.nSpotColor2Mask = (ushort)0xFF00;

                if (SPrinterProperty.IsInwearSimpleUi() || AllParams.PrinterProperty.IsAllWinUV())
                {
                    // 变革只要是uv灯错排校准也多扫几pass
                    tempSetting.sExtensionSetting.bEnableAnotherUvLight = tempSetting.sExtensionSetting.fRunDistanceAfterPrint > 0;
                    tempSetting.sBaseSetting.fYAddDistance = tempSetting.sExtensionSetting.fRunDistanceAfterPrint;
                    CoreInterface.SetPrinterSetting(ref tempSetting);
                }
                else
                {
                    tempSetting.sExtensionSetting.bEnableAnotherUvLight = false;
                    tempSetting.sBaseSetting.fYAddDistance = 0;
                    CoreInterface.SetPrinterSetting(ref tempSetting);
                }
                // 卷轴机类似平板应用
                if (AllParams.ExtendedSettings.IsFlatMode)
                {
                    bool bsupportwhite = (AllParams.PrinterProperty.nWhiteInkNum & 0x0F) > 0 &&
                         ((tempSetting.sBaseSetting.nLayerColorArray & 0x02) == 0);
                    bool bsupportVarnish = (AllParams.PrinterProperty.nWhiteInkNum >> 4) > 0 &&
                                           ((tempSetting.sBaseSetting.nLayerColorArray & 0x04) == 0);
                    bool bsupportColor = ((tempSetting.sBaseSetting.nLayerColorArray & 0x01) == 0);
                    tempSetting.sBaseSetting.fJobSpace = AllParams.ExtendedSettings.PlatformCorrect + AllParams.ExtendedSettings.fRoll2FlatJobSpace;
                    // 卷轴机类似平板应用,错排彩+白且只打彩时作业间距自动加上y间距,防止彩白都打和只打彩时起始点不同
                    if (AllParams.PrinterProperty.SurpportJobSpaceAsOriginY()
                        && AllParams.PrinterProperty.bSupportWhiteInkYoffset
                        && bsupportColor
                        && !bsupportwhite)
                    {
                        tempSetting.sBaseSetting.fJobSpace += AllParams.PrinterProperty.fHeadYSpace;
                    }
                }
                int _mediaindex = 0;
                if (mediaIndex > 0) //校准向导传入临时设置
                    _mediaindex = mediaIndex;
                else
                {
                    if (AllParams.Preference.MediaModeIndex > 0) //系统当前设置
                        _mediaindex = AllParams.Preference.MediaModeIndex;
                }
                if (_mediaindex > 0)
                {
                    JobMediaModes mediaModes = PubFunc.LoadMediaModesFromFile();
                    //介质模式
                    MediaConfig mediaMode = mediaModes.Items[AllParams.Preference.MediaModeIndex - 1].Item;
                    tempSetting.sCalibrationSetting.nStepPerHead = mediaMode.BaseStep;
                }

                LayoutSetting tempLayoutSetting = new LayoutSetting(1);
                tempLayoutSetting.layerSetting[0].curLayerType = 0;
                tempLayoutSetting.layerSetting[0].layerYOffset = 0.00f;
                tempLayoutSetting.layerSetting[0].subLayerNum = 1;
                tempLayoutSetting.layerSetting[0].YContinueHead = 1;
                tempLayoutSetting.layerSetting[0].curYinterleaveNum = 2;
                tempSetting.layoutSetting = tempLayoutSetting;

                
                return SendCalibrateCmd(cmd, nValue, ref tempSetting);
            }
            catch (Exception ex)
            {
                LogWriter.SaveOptionLog("SendCalibrateCmd===" + ex.Message + ex.StackTrace);
                //MessageBox.Show(ex.Message + ex.StackTrace);
                return 0;
            }
        }



        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetSPrinterProperty(ref SPrinterProperty sProperty);

        /// <summary>
        /// 墨滴监测 写的不同喷孔阵列的打印命令 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="nValue"></param>
        /// <param name="sSetting"></param>
        /// <param name="sDataSetting"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SendConstructDataCmd(CalibrationCmdEnum cmd, int nValue, ref SPrinterSetting sSetting, ConstructDataSetting sDataSetting, byte[] DataBegin, int len);

        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int GetPrinterSetting(ref SPrinterSetting sSetting);

        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int SetPrinterSetting(ref SPrinterSetting sSetting);

        [HandleProcessCorruptedStateExceptions]
        public static int GetPrinterSetting(ref SPrinterSetting sSetting, bool useNewInterface = true)
        {
            int ret = GetPrinterSetting(ref sSetting);
            return ret;
        }

        [HandleProcessCorruptedStateExceptions]
        public static int SetPrinterSetting(ref SPrinterSetting sSetting, bool useNewInterface = true)
        {
            sSetting.sExtensionSetting.bIsNewCalibration = 1;//默认都使用新校准
            //Tony found if nWhiteInkLayer == 0, 结果就是打印空白band，load 老的参数 nWhiteInkLayer 就会是0
            if (sSetting.sBaseSetting.nWhiteInkLayer < 1)
                sSetting.sBaseSetting.nWhiteInkLayer = 1;
            sSetting.sExtensionSetting.BoardId = BoardId;

            if (sSetting.sBaseSetting.nSpotColor1Mask == 0)
                sSetting.sBaseSetting.nSpotColor1Mask = (ushort)0xFF00;

            if (sSetting.sBaseSetting.nSpotColor2Mask == 0)
                sSetting.sBaseSetting.nSpotColor2Mask = (ushort)0xFF00;

            int ret = SetPrinterSetting(ref sSetting);
            LogWriter.SaveOptionLog(string.Format("###########sSetting.sBaseSetting.fJobSpace={0};" +
                                                  "sSetting.sBaseSetting.fYOrigin={1};" +
                                                  "sSetting.sExtensionSetting.LineWidth={2};" +
                                                  "sSetting.sFrequencySetting.nResolutionY={3};" +
                                                  "sSetting.sExtensionSetting.BoardId={4};"+
                                                  "sSetting.sBaseSetting.bMirrorX={5};"+
                                                  "sSetting.sBaseSetting.nSpotColor1Mask={6};"+
                                                  "sSetting.sBaseSetting.nSpotColor2Mask={7};"+
                                                  "sSetting.sExtensionSetting.idleFlashUseStrongParams={8};",
                sSetting.sBaseSetting.fJobSpace,
                sSetting.sBaseSetting.fYOrigin,
                sSetting.sExtensionSetting.LineWidth,
                sSetting.sFrequencySetting.nResolutionY,
                sSetting.sExtensionSetting.BoardId,
                sSetting.sBaseSetting.nSpotColor1Mask,
                sSetting.sBaseSetting.nSpotColor2Mask,
                sSetting.sBaseSetting.bMirrorX,
                sSetting.sExtensionSetting.idleFlashUseStrongParams
                ));
            try
            {
                if (useNewInterface && AllParams != null)
                {
                    int validDataLen = AllParams.PrinterProperty.nHeadNum;
                    SCalibrationHorizonArrayUI horizonArray = AllParams.NewCalibrationHorizonArray;
                    for (int i = 0; i < horizonArray.Length; i++)
                    {
                        byte[] left = new byte[validDataLen];
                        byte[] right = new byte[validDataLen];
                        short[] sGroupLeft = new short[32];
                        byte[] groupLeft = new byte[64];
                        short[] sGroupRight = new short[32];
                        byte[] groupRight = new byte[64];
                        Buffer.BlockCopy(horizonArray[i].XLeftArray, 0, left, 0, validDataLen);
                        Buffer.BlockCopy(horizonArray[i].XRightArray, 0, right, 0, validDataLen);
                        Array.Copy(AllParams.CalibrationGroupUILeft.GCValue, i * 32, sGroupLeft, 0, 32);
                        Array.Copy(AllParams.CalibrationGroupUIRight.GCValue, i * 32, sGroupRight, 0, 32);
                        for (int j = 0; j < sGroupLeft.Length; j++)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(sGroupLeft[j]), 0, groupLeft, j * 2, 2);
                        }
                        for (int j = 0; j < sGroupRight.Length; j++)
                        {
                            Buffer.BlockCopy(BitConverter.GetBytes(sGroupRight[j]), 0, groupRight, j * 2, 2);
                        }
                        //IntPtr bufPtr = Marshal.AllocHGlobal(Marshal.SizeOf(horizonArray[i]));
                        //Marshal.StructureToPtr(horizonArray[i], bufPtr, false);
                        //Marshal.Copy(bufPtr, left, 0, validDataLen);
                        //Marshal.Copy(bufPtr, right, CoreConst.MAX_HORIZONARRAY_LEN, validDataLen);
                        UpdatePrinterSetting((int)PrinterSettingCmd.HorizonCalValueLeft, left, validDataLen,
                            horizonArray[i].ResIndex, horizonArray[i].SpeedIndex);
                        LogWriter.SaveOptionLog(string.Format("###########SetPrinterSetting left={0};", string.Join(",", left)));
                        UpdatePrinterSetting((int)PrinterSettingCmd.HorizonCalValueRight, right, validDataLen,
                            horizonArray[i].ResIndex, horizonArray[i].SpeedIndex);
                        LogWriter.SaveOptionLog(string.Format("###########SetPrinterSetting right={0};", string.Join(",", right)));
                        LogWriter.SaveOptionLog(string.Format("###########SetPrinterSetting horizonArray[i],i={0};ResIndex={1}, SpeedIndex={2}", i, horizonArray[i].ResIndex, horizonArray[i].SpeedIndex));
                        //Marshal.FreeHGlobal(bufPtr); //释放
                        UpdatePrinterSetting((int)PrinterSettingCmd.GroupCmdLeft, groupLeft, groupLeft.Length, horizonArray[i].ResIndex, horizonArray[i].SpeedIndex);
                        UpdatePrinterSetting((int)PrinterSettingCmd.GroupCmdRight, groupRight, groupRight.Length, horizonArray[i].ResIndex, horizonArray[i].SpeedIndex);
                        
                    }
                }
                if (UIFunctionOnOff.SupportCloseNozzle)
                {
                    ret=SetOverLapCheckData(AllParams.NozzleOverlap);//设置重叠校准参数
                }

                if (UIFunctionOnOff.SupportDebugQuality)
                {
                    ret = SetDebugQualityData(AllParams.PrintQualityUI);
                }   
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
                LogWriter.SaveOptionLog(string.Format("SetPrinterSetting Exception ={0},useNewInterface={1}", ex.Message, useNewInterface));
            }
            return ret;
        }

        private static int SetOverLapCheckData(SNozzleOverlap fwData)
        {
            bool[] rets = new bool[5];
            rets[0] = UpdatePS(PrinterSettingCmd.OverLapTotalNozzle, fwData.OverLapTotalNozzle);
            rets[1] = UpdatePS(PrinterSettingCmd.OverLapUpWasteNozzle, fwData.OverLapUpWasteNozzle);
            rets[2] = UpdatePS(PrinterSettingCmd.OverLapDownWasteNozzle, fwData.OverLapDownWasteNozzle);
            rets[3] = UpdatePS(PrinterSettingCmd.OverLapUpPercent, fwData.OverLapUpPercent);
            rets[4] = UpdatePS(PrinterSettingCmd.OverLapDownPercent, fwData.OverLapDownPercent);
            for (int i = 0; i < 5; i++)
            {
                if (!rets[i]) return 0;
            }
            return 1;
        }

        private static int SetDebugQualityData(SPrintQualityUI fwData)
        {
            UpdatePS(PrinterSettingCmd.VerticalOffset , fwData.VerticalOffset);

            return 1;
        }

        private static bool UpdatePS(PrinterSettingCmd pc, ushort[] data)
        {
            byte[] byteData = new byte[data.Length * 2];
            for (int j = 0; j < data.Length; j++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(data[j]), 0, byteData, j * 2, 2);
            }
            int ret = CoreInterface.UpdatePrinterSetting((int)pc, byteData, byteData.Length, 0, 79);
            return ret != 0;
        }

        private static bool UpdatePS(PrinterSettingCmd pc, float[] data)
        {
            byte[] byteData = new byte[data.Length * 4];
            for (int j = 0; j < data.Length; j++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(data[j]), 0, byteData, j * 4, 4);
            }
            int ret = CoreInterface.UpdatePrinterSetting((int)pc, byteData, byteData.Length, 0, 79);
            return ret != 0;
        }

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SavePrinterSetting();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetPrinterProperty(ref SPrinterProperty sProperty);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetSJobSetting(ref SJobSetting advSet);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetSJobSetting(ref SJobSetting advSet);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetSeviceSetting(ref SSeviceSetting sSetting);
        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int SetSeviceSetting(ref SSeviceSetting sSetting);

        /// <summary>
        /// 下发前增加合法性检查,防止配置文件破坏导致异常
        /// </summary>
        /// <param name="sSetting"></param>
        /// <param name="bRev"></param>
        /// <returns></returns>
        public static int SetSeviceSetting(ref SSeviceSetting sSetting, bool bRev = false)
        {
            if (sSetting.Vsd2ToVsd3_ColorDeep == 0)
            {
                ushort pid,vid ;
				pid = vid = 0;
                int ret = CoreInterface.GetProductID(ref vid, ref pid);
                if (ret != 0 && (vid == (ushort)VenderID.TATE || vid == 0x218))
                {
                    sSetting.Vsd2ToVsd3_ColorDeep = 1;
                }
                else
                {
                sSetting.Vsd2ToVsd3_ColorDeep = 3;
                }
            }
            if (sSetting.Vsd2ToVsd3 == 0)
            {
                sSetting.Vsd2ToVsd3 = 2;
            }
            if (sSetting.nBit2Mode == 0)
            {
                sSetting.nBit2Mode = 3;
            }
            return SetSeviceSetting(ref sSetting);
        }

        [DllImport(CoreConst.c_KernelDllName, CharSet = CharSet.Auto)]
        public static extern int GetBoardInfo(int BoardId, ref SBoardInfo sBoardInfo);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetPassword(string sPwd, int nPwdLen, ushort PortId, int bLang);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPassword(byte[] sPwd, ref int nPwdLen, ushort PortId, int bLang);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetProductID(ref ushort Vid, ref ushort Pid);
        //RealTime Info
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern JetStatusEnum GetBoardStatus();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetBoardError();
        //Version String(最好有信息）		
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetRealTimeInfo(ref  SRealTimeCurrentInfo info, uint mask);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetRealTimeInfo(ref  SRealTimeCurrentInfo info, uint mask);
        /// <summary>
        /// z轴移动;要求必须z轴找原点且找终点
        /// </summary>
        /// <param name="type">自动/手动</param>
        /// <param name="fZSpace"></param>
        /// <param name="fPaperThick"></param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int MoveZ(int type, float fZSpace, float fPaperThick);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetFWFactoryData(ref  SFWFactoryData info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetFWFactoryData(ref  SFWFactoryData info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetSupportList(ref  SSupportList info);
        //[DllImport(CoreConst.c_KernelDllName)]
        //public static extern int GetHeadMap(byte[]pElectricMap,int length);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetHWHeadBoardInfo(ref SWriteHeadBoardInfo info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPasswdInfo(ref int nLimitTime, ref int nDuration, ref int nLang);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPWDInfo(ref  SPwdInfo info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPWDInfo_UV(ref  SPwdInfo_UV info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetLangInfo(ref int nLimitTime);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetDebugInfo(byte[] pElectricMap, int length);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetUVStatus(ref int status);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetUVStatus(int status);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int ClearErrorCode(int code);
        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int SetFWVoltage(byte[] sVol, int nVolLen, int lcd);

        [HandleProcessCorruptedStateExceptions]
        public static int SetFWVoltage(byte[] sVol, int nVolLen, int lcid, bool rev = false)
        {
            if (lcid != (new CultureInfo("zh-CHS").LCID) && lcid != (new CultureInfo("zh-CHT").LCID))
            {
                // 中文以外的其他语言全按英文密码验证
                lcid = (new CultureInfo("en-US").LCID);
            }
            return CoreInterface.SetFWVoltage(sVol, nVolLen, lcid);
        }
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetInkParam(ref byte jetSpeed, ref byte inkType);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetInkParam(byte jetSpeed, byte inkType);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int VerifyHeadType();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int WriteBmpHeaderToBuffer(byte[] buf, int w, int height, int bitperpixel);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetOneHeadNozzleNum();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int UpdateHeadMask(byte[] mask, int len);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void NotifyPrinterPropertyChange();

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int StartBarcodePrint(int PageWidth, int PageHeight, int PageNum);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int EndBarcodePrint(int handle);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int PrintBarcode(int handle, int pageIndex, byte[] srcArray, int srcNum);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int ClipPrtTo1BitBuffer(string filename, int ClipX, int ClipY, int ClipW, int ClipH,
            int DestX, int DestY, int DestW, int DestH, byte[] buffer, int bufIndex, int ColorIndex);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int RefreshPreview(string srcfilename, string dstfilename, int PreviewWidth, int PreviewHeight, int ColorIndex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int ReadHBEEprom(byte[] buffer, int buffersize, int startoffset);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int WriteHBEEprom(byte[] buffer, int buffersize, int startoffset);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int BeginUpdateMotion(string sFilename);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetFWSetting(byte[] buffer, int buffersize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetFWSetting(byte[] buffer, int buffersize);

        #region 20120828 因Set382RealTimeInfo与Set382VtrimCurve在Vtrim设置上交叉,增加mask屏蔽Set382RealTimeInfo设置Vtrim功能

        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int Get382RealTimeInfo(ref  SRealTimeCurrentInfo_382 info, uint mask);
        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int Set382RealTimeInfo(ref  SRealTimeCurrentInfo_382 info, uint mask);

        enum Xaar382InfoMask
        {
            PWM = 0,
            Vtrim = 1,
            DualBank = 2,
            WVFMSelect = 3,
            HeadInfo = 4,
            Temperature = 5,
            TemperatureSet = 6,
            Xaar382HeatMode = 7,
            Xaar382ComStr = 8,
            MBSingleBandmode = 9,
        };

        public static int Get382RealTimeInfo(ref  SRealTimeCurrentInfo_382 info)
        {
            uint mask = 0;
            mask |= 1 << ((int)Xaar382InfoMask.PWM);
            mask |= 1 << ((int)Xaar382InfoMask.Temperature);
            mask |= 1 << ((int)Xaar382InfoMask.TemperatureSet);
            mask |= 1 << ((int)Xaar382InfoMask.Vtrim);
            mask |= 1 << ((int)Xaar382InfoMask.Xaar382HeatMode);
            return Get382RealTimeInfo(ref info, mask);
        }
        public static int Set382RealTimeInfo(ref  SRealTimeCurrentInfo_382 info)
        {
            uint mask = 0;
            //			mask |= 1 << ((int)Xaar382InfoMask.PWM);
            //			mask |= 1 << ((int)Xaar382InfoMask.Temperature);
            mask |= 1 << ((int)Xaar382InfoMask.TemperatureSet);
            //			mask |= 1 << ((int)Xaar382InfoMask.Vtrim);
            mask |= 1 << ((int)Xaar382InfoMask.Xaar382HeatMode);
            return Set382RealTimeInfo(ref info, mask);
        }
        #endregion
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Get501HeadInfo(byte[] info, ref ushort recvNum);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Get382HeadInfo(ref  SHeadInfoType_382 info, int nHeadIndex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Set382DualBand(ushort nDualBand, int nHeadIndex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Set382WVFMSelect(int nIndex, int nHeadIndex);


        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Down382WaveForm(byte[] sBuffer, int nBufferSize, int nHeadIndex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Set382VtrimCurve(byte[] sBuffer, int nBufferSize, int nHeadIndex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Get382VtrimCurve(byte[] sBuffer, ref int nBufferSize, int nHeadIndex);


        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetSpectraVolMeasure(int value, float[] fBuf, int len);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetDebugInk(byte[] info, int infosize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void TileImage(byte[] srcBuf, int srcBitOffset, int srcBitPerLine,
            byte[] dstBuf, int dstBitOffset, int dstBitPerLine,
            int nheight, int bitLen, int copies, int DetaBit, int colornum, bool bReversePrint);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetDspPwmInfo(byte[] SetPulseWidthInfo, int nBufferSize);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetUIHeadType(ref uint type);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetEpsonEP0Cmd(byte cmd, byte[] buffer, ref uint bufferSize, ushort value, ushort index);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetEpsonEP0Cmd(byte cmd, byte[] buffer, ref uint bufferSize, ushort value, ushort index);
        public static int SetEpsonEP0Cmd(byte cmd, byte[] buffer, ref uint bufferSize, ushort value, ushort index,
            int mbid = CoreConst.DefaultMbid)
        {
            return SetEpsonEP0Cmd(cmd, buffer, ref bufferSize, value, index);
        }
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern float GetfPulsePerInchY(int bFromProperty);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void SetPrintMode(int mode);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPrinterResolution(ref int nEncoderRes, ref int nPrinterRes);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetResXList(int[] nResolutionX, ref int nLen);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Set382ComCmd(byte[] buf, int len, int nHeadIndex, int subcmd);


        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPrintArea(ref double area);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetDirtyCmd(byte cmd, byte[] buf, ref int len);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetOutputColorDeep(byte area);
        [DllImport(CoreConst.c_KernelDllName)]
        private static extern int QueryCurrentPos(ref int xPos, ref int yPos, ref int zPos);

        /// <summary>
        /// X轴因为机械软，板卡发完脉冲之后，小车没动，所以位置在变,需重复抓俩次才准
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="zPos"></param>
        /// <returns></returns>
        public static int QueryCurrentPosN(ref int xPos, ref int yPos, ref int zPos)
        {
            QueryCurrentPos(ref xPos, ref yPos, ref zPos);
            Thread.Sleep(10);
            return QueryCurrentPos(ref xPos, ref yPos, ref zPos);
        }

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetSBiSideSetting(ref SBiSideSetting advSet);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetSBiSideSetting(ref SBiSideSetting advSet);

        /// <summary>
        /// 老的参数有新的定义
        ///float    fxTotalAdjust;   表示X 方向偏移
        ///float    fyTotalAdjust;   隐藏 总是为0
        ///float    fLeftTotalAdjust;  表示LEFT Y 向偏差
        ///float    fRightTotalAdjust;  表示右偏差
        ///float       fStepAdjust;     表示调整的步进
        /// </summary>
        /// <param name="advSet"></param>
        /// <param name="fXRightHeadToCurosr">表示最右喷头到光标安装位置的X 偏差</param>
        /// <param name="fYRightHeadToCurosr">表示最右喷头到光标安装位置的Y 偏差</param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetCurPosSBideSetting(ref SBiSideSetting advSet, float fXRightHeadToCurosr, float fYRightHeadToCurosr);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GenDoublePrintPrt(string infile, string outfile, bool bPos, ref SDoubleSidePrint param);
        [DllImport(CoreConst.c_KernelDllName)]
        /* 双面喷图像打印;
Input:
    imageInfo       PRT文件剪切、拼贴的参数;
    num             PRT文件个数;
    height          剪切、拼贴后图像高，单位：英寸;
    width           剪切、拼贴后图像宽，单位：英寸;
    printPosition   打印方向，true: 正向， false: 反向;
    param           彩条信息参数;
*/
        public static extern int Printer_DoublePrint(ImageTileItem[] images, int imageCount, double height, double width, bool bPos, ref SDoubleSidePrint param);


        [DllImport(CoreConst.c_KernelDllName)]
        /* 单文件双面喷打印;
注：老PM打印双面喷调用函数，新接口不使用;不支持反向打印
Input:
sFilename		双面喷文件;
Return:
0		失败;
1		成功;
*/
        public static extern int Printer_DoublePrintFile(string sFilename);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int Printer_PrintFile(string sFilename);
        /// <summary>
        /// 多介质多图片拼贴打印
        /// HANDLE OpenMulitImagePrinter(struct image_interface argv[], int num, float h, float w);
        /// </summary>
        /// <param name="images">拼贴使用的图片信息数组</param>
        /// <param name="imageCount">图片个数</param>
        /// <param name="height">prt高度（单位：英寸）</param>
        /// <param name="width">prt宽度（单位：英寸）</param>
        /// <returns></returns>
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int OpenMulitImagePrinter(ImageTileItem[] images, int imageCount, float height, float width);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int OpenMulitImagePrinter2(ImageTileItem[] images, int imageCount, double height, double width, bool bIsAWBMode, NoteInfo noteInfo);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPrintAmendProperty(ref SPrintAmendProperty info);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetPrintAmendProperty(ref SPrintAmendProperty info);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetUserSetInfo(ref USER_SET_INFORMATION info);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int SetUserSetInfo(ref USER_SET_INFORMATION info);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int RotationImage(string source, byte[] dest, float angle);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void SaveFWLog();


        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int AddDynamicListData(DynamicData info);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int ClearList();

#if MULTI_MB
		[DllImport(CoreConst.c_KernelDllName)]
		public static extern  int GetSystemUsbCnt();
        	[DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetPWDInfo(ref  SPwdInfo info, int mbid = CoreConst.DefaultMbid);
#else
        public static int GetSystemUsbCnt()
        {
            return 1;
        }
        public static int GetPWDInfo(ref SPwdInfo info, int mbid = CoreConst.DefaultMbid)
        {
            return GetPWDInfo(ref info);
        }
#endif
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int QueryPrintMaxLen(ref int xPos, ref int yPos, ref int zPos);


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++新布局接口

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetYinterleavePerRow(int currow);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetLayoutColorNum();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetLayoutColorID(int index);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetRowNum();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern long GetRowColor(int rowindex);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetTemperaturePerHead();
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetHeadNumPerRow(int currow);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void GetHeadIDPerRow(int currow, int headNum, byte[] data);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetLineNumPerRow(int currow);
        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void GetlineIDtoNozzleline(int lineID, ref NozzleLineData data);

        [DllImport(CoreConst.c_KernelDllName)]
        //public static extern int GetRealTimeInfo2(ref RealTimeDataOneHead info, ref int headNum, uint mask);
        //public static extern int GetRealTimeInfo2(byte[] infos, ref int headNum, uint mask);
        public static extern int GetRealTimeInfo2(IntPtr infosIntPtr, ref int headNum, uint mask);
        [DllImport(CoreConst.c_KernelDllName)]
        //public static extern int SetRealTimeInfo2(ref RealTimeDataOneHead[] info, int headNum, uint mask);
        public static extern int SetRealTimeInfo2(IntPtr infosIntPtr, int headNum, uint mask);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetLineIndexInHead(int lineid);

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetCaliGroupNum();

        [DllImport(CoreConst.c_KernelDllName)]
        public static extern int GetMaxColumnNum();

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        [DllImport(CoreConst.c_KernelDllName)]
        public static extern void ModifySPrinterModeSetting(ref LayoutSetting layoutSetting);
#if MULTI_MB
        [DllImport(CoreConst.c_KernelDllName)]
	    public static extern int EnumUsb(ref int usbNum, int[] usbIdArray, int arrayNum);
#else
        public static int EnumUsb(ref int usbNum, int[] usbIdArray, int arrayNum)
        {
            usbNum = 1;
            usbIdArray = new int[] { 1 };
            arrayNum = 1;
            return 1;
        }
#endif
        public static int GetFWFactoryData(ref SFWFactoryData info, int mbid)
        {
            return GetFWFactoryData(ref info);
        }

        public static void UpdatePrinterSettingByJobPrintMode(ref SPrinterSetting newSs, ModeConfig layer)
        {
            bool isGz = SPrinterProperty.IsGongZengUv() || SPrinterProperty.IsTILE_PRINT_ID();
            //SPrinterProperty sp = allParam.PrinterProperty;
            SPrinterSetting ss = AllParams.PrinterSetting;
            bool globalBidirection = (ss.sFrequencySetting.nBidirection != 0);
            bool globalAutoJumpWhite = ss.sBaseSetting.bIgnorePrintWhiteX;

            byte globalPrintMode = ss.sBaseSetting.nXResutionDiv;
            byte globalPass = ss.sFrequencySetting.nPass;
            byte globalFeatherType = ss.sBaseSetting.nFeatherType;
            int globalFeatherPercent = ss.sBaseSetting.nFeatherPercent;
            //byte GlobalBetweenNozzles = ss.sBaseSetting.bFeatherBetweenHead;喷头间羽化没有
            //byte GlobalIntensityFeather = ss.sBaseSetting.bColorCohesion;增强羽化没有
            SpeedEnum globalSpeed = ss.sFrequencySetting.nSpeed;
            byte globalMultipleInk = ss.sBaseSetting.multipleInk;
            float globalFeatherWavelength = ss.sBaseSetting.fFeatherWavelength;
            byte globalAdvanceFeatherPercent = ss.sBaseSetting.nAdvanceFeatherPercent;

            bool globalLeftLightLeftPrint = (ss.UVSetting.iLeftRightMask & 0x02) != 0;
            bool globalLeftLightRightPrint = (ss.UVSetting.iLeftRightMask & 0x01) != 0;
            bool globalRightLightLeftPrint = (ss.UVSetting.iLeftRightMask & 0x08) != 0;
            bool globalRightLightRightPrint = (ss.UVSetting.iLeftRightMask & 0x04) != 0;
            //改各属性，要考虑Global时使用全局设置，非Global时使用私有值

            //Bidirection
            if (!AllParams.PrinterProperty.IsDocan())
            {
                newSs.sFrequencySetting.nBidirection =
                    (byte)
                        ((BoolEx.Global == layer.Bidirection)
                            ? (globalBidirection ? ss.sFrequencySetting.nBidirection : 0)
                            : (BoolEx.True == layer.Bidirection ? 1 : 0)); // todo 这里有问题,打印模式里还不支持打印起始方向
            }

            //AutoJumpWhite
            newSs.sBaseSetting.bIgnorePrintWhiteX =
                newSs.sBaseSetting.bIgnorePrintWhiteY =
                    (BoolEx.Global == layer.AutoJumpWhite)
                        ? globalAutoJumpWhite
                        : (BoolEx.True != layer.AutoJumpWhite);
            //ExquisiteFeather
            newSs.sExtensionSetting.bExquisiteFeather =
                layer.ExquisiteFeather;
            //FeatherType
            if ("Global" == layer.Feather)
            {
                newSs.sBaseSetting.nFeatherPercent =
                    globalFeatherPercent;
            }
            else
            {
                foreach (EpsonFeatherType place in Enum.GetValues(typeof(EpsonFeatherType)))
                {
                    string cmode = ResString.GetEnumDisplayName(typeof(EpsonFeatherType), place);
                    if (cmode == layer.Feather)
                    {
                        //TODO code duplicated
                        switch (place)
                        {
                            case EpsonFeatherType.None:
                                {
                                    newSs.sBaseSetting.nFeatherPercent = 0;
                                    newSs.sBaseSetting.bFeatherMaxNew = 0;
                                }
                                break;
                            case EpsonFeatherType.Small:
                                {
                                    newSs.sBaseSetting.nFeatherPercent =
                                        isGz ? 100 : 33;
                                    newSs.sBaseSetting.bFeatherMaxNew = 0;
                                }
                                break;
                            case EpsonFeatherType.Medium:
                                {
                                    newSs.sBaseSetting.nFeatherPercent =
                                        isGz ? 200 : 66;
                                    newSs.sBaseSetting.bFeatherMaxNew = 0;
                                }
                                break;
                            case EpsonFeatherType.Large:
                                {
                                    newSs.sBaseSetting.nFeatherPercent =
                                        101;
                                    newSs.sBaseSetting.bFeatherMaxNew = 1;
                                }
                                break;
                            case EpsonFeatherType.Custom:
                                {
                                    newSs.sBaseSetting.nFeatherPercent =
                                        layer.Feather_Custom;
                                    newSs.sBaseSetting.bFeatherMaxNew = 0;
                                }
                                break;
                        }

                        break;
                    }
                }
            }

            if ("Global" == layer.FeatherType)
            {
                newSs.sBaseSetting.nFeatherType = globalFeatherType;
                newSs.sBaseSetting.fFeatherWavelength =
                    globalFeatherWavelength;
                newSs.sBaseSetting.nAdvanceFeatherPercent =
                    globalAdvanceFeatherPercent;
            }
            else
            {
                foreach (FeatherType type in Enum.GetValues(typeof(FeatherType)))
                {
                    string cmode = ResString.GetEnumDisplayName(typeof(FeatherType), type);
                    if (cmode == layer.FeatherType)
                    {
                        switch (type)
                        {
                            case FeatherType.Advance:
                            case FeatherType.Uv:
                                {
                                    newSs.sBaseSetting.nFeatherType =
                                        (byte)type;
                                    newSs.sBaseSetting.fFeatherWavelength =
                                        globalFeatherWavelength;
                                    newSs.sBaseSetting.nAdvanceFeatherPercent =
                                        (byte)layer.FeatherType_Value;
                                    break;
                                }
                            case FeatherType.Wave:
                                {
                                    newSs.sBaseSetting.nFeatherType =
                                        (byte)type;
                                    newSs.sBaseSetting.fFeatherWavelength =
                                        UIPreference.ToInchLength(AllParams.Preference.Unit,
                                            layer.FeatherType_Value);
                                    AllParams.PrinterSetting.sBaseSetting.nAdvanceFeatherPercent =
                                        globalAdvanceFeatherPercent;
                                    break;
                                }
                            default:
                                {
                                    newSs.sBaseSetting.nFeatherType =
                                        (byte)type;
                                    newSs.sBaseSetting.fFeatherWavelength =
                                        globalFeatherWavelength;
                                    AllParams.PrinterSetting.sBaseSetting.nAdvanceFeatherPercent =
                                        globalAdvanceFeatherPercent;
                                    break;
                                }
                        }
                    }
                }
            }

            //MultipleInk
            if ("Global" == layer.MultipleInk)
            {
                newSs.sBaseSetting.multipleInk = globalMultipleInk;
            }
            else
            {
                int mulInk = 1;
                int.TryParse(layer.MultipleInk, out mulInk);
                newSs.sBaseSetting.multipleInk = (byte)mulInk;
                //foreach (MultipleInkEnum place in Enum.GetValues(typeof(MultipleInkEnum)))
                //{
                //    string cmode = ResString.GetEnumDisplayName(typeof(MultipleInkEnum), place);
                //    if (cmode == layer.MultipleInk)
                //    {
                //        newSs.sBaseSetting.multipleInk = (byte) place;
                //        break;
                //    }
                //}
            }

            //Pass
            if (layer.Pass != "Global")
            {
                //TODO 照搬ToolBarSetting::OnGetPrinterSetting
                string passString = layer.Pass;
                string[] split = passString.Split(new char[] { ' ' });
                Debug.Assert(split != null && split.Length == 2);
                newSs.sFrequencySetting.nPass = Convert.ToByte(split[0]);
            }
            else
            {
                newSs.sFrequencySetting.nPass = globalPass;
            }

            //PrintMode
            if (layer.PrintMode != "Global")
            {
                foreach (XResDivMode place in Enum.GetValues(typeof(XResDivMode)))
                {
                    string cmode = ResString.GetEnumDisplayName(typeof(XResDivMode), place);
                    if (cmode == layer.PrintMode)
                    {
                        newSs.sBaseSetting.nXResutionDiv = (byte)place;
                        break;
                    }
                }
            }
            else
            {
                newSs.sBaseSetting.nXResutionDiv = globalPrintMode;
            }


            //镜像
            //newSs.sBaseSetting.bMirrorX = (BoolEx.Global == layer.MirrorX) ? GlobalMirrorX : (BoolEx.True == layer.MirrorX);

            //速度
            newSs.sFrequencySetting.nSpeed = layer.Speed != "Global"
                ? GetSpeedEnum(layer.Speed)
                : globalSpeed;
            LogWriter.WriteLog(
                new string[]
                    {
                        string.Format("Befor SetPrinterSetting {0}",
                            newSs.sBaseSetting.nLayerColorArray)
                    }, true);
            //CoreInterface.SetPrinterSetting(ref newSs);


            //UV
            int mask = 0x00;
            bool leftLightLeftPrintAvailable = (BoolEx.Global == layer.LeftLightLeftPrint)
                ? globalLeftLightLeftPrint
                : (BoolEx.True == layer.LeftLightLeftPrint);
            bool leftLightRightPrintAvailable = (BoolEx.Global == layer.LeftLightRightPrint)
                ? globalLeftLightRightPrint
                : (BoolEx.True == layer.LeftLightRightPrint);
            bool rightLightLeftPrintAvailable = (BoolEx.Global == layer.RightLightLeftPrint)
                ? globalRightLightLeftPrint
                : (BoolEx.True == layer.RightLightLeftPrint);
            bool rightLightRightPrintAvailable = (BoolEx.Global == layer.RightLightRightPrint)
                ? globalRightLightRightPrint
                : (BoolEx.True == layer.RightLightRightPrint);
            mask |= (leftLightLeftPrintAvailable ? 0x2 : 0);
            mask |= (leftLightRightPrintAvailable ? 0x1 : 0);
            mask |= (rightLightLeftPrintAvailable ? 0x8 : 0);
            mask |= (rightLightRightPrintAvailable ? 0x4 : 0);
            newSs.UVSetting.iLeftRightMask = (uint)mask;
        }

        public static void UpdatePrinterSettingByMediaMode(ref SPrinterSetting ss, MediaConfig mediaMode)
        {
            ss.sCalibrationSetting.nStepPerHead = mediaMode.BaseStep;
        }

        /// <summary>
        /// 根据速度描述字符串获得相应的枚举值
        /// 从PropertyGrid里得到的速度是由字符串描述的，所以此处需要一个转换
        /// </summary>
        /// <param name="SpeedDescription"></param>
        /// <returns></returns>
        private static SpeedEnum GetSpeedEnum(string SpeedDescription)
        {
            SpeedEnum Ret = (SpeedEnum)0;
            foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                if (cmode == SpeedDescription)
                {
                    Ret = mode;
                    break;
                }
            }
            //if ((int)Ret >= 10) Ret = (SpeedEnum)((int)Ret - 10);

            return Ret;

        }
    }
    public class PreInterface
    {
        [DllImport("LangUsb.dll")]
        public static extern int GetEpsonEP0Cmd(byte cmd, byte[] buffer, ref uint bufferSize, ushort value, ushort index);
    }
    public class SerialFunction
    {
        unsafe public static string CovertAnsiToString(byte[] array)
        {
            string info = "";
            fixed (byte* bytePtr = array)
            {
                IntPtr sPtr = new IntPtr(bytePtr);
                info = Marshal.PtrToStringAnsi(sPtr);
            }
            return info;

        }
        public static string VersionToString(int Version)
        {
            int ProductMajorPart = (Version & 0xff00) >> 8;
            int ProductMinorPart = (Version & 0xff);

            return ProductMajorPart.ToString("X")
                + "."
                + ProductMinorPart.ToString("X");
        }
        public static Bitmap CreateImageWithImageInfo(SPrtImageInfo imageInfo)
        {
            if (imageInfo.nImageData != IntPtr.Zero)
            {
                SPrtImagePreview previewData = (SPrtImagePreview)Marshal.PtrToStructure((IntPtr)imageInfo.nImageData, typeof(SPrtImagePreview));

                return CreateImageWithPreview(previewData);
            }
            else
            {
                return null;
            }
        }

        public static Bitmap CreateImageWithPreview(SPrtImagePreview previewData)
        {
            SPrinterProperty sProperty = new SPrinterProperty();
            CoreInterface.GetSPrinterProperty(ref sProperty);
            byte[] defaultChannelOrder = sProperty.eColorOrder;

            if (previewData.nImageType != 0)
            {

                Bitmap image = null;
                try
                {
                    MemoryStream mem = new MemoryStream(previewData.nImageData, 0, previewData.nImageDataSize);

                    image = new Bitmap(mem);

                    mem = null;
                }
                catch
                {
                    if (image != null)
                    {
                        image.Dispose();
                        image = null;
                    }
                    return image;
                }

                return image;
            }
            else if (defaultChannelOrder.Length >= previewData.nImageColorNum && previewData.nImageColorNum > 0)
            {
                byte[] channels = new byte[previewData.nImageColorNum];

                for (int i = 0; i < previewData.nImageColorNum; i++)
                {
                    channels[i] = defaultChannelOrder[i];
                }

                Bitmap image = null;

                CMYKToImage(previewData.nImageData, previewData.nImageWidth, previewData.nImageHeight, channels, out image);

                return image;
            }
            else
            {
                //Debug.Assert(false,"The color channel is out of range!");

                return null;
            }
        }
        private unsafe static bool CMYKToImage(byte[] inputData, int w, int h, byte[] colorMode, out Bitmap image)
        {
            image = null;
            try
            {
                if (w <= 0 || h <= 0)
                    return false;
                image = new Bitmap(w, h, PixelFormat.Format24bppRgb);
                BitmapData data = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                byte* buf = (byte*)data.Scan0;

                byte c, m, y, k, lc, lm, r, g, b;
                c = m = y = k = 0;
                int offsetC, offsetM, offsetY, offsetK, offsetLc, offsetLm;
                offsetC = offsetM = offsetY = offsetK = offsetLc = offsetLm = 0;
                int colorNumber = colorMode.Length;
                for (int i = 0; i < colorNumber; i++)
                {
                    switch ((ColorEnum)colorMode[i])
                    {
                        case ColorEnum.Cyan:
                            offsetC = i;
                            break;
                        case ColorEnum.Magenta:
                            offsetM = i;
                            break;
                        case ColorEnum.Yellow:
                            offsetY = i;
                            break;
                        case ColorEnum.Black:
                            offsetK = i;
                            break;
                        case ColorEnum.LightCyan:
                            offsetLc = i;
                            break;
                        case ColorEnum.LightMagenta:
                            offsetLm = i;
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(true, "Unkown color channel!!!");
                            break;
                    }
                }

                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        r = g = b = 0;
                        switch (colorNumber)
                        {
                            case 3:
                                c = inputData[3 * (i + j * w) + offsetC];
                                m = inputData[3 * (i + j * w) + offsetM];
                                y = inputData[3 * (i + j * w) + offsetY];
                                k = 0;
                                break;
                            case 6:
                                c = inputData[6 * (i + j * w) + offsetC];
                                m = inputData[6 * (i + j * w) + offsetM];
                                y = inputData[6 * (i + j * w) + offsetY];
                                k = inputData[6 * (i + j * w) + offsetK];
                                lc = inputData[6 * (i + j * w) + offsetLc];
                                lm = inputData[6 * (i + j * w) + offsetLm];

                                c = (byte)Math.Min(byte.MaxValue, c + lc * 0.3);
                                m = (byte)Math.Min(byte.MaxValue, m + lm * 0.3);

                                break;
                            case 4:
                                c = inputData[4 * (i + j * w) + offsetC];
                                m = inputData[4 * (i + j * w) + offsetM];
                                y = inputData[4 * (i + j * w) + offsetY];
                                k = inputData[4 * (i + j * w) + offsetK];
                                break;
                            default:
                                System.Diagnostics.Debug.Assert(true, "Unkown color mode!!!");
                                break;
                        }
                        r = (byte)(byte.MaxValue - Math.Min(c + k, byte.MaxValue));
                        g = (byte)(byte.MaxValue - Math.Min(m + k, byte.MaxValue));
                        b = (byte)(byte.MaxValue - Math.Min(y + k, byte.MaxValue));
                        buf[3 * i + data.Stride * j] = b;
                        buf[3 * i + 1 + data.Stride * j] = g;
                        buf[3 * i + 2 + data.Stride * j] = r;

                    }
                }
                image.UnlockBits(data);
                return true;
            }
            catch
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                return false;
            }
        }


    }

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////
    /// 
    /// </summary>
    //?????????????????????????????????????????????????????????????????????????????????????????????
    //Below class need discuss Whether support One Config File Format 
    public class ObjectContainer
    {
        public object Object;
        public Array ParentArray;
        public FieldInfo Info;
        public int Index;
        public Type ObjType;
    }
    public enum UserPermission : int
    {
        Operator = 0,
        FactoryUser = 1,
        SupperUser = 0x7FFFFFFF
    }

    public class LayoutHelper
    {
        /// <summary>
        /// 俩个控件x坐标最小距离[width+margin]
        /// </summary>
        public const int MinSpace = 55;
        public static void CalculateHorNum(int num, int start_x, int end_x, ref int width, out int space)
        {
            const int max_space = 100;
            const int m_HorGap = 4;
            const int m_Margin = 8;

            space = m_HorGap + width;
            if (num > 1)
                space = (end_x - start_x - m_Margin - width) / (num - 1);
            if ((width + m_HorGap) > space)
            {
                width = (end_x - start_x - m_HorGap * (num - 1) - m_HorGap) / num;
                space = width + m_HorGap;
            }
            space = Math.Min(space, max_space);
        }
    }


    public struct TcpipCmdPara
    {
        public int CmdType;
        public string PrtPath;
        public bool ReversePrint;//0为正向1为反向
        public byte PrintPlatform;//AXIS_X = 0x01; //  第一个工作台;AXIS_4 = 0x08; //  第二个工作台
        /// <summary>
        /// 全印双平台
        /// 1：表示图像中只有彩色，2：表示作业中既有彩色也有白色//
        /// </summary>
        public byte ColorType;//
        /// <summary>
        /// 全印双平台
        /// </summary>
        public int Jobid;
    }

    public class CLockQueue
    {
        private Queue m_pQueue;

        private Mutex m_hLockMutex;
        private ManualResetEvent m_hZeroEvent;

        public CLockQueue()
        {
            m_pQueue = new Queue();

            m_hLockMutex = new Mutex();
            m_hZeroEvent = new ManualResetEvent(false);
        }


        public virtual int GetCount()
        {
            m_hLockMutex.WaitOne();
            int size = (int)m_pQueue.Count;
            m_hLockMutex.ReleaseMutex();
            return size;
        }
        int bandCount = 0;
        public virtual object GetFromQueue()
        {
            //			LogWriter.WriteLog(new string[]{"enter get points from queue."},true);
            object ret = 0;
            m_hLockMutex.WaitOne();
            int size = (int)m_pQueue.Count;
            if (size == 0)
            {
                //				LogWriter.WriteLog(new string[]{"waiting for points..."},true);
                m_hZeroEvent.Reset();
                m_hLockMutex.ReleaseMutex();
                m_hZeroEvent.WaitOne();
                m_hLockMutex.WaitOne();
            }
            ret = m_pQueue.Dequeue();
            m_hLockMutex.ReleaseMutex();
            //			LogWriter.WriteLog(new string[]{"exit get points from queue."},true);

            return ret;
        }
        public virtual object PeekFromQueue()
        {
            m_hLockMutex.WaitOne();
            int size = (int)m_pQueue.Count;
            object ret = null;
            if (size == 0)
            {
                ret = null;
            }
            else
                ret = m_pQueue.Peek();
            m_hLockMutex.ReleaseMutex();
            return ret;
        }
        public virtual void PutInQueue(object a)
        {
            m_hLockMutex.WaitOne();
            int size = (int)m_pQueue.Count;
            m_pQueue.Enqueue(a);
            m_hZeroEvent.Set();
            m_hLockMutex.ReleaseMutex();
        }

        public virtual void ClearQueue()
        {
            m_hLockMutex.WaitOne();
            m_pQueue.Clear();
            m_hZeroEvent.Reset();
            m_hLockMutex.ReleaseMutex();
            bandCount = 0;
        }

        public virtual void Exit()
        {
            TcpipCmdPara array = new TcpipCmdPara();
            array.CmdType = -1;
            PutInQueue(array);
            //m_hZeroEvent.Set();
        }
    }
    public class ConvertCrossStitchPrt
    {

        static private double GetAngleCoor(double row, double col)
        {
            const int h = 719;
            const double deta = 14;
            double new_x = 0;
            if (deta > 0)
                new_x = col + (double)deta / (double)h * row;
            else
                new_x = col - (double)deta / (double)h * row;

            return new_x;
        }
    }

    public enum PrinterSettingCmd
    {
        MultiLayerCfg = 1,
        HorizonCalValueLeft,
        HorizonCalValueRight,
        HeadChannelSwitch,
        GroupCmdLeft = 6,
        GroupCmdRight = 7,
        OverLapTotalNozzle,
        OverLapUpWasteNozzle,
        OverLapDownWasteNozzle,
        OverLapUpPercent,
        OverLapDownPercent,

        VerticalOffset = 13,
        //add new cmd in hear
        CMD_Last
    };
}
