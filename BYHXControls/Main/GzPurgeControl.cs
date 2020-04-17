using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BYHXPrinterManager.Setting;
using Timer = System.Windows.Forms.Timer;

namespace BYHXPrinterManager.Main
{
    public partial class GzPurgeControl : BYHXUserControl
    {
        private List<CheckBox> reservoirsButtons;
        private List<TextBox> _setTemps;
        private List<TextBox> _curTemps;
        private Timer _refreshTimer;
        private Timer _pressureTimer;
        private bool _hasReaded = false;
        public GzPurgeControl()
        {
            InitializeComponent();


            _setTemps = new List<TextBox>()
            {
                checkBox1,
                checkBox2,
                checkBox3,
                checkBox4,
                checkBox5,
                checkBox6,
                checkBox7,
                checkBox8
            };
            _curTemps = new List<TextBox>()
            {
                textBox1,
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8
            };

            reservoirsButtons = new List<CheckBox>()
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

            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse(5, 5, 16, 16);

            _refreshTimer = new Timer();
            _refreshTimer.Interval = 1000;
            _refreshTimer.Tick += new EventHandler(_refreshTimer_Tick);
            _pressureTimer = new Timer();
            _pressureTimer.Interval = 1000;
            _pressureTimer.Tick += new EventHandler(_pressureTimer_Tick);
        }

        private int bGetCurTempFailTimes = 0;
        void _refreshTimer_Tick(object sender, EventArgs e)
        {
            if (bGzDoubleSide||_curStatus == JetStatusEnum.PowerOff || _curStatus == JetStatusEnum.Initializing || bGetCurTempFailTimes > 5)
                return;
            float[] curtemps = new float[8];
            byte[] buf = new byte[64];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE2);
            if (ret != 0)
            {
                for (int i = 0; i < curtemps.Length; i++)
                {
                    if (buf.Length >=(2+ i * 2 + 2))
                        curtemps[i] = BitConverter.ToInt16(buf,2+ i * 2)/100f;
                }

                for (int i = 0; i < _curTemps.Count; i++)
                {
                    _curTemps[i].Text = curtemps[i].ToString("F1");
                }
                bGetCurTempFailTimes = 0;
            }
            else
            {
                bGetCurTempFailTimes++;
            }
        }

        private void _pressureTimer_Tick(object sender, EventArgs e)
        {
            GetCurrentP();
        }
        private SPrinterProperty _property;
        private JetStatusEnum _curStatus;
        private bool bGzDoubleSide = false;
        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            _curStatus = status;
            buttonApply.Enabled =
            buttonAllPurge.Enabled =
            btnAutoPurge.Enabled = status == JetStatusEnum.Ready || status == JetStatusEnum.Pause;
            if (_curStatus == JetStatusEnum.Ready && _property.ShowGzPurgeControl() && !_pressureTimer.Enabled)
            {
                _pressureTimer.Enabled = true;
            }
            if (_curStatus == JetStatusEnum.PowerOff && _property.ShowGzPurgeControl() && _pressureTimer.Enabled)
            {
                _pressureTimer.Enabled = false;
            }
            if (_curStatus != JetStatusEnum.PowerOff && _curStatus != JetStatusEnum.Initializing && _property.ShowGzPurgeControl())
            {
                _refreshTimer.Start();
                if (!_hasReaded)
                {
                    _hasReaded = true;
                    ushort pid, vid;
                    pid = vid = 0;
                    int ret = CoreInterface.GetProductID(ref vid, ref pid);
                    if (ret != 0)
                    {
                        if (vid == ((ushort) VenderID.GONGZENG_DOUBLE_SIDE))
                        {
                            bGzDoubleSide = true;
                        }
                        grouperHeater.Visible = !bGzDoubleSide;
                        if(!bGzDoubleSide)
                        {
                            grouperHeater.Visible = true;
                            float[] settemps = new float[8];
                            byte[] buf = new byte[64];
                            uint bufsize = (uint)buf.Length;
                            ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE0);
                            if (ret != 0)
                            {
                                for (int i = 0; i < settemps.Length; i++)
                                {
                                    if (buf.Length >= 2 + i * 2 + 2)
                                        settemps[i] = BitConverter.ToInt16(buf, 2 + i * 2) / 100;
                                }

                                for (int i = 0; i < _setTemps.Count; i++)
                                {
                                    _setTemps[i].Text = settemps[i].ToString("F1");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _refreshTimer.Stop();
                bGetCurTempFailTimes = 0;
            }
        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;
            int whiteNum = (_property.nWhiteInkNum & 0x0F);
            int varnishNum = (_property.nWhiteInkNum >> 4);
            int len = this.reservoirsButtons.Count;
            if (whiteNum + varnishNum > 0)
            {
                int wIndex = 0;
                int cIndex = 0;
                for (int i = 0; i < this.reservoirsButtons.Count; i++)
                {
                    ColorEnum color = property.Get_ColorEnum(i);
                    bool isWorV = color == ColorEnum.White || color == ColorEnum.Vanish;
                    if (isWorV)
                    {
                        if (wIndex <= whiteNum + varnishNum)
                        {
                            reservoirsButtons[len - whiteNum - varnishNum + wIndex].Text = string.Format("{0}({1})", property.Get_ColorIndex(i), i + 1);
                        }
                        else
                        {
                            reservoirsButtons[len - whiteNum - varnishNum + wIndex].Text = "";
                        }
                        wIndex++;
                    }
                    else
                    {
                        if (i < property.nColorNum)
                        {
                            reservoirsButtons[cIndex].Text = string.Format("{0}({1})", property.Get_ColorIndex(i), i + 1);
                        }
                        else
                        {
                            reservoirsButtons[cIndex].Text = "";
                        }
                        cIndex++;
                    }
                }
                for (int i = 0; i < this.reservoirsButtons.Count; i++)
                {
                    reservoirsButtons[i].Visible = !string.IsNullOrEmpty(reservoirsButtons[i].Text.Trim());
                }
            }
            else
            {
                for (int i = 0; i < this.reservoirsButtons.Count; i++)
                {
                    if (i < property.nColorNum)
                    {
                        reservoirsButtons[i].Text = string.Format("{0}({1})", property.Get_ColorIndex(i), i + 1);
                        reservoirsButtons[i].Visible = true;
                    }
                    else
                    {
                        reservoirsButtons[i].Visible = false;
                    }
                }
            }
            panelPressureW.Visible = whiteNum + varnishNum > 0;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            List<byte> bytebuf = new List<byte>();
            short[] buf = new short[8];
            for (int i = 0; i < _setTemps.Count; i++)
            {
                float temp = 0;
                if (float.TryParse(_setTemps[i].Text, out temp))
                {
                    buf[i] = (short) (temp*100);
                }
            }
            for (int i = 0; i < buf.Length; i++)
            {
                bytebuf.AddRange(BitConverter.GetBytes(buf[i]));
            }
            uint bufsize = (uint)bytebuf.Count;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, bytebuf.ToArray(), ref bufsize, 0, 0xE0);
            if (ret == 0)
            {
                MessageBox.Show("Set target temp error!");
            }
        }

        private void GzPurgeControl_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonAllPurge_MouseDown(object sender, MouseEventArgs e)
        {
            btnAutoPurge.Enabled = false;
            byte[] buf = new byte[1];
            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                if (reservoirsButtons[i].Checked)
                {
                    buf[0] |= (byte)(1 << (7 - i));
                }
            }
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE1);
            //if (ret == 0)
            //{
            //    MessageBox.Show("Send Purge commond error!");
            //}
        }

