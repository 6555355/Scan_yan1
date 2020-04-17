using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Xml;
using BYHXPrinterManager.Setting;
using PrinterStubC.CInterface;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using PrinterStubC.Common;
using SysCtrlLibary;
using System.Collections.Generic;

namespace BYHXPrinterManager.JobListView
{
    public delegate void CallbackWorkingJobFinished(UIJob job);

    /// <summary>
    /// WorkTask 的摘要说明。
    /// </summary>
    public class WorkerTask
    {
        public Thread m_Worker;
        private CallbackWorkingJobFinished m_CallbackJobStatusChanged;
        private UIJob m_WorkingJob = null;
        private ArrayList m_WorkingJobList = new ArrayList();
        private bool m_TerminalFlag = false;
        private bool m_AbortFlag = false;
        private bool m_bWorking = false;
        private IPrinterChange m_iPrinterChange;
        private int m_workingType = 0;
        private Size m_ThumbnailImageSize = new Size(16, 16);
        private ListView m_JobListForm = null;
        private int m_CopiesIndex = 1;
        public event EventHandler allWorkFinished;
        public event EventHandler JobCopyPrinted;
        private bool m_bReversePrint = false;
        private ushort _jobIndex = 0;
        private bool m_IsUsedJobMode = false;
        private bool m_IsUsedJobMediaMode = false;
        private JobMode m_JobMode = new JobMode();
        private JobMediaMode m_JobMediaMode = null;
        private bool _currentJobPrintFinish = false;
        public WorkerTask(CallbackWorkingJobFinished change, int workingType, Size ThumbnailImageSize, ListView jobListForm)
        {
            m_WorkingJobList = new ArrayList();
            m_TerminalFlag = m_AbortFlag = m_bWorking = false;
            _currentJobPrintFinish = false;
            m_WorkingJob = null;
            m_CallbackJobStatusChanged = change;
            m_workingType = workingType;
            m_ThumbnailImageSize = ThumbnailImageSize;
            m_JobListForm = jobListForm;
        }


        public void SetWorker(Thread mWorker)
        {
            lock (this)
            {
                m_Worker = mWorker;
            }
        }
        public bool IsAlive()
        {
            if (m_Worker == null)
                return false;
            return m_Worker.IsAlive;
        }
        public bool IsWorking()
        {
            bool ret = false;
            lock (this)
            {
                ret = m_bWorking;
            }
            return ret;
        }
        public int PrintingPage
        {
            get { return m_CopiesIndex; }
        }
        public void SetWorking(bool bworking)
        {
            lock (this)
            {
                m_bWorking = bworking;
            }
        }

        private IntPtr _handle;
        private uint _kernelMessage = 0;
        public void SetPrinterChange(IPrinterChange ic, IntPtr handle = default(IntPtr), uint kernelMessage = 0)
        {
            m_iPrinterChange = ic;
            _handle = handle;
            _kernelMessage = kernelMessage;
        }

        public UIJob GetWorkingJob()
        {
            return m_WorkingJob;
        }
        public bool IsWorkingJob(UIJob job)
        {
            bool ret;
            lock (this)
            {
                ret = (m_WorkingJob == job);
            }
            return ret;
        }
        public void AddJob(UIJob job)
        {
            lock (this)
            {
                if (!m_TerminalFlag)
                    m_WorkingJobList.Add(job);
            }
        }
        public void DeleteJob(UIJob job)
        {
            lock (this)
            {
                if (m_WorkingJobList.Contains(job))
                    m_WorkingJobList.Remove(job);
            }
        }
        public void AbortJob(UIJob job)
        {
            lock (this)
            {
                job.IsAbort = true;
                if (m_WorkingJob == job)
                    m_AbortFlag = true;
                else if (m_WorkingJobList.Contains(job))
                {
                    if (m_WorkingJobList.Contains(job))
                        m_WorkingJobList.Remove(job);
                    if (m_workingType == 0)
                        ChangeJobStatusTo(job, JobStatus.Idle);
                }
            }
        }
        public void Abort()
        {
            lock (this)
            {
                if (m_WorkingJob != null)
                {
                    m_WorkingJob.IsAbort = true;
                }
                m_AbortFlag = true;
            }
        }
        public virtual void Terminate(bool bTerm)
        {
            lock (this)
            {
                string loginfo = string.Empty;
                int count = m_WorkingJobList.Count;
                for (int i = 0; i < count; i++)
                {
                    UIJob job = (UIJob)m_WorkingJobList[i];
                    if (m_workingType == 0)
                    {
                        job.IsAbort = true;
                        ChangeJobStatusTo(job, JobStatus.Idle);
                    }
                    loginfo += job.FileLocation + ";";
                }
                LogWriter.WriteLog(new string[] { string.Format("Terminate Printing jobs=[{0}]", loginfo) }, true);

                m_WorkingJobList.Clear();
                if (bTerm)
                    m_TerminalFlag = true;
                else
                    m_AbortFlag = true;
            }
        }

        public void OnAllWorkFinished(EventArgs e)
        {
            EventHandler handler = allWorkFinished;
            if (handler != null)
            {
                if (this.m_JobListForm.InvokeRequired)
                    this.m_JobListForm.Invoke(handler, new object[] { this, e });
                else
                    handler(this, e);
            }
        }

        public void OnJobCopyPrinted(EventArgs e)
        {
            EventHandler handler = JobCopyPrinted;
            if (handler != null)
            {
                if (this.m_JobListForm.InvokeRequired)
                    this.m_JobListForm.Invoke(handler, new object[] { this, e });
                else
                    handler(this, e);
            }
        }
        /// <summary>
        /// 是否处于层间暂停期间
        /// </summary>
        public bool WaitingPauseBetweenLayers { get; set; }
        public bool IsUsedJobMode
        {
            set { m_IsUsedJobMode = value; }
        }
        public bool IsUsedJobMediaMode
        {
            set { m_IsUsedJobMediaMode = value; }
        }
        public JobMode JobMode
        {
            set { m_JobMode = value; }
        }
        public JobMediaMode JobMediaMode
        {
            set { m_JobMediaMode = value; }
        }

        private int _currentJobPrintPercent = 0;
        public int CurrentJobPrintPercent
        {
            set
            {
                _currentJobPrintPercent = value;
                _currentJobPrintFinish = 100 == _currentJobPrintPercent;
            }
            get
            {
                return _currentJobPrintPercent;
            }
        }

