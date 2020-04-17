using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace BYHXPrinterManager.JobListView
{
    /// <summary>
    /// 打印记录，包括作业基本信息和打印参数，以及作业消耗墨量
    /// </summary>
    [Serializable]
    public class PrintRecord
    {
        private UIJob job;
        private long m_allCopiesTime;
        private Dictionary<ColorEnum, double> inkCount;
        private List<InnerInkCount> innerInkCounts;
        /// <summary>
        /// 打印长度
        /// </summary>
        private float printedLength;

        public float PrintedLength
        {
            get { return printedLength; }
            set { printedLength = value; }
        }
        private float printedArea;
        /// <summary>
        /// 打印面积
        /// </summary>
        public float PrintedArea
        {
            get { return printedArea; }
            set { printedArea = value; }
        }

        public PrintRecord()
        {
            inkCount = new Dictionary<ColorEnum, double>();
            innerInkCounts = new List<InnerInkCount>();
        }


        public UIJob Job
        {
            get { return job; }
            set { job = value; }
        }

        public long AllCopiesTime
        {
            get { return m_allCopiesTime; }
            set
            {
                if (m_allCopiesTime != value)
                {
                    m_allCopiesTime = value;
                }
            }
        }


        public TimeSpan UsedTime
        {
            get
            {
                return new TimeSpan(AllCopiesTime);
            }
        }


        /// <summary>
        /// 各个颜色的墨量统计(单位:L）
        /// </summary>
        public Dictionary<ColorEnum, double> InkCount
        {
            get { return inkCount; }
            set { inkCount = value; }
        }


        public List<InnerInkCount> InnerInkCounts
        {
            get { return innerInkCounts; }
        }
        
        private float fXOrigin;

        public float FXOrigin
        {
            get { return fXOrigin; }
            set { fXOrigin = value; }
        }

        private float fYOrigin;

        public float FYOrigin
        {
            get { return fYOrigin; }
            set { fYOrigin = value; }
        }
        /// <summary>
        /// 已打印的拼贴分数
        /// </summary>
        public int PrintedTileCount { get; set; }
        public string SystemConvertToXml()
        {
            innerInkCounts.Clear();
            foreach (var ink in InkCount.Keys)
            {
                InnerInkCount item = new InnerInkCount() { Color = ink, Value = InkCount[ink] };
                innerInkCounts.Add(item);
            }

            string xml = "<PrintRecord>";
            xml += job.SystemConvertToXml();
            xml += PubFunc.SystemConvertToXml(InnerInkCounts, typeof(List<InnerInkCount>));
            xml += PubFunc.SystemConvertToXml(AllCopiesTime, typeof(long));
            xml += PubFunc.SystemConvertToXml(PrintedLength, typeof(float));
            xml += PubFunc.SystemConvertToXml(PrintedArea, typeof(float));
            xml += PubFunc.SystemConvertToXml(FXOrigin, typeof(float));
            xml += PubFunc.SystemConvertToXml(FYOrigin, typeof(float));
            xml += PubFunc.SystemConvertToXml(PrintedTileCount, typeof(int));
            xml += "</PrintRecord>";
            return xml;
        }

        public static object SystemConvertFromXml(XmlElement printRecord, Type type)
        {
            PrintRecord record = new PrintRecord();
            XmlNode currNode = printRecord.FirstChild;
            record.Job = (UIJob)UIJob.SystemConvertFromXml((XmlElement)currNode, typeof(UIJob));

            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.innerInkCounts = (List<InnerInkCount>)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(List<InnerInkCount>));
            if (record.innerInkCounts != null)
            {
                foreach (var item in record.innerInkCounts)
                {
                    record.InkCount[item.Color] = item.Value;
                }
            }
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.AllCopiesTime = (long)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(long));
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.PrintedLength = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.PrintedArea = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.FXOrigin = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.FYOrigin = (float)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(float));
            currNode = currNode.NextSibling;
            if (currNode == null) return record;
            record.PrintedTileCount = (int)PubFunc.SystemConvertFromXml(currNode.OuterXml, typeof(int));
            return record;
        }

        [Serializable]
        public class InnerInkCount
        {
            /// <summary>
            /// 墨水颜色
            /// </summary>
            public ColorEnum Color { get; set; }

            /// <summary>
            /// 消耗墨量
            /// </summary>
            public double Value { get; set; }
        }

    }
}
