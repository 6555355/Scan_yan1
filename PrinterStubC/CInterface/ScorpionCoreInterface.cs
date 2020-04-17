using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using PrinterStubC.Interface;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace BYHXPrinterManager
{
    public class ScorpionCoreInterface
    {
        public const ushort OFFHEATER = 0xffff;
        public const byte PIPE_BASE_CMD = 0xc5;
        /// <summary>
        /// 2cm
        /// </summary>
        public const int MAX_OFFSET = 2000; // 2cm  
        //					const int ScorpionResX = 2540;
        //public const int SERVICE_STATION_POS = 402000 - 6000 - MAX_OFFSET;
        //public const int ORIGIN_POSITION = -6000 + MAX_OFFSET;

        public static void GetScorpioColorMode()
        {
            // 发送获取信号状态命令
            SendScorpionEp6Cmd(ScorpionCmd.ColorModeSwitch, 'G');
        }

        public static  void GetScorpionSingleStatus()
        {
            // 发送获取信号状态命令
            SendScorpionEp6Cmd(ScorpionCmd.SingleStatus,'G');
        }

        public static  void GetScorpionMediaUpDown()
        {
            // 发送获取当前卷纸模式命令
            SendScorpionEp6Cmd(ScorpionCmd.MediaUpDown,'G');
        }

        public static  void GetScorpionHeaterCurSet()
        {
            // 发送获取当前目标温度命令
            SendScorpionEp6Cmd(ScorpionCmd.Heater,'G');
        }

        public static  void GetScorpionHeaterCur()
        {
            // 发送获取当前目标温度命令
            SendScorpionEp6Cmd(ScorpionCmd.Heater,'N');
        }

        public static  void GetScorpionFlushWiping()
        {
            // 发送获取当前目标温度命令
            SendScorpionEp6Cmd(ScorpionCmd.FlushWiping,'G');
        }

        /// <summary>
        /// 1.31.	状态查询
        /// </summary>
        /// <param name="type">1:CoolingOff_In 2: FrontSettingOK_In 3: RearSettingOK_In</param>
        public static void GetScorpionCoolFrontRearStatus(byte type)
        {
            byte[] val = new byte[5];
            int i = 0;
            val[i++] = (byte)val.Length;
            val[i++] = PIPE_BASE_CMD;
            val[i++] = (byte)ScorpionCmd.CoolFrontRear;
            val[i++] = Convert.ToByte('G');
            val[i++] = type;
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, 0, 0);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", ScorpionCmd.CoolFrontRear, 'G'));
            }
        }


        public static void SendScorpionEp6Cmd(ScorpionCmd cmd,char subcmd)
        {
            byte[] val = new byte[4];
            int i = 0;
            val[i++] = (byte)val.Length;
            val[i++] = PIPE_BASE_CMD;
            val[i++] = (byte)cmd;
            val[i++] =Convert.ToByte(subcmd);
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, 0, 0);
            if (ret == 0)
            {
                Debug.Assert(false,string.Format("Send {0}_{1} cmd fialed!",cmd,subcmd));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="subcmd"></param>
        /// <param name="data"></param>
        public static void SetScorpionSettingByEp6(ScorpionCmd cmd,char subcmd,byte[] data)
        {
#if true
            byte[] val = new byte[4 + data.Length];
            int i = 0;
            val[i++] = (byte)val.Length;
            val[i++] = PIPE_BASE_CMD;
            val[i++] = (byte)cmd;
            val[i++] =Convert.ToByte(subcmd);
            for(int j = 0; j < data.Length; j++)	
                val[i++] =data[j];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x80, val, ref bufsize, 0, 0);
            if (ret == 0)
            {
                Debug.Assert(false,string.Format("Send {0}_{1} cmd fialed!",cmd,subcmd));
            }
#else
            uint bufsize = (uint)data.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x90, data, ref bufsize, 0, 0x4000);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", cmd, subcmd));
            }