        public void WorkingThreadProc()
        {
            try
            {
                //如果开启印前闪喷，则_jobIndex==0标识打印改job前是否有清洗动作
                _jobIndex = 0;
                while (!m_TerminalFlag)
                {
                    lock (this)
                    {
                        m_bWorking = !(m_TerminalFlag || m_WorkingJobList.Count == 0);
                    }
                    if (!m_bWorking)
                    {
                        m_WorkingJob = null;
                        break;
                    }
                    UIJob job = (UIJob)m_WorkingJobList[0];
                    m_WorkingJob = job;
                    m_WorkingJob.IsAbort = false;
                    DeleteJob(job);
                    if (job != null)
                    {
                        switch (m_workingType)
                        {
                            case 0:
                                if (!m_IsUsedJobMode && !m_IsUsedJobMediaMode)
                                    RealPrintJob(job);
                                else
                                    JobConfigPrintJob(job);
                                _jobIndex++;
                                bool bAutoPauseBetweenLayers = this.m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting.bAutoPausePerPage;
                                int i = 0;
                                if (PubFunc.Is3DPrintMachine())
                                {
                                    while (bAutoPauseBetweenLayers && m_WorkingJobList.Count > 0)
                                    {
                                        LogWriter.SaveOptionLog("Enter bAutoPauseBetweenLayers wait");
                                        if (!WaitingPauseBetweenLayers || i % 10 == 0)
                                            SystemCall.PostMessage(_handle, _kernelMessage, (int)CoreMsgEnum.Status_Change, (int)CoreInterface.CurBoardStatus);

                                        WaitingPauseBetweenLayers = true;
                                        Thread.Sleep(100);
                                        if (m_AbortFlag || m_TerminalFlag)
                                        {
                                            break;
                                        }
                                        i++;
                                        bAutoPauseBetweenLayers = this.m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting.bAutoPausePerPage;
                                    }
                                    LogWriter.SaveOptionLog("Exit bAutoPauseBetweenLayers wait");
                                    //SystemCall.PostMessage(_handle, _kernelMessage, (int)CoreMsgEnum.Status_Change,
                                    //    (int)CoreInterface.CurBoardStatus);
                                }
                                WaitingPauseBetweenLayers = false;
                                break;
                            case 1:
                                CreatePreviewJob(job);
                                break;
                            case 2:
                                GenDoublePrintPrt(job);
                                break;
                            case 3:
                                PrintToFile(job);
                                break;
                        }
                    }
                }
                // 如果启用了打印后移动距离功能,真正打印完成后介质要回退相应距离
                SExtensionSetting extensionSetting = m_iPrinterChange.GetAllParam().PrinterSetting.sExtensionSetting;
                if (m_workingType == 0 && extensionSetting.fRunDistanceAfterPrint > 0 && extensionSetting.BackBeforePrint)
                {
                    LogWriter.SaveOptionLog("Enter fRunDistanceAfterPrint wait");
                    while (!(m_AbortFlag || m_TerminalFlag))
                    {
                        JetStatusEnum status = CoreInterface.CurBoardStatus;
                        if (status == JetStatusEnum.Ready)
                        {
                            int speed = m_iPrinterChange.GetAllParam().PrinterSetting.sMoveSetting.nYMoveSpeed;
                            MoveDirectionEnum dir = MoveDirectionEnum.Up;
                            int len = Convert.ToInt32(extensionSetting.fRunDistanceAfterPrint * m_iPrinterChange.GetAllParam().PrinterProperty.fPulsePerInchY);
                            CoreInterface.MoveCmd((int)dir, len, speed);
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    LogWriter.SaveOptionLog("Exit fRunDistanceAfterPrint wait");
                }
                lock (this)
                {
                    m_bWorking = !(m_TerminalFlag || m_WorkingJobList.Count == 0);
                }
                if (!m_bWorking)
                {
                    m_WorkingJob = null;
                }
                m_TerminalFlag = m_AbortFlag = false;
                _currentJobPrintFinish = false;
                OnAllWorkFinished(new EventArgs());
            }
            catch (Exception e)
            {
                LogWriter.SaveOptionLog(e.Message + e.StackTrace);
                Debug.Assert(false, e.Message + e.StackTrace);
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }

        private void JobConfigPrintJob(UIJob job)
        {
            //TODO此处来不及仔细考虑结构
            AllParam allParam = this.m_iPrinterChange.GetAllParam();

            //if (m_IsUsedJobMode)
            {
                bool globalReversePrint = allParam.PrinterSetting.sBaseSetting.bReversePrint;//job.sJobSetting.bReversePrint;
                CoreInterface.UpdatePrinterSettingByJobPrintMode(ref allParam.PrinterSetting, m_JobMode.Item);

                //反向打印 ReversePrint
                job.sJobSetting.bReversePrint = (BoolEx.Global == m_JobMode.Item.ReversePrint)
                    ? globalReversePrint
                    : (BoolEx.True == m_JobMode.Item.ReversePrint);

            }
            if (m_IsUsedJobMediaMode)
            {
                //介质模式
                MediaConfig mediaMode = m_JobMediaMode.Item;
                CoreInterface.UpdatePrinterSettingByMediaMode(ref allParam.PrinterSetting, mediaMode);
            }
            CoreInterface.SetPrinterSetting(ref allParam.PrinterSetting);
            RealPrintJob(job);
            CoreInterface.SetPrinterSetting(ref allParam.PrinterSetting);
        }


        private void RealPrintJob(UIJob job)
        {
            //测试新布局代码
            LayoutSetting curLayoutSetting = new LayoutSetting();

            int LayoutIdx = 0;
            if (CoreInterface.LayoutIndex >= 0)
                LayoutIdx = CoreInterface.LayoutIndex;

            if (NewLayoutFun.GetLayoutSetting(LayoutIdx, ref curLayoutSetting))
            {
                this.m_iPrinterChange.GetAllParam().PrinterSetting.layoutSetting = curLayoutSetting;
                CoreInterface.SetPrinterSetting(ref this.m_iPrinterChange.GetAllParam().PrinterSetting);
            }
            else
            {
                MessageBox.Show("Layout setting not found, can't print", ResString.GetProductName());
                return;
            }

            const int c_BufferSize = 1024 * 1024;
            m_TerminalFlag = m_AbortFlag = false;
            if (File.Exists(job.FileLocation))
            {
                //
                AllParam allParam = this.m_iPrinterChange.GetAllParam();
                SPrinterProperty sp = allParam.PrinterProperty;
                SPrinterSetting ss = allParam.PrinterSetting;
                if (m_iPrinterChange != null)
                {
                    if (ss.sBaseSetting.bMeasureBeforePrint)
                        CoreInterface.SendJetCommand((int)JetCmdEnum.MeasurePaper, 0);
                }
                while (true)
                {
                    JetStatusEnum status = CoreInterface.CurBoardStatus;
                    if (status != JetStatusEnum.Moving && status != JetStatusEnum.Measuring)
                    {
                        break;
                    }
                    if (m_TerminalFlag || m_AbortFlag)
                    {
                        break;
                    }
                }

                // 上端job.sJobSetting.bReversePrint是等于ss.sBaseSetting.bReversePrint的;加入tcpip控制后改为用job.sJobSetting.bReversePrint
                bool bReversePrint = ss.sBaseSetting.bReversePrint; //job.sJobSetting.bReversePrint; 
                //				bool bCached = false;
                string sRealFilePath = job.FileLocation;
                XmlNode root = null;
                int count = 1;
                int colorNum = sp.nColorNum;
                bool isSingleLayerMode = allParam.ExtendedSettings.EnableSingleLayerMode;
                bool isFloraT50OrT180 = SPrinterProperty.IsFloraT50OrT180() && sp.nMediaType != 0;
                bool bLevelLingPrint = job.sJobSetting.bLevelLingPrint;
                if (bLevelLingPrint)
                {
                    string path = Path.Combine(Application.StartupPath, "ll.xml");
                    if (File.Exists(path))
                    {
                        SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                        doc.Load(path);
                        root = doc.FirstChild;
                        count = root.ChildNodes.Count;
                        if (count < 1) //如果xml文档有问题，则关闭留平打印
                        {
                            count = 1;
                            bLevelLingPrint = false;
                        }
                    }
                    else
                        bLevelLingPrint = false;
                }
                int printCopies = job.Copies;
                if (AutoInkTestHelper.Para.Enable)
                {
                    AutoInkTestHelper.BakupRealTimeInfo();
                    printCopies = AutoInkTestHelper.Para.PrintTimes;
                }
                int WorkPosBeginIdx = 0;
                int WorkPosEndIdx = 0;
                bool bReadCopiesCompition = false;
                List<int> WorkPosList = new List<int>();
                if (SPrinterProperty.IsJianRui())
                {
                    for (int i = 0; i < 4; i++)
                    {
                        bool isEnable = ((ss.sExtensionSetting.WorkPosEnable >> i) & 1) == 1;
                        if (isEnable)
                        {
                            WorkPosList.Add(ss.sExtensionSetting.WorkPosList[i]);
                            WorkPosEndIdx++;
                        }
                    }
                }


                //Check BoundingBox in this Area
                for (int i = 0; i < printCopies; i++)
                {
                    //坚锐厂家的打印中两个工位交替打印
                    if (SPrinterProperty.IsJianRui() && WorkPosList.Count > 0)
                    {
                        int pulse = WorkPosList[WorkPosBeginIdx];
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

                        WorkPosBeginIdx++;
                        if (WorkPosBeginIdx >= WorkPosEndIdx) WorkPosBeginIdx = 0;

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

                        LogWriter.WriteLog(new string[] { string.Format("[JianRui]Begin Print") }, true);
                    }

                    m_CopiesIndex = i + 1;
                    if (m_TerminalFlag || m_AbortFlag)
                    {
                        ChangeJobStatusTo(job, JobStatus.Idle);
                        break;
                    }
                    if (AutoInkTestHelper.Para.Enable)
                    {
                        AutoInkTestHelper.SetRealTimeInfoWithStep(i);
                        if (job.IsClipOrTile)
                        {
                            Thread.Sleep(100);
                            //job.Clips.UpdateNoteTextAndImage();
                        }
                    }
                    if (job.IsClipOrTile && ((job.Clips.AddtionInfoMask & 0x100000) > 0))//作业编辑界面中勾选了电压脉宽才会更新
                    {
                        job.Clips.UpdateNoteTextAndImage(ss);
                    }
                    for (int j = 0; j < count; j++)
                    {
                        int layerNum = 1;
                        bool bsupportwhite = (sp.nWhiteInkNum & 0x0F) > 0 &&
                                             ((ss.sBaseSetting.nLayerColorArray & 0x02) == 0);
                        bool bsupportVarnish = (sp.nWhiteInkNum >> 4) > 0 &&
                                               ((ss.sBaseSetting.nLayerColorArray & 0x04) == 0);
                        bool bsupportColor = ((ss.sBaseSetting.nLayerColorArray & 0x01) == 0);
                        uint[] masks = new uint[3];
                        if (isSingleLayerMode)
                        {
                            if (sp.bSupportWhiteInk)
                            {
                                layerNum = ss.sBaseSetting.nWhiteInkLayer == 0
                                    ? 1
                                    : (int)ss.sBaseSetting.nWhiteInkLayer;
                            }
                            if (sp.bSupportWhiteInkYoffset)
                            {
                                layerNum = 0;
                                if (bsupportColor)
                                {
                                    layerNum++;
                                }
                                if (bsupportwhite)
                                {
                                    layerNum++;
                                }
                                if (bsupportVarnish)
                                {
                                    layerNum++;
                                }
                                switch (layerNum)
                                {
                                    case 1:
                                        {
                                            masks[0] = ss.sBaseSetting.nLayerColorArray;
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (bsupportVarnish)
                                                masks[0] = 3; //只打亮油
                                            if (bsupportColor)
                                                masks[0] = 6; //只打彩
                                            if (bsupportwhite)
                                                masks[0] = 5; //只打白

                                            if (bsupportVarnish)
                                                masks[1] = 3; //只打亮油
                                            if (bsupportColor)
                                                masks[1] = 6; //只打彩
                                            break;
                                        }
                                    case 3:
                                        {
                                            masks[0] = 5; //只打白
                                            masks[1] = 6; //只打彩
                                            masks[3] = 3; //只打亮油
                                            break;
                                        }
                                }
                            }
                        }
                        for (int k = 0; k < layerNum; k++)
                        {
                            if (allParam.ExtendedSettings.bEnableDetectRollLength)
                            {
                                // 检查剩余纸长是否够用
                                PubFunc.RefreshRollLength();
                                if (allParam.ExtendedSettings.fCalculateRollLength < job.JobSize.Height)
                                {
                                    string errorInfo = SErrorCode.GetResString("Software_MediaHeightTooSmall");
                                    DialogResult result = MessageBox.Show(errorInfo, ResString.GetProductName(),
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    if (result == DialogResult.No)
                                    {
                                        m_TerminalFlag = true;
                                    }
                                }
                            }
                            if (k > 0 && isSingleLayerMode && isFloraT50OrT180)
                            {
                                // 彩神t50 第一层之后的不做预处理
                                FloraParamUI floraParamBak = allParam.ExtendedSettings.FloraParam;
                                FloraParamUI floraParam = allParam.ExtendedSettings.FloraParam;
                                floraParam.bIsNeedPrepare = false;
                                allParam.ExtendedSettings.FloraParam = floraParam;
                                EpsonLCD.SetFloraParam(allParam);
                                //恢复全集数据结构参数
                                allParam.ExtendedSettings.FloraParam = floraParamBak;

                                Thread.Sleep(allParam.ExtendedSettings.FloraParam.waitTime * 1000);
                            }
                            if (m_TerminalFlag || m_AbortFlag)
                            {
                                ChangeJobStatusTo(job, JobStatus.Idle);
                                break;
                            }
                            else
                                ChangeJobStatusTo(job, JobStatus.Printing);
                            int handle = 0;

                            float jobHeigt = job.SetHeight;
                            if (!job.IsHeight)
                            {
                                jobHeigt = job.JobSize.Height;
                            }
                            //CoreInterface.SetPrintHeight(jobHeigt);
                            CoreInterface.JobLineCount = (int)(jobHeigt * job.ResolutionY);
                            job.LineCount = 0;
                            CoreInterface.PrintType = 0;
                            CoreInterface.JobBegin = DateTime.Now;

                            if (!PubFunc.SupportDoubleSidePrint && !(job.IsClipOrTile || PubFunc.IsAwBMode(curLayoutSetting)))
                            {
                                LogWriter.SaveOptionLog("Enter Printer_Open wait");
                                while (!(m_AbortFlag || m_TerminalFlag || handle != 0))
                                {
                                    handle = CoreInterface.Printer_Open();
                                    if (handle == 0)
                                        Thread.Sleep(100);
                                }
                                LogWriter.SaveOptionLog("Exit Printer_Open wait");
                            }
                            //打印前设置JobSetting
                            SJobSetting sjobseting = new SJobSetting();
                            SSeviceSetting sSeviceSet_bak = allParam.SeviceSetting;
                            SPrinterSetting ssNew = ss;
                            //彩神T50 T180
                            if (isSingleLayerMode)
                            {
                                if (ss.sBaseSetting.bAutoCenterPrint)
                                {
                                    ssNew.sBaseSetting.bAutoCenterPrint = false; // 彩神只要y居中不要x居中
                                    //ssNew.sExtensionSetting.FlatSpaceY = 0;
                                    if (ss.sBaseSetting.fPaperHeight > job.JobSize.Height)
                                    {
                                        ssNew.sBaseSetting.fYOrigin = (ss.sBaseSetting.fPaperHeight - job.JobSize.Height) /
                                                                      2 + ss.sBaseSetting.fTopMargin;
                                    }
                                    else
                                    {
                                        ssNew.sBaseSetting.fYOrigin = 0;
                                    }
                                }
                                if (sp.bSupportWhiteInk)
                                {
                                    ssNew.sBaseSetting.nWhiteInkLayer = 1;
                                    ssNew.sBaseSetting.nLayerColorArray = (ss.sBaseSetting.nLayerColorArray >> (2 * k)) &
                                                                          0x03;
                                }
                                if (sp.bSupportWhiteInkYoffset)
                                {
                                    uint mask = masks[k];
                                    ssNew.sBaseSetting.nLayerColorArray = mask;
                                }

                                LogWriter.SaveOptionLog(
                                    string.Format("isFloraT50OrT180={0},layerNum={3}/{1},nLayerColorArray={2}--",
                                        isSingleLayerMode, layerNum, ssNew.sBaseSetting.nLayerColorArray, k + 1));
                            }
                            SSeviceSetting sSeviceSet = allParam.SeviceSetting;
                            if (bLevelLingPrint)
                            {
                                XmlNode xn = root.ChildNodes[j];
                                sjobseting.bReversePrint = bool.Parse(xn.Attributes["PrintDir"].Value);
                                CoreInterface.GetSeviceSetting(ref sSeviceSet);
                                sSeviceSet_bak = sSeviceSet;
                                string[] strmask = xn.Attributes["Mask"].Value.Split(',');
                                uint cur = 0xff;
                                foreach (string strbit in strmask)
                                {
                                    uint mask = 0xfe;
                                    int m = int.Parse(strbit);
                                    if (m > colorNum)
                                        continue;
                                    if (m == -1)
                                    {
                                        cur = 0xff;
                                    }
                                    else
                                    {
                                        mask <<= (m - 1);
                                        cur &= mask;
                                    }
                                }
                                sSeviceSet.unColorMask = cur;
                            }
                            else if (job.sJobSetting.bAlternatingPrint)
                            {
                                sjobseting.bReversePrint = bReversePrint;
                                bReversePrint = !bReversePrint;
                            }
                            else
                            {
                                sjobseting.bReversePrint = bReversePrint;
                            }

                            if (UIFunctionOnOff.SupportGlogalAlternatingPrint)
                            {
                                bool globalAlternatingPrint =
                                    this.m_iPrinterChange.GetAllParam().Preference.bAlternatingPrint;
                                if (globalAlternatingPrint)
                                {
                                    sjobseting.bReversePrint = m_bReversePrint;
                                    m_bReversePrint = !m_bReversePrint;
                                }
                            }

                            if (sp.Y_BackToOrgControlBySw())
                            {
                                if (isSingleLayerMode) //多层,每次只打一层的模式时,只有最后一层回原点
                                {
                                    sjobseting.Yorg = (byte)(ss.sExtensionSetting.bYBackOrigin && (k == layerNum - 1) ? 1 : 2);
                                }
                                else
                                {
                                    sjobseting.Yorg = (byte)(ss.sExtensionSetting.bYBackOrigin ? 1 : 2);
                                }
                                // !globalAlternatingPrint;
                            }
                            else
                            {
                                sjobseting.Yorg = 0;
                            }
                            //set jobID
                            if (true)
                            {
                                sjobseting.nJobID = job.JobID;
                                sjobseting.nJobIndex = _jobIndex;
                            }
                            sjobseting.bNeedWaitPrintStartSignal = k == 0;
                            string fileLocationTemp = job.FileLocation;
                            string prtPath = string.Empty;
                            if (PubFunc.IsZhuoZhan())
                            {
                                switch (allParam.Preference.ScanningAxis)
                                {
                                    case CoreConst.AXIS_X:
                                        PubFunc.SwitchPrintPlatform(allParam, CoreConst.AXIS_X, CoreConst.AXIS_X);
                                        sSeviceSet.scanningAxis = CoreConst.AXIS_X;
                                        allParam.Preference.ScanningAxis = CoreConst.AXIS_4;
                                        break;
                                    case CoreConst.AXIS_4:
                                        PubFunc.SwitchPrintPlatform(allParam, CoreConst.AXIS_4, CoreConst.AXIS_4);
                                        sSeviceSet.scanningAxis = CoreConst.AXIS_4;
                                        allParam.Preference.ScanningAxis = CoreConst.AXIS_X;
                                        break;
                                    default:
                                        PubFunc.SwitchPrintPlatform(allParam, CoreConst.AXIS_4, CoreConst.AXIS_4);
                                        sSeviceSet.scanningAxis = CoreConst.AXIS_4;
                                        allParam.Preference.ScanningAxis = CoreConst.AXIS_X;
                                        break;
                                }
#if false
                                // 等待卓展拍照后接口返回数据
                                while (true)
                                {
                                    if (m_TerminalFlag || m_AbortFlag)
                                    {
                                        break;
                                    }
                                    SysStorageUnit.SysRRead(Application.StartupPath, 101, 104);
                                    if ((int)SysStorageUnit.SysRegister[104] != 1) continue;
                                    var x = SysStorageUnit.SysRegister[101];
                                    var y = SysStorageUnit.SysRegister[102];
                                    var angle = (float)SysStorageUnit.SysRegister[103];
                                    byte[] buf = new byte[128];
                                    int ret = CoreInterface.RotationImage(job.FileLocation, buf, angle);
                                    if (ret > 0)
                                    {
                                        prtPath = System.Text.Encoding.Default.GetString(buf).Substring(0, ret);
                                        job.FileLocation = prtPath;
                                    }
                                    ssNew.sFrequencySetting.fXOrigin += (float)x;
                                    ssNew.sBaseSetting.fYOrigin =allParam.PrinterSetting.sBaseSetting.fYOrigin+ (float)y;
                                    SysStorageUnit.SysRegister[104] = 0;
                                    SysStorageUnit.SysRWrite(Application.StartupPath, 104, 104);
                                    break;
                                } 
#else
                                ssNew.sBaseSetting.fYOrigin = allParam.PrinterSetting.sBaseSetting.fYOrigin;
#endif
                            }
                            else
                            {
                                sSeviceSet.scanningAxis = job.sJobSetting.scanningAxis;
                            }

                            // 卷轴机类似平板应用
                            if (allParam.ExtendedSettings.IsFlatMode)
                            {
                                ssNew.sBaseSetting.fJobSpace = allParam.ExtendedSettings.PlatformCorrect + allParam.ExtendedSettings.fRoll2FlatJobSpace;
                                // 卷轴机类似平板应用,错排彩+白且只打彩时作业间距自动加上y间距,防止彩白都打和只打彩时起始点不同
                                if (sp.SurpportJobSpaceAsOriginY()
                                    && sp.bSupportWhiteInkYoffset
                                    && bsupportColor
                                    && !bsupportwhite)
                                {
                                    ssNew.sBaseSetting.fJobSpace += sp.fHeadYSpace;
                                }
                            }
                            if (m_WorkingJobList.Count == 0 && i == printCopies - 1 && j == count - 1 && k == layerNum - 1) // 列表的最后一个作业的最后一个copy
                            {
                                if (ss.sExtensionSetting.AutoRunAfterPrint)
                                {
                                    ssNew.sBaseSetting.fYAddDistance = ss.sExtensionSetting.fRunDistanceAfterPrint;
                                    ssNew.sExtensionSetting.bEnableAnotherUvLight = ssNew.sExtensionSetting.fRunDistanceAfterPrint > 0;
                                }
                            }
                            else
                            {
                                ssNew.sBaseSetting.fYAddDistance = 0;
                                ssNew.sExtensionSetting.bEnableAnotherUvLight = false;
                            }
                            if ((job.IsClipOrTile && PubFunc.SupportDoubleSidePrint) ||
                                                          !PubFunc.SupportDoubleSidePrint || PubFunc.IsAwBMode(curLayoutSetting)) ssNew.sExtensionSetting.ClipSpliceUnitIsPixel = 1;
                            CoreInterface.SetPrinterSetting(ref ssNew, false);
                            CoreInterface.SetSeviceSetting(ref sSeviceSet);
                            sjobseting.cNegMaxGray = job.sJobSetting.cNegMaxGray;
                            if (isSingleLayerMode)
                                sjobseting.bMultilayerCompleted = (byte)((k == layerNum - 1) ? 2 : 1);
                            CoreInterface.SetSJobSetting(ref sjobseting);
                            LogWriter.WriteLog(
                                new string[]
                                {
                                    string.Format(
                                        "Printing job[{0}],copies=[{1}/{6}],sjobseting.bReversePrint={2},sjobseting.Yorg={3},bNeedWaitPrintStartSignal={4},platform={5}",
                                        job.Name, i, sjobseting.bReversePrint, sjobseting.Yorg,
                                        sjobseting.bNeedWaitPrintStartSignal, sSeviceSet.scanningAxis,printCopies)
                                }, true);
                            CoreInterface.SetPrintMode((int)job.sJobSetting.pPrintMode);
                            // 打印前下发直通fw的作业相关参数
                            FwJobSettings fwJobSettings = new FwJobSettings();

                            int xOrigin = (int)(ss.sFrequencySetting.fXOrigin * sp.fPulsePerInchX);
                            int yOrigin = (int)(ss.sBaseSetting.fYOrigin * sp.fPulsePerInchY);
                            int width = (int)(job.JobSize.Width * sp.fPulsePerInchX); // / ResolutionX;
                            int height = (int)(job.JobSize.Height * sp.fPulsePerInchY); // / ResolutionY;
                            fwJobSettings.xStartPos = (uint)xOrigin;
                            fwJobSettings.xEndPos = (uint)(xOrigin + width);
                            fwJobSettings.yStartPos = (uint)xOrigin;
                            fwJobSettings.yEndPos = (uint)(yOrigin + height);
                            if (!EpsonLCD.SetFwJobSettings(fwJobSettings))
                            {
                                LogWriter.WriteLog(new string[] { "FwJobSettings sent fail!!" }, true);
                            }

                            try
                            {
                                #region dayin
                                #region 新打印逻辑

                                if (job.IsClipOrTile || PubFunc.IsAwBMode(curLayoutSetting))
                                {
                                    #region 剪切拼贴图片集合
                                    List<ImageTileItem> images = new List<ImageTileItem>();
                                    int prtcount = 1;
                                    if (PubFunc.IsAwBMode(curLayoutSetting))
                                        prtcount = 2;
                                    for (int prtnum = 0; prtnum < prtcount; prtnum++)
                                    {
                                        for (int ycnt = 0; ycnt < job.Clips.YCnt; ycnt++)
                                        {
                                            for (int xcnt = 0; xcnt < job.Clips.XCnt; xcnt++)
                                            {
                                                ImageTileItem item = new ImageTileItem();
                                                if (prtnum == 0)
                                                    item.File = job.FileLocation;
                                                else if (!string.IsNullOrEmpty(job.FileLocation2))
                                                    item.File = job.FileLocation2;
                                                item.ImageTileItemClip = new ImageTileItemClip()
                                                {
                                                    ClipX = (double)job.Clips.ClipRect.Width - (double)job.Clips.ClipRect.X,// / job.ResolutionX,
                                                    ClipY = (double)job.Clips.ClipRect.Y // / job.ResolutionY
                                                };
                                                if (job.IsClip)
                                                {
                                                    item.ImageTileItemClip.ClipX = (double)job.Clips.ClipRect.X;
                                                    item.ImageTileItemClip.ClipY = (double)job.Clips.ClipRect.Y;
                                                    item.ImageTileItemClip.ClipH = (double)job.Clips.ClipRect.Height;/// job.ResolutionY;
                                                    item.ImageTileItemClip.ClipW = (double)job.Clips.ClipRect.Width;/// job.ResolutionX;
                                                }
                                                else
                                                {
                                                    item.ImageTileItemClip.ClipX = 0;
                                                    item.ImageTileItemClip.ClipY = 0;
                                                    item.ImageTileItemClip.ClipH = (double)job.PrtFileInfo.sImageInfo.nImageHeight;/// job.ResolutionY;
                                                    item.ImageTileItemClip.ClipW = (double)job.PrtFileInfo.sImageInfo.nImageWidth;/// job.ResolutionX;
                                                }
                                                item.X = (item.ImageTileItemClip.ClipW * job.ResolutionX + job.Clips.XDis) * xcnt;/// job.ResolutionX;
                                                item.Y = (item.ImageTileItemClip.ClipH * job.ResolutionY + job.Clips.YDis) * ycnt;/// job.ResolutionY;
                                                item.X = (item.ImageTileItemClip.ClipW + job.Clips.XDis) * xcnt;/// job.ResolutionX;
                                                item.Y = (item.ImageTileItemClip.ClipH + job.Clips.YDis) * ycnt;/// job.ResolutionY;
                                                images.Add(item);
                                            }
                                        }
                                    }
                                    #endregion
                                    #region 打印批注文本
                                    NoteInfo notInfo = new NoteInfo();
                                    notInfo.AddtionInfoMask = job.Clips.AddtionInfoMask;
                                    notInfo.FontName = job.Clips.NoteFont.Name;
                                    notInfo.FontSize = (int)job.Clips.NoteFont.Size;
                                    notInfo.fontStyle = (int)((job.Clips.NoteFont.Bold ? FontStyle.Bold : 0) | (job.Clips.NoteFont.Italic ? FontStyle.Italic : 0)
                                        | (job.Clips.NoteFont.Strikeout ? FontStyle.Strikeout : 0)
                                        | (job.Clips.NoteFont.Underline ? FontStyle.Underline : 0));
                                    notInfo.NoteMargin = job.Clips.NoteMargin;
                                    notInfo.NotePosition = 3;//这里固定为Bottom，以后可能支持0,1,2,3

                                    #endregion
                                    double srcHeight = Math.Ceiling(job.JobSize.Height * job.ResolutionY);
                                    double srcWidth = Math.Ceiling(job.JobSize.Width * job.ResolutionX);
                                    bool bAwb = PubFunc.IsAwBMode(curLayoutSetting);
                                    CoreInterface.OpenMulitImagePrinter2(images.ToArray(), images.Count, srcHeight
                                    , srcWidth, bAwb, notInfo);
                                }
                                else if (!PubFunc.SupportDoubleSidePrint)
                                {
                                    #region 老打印

                                    bool bFileHeader = true;
                                    bool bNoteHeader = false;
                                    if (sjobseting.bReversePrint && job.HasNote)
                                    {
                                        bNoteHeader = true;
                                    }
                                    JobFileReader fileStream = new JobFileReader(job, false);
                                    byte[] buffer = new byte[c_BufferSize];
                                    int readBytes = 0;
                                    int sendBytes = 0;
                                    IntPtr memHandler = Marshal.AllocHGlobal(c_BufferSize);
                                    int step = 0;
                                    bool bNote = false;
#if DEBUG
                                    string dumppath = Path.Combine(Application.StartupPath, "1.prt");
                                    if (File.Exists(dumppath))
                                        File.Delete(dumppath);
#endif
                                    fileStream.Seek(0, SeekOrigin.Begin);
                                    while (!(m_TerminalFlag || m_AbortFlag))
                                    {
                                        bool bterm = false;
                                        if (sjobseting.bReversePrint)
                                        {
                                            readBytes = job.SendRevPrt(step, fileStream, bFileHeader, ref buffer,
                                                c_BufferSize, out bterm);
#if !ADD_HARDKEY
                                            bFileHeader = false;
#endif
                                            step++;
                                        }
                                        else
                                        {
                                            readBytes = fileStream.Read(buffer, 0, c_BufferSize);
                                            bterm = (readBytes != c_BufferSize);
                                        }

#if ADD_HARDKEY
                                                                            int subsize = 32;
                                                                            byte[] lastValue = buffer;
                                                                            if (bFileHeader)
                                                                            {
                                                                                bFileHeader = false;
                                                                                lastValue = new byte[c_BufferSize + 8];

                                                                                byte[] mjobhead = new byte[subsize];
                                                                                byte[] retValue = new byte[subsize + Marshal.SizeOf(typeof (int))];
                                                                                for (int n = 0; n < BYHXSoftLock.JOBHEADERSIZE/subsize; n++)
                                                                                {
                                                                                    Buffer.BlockCopy(buffer, n*mjobhead.Length, mjobhead, 0, mjobhead.Length);
                                                                                    BYHX_SL_RetValue ret = BYHXSoftLock.CheckValidDateWithData(mjobhead,
                                                                                        ref retValue);
                                                                                    Buffer.BlockCopy(retValue, 0, lastValue, n*retValue.Length,
                                                                                        retValue.Length);
                                                                                }
                                                                                Buffer.BlockCopy(buffer, BYHXSoftLock.JOBHEADERSIZE, lastValue,
                                                                                    BYHXSoftLock.JOBHEADERSIZE + 8,
                                                                                    buffer.Length - BYHXSoftLock.JOBHEADERSIZE);
                                                                                sendBytes = CoreInterface.Printer_Send(handle, lastValue, readBytes + 8);
                                                                                Debug.Assert(sendBytes == readBytes + 8);

                                                                            }
                                                                            else
                                                                            {
                                                                                sendBytes = CoreInterface.Printer_Send(handle, lastValue, readBytes);
                                                                                Debug.Assert(sendBytes == readBytes);
                                                                            }
#if DEBUG
                                                                            FileStream outfs = new FileStream(
                                                                                Path.Combine(Application.StartupPath, "1.prt"), FileMode.Append,
                                                                                FileAccess.Write, FileShare.ReadWrite);
                                                                            outfs.Write(lastValue, 0, sendBytes);
                                                                            outfs.Close();
#endif

#else
                                        sendBytes = CoreInterface.Printer_Send(handle, buffer, readBytes);
                                        Debug.Assert(sendBytes == readBytes);
#if DEBUG
                                        FileStream outfs = new FileStream(dumppath, FileMode.Append, FileAccess.Write,
                                            FileShare.ReadWrite);
                                        outfs.Write(buffer, 0, sendBytes);
                                        outfs.Close();
#endif
#endif

                                        if (bterm)
                                        {
                                            break;
                                        }
                                    }
                                    fileStream.Close();

                                    Marshal.FreeHGlobal(memHandler);
                                    if (handle != 0)
                                    {
                                        CoreInterface.Printer_Close(handle);
#if FHZL3D
                                                                            if(PubFunc.IsFhzl3D())//if (PubFunc.Is3DPrintMachine())
                                                                            {
                                                                                while (true)
                                                                                {
                                                                                    JetStatusEnum status = CoreInterface.GetBoardStatus();
                                                                                    if (status != JetStatusEnum.Busy
                                                                                        && status != JetStatusEnum.Pause
                                                                                        && status != JetStatusEnum.Cleaning
                                                                                        && status != JetStatusEnum.Spraying
                                                                                        ) // 非处于打印过程中的状态
                                                                                    {
                                                                                        if (!_currentJobPrintFinish)
                                                                                        {
                                                                                            string errorInfo =
                                                                                                SErrorCode.GetResString("Software_MeybeLostLayer");
                                                                                            DialogResult result = MessageBox.Show(errorInfo,
                                                                                                ResString.GetProductName(), MessageBoxButtons.YesNo,
                                                                                                MessageBoxIcon.Exclamation);
                                                                                            if (result == DialogResult.Yes)
                                                                                            {
                                                                                                m_WorkingJobList.Insert(0, m_WorkingJob);
                                                                                            }
                                                                                            LogWriter.SaveOptionLog(string.Format("==Software_MeybeLostLayer:status={0};result={1};_currentJobPrintFinish={2};_currentJobPrintPercent={3}",
                                                                                                status,result,_currentJobPrintFinish,_currentJobPrintPercent));
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                    Thread.Sleep(100);
                                                                                }
                                                                                _currentJobPrintFinish = false;
                                                                            }
#endif
                                    }
                                }
                                else
                                {
                                    LogWriter.WriteLog(string.Format("start Printing Printer_DoublePrint={0}", job.FileLocation), true);
                                    CoreInterface.Printer_DoublePrintFile(job.FileLocation); //不支持反向打印
                                }
                                    #endregion

                                #endregion


                                if (!m_TerminalFlag && !m_AbortFlag)
                                {
                                    ChangeJobStatusTo(job, JobStatus.Printed);

                                    if (UIFunctionOnOff.SupportGlogalAlternatingPrint
                                        && allParam.PrinterProperty.IsAllWinUV()
                                        && !(m_WorkingJobList.Count == 0 && i == printCopies - 1 && j == count - 1 && k == layerNum - 1) // 不是列表的最后一个作业的最后一个copy
                                    )
                                    {
                                        OnJobCopyPrinted(new EventArgs());
                                    }
                                }
                                else
                                {
                                    ChangeJobStatusTo(job, JobStatus.Idle);
                                    break;
                                }
                                //打印后恢复jobseting到正向打印
                                sjobseting.bReversePrint = false;
                                CoreInterface.SetSJobSetting(ref sjobseting);
                                //if (job.sJobSetting.bLevelLingPrint)
                                CoreInterface.SetSeviceSetting(ref sSeviceSet_bak);
                                CoreInterface.SetPrintMode((int)PrintMode.Normal);
                                //彩神T50 T180
                                //if (isSingleLayerMode)
                                {
                                    //if (ss.sBaseSetting.bAutoCenterPrint)
                                    {
                                        CoreInterface.SetPrinterSetting(ref ss, false);
                                    }
                                }
                                if (PubFunc.IsZhuoZhan())
                                {
                                    if (File.Exists(prtPath))
                                    {
                                        File.Delete(prtPath);
                                    }
                                    PubFunc.SwitchPrintPlatform(allParam, CoreConst.AXIS_X, CoreConst.AXIS_X);
                                    job.FileLocation = fileLocationTemp;
                                }
                                #endregion
                            }
                            catch (Exception e)
                            {
                                LogWriter.WriteLog(e.Message + e.StackTrace, true);
                                Debug.Assert(false, e.Message + e.StackTrace);
                                MessageBox.Show(e.Message + e.StackTrace);
                            }
                        }
                        if (AutoInkTestHelper.Para.Enable)
                        {
                            AutoInkTestHelper.WaitStatusToReady();
                        }
                        // 彩神t50 打印开始前重新应用预处理参数
                        if (isSingleLayerMode && isFloraT50OrT180)
                            EpsonLCD.SetFloraParam(allParam);
                    }
                }
                if (AutoInkTestHelper.Para.Enable)
                {
                    AutoInkTestHelper.Disable();
                    AutoInkTestHelper.RevertRealTimeInfo();
                }
            }
        }

        private void CreatePreviewJob(UIJob job)
        {
            m_TerminalFlag = m_AbortFlag = false;
            try
            {
                if (File.Exists(job.FileLocation))
                {
                    string jobTilePreViewFile = job.GeneratePreviewName(true);
                    string jobPreViewFile = job.GeneratePreviewName(false);

                    string jobMiniature = PubFunc.GetFullPreviewPath(jobPreViewFile);
                    string jobClipsSrcMiniature = PubFunc.GetFullPreviewPath(jobPreViewFile);
                    if (File.Exists(PubFunc.GetFullPreviewPath(jobPreViewFile)))
                    {
                        if (job.IsClipOrTile)
                        {
                            jobClipsSrcMiniature = PubFunc.GetFullPreviewPath(jobPreViewFile);//new Bitmap(stream);
                            Image srcImage = job.Clips.CreateClipsMiniature();
                            srcImage.Save(jobMiniature);
                        }
                        Image miniature = new Bitmap(jobMiniature);
                        //job.ThumbnailImage = PubFunc.CreateThumbnailImage(miniature, this.m_ThumbnailImageSize, Color.Wheat);
                        miniature.Dispose();
                        if (!m_TerminalFlag)
                            ChangeJobStatusTo(job, job.Status);
                    }
                    else
                    {
                        SPrtFileInfo jobInfo = new SPrtFileInfo();
                        IntPtr imgadataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SPrtImagePreview)));
                        jobInfo.sImageInfo.nImageData = imgadataPtr;
                        int bret = CoreInterface.Printer_GetFileInfo(job.FileLocation, ref jobInfo, 1);
                        if (bret == 1)
                        {
                            if (job.IsClipOrTile)
                            {
                                string path = PubFunc.GetFullPreviewPath(jobTilePreViewFile);
                                Bitmap image = SerialFunction.CreateImageWithImageInfo(jobInfo.sImageInfo);
                                image.Save(path);
                                image.Dispose();
                                jobClipsSrcMiniature = job.TilePreViewFile;
                                job.Clips.CreateClipsMiniature().Save(jobMiniature);
                            }
                            else
                            {
                                Bitmap image = SerialFunction.CreateImageWithImageInfo(jobInfo.sImageInfo);
                                image.Save(jobMiniature);
                                image.Dispose();
                            }
                            //Image miniature = new Bitmap(job.Miniature);
                            //job.ThumbnailImage = PubFunc.CreateThumbnailImage(miniature, this.m_ThumbnailImageSize, Color.Wheat);
                            //miniature.Dispose();
                            Marshal.FreeHGlobal(imgadataPtr);
                            if (!m_TerminalFlag)
                                ChangeJobStatusTo(job, job.Status);

                        }
                        else
                        {
                            string info = SErrorCode.GetEnumDisplayName(typeof(Software), Software.Parser);
                            info += ":" + job.FileLocation;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor.Current = Cursors.WaitCursor;
                        }
                    }
                    // 预览生成后再赋值相应属性,避免文件已创建但是未写入完成情况,导致异常
                    job.Miniature = jobMiniature;
                    job.Clips.SrcMiniature = jobClipsSrcMiniature;
                    job.TilePreViewFile = jobTilePreViewFile;
                    job.PreViewFile = jobPreViewFile;
                }
                GC.Collect(0);
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.Message + e.StackTrace);
            }
            m_TerminalFlag = m_AbortFlag = false;
        }

        private void GenDoublePrintPrt(UIJob job)
        {
            try
            {
                ChangeJobStatusTo(job, JobStatus.GenDoudleFile);
                SDoubleSidePrint param = new SDoubleSidePrint();
#if true
                param = m_iPrinterChange.GetAllParam().DoubleSidePrint;
#else
                string paramFile = Path.Combine(Application.StartupPath, "DoublePrint.xml");
                if(File.Exists(paramFile))
                {
                    SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                    doc.Load(paramFile);
                    XmlElement root = doc.DocumentElement;
                    try
                    {
                        if (root != null)
                        {
                          param = (SDoubleSidePrint) PubFunc.SystemConvertFromXml(root.FirstChild.OuterXml, param.GetType());
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Assert(false, e.Message + e.StackTrace);
                    }
                }
                else
                {
                    XmlElement root;
                    SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                    root = doc.CreateElement("", "DoublePrint", "");
                    doc.AppendChild(root);
                    string xml = PubFunc.SystemConvertToXml(param, param.GetType());
                    root.InnerXml = xml;
                    doc.Save(paramFile);
                }
#endif
#if false
                //Thread.Sleep(10000);                ;
#else
                string outfilePre = PubFunc.GetDoublePrintFileName(job.FileLocation, false);
                string outfilePos = PubFunc.GetDoublePrintFileName(job.FileLocation, true);
                CoreInterface.GenDoublePrintPrt(job.FileLocation, outfilePre, false, ref param);
                CoreInterface.GenDoublePrintPrt(job.FileLocation, outfilePos, true, ref param);
#endif
                ChangeJobStatusTo(job, JobStatus.Idle);
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
            }
        }

        private void PrintToFile(UIJob job)
        {
            const int c_BufferSize = 1024 * 1024;
            bool bReversePrint = m_iPrinterChange.GetAllParam().PrinterSetting.sBaseSetting.bReversePrint; // job.sJobSetting.bReversePrint;
            m_TerminalFlag = m_AbortFlag = false;
            if (File.Exists(job.FileLocation))
            {
                if (m_TerminalFlag || m_AbortFlag)
                {
                    ChangeJobStatusTo(job, JobStatus.Unknown);
                    return;
                }
                else
                    ChangeJobStatusTo(job, JobStatus.Creating);
                try
                {
                    bool bFileHeader = true;
#if true
                    LayoutSetting curLayoutSetting = new LayoutSetting();

                    int LayoutIdx = 0;
                    if (CoreInterface.LayoutIndex >= 0)
                        LayoutIdx = CoreInterface.LayoutIndex;

                    NewLayoutFun.GetLayoutSetting(LayoutIdx, ref curLayoutSetting);
                    JobFileReader fileStream = new JobFileReader(job, PubFunc.IsAwBMode(curLayoutSetting));
                    //BinaryReader binaryReader = new BinaryReader(fileStream.FileStream1);
#else
                            FileStream		fileStream		= new FileStream(sRealFilePath, FileMode.Open,FileAccess.Read,FileShare.Read);
                            BinaryReader binaryReader = new BinaryReader(fileStream);
#endif
                    byte[] buffer = new byte[c_BufferSize];
                    int readBytes = 0;
                    int sendBytes = 0;
                    int step = 0;
                    bool bNote = false;

                    fileStream.Seek(0, SeekOrigin.Begin);

                    FileStream clipFileStream = new FileStream(Path.Combine(Application.StartupPath, "printToFile.prt"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    BinaryWriter bunaryWriter = new BinaryWriter(clipFileStream);
                    while (!(m_TerminalFlag || m_AbortFlag))
                    {
                        bool bterm = false;
                        if (job.IsClipOrTile)
                        {
                            if (!bNote)
                            {
                                readBytes = job.ReadPrintBuf(step, fileStream, buffer, c_BufferSize, bFileHeader, out bNote, bReversePrint, out bterm);
                                bFileHeader = false;
                                step++;
                                if (bNote)
                                {
                                    step = 0;
                                    bFileHeader = true;
                                }
                            }
                            else
                            {
                                readBytes = job.ReadLabel(step, buffer, c_BufferSize, bFileHeader, out bterm, bReversePrint);
                                bFileHeader = false;
                                step++;
                            }
                        }
                        else if (bReversePrint)
                        {
                            readBytes = job.SendRevPrt(step, fileStream, bFileHeader, ref buffer, c_BufferSize, out bterm);
                            bFileHeader = false;
                            step++;
                        }
                        else
                        {
                            readBytes = fileStream.Read(buffer, 0, c_BufferSize);
                            bterm = (readBytes != c_BufferSize);
                        }
                        bunaryWriter.Write(buffer, 0, readBytes);
                        if (bterm)
                        {
                            break;
                        }
                    }
                    fileStream.Close();
                    bunaryWriter.Close();
                    clipFileStream.Close();
                    if (!m_TerminalFlag && !m_AbortFlag)
                    {
                        ChangeJobStatusTo(job, JobStatus.Idle);
                    }
                    else
                    {
                        ChangeJobStatusTo(job, JobStatus.Unknown);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Debug.Assert(false, e.Message + e.StackTrace);
                }
                m_TerminalFlag = m_AbortFlag = false;
            }
        }

        private void ChangeJobStatusTo(UIJob job, JobStatus status)
        {
            lock (job)
            {
                job.Status = status;
            }
            if (!m_TerminalFlag)
            {
                if (this.m_JobListForm.InvokeRequired)
                    this.m_JobListForm.Invoke(m_CallbackJobStatusChanged, new object[] { job });
                else
                    m_CallbackJobStatusChanged(job);
            }
        }
        public string GetCopiesString()
        {
            string ret;
            if (m_WorkingJob != null)
                ret = m_CopiesIndex.ToString() + "/" + m_WorkingJob.Copies.ToString();
            else
                ret = "";
            return ret;
        }

    }
    public class PrintJobTask : WorkerTask
    {
        public PrintJobTask(CallbackWorkingJobFinished change, Size thumbnailImage, ListView jobListForm)
            : base(change, 0, thumbnailImage, jobListForm)
        {
        }
    }

    public class PreviewJobTask : WorkerTask
    {
        public PreviewJobTask(CallbackWorkingJobFinished change, Size thumbnailImage, ListView jobListForm)
            : base(change, 1, thumbnailImage, jobListForm)
        {
        }
    }

    public class GenDoublePrintPrtWorker : WorkerTask
    {
        public GenDoublePrintPrtWorker(CallbackWorkingJobFinished change, Size thumbnailImage, ListView jobListForm)
            : base(change, 2, thumbnailImage, jobListForm)
        {
        }
    }

    public class PrintToFileWorker : WorkerTask
    {
        public PrintToFileWorker(CallbackWorkingJobFinished change, Size thumbnailImage, ListView jobListForm)
            : base(change, 3, thumbnailImage, jobListForm)
        {
        }
    }
}
