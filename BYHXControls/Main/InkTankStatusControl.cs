using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.Setting;

namespace BYHXPrinterManager.Main
{
    public partial class InkTankStatusControl : BYHXUserControl
    {
        private List<Button> reservoirsButtons;
        private List<Button> mainButtons;
        private List<Button> pumpButtons;
        private List<Label> clolorsLabels;
        private Timer _timer = new Timer();
        public InkTankStatusControl()
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
            clolorsLabels = new List<Label>() {label3, label4, label5, label6, label7, label8, label9, label10};

            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(2, 2, 16, 16);

            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                reservoirsButtons[i].BackColor = Color.Red;
                //reservoirsButtons[i].Size = new Size(20, 20);
                reservoirsButtons[i].Region = new Region(myPath);

                //mainButtons[i].Size = new Size(20, 20);
                mainButtons[i].Region = new Region(myPath);
                mainButtons[i].BackColor = Color.Red;

                //mainButtons[i].Size = new Size(20, 20);
                pumpButtons[i].Region = new Region(myPath);
                pumpButtons[i].BackColor = Color.Red;

            }

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
        }
        private const int MaxRetryTimes = 10;
        private  int retryTimes = 0;
        void _timer_Tick(object sender, EventArgs e)
        {
            if (retryTimes > MaxRetryTimes)
                return;
            uint bufsize = 40;
            byte[] buf = new byte[bufsize];
            int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                retryTimes++;
                return;
            }
            if (buf.Length >= 3)
            {
                byte inkstatus = buf[2];
                for (int i = 0; i < 8; i++)
                {
                    reservoirsButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
                }
            }

            if (buf.Length >= 4)
            {
                byte inkstatus = buf[3];
                for (int i = 0; i < 8; i++)
                {
                    mainButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
                }
            }

            if (buf.Length >= 5)
            {
                byte inkstatus = buf[4];
                for (int i = 0; i < 8; i++)
                {
                    pumpButtons[i].BackColor = (inkstatus & (1 << i)) == 0 ? Color.Green : Color.Red;
                }
            }
        }

        private SPrinterProperty _property;
        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            if (status == JetStatusEnum.PowerOff || status == JetStatusEnum.Initializing)
            {
                if (_timer.Enabled)
                {
                    _timer.Stop();
                }
            }
            else
            {
                if (!_timer.Enabled && this.Visible)
                {
                    retryTimes = 0;
                    _timer.Start();
                }
                _property = new SPrinterProperty();
                CoreInterface.GetSPrinterProperty(ref _property);
            }
        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;

            for (int i = 0; i < mainButtons.Count; i++)
            {
                if (i < 2)
                {
                    if (property.nWhiteInkNum > 0 && i < property.nWhiteInkNum)
                    {
                        clolorsLabels[i].Text =string.Format("{0}({1})", property.Get_ColorIndex(i + property.nColorNum - property.nWhiteInkNum),i+1);
                    }
                    else
                    {
                        clolorsLabels[i].Text = string.Empty;
                        pumpButtons[i].Visible = clolorsLabels[i].Visible = mainButtons[i].Visible = reservoirsButtons[i].Visible = false;
                    }
                }
                else
                {
                    if (i - 2 < property.nColorNum - property.nWhiteInkNum)
                    {
                        clolorsLabels[i].Text = string.Format("{0}({1})",property.Get_ColorIndex(i - 2),i+1);
                    }
                    else
                    {
                        clolorsLabels[i].Text = string.Empty;
                        pumpButtons[i].Visible = clolorsLabels[i].Visible = mainButtons[i].Visible = reservoirsButtons[i].Visible = false;
                    }
                }
            }
        }
        
    }
}
