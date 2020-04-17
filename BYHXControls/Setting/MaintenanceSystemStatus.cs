using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class MaintenanceSystemStatus : BYHXUserControl
    {
        private List<Button> reservoirsButtons;
        private List<Button> mainButtons;
        private List<Button> pumpButtons;
        private List<Button> purgeButtons;
        private List<Button> pumtTimeoutButtons;
        private List<Label> clolorsLabels;
        private List<TextBox> tempTextBoxs;
        private SPrinterProperty _property;

        public MaintenanceSystemStatus()
        {
            InitializeComponent();

            reservoirsButtons = new List<Button>()
            {
                button1,
                button2,
                button3,
                button4,
                button5,
                button6,
                button7,
                button8
            };
            mainButtons = new List<Button>()
            {
                button9,
                button10,
                button11,
                button12,
                button13,
                button14,
                button15,
                button16
            };
            pumpButtons = new List<Button>()
            {
                button17,
                button18,
                button19,
                button20,
                button21,
                button22,
                button23,
                button24
            };
            purgeButtons = new List<Button>()
            {
                button25,
                button26,
                button27,
                button28,
                button29,
                button30,
                button31,
                button32
            };
            pumtTimeoutButtons = new List<Button>()
            {
                button33,
                button34,
                button35,
                button36,
                button37,
                button38,
                button39,
                button40
            };
            clolorsLabels = new List<Label>() { label3, label4, label5, label6, label7, label8, label9, label10 };
            tempTextBoxs = new List<TextBox>() { textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8};
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(2, 2, 16, 16);

            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                reservoirsButtons[i].BackColor = Color.Red;
                //reservoirsButtons[i].Size = new Size(20, 20);
                reservoirsButtons[i].Region = new Region(myPath);
                reservoirsButtons[i].Visible = false;

                //mainButtons[i].Size = new Size(20, 20);
                mainButtons[i].Region = new Region(myPath);
                mainButtons[i].BackColor = Color.Red;

                //mainButtons[i].Size = new Size(20, 20);
                pumpButtons[i].Region = new Region(myPath);
                pumpButtons[i].BackColor = Color.Red;
                pumpButtons[i].Visible = false;

                purgeButtons[i].Region = new Region(myPath);
                purgeButtons[i].BackColor = Color.Red;
                purgeButtons[i].Visible = false;

                pumtTimeoutButtons[i].Region = new Region(myPath);
                pumtTimeoutButtons[i].BackColor = Color.Red;

                tempTextBoxs[i].Visible = false;

            }
            label19.Visible=label1.Visible = label11.Visible = label16.Visible = false;

#if NEW_MAINTENANCE
            for (int i = 0; i < 8; i++)
            {
                reservoirsButtons[i].Visible = true;
                tempTextBoxs[i].Visible = true;
                pumtTimeoutButtons[i].Visible = false;
            }
            label19.Visible=label1.Visible = true;
            label17.Visible = false;
            if (grouperUV.Visible)
                grouperUV.Visible = false;
#endif
        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;
            for (int i = 0; i < mainButtons.Count; i++)
            {
                if (i < property.nColorNum)
                {
                    clolorsLabels[i].Text = string.Format("{0}({1})",
                          property.Get_ColorIndex(property.nColorNum - (1 + i)), i + 1);//约定从右往左显示，与校准向导相反
                }
                else
                {
                    clolorsLabels[i].Text = string.Empty;
                    pumtTimeoutButtons[i].Visible = purgeButtons[i].Visible = tempTextBoxs[i].Visible =
                    pumpButtons[i].Visible = clolorsLabels[i].Visible = mainButtons[i].Visible = reservoirsButtons[i].Visible = false;
                }
#if false
                if (i < 2)
                {
                    if (property.nWhiteInkNum > 0 && i < property.nWhiteInkNum)
                    {
                        clolorsLabels[i].Text = property.Get_ColorIndex(i + property.nColorNum - property.nWhiteInkNum);
                    }
                    else
                    {
                        clolorsLabels[i].Text = string.Empty;
                        pumtTimeoutButtons[i].Visible = purgeButtons[i].Visible = tempTextBoxs[i].Visible =
                        pumpButtons[i].Visible = clolorsLabels[i].Visible = mainButtons[i].Visible = reservoirsButtons[i].Visible = false;
                    }
                }
                else
                {
                    if (i - 2 < property.nColorNum - property.nWhiteInkNum)
                    {
                        clolorsLabels[i].Text = property.Get_ColorIndex(i - 2);
                    }
                    else
                    {
                        clolorsLabels[i].Text = string.Empty;
                        pumtTimeoutButtons[i].Visible = purgeButtons[i].Visible = tempTextBoxs[i].Visible =
                        pumpButtons[i].Visible = clolorsLabels[i].Visible = mainButtons[i].Visible = reservoirsButtons[i].Visible = false;
                    }
                } 
#endif
            }
        }

        /// <summary>
        /// 从右往左解析数据
        /// </summary>
        /// <param name="buf"></param>
        public void OnStatusDataChanged(byte[] buf)
        {
#if !NEW_MAINTENANCE
            #region old
            if (buf.Length < 10) return;
            string uvLeftStatus = string.Format("[{0}]{1}℃", (UvStatus)buf[5], buf[3]);
            string uvRightStatus = string.Format("[{0}]{1}℃", (UvStatus)buf[6], buf[4]);
            lblTempInkTank.Text = buf[0].ToString();
            labelColor.Text = buf[1].ToString();
            labelWhite.Text = buf[2].ToString();
            lblTempLeft.Text = uvLeftStatus;
            lblTempRight.Text = uvRightStatus;
            //if (buf.Length >= 3)
            //{
            //    byte inkstatus = buf[8];
            //    for (int i = 0; i < 8; i++)
            //    {
            //        reservoirsButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
            //    }
            //}

            //if (buf.Length >= 4)
            {
                byte inkstatus = buf[9];
                for (int i = 0; i < 8; i++)
                {
                    mainButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
                }
            }

            //if (buf.Length >= 5)
            //{
            //    byte inkstatus = buf[10];
            //    for (int i = 0; i < 8; i++)
            //    {
            //        pumpButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
            //    }
            //}

            //{
            //    byte inkstatus = buf[11];
            //    for (int i = 0; i < 8; i++)
            //    {
            //        purgeButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
            //    }
            //}

            {
                byte inkstatus = buf[8];
                for (int i = 0; i < 8; i++)
                {
                    pumtTimeoutButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
                }
            } 
            #endregion
#else
            int colorNum = _property.nColorNum;//如colorNum=8,目前最多支持8色
            if (buf.Length < 9) return;
            //温度 第2-9字节 （共8字节）  第1字节保留，用作以后子命令扩充
            for (int i = colorNum; i >= 1; i--)
            {
                tempTextBoxs[colorNum - i].Text = buf[i].ToString();
            }

            //副墨盒 第10字节（index start 0）
            byte inkstatus = buf[9];
            for (int i = colorNum - 1; i >= 0; i--)
            {
                reservoirsButtons[colorNum-1-i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
            }

            //主墨盒 第11字节（index start 0）
            inkstatus = buf[10];
            for (int i = colorNum - 1; i >= 0; i--)
            {
                mainButtons[colorNum - 1 - i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
            }
#endif
        }
            
    }

    enum UvStatus
    {
        Close =0,
        Preheat,
        LowPower,
        MidPower,
        HighPower,
        TempTooHigh
    }

    public enum MaintenanceSystemError
    {
        OK = 0, //D7 = 0正常
        Error1 = 1, //D7=1气路主板FPGA出错
        Error2 = 2, //D7=2气路小车板FPGA出错
        Error3 = 3, //D7=3气路主板和气路小车板通讯出错 
        Error4 = 4, //D7=4气路主板和气路供墨板通讯出错
        Error5 = 5, //D7=5外部气压过高
        Error6 = 6, //D7=6外部气压过低
        Error7 = 7, //D7=7安全气瓶1出错
        Error8 = 8, //D7=8安全气瓶2出错
        Error9 = 9, //D7=9彩色负压出错
        Error10 = 10, //D7=10白色负压出错
    }

    public enum MaintenanceSystemErrorEx
    {
        Error_MainFpga,    //ALLWIN主板FPGA错误
        Error_HeadFpga,   //ALLWIN头板FPGA错误
        Error_Head485,	//ALLWIN头板485通讯出错
        Error_Bink485,	//ALLWIN供墨板485通讯出错
        Error_Lamp485,	//ALLWIN UV灯板485通讯出错
        Error_NegColor1_L,	//彩墨负压过低异常
        Error_NegColor1_H,	//彩墨负压过高异常
        Error_NegColor2_L,	//白墨负压过低异常
        Error_NegColor2_H,	//白墨负压过高异常
        Error_SafetyBottle_Color1,	//彩墨安全气瓶出错
        Error_SafetyBottle_Color2,	//白墨安全气瓶出错
        Error_Left_Lamp,	//左UV灯出错异常
        Error_Right_Lamp,	//右UV灯出错异常
        Error_5Ltank_Empty,	//主墨瓶缺墨，墨量过低
        Error_Subtank_Empty	//副墨瓶缺墨，墨量过低
    }
}
