using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class HeadParameterForm : Form
    {
        NumericUpDown[] numericUpDown_Ratios = null;
        List<string> NameList = new List<string>();
        int channelNum = 0;

        public HeadParameterForm()
        {
            InitializeComponent();
        }

        private void HeadParameterForm_Load(object sender, EventArgs e)
        {
            GetNameList();

            this.panel1.Controls.Add(GetRatioBoxGroup());
        }

        private void GetNameList()
        {
            int rowNum = CoreInterface.GetRowNum();
            int lineNum = 0;
            for (int i = 0; i < rowNum; i++)
            {
                lineNum += CoreInterface.GetLineNumPerRow(i);
            }

            int firstHeadID = -1;
            int firstVID = -1;
            int VNumPerHead = 0;
            List<NozzleLineData> NozzleList = new List<NozzleLineData>();
            for (int i = 1; i <= lineNum; i++)
            {
                NozzleLineData lineData = new NozzleLineData();
                CoreInterface.GetlineIDtoNozzleline(i, ref lineData);
                NozzleList.Add(lineData);

                if (firstHeadID == -1)
                {
                    firstHeadID = lineData.HeadID;
                    firstVID = lineData.VoltageChannel;
                    VNumPerHead = 1;
                }
                else if (firstHeadID == lineData.HeadID)
                {
                    if (firstVID != lineData.VoltageChannel)
                    {
                        firstVID = lineData.VoltageChannel;
                        VNumPerHead++;
                    }
                }

            }

            NozzleList.Sort((NozzleLineData l1, NozzleLineData l2) => l1.VoltageChannel.CompareTo(l2.VoltageChannel));

            int oldVoltage = -1;
            string strHead = "";
            string strColor = "";
            for (int i = 0; i < NozzleList.Count; i++)
            {
                NozzleLineData lineData = NozzleList[i];

                if (oldVoltage == -1)
                {
                    oldVoltage = lineData.VoltageChannel;
                    strHead = "H" + (lineData.HeadID).ToString();
                    strColor = GetColorName((int)lineData.ColorID) + (CoreInterface.GetLineIndexInHead(lineData.ID) +1).ToString();
                }
                else
                {
                    if (oldVoltage == lineData.VoltageChannel)
                    {
                        strColor += "," + GetColorName((int)lineData.ColorID) + (CoreInterface.GetLineIndexInHead(lineData.ID) + 1).ToString();
                    }
                    else
                    {

                        if (VNumPerHead <= 1)
                        {
                            NameList.Add(strHead);
                        }
                        else
                        {
                            NameList.Add(strHead + "(" + strColor + ")");
                        }

                        oldVoltage = lineData.VoltageChannel;
                        strHead = "H" + (lineData.HeadID).ToString();
                        strColor = GetColorName((int)lineData.ColorID) + (CoreInterface.GetLineIndexInHead(lineData.ID) + 1).ToString();
                    }  
                }

                if (i == NozzleList.Count - 1)
                {
                    if (VNumPerHead <= 1)
                    {
                        NameList.Add(strHead);
                    }
                    else
                    {
                        NameList.Add(strHead + "(" + strColor + ")");
                    }
                }
            }

            channelNum = NameList.Count;

        }

        private GroupBox GetRatioBoxGroup()
        {
            int width = 0;
            int height = 0;

            GroupBox Group = new GroupBox();
            List<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
                EpsonLCD.GetAllChannelsFromBoard_Y2(channelNum, NameList, ref channels);
                //int groupNum = channels.Count > 16 ? m_GroupNum : 1;//分组个数
                int groupNum = GetHeadParameterGroupNum(channels.Count);

                int group = 0;
                int colPerGrp = channels.Count / groupNum;
                int rowPerGrp = 2;
                int cidx = 0;
                int colidx = 0;//当前列
                int rowidx = 0;//当前行

                TableLayoutPanel Table = new TableLayoutPanel();
                Table.RowCount = 2 * groupNum;
                Table.ColumnCount = colPerGrp + 1;
                numericUpDown_Ratios = new NumericUpDown[channels.Count];

                Label label = new Label();
                Padding p = new System.Windows.Forms.Padding(0, 7, 0, 0);
                Padding p2 = new System.Windows.Forms.Padding(0, 0, 0, 0);

                for (int i = 0; i < Table.ColumnCount; i++)
                {
                    if (0 == i)
                    {
                        //第一列放文字，宽一些
                        Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
                    }
                    else
                    {
                        Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
                    }
                    Table.Dock = DockStyle.Fill;

                    width += 80;
                }

                do
                {
                    for (int i = 0; i < colPerGrp; i++)
                    {
                        colidx = i + 1;
                        cidx = i + group * colPerGrp;
                        ChannelInfo cinfo = null;
                        if (cidx >= 0 && cidx <= channels.Count)
                        {
                            cinfo = channels[cidx];
                        }
                        else
                        {
                            continue;
                        }

                        //行头
                        if (i == 0)
                        {
                            //rowidx = 0 + group * rowPerGrp;
                            //label = new Label();
                            //label.Text = ResString.GetResString("TemperatureChannel");
                            //Table.Controls.Add(label, 0, rowidx);


                            rowidx = 1 + group * rowPerGrp;
                            label = new Label();
                            label.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
                            label.Text = ResString.GetResString("ParamterRatio");
                            Table.Controls.Add(label, 0, rowidx);
                        }

                        //TemperatureChannel
                        rowidx = 0 + group * rowPerGrp;
                        label = new Label();
                        label.AutoSize = true;
                        label.Text = cinfo.Name;
                        label.Margin = p;
                        Table.Controls.Add(label, colidx, rowidx);
                        //ParamterRatio
                        rowidx = 1 + group * rowPerGrp;
                        NumericUpDown Number = new NumericUpDown();
                        Number.Width = 58;
                        Number.Margin = p2;
                        Number.DecimalPlaces = 1;
                        Number.Minimum = new System.Decimal(-50.0f);
                        Number.Maximum = new System.Decimal(50.0f);
                        int value = (int)((sbyte)cinfo.Value);
                        UIPreference.SetValueAndClampWithMinMax(Number, value);
                        Number.Tag = cidx;
                        this.numericUpDown_Ratios[cidx] = Number;
                        Table.Controls.Add(Number, colidx, rowidx);

                    }

                    group++;
                }
                while (group < groupNum);

                height = groupNum * 70;
                Group.Text = ResString.GetResString("HeadParamter");
                Group.Controls.Add(Table);
                Group.Dock = DockStyle.Fill;
                Group.Height = height;
                Group.Width  = width;
            }
            catch (Exception ex)
            {

            }

            return Group;
        }

        //根据获得的喷头通道数，计算折行行数
        private int GetHeadParameterGroupNum(int channelCount)
        {
            int groupnum = 1;

            if (channelCount > 16)
            {
                groupnum = channelCount % 16 > 0 ? channelCount / 16 + 1 : channelCount / 16;
            }

            return groupnum;
        }

        //刷新喷头参数列表
        private void UpdateHeadParameterPanel(TableLayoutPanel headParameterPanel)
        {
            List<ChannelInfo> channels = new List<ChannelInfo>();
            EpsonLCD.GetAllChannelsFromBoard_Y2(channelNum, NameList, ref channels);

            //int groupNum = channels.Count > 16 ? m_GroupNum : 1;
            int groupNum = GetHeadParameterGroupNum(channels.Count);
            int row = 0;
            int doNum = 0;
            int cidx = 0;
            int colNum = channels.Count / groupNum;

            do
            {
                row = doNum * 2 + 1;
                for (int i = 0; i < colNum; i++)
                {
                    cidx = i + doNum * colNum;
                    NumericUpDown Number = (NumericUpDown)headParameterPanel.GetControlFromPosition(i + 1, row);
                    int value = (int)((sbyte)channels[cidx].Value);
                    UIPreference.SetValueAndClampWithMinMax(Number, value);
                }
                doNum++;
            }
            while (doNum < groupNum);
        }

        private int SetHeadParameter()
        {
            if (numericUpDown_Ratios != null)
            {
                List<byte> list = new List<byte>(this.numericUpDown_Ratios.Length);
                for (int i = 0; i < numericUpDown_Ratios.Length; i++)
                {
                    int n = (int)numericUpDown_Ratios[i].Value;
                    if (n < 0)
                    {
                        byte b = BitConverter.GetBytes(n)[0];
                        list.Add((byte)(b));
                    }
                    else
                    {
                        list.Add((byte)numericUpDown_Ratios[i].Value);
                    }
                }
                return EpsonLCD.SetChannelParamValue_Y2(list.ToArray());
            }

            return 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0 && panel1.Controls[0].Controls.Count > 0)
            {
                TableLayoutPanel HeadParameterPanel = (TableLayoutPanel)panel1.Controls[0].Controls[0];
                UpdateHeadParameterPanel(HeadParameterPanel);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetHeadParameter();
        }

        string GetColorName(int colorIdx)
        {
            string name = "";

            name = Enum.GetName(typeof(LayoutColorNameEnum), colorIdx);

            return name;
        }
    }

}
