using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using TcpIpBase;
using WAF_OnePass.Domain.Utility;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;
using PrinterStubC.Utility;
using TcpIpBase.Byhx;

namespace BYHXPrinterManager.Main
{
    partial class MainForm
    {
        #region FHZL tcpip协议支持
#if !FHZL3D
        private TcpIpCmdByhx _curCmdFhzl = TcpIpCmdByhx.GetInfo;
        void remoteControlServerTask_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                ProtocolByhx protocol = (ProtocolByhx)e.UserState;
                string protocolstr = string.Format("protocol.Cmd={0},protocol.SubCmd={1},protocol.Parameter={2}", protocol.Cmd, protocol.SubCmd, protocol.Parameter);
                LogWriter.SaveOptionLog(protocolstr);
                switch (e.ProgressPercentage)
                {
                    case -1:
                        {
                            break;
                        }
                    case 0:
                        {
                            this.m_PrintInformation.PrintString(protocolstr, UserLevel.manager, ErrorAction.UserResume);
                            switch (protocol.Cmd)
                            {
                                case TcpIpCmdByhx.EndPackage:
                                    {
                                        ProcessEndPackageCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.ResponsePackage:
                                    break;
                                case TcpIpCmdByhx.GetInfo:
                                    {
                                        ProcessGetInfoCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.OprationCmd:
                                    {
                                        ProcessOprationCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.PrintParameter:
                                    {
                                        ProcessPrintParameterCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.PrinterSetting:
                                    {
                                        ProcessPrinterSettingCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.ColorBarSetting:
                                    {
                                        ProcessColorBarSettingCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.SpraySetting:
                                    {
                                        ProcessSpraySettingCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.PrintDir:
                                    {
                                        ProcessPrintDirCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.PreferenceSetting:
                                    {
                                        ProcessPreferenceSettingCmd(protocol);
                                        break;
                                    }
                                case TcpIpCmdByhx.CaribrationHor:
                                case TcpIpCmdByhx.CaribrationStep:
                                    //case TcpIpCmdByhx.CaribrationVer:
                                    //case TcpIpCmdByhx.CaribrationOverLap:
                                    {
                                        ProcessCaribrationCmd(protocol);
                                        break;
                                    }
                                //case TcpIpCmdByhx.FactoryDebug:
                                //    {
                                //        ProcessFactoryDebugCmd(protocol);
                                //        break;
                                //    }
                                case TcpIpCmdByhx.SystemSetting:
                                    {
                                        ProcessSystemSettingCmd(protocol);
                                        break;
                                    }
                                //default:
                                //    throw new ArgumentOutOfRangeException();
                            }

                            if (_curCmdFhzl != protocol.Cmd && protocol.Cmd != TcpIpCmdByhx.GetInfo)
                                _curCmdFhzl = protocol.Cmd;
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
        /// <summary>
        /// 处理系统设置命令
        /// </summary>
        /// <param name="protocol"></param>
        private void ProcessSystemSettingCmd(ProtocolByhx protocol)
        {
            switch ((SystemSettingSubCmd)protocol.SubCmd)
            {
                case SystemSettingSubCmd.Exit: //1:关闭程序	1
                    {
                        End();
                        Application.Exit();
                        break;
                    }
                case SystemSettingSubCmd.FormMinimized: //2: 程序最小化到托盘	1
                    {
                        this.WindowState = FormWindowState.Minimized;
                        break;
                    }
            }
        }

        private void ProcessEndPackageCmd(ProtocolByhx protocol)
        {
            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, (byte)TcpIpCmdByhx.ResponsePackage, protocol.Parameter);// 偷懒直接返回了数量,应该自己计数...
            remoteControlServer.SendCmdReturnValue(returnFhzl);
            switch (_curCmdFhzl)
            {
                case TcpIpCmdByhx.EndPackage:
                case TcpIpCmdByhx.ResponsePackage:
                    {
                        break;
                    }
                case TcpIpCmdByhx.GetInfo:
                    {
                        break;
                    }
                case TcpIpCmdByhx.OprationCmd:
                    {
                        break;
                    }
                case TcpIpCmdByhx.PrintParameter:
                case TcpIpCmdByhx.PrinterSetting:
                case TcpIpCmdByhx.ColorBarSetting:
                case TcpIpCmdByhx.SpraySetting:
                case TcpIpCmdByhx.PrintDir:
                case TcpIpCmdByhx.CaribrationHor:
                case TcpIpCmdByhx.CaribrationStep:
                    //case TcpIpCmdByhx.CaribrationVer:
                    //case TcpIpCmdByhx.CaribrationOverLap:
                    {
                        OnPrinterSettingChange(m_allParam.PrinterSetting);
                        CoreInterface.SetPrinterSetting(ref m_allParam.PrinterSetting);
                        break;
                    }
                case TcpIpCmdByhx.PreferenceSetting:
                    {
                        OnPreferenceChange(m_allParam.Preference);
                        break;
                    }
                //case TcpIpCmdByhx.FactoryDebug:
                //    break;
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 处理获取信息命令
        /// </summary>
        /// <param name="protocol"></param>
        private void ProcessGetInfoCmd(ProtocolByhx protocol)
        {
            switch ((GetInfoSubCmd)protocol.SubCmd)
            {
                case GetInfoSubCmd.CurPos: //1：当前X、Y轴坐标值
                    {
                        int x = 0, y = 0, z = 0;
                        CoreInterface.QueryCurrentPosN(ref x, ref y, ref z);
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                          (byte)GetInfoSubCmd.CurPos, string.Format("({0},{1},{2})", x, y, z));
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.Status: //2：设备状态
                    {
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                         (byte)GetInfoSubCmd.Status, ((int)CoreInterface.CurBoardStatus).ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.ErrorCode: //3：错误代码
                    {
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                          (byte)GetInfoSubCmd.ErrorCode, _errorCode.ToString("X8"));
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.JobSize: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //4：图像尺寸（单位：cm）	格式：(X,Y)
                            string jobsize = string.Format("({0},{1})",
                                UIPreference.ToDisplayLength(UILengthUnit.Centimeter, job.JobSize.Width),
                                UIPreference.ToDisplayLength(UILengthUnit.Centimeter, job.JobSize.Height));
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, jobsize);
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.JobRes: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //5：图像分辨率（单位：DPI）	格式：(X,Y)
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                                string.Format("({0},{1})", job.ResolutionX, job.ResolutionY));
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.PrintMode: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //6：打印模式	参数未知
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, job.ColorDeep.ToString());
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.PrintPass: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //7：扫描次数（每Pass的扫描次数）
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, job.PassNumber.ToString());
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.ScanMode: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //8：扫描模式	单向值为1，双向值为2
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, (job.PrintingDirection + 1).ToString());
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.JobPath: //4：打印任务详细信息
                    {
                        UIJob job = m_JobListForm.GetJobByPath(protocol.Parameter);
                        if (job != null)
                        {
                            //9：任务地址	文件路径
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, job.FileLocation);
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        else
                        {
                            ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd, "");
                            remoteControlServer.SendCmdReturnValue(returnFhzl);
                        }
                        break;
                    }
                case GetInfoSubCmd.CariStepPass: //4：打印任务详细信息
                case GetInfoSubCmd.ToolbarPass: //4：打印任务详细信息
                    {
                        //13：打印栏-Pass数	数值
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            m_allParam.PrinterSetting.sFrequencySetting.nPass.ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.CariStep: //4：打印任务详细信息
                case GetInfoSubCmd.ToolbarStepAdjust: //4：打印任务详细信息
                    {
                        int step = 0;
                        SPrinterSetting ss = m_allParam.PrinterSetting;
                        if (this.m_bDuringPrinting)
                        {
                            SPrtFileInfo jobInfo = new SPrtFileInfo();
                            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
                            {
                                int realpass = jobInfo.sFreSetting.nPass;
                                if (m_allParam.PrinterSetting.nKillBiDirBanding != 0)
                                    step = ss.sCalibrationSetting.nPassStepArray[(realpass + 1) / 2 - 1];
                                else
                                    step = ss.sCalibrationSetting.nPassStepArray[realpass - 1];
                            }
                        }
                        else
                        {
                            if (ss.nKillBiDirBanding != 0)
                                step = ss.sCalibrationSetting.nPassStepArray[(ss.sFrequencySetting.nPass + 1) / 2 - 1];
                            else
                                step = ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass - 1];
                        }

                        //13：打印栏-Pass数	数值
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            step.ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.ToolbarSpeed: //14：打印栏-喷车速度	高中低速（值为1-3）
                    {
                        SPrinterSetting ss = m_allParam.PrinterSetting;
                        int speed = (int)ss.sFrequencySetting.nSpeed + 1;

                        //13：打印栏-Pass数	数值
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            speed.ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.PlatformDistanceY: //
                    {
                        SPrinterSetting ss = m_allParam.PrinterSetting;
                        float step = UIPreference.ToDisplayLength(UILengthUnit.Centimeter, ss.sExtensionSetting.FlatSpaceY);
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            step.ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.JobList: //
                    {
                        SPrinterSetting ss = m_allParam.PrinterSetting;
                        string strJoblist = string.Empty;
                        List<UIJob> jobList = m_JobListForm.GetJobList();
                        for (int i = 0; i < jobList.Count; i++)
                        {
                            strJoblist += jobList[i].FileLocation;
                            strJoblist += ",";
                        }
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            strJoblist);
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                case GetInfoSubCmd.PrintPercent:
                    {
                        ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage, protocol.SubCmd,
                            _printPercent.ToString());
                        remoteControlServer.SendCmdReturnValue(returnFhzl);
                        break;
                    }
                //    case GetInfoSubCmd.StatusEx:
                //{
                //    string
                //        paras = string.Format("{0},{1},{2},{3},{4}", curStatus, _errorCode, m_PrintPercent, m_UpdatePercent, m_allParam.CurPrintPlatForm);
                //    ProtocolByhx returnFhzl = new ProtocolByhx(TcpIpCmdByhx.ResponsePackage,
                //        (byte) GetInfoSubCmd.StatusEx, paras);
                //    remoteControlServer.SendCmdReturnValue(returnFhzl);
                //    break;
                //}
            }
        }

        /// <summary>
        /// 处理操作命令
        /// </summary>
        /// <param name="protocol"></param>
        private void ProcessOprationCmd(ProtocolByhx protocol)
        {
            switch ((OprationCmdSubCmd)protocol.SubCmd)
            {
                case OprationCmdSubCmd.AddJob:
                    {
                        m_JobListForm.AddJobs(new string[] { protocol.Parameter });
                        break;
                    }
                case OprationCmdSubCmd.DelJob:
                    {
                        m_JobListForm.DeleteJob(protocol.Parameter);
                        break;
                    }
                case OprationCmdSubCmd.PrintJob:
                    {
                        m_JobListForm.PrintJob(protocol.Parameter);
                        break;
                    }
                case OprationCmdSubCmd.PauseResume:
                    {
                        CoreInterface.Printer_PauseOrResume();
                        break;
                    }
                case OprationCmdSubCmd.StopPrint:
                    {
                        m_JobListForm.TerminatePrintingJob(false);
                        break;
                    }
                case OprationCmdSubCmd.SetOriginX:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.SetOrigin;
                        cmdvalue = (int)AxisDir.X;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                case OprationCmdSubCmd.SetOriginY:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.SetOrigin;
                        cmdvalue = (int)AxisDir.Y;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                //case OprationCmdSubCmd.PrintCaribration:
                //    {
                //        int caricmd = (int)CalibrationCmdEnum.CheckNozzleCmd;
                //        bool ret = int.TryParse(protocol.Parameter, out caricmd);
                //        if (!ret)
                //            break;
                //        CoreInterface.SendCalibrateCmd(caricmd, 0, ref this.m_allParam.PrinterSetting);
                //        break;
                //    }
                case OprationCmdSubCmd.StartMoveTest:
                    {
                        //格式：(P,D,S)  P为脉冲数，D为方向（值为1-4），S位速度（值为1-7）
                        string[] paras = protocol.Parameter.Substring(1, protocol.Parameter.Length - 2).Split(',');
                        int speed = 4;
                        int dir = 0;
                        int len = 0;
                        bool ret = false;
                        if (paras.Length > 0)
                        {
                            ret = int.TryParse(paras[0], out len);
                        }
                        if (!ret)
                            break;
                        if (paras.Length > 1)
                            ret = int.TryParse(paras[1], out dir);
                        if (!ret)
                            break;
                        if (paras.Length > 2)
                            ret = int.TryParse(paras[2], out speed);
                        if (!ret)
                            break;
                        // 老的移动
                        const int port = 1;
                        const byte PRINTER_PIPECMDSIZE = 26;
                        byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                        //First Send Begin Updater
                        m_pData[0] = 6 + 2;
                        m_pData[1] = 0x31; //Move cmd

                        m_pData[2] = (byte)dir; //Move cmd
                        m_pData[3] = (byte)speed; //Move cmd
                        m_pData[4] = (byte)(len & 0xff); //Move cmd
                        m_pData[5] = (byte)((len >> 8) & 0xff); //Move cmd
                        m_pData[6] = (byte)((len >> 16) & 0xff); //Move cmd
                        m_pData[7] = (byte)((len >> 24) & 0xff); //Move cmd

                        CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
                        break;
                    }
                case OprationCmdSubCmd.StopMoveTest:
                    {
                        const int port = 1;
                        const byte PRINTER_PIPECMDSIZE = 26;
                        byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                        //First Send Begin Updater
                        m_pData[0] = 2;
                        m_pData[1] = 0x3a; //Move cmd
                        CoreInterface.SetPipeCmdPackage(m_pData, m_pData[0], port);
                        break;
                    }
                case OprationCmdSubCmd.StartMove:
                    {
                        int dir = (int)MoveDirectionEnum.Left;
                        bool ret = int.TryParse(protocol.Parameter, out dir);
                        if (ret)
                        {
                            dir = (int)PubFunc.GetRealMoveDir((MoveDirectionEnum)dir, m_allParam.PrinterProperty, m_allParam.Preference);
                            int speed = GetSpeedWithDir((MoveDirectionEnum)dir);
                            CoreInterface.MoveCmd(dir, 0, speed);
                        }
                        break;
                    }
                case OprationCmdSubCmd.StopMove:
                    {
                        CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
                        break;
                    }
                case OprationCmdSubCmd.StartMoveLen:
                    {
                        //格式：(P,D,S)  P为脉冲数，D为方向（值为1-4），S位速度（值为1-7）
                        string[] paras = protocol.Parameter.Substring(1, protocol.Parameter.Length - 2).Split(',');
                        int speed = 4;
                        int dir = 0;
                        float flen = 0;
                        int len = 0;
                        bool ret = false;
                        if (paras.Length > 0)
                            ret = int.TryParse(paras[0], out len);
                        if (!ret)
                            break;
                        if (paras.Length > 1)
                            ret = int.TryParse(paras[1], out dir);
                        if (!ret)
                            break;
                        if (paras.Length > 2)
                            ret = int.TryParse(paras[2], out speed);
                        if (!ret)
                            break;

                        const int port = 1;
                        const byte PRINTER_PIPECMDSIZE = 26;
                        byte[] m_pData = new byte[PRINTER_PIPECMDSIZE + 2];
                        //switch (dir)
                        //{
                        //    case MoveDirectionEnum.Left:
                        //    case MoveDirectionEnum.Right:
                        //        len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, flen) * m_SPrinterProperty.fPulsePerInchX);
                        //        break;
                        //    case MoveDirectionEnum.Down:
                        //    case MoveDirectionEnum.Up:
                        //        //len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, flen) * m_SPrinterProperty.fPulsePerInchY);
                        //        float pulse = m_SPrinterProperty.fPulsePerInchY;
                        //        pulse = pulse * UIPreference.ToInchLength(m_CurrentUnit, flen);
                        //        len = Convert.ToInt32(pulse);
                        //        break;
                        //    case MoveDirectionEnum.Down_Z:
                        //    case MoveDirectionEnum.Up_Z:
                        //        len = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, flen) * m_SPrinterProperty.fPulsePerInchZ);
                        //        break;
                        //}
                        CoreInterface.MoveCmd((int)dir, len, speed);

                        break;
                    }

                case OprationCmdSubCmd.Back2HomeX:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.BackToHomePoint;
                        cmdvalue = 0;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                case OprationCmdSubCmd.Back2HomeY:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.BackToHomePointY;
                        cmdvalue = (int)AxisDir.Y;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                case OprationCmdSubCmd.PrintJobEx: //打印作业,带平台选择
                    {
                        CLockQueue mLinesQueue = this.GetLockQueue();
                        if (mLinesQueue != null)
                        {
                            string[] temp = protocol.Parameter.Split(new char[] { '|' });
                            int reversePrint = 0;
                            TcpipCmdPara linearray = new TcpipCmdPara();
                            linearray.CmdType = 0;
                            linearray.PrtPath = temp[0];
                            linearray.ReversePrint = reversePrint == 1;
                            if (temp.Length > 1)
                                linearray.PrintPlatform = temp[1] == "A"
                                ? CoreConst.AXIS_X
                                : CoreConst.AXIS_4;
                            if (temp.Length > 2)
                                byte.TryParse(temp[2], out linearray.ColorType);
                            if (temp.Length > 3)
                                int.TryParse(temp[3], out linearray.Jobid);
                            mLinesQueue.PutInQueue(linearray);
                            if ((PubFunc.GetUserPermission() == (int)UserPermission.SupperUser))
                            {
                                LogWriter.WriteLog(
                                    new string[] { protocol + "  " + mLinesQueue.GetCount() }, true);
                            }
                        }
                        else
                        {
                            if ((PubFunc.GetUserPermission() == (int)UserPermission.SupperUser))
                                LogWriter.WriteLog(
                                    new string[] { protocol + "  " + "mLinesQueue=null" }, true);
                        }
                        break;
                    }
                case OprationCmdSubCmd.OpenSettingForm:
                    {
                        OnEditPrinterSetting();
                        break;
                    }
                case OprationCmdSubCmd.OpenVolTempForm:
                    {
                        m_MenuItemRealTime_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.OpenCaribrationWizard:
                    {
                        m_MenuItemCalibraion_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.OpenAboutForm:
                    {
                        m_MenuItemAbout_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.OpenUpdateForm:
                    {
                        m_MenuItemUpdate_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.Exit:
                    {
                        End();
                        Application.Exit();
                        break;
                    }
                case OprationCmdSubCmd.OpenFactoryDebugForm:
                    {
                        m_MenuItemFactoryDebug_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.OpenPasswordForm:
                    {
                        m_MenuItemPassword_Click(null, null);
                        break;
                    }
                case OprationCmdSubCmd.StopPrintEx:
                    {
                        //if (protocol.Parameter.StartsWith("A"))
                        //{
                        //    m_JobListForm.AbortPrintByPlatform(CoreConst.AXIS_X);
                        //}
                        //else
                        //{
                        //    m_JobListForm.AbortPrintByPlatform(CoreConst.AXIS_4);
                        //}
                        break;
                    }
                case OprationCmdSubCmd.AutoClean:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.AutoSuckHead;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                case OprationCmdSubCmd.Spray:
                    {
                        JetCmdEnum cmd;
                        int cmdvalue = 0;
                        cmd = JetCmdEnum.FireSprayHead;
                        CoreInterface.SendJetCommand((int)cmd, cmdvalue);
                        break;
                    }
                case OprationCmdSubCmd.AutoPausePerPage:
                    {
                        SPrinterSetting ss = m_allParam.PrinterSetting;
                        int bAutoPausePerPage = 0;
                        bool ret = int.TryParse(protocol.Parameter, out bAutoPausePerPage);
                        if (ret)
                            ss.sExtensionSetting.bAutoPausePerPage = bAutoPausePerPage == 1;
                        m_allParam.PrinterSetting = ss;
                        OnPrinterSettingChange(m_allParam.PrinterSetting);
                        break;
                    }
                case OprationCmdSubCmd.LayingSand:
                    {
                        EpsonLCD.LayingSand();
                        break;
                    }

            }
        }
        private void ProcessPrintParameterCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            switch ((PrintParameterSubCmd)protocol.SubCmd)
            {
                case PrintParameterSubCmd.AutoCenterPrint:
                    {
                        int autoCenter = 0;
                        bool ret = int.TryParse(protocol.Parameter, out autoCenter);
                        if (ret)
                            ss.sBaseSetting.bAutoCenterPrint = autoCenter == 1;
                        break;
                    }
                case PrintParameterSubCmd.OriginX:
                    {
                        int x = 0;
                        bool ret = int.TryParse(protocol.Parameter, out x);
                        if (ret)
                            ss.sFrequencySetting.fXOrigin = x / m_allParam.PrinterProperty.fPulsePerInchX;
                        break;
                    }
                case PrintParameterSubCmd.OriginY:
                    {
                        int y = 0;
                        bool ret = int.TryParse(protocol.Parameter, out y);
                        if (ret)
                            ss.sBaseSetting.fYOrigin = y / m_allParam.PrinterProperty.fPulsePerInchY;
                        break;
                    }
                case PrintParameterSubCmd.StepAdjust:
                    {
                        int step = 0;
                        bool ret = int.TryParse(protocol.Parameter, out step);
                        if (!ret)
                            break;
                        if (this.m_bDuringPrinting)
                        {
                            SPrtFileInfo jobInfo = new SPrtFileInfo();
                            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
                            {
                                int realpass = jobInfo.sFreSetting.nPass;
                                if (ss.nKillBiDirBanding != 0)
                                    ss.sCalibrationSetting.nPassStepArray[(realpass + 1) / 2 - 1] = step;
                                else
                                    ss.sCalibrationSetting.nPassStepArray[realpass - 1] = step;
                            }
                        }
                        else
                        {
                            if (ss.nKillBiDirBanding != 0)
                                ss.sCalibrationSetting.nPassStepArray[(ss.sFrequencySetting.nPass + 1) / 2 - 1] = step;
                            else
                                ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass - 1] = step;
                        }
                        break;
                    }
                case PrintParameterSubCmd.PrintPass:
                    {
                        int pass = 0;
                        bool ret = int.TryParse(protocol.Parameter, out pass);
                        if (ret)
                            ss.sFrequencySetting.nPass = (byte)pass;
                        break;
                    }
                case PrintParameterSubCmd.PrintSpeed:
                    {
                        int speed = 0;
                        bool ret = int.TryParse(protocol.Parameter, out speed);
                        if (ret)
                            ss.sFrequencySetting.nSpeed = (SpeedEnum)(speed - 1);
                        break;
                    }
                case PrintParameterSubCmd.PrintDir:
                    {
                        int bidir = 0;
                        bool ret = int.TryParse(protocol.Parameter, out bidir);
                        if (ret)
                            ss.sFrequencySetting.nBidirection = (byte)bidir;
                        break;
                    }
                case PrintParameterSubCmd.PlatformDistanceY:
                    {
                        float flatSpaceY = 0;
                        bool ret = float.TryParse(protocol.Parameter, out flatSpaceY);
                        if (ret)
                            ss.sExtensionSetting.FlatSpaceY = flatSpaceY;
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.PrinterSetting = ss;
        }
        private void ProcessPrinterSettingCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            switch ((PrinterSettingSubCmd)protocol.SubCmd)
            {
                case PrinterSettingSubCmd.AutoJumpWhite:
                    {
                        int jumpW = 0;
                        bool ret = int.TryParse(protocol.Parameter, out jumpW);
                        if (!ret)
                            break;
                        ss.sBaseSetting.bIgnorePrintWhiteX =
                            ss.sBaseSetting.bIgnorePrintWhiteY = (jumpW == 0);
                        break;
                    }
                case PrinterSettingSubCmd.JumpWhiteTime:
                    {
                        //2：跳白时间（单位：s）	数值
                        float jumpW = 0;
                        bool ret = float.TryParse(protocol.Parameter, out jumpW);
                        if (!ret)
                            break;
                        ss.sBaseSetting.fStepTime = jumpW;
                        break;
                    }
                case PrinterSettingSubCmd.JobSpace:
                    {
                        //3：作业间距（单位：cm）	数值
                        float jobspace = 0;
                        bool ret = float.TryParse(protocol.Parameter, out jobspace);
                        if (!ret)
                            break;
                        ss.sBaseSetting.fJobSpace = UIPreference.ToInchLength(UILengthUnit.Centimeter, jobspace);
                        break;
                    }
                case PrinterSettingSubCmd.FeatherType:
                    {
                        byte feathertype = 0;
                        bool ret = byte.TryParse(protocol.Parameter, out feathertype);
                        if (!ret)
                            break;
                        ss.sBaseSetting.nFeatherType = (byte)(feathertype - 1);//(byte)this.m_ComboBoxFeatherType.SelectedIndex;
                        break;
                    }
                case PrinterSettingSubCmd.FeatherPeacent:
                    {
                        //4：羽化类型	渐变、均匀、波浪、高级、UV（值为1-5）
                        const int FeatherPercentLarge = 101;//dll超过100 的按最大
                        const int FeatherPercentMedium = 66;
                        const int FeatherPercentSmall = 33;
                        const int FeatherPercentNone = 0;
                        byte feathertype = 0;
                        bool ret = byte.TryParse(protocol.Parameter, out feathertype);
                        if (!ret)
                            break;
                        EpsonFeatherType featherPercentType = (EpsonFeatherType)(feathertype - 1);
                        int featherPercent = 0;
                        byte bMax = 0;
                        switch (featherPercentType)
                        {
                            case EpsonFeatherType.None:
                                featherPercent = FeatherPercentNone;
                                break;
                            case EpsonFeatherType.Small:
                                featherPercent = FeatherPercentSmall;
                                break;
                            case EpsonFeatherType.Medium:
                                featherPercent = FeatherPercentMedium;
                                break;
                            case EpsonFeatherType.Large:
                                featherPercent = FeatherPercentLarge;
                                bMax = 1;
                                break;
                            //case EpsonFeatherType.Custom:
                            //    ret = Decimal.ToInt32(m_NumericUpDownFeather.Value);
                            //    break;
                            //case EpsonFeatherType.SuperLarge:
                            //    bMax = 2;
                            //    break;
                        }
                        ss.sBaseSetting.nFeatherPercent = featherPercent;
                        ss.sBaseSetting.bFeatherMaxNew = bMax;
                        break;
                    }
                case PrinterSettingSubCmd.MultiInk:
                    {
                        //6：墨量	默认、两倍、多倍（值为1-3）
                        byte multipleInk = 0;
                        bool ret = byte.TryParse(protocol.Parameter, out multipleInk);
                        if (!ret)
                            break;
                        ss.sBaseSetting.multipleInk = (byte)(multipleInk - 1);
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.PrinterSetting = ss;
        }
        private void ProcessColorBarSettingCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            switch ((ColorBarSettingSubCmd)protocol.SubCmd)
            {
                case ColorBarSettingSubCmd.Space:
                    {
                        float space = 0;
                        bool ret = float.TryParse(protocol.Parameter, out space);
                        if (!ret)
                            break;
                        ss.sBaseSetting.sStripeSetting.fStripeOffset = UIPreference.ToInchLength(UILengthUnit.Centimeter, space);
                        break;
                    }
                case ColorBarSettingSubCmd.Width:
                    {
                        float width = 0;
                        bool ret = float.TryParse(protocol.Parameter, out width);
                        if (!ret)
                            break;
                        ss.sBaseSetting.sStripeSetting.fStripeWidth = UIPreference.ToInchLength(UILengthUnit.Centimeter, width);
                        break;
                    }
                case ColorBarSettingSubCmd.Position:
                    {
                        //3：位置	两边、左、右、无(值为1-4)
                        int pos = 0;
                        bool ret = int.TryParse(protocol.Parameter, out pos);
                        if (!ret)
                            break;
                        ss.sBaseSetting.sStripeSetting.eStripePosition = (InkStrPosEnum)(pos - 1);
                        break;
                    }
                case ColorBarSettingSubCmd.Normally:
                    {
                        byte NormalType = 0;
                        int normal = 0;
                        bool ret = int.TryParse(protocol.Parameter, out normal);
                        if (!ret)
                            break;
                        NormalType |= (byte)EnumStripeType.Normal;
                        if (normal == 1)
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType |= NormalType;
                        }
                        else
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType &= (byte)(~NormalType);
                        }
                        break;
                    }
                case ColorBarSettingSubCmd.ColorOverlap:
                    {
                        byte NormalType = 0;
                        int normal = 0;
                        bool ret = int.TryParse(protocol.Parameter, out normal);
                        if (!ret)
                            break;
                        NormalType |= (byte)EnumStripeType.ColorMixed;
                        if (normal == 1)
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType |= NormalType;
                        }
                        else
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType &= (byte)(~NormalType);
                        }
                        break;
                    }
                case ColorBarSettingSubCmd.SameHeightWithImage:
                    {
                        byte NormalType = 0;
                        int normal = 0;
                        bool ret = int.TryParse(protocol.Parameter, out normal);
                        if (!ret)
                            break;
                        NormalType |= (byte)EnumStripeType.HeightWithImage;
                        if (normal == 1)
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType |= NormalType;
                        }
                        else
                        {
                            ss.sBaseSetting.sStripeSetting.bNormalStripeType &= (byte)(~NormalType);
                        }
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.PrinterSetting = ss;
        }
        private void ProcessSpraySettingCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            switch ((SpraySettingSubCmd)protocol.SubCmd)
            {
                case SpraySettingSubCmd.AutoSpray:
                    {
                        //1：自动闪喷（单位：pass）	数值
                        int passInterval = 0;
                        bool ret = int.TryParse(protocol.Parameter, out passInterval);
                        if (!ret)
                            break;
                        ss.sCleanSetting.nSprayPassInterval = passInterval;
                        break;
                    }
                case SpraySettingSubCmd.SrayPeriod:
                    {
                        //2：闪喷周期（单位：ms）	数值
                        int sprayCycle = 0;
                        bool ret = int.TryParse(protocol.Parameter, out sprayCycle);
                        if (!ret)
                            break;
                        ss.sCleanSetting.nSprayFireInterval = sprayCycle;
                        break;
                    }
                case SpraySettingSubCmd.SprayTimeBeforPrint:
                    {
                        //打印前闪喷持续时间（单位：ms）
                        int ptaSpraying = 0;
                        bool ret = int.TryParse(protocol.Parameter, out ptaSpraying);
                        if (!ret)
                            break;
                        ss.sCleanSetting.nPauseTimeAfterSpraying = Decimal.ToUInt16(ptaSpraying * 1000);
                        break;
                    }
                case SpraySettingSubCmd.IdleSpray:
                    {
                        //4：空闲闪喷	值为0(假)，值为1(真)
                        int idleSpraying = 0;
                        bool ret = int.TryParse(protocol.Parameter, out idleSpraying);
                        if (!ret)
                            break;
                        ss.sCleanSetting.bSprayWhileIdle = idleSpraying == 1;
                        break;
                    }
                case SpraySettingSubCmd.SprayBeforPrint:
                    {
                        //5：打印前闪喷	值为0(假)，值为1(真)
                        int idleSpraying = 0;
                        bool ret = int.TryParse(protocol.Parameter, out idleSpraying);
                        if (!ret)
                            break;
                        ss.sCleanSetting.bSprayBeforePrint = idleSpraying == 1;
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.PrinterSetting = ss;
        }
        private void ProcessPrintDirCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            switch ((PrintDirSubCmd)protocol.SubCmd)
            {
                case PrintDirSubCmd.MirrorPrint:
                    {
                        int bMirrorX = 0;
                        bool ret = int.TryParse(protocol.Parameter, out bMirrorX);
                        if (!ret)
                            break;
                        ss.sBaseSetting.bMirrorX = bMirrorX == 1;
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.PrinterSetting = ss;
        }
        private void ProcessPreferenceSettingCmd(ProtocolByhx protocol)
        {
            UIPreference up = m_allParam.Preference;
            switch ((PreferenceSettingSubCmd)protocol.SubCmd)
            {
                case PreferenceSettingSubCmd.HotForlder:
                    {
                        //1：使用热文件位置	值为0则不可用，合法路径则可用。
                        if (!string.IsNullOrEmpty(protocol.Parameter) && Directory.Exists(protocol.Parameter))
                        {
                            up.EnableHotForlder = true;
                            up.HotForlderPath = protocol.Parameter;
                        }
                        else
                        {
                            up.EnableHotForlder = false;
                        }
                        break;
                    }
                case PreferenceSettingSubCmd.DelJobAfterPrint:
                    {
                        int deljob = 0;
                        bool ret = int.TryParse(protocol.Parameter, out deljob);
                        if (!ret)
                            break;
                        up.DelJobAfterPrint = deljob == 1;
                        break;
                    }
                case PreferenceSettingSubCmd.DelFileAfterPrint:
                    {
                        int delFile = 0;
                        bool ret = int.TryParse(protocol.Parameter, out delFile);
                        if (!ret)
                            break;
                        up.DelFileAfterPrint = delFile == 1;
                        break;
                    }
                case PreferenceSettingSubCmd.LeftRightSwap:
                    {
                        int reverseHoriDir = 0;
                        bool ret = int.TryParse(protocol.Parameter, out reverseHoriDir);
                        if (!ret)
                            break;
                        up.ReverseHoriMoveDirection = reverseHoriDir == 1;
                        break;
                    }
                case PreferenceSettingSubCmd.DisplayUnit:
                    {
                        int unit = 0;
                        bool ret = int.TryParse(protocol.Parameter, out unit);
                        if (!ret)
                            break;
                        up.Unit = (UILengthUnit)(unit - 1);
                        break;
                    }
                case PreferenceSettingSubCmd.DisplayLang:
                    {
                        int langindex = 0;
                        bool ret = int.TryParse(protocol.Parameter, out langindex);
                        if (!ret)
                            break;
                        up.LangIndex = langindex;
                        break;
                    }
                //default:
                //    throw new ArgumentOutOfRangeException();
            }
            m_allParam.Preference = up;

        }

        private void ProcessCaribrationCmd(ProtocolByhx protocol)
        {
            SPrinterSetting ss = m_allParam.PrinterSetting;
            SCalibrationSetting m_sCalibrationSetting = ss.sCalibrationSetting;
            SCalibrationHorizonArrayUI m_NewHorizonArrayUI = m_allParam.NewCalibrationHorizonArray;
            int m_nDisplaySpeedNum = 3;
            switch (protocol.Cmd)
            {
                case TcpIpCmdByhx.CaribrationHor:
                    {
                        switch ((CaribrationHorSubCmd)protocol.SubCmd)
                        {
                            //case CaribrationHorSubCmd.SpeedDpiIndex:
                            //{
                            //    break;
                            //}
                            //case CaribrationHorSubCmd.CopyTo:
                            //{
                            //    break;
                            //}
                            //case CaribrationHorSubCmd.CariMode:
                            //{
                            //    break;
                            //}
                            case CaribrationHorSubCmd.BiDri:
                                {
                                    int resIndex = 0, speedIndex = 0, selectedIndex = 0, bidirectionValue = 0;
                                    string[] paras = protocol.Parameter.Split(',');
                                    int.TryParse(paras[0], out selectedIndex);
                                    int.TryParse(paras[1], out bidirectionValue);
                                    int curParamIndex = (selectedIndex / m_nDisplaySpeedNum) * CoreConst.MAX_SPEED_NUM + (selectedIndex % m_nDisplaySpeedNum);
                                    SCalibrationHorizonSetting curSpeed = m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex];
                                    SCalibrationHorizonSettingUI curSpeedNew = m_NewHorizonArrayUI[curParamIndex];
                                    curSpeed.nBidirRevise = bidirectionValue;
                                    m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex] = curSpeed;
                                    break;
                                }
                            case CaribrationHorSubCmd.LineWidth:
                                {
                                    byte lineWidth = 0;
                                    bool ret = byte.TryParse(protocol.Parameter, out lineWidth);
                                    if (!ret)
                                        break;
                                    ss.sExtensionSetting.LineWidth = lineWidth;
                                    break;
                                }
                            case CaribrationHorSubCmd.LeftCari:
                                {
                                    int resIndex = 0, speedIndex = 0, selectedIndex = 0, bidirectionValue = 0;
                                    string[] paras = protocol.Parameter.Split(',');
                                    List<int> values = new List<int>();
                                    for (int i = 1; i < paras.Length; i++)
                                    {
                                        int val = 0;
                                        int.TryParse(paras[0], out val);
                                        values.Add(val);
                                    }
                                    int.TryParse(paras[1], out bidirectionValue);
                                    int curParamIndex = (selectedIndex / m_nDisplaySpeedNum) * CoreConst.MAX_SPEED_NUM + (selectedIndex % m_nDisplaySpeedNum);
                                    SCalibrationHorizonSetting curSpeed = m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex];
                                    SCalibrationHorizonSettingUI curSpeedNew = m_NewHorizonArrayUI[curParamIndex];
                                    for (int i = 0; i < values.Count; i++)
                                    {
                                        curSpeedNew.XLeftArray[i] = Convert.ToSByte(values[i]);
                                        if (curSpeed.XLeftArray.Length > i)
                                            curSpeed.XLeftArray[i] = curSpeedNew.XLeftArray[i];
                                    }
                                    m_NewHorizonArrayUI[curParamIndex] = curSpeedNew;
                                    m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex] = curSpeed;

                                    break;
                                }
                            case CaribrationHorSubCmd.RightCari:
                                {
                                    int resIndex = 0, speedIndex = 0, selectedIndex = 0, bidirectionValue = 0;
                                    string[] paras = protocol.Parameter.Split(',');
                                    List<int> values = new List<int>();
                                    for (int i = 1; i < paras.Length; i++)
                                    {
                                        int val = 0;
                                        int.TryParse(paras[0], out val);
                                        values.Add(val);
                                    }
                                    int.TryParse(paras[1], out bidirectionValue);
                                    int curParamIndex = (selectedIndex / m_nDisplaySpeedNum) * CoreConst.MAX_SPEED_NUM + (selectedIndex % m_nDisplaySpeedNum);
                                    SCalibrationHorizonSetting curSpeed = m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex];
                                    SCalibrationHorizonSettingUI curSpeedNew = m_NewHorizonArrayUI[curParamIndex];
                                    for (int i = 0; i < values.Count; i++)
                                    {
                                        curSpeedNew.XRightArray[i] = Convert.ToSByte(values[i]);
                                        if (curSpeed.XRightArray.Length > i)
                                            curSpeed.XRightArray[i] = curSpeedNew.XRightArray[i];
                                    }
                                    m_NewHorizonArrayUI[curParamIndex] = curSpeedNew;
                                    m_sCalibrationSetting.sCalibrationHorizonArray[curParamIndex] = curSpeed;
                                    break;
                                }
                            //default:
                            //    throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                case TcpIpCmdByhx.CaribrationStep:
                    {
                        switch ((CaribrationStepSubCmd)protocol.SubCmd)
                        {
                            //case CaribrationStepSubCmd.PassNum:
                            //{
                            //    break;
                            //}
                            case CaribrationStepSubCmd.AdjustValue:
                                {
                                    string[] paras = protocol.Parameter.Split(',');
                                    if (paras.Length < 2)
                                        return;
                                    int curPassSelectIndex = -1;
                                    float revise = 0;
                                    int.TryParse(paras[0], out curPassSelectIndex);
                                    float.TryParse(paras[0], out revise);

                                    int bOnePass = 0;
                                    if (curPassSelectIndex == 0)
                                        bOnePass = 1;
                                    if (Math.Abs(revise) > 0.00001f)
                                    {
                                        int nNew = CoreInterface.GetStepReviseValue(revise, curPassSelectIndex + 1, ref m_sCalibrationSetting, bOnePass);

                                        if (bOnePass == 1)
                                        {
                                            m_sCalibrationSetting.nStepPerHead = nNew;
                                        }
                                        else
                                        {
                                            m_sCalibrationSetting.nPassStepArray[curPassSelectIndex] = nNew;
                                        }
                                    }
                                    break;
                                }
                            case CaribrationStepSubCmd.Step:
                                {
                                    string[] paras = protocol.Parameter.Split(',');
                                    if (paras.Length < 2)
                                        break;
                                    int curPassSelectIndex = -1;
                                    int nNew = 0;
                                    int.TryParse(paras[0], out curPassSelectIndex);
                                    int.TryParse(paras[0], out nNew);

                                    int bOnePass = 0;
                                    if (curPassSelectIndex == 0)
                                        bOnePass = 1;
                                    if (bOnePass == 1)
                                    {
                                        m_sCalibrationSetting.nStepPerHead = nNew;
                                    }
                                    else
                                    {
                                        m_sCalibrationSetting.nPassStepArray[curPassSelectIndex] = nNew;
                                    }
                                    break;
                                }
                            case CaribrationStepSubCmd.BaseStep:
                                {
                                    int basestep = -1;
                                    int.TryParse(protocol.Parameter, out basestep);
                                    if (basestep > 0)
                                        m_sCalibrationSetting.nStepPerHead = basestep;
                                    break;
                                }
                            //default:
                            //    throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                //case TcpIpCmdByhx.CaribrationVer:
                //    {
                //        switch ((CaribrationVerSubCmd)protocol.SubCmd)
                //        {
                //            case CaribrationVerSubCmd.AdjustValue:
                //                {
                //                    string[] paras = protocol.Parameter.Split(',');
                //                    List<int> values = new List<int>();
                //                    for (int i = 1; i < paras.Length; i++)
                //                    {
                //                        int val = 0;
                //                        int.TryParse(paras[0], out val);
                //                        values.Add(val);
                //                    }
                //                    for (int i = 0; i < m_allParam.PrinterProperty.nColorNum; i++)
                //                    {
                //                        m_sCalibrationSetting.nVerticalArray[i] = values[i];
                //                    }
                //                    break;
                //                }
                //            //default:
                //            //    throw new ArgumentOutOfRangeException();
                //        }
                //        break;
                //    }
                //case TcpIpCmdByhx.CaribrationOverLap:
                //    {
                //        switch ((CaribrationOverLapSubCmd)protocol.SubCmd)
                //        {
                //            case CaribrationOverLapSubCmd.AdjustValue:
                //                {
                //                    string[] paras = protocol.Parameter.Split(',');
                //                    List<int> values = new List<int>();
                //                    for (int i = 1; i < paras.Length; i++)
                //                    {
                //                        int val = 0;
                //                        int.TryParse(paras[0], out val);
                //                        values.Add(val);
                //                    }
                //                    for (int i = 0; i < values.Count; i++)
                //                    {
                //                        m_sCalibrationSetting.nVerticalArray[m_allParam.PrinterProperty.nColorNum + i] = values[i];
                //                    }
                //                    break;
                //                }
                //            //default:
                //            //    throw new ArgumentOutOfRangeException();
                //        }
                //        break;
                //    }
            }
            ss.sCalibrationSetting = m_sCalibrationSetting;
            m_allParam.NewCalibrationHorizonArray = m_NewHorizonArrayUI;
            m_allParam.PrinterSetting = ss;
        }

        private void ProcessFactoryDebugCmd(ProtocolByhx protocol)
        {

        }

        void remoteControlServerTask_DoWork(object sender, DoWorkEventArgs e)
        {
            remoteControlServer.Worker = (sender as MyBackgroundWorker);
            remoteControlServer.Run();
        }
#endif
        #endregion
    }
}
