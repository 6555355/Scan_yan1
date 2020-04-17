using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    public partial class MotionSetting_DY : ByhxBaseChildForm
    {
        public MotionSetting_DY()
        {
            InitializeComponent();
        }

        private void btnReFresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void GetData()
        {
            try
            {
                HapondMotorParam_Auto data = new HapondMotorParam_Auto();
                if (EpsonLCD.GetMotionSetting(ref data))
                {
                    comboBoxControlMode.SelectedIndex = (int)data.sysMode;
                    comboBoxMotorType.SelectedIndex = (int)data.motorType;
                    numericUpDownTorque.Value = data.torValue;
                    numericUpDownPrintHeadHeight.Value = data.headAdjustHei;
                    numericUpDownLV.Value = data.torSwitchStepVal;
                    numericUpDownLT.Value = data.torSwitchStepTime;
                    numericUpDownTimeout.Value = data.StepTimeOut;
                    comboBoxSwitchType.SelectedIndex = (int)data.SwitchType;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetData()
        {
            try
            {
                HapondMotorParam_Auto data = new HapondMotorParam_Auto();
                data.Flag = new char[] { 'Y', 'T', 'O', 'Q' };
                data.sysMode = (byte)(comboBoxControlMode.SelectedIndex);
                data.motorType = (byte)(comboBoxMotorType.SelectedIndex);
                data.torValue = (short)numericUpDownTorque.Value;
                data.headAdjustHei = (uint)numericUpDownPrintHeadHeight.Value;
                data.torSwitchStepVal = (short)numericUpDownLV.Value;
                data.torSwitchStepTime = (short)numericUpDownLT.Value;
                data.StepTimeOut = (short)numericUpDownTimeout.Value;
                data.SwitchType = (byte)(comboBoxSwitchType.SelectedIndex);

                EpsonLCD.SetMotionSetting(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

    
}
