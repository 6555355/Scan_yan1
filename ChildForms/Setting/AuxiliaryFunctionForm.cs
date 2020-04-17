using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    /*FW接口定义
     * 通过ep0下发的数据:
        req:0x92
        index:0x00
        typedef struct AllwinRRParam_tag
        {
	        INT32U Flag;
	        INT8U inkTemp[8];
	        INT16U cycleParam[4];
	
	        INT8U rev[44];
        } AllwinRRParam;
        上面结构体，总共64个字节

     */
  

    public partial class AuxiliaryFunctionForm : Form
    {
        private List<Label> colorLabels;
        private List<NumericUpDown> tempInputs;
        private List<Button> colorFlags;
        public AuxiliaryFunctionForm()
        {
            InitializeComponent();
            #region 界面约定从右往左解析数据
            colorLabels = new List<Label>() { label12, label11, label10, label9, label8, label7, label6, label5 };
            tempInputs = new List<NumericUpDown>() 
            {
                numericUpDown12,numericUpDown11,numericUpDown10,numericUpDown9,
                numericUpDown8,numericUpDown7,numericUpDown6,numericUpDown5
            };
            colorFlags = new List<Button>() { button8, button7, button6, button5, button4, button3, button2, button1 }; 
            #endregion
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(2, 2, 16, 16);

            for (int i = 0; i < colorFlags.Count; i++)
            {
                colorFlags[i].BackColor = Color.Red;
                colorFlags[i].Region = new Region(myPath);
            }
        }

        private SPrinterProperty _property;

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;

            for (int i = 0; i < this.colorLabels.Count; i++)
            {
                if (i < property.nColorNum)
                {
                    
                    colorLabels[i].Text = string.Format("{0}({1})",
                          property.Get_ColorIndex(i), property.nColorNum-i);
                    colorFlags[i].BackColor = GetBackColorByIndex(i);
                }
                else
                {
                    colorFlags[i].Visible = tempInputs[i].Visible = colorLabels[i].Visible = false;
                }

#if false
                if (i < 2)
                {
                    if (property.nWhiteInkNum > 0 && i < property.nWhiteInkNum)
                    {
                        colorLabels[i].Text = string.Format("{0}({1})",
                            property.Get_ColorIndex(i + property.nColorNum - property.nWhiteInkNum), i + 1);
                        colorFlags[i].BackColor = GetBackColorByIndex(i + property.nColorNum - property.nWhiteInkNum);
                    }
                    else
                    {
                        colorFlags[i].Visible = tempInputs[i].Visible = colorLabels[i].Visible = false;
                    }
                }
                else
                {
                    if (i - 2 < property.nColorNum - property.nWhiteInkNum)
                    {
                        colorLabels[i].Text = string.Format("{0}({1})", property.Get_ColorIndex(i - 2), i + 1);
                        colorFlags[i].BackColor = GetBackColorByIndex(i - 2);
                    }
                    else
                    {
                        colorFlags[i].Visible = tempInputs[i].Visible = colorLabels[i].Visible = false;

                    }
                } 
#endif
            }
            LoadData();
        }

       

        private Color GetBackColorByIndex(int index)
        {
            ColorEnum colorEnum = _property.Get_ColorEnum(index);
            switch (colorEnum)
            {
                case ColorEnum.Cyan:
                    return Color.Cyan;
                case ColorEnum.Magenta:
                    return Color.Magenta;
                case ColorEnum.Yellow:
                    return Color.Yellow;
                case ColorEnum.Black:
                    return Color.Black;
                case ColorEnum.LightCyan:
                    return Color.LightCyan;
                case ColorEnum.LightMagenta:
                    return Color.LightPink;//TODO
                case ColorEnum.LightYellow:
                    return Color.LightYellow;
                case ColorEnum.LightBlack:
                    return Color.LightGray;//TODO
                case ColorEnum.Red:
                    return Color.Red;
                case ColorEnum.Blue:
                    return Color.Blue;
                case ColorEnum.Green:
                    return Color.Green;
                case ColorEnum.Orange:
                    return Color.Orange;
                case ColorEnum.White:
                    return Color.White;
                case ColorEnum.Vanish:
                    return Color.Violet;//TODO
                case ColorEnum.SkyBlue:
                    return Color.SkyBlue;
                case ColorEnum.Gray:
                    return Color.Gray;
                case ColorEnum.Pink:
                    return Color.Pink;
                case ColorEnum.NULL:
                    return Color.Transparent;
                default:
                    break;
            }
            return Color.Transparent;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            List<byte> allData = new List<byte>();
            int charA = (int)'A';
            int charW = (int)'W';
            int charR = (int)'R';
            UInt32 flag = (UInt32)(charA | (charW << 8) | (charR << 16) | (charR << 24));
            byte[] flagData = BitConverter.GetBytes(flag);
            allData.AddRange(flagData);

            #region 墨水加热温度设置

            byte[] tempBuf = new byte[8];
            for (int i = 0; i < tempInputs.Count; i++)
            {
                tempBuf[i] = (byte)tempInputs[i].Value;
            }
            allData.AddRange(tempBuf);

            #endregion

            #region 墨水循环搅设置

            short cycleAction = (short)numericUpDowncCycleAction.Value;
            short cyclePause = (short)numericUpDownCyclePause.Value;
            short stirredAction = (short)numericUpDownStirredAction.Value;
            short stirredPause = (short)numericUpDownStirredPause.Value;
            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes(cycleAction));
            data.AddRange(BitConverter.GetBytes(cyclePause));
            data.AddRange(BitConverter.GetBytes(stirredAction));
            data.AddRange(BitConverter.GetBytes(stirredPause));

            allData.AddRange(data);
            #endregion

            byte[] rev=new byte[44];
            allData.AddRange(rev);

            byte[] buf =allData.ToArray();
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x00);
            if (ret == 0)
            {
                MessageBox.Show("Send Auxiliary control parameters error!");
            }
            else
            {
                MessageBox.Show("应用成功！");
            }


        }


        private void LoadData()
        {
            try
            {
                byte[] buf = new byte[64];
                uint bufsize = (uint)buf.Length;
                int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x00);
                if (ret == 0)
                {
                    MessageBox.Show("Get Auxiliary control parameters error!");
                }
                else
                {
                    if (buf.Length >= 12)
                    {
                        byte[] tempBuf = buf.Skip(6).Take(8).ToArray();
                        for (int i = 0; i < tempBuf.Length; i++)
                        {
                            if (tempBuf[i] >= tempInputs[i].Minimum && tempBuf[i] <= tempInputs[i].Maximum)
                            {
                                tempInputs[i].Value = tempBuf[i];
                            }
                        }
                    }

                    if (buf.Length >= 20)
                    {
                        byte[] inkBuf = buf.Skip(14).Take(8).ToArray();
                        short inkValue = BitConverter.ToInt16(inkBuf, 0);
                        if (inkValue >= numericUpDowncCycleAction.Minimum && inkValue <= numericUpDowncCycleAction.Maximum)
                        {
                            numericUpDowncCycleAction.Value = inkValue;
                        }
                        inkValue = BitConverter.ToInt16(inkBuf, 2);
                        if (inkValue >= numericUpDownCyclePause.Minimum && inkValue <= numericUpDownCyclePause.Maximum)
                        {
                            numericUpDownCyclePause.Value = inkValue;
                        }
                        inkValue = BitConverter.ToInt16(inkBuf, 4);
                        if (inkValue >= numericUpDownStirredAction.Minimum && inkValue <= numericUpDownStirredAction.Maximum)
                        {
                            numericUpDownStirredAction.Value = inkValue;
                        }
                        inkValue = BitConverter.ToInt16(inkBuf, 6);
                        if (inkValue >= numericUpDownStirredPause.Minimum && inkValue <= numericUpDownStirredPause.Maximum)
                        {
                            numericUpDownStirredPause.Value = inkValue;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
