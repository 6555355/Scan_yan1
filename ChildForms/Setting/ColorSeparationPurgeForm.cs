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
    /// <summary>
    /// 注：界面顏色显示順序，从右到左与ColorOrder一致。
    /// 所以要求从右到左访问三排界面控件。
    /// </summary>
    public partial class ColorSeparationPurgeForm : Form
    {
        private List<Label> colorLabels;
        private List<CheckBox> colorCheckBoxes;
        private List<Button> colorFlags;
        public ColorSeparationPurgeForm()
        {
            InitializeComponent();

            #region 按照从右到左的顺序保存控件数组
            colorLabels = new List<Label>()
            {
                label8,
                label7,
                label6,
                label5,
                label4,
                label3,
                label2,
                label1
            };
            colorCheckBoxes = new List<CheckBox>()
            {
                checkBox8,
                checkBox7,
                checkBox6,
                checkBox5,
                checkBox4,
                checkBox3,
                checkBox2,
                checkBox1
            };
            colorFlags = new List<Button>()
            {
                button8,
                button7,
                button6,
                button5,
                button4,
                button3,
                button2,
                button1
            };  
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
        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            btnPurge.Enabled = status == JetStatusEnum.Ready || status == JetStatusEnum.Pause;
        }

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
                    colorFlags[i].Visible = colorCheckBoxes[i].Visible = colorLabels[i].Visible = false;
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
                        colorFlags[i].Visible = colorCheckBoxes[i].Visible = colorLabels[i].Visible = false;
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
                        colorFlags[i].Visible = colorCheckBoxes[i].Visible = colorLabels[i].Visible = false;

                    }
                } 
#endif
            }
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

        /// <summary>
        /// 注：界面显示从右到左和ColorOrder一致。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPurge_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[1];
            for (int i = 0; i < colorCheckBoxes.Count; i++)
            {
                if (colorCheckBoxes[i].Checked)
                {
                    //buf[0] |= (byte)(1 << (7 - i));
                    buf[0] |= (byte)(1 << i);
                }
            }
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x52);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge commond error!");
            }
            btnPurge.Enabled = false;
        }

        private void btnPurgeOff_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[1];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x52);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge off commond error!");
            }
            btnPurge.Enabled = true;
        }

    }
}
