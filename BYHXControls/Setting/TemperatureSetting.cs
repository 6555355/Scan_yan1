using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Setting
{
    public partial class TemperatureSetting : UserControl
    {
        private List<NumericUpDown> _setTemps;
        public TemperatureSetting()
        {
            InitializeComponent();
            _setTemps = new List<NumericUpDown>()
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
            numHeatingProtectionTemp.Enabled = (byte)PubFunc.GetUserPermission() >= (byte)UserPermission.FactoryUser;
        }

        private void TemperatureSetting_Load(object sender, EventArgs e)
        {
            try
            {
                GetCurrentTemperature();
                GetExtHeadOverHeat();
            }
            catch { }
        }

        private void GetCurrentTemperature()
        {
            float[] settemps = new float[8];
            byte[] buf = new byte[64];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x92, buf, ref bufsize, 0, 0xE0);
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
        /// <summary>
        /// 获取扩展板加热保护温度.并显示到界面
        /// </summary>
        private void GetExtHeadOverHeat()
        {
            byte[] buf = new byte[64];
            uint bufsize = (uint)buf.Length;
            int ret = CoreInterface.GetEpsonEP0Cmd(0x7f, buf, ref bufsize, 0, 3);
            if (ret != 0)
            {
               ExtHeadOverHeat overHeat = (ExtHeadOverHeat) SerializationUnit.BytesToStruct(buf.Skip(2).ToArray(), typeof (ExtHeadOverHeat));
                numHeatingProtectionTemp.Value = (decimal) (overHeat.OverHeatTmp/100f);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SetCurrentTemperature();
            SetExtHeadOverHeat();
        }

        private void SetCurrentTemperature()
        {
            List<byte> bytebuf = new List<byte>();
            short[] buf = new short[8];
            for (int i = 0; i < _setTemps.Count; i++)
            {
                float temp = 0;
                if (float.TryParse(_setTemps[i].Text, out temp))
                {
                    buf[i] = (short)(temp * 100);
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

        /// <summary>
        /// 设置扩展板加热保护温度
        /// </summary>
        private void SetExtHeadOverHeat()
        {
            ExtHeadOverHeat headOverHeat = new ExtHeadOverHeat();
            headOverHeat.Flag = new char[] { 'H', 'H', 'O', 'T' };
            headOverHeat.OverHeatTmp = (ushort) (numHeatingProtectionTemp.Value*100);
            byte[] buf = SerializationUnit.StructToBytes(headOverHeat);
            uint bufsize = (uint) buf.Length;
            int ret = CoreInterface.SetEpsonEP0Cmd(0x7f, buf, ref bufsize, 0, 3);
            if (ret == 0)
            {
                MessageBox.Show("Set Over Heat temp error!");
            }


        }

    }

    public struct ExtHeadOverHeat
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] 
        public char[] Flag; //!< 'WICF'//white ink cycle flag
        public ushort OverHeatTmp; // 系数0.01摄氏度,扩展板加热保护温度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)] 
        public byte[] rev;
    }
}
