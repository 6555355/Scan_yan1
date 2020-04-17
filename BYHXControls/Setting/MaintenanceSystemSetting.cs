using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using PrinterStubC.VenderSetting;

namespace BYHXPrinterManager.Setting
{
    public partial class MaintenanceSystemSetting : BYHXUserControl, IVenderSetting<AllWin_Paras>
    {
        private List<CheckBox> reservoirsButtons;
        private SPrinterProperty _property;

        public MaintenanceSystemSetting()
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
        }


        public void OnStatusDataChanged(byte[] buf)
        {

        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            _property = property;

            for (int i = 0; i < this.reservoirsButtons.Count; i++)
            {
                if (i < 2)
                {
                    if (property.nWhiteInkNum > 0 && i < property.nWhiteInkNum)
                    {
                        reservoirsButtons[i].Text = string.Format("{0}({1})",
                            property.Get_ColorIndex(i + property.nColorNum - property.nWhiteInkNum), i + 1);
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
                        reservoirsButtons[i].Text = string.Format("{0}({1})", property.Get_ColorIndex(i - 2), i + 1);
                    }
                    else
                    {
                        reservoirsButtons[i].Visible = false;
                    }
                }
            }
        }

        private void buttonAllPurge_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[3]{0x05,0x21,0};
            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                if (reservoirsButtons[i].Checked)
                {
                    buf[2] |= (byte)(1 << (7 - i));
                }
            }
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge commond error!");
            }
            buttonAllPurge.Enabled = false;
        }

        private void buttonPurgeOff_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[3] { 0x05, 0x21, 0 };
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send Purge off commond error!");
            }
            for (int i = 0; i < reservoirsButtons.Count; i++)
            {
                reservoirsButtons[i].Checked = false;
            }
            buttonAllPurge.Enabled = true;
        }

        private void buttonPumpReset_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[2] { 0x05, 0x22 };
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send pump reset commond error!");
            }
        }

        private void buttonFuyaReset_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[2] { 0x05, 0x23 };
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send negative pressure  reset commond error!");
            }
        }

        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[2] { 0x05, 0x2 };
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send shutdown commond error!");
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[10];
            buf[0] = 0x05;
            buf[1] = 0x11;
            buf[2] = (byte) num2LevelInkTankTemp.Value;
            buf[3] = (byte) numColorHeatTemp.Value;
            buf[4] = (byte)numWhiteHeatTemp.Value;
            buf[5] = (byte) numPumpTimeOut.Value;
            buf[6] = (byte) numWhiteCycleActionTime.Value;
            buf[7] = (byte) numWhiteCyclePauseTime.Value;
            buf[8] = (byte) numWhiteMixActionTime.Value;
            buf[9] = (byte) numWhiteMixPauseTime.Value;
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0x51);
            if (ret == 0)
            {
                MessageBox.Show("Send Auxiliary control parameters error!");
            }
            //else
            {
                AllWin_Paras setting= new AllWin_Paras();
                setting.InkTankTemp = (byte)num2LevelInkTankTemp.Value;
                setting.ColorHeatTemp = (byte)numColorHeatTemp.Value;
                setting.WhiteHeatTemp = (byte)numWhiteHeatTemp.Value;
                setting.PumpTimeOut = (byte)numPumpTimeOut.Value;
                setting.WhiteCycleActionTime = (byte)numWhiteCycleActionTime.Value;
                setting.WhiteCyclePauseTime = (byte)numWhiteCyclePauseTime.Value;
                setting.WhiteMixActionTime = (byte)numWhiteMixActionTime.Value;
                setting.WhiteMixPauseTime = (byte)numWhiteMixPauseTime.Value;
                SaveVenderSettings(setting);
            }
        }

        private void MaintenanceSystemSetting_Load(object sender, EventArgs e)
        {
            AllWin_Paras? setting1 = LoadVenderSettings();
            if (setting1.HasValue)
            {
                AllWin_Paras setting = setting1.Value;
                num2LevelInkTankTemp.Value = setting.InkTankTemp;
                numColorHeatTemp.Value = setting.ColorHeatTemp;
                numWhiteHeatTemp.Value = setting.WhiteHeatTemp;
                numPumpTimeOut.Value = setting.PumpTimeOut;
                numWhiteCycleActionTime.Value = setting.WhiteCycleActionTime;
                numWhiteCyclePauseTime.Value = setting.WhiteCyclePauseTime;
                numWhiteMixActionTime.Value = setting.WhiteMixActionTime;
                numWhiteMixPauseTime.Value = setting.WhiteMixPauseTime;
            }
        }

        public bool SaveVenderSettings(AllWin_Paras setting)
        {
            string path = PubFunc.GetVenderSettingPath();
            if (string.IsNullOrEmpty(path))
                return false;
            XmlSerializer xml = new XmlSerializer(typeof(AllWin_Paras));
            using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml.Serialize(stream, setting);
            }

            return true;
        }

        public AllWin_Paras? LoadVenderSettings()
        {
            AllWin_Paras? setting = null;
            string path = PubFunc.GetVenderSettingPath();
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return setting;
            XmlSerializer xml = new XmlSerializer(typeof(AllWin_Paras));
            using (Stream stream = new FileStream(path,
                                                  FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                setting = (AllWin_Paras)xml.Deserialize(stream);
                stream.Close();
            }
            return setting; 
        }
    }

    [Serializable]
    public struct AllWin_Paras
    {
        public byte InkTankTemp;
        public byte ColorHeatTemp;
        public byte WhiteHeatTemp;
        public byte PumpTimeOut;
        public byte WhiteCycleActionTime;
        public byte WhiteCyclePauseTime;
        public byte WhiteMixActionTime;
        public byte WhiteMixPauseTime;
    }

}
