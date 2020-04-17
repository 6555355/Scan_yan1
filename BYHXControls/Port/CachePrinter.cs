using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.Common;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Port
{
    public class CachePrinter
    {
        public const int BUFSIZE = 8 * 1024;
        private System.Collections.Concurrent.ConcurrentQueue<MyStruct> _cachList;
        private volatile bool _bReadExited = false;
        private volatile bool _bSendExited = true;
        IPrinterChange m_IPrinterChange;

        public CachePrinter(IPrinterChange ipc)
        {
            m_IPrinterChange = ipc;
            _cachList = new ConcurrentQueue<MyStruct>();
            _bReadExited = false;
        }

        public void Run()
        {
            if (_bSendExited) // 上一次打印数据发送线程已退出
            {
                //启动发送打印数据线程
                Task.Factory.StartNew(() => { SendPrintData(); });
            }
            else
            {
                LogWriter.WriteLog(new string[] { "[CachePrinter]Run fail,上次打印线程未退出" }, true);
            }
        }

        public void AddPrintData(MyStruct data)
        {
            _cachList.Enqueue(data);
            //LogWriter.WriteLog(new string[] { string.Format("[ReceiveDataThread]  CachList.Count = {0}", CachList.Count) }, true);
            while (_cachList.Count >= 8192)
            {
                Thread.Sleep(10);
            }
        }

        public void SendPrintData()
        {
            int hPrinterPort =0;
            try
            {
                bool isFirstRIP = false;
                _bSendExited = _bReadExited = false;
                bool bFileHeader = true;
                while (_cachList.Count < 2048 && !_bReadExited) //缓存一定数量才开始打印16M
                {
                    Thread.Sleep(100);
                }

                while ((hPrinterPort = CoreInterface.Printer_Open()) == 0)
                {
                    Thread.Sleep(100);
                }

                SPrinterSetting ssNew = this.m_IPrinterChange.GetAllParam().PrinterSetting;
                SJobSetting sjobseting = new SJobSetting();
                if (UIFunctionOnOff.SupportPrintMode)
                {
                    //todo
                }
                if (UIFunctionOnOff.SupportMediaMode)
                {
                    //todo
                }
                LayoutSetting curLayoutSetting = new LayoutSetting();

                int layoutIdx = 0;
                if (CoreInterface.LayoutIndex >= 0)
                    layoutIdx = CoreInterface.LayoutIndex;

                if (NewLayoutFun.GetLayoutSetting(layoutIdx, ref curLayoutSetting))
                {
                    ssNew.layoutSetting = curLayoutSetting;
                }
                if (ssNew.sExtensionSetting.AutoRunAfterPrint)
                {
                    ssNew.sBaseSetting.fYAddDistance = ssNew.sExtensionSetting.fRunDistanceAfterPrint;
                    ssNew.sExtensionSetting.bEnableAnotherUvLight = ssNew.sExtensionSetting.fRunDistanceAfterPrint > 0;
                    CoreInterface.SetPrinterSetting(ref ssNew, false); //打印结束后继续扫描一段距离生效
                }
                //打印前设置JobSetting
                sjobseting.bReversePrint = ssNew.sBaseSetting.bReversePrint;
                CoreInterface.SetSJobSetting(ref sjobseting);



                LogWriter.WriteLog(new string[] {"[RIP]Printer open"}, true);
                int cbBytesRead = 0;
                isFirstRIP = true;
                while (true)
                {
                    if (_cachList.Count > 0)
                    {
                        MyStruct data = new MyStruct();
                        if (!_cachList.TryDequeue(out data))
                            continue;
                        byte[] chRequest = data.buf;
                        cbBytesRead = data.buflen;
#if ADD_HARDKEY
                        {
                            int subsize = 32;
                            byte[] lastValue = chRequest;
                            if (bFileHeader)
                            {
                                bFileHeader = false;
                                lastValue = new byte[BUFSIZE + 8];


                                byte[] mjobhead = new byte[subsize];
                                byte[] retValue = new byte[subsize + Marshal.SizeOf(typeof(int))];
                                for (int j = 0; j < BYHXSoftLock.JOBHEADERSIZE / subsize; j++)
                                {
                                    Buffer.BlockCopy(chRequest, j * mjobhead.Length, mjobhead, 0, mjobhead.Length);
                                    BYHX_SL_RetValue ret = BYHXSoftLock.CheckValidDateWithData(mjobhead, ref retValue);
                                    Buffer.BlockCopy(retValue, 0, lastValue, j * retValue.Length, retValue.Length);
                                }

                                Buffer.BlockCopy(chRequest, BYHXSoftLock.JOBHEADERSIZE, lastValue,
                                    BYHXSoftLock.JOBHEADERSIZE + 8, chRequest.Length - BYHXSoftLock.JOBHEADERSIZE);
                                int sendBytes = CoreInterface.Printer_Send(hPrinterPort, lastValue, cbBytesRead + 8);
                                Debug.Assert(sendBytes == cbBytesRead + 8);
                            }
                            else
                            {
                                CoreInterface.Printer_Send(hPrinterPort, chRequest, cbBytesRead);
                            }
                        }
#else
                        {
                            if (isFirstRIP)
                            {
                                string strLog = "";
                                int nVersion = 0;
                                byte bReversePrint = 0;
                                byte nPrintLayerNum = 0;
                                int printmodePerLayer = 0;
                                isFirstRIP = false;

                                if (cbBytesRead >= 84)
                                {
                                    for (int i = 0; i < 84; i++)
                                    {
                                        strLog += Convert.ToString(chRequest[i], 2) + ",";
                                    }

                                    LogWriter.WriteLog(new string[] {"[RIP]" + strLog}, true);


                                    nVersion = BitConverter.ToInt32(chRequest, 4);
                                    if (nVersion == 4)
                                    {
                                        bReversePrint = chRequest[55];
                                        //nPrintLayerNum = chRequest[56];
                                        //printmodePerLayer = BitConverter.ToInt32(chRequest, 57);

                                        //PrinterSettingHelper.SetPropertyWhiteInkLayer(
                                        //    ref m_IPrinterChange.GetAllParam().PrinterSetting, bReversePrint,
                                        //    nPrintLayerNum,
                                        //    (uint) printmodePerLayer);
                                        CoreInterface.SetPrinterSetting(ref m_IPrinterChange.GetAllParam()
                                            .PrinterSetting);
                                        //isSetWhiteFormPrt = true;

                                        sjobseting = new SJobSetting();
                                        sjobseting.bReversePrint = bReversePrint == 1;
                                        CoreInterface.SetSJobSetting(ref sjobseting);

                                    }
                                    else if (nVersion == 6)
                                    {
                                        bReversePrint = chRequest[71];

                                        sjobseting = new SJobSetting();
                                        sjobseting.bReversePrint = bReversePrint == 1;
                                        CoreInterface.SetSJobSetting(ref sjobseting);
                                    }
                                }
                            }

                            //#else
                            CoreInterface.Printer_Send(hPrinterPort, chRequest, cbBytesRead);
                        }
#endif
                    }

                    if (_cachList.Count == 0 && _bReadExited)
                    {
                        LogWriter.WriteLog(
                            new string[]
                            {
                                string.Format("[SendPrintData]  CachList.Count = {0};bReadExited={1}", _cachList.Count,
                                    _bReadExited)
                            }, true);
                        break;
                    }

                    if (_cachList.Count == 0)
                    {
                        LogWriter.WriteLog(
                            new string[]
                            {
                                string.Format("[SendPrintData]  CachList.Count = {0};waittime={1}", _cachList.Count, 10)
                            }, true);
                        Thread.Sleep(10);
                    }
                }

                CoreInterface.Printer_Close(hPrinterPort);
                _bSendExited = true;
				SExtensionSetting extensionSetting = ssNew.sExtensionSetting;
                if (extensionSetting.fRunDistanceAfterPrint > 0 && extensionSetting.BackBeforePrint)
                {
                    while (true)
                    {
                        JetStatusEnum status = CoreInterface.GetBoardStatus();
                        if (status == JetStatusEnum.Ready)
                        {
                            int speed = ssNew.sMoveSetting.nYMoveSpeed;
                            MoveDirectionEnum dir = MoveDirectionEnum.Up;
                            int len = Convert.ToInt32(extensionSetting.fRunDistanceAfterPrint * m_IPrinterChange.GetAllParam().PrinterProperty.fPulsePerInchY);
                            CoreInterface.MoveCmd((int)dir, len, speed);
                            break;
                        }
                        Thread.Sleep(100);
                    }
                }


            }
            catch (Exception e)
            {
                LogWriter.WriteLog(
                    new string[]
                    {
                        string.Format("[SendPrintData]  CachList.Count = {0};Exception={1}", _cachList.Count, e.Message)
                    }, true);
                if (hPrinterPort != 0)
                {
                    CoreInterface.Printer_Close(hPrinterPort);
                    _bSendExited = true;
                }
            }
        }
        public bool SetJobModeToPrintSetting(ref SPrinterSetting ss, ref SJobSetting jobseting)
        {
            //todo
            return true;

        }
        private LayoutSetting GetLayoutSetting(LayoutSettingClass oldLayoutSettingClass, ref bool isSpecialLayout, ref int SpecialYSpace)
        {
            LayoutSetting curLayoutSetting = new LayoutSetting();
            curLayoutSetting = oldLayoutSettingClass.Layout;

            try
            {
                LayoutSettingClassList m_LayoutSettingList = new LayoutSettingClassList();

                string layoutFilePath = Path.Combine(Application.StartupPath, CoreConst.LayoutFileName);
                if (File.Exists(layoutFilePath))
                {
                    var doc = new XmlDocument();
                    doc.Load(layoutFilePath);
                    m_LayoutSettingList = (LayoutSettingClassList)PubFunc.SystemConvertFromXml(doc.InnerXml, typeof(LayoutSettingClassList));

                    foreach (LayoutSettingClass item in m_LayoutSettingList.Items)
                    {
                        if (item.Name == oldLayoutSettingClass.Name)
                        {
                            curLayoutSetting = item.Layout;
                            isSpecialLayout = item.SpecialLayout;
                            SpecialYSpace = item.SpecialYSpace;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }

            return curLayoutSetting;
        }

        public void ReceiveDataFinishedFlag(bool flag)
        {
            _bReadExited = flag;
        }
        /// <summary>
        /// 等待数据发送线程退出
        /// </summary>
        public void WaitSendPrintDataFinish()
        {
            while (_bSendExited)
            {
                Thread.Sleep(200);
            }
        }
    }

    public struct MyStruct
    {
        public int buflen;
        public byte[] buf;
    }
}
