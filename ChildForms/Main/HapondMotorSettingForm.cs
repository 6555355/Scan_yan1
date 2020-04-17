using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    public partial class HapondMotorSettingForm : Form
    {
        public HapondMotorSettingForm()
        {
            InitializeComponent();
        }

        private List<ComboBox> modeBoxs;
        private List<ComboBox> dirBoxs;
        private List<NumericUpDown> values; 
        HapondPrintParam hapondPrintParam = new HapondPrintParam();
        private void HapondMotorSettingForm_Load(object sender, EventArgs e)
        {
            modeBoxs = new List<ComboBox>()
            {
                comboBoxMode1,comboBoxMode2,comboBoxMode3,comboBoxMode4,comboBoxMode5,comboBoxMode6
            };
            dirBoxs = new List<ComboBox>()
            {
                comboBoxDir1,comboBoxDir2,comboBoxDir3,comboBoxDir4,comboBoxDir5,comboBoxDir6
            };
            values = new List<NumericUpDown>()
            {
                numericUpDown1,numericUpDown2,numericUpDown3,numericUpDown4,numericUpDown5,numericUpDown6
            };

            for (int i = 0; i < modeBoxs.Count; i++)
            {
                 modeBoxs[i].Items.Clear();
                WorkMode[] modes = (WorkMode[]) Enum.GetValues(typeof (WorkMode));
                for (int j = 0; j < modes.Length; j++)
                {
                    modeBoxs[i].Items.Add(modes[j].ToString());
                }

                dirBoxs[i].Items.Clear();
                MotorDIR[] dirs = (MotorDIR[])Enum.GetValues(typeof(MotorDIR));
                for (int j = 0; j < dirs.Length; j++)
                {
                    dirBoxs[i].Items.Add(dirs[j].ToString());
                }
            }
            hapondPrintParam = new HapondPrintParam(){MotorTors = new MotorTorParam[6]};
            byte[] buf = new byte[Marshal.SizeOf(hapondPrintParam)+2];
            uint len = (uint) buf.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref len, 0, 0x0100);
            if (ret != 0)
            {
                byte[] data = buf.Skip(2).Take((int)len).ToArray();
                hapondPrintParam = (HapondPrintParam)PubFunc.BytesToStruct(data, typeof(HapondPrintParam));
                string flag = new string(hapondPrintParam.Flag);
                if (flag == "HAPD")
                {
                    for (int i = 0; i < hapondPrintParam.MotorTors.Length; i++)
                    {
                        if (i == 2 || i == 3)
                        {
                            modeBoxs[i].SelectedIndex = hapondPrintParam.MotorTors[i].mode;
                        }
                        else
                        {
                            modeBoxs[i].SelectedIndex = 1;
                        }
                        dirBoxs[i].SelectedIndex = hapondPrintParam.MotorTors[i].dir;
                        values[i].Value = hapondPrintParam.MotorTors[i].value;
                    }
                }
                else
                {
                    MessageBox.Show(@"Get HapondPrintParam Failed");
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            hapondPrintParam.Flag = new char[] { 'H', 'A', 'P', 'D' };
            for (int i = 0; i < hapondPrintParam.MotorTors.Length; i++)
            {
                MotorTorParam motorTorParam = hapondPrintParam.MotorTors[i];
                if (i == 2 || i == 3)
                {
                    motorTorParam.mode = (byte)modeBoxs[i].SelectedIndex;
                }
                else
                {
                    motorTorParam.mode = 1;
                }
                motorTorParam.dir = (byte)dirBoxs[i].SelectedIndex;
                motorTorParam.value=(ushort) values[i].Value;
                hapondPrintParam.MotorTors[i] = motorTorParam;
            }
            byte[] buf = PubFunc.StructToBytes(hapondPrintParam);
            uint len = (uint)buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x92, buf, ref len, 0, 0x0100);
            if (ret == 0)
            {
                MessageBox.Show("应用电机参数失败!");
            }
        }

        public void OnPrinterStatusChanged(JetStatusEnum status)
        {
            groupBox3.Enabled = groupBox4.Enabled = status != JetStatusEnum.Busy && status != JetStatusEnum.Pause && status != JetStatusEnum.Aborting && status != JetStatusEnum.Moving;
        }
    }

    public struct MotorTorParam
    {
        public byte mode;
        public byte dir; //方向单独列出来，只为bushound好调试
        public ushort value; //控制扭矩的大小
    }

    public struct HapondPrintParam
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Flag; // 'HAPD'
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] 
        public MotorTorParam[] MotorTors;
    }

    public enum WorkMode : byte
    {
        POSION = 0, //位置模式
        TORQUEE = 1, //扭矩模式
    }

    public enum MotorDIR : byte
    {
        REV = 0,
        POS = 1,
    }
}