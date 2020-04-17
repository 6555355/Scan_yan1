using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class DoubleYAxisSetting : BYHXUserControl
    {
        public DoubleYAxisSetting()
        {
            InitializeComponent();

            numDoubleYMoveLen.Minimum = 0;
            numDoubleYMoveLen.Maximum = int.MaxValue;
            comboBoxDoubleYMoveDir.SelectedIndex = 0;
            comboBoxDoubleYAxis.SelectedIndex = 1;
            numDoubleYMoveLen.Value = 500;
        }
        SPrinterSetting _ss;
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            _ss = ss;
            GZClothMotionParam cc = new GZClothMotionParam();
            DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
            if (EpsonLCD.GetGZClothMotionParam(ref cc) && EpsonLCD.GetDoubleYAxis_Info(ref doubleYaxis))
            {
                rollingMotorCheckBox.Checked = cc.enable != 0;
                comboBoxMotorMode.SelectedIndex = (cc.mode - 1);
                numericUpDownMotorSpeed.Value = cc.speed;
                numericUpDownDoubeYRatio.Value = (decimal)doubleYaxis.DoubeYRatio;
            }
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            groupBoxMotorParam.Visible = PubFunc.IsDoubleYAxis();
        }

        private void buttonMoveY_Click(object sender, EventArgs e)
        {
            if (comboBoxDoubleYAxis.SelectedIndex != -1
                && comboBoxDoubleYMoveDir.SelectedIndex != -1
                && numDoubleYMoveLen.Value > 0)
            {
                byte[] buf = new byte[10];
                uint buflen = (uint)buf.Length;
                buf[0] = (byte)(comboBoxDoubleYAxis.SelectedIndex == 0 ? 0x02 : 0x10);
                buf[1] = (byte)(comboBoxDoubleYMoveDir.SelectedIndex == 0 ? 1 : 0);//正:1,反:0
                Buffer.BlockCopy(BitConverter.GetBytes((int)_ss.sMoveSetting.nYMoveSpeed), 0, buf, 2, 4);
                Buffer.BlockCopy(BitConverter.GetBytes((int)numDoubleYMoveLen.Value), 0, buf, 6, 4);
                CoreInterface.SetEpsonEP0Cmd(0x85, buf, ref buflen, 0, 0);
            }
            else
            {
                MessageBox.Show("Input can't be empty.");
            }
        }
        private void buttonMotorGet_Click(object sender, EventArgs e)
        {
            GZClothMotionParam cc = new GZClothMotionParam();
            DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
            if (EpsonLCD.GetGZClothMotionParam(ref cc) && EpsonLCD.GetDoubleYAxis_Info(ref doubleYaxis))
            {
                rollingMotorCheckBox.Checked = cc.enable != 0;
                comboBoxMotorMode.SelectedIndex = (cc.mode - 1);
                numericUpDownMotorSpeed.Value = cc.speed;
                numericUpDownDoubeYRatio.Value = (decimal)doubleYaxis.DoubeYRatio;
            }
            else
            {
                MessageBox.Show("读取失败.");
            }
        }

        private void buttonMotorSet_Click(object sender, EventArgs e)
        {
            if (numericUpDownDoubeYRatio.Value == 0)
            {
                MessageBox.Show("双轴偏差比例系数不能为0.");
                return;
            }
            GZClothMotionParam cc = new GZClothMotionParam
            {
                enable = (byte)(rollingMotorCheckBox.Checked == true ? 1 : 0),
                mode = (byte)(comboBoxMotorMode.SelectedIndex + 1),
                speed = (uint)numericUpDownMotorSpeed.Value
            };
            DOUBLE_YAXIS doubleYaxis = new DOUBLE_YAXIS();
            if (EpsonLCD.GetDoubleYAxis_Info(ref doubleYaxis))
            {
                doubleYaxis.DoubeYRatio = (float)numericUpDownDoubeYRatio.Value;
                MessageBox.Show(EpsonLCD.SetGZClothMotionParam(cc) && EpsonLCD.SetDoubleYAxis_Info(doubleYaxis)
                    ? "设置成功."
                    : "设置失败.");
            }
            else
            {
                MessageBox.Show("设置失败");
            }
        }

        private void buttonRetractableRoll_MouseDown(object sender, MouseEventArgs e)
        {
            byte[] buf = new byte[10];
            uint buflen = (uint)buf.Length;
            buf[0] = 0x10;
            buf[1] = 1;//正:1,反:0
            Buffer.BlockCopy(BitConverter.GetBytes((int)_ss.sMoveSetting.nYMoveSpeed), 0, buf, 2, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, buf, 6, 4);
            CoreInterface.SetEpsonEP0Cmd(0x85, buf, ref buflen, 0, 0);
        }

        private void buttonRetractableRoll_MouseUp(object sender, MouseEventArgs e)
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
        }

        private void buttonRetractableUnroll_MouseDown(object sender, MouseEventArgs e)
        {
            byte[] buf = new byte[10];
            uint buflen = (uint)buf.Length;
            buf[0] = 0x10;
            buf[1] = 0;//正:1,反:0
            Buffer.BlockCopy(BitConverter.GetBytes((int)_ss.sMoveSetting.nYMoveSpeed), 0, buf, 2, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, buf, 6, 4);
            CoreInterface.SetEpsonEP0Cmd(0x85, buf, ref buflen, 0, 0);
        }

        private void buttonRetractableUnroll_MouseUp(object sender, MouseEventArgs e)
        {
            CoreInterface.SendJetCommand((int)JetCmdEnum.StopMove, 0);
        }

    }
}
