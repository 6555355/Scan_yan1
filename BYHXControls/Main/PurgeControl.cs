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
    public partial class PurgeControl : BYHXUserControl
    {
        private List<CheckBox> reservoirsButtons;
        public PurgeControl()
        {
            InitializeComponent();

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
            myPath.AddEllipse(2, 2, 16, 16);
        }

        private SPrinterProperty _property;
        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            buttonAllPurge.Enabled = status == JetStatusEnum.Ready || status == JetStatusEnum.Pause;
        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;

            for (int i = 0; i < this.reservoirsButtons.Count; i++)
            {
#if false
                if (i < 2)
                {
                    if (property.nWhiteInkNum > 0 && i < property.nWhiteInkNum)
                    {
                        reservoirsButtons[i].Text =string.Format("{0}({1})",
                            property.Get_ColorIndex(i + property.nColorNum - property.nWhiteInkNum),i+1);
                    }
                    else
                    {
                        reservoirsButtons[i].Visible = false;
                    }
                }
                else
                {
                    if (i - 2 < property.nColorNum - property.nWhiteInkNum)
                    {
                        reservoirsButtons[i].Text = string.Format("{0}({1})",property.Get_ColorIndex(i - 2),i+1);
                    }
                    else
                    {
                        reservoirsButtons[i].Visible = false;
                    }
                } 
#endif
                if (i < property.nColorNum)
                {
                    reservoirsButtons[i].Text = string.Format("{0}({1})", property.Get_ColorIndex(i), i + 1);
                    reservoirsButtons[i].Visible =true;
                }
                else
                {
                    reservoirsButtons[i].Visible = false;
                }
            }
        }

        private void buttonAllPurge_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[1];
            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                if (reservoirsButtons[i].Checked)
                {
                    buf[0] |= (byte)(1 << (7-i));
                } 
            }
            uint bufsize = (uint) buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x52);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge commond error!");
            }
            buttonAllPurge.Enabled = false;
        }

        private void buttonPurgeOff_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[1];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x52);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge off commond error!");
            }
            buttonAllPurge.Enabled = true;
        }
        
    }
}
