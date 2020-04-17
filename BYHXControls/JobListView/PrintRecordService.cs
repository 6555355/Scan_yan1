using BYHXPrinterManager;
using BYHXPrinterManager.JobListView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.JobListView
{
    public class PrintRecordService 
    {
        private static readonly PrintRecordService instance=new PrintRecordService();

        public const string PRINT_RECORD_FOLDER = "PrintHistroy";
        
        private List<PrintRecord> printRecords = new List<PrintRecord>();
        private readonly Dictionary<ColorEnum, double> inkCountBase = new Dictionary<ColorEnum, double>();

        public static PrintRecordService GetInstance()
        {
            return instance;
        }

        private PrintRecordService()
        {
            
        }
        public List<MbInfo> GetMbList()
        {
            // 此处应该按照MultiMbConfig.ini配置文件确定界面显示几个主板
            List<MbInfo> mblist = new List<MbInfo>();
            int mbcount = CoreInterface.GetSystemUsbCnt();
            if (mblist == null || mblist.Count < mbcount + 1)
            {
#if false
                int usbNum = PubFunc.ExcuteEnumMBId().Count;
                if (mblist == null || mblist.Count - 1 < usbNum)
                {
                    mblist = new List<MbInfo>();
                    // 第一个为虚拟的主板,为多个主板合并状态后的虚拟主板
                    mblist.Add(new MbInfo(0, JetStatusEnum.PowerOff));

                    //modify by ljp 20140527
                    var mbidList = PubFunc.ExcuteEnumMBId();
                    //添加实际的物理主板
                    foreach (var mbid in mbidList)
                    {
                        mblist.Add(new MbInfo(mbid, JetStatusEnum.PowerOff));
                    }
                }
#else
                // 第一个为虚拟的主板,为多个主板合并状态后的虚拟主板
                mblist.Add(new MbInfo(0, JetStatusEnum.PowerOff));

                // 添加实际的物理主板
                for (int i = 0; i < mbcount; i++)
                {
                    mblist.Add(new MbInfo(i + 1, JetStatusEnum.PowerOff));
                }
#endif
            }

            return mblist;
        }
        /// <summary>
        /// 获取当前累计已消耗墨量
        /// </summary>
        /// <returns></returns>
        private List<Tuple<ColorEnum, double>> GetUsedInk()
        {
           
            List<Tuple<ColorEnum, double>> curInks = new List<Tuple<ColorEnum, double>>();
            try
            {
                if (EpsonLCD.IsNewDurationInkCmd())
                {
                    //pl墨量统计

                    #region PL单位墨量统计

                    long[] durationInk = new long[8];
                    if (EpsonLCD.GetDurationInk(ref durationInk))
                    {
                        List<MbInfo> list = GetMbList();
                        for (int i = 1; i < list.Count; i++) //多个主板 将每一个的都加到pwdinfo1里面
                        {
                            long[] durationInk2 = new long[8];
                            if (EpsonLCD.GetDurationInk(ref durationInk2, list[i].MbId))
                            {
                                if (durationInk2 == null)
                                    durationInk2 = durationInk;
                                else
                                {
                                    for (int j = 0; j < durationInk.Length; j++)
                                    {
                                        durationInk[j] += durationInk2[j];
                                    }
                                }
                            }
                        }

                        SPrinterProperty PrinterProperty = new SPrinterProperty();
                        CoreInterface.GetSPrinterProperty(ref PrinterProperty);
                        int colornum = PrinterProperty.GetRealColorNum();
                        for (int i = 0; i < colornum; i++)
                        {
                            if (i < durationInk.Length)
                            {
                                Tuple<ColorEnum, double> ink = new Tuple<ColorEnum, double>(
                                    (ColorEnum) PrinterProperty.eColorOrder[i],
                                    (((double) durationInk[i]) / (Math.Pow(10, 12))));
                                curInks.Add(ink);
                            }
                        }
                    }
                    else
                    {
                        LogWriter.SaveOptionLog("GetUsedInk:GetDurationInk fail!");                        
                    }
                    #endregion
                }
                else
                {
                    LogWriter.SaveOptionLog("GetUsedInk:IsNewDurationInkCmd=false!");
                    List<MbInfo> list = GetMbList();
                    SPwdInfo pwdinfo1 = new SPwdInfo();
                    for (int i = 1; i < list.Count; i++)
                    {
                        SPwdInfo pwdinfo2 = new SPwdInfo();
                        if (CoreInterface.GetPWDInfo(ref pwdinfo2, list[i].MbId) != 0)
                        {
                            if (pwdinfo1.nDurationInk == null)
                                pwdinfo1 = pwdinfo2;
                            else
                            {
                                for (int j = 0; j < pwdinfo1.nDurationInk.Length; j++)
                                {
                                    pwdinfo1.nDurationInk[j] += pwdinfo2.nDurationInk[j];
                                }
                            }
                        }
                    }

                    string mbversion = string.Empty;
                    if (pwdinfo1.nDurationInk != null)
                    {
                        SPrinterProperty PrinterProperty = new SPrinterProperty();
                        CoreInterface.GetSPrinterProperty(ref PrinterProperty);
                        int colornum = PrinterProperty.GetRealColorNum();
                        for (int i = 0; i < colornum && i < pwdinfo1.nDurationInk.Length; i++)
                        {
                            Tuple<ColorEnum, double> ink =
                                new Tuple<ColorEnum, double>((ColorEnum) PrinterProperty.eColorOrder[i],
                                    pwdinfo1.nDurationInk[i]);
                            curInks.Add(ink);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogWriter.SaveOptionLog("GetUsedInk:Exception="+ex.Message);
            }

            return curInks;
        }


        /// <summary>
        /// 开始记录，主要是做了以下事情：
        /// 1、清除上一次打印记录缓存
        /// 2、获取最新的墨量消耗，作为基准
        /// </summary>
        /// <param name="job"></param>
        public void StartRecord(UIJob job)
        {
            printRecords.Clear();
            inkCountBase.Clear();
            foreach (var colorInk in GetUsedInk())
            {
                inkCountBase[colorInk.Item1] = colorInk.Item2;
            }
        }
        
        /// <summary>
        /// 统计墨量
        /// 注：当前提供的接口只能获取到自第一次打印以来所消耗的各个颜色的墨量，
        /// 要计算某个作业消耗的墨量，只能是打印前和打印后各获取一次消耗的累计墨量，取两者的差值。
        /// </summary>
        /// <param name="job"></param>
        /// <param name="PrintedCopy">本作业打印的份数</param>
        /// <param name="endPagePercent">最后一份打印的百分比</param>
        public void CalculateInk(UIJob job,float printedWidth, int PrintedCopy, int currentPrintPage, float endPagePercent,SPrinterSetting printerSetting)
        {
            PrintRecord printRecord = new PrintRecord();
            printRecord.Job = job.Clone();
            printRecord.AllCopiesTime = job.AllCopiesTime.Ticks;
            foreach (var colorInk in GetUsedInk())
            {
                printRecord.InkCount[colorInk.Item1] = colorInk.Item2 - inkCountBase[colorInk.Item1];
            }
            if (PrintedCopy == currentPrintPage)
            {
                printRecord.PrintedLength = UIPreference.ToDisplayLength(UILengthUnit.Meter, job.JobSize.Height)
                    * (PrintedCopy - 1 + endPagePercent / 100.0f);
            }
            else
            {
                printRecord.PrintedLength = UIPreference.ToDisplayLength(UILengthUnit.Meter, job.JobSize.Height)
                    * (PrintedCopy + endPagePercent / 100.0f);
            }

            if (job.IsClipOrTile)
            {
                int printedHeight =
                    (int) (job.JobSize.Height * job.ResolutionY * endPagePercent / 100.0f + job.Clips.YDis);
                int printedcopy = PrintedCopy;
                if (PrintedCopy == currentPrintPage)
                    printedcopy = (PrintedCopy - 1);
                printRecord.PrintedTileCount = printedcopy * job.Clips.XCnt * job.Clips.YCnt + ((printedHeight / (job.Clips.ClipRect.Height + job.Clips.YDis)) * job.Clips.XCnt);
            }
            else
            {
                printRecord.PrintedTileCount = PrintedCopy;
            }
            printRecord.PrintedArea = printedWidth * printRecord.PrintedLength;
            printRecord.FXOrigin = printerSetting.sFrequencySetting.fXOrigin;
            printRecord.FYOrigin = printerSetting.sBaseSetting.fYOrigin;
            printRecords.Add(printRecord);

        }

        /// <summary>
        /// 保存打印记录到本地
        /// </summary>
        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("PrintRecordList");
            doc.AppendChild(root);
            string xml = "";
            for (int i = 0; i < printRecords.Count; i++)
            {
                PrintRecord printRecord = printRecords[i];
                printRecord.Job.Miniature = null;
                printRecord.Job.PrtFileInfo.sImageInfo.nImageData = IntPtr.Zero;
                xml += printRecord.SystemConvertToXml();
            }
            root.InnerXml = xml;
            var fullFileName = GenPrintRecordFullFileName();
            doc.Save(fullFileName);
        }

        public static List<PrintRecord> Load(string recordFileName)
        {
            List<PrintRecord> results = new List<PrintRecord>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(recordFileName);
                var root = doc.DocumentElement;
                foreach (XmlElement recordElement in root.ChildNodes)
                {
                    PrintRecord record = PrintRecord.SystemConvertFromXml(recordElement, typeof(PrintRecord)) as PrintRecord;
                    results.Add(record);
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            return results;
        }

        private string GenPrintRecordFullFileName()
        {
            string path = Path.Combine(PRINT_RECORD_FOLDER, DateTime.Now.Year.ToString(), DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, string.Format("{0}.xml", DateTime.Now.ToString("yyyyMMddhhmmss")));
        }
    }
    public struct MbInfo
    {
        public int MbId;
        public JetStatusEnum Status;
        public int Errorcode;

        public MbInfo(int id, JetStatusEnum status)
        {
            MbId = id;
            Status = status;
            Errorcode = 0;
        }
    }
}
