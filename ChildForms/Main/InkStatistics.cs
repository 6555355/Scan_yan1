using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Main
{
    public partial class InkStatistics : ByhxBaseChildForm
    {
        public InkStatistics()
        {
            InitializeComponent();
        }

        private void InkStatistics_Load(object sender, EventArgs e)
        {
            List<string> color = new List<string>();
            SPrinterProperty PrinterProperty = new SPrinterProperty();
            CoreInterface.GetSPrinterProperty(ref PrinterProperty);
            int colornum = CoreConst.OLD_MAX_COLOR_NUM;//PrinterProperty.GetRealColorNum();
            for (int i = 0; i < colornum; i++)
            {
                ColorEnum_Short ink = ColorEnum_Short.N;
                if (Enum.IsDefined(typeof(ColorEnum_Short), PrinterProperty.eColorOrder[i]))
                {
                    ink = (ColorEnum_Short)PrinterProperty.eColorOrder[i];
                }
                color.Add(ink.ToString());
            }


            InkOfMonths InkOfMonths = new InkOfMonths();
            AreaOfMonths AreaOfMonths = new AreaOfMonths();
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                string filePath = InkAreaStaticsHelper.GetInkStaticsFileName(sBoardInfo.m_nBoardSerialNum);
                if (File.Exists(filePath))
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] ink = new byte[Marshal.SizeOf(typeof (InkOfMonths))];
                    byte[] area = new byte[Marshal.SizeOf(typeof (AreaOfMonths))];
                    ink = br.ReadBytes(ink.Length);
                    area = br.ReadBytes(area.Length);
                    InkOfMonths = (InkOfMonths) SerializationUnit.BytesToStruct(ink, typeof (InkOfMonths));
                    AreaOfMonths = (AreaOfMonths) SerializationUnit.BytesToStruct(area.ToArray(), typeof (AreaOfMonths));
                    fs.Close();
                    DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                    acCode.Name = "color";
                    acCode.DataPropertyName = "";
                    acCode.HeaderText = "";
                    dataGridViewInkStatistics.Columns.Insert(0, acCode);
                    for (int i = 0; i < 8; i++)
                    {
                        dataGridViewInkStatistics.Rows.Add();
                        dataGridViewInkStatistics.Rows[i].Cells["color"].Value = color[i];
                    }
                    DataGridViewTextBoxColumn months;
                    // 获取对应语言的日期时间格式  
                    //DateTimeFormatInfo info = Thread.CurrentThread.CurrentUICulture.DateTimeFormat;

                    List<uint> allColorInk = new List<uint>(){0,0,0,0,0,0,0,0};
                    DateTime dateTime = BYHXSoftLock.GetDateTime(); // 优先用加密狗时间,避免机器时间错误
                    DateTime now = new DateTime(dateTime.Year - 1, dateTime.Month, dateTime.Day);
                    for (int i = 0; i < InkOfMonths.inkOfColors.Length; i++)
                    {
                        now = now.AddMonths(1);
                        months = new DataGridViewTextBoxColumn
                        {
                            Name = "",
                            DataPropertyName = "",
                            HeaderText =string.Format("{0}-{1}",now.Year,now.Month)//info.GetMonthName(i+1) //获取月份名称  
                        };
                        dataGridViewInkStatistics.Columns.Insert(i+1, months);
                    }
                    now = new DateTime(dateTime.Year - 1, dateTime.Month, dateTime.Day);
                    for (int i = 0; i < InkOfMonths.inkOfColors.Length; i++)
                    {
                        now = now.AddMonths(1);
                        int monthIndex = now.Month - 1;
                        for (int j = 0; j < InkOfMonths.inkOfColors[monthIndex].colors.Length; j++)
                        {
                            //dataGridViewInkStatistics.Rows[j].Cells[monthIndex + 1].Value = InkOfMonths.inkOfColors[monthIndex].colors[j].ToString("f2");
                            dataGridViewInkStatistics.Rows[j].Cells[i + 1].Value = InkOfMonths.inkOfColors[monthIndex].colors[j].ToString("f2");
                            allColorInk[j] += InkOfMonths.inkOfColors[monthIndex].colors[j];
                        }
                    }
                    //插入全年合计列
                    months = new DataGridViewTextBoxColumn
                    {
                        Name = "",
                        DataPropertyName = "",
                        HeaderText = ResString.GetResString("StrCurYearAmountInTotal")// "合计"
                    };
                    dataGridViewInkStatistics.Columns.Insert(13, months);
                    for (int i = 0; i < allColorInk.Count; i++)
                    {
                        dataGridViewInkStatistics.Rows[i].Cells[13].Value =string.Format("{0}L",(allColorInk[i]/1000f).ToString("f2"));
                    }

                    ////插入历史合计列
                    //months = new DataGridViewTextBoxColumn
                    //{
                    //    Name = "",
                    //    DataPropertyName = "",
                    //    HeaderText = ResString.GetResString("StrHistoryAmountInTotal")+"(L)"// "合计"
                    //};
                    //// 获取并显示历史合计数据
                    //SPwdInfo pwdinfo = new SPwdInfo();
                    //if (CoreInterface.GetPWDInfo(ref pwdinfo) != 0)
                    //{
                    //    for (int i = 0; i < allColorInk.Count && i < pwdinfo.nDurationInk.Length; i++)
                    //    {
                    //        dataGridViewInkStatistics.Rows[i].Cells[13].Value = pwdinfo.nDurationInk[i].ToString("f2");
                    //    }
                    //}
                    /////////////////////////////面积//////////////////////////////////
                    now = new DateTime(dateTime.Year - 1, dateTime.Month, dateTime.Day);
                    float inch2M2 = (100 / 2.54f) * (100 / 2.54f); //平方英寸到平方米的转换系数
                    double allMonthArea = 0; // 12个月合计打印面积
                    for (int i = 0; i < 12; i++)
                    {
                        now = now.AddMonths(1);
                        months = new DataGridViewTextBoxColumn
                        {
                            Name = "",
                            DataPropertyName = "",
                            HeaderText = string.Format("{0}-{1}", now.Year, now.Month)// info.GetMonthName(i + 1)
                        };
                        dataGridViewPrintAreaStatistics.Columns.Insert(i, months);
                    }
                    dataGridViewPrintAreaStatistics.Rows.Add();//添加行
                    now = new DateTime(dateTime.Year - 1, dateTime.Month, dateTime.Day);
                    for (int i = 0; i < 12; i++)
                    {
                        now = now.AddMonths(1);
                        int monthIndex = now.Month - 1;
                        //dataGridViewPrintAreaStatistics.Rows[0].Cells[monthIndex].Value = (AreaOfMonths.Area[monthIndex] / inch2M2).ToString("f2");
                        dataGridViewPrintAreaStatistics.Rows[0].Cells[i].Value = (AreaOfMonths.Area[monthIndex] / inch2M2).ToString("f2");
                        allMonthArea += AreaOfMonths.Area[monthIndex] / inch2M2;
                    }
                    //插入全年合计列
                    months = new DataGridViewTextBoxColumn
                    {
                        Name = "",
                        DataPropertyName = "",
                        HeaderText = ResString.GetResString("StrCurYearAmountInTotal")// "合计"
                    };
                    dataGridViewPrintAreaStatistics.Columns.Insert(12, months);

                    // 显示合计
                    dataGridViewPrintAreaStatistics.Rows[0].Cells[12].Value = allMonthArea.ToString("f2");

                    ////插入历史合计列
                    //months = new DataGridViewTextBoxColumn
                    //{
                    //    Name = "",
                    //    DataPropertyName = "",
                    //    HeaderText = ResString.GetResString("StrHistoryAmountInTotal")// "合计"
                    //};
                    //dataGridViewPrintAreaStatistics.Columns.Insert(12, months);

                    //// 显示历史合计
                    //double areaHistory = 0;
                    //if (CoreInterface.GetPrintArea(ref areaHistory) != 0)
                    //{
                    //    double m2 = areaHistory / inch2M2;
                    //    dataGridViewPrintAreaStatistics.Rows[0].Cells[12].Value = m2.ToString("f2");
                    //}
                }
            }

        }
    }
}
