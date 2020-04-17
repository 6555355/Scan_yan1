using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    enum CloseColorNozzleModelEnum
    {
        Easy,
        Feather,
        Advanced
    }
    public partial class CloseColorNozzle_New : BYHXUserControl
    {
        int RowNum = 0;
        //int HeadNumPerRow = 0;
        int maxHeadNumPerRow = 0;
        int m_HorColumnNum = 1;
        SNozzleOverlap curNO = new SNozzleOverlap(null);
        private bool IsSupperUser;
        private CloseColorNozzleModelEnum CurrentModel = CloseColorNozzleModelEnum.Easy;
        private bool loadDataDone = false;
        List<LayoutColorNameEnum> LayoutColorNameEnums = new List<LayoutColorNameEnum>();
        List<List<LayoutColorNameEnum>> LayoutColorNameEnumListList = new List<List<LayoutColorNameEnum>>();
        bool isFW = false;
        //SNozzleOverlap fwData = new SNozzleOverlap(null);

        public CloseColorNozzle_New()
        {
            InitializeComponent();
            if (PubFunc.IsInDesignMode())
                return;
            //加载模式
            if (PubFunc.IsFactoryUser())
            {
                IsSupperUser = true;
            }
            comboBox_model.Items.Clear();
            foreach (CloseColorNozzleModelEnum place in Enum.GetValues(typeof(CloseColorNozzleModelEnum)))
            {
                string cmode = ResString.GetEnumDisplayName(typeof(CloseColorNozzleModelEnum), place);
                comboBox_model.Items.Add(cmode);
            }
            if (!IsSupperUser)
            {
                comboBox_model.Items.RemoveAt(2);
            }
            RowNum = CoreInterface.GetRowNum();
            if (RowNum > 1) RowNum -= 1;
            //HeadNumPerRow = 0;
            //maxHeadNumPerRow = 0;
            //for (int i = 0; i < RowNum; i++)
            //{
            //    HeadNumPerRow = CoreInterface.GetHeadNumPerRow(i);
            //    if (maxHeadNumPerRow < HeadNumPerRow)
            //    {
            //        maxHeadNumPerRow = HeadNumPerRow;
            //    }
            //}

            //int lineIdx = 1;
            //for (int i = 0; i < RowNum; i++)
            //{
            //    List<byte> tempArray = new List<byte>();
            //    int lineNum = CoreInterface.GetLineNumPerRow(i);
            //    for (int j = 0; j < lineNum; j++)
            //    {
            //        NozzleLineData lineData = new NozzleLineData();
            //        CoreInterface.GetlineIDtoNozzleline(lineIdx, ref lineData);

            //        if (tempArray.IndexOf(lineData.ColorID) < 0)
            //        {
            //            tempArray.Add(lineData.ColorID);
            //        }

            //        lineIdx++;
            //    }

            //    if (maxHeadNumPerRow < tempArray.Count)
            //    {
            //        maxHeadNumPerRow = tempArray.Count;
            //    }

            //}

            //从JobPrint中获取支持的最大颜色数
            maxHeadNumPerRow = CoreInterface.GetLayoutColorNum();
            for (int i = 0; i < maxHeadNumPerRow; i++)
            {
                LayoutColorNameEnums.Add((LayoutColorNameEnum)CoreInterface.GetLayoutColorID(i));
            }

            m_HorColumnNum = CoreInterface.GetMaxColumnNum();
            if (m_HorColumnNum < 1) m_HorColumnNum = 1;
            //if (maxHeadNumPerRow < 2) maxHeadNumPerRow = 2;

            comboBox_model.SelectedIndex = 0;
        }

        public void OnPrinterSettingChange(AllParam ss)
        {
            SynchFromCalib(ref ss);
            curNO = ss.NozzleOverlap;
            CurrentModel = (CloseColorNozzleModelEnum)ss.Preference.CloseNozzleSetMode;
            comboBox_model.SelectedIndex = (int)ss.Preference.CloseNozzleSetMode;
            ss.NozzleOverlap = GetDefaultNozzleOverlap(CurrentModel);
            LoadData(ss.NozzleOverlap);
            loadDataDone = true;
        }

        private void SynchFromCalib(ref AllParam ss)
        {
            int m_ColorNum = CoreInterface.GetLayoutColorNum();
            int m_ColorNumAndColumnNum = m_ColorNum * m_HorColumnNum;
            SNozzleOverlap n = ss.NozzleOverlap;
            int rowNum = CoreInterface.GetRowNum();
            if (rowNum > 1) rowNum--;
            int MaxColorNum = 32;//SNozzleOverlap结构体为32色*32组
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                for (int j = 0; j < rowNum; j++)
                {
                    n.OverLapTotalNozzle[j * MaxColorNum + i] = (ushort)(ss.PrinterSetting.sCalibrationSetting.nVerticalArray[(j + 1) * m_ColorNumAndColumnNum + i]);
                }
            }
            ss.NozzleOverlap= n;
            
        }

        public void OnGetPrinterSetting(ref AllParam ss)
        {
            //ushort[] validnum = new ushort[CoreConst.MAX_HEAD_NUM * maxColor];
            //byte[] fwData = new byte[CoreConst.MAX_HEAD_NUM * maxColor * 3];
            
            ss.NozzleOverlap= SaveData();
            ss.Preference.CloseNozzleSetMode = (byte)comboBox_model.SelectedIndex;
            curNO = ss.NozzleOverlap;
           
        }

        private bool SetOverLapCheckData(SNozzleOverlap fwData)
        {
            bool[] rets = new bool[5];
            rets[0] = UpdatePS(PrinterSettingCmd.OverLapTotalNozzle, fwData.OverLapTotalNozzle);
            rets[1] = UpdatePS(PrinterSettingCmd.OverLapUpWasteNozzle, fwData.OverLapUpWasteNozzle);
            rets[2] = UpdatePS(PrinterSettingCmd.OverLapDownWasteNozzle, fwData.OverLapDownWasteNozzle);
            rets[3] = UpdatePS(PrinterSettingCmd.OverLapUpPercent, fwData.OverLapUpPercent);
            rets[4] = UpdatePS(PrinterSettingCmd.OverLapDownPercent, fwData.OverLapDownPercent);
            for (int i = 0; i < 5; i++)
            {
                if (!rets[i]) return false;
            }
            return true;
        }
        private bool UpdatePS(PrinterSettingCmd pc, ushort[] data)
        {
            byte[] byteData = new byte[data.Length * 2];
            for (int j = 0; j < data.Length; j++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(data[j]), 0, byteData, j * 2, 2);
            }
            int ret = CoreInterface.UpdatePrinterSetting((int)pc, byteData, byteData.Length, 0, 79);
            return ret != 0;
        }
        private int GetColorIndex(List<LayoutColorNameEnum> LayoutColorNameEnumList_CurrentRow, int index_UI)
        {
            int index = 0;
            LayoutColorNameEnum color = LayoutColorNameEnumList_CurrentRow[index_UI];
            for (int k = 0; k < LayoutColorNameEnums.Count; k++)
            {
                if (color == LayoutColorNameEnums[k])
                {
                    index = k;
                    break;
                }
            }
            return index;
        }

        private int GetValueIdx(int tempIdx, int rowIdx)
        {
            return (tempIdx / maxHeadNumPerRow * maxHeadNumPerRow) + GetColorIndex(LayoutColorNameEnumListList[rowIdx], tempIdx % maxHeadNumPerRow);
        }

        private void LoadData(SNozzleOverlap bs)
        {
            try
            {
                //bool isFW = false;
                //byte[] fwData = new byte[CoreConst.MAX_HEAD_NUM * maxColor * 2];
                //原研二代码为：if (!loadDataDone)
                //{
                //    if (EpsonLCD.GetOverLapCheckData(ref fwData))
                //    {
                //        isFW = true;
                //        btnOverLapCheck.Visible = true;
                //    }
                //    else
                //    {
                //        isFW = false;
                //        btnOverLapCheck.Visible = false;
                //    }
                //}

                if (true)//原研二代码为：(Misc.IsNewCalibration)
                {
                    isFW = false;
                    btnOverLapCheck.Visible = false;
                    btnApply.Visible = false;
                }

                for (int i = 0; i < RowNum; i++)
                {
                    ushort[] GetValue = new ushort[maxColor];
                    byte[] RowData = new byte[maxColor * 3];
                    int[] upValue = new int[maxColor];
                    //int[] downValue = new int[maxColor];
                    int[] upCNValue = new int[maxColor];
                    int[] downCNValue = new int[maxColor];
                    int[] upPercentValue = new int[maxColor];
                    int[] downPercentValue = new int[maxColor];

                    if (isFW)
                    {
                        //原研二代码为：Array.Copy(fwData, i * maxColor * 3, RowData, 0, maxColor * 3);
                        for (int j = 0; j < maxColor; j++)
                        {
                            //upValue[j] = RowData[j * 2];
                            //downValue[j] = RowData[j * 2 + 1];
                            upValue[j] = RowData[j * 3];
                            //downValue[j] = RowData[j * 3];
                        }
                    }
                    else
                    {
                        Array.Copy(bs.OverLapTotalNozzle, i * maxColor, GetValue, 0, maxColor);
                        for (int j = 0; j < GetValue.Length; j++)
                        {
                            byte[] tempByte = BitConverter.GetBytes(GetValue[j]);

                            upValue[j] = tempByte[0];
                            //downValue[j] = tempByte[0];
                        }
                    }

                    //上
                    GetValue = new ushort[maxColor];
                    Array.Copy(bs.OverLapUpWasteNozzle, i * maxColor, GetValue, 0, maxColor);
                    for (int j = 0; j < GetValue.Length; j++)
                    {
                        upCNValue[j] = GetValue[j];
                    }

                    //下
                    GetValue = new ushort[maxColor];
                    Array.Copy(bs.OverLapDownWasteNozzle, i * maxColor, GetValue, 0, maxColor);
                    for (int j = 0; j < GetValue.Length; j++)
                    {
                        downCNValue[j] = GetValue[j];
                    }

                    //上百分比
                    GetValue = new ushort[maxColor];
                    Array.Copy(bs.OverLapUpPercent, i * maxColor, GetValue, 0, maxColor);
                    for (int j = 0; j < GetValue.Length; j++)
                    {
                        upPercentValue[j] = GetValue[j];
                    }

                    //下百分比
                    GetValue = new ushort[maxColor];
                    Array.Copy(bs.OverLapDownPercent, i * maxColor, GetValue, 0, maxColor);
                    for (int j = 0; j < GetValue.Length; j++)
                    {
                        downPercentValue[j] = GetValue[j];
                    }

                    int upIdx = 0;
                    int downIdx = 0;
                    int upCNIdx = 0;
                    int downCNIdx = 0;
                    int upPercent = 0;
                    int downPercent = 0;
                    Panel panel = (Panel)flowLayoutPanel1.Controls[i];
                    List<LayoutColorNameEnum> LayoutColorNameEnumList = LayoutColorNameEnumListList[i];

                    //for (int j = panel.Controls.Count - 1; j >= 0; j--)
                    for (int j = 0; j < panel.Controls.Count; j++)
                    {
                        switch (CurrentModel)
                        {
                            case CloseColorNozzleModelEnum.Easy:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upValue[GetValueIdx(downIdx, i)];
                                        downIdx++;
                                    }
                                }
                                break;
                            case CloseColorNozzleModelEnum.Feather:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upValue[GetValueIdx(downIdx, i)];
                                        downIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upPercentValue[GetValueIdx(upPercent, i)];
                                        upPercent++;
                                    }
                                }
                                break;
                            case CloseColorNozzleModelEnum.Advanced:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upValue[GetValueIdx(downIdx, i)];
                                        downIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpCN")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upCNValue[GetValueIdx(upCNIdx, i)];
                                        upCNIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "DownCN")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = downCNValue[GetValueIdx(downCNIdx, i)];
                                        downCNIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = upPercentValue[GetValueIdx(upPercent, i)];
                                        upPercent++;
                                    }
                                    else if (panel.Controls[j].Tag == "DownPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        num.Value = downPercentValue[GetValueIdx(downPercent, i)];
                                        downPercent++;
                                    }
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int maxColor = 32;//接口定义最大颜色数为 32
        private SNozzleOverlap SaveData()
        {
            SNozzleOverlap sNO = new SNozzleOverlap(null);
            try
            {
                for (int i = 0; i < RowNum; i++)
                {
                    ushort[] GetValue = new ushort[maxColor];
                    ushort[] upCNValue = new ushort[maxColor];
                    ushort[] downCNValue = new ushort[maxColor];
                    ushort[] upPercentValue = new ushort[maxColor];
                    ushort[] downPercentValue = new ushort[maxColor];

                    int upIdx = 0;
                    int downIdx = 0;
                    int upCNIdx = 0;
                    int downCNIdx = 0;
                    int upPercent = 0;
                    int downPercent = 0;
                    Panel panel = (Panel)flowLayoutPanel1.Controls[i];
                    List<LayoutColorNameEnum> LayoutColorNameEnumList = LayoutColorNameEnumListList[i];

                    //for (int j = panel.Controls.Count - 1; j >= 0; j--)
                    for (int j = 0; j < panel.Controls.Count; j++)
                    {
                        switch (CurrentModel)
                        {
                            case CloseColorNozzleModelEnum.Easy:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        GetValue[GetValueIdx(downIdx, i)] = (ushort)num.Value;


                                        upCNValue[GetValueIdx(downIdx, i)] = (ushort)num.Value;
                                        downCNValue[GetValueIdx(downIdx, i)] = (ushort)0;
                                        upPercentValue[GetValueIdx(downIdx, i)] = (ushort)50;
                                        downPercentValue[GetValueIdx(downIdx, i)] = (ushort)50;

                                        downIdx++;
                                    }
                                }
                                break;
                            case CloseColorNozzleModelEnum.Feather:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        GetValue[GetValueIdx(downIdx, i)] = (ushort)num.Value;


                                        upCNValue[GetValueIdx(downIdx, i)] = 0;
                                        downCNValue[GetValueIdx(downIdx, i)] = (ushort)0;
                                        downPercentValue[GetValueIdx(downIdx, i)] = (ushort)0;

                                        downIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        upPercentValue[GetValueIdx(upPercent, i)] = (ushort)num.Value;
                                        upPercent++;
                                    }
                                }
                                break;
                            case CloseColorNozzleModelEnum.Advanced:
                                {
                                    if (panel.Controls[j].Tag == "OverLap")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        GetValue[GetValueIdx(downIdx, i)] = (ushort)num.Value;
                                        downIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpCN")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        upCNValue[GetValueIdx(upCNIdx, i)] = (ushort)num.Value;
                                        upCNIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "DownCN")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        downCNValue[GetValueIdx(downCNIdx, i)] = (ushort)num.Value;
                                        downCNIdx++;
                                    }
                                    else if (panel.Controls[j].Tag == "UpPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        upPercentValue[GetValueIdx(upPercent, i)] = (ushort)num.Value;
                                        upPercent++;
                                    }
                                    else if (panel.Controls[j].Tag == "DownPercent")
                                    {
                                        NumericUpDown num = (NumericUpDown)panel.Controls[j];
                                        downPercentValue[GetValueIdx(downPercent, i)] = (ushort)num.Value;
                                        downPercent++;
                                    }
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    //for (int j = 0; j < GetValue.Length; j++)
                    //{
                    //    byte[] tempByte = new byte[2];

                    //    tempByte[0] = (byte)downValue[j];
                    //    tempByte[1] = (byte)upValue[j];
                    //    GetValue[j] = (ushort)BitConverter.ToInt16(tempByte, 0);

                    //    //原研二代码为：    fwData[i * maxColor * 3 + j * 3] = (byte)upValue[j];
                    //    //    fwData[i * maxColor * 3 + j * 3 + 1] = (byte)0;
                    //    //    fwData[i * maxColor * 3 + j * 3 + 2] = (byte)0;
                    //}
                   
                    Array.Copy(GetValue, 0, sNO.OverLapTotalNozzle, i * maxColor, maxColor);
                    Array.Copy(upCNValue, 0, sNO.OverLapUpWasteNozzle, i * maxColor, maxColor);
                    Array.Copy(downCNValue, 0, sNO.OverLapDownWasteNozzle, i * maxColor, maxColor);
                    Array.Copy(upPercentValue, 0, sNO.OverLapUpPercent, i * maxColor, maxColor);
                    Array.Copy(downPercentValue, 0, sNO.OverLapDownPercent, i * maxColor, maxColor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sNO;
        }

        private void BuildLayoutPanel()
        {
            int lineIdx = 1;
            List<string> NameArray = new List<string>();
            for (int i = 0; i < RowNum; i++)
            {
                //int HeadNumPerRow = CoreInterface.GetHeadNumPerRow(i);
                //byte[] HeadID = new byte[HeadNumPerRow];
                NameArray = new List<string>();

                //int lineNum = CoreInterface.GetLineNumPerRow(i);
                //for (int j = 0; j < lineNum; j++)
                //{
                //    NozzleLineData lineData = new NozzleLineData();

                //    CoreInterface.GetlineIDtoNozzleline(lineIdx, ref lineData);

                //    string temp = GetColorName((int)lineData.ColorID);
                //    //temp = "H" + lineData.HeadID.ToString() + temp.Trim();

                //    if (NameArray.IndexOf(temp) < 0)
                //    {
                //        NameArray.Add(temp);
                //    }

                //    lineIdx++;
                //}

                //从JobPrint中获取当前行显示哪些颜色，按64bit存储，显示为1，不显示为0，色序为颜色枚举值LayoutColorNameEnum
                long rowColor = CoreInterface.GetRowColor(i);
                List<LayoutColorNameEnum> LayoutColorNameEnumList = new List<LayoutColorNameEnum>();
                for (int j = 0; j < 64; j++)
                {
                    if (((rowColor >> j) & 0x01) == 1)
                    {
                        string name = Enum.GetName(typeof(LayoutColorNameEnum), (j + 1));
                        LayoutColorNameEnumList.Add((LayoutColorNameEnum)(j + 1));
                        NameArray.Add(name);
                    }
                }
                LayoutColorNameEnumListList.Add(LayoutColorNameEnumList);

                Panel panel = BuileLayerPanel(NameArray);
                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        private Panel BuileLayerPanel(List<string> NameArray)
        {
            Panel p = new Panel();

            int top = 5;
            int left = 10;
            int TitleWidth = 100;
            int lblWidth = 40;
            int ctlHeight = 24;
            int ctlWidth = 40;
            int spacingW = 20;
            int spacingH = 3;
            int width = 0;

            p.BorderStyle = BorderStyle.FixedSingle;
            switch (CurrentModel)
            {
                case CloseColorNozzleModelEnum.Easy:
                    p.Height = 60;
                    break;
                case CloseColorNozzleModelEnum.Feather:
                    p.Height = 85;
                    break;
                case CloseColorNozzleModelEnum.Advanced:
                    p.Height = 155;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            width = (ctlWidth + spacingW) * maxHeadNumPerRow * m_HorColumnNum + m_HorColumnNum * spacingW + TitleWidth;

            if (width < 500)
                width = 500;
            p.Width = width;

            //左标题
            top = 5 + ctlHeight;
            Label label = new Label();
            label.Width = TitleWidth;
            label.Text = ResString.GetResString("CloseColorNozzle_OverLapNozzle");
            label.Location = new Point(left, top);
            p.Controls.Add(label);
            switch (CurrentModel)
            {
                case CloseColorNozzleModelEnum.Easy:
                    break;
                case CloseColorNozzleModelEnum.Feather:

                    top = top + ctlHeight;
                    label = new Label();
                    label.Width = TitleWidth;
                    label.Text = ResString.GetResString("CloseColorNozzle_UpPercent");
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);
                    break;
                case CloseColorNozzleModelEnum.Advanced:

                    top = top + ctlHeight;
                    label = new Label();
                    label.Width = TitleWidth;
                    label.Text = ResString.GetResString("CloseColorNozzle_UpCloseNozzle");
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);

                    top = top + ctlHeight;
                    label = new Label();
                    label.Width = TitleWidth;
                    label.Text = ResString.GetResString("CloseColorNozzle_DownCloseNozzle");
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);

                    top = top + ctlHeight;
                    label = new Label();
                    label.Width = TitleWidth;
                    label.Text = ResString.GetResString("CloseColorNozzle_UpPercent");
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);

                    top = top + ctlHeight;
                    label = new Label();
                    label.Width = TitleWidth;
                    label.Text = ResString.GetResString("CloseColorNozzle_DownPercent");
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);
                    break;
            }

            left = left + TitleWidth;

            //for (int i = NameArray.Count - 1; i >= 0; i--)
            for (int hg = 0; hg < m_HorColumnNum; hg++)
            {
                for (int i = 0; i < NameArray.Count; i++)
                {
                    string name = NameArray[i];

                    top = 5;
                    label = new Label();
                    label.Width = lblWidth;
                    label.Text = name;
                    label.Location = new Point(left, top);
                    p.Controls.Add(label);

                    top = top + ctlHeight;
                    NumericUpDown numOverLap = new NumericUpDown();
                    numOverLap.Width = ctlWidth;
                    numOverLap.Maximum = 255;
                    numOverLap.Minimum = 0;
                    numOverLap.Value = 0;
                    numOverLap.Tag = "OverLap";
                    numOverLap.Location = new Point(left, top);
                    //p.Controls.Add(numOverLap);

                    switch (CurrentModel)
                    {
                        case CloseColorNozzleModelEnum.Easy:
                            numOverLap.Enabled = false;
                            p.Controls.Add(numOverLap);
                            break;
                        case CloseColorNozzleModelEnum.Feather:
                            {
                                numOverLap.Enabled = false;
                                p.Controls.Add(numOverLap);
                                top = top + ctlHeight;
                                NumericUpDown numUpPercent = new NumericUpDown();
                                numUpPercent.Width = ctlWidth;
                                numUpPercent.Maximum = 100;
                                numUpPercent.Minimum = 0;
                                numUpPercent.Value = 0;
                                numUpPercent.Tag = "UpPercent";
                                numUpPercent.Location = new Point(left, top);
                                p.Controls.Add(numUpPercent);
                            }
                            break;
                        case CloseColorNozzleModelEnum.Advanced:
                            {
                                numOverLap.Enabled = false;
                                p.Controls.Add(numOverLap);
                                top = top + ctlHeight;
                                NumericUpDown numUpCN = new NumericUpDown();
                                numUpCN.Width = ctlWidth;
                                numUpCN.Maximum = 255;
                                numUpCN.Minimum = 0;
                                numUpCN.Value = 0;
                                numUpCN.Tag = "UpCN";
                                numUpCN.Location = new Point(left, top);
                                p.Controls.Add(numUpCN);

                                top = top + ctlHeight;
                                NumericUpDown numDownCN = new NumericUpDown();
                                numDownCN.Width = ctlWidth;
                                numDownCN.Maximum = 255;
                                numDownCN.Minimum = 0;
                                numDownCN.Value = 0;
                                numDownCN.Tag = "DownCN";
                                numDownCN.Location = new Point(left, top);
                                p.Controls.Add(numDownCN);

                                top = top + ctlHeight;
                                NumericUpDown numUpPercent = new NumericUpDown();
                                numUpPercent.Width = ctlWidth;
                                numUpPercent.Maximum = 100;
                                numUpPercent.Minimum = 0;
                                numUpPercent.Value = 0;
                                numUpPercent.Tag = "UpPercent";
                                numUpPercent.Location = new Point(left, top);
                                p.Controls.Add(numUpPercent);

                                top = top + ctlHeight;
                                NumericUpDown numDownPercent = new NumericUpDown();
                                numDownPercent.Width = ctlWidth;
                                numDownPercent.Maximum = 100;
                                numDownPercent.Minimum = 0;
                                numDownPercent.Value = 0;
                                numDownPercent.Tag = "DownPercent";
                                numDownPercent.Location = new Point(left, top);
                                p.Controls.Add(numDownPercent);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }


                    left = left + lblWidth + spacingW;
                }

                left = left + spacingW;
            }

            return p;
        }

        string GetColorName(int colorIdx)
        {
            string name = "";

            name = Enum.GetName(typeof(LayoutColorNameEnum), colorIdx);

            return name;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //OnGetPrinterSetting(ref curPS);
        }

        private void btnOverLapCheck_Click(object sender, EventArgs e)
        {
            //原研二代码为：   var subval = new byte[9];
            //    subval[0] = (byte)BYHXPrinterManager.Cali_Pattern_Type.OverLapCheck;
            //    subval[1] = (byte)1;//VSD
            //    subval[2] = (byte)2;//大中小点
            //    subval[3] = (byte)2;//
            //    short Start = 0;// (short)(m_allParam.EpsonAllParam.sUSB_RPT_MainUI_Param.PrintOrigin * 600.0f);//起始位置
            //    byte[] startp = BitConverter.GetBytes(Start);//
            //    Buffer.BlockCopy(startp, 0, subval, 4, 2);
            //    subval[6] = (byte)0;//媒质
            //    subval[7] = (byte)1;//DPI
            //    subval[8] = (byte)0;//额外参数

            //    uint bufsize = (uint)subval.Length;

            //    if (CoreInterface.SetEpsonEP0Cmd(0x7F, subval, ref bufsize, 3, 0) == 0)
            //    {
            //        MessageBox.Show("Unsupported command,cmd=3 Cali_Pattern_Type=20");
            //    }

        }

        private void comboBox_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_model.SelectedIndex >= 0 )
            {
                CurrentModel = (CloseColorNozzleModelEnum)comboBox_model.SelectedIndex;
                flowLayoutPanel1.Controls.Clear();
                BuildLayoutPanel();
                if (loadDataDone)
                {
                    SNozzleOverlap sNO = GetDefaultNozzleOverlap(CurrentModel);
                    LoadData(sNO);
                }
            }
        }
        //构造不同模式下的默认数据
        private SNozzleOverlap GetDefaultNozzleOverlap(CloseColorNozzleModelEnum CurrentModel)
        {
            SNozzleOverlap result = curNO;
            int maxColorNum = 32;
            int m_ColorNum = CoreInterface.GetLayoutColorNum();
            int m_ColorNumAndColumnNum = m_ColorNum * m_HorColumnNum;
            for (int i = 0; i < RowNum; i++)
            {
                for (int j = 0; j < m_ColorNumAndColumnNum; j++)
                {
                    switch (CurrentModel)
                    {
                        case CloseColorNozzleModelEnum.Easy://简单模式(默认切点不羽化)
                            result.OverLapUpWasteNozzle[i * maxColorNum + j] = result.OverLapTotalNozzle[i * maxColorNum + j];
                            result.OverLapDownWasteNozzle[i * maxColorNum + j] = 0;
                            result.OverLapUpPercent[i * maxColorNum + j] = 50;
                            result.OverLapDownPercent[i * maxColorNum + j] = 50; break;
                        case CloseColorNozzleModelEnum.Feather://羽化模式（默认羽化）
                            result.OverLapUpWasteNozzle[i * maxColorNum + j] = 0;
                            result.OverLapDownWasteNozzle[i * maxColorNum + j] = 0;
                            result.OverLapUpPercent[i * maxColorNum + j] = 0;
                            result.OverLapDownPercent[i * maxColorNum + j] = 0;
                            break;
                        case CloseColorNozzleModelEnum.Advanced://高级模式（返回原值）
                            break;
                        default: break;
                    }
                }
            }

            return result;
        }
    }
}