        private void buttonAllPurge_MouseUp(object sender, MouseEventArgs e)
        {
            btnAutoPurge.Enabled = true;
            byte[] buf = new byte[1];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE1);
            //if (ret == 0)
            //{
            //    MessageBox.Show("Send Purge off commond error!");
            //}
        }

        private void btnAutoPurge_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAutoPurge.Checked == false) return;

            try
            {
                if (numPurgeTime.Value <= 0) return;

                btnAutoPurge.Enabled = false;
                buttonAllPurge.Enabled = false;

                bool isPurge = false;
                byte[] buf = new byte[1];
                for (int i = 0; i < reservoirsButtons.Count; i++)
                {
                    if (reservoirsButtons[i].Checked)
                    {
                        buf[0] |= (byte)(1 << (7 - i));
                        isPurge = true;
                    }
                }

                if (isPurge)
                {
                    int purgeTime = (int)numPurgeTime.Value;
                    uint bufsize = (uint)buf.Length;
                    int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE1);

                    int i=0;
                    while (i < purgeTime)
                    {
                        i++;
                        System.Threading.Thread.Sleep(1000);
                    }

                    buf = new byte[1];
                    bufsize = (uint)buf.Length;
                    ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE1);
                }

            }
            finally
            {
                btnAutoPurge.Enabled = true;
                buttonAllPurge.Enabled = true;
                btnAutoPurge.Checked = false;
            }
        }
        private void GetCurrentP()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Thread.Sleep(1500);
                byte[] buf = new byte[10];
                uint bufsize = (uint)buf.Length;
                int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE7);
                if (ret != 0)
                {
                    //气压
                    byte[] p1 = new byte[2];
                    byte[] p2 = new byte[2];
                    byte[] p3 = new byte[2];
                    byte[] p4 = new byte[2];
                    Buffer.BlockCopy(buf, 2, p1, 0, 2);
                    Buffer.BlockCopy(buf, 4, p2, 0, 2);
                    Buffer.BlockCopy(buf, 6, p3, 0, 2);
                    Buffer.BlockCopy(buf, 8, p4, 0, 2);
                    this.BeginInvoke(new Action(() =>
                    {
                        lblP1.Text = string.Format(" {0}Kpa ", (BitConverter.ToUInt16(p3, 0) * 0.01));
                        lblp2.Text = string.Format(" {0}Kpa ", (BitConverter.ToUInt16(p4, 0) * 0.01));
                        lblp3.Text = string.Format(" {0}Kpa ", (BitConverter.ToUInt16(p1, 0) * 0.01));
                        lblp4.Text = string.Format(" {0}Kpa ", (BitConverter.ToUInt16(p2, 0) * 0.01));
                    }));
                }
            }));
        }
    }
}