#endif
        }

        public static void GetScorpionParam(out ScorpionParam param)
        {
            param = new ScorpionParam();
            byte[] val = new byte[Marshal.SizeOf(param) + 2];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x90, val, ref bufsize, 0, 0x4002);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x90, 0x4002));
            }
            byte[] buf = new byte[Marshal.SizeOf(param)];
            Buffer.BlockCopy(val,2,buf,0,buf.Length);
            param = (ScorpionParam)SerializationUnit.BytesToStruct(buf, param.GetType());
        }

        public static void SetScorpionParam(ScorpionParam param)
        {
            byte[] val = SerializationUnit.StructToBytes(param);
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x90, val, ref bufsize, 0, 0x4002);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x90, 0x4002));
            }
        }

        /// <summary>
        /// 获取Aojet专用参数
        /// </summary>
        /// <param name="param"></param>
        public static bool GetAojetParam(out AojetParam param)
        {
            param = new AojetParam();
            byte[] val = new byte[Marshal.SizeOf(param) + 2];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x70);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x92, 0x70));
                return false;
            }
            byte[] buf = new byte[Marshal.SizeOf(param)];
            Buffer.BlockCopy(val, 2, buf, 0, buf.Length);
            param = (AojetParam)SerializationUnit.BytesToStruct(buf, param.GetType());
            string flag=Encoding.ASCII.GetString(BitConverter.GetBytes(param.Flag));
            if (flag != "AJET")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置Aojet专用参数
        /// </summary>
        /// <param name="param"></param>
        public static void SetAojetParam(AojetParam param)
        {
            byte[] val = SerializationUnit.StructToBytes(param);
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, val, ref bufsize, 0, 0x70);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x92, 0x70));
            }
        }


        /// <summary>
        /// 获取通用Z轴参数
        /// </summary>
        /// <param name="param"></param>
        public static bool GetByhxZMoveParam(out ByhxZMoveParam param)
        {
            param = new ByhxZMoveParam();
            byte[] val = new byte[Marshal.SizeOf(param) + 2];
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 2);
            if (ret == 0)
            {
                //Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x82, 2));
                return false;
            }
            byte[] buf = new byte[Marshal.SizeOf(param)];
            Buffer.BlockCopy(val, 2, buf, 0, buf.Length);
            param = (ByhxZMoveParam)SerializationUnit.BytesToStruct(buf, param.GetType());
            return true;
        }
        /// <summary>
        /// 设置通用Z轴参数
        /// </summary>
        /// <param name="param"></param>
        public static void SetByhxZMoveParam(ByhxZMoveParam param)
        {
            byte[] val = SerializationUnit.StructToBytes(param);
            uint bufsize = (uint)val.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x82, val, ref bufsize, 0, 2);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", 0x82, 2));
            }
        }

        public static void SetPumpInkPause(bool bPause)
        {
            byte[] data = new byte[1] {(byte) (bPause ? 1 : 0)};
            uint bufsize = (uint) data.Length;

            int ret = CoreInterface.SetEpsonEP0Cmd(0x90, data, ref bufsize, 0, 0x4001);
            if (ret == 0)
            {
                Debug.Assert(false, string.Format("Send {0}_{1} cmd fialed!", ScorpionCmd.SingleStatus, 'S'));
            }
        }

        public static void SetScorpionHeater(HeaterSetting heater)
        {
            // 发送设置当前目标温度命令
            byte[] val = new byte[8];
            int i = 0;
            ushort halogen, carrigage, dry, printing;
            halogen = (ushort)(heater.HalogenTargetTemp * 10f);
            if (heater.HalogenHeatOff)
                halogen = (ushort)(halogen | (1 << 15));
            carrigage = (ushort)(heater.CarriageTargetTemp * 10f);
            if (heater.CarriageHeatOff)
                carrigage = (ushort)(carrigage | (1 << 15));
            dry = (ushort)(heater.DryTargetTemp * 10f);
            if (heater.DryHeatOff)
                dry = (ushort)(dry | (1 << 15));
            printing = (ushort)(heater.PrintingTargetTemp * 10f);
            if (heater.PrintingHeatOff)
                printing = (ushort)(printing | (1 << 15));

            byte[] tempval = BitConverter.GetBytes(halogen);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(carrigage);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(dry);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(printing);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            SetScorpionSettingByEp6(ScorpionCmd.Heater, 'S', val);
        }

        public static void SetScorpionFlushWiping(FlushWipingSetting flushWiping)
        {
            // 发送设置设置头清洗参数
            byte[] val = new byte[22];
            int i = 0;
            byte[] tempval = BitConverter.GetBytes(flushWiping.FirstSolutionPump);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.FirstSolutionAir);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.CappingPlateUp);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.FlushTimes);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.Time);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.Delay);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.WipingTimes);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.SecondSolutionPump);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            i += tempval.Length;
            tempval = BitConverter.GetBytes(flushWiping.SecondSolutionAir);
            Buffer.BlockCopy(tempval, 0, val, i, tempval.Length);
            ScorpionCoreInterface.SetScorpionSettingByEp6(ScorpionCmd.FlushWiping, 'S', val);
        }

        public static void SetVacuumHalogen(VacuumHalogenSections vacuumHalogen)
        {
            byte[] data = new byte[5];
            byte temp = 0;
            if (vacuumHalogen.HalogenSection0On)
                temp |= 1;
            if (vacuumHalogen.HalogenSection1On)
                temp |= (1 << 1);
            if (vacuumHalogen.HalogenSection2On)
                temp |= (1 << 2);
            data[0] = temp; // Bit2~0分别对应3段
            data[1] = (byte) vacuumHalogen.VacuumMode;
            temp = 0;
            if (vacuumHalogen.VacuumSection1On)
                temp |= 1;
            if (vacuumHalogen.VacuumSection2On)
                temp |= (1 << 1);
            if (vacuumHalogen.VacuumSection3On)
                temp |= (1 << 2);
            data[2] = temp;// 
            Buffer.BlockCopy(BitConverter.GetBytes(vacuumHalogen.VacuumPower), 0, data, 3, 2);

            SetScorpionSettingByEp6(ScorpionCmd.VacuumHalogen, 'S', data);
        }

        private static bool _bAbortedRun = false;
        public static void ContinueRunSomeDistanceOnPrintEnded(int speed, float flen)
        {
            _bAbortedRun = false;
            // 打开烘干
            SendScorpionEp6Cmd(ScorpionCmd.OpenCloseDry, 'S');

            JetStatusEnum status = CoreInterface.GetBoardStatus();

            // 开始移动
            int len = 0;
            MoveDirectionEnum dir = MoveDirectionEnum.Down;
            float pulse = CoreInterface.GetfPulsePerInchY(0);
            len = Convert.ToInt32(pulse * flen);
            CoreInterface.MoveCmd((int) dir, len, speed);

            int delta = 500;
            Thread.Sleep(delta);
            JetStatusEnum temp = CoreInterface.GetBoardStatus();
            int timeout = 10000;// 10s
            while (temp == status || temp != JetStatusEnum.Moving)
            {
                Thread.Sleep(delta);
                temp = CoreInterface.GetBoardStatus();
                timeout -= delta;
                if (timeout <= 0 || _bAbortedRun)
                {
                    break;
                }
            }

            //timeout = 10000;// 10s
            while (temp == JetStatusEnum.Moving)
            {
                Thread.Sleep(delta);
                temp = CoreInterface.GetBoardStatus();
                if (_bAbortedRun)
                {
                    break;
                }
            }

            // 关闭烘干
            SendScorpionEp6Cmd(ScorpionCmd.OpenCloseDry, 'E');
        }

        public static void StopRunSomeDistanceOnPrintEnded()
        {
            _bAbortedRun = true;
            CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
            // 关闭烘干
            SendScorpionEp6Cmd(ScorpionCmd.OpenCloseDry, 'E');
        }

        public static void GetDaControl()
        {
            byte[] val = new byte[1];
            val[0] = 1;
            SetScorpionSettingByEp6(ScorpionCmd.DaOutControl, 'G', val);
        }

        public static void SetDaControl(byte[] buf)
        {
            byte[] val = new byte[buf.Length +1];
            val[0] = 1;
            Buffer.BlockCopy(buf,0,val,1,buf.Length);
            SetScorpionSettingByEp6(ScorpionCmd.DaOutControl, 'S', val);
        }


        public static bool GetIsSupportNewErrorCode()
        {
            byte[] data = new byte[6];
            uint bufferSize = (uint)data.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x54, data, ref bufferSize, 0, 0x02);
            if (ret != 0)
            {
                //第3(前2个字节跳过)个字节的第2个bit置位，表示arm支持扩展错误格式
                return (data[4] & (1 << 1)) == 2;
            }
            return false;
        }
        /// <summary>
        /// 获取板卡是否支持变革revo_e喷头限制
        /// </summary>
        /// <returns></returns>
        public static bool GetSupportInwearHeadLimite()
        {
            byte[] val = new byte[64];
            uint bufsize = (uint)val.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x54, val, ref bufsize, 0, 0x2) != 0)
            {
                if ((val[4] & (1 << 2)) == 4)
                    return true;
            }
            return false;
        }
        public static void SetFwSupportErrorCode(ErrorFormat errorFormat)
        {
            byte[] data = new byte[1];
            data[0] = (byte)errorFormat;
            uint bufferSize = (uint)data.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x54, data, ref bufferSize, 0, 0x03);
        }
        /// <summary>
        /// 获取主板是否支持16位温度
        /// </summary>
        /// <returns></returns>
        public static bool GetIsSupport16bitTemp()
        {
            byte[] data = new byte[6];
            uint bufferSize = (uint)data.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x54, data, ref bufferSize, 0, 0x02);
            if (ret != 0)
            {
                //第3(前2个字节跳过)个字节的第2个bit置位，表示arm支持扩展错误格式
                return (data[4] & (1 << 4)) >0;
            }
            return false;
        }
    }
}