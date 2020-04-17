using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class PressureSetting : UserControl
    {
        public PressureSetting()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            //int whiteNum = (sp.nWhiteInkNum & 0x0F);
            //int varnishNum = (sp.nWhiteInkNum >> 4);
            panelWhitePressure.Visible = sp.bSupportWhiteInk;
        }
        
        private void PressureSetting_Load(object sender, EventArgs e)
        {
            GetTragetP();
        }
        private void GetTragetP()
        {
            Task.Factory.StartNew(new Action(() =>
            {
                Thread.Sleep(1500);
                byte[] buf = new byte[10];
                uint bufsize = (uint)buf.Length;
                int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE6);
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
                        this.tbTargetP1.Text = (BitConverter.ToUInt16(p3, 0) * 0.01).ToString();
                        this.tbTargetP2.Text = (BitConverter.ToUInt16(p4, 0) * 0.01).ToString();
                        this.tbTargetP3.Text = (BitConverter.ToUInt16(p1, 0) * 0.01).ToString();
                        this.tbTargetP4.Text = (BitConverter.ToUInt16(p2, 0) * 0.01).ToString();
                    }));
                }
            }));
        }
        private void SetTargetP()
        {
            if (tbTargetP1.Text.Trim() == ""
                || tbTargetP2.Text.Trim() == ""
                || tbTargetP3.Text.Trim() == ""
                || tbTargetP4.Text.Trim() == "")
            {
                MessageBox.Show("请输入目标气压值");
                return;
            }
            byte[] buf = new byte[8];
            double temp1;
            double temp2;
            double temp3;
            double temp4;
            if (!double.TryParse(tbTargetP1.Text.Trim(), out temp1))
            {
                MessageBox.Show("请输入正确的数值");
                return;
            }
            if (!double.TryParse(tbTargetP2.Text.Trim(), out temp2))
            {
                MessageBox.Show("请输入正确的数值");
                return;
            }
            if (!double.TryParse(tbTargetP3.Text.Trim(), out temp3))
            {
                MessageBox.Show("请输入正确的数值");
                return;
            }
            if (!double.TryParse(tbTargetP4.Text.Trim(), out temp4))
            {
                MessageBox.Show("请输入正确的数值");
                return;
            }
            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)(temp3 * 100)), 0, buf, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)(temp4 * 100)), 0, buf, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)(temp1 * 100)), 0, buf, 4, 2);
            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)(temp2 * 100)), 0, buf, 6, 2);
            uint bufSize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufSize, 0, 0xE6);
            if (ret == 0)
            {
                MessageBox.Show("Set target Pressure error!");
            }
        }

        private void btnSet1_Click(object sender, EventArgs e)
        {
            SetTargetP();
        }
    }
}
